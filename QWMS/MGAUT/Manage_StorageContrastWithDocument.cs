using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using QCI.QWMS;
using QWMS.Common;
using Qci.Base.Common;
using QWMS.Entity;


namespace QWMS
{
	/// <summary>
	/// Manage_StorageContrastWithDocument 的摘要描述。
	/// </summary>
	public class Manage_StorageContrastWithDocument : System.Windows.Forms.Form
	{
        UserInfo UserData = new UserInfo();
		protected internal System.Windows.Forms.StatusBar stbStatus;
		private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
		private System.Windows.Forms.StatusBarPanel stsUsrnm;
		private System.Windows.Forms.StatusBarPanel stsWarning;
		private System.Windows.Forms.StatusBarPanel stsDate;
		private System.Windows.Forms.OpenFileDialog ofdOpenFile;
		private System.Windows.Forms.SaveFileDialog sfdSaveFile;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnDownload;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.Button btnFile;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbLgort;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.ComboBox cmbWerks;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkDate;
		private System.Windows.Forms.DateTimePicker dtpStartDate;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.DateTimePicker dtpEndDate;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.ComboBox cmbType;
		private System.Windows.Forms.Label label10;
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private SapData objSapData;
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
		private System.Windows.Forms.ComboBox cmbMType;
		private System.Windows.Forms.Label label5;
        private DataGridView dgvData;
        private Label lblStorage;
		private StreamReader srFileReader;

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

		public Manage_StorageContrastWithDocument(UserInfo _UserData, string strProgid)
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

				//檢查權限
				if(!objStorageIn.CheckAuthority("MANAGE"))
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					//秀出Status的資料
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
					cmbMType.SelectedIndex = 0;
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
					if(cmbLgort.Items.Count > 0)
					{
						this.cmbLgort.SelectedIndex = 0;
					}
					this.cmbType.SelectedIndex = 0;
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form 設計工具產生的程式碼
		/// <summary>
		/// 此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cmbMType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStorage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 451);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(692, 22);
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
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 410;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "InventoryComparsionWithDocument.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblStorage);
            this.panel2.Controls.Add(this.btnDownload);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnExecute);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 473);
            this.panel2.TabIndex = 35;
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(264, 416);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(80, 32);
            this.btnDownload.TabIndex = 45;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(432, 416);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 31);
            this.btnExit.TabIndex = 27;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(24, 416);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 32);
            this.btnImport.TabIndex = 24;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.Enabled = false;
            this.btnPrint.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(184, 416);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 32);
            this.btnPrint.TabIndex = 29;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecute.Enabled = false;
            this.btnExecute.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Location = new System.Drawing.Point(104, 416);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 32);
            this.btnExecute.TabIndex = 28;
            this.btnExecute.Text = "Execute";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(352, 416);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 31);
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
            this.dgvData.Location = new System.Drawing.Point(24, 132);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(640, 268);
            this.dgvData.TabIndex = 46;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(692, 104);
            this.panel1.TabIndex = 34;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtFilePath);
            this.panel4.Controls.Add(this.btnFile);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 72);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(692, 32);
            this.panel4.TabIndex = 15;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(134, 0);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(440, 21);
            this.txtFilePath.TabIndex = 10;
            // 
            // btnFile
            // 
            this.btnFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFile.Location = new System.Drawing.Point(576, 0);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(24, 23);
            this.btnFile.TabIndex = 11;
            this.btnFile.Text = "...";
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(62, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "File Path";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(692, 72);
            this.panel3.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dtpEndDate);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.cmbLgort);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(256, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(236, 72);
            this.panel6.TabIndex = 15;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(72, 40);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(112, 21);
            this.dtpEndDate.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(24, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 23);
            this.label4.TabIndex = 57;
            this.label4.Text = "To";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 12;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(72, 8);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(112, 23);
            this.cmbLgort.TabIndex = 13;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cmbMType);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.cmbType);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(492, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 72);
            this.panel7.TabIndex = 16;
            // 
            // cmbMType
            // 
            this.cmbMType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMType.Items.AddRange(new object[] {
            "SAP",
            "QMS",
            "SDS"});
            this.cmbMType.Location = new System.Drawing.Point(72, 40);
            this.cmbMType.Name = "cmbMType";
            this.cmbMType.Size = new System.Drawing.Size(112, 23);
            this.cmbMType.TabIndex = 51;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Location = new System.Drawing.Point(16, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 23);
            this.label5.TabIndex = 50;
            this.label5.Text = "Type";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Items.AddRange(new object[] {
            "Not Finished"});
            this.cmbType.Location = new System.Drawing.Point(72, 8);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(112, 23);
            this.cmbType.TabIndex = 49;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.Location = new System.Drawing.Point(16, 8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 23);
            this.label10.TabIndex = 48;
            this.label10.Text = "Status";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.chkDate);
            this.panel5.Controls.Add(this.dtpStartDate);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.cmbWerks);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(256, 72);
            this.panel5.TabIndex = 14;
            // 
            // chkDate
            // 
            this.chkDate.Location = new System.Drawing.Point(20, 40);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(16, 24);
            this.chkDate.TabIndex = 62;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(132, 40);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(108, 21);
            this.dtpStartDate.TabIndex = 59;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.Location = new System.Drawing.Point(28, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 23);
            this.label9.TabIndex = 61;
            this.label9.Text = "Download From";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(131, 8);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(109, 23);
            this.cmbWerks.TabIndex = 7;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(56, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStorage
            // 
            this.lblStorage.AutoSize = true;
            this.lblStorage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStorage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorage.Location = new System.Drawing.Point(25, 111);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(64, 18);
            this.lblStorage.TabIndex = 49;
            this.lblStorage.Text = "0 records";
            // 
            // Manage_StorageContrastWithDocument
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.ClientSize = new System.Drawing.Size(692, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.panel2);
            this.Name = "Manage_StorageContrastWithDocument";
            this.Text = "Inventory Comparison With Material Document";
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

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
				for(int i=0;i<dtTemp.Rows.Count;i++)
				{
					cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
				}
			}
			catch(Exception ex)
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
				if(cmbWerks.SelectedIndex != -1)
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
				if(cmbLgort.SelectedIndex != -1)
				{
					strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
				}
				else
				{
					cmbLgort.Items.Clear();
				}
				
				if(dtTemp.Rows.Count == 0)
				{
					cmbLgort.Items.Clear();
					strLgort = "";
				}
				else
				{
					cmbLgort.Items.Clear();
					for(int i=0;i<dtTemp.Rows.Count;i++)
					{
						cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
						if(dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
						{
							cmbLgort.SelectedIndex = i;
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlLgort()");
			}
		}

		//		private void ShowDdlLgort()
		//		{
		//			string strWerks = "";
		//			try
		//			{
		//				DataTable dtTemp = new DataTable();
		//				cmbLgort.Items.Clear();
		//				if(cmbWerks.SelectedIndex != -1)
		//				{
		//					strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
		//					dtTemp = objPlantData.GetDdlLgortData(strWerks);
		//				}
		//				else
		//				{
		//					dtTemp = objPlantData.GetDdlLgortData();
		//				}
		//				for(int i=0;i<dtTemp.Rows.Count;i++)
		//				{
		//					cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
		//				}
		//				
		//			}
		//			catch(Exception ex)
		//			{
		//				throw new Exception(ex.Message + "<-ShowDdlLgort()");
		//			}
		//		}

		private void ShowDataGrid()
		{
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
			//dgvData.TableStyles.Clear();
			try
			{
				//DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
				//mydtgTableStyle.MappingName = dtData.TableName;

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

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "SAP_MENGE";
				mengeStyle.HeaderText = "SAP Qty";
				mengeStyle.ReadOnly = true;
				dgvData.Columns.Add(mengeStyle);

                DataGridViewTextBoxColumn lifnrStyle = new DataGridViewTextBoxColumn();
                lifnrStyle.DataPropertyName = "QWMS_MENGE";
				lifnrStyle.HeaderText = "QWMS Qty";
				lifnrStyle.ReadOnly = true;
				dgvData.Columns.Add(lifnrStyle);

                DataGridViewTextBoxColumn indatStyle = new DataGridViewTextBoxColumn();
                indatStyle.DataPropertyName = "KTMNG";
				indatStyle.HeaderText = "SAP - QWMS";
				indatStyle.ReadOnly = true;
				dgvData.Columns.Add(indatStyle);

                DataGridViewTextBoxColumn sapgrStyle = new DataGridViewTextBoxColumn();
                sapgrStyle.DataPropertyName = "SAP_GR";
				sapgrStyle.HeaderText = "G/R";
				sapgrStyle.ReadOnly = true;
				dgvData.Columns.Add(sapgrStyle);

                DataGridViewTextBoxColumn sapgiStyle = new DataGridViewTextBoxColumn();
                sapgiStyle.DataPropertyName = "SAP_GI";
				sapgiStyle.HeaderText = "G/I";
				sapgiStyle.ReadOnly = true;
				dgvData.Columns.Add(sapgiStyle);

                DataGridViewTextBoxColumn sapbalanceStyle = new DataGridViewTextBoxColumn();
                sapbalanceStyle.DataPropertyName = "SAP_BALANCE";
				sapbalanceStyle.HeaderText = "G/R - G/I";
				sapbalanceStyle.ReadOnly = true;
				dgvData.Columns.Add(sapbalanceStyle);

                dgvData.DataSource = dtData;
                lblStorage.Text = dtData.Rows.Count + "Records";
                //dtgData.DataSource = dtData;	
                //dtgData.TableStyles.Add(mydtgTableStyle);

                //dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
			}
		}


		private void btnFile_Click(object sender, System.EventArgs e)
		{
			string strWerks = "";
			string strLgort = "";
			stsWarning.Text = "";
			if(cmbWerks.SelectedIndex != -1)
			{
				strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
			}
			if(cmbLgort.SelectedIndex != -1)
			{
				strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
			}
			if(strWerks == "" || strLgort == "")
			{
				stsWarning.Text = "Plant and storage can't be empty!!";
				return;
			}
			if(ofdOpenFile.ShowDialog() == DialogResult.OK)
			{
				this.txtFilePath.Text = ofdOpenFile.FileName;
			}
		}

		private void btnImport_Click(object sender, System.EventArgs e)
		{
			string strMType="";
			stsWarning.Text = "";
			try
			{
				
				if(this.txtFilePath.Text.Trim() == "")
				{
					stsWarning.Text = "Please select one file!!";
					return;
				}

				if(cmbMType.SelectedIndex==-1)
				{
					stsWarning.Text = "MType can't be empty!!";
					return;				
				}
				else
				{
					strMType = cmbMType.Items[cmbMType.SelectedIndex].ToString();
				}

				ProcessContrastFiles(this.txtFilePath.Text.Trim());
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
		}

		public void ProcessContrastFiles(string strContrastFilePath)
		{
			ArrayList alExecuteSQL = new ArrayList();
			bool bolTransaction = false;
			
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				//Parse file
				ParseContrastFiles(strContrastFilePath);
                alExecuteSQL.Clear();
				alExecuteSQL = arySQL;
			}
			catch(Exception ex)
			{
				Cursor.Current = Cursors.Default;
				throw new Exception(ex.Message + "<-ProcessContrastFiles()");
			}

			//Execute SQL
			if(alExecuteSQL.Count > 0)
			{
                try
                {

                    QCI.QWMS.StorageData objWhcst = new StorageData(UserData);
                    bolTransaction = objWhcst.ExeSqlAry(alExecuteSQL);

                    if (bolTransaction)
                    {
                        Cursor.Current = Cursors.Default;
                        stsWarning.Text = "Import OK!";
                        this.panel1.Enabled = false;
                        this.btnImport.Enabled = false;
                        this.btnExecute.Enabled = true;
                    }
                }
                catch (CommonObjectsException ex)
                {
                    string aaa = ex.Message;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "<-ProcessContrastFiles()");
                }
			}
		}

		public void ParseContrastFiles(string varFileName)
		{
			
			string strFileLine = "";
			int intLineNum = 0;
			string[] aryData;

			//File Data
			string strWerks = "";
			string strLgort = "";
			string strMatnr = "";
            string strComcd = "";
			string strCharg = "";
			string strMenge_0 = "";			
			string strMenge_G = "";
			string strMenge_J = "";
			string strMenge_S = "";

			try
			{
                arySQL.Clear();
                DataWhcst objWhcst = new DataWhcst(UserData);
                ArrayList alDeleteCondition = new ArrayList();

				srFileReader = File.OpenText(varFileName);
				while(srFileReader.Peek() != -1)
				{
                    
					intLineNum ++;
					strFileLine = srFileReader.ReadLine();

					if(strFileLine.Length > 0)
					{
						aryData = strFileLine.Split(new char[]{','});
						strMandt = aryData[0].Trim();
                        strComcd = UserData.CompanyCode.ToString();
						strWerks = aryData[1].Trim();
						strLgort = aryData[2].Trim();
						strMatnr = aryData[3].Trim();
						strMenge_0 = aryData[4].Trim();
						strMenge_G = aryData[5].Trim();
						strMenge_J = aryData[6].Trim();
						strMenge_S = aryData[7].Trim();
						strCharg = aryData[8].Trim();

						if(intLineNum == 1)
						{
							//檢查Mandt, Werks, Lgort是不是正確
							if(strMandt != UserData.Client || strComcd != UserData.CompanyCode || strWerks != cmbWerks.Items[cmbWerks.SelectedIndex].ToString() || strLgort != cmbLgort.Items[cmbLgort.SelectedIndex].ToString())
							{
								stsWarning.Text = "The plant and storage in the file are not the samas your choice!!";
								return;
							}

                            alDeleteCondition.Clear();

                            alDeleteCondition.Add("MANDT = '" + UserData.Client + "'");
                            alDeleteCondition.Add("COMCD = '" + UserData.CompanyCode + "'");
                            alDeleteCondition.Add("WERKS = '" + strWerks + "'");
                            alDeleteCondition.Add("LGORT = '" + strLgort + "'");

                            arySQL.Add(objWhcst.EntityGetDeleteSql(alDeleteCondition));

							//arySQL.Add("delete from WHCST where MANDT='"+ Mandt +"' and WERKS='"+ strWerks +"' and LGORT='"+ strLgort +"'");
						}

						//新增WHCST--0庫
						if(strMenge_0 != "0")
						{
                           
                            objWhcst.Mandt = UserData.Client;
                            objWhcst.Comcd = UserData.CompanyCode;
                            objWhcst.Werks = strWerks;
                            objWhcst.Lgort = strLgort;
                            objWhcst.Matnr = strMatnr;
                            objWhcst.Insmk = "0";
                            objWhcst.Charg = strCharg;
                            objWhcst.Menge = strMenge_0;
                            objWhcst.Crnam = UserData.UserId;
                            objWhcst.Crdat = "getdate()";

                            arySQL.Add(objWhcst.EntityGetInsertSql());

                            //arySQL.Add("insert into WHCST(MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, MENGE, CRNAM, CRDAT) values(" + 
                            //    "'"+ Mandt +"', " +
                            //    "'"+ strWerks +"', " + 
                            //    "'"+ strLgort +"', " + 
                            //    "'"+ strMatnr +"', " + 
                            //    "'"+ '0' +"', " + 
                            //    "'"+ strCharg +"', " + 
                            //    "'"+ strMenge_0 +"', " + 
                            //    "'"+ Usrnm +"', " + 
                            //    "getdate())");
						}
						//新增WHCST--G庫
						if(strMenge_G != "0")
						{

                            objWhcst.Mandt = UserData.Client;
                            objWhcst.Comcd = UserData.CompanyCode;
                            objWhcst.Werks = strWerks;
                            objWhcst.Lgort = strLgort;
                            objWhcst.Matnr = strMatnr;
                            objWhcst.Insmk = "G";
                            objWhcst.Charg = strCharg;
                            objWhcst.Menge = strMenge_G;
                            objWhcst.Crnam = UserData.UserId;
                            objWhcst.Crdat = "getdate()";

                            arySQL.Add(objWhcst.EntityGetInsertSql());

                            //arySQL.Add("insert into WHCST(MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, MENGE, CRNAM, CRDAT) values(" + 
                            //    "'"+ Mandt +"', " +
                            //    "'"+ strWerks +"', " + 
                            //    "'"+ strLgort +"', " + 
                            //    "'"+ strMatnr +"', " + 
                            //    "'"+ 'G' +"', " + 
                            //    "'"+ strCharg +"', " + 
                            //    "'"+ strMenge_G +"', " + 
                            //    "'"+ Usrnm +"', " + 
                            //    "getdate())");
						}
						//新增WHCST--J庫
						if(strMenge_J != "0")
						{
                            objWhcst.Mandt = UserData.Client;
                            objWhcst.Comcd = UserData.CompanyCode;
                            objWhcst.Werks = strWerks;
                            objWhcst.Lgort = strLgort;
                            objWhcst.Matnr = strMatnr;
                            objWhcst.Insmk = "J";
                            objWhcst.Charg = strCharg;
                            objWhcst.Menge = strMenge_J;
                            objWhcst.Crnam = UserData.UserId;
                            objWhcst.Crdat = "getdate()";

                            arySQL.Add(objWhcst.EntityGetInsertSql());

                            //arySQL.Add("insert into WHCST(MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, MENGE, CRNAM, CRDAT) values(" + 
                            //    "'"+ Mandt +"', " +
                            //    "'"+ strWerks +"', " + 
                            //    "'"+ strLgort +"', " + 
                            //    "'"+ strMatnr +"', " + 
                            //    "'"+ 'J' +"', " + 
                            //    "'"+ strCharg +"', " + 
                            //    "'"+ strMenge_J +"', " + 
                            //    "'"+ Usrnm +"', " + 
                            //    "getdate())");
						}
						//新增WHCST--S庫
						if(strMenge_S != "0")
						{
                            objWhcst.Mandt = Mandt;
                            objWhcst.Comcd = strComcd;
                            objWhcst.Werks = strWerks;
                            objWhcst.Lgort = strLgort;
                            objWhcst.Matnr = strMatnr;
                            objWhcst.Insmk = "S";
                            objWhcst.Charg = strCharg;
                            objWhcst.Menge = strMenge_S;
                            objWhcst.Crnam = UserData.UserId;
                            objWhcst.Crdat = "getdate()";

                            arySQL.Add(objWhcst.EntityGetInsertSql());

                            //arySQL.Add("insert into WHCST(MANDT, WERKS, LGORT, MATNR, INSMK, CHARG, MENGE, CRNAM, CRDAT) values(" + 
                            //    "'"+ Mandt +"', " +
                            //    "'"+ strWerks +"', " + 
                            //    "'"+ strLgort +"', " + 
                            //    "'"+ strMatnr +"', " + 
                            //    "'"+ 'S' +"', " + 
                            //    "'"+ strCharg +"', " + 
                            //    "'"+ strMenge_S +"', " + 
                            //    "'"+ Usrnm +"', " + 
                            //    "getdate())");		
						}
					}				
				}
				srFileReader.Close();
			}
			catch(Exception ex)
			{
				//Move to error folder
				srFileReader.Close();
				throw new Exception(ex.Message + "<-ParseContrastFiles()");
			}
			
		}

		private DataTable GetSAPDocumentData()
		{
			string strStartDate = "";
			string strEndDate = "";
			string strStartHour = "";
			string strEndHour = "";
			string strWerks = "";
			string strLgort = "";
			string strMType="";

			stsWarning.Text = "";
			
			try
			{
				if(cmbWerks.SelectedIndex != -1)
				{
					strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
				}
				if(cmbLgort.SelectedIndex != -1)
				{
					strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
				}
				strStartHour = "00:00:00";
				strEndHour = "23:59:59";

				if(chkDate.Checked)
				{
					strStartDate = dtpStartDate.Value.ToString("yyyyMMdd") + " " + strStartHour;
					strEndDate = dtpEndDate.Value.ToString("yyyyMMdd") + " " + strEndHour;
				}
				else
				{
					strStartDate = "";
					strEndDate = "";
				}

				if(cmbMType.SelectedIndex != -1)
				{
					strMType = cmbMType.Items[cmbMType.SelectedIndex].ToString();
				}

				objSapData = new SapData(UserData, strWerks, strLgort);
				return objSapData.QuerySapData(strStartDate, strEndDate, 2, "", "",strMType.Trim());
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-GetSAPDocumentData()");
			}
		}

		private void btnExecute_Click(object sender, System.EventArgs e)
		{
			stsWarning.Text = "";
			DataTable dtSapData = new DataTable();
			DataRow[] foundRow;
			Int32 intTotalGR = 0;
			Int32 intTotalGI = 0;
			Int32 intTotalBalance = 0;
			string strMType="";
			try
			{
				if(cmbMType.SelectedIndex==-1)
				{
					stsWarning.Text = "MType can't be empty!!";
					return;				
				}
				else
				{
					strMType = cmbMType.Items[cmbMType.SelectedIndex].ToString();
				}
				Cursor.Current = Cursors.WaitCursor;
				objStorageData = new StorageData(UserData, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString());
				dtData = objStorageData.QueryContrastData("", false,strMType.Trim());
				//dtData = objStorageData.QueryContrastData("", true,strMType.Trim());
				//dtData = objStorageData.QueryContrastData("", true);
				dtData.Columns.Add("SAP_GR");
				dtData.Columns.Add("SAP_GI");
				dtData.Columns.Add("SAP_BALANCE");
				if(dtData.Rows.Count == 0)
				{
					Cursor.Current = Cursors.Default;
					stsWarning.Text = "No Different!!";
					ShowDataGrid();
					return;
				}
				else
				{
					Cursor.Current = Cursors.Default;
					dtSapData = GetSAPDocumentData();
					for(int i=0;i<dtData.Rows.Count;i++)
					{
						intTotalGR = 0;
						intTotalGI = 0;
						intTotalBalance = 0;
						
						//入庫
						foundRow = dtSapData.Select("MATNR='"+ dtData.Rows[i]["MATNR"].ToString().Trim() +"' and INSMK='"+ dtData.Rows[i]["INSMK"].ToString().Trim() +"' and CHARG='"+ dtData.Rows[i]["CHARG"].ToString().Trim() +"' and TRNTP = '+'");
						for(int j=0;j<foundRow.Length;j++)
						{
							if(Int32.Parse(foundRow[j]["MENGE"].ToString()) - Int32.Parse(foundRow[j]["OTQTY"].ToString()) < 0)
							{
								intTotalGR += 0;
							}
							else
							{
								intTotalGR += Int32.Parse(foundRow[j]["MENGE"].ToString()) - Int32.Parse(foundRow[j]["OTQTY"].ToString());
							}
						}
						//出庫
						foundRow = dtSapData.Select("MATNR='"+ dtData.Rows[i]["MATNR"].ToString().Trim() +"' and INSMK='"+ dtData.Rows[i]["INSMK"].ToString().Trim() +"' and CHARG='"+ dtData.Rows[i]["CHARG"].ToString().Trim() +"' and TRNTP = '-'");
						for(int j=0;j<foundRow.Length;j++)
						{
							if(Int32.Parse(foundRow[j]["MENGE"].ToString()) - Int32.Parse(foundRow[j]["OTQTY"].ToString()) < 0)
							{
								intTotalGI += 0;
							}
							else
							{
								intTotalGI += Int32.Parse(foundRow[j]["MENGE"].ToString()) - Int32.Parse(foundRow[j]["OTQTY"].ToString());
							}
						}
						intTotalBalance = intTotalGR - intTotalGI;
						dtData.Rows[i]["SAP_GR"] = intTotalGR.ToString();
						dtData.Rows[i]["SAP_GI"] = intTotalGI.ToString();
						dtData.Rows[i]["SAP_BALANCE"] = intTotalBalance.ToString();
					}
					this.btnPrint.Enabled = true;
					this.btnExecute.Enabled = true;
					ShowDataGrid();
				}
			}
			catch(Exception ex)
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
			this.btnPrint.Enabled = false;
			this.cmbType.SelectedIndex = 0;
			this.dtpStartDate.Value = DateTime.Now;
			this.dtpEndDate.Value = DateTime.Now;
			this.chkDate.Checked = false;
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			stsWarning.Text = "";
			ReportPrint objReportPrint = new ReportPrint(UserData, "CONTRAST_DOCUMENT", dtData);
			objReportPrint.MdiParent = this.ParentForm;
			objReportPrint.Show();
		}

		private void Manage_StorageContrast_Resize(object sender, System.EventArgs e)
		{
			panel5.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel5.Size.Height);
			if(this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
				stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
		}

		private void btnDownload_Click(object sender, System.EventArgs e)
		{
			string strExportName = "";
			try
			{
				if(sfdSaveFile.ShowDialog() == DialogResult.OK)
				{
					strExportName = sfdSaveFile.FileName;
					CountingResult2File(strExportName);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private void CountingResult2File(string strFilePath)
		{
			
			string strLine = "";
			try
			{
				fi = new FileInfo(strFilePath);
				sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
				strLine = "Plant\tStorage\tPart No\tStock\tVersion\t" + cmbMType.SelectedText + " Qty\tQWMS Qty\tSAP-QWMS\tG/R\tG/I\tG/R-G/I";
				sw.WriteLine(strLine);
				//Reading data
				for(int i=0; i< dtData.Rows.Count; i++)
				{
					strLine = "";
					strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
					strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
					strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
					strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
					strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
					strLine += dtData.Rows[i]["SAP_MENGE"].ToString() + "\t";
					strLine += dtData.Rows[i]["QWMS_MENGE"].ToString() + "\t";
					strLine += dtData.Rows[i]["KTMNG"].ToString() + "\t";
					strLine += dtData.Rows[i]["SAP_GR"].ToString() + "\t";
					strLine += dtData.Rows[i]["SAP_GI"].ToString() + "\t";
					strLine += dtData.Rows[i]["SAP_BALANCE"].ToString();
					sw.WriteLine(strLine);
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-CountingResult2File()");
			}
			finally
			{
				sw.Close();
			}
	
		}
	}
}
