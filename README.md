# 🛡️ "Why Can't John Edit This Record?" — Effective Permissions Inspector

> The **#1 support ticket solver in Microsoft Dynamics 365 & Power Apps**. Instantly unpack the complex Dataverse security model to reveal exactly why a user can or cannot see, edit, delete, or share any specific record.

---

## 🎯 The Problem
Security in Microsoft Dynamics 365 is notoriously layered and complex. When a user complains they cannot edit an Opportunity, Account, or custom record, their access is determined by a combination of:
1. **Direct Security Roles** (assigned directly to the user).
2. **Team Inherited Roles** (Owner Teams, Access Teams, Entra Group Teams).
3. **Business Unit Hierarchy** (User depth vs Local BU vs Deep Parent-Child vs Global Org).
4. **Record Ownership** (User-owned vs Team-owned).
5. **Record Sharing (`PrincipalObjectAccess` / POA table)** (Direct shares, team shares, cascade sharing).
6. **Field-Level Security (FLS)** (`FieldSecurityProfile` blocking specific attributes like *Budget*, *SSN*, *Credit Limit*).
7. **Hierarchy / Manager Security**.

Admins routinely waste **30 to 90 minutes per ticket** cross-referencing roles, teams, and BU trees to diagnose what is blocking the user.

---

## 🚀 Key Features

### 1. 🔍 Instant URL & GUID Parser
- Paste a full Dynamics 365 / Power Apps URL directly from your browser:
  ```
  https://myorg.crm.dynamics.com/main.aspx?pagetype=entityrecord&etn=opportunity&id=b114d567-33a5-ee11-be37-000d3a58d641
  ```
- Or enter an entity name and record GUID (`opportunity|{guid}`).
- Automatically parses the entity type and record ID.

### 2. 🚦 7-Privilege Visual Traffic Light Dashboard
- Instant Green / Red visual badges for all 7 Dataverse core privileges:
  * **READ**
  * **WRITE**
  * **DELETE**
  * **APPEND**
  * **APPEND TO**
  * **ASSIGN**
  * **SHARE**

### 3. 🧠 Detailed Reason Engine
- Tells you **exactly why** a right is granted or blocked:
  * *Granted via Record Owner*
  * *Granted via Sales Team Role (Parent-Child BU depth)*
  * *Granted via POA Share from Alice*
  * *Denied: User's BU ('Europe Sales') differs from Record's BU ('US Finance') and roles lack Global depth.*

### 4. 🔒 Field-Level Security (FLS) Inspector
- Scans all secured fields on the target record (`IsSecured == true`).
- Compares user's and user's teams' assigned `FieldSecurityProfile` records.
- Identifies whether the user can Read, Create, or Update each field and flags the exact blocking profile.

### 5. 🔮 "What-If" Simulation Studio (Pro Feature)
- *"What would happen if I add John to the 'Finance Reviewers' team or assign him 'Account Manager'?"*
- Select any role or team and click **"Simulate Permission Changes"**.
- Accurately calculates the permission delta **without changing anything in CRM**!

### 6. 📊 Executive HTML Security Audit Report
- One-click export of a formal, timestamped security audit document ready for compliance, SOC2 reviews, or closing support tickets with full proof.

---

## 🏗️ Project Architecture

```
EffectivePermissionsInspector/
├── EffectivePermissionsInspector.sln
├── README.md
├── docs/
│   └── ARCHITECTURE.md
└── src/
    └── EffectivePermissionsInspector/
        ├── EffectivePermissionsInspector.csproj
        ├── PluginInfo.cs                           # MEF Export for XrmToolBox
        ├── Properties/
        │   └── AssemblyInfo.cs
        ├── AppCode/
        │   ├── Models/
        │   │   ├── SecurityEnums.cs                # AccessLayer, PrivilegeDepth
        │   │   ├── UserSummary.cs                  # User & BU model
        │   │   ├── RecordTarget.cs                 # Record ownership model
        │   │   ├── AccessRightItem.cs              # 7-privilege breakdown model
        │   │   ├── FieldPermissionItem.cs          # Field security model
        │   │   ├── RoleItem.cs & TeamItem.cs       # Roles & Teams models
        │   │   ├── SharingItem.cs                  # POA model
        │   │   └── EffectivePermissionAnalysisResult.cs
        │   ├── Helpers/
        │   │   ├── UrlParserHelper.cs              # Browser URL / GUID parser
        │   │   └── SecurityReportExporter.cs       # HTML & CSV audit reports
        │   └── Services/
        │       ├── FieldSecurityAnalyzer.cs        # Field-Level Security engine
        │       ├── SharingAnalyzer.cs              # POA sharing engine
        │       ├── SecurityInspectorEngine.cs      # Core inspection orchestrator
        │       └── SimulationEngine.cs             # What-If delta simulator
        └── UI/
            ├── PermissionsInspectorControl.cs      # WinForms controller
            └── PermissionsInspectorControl.Designer.cs
```

---

## 🛠️ How to Build and Deploy to XrmToolBox

1. Open `EffectivePermissionsInspector\EffectivePermissionsInspector.sln` in **Visual Studio**.
2. Build the project in **Release** mode (`Ctrl + Shift + B`).
3. Copy the compiled files:
   ```
   bin\Release\EffectivePermissionsInspector.dll
   ```
   Into your XrmToolBox plugins folder:
   ```
   %AppData%\MscrmTools\XrmToolBox\Plugins\EffectivePermissionsInspector\
   ```
4. Launch **XrmToolBox**, connect to your Dataverse environment, and search for **"Effective Permissions Inspector"**.
