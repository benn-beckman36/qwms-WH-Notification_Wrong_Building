namespace QWMS
{
    partial class InventoryCheck_InvContent
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
            this.dgvInvContent = new System.Windows.Forms.DataGridView();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvContent)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInvContent
            // 
            this.dgvInvContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInvContent.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvInvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvContent.Location = new System.Drawing.Point(3, 37);
            this.dgvInvContent.Name = "dgvInvContent";
            this.dgvInvContent.RowTemplate.Height = 30;
            this.dgvInvContent.Size = new System.Drawing.Size(1187, 437);
            this.dgvInvContent.TabIndex = 0;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount.Location = new System.Drawing.Point(10, 6);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(94, 23);
            this.lblCount.TabIndex = 1;
            this.lblCount.Text = "0 records";
            // 
            // InventoryCheck_InvContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 500);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.dgvInvContent);
            this.Name = "InventoryCheck_InvContent";
            this.Text = "InventoryCheck_InvContent";
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInvContent;
        private System.Windows.Forms.Label lblCount;
    }
}