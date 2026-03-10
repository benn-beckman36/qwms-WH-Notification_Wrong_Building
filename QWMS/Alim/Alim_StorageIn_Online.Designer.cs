namespace QWMS
{
    partial class Alim_StorageIn_Online
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTdate = new System.Windows.Forms.DateTimePicker();
            this.lblTdate = new System.Windows.Forms.Label();
            this.lblFdate = new System.Windows.Forms.Label();
            this.dtpFdate = new System.Windows.Forms.DateTimePicker();
            this.txtBwart = new System.Windows.Forms.TextBox();
            this.lblBwart = new System.Windows.Forms.Label();
            this.cmbRunner = new System.Windows.Forms.ComboBox();
            this.lbRunner = new System.Windows.Forms.Label();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.checkLock = new System.Windows.Forms.CheckBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.lbMblnr = new System.Windows.Forms.Label();
            this.lbStorage = new System.Windows.Forms.Label();
            this.lbPlant = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvMblnr = new System.Windows.Forms.DataGridView();
            this.lblCount = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvRunner = new System.Windows.Forms.DataGridView();
            this.btnLockRun = new System.Windows.Forms.Button();
            this.btEnd = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btStart = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMblnr)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunner)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.txtBwart);
            this.splitContainer1.Panel1.Controls.Add(this.lblBwart);
            this.splitContainer1.Panel1.Controls.Add(this.cmbRunner);
            this.splitContainer1.Panel1.Controls.Add(this.lbRunner);
            this.splitContainer1.Panel1.Controls.Add(this.txtMblnr);
            this.splitContainer1.Panel1.Controls.Add(this.checkLock);
            this.splitContainer1.Panel1.Controls.Add(this.cmbLgort);
            this.splitContainer1.Panel1.Controls.Add(this.cmbWerks);
            this.splitContainer1.Panel1.Controls.Add(this.lbMblnr);
            this.splitContainer1.Panel1.Controls.Add(this.lbStorage);
            this.splitContainer1.Panel1.Controls.Add(this.lbPlant);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(974, 449);
            this.splitContainer1.SplitterDistance = 123;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTdate);
            this.groupBox1.Controls.Add(this.lblTdate);
            this.groupBox1.Controls.Add(this.lblFdate);
            this.groupBox1.Controls.Add(this.dtpFdate);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date";
            // 
            // dtpTdate
            // 
            this.dtpTdate.CalendarFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpTdate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTdate.Location = new System.Drawing.Point(65, 63);
            this.dtpTdate.Name = "dtpTdate";
            this.dtpTdate.Size = new System.Drawing.Size(96, 22);
            this.dtpTdate.TabIndex = 50;
            // 
            // lblTdate
            // 
            this.lblTdate.AutoSize = true;
            this.lblTdate.Location = new System.Drawing.Point(36, 63);
            this.lblTdate.Name = "lblTdate";
            this.lblTdate.Size = new System.Drawing.Size(20, 16);
            this.lblTdate.TabIndex = 51;
            this.lblTdate.Text = "to";
            // 
            // lblFdate
            // 
            this.lblFdate.AutoSize = true;
            this.lblFdate.Location = new System.Drawing.Point(19, 31);
            this.lblFdate.Name = "lblFdate";
            this.lblFdate.Size = new System.Drawing.Size(37, 16);
            this.lblFdate.TabIndex = 50;
            this.lblFdate.Text = "from";
            // 
            // dtpFdate
            // 
            this.dtpFdate.CalendarFont = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpFdate.CustomFormat = "";
            this.dtpFdate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFdate.Location = new System.Drawing.Point(65, 26);
            this.dtpFdate.Name = "dtpFdate";
            this.dtpFdate.Size = new System.Drawing.Size(96, 22);
            this.dtpFdate.TabIndex = 49;
            this.dtpFdate.Value = new System.DateTime(2020, 5, 21, 8, 55, 52, 0);
            // 
            // txtBwart
            // 
            this.txtBwart.Location = new System.Drawing.Point(296, 73);
            this.txtBwart.Name = "txtBwart";
            this.txtBwart.Size = new System.Drawing.Size(121, 22);
            this.txtBwart.TabIndex = 23;
            // 
            // lblBwart
            // 
            this.lblBwart.AutoSize = true;
            this.lblBwart.Location = new System.Drawing.Point(239, 76);
            this.lblBwart.Name = "lblBwart";
            this.lblBwart.Size = new System.Drawing.Size(34, 16);
            this.lblBwart.TabIndex = 22;
            this.lblBwart.Text = "Mvt.";
            // 
            // cmbRunner
            // 
            this.cmbRunner.FormattingEnabled = true;
            this.cmbRunner.Location = new System.Drawing.Point(782, 73);
            this.cmbRunner.Name = "cmbRunner";
            this.cmbRunner.Size = new System.Drawing.Size(121, 24);
            this.cmbRunner.TabIndex = 21;
            this.cmbRunner.SelectedIndexChanged += new System.EventHandler(this.cmbRunner_SelectedIndexChanged);
            // 
            // lbRunner
            // 
            this.lbRunner.AutoSize = true;
            this.lbRunner.Location = new System.Drawing.Point(708, 76);
            this.lbRunner.Name = "lbRunner";
            this.lbRunner.Size = new System.Drawing.Size(54, 16);
            this.lbRunner.TabIndex = 20;
            this.lbRunner.Text = "Runner";
            // 
            // txtMblnr
            // 
            this.txtMblnr.Location = new System.Drawing.Point(550, 72);
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(140, 22);
            this.txtMblnr.TabIndex = 19;
            this.txtMblnr.DoubleClick += new System.EventHandler(this.txtMblnr_DoubleClick);
            this.txtMblnr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMblnr_KeyPress);
            // 
            // checkLock
            // 
            this.checkLock.AutoSize = true;
            this.checkLock.Location = new System.Drawing.Point(742, 30);
            this.checkLock.Name = "checkLock";
            this.checkLock.Size = new System.Drawing.Size(107, 20);
            this.checkLock.TabIndex = 18;
            this.checkLock.Text = "LockStorage";
            this.checkLock.UseVisualStyleBackColor = true;
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(550, 28);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(140, 24);
            this.cmbLgort.TabIndex = 17;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(296, 28);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 24);
            this.cmbWerks.TabIndex = 16;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // lbMblnr
            // 
            this.lbMblnr.AutoSize = true;
            this.lbMblnr.Location = new System.Drawing.Point(440, 76);
            this.lbMblnr.Name = "lbMblnr";
            this.lbMblnr.Size = new System.Drawing.Size(93, 16);
            this.lbMblnr.TabIndex = 15;
            this.lbMblnr.Text = "Document No";
            // 
            // lbStorage
            // 
            this.lbStorage.AutoSize = true;
            this.lbStorage.Location = new System.Drawing.Point(467, 31);
            this.lbStorage.Name = "lbStorage";
            this.lbStorage.Size = new System.Drawing.Size(58, 16);
            this.lbStorage.TabIndex = 14;
            this.lbStorage.Text = "Storage";
            // 
            // lbPlant
            // 
            this.lbPlant.AutoSize = true;
            this.lbPlant.Location = new System.Drawing.Point(232, 31);
            this.lbPlant.Name = "lbPlant";
            this.lbPlant.Size = new System.Drawing.Size(41, 16);
            this.lbPlant.TabIndex = 13;
            this.lbPlant.Text = "Plant";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btEnd);
            this.splitContainer3.Panel2.Controls.Add(this.btnRefresh);
            this.splitContainer3.Panel2.Controls.Add(this.btStart);
            this.splitContainer3.Panel2.Controls.Add(this.btnExit);
            this.splitContainer3.Panel2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.splitContainer3.Size = new System.Drawing.Size(974, 322);
            this.splitContainer3.SplitterDistance = 270;
            this.splitContainer3.TabIndex = 39;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(974, 270);
            this.tabControl.TabIndex = 38;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMblnr);
            this.tabPage1.Controls.Add(this.lblCount);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(966, 244);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Mblnr";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvMblnr
            // 
            this.dgvMblnr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMblnr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMblnr.Location = new System.Drawing.Point(19, 40);
            this.dgvMblnr.Name = "dgvMblnr";
            this.dgvMblnr.RowTemplate.Height = 24;
            this.dgvMblnr.Size = new System.Drawing.Size(920, 184);
            this.dgvMblnr.TabIndex = 36;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Location = new System.Drawing.Point(19, 15);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(61, 14);
            this.lblCount.TabIndex = 37;
            this.lblCount.Text = "0 records";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvRunner);
            this.tabPage2.Controls.Add(this.btnLockRun);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(966, 244);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Runner";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvRunner
            // 
            this.dgvRunner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRunner.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRunner.Location = new System.Drawing.Point(22, 43);
            this.dgvRunner.Name = "dgvRunner";
            this.dgvRunner.RowTemplate.Height = 24;
            this.dgvRunner.Size = new System.Drawing.Size(923, 182);
            this.dgvRunner.TabIndex = 37;
            // 
            // btnLockRun
            // 
            this.btnLockRun.Location = new System.Drawing.Point(22, 9);
            this.btnLockRun.Name = "btnLockRun";
            this.btnLockRun.Size = new System.Drawing.Size(107, 28);
            this.btnLockRun.TabIndex = 9;
            this.btnLockRun.Text = "绑定主流道";
            this.btnLockRun.UseVisualStyleBackColor = true;
            this.btnLockRun.Click += new System.EventHandler(this.btnLockRun_Click);
            // 
            // btEnd
            // 
            this.btEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btEnd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btEnd.Location = new System.Drawing.Point(438, 8);
            this.btEnd.Name = "btEnd";
            this.btEnd.Size = new System.Drawing.Size(75, 27);
            this.btEnd.TabIndex = 1;
            this.btEnd.Text = "End";
            this.btEnd.UseVisualStyleBackColor = true;
            this.btEnd.Click += new System.EventHandler(this.btEnd_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(595, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 26);
            this.btnRefresh.TabIndex = 34;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btStart
            // 
            this.btStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btStart.Enabled = false;
            this.btStart.Location = new System.Drawing.Point(285, 8);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 28);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(731, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(84, 26);
            this.btnExit.TabIndex = 35;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 449);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(974, 22);
            this.statusStrip1.TabIndex = 45;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(50, 17);
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(50, 17);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(80, 17);
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(400, 17);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 17);
            // 
            // Alim_StorageIn_Online
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 471);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Alim_StorageIn_Online";
            this.Text = "Alim_StorageIn_Online";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Alim_StorageIn_Online_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMblnr)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRunner)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.Button btnLockRun;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button btEnd;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvMblnr;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvRunner;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lblBwart;
        private System.Windows.Forms.ComboBox cmbRunner;
        private System.Windows.Forms.Label lbRunner;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.CheckBox checkLock;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label lbMblnr;
        private System.Windows.Forms.Label lbStorage;
        private System.Windows.Forms.Label lbPlant;
        private System.Windows.Forms.TextBox txtBwart;
        private System.Windows.Forms.DateTimePicker dtpFdate;
        private System.Windows.Forms.DateTimePicker dtpTdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTdate;
        private System.Windows.Forms.Label lblFdate;
    }
}