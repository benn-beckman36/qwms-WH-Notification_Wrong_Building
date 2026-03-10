namespace QWMS
{
    partial class Admin_UserMaintenance_List
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
            this.dgvUserList = new System.Windows.Forms.DataGridView();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlantAuthority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrAuthority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiAuthority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SystemMaintenance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Management = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhysicalCounting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUserList
            // 
            this.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.User,
            this.PlantAuthority,
            this.GrAuthority,
            this.GiAuthority,
            this.SystemMaintenance,
            this.Management,
            this.PhysicalCounting});
            this.dgvUserList.Location = new System.Drawing.Point(7, 14);
            this.dgvUserList.Name = "dgvUserList";
            this.dgvUserList.RowTemplate.Height = 24;
            this.dgvUserList.Size = new System.Drawing.Size(682, 272);
            this.dgvUserList.TabIndex = 0;
            this.dgvUserList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUserList_CellClick);
            // 
            // User
            // 
            this.User.DataPropertyName = "USRNM";
            this.User.HeaderText = "User";
            this.User.Name = "User";
            // 
            // PlantAuthority
            // 
            this.PlantAuthority.DataPropertyName = "WKAUT";
            this.PlantAuthority.HeaderText = "Plant Authority";
            this.PlantAuthority.Name = "PlantAuthority";
            // 
            // GrAuthority
            // 
            this.GrAuthority.DataPropertyName = "INAUT";
            this.GrAuthority.HeaderText = "G/R Authority";
            this.GrAuthority.Name = "GrAuthority";
            // 
            // GiAuthority
            // 
            this.GiAuthority.DataPropertyName = "OTAUT";
            this.GiAuthority.HeaderText = "G/I Authority";
            this.GiAuthority.Name = "GiAuthority";
            // 
            // SystemMaintenance
            // 
            this.SystemMaintenance.DataPropertyName = "MAAUT";
            this.SystemMaintenance.HeaderText = "System Maintenance";
            this.SystemMaintenance.Name = "SystemMaintenance";
            // 
            // Management
            // 
            this.Management.DataPropertyName = "MGAUT";
            this.Management.HeaderText = "Management";
            this.Management.Name = "Management";
            // 
            // PhysicalCounting
            // 
            this.PhysicalCounting.DataPropertyName = "IVAUT";
            this.PhysicalCounting.HeaderText = "Physical Counting";
            this.PhysicalCounting.Name = "PhysicalCounting";
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.Enabled = false;
            this.btnSelect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSelect.Location = new System.Drawing.Point(21, 309);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 21);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnReturn.Location = new System.Drawing.Point(128, 309);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 21);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDownload.Location = new System.Drawing.Point(235, 309);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(95, 21);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "UserAccountList.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblCount.Location = new System.Drawing.Point(6, 5);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 15);
            this.lblCount.TabIndex = 45;
            // 
            // Admin_UserMaintenance_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 341);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dgvUserList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Admin_UserMaintenance_List";
            this.Text = "User List";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUserList;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlantAuthority;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrAuthority;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiAuthority;
        private System.Windows.Forms.DataGridViewTextBoxColumn SystemMaintenance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Management;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhysicalCounting;
        private System.Windows.Forms.Label lblCount;
    }
}