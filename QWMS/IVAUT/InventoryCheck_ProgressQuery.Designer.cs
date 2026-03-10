namespace QWMS
{
    partial class InventoryCheck_ProgressQuery
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
            this.txtToLocat = new System.Windows.Forms.TextBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.txtFromLocat = new System.Windows.Forms.TextBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnException = new System.Windows.Forms.Button();
            this.lblLgort = new System.Windows.Forms.Label();
            this.lblLocat = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new System.Windows.Forms.Button();
            this.lblWerks = new System.Windows.Forms.Label();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbInvNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Panel();
            this.cbSelect = new System.Windows.Forms.CheckBox();
            this.btnEX = new System.Windows.Forms.Button();
            this.lblCount3 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.btnExit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtToLocat);
            this.panel1.Controls.Add(this.cmbLgort);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbWerks);
            this.panel1.Controls.Add(this.txtFromLocat);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.btnException);
            this.panel1.Controls.Add(this.lblLgort);
            this.panel1.Controls.Add(this.lblLocat);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.lblWerks);
            this.panel1.Controls.Add(this.txtMatnr);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbInvNo);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(875, 102);
            this.panel1.TabIndex = 0;
            // 
            // txtToLocat
            // 
            this.txtToLocat.Location = new System.Drawing.Point(561, 37);
            this.txtToLocat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtToLocat.Name = "txtToLocat";
            this.txtToLocat.Size = new System.Drawing.Size(83, 28);
            this.txtToLocat.TabIndex = 13;
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(79, 38);
            this.cmbLgort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(121, 29);
            this.cmbLgort.TabIndex = 4;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbLgort_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(540, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 21);
            this.label5.TabIndex = 12;
            this.label5.Text = "-";
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(79, 6);
            this.cmbWerks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 29);
            this.cmbWerks.TabIndex = 3;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // txtFromLocat
            // 
            this.txtFromLocat.Location = new System.Drawing.Point(454, 37);
            this.txtFromLocat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtFromLocat.Name = "txtFromLocat";
            this.txtFromLocat.Size = new System.Drawing.Size(83, 28);
            this.txtFromLocat.TabIndex = 5;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(200, 70);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(100, 28);
            this.dtpEndDate.TabIndex = 10;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // btnException
            // 
            this.btnException.Location = new System.Drawing.Point(750, 68);
            this.btnException.Name = "btnException";
            this.btnException.Size = new System.Drawing.Size(70, 28);
            this.btnException.TabIndex = 11;
            this.btnException.Text = "结果";
            this.btnException.UseVisualStyleBackColor = true;
            this.btnException.Click += new System.EventHandler(this.btnException_Click);
            // 
            // lblLgort
            // 
            this.lblLgort.AutoSize = true;
            this.lblLgort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblLgort.Location = new System.Drawing.Point(18, 41);
            this.lblLgort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLgort.Name = "lblLgort";
            this.lblLgort.Size = new System.Drawing.Size(55, 21);
            this.lblLgort.TabIndex = 1;
            this.lblLgort.Text = "仓别:";
            // 
            // lblLocat
            // 
            this.lblLocat.AutoSize = true;
            this.lblLocat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblLocat.Location = new System.Drawing.Point(372, 40);
            this.lblLocat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocat.Name = "lblLocat";
            this.lblLocat.Size = new System.Drawing.Size(55, 21);
            this.lblLocat.TabIndex = 2;
            this.lblLocat.Text = "储位:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(79, 70);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(100, 28);
            this.dtpStartDate.TabIndex = 9;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(661, 68);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(70, 28);
            this.btnQuery.TabIndex = 8;
            this.btnQuery.Text = "进度";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblWerks
            // 
            this.lblWerks.AutoSize = true;
            this.lblWerks.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblWerks.Location = new System.Drawing.Point(18, 9);
            this.lblWerks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWerks.Name = "lblWerks";
            this.lblWerks.Size = new System.Drawing.Size(55, 21);
            this.lblWerks.TabIndex = 0;
            this.lblWerks.Text = "厂区:";
            // 
            // txtMatnr
            // 
            this.txtMatnr.Location = new System.Drawing.Point(454, 68);
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(154, 28);
            this.txtMatnr.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "日期:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(372, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "料号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "-";
            // 
            // cmbInvNo
            // 
            this.cmbInvNo.FormattingEnabled = true;
            this.cmbInvNo.Location = new System.Drawing.Point(454, 5);
            this.cmbInvNo.Name = "cmbInvNo";
            this.cmbInvNo.Size = new System.Drawing.Size(154, 29);
            this.cmbInvNo.TabIndex = 5;
            this.cmbInvNo.SelectedIndexChanged += new System.EventHandler(this.cmbInvNo_SelectedIndexChanged);
            this.cmbInvNo.Click += new System.EventHandler(this.cmbInvNo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "盘点票号:";
            // 
            // btnExit
            // 
            this.btnExit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnExit.Controls.Add(this.cbSelect);
            this.btnExit.Controls.Add(this.btnEX);
            this.btnExit.Controls.Add(this.lblCount3);
            this.btnExit.Controls.Add(this.lblCount);
            this.btnExit.Controls.Add(this.dgvData);
            this.btnExit.Controls.Add(this.button1);
            this.btnExit.Controls.Add(this.btnDownload);
            this.btnExit.Controls.Add(this.btnRefresh);
            this.btnExit.Controls.Add(this.statusStrip1);
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(0, 102);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(875, 442);
            this.btnExit.TabIndex = 2;
            // 
            // cbSelect
            // 
            this.cbSelect.AutoSize = true;
            this.cbSelect.Location = new System.Drawing.Point(108, 1);
            this.cbSelect.Name = "cbSelect";
            this.cbSelect.Size = new System.Drawing.Size(74, 25);
            this.cbSelect.TabIndex = 65;
            this.cbSelect.Text = "全选";
            this.cbSelect.UseVisualStyleBackColor = true;
            this.cbSelect.CheckedChanged += new System.EventHandler(this.cbSelect_CheckedChanged);
            // 
            // btnEX
            // 
            this.btnEX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEX.Location = new System.Drawing.Point(321, 379);
            this.btnEX.Name = "btnEX";
            this.btnEX.Size = new System.Drawing.Size(100, 28);
            this.btnEX.TabIndex = 64;
            this.btnEX.Text = "处理异常";
            this.btnEX.UseVisualStyleBackColor = true;
            this.btnEX.Click += new System.EventHandler(this.btnEX_Click);
            // 
            // lblCount3
            // 
            this.lblCount3.AutoSize = true;
            this.lblCount3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount3.Location = new System.Drawing.Point(15, 3);
            this.lblCount3.Name = "lblCount3";
            this.lblCount3.Size = new System.Drawing.Size(100, 23);
            this.lblCount3.TabIndex = 63;
            this.lblCount3.Text = "0 Records";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(19, 4);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(15, 21);
            this.lblCount.TabIndex = 62;
            this.lblCount.Text = " ";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(39, 27);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 30;
            this.dgvData.Size = new System.Drawing.Size(792, 347);
            this.dgvData.TabIndex = 61;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(722, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 28);
            this.button1.TabIndex = 60;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Location = new System.Drawing.Point(583, 379);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(105, 28);
            this.btnDownload.TabIndex = 58;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(452, 379);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 28);
            this.btnRefresh.TabIndex = 57;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(873, 29);
            this.statusStrip1.TabIndex = 56;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(40, 24);
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(40, 24);
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(80, 24);
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(400, 24);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 24);
            // 
            // InventoryCheck_ProgressQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 544);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "InventoryCheck_ProgressQuery";
            this.Text = "InventoryCheck_ProgressQuery";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.btnExit.ResumeLayout(false);
            this.btnExit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLocat;
        private System.Windows.Forms.Label lblLgort;
        private System.Windows.Forms.Label lblWerks;
        private System.Windows.Forms.TextBox txtFromLocat;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.ComboBox cmbInvNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblCount3;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnException;
        private System.Windows.Forms.TextBox txtToLocat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEX;
        private System.Windows.Forms.CheckBox cbSelect;
    }
}