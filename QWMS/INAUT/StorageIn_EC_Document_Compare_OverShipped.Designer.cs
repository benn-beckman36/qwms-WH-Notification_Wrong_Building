namespace QWMS
{
    partial class StorageIn_EC_Document_Compare_OverShipped
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
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.txtQtyLeft = new System.Windows.Forms.TextBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblQtyLeft = new System.Windows.Forms.Label();
            this.txtOverRmk = new System.Windows.Forms.TextBox();
            this.lblrmk = new System.Windows.Forms.Label();
            this.lblShortQty = new System.Windows.Forms.Label();
            this.txtOverQty = new System.Windows.Forms.TextBox();
            this.txtECQty = new System.Windows.Forms.TextBox();
            this.lblECQty = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPartno = new System.Windows.Forms.TextBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblNotice = new System.Windows.Forms.Label();
            this.txtNotice = new System.Windows.Forms.TextBox();
            this.gbHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(12, 245);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(89, 32);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // gbHeader
            // 
            this.gbHeader.Controls.Add(this.txtQtyLeft);
            this.gbHeader.Controls.Add(this.lblQty);
            this.gbHeader.Controls.Add(this.txtQty);
            this.gbHeader.Controls.Add(this.lblQtyLeft);
            this.gbHeader.Controls.Add(this.txtOverRmk);
            this.gbHeader.Controls.Add(this.lblrmk);
            this.gbHeader.Controls.Add(this.lblShortQty);
            this.gbHeader.Controls.Add(this.txtOverQty);
            this.gbHeader.Controls.Add(this.txtECQty);
            this.gbHeader.Controls.Add(this.lblECQty);
            this.gbHeader.Controls.Add(this.label6);
            this.gbHeader.Controls.Add(this.txtPartno);
            this.gbHeader.Location = new System.Drawing.Point(12, 12);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(297, 227);
            this.gbHeader.TabIndex = 21;
            this.gbHeader.TabStop = false;
            // 
            // txtQtyLeft
            // 
            this.txtQtyLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQtyLeft.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQtyLeft.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtyLeft.ForeColor = System.Drawing.Color.Red;
            this.txtQtyLeft.Location = new System.Drawing.Point(161, 131);
            this.txtQtyLeft.MaxLength = 100;
            this.txtQtyLeft.Name = "txtQtyLeft";
            this.txtQtyLeft.ReadOnly = true;
            this.txtQtyLeft.Size = new System.Drawing.Size(128, 22);
            this.txtQtyLeft.TabIndex = 52;
            // 
            // lblQty
            // 
            this.lblQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(127, 75);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(36, 23);
            this.lblQty.TabIndex = 51;
            this.lblQty.Text = "Qty ";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQty
            // 
            this.txtQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQty.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtQty.ForeColor = System.Drawing.Color.Red;
            this.txtQty.Location = new System.Drawing.Point(161, 75);
            this.txtQty.MaxLength = 100;
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(128, 22);
            this.txtQty.TabIndex = 50;
            // 
            // lblQtyLeft
            // 
            this.lblQtyLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQtyLeft.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyLeft.Location = new System.Drawing.Point(99, 131);
            this.lblQtyLeft.Name = "lblQtyLeft";
            this.lblQtyLeft.Size = new System.Drawing.Size(60, 23);
            this.lblQtyLeft.TabIndex = 49;
            this.lblQtyLeft.Text = "Qty Left";
            this.lblQtyLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOverRmk
            // 
            this.txtOverRmk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOverRmk.BackColor = System.Drawing.Color.White;
            this.txtOverRmk.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOverRmk.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOverRmk.Location = new System.Drawing.Point(161, 159);
            this.txtOverRmk.MaxLength = 100;
            this.txtOverRmk.Name = "txtOverRmk";
            this.txtOverRmk.Size = new System.Drawing.Size(128, 22);
            this.txtOverRmk.TabIndex = 45;
            // 
            // lblrmk
            // 
            this.lblrmk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblrmk.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblrmk.Location = new System.Drawing.Point(6, 157);
            this.lblrmk.Name = "lblrmk";
            this.lblrmk.Size = new System.Drawing.Size(152, 23);
            this.lblrmk.TabIndex = 44;
            this.lblrmk.Text = "Over-Shipped Reason";
            this.lblrmk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShortQty
            // 
            this.lblShortQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShortQty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortQty.Location = new System.Drawing.Point(33, 100);
            this.lblShortQty.Name = "lblShortQty";
            this.lblShortQty.Size = new System.Drawing.Size(132, 23);
            this.lblShortQty.TabIndex = 42;
            this.lblShortQty.Text = "Over-Shipped Qty";
            this.lblShortQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOverQty
            // 
            this.txtOverQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOverQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOverQty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOverQty.ForeColor = System.Drawing.Color.Red;
            this.txtOverQty.Location = new System.Drawing.Point(161, 102);
            this.txtOverQty.MaxLength = 100;
            this.txtOverQty.Name = "txtOverQty";
            this.txtOverQty.ReadOnly = true;
            this.txtOverQty.Size = new System.Drawing.Size(128, 22);
            this.txtOverQty.TabIndex = 41;
            // 
            // txtECQty
            // 
            this.txtECQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtECQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtECQty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtECQty.Location = new System.Drawing.Point(161, 49);
            this.txtECQty.MaxLength = 100;
            this.txtECQty.Name = "txtECQty";
            this.txtECQty.ReadOnly = true;
            this.txtECQty.Size = new System.Drawing.Size(128, 22);
            this.txtECQty.TabIndex = 39;
            // 
            // lblECQty
            // 
            this.lblECQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblECQty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblECQty.Location = new System.Drawing.Point(36, 49);
            this.lblECQty.Name = "lblECQty";
            this.lblECQty.Size = new System.Drawing.Size(131, 23);
            this.lblECQty.TabIndex = 38;
            this.lblECQty.Text = "EC Document Qty";
            this.lblECQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(80, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 23);
            this.label6.TabIndex = 37;
            this.label6.Text = "Part No.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPartno
            // 
            this.txtPartno.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txtPartno.Location = new System.Drawing.Point(161, 21);
            this.txtPartno.Name = "txtPartno";
            this.txtPartno.ReadOnly = true;
            this.txtPartno.Size = new System.Drawing.Size(128, 22);
            this.txtPartno.TabIndex = 0;
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Location = new System.Drawing.Point(206, 246);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(99, 31);
            this.btnReturn.TabIndex = 22;
            this.btnReturn.Text = "Return";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.btnReset.Image = global::QWMS.Properties.Resources.warning;
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReset.Location = new System.Drawing.Point(107, 246);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(93, 32);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblNotice
            // 
            this.lblNotice.AutoSize = true;
            this.lblNotice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNotice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            //this.lblNotice.Image = global::QWMS.Properties.Resources.warning;
            this.lblNotice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNotice.Location = new System.Drawing.Point(12, 284);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(271, 16);
            this.lblNotice.TabIndex = 23;
            this.lblNotice.Text = "      Warnning: Click \"Reset\" will restart !!!";
            // 
            // txtNotice
            // 
            this.txtNotice.BackColor = System.Drawing.SystemColors.Control;
            this.txtNotice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotice.ForeColor = System.Drawing.Color.Red;
            this.txtNotice.Location = new System.Drawing.Point(15, 304);
            this.txtNotice.Name = "txtNotice";
            this.txtNotice.Size = new System.Drawing.Size(200, 15);
            this.txtNotice.TabIndex = 49;
            // 
            // StorageIn_EC_Document_Compare_OverShipped
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 337);
            this.Controls.Add(this.txtNotice);
            this.Controls.Add(this.lblNotice);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.gbHeader);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnConfirm);
            this.Name = "StorageIn_EC_Document_Compare_OverShipped";
            this.Text = "StorageIn_EC_Document_Compare_OverShipped";
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.TextBox txtOverRmk;
        private System.Windows.Forms.Label lblrmk;
        private System.Windows.Forms.Label lblShortQty;
        private System.Windows.Forms.TextBox txtOverQty;
        private System.Windows.Forms.TextBox txtECQty;
        private System.Windows.Forms.Label lblECQty;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPartno;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label lblQtyLeft;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblNotice;
        private System.Windows.Forms.TextBox txtNotice;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtQtyLeft;
    }
}