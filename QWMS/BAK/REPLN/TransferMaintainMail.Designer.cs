namespace QWMS
{
    partial class TransferMaintainMail
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
            this.label18 = new System.Windows.Forms.Label();
            this.cmbMailToType = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblwarning = new System.Windows.Forms.Label();
            this.cmbMailType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblhint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(88, 55);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
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
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(737, 261);
            this.dgvData.TabIndex = 35;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(228, 55);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 36;
            this.btnConfirm.Text = "保存";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(11, 10);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 12);
            this.label18.TabIndex = 37;
            this.label18.Text = "收件人类别:";
            // 
            // cmbMailToType
            // 
            this.cmbMailToType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMailToType.ItemHeight = 12;
            this.cmbMailToType.Location = new System.Drawing.Point(88, 7);
            this.cmbMailToType.Name = "cmbMailToType";
            this.cmbMailToType.Size = new System.Drawing.Size(107, 20);
            this.cmbMailToType.TabIndex = 38;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblwarning);
            this.panel1.Controls.Add(this.cmbMailType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Controls.Add(this.cmbMailToType);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Location = new System.Drawing.Point(1, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 101);
            this.panel1.TabIndex = 41;
            // 
            // lblwarning
            // 
            this.lblwarning.AutoSize = true;
            this.lblwarning.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblwarning.ForeColor = System.Drawing.Color.Red;
            this.lblwarning.Location = new System.Drawing.Point(381, 60);
            this.lblwarning.Name = "lblwarning";
            this.lblwarning.Size = new System.Drawing.Size(0, 16);
            this.lblwarning.TabIndex = 41;
            // 
            // cmbMailType
            // 
            this.cmbMailType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMailType.ItemHeight = 12;
            this.cmbMailType.Items.AddRange(new object[] {
            "收件",
            "抄送"});
            this.cmbMailType.Location = new System.Drawing.Point(383, 7);
            this.cmbMailType.Name = "cmbMailType";
            this.cmbMailType.Size = new System.Drawing.Size(107, 20);
            this.cmbMailType.TabIndex = 40;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "收件方式:";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.dgvData);
            this.panel3.Location = new System.Drawing.Point(1, 138);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(737, 261);
            this.panel3.TabIndex = 43;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblhint);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(735, 22);
            this.panel5.TabIndex = 45;
            // 
            // lblhint
            // 
            this.lblhint.AutoSize = true;
            this.lblhint.ForeColor = System.Drawing.Color.Red;
            this.lblhint.Location = new System.Drawing.Point(11, 6);
            this.lblhint.Name = "lblhint";
            this.lblhint.Size = new System.Drawing.Size(341, 12);
            this.lblhint.TabIndex = 37;
            this.lblhint.Text = "注意事项：各邮件之间请用英文分号“;”,否则将无法收到邮件";
            // 
            // TransferMaintainMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(740, 624);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "TransferMaintainMail";
            this.Text = "TransferWHReceive";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbMailToType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblhint;
        private System.Windows.Forms.ComboBox cmbMailType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblwarning;
    }
}