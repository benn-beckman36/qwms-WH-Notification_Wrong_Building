namespace QWMS
{
    partial class IQCBorrowMaterial_HistoricalQuery
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
            this.cbUnReturn = new System.Windows.Forms.CheckBox();
            this.cbReturning = new System.Windows.Forms.CheckBox();
            this.cbReturned = new System.Windows.Forms.CheckBox();
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
            this.stsBaseInfo.SuspendLayout();
            this.gbQConditions.SuspendLayout();
            this.gbQResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // cbUnReturn
            // 
            this.cbUnReturn.AutoSize = true;
            this.cbUnReturn.Location = new System.Drawing.Point(73, 109);
            this.cbUnReturn.Name = "cbUnReturn";
            this.cbUnReturn.Size = new System.Drawing.Size(72, 16);
            this.cbUnReturn.TabIndex = 34;
            this.cbUnReturn.Text = "已借未还";
            this.cbUnReturn.UseVisualStyleBackColor = true;
            // 
            // cbReturning
            // 
            this.cbReturning.AutoSize = true;
            this.cbReturning.Location = new System.Drawing.Point(189, 109);
            this.cbReturning.Name = "cbReturning";
            this.cbReturning.Size = new System.Drawing.Size(72, 16);
            this.cbReturning.TabIndex = 35;
            this.cbReturning.Text = "未 还 完";
            this.cbReturning.UseVisualStyleBackColor = true;
            // 
            // cbReturned
            // 
            this.cbReturned.AutoSize = true;
            this.cbReturned.Location = new System.Drawing.Point(298, 109);
            this.cbReturned.Name = "cbReturned";
            this.cbReturned.Size = new System.Drawing.Size(72, 16);
            this.cbReturned.TabIndex = 36;
            this.cbReturned.Text = "已借已还";
            this.cbReturned.UseVisualStyleBackColor = true;
            // 
            // stsBaseInfo
            // 
            this.stsBaseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stsBaseInfo.Dock = System.Windows.Forms.DockStyle.None;
            this.stsBaseInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsernm,
            this.stsWarning,
            this.stsDate});
            this.stsBaseInfo.Location = new System.Drawing.Point(0, 375);
            this.stsBaseInfo.Name = "stsBaseInfo";
            this.stsBaseInfo.Size = new System.Drawing.Size(849, 26);
            this.stsBaseInfo.TabIndex = 3;
            // 
            // stsMandt
            // 
            this.stsMandt.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsMandt.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsMandt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Size = new System.Drawing.Size(56, 21);
            this.stsMandt.Text = "           ";
            // 
            // stsComcd
            // 
            this.stsComcd.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsComcd.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsComcd.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Size = new System.Drawing.Size(56, 21);
            this.stsComcd.Text = "           ";
            // 
            // stsUsernm
            // 
            this.stsUsernm.AutoSize = false;
            this.stsUsernm.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsUsernm.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsUsernm.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsUsernm.Name = "stsUsernm";
            this.stsUsernm.Size = new System.Drawing.Size(80, 21);
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
            this.stsWarning.Size = new System.Drawing.Size(530, 21);
            // 
            // stsDate
            // 
            this.stsDate.AutoSize = false;
            this.stsDate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.stsDate.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.stsDate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsDate.Name = "stsDate";
            this.stsDate.Size = new System.Drawing.Size(80, 21);
            // 
            // gbQConditions
            // 
            this.gbQConditions.Controls.Add(this.cmbMblnr);
            this.gbQConditions.Controls.Add(this.btQuery);
            this.gbQConditions.Controls.Add(this.cbReturned);
            this.gbQConditions.Controls.Add(this.cbReturning);
            this.gbQConditions.Controls.Add(this.txtBorrowNo);
            this.gbQConditions.Controls.Add(this.txtID);
            this.gbQConditions.Controls.Add(this.cbUnReturn);
            this.gbQConditions.Controls.Add(this.txtMatnr);
            this.gbQConditions.Controls.Add(this.lblBorrowNo);
            this.gbQConditions.Controls.Add(this.lblID);
            this.gbQConditions.Controls.Add(this.lblMatnr);
            this.gbQConditions.Controls.Add(this.cmbLgort);
            this.gbQConditions.Controls.Add(this.cmbWerks);
            this.gbQConditions.Controls.Add(this.lblMblnr);
            this.gbQConditions.Controls.Add(this.lblStorage);
            this.gbQConditions.Controls.Add(this.lblPlant);
            this.gbQConditions.Location = new System.Drawing.Point(0, 1);
            this.gbQConditions.Name = "gbQConditions";
            this.gbQConditions.Size = new System.Drawing.Size(828, 147);
            this.gbQConditions.TabIndex = 37;
            this.gbQConditions.TabStop = false;
            this.gbQConditions.Text = "查询条件";
            // 
            // cmbMblnr
            // 
            this.cmbMblnr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMblnr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbMblnr.FormattingEnabled = true;
            this.cmbMblnr.Location = new System.Drawing.Point(393, 19);
            this.cmbMblnr.Name = "cmbMblnr";
            this.cmbMblnr.Size = new System.Drawing.Size(130, 20);
            this.cmbMblnr.TabIndex = 37;
            // 
            // btQuery
            // 
            this.btQuery.Location = new System.Drawing.Point(636, 101);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(90, 30);
            this.btQuery.TabIndex = 33;
            this.btQuery.Text = "Query";
            this.btQuery.UseVisualStyleBackColor = true;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // txtBorrowNo
            // 
            this.txtBorrowNo.Location = new System.Drawing.Point(637, 61);
            this.txtBorrowNo.Name = "txtBorrowNo";
            this.txtBorrowNo.Size = new System.Drawing.Size(120, 21);
            this.txtBorrowNo.TabIndex = 28;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(393, 60);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(130, 21);
            this.txtID.TabIndex = 27;
            // 
            // txtMatnr
            // 
            this.txtMatnr.Location = new System.Drawing.Point(637, 19);
            this.txtMatnr.Name = "txtMatnr";
            this.txtMatnr.Size = new System.Drawing.Size(120, 21);
            this.txtMatnr.TabIndex = 26;
            // 
            // lblBorrowNo
            // 
            this.lblBorrowNo.AutoSize = true;
            this.lblBorrowNo.Location = new System.Drawing.Point(578, 64);
            this.lblBorrowNo.Name = "lblBorrowNo";
            this.lblBorrowNo.Size = new System.Drawing.Size(53, 12);
            this.lblBorrowNo.TabIndex = 25;
            this.lblBorrowNo.Text = "借料单号";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(334, 64);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(53, 12);
            this.lblID.TabIndex = 24;
            this.lblID.Text = "工    号";
            // 
            // lblMatnr
            // 
            this.lblMatnr.AutoSize = true;
            this.lblMatnr.Location = new System.Drawing.Point(583, 22);
            this.lblMatnr.Name = "lblMatnr";
            this.lblMatnr.Size = new System.Drawing.Size(23, 12);
            this.lblMatnr.TabIndex = 23;
            this.lblMatnr.Text = "P/N";
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.FormattingEnabled = true;
            this.cmbLgort.Location = new System.Drawing.Point(125, 61);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(121, 20);
            this.cmbLgort.TabIndex = 21;
            this.cmbLgort.SelectedIndexChanged += new System.EventHandler(this.cmbStorage_SelectedIndexChanged);
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.FormattingEnabled = true;
            this.cmbWerks.Location = new System.Drawing.Point(125, 19);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(121, 20);
            this.cmbWerks.TabIndex = 20;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // lblMblnr
            // 
            this.lblMblnr.AutoSize = true;
            this.lblMblnr.Location = new System.Drawing.Point(334, 22);
            this.lblMblnr.Name = "lblMblnr";
            this.lblMblnr.Size = new System.Drawing.Size(53, 12);
            this.lblMblnr.TabIndex = 19;
            this.lblMblnr.Text = "扣账编号";
            // 
            // lblStorage
            // 
            this.lblStorage.AutoSize = true;
            this.lblStorage.Location = new System.Drawing.Point(72, 64);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(47, 12);
            this.lblStorage.TabIndex = 18;
            this.lblStorage.Text = "Storage";
            // 
            // lblPlant
            // 
            this.lblPlant.AutoSize = true;
            this.lblPlant.Location = new System.Drawing.Point(72, 22);
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
            this.gbQResults.Location = new System.Drawing.Point(0, 155);
            this.gbQResults.Name = "gbQResults";
            this.gbQResults.Size = new System.Drawing.Size(828, 217);
            this.gbQResults.TabIndex = 38;
            this.gbQResults.TabStop = false;
            this.gbQResults.Text = "查询结果";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(6, 14);
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
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(6, 32);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(816, 179);
            this.dgvData.TabIndex = 0;
            // 
            // IQCBorrowMaterial_HistoricalQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 401);
            this.Controls.Add(this.gbQResults);
            this.Controls.Add(this.gbQConditions);
            this.Controls.Add(this.stsBaseInfo);
            this.Name = "IQCBorrowMaterial_HistoricalQuery";
            this.Text = "IQCBorrowMaterial_HistoricalQuery";
            this.stsBaseInfo.ResumeLayout(false);
            this.stsBaseInfo.PerformLayout();
            this.gbQConditions.ResumeLayout(false);
            this.gbQConditions.PerformLayout();
            this.gbQResults.ResumeLayout(false);
            this.gbQResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbUnReturn;
        private System.Windows.Forms.CheckBox cbReturned;
        private System.Windows.Forms.CheckBox cbReturning;
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
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.ComboBox cmbMblnr;
    }
}