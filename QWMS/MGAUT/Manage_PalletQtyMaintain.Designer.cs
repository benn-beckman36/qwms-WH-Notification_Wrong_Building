namespace QWMS
{
    partial class Manage_PalletQtyMaintain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBalanceQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAlqty = new System.Windows.Forms.TextBox();
            this.txtMenge = new System.Windows.Forms.TextBox();
            this.labQty = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLocation);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtBalanceQty);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMblnr);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAlqty);
            this.groupBox1.Controls.Add(this.txtMenge);
            this.groupBox1.Controls.Add(this.labQty);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 197);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtBalanceQty
            // 
            this.txtBalanceQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBalanceQty.Location = new System.Drawing.Point(96, 117);
            this.txtBalanceQty.Name = "txtBalanceQty";
            this.txtBalanceQty.Size = new System.Drawing.Size(215, 22);
            this.txtBalanceQty.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Balance Qty";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMblnr
            // 
            this.txtMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMblnr.Enabled = false;
            this.txtMblnr.Location = new System.Drawing.Point(96, 18);
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(215, 22);
            this.txtMblnr.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Pallet ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAlqty
            // 
            this.txtAlqty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAlqty.Location = new System.Drawing.Point(96, 86);
            this.txtAlqty.Name = "txtAlqty";
            this.txtAlqty.Size = new System.Drawing.Size(215, 22);
            this.txtAlqty.TabIndex = 3;
            this.txtAlqty.TextChanged += new System.EventHandler(this.txtAlqty_TextChanged);
            // 
            // txtMenge
            // 
            this.txtMenge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMenge.Enabled = false;
            this.txtMenge.Location = new System.Drawing.Point(96, 56);
            this.txtMenge.Name = "txtMenge";
            this.txtMenge.Size = new System.Drawing.Size(215, 22);
            this.txtMenge.TabIndex = 2;
            // 
            // labQty
            // 
            this.labQty.AutoSize = true;
            this.labQty.Location = new System.Drawing.Point(6, 86);
            this.labQty.Name = "labQty";
            this.labQty.Size = new System.Drawing.Size(55, 16);
            this.labQty.TabIndex = 1;
            this.labQty.Text = "Out Qty";
            this.labQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location Qty";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(30, 208);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 21);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnReturn.Location = new System.Drawing.Point(167, 208);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 21);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Location";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLocation
            // 
            this.txtLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocation.Location = new System.Drawing.Point(96, 159);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(215, 22);
            this.txtLocation.TabIndex = 9;
            this.txtLocation.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtLocation_MouseDoubleClick);
            // 
            // Manage_PalletQtyMaintain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 241);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.groupBox1);
            this.Name = "Manage_PalletQtyMaintain";
            this.Text = "Qty Change";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAlqty;
        private System.Windows.Forms.TextBox txtMenge;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBalanceQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label5;
    }
}