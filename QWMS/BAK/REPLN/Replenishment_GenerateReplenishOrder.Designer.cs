namespace QWMS
{
    partial class Replenishment_GenerateReplenishOrder
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoLocat = new System.Windows.Forms.RadioButton();
            this.rdoMatnr = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtgOutSource = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dtgStorage = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOutSource)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStorage)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 90);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnConfirm);
            this.panel5.Controls.Add(this.chkPrint);
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(440, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(251, 90);
            this.panel5.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(140, 55);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(81, 26);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkPrint.Location = new System.Drawing.Point(31, 59);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(103, 20);
            this.chkPrint.TabIndex = 1;
            this.chkPrint.Text = "Print Report";
            this.chkPrint.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoLocat);
            this.groupBox1.Controls.Add(this.rdoMatnr);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(10, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order by";
            // 
            // rdoLocat
            // 
            this.rdoLocat.AutoSize = true;
            this.rdoLocat.Location = new System.Drawing.Point(130, 21);
            this.rdoLocat.Name = "rdoLocat";
            this.rdoLocat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rdoLocat.Size = new System.Drawing.Size(81, 20);
            this.rdoLocat.TabIndex = 1;
            this.rdoLocat.Text = "Location";
            this.rdoLocat.UseVisualStyleBackColor = true;
            // 
            // rdoMatnr
            // 
            this.rdoMatnr.AutoSize = true;
            this.rdoMatnr.Location = new System.Drawing.Point(21, 21);
            this.rdoMatnr.Name = "rdoMatnr";
            this.rdoMatnr.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rdoMatnr.Size = new System.Drawing.Size(73, 20);
            this.rdoMatnr.TabIndex = 0;
            this.rdoMatnr.Text = "Part No";
            this.rdoMatnr.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbLgort);
            this.panel4.Controls.Add(this.cmbWerks);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(440, 90);
            this.panel4.TabIndex = 0;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(279, 33);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(103, 24);
            this.cmbLgort.TabIndex = 3;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(86, 33);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(103, 24);
            this.cmbWerks.TabIndex = 2;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(224, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Storage";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(48, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plant";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtgOutSource);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 132);
            this.panel2.TabIndex = 1;
            // 
            // dtgOutSource
            // 
            this.dtgOutSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgOutSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgOutSource.Location = new System.Drawing.Point(10, 3);
            this.dtgOutSource.Name = "dtgOutSource";
            this.dtgOutSource.Size = new System.Drawing.Size(672, 125);
            this.dtgOutSource.TabIndex = 0;
            this.dtgOutSource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgOutSource_MouseDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnQuery);
            this.panel3.Controls.Add(this.dtgStorage);
            this.panel3.Controls.Add(this.statusStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 222);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(692, 180);
            this.panel3.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(440, 130);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(333, 130);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 28);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnPrint.Location = new System.Drawing.Point(226, 130);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 28);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(119, 130);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 28);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.Enabled = false;
            this.btnQuery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnQuery.Location = new System.Drawing.Point(12, 130);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 28);
            this.btnQuery.TabIndex = 5;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dtgStorage
            // 
            this.dtgStorage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgStorage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgStorage.Location = new System.Drawing.Point(11, 3);
            this.dtgStorage.Name = "dtgStorage";
            this.dtgStorage.Size = new System.Drawing.Size(670, 121);
            this.dtgStorage.TabIndex = 4;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 158);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(692, 22);
            this.statusStrip1.TabIndex = 3;
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
            // Replenishment_GenerateReplenishOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 402);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Replenishment_GenerateReplenishOrder";
            this.Text = "Generate Replenish Order";
            this.Resize += new System.EventHandler(this.Replenishment_GenerateReplenishOrder_Resize);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOutSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStorage)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.RadioButton rdoLocat;
        private System.Windows.Forms.RadioButton rdoMatnr;
        private System.Windows.Forms.DataGridView dtgOutSource;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dtgStorage;
    }
}