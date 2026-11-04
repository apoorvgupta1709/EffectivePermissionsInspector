using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using EffectivePermissionsInspector.AppCode.Models;

namespace EffectivePermissionsInspector.AppCode.Services
{
    public class SimulationEngine
    {
        private readonly IOrganizationService _service;

        public SimulationEngine(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public SimulationResult SimulateImpact(
            EffectivePermissionAnalysisResult baseline,
            List<RoleItem> rolesToAdd,
            List<TeamItem> teamsToAdd)
        {
            var result = new SimulationResult
            {
                Baseline = baseline,
                AddedRoles = rolesToAdd.Select(r => r.Name).ToList(),
                AddedTeams = teamsToAdd.Select(t => t.Name).ToList()
            };

            // Deep clone baseline to create simulated target
            var sim = new EffectivePermissionAnalysisResult
            {
                User = baseline.User,
                Record = baseline.Record,
                InspectedAtUtc = DateTime.UtcNow
            };

            // Copy baseline rights
            foreach (var r in baseline.AccessRights)
            {
                sim.AccessRights.Add(new AccessRightItem
                {
                    RightName = r.RightName,
                    IsGranted = r.IsGranted,
                    SourceLayer = r.SourceLayer,
                    SourceDetails = r.SourceDetails,
                    PrivilegeDepth = r.PrivilegeDepth,
                    Reason = r.Reason
                });
            }

            // Check if adding teams makes user a member of the owning team
            bool joinsOwnerTeam = teamsToAdd.Any(t => t.TeamId == baseline.Record.OwnerId);
            if (joinsOwnerTeam)
            {
                foreach (var right in sim.AccessRights)
                {
                    if (!right.IsGranted)
                    {
                        right.IsGranted = true;
                        right.SourceLayer = AccessLayer.TeamSecurityRole;
                        right.SourceDetails = $"Simulated Membership in Owner Team ({baseline.Record.OwnerName})";
                        right.Reason = $"Unlocked: Adding user to '{baseline.Record.OwnerName}' grants direct owner-level access.";
                        result.GrantedChanges.Add($"[{right.RightName}] granted by joining Owner Team '{baseline.Record.OwnerName}'");
                    }
                }
            }
            else
            {
                // Check privileges granted by added roles
                var roleIds = new HashSet<Guid>(rolesToAdd.Select(r => r.RoleId));

                // If added teams have roles, include them
                if (teamsToAdd.Count > 0)
                {
                    var teamRoleIds = GetRolesForTeams(teamsToAdd.Select(t => t.TeamId));
                    foreach (var trId in teamRoleIds) roleIds.Add(trId);
                }

                if (roleIds.Count > 0)
                {
                    var grantedPrivileges = GetPrivilegesForRoles(roleIds, baseline.Record.EntityLogicalName);
                    bool isSameBU = baseline.User.BusinessUnitId == baseline.Record.OwningBusinessUnitId;

                    foreach (var right in sim.AccessRights)
                    {
                        if (!right.IsGranted && grantedPrivileges.TryGetValue(right.RightName, out int depthMask))
                        {
                            // depthMask: 1 = Basic (User), 2 = Local (BU), 4 = Deep (Parent-Child), 8 = Global (Org)
                            bool canAccess = false;
                            string depthDesc = "User";

                            if ((depthMask & 8) != 0) // Global
                            {
                                canAccess = true;
                                depthDesc = "Organization (Global)";
                            }
                            else if ((depthMask & 4) != 0) // Deep
                            {
                                canAccess = true;
                                depthDesc = "Parent-Child BU (Deep)";
                            }
                            else if ((depthMask & 2) != 0 && isSameBU) // Local
                            {
                                canAccess = true;
                                depthDesc = "Business Unit (Local)";
                            }

                            if (canAccess)
                            {
                                right.IsGranted = true;
                                right.SourceLayer = AccessLayer.DirectSecurityRole;
                                right.SourceDetails = "Simulated Role Privilege";
                                right.PrivilegeDepth = depthDesc;
                                right.Reason = $"Simulated unlock via added role privilege ({depthDesc} depth).";
                                result.GrantedChanges.Add($"[{right.RightName}] unlocked with {depthDesc} access.");
                            }
                        }
                    }
                }
            }

            result.Simulated = sim;
            return result;
        }

        private List<Guid> GetRolesForTeams(IEnumerable<Guid> teamIds)
        {
            var roleIds = new List<Guid>();
            string idsXml = string.Join("", teamIds.Select(id => $"<value>{id}</value>"));

            string fetch = $@"
                <fetch>
                  <entity name='role'>
                    <attribute name='roleid' />
                    <link-entity name='teamroles' from='roleid' to='roleid' intersect='true'>
                      <filter>
                        <condition attribute='teamid' operator='in'>
                          {idsXml}
                        </condition>
                      </filter>
                    </link-entity>
                  </entity>
                </fetch>";

            try
            {
                var res = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in res.Entities) roleIds.Add(r.Id);
            }
            catch { }

            return roleIds;
        }

        private Dictionary<string, int> GetPrivilegesForRoles(HashSet<Guid> roleIds, string entityLogicalName)
        {
            // Maps RightName -> Max Depth Mask
            var map = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            string idsXml = string.Join("", roleIds.Select(id => $"<value>{id}</value>"));

            string fetch = $@"
                <fetch>
                  <entity name='roleprivileges'>
                    <attribute name='privilegedepthmask' />
                    <link-entity name='privilege' from='privilegeid' to='privilegeid'>
                      <attribute name='name' alias='PrivName' />
                      <attribute name='accessright' alias='AccessRight' />
                    </link-entity>
                    <filter>
                      <condition attribute='roleid' operator='in'>
                        {idsXml}
                      </condition>
                    </filter>
                  </entity>
                </fetch>";

            try
            {
                var res = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var r in res.Entities)
                {
                    int depth = r.Contains("privilegedepthmask") ? Convert.ToInt32(r["privilegedepthmask"]) : 0;
                    string privName = r.Contains("PrivName") ? ((AliasedValue)r["PrivName"]).Value?.ToString() ?? string.Empty : string.Empty;

                    if (!string.IsNullOrEmpty(privName) && privName.IndexOf(entityLogicalName, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string right = DeriveRightFromPrivilegeName(privName);
                        if (!string.IsNullOrEmpty(right))
                        {
                            if (!map.TryGetValue(right, out int currentDepth) || depth > currentDepth)
                            {
                                map[right] = depth;
                            }
                        }
                    }
                }
            }
            catch { }

            return map;
        }

        private static string DeriveRightFromPrivilegeName(string privName)
        {
            if (privName.StartsWith("prvRead", StringComparison.OrdinalIgnoreCase)) return "Read";
            if (privName.StartsWith("prvWrite", StringComparison.OrdinalIgnoreCase)) return "Write";
            if (privName.StartsWith("prvDelete", StringComparison.OrdinalIgnoreCase)) return "Delete";
            if (privName.StartsWith("prvAppendTo", StringComparison.OrdinalIgnoreCase)) return "AppendTo";
            if (privName.StartsWith("prvAppend", StringComparison.OrdinalIgnoreCase)) return "Append";
            if (privName.StartsWith("prvAssign", StringComparison.OrdinalIgnoreCase)) return "Assign";
            if (privName.StartsWith("prvShare", StringComparison.OrdinalIgnoreCase)) return "Share";
            return string.Empty;
        }
    }
}
