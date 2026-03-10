namespace QWMS
{
    partial class Manage_MaterialConvertSelect
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
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.dtgData = new System.Windows.Forms.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Location = new System.Drawing.Point(242, 390);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(90, 30);
            this.btnReturn.TabIndex = 71;
            this.btnReturn.Text = "Return";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Enabled = false;
            this.btnSelect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(32, 390);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(90, 30);
            this.btnSelect.TabIndex = 70;
            this.btnSelect.Text = "Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // dtgData
            // 
            this.dtgData.DataMember = "";
            this.dtgData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgData.Location = new System.Drawing.Point(2, 2);
            this.dtgData.Name = "dtgData";
            this.dtgData.Size = new System.Drawing.Size(371, 373);
            this.dtgData.TabIndex = 69;
            this.dtgData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgData_MouseDown);
            // 
            // Manage_MaterialConvertSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 442);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dtgData);
            this.Name = "Manage_MaterialConvertSelect";
            this.Text = "Manage_SapDataSelect";
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMblnr;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.DateTimePicker dtpOudat;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGrid dtgData;
    }
}