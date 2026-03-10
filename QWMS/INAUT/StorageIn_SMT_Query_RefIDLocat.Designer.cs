namespace QWMS
{
    partial class StorageIn_SMT_Query_RefIDLocat
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.lblDidNo = new System.Windows.Forms.Label();
            this.txtDidNo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(12, 75);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(672, 289);
            this.dgvData.TabIndex = 18;
            // 
            // lblDidNo
            // 
            this.lblDidNo.AutoSize = true;
            this.lblDidNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDidNo.Location = new System.Drawing.Point(33, 28);
            this.lblDidNo.Name = "lblDidNo";
            this.lblDidNo.Size = new System.Drawing.Size(62, 19);
            this.lblDidNo.TabIndex = 20;
            this.lblDidNo.Text = "DIDNO";
            // 
            // txtDidNo
            // 
            this.txtDidNo.Location = new System.Drawing.Point(101, 27);
            this.txtDidNo.Name = "txtDidNo";
            this.txtDidNo.Size = new System.Drawing.Size(294, 25);
            this.txtDidNo.TabIndex = 21;
            this.txtDidNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDidNo_KeyPress);
            // 
            // StorageIn_SMT_Query_RefIDLocat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 392);
            this.Controls.Add(this.txtDidNo);
            this.Controls.Add(this.lblDidNo);
            this.Controls.Add(this.dgvData);
            this.Name = "StorageIn_SMT_Query_RefIDLocat";
            this.Text = "StorageIn_SMT_Query_RefIDLocat";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label lblDidNo;
        private System.Windows.Forms.TextBox txtDidNo;
    }
}