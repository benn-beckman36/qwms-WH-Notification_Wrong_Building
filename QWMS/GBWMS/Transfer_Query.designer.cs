namespace QWMS
{
    partial class Transfer_Query
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCarNo = new System.Windows.Forms.ComboBox();
            this.CHKDATE = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTransferID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbHTo = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtTruckOrder = new System.Windows.Forms.TextBox();
            this.cmbHFrom = new System.Windows.Forms.ComboBox();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbMTo = new System.Windows.Forms.ComboBox();
            this.cmbMFrom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeFrom = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFPort = new System.Windows.Forms.ComboBox();
            this.cmbFHouse = new System.Windows.Forms.ComboBox();
            this.cmbFWerks = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsrnm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gvData = new System.Windows.Forms.DataGridView();
            this.lblCount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbCarNo);
            this.panel1.Controls.Add(this.CHKDATE);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtTransferID);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cmbHTo);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.txtTruckOrder);
            this.panel1.Controls.Add(this.cmbHFrom);
            this.panel1.Controls.Add(this.dateTo);
            this.panel1.Controls.Add(this.dateFrom);
            this.panel1.Controls.Add(this.cmbMTo);
            this.panel1.Controls.Add(this.cmbMFrom);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TimeFrom);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbFPort);
            this.panel1.Controls.Add(this.cmbFHouse);
            this.panel1.Controls.Add(this.cmbFWerks);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(842, 132);
            this.panel1.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(469, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "车牌号：";
            // 
            // cmbCarNo
            // 
            this.cmbCarNo.FormattingEnabled = true;
            this.cmbCarNo.Location = new System.Drawing.Point(534, 51);
            this.cmbCarNo.Name = "cmbCarNo";
            this.cmbCarNo.Size = new System.Drawing.Size(107, 23);
            this.cmbCarNo.TabIndex = 30;
            // 
            // CHKDATE
            // 
            this.CHKDATE.AutoSize = true;
            this.CHKDATE.Checked = true;
            this.CHKDATE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKDATE.Location = new System.Drawing.Point(63, 100);
            this.CHKDATE.Name = "CHKDATE";
            this.CHKDATE.Size = new System.Drawing.Size(15, 14);
            this.CHKDATE.TabIndex = 29;
            this.CHKDATE.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "厂房 :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(480, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "码头:";
            // 
            // txtTransferID
            // 
            this.txtTransferID.Location = new System.Drawing.Point(309, 50);
            this.txtTransferID.MaxLength = 15;
            this.txtTransferID.Name = "txtTransferID";
            this.txtTransferID.Size = new System.Drawing.Size(142, 21);
            this.txtTransferID.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(241, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "调拨单号:";
            // 
            // cmbHTo
            // 
            this.cmbHTo.FormattingEnabled = true;
            this.cmbHTo.Items.AddRange(new object[] {
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
            this.cmbHTo.Location = new System.Drawing.Point(548, 95);
            this.cmbHTo.Name = "cmbHTo";
            this.cmbHTo.Size = new System.Drawing.Size(49, 23);
            this.cmbHTo.TabIndex = 25;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(684, 56);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(130, 26);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "派车单号:";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(684, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(129, 26);
            this.btnQuery.TabIndex = 11;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtTruckOrder
            // 
            this.txtTruckOrder.Location = new System.Drawing.Point(108, 53);
            this.txtTruckOrder.Name = "txtTruckOrder";
            this.txtTruckOrder.Size = new System.Drawing.Size(126, 21);
            this.txtTruckOrder.TabIndex = 2;
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
            this.cmbHFrom.Location = new System.Drawing.Point(285, 95);
            this.cmbHFrom.Name = "cmbHFrom";
            this.cmbHFrom.Size = new System.Drawing.Size(42, 23);
            this.cmbHFrom.TabIndex = 25;
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(407, 95);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(135, 21);
            this.dateTo.TabIndex = 24;
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(160, 95);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(119, 21);
            this.dateFrom.TabIndex = 24;
            // 
            // cmbMTo
            // 
            this.cmbMTo.FormattingEnabled = true;
            this.cmbMTo.Items.AddRange(new object[] {
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
            this.cmbMTo.Location = new System.Drawing.Point(603, 95);
            this.cmbMTo.Name = "cmbMTo";
            this.cmbMTo.Size = new System.Drawing.Size(49, 23);
            this.cmbMTo.TabIndex = 26;
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
            this.cmbMFrom.Location = new System.Drawing.Point(333, 95);
            this.cmbMFrom.Name = "cmbMFrom";
            this.cmbMFrom.Size = new System.Drawing.Size(49, 23);
            this.cmbMFrom.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "至";
            // 
            // TimeFrom
            // 
            this.TimeFrom.AutoSize = true;
            this.TimeFrom.Location = new System.Drawing.Point(80, 98);
            this.TimeFrom.Name = "TimeFrom";
            this.TimeFrom.Size = new System.Drawing.Size(72, 15);
            this.TimeFrom.TabIndex = 23;
            this.TimeFrom.Text = "发车时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "厂区 :";
            // 
            // cmbFPort
            // 
            this.cmbFPort.FormattingEnabled = true;
            this.cmbFPort.Location = new System.Drawing.Point(534, 15);
            this.cmbFPort.Name = "cmbFPort";
            this.cmbFPort.Size = new System.Drawing.Size(107, 23);
            this.cmbFPort.TabIndex = 5;
            // 
            // cmbFHouse
            // 
            this.cmbFHouse.FormattingEnabled = true;
            this.cmbFHouse.Items.AddRange(new object[] {
            "A1",
            "C1",
            "C2",
            "C3",
            "D1",
            "D2"});
            this.cmbFHouse.Location = new System.Drawing.Point(309, 12);
            this.cmbFHouse.Name = "cmbFHouse";
            this.cmbFHouse.Size = new System.Drawing.Size(142, 23);
            this.cmbFHouse.TabIndex = 5;
            this.cmbFHouse.SelectedIndexChanged += new System.EventHandler(this.cmbFHouse_SelectedIndexChanged);
            // 
            // cmbFWerks
            // 
            this.cmbFWerks.FormattingEnabled = true;
            this.cmbFWerks.Items.AddRange(new object[] {
            "CS70",
            "CS72",
            "CS75"});
            this.cmbFWerks.Location = new System.Drawing.Point(108, 12);
            this.cmbFWerks.Name = "cmbFWerks";
            this.cmbFWerks.Size = new System.Drawing.Size(126, 23);
            this.cmbFWerks.TabIndex = 5;
            this.cmbFWerks.SelectedIndexChanged += new System.EventHandler(this.cmbFWerks_SelectedIndexChanged);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 392);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(842, 22);
            this.statusStrip1.TabIndex = 22;
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
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Location = new System.Drawing.Point(-78, 421);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 26);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // gvData
            // 
            this.gvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvData.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.Location = new System.Drawing.Point(33, 155);
            this.gvData.Name = "gvData";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvData.RowTemplate.Height = 23;
            this.gvData.Size = new System.Drawing.Size(780, 224);
            this.gvData.TabIndex = 23;
            this.gvData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gvData_DataBindingComplete);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(-25, 120);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(35, 12);
            this.lblCount.TabIndex = 25;
            this.lblCount.Text = "     ";
            // 
            // Transfer_Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 414);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.gvData);
            this.Controls.Add(this.lblCount);
            this.Name = "Transfer_Query";
            this.Text = "Transfer_Query";
            this.Resize += new System.EventHandler(this.Transfer_Query_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtTransferID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTruckOrder;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsrnm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView gvData;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFPort;
        private System.Windows.Forms.ComboBox cmbFWerks;
        private System.Windows.Forms.ComboBox cmbFHouse;
        private System.Windows.Forms.ComboBox cmbHTo;
        private System.Windows.Forms.ComboBox cmbHFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.ComboBox cmbMTo;
        private System.Windows.Forms.ComboBox cmbMFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TimeFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CHKDATE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCarNo;
    }
}