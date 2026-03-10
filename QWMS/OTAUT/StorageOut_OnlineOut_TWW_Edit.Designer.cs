namespace QWMS.OTAUT
{
    partial class StorageOut_OnlineOut_TWW_Edit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.cmbVbeln = new System.Windows.Forms.ComboBox();
            this.lbVbeln = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.lbTxet = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 506);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(332, 471);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirm.Location = new System.Drawing.Point(47, 471);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(105, 24);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 115);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(485, 350);
            this.dgv.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.cmbVbeln);
            this.groupBox1.Controls.Add(this.lbVbeln);
            this.groupBox1.Controls.Add(this.txtContent);
            this.groupBox1.Controls.Add(this.lbTxet);
            this.groupBox1.Controls.Add(this.lbType);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Location = new System.Drawing.Point(55, 9);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 20);
            this.cmbType.TabIndex = 9;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // cmbVbeln
            // 
            this.cmbVbeln.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVbeln.Location = new System.Drawing.Point(55, 35);
            this.cmbVbeln.Name = "cmbVbeln";
            this.cmbVbeln.Size = new System.Drawing.Size(121, 20);
            this.cmbVbeln.TabIndex = 8;
            this.cmbVbeln.SelectedIndexChanged += new System.EventHandler(this.cmbVbeln_SelectedIndexChanged);
            // 
            // lbVbeln
            // 
            this.lbVbeln.AutoSize = true;
            this.lbVbeln.Location = new System.Drawing.Point(20, 38);
            this.lbVbeln.Name = "lbVbeln";
            this.lbVbeln.Size = new System.Drawing.Size(29, 12);
            this.lbVbeln.TabIndex = 6;
            this.lbVbeln.Text = "Item";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(55, 61);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(198, 21);
            this.txtContent.TabIndex = 4;
            this.txtContent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContent_KeyPress);
            // 
            // lbTxet
            // 
            this.lbTxet.AutoSize = true;
            this.lbTxet.Location = new System.Drawing.Point(20, 64);
            this.lbTxet.Name = "lbTxet";
            this.lbTxet.Size = new System.Drawing.Size(29, 12);
            this.lbTxet.TabIndex = 3;
            this.lbTxet.Text = "Text";
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Location = new System.Drawing.Point(20, 12);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(29, 12);
            this.lbType.TabIndex = 1;
            this.lbType.Text = "Type";
            // 
            // StorageOut_OnlineOut_TWW_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 506);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "StorageOut_OnlineOut_TWW_Edit";
            this.Text = "StorageOut_OnlineOut_TWW_Edit";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lbTxet;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbVbeln;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.ComboBox cmbVbeln;
        private System.Windows.Forms.ComboBox cmbType;
    }
}