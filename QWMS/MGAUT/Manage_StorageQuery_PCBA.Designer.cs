namespace QWMS
{
    partial class Manage_StorageQuery_PCBA
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
            this.DateTo = new System.Windows.Forms.DateTimePicker();
            this.DateFrom = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.To = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBxID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUmlgo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSn = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRefID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbHourT = new System.Windows.Forms.ComboBox();
            this.cmbHourF = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSnStaus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.DateTo);
            this.panel1.Controls.Add(this.DateFrom);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.txtMatnr);
            this.panel1.Controls.Add(this.lblCount);
            this.panel1.Controls.Add(this.To);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtBxID);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtUmlgo);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtSn);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtRefID);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbHourT);
            this.panel1.Controls.Add(this.cmbHourF);
            this.panel1.Controls.Add(this.cmbLgort);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbWerks);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbSnStaus);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(965, 138);
            this.panel1.TabIndex = 0;
            // 
            // DateTo
            // 
            this.DateTo.Location = new System.Drawing.Point(553, 89);
            this.DateTo.Name = "DateTo";
            this.DateTo.Size = new System.Drawing.Size(124, 21);
            this.DateTo.TabIndex = 5;
            // 
            // DateFrom
            // 
            this.DateFrom.Location = new System.Drawing.Point(317, 90);
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Size = new System.Drawing.Size(124, 21);
            this.DateFrom.TabIndex = 5;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(778, 84);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 31);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(877, 81);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 34);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtMatnr
            // 
            this.txtMatnr.Location = new System.Drawing.Point(535, 45);
            this.txtMatnr.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(171, 21);
            this.txtMatnr.TabIndex = 3;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(885, 121);
            this.lblCount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(65, 15);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "0 Records";
            // 
            // To
            // 
            this.To.AutoSize = true;
            this.To.Location = new System.Drawing.Point(512, 92);
            this.To.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.To.Name = "To";
            this.To.Size = new System.Drawing.Size(20, 15);
            this.To.TabIndex = 2;
            this.To.Text = "To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(497, 52);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "料号";
            // 
            // txtBxID
            // 
            this.txtBxID.Location = new System.Drawing.Point(319, 48);
            this.txtBxID.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBxID.Name = "txtBxID";
            this.txtBxID.Size = new System.Drawing.Size(171, 21);
            this.txtBxID.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(228, 95);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "WH IN Date:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "BOX ID";
            // 
            // txtUmlgo
            // 
            this.txtUmlgo.Location = new System.Drawing.Point(778, 16);
            this.txtUmlgo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtUmlgo.Name = "txtUmlgo";
            this.txtUmlgo.Size = new System.Drawing.Size(116, 21);
            this.txtUmlgo.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(724, 19);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "线上仓";
            // 
            // txtSn
            // 
            this.txtSn.Location = new System.Drawing.Point(535, 13);
            this.txtSn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSn.Name = "txtSn";
            this.txtSn.Size = new System.Drawing.Size(171, 21);
            this.txtSn.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(500, 19);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "SN";
            // 
            // txtRefID
            // 
            this.txtRefID.Location = new System.Drawing.Point(319, 18);
            this.txtRefID.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtRefID.Name = "txtRefID";
            this.txtRefID.Size = new System.Drawing.Size(171, 21);
            this.txtRefID.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Pallet ID";
            // 
            // cmbHourT
            // 
            this.cmbHourT.FormattingEnabled = true;
            this.cmbHourT.Items.AddRange(new object[] {
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
            this.cmbHourT.Location = new System.Drawing.Point(685, 88);
            this.cmbHourT.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbHourT.Name = "cmbHourT";
            this.cmbHourT.Size = new System.Drawing.Size(53, 23);
            this.cmbHourT.TabIndex = 1;
            // 
            // cmbHourF
            // 
            this.cmbHourF.FormattingEnabled = true;
            this.cmbHourF.Items.AddRange(new object[] {
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
            this.cmbHourF.Location = new System.Drawing.Point(449, 90);
            this.cmbHourF.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbHourF.Name = "cmbHourF";
            this.cmbHourF.Size = new System.Drawing.Size(53, 23);
            this.cmbHourF.TabIndex = 1;
            // 
            // cmbLgort
            // 
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(77, 92);
            this.cmbLgort.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(139, 23);
            this.cmbLgort.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Storage";
            // 
            // cmbWerks
            // 
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(77, 51);
            this.cmbWerks.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(139, 23);
            this.cmbWerks.TabIndex = 1;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Plant";
            // 
            // cmbSnStaus
            // 
            this.cmbSnStaus.FormattingEnabled = true;
            this.cmbSnStaus.Items.AddRange(new object[] {
            "",
            "WH_In",
            "WH_Out",
            "WH_Ship"});
            this.cmbSnStaus.Location = new System.Drawing.Point(77, 18);
            this.cmbSnStaus.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.cmbSnStaus.Name = "cmbSnStaus";
            this.cmbSnStaus.Size = new System.Drawing.Size(139, 23);
            this.cmbSnStaus.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "状态";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Location = new System.Drawing.Point(-1, 146);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(965, 259);
            this.panel2.TabIndex = 1;
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(16, 8);
            this.dgvData.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(936, 251);
            this.dgvData.TabIndex = 0;
            // 
            // stbStatus
            // 
            this.stbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stbStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(-1, 413);
            this.stbStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(965, 28);
            this.stbStatus.TabIndex = 30;
            this.stbStatus.TabStop = true;
            this.stbStatus.Text = "Status";
            // 
            // stsMandt
            // 
            this.stsMandt.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Width = 40;
            // 
            // stsComcd
            // 
            this.stsComcd.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Width = 40;
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 430;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // Manage_StorageQuery_PCBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 441);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Manage_StorageQuery_PCBA";
            this.Text = "Manage_StorageQuery_PCBA";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbSnStaus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBxID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRefID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblCount;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker DateTo;
        private System.Windows.Forms.DateTimePicker DateFrom;
        private System.Windows.Forms.Label To;
        private System.Windows.Forms.TextBox txtUmlgo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbHourT;
        private System.Windows.Forms.ComboBox cmbHourF;
    }
}