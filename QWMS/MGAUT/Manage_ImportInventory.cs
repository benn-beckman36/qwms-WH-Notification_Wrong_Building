using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using QCI.QWMS;
using System.Diagnostics;
using QWMS.Common;
using QWMS.Entity;
using Qci.Base.Common;
using System.Data.OleDb;
using System.Text;



namespace QWMS
{
    /// <summary>
    /// Manage_ImportInventory ªººK­n´y­z¡C
    /// </summary>
    public class Manage_ImportInventory : System.Windows.Forms.Form
    {

        UserInfo UserData = new UserInfo();
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLgort;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel7;

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private ArrayList arySQL = new ArrayList();
        //private SQLAccess objDB;
        private PlantData objPlantData;
        private Authority objAuthority;
        //private AccessConfig objConfig;
        private StorageData objStorageData;
        private StorageIn objStorageIn;
        //private StreamReader srFileReader;
        private DateTime beforeTime;
        private DateTime afterTime;
        private System.Windows.Forms.LinkLabel lnkSample;
        private DataGridView dgvData;
        private CheckBox chkInventory;
        private Label lblWarning;
        private Button btnInvImport;


        /// <summary>
        /// ³]­p¤u¨ã©Ò»ÝªºÅÜ¼Æ¡C
        /// </summary>
        private System.ComponentModel.Container components = null;

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

        public Manage_ImportInventory(UserInfo _UserData, string strProgid)
        {
            UserData = _UserData;
            InitializeComponent();
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                //objConfig = new AccessConfig(Mandt, "QWMS.xml");
                //objDB = new SQLAccess(objConfig.DBServer, objConfig.UserID, objConfig.Password, objConfig.InitialCatalog);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(UserData, Progid);

                //ÀË¬dÅv­­
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
               
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    objStorageData = new StorageData(UserData);

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

        /// <summary>
        /// ²M°£¥ô¦ó¨Ï¥Î¤¤ªº¸ê·½¡C
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form ³]­p¤u¨ã²£¥Íªºµ{¦¡½X
        /// <summary>
        /// ¦¹¬°³]­p¤u¨ã¤ä´©©Ò¥²¶·ªº¤èªk - ½Ð¤Å¨Ï¥Îµ{¦¡½X½s¿è¾¹­×§ï
        /// ³o­Ó¤èªkªº¤º®e¡C
        /// </summary>
        private void InitializeComponent()
        {
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnInvImport = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblWarning = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lnkSample = new System.Windows.Forms.LinkLabel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.chkInventory = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "InventoryComparsion.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnInvImport);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Controls.Add(this.btnExecute);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(878, 455);
            this.panel2.TabIndex = 35;
            // 
            // btnInvImport
            // 
            this.btnInvImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInvImport.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInvImport.Location = new System.Drawing.Point(422, 413);
            this.btnInvImport.Name = "btnInvImport";
            this.btnInvImport.Size = new System.Drawing.Size(105, 30);
            this.btnInvImport.TabIndex = 48;
            this.btnInvImport.Text = "盘点导入";
            this.btnInvImport.UseVisualStyleBackColor = true;
            this.btnInvImport.Click += new System.EventHandler(this.btnInvImport_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(317, 413);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 29);
            this.btnExit.TabIndex = 27;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(29, 413);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(90, 30);
            this.btnImport.TabIndex = 24;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecute.Enabled = false;
            this.btnExecute.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Location = new System.Drawing.Point(125, 413);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(90, 30);
            this.btnExecute.TabIndex = 28;
            this.btnExecute.Text = "Execute";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(221, 413);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 29);
            this.btnRefresh.TabIndex = 26;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(29, 97);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(814, 306);
            this.dgvData.TabIndex = 47;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 410;
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 455);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(878, 18);
            this.stbStatus.TabIndex = 33;
            this.stbStatus.Text = "Status";
            // 
            // stsMandt
            // 
            this.stsMandt.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsMandt.Name = "stsMandt";
            this.stsMandt.Width = 40;
            // 
            // stsComcd
            // 
            this.stsComcd.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsComcd.Name = "stsComcd";
            this.stsComcd.Width = 40;
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 90);
            this.panel1.TabIndex = 34;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblWarning);
            this.panel4.Controls.Add(this.txtFilePath);
            this.panel4.Controls.Add(this.btnFile);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 45);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(878, 45);
            this.panel4.TabIndex = 15;
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Location = new System.Drawing.Point(508, 23);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(0, 15);
            this.lblWarning.TabIndex = 12;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(80, 15);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(396, 21);
            this.txtFilePath.TabIndex = 10;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFile.Location = new System.Drawing.Point(497, 15);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(28, 21);
            this.btnFile.TabIndex = 11;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "File Path";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(878, 45);
            this.panel3.TabIndex = 14;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.lnkSample);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(624, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(254, 45);
            this.panel8.TabIndex = 47;
            // 
            // lnkSample
            // 
            this.lnkSample.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSample.Location = new System.Drawing.Point(58, 15);
            this.lnkSample.Name = "lnkSample";
            this.lnkSample.Size = new System.Drawing.Size(96, 15);
            this.lnkSample.TabIndex = 12;
            this.lnkSample.TabStop = true;
            this.lnkSample.Text = "Sample";
            this.lnkSample.Click += new System.EventHandler(this.lnkSample_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.cmbLgort);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(422, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(202, 45);
            this.panel6.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(19, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(86, 15);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(106, 23);
            this.cmbLgort.TabIndex = 13;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cmbWerks);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(202, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(220, 45);
            this.panel5.TabIndex = 14;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(96, 15);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(106, 23);
            this.cmbWerks.TabIndex = 7;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.chkInventory);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(202, 45);
            this.panel7.TabIndex = 46;
            // 
            // chkInventory
            // 
            this.chkInventory.AutoSize = true;
            this.chkInventory.Location = new System.Drawing.Point(29, 15);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(52, 19);
            this.chkInventory.TabIndex = 0;
            this.chkInventory.Text = "盘点";
            this.chkInventory.UseVisualStyleBackColor = true;
            // 
            // Manage_ImportInventory
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(878, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.stbStatus);
            this.Name = "Manage_ImportInventory";
            this.Text = "Manage_ImportInventory";
            this.Resize += new System.EventHandler(this.Manage_ImportInventory_Resize);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region ×´Ì¬À¸ºÍ³§Çø²Ö±ð
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
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
        #endregion

        private void btnFile_Click(object sender, System.EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            stsWarning.Text = "";
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "Plant and storage can't be empty!!";
                return;
            }
            if (chkInventory.Checked == true)
            {
                this.btnImport.Enabled = false;
            }
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = ofdOpenFile.FileName;
            }

            //this.txtFilePath.Text = "C:\\111.xls";
        }

        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();

            //dtgData.TableStyles.Clear();
            try
            {
                //DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                //mydtgTableStyle.MappingName = dtData.TableName;

                DataGridViewTextBoxColumn mandtStyle = new DataGridViewTextBoxColumn();
                mandtStyle.DataPropertyName = "MANDT";
                mandtStyle.HeaderText = "Client";
                mandtStyle.ReadOnly = true;
                dgvData.Columns.Add(mandtStyle);


                DataGridViewTextBoxColumn werksStyle = new DataGridViewTextBoxColumn();
                werksStyle.DataPropertyName = "WERKS";
                werksStyle.HeaderText = "Plant";
                werksStyle.ReadOnly = true;
                dgvData.Columns.Add(werksStyle);

                DataGridViewTextBoxColumn lgortStyle = new DataGridViewTextBoxColumn();
                lgortStyle.DataPropertyName = "LGORT";
                lgortStyle.HeaderText = "Storage";
                lgortStyle.ReadOnly = true;
                dgvData.Columns.Add(lgortStyle);

                DataGridViewTextBoxColumn locatStyle = new DataGridViewTextBoxColumn();
                locatStyle.DataPropertyName = "LOCAT";
                locatStyle.HeaderText = "Location";
                locatStyle.ReadOnly = true;
                dgvData.Columns.Add(locatStyle);

                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                dgvData.Columns.Add(matnrStyle);

                DataGridViewTextBoxColumn insmkStyle = new DataGridViewTextBoxColumn();
                insmkStyle.DataPropertyName = "INSMK";
                insmkStyle.HeaderText = "Stock";
                insmkStyle.ReadOnly = true;
                dgvData.Columns.Add(insmkStyle);

                DataGridViewTextBoxColumn chargStyle = new DataGridViewTextBoxColumn();
                chargStyle.DataPropertyName = "CHARG";
                chargStyle.HeaderText = "Version";
                chargStyle.ReadOnly = true;
                dgvData.Columns.Add(chargStyle);

                DataGridViewTextBoxColumn rmanoStyle = new DataGridViewTextBoxColumn();
                rmanoStyle.DataPropertyName = "RMANO";
                rmanoStyle.HeaderText = "RMA No.";
                rmanoStyle.Width = 110;
                rmanoStyle.ReadOnly = true;
                dgvData.Columns.Add(rmanoStyle);

                DataGridViewTextBoxColumn rmanoLIFNR = new DataGridViewTextBoxColumn();
                rmanoLIFNR.DataPropertyName = "LIFNR";
                rmanoLIFNR.HeaderText = "Vendor";
                rmanoLIFNR.Width = 80;
                rmanoLIFNR.ReadOnly = true;
                dgvData.Columns.Add(rmanoLIFNR);

                DataGridViewTextBoxColumn rmanoDACOD = new DataGridViewTextBoxColumn();
                rmanoDACOD.DataPropertyName = "DACOD";
                rmanoDACOD.HeaderText = "DateCode";
                rmanoDACOD.Width = 110;
                rmanoDACOD.ReadOnly = true;
                dgvData.Columns.Add(rmanoDACOD);

                DataGridViewTextBoxColumn rmanoVEDAT = new DataGridViewTextBoxColumn();
                rmanoVEDAT.DataPropertyName = "VEDAT";
                rmanoVEDAT.HeaderText = "Vendor Manufacture Date";
                rmanoVEDAT.Width = 110;
                rmanoVEDAT.ReadOnly = true;
                dgvData.Columns.Add(rmanoVEDAT);

                DataGridViewTextBoxColumn rmanoLOCOD = new DataGridViewTextBoxColumn();
                rmanoLOCOD.DataPropertyName = "LOCOD";
                rmanoLOCOD.HeaderText = "LockCode";
                rmanoLOCOD.Width = 110;
                rmanoLOCOD.ReadOnly = true;
                dgvData.Columns.Add(rmanoLOCOD);

                DataGridViewTextBoxColumn indatStyle = new DataGridViewTextBoxColumn();
                indatStyle.DataPropertyName = "INDAT";
                indatStyle.HeaderText = "Date";
                indatStyle.ReadOnly = true;
                dgvData.Columns.Add(indatStyle);

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
                mengeStyle.HeaderText = "Qty";
                mengeStyle.ReadOnly = true;
                dgvData.Columns.Add(mengeStyle);

                dgvData.DataSource = dtData;

                //dtgData.DataSource = dtData;	
                //dtgData.TableStyles.Add(mydtgTableStyle);

                //dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnImport_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strWerks = cmbWerks.Text.Trim().ToString();
            strLgort = cmbLgort.Text.Trim().ToString();
            try
            {
                if (this.txtFilePath.Text.Trim() == "")
                {
                    stsWarning.Text = "Please select one file!!";
                    return;
                }
                dtData = GetExcelData();
                if (dtData.Rows.Count > 0)
                {
                    #region Êý¾ÝÐ£Ñé
                    ArrayList arrCheck = new ArrayList();
                    DataRow[] drSelect = dtData.Select(" WERKS = '" + strWerks + "' AND LGORT = '" + strLgort + "' ");
                    if (drSelect.Length != dtData.Rows.Count)
                    {
                        stsWarning.Text = "导入的数据中厂区仓别与选择的不一致，请确认！！";
                        return;
                    }
                    else
                    {
                        StorageData objStorage = new StorageData(UserData);

                        #region 判断厂区仓别是否有库存，有库存不允许导入
                        if (objStorage.QueryStorageCount(strWerks, strLgort) && strLgort.Substring(0, 2).ToString() != "FG" && strLgort.Substring(0, 2).ToString() != "FJ" && strLgort.Substring(0, 2).ToString() != "RF" && strLgort.Substring(0, 3).ToString() != "PAP" && !(strWerks == "CS41" && (strLgort == "PA1A" || strLgort == "PA1A" || strLgort == "PA1B" || strLgort == "PA1C" || strLgort == "TW1A" || strLgort == "TW1B")) && !(strWerks == "CS31" && (strLgort.Substring(0, 2).ToString() == "SC" || strLgort.Substring(0, 2).ToString() == "RW" || strLgort.Substring(0, 2).ToString() == "BN" )))
                        {
                            stsWarning.Text = "手动转库存只针对无库存的仓别！";
                            btnExecute.Enabled = false;
                            return;
                        }
                        #endregion

                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (string.IsNullOrEmpty(dtData.Rows[i]["MANDT"].ToString()) ||
                                string.IsNullOrEmpty(dtData.Rows[i]["WERKS"].ToString()) ||
                                string.IsNullOrEmpty(dtData.Rows[i]["LGORT"].ToString()) ||
                                string.IsNullOrEmpty(dtData.Rows[i]["LOCAT"].ToString()) ||
                                string.IsNullOrEmpty(dtData.Rows[i]["MATNR"].ToString()) ||
                                string.IsNullOrEmpty(dtData.Rows[i]["INSMK"].ToString()) ||
                                string.IsNullOrEmpty(dtData.Rows[i]["INDAT"].ToString()) ||
                                string.IsNullOrEmpty(dtData.Rows[i]["MENGE"].ToString()))
                            {
                                int B = i + 2;
                                stsWarning.Text = "第" + B + "行存在空值，请确认！！";
                                return;
                            }

                            string strCheckDuplicate = dtData.Rows[i]["MANDT"].ToString() + dtData.Rows[i]["WERKS"].ToString()
                                                              + dtData.Rows[i]["LGORT"].ToString() + dtData.Rows[i]["LOCAT"].ToString()
                                                              + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["INSMK"].ToString()
                                                              + dtData.Rows[i]["CHARG"].ToString();

                            if (!arrCheck.Contains(strCheckDuplicate))
                            {
                                arrCheck.Add(strCheckDuplicate);
                            }
                            else
                            {
                                stsWarning.Text = i + 2 + "行存在重复行，请确认！！";
                                return;
                            }


                            if (dtData.Rows[i]["INDAT"].ToString().Length != 8)
                            {
                                stsWarning.Text = i + 2 + "入库日期不正确，请确认！！";
                                return;
                            }
                            else
                            {
                                try
                                {
                                    DateTime b = DateTime.ParseExact(dtData.Rows[i]["INDAT"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                                }
                                catch (Exception e1)
                                {
                                    stsWarning.Text = "第" + i + 1 + "行入库日期必须为8位数据日期 如：20180101，请确认！！";
                                    return;
                                }
                            }

                            try
                            {
                                int iMenge = int.Parse(dtData.Rows[i]["MENGE"].ToString());
                            }
                            catch (Exception e1)
                            {
                                stsWarning.Text = "第" + i + 1 + "行数量必须为数值，请确认！！";
                                return;
                            }
                            //dtData.Rows[i]["ITEM"]=i;
                            dtData.Rows[i]["REMARK"] = "";
                            dtData.Rows[i]["COMCD"] = Comcd;
                            dtData.Rows[i]["CRNAM"] = UserData.UserId;
                            dtData.Rows[i]["CRDAT"] = DateTime.Now.ToString();

                            if (string.IsNullOrEmpty(dtData.Rows[i]["LIFNR"].ToString()))
                            {
                                dtData.Rows[i]["LIFNR"] = "";
                            }
                            if (string.IsNullOrEmpty(dtData.Rows[i]["DACOD"].ToString()))
                            {
                                dtData.Rows[i]["DACOD"] = "";
                            }
                            if (objStorageData.CheckStorageInType(strWerks, strLgort, "Diff DACOD Diff Locat"))
                            {
                                if (string.IsNullOrEmpty(dtData.Rows[i]["DACOD"].ToString()))
                                {
                                    MessageBox.Show(strWerks + " "+ strLgort + "Date Code管控仓，请检查DateCode维护数据！");
                                    return;
                                }
                                string strVendorcode = dtData.Rows[i]["LIFNR"].ToString();
                                string strDCbefore = dtData.Rows[i]["DACOD"].ToString();
                                try
                                {
                                    bool blTrans = false;
                                    string strTransType = "NEW";
                                    string DC_After = objStorageData.WHDCR_Query(strVendorcode, strDCbefore);
                                    if (DC_After != "")
                                    {
                                        DateTime dtVedat = new DateTime();
                                        try
                                        {
                                            dtVedat = Convert.ToDateTime(DC_After);
                                            DC_After = dtVedat.ToString("yyyyMMdd");
                                            dtData.Rows[i]["VEDAT"] = DC_After;
                                            blTrans = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(strDCbefore + ":DateCode维护规则非日期格式，请检查DateCode维护数据！");
                                            return;

                                        }
                                    }
                                    else if (!blTrans)
                                    {
                                        #region 确认是否要转化DateCode Rule
                                        string strTemp = objStorageData.getDCTrans(strVendorcode, strDCbefore).ToString();
                                        if (!string.IsNullOrEmpty(strTemp))
                                        {
                                            DataTable dtNewDateCode = new DataTable();
                                            dtNewDateCode.Columns.Add("LIFNR");
                                            dtNewDateCode.Columns.Add("DC_Before");
                                            dtNewDateCode.Columns.Add("DC_After");

                                            DataRow dr = dtNewDateCode.NewRow();
                                            dr["LIFNR"] = strVendorcode;
                                            dr["DC_Before"] = strDCbefore;
                                            dr["DC_After"] = strTemp;
                                            dtNewDateCode.Rows.Add(dr.ItemArray);
                                            objStorageData.WHDCR_DML(dtNewDateCode, strTransType);
                                            dtData.Rows[i]["VEDAT"] = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                                        }
                                        else
                                        {
                                            MessageBox.Show("无D/C转换信息找D/C管理人员处理!");
                                            return;

                                        }
                                        #endregion
                                    }
                                }
                                catch (Exception e1)
                                {
                                    MessageBox.Show("DateCode转化错误！" + e1.ToString());
                                    return;
                                }

                            }
                            else 
                            {
                                dtData.Rows[i]["VEDAT"] = "";
                            }

                            if (string.IsNullOrEmpty(dtData.Rows[i]["LOCOD"].ToString()))
                            {
                                dtData.Rows[i]["LOCOD"] = "";
                            }

                        }
                        dtData.Columns["MANDT"].SetOrdinal(0);
                        dtData.Columns["WERKS"].SetOrdinal(1);
                        dtData.Columns["LGORT"].SetOrdinal(2);
                        dtData.Columns["LOCAT"].SetOrdinal(3);
                        dtData.Columns["MATNR"].SetOrdinal(4);
                        dtData.Columns["INSMK"].SetOrdinal(5);
                        dtData.Columns["CHARG"].SetOrdinal(6);
                        dtData.Columns["INDAT"].SetOrdinal(7);
                        dtData.Columns["MENGE"].SetOrdinal(8);
                        dtData.Columns["REMARK"].SetOrdinal(9);
                        dtData.Columns["CRNAM"].SetOrdinal(10);
                        dtData.Columns["CRDAT"].SetOrdinal(11);
                        dtData.Columns["COMCD"].SetOrdinal(12);
                        dtData.Columns["RMANO"].SetOrdinal(13);
                        dtData.Columns["LIFNR"].SetOrdinal(14);
                        dtData.Columns["DACOD"].SetOrdinal(15);
                        dtData.Columns["VEDAT"].SetOrdinal(16);
                        dtData.Columns["LOCOD"].SetOrdinal(17);

                        try
                        {
                            objStorageData = new StorageData(UserData);
                            objStorageData.ImportStorageData(dtData, strWerks, strLgort);
                        }
                        catch (Exception e2)
                        {
                            stsWarning.Text = "导入数据库异常，请确认是否存在同储位同料号的数据！";
                            return;
                        }

                    }
                    #endregion
                }
                else
                {
                    stsWarning.Text = "读取文档异常,请确认!!";
                    return;
                }
                dtData = objStorageData.QueryWhexcelData(strWerks, strLgort);
                ShowDataGrid();

                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "Import Fail !";
                    this.btnExecute.Enabled = false;
                }
                else
                {
                    stsWarning.Text = "Import OK!";
                    this.btnExecute.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        public void ProcessImportFiles(string strContrastFilePath)
        {
            ArrayList alExecuteSQL = new ArrayList();
            bool bolTransaction = false;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ParseImportFiles(strContrastFilePath);
                alExecuteSQL = arySQL;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                throw new Exception(ex.Message + "<-ProcessImportFiles()");
            }

            //Execute SQL
            if (alExecuteSQL.Count > 0)
            {
                try
                {
                    QCI.QWMS.StorageData obj = new StorageData(UserData);
                    bolTransaction = obj.ExeSqlAry(alExecuteSQL);


                    //bolTransaction = obj.ControlSqlAccess.ExecSqlArray(alExecuteSQL);

                    //bolTransaction = objDB.ExecSQLArray(alExecuteSQL);
                    if (bolTransaction)
                    {
                        Cursor.Current = Cursors.Default;
                        stsWarning.Text = "Import OK!";
                        this.panel1.Enabled = false;
                        this.btnImport.Enabled = false;
                        this.btnExecute.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "<-ProcessContrastFiles()");
                }
            }
        }

        public void ParseImportFiles(string varFileName)
        {
            arySQL.Clear();
            beforeTime = DateTime.Now;
            Excel.ApplicationClass app = new Excel.ApplicationClass();
            afterTime = DateTime.Now;

            Excel.Workbook workBook = app.Workbooks.Open(varFileName,
                0,
                true,
                5,
                "",
                "",
                true,
                Excel.XlPlatform.xlWindows,
                "\t",
                false,
                false,
                0,
                true,
                1,
                0);

            Excel.Worksheet workSheet = (Excel.Worksheet)workBook.ActiveSheet;
            int index = 0;
            object rowIndex = 1;
            object colIndex = 1;
            ArrayList arrData = new ArrayList();

            try
            {
                rowIndex = 2 + index;

                DataWhexcel objWhexcel = new DataWhexcel(UserData);
                ArrayList alDeleteCondition = new ArrayList();

                #region É¾³ýÁÙÊ±±í WHEXCEL ¹ØÓÚ¸Ã³§Çø²Ö±ðµÄÊý¾Ý
                alDeleteCondition.Add("MANDT = '" + UserData.Client + "'");
                alDeleteCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
                alDeleteCondition.Add("WERKS = '" + cmbWerks.Items[cmbWerks.SelectedIndex].ToString() + "'");
                alDeleteCondition.Add("LGORT = '" + cmbLgort.Items[cmbLgort.SelectedIndex].ToString() + "'");
                arySQL.Add(objWhexcel.EntityGetDeleteSql(alDeleteCondition));

                #endregion

                #region Ð£Ñéµ¼ÈëµÄexcelÀ¸Î»µÄÎÊÌâ

                #region

                //for check pk
                ArrayList pk_excel = new ArrayList();//
                bool flag_excel = true;//
                bool flag_version = true;//

                while (!string.IsNullOrEmpty(((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString()) &&
                          !string.IsNullOrEmpty(((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2.ToString()))
                {
                    rowIndex = 1 + index;
                    if (!string.IsNullOrEmpty(((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString()) &&
                         !string.IsNullOrEmpty(((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2.ToString()))
                    {
                        #region excel Ð£Ñé²¿·Ö

                        if (flag_excel)//²Ä¤@¦¸Åª¨úexcel
                        {
                            #region excel ÁÐ±íÍ·ÃèÊö

                            string Client = "";
                            string Plant = "";
                            string Storage = "";
                            string Location = "";
                            string Part = "";
                            string Stock = "";
                            string Version = "";
                            string RMAN = "";
                            string Store_In_Date = "";
                            string Qty = "";


                            if (((Excel.Range)workSheet.Cells[1, 1]).Value2 != null)
                                Client = ((Excel.Range)workSheet.Cells[1, 1]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 2]).Value2 != null)
                                Plant = ((Excel.Range)workSheet.Cells[1, 2]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 3]).Value2 != null)
                                Storage = ((Excel.Range)workSheet.Cells[1, 3]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 4]).Value2 != null)
                                Location = ((Excel.Range)workSheet.Cells[1, 4]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 5]).Value2 != null)
                                Part = ((Excel.Range)workSheet.Cells[1, 5]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 6]).Value2 != null)
                                Stock = ((Excel.Range)workSheet.Cells[1, 6]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 7]).Value2 != null)
                                Version = ((Excel.Range)workSheet.Cells[1, 7]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 8]).Value2 != null)
                                RMAN = ((Excel.Range)workSheet.Cells[1, 8]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 9]).Value2 != null)
                                Store_In_Date = ((Excel.Range)workSheet.Cells[1, 9]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[1, 10]).Value2 != null)
                                Qty = ((Excel.Range)workSheet.Cells[1, 10]).Value2.ToString().ToUpper();

                            #endregion

                            #region excelÁÐ±íÍ·Ð£Ñé

                            bool flag_number = true;
                            int number = 0;

                            for (int y = 0; y < workSheet.Columns.Count; y++)
                            {
                                if (!string.IsNullOrEmpty(((Excel.Range)workSheet.Cells[1, y + 1]).Value2.ToString().Trim()))
                                {
                                    number++;
                                }
                            }
                            if (number != 10)
                            {
                                #region Ð£ÑéÊÇ·ñÂú×ã10ÁÐ

                                flag_version = false;
                                MessageBox.Show("Excel version incorrect!(column number)", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw new Exception("Excel version incorrect!(column number)");

                                #endregion
                            }
                            else
                            {
                                #region ±È½ÏÀ¸Î»ÊÇ·ñÓëÄ£°åÒ»ÖÂ

                                if (!Client.Trim().Equals("Client", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!Plant.Trim().Equals("Plant", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!Storage.Trim().Equals("Storage", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!Location.Trim().Equals("Location", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!Part.Trim().Equals("Part No", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!Stock.Trim().Equals("Stock", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!Version.Trim().Equals("Version", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!RMAN.Trim().Equals("RMANO", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!Store_In_Date.Trim().Equals("Store In Date(For FIFO)", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;
                                if (!Qty.Trim().Equals("Qty", StringComparison.CurrentCultureIgnoreCase))
                                    flag_version = false;



                                if (!flag_version)
                                {
                                    MessageBox.Show("Excel version incorrect!(column name)", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    throw new Exception("Excel version incorrect!(column name)");
                                }
                                #endregion
                            }

                            #endregion

                            flag_excel = false;
                        }

                        #endregion

                        //if (((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString().ToUpper().Trim() != "CLIENT")
                        #region excelÐ´ÈëÊý¾Ý

                        if (flag_version)
                        {
                            #region ÁÙÊ±±íÊý¾Ý

                            string strTempMandt = "";
                            string strTempComcd = "";
                            string strTempWerks = "";
                            string strTempLgort = "";
                            string strTempLocat = "";
                            string strTempMatnr = "";
                            string strTempInsmk = "";
                            string strTempCharg = "";
                            string strTempIndat = "";
                            string strTempRmano = "";


                            if (((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)
                                strTempMandt = ((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null)
                                strTempWerks = ((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[rowIndex, 3]).Value2 != null)
                                strTempLgort = ((Excel.Range)workSheet.Cells[rowIndex, 3]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[rowIndex, 4]).Value2 != null)
                                strTempLocat = ((Excel.Range)workSheet.Cells[rowIndex, 4]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[rowIndex, 5]).Value2 != null)
                                strTempMatnr = ((Excel.Range)workSheet.Cells[rowIndex, 5]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[rowIndex, 6]).Value2 != null)
                                strTempInsmk = ((Excel.Range)workSheet.Cells[rowIndex, 6]).Value2.ToString().ToUpper();
                            if (((Excel.Range)workSheet.Cells[rowIndex, 7]).Value2 != null)
                                strTempCharg = ((Excel.Range)workSheet.Cells[rowIndex, 7]).Value2.ToString().ToUpper();

                            #endregion

                            #region ½«Êý¾ÝÌí¼Óµ½ÁÐ±í
                            ArrayList pk_row = new ArrayList();
                            pk_row.Add(strTempMandt);
                            pk_row.Add(strTempWerks);
                            pk_row.Add(strTempLgort);
                            pk_row.Add(strTempLocat);
                            pk_row.Add(strTempMatnr);
                            pk_row.Add(strTempInsmk);
                            pk_row.Add(strTempCharg);
                            pk_excel.Add(pk_row);

                            #endregion
                        }

                        index++;
                    }
                }

                        #endregion

                #region check pk

                ArrayList a1 = new ArrayList();//¬ö¿ý­«½Æ¦C½s¸¹
                ArrayList a1_all = new ArrayList();//¬ö¿ý­«½Æ¦C½s¸¹(¥þ³¡)
                bool flag1 = false;//§PÂ_³QÀË¬d¦C¬O§_­«½Æ
                bool flag_all = false;
                string message = "";

                for (int i = 0; i < pk_excel.Count; i++)
                {
                    bool flag_x = false;

                    if (a1_all.Count == 0)//¬°²Ä¤@¦C
                        flag_x = false;
                    else
                    {
                        for (int x = 0; x < a1_all.Count; x++)
                        {
                            if (Convert.ToInt32(a1_all[x]) == i)//¦¹¦C³Q§PÂ_¬°­«½Æ
                            {
                                flag_x = true;
                                break;
                            }
                            else
                                continue;
                        }
                    }

                    if (!flag_x)//¦¹¦C¥¼³Q§PÂ_¬°­«½Æ
                    {
                        #region ­«½Æ¦C§PÂ_

                        ArrayList arry_base = (ArrayList)pk_excel[i];//­n³QÀË¬dªº¦C
                        flag1 = false;

                        a1.Clear();
                        a1.Add(i);

                        for (int j = i + 1; j < pk_excel.Count; j++)
                        {
                            #region ³QÀË¬d¦C»P¨ä¥L¦C§@¤ñ¸û

                            ArrayList arry_compare = (ArrayList)pk_excel[j];
                            ArrayList a2 = new ArrayList();//¬ö¿ý¨CÄd¬O§_­«½Æ

                            for (int k = 0; k < arry_base.Count; k++)//¤ñ¹ï¶}©l
                            {
                                if (arry_base[k].ToString().Equals(arry_compare[k].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                    a2.Add(true);
                                else
                                    a2.Add(false);
                            }

                            //§PÂ_¬O§_¨C­Ókey³£¬Û¦P
                            bool flag2 = true;
                            for (int l = 0; l < a2.Count; l++)
                            {
                                flag2 = flag2 && (bool)a2[l];
                            }

                            if (flag2)//¦¹¦C­«½Æ,«h°O¿ý¦C¸¹
                            {
                                a1.Add(j);
                                flag1 = true;
                                flag_all = true;
                            }

                            #endregion
                        }

                        if (!flag1)//³QÀË¬d¦C¥¼­«½Æ             
                            continue;
                        else
                        {
                            //§ì¥X­«½Æ¦CªºÄæ¦ì¸ê°T
                            string temp = "";
                            for (int n = 0; n < arry_base.Count; n++)
                            {
                                switch (n)
                                {
                                    case 1:
                                        temp = temp + " Plant:" + arry_base[n];
                                        break;
                                    case 2:
                                        temp = temp + " Storage:" + arry_base[n];
                                        break;
                                    case 3:
                                        temp = temp + " Location:" + arry_base[n];
                                        break;
                                    case 4:
                                        temp = temp + " Part No:" + arry_base[n];
                                        break;
                                    case 5:
                                        temp = temp + " Stock:" + arry_base[n];
                                        break;
                                    case 6:
                                        temp = temp + " Version:" + arry_base[n];
                                        break;
                                }

                            }

                            //±N­«½Æ¦CarraylistÂà´«¦¨¤å¦r                                                       
                            for (int m = 0; m < a1.Count; m++)
                            {
                                if (m == 0)//²Ä¤@¦C                                                             
                                    message = message + "Excel record " + (Convert.ToInt32(a1[m]) + 1) + ",";
                                else
                                {
                                    if (!(m == a1.Count - 1))//«D³Ì«á¤@¦C                                       
                                        message = message + (Convert.ToInt32(a1[m]) + 1) + ",";
                                    else//³Ì«á¤@¦C                                                              
                                        message = message + (Convert.ToInt32(a1[m]) + 1) + " duplicate!(" + temp + ")" + "\n\n";
                                }

                                //¬ö¿ý­«½Æ¦C¸¹                                                                  
                                a1_all.Add(a1[m]);
                            }
                        }

                        #endregion
                    }
                    else
                        continue;
                }

                #endregion
                #endregion

                #endregion

                #region Ð£ÑéÀ¸Î»ÊÇ·ñÎª¿Õ£¬Client, Plant or Storage ÊÇ·ñÓëÑ¡ÔñµÄÊÇ·ñÒ»Ñù£¬Ð£ÑéÈÕÆÚÊÇ·ñÎª8Î»£¬µ¼Èëµ½WHEXCEL±í

                if (!flag_all)//³QÀË¬d¦C¥¼­«½Æ
                {
                    #region original code

                    index = 0;
                    rowIndex = 2 + index;

                    while (((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null && ((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null && ((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString().Trim() != "" && ((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2.ToString().Trim() != "")
                    {
                        rowIndex = 1 + index;
                        if (((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null && ((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2 != null && ((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString().Trim() != "" && ((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2.ToString().Trim() != "")
                        {
                            if (((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString().ToUpper().Trim() != "CLIENT")
                            {
                                string strTempMandt = "";
                                string strTempComcd = "";
                                string strTempWerks = "";
                                string strTempLgort = "";
                                string strTempLocat = "";
                                string strTempMatnr = "";
                                string strTempInsmk = "";
                                string strTempCharg = "";
                                string strTempIndat = "";
                                string strTempRmano = "";

                                try
                                {

                                    strTempMandt = ((Excel.Range)workSheet.Cells[rowIndex, 1]).Value2.ToString().ToUpper();
                                    strTempComcd = UserData.CompanyCode.ToString().Trim();
                                    strTempWerks = ((Excel.Range)workSheet.Cells[rowIndex, 2]).Value2.ToString().ToUpper();
                                    strTempLgort = ((Excel.Range)workSheet.Cells[rowIndex, 3]).Value2.ToString().ToUpper();
                                    strTempLocat = ((Excel.Range)workSheet.Cells[rowIndex, 4]).Value2.ToString().ToUpper();
                                    strTempMatnr = ((Excel.Range)workSheet.Cells[rowIndex, 5]).Value2.ToString().ToUpper();
                                    strTempInsmk = ((Excel.Range)workSheet.Cells[rowIndex, 6]).Value2.ToString().ToUpper();
                                    if (((Excel.Range)workSheet.Cells[rowIndex, 7]).Value2 != null)
                                    {
                                        strTempCharg = ((Excel.Range)workSheet.Cells[rowIndex, 7]).Value2.ToString().ToUpper();
                                    }
                                    if (((Excel.Range)workSheet.Cells[rowIndex, 8]).Value2 != null)
                                    {
                                        strTempRmano = ((Excel.Range)workSheet.Cells[rowIndex, 8]).Value2.ToString().ToUpper();
                                    }
                                    //strTempIndat = ((Excel.Range)workSheet.Cells[rowIndex, 8]).Value2.ToString().ToUpper();
                                    strTempIndat = ((Excel.Range)workSheet.Cells[rowIndex, 9]).Value2.ToString().ToUpper();

                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Data of line " + rowIndex + "(" + strTempLocat + "," + strTempMatnr + ") is empty!");
                                }

                                if (strTempMandt != Mandt || strTempComcd != Comcd || strTempWerks != cmbWerks.Items[cmbWerks.SelectedIndex].ToString() || strTempLgort != cmbLgort.Items[cmbLgort.SelectedIndex].ToString())
                                {
                                    throw new Exception("Client, Plant or Storage in line " + rowIndex + "(" + strTempLocat + "," + strTempMatnr + ") is different the data you select!");
                                }

                                //ÀË¬d¤é´Á®æ¦¡
                                if (strTempIndat.Length != 8)
                                {
                                    throw new Exception("Date of line " + rowIndex + "(" + strTempLocat + "," + strTempMatnr + ") should be YYYYMMDD format!");
                                }
                                try
                                {
                                    //ÀË¬d¤é´Á
                                    DateTime dtTempDate = Convert.ToDateTime(strTempIndat.Substring(0, 4) + "/" + strTempIndat.Substring(4, 2) + "/" + strTempIndat.Substring(6, 2));

                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Date of line " + rowIndex + "(" + strTempLocat + "," + strTempMatnr + ") has problem!");
                                }
                                Int64 intTempMenge = 0;
                                Int64 intTempMenge1 = 0;
                                Int64 intTempMenge2 = 0;
                                Int64 intTempMenge3 = 0;
                                try
                                {
                                    //intTempMenge = Int64.Parse(((Excel.Range)workSheet.Cells[rowIndex, 9]).Value2.ToString());
                                    intTempMenge = Int64.Parse(((Excel.Range)workSheet.Cells[rowIndex, 10]).Value2.ToString());
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Date of line " + rowIndex + "(" + strTempLocat + "," + strTempMatnr + ") should be numeric format!");
                                }

                                objWhexcel.Mandt = UserData.Client;
                                objWhexcel.Comcd = UserData.CompanyCode;
                                objWhexcel.Werks = strTempWerks;
                                objWhexcel.Lgort = strTempLgort;
                                objWhexcel.Locat = strTempLocat;
                                objWhexcel.Matnr = strTempMatnr;
                                objWhexcel.Insmk = strTempInsmk;
                                objWhexcel.Charg = strTempCharg;
                                objWhexcel.Rmano = strTempRmano;
                                objWhexcel.Indat = strTempIndat;
                                objWhexcel.Menge = intTempMenge.ToString();
                                objWhexcel.Remak = "";
                                objWhexcel.Crnam = UserData.UserId;
                                objWhexcel.Crdat = "getdate()";

                                arySQL.Add(objWhexcel.EntityGetInsertSql());
                            }
                        }
                        index++;
                    }

                    #endregion
                }
                else
                {
                    //show¥X°T®§                  
                    MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception(message);
                }
                #endregion

                #region ½áÊøexcel½ø³Ì
                Process[] myProcesses;
                DateTime startTime;
                myProcesses = Process.GetProcessesByName("Excel");

                foreach (Process myProcess in myProcesses)
                {
                    startTime = myProcess.StartTime;

                    if (startTime > beforeTime && startTime < afterTime)
                    {
                        myProcess.Kill();
                    }
                }


                app = null;
                workBook = null;
                workSheet = null;
                #endregion
            }
            catch (Exception ex)
            {
                #region ³öÏÖÒì³££¬½áÊø½ø³Ì,²¢Å×³öÒì³£
                Process[] myProcesses;
                DateTime startTime;
                myProcesses = Process.GetProcessesByName("Excel");
                foreach (Process myProcess in myProcesses)
                {
                    startTime = myProcess.StartTime;

                    if (startTime > beforeTime && startTime < afterTime)
                    {
                        myProcess.Kill();
                    }
                }

                app = null;
                workBook = null;
                workSheet = null;
                throw new Exception(ex.Message + "<-ParseImportFiles()");
                #endregion
            }

        }

        private void btnExecute_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string message = "All inventory of the storage will be deleted. Are you sure?";
                string caption = "Warning";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(this, message, caption, buttons);
                #region 判断厂区仓别是否有库存，有库存不允许导入
                StorageData objStorage = new StorageData(UserData);
                if (objStorage.QueryStorageCount(strWerks, strLgort) && strLgort.Substring(0, 2).ToString() != "FG" && strLgort.Substring(0, 2).ToString() != "FJ" && strLgort.Substring(0, 2).ToString() != "RF" && strLgort.Substring(0, 3).ToString() != "PAP" && !(strWerks == "CS31" && (strLgort.Substring(0, 2).ToString() == "SC" || strLgort.Substring(0, 2).ToString() == "RW" || strLgort.Substring(0, 2).ToString() == "BN" )))
                {
                    stsWarning.Text = "手动转库存只针对无库存的仓别！";
                    return;
                }
                #endregion
                if (result == DialogResult.Yes)
                {
                    try
                    {

                        if (objStorageData.ImportStorageData(strWerks, strLgort))
                        {
                            stsWarning.Text = "Execute OK!";
                            return;
                        }
                        else
                        {
                            stsWarning.Text = "Execute Fail!";
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + "<-btnExecute_Click()");
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                stsWarning.Text = ex.Message;
                return;
            }


        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            this.stsWarning.Text = "";
            this.txtFilePath.Text = "";
            this.panel1.Enabled = true;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.btnImport.Enabled = true;
            this.btnExecute.Enabled = false;
            this.btnImport.Enabled = true;
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Manage_ImportInventory_Resize(object sender, System.EventArgs e)
        {
            panel5.Size = new System.Drawing.Size((int)(this.Size.Width * 0.25), panel8.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }

        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Plant\tStorage\tPart No\tStock\tVersion\tSAP Qty\tQWMS Qty\tSAP-QWMS";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                    strLine += dtData.Rows[i]["SAP_MENGE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["QWMS_MENGE"].ToString() + "\t";
                    strLine += dtData.Rows[i]["KTMNG"].ToString();
                    sw.WriteLine(strLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }

        }

        private void lnkSample_Click(object sender, System.EventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "手动转入库存模板.xlsx";
                try
                {
                    Process.Start("Excel", strPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"无法打开文件，请手动打开" + strPath);
                }
            }
            else
            {
                MessageBox.Show("未在数据库维护模板路径，请联系QWMS负责人");
            }
        }

        private void btnInvImport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();

            try
            {
                StorageData objStorage = new StorageData(UserData);
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "厂区和仓别不能为空！";
                    return;
                }
                if (this.txtFilePath.Text.Trim() == "")
                {
                    stsWarning.Text = "Please select one file!!";
                    return;
                }
                if (chkInventory.Checked == true)
                {
                    # region 校验格式
                    string strFileName = this.txtFilePath.Text;
                    string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                    if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                    {
                        lblWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                        return;
                    }
                    # endregion

                    #region 获取Excel数据
                    ClaExeclHelper objExcel = new ClaExeclHelper();
                    try
                    {
                        dtData = objExcel.GetDataTableFromExcel(strFileName, true);
                        if (dtData.Rows.Count <= 0)
                        {
                            stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                            return;
                        }
                        else
                        {
                            ArrayList arrCheck = new ArrayList();
                            DataRow[] drSelect = dtData.Select(" Plant = '" + strWerks + "' AND Storage = '" + strLgort + "' ");
                            if (drSelect.Length != dtData.Rows.Count)
                            {
                                stsWarning.Text = "导入的数据中厂区仓别与选择的不一致，请确认！！";
                                return;
                            }
                            else
                            {
                                #region 判断厂区仓别是否有库存，有库存不允许导入
                                if (objStorage.QueryStorageCount(strWerks, strLgort) && strLgort.Substring(0, 2).ToString() != "FG" && strLgort.Substring(0, 2).ToString() != "FJ" && strLgort.Substring(0, 2).ToString() != "RF" && strLgort.Substring(0, 3).ToString() != "PAP")
                                {
                                    stsWarning.Text = "手动转库存只针对无库存的仓别！";
                                    return;
                                }
                                #endregion
                            }              
                        }
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = "读取文档失败" + ex.Message;
                        return;
                    }
                    #endregion

                    # region 不用

                    //OleDbConnection oleConn = null;
                    //DataSet dsExcel = new DataSet();
                    //try
                    //{
                    //    string strConn = "";
                    //    if (strFileType.ToUpper() == "XLS")
                    //    {
                    //        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName + ";Extended Properties=Excel 8.0";
                    //    }
                    //    else if (strFileType.ToUpper() == "XLSX")
                    //    {

                    //        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";" + "Extended Properties='Excel 12.0'";
                    //    }

                    //    oleConn = new OleDbConnection(strConn);

                    //    oleConn.Open();
                    //    StringBuilder strSQL = new StringBuilder();
                    //    strSQL.Append(" select * from [Sheet1$] ");
                    //    OleDbDataAdapter oleAdapter = new OleDbDataAdapter(strSQL.ToString(), oleConn);

                    //    oleAdapter.Fill(dsExcel, "Assets");
                    //    dtData = dsExcel.Tables["Assets"];
                    //    if (dtData.Rows.Count <= 0)
                    //    {
                    //        stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据且sheet 名字是否为sheet1";
                    //        return;
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    stsWarning.Text = "读取文档失败" + ex.Message;
                    //    return;
                    //}
                    //finally
                    //{
                    //    oleConn.Close();
                    //    if (null != oleConn)
                    //    {
                    //        oleConn.Dispose();
                    //        oleConn = null;
                    //    }
                    //}
                    # endregion

                    # region 导入数据
                  
                    bool flg = true;
                    try
                    {
                        flg = objStorage.ImportInventoryData(dtData, strWerks, strLgort);
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = "导入失败，请确认EXCEL格式是否正确";
                        return;
                    }
                    if (flg)
                    {
                        stsWarning.Text = "导入成功！";
                        dtData = objStorage.QueryInventoryData(strWerks, strLgort);
                        ShowDataGrid();
                    }
                    else
                    {
                        stsWarning.Text = "导入失败";
                    }

                    #endregion

                }
                else
                {
                    stsWarning.Text = "该操作只适用盘底票批量导入";
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        public DataTable GetExcelData()
        {
            DataTable dtTmpe = new DataTable();
            try
            {
                # region 校验格式
                string strFileName = this.txtFilePath.Text;
                string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                {
                    lblWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                    return dtTmpe;
                }
                # endregion

                # region 获取EXCEL数据
                ClaExeclHelper objExcel = new ClaExeclHelper();
                try
                {
                    dtTmpe = objExcel.GetDataTableFromExcel(strFileName, true);
                    if (dtTmpe.Rows.Count <= 0)
                    {
                        stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                        return dtTmpe;
                    }
                    else
                    {
                        #region 去重
                        for (int i = dtTmpe.Rows.Count - 1; i >= 0; i--)
                        {
                            if (string.IsNullOrEmpty(dtTmpe.Rows[i][0].ToString()) && string.IsNullOrEmpty(dtTmpe.Rows[i][1].ToString()) && string.IsNullOrEmpty(dtTmpe.Rows[i][2].ToString()) &&
                                string.IsNullOrEmpty(dtTmpe.Rows[i][3].ToString()) && string.IsNullOrEmpty(dtTmpe.Rows[i][4].ToString()) && string.IsNullOrEmpty(dtTmpe.Rows[i][5].ToString()))
                            {
                                dtTmpe.Rows.Remove(dtTmpe.Rows[i]);
                            }
                        }
                        #endregion
                    }

                }
                catch (Exception ex)
                {
                    stsWarning.Text = "读取文档失败" + ex.Message;
                    return dtTmpe;
                }

                # endregion

                #region 重置表名
                dtTmpe.Columns["Client"].ColumnName = "MANDT";
                dtTmpe.Columns["Plant"].ColumnName = "WERKS";
                dtTmpe.Columns["Storage"].ColumnName = "LGORT";
                dtTmpe.Columns["location"].ColumnName = "LOCAT";
                dtTmpe.Columns["Part No"].ColumnName = "MATNR";
                dtTmpe.Columns["Stock"].ColumnName = "INSMK";
                dtTmpe.Columns["Version"].ColumnName = "CHARG";
                dtTmpe.Columns["Store In Date(For FIFO)"].ColumnName = "INDAT";
                dtTmpe.Columns["Qty"].ColumnName = "MENGE";
                dtTmpe.Columns["Vendor"].ColumnName = "LIFNR";
                dtTmpe.Columns["DateCode"].ColumnName = "DACOD";
                dtTmpe.Columns["Vendor Manufacture Date"].ColumnName = "VEDAT";
                dtTmpe.Columns["LockCode"].ColumnName = "LOCOD";


                dtTmpe.Columns.Add("CRDAT");
                dtTmpe.Columns.Add("REMARK");
                dtTmpe.Columns.Add("COMCD");
                dtTmpe.Columns.Add("CRNAM");
                #endregion

            }
            catch (Exception e)
            {
                MessageBox.Show("导入excel异常，请确认！！" + e.ToString());
                return dtTmpe;
            }
            return dtTmpe;
        }

        #region OLEDB方式读取文档  不用
        //public DataTable GetExcelData()
        //{
        //    DataTable dtTmpe = new DataTable();
        //    try
        //    {
        //        # region ±£´æÎÄ¼þ
        //        string strFileName = this.txtFilePath.Text;
        //        string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

        //        if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
        //        {
        //            lblWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
        //            return dtTmpe;
        //        }
        //        # endregion

        //        # region ¶ÁÈ¡ÎÄ¼þÄÚÈÝ

        //        OleDbConnection oleConn = null;
        //        DataSet dsExcel = new DataSet();
        //        try
        //        {
        //            string strConn = "";
        //            if (strFileType.ToUpper() == "XLS")
        //            {
        //                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName + ";Extended Properties=Excel 8.0";
        //            }
        //            else if (strFileType.ToUpper() == "XLSX")
        //            {

        //                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";" + "Extended Properties='Excel 12.0'";
        //            }

        //            oleConn = new OleDbConnection(strConn);

        //            oleConn.Open();
        //            StringBuilder strSQL = new StringBuilder();
        //            strSQL.Append(" select * from [Sheet1$] ");
        //            OleDbDataAdapter oleAdapter = new OleDbDataAdapter(strSQL.ToString(), oleConn);

        //            oleAdapter.Fill(dsExcel, "Assets");
        //            dtTmpe = dsExcel.Tables["Assets"];
        //            if (dtTmpe.Rows.Count <= 0)
        //            {
        //                stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据且sheet 名字是否为sheet1";
        //                return dtTmpe;
        //            }
        //            else
        //            {
        //                #region »ñÈ¡EXCEL µÄÊ±ºò»á°Ñ É¾³ýµôµÄ¿ÕÐÐ¶¼»ñÈ¡µ½£¬Õâ±ßÒªÏÈÈ¥³ý¿ÕÐÐ
        //                for (int i = dtTmpe.Rows.Count - 1; i >= 0; i--)
        //                {
        //                    if (string.IsNullOrEmpty(dtTmpe.Rows[i][0].ToString()) && string.IsNullOrEmpty(dtTmpe.Rows[i][1].ToString()) && string.IsNullOrEmpty(dtTmpe.Rows[i][2].ToString()) &&
        //                        string.IsNullOrEmpty(dtTmpe.Rows[i][3].ToString()) && string.IsNullOrEmpty(dtTmpe.Rows[i][4].ToString()) && string.IsNullOrEmpty(dtTmpe.Rows[i][5].ToString()))
        //                    {
        //                        dtTmpe.Rows.Remove(dtTmpe.Rows[i]);
        //                    }
        //                }
        //                #endregion
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            stsWarning.Text = "读取文档失败" + ex.Message;
        //            return dtTmpe;
        //        }
        //        finally
        //        {
        //            oleConn.Close();
        //            if (null != oleConn)
        //            {
        //                oleConn.Dispose();
        //                oleConn = null;
        //            }
        //        }

        //        # endregion

        //        #region ÖØÐÂÕûÀíDataTable
        //        dtTmpe.Columns["Client"].ColumnName = "MANDT";
        //        dtTmpe.Columns["Plant"].ColumnName = "WERKS";
        //        dtTmpe.Columns["Storage"].ColumnName = "LGORT";
        //        dtTmpe.Columns["location"].ColumnName = "LOCAT";
        //        dtTmpe.Columns["Part No"].ColumnName = "MATNR";
        //        dtTmpe.Columns["Stock"].ColumnName = "INSMK";
        //        dtTmpe.Columns["Version"].ColumnName = "CHARG";
        //        dtTmpe.Columns["Store In Date(For FIFO)"].ColumnName = "INDAT";
        //        dtTmpe.Columns["Qty"].ColumnName = "MENGE";
        //        dtTmpe.Columns["Vendor"].ColumnName = "LIFNR";
        //        dtTmpe.Columns["DateCode"].ColumnName = "DACOD";
        //        dtTmpe.Columns["Vendor Manufacture Date"].ColumnName = "VEDAT";
        //        dtTmpe.Columns["LockCode"].ColumnName = "LOCOD";


        //        dtTmpe.Columns.Add("CRDAT");
        //        dtTmpe.Columns.Add("REMARK");
        //        dtTmpe.Columns.Add("COMCD");
        //        dtTmpe.Columns.Add("CRNAM");
        //        // dtTmpe.Columns.Add("ITEM");


        //        #endregion

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("导入excel异常，请确认！！" + e.ToString());
        //        return dtTmpe;
        //    }
        //    return dtTmpe;


        //}
        #endregion

    }
}
