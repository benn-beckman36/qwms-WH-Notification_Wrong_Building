using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;
using QCI_QWMS_Models;


namespace QWMS.Models
{
    public partial class Model_StorageIn_OffLineIn : Form
    {
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strMatnr = "";
        private string strType = "";
        private string strMrgid = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strComcd = "";
        private bool bolDuplicate = false;
        private int intFormIndex = 0;
        private DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private StorageData objStorageData;

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
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.btnBatchImport = new System.Windows.Forms.Button();
            this.txtAssetNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtModelNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rdoAdd = new System.Windows.Forms.RadioButton();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.rdoNew = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblData = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.gbHeader.SuspendLayout();
            this.panel4.SuspendLayout();
            this.gbFunction.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            this.SuspendLayout();
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.btnBatchImport);
            this.gbHeader.Controls.Add(this.txtAssetNo);
            this.gbHeader.Controls.Add(this.label5);
            this.gbHeader.Controls.Add(this.txtModelNo);
            this.gbHeader.Controls.Add(this.label4);
            this.gbHeader.Controls.Add(this.cmbWerks);
            this.gbHeader.Controls.Add(this.txtLocat);
            this.gbHeader.Controls.Add(this.label2);
            this.gbHeader.Controls.Add(this.btnConfirm);
            this.gbHeader.Controls.Add(this.label3);
            this.gbHeader.Controls.Add(this.label1);
            this.gbHeader.Controls.Add(this.cmbLgort);
            this.gbHeader.Enabled = false;
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(10, 7);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(452, 105);
            this.gbHeader.TabIndex = 32;
            this.gbHeader.TabStop = false;
            // 
            // btnBatchImport
            // 
            this.btnBatchImport.Location = new System.Drawing.Point(252, 70);
            this.btnBatchImport.Name = "btnBatchImport";
            this.btnBatchImport.Size = new System.Drawing.Size(98, 30);
            this.btnBatchImport.TabIndex = 38;
            this.btnBatchImport.Text = "Batch Import";
            this.btnBatchImport.UseVisualStyleBackColor = true;
            this.btnBatchImport.Click += new System.EventHandler(this.btnBatchImport_Click);
            // 
            // txtAssetNo
            // 
            this.txtAssetNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAssetNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAssetNo.Location = new System.Drawing.Point(306, 45);
            this.txtAssetNo.MaxLength = 9;
            this.txtAssetNo.Name = "txtAssetNo";
            this.txtAssetNo.Size = new System.Drawing.Size(140, 22);
            this.txtAssetNo.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Asset No";
            // 
            // txtModelNo
            // 
            this.txtModelNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtModelNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtModelNo.Location = new System.Drawing.Point(306, 14);
            this.txtModelNo.MaxLength = 20;
            this.txtModelNo.Name = "txtModelNo";
            this.txtModelNo.Size = new System.Drawing.Size(140, 22);
            this.txtModelNo.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(230, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Model No";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(87, 15);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(137, 24);
            this.cmbWerks.TabIndex = 2;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // txtLocat
            // 
            this.txtLocat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocat.Location = new System.Drawing.Point(87, 75);
            this.txtLocat.MaxLength = 10;
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(137, 22);
            this.txtLocat.TabIndex = 4;
            this.txtLocat.DoubleClick += new System.EventHandler(this.txtLocat_DoubleClick);
            this.txtLocat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocat_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(19, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(356, 70);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(90, 30);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(19, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Location";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(87, 45);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(137, 24);
            this.cmbLgort.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.gbHeader);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(250, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(495, 134);
            this.panel4.TabIndex = 34;
            // 
            // rdoAdd
            // 
            this.rdoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoAdd.Location = new System.Drawing.Point(19, 67);
            this.rdoAdd.Name = "rdoAdd";
            this.rdoAdd.Size = new System.Drawing.Size(125, 23);
            this.rdoAdd.TabIndex = 1;
            this.rdoAdd.Text = "Add In";
            this.rdoAdd.CheckedChanged += new System.EventHandler(this.rdoAdd_CheckedChanged);
            // 
            // gbFunction
            // 
            this.gbFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFunction.Controls.Add(this.rdoAdd);
            this.gbFunction.Controls.Add(this.rdoNew);
            this.gbFunction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFunction.Location = new System.Drawing.Point(29, 7);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(201, 105);
            this.gbFunction.TabIndex = 31;
            this.gbFunction.TabStop = false;
            // 
            // rdoNew
            // 
            this.rdoNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoNew.Location = new System.Drawing.Point(19, 30);
            this.rdoNew.Name = "rdoNew";
            this.rdoNew.Size = new System.Drawing.Size(125, 22);
            this.rdoNew.TabIndex = 0;
            this.rdoNew.Text = "New Pallet";
            this.rdoNew.CheckedChanged += new System.EventHandler(this.rdoNew_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbFunction);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 134);
            this.panel3.TabIndex = 33;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblData);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 134);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(745, 288);
            this.panel2.TabIndex = 37;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Location = new System.Drawing.Point(29, 0);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(61, 14);
            this.lblData.TabIndex = 31;
            this.lblData.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(29, 21);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(635, 207);
            this.dgvData.TabIndex = 30;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(221, 249);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 33);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(125, 249);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 33);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(29, 249);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 33);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 134);
            this.panel1.TabIndex = 36;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 430;
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            // 
            // stsComcd
            // 
            this.stsComcd.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Width = 40;
            // 
            // stsMandt
            // 
            this.stsMandt.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Width = 40;
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 422);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(745, 20);
            this.stbStatus.TabIndex = 35;
            this.stbStatus.Text = "Status";
            // 
            // Model_StorageIn_OffLineIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 442);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Name = "Model_StorageIn_OffLineIn";
            this.Text = "离线入库";
            this.Resize += new System.EventHandler(this.Model_StorageIn_OffLineIn_Resize);
            this.gbHeader.ResumeLayout(false);
            this.gbHeader.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.gbFunction.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rdoAdd;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.RadioButton rdoNew;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtModelNo;
        private System.Windows.Forms.TextBox txtAssetNo;
        private System.Windows.Forms.Label label5;

        #region 設定變數
        public string Mandt
        {
            get
            {
                return strMandt;
            }
            set
            {
                strMandt = value;
            }
        }

        public string Usrnm
        {
            get
            {
                return strUsrnm;
            }
            set
            {
                strUsrnm = value;
            }
        }

        public string Progid
        {
            get
            {
                return strProgid;
            }
            set
            {
                strProgid = value;
            }
        }

        public string Werks
        {
            get
            {
                return strWerks;
            }
            set
            {
                strWerks = value;
            }
        }

        public string Lgort
        {
            get
            {
                return strLgort;
            }
            set
            {
                strLgort = value;
            }
        }

        public string Locat
        {
            get
            {
                return this.txtLocat.Text.Trim();
            }
            set
            {
                this.txtLocat.Text = value;
            }
        }

        public string Matnr
        {
            get
            {
                return strMatnr;
            }
            set
            {
                strMatnr = value;
            }
        }

        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }

        public string Charg
        {
            get
            {
                return strCharg;
            }
            set
            {
                strCharg = value;
            }
        }

        public string Mrgid
        {
            get
            {
                return strMrgid;
            }
            set
            {
                strMrgid = value;
            }
        }

        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
            }
        }

        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
            }
        }

        public bool Duplicate
        {
            get
            {
                return bolDuplicate;
            }
            set
            {
                bolDuplicate = value;
            }
        }

        public string Comcd
        {
            get
            {
                return strComcd;
            }
            set
            {
                strComcd = value;
            }
        }

        #endregion

        public Model_StorageIn_OffLineIn(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                //檢查權限
                if (!objStorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    bolDuplicate = objStorageIn.CheckDuplicatLocat();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";

                if (cmbLgort.SelectedIndex != -1)
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    Lgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
                    objStorageIn_LocationSelect.ShowDialog();
                    txtLocat.Text = objStorageIn_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void rdoNew_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                strLocat = txtLocat.Text.Trim();

                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                //取得Sttyp及Lotyp
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                if (dtTemp.Rows.Count >= 1)
                {
                    strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                    strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                }
                else
                {
                    stsWarning.Text = "Can't find the storage data!!";
                    return;
                }

                    if (strType == "ADD")
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            stsWarning.Text = "Please input a location first!!";
                            this.txtLocat.Focus();
                            return;
                        }
                    }
                    if (strType == "NEW")
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            txtLocat.Text = objPlantData.GetEmptyLocation(Werks, Lgort);
                        }
                        else
                        {
                            if (objPlantData.CheckStorageData(Werks, Lgort, Locat))
                            {
                                stsWarning.Text = "The location you input is not a empty location!!";
                                this.txtLocat.Focus();
                                return;
                            }
                        }
                    }
                    //储位不为空
                    if (txtLocat.Text.Trim() == "")
                    {
                        stsWarning.Text = "Please input a location first!!";
                        this.txtLocat.Focus();
                        return;
                    }
                    //储位是否存在
                    if (!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
                    {
                        stsWarning.Text = "The location doesn't exist!!";
                        this.txtLocat.Focus();
                        return;
                    }
                    //模号不可为空
                    if(txtModelNo.Text.Trim() == "" )
                    {
                        stsWarning.Text = "ModelNo can't be empty";
                        return;
                    }

                    ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                    //资产编号、模号是否存在
                if (this.txtAssetNo.Text.Trim() != "")
                {
                    if (!objModelsData.CheckExistedAssetsModel(this.txtModelNo.Text.Trim(), this.txtAssetNo.Text.Trim()))
                    {
                        MessageBox.Show(
                            this.txtModelNo.Text.Trim() + "and" + this.txtAssetNo.Text.Trim() + " doesn't exist!!",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                else
                { 
                    if (!objModelsData.CheckExistedAssetsModel(this.txtModelNo.Text.Trim(), ""))
                    {
                        MessageBox.Show(
                            this.txtModelNo.Text.Trim() + " doesn't exist!!",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string strModels = txtModelNo.Text.Trim();
                    string strAssets = txtAssetNo.Text.Trim();
                    dtData = objModelsData.QueryModelData(strLocat,strModels, strAssets);
                    if (dtData.Rows.Count > 0)
                    {
                        ShowDataGrid();
                        this.gbHeader.Enabled = false;
                        this.btnSave.Enabled = true;
                        
                    }
                    if (dtData.Rows.Count == 0) 
                    {
                        stsWarning.Text = "No data!!";
                        return;
                    }
                gbHeader.Enabled = false;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcModelNO = new DataGridViewTextBoxColumn();
                dgvcModelNO.DataPropertyName = "ModelNO";
                dgvcModelNO.HeaderText = "模号";
                dgvcModelNO.Width = 90;
                dgvcModelNO.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcModelNO);

                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "资产编号";
                dgvcAssetsNo.Width = 90;
                dgvcAssetsNo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAssetsNo);

                DataGridViewTextBoxColumn dgvcItemName = new DataGridViewTextBoxColumn();
                dgvcItemName.DataPropertyName = "ItemName";
                dgvcItemName.HeaderText = "品名";
                dgvcItemName.Width = 50;
                dgvcItemName.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcItemName);

                DataGridViewTextBoxColumn dgvcBU = new DataGridViewTextBoxColumn();
                dgvcBU.DataPropertyName = "BU";
                dgvcBU.HeaderText = "PU";
                dgvcBU.Width = 60;
                dgvcBU.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBU);

                DataGridViewTextBoxColumn dgvcMachine = new DataGridViewTextBoxColumn();
                dgvcMachine.DataPropertyName = "Machine";
                dgvcMachine.HeaderText = "机种";
                dgvcMachine.Width = 90;
                dgvcMachine.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMachine);

                DataGridViewTextBoxColumn dgvcQuantity = new DataGridViewTextBoxColumn();
                dgvcQuantity.DataPropertyName = "Quantity";
                dgvcQuantity.HeaderText = "数量";
                dgvcQuantity.Width = 90;
                dgvcQuantity.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQuantity);

                DataGridViewTextBoxColumn dgvcNweight = new DataGridViewTextBoxColumn();
                dgvcNweight.DataPropertyName = "Nweight";
                dgvcNweight.HeaderText = "净重";
                dgvcNweight.Width = 90;
                dgvcNweight.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcNweight);

                DataGridViewTextBoxColumn dgvcPoNo = new DataGridViewTextBoxColumn();
                dgvcPoNo.DataPropertyName = "PoNo";
                dgvcPoNo.HeaderText = "PO.";
                dgvcPoNo.Width = 90;
                dgvcPoNo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcPoNo);

                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "Remark";
                dgvcRemark.HeaderText = "模具备注";
                dgvcRemark.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRemark);

                DataGridViewTextBoxColumn dgvcCRNAM = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRNAM";
                dgvcCRNAM.HeaderText = "建立者";
                dgvcCRNAM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCRNAM);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRDAT";
                dgvcCRNAM.HeaderText = "建立时间";
                dgvcCRNAM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCRDAT);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            try
            {
                SetbtnSaveProcess();
                stsWarning.Text = "";
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "The data can't be empty!!";
                    SetbtnSaveException();
                    return;
                }

                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                if (objModelsData.CheckRepeatAssetsModel(txtLocat.Text.Trim(), dtData))
                {
                    MessageBox.Show(this.txtModelNo.Text.Trim() + "has storage in before!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (objModelsData.ModelOffLineInData(Werks, Lgort, Locat, dtData))
                    {
                        stsWarning.Text = "Add OK!!";
                        this.btnSave.Enabled = false;
                        return;
                    }
                    else
                    {
                        stsWarning.Text = "Add fail!! " + objModelsData.ERRMSG;
                        SetbtnSaveException();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetbtnSaveException();
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.txtModelNo.Text = "";
            this.txtAssetNo.Text = "";
            this.gbFunction.Enabled = true;
            this.lblData.Text = "0 records";
            this.txtLocat.Text = "";
            this.gbHeader.Enabled = false;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.btnSave.Enabled = false;
            strWerks = "";
            strLgort = "";
            strInsmk = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Model_StorageIn_OffLineIn_Resize(object sender, EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }

        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    btnConfirm_Click(null, null);
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }

        private bool CheckIsOpen(string strForm)
        {
            bool bolOpened = false;
            string strTest = "";
            try
            {
                for (int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
                {
                    strTest = MdiParent.MdiChildren[i].ToString();
                    if (MdiParent.MdiChildren[i].ToString().IndexOf(strForm) != -1)
                    {
                        bolOpened = true;
                        intFormIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CheckIsOpen()");
            }
            return bolOpened;
        }

        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;

        }
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

        private void btnBatchImport_Click(object sender, EventArgs e)
        {
            Model_StrorageIn_OffLineIn_BatchImport batchImport = new Model_StrorageIn_OffLineIn_BatchImport(UserData,Progid );
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                strLocat = txtLocat.Text.Trim();

                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                //取得Sttyp及Lotyp
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                if (dtTemp.Rows.Count >= 1)
                {
                    strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                    strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                }
                else
                {
                    stsWarning.Text = "Can't find the storage data!!";
                    return;
                }

                if (strType == "ADD")
                {
                    if (txtLocat.Text.Trim() == "")
                    {
                        stsWarning.Text = "Please input a location first!!";
                        this.txtLocat.Focus();
                        return;
                    }
                }
                if (strType == "NEW")
                {
                    if (txtLocat.Text.Trim() == "")
                    {
                        txtLocat.Text = objPlantData.GetEmptyLocation(Werks, Lgort);
                    }
                    else
                    {
                        if (objPlantData.CheckStorageData(Werks, Lgort, Locat))
                        {
                            stsWarning.Text = "The location you input is not a empty location!!";
                            this.txtLocat.Focus();
                            return;
                        }
                    }
                }
                //储位不为空
                if (txtLocat.Text.Trim() == "")
                {
                    stsWarning.Text = "Please input a location first!!";
                    this.txtLocat.Focus();
                    return;
                }
                //储位是否存在
                if (!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
                {
                    stsWarning.Text = "The location doesn't exist!!";
                    this.txtLocat.Focus();
                    return;
                }
                batchImport.Werks = Werks;
                batchImport.Lgort = Lgort;
                batchImport.Locat = txtLocat.Text.Trim().ToString();
                if (batchImport.ShowDialog() == DialogResult.OK)
                {
                    txtLocat.Text = batchImport.Locat;
                    cmbWerks.Text = batchImport.Werks;
                    cmbLgort.Text = batchImport.Lgort;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
            
            this.gbHeader.Enabled = false;
        }
    }
}
