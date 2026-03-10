namespace QWMS
{
    partial class AddTruckOrder
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
            this.gvData = new System.Windows.Forms.DataGridView();
            this.txtSealNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.stsWaring = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).BeginInit();
            this.SuspendLayout();
            // 
            // gvData
            // 
            this.gvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvData.Location = new System.Drawing.Point(29, 105);
            this.gvData.Name = "gvData";
            this.gvData.RowTemplate.Height = 23;
            this.gvData.Size = new System.Drawing.Size(463, 214);
            this.gvData.TabIndex = 0;
            // 
            // txtSealNo
            // 
            this.txtSealNo.Location = new System.Drawing.Point(92, 30);
            this.txtSealNo.Name = "txtSealNo";
            this.txtSealNo.Size = new System.Drawing.Size(100, 21);
            this.txtSealNo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "封条号：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(281, 27);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // stsWaring
            // 
            this.stsWaring.AutoSize = true;
            this.stsWaring.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stsWaring.ForeColor = System.Drawing.Color.Red;
            this.stsWaring.Location = new System.Drawing.Point(25, 61);
            this.stsWaring.Name = "stsWaring";
            this.stsWaring.Size = new System.Drawing.Size(0, 20);
            this.stsWaring.TabIndex = 4;
            // 
            // AddTruckOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 351);
            this.Controls.Add(this.stsWaring);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSealNo);
            this.Controls.Add(this.gvData);
            this.Name = "AddTruckOrder";
            this.Text = "AddTruckOrder";
            ((System.ComponentModel.ISupportInitialize)(this.gvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvData;
        private System.Windows.Forms.TextBox txtSealNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label stsWaring;
    }
}