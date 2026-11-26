using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using PermissionsInspector.AppCode.Models;

namespace PermissionsInspector.AppCode.Services
{
    public class FieldSecurityAnalyzer
    {
        private readonly IOrganizationService _service;

        public FieldSecurityAnalyzer(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public List<FieldPermissionItem> AnalyzeFieldSecurity(
            string entityLogicalName,
            Guid userId,
            List<Guid> userTeamIds,
            Action<string>? statusCallback = null)
        {
            statusCallback?.Invoke($"Scanning Field-Level Security on entity '{entityLogicalName}'...");

            var results = new List<FieldPermissionItem>();

            // 1. Retrieve Entity Attributes that have IsSecured == true
            var entityReq = new RetrieveEntityRequest
            {
                LogicalName = entityLogicalName,
                EntityFilters = EntityFilters.Attributes,
                RetrieveAsIfPublished = true
            };

            var entityResp = (RetrieveEntityResponse)_service.Execute(entityReq);
            var securedAttributes = entityResp.EntityMetadata.Attributes
                .Where(a => a.IsSecured == true)
                .ToList();

            if (securedAttributes.Count == 0)
            {
                return results; // No secured fields on this entity
            }

            // 2. Query all Field Security Profiles assigned to the User or User's Teams
            var profileIds = GetAssignedProfileIds(userId, userTeamIds);
            var profileNameMap = GetProfileNames(profileIds);

            // 3. Query FieldPermission records for this entity
            var fieldPermissions = GetFieldPermissionsForEntity(entityLogicalName, profileIds);

            foreach (var attr in securedAttributes)
            {
                string attrName = attr.LogicalName.ToLowerInvariant();
                string displayName = attr.DisplayName?.UserLocalizedLabel?.Label ?? attrName;

                var item = new FieldPermissionItem
                {
                    AttributeLogicalName = attrName,
                    AttributeDisplayName = displayName,
                    IsSecured = true
                };

                // Check permissions granted by user's assigned profiles
                var matchingPerms = fieldPermissions.Where(fp => string.Equals(fp.AttributeName, attrName, StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (var perm in matchingPerms)
                {
                    string profName = profileNameMap.TryGetValue(perm.ProfileId, out var name) ? name : "Profile";

                    if (perm.CanRead)
                    {
                        item.CanRead = true;
                        item.ReadSourceProfile = profName;
                    }

                    if (perm.CanCreate)
                    {
                        item.CanCreate = true;
                    }

                    if (perm.CanUpdate)
                    {
                        item.CanUpdate = true;
                        item.UpdateSourceProfile = profName;
                    }
                }

                if (!item.CanRead && !item.CanUpdate)
                {
                    item.BlockingReason = profileIds.Count == 0
                        ? "User has no Field Security Profiles assigned."
                        : "Assigned Field Security Profiles do not grant Read or Update.";
                }

                results.Add(item);
            }

            return results.OrderBy(r => r.AttributeDisplayName).ToList();
        }

        private HashSet<Guid> GetAssignedProfileIds(Guid userId, List<Guid> teamIds)
        {
            var profileIds = new HashSet<Guid>();

            // Query profiles directly assigned to user
            string userProfilesFetch = $@"
                <fetch>
                  <entity name='fieldsecurityprofile'>
                    <attribute name='fieldsecurityprofileid' />
                    <link-entity name='systemuserprofiles' from='fieldsecurityprofileid' to='fieldsecurityprofileid' intersect='true'>
                      <filter>
                        <condition attribute='systemuserid' operator='eq' value='{userId}' />
                      </filter>
                    </link-entity>
                  </entity>
                </fetch>";

            try
            {
                var userProfiles = _service.RetrieveMultiple(new FetchExpression(userProfilesFetch));
                foreach (var p in userProfiles.Entities)
                {
                    profileIds.Add(p.Id);
                }
            }
            catch { }

            // Query profiles assigned to user's teams
            if (teamIds != null && teamIds.Count > 0)
            {
                string teamIdsXml = string.Join("", teamIds.Select(id => $"<value>{id}</value>"));
                string teamProfilesFetch = $@"
                    <fetch>
                      <entity name='fieldsecurityprofile'>
                        <attribute name='fieldsecurityprofileid' />
                        <link-entity name='teamprofiles' from='fieldsecurityprofileid' to='fieldsecurityprofileid' intersect='true'>
                          <filter>
                            <condition attribute='teamid' operator='in'>
                              {teamIdsXml}
                            </condition>
                          </filter>
                        </link-entity>
                      </entity>
                    </fetch>";

                try
                {
                    var teamProfiles = _service.RetrieveMultiple(new FetchExpression(teamProfilesFetch));
                    foreach (var p in teamProfiles.Entities)
                    {
                        profileIds.Add(p.Id);
                    }
                }
                catch { }
            }

            return profileIds;
        }

        private Dictionary<Guid, string> GetProfileNames(HashSet<Guid> profileIds)
        {
            var map = new Dictionary<Guid, string>();
            if (profileIds.Count == 0) return map;

            string idsXml = string.Join("", profileIds.Select(id => $"<value>{id}</value>"));
            string fetch = $@"
                <fetch>
                  <entity name='fieldsecurityprofile'>
                    <attribute name='fieldsecurityprofileid' />
                    <attribute name='name' />
                    <filter>
                      <condition attribute='fieldsecurityprofileid' operator='in'>
                        {idsXml}
                      </condition>
                    </filter>
                  </entity>
                </fetch>";

            try
            {
                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    map[r.Id] = r.GetAttributeValue<string>("name") ?? "Profile";
                }
            }
            catch { }

            return map;
        }

        private List<(Guid ProfileId, string AttributeName, bool CanRead, bool CanCreate, bool CanUpdate)> GetFieldPermissionsForEntity(
            string entityLogicalName,
            HashSet<Guid> profileIds)
        {
            var list = new List<(Guid ProfileId, string AttributeName, bool CanRead, bool CanCreate, bool CanUpdate)>();
            if (profileIds.Count == 0) return list;

            string idsXml = string.Join("", profileIds.Select(id => $"<value>{id}</value>"));
            string fetch = $@"
                <fetch>
                  <entity name='fieldpermission'>
                    <attribute name='fieldsecurityprofileid' />
                    <attribute name='attributelogicalname' />
                    <attribute name='canread' />
                    <attribute name='cancreate' />
                    <attribute name='canupdate' />
                    <filter type='and'>
                      <condition attribute='entityname' operator='eq' value='{entityLogicalName}' />
                      <condition attribute='fieldsecurityprofileid' operator='in'>
                        {idsXml}
                      </condition>
                    </filter>
                  </entity>
                </fetch>";

            try
            {
                var records = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in records.Entities)
                {
                    var profRef = r.GetAttributeValue<EntityReference>("fieldsecurityprofileid");
                    string attrName = r.GetAttributeValue<string>("attributelogicalname") ?? string.Empty;
                    int readVal = r.Contains("canread") ? ((OptionSetValue)r["canread"]).Value : 0;
                    int createVal = r.Contains("cancreate") ? ((OptionSetValue)r["cancreate"]).Value : 0;
                    int updateVal = r.Contains("canupdate") ? ((OptionSetValue)r["canupdate"]).Value : 0;

                    if (profRef != null && !string.IsNullOrEmpty(attrName))
                    {
                        list.Add((profRef.Id, attrName, readVal == 4, createVal == 4, updateVal == 4));
                    }
                }
            }
            catch { }

            return list;
        }
    }
}
