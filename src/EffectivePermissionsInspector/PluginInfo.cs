using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using EffectivePermissionsInspector.UI;

namespace EffectivePermissionsInspector
{
    [Export(typeof(IXrmToolBoxPlugin)),
     ExportMetadata("Name", "Effective Permissions Inspector"),
     ExportMetadata("Description", "Diagnose 'Why Can't John Edit This Record?' — Evaluate all security layers: Security Roles, Team inheritance, BU depth, POA sharing, and Field-Level Security."),
     ExportMetadata("BigImageBase64", null),
     ExportMetadata("SmallImageBase64", null),
     ExportMetadata("BackgroundColor", "#FFFFFF"),
     ExportMetadata("PrimaryFontColor", "#2563EB"),
     ExportMetadata("SecondaryFontColor", "#64748B")]
    public class PermissionsInspectorPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new PermissionsInspectorControl();
        }
    }
}
