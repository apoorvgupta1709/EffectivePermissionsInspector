using System;
using System.IO;
using System.Text;
using EffectivePermissionsInspector.AppCode.Models;

namespace EffectivePermissionsInspector.AppCode.Helpers
{
    public static class SecurityReportExporter
    {
        public static string ExportHtmlReport(EffectivePermissionAnalysisResult result, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang='en'>");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset='utf-8'>");
            sb.AppendLine("  <title>Dataverse Effective Permissions Security Audit</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Helvetica, Arial, sans-serif; background: #f8fafc; color: #0f172a; margin: 0; padding: 24px; }");
            sb.AppendLine("    .container { max-width: 1100px; margin: 0 auto; }");
            sb.AppendLine("    .header { margin-bottom: 24px; border-bottom: 2px solid #e2e8f0; padding-bottom: 16px; }");
            sb.AppendLine("    h1 { margin: 0 0 6px; font-size: 24px; color: #1e40af; }");
            sb.AppendLine("    .meta { color: #64748b; font-size: 14px; }");
            sb.AppendLine("    .cards { display: grid; grid-template-columns: repeat(auto-fit, minmax(280px, 1fr)); gap: 16px; margin-bottom: 24px; }");
            sb.AppendLine("    .card { background: #ffffff; border: 1px solid #cbd5e1; border-radius: 8px; padding: 16px; box-shadow: 0 1px 3px rgba(0,0,0,0.05); }");
            sb.AppendLine("    .card-title { font-size: 12px; text-transform: uppercase; color: #475569; margin-bottom: 8px; font-weight: 700; }");
            sb.AppendLine("    .card-row { display: flex; justify-content: space-between; font-size: 13px; margin-bottom: 5px; }");
            sb.AppendLine("    .badge { display: inline-block; padding: 3px 10px; border-radius: 9999px; font-size: 11px; font-weight: 700; text-transform: uppercase; }");
            sb.AppendLine("    .badge-grant { background: #ecfdf5; color: #065f46; border: 1px solid #a7f3d0; }");
            sb.AppendLine("    .badge-deny { background: #fef2f2; color: #991b1b; border: 1px solid #fecaca; }");
            sb.AppendLine("    table { width: 100%; border-collapse: collapse; margin-top: 12px; margin-bottom: 24px; background: #ffffff; border-radius: 8px; overflow: hidden; border: 1px solid #e2e8f0; box-shadow: 0 1px 2px rgba(0,0,0,0.04); }");
            sb.AppendLine("    th { background: #f1f5f9; text-align: left; padding: 11px 12px; font-size: 13px; color: #334155; font-weight: 700; border-bottom: 1px solid #e2e8f0; }");
            sb.AppendLine("    td { padding: 10px 12px; font-size: 13px; border-bottom: 1px solid #f1f5f9; }");
            sb.AppendLine("    tr:nth-child(even) { background: #f8fafc; }");
            sb.AppendLine("    h2 { font-size: 17px; margin: 24px 0 8px; color: #1e293b; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <div class='container'>");
            sb.AppendLine("    <div class='header'>");
            sb.AppendLine("      <h1>Effective Permissions & Security Audit</h1>");
            sb.AppendLine($"     <div class='meta'>Generated: {result.InspectedAtUtc:yyyy-MM-dd HH:mm:ss} UTC | Environment Audit</div>");
            sb.AppendLine("    </div>");

            // User & Record summary cards
            sb.AppendLine("    <div class='cards'>");
            sb.AppendLine("      <div class='card'>");
            sb.AppendLine("        <div class='card-title'>Target User Context</div>");
            sb.AppendLine($"       <div class='card-row'><span>Name:</span> <strong>{Escape(result.User.FullName)}</strong></div>");
            sb.AppendLine($"       <div class='card-row'><span>Username:</span> <code>{Escape(result.User.DomainName)}</code></div>");
            sb.AppendLine($"       <div class='card-row'><span>Business Unit:</span> <span>{Escape(result.User.BusinessUnitName)}</span></div>");
            sb.AppendLine($"       <div class='card-row'><span>Account Status:</span> <span>{(result.User.IsDisabled ? "Disabled" : "Active")}</span></div>");
            sb.AppendLine("      </div>");

            sb.AppendLine("      <div class='card'>");
            sb.AppendLine("        <div class='card-title'>Target Record Context</div>");
            sb.AppendLine($"       <div class='card-row'><span>Record:</span> <strong>{Escape(result.Record.RecordName)}</strong></div>");
            sb.AppendLine($"       <div class='card-row'><span>Entity:</span> <code>{Escape(result.Record.EntityLogicalName)}</code></div>");
            sb.AppendLine($"       <div class='card-row'><span>Owner:</span> <span>{Escape(result.Record.OwnerName)} ({result.Record.OwnerType})</span></div>");
            sb.AppendLine($"       <div class='card-row'><span>Record BU:</span> <span>{Escape(result.Record.OwningBusinessUnitName)}</span></div>");
            sb.AppendLine("      </div>");
            sb.AppendLine("    </div>");

            // Core Privileges Table
            sb.AppendLine("    <h2>1. Effective Record-Level Access Privileges</h2>");
            sb.AppendLine("    <table>");
            sb.AppendLine("      <thead><tr><th>Right</th><th>Status</th><th>Access Source</th><th>Privilege Depth</th><th>Evaluation Explanation</th></tr></thead>");
            sb.AppendLine("      <tbody>");
            foreach (var r in result.AccessRights)
            {
                string badge = r.IsGranted ? "<span class='badge badge-grant'>✔ GRANTED</span>" : "<span class='badge badge-deny'>✖ DENIED</span>";
                sb.AppendLine("      <tr>");
                sb.AppendLine($"       <td><strong>{r.RightName}</strong></td>");
                sb.AppendLine($"       <td>{badge}</td>");
                sb.AppendLine($"       <td>{Escape(r.SourceDetails)}</td>");
                sb.AppendLine($"       <td>{Escape(r.PrivilegeDepth)}</td>");
                sb.AppendLine($"       <td>{Escape(r.Reason)}</td>");
                sb.AppendLine("      </tr>");
            }
            sb.AppendLine("      </tbody>");
            sb.AppendLine("    </table>");

            // Field-Level Security
            if (result.FieldPermissions.Count > 0)
            {
                sb.AppendLine("    <h2>2. Field-Level Security (FLS) Permissions</h2>");
                sb.AppendLine("    <table>");
                sb.AppendLine("      <thead><tr><th>Field Display Name</th><th>Logical Name</th><th>Read Access</th><th>Update Access</th><th>Source Profile</th><th>Blocking Detail</th></tr></thead>");
                sb.AppendLine("      <tbody>");
                foreach (var f in result.FieldPermissions)
                {
                    string readBadge = f.CanRead ? "<span class='badge badge-grant'>READ</span>" : "<span class='badge badge-deny'>BLOCKED</span>";
                    string updateBadge = f.CanUpdate ? "<span class='badge badge-grant'>UPDATE</span>" : "<span class='badge badge-deny'>BLOCKED</span>";
                    sb.AppendLine("      <tr>");
                    sb.AppendLine($"       <td><strong>{Escape(f.AttributeDisplayName)}</strong></td>");
                    sb.AppendLine($"       <td><code>{Escape(f.AttributeLogicalName)}</code></td>");
                    sb.AppendLine($"       <td>{readBadge}</td>");
                    sb.AppendLine($"       <td>{updateBadge}</td>");
                    sb.AppendLine($"       <td>{Escape(f.ReadSourceProfile)}</td>");
                    sb.AppendLine($"       <td style='color:#b91c1c;'>{Escape(f.BlockingReason)}</td>");
                    sb.AppendLine("      </tr>");
                }
                sb.AppendLine("      </tbody>");
                sb.AppendLine("    </table>");
            }

            // Roles & Teams
            sb.AppendLine("    <h2>3. Security Roles & Team Memberships</h2>");
            sb.AppendLine("    <table>");
            sb.AppendLine("      <thead><tr><th>Role Name</th><th>Assignment Type</th><th>Source Team</th><th>Business Unit</th></tr></thead>");
            sb.AppendLine("      <tbody>");
            foreach (var r in result.DirectRoles)
            {
                sb.AppendLine($"      <tr><td>{Escape(r.Name)}</td><td><span class='badge' style='background:#dbeafe;color:#1e40af;'>DIRECT USER ROLE</span></td><td>—</td><td>{Escape(r.BusinessUnitName)}</td></tr>");
            }
            foreach (var r in result.InheritedTeamRoles)
            {
                sb.AppendLine($"      <tr><td>{Escape(r.Name)}</td><td><span class='badge' style='background:#f3e8ff;color:#6b21a8;'>TEAM INHERITED</span></td><td>{Escape(r.InheritedFromTeamName)}</td><td>{Escape(r.BusinessUnitName)}</td></tr>");
            }
            sb.AppendLine("      </tbody>");
            sb.AppendLine("    </table>");

            sb.AppendLine("  </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            return filePath;
        }

        private static string Escape(string? s) => System.Net.WebUtility.HtmlEncode(s ?? string.Empty);
    }
}
