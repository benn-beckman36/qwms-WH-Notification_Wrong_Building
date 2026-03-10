namespace QWMS
{
    partial class Transfer_BindPortInfo
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
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDHouse = new System.Windows.Forms.ComboBox();
            this.cmbFHouse = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtCartons = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPallets = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbDWerks = new System.Windows.Forms.ComboBox();
            this.cmbFWerks = new System.Windows.Forms.ComboBox();
            this.txtTransferID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbDPort = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbFPort = new System.Windows.Forms.ComboBox();
            this.cmbHFrom = new System.Windows.Forms.ComboBox();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbMFrom = new System.Windows.Forms.ComboBox();
            this.TimeFrom = new System.Windows.Forms.Label();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.SuspendLayout();
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.AutoSize = false;
            this.stsUsrnm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsrnm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsrnm.Name = "stsUsrnm";
            this.stsUsrnm.Size = new System.Drawing.Size(80, 17);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbDHouse);
            this.panel1.Controls.Add(this.cmbFHouse);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.txtCartons);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtPallets);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cmbDPort);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbFPort);
            this.panel1.Controls.Add(this.cmbHFrom);
            this.panel1.Controls.Add(this.dateFrom);
            this.panel1.Controls.Add(this.cmbMFrom);
            this.panel1.Controls.Add(this.TimeFrom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 132);
            this.panel1.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(389, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "_";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(541, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 15);
            this.label10.TabIndex = 24;
            this.label10.Text = "：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "_";
            // 
            // cmbDHouse
            // 
            this.cmbDHouse.FormattingEnabled = true;
            this.cmbDHouse.Location = new System.Drawing.Point(333, 88);
            this.cmbDHouse.Name = "cmbDHouse";
            this.cmbDHouse.Size = new System.Drawing.Size(54, 23);
            this.cmbDHouse.TabIndex = 23;
            this.cmbDHouse.SelectedIndexChanged += new System.EventHandler(this.cmbDHouse_SelectedIndexChanged);
            // 
            // cmbFHouse
            // 
            this.cmbFHouse.FormattingEnabled = true;
            this.cmbFHouse.Location = new System.Drawing.Point(333, 49);
            this.cmbFHouse.Name = "cmbFHouse";
            this.cmbFHouse.Size = new System.Drawing.Size(54, 23);
            this.cmbFHouse.TabIndex = 23;
            this.cmbFHouse.SelectedIndexChanged += new System.EventHandler(this.cmbFHouse_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(655, 87);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 26);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtCartons
            // 
            this.txtCartons.Location = new System.Drawing.Point(551, 90);
            this.txtCartons.MaxLength = 10;
            this.txtCartons.Name = "txtCartons";
            this.txtCartons.Size = new System.Drawing.Size(81, 21);
            this.txtCartons.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(509, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "箱数:";
            // 
            // txtPallets
            // 
            this.txtPallets.Location = new System.Drawing.Point(551, 49);
            this.txtPallets.MaxLength = 10;
            this.txtPallets.Name = "txtPallets";
            this.txtPallets.Size = new System.Drawing.Size(79, 21);
            this.txtPallets.TabIndex = 8;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(655, 17);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 28);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(497, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "栈板数:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbDWerks);
            this.panel2.Controls.Add(this.cmbFWerks);
            this.panel2.Controls.Add(this.txtTransferID);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(232, 132);
            this.panel2.TabIndex = 0;
            // 
            // cmbDWerks
            // 
            this.cmbDWerks.FormattingEnabled = true;
            this.cmbDWerks.Location = new System.Drawing.Point(88, 91);
            this.cmbDWerks.Name = "cmbDWerks";
            this.cmbDWerks.Size = new System.Drawing.Size(126, 23);
            this.cmbDWerks.TabIndex = 13;
            this.cmbDWerks.SelectedIndexChanged += new System.EventHandler(this.cmbDWerks_SelectedIndexChanged);
            // 
            // cmbFWerks
            // 
            this.cmbFWerks.FormattingEnabled = true;
            this.cmbFWerks.Location = new System.Drawing.Point(88, 49);
            this.cmbFWerks.Name = "cmbFWerks";
            this.cmbFWerks.Size = new System.Drawing.Size(126, 23);
            this.cmbFWerks.TabIndex = 12;
            this.cmbFWerks.SelectedIndexChanged += new System.EventHandler(this.cmbFWerks_SelectedIndexChanged);
            // 
            // txtTransferID
            // 
            this.txtTransferID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTransferID.Location = new System.Drawing.Point(88, 12);
            this.txtTransferID.MaxLength = 15;
            this.txtTransferID.Name = "txtTransferID";
            this.txtTransferID.Size = new System.Drawing.Size(126, 21);
            this.txtTransferID.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "目的厂区:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "调出厂区:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "调拨单号:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(759, 88);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 26);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(240, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "目的厂房/码头:";
            // 
            // cmbDPort
            // 
            this.cmbDPort.FormattingEnabled = true;
            this.cmbDPort.Location = new System.Drawing.Point(404, 88);
            this.cmbDPort.Name = "cmbDPort";
            this.cmbDPort.Size = new System.Drawing.Size(50, 23);
            this.cmbDPort.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "起始厂房/码头:";
            // 
            // cmbFPort
            // 
            this.cmbFPort.FormattingEnabled = true;
            this.cmbFPort.Location = new System.Drawing.Point(404, 49);
            this.cmbFPort.Name = "cmbFPort";
            this.cmbFPort.Size = new System.Drawing.Size(50, 23);
            this.cmbFPort.TabIndex = 4;
            // 
            // cmbHFrom
            // 
            this.cmbHFrom.FormattingEnabled = true;
            this.cmbHFrom.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.cmbHFrom.Location = new System.Drawing.Point(492, 10);
            this.cmbHFrom.Name = "cmbHFrom";
            this.cmbHFrom.Size = new System.Drawing.Size(49, 23);
            this.cmbHFrom.TabIndex = 22;
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(323, 9);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(135, 21);
            this.dateFrom.TabIndex = 21;
            // 
            // cmbMFrom
            // 
            this.cmbMFrom.FormattingEnabled = true;
            this.cmbMFrom.Items.AddRange(new object[] {
            "00",
            "05",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55"});
            this.cmbMFrom.Location = new System.Drawing.Point(567, 10);
            this.cmbMFrom.Name = "cmbMFrom";
            this.cmbMFrom.Size = new System.Drawing.Size(49, 23);
            this.cmbMFrom.TabIndex = 22;
            // 
            // TimeFrom
            // 
            this.TimeFrom.AutoSize = true;
            this.TimeFrom.Location = new System.Drawing.Point(240, 9);
            this.TimeFrom.Name = "TimeFrom";
            this.TimeFrom.Size = new System.Drawing.Size(62, 15);
            this.TimeFrom.TabIndex = 1;
            this.TimeFrom.Text = "需求时间:";
            // 
            // stsComcd
            // 
            this.stsComcd.AutoSize = false;
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(50, 17);
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(400, 17);
            // 
            // stsMandt
            // 
            this.stsMandt.AutoSize = false;
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(50, 17);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 17);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 442);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(846, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gvData
            // 
            this.gvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.Location = new System.Drawing.Point(82, 156);
            this.gvData.Name = "gvData";
            this.gvData.RowTemplate.Height = 23;
            this.gvData.Size = new System.Drawing.Size(648, 246);
            this.gvData.TabIndex = 27;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(323, 408);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 26);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(91, 135);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(17, 12);
            this.lblCount.TabIndex = 29;
            this.lblCount.Text = "  ";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(759, 17);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 28);
            this.btnDelete.TabIndex = 25;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Transfer_BindPortInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 464);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gvData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Transfer_BindPortInfo";
            this.Text = "Transfer_BindPortInfo";
            this.Resize += new System.EventHandler(this.Transfer_BindPortInfo_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTransferID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbDPort;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cmbFPort;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbHFrom;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.ComboBox cmbMFrom;
        private System.Windows.Forms.Label TimeFrom;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox txtPallets;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCartons;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView gvData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.ComboBox cmbDWerks;
        private System.Windows.Forms.ComboBox cmbFWerks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbDHouse;
        private System.Windows.Forms.ComboBox cmbFHouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
    }
}