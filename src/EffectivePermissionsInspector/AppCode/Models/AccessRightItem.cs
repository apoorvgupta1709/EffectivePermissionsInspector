using System;

namespace EffectivePermissionsInspector.AppCode.Models
{
    public class AccessRightItem
    {
        public string RightName { get; set; } = string.Empty; // Read, Write, Delete, Append, AppendTo, Assign, Share
        public bool IsGranted { get; set; }
        public AccessLayer SourceLayer { get; set; } = AccessLayer.Denied;
        public string SourceDetails { get; set; } = string.Empty; // e.g. "Sales Manager (Direct Role)", "POA Share from Alice"
        public string PrivilegeDepth { get; set; } = string.Empty; // User, BU, Parent-Child, Org
        public string Reason { get; set; } = string.Empty;
    }

    public class FieldPermissionItem
    {
        public string AttributeLogicalName { get; set; } = string.Empty;
        public string AttributeDisplayName { get; set; } = string.Empty;
        public bool IsSecured { get; set; } = true;

        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }

        public string ReadSourceProfile { get; set; } = string.Empty;
        public string UpdateSourceProfile { get; set; } = string.Empty;
        public string BlockingReason { get; set; } = string.Empty;
    }

    public class RoleItem
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; } = string.Empty;
        public bool IsInheritedFromTeam { get; set; }
        public string InheritedFromTeamName { get; set; } = string.Empty;
        public override string ToString() => IsInheritedFromTeam ? $"{Name} (via Team: {InheritedFromTeamName})" : Name;
    }

    public class TeamItem
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TeamType { get; set; } = "Owner"; // Owner, Access, EntraSecurityGroup, EntraOfficeGroup
        public Guid BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; } = string.Empty;
        public int AssignedRolesCount { get; set; }
        public override string ToString() => $"{Name} ({TeamType} Team)";
    }

    public class SharingItem
    {
        public Guid PrincipalId { get; set; }
        public string PrincipalType { get; set; } = "systemuser"; // systemuser or team
        public string PrincipalName { get; set; } = string.Empty;
        public int AccessRightsMask { get; set; }
        public string GrantedRightsSummary { get; set; } = string.Empty;
        public bool IsDirectToUser { get; set; }
        public string ViaTeamName { get; set; } = string.Empty;
    }
}
