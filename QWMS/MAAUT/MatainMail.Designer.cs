namespace QWMS
{
    partial class MatainMail
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTran = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.rdbtnUpdate = new System.Windows.Forms.RadioButton();
            this.rdbtnDelete = new System.Windows.Forms.RadioButton();
            this.rdbtnInsert = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.lblMail = new System.Windows.Forms.Label();
            this.txtUMLGO = new System.Windows.Forms.TextBox();
            this.lblUMLGO = new System.Windows.Forms.Label();
            this.lblwarning = new System.Windows.Forms.Label();
            this.cmbMailClass = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.cmbMailType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblhint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbFunction.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(1033, 76);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 29);
            this.btnQuery.TabIndex = 12;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(31, 24);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(1103, 388);
            this.dgvData.TabIndex = 35;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(925, 72);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 29);
            this.btnConfirm.TabIndex = 36;
            this.btnConfirm.Text = "保存";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTran);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.gbFunction);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtMail);
            this.panel1.Controls.Add(this.lblMail);
            this.panel1.Controls.Add(this.txtUMLGO);
            this.panel1.Controls.Add(this.lblUMLGO);
            this.panel1.Controls.Add(this.lblwarning);
            this.panel1.Controls.Add(this.cmbMailClass);
            this.panel1.Controls.Add(this.cmbWerks);
            this.panel1.Controls.Add(this.cmbMailType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(1, 39);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1147, 145);
            this.panel1.TabIndex = 41;
            // 
            // lblTran
            // 
            this.lblTran.AutoSize = true;
            this.lblTran.Font = new System.Drawing.Font("宋体", 9F);
            this.lblTran.ForeColor = System.Drawing.Color.Red;
            this.lblTran.Location = new System.Drawing.Point(465, 115);
            this.lblTran.Name = "lblTran";
            this.lblTran.Size = new System.Drawing.Size(0, 15);
            this.lblTran.TabIndex = 48;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1033, 24);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 29);
            this.btnRefresh.TabIndex = 47;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.rdbtnUpdate);
            this.gbFunction.Controls.Add(this.rdbtnDelete);
            this.gbFunction.Controls.Add(this.rdbtnInsert);
            this.gbFunction.Location = new System.Drawing.Point(24, 10);
            this.gbFunction.Margin = new System.Windows.Forms.Padding(4);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Padding = new System.Windows.Forms.Padding(4);
            this.gbFunction.Size = new System.Drawing.Size(179, 119);
            this.gbFunction.TabIndex = 46;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Function";
            // 
            // rdbtnUpdate
            // 
            this.rdbtnUpdate.AutoSize = true;
            this.rdbtnUpdate.Location = new System.Drawing.Point(24, 91);
            this.rdbtnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.rdbtnUpdate.Name = "rdbtnUpdate";
            this.rdbtnUpdate.Size = new System.Drawing.Size(60, 24);
            this.rdbtnUpdate.TabIndex = 1;
            this.rdbtnUpdate.TabStop = true;
            this.rdbtnUpdate.Text = "修改";
            this.rdbtnUpdate.UseVisualStyleBackColor = true;
            this.rdbtnUpdate.CheckedChanged += new System.EventHandler(this.rdbtnUpdate_CheckedChanged);
            // 
            // rdbtnDelete
            // 
            this.rdbtnDelete.AutoSize = true;
            this.rdbtnDelete.Location = new System.Drawing.Point(24, 56);
            this.rdbtnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.rdbtnDelete.Name = "rdbtnDelete";
            this.rdbtnDelete.Size = new System.Drawing.Size(60, 24);
            this.rdbtnDelete.TabIndex = 0;
            this.rdbtnDelete.TabStop = true;
            this.rdbtnDelete.Text = "删除";
            this.rdbtnDelete.UseVisualStyleBackColor = true;
            this.rdbtnDelete.CheckedChanged += new System.EventHandler(this.rdbtnDelete_CheckedChanged);
            // 
            // rdbtnInsert
            // 
            this.rdbtnInsert.AutoSize = true;
            this.rdbtnInsert.Location = new System.Drawing.Point(24, 18);
            this.rdbtnInsert.Margin = new System.Windows.Forms.Padding(4);
            this.rdbtnInsert.Name = "rdbtnInsert";
            this.rdbtnInsert.Size = new System.Drawing.Size(60, 24);
            this.rdbtnInsert.TabIndex = 0;
            this.rdbtnInsert.TabStop = true;
            this.rdbtnInsert.Text = "新增";
            this.rdbtnInsert.UseVisualStyleBackColor = true;
            this.rdbtnInsert.CheckedChanged += new System.EventHandler(this.rdbtnInsert_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(464, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 45;
            this.label3.Text = "邮件类型:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 45;
            this.label2.Text = "厂区:";
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(801, 25);
            this.txtMail.Margin = new System.Windows.Forms.Padding(4);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(223, 27);
            this.txtMail.TabIndex = 43;
            this.txtMail.Visible = false;
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(723, 28);
            this.lblMail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(69, 20);
            this.lblMail.TabIndex = 42;
            this.lblMail.Text = "收件人：";
            this.lblMail.Visible = false;
            // 
            // txtUMLGO
            // 
            this.txtUMLGO.Location = new System.Drawing.Point(548, 22);
            this.txtUMLGO.Margin = new System.Windows.Forms.Padding(4);
            this.txtUMLGO.Name = "txtUMLGO";
            this.txtUMLGO.Size = new System.Drawing.Size(173, 27);
            this.txtUMLGO.TabIndex = 43;
            // 
            // lblUMLGO
            // 
            this.lblUMLGO.AutoSize = true;
            this.lblUMLGO.Location = new System.Drawing.Point(467, 26);
            this.lblUMLGO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUMLGO.Name = "lblUMLGO";
            this.lblUMLGO.Size = new System.Drawing.Size(73, 20);
            this.lblUMLGO.TabIndex = 42;
            this.lblUMLGO.Text = "线上仓别:";
            // 
            // lblwarning
            // 
            this.lblwarning.AutoSize = true;
            this.lblwarning.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblwarning.ForeColor = System.Drawing.Color.Red;
            this.lblwarning.Location = new System.Drawing.Point(231, 102);
            this.lblwarning.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblwarning.Name = "lblwarning";
            this.lblwarning.Size = new System.Drawing.Size(0, 20);
            this.lblwarning.TabIndex = 41;
            // 
            // cmbMailClass
            // 
            this.cmbMailClass.ItemHeight = 20;
            this.cmbMailClass.Items.AddRange(new object[] {
            "收件",
            "抄送"});
            this.cmbMailClass.Location = new System.Drawing.Point(548, 72);
            this.cmbMailClass.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMailClass.Name = "cmbMailClass";
            this.cmbMailClass.Size = new System.Drawing.Size(339, 28);
            this.cmbMailClass.TabIndex = 40;
            this.cmbMailClass.SelectedValueChanged += new System.EventHandler(this.cmbMailClass_SelectedValueChanged);
            // 
            // cmbWerks
            // 
            this.cmbWerks.ItemHeight = 20;
            this.cmbWerks.Items.AddRange(new object[] {
            "收件",
            "抄送"});
            this.cmbWerks.Location = new System.Drawing.Point(307, 22);
            this.cmbWerks.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(141, 28);
            this.cmbWerks.TabIndex = 40;
            // 
            // cmbMailType
            // 
            this.cmbMailType.ItemHeight = 20;
            this.cmbMailType.Items.AddRange(new object[] {
            "收件",
            "抄送",
            ""});
            this.cmbMailType.Location = new System.Drawing.Point(307, 69);
            this.cmbMailType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMailType.Name = "cmbMailType";
            this.cmbMailType.Size = new System.Drawing.Size(141, 28);
            this.cmbMailType.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "收件方式:";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.dgvData);
            this.panel3.Location = new System.Drawing.Point(1, 191);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1147, 480);
            this.panel3.TabIndex = 43;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblhint);
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1144, 28);
            this.panel5.TabIndex = 45;
            // 
            // lblhint
            // 
            this.lblhint.AutoSize = true;
            this.lblhint.ForeColor = System.Drawing.Color.Red;
            this.lblhint.Location = new System.Drawing.Point(15, 8);
            this.lblhint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblhint.Name = "lblhint";
            this.lblhint.Size = new System.Drawing.Size(428, 15);
            this.lblhint.TabIndex = 37;
            this.lblhint.Text = "注意事项：各邮件之间请用英文分号“;”,否则将无法收到邮件";
            // 
            // MatainMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1151, 674);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MatainMail";
            this.Text = "StorageOut_AddDocMail";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbFunction.ResumeLayout(false);
            this.gbFunction.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblhint;
        private System.Windows.Forms.ComboBox cmbMailType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblwarning;
        private System.Windows.Forms.TextBox txtUMLGO;
        private System.Windows.Forms.Label lblUMLGO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.RadioButton rdbtnDelete;
        private System.Windows.Forms.RadioButton rdbtnInsert;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rdbtnUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMailClass;
        private System.Windows.Forms.Label lblTran;
    }
}