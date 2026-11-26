using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using PermissionsInspector.AppCode.Models;

namespace PermissionsInspector.AppCode.Services
{
    public class SecurityInspectorEngine
    {
        private readonly IOrganizationService _service;
        private readonly FieldSecurityAnalyzer _fieldAnalyzer;
        private readonly SharingAnalyzer _sharingAnalyzer;

        public SecurityInspectorEngine(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _fieldAnalyzer = new FieldSecurityAnalyzer(service);
            _sharingAnalyzer = new SharingAnalyzer(service);
        }

        public EffectivePermissionAnalysisResult InspectEffectivePermissions(
            Guid userId,
            string entityLogicalName,
            Guid recordId,
            Action<int, string>? progressCallback = null)
        {
            var result = new EffectivePermissionAnalysisResult();

            // 1. Retrieve User Details
            progressCallback?.Invoke(10, "Retrieving User profile and Business Unit...");
            result.User = GetUserDetails(userId);

            // 2. Retrieve Target Record Details
            progressCallback?.Invoke(25, $"Querying record '{entityLogicalName}' ({recordId})...");
            result.Record = GetRecordDetails(entityLogicalName, recordId);

            // 3. Retrieve Direct User Roles
            progressCallback?.Invoke(40, "Querying user direct security roles...");
            result.DirectRoles = GetUserDirectRoles(userId);

            // 4. Retrieve User Teams & Inherited Roles
            progressCallback?.Invoke(55, "Querying user teams and team security roles...");
            result.UserTeams = GetUserTeams(userId);
            result.InheritedTeamRoles = GetTeamInheritedRoles(result.UserTeams);

            // 5. Query Sharing (POA table)
            progressCallback?.Invoke(70, "Inspecting PrincipalObjectAccess (POA) table...");
            result.SharingGrants = _sharingAnalyzer.RetrieveSharingGrants(recordId, userId, result.UserTeams);

            // 6. Execute Native RetrievePrincipalAccessRequest
            progressCallback?.Invoke(85, "Evaluating effective access rights via Dataverse Security Engine...");
            var principalAccess = CallRetrievePrincipalAccess(userId, entityLogicalName, recordId);

            // 7. Evaluate and Breakdown Each Right
            result.AccessRights = EvaluateRightsBreakdown(result, principalAccess);

            // 8. Field-Level Security Scan
            progressCallback?.Invoke(95, "Evaluating Field-Level Security profiles...");
            var teamIds = result.UserTeams.Select(t => t.TeamId).ToList();
            result.FieldPermissions = _fieldAnalyzer.AnalyzeFieldSecurity(entityLogicalName, userId, teamIds);

            progressCallback?.Invoke(100, "Security inspection completed successfully.");
            return result;
        }

        private AccessRights CallRetrievePrincipalAccess(Guid userId, string entityLogicalName, Guid recordId)
        {
            try
            {
                var req = new RetrievePrincipalAccessRequest
                {
                    Principal = new EntityReference("systemuser", userId),
                    Target = new EntityReference(entityLogicalName, recordId)
                };

                var resp = (RetrievePrincipalAccessResponse)_service.Execute(req);
                return resp.AccessRights;
            }
            catch
            {
                return AccessRights.None;
            }
        }

        private List<AccessRightItem> EvaluateRightsBreakdown(EffectivePermissionAnalysisResult result, AccessRights effective)
        {
            var rights = new List<AccessRightItem>();

            var rightDefinitions = new (string Name, AccessRights Flag)[]
            {
                ("Read", AccessRights.ReadAccess),
                ("Write", AccessRights.WriteAccess),
                ("Delete", AccessRights.DeleteAccess),
                ("Append", AccessRights.AppendAccess),
                ("AppendTo", AccessRights.AppendToAccess),
                ("Assign", AccessRights.AssignAccess),
                ("Share", AccessRights.ShareAccess)
            };

            bool isOwner = result.Record.IsUserOwned && result.Record.OwnerId == result.User.SystemUserId;
            bool isInOwnerTeam = result.Record.IsTeamOwned && result.UserTeams.Any(t => t.TeamId == result.Record.OwnerId);
            bool isSameBU = result.User.BusinessUnitId == result.Record.OwningBusinessUnitId;

            foreach (var def in rightDefinitions)
            {
                bool isGranted = (effective & def.Flag) != 0;
                var item = new AccessRightItem
                {
                    RightName = def.Name,
                    IsGranted = isGranted
                };

                if (isGranted)
                {
                    if (isOwner)
                    {
                        item.SourceLayer = AccessLayer.RecordOwner;
                        item.SourceDetails = "Record Owner";
                        item.PrivilegeDepth = "User (Basic)";
                        item.Reason = "User is the direct owner of this record.";
                    }
                    else if (isInOwnerTeam)
                    {
                        item.SourceLayer = AccessLayer.TeamSecurityRole;
                        item.SourceDetails = $"Owner Team: {result.Record.OwnerName}";
                        item.PrivilegeDepth = "User (Basic)";
                        item.Reason = $"User is a member of the owning team '{result.Record.OwnerName}'.";
                    }
                    else
                    {
                        var matchingShare = result.SharingGrants.FirstOrDefault(s => (s.AccessRightsMask & (int)def.Flag) != 0);
                        if (matchingShare != null)
                        {
                            item.SourceLayer = AccessLayer.SharingPOA;
                            item.SourceDetails = matchingShare.IsDirectToUser ? "Direct POA Share" : $"Shared via Team: {matchingShare.ViaTeamName}";
                            item.Reason = $"Granted via explicit sharing in PrincipalObjectAccess ({item.SourceDetails}).";
                        }
                        else if (result.DirectRoles.Count > 0)
                        {
                            item.SourceLayer = AccessLayer.DirectSecurityRole;
                            item.SourceDetails = string.Join(", ", result.DirectRoles.Take(2).Select(r => r.Name));
                            item.PrivilegeDepth = isSameBU ? "Business Unit (Local)" : "Organization (Global)";
                            item.Reason = $"Granted via direct security role privilege ({item.SourceDetails}).";
                        }
                        else if (result.InheritedTeamRoles.Count > 0)
                        {
                            item.SourceLayer = AccessLayer.TeamSecurityRole;
                            item.SourceDetails = string.Join(", ", result.InheritedTeamRoles.Take(2).Select(r => r.Name));
                            item.PrivilegeDepth = isSameBU ? "Business Unit (Local)" : "Organization (Global)";
                            item.Reason = $"Granted via team role inheritance ({item.SourceDetails}).";
                        }
                        else
                        {
                            item.SourceLayer = AccessLayer.HierarchySecurity;
                            item.SourceDetails = "Hierarchy / System";
                            item.Reason = "Granted via Manager Hierarchy or System Administrative privilege.";
                        }
                    }
                }
                else
                {
                    item.SourceLayer = AccessLayer.Denied;
                    item.SourceDetails = "Blocked";

                    if (!isSameBU)
                    {
                        item.Reason = $"Denied: User's BU ('{result.User.BusinessUnitName}') differs from Record's BU ('{result.Record.OwningBusinessUnitName}') and roles lack Deep/Global depth.";
                    }
                    else
                    {
                        item.Reason = $"Denied: User roles lack sufficient privilege on '{result.Record.EntityLogicalName}' and record is not shared with {def.Name} rights.";
                    }
                }

                rights.Add(item);
            }

            return rights;
        }

        private UserSummary GetUserDetails(Guid userId)
        {
            var user = new UserSummary { SystemUserId = userId };

            string fetch = $@"
                <fetch top='1'>
                  <entity name='systemuser'>
                    <attribute name='systemuserid' />
                    <attribute name='fullname' />
                    <attribute name='domainname' />
                    <attribute name='internalemailaddress' />
                    <attribute name='businessunitid' />
                    <attribute name='isdisabled' />
                    <attribute name='accessmode' />
                    <filter>
                      <condition attribute='systemuserid' operator='eq' value='{userId}' />
                    </filter>
                  </entity>
                </fetch>";

            var res = _service.RetrieveMultiple(new FetchExpression(fetch));
            if (res.Entities.Count > 0)
            {
                var e = res.Entities[0];
                user.FullName = e.GetAttributeValue<string>("fullname") ?? "Unknown";
                user.DomainName = e.GetAttributeValue<string>("domainname") ?? string.Empty;
                user.PrimaryEmail = e.GetAttributeValue<string>("internalemailaddress") ?? string.Empty;
                user.IsDisabled = e.GetAttributeValue<bool>("isdisabled");
                user.AccessMode = e.Contains("accessmode") ? ((OptionSetValue)e["accessmode"]).Value : 0;

                var buRef = e.GetAttributeValue<EntityReference>("businessunitid");
                if (buRef != null)
                {
                    user.BusinessUnitId = buRef.Id;
                    user.BusinessUnitName = buRef.Name ?? "Business Unit";
                }
            }

            return user;
        }

        private RecordTarget GetRecordDetails(string entityLogicalName, Guid recordId)
        {
            var target = new RecordTarget
            {
                EntityLogicalName = entityLogicalName,
                RecordId = recordId
            };

            var metaReq = new RetrieveEntityRequest
            {
                LogicalName = entityLogicalName,
                EntityFilters = EntityFilters.Entity,
                RetrieveAsIfPublished = true
            };

            string primaryNameAttr = "name";
            try
            {
                var metaResp = (RetrieveEntityResponse)_service.Execute(metaReq);
                target.EntityDisplayName = metaResp.EntityMetadata.DisplayName?.UserLocalizedLabel?.Label ?? entityLogicalName;
                primaryNameAttr = metaResp.EntityMetadata.PrimaryNameAttribute ?? "name";
            }
            catch
            {
                target.EntityDisplayName = entityLogicalName;
            }

            try
            {
                var record = _service.Retrieve(entityLogicalName, recordId, new ColumnSet("ownerid", "owningbusinessunit", primaryNameAttr));
                if (record != null)
                {
                    target.RecordName = record.Contains(primaryNameAttr) ? record[primaryNameAttr]?.ToString() ?? "Record" : "Record";
                    var ownerRef = record.GetAttributeValue<EntityReference>("ownerid");
                    if (ownerRef != null)
                    {
                        target.OwnerId = ownerRef.Id;
                        target.OwnerType = ownerRef.LogicalName;
                        target.OwnerName = ownerRef.Name ?? ownerRef.LogicalName;
                    }

                    var buRef = record.GetAttributeValue<EntityReference>("owningbusinessunit");
                    if (buRef != null)
                    {
                        target.OwningBusinessUnitId = buRef.Id;
                        target.OwningBusinessUnitName = buRef.Name ?? "Business Unit";
                    }
                }
            }
            catch (Exception ex)
            {
                target.RecordName = $"Error reading record: {ex.Message}";
            }

            return target;
        }

        private List<RoleItem> GetUserDirectRoles(Guid userId)
        {
            var roles = new List<RoleItem>();
            string fetch = $@"
                <fetch>
                  <entity name='role'>
                    <attribute name='roleid' />
                    <attribute name='name' />
                    <attribute name='businessunitid' />
                    <link-entity name='systemuserroles' from='roleid' to='roleid' intersect='true'>
                      <filter>
                        <condition attribute='systemuserid' operator='eq' value='{userId}' />
                      </filter>
                    </link-entity>
                  </entity>
                </fetch>";

            try
            {
                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    var buRef = r.GetAttributeValue<EntityReference>("businessunitid");
                    roles.Add(new RoleItem
                    {
                        RoleId = r.Id,
                        Name = r.GetAttributeValue<string>("name") ?? "Role",
                        BusinessUnitId = buRef?.Id ?? Guid.Empty,
                        BusinessUnitName = buRef?.Name ?? string.Empty,
                        IsInheritedFromTeam = false
                    });
                }
            }
            catch { }

            return roles;
        }

        private List<TeamItem> GetUserTeams(Guid userId)
        {
            var teams = new List<TeamItem>();
            string fetch = $@"
                <fetch>
                  <entity name='team'>
                    <attribute name='teamid' />
                    <attribute name='name' />
                    <attribute name='teamtype' />
                    <attribute name='businessunitid' />
                    <link-entity name='teammembership' from='teamid' to='teamid' intersect='true'>
                      <filter>
                        <condition attribute='systemuserid' operator='eq' value='{userId}' />
                      </filter>
                    </link-entity>
                  </entity>
                </fetch>";

            try
            {
                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    var buRef = r.GetAttributeValue<EntityReference>("businessunitid");
                    int teamTypeVal = r.Contains("teamtype") ? ((OptionSetValue)r["teamtype"]).Value : 0;
                    string teamTypeName = teamTypeVal switch
                    {
                        0 => "Owner",
                        1 => "Access",
                        2 => "Entra Security Group",
                        3 => "Entra Office Group",
                        _ => "Team"
                    };

                    teams.Add(new TeamItem
                    {
                        TeamId = r.Id,
                        Name = r.GetAttributeValue<string>("name") ?? "Team",
                        TeamType = teamTypeName,
                        BusinessUnitId = buRef?.Id ?? Guid.Empty,
                        BusinessUnitName = buRef?.Name ?? string.Empty
                    });
                }
            }
            catch { }

            return teams;
        }

        private List<RoleItem> GetTeamInheritedRoles(List<TeamItem> teams)
        {
            var roles = new List<RoleItem>();
            if (teams.Count == 0) return roles;

            string idsXml = string.Join("", teams.Select(t => $"<value>{t.TeamId}</value>"));
            string fetch = $@"
                <fetch>
                  <entity name='role'>
                    <attribute name='roleid' />
                    <attribute name='name' />
                    <attribute name='businessunitid' />
                    <link-entity name='teamroles' from='roleid' to='roleid' intersect='true'>
                      <filter>
                        <condition attribute='teamid' operator='in'>
                          {idsXml}
                        </condition>
                      </filter>
                      <link-entity name='team' from='teamid' to='teamid'>
                        <attribute name='name' alias='TeamName' />
                      </link-entity>
                    </link-entity>
                  </entity>
                </fetch>";

            try
            {
                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    var buRef = r.GetAttributeValue<EntityReference>("businessunitid");
                    string teamName = r.Contains("TeamName") ? ((AliasedValue)r["TeamName"]).Value?.ToString() ?? "Team" : "Team";

                    roles.Add(new RoleItem
                    {
                        RoleId = r.Id,
                        Name = r.GetAttributeValue<string>("name") ?? "Role",
                        BusinessUnitId = buRef?.Id ?? Guid.Empty,
                        BusinessUnitName = buRef?.Name ?? string.Empty,
                        IsInheritedFromTeam = true,
                        InheritedFromTeamName = teamName
                    });
                }
            }
            catch { }

            return roles;
        }

        public List<UserSummary> SearchUsers(string query, int maxResults = 1000)
        {
            var users = new List<UserSummary>();

            // Clean search query
            string trimmedQuery = (query ?? string.Empty).Trim();
            string searchFilterXml = string.Empty;

            if (!string.IsNullOrEmpty(trimmedQuery))
            {
                searchFilterXml = $@"
                    <filter type='or'>
                      <condition attribute='fullname' operator='like' value='%{trimmedQuery}%' />
                      <condition attribute='domainname' operator='like' value='%{trimmedQuery}%' />
                      <condition attribute='internalemailaddress' operator='like' value='%{trimmedQuery}%' />
                    </filter>";
            }

            // Exclude application users, bots, system stubs, and non-interactive users
            string fetch = $@"
                <fetch top='{maxResults}'>
                  <entity name='systemuser'>
                    <attribute name='systemuserid' />
                    <attribute name='fullname' />
                    <attribute name='domainname' />
                    <attribute name='internalemailaddress' />
                    <attribute name='businessunitid' />
                    <attribute name='isdisabled' />
                    <attribute name='accessmode' />
                    <filter type='and'>
                      <condition attribute='isdisabled' operator='eq' value='0' />
                      <condition attribute='accessmode' operator='ne' value='4' />
                      <condition attribute='fullname' operator='not-null' />
                      <condition attribute='fullname' operator='not-like' value='#%' />
                      <condition attribute='fullname' operator='not-like' value='SYSTEM%' />
                      <condition attribute='fullname' operator='not-like' value='INTEGRATION%' />
                      {searchFilterXml}
                    </filter>
                    <order attribute='fullname' />
                  </entity>
                </fetch>";

            try
            {
                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    var buRef = r.GetAttributeValue<EntityReference>("businessunitid");
                    users.Add(new UserSummary
                    {
                        SystemUserId = r.Id,
                        FullName = r.GetAttributeValue<string>("fullname") ?? "User",
                        DomainName = r.GetAttributeValue<string>("domainname") ?? string.Empty,
                        PrimaryEmail = r.GetAttributeValue<string>("internalemailaddress") ?? string.Empty,
                        BusinessUnitId = buRef?.Id ?? Guid.Empty,
                        BusinessUnitName = buRef?.Name ?? string.Empty,
                        IsDisabled = r.GetAttributeValue<bool>("isdisabled"),
                        AccessMode = r.Contains("accessmode") ? ((OptionSetValue)r["accessmode"]).Value : 0
                    });
                }
            }
            catch
            {
                // Fallback query if older CRM doesn't support specific filters
                try
                {
                    string fallbackFetch = $@"
                        <fetch top='500'>
                          <entity name='systemuser'>
                            <attribute name='systemuserid' />
                            <attribute name='fullname' />
                            <attribute name='domainname' />
                            <attribute name='internalemailaddress' />
                            <attribute name='businessunitid' />
                            <attribute name='isdisabled' />
                            <filter>
                              <condition attribute='isdisabled' operator='eq' value='0' />
                            </filter>
                            <order attribute='fullname' />
                          </entity>
                        </fetch>";

                    var fallbackRecords = _service.RetrieveMultiple(new FetchExpression(fallbackFetch));
                    foreach (var r in fallbackRecords.Entities)
                    {
                        var buRef = r.GetAttributeValue<EntityReference>("businessunitid");
                        users.Add(new UserSummary
                        {
                            SystemUserId = r.Id,
                            FullName = r.GetAttributeValue<string>("fullname") ?? "User",
                            DomainName = r.GetAttributeValue<string>("domainname") ?? string.Empty,
                            PrimaryEmail = r.GetAttributeValue<string>("internalemailaddress") ?? string.Empty,
                            BusinessUnitId = buRef?.Id ?? Guid.Empty,
                            BusinessUnitName = buRef?.Name ?? string.Empty,
                            IsDisabled = r.GetAttributeValue<bool>("isdisabled")
                        });
                    }
                }
                catch { }
            }

            return users;
        }

        public List<RoleItem> GetAllAvailableRoles()
        {
            var roles = new List<RoleItem>();
            string fetch = @"
                <fetch>
                  <entity name='role'>
                    <attribute name='roleid' />
                    <attribute name='name' />
                    <attribute name='businessunitid' />
                    <filter>
                      <condition attribute='parentroleid' operator='null' />
                    </filter>
                    <order attribute='name' />
                  </entity>
                </fetch>";

            try
            {
                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    var buRef = r.GetAttributeValue<EntityReference>("businessunitid");
                    roles.Add(new RoleItem
                    {
                        RoleId = r.Id,
                        Name = r.GetAttributeValue<string>("name") ?? "Role",
                        BusinessUnitId = buRef?.Id ?? Guid.Empty,
                        BusinessUnitName = buRef?.Name ?? string.Empty
                    });
                }
            }
            catch { }

            return roles;
        }

        public List<TeamItem> GetAllAvailableTeams()
        {
            var teams = new List<TeamItem>();
            string fetch = @"
                <fetch top='200'>
                  <entity name='team'>
                    <attribute name='teamid' />
                    <attribute name='name' />
                    <attribute name='teamtype' />
                    <attribute name='businessunitid' />
                    <order attribute='name' />
                  </entity>
                </fetch>";

            try
            {
                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    var buRef = r.GetAttributeValue<EntityReference>("businessunitid");
                    teams.Add(new TeamItem
                    {
                        TeamId = r.Id,
                        Name = r.GetAttributeValue<string>("name") ?? "Team",
                        BusinessUnitId = buRef?.Id ?? Guid.Empty,
                        BusinessUnitName = buRef?.Name ?? string.Empty
                    });
                }
            }
            catch { }

            return teams;
        }
    }
}
