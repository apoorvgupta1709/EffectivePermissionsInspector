using System;
using System.Collections.Generic;

namespace EffectivePermissionsInspector.AppCode.Models
{
    public class EffectivePermissionAnalysisResult
    {
        public DateTime InspectedAtUtc { get; set; } = DateTime.UtcNow;
        public UserSummary User { get; set; } = new UserSummary();
        public RecordTarget Record { get; set; } = new RecordTarget();

        // 7 Core Dataverse Privileges
        public List<AccessRightItem> AccessRights { get; set; } = new List<AccessRightItem>();

        // Field-Level Security
        public List<FieldPermissionItem> FieldPermissions { get; set; } = new List<FieldPermissionItem>();

        // Roles & Teams
        public List<RoleItem> DirectRoles { get; set; } = new List<RoleItem>();
        public List<RoleItem> InheritedTeamRoles { get; set; } = new List<RoleItem>();
        public List<TeamItem> UserTeams { get; set; } = new List<TeamItem>();

        // Sharing / POA
        public List<SharingItem> SharingGrants { get; set; } = new List<SharingItem>();

        // Quick boolean convenience properties
        public bool CanRead => GetRight("Read")?.IsGranted ?? false;
        public bool CanWrite => GetRight("Write")?.IsGranted ?? false;
        public bool CanDelete => GetRight("Delete")?.IsGranted ?? false;
        public bool CanAppend => GetRight("Append")?.IsGranted ?? false;
        public bool CanAppendTo => GetRight("AppendTo")?.IsGranted ?? false;
        public bool CanAssign => GetRight("Assign")?.IsGranted ?? false;
        public bool CanShare => GetRight("Share")?.IsGranted ?? false;

        public AccessRightItem? GetRight(string name)
        {
            return AccessRights.Find(r => string.Equals(r.RightName, name, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class SimulationResult
    {
        public EffectivePermissionAnalysisResult Baseline { get; set; } = new EffectivePermissionAnalysisResult();
        public EffectivePermissionAnalysisResult Simulated { get; set; } = new EffectivePermissionAnalysisResult();
        public List<string> GrantedChanges { get; set; } = new List<string>();
        public List<string> AddedRoles { get; set; } = new List<string>();
        public List<string> AddedTeams { get; set; } = new List<string>();
    }
}
