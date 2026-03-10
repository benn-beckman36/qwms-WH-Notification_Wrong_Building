namespace QWMS.OTAUT
{
    partial class StorageOut_OnlineOut_TWW
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.lbWerks = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.lbMblnr = new System.Windows.Forms.Label();
            this.txtMblnr = new System.Windows.Forms.TextBox();
            this.lbLgort = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnFresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbCondition.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 488);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.gbCondition);
            this.panel3.Location = new System.Drawing.Point(3, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(868, 151);
            this.panel3.TabIndex = 1;
            // 
            // gbCondition
            // 
            this.gbCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCondition.Controls.Add(this.lbWerks);
            this.gbCondition.Controls.Add(this.cmbWerks);
            this.gbCondition.Controls.Add(this.lbMblnr);
            this.gbCondition.Controls.Add(this.txtMblnr);
            this.gbCondition.Controls.Add(this.lbLgort);
            this.gbCondition.Controls.Add(this.cmbLgort);
            this.gbCondition.Location = new System.Drawing.Point(9, 12);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(847, 135);
            this.gbCondition.TabIndex = 0;
            this.gbCondition.TabStop = false;
            // 
            // lbWerks
            // 
            this.lbWerks.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbWerks.Location = new System.Drawing.Point(21, 23);
            this.lbWerks.Name = "lbWerks";
            this.lbWerks.Size = new System.Drawing.Size(68, 22);
            this.lbWerks.TabIndex = 14;
            this.lbWerks.Text = "Werks";
            this.lbWerks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.ItemHeight = 12;
            this.cmbWerks.Location = new System.Drawing.Point(97, 25);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(199, 20);
            this.cmbWerks.TabIndex = 13;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // lbMblnr
            // 
            this.lbMblnr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbMblnr.Location = new System.Drawing.Point(13, 90);
            this.lbMblnr.Name = "lbMblnr";
            this.lbMblnr.Size = new System.Drawing.Size(77, 22);
            this.lbMblnr.TabIndex = 12;
            this.lbMblnr.Text = "Document No.";
            this.lbMblnr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMblnr
            // 
            this.txtMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMblnr.Location = new System.Drawing.Point(96, 90);
            this.txtMblnr.MaxLength = 100;
            this.txtMblnr.Name = "txtMblnr";
            this.txtMblnr.Size = new System.Drawing.Size(393, 21);
            this.txtMblnr.TabIndex = 11;
            this.txtMblnr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMblnr_KeyPress);
            // 
            // lbLgort
            // 
            this.lbLgort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbLgort.Location = new System.Drawing.Point(21, 57);
            this.lbLgort.Name = "lbLgort";
            this.lbLgort.Size = new System.Drawing.Size(68, 22);
            this.lbLgort.TabIndex = 10;
            this.lbLgort.Text = "Storage In";
            this.lbLgort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.ItemHeight = 12;
            this.cmbLgort.Location = new System.Drawing.Point(97, 59);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(199, 20);
            this.cmbLgort.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnFresh);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnConfirm);
            this.panel2.Controls.Add(this.dgView);
            this.panel2.Location = new System.Drawing.Point(3, 153);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(865, 332);
            this.panel2.TabIndex = 0;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Enabled = false;
            this.btnEdit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(123, 293);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(90, 30);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Enabled = false;
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(477, 293);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            // 
            // btnFresh
            // 
            this.btnFresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFresh.Enabled = false;
            this.btnFresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFresh.Location = new System.Drawing.Point(355, 293);
            this.btnFresh.Name = "btnFresh";
            this.btnFresh.Size = new System.Drawing.Size(90, 30);
            this.btnFresh.TabIndex = 9;
            this.btnFresh.Text = "Refresh";
            this.btnFresh.Click += new System.EventHandler(this.btnFresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(237, 293);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirm.Enabled = false;
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(9, 293);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 30);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dgView
            // 
            this.dgView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Location = new System.Drawing.Point(3, 3);
            this.dgView.Name = "dgView";
            this.dgView.RowTemplate.Height = 23;
            this.dgView.Size = new System.Drawing.Size(859, 284);
            this.dgView.TabIndex = 0;
            // 
            // StorageOut_OnlineOut_TWW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 488);
            this.Controls.Add(this.panel1);
            this.Name = "StorageOut_OnlineOut_TWW";
            this.Text = "StorageOut_OnlineOut_TWW";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbCondition.ResumeLayout(false);
            this.gbCondition.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbCondition;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Label lbLgort;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.Label lbMblnr;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbWerks;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Button btnFresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnEdit;
    }
}