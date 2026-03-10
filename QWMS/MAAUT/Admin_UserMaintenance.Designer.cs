namespace QWMS
{
    partial class Admin_UserMaintenance
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnList = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtPasswd = new System.Windows.Forms.TextBox();
            this.txtUsrnm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.rdoAdd = new System.Windows.Forms.RadioButton();
            this.rdoUpdate = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Chk = new System.Windows.Forms.TabControl();
            this.tbpMaaut = new System.Windows.Forms.TabPage();
            this.chkMaaut = new System.Windows.Forms.CheckedListBox();
            this.tbpMgaut = new System.Windows.Forms.TabPage();
            this.chkMgaut = new System.Windows.Forms.CheckedListBox();
            this.tbpIvaut = new System.Windows.Forms.TabPage();
            this.chkIvaut = new System.Windows.Forms.CheckedListBox();
            this.tboOtaut = new System.Windows.Forms.TabPage();
            this.chkOtaut = new System.Windows.Forms.CheckedListBox();
            this.tbpInaut = new System.Windows.Forms.TabPage();
            this.chkInaut = new System.Windows.Forms.CheckedListBox();
            this.tbpWkaut = new System.Windows.Forms.TabPage();
            this.chkWkaut = new System.Windows.Forms.CheckedListBox();
            this.tbpRepln = new System.Windows.Forms.TabPage();
            this.chkRepln = new System.Windows.Forms.CheckedListBox();
            this.tbpTrans = new System.Windows.Forms.TabPage();
            this.chkTrans = new System.Windows.Forms.CheckedListBox();
            this.stbStatus = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkAgaut = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbFunction.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Chk.SuspendLayout();
            this.tbpMaaut.SuspendLayout();
            this.tbpMgaut.SuspendLayout();
            this.tbpIvaut.SuspendLayout();
            this.tboOtaut.SuspendLayout();
            this.tbpInaut.SuspendLayout();
            this.tbpWkaut.SuspendLayout();
            this.tbpRepln.SuspendLayout();
            this.tbpTrans.SuspendLayout();
            this.stbStatus.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.gbFunction);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 460);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 382);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(204, 35);
            this.label3.TabIndex = 2;
            this.label3.Text = "※Add与Delete可以批次处理(用户名以逗号分隔)";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnList);
            this.groupBox2.Controls.Add(this.btnQuery);
            this.groupBox2.Controls.Add(this.txtPasswd);
            this.groupBox2.Controls.Add(this.txtUsrnm);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(21, 179);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(267, 184);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User Data";
            // 
            // btnList
            // 
            this.btnList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnList.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnList.Location = new System.Drawing.Point(60, 150);
            this.btnList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(100, 26);
            this.btnList.TabIndex = 5;
            this.btnList.Text = "User List";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuery.Enabled = false;
            this.btnQuery.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuery.Location = new System.Drawing.Point(60, 114);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 26);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtPasswd
            // 
            this.txtPasswd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPasswd.Enabled = false;
            this.txtPasswd.Location = new System.Drawing.Point(103, 82);
            this.txtPasswd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(155, 26);
            this.txtPasswd.TabIndex = 3;
            // 
            // txtUsrnm
            // 
            this.txtUsrnm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsrnm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsrnm.Enabled = false;
            this.txtUsrnm.Location = new System.Drawing.Point(103, 46);
            this.txtUsrnm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsrnm.Name = "txtUsrnm";
            this.txtUsrnm.Size = new System.Drawing.Size(155, 26);
            this.txtUsrnm.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Id";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbFunction
            // 
            this.gbFunction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFunction.Controls.Add(this.rdoAdd);
            this.gbFunction.Controls.Add(this.rdoUpdate);
            this.gbFunction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gbFunction.Location = new System.Drawing.Point(21, 24);
            this.gbFunction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbFunction.Size = new System.Drawing.Size(267, 128);
            this.gbFunction.TabIndex = 0;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Function";
            // 
            // rdoAdd
            // 
            this.rdoAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rdoAdd.AutoSize = true;
            this.rdoAdd.Location = new System.Drawing.Point(24, 74);
            this.rdoAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoAdd.Name = "rdoAdd";
            this.rdoAdd.Size = new System.Drawing.Size(61, 23);
            this.rdoAdd.TabIndex = 2;
            this.rdoAdd.Text = "Add";
            this.rdoAdd.UseVisualStyleBackColor = true;
            this.rdoAdd.CheckedChanged += new System.EventHandler(this.rdoAdd_CheckedChanged);
            // 
            // rdoUpdate
            // 
            this.rdoUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rdoUpdate.AutoSize = true;
            this.rdoUpdate.Location = new System.Drawing.Point(25, 39);
            this.rdoUpdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdoUpdate.Name = "rdoUpdate";
            this.rdoUpdate.Size = new System.Drawing.Size(86, 23);
            this.rdoUpdate.TabIndex = 0;
            this.rdoUpdate.Text = "Update";
            this.rdoUpdate.UseVisualStyleBackColor = true;
            this.rdoUpdate.CheckedChanged += new System.EventHandler(this.rdoUpdate_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.Chk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(299, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(624, 460);
            this.panel2.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(345, 391);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 26);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(191, 391);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 26);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(36, 391);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 26);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Chk
            // 
            this.Chk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Chk.Controls.Add(this.tbpMaaut);
            this.Chk.Controls.Add(this.tbpMgaut);
            this.Chk.Controls.Add(this.tbpIvaut);
            this.Chk.Controls.Add(this.tboOtaut);
            this.Chk.Controls.Add(this.tbpInaut);
            this.Chk.Controls.Add(this.tbpWkaut);
            this.Chk.Controls.Add(this.tbpRepln);
            this.Chk.Controls.Add(this.tbpTrans);
            this.Chk.Controls.Add(this.tabPage1);
            this.Chk.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.Chk.Location = new System.Drawing.Point(23, 12);
            this.Chk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Chk.Multiline = true;
            this.Chk.Name = "Chk";
            this.Chk.SelectedIndex = 0;
            this.Chk.Size = new System.Drawing.Size(587, 368);
            this.Chk.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.Chk.TabIndex = 0;
            // 
            // tbpMaaut
            // 
            this.tbpMaaut.Controls.Add(this.chkMaaut);
            this.tbpMaaut.Location = new System.Drawing.Point(4, 52);
            this.tbpMaaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpMaaut.Name = "tbpMaaut";
            this.tbpMaaut.Size = new System.Drawing.Size(579, 312);
            this.tbpMaaut.TabIndex = 0;
            this.tbpMaaut.Text = "System Maintenance";
            this.tbpMaaut.UseVisualStyleBackColor = true;
            // 
            // chkMaaut
            // 
            this.chkMaaut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMaaut.CheckOnClick = true;
            this.chkMaaut.FormattingEnabled = true;
            this.chkMaaut.Location = new System.Drawing.Point(17, 14);
            this.chkMaaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMaaut.Name = "chkMaaut";
            this.chkMaaut.Size = new System.Drawing.Size(532, 158);
            this.chkMaaut.TabIndex = 0;
            // 
            // tbpMgaut
            // 
            this.tbpMgaut.Controls.Add(this.chkMgaut);
            this.tbpMgaut.Location = new System.Drawing.Point(4, 52);
            this.tbpMgaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpMgaut.Name = "tbpMgaut";
            this.tbpMgaut.Size = new System.Drawing.Size(579, 312);
            this.tbpMgaut.TabIndex = 1;
            this.tbpMgaut.Text = "Management";
            this.tbpMgaut.UseVisualStyleBackColor = true;
            // 
            // chkMgaut
            // 
            this.chkMgaut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMgaut.CheckOnClick = true;
            this.chkMgaut.FormattingEnabled = true;
            this.chkMgaut.Location = new System.Drawing.Point(16, 14);
            this.chkMgaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMgaut.Name = "chkMgaut";
            this.chkMgaut.Size = new System.Drawing.Size(532, 246);
            this.chkMgaut.TabIndex = 0;
            // 
            // tbpIvaut
            // 
            this.tbpIvaut.Controls.Add(this.chkIvaut);
            this.tbpIvaut.Location = new System.Drawing.Point(4, 52);
            this.tbpIvaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpIvaut.Name = "tbpIvaut";
            this.tbpIvaut.Size = new System.Drawing.Size(579, 312);
            this.tbpIvaut.TabIndex = 2;
            this.tbpIvaut.Text = "Physical Counting";
            this.tbpIvaut.UseVisualStyleBackColor = true;
            // 
            // chkIvaut
            // 
            this.chkIvaut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIvaut.CheckOnClick = true;
            this.chkIvaut.FormattingEnabled = true;
            this.chkIvaut.Location = new System.Drawing.Point(27, 14);
            this.chkIvaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkIvaut.Name = "chkIvaut";
            this.chkIvaut.Size = new System.Drawing.Size(532, 246);
            this.chkIvaut.TabIndex = 0;
            // 
            // tboOtaut
            // 
            this.tboOtaut.Controls.Add(this.chkOtaut);
            this.tboOtaut.Location = new System.Drawing.Point(4, 52);
            this.tboOtaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tboOtaut.Name = "tboOtaut";
            this.tboOtaut.Size = new System.Drawing.Size(579, 312);
            this.tboOtaut.TabIndex = 3;
            this.tboOtaut.Text = "Goods Issue";
            this.tboOtaut.UseVisualStyleBackColor = true;
            // 
            // chkOtaut
            // 
            this.chkOtaut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOtaut.CheckOnClick = true;
            this.chkOtaut.FormattingEnabled = true;
            this.chkOtaut.Location = new System.Drawing.Point(24, 14);
            this.chkOtaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkOtaut.Name = "chkOtaut";
            this.chkOtaut.Size = new System.Drawing.Size(532, 224);
            this.chkOtaut.TabIndex = 0;
            // 
            // tbpInaut
            // 
            this.tbpInaut.Controls.Add(this.chkInaut);
            this.tbpInaut.Location = new System.Drawing.Point(4, 52);
            this.tbpInaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpInaut.Name = "tbpInaut";
            this.tbpInaut.Size = new System.Drawing.Size(579, 312);
            this.tbpInaut.TabIndex = 4;
            this.tbpInaut.Text = "Goods Receipt";
            this.tbpInaut.UseVisualStyleBackColor = true;
            // 
            // chkInaut
            // 
            this.chkInaut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInaut.CheckOnClick = true;
            this.chkInaut.FormattingEnabled = true;
            this.chkInaut.Location = new System.Drawing.Point(17, 14);
            this.chkInaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkInaut.Name = "chkInaut";
            this.chkInaut.Size = new System.Drawing.Size(532, 180);
            this.chkInaut.TabIndex = 0;
            // 
            // tbpWkaut
            // 
            this.tbpWkaut.Controls.Add(this.chkWkaut);
            this.tbpWkaut.Location = new System.Drawing.Point(4, 52);
            this.tbpWkaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpWkaut.Name = "tbpWkaut";
            this.tbpWkaut.Size = new System.Drawing.Size(579, 312);
            this.tbpWkaut.TabIndex = 5;
            this.tbpWkaut.Text = "Plant Authority";
            this.tbpWkaut.UseVisualStyleBackColor = true;
            // 
            // chkWkaut
            // 
            this.chkWkaut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkWkaut.CheckOnClick = true;
            this.chkWkaut.FormattingEnabled = true;
            this.chkWkaut.Location = new System.Drawing.Point(17, 14);
            this.chkWkaut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkWkaut.Name = "chkWkaut";
            this.chkWkaut.Size = new System.Drawing.Size(532, 180);
            this.chkWkaut.TabIndex = 0;
            this.chkWkaut.SelectedIndexChanged += new System.EventHandler(this.chkWkaut_SelectedIndexChanged);
            this.chkWkaut.DoubleClick += new System.EventHandler(this.chkWkaut_DoubleClick);
            // 
            // tbpRepln
            // 
            this.tbpRepln.Controls.Add(this.chkRepln);
            this.tbpRepln.Location = new System.Drawing.Point(4, 52);
            this.tbpRepln.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpRepln.Name = "tbpRepln";
            this.tbpRepln.Size = new System.Drawing.Size(579, 312);
            this.tbpRepln.TabIndex = 6;
            this.tbpRepln.Text = "GB Transfer";
            this.tbpRepln.UseVisualStyleBackColor = true;
            // 
            // chkRepln
            // 
            this.chkRepln.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRepln.CheckOnClick = true;
            this.chkRepln.FormattingEnabled = true;
            this.chkRepln.Location = new System.Drawing.Point(16, 14);
            this.chkRepln.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkRepln.Name = "chkRepln";
            this.chkRepln.Size = new System.Drawing.Size(532, 202);
            this.chkRepln.TabIndex = 0;
            // 
            // tbpTrans
            // 
            this.tbpTrans.Controls.Add(this.chkTrans);
            this.tbpTrans.Location = new System.Drawing.Point(4, 52);
            this.tbpTrans.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpTrans.Name = "tbpTrans";
            this.tbpTrans.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbpTrans.Size = new System.Drawing.Size(579, 312);
            this.tbpTrans.TabIndex = 7;
            this.tbpTrans.Text = "Transfer";
            this.tbpTrans.UseVisualStyleBackColor = true;
            // 
            // chkTrans
            // 
            this.chkTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTrans.FormattingEnabled = true;
            this.chkTrans.Location = new System.Drawing.Point(4, 4);
            this.chkTrans.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkTrans.Name = "chkTrans";
            this.chkTrans.Size = new System.Drawing.Size(571, 304);
            this.chkTrans.TabIndex = 0;
            // 
            // stbStatus
            // 
            this.stbStatus.BackColor = System.Drawing.SystemColors.Control;
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.stbStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.stbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.Location = new System.Drawing.Point(299, 438);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.stbStatus.Size = new System.Drawing.Size(624, 22);
            this.stbStatus.TabIndex = 39;
            this.stbStatus.Text = "Status";
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(40, 16);
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(40, 16);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(70, 16);
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(185, 16);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(65, 16);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkAgaut);
            this.tabPage1.Location = new System.Drawing.Point(4, 52);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(579, 312);
            this.tabPage1.TabIndex = 8;
            this.tabPage1.Text = "AGV Intelligence";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkAgaut
            // 
            this.chkAgaut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAgaut.FormattingEnabled = true;
            this.chkAgaut.Location = new System.Drawing.Point(3, 3);
            this.chkAgaut.Margin = new System.Windows.Forms.Padding(4);
            this.chkAgaut.Name = "chkAgaut";
            this.chkAgaut.Size = new System.Drawing.Size(573, 306);
            this.chkAgaut.TabIndex = 1;
            // 
            // Admin_UserMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 460);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Admin_UserMaintenance";
            this.Text = "User Account Maintenance";
            this.Resize += new System.EventHandler(this.Admin_UserMaintenance_Resize);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbFunction.ResumeLayout(false);
            this.gbFunction.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.Chk.ResumeLayout(false);
            this.tbpMaaut.ResumeLayout(false);
            this.tbpMgaut.ResumeLayout(false);
            this.tbpIvaut.ResumeLayout(false);
            this.tboOtaut.ResumeLayout(false);
            this.tbpInaut.ResumeLayout(false);
            this.tbpWkaut.ResumeLayout(false);
            this.tbpRepln.ResumeLayout(false);
            this.tbpTrans.ResumeLayout(false);
            this.stbStatus.ResumeLayout(false);
            this.stbStatus.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.RadioButton rdoUpdate;
        private System.Windows.Forms.RadioButton rdoAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtUsrnm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtPasswd;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        protected internal System.Windows.Forms.StatusStrip stbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl Chk;
        private System.Windows.Forms.TabPage tbpMaaut;
        private System.Windows.Forms.CheckedListBox chkMaaut;
        private System.Windows.Forms.TabPage tbpMgaut;
        private System.Windows.Forms.CheckedListBox chkMgaut;
        private System.Windows.Forms.TabPage tbpIvaut;
        private System.Windows.Forms.CheckedListBox chkIvaut;
        private System.Windows.Forms.TabPage tboOtaut;
        private System.Windows.Forms.CheckedListBox chkOtaut;
        private System.Windows.Forms.TabPage tbpInaut;
        private System.Windows.Forms.CheckedListBox chkInaut;
        private System.Windows.Forms.TabPage tbpWkaut;
        private System.Windows.Forms.CheckedListBox chkWkaut;
        private System.Windows.Forms.TabPage tbpRepln;
        private System.Windows.Forms.CheckedListBox chkRepln;
        private System.Windows.Forms.TabPage tbpTrans;
        private System.Windows.Forms.CheckedListBox chkTrans;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckedListBox chkAgaut;
    }
}