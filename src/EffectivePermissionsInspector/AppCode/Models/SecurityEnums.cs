using System;

namespace EffectivePermissionsInspector.AppCode.Models
{
    public enum AccessLayer
    {
        RecordOwner,
        DirectSecurityRole,
        TeamSecurityRole,
        SharingPOA,
        HierarchySecurity,
        Denied
    }

    public enum PrivilegeDepthLevel
    {
        None = 0,
        User = 1,          // Basic
        BusinessUnit = 2,  // Local
        ParentChild = 4,   // Deep
        Organization = 8   // Global
    }
}
