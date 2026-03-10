namespace QWMS
{
    partial class InventoryCheck_RTime
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
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.lblLocat = new System.Windows.Forms.Label();
            this.lblInvNo = new System.Windows.Forms.Label();
            this.lblPlant = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.lblStroage = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoSN = new System.Windows.Forms.RadioButton();
            this.rdoBoxid = new System.Windows.Forms.RadioButton();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblitm = new System.Windows.Forms.Label();
            this.lblQR = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.cmbInvNo = new System.Windows.Forms.ComboBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblCount2 = new System.Windows.Forms.Label();
            this.lblCount1 = new System.Windows.Forms.Label();
            this.dgvError = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLocat
            // 
            this.txtLocat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtLocat.Location = new System.Drawing.Point(564, 17);
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(77, 25);
            this.txtLocat.TabIndex = 23;
            this.txtLocat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocat_KeyDown);
            // 
            // lblLocat
            // 
            this.lblLocat.AutoSize = true;
            this.lblLocat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocat.Location = new System.Drawing.Point(504, 21);
            this.lblLocat.Name = "lblLocat";
            this.lblLocat.Size = new System.Drawing.Size(46, 18);
            this.lblLocat.TabIndex = 22;
            this.lblLocat.Text = "储位:";
            // 
            // lblInvNo
            // 
            this.lblInvNo.AutoSize = true;
            this.lblInvNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblInvNo.Location = new System.Drawing.Point(14, 103);
            this.lblInvNo.Name = "lblInvNo";
            this.lblInvNo.Size = new System.Drawing.Size(93, 18);
            this.lblInvNo.TabIndex = 16;
            this.lblInvNo.Text = "盘点票号：";
            // 
            // lblPlant
            // 
            this.lblPlant.AutoSize = true;
            this.lblPlant.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlant.Location = new System.Drawing.Point(45, 9);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(46, 18);
            this.lblPlant.TabIndex = 18;
            this.lblPlant.Text = "厂区:";
            // 
            // cmbWerks
            // 
            this.cmbWerks.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(110, 4);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(80, 26);
            this.cmbWerks.TabIndex = 19;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // lblStroage
            // 
            this.lblStroage.AutoSize = true;
            this.lblStroage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblStroage.Location = new System.Drawing.Point(43, 40);
            this.lblStroage.Name = "lblStroage";
            this.lblStroage.Size = new System.Drawing.Size(46, 18);
            this.lblStroage.TabIndex = 20;
            this.lblStroage.Text = "仓别:";
            // 
            // cmbLgort
            // 
            this.cmbLgort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(110, 36);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(80, 26);
            this.cmbLgort.TabIndex = 21;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoSN);
            this.panel1.Controls.Add(this.rdoBoxid);
            this.panel1.Controls.Add(this.txtCode);
            this.panel1.Controls.Add(this.lblitm);
            this.panel1.Controls.Add(this.lblQR);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtQty);
            this.panel1.Controls.Add(this.cmbInvNo);
            this.panel1.Controls.Add(this.lblQty);
            this.panel1.Controls.Add(this.cmbLgort);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.lblStroage);
            this.panel1.Controls.Add(this.txtLocat);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.lblLocat);
            this.panel1.Controls.Add(this.cmbWerks);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.lblPlant);
            this.panel1.Controls.Add(this.lblInvNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 134);
            this.panel1.TabIndex = 54;
            // 
            // rdoSN
            // 
            this.rdoSN.AutoSize = true;
            this.rdoSN.Enabled = false;
            this.rdoSN.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.rdoSN.Location = new System.Drawing.Point(803, 51);
            this.rdoSN.Name = "rdoSN";
            this.rdoSN.Size = new System.Drawing.Size(50, 22);
            this.rdoSN.TabIndex = 58;
            this.rdoSN.TabStop = true;
            this.rdoSN.Text = "SN";
            this.rdoSN.UseVisualStyleBackColor = true;
            this.rdoSN.Visible = false;
            // 
            // rdoBoxid
            // 
            this.rdoBoxid.AutoSize = true;
            this.rdoBoxid.Enabled = false;
            this.rdoBoxid.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.rdoBoxid.Location = new System.Drawing.Point(725, 51);
            this.rdoBoxid.Name = "rdoBoxid";
            this.rdoBoxid.Size = new System.Drawing.Size(72, 22);
            this.rdoBoxid.TabIndex = 57;
            this.rdoBoxid.TabStop = true;
            this.rdoBoxid.Text = "BoxID";
            this.rdoBoxid.UseVisualStyleBackColor = true;
            this.rdoBoxid.Visible = false;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtCode.Location = new System.Drawing.Point(564, 50);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(155, 25);
            this.txtCode.TabIndex = 56;
            this.txtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCode_KeyDown);
            // 
            // lblitm
            // 
            this.lblitm.AutoSize = true;
            this.lblitm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblitm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblitm.Location = new System.Drawing.Point(286, 102);
            this.lblitm.Name = "lblitm";
            this.lblitm.Size = new System.Drawing.Size(65, 20);
            this.lblitm.TabIndex = 49;
            this.lblitm.Text = "总笔数:";
            this.lblitm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQR
            // 
            this.lblQR.AutoSize = true;
            this.lblQR.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblQR.Location = new System.Drawing.Point(487, 54);
            this.lblQR.Name = "lblQR";
            this.lblQR.Size = new System.Drawing.Size(63, 18);
            this.lblQR.TabIndex = 55;
            this.lblQR.Text = "识别码:";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(229, 72);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 18);
            this.label12.TabIndex = 47;
            this.label12.Text = "-";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(564, 86);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(68, 25);
            this.txtQty.TabIndex = 54;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // cmbInvNo
            // 
            this.cmbInvNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.cmbInvNo.FormattingEnabled = true;
            this.cmbInvNo.Location = new System.Drawing.Point(110, 99);
            this.cmbInvNo.Name = "cmbInvNo";
            this.cmbInvNo.Size = new System.Drawing.Size(152, 26);
            this.cmbInvNo.TabIndex = 35;
            this.cmbInvNo.SelectedIndexChanged += new System.EventHandler(this.cmbInvNo_SelectedIndexChanged);
            this.cmbInvNo.Click += new System.EventHandler(this.cmbInvNo_Click);
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblQty.Location = new System.Drawing.Point(506, 90);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(46, 18);
            this.lblQty.TabIndex = 53;
            this.lblQty.Text = "数量:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "";
            this.dtpStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(110, 68);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(113, 25);
            this.dtpStartDate.TabIndex = 46;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // lblDate
            // 
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblDate.Location = new System.Drawing.Point(43, 72);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(59, 18);
            this.lblDate.TabIndex = 45;
            this.lblDate.Text = "日期：";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "";
            this.dtpEndDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(251, 69);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(112, 25);
            this.dtpEndDate.TabIndex = 48;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.lblCount2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.lblCount1);
            this.panel4.Controls.Add(this.dgvError);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Controls.Add(this.dgvData);
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Controls.Add(this.btnRefresh);
            this.panel4.Controls.Add(this.statusStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(890, 645);
            this.panel4.TabIndex = 57;
            // 
            // lblCount2
            // 
            this.lblCount2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount2.AutoSize = true;
            this.lblCount2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount2.Location = new System.Drawing.Point(10, 346);
            this.lblCount2.Name = "lblCount2";
            this.lblCount2.Size = new System.Drawing.Size(82, 20);
            this.lblCount2.TabIndex = 64;
            this.lblCount2.Text = "0 Records";
            // 
            // lblCount1
            // 
            this.lblCount1.AutoSize = true;
            this.lblCount1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount1.Location = new System.Drawing.Point(10, 149);
            this.lblCount1.Name = "lblCount1";
            this.lblCount1.Size = new System.Drawing.Size(82, 20);
            this.lblCount1.TabIndex = 63;
            this.lblCount1.Text = "0 Records";
            // 
            // dgvError
            // 
            this.dgvError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvError.ColumnHeadersHeight = 29;
            this.dgvError.Location = new System.Drawing.Point(12, 187);
            this.dgvError.Name = "dgvError";
            this.dgvError.RowHeadersWidth = 51;
            this.dgvError.RowTemplate.Height = 30;
            this.dgvError.Size = new System.Drawing.Size(854, 156);
            this.dgvError.TabIndex = 62;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(466, 582);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 28);
            this.btnSave.TabIndex = 61;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeight = 29;
            this.dgvData.Location = new System.Drawing.Point(12, 384);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 30;
            this.dgvData.Size = new System.Drawing.Size(854, 177);
            this.dgvData.TabIndex = 54;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(750, 582);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 28);
            this.btnExit.TabIndex = 60;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(600, 582);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 28);
            this.btnRefresh.TabIndex = 58;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 612);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(886, 29);
            this.statusStrip1.TabIndex = 55;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(40, 23);
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(40, 23);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(80, 23);
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(400, 23);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 23);
            // 
            // InventoryCheck_RTime
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(890, 645);
            this.Controls.Add(this.panel4);
            this.Name = "InventoryCheck_RTime";
            this.Text = "InventoryCheck_RTime";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.Label lblLocat;
        private System.Windows.Forms.Label lblInvNo;
        private System.Windows.Forms.Label lblPlant;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label lblStroage;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.ComboBox cmbInvNo;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblQR;
        private System.Windows.Forms.DataGridView dgvError;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblCount1;
        private System.Windows.Forms.Label lblCount2;
        private System.Windows.Forms.Label lblitm;
        private System.Windows.Forms.RadioButton rdoBoxid;
        private System.Windows.Forms.RadioButton rdoSN;
    }
}