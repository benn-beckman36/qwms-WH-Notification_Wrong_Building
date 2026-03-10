namespace QWMS
{
    partial class StorageIn_EC_Document_Compare_DoubleCheck
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.lblAlert1 = new System.Windows.Forms.Label();
            this.lblAlert2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.btnConfirm.Image = global::QWMS.Properties.Resources.warning;
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(29, 60);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(125, 32);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "    Double Check";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Location = new System.Drawing.Point(169, 60);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 32);
            this.btnReturn.TabIndex = 9;
            this.btnReturn.Text = "Return";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // lblAlert1
            // 
            this.lblAlert1.AutoSize = true;
            this.lblAlert1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert1.ForeColor = System.Drawing.Color.Red;
            this.lblAlert1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAlert1.Location = new System.Drawing.Point(31, 10);
            this.lblAlert1.Name = "lblAlert1";
            this.lblAlert1.Size = new System.Drawing.Size(110, 16);
            this.lblAlert1.TabIndex = 10;
            this.lblAlert1.Text = "点收完成！！";
            // 
            // lblAlert2
            // 
            this.lblAlert2.AutoSize = true;
            this.lblAlert2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert2.ForeColor = System.Drawing.Color.Red;
            //this.lblAlert2.Image = global::QWMS.Properties.Resources.warning;
            this.lblAlert2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAlert2.Location = new System.Drawing.Point(8, 35);
            this.lblAlert2.Name = "lblAlert2";
            this.lblAlert2.Size = new System.Drawing.Size(294, 16);
            this.lblAlert2.TabIndex = 11;
            this.lblAlert2.Text = "      Double Check 会清空待验储位之库存";
            // 
            // StorageIn_EC_Document_Compare_DoubleCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 108);
            this.Controls.Add(this.lblAlert2);
            this.Controls.Add(this.lblAlert1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnConfirm);
            this.Name = "StorageIn_EC_Document_Compare_DoubleCheck";
            this.Text = "StorageIn_EC_Document_Compare_DoubleCheck";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label lblAlert1;
        private System.Windows.Forms.Label lblAlert2;
    }
}