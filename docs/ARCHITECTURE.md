# Technical Architecture: Effective Permissions Inspector

## 1. The Multi-Layer Security Evaluation Flow

When a user attempts to interact with a Dataverse record, the platform security pipeline evaluates permissions in a strict hierarchical order:

```
[Target User + Record]
          │
          ▼
1. Is User System Administrator? ───────────► Full Access Granted (All Privileges)
          │ No
          ▼
2. Record Ownership:
   - Does User directly own record? ────────► Owner Access Granted
   - Does a Team containing User own it? ───► Owner Access Granted
          │ No
          ▼
3. Security Roles (Direct + Inherited):
   - Evaluate Privilege Depth against Record BU:
     • Basic (User-owned only)
     • Local (Same Business Unit)
     • Deep (Parent & Child Business Units)
     • Global (Entire Organization) ────────► Role Access Granted
          │ No
          ▼
4. PrincipalObjectAccess (POA):
   - Direct User Share? ────────────────────► Shared Access Granted
   - Team Share? ───────────────────────────► Shared Access Granted
          │ No
          ▼
5. Hierarchy / Manager Access: ─────────────► Hierarchy Access Granted
          │ No
          ▼
        DENIED
```

---

## 2. Core SDK Methods Used

| Component | SDK Class / Method | Description |
|---|---|---|
| **Effective Access** | `RetrievePrincipalAccessRequest` | Returns the raw `AccessRights` bitmask (`ReadAccess`, `WriteAccess`, `DeleteAccess`, `AppendAccess`, `AppendToAccess`, `AssignAccess`, `ShareAccess`) |
| **Direct Roles** | `FetchExpression` on `systemuserroles` intersect `role` | Extracts direct role IDs and Business Units |
| **Team Roles** | `FetchExpression` on `teamroles` intersect `role` | Extracts roles inherited through Owner Teams or Entra Security Groups |
| **POA Sharing** | `FetchExpression` on `principalobjectaccess` | Queries explicit shares matching `objectid` and User/Team principal IDs |
| **Field Security** | `RetrieveEntityRequest` (EntityFilters.Attributes) | Detects fields with `IsSecured == true` |
| **Field Profiles** | `systemuserprofiles`, `teamprofiles`, `fieldpermission` | Evaluates effective Read, Create, and Update permissions per field |
| **Simulation** | `roleprivileges` query | Calculates simulated depth masks (`Basic`, `Local`, `Deep`, `Global`) against target entity |
