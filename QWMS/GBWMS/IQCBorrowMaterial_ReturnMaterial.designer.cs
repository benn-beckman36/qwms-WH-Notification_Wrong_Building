namespace QWMS
{
    partial class IQCBorrowMaterial_ReturnMaterial
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtReturnWHID = new System.Windows.Forms.TextBox();
            this.lblReturnWHID = new System.Windows.Forms.Label();
            this.txtReturnIQCID = new System.Windows.Forms.TextBox();
            this.lblReturnIQCID = new System.Windows.Forms.Label();
            this.stsBaseInfo = new System.Windows.Forms.StatusStrip();
            this.stsMandt = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsComcd = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUsernm = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsWarning = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbQConditions = new System.Windows.Forms.GroupBox();
            this.cmbMblnr = new System.Windows.Forms.ComboBox();
            this.btQuery = new System.Windows.Forms.Button();
            this.txtBorrowNo = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtMatnr = new System.Windows.Forms.TextBox();
            this.lblBorrowNo = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblMatnr = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.lblMblnr = new System.Windows.Forms.Label();
            this.lblStorage = new System.Windows.Forms.Label();
            this.lblPlant = new System.Windows.Forms.Label();
            this.gbQResults = new System.Windows.Forms.GroupBox();
            this.lblRecords = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.gbCheckID = new System.Windows.Forms.GroupBox();
            this.lblIQCCheck = new System.Windows.Forms.Label();
            this.lblWHCheck = new System.Windows.Forms.Label();
            this.stsBaseInfo.SuspendLayout();
            this.gbQConditions.SuspendLayout();
            this.gbQResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.gbCheckID.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReturnWHID
            // 
            this.txtReturnWHID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReturnWHID.Location = new System.Drawing.Point(71, 87);
            this.txtReturnWHID.Name = "txtReturnWHID";
            this.txtReturnWHID.Size = new System.Drawing.Size(200, 21);
            this.txtReturnWHID.TabIndex = 15;
            this.txtReturnWHID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReturnWHID_KeyPress);
            // 
            // lblReturnWHID
            // 
            this.lblReturnWHID.AutoSize = true;
            this.lblReturnWHID.Location = new System.Drawing.Point(18, 90);
            this.lblReturnWHID.Name = "lblReturnWHID";
            this.lblReturnWHID.Size = new System.Drawing.Size(47, 12);
            this.lblReturnWHID.TabIndex = 14;
            this.lblReturnWHID.Text = "WH 刷卡";
            // 
            // txtReturnIQCID
            // 
            this.txtReturnIQCID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReturnIQCID.Location = new System.Drawing.Point(71, 25);
            this.txtReturnIQCID.Name = "txtReturnIQCID";
            this.txtReturnIQCID.Size = new System.Drawing.Size(200, 21);
            this.txtReturnIQCID.TabIndex = 13;
            this.txtReturnIQCID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReturnIQCID_KeyPress);
            // 
            // lblReturnIQCID
            // 
            this.lblReturnIQCID.AutoSize = true;
            this.lblReturnIQCID.Location = new System.Drawing.Point(18, 28);
            this.lblReturnIQCID.Name = "lblReturnIQCID";
            this.lblReturnIQCID.Size = new System.Drawing.Size(47, 12);
            this.lblReturnIQCID.TabIndex = 12;
            this.lblReturnIQCID.Text = "IQC刷卡";
            // 
            // stsBaseInfo
            // 
            this.stsBaseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stsBaseInfo.AutoSize = false;
            this.stsBaseInfo.Dock = System.Windows.Forms.DockStyle.None;
            this.stsBaseInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsernm,
            this.stsWarning,
            this.stsDate});
            this.stsBaseInfo.Location = new System.Drawing.Point(1, 371);
            this.stsBaseInfo.Name = "stsBaseInfo";
            this.stsBaseInfo.Size = new System.Drawing.Size(828, 31);
            this.stsBaseInfo.TabIndex = 2;
            // 
            // stsMandt
            // 
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(56, 26);
            this.stsMandt.Text = "           ";
            // 
            // stsComcd
            // 
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(56, 26);
            this.stsComcd.Text = "           ";
            // 
            // stsUsernm
            // 
            this.stsUsernm.AutoSize = false;
            this.stsUsernm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsernm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsernm.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsUsernm.Name = "stsUsernm";
            this.stsUsernm.Size = new System.Drawing.Size(80, 26);
            this.stsUsernm.Text = "                                     ";
            // 
            // stsWarning
            // 
            this.stsWarning.AutoSize = false;
            this.stsWarning.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsWarning.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsWarning.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsWarning.ForeColor = System.Drawing.Color.Red;
            this.stsWarning.LinkColor = System.Drawing.Color.Blue;
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Size = new System.Drawing.Size(520, 26);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 26);
            // 
            // gbQConditions
            // 
            this.gbQConditions.Controls.Add(this.cmbMblnr);
            this.gbQConditions.Controls.Add(this.btQuery);
            this.gbQConditions.Controls.Add(this.txtBorrowNo);
            this.gbQConditions.Controls.Add(this.txtID);
            this.gbQConditions.Controls.Add(this.txtMatnr);
            this.gbQConditions.Controls.Add(this.lblBorrowNo);
            this.gbQConditions.Controls.Add(this.lblID);
            this.gbQConditions.Controls.Add(this.lblMatnr);
            this.gbQConditions.Controls.Add(this.cmbLgort);
            this.gbQConditions.Controls.Add(this.cmbWerks);
            this.gbQConditions.Controls.Add(this.lblMblnr);
            this.gbQConditions.Controls.Add(this.lblStorage);
            this.gbQConditions.Controls.Add(this.lblPlant);
            this.gbQConditions.Location = new System.Drawing.Point(2, 3);
            this.gbQConditions.Name = "gbQConditions";
            this.gbQConditions.Size = new System.Drawing.Size(533, 147);
            this.gbQConditions.TabIndex = 38;
            this.gbQConditions.TabStop = false;
            this.gbQConditions.Text = "查询条件";
            // 
            // cmbMblnr
            // 
            this.cmbMblnr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMblnr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMblnr.FormattingEnabled = true;
            this.cmbMblnr.Location = new System.Drawing.Point(281, 25);
            this.cmbMblnr.Name = "cmbMblnr";
            this.cmbMblnr.Size = new System.Drawing.Size(130, 20);
            this.cmbMblnr.TabIndex = 34;
            // 
            // btQuery
            // 
            this.btQuery.Location = new System.Drawing.Point(431, 109);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(90, 30);
            this.btQuery.TabIndex = 33;
            this.btQuery.Text = "Query";
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // txtBorrowNo
            // 
            this.txtBorrowNo.Location = new System.Drawing.Point(281, 105);
            this.txtBorrowNo.Name = "txtBorrowNo";
            this.txtBorrowNo.Size = new System.Drawing.Size(130, 21);
            this.txtBorrowNo.TabIndex = 28;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(59, 105);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(121, 21);
            this.txtID.TabIndex = 27;
            // 
            // txtMatnr
            // 
            this.txtMatnr.Location = new System.Drawing.Point(281, 65);
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(130, 21);
            this.txtMatnr.TabIndex = 26;
            // 
            // lblBorrowNo
            // 
            this.lblBorrowNo.AutoSize = true;
            this.lblBorrowNo.Location = new System.Drawing.Point(212, 105);
            this.lblBorrowNo.Name = "lblBorrowNo";
            this.lblBorrowNo.Size = new System.Drawing.Size(53, 12);
            this.lblBorrowNo.TabIndex = 25;
            this.lblBorrowNo.Text = "借料单号";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(6, 105);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(41, 12);
            this.lblID.TabIndex = 24;
            this.lblID.Text = "工  号";
            // 
            // lblMatnr
            // 
            this.lblMatnr.AutoSize = true;
            this.lblMatnr.Location = new System.Drawing.Point(212, 65);
            this.lblMatnr.Name = "lblMatnr";
            this.lblMatnr.Size = new System.Drawing.Size(23, 12);
            this.lblMatnr.TabIndex = 23;
            this.lblMatnr.Text = "P/N";
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(59, 65);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(121, 20);
            this.cmbLgort.TabIndex = 21;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbStorage_SelectedIndexChanged);
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(59, 25);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 20);
            this.cmbWerks.TabIndex = 20;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // lblMblnr
            // 
            this.lblMblnr.AutoSize = true;
            this.lblMblnr.Location = new System.Drawing.Point(212, 25);
            this.lblMblnr.Name = "lblMblnr";
            this.lblMblnr.Size = new System.Drawing.Size(53, 12);
            this.lblMblnr.TabIndex = 19;
            this.lblMblnr.Text = "扣账编号";
            // 
            // lblStorage
            // 
            this.lblStorage.AutoSize = true;
            this.lblStorage.Location = new System.Drawing.Point(6, 65);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(47, 12);
            this.lblStorage.TabIndex = 18;
            this.lblStorage.Text = "Storage";
            // 
            // lblPlant
            // 
            this.lblPlant.AutoSize = true;
            this.lblPlant.Location = new System.Drawing.Point(6, 25);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(35, 12);
            this.lblPlant.TabIndex = 17;
            this.lblPlant.Text = "Plant";
            // 
            // gbQResults
            // 
            this.gbQResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbQResults.Controls.Add(this.lblRecords);
            this.gbQResults.Controls.Add(this.dgvData);
            this.gbQResults.Location = new System.Drawing.Point(1, 158);
            this.gbQResults.Name = "gbQResults";
            this.gbQResults.Size = new System.Drawing.Size(828, 210);
            this.gbQResults.TabIndex = 39;
            this.gbQResults.TabStop = false;
            this.gbQResults.Text = "查询结果";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(6, 13);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(59, 12);
            this.lblRecords.TabIndex = 1;
            this.lblRecords.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvData.Location = new System.Drawing.Point(6, 30);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(822, 174);
            this.dgvData.TabIndex = 0;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            // 
            // gbCheckID
            // 
            this.gbCheckID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCheckID.Controls.Add(this.lblWHCheck);
            this.gbCheckID.Controls.Add(this.lblIQCCheck);
            this.gbCheckID.Controls.Add(this.txtReturnWHID);
            this.gbCheckID.Controls.Add(this.txtReturnIQCID);
            this.gbCheckID.Controls.Add(this.lblReturnWHID);
            this.gbCheckID.Controls.Add(this.lblReturnIQCID);
            this.gbCheckID.Location = new System.Drawing.Point(547, 3);
            this.gbCheckID.Name = "gbCheckID";
            this.gbCheckID.Size = new System.Drawing.Size(282, 147);
            this.gbCheckID.TabIndex = 40;
            this.gbCheckID.TabStop = false;
            this.gbCheckID.Text = "身份验证";
            // 
            // lblIQCCheck
            // 
            this.lblIQCCheck.AutoSize = true;
            this.lblIQCCheck.ForeColor = System.Drawing.Color.Red;
            this.lblIQCCheck.Location = new System.Drawing.Point(71, 53);
            this.lblIQCCheck.Name = "lblIQCCheck";
            this.lblIQCCheck.Size = new System.Drawing.Size(137, 12);
            this.lblIQCCheck.TabIndex = 16;
            this.lblIQCCheck.Text = "请按Enter键验证IQC身份";
            // 
            // lblWHCheck
            // 
            this.lblWHCheck.AutoSize = true;
            this.lblWHCheck.ForeColor = System.Drawing.Color.Red;
            this.lblWHCheck.Location = new System.Drawing.Point(71, 119);
            this.lblWHCheck.Name = "lblWHCheck";
            this.lblWHCheck.Size = new System.Drawing.Size(131, 12);
            this.lblWHCheck.TabIndex = 17;
            this.lblWHCheck.Text = "请按Enter键验证WH身份";
            // 
            // IQCBorrowMaterial_ReturnMaterial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(830, 401);
            this.Controls.Add(this.gbCheckID);
            this.Controls.Add(this.gbQConditions);
            this.Controls.Add(this.gbQResults);
            this.Controls.Add(this.stsBaseInfo);
            this.Name = "IQCBorrowMaterial_ReturnMaterial";
            this.Text = "IQCBorrowMaterial_ReturnMaterial";
            this.stsBaseInfo.ResumeLayout(false);
            this.stsBaseInfo.PerformLayout();
            this.gbQConditions.ResumeLayout(false);
            this.gbQConditions.PerformLayout();
            this.gbQResults.ResumeLayout(false);
            this.gbQResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.gbCheckID.ResumeLayout(false);
            this.gbCheckID.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtReturnWHID;
        private System.Windows.Forms.Label lblReturnWHID;
        private System.Windows.Forms.TextBox txtReturnIQCID;
        private System.Windows.Forms.Label lblReturnIQCID;
        private System.Windows.Forms.StatusStrip stsBaseInfo;
        private System.Windows.Forms.ToolStripStatusLabel stsMandt;
        private System.Windows.Forms.ToolStripStatusLabel stsComcd;
        private System.Windows.Forms.ToolStripStatusLabel stsUsernm;
        private System.Windows.Forms.ToolStripStatusLabel stsWarning;
        private System.Windows.Forms.ToolStripStatusLabel stsDate;
        private System.Windows.Forms.GroupBox gbQConditions;
        private System.Windows.Forms.Button btQuery;
        private System.Windows.Forms.TextBox txtBorrowNo;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtMatnr;
        private System.Windows.Forms.Label lblBorrowNo;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblMatnr;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label lblMblnr;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.Label lblPlant;
        private System.Windows.Forms.GroupBox gbQResults;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ComboBox cmbMblnr;
        private System.Windows.Forms.GroupBox gbCheckID;
        private System.Windows.Forms.Label lblWHCheck;
        private System.Windows.Forms.Label lblIQCCheck;

    }
}