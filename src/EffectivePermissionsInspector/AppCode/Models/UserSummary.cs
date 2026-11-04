using System;

namespace EffectivePermissionsInspector.AppCode.Models
{
    public class UserSummary
    {
        public Guid SystemUserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string DomainName { get; set; } = string.Empty;
        public string PrimaryEmail { get; set; } = string.Empty;
        public Guid BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; } = string.Empty;
        public bool IsDisabled { get; set; }
        public int AccessMode { get; set; }

        public string DisplayText
        {
            get
            {
                string identifier = !string.IsNullOrWhiteSpace(PrimaryEmail) 
                    ? PrimaryEmail 
                    : (!string.IsNullOrWhiteSpace(DomainName) ? DomainName : "No Email");

                string bu = !string.IsNullOrWhiteSpace(BusinessUnitName) 
                    ? $"  |  BU: {BusinessUnitName}" 
                    : string.Empty;

                return $"{FullName} ({identifier}){bu}";
            }
        }

        public override string ToString() => DisplayText;
    }

    public class RecordTarget
    {
        public string EntityLogicalName { get; set; } = string.Empty;
        public string EntityDisplayName { get; set; } = string.Empty;
        public Guid RecordId { get; set; }
        public string RecordName { get; set; } = string.Empty;
        
        public Guid OwnerId { get; set; }
        public string OwnerType { get; set; } = "systemuser"; // systemuser or team
        public string OwnerName { get; set; } = string.Empty;

        public Guid OwningBusinessUnitId { get; set; }
        public string OwningBusinessUnitName { get; set; } = string.Empty;

        public bool IsUserOwned => OwnerType == "systemuser";
        public bool IsTeamOwned => OwnerType == "team";
    }
}
