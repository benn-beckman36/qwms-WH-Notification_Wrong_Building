namespace QWMS
{
    partial class ECForm
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
            this.txtUsrnm = new System.Windows.Forms.TextBox();
            this.lblUsrnm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUsrnm
            // 
            this.txtUsrnm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsrnm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsrnm.Location = new System.Drawing.Point(52, 113);
            this.txtUsrnm.MaxLength = 10;
            this.txtUsrnm.Name = "txtUsrnm";
            this.txtUsrnm.Size = new System.Drawing.Size(161, 21);
            this.txtUsrnm.TabIndex = 2;
            this.txtUsrnm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsrnm_KeyDown);
            // 
            // lblUsrnm
            // 
            this.lblUsrnm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsrnm.Location = new System.Drawing.Point(49, 81);
            this.lblUsrnm.Name = "lblUsrnm";
            this.lblUsrnm.Size = new System.Drawing.Size(96, 15);
            this.lblUsrnm.TabIndex = 3;
            this.lblUsrnm.Text = "工号：";
            // 
            // ECForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblUsrnm);
            this.Controls.Add(this.txtUsrnm);
            this.Name = "ECForm";
            this.Text = "ECForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsrnm;
        private System.Windows.Forms.Label lblUsrnm;
    }
}