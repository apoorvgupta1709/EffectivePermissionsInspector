using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using PermissionsInspector.AppCode.Helpers;
using PermissionsInspector.AppCode.Models;
using PermissionsInspector.AppCode.Services;

namespace PermissionsInspector.UI
{
    public partial class PermissionsInspectorControl : PluginControlBase, IXrmToolBoxPluginControl
    {
        private EffectivePermissionAnalysisResult? _currentResult;
        private List<UserSummary> _cachedUsers = new List<UserSummary>();
        private List<RoleItem> _allRoles = new List<RoleItem>();
        private List<TeamItem> _allTeams = new List<TeamItem>();

        public PermissionsInspectorControl()
        {
            InitializeComponent();
        }

        private void PermissionsInspectorControl_Load(object sender, EventArgs e)
        {
            SetupGridStyles();
            ResetCardsToWaiting();

            // Set placeholder hint in record target text
            if (string.IsNullOrEmpty(txtRecordUrlOrId.Text))
            {
                txtRecordUrlOrId.Text = "Paste full Model-Driven App URL or account|{00000000-0000-0000-0000-000000000000}";
                txtRecordUrlOrId.ForeColor = Color.FromArgb(148, 163, 184);
            }

            txtRecordUrlOrId.GotFocus += (s, args) =>
            {
                if (txtRecordUrlOrId.Text.StartsWith("Paste full Model-Driven App URL"))
                {
                    txtRecordUrlOrId.Text = string.Empty;
                    txtRecordUrlOrId.ForeColor = Color.FromArgb(15, 23, 42);
                }
            };
        }

        private void SetupGridStyles()
        {
            Color lightBg = Color.White;
            Color altBg = Color.FromArgb(248, 250, 252);
            Color headerBg = Color.FromArgb(241, 245, 249);
            Color textDark = Color.FromArgb(15, 23, 42);
            Color gridLine = Color.FromArgb(226, 232, 240);

            foreach (var dgv in new[] { dgvRights, dgvFields, dgvRoles, dgvSharing })
            {
                dgv.EnableHeadersVisualStyles = false;
                dgv.BackgroundColor = lightBg;
                dgv.DefaultCellStyle.BackColor = lightBg;
                dgv.DefaultCellStyle.ForeColor = textDark;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 242, 254);
                dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(3, 105, 161);
                dgv.AlternatingRowsDefaultCellStyle.BackColor = altBg;
                dgv.GridColor = gridLine;
                dgv.BorderStyle = BorderStyle.FixedSingle;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

                dgv.ColumnHeadersDefaultCellStyle.BackColor = headerBg;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgv.ColumnHeadersHeight = 34;
            }
        }

        private void ResetCardsToWaiting()
        {
            SetCardState(cardRead, lblReadVal, lblReadTitle, null);
            SetCardState(cardWrite, lblWriteVal, lblWriteTitle, null);
            SetCardState(cardDelete, lblDeleteVal, lblDeleteTitle, null);
            SetCardState(cardAppend, lblAppendVal, lblAppendTitle, null);
            SetCardState(cardAppendTo, lblAppendToVal, lblAppendToTitle, null);
            SetCardState(cardAssign, lblAssignVal, lblAssignTitle, null);
            SetCardState(cardShare, lblShareVal, lblShareTitle, null);
        }

        private void btnLoadUsers_Click(object sender, EventArgs e)
        {
            LoadUsers(string.Empty);
        }

        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            LoadUsers(txtSearchUser.Text.Trim());
        }

        private void txtSearchUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearchUser_Click(sender, e);
            }
        }

        private void LoadUsers(string query)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to Dataverse first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblStatus.Text = string.IsNullOrEmpty(query) ? "Loading active users..." : $"Searching for users matching '{query}'...";
            progressBar.Visible = true;
            btnLoadUsers.Enabled = false;
            btnSearchUser.Enabled = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving active human users and security metadata...",
                Work = (worker, args) =>
                {
                    var engine = new SecurityInspectorEngine(Service);
                    var users = engine.SearchUsers(query, 1000);
                    var roles = engine.GetAllAvailableRoles();
                    var teams = engine.GetAllAvailableTeams();
                    args.Result = (users, roles, teams);
                },
                PostWorkCallBack = (args) =>
                {
                    progressBar.Visible = false;
                    btnLoadUsers.Enabled = true;
                    btnSearchUser.Enabled = true;

                    if (args.Error != null)
                    {
                        MessageBox.Show("Failed to load users: " + args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblStatus.Text = "Error loading users.";
                        return;
                    }

                    if (args.Result is ValueTuple<List<UserSummary>, List<RoleItem>, List<TeamItem>> tuple)
                    {
                        _cachedUsers = tuple.Item1;
                        _allRoles = tuple.Item2;
                        _allTeams = tuple.Item3;

                        cboUsers.DataSource = null;
                        cboUsers.DisplayMember = "DisplayText";
                        cboUsers.ValueMember = "SystemUserId";
                        cboUsers.DataSource = _cachedUsers;

                        chkListRoles.Items.Clear();
                        foreach (var r in _allRoles) chkListRoles.Items.Add(r);

                        chkListTeams.Items.Clear();
                        foreach (var t in _allTeams) chkListTeams.Items.Add(t);

                        lblStatus.Text = $"Found {_cachedUsers.Count} active user(s). Select a user and enter a Record URL / GUID.";

                        if (_cachedUsers.Count == 0)
                        {
                            MessageBox.Show("No active users found matching your search. Try clearing the search box to load all active users.", "No Users Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            });
        }

        private void btnInspect_Click(object sender, EventArgs e)
        {
            if (Service == null)
            {
                MessageBox.Show("Please connect to Dataverse first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboUsers.SelectedItem is not UserSummary selectedUser)
            {
                MessageBox.Show("Please select a User first.\n\nClick 'Load Active Users' or search by name to populate the dropdown.", "User Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string input = txtRecordUrlOrId.Text.Trim();
            if (string.IsNullOrEmpty(input) || input.StartsWith("Paste full Model-Driven App URL"))
            {
                MessageBox.Show("Please enter a Dataverse Record URL or Record GUID.", "Record Target Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!UrlParserHelper.TryParseDataverseUrl(input, out string entityLogicalName, out Guid recordId))
            {
                MessageBox.Show("Could not parse entity and GUID from input.\n\nAccepted formats:\n1. Full browser URL: https://org.crm.dynamics.com/main.aspx?etn=opportunity&id=...\n2. Format: account|{GUID}\n3. Direct GUID (will prompt for entity name)", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(entityLogicalName))
            {
                string entityPrompt = Microsoft.VisualBasic.Interaction.InputBox("Enter the Entity Logical Name for this record (e.g. account, contact, opportunity):", "Entity Logical Name Required", "account");
                if (string.IsNullOrWhiteSpace(entityPrompt)) return;
                entityLogicalName = entityPrompt.Trim().ToLowerInvariant();
            }

            btnInspect.Enabled = false;
            btnInspectTool.Enabled = false;
            progressBar.Visible = true;
            progressBar.Value = 0;
            lblStatus.Text = $"Evaluating effective access for {selectedUser.FullName} on '{entityLogicalName}' ({recordId})...";

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Inspecting effective permissions for '{selectedUser.FullName}'...",
                Work = (worker, args) =>
                {
                    var engine = new SecurityInspectorEngine(Service);
                    var result = engine.InspectEffectivePermissions(
                        selectedUser.SystemUserId,
                        entityLogicalName,
                        recordId,
                        (pct, msg) => worker.ReportProgress(pct, msg));

                    args.Result = result;
                },
                ProgressChanged = (args) =>
                {
                    progressBar.Value = Math.Min(100, Math.Max(0, args.ProgressPercentage));
                    if (args.UserState is string msg) lblStatus.Text = msg;
                },
                PostWorkCallBack = (args) =>
                {
                    btnInspect.Enabled = true;
                    btnInspectTool.Enabled = true;
                    progressBar.Visible = false;

                    if (args.Error != null)
                    {
                        MessageBox.Show("Inspection failed: " + args.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblStatus.Text = "Error during permission inspection.";
                        return;
                    }

                    if (args.Result is EffectivePermissionAnalysisResult result)
                    {
                        _currentResult = result;
                        RenderResults(result);
                        lblStatus.Text = $"Inspection complete: {result.Record.EntityDisplayName} '{result.Record.RecordName}'. Read: {(result.CanRead ? "YES" : "NO")}, Write: {(result.CanWrite ? "YES" : "NO")}.";
                    }
                }
            });
        }

        private void RenderResults(EffectivePermissionAnalysisResult result)
        {
            // 1. Update Top Badges with Clean Light Styling
            SetCardState(cardRead, lblReadVal, lblReadTitle, result.CanRead);
            SetCardState(cardWrite, lblWriteVal, lblWriteTitle, result.CanWrite);
            SetCardState(cardDelete, lblDeleteVal, lblDeleteTitle, result.CanDelete);
            SetCardState(cardAppend, lblAppendVal, lblAppendTitle, result.CanAppend);
            SetCardState(cardAppendTo, lblAppendToVal, lblAppendToTitle, result.CanAppendTo);
            SetCardState(cardAssign, lblAssignVal, lblAssignTitle, result.CanAssign);
            SetCardState(cardShare, lblShareVal, lblShareTitle, result.CanShare);

            // 2. Populate Rights Grid
            dgvRights.DataSource = null;
            dgvRights.DataSource = result.AccessRights.Select(r => new
            {
                Privilege = r.RightName,
                Status = r.IsGranted ? "✔ GRANTED" : "✖ DENIED",
                Layer = r.SourceLayer.ToString(),
                Source = r.SourceDetails,
                Depth = r.PrivilegeDepth,
                DetailedReason = r.Reason
            }).ToList();

            // 3. Populate Field Security Grid
            dgvFields.DataSource = null;
            dgvFields.DataSource = result.FieldPermissions.Select(f => new
            {
                Field = f.AttributeDisplayName,
                LogicalName = f.AttributeLogicalName,
                Read = f.CanRead ? "✔ ALLOWED" : "✖ BLOCKED",
                Update = f.CanUpdate ? "✔ ALLOWED" : "✖ BLOCKED",
                ReadProfile = f.ReadSourceProfile,
                UpdateProfile = f.UpdateSourceProfile,
                BlockingReason = f.BlockingReason
            }).ToList();

            // 4. Populate Roles Grid
            var combinedRoles = new List<object>();
            foreach (var r in result.DirectRoles)
            {
                combinedRoles.Add(new { Role = r.Name, Type = "Direct User Role", Team = "—", BusinessUnit = r.BusinessUnitName });
            }
            foreach (var r in result.InheritedTeamRoles)
            {
                combinedRoles.Add(new { Role = r.Name, Type = "Team Inherited", Team = r.InheritedFromTeamName, BusinessUnit = r.BusinessUnitName });
            }
            dgvRoles.DataSource = null;
            dgvRoles.DataSource = combinedRoles;

            // 5. Populate Sharing Grid
            dgvSharing.DataSource = null;
            dgvSharing.DataSource = result.SharingGrants.Select(s => new
            {
                SharedTo = s.PrincipalName,
                Type = s.PrincipalType,
                RightsGranted = s.GrantedRightsSummary,
                DirectOrTeam = s.IsDirectToUser ? "Direct User Share" : $"Via Team ({s.ViaTeamName})"
            }).ToList();
        }

        private static void SetCardState(Panel panel, Label labelVal, Label labelTitle, bool? isGranted)
        {
            if (isGranted == null)
            {
                // Waiting / Default State
                panel.BackColor = Color.White;
                labelVal.ForeColor = Color.FromArgb(148, 163, 184);
                labelTitle.ForeColor = Color.FromArgb(100, 116, 139);
                labelVal.Text = "WAITING";
            }
            else if (isGranted.Value)
            {
                // Clean Emerald Mint
                panel.BackColor = Color.FromArgb(236, 253, 245); // Emerald-50
                labelVal.ForeColor = Color.FromArgb(5, 150, 105);   // Emerald-600
                labelTitle.ForeColor = Color.FromArgb(6, 95, 70);   // Emerald-800
                labelVal.Text = "GRANTED";
            }
            else
            {
                // Clean Rose Red
                panel.BackColor = Color.FromArgb(254, 242, 242); // Rose-50
                labelVal.ForeColor = Color.FromArgb(220, 38, 38);   // Red-600
                labelTitle.ForeColor = Color.FromArgb(153, 27, 27); // Red-800
                labelVal.Text = "DENIED";
            }
        }

        private void btnRunSimulation_Click(object sender, EventArgs e)
        {
            if (Service == null || _currentResult == null)
            {
                MessageBox.Show("Please run an initial Effective Permissions inspection first before simulating changes.", "Baseline Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRoles = chkListRoles.CheckedItems.OfType<RoleItem>().ToList();
            var selectedTeams = chkListTeams.CheckedItems.OfType<TeamItem>().ToList();

            if (selectedRoles.Count == 0 && selectedTeams.Count == 0)
            {
                MessageBox.Show("Please check at least one Role or Team to simulate.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtSimulateLog.Clear();
            AppendSimLog($"--- WHAT-IF SIMULATION [{DateTime.Now:HH:mm:ss}] ---");
            AppendSimLog($"User: {_currentResult.User.FullName}");
            AppendSimLog($"Record: {_currentResult.Record.RecordName} ({_currentResult.Record.EntityLogicalName})");
            AppendSimLog($"Simulating addition of {selectedRoles.Count} Roles and {selectedTeams.Count} Teams...");
            AppendSimLog("☕ Donate & Support continuous development: https://github.com/sponsors/apoorvgupta1709");
            AppendSimLog("--------------------------------------------------");

            var simEngine = new SimulationEngine(Service);
            var simResult = simEngine.SimulateImpact(_currentResult, selectedRoles, selectedTeams);

            if (simResult.GrantedChanges.Count == 0)
            {
                AppendSimLog("RESULT: No new permissions were unlocked.");
                AppendSimLog("Why? The selected roles/teams do not grant higher privileges or sufficient Business Unit depth on this entity.");
            }
            else
            {
                AppendSimLog($"SUCCESS! Unlocked {simResult.GrantedChanges.Count} new permission changes:\r\n");
                foreach (var change in simResult.GrantedChanges)
                {
                    AppendSimLog($"  ★ {change}");
                }
            }

            AppendSimLog("\r\nSIMULATED ACCESS RIGHTS STATE:");
            foreach (var right in simResult.Simulated.AccessRights)
            {
                string status = right.IsGranted ? "[GRANTED]" : "[DENIED] ";
                AppendSimLog($"  {status} {right.RightName.PadRight(10)} | {right.SourceDetails}");
            }
        }

        private void AppendSimLog(string text)
        {
            txtSimulateLog.AppendText(text + "\r\n");
            txtSimulateLog.SelectionStart = txtSimulateLog.Text.Length;
            txtSimulateLog.ScrollToCaret();
        }

        private void btnSimulateTab_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabSimulate;
        }

        private void btnExportHtml_Click(object sender, EventArgs e)
        {
            if (_currentResult == null)
            {
                MessageBox.Show("Please inspect a user and record first before exporting an audit report.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "HTML Report (*.html)|*.html";
                sfd.FileName = $"Security_Audit_{_currentResult.User.DomainName}_{_currentResult.Record.EntityLogicalName}_{DateTime.UtcNow:yyyyMMdd}.html";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SecurityReportExporter.ExportHtmlReport(_currentResult, sfd.FileName);
                    if (MessageBox.Show("Security audit report exported successfully! Open now?", "Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                }
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (_currentResult == null) return;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.FileName = $"Security_Audit_{_currentResult.User.DomainName}_{DateTime.UtcNow:yyyyMMdd}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine("Privilege,Status,SourceLayer,SourceDetails,Depth,Reason");
                    foreach (var r in _currentResult.AccessRights)
                    {
                        sb.AppendLine($"\"{r.RightName}\",\"{(r.IsGranted ? "GRANTED" : "DENIED")}\",\"{r.SourceLayer}\",\"{r.SourceDetails}\",\"{r.PrivilegeDepth}\",\"{r.Reason.Replace("\"", "\"\"")}\"");
                    }
                    System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                    MessageBox.Show("CSV data saved to:\n" + sfd.FileName, "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSponsor_Click(object sender, EventArgs e)
        {
            SponsorHelper.ShowSponsorDialog(this, "Permissions Inspector");
        }
    }
}
