namespace EffectivePermissionsInspector.UI
{
    partial class PermissionsInspectorControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnInspectTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportHtml = new System.Windows.Forms.ToolStripButton();
            this.btnExportCsv = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSimulateTab = new System.Windows.Forms.ToolStripButton();
            this.btnSponsor = new System.Windows.Forms.ToolStripButton();
            this.pnlInputs = new System.Windows.Forms.Panel();
            this.tblInputs = new System.Windows.Forms.TableLayoutPanel();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtSearchUser = new System.Windows.Forms.TextBox();
            this.btnSearchUser = new System.Windows.Forms.Button();
            this.cboUsers = new System.Windows.Forms.ComboBox();
            this.btnLoadUsers = new System.Windows.Forms.Button();
            this.lblRecordUrl = new System.Windows.Forms.Label();
            this.txtRecordUrlOrId = new System.Windows.Forms.TextBox();
            this.btnInspect = new System.Windows.Forms.Button();
            this.tblCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardShare = new System.Windows.Forms.Panel();
            this.lblShareVal = new System.Windows.Forms.Label();
            this.lblShareTitle = new System.Windows.Forms.Label();
            this.cardAssign = new System.Windows.Forms.Panel();
            this.lblAssignVal = new System.Windows.Forms.Label();
            this.lblAssignTitle = new System.Windows.Forms.Label();
            this.cardAppendTo = new System.Windows.Forms.Panel();
            this.lblAppendToVal = new System.Windows.Forms.Label();
            this.lblAppendToTitle = new System.Windows.Forms.Label();
            this.cardAppend = new System.Windows.Forms.Panel();
            this.lblAppendVal = new System.Windows.Forms.Label();
            this.lblAppendTitle = new System.Windows.Forms.Label();
            this.cardDelete = new System.Windows.Forms.Panel();
            this.lblDeleteVal = new System.Windows.Forms.Label();
            this.lblDeleteTitle = new System.Windows.Forms.Label();
            this.cardWrite = new System.Windows.Forms.Panel();
            this.lblWriteVal = new System.Windows.Forms.Label();
            this.lblWriteTitle = new System.Windows.Forms.Label();
            this.cardRead = new System.Windows.Forms.Panel();
            this.lblReadVal = new System.Windows.Forms.Label();
            this.lblReadTitle = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabRights = new System.Windows.Forms.TabPage();
            this.dgvRights = new System.Windows.Forms.DataGridView();
            this.tabFields = new System.Windows.Forms.TabPage();
            this.dgvFields = new System.Windows.Forms.DataGridView();
            this.tabRoles = new System.Windows.Forms.TabPage();
            this.dgvRoles = new System.Windows.Forms.DataGridView();
            this.tabSharing = new System.Windows.Forms.TabPage();
            this.dgvSharing = new System.Windows.Forms.DataGridView();
            this.tabSimulate = new System.Windows.Forms.TabPage();
            this.pnlSimulateContainer = new System.Windows.Forms.Panel();
            this.txtSimulateLog = new System.Windows.Forms.TextBox();
            this.btnRunSimulation = new System.Windows.Forms.Button();
            this.grpSimTeams = new System.Windows.Forms.GroupBox();
            this.chkListTeams = new System.Windows.Forms.CheckedListBox();
            this.grpSimRoles = new System.Windows.Forms.GroupBox();
            this.chkListRoles = new System.Windows.Forms.CheckedListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1.SuspendLayout();
            this.pnlInputs.SuspendLayout();
            this.tblInputs.SuspendLayout();
            this.tblCards.SuspendLayout();
            this.cardShare.SuspendLayout();
            this.cardAssign.SuspendLayout();
            this.cardAppendTo.SuspendLayout();
            this.cardAppend.SuspendLayout();
            this.cardDelete.SuspendLayout();
            this.cardWrite.SuspendLayout();
            this.cardRead.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabRights.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRights)).BeginInit();
            this.tabFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).BeginInit();
            this.tabRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();
            this.tabSharing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSharing)).BeginInit();
            this.tabSimulate.SuspendLayout();
            this.pnlSimulateContainer.SuspendLayout();
            this.grpSimTeams.SuspendLayout();
            this.grpSimRoles.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInspectTool,
            this.toolStripSeparator1,
            this.btnExportHtml,
            this.btnExportCsv,
            this.toolStripSeparator2,
            this.btnSimulateTab,
            this.btnSponsor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.toolStrip1.Size = new System.Drawing.Size(1150, 39);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnInspectTool
            // 
            this.btnInspectTool.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInspectTool.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnInspectTool.Name = "btnInspectTool";
            this.btnInspectTool.Size = new System.Drawing.Size(200, 28);
            this.btnInspectTool.Text = "Inspect Effective Access";
            this.btnInspectTool.Click += new System.EventHandler(this.btnInspect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btnExportHtml
            // 
            this.btnExportHtml.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnExportHtml.Name = "btnExportHtml";
            this.btnExportHtml.Size = new System.Drawing.Size(185, 28);
            this.btnExportHtml.Text = "Export Security Audit (HTML)";
            this.btnExportHtml.Click += new System.EventHandler(this.btnExportHtml_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(130, 28);
            this.btnExportCsv.Text = "Export CSV Data";
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // btnSimulateTab
            // 
            this.btnSimulateTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.btnSimulateTab.Name = "btnSimulateTab";
            this.btnSimulateTab.Size = new System.Drawing.Size(175, 28);
            this.btnSimulateTab.Text = "What-If Simulation Studio";
            this.btnSimulateTab.Click += new System.EventHandler(this.btnSimulateTab_Click);
            // 
            // btnSponsor
            // 
            this.btnSponsor.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSponsor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(242)))), ((int)(((byte)(248)))));
            this.btnSponsor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSponsor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(39)))), ((int)(((byte)(119)))));
            this.btnSponsor.Name = "btnSponsor";
            this.btnSponsor.Size = new System.Drawing.Size(160, 28);
            this.btnSponsor.Text = "☕ Donate / Support";
            this.btnSponsor.ToolTipText = "Support developer & donate to continuous development";
            this.btnSponsor.Click += new System.EventHandler(this.btnSponsor_Click);
            // 
            // pnlInputs
            // 
            this.pnlInputs.BackColor = System.Drawing.Color.White;
            this.pnlInputs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInputs.Controls.Add(this.tblInputs);
            this.pnlInputs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInputs.Location = new System.Drawing.Point(0, 39);
            this.pnlInputs.Name = "pnlInputs";
            this.pnlInputs.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.pnlInputs.Size = new System.Drawing.Size(1150, 120);
            this.pnlInputs.TabIndex = 1;
            // 
            // tblInputs
            // 
            this.tblInputs.ColumnCount = 5;
            this.tblInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tblInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tblInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tblInputs.Controls.Add(this.lblUser, 0, 0);
            this.tblInputs.Controls.Add(this.txtSearchUser, 1, 0);
            this.tblInputs.Controls.Add(this.btnSearchUser, 2, 0);
            this.tblInputs.Controls.Add(this.cboUsers, 3, 0);
            this.tblInputs.Controls.Add(this.btnLoadUsers, 4, 0);
            this.tblInputs.Controls.Add(this.lblRecordUrl, 0, 1);
            this.tblInputs.Controls.Add(this.txtRecordUrlOrId, 1, 1);
            this.tblInputs.Controls.Add(this.btnInspect, 4, 1);
            this.tblInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInputs.Location = new System.Drawing.Point(12, 6);
            this.tblInputs.Name = "tblInputs";
            this.tblInputs.RowCount = 2;
            this.tblInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tblInputs.Size = new System.Drawing.Size(1124, 106);
            this.tblInputs.TabIndex = 0;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblUser.Location = new System.Drawing.Point(3, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(104, 52);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Select User:";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSearchUser
            // 
            this.txtSearchUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchUser.BackColor = System.Drawing.Color.White;
            this.txtSearchUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchUser.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearchUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearchUser.Location = new System.Drawing.Point(113, 11);
            this.txtSearchUser.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.txtSearchUser.Name = "txtSearchUser";
            this.txtSearchUser.Size = new System.Drawing.Size(144, 29);
            this.txtSearchUser.TabIndex = 1;
            this.txtSearchUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchUser_KeyDown);
            // 
            // btnSearchUser
            // 
            this.btnSearchUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnSearchUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearchUser.FlatAppearance.BorderSize = 0;
            this.btnSearchUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearchUser.ForeColor = System.Drawing.Color.White;
            this.btnSearchUser.Location = new System.Drawing.Point(263, 8);
            this.btnSearchUser.Margin = new System.Windows.Forms.Padding(3, 8, 6, 8);
            this.btnSearchUser.Name = "btnSearchUser";
            this.btnSearchUser.Size = new System.Drawing.Size(86, 36);
            this.btnSearchUser.TabIndex = 2;
            this.btnSearchUser.Text = "Filter";
            this.btnSearchUser.UseVisualStyleBackColor = false;
            this.btnSearchUser.Click += new System.EventHandler(this.btnSearchUser_Click);
            // 
            // cboUsers
            // 
            this.cboUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUsers.BackColor = System.Drawing.Color.White;
            this.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsers.DropDownWidth = 750;
            this.cboUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboUsers.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboUsers.FormattingEnabled = true;
            this.cboUsers.Location = new System.Drawing.Point(358, 11);
            this.cboUsers.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.cboUsers.Name = "cboUsers";
            this.cboUsers.Size = new System.Drawing.Size(573, 29);
            this.cboUsers.TabIndex = 3;
            // 
            // btnLoadUsers
            // 
            this.btnLoadUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnLoadUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLoadUsers.FlatAppearance.BorderSize = 0;
            this.btnLoadUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadUsers.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLoadUsers.ForeColor = System.Drawing.Color.White;
            this.btnLoadUsers.Location = new System.Drawing.Point(937, 8);
            this.btnLoadUsers.Margin = new System.Windows.Forms.Padding(6, 8, 3, 8);
            this.btnLoadUsers.Name = "btnLoadUsers";
            this.btnLoadUsers.Size = new System.Drawing.Size(184, 36);
            this.btnLoadUsers.TabIndex = 4;
            this.btnLoadUsers.Text = "Load Users";
            this.btnLoadUsers.UseVisualStyleBackColor = false;
            this.btnLoadUsers.Click += new System.EventHandler(this.btnLoadUsers_Click);
            // 
            // lblRecordUrl
            // 
            this.lblRecordUrl.AutoSize = true;
            this.lblRecordUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecordUrl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecordUrl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblRecordUrl.Location = new System.Drawing.Point(3, 52);
            this.lblRecordUrl.Name = "lblRecordUrl";
            this.lblRecordUrl.Size = new System.Drawing.Size(104, 54);
            this.lblRecordUrl.TabIndex = 5;
            this.lblRecordUrl.Text = "Record URL:";
            this.lblRecordUrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRecordUrlOrId
            // 
            this.txtRecordUrlOrId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordUrlOrId.BackColor = System.Drawing.Color.White;
            this.txtRecordUrlOrId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblInputs.SetColumnSpan(this.txtRecordUrlOrId, 3);
            this.txtRecordUrlOrId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtRecordUrlOrId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtRecordUrlOrId.Location = new System.Drawing.Point(113, 64);
            this.txtRecordUrlOrId.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.txtRecordUrlOrId.Name = "txtRecordUrlOrId";
            this.txtRecordUrlOrId.Size = new System.Drawing.Size(818, 29);
            this.txtRecordUrlOrId.TabIndex = 6;
            // 
            // btnInspect
            // 
            this.btnInspect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnInspect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInspect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInspect.FlatAppearance.BorderSize = 0;
            this.btnInspect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInspect.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInspect.ForeColor = System.Drawing.Color.White;
            this.btnInspect.Location = new System.Drawing.Point(937, 60);
            this.btnInspect.Margin = new System.Windows.Forms.Padding(6, 8, 3, 8);
            this.btnInspect.Name = "btnInspect";
            this.btnInspect.Size = new System.Drawing.Size(184, 38);
            this.btnInspect.TabIndex = 7;
            this.btnInspect.Text = "Inspect Access";
            this.btnInspect.UseVisualStyleBackColor = false;
            this.btnInspect.Click += new System.EventHandler(this.btnInspect_Click);
            // 
            // tblCards
            // 
            this.tblCards.ColumnCount = 7;
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCards.Controls.Add(this.cardShare, 6, 0);
            this.tblCards.Controls.Add(this.cardAssign, 5, 0);
            this.tblCards.Controls.Add(this.cardAppendTo, 4, 0);
            this.tblCards.Controls.Add(this.cardAppend, 3, 0);
            this.tblCards.Controls.Add(this.cardDelete, 2, 0);
            this.tblCards.Controls.Add(this.cardWrite, 1, 0);
            this.tblCards.Controls.Add(this.cardRead, 0, 0);
            this.tblCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblCards.Location = new System.Drawing.Point(0, 159);
            this.tblCards.Name = "tblCards";
            this.tblCards.RowCount = 1;
            this.tblCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCards.Size = new System.Drawing.Size(1150, 76);
            this.tblCards.TabIndex = 2;
            // 
            // cardShare
            // 
            this.cardShare.BackColor = System.Drawing.Color.White;
            this.cardShare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardShare.Controls.Add(this.lblShareVal);
            this.cardShare.Controls.Add(this.lblShareTitle);
            this.cardShare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardShare.Location = new System.Drawing.Point(987, 5);
            this.cardShare.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cardShare.Name = "cardShare";
            this.cardShare.Size = new System.Drawing.Size(160, 66);
            this.cardShare.TabIndex = 6;
            // 
            // lblShareVal
            // 
            this.lblShareVal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShareVal.AutoSize = true;
            this.lblShareVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblShareVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblShareVal.Location = new System.Drawing.Point(6, 32);
            this.lblShareVal.Name = "lblShareVal";
            this.lblShareVal.Size = new System.Drawing.Size(95, 25);
            this.lblShareVal.TabIndex = 1;
            this.lblShareVal.Text = "WAITING";
            // 
            // lblShareTitle
            // 
            this.lblShareTitle.AutoSize = true;
            this.lblShareTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblShareTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblShareTitle.Location = new System.Drawing.Point(6, 6);
            this.lblShareTitle.Name = "lblShareTitle";
            this.lblShareTitle.Size = new System.Drawing.Size(53, 19);
            this.lblShareTitle.TabIndex = 0;
            this.lblShareTitle.Text = "SHARE";
            // 
            // cardAssign
            // 
            this.cardAssign.BackColor = System.Drawing.Color.White;
            this.cardAssign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardAssign.Controls.Add(this.lblAssignVal);
            this.cardAssign.Controls.Add(this.lblAssignTitle);
            this.cardAssign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAssign.Location = new System.Drawing.Point(823, 5);
            this.cardAssign.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cardAssign.Name = "cardAssign";
            this.cardAssign.Size = new System.Drawing.Size(158, 66);
            this.cardAssign.TabIndex = 5;
            // 
            // lblAssignVal
            // 
            this.lblAssignVal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAssignVal.AutoSize = true;
            this.lblAssignVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAssignVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAssignVal.Location = new System.Drawing.Point(6, 32);
            this.lblAssignVal.Name = "lblAssignVal";
            this.lblAssignVal.Size = new System.Drawing.Size(95, 25);
            this.lblAssignVal.TabIndex = 1;
            this.lblAssignVal.Text = "WAITING";
            // 
            // lblAssignTitle
            // 
            this.lblAssignTitle.AutoSize = true;
            this.lblAssignTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblAssignTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAssignTitle.Location = new System.Drawing.Point(6, 6);
            this.lblAssignTitle.Name = "lblAssignTitle";
            this.lblAssignTitle.Size = new System.Drawing.Size(60, 19);
            this.lblAssignTitle.TabIndex = 0;
            this.lblAssignTitle.Text = "ASSIGN";
            // 
            // cardAppendTo
            // 
            this.cardAppendTo.BackColor = System.Drawing.Color.White;
            this.cardAppendTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardAppendTo.Controls.Add(this.lblAppendToVal);
            this.cardAppendTo.Controls.Add(this.lblAppendToTitle);
            this.cardAppendTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAppendTo.Location = new System.Drawing.Point(659, 5);
            this.cardAppendTo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cardAppendTo.Name = "cardAppendTo";
            this.cardAppendTo.Size = new System.Drawing.Size(158, 66);
            this.cardAppendTo.TabIndex = 4;
            // 
            // lblAppendToVal
            // 
            this.lblAppendToVal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAppendToVal.AutoSize = true;
            this.lblAppendToVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAppendToVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAppendToVal.Location = new System.Drawing.Point(6, 32);
            this.lblAppendToVal.Name = "lblAppendToVal";
            this.lblAppendToVal.Size = new System.Drawing.Size(95, 25);
            this.lblAppendToVal.TabIndex = 1;
            this.lblAppendToVal.Text = "WAITING";
            // 
            // lblAppendToTitle
            // 
            this.lblAppendToTitle.AutoSize = true;
            this.lblAppendToTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblAppendToTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAppendToTitle.Location = new System.Drawing.Point(6, 6);
            this.lblAppendToTitle.Name = "lblAppendToTitle";
            this.lblAppendToTitle.Size = new System.Drawing.Size(89, 19);
            this.lblAppendToTitle.TabIndex = 0;
            this.lblAppendToTitle.Text = "APPEND TO";
            // 
            // cardAppend
            // 
            this.cardAppend.BackColor = System.Drawing.Color.White;
            this.cardAppend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardAppend.Controls.Add(this.lblAppendVal);
            this.cardAppend.Controls.Add(this.lblAppendTitle);
            this.cardAppend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardAppend.Location = new System.Drawing.Point(495, 5);
            this.cardAppend.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cardAppend.Name = "cardAppend";
            this.cardAppend.Size = new System.Drawing.Size(158, 66);
            this.cardAppend.TabIndex = 3;
            // 
            // lblAppendVal
            // 
            this.lblAppendVal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAppendVal.AutoSize = true;
            this.lblAppendVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAppendVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAppendVal.Location = new System.Drawing.Point(6, 32);
            this.lblAppendVal.Name = "lblAppendVal";
            this.lblAppendVal.Size = new System.Drawing.Size(95, 25);
            this.lblAppendVal.TabIndex = 1;
            this.lblAppendVal.Text = "WAITING";
            // 
            // lblAppendTitle
            // 
            this.lblAppendTitle.AutoSize = true;
            this.lblAppendTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblAppendTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAppendTitle.Location = new System.Drawing.Point(6, 6);
            this.lblAppendTitle.Name = "lblAppendTitle";
            this.lblAppendTitle.Size = new System.Drawing.Size(65, 19);
            this.lblAppendTitle.TabIndex = 0;
            this.lblAppendTitle.Text = "APPEND";
            // 
            // cardDelete
            // 
            this.cardDelete.BackColor = System.Drawing.Color.White;
            this.cardDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardDelete.Controls.Add(this.lblDeleteVal);
            this.cardDelete.Controls.Add(this.lblDeleteTitle);
            this.cardDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDelete.Location = new System.Drawing.Point(331, 5);
            this.cardDelete.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cardDelete.Name = "cardDelete";
            this.cardDelete.Size = new System.Drawing.Size(158, 66);
            this.cardDelete.TabIndex = 2;
            // 
            // lblDeleteVal
            // 
            this.lblDeleteVal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDeleteVal.AutoSize = true;
            this.lblDeleteVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDeleteVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblDeleteVal.Location = new System.Drawing.Point(6, 32);
            this.lblDeleteVal.Name = "lblDeleteVal";
            this.lblDeleteVal.Size = new System.Drawing.Size(95, 25);
            this.lblDeleteVal.TabIndex = 1;
            this.lblDeleteVal.Text = "WAITING";
            // 
            // lblDeleteTitle
            // 
            this.lblDeleteTitle.AutoSize = true;
            this.lblDeleteTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblDeleteTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDeleteTitle.Location = new System.Drawing.Point(6, 6);
            this.lblDeleteTitle.Name = "lblDeleteTitle";
            this.lblDeleteTitle.Size = new System.Drawing.Size(58, 19);
            this.lblDeleteTitle.TabIndex = 0;
            this.lblDeleteTitle.Text = "DELETE";
            // 
            // cardWrite
            // 
            this.cardWrite.BackColor = System.Drawing.Color.White;
            this.cardWrite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardWrite.Controls.Add(this.lblWriteVal);
            this.cardWrite.Controls.Add(this.lblWriteTitle);
            this.cardWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardWrite.Location = new System.Drawing.Point(167, 5);
            this.cardWrite.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cardWrite.Name = "cardWrite";
            this.cardWrite.Size = new System.Drawing.Size(158, 66);
            this.cardWrite.TabIndex = 1;
            // 
            // lblWriteVal
            // 
            this.lblWriteVal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWriteVal.AutoSize = true;
            this.lblWriteVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblWriteVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblWriteVal.Location = new System.Drawing.Point(6, 32);
            this.lblWriteVal.Name = "lblWriteVal";
            this.lblWriteVal.Size = new System.Drawing.Size(95, 25);
            this.lblWriteVal.TabIndex = 1;
            this.lblWriteVal.Text = "WAITING";
            // 
            // lblWriteTitle
            // 
            this.lblWriteTitle.AutoSize = true;
            this.lblWriteTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblWriteTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblWriteTitle.Location = new System.Drawing.Point(6, 6);
            this.lblWriteTitle.Name = "lblWriteTitle";
            this.lblWriteTitle.Size = new System.Drawing.Size(52, 19);
            this.lblWriteTitle.TabIndex = 0;
            this.lblWriteTitle.Text = "WRITE";
            // 
            // cardRead
            // 
            this.cardRead.BackColor = System.Drawing.Color.White;
            this.cardRead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardRead.Controls.Add(this.lblReadVal);
            this.cardRead.Controls.Add(this.lblReadTitle);
            this.cardRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRead.Location = new System.Drawing.Point(3, 5);
            this.cardRead.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cardRead.Name = "cardRead";
            this.cardRead.Size = new System.Drawing.Size(158, 66);
            this.cardRead.TabIndex = 0;
            // 
            // lblReadVal
            // 
            this.lblReadVal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReadVal.AutoSize = true;
            this.lblReadVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblReadVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblReadVal.Location = new System.Drawing.Point(6, 32);
            this.lblReadVal.Name = "lblReadVal";
            this.lblReadVal.Size = new System.Drawing.Size(95, 25);
            this.lblReadVal.TabIndex = 1;
            this.lblReadVal.Text = "WAITING";
            // 
            // lblReadTitle
            // 
            this.lblReadTitle.AutoSize = true;
            this.lblReadTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblReadTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblReadTitle.Location = new System.Drawing.Point(6, 6);
            this.lblReadTitle.Name = "lblReadTitle";
            this.lblReadTitle.Size = new System.Drawing.Size(46, 19);
            this.lblReadTitle.TabIndex = 0;
            this.lblReadTitle.Text = "READ";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabRights);
            this.tabControlMain.Controls.Add(this.tabFields);
            this.tabControlMain.Controls.Add(this.tabRoles);
            this.tabControlMain.Controls.Add(this.tabSharing);
            this.tabControlMain.Controls.Add(this.tabSimulate);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabControlMain.Location = new System.Drawing.Point(0, 235);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1150, 415);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabRights
            // 
            this.tabRights.BackColor = System.Drawing.Color.White;
            this.tabRights.Controls.Add(this.dgvRights);
            this.tabRights.Location = new System.Drawing.Point(4, 30);
            this.tabRights.Name = "tabRights";
            this.tabRights.Padding = new System.Windows.Forms.Padding(8);
            this.tabRights.Size = new System.Drawing.Size(1142, 381);
            this.tabRights.TabIndex = 0;
            this.tabRights.Text = "  Record Access Rights  ";
            // 
            // dgvRights
            // 
            this.dgvRights.AllowUserToAddRows = false;
            this.dgvRights.AllowUserToDeleteRows = false;
            this.dgvRights.BackgroundColor = System.Drawing.Color.White;
            this.dgvRights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRights.Location = new System.Drawing.Point(8, 8);
            this.dgvRights.Name = "dgvRights";
            this.dgvRights.ReadOnly = true;
            this.dgvRights.RowHeadersVisible = false;
            this.dgvRights.RowHeadersWidth = 51;
            this.dgvRights.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRights.Size = new System.Drawing.Size(1126, 365);
            this.dgvRights.TabIndex = 0;
            // 
            // tabFields
            // 
            this.tabFields.BackColor = System.Drawing.Color.White;
            this.tabFields.Controls.Add(this.dgvFields);
            this.tabFields.Location = new System.Drawing.Point(4, 30);
            this.tabFields.Name = "tabFields";
            this.tabFields.Padding = new System.Windows.Forms.Padding(8);
            this.tabFields.Size = new System.Drawing.Size(1142, 381);
            this.tabFields.TabIndex = 1;
            this.tabFields.Text = "  Field-Level Security (FLS)  ";
            // 
            // dgvFields
            // 
            this.dgvFields.AllowUserToAddRows = false;
            this.dgvFields.AllowUserToDeleteRows = false;
            this.dgvFields.BackgroundColor = System.Drawing.Color.White;
            this.dgvFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFields.Location = new System.Drawing.Point(8, 8);
            this.dgvFields.Name = "dgvFields";
            this.dgvFields.ReadOnly = true;
            this.dgvFields.RowHeadersVisible = false;
            this.dgvFields.RowHeadersWidth = 51;
            this.dgvFields.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFields.Size = new System.Drawing.Size(1126, 365);
            this.dgvFields.TabIndex = 0;
            // 
            // tabRoles
            // 
            this.tabRoles.BackColor = System.Drawing.Color.White;
            this.tabRoles.Controls.Add(this.dgvRoles);
            this.tabRoles.Location = new System.Drawing.Point(4, 30);
            this.tabRoles.Name = "tabRoles";
            this.tabRoles.Padding = new System.Windows.Forms.Padding(8);
            this.tabRoles.Size = new System.Drawing.Size(1142, 381);
            this.tabRoles.TabIndex = 2;
            this.tabRoles.Text = "  Security Roles & Teams  ";
            // 
            // dgvRoles
            // 
            this.dgvRoles.AllowUserToAddRows = false;
            this.dgvRoles.AllowUserToDeleteRows = false;
            this.dgvRoles.BackgroundColor = System.Drawing.Color.White;
            this.dgvRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoles.Location = new System.Drawing.Point(8, 8);
            this.dgvRoles.Name = "dgvRoles";
            this.dgvRoles.ReadOnly = true;
            this.dgvRoles.RowHeadersVisible = false;
            this.dgvRoles.RowHeadersWidth = 51;
            this.dgvRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoles.Size = new System.Drawing.Size(1126, 365);
            this.dgvRoles.TabIndex = 0;
            // 
            // tabSharing
            // 
            this.tabSharing.BackColor = System.Drawing.Color.White;
            this.tabSharing.Controls.Add(this.dgvSharing);
            this.tabSharing.Location = new System.Drawing.Point(4, 30);
            this.tabSharing.Name = "tabSharing";
            this.tabSharing.Padding = new System.Windows.Forms.Padding(8);
            this.tabSharing.Size = new System.Drawing.Size(1142, 381);
            this.tabSharing.TabIndex = 3;
            this.tabSharing.Text = "  POA & Explicit Sharing  ";
            // 
            // dgvSharing
            // 
            this.dgvSharing.AllowUserToAddRows = false;
            this.dgvSharing.AllowUserToDeleteRows = false;
            this.dgvSharing.BackgroundColor = System.Drawing.Color.White;
            this.dgvSharing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSharing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSharing.Location = new System.Drawing.Point(8, 8);
            this.dgvSharing.Name = "dgvSharing";
            this.dgvSharing.ReadOnly = true;
            this.dgvSharing.RowHeadersVisible = false;
            this.dgvSharing.RowHeadersWidth = 51;
            this.dgvSharing.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSharing.Size = new System.Drawing.Size(1126, 365);
            this.dgvSharing.TabIndex = 0;
            // 
            // tabSimulate
            // 
            this.tabSimulate.BackColor = System.Drawing.Color.White;
            this.tabSimulate.Controls.Add(this.pnlSimulateContainer);
            this.tabSimulate.Location = new System.Drawing.Point(4, 30);
            this.tabSimulate.Name = "tabSimulate";
            this.tabSimulate.Padding = new System.Windows.Forms.Padding(12);
            this.tabSimulate.Size = new System.Drawing.Size(1142, 381);
            this.tabSimulate.TabIndex = 4;
            this.tabSimulate.Text = "  🔮 What-If Simulation Studio  ";
            // 
            // pnlSimulateContainer
            // 
            this.pnlSimulateContainer.Controls.Add(this.txtSimulateLog);
            this.pnlSimulateContainer.Controls.Add(this.btnRunSimulation);
            this.pnlSimulateContainer.Controls.Add(this.grpSimTeams);
            this.pnlSimulateContainer.Controls.Add(this.grpSimRoles);
            this.pnlSimulateContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSimulateContainer.Location = new System.Drawing.Point(12, 12);
            this.pnlSimulateContainer.Name = "pnlSimulateContainer";
            this.pnlSimulateContainer.Size = new System.Drawing.Size(1118, 357);
            this.pnlSimulateContainer.TabIndex = 0;
            // 
            // txtSimulateLog
            // 
            this.txtSimulateLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSimulateLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSimulateLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSimulateLog.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtSimulateLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSimulateLog.Location = new System.Drawing.Point(620, 8);
            this.txtSimulateLog.Multiline = true;
            this.txtSimulateLog.Name = "txtSimulateLog";
            this.txtSimulateLog.ReadOnly = true;
            this.txtSimulateLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSimulateLog.Size = new System.Drawing.Size(490, 340);
            this.txtSimulateLog.TabIndex = 3;
            // 
            // btnRunSimulation
            // 
            this.btnRunSimulation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.btnRunSimulation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRunSimulation.FlatAppearance.BorderSize = 0;
            this.btnRunSimulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunSimulation.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRunSimulation.ForeColor = System.Drawing.Color.White;
            this.btnRunSimulation.Location = new System.Drawing.Point(8, 305);
            this.btnRunSimulation.Name = "btnRunSimulation";
            this.btnRunSimulation.Size = new System.Drawing.Size(595, 42);
            this.btnRunSimulation.TabIndex = 2;
            this.btnRunSimulation.Text = "⚡  Simulate Permission Changes";
            this.btnRunSimulation.UseVisualStyleBackColor = false;
            this.btnRunSimulation.Click += new System.EventHandler(this.btnRunSimulation_Click);
            // 
            // grpSimTeams
            // 
            this.grpSimTeams.Controls.Add(this.chkListTeams);
            this.grpSimTeams.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpSimTeams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.grpSimTeams.Location = new System.Drawing.Point(310, 8);
            this.grpSimTeams.Name = "grpSimTeams";
            this.grpSimTeams.Padding = new System.Windows.Forms.Padding(8);
            this.grpSimTeams.Size = new System.Drawing.Size(295, 290);
            this.grpSimTeams.TabIndex = 1;
            this.grpSimTeams.TabStop = false;
            this.grpSimTeams.Text = "Add Simulated Teams";
            // 
            // chkListTeams
            // 
            this.chkListTeams.BackColor = System.Drawing.Color.White;
            this.chkListTeams.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkListTeams.CheckOnClick = true;
            this.chkListTeams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkListTeams.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkListTeams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.chkListTeams.FormattingEnabled = true;
            this.chkListTeams.Location = new System.Drawing.Point(8, 28);
            this.chkListTeams.Name = "chkListTeams";
            this.chkListTeams.Size = new System.Drawing.Size(279, 254);
            this.chkListTeams.TabIndex = 0;
            // 
            // grpSimRoles
            // 
            this.grpSimRoles.Controls.Add(this.chkListRoles);
            this.grpSimRoles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpSimRoles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.grpSimRoles.Location = new System.Drawing.Point(8, 8);
            this.grpSimRoles.Name = "grpSimRoles";
            this.grpSimRoles.Padding = new System.Windows.Forms.Padding(8);
            this.grpSimRoles.Size = new System.Drawing.Size(295, 290);
            this.grpSimRoles.TabIndex = 0;
            this.grpSimRoles.TabStop = false;
            this.grpSimRoles.Text = "Add Simulated Security Roles";
            // 
            // chkListRoles
            // 
            this.chkListRoles.BackColor = System.Drawing.Color.White;
            this.chkListRoles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkListRoles.CheckOnClick = true;
            this.chkListRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkListRoles.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkListRoles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.chkListRoles.FormattingEnabled = true;
            this.chkListRoles.Location = new System.Drawing.Point(8, 28);
            this.chkListRoles.Name = "chkListRoles";
            this.chkListRoles.Size = new System.Drawing.Size(279, 254);
            this.chkListRoles.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1150, 26);
            this.statusStrip1.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1135, 20);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Ready. Select a User and paste a Record URL / GUID to inspect permissions.";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(140, 18);
            this.progressBar.Visible = false;
            // 
            // PermissionsInspectorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.tblCards);
            this.Controls.Add(this.pnlInputs);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "PermissionsInspectorControl";
            this.Size = new System.Drawing.Size(1150, 676);
            this.Load += new System.EventHandler(this.PermissionsInspectorControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlInputs.ResumeLayout(false);
            this.tblInputs.ResumeLayout(false);
            this.tblInputs.PerformLayout();
            this.tblCards.ResumeLayout(false);
            this.cardShare.ResumeLayout(false);
            this.cardShare.PerformLayout();
            this.cardAssign.ResumeLayout(false);
            this.cardAssign.PerformLayout();
            this.cardAppendTo.ResumeLayout(false);
            this.cardAppendTo.PerformLayout();
            this.cardAppend.ResumeLayout(false);
            this.cardAppend.PerformLayout();
            this.cardDelete.ResumeLayout(false);
            this.cardDelete.PerformLayout();
            this.cardWrite.ResumeLayout(false);
            this.cardWrite.PerformLayout();
            this.cardRead.ResumeLayout(false);
            this.cardRead.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabRights.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRights)).EndInit();
            this.tabFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFields)).EndInit();
            this.tabRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).EndInit();
            this.tabSharing.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSharing)).EndInit();
            this.tabSimulate.ResumeLayout(false);
            this.pnlSimulateContainer.ResumeLayout(false);
            this.grpSimTeams.ResumeLayout(false);
            this.grpSimRoles.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnInspectTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExportHtml;
        private System.Windows.Forms.ToolStripButton btnExportCsv;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSimulateTab;
        private System.Windows.Forms.ToolStripButton btnSponsor;
        private System.Windows.Forms.Panel pnlInputs;
        private System.Windows.Forms.TableLayoutPanel tblInputs;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtSearchUser;
        private System.Windows.Forms.Button btnSearchUser;
        private System.Windows.Forms.ComboBox cboUsers;
        private System.Windows.Forms.Button btnLoadUsers;
        private System.Windows.Forms.Label lblRecordUrl;
        private System.Windows.Forms.TextBox txtRecordUrlOrId;
        private System.Windows.Forms.Button btnInspect;
        private System.Windows.Forms.TableLayoutPanel tblCards;
        private System.Windows.Forms.Panel cardRead;
        private System.Windows.Forms.Label lblReadVal;
        private System.Windows.Forms.Label lblReadTitle;
        private System.Windows.Forms.Panel cardWrite;
        private System.Windows.Forms.Label lblWriteVal;
        private System.Windows.Forms.Label lblWriteTitle;
        private System.Windows.Forms.Panel cardDelete;
        private System.Windows.Forms.Label lblDeleteVal;
        private System.Windows.Forms.Label lblDeleteTitle;
        private System.Windows.Forms.Panel cardAppend;
        private System.Windows.Forms.Label lblAppendVal;
        private System.Windows.Forms.Label lblAppendTitle;
        private System.Windows.Forms.Panel cardAppendTo;
        private System.Windows.Forms.Label lblAppendToVal;
        private System.Windows.Forms.Label lblAppendToTitle;
        private System.Windows.Forms.Panel cardAssign;
        private System.Windows.Forms.Label lblAssignVal;
        private System.Windows.Forms.Label lblAssignTitle;
        private System.Windows.Forms.Panel cardShare;
        private System.Windows.Forms.Label lblShareVal;
        private System.Windows.Forms.Label lblShareTitle;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabRights;
        private System.Windows.Forms.DataGridView dgvRights;
        private System.Windows.Forms.TabPage tabFields;
        private System.Windows.Forms.DataGridView dgvFields;
        private System.Windows.Forms.TabPage tabRoles;
        private System.Windows.Forms.DataGridView dgvRoles;
        private System.Windows.Forms.TabPage tabSharing;
        private System.Windows.Forms.DataGridView dgvSharing;
        private System.Windows.Forms.TabPage tabSimulate;
        private System.Windows.Forms.Panel pnlSimulateContainer;
        private System.Windows.Forms.GroupBox grpSimRoles;
        private System.Windows.Forms.CheckedListBox chkListRoles;
        private System.Windows.Forms.GroupBox grpSimTeams;
        private System.Windows.Forms.CheckedListBox chkListTeams;
        private System.Windows.Forms.Button btnRunSimulation;
        private System.Windows.Forms.TextBox txtSimulateLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
    }
}
