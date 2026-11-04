using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using EffectivePermissionsInspector.AppCode.Models;

namespace EffectivePermissionsInspector.AppCode.Services
{
    public class SharingAnalyzer
    {
        private readonly IOrganizationService _service;

        public SharingAnalyzer(IOrganizationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public List<SharingItem> RetrieveSharingGrants(
            Guid recordId,
            Guid userId,
            List<TeamItem> userTeams,
            Action<string>? statusCallback = null)
        {
            statusCallback?.Invoke("Inspecting PrincipalObjectAccess (POA) sharing table...");

            var results = new List<SharingItem>();
            var principalIds = new HashSet<Guid> { userId };
            var teamMap = new Dictionary<Guid, string>();

            foreach (var t in userTeams)
            {
                principalIds.Add(t.TeamId);
                teamMap[t.TeamId] = t.Name;
            }

            string idsXml = string.Join("", principalIds.Select(id => $"<value>{id}</value>"));

            string fetch = $@"
                <fetch>
                  <entity name='principalobjectaccess'>
                    <attribute name='principalobjectaccessid' />
                    <attribute name='principalid' />
                    <attribute name='principaltypecode' />
                    <attribute name='accessrightsmask' />
                    <attribute name='inheritedaccessrightsmask' />
                    <filter type='and'>
                      <condition attribute='objectid' operator='eq' value='{recordId}' />
                      <condition attribute='principalid' operator='in'>
                        {idsXml}
                      </condition>
                    </filter>
                  </entity>
                </fetch>";

            try
            {
                var response = _service.RetrieveMultiple(new FetchExpression(fetch));
                foreach (var poa in response.Entities)
                {
                    var principalRef = poa.GetAttributeValue<EntityReference>("principalid");
                    int mask = poa.Contains("accessrightsmask") ? Convert.ToInt32(poa["accessrightsmask"]) : 0;
                    int inheritedMask = poa.Contains("inheritedaccessrightsmask") ? Convert.ToInt32(poa["inheritedaccessrightsmask"]) : 0;
                    int combinedMask = mask | inheritedMask;

                    Guid pId = principalRef?.Id ?? Guid.Empty;
                    bool isDirect = pId == userId;

                    results.Add(new SharingItem
                    {
                        PrincipalId = pId,
                        PrincipalType = isDirect ? "User" : "Team",
                        PrincipalName = isDirect ? "User Direct Share" : (teamMap.TryGetValue(pId, out var tn) ? tn : "Team Share"),
                        AccessRightsMask = combinedMask,
                        IsDirectToUser = isDirect,
                        ViaTeamName = isDirect ? string.Empty : (teamMap.TryGetValue(pId, out var teamName) ? teamName : string.Empty),
                        GrantedRightsSummary = DecodeAccessRightsMask(combinedMask)
                    });
                }
            }
            catch (Exception ex)
            {
                statusCallback?.Invoke($"Note: Could not query POA table directly ({ex.Message}).");
            }

            return results;
        }

        public static string DecodeAccessRightsMask(int mask)
        {
            var rights = new List<string>();
            if ((mask & (int)AccessRights.ReadAccess) != 0) rights.Add("Read");
            if ((mask & (int)AccessRights.WriteAccess) != 0) rights.Add("Write");
            if ((mask & (int)AccessRights.DeleteAccess) != 0) rights.Add("Delete");
            if ((mask & (int)AccessRights.AppendAccess) != 0) rights.Add("Append");
            if ((mask & (int)AccessRights.AppendToAccess) != 0) rights.Add("AppendTo");
            if ((mask & (int)AccessRights.AssignAccess) != 0) rights.Add("Assign");
            if ((mask & (int)AccessRights.ShareAccess) != 0) rights.Add("Share");

            return rights.Count > 0 ? string.Join(", ", rights) : "None";
        }
    }
}
