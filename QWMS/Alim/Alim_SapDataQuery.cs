using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using QCI.QWMS;
using QWMS.Common;
using System.Linq;
using QCI_QWMS_Alim;

namespace QWMS
{
	/// <summary>
	/// Alim_SapDataQuery 的摘要描述。
	/// </summary>
	public class Alim_SapDataQuery : System.Windows.Forms.Form
	{
        UserInfo UserData = new UserInfo();
		private System.Windows.Forms.SaveFileDialog sfdSaveFile;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.StatusBarPanel stsWarning;
		private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsDate;
		private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        protected internal System.Windows.Forms.StatusBar stbStatus;

		private string strMandt = "";
		private string strUsrnm = "";
        private string StrComcd = "";
		private string strProgid = "";
		private string strWerks = "";
		private string strLgort = "";
		private FileInfo fi;
		private StreamWriter sw;
		private DataTable dtData = new DataTable();
		//private SQLAccess objDB;
		//private StorageIn objStorageIn;
		//private PlantData objPlantData;
		//private Authority objAuthority;
		//private SapData objSapData;
        private Alim_StorageLogQuery objAlim_StorageLogQuery;
        //private AccessConfig objConfig;
        private DataGridView dgvData;
        private Label lblData;
        private Panel panel1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Panel panel6;
        private Label label12;
        private TextBox txtBwart;
        private Label label6;
        private TextBox txtEndMblnr;
        private ComboBox cmbEndHour;
        private DateTimePicker dtpEndDate;
        private Button btnConfirm;
        private Label label7;
        private TextBox txtEndMatnr;
        private Label label4;
        private Panel panel5;
        private Label label14;
        private CheckBox chkOldData;
        private CheckBox chkDate;
        private ComboBox cmbGRGI;
        private Label label11;
        private TextBox txtStartMblnr;
        private Label label3;
        private ComboBox cmbStartHour;
        private DateTimePicker dtpStartDate;
        private Label label9;
        private TextBox txtStartMatnr;
        private Label label8;
        private Panel panel3;
        private Label label13;
        private ComboBox cmbMType;
        private ComboBox cmbType;
        private Label label10;
        private ComboBox cmbInsmk;
        private Label label5;
        private ComboBox cmbLgort;
        private ComboBox cmbWerks;
        private Label label2;
        private Label label1;
        private TabPage tabPage2;
        private Button btnConfirm_XL;
        private TextBox tbXLID;
        private Label label15;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Panel panel4;
        private ComboBox cmbLgort_XL;
        private ComboBox cmbWerks_XL;
        private Label label19;
        private Label label20;
        private Panel panel7;
        private ComboBox cmbInsmk_XY;
        private Label label23;
        private ComboBox cmbLgort_XY;
        private ComboBox cmbWerks_XY;
        private Label label24;
        private Label label25;
        private Panel panel8;
        private ComboBox cmbInsmk_DID;
        private Label label28;
        private ComboBox cmbLgort_DID;
        private ComboBox cmbWerks_DID;
        private Label label29;
        private Label label30;
        private TextBox tbXYGrNo;
        private Label label32;
        private Button btnConfirm_XY;
        private TextBox tbXYNo;
        private Label label31;
        private TextBox tbXNDJ;
        private Label label33;
        private Button btnConfirm_DID;
        private TextBox tbDIDNo;
        private Label label34;
        string strType = "";
        private TabPage tabPage5;
        private Panel panel9;
        private ComboBox cmbLgort_GR;
        private ComboBox cmbWerks_GR;
        private Label label17;
        private Label label21;
        private Button btnConfirm_GR;
        private TextBox tbGRID;
        private Label label18; //暮翹岆闡珨跺耀宒腔download

		/// <summary>
		/// 扽俶
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
                return StrComcd;
            }
            set
            {
                StrComcd = value;
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

        public Alim_SapDataQuery(UserInfo _UserData, string strProgid)
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
				//objStorageIn = new StorageIn(UserData, Progid);
				//objPlantData = new PlantData(UserData);
				//objAuthority = new Authority(UserData);
                objAlim_StorageLogQuery = new Alim_StorageLogQuery(UserData, Progid);

				//檢查權限
                if (!objAlim_StorageLogQuery.CheckAuthority())
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					//秀出Status的資料
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
					ShowDdlInsmk();
					ShowDdlStartEndHour();

                    ShowDdlLgort_XL();
                    ShowDdlWerks_XL();
                    ShowDdlLgort_DID();
                    ShowDdlWerks_DID();
                    ShowDdlInsmk_DID();
                    ShowDdlLgort_XY();
                    ShowDdlWerks_XY();
                    ShowDdlInsmk_XY();
                    ShowDdlLgort_GR();
                    ShowDdlWerks_GR();
					this.cmbType.SelectedIndex = 2;
					this.cmbGRGI.SelectedIndex = 2;
					this.cmbMType.SelectedIndex = 0;
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
					if(cmbLgort.Items.Count > 0)
					{
						this.cmbLgort.SelectedIndex = 0;
					}
                    //扢隅蘇�珋�
                    if (cmbWerks_XL.Items.Count > 0)
                    {
                        this.cmbWerks_XL.SelectedIndex = 0;
                    }
                    if (cmbWerks_DID.Items.Count > 0)
                    {
                        this.cmbWerks_DID.SelectedIndex = 0;
                    }
                    if (cmbWerks_XY.Items.Count > 0)
                    {
                        this.cmbWerks_XY.SelectedIndex = 0;
                    }
                    if (cmbWerks_GR.Items.Count > 0)
                    {
                        this.cmbWerks_GR.SelectedIndex = 0;
                    }
                    if (cmbLgort_XL.Items.Count > 0)
                    {
                        this.cmbLgort_XL.SelectedIndex = 0;
                    }
                    if (cmbLgort_DID.Items.Count > 0)
                    {
                        this.cmbLgort_DID.SelectedIndex = 0;
                    }
                    if (cmbLgort_XY.Items.Count > 0)
                    {
                        this.cmbLgort_XY.SelectedIndex = 0;
                    }
                    if (cmbLgort_GR.Items.Count > 0)
                    {
                        this.cmbLgort_GR.SelectedIndex = 0;
                    }
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
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblData = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBwart = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEndMblnr = new System.Windows.Forms.TextBox();
            this.cmbEndHour = new System.Windows.Forms.ComboBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEndMatnr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.chkOldData = new System.Windows.Forms.CheckBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.cmbGRGI = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtStartMblnr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbStartHour = new System.Windows.Forms.ComboBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStartMatnr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbMType = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbInsmk = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbLgort_XL = new System.Windows.Forms.ComboBox();
            this.cmbWerks_XL = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnConfirm_XL = new System.Windows.Forms.Button();
            this.tbXLID = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbXYGrNo = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.btnConfirm_XY = new System.Windows.Forms.Button();
            this.tbXYNo = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cmbInsmk_XY = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbLgort_XY = new System.Windows.Forms.ComboBox();
            this.cmbWerks_XY = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tbXNDJ = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.btnConfirm_DID = new System.Windows.Forms.Button();
            this.tbDIDNo = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.cmbInsmk_DID = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cmbLgort_DID = new System.Windows.Forms.ComboBox();
            this.cmbWerks_DID = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.cmbLgort_GR = new System.Windows.Forms.ComboBox();
            this.cmbWerks_GR = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btnConfirm_GR = new System.Windows.Forms.Button();
            this.tbGRID = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "SapDocument.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblData);
            this.panel2.Controls.Add(this.btnDownload);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 163);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1370, 290);
            this.panel2.TabIndex = 40;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Location = new System.Drawing.Point(19, 3);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(61, 14);
            this.lblData.TabIndex = 47;
            this.lblData.Text = "0 records";
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(19, 249);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(96, 30);
            this.btnDownload.TabIndex = 44;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(125, 249);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 16;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(221, 249);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(19, 5);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(1341, 239);
            this.dgvData.TabIndex = 46;
            // 
            // stsWarning
            // 
            this.stsWarning.Name = "stsWarning";
            this.stsWarning.Width = 410;
            // 
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
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
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 453);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1370, 20);
            this.stbStatus.TabIndex = 38;
            this.stbStatus.Text = "Status";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 163);
            this.panel1.TabIndex = 39;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(19, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1002, 158);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel6);
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(994, 130);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SAP等擂脤戙";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label12);
            this.panel6.Controls.Add(this.txtBwart);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.txtEndMblnr);
            this.panel6.Controls.Add(this.cmbEndHour);
            this.panel6.Controls.Add(this.dtpEndDate);
            this.panel6.Controls.Add(this.btnConfirm);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.txtEndMatnr);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(713, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(278, 124);
            this.panel6.TabIndex = 70;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.Location = new System.Drawing.Point(9, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 21);
            this.label12.TabIndex = 58;
            this.label12.Text = "Mvt";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBwart
            // 
            this.txtBwart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBwart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBwart.Location = new System.Drawing.Point(48, 75);
            this.txtBwart.MaxLength = 14;
            this.txtBwart.Name = "txtBwart";
            this.txtBwart.Size = new System.Drawing.Size(56, 21);
            this.txtBwart.TabIndex = 57;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.Location = new System.Drawing.Point(9, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 21);
            this.label6.TabIndex = 56;
            this.label6.Text = "To";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndMblnr
            // 
            this.txtEndMblnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndMblnr.Location = new System.Drawing.Point(48, 8);
            this.txtEndMblnr.MaxLength = 20;
            this.txtEndMblnr.Name = "txtEndMblnr";
            this.txtEndMblnr.Size = new System.Drawing.Size(143, 21);
            this.txtEndMblnr.TabIndex = 55;
            // 
            // cmbEndHour
            // 
            this.cmbEndHour.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmbEndHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndHour.Location = new System.Drawing.Point(143, 53);
            this.cmbEndHour.Name = "cmbEndHour";
            this.cmbEndHour.Size = new System.Drawing.Size(48, 23);
            this.cmbEndHour.TabIndex = 10;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(48, 53);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 21);
            this.dtpEndDate.TabIndex = 9;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(18, 99);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(76, 30);
            this.btnConfirm.TabIndex = 14;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.Location = new System.Drawing.Point(9, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 21);
            this.label7.TabIndex = 51;
            this.label7.Text = "To";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndMatnr
            // 
            this.txtEndMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndMatnr.Location = new System.Drawing.Point(48, 31);
            this.txtEndMatnr.MaxLength = 20;
            this.txtEndMatnr.Name = "txtEndMatnr";
            this.txtEndMatnr.Size = new System.Drawing.Size(143, 21);
            this.txtEndMatnr.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(9, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 22);
            this.label4.TabIndex = 54;
            this.label4.Text = "To";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.chkOldData);
            this.panel5.Controls.Add(this.chkDate);
            this.panel5.Controls.Add(this.cmbGRGI);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.txtStartMblnr);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.cmbStartHour);
            this.panel5.Controls.Add(this.dtpStartDate);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.txtStartMatnr);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(205, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(786, 124);
            this.panel5.TabIndex = 69;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.Location = new System.Drawing.Point(55, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(170, 22);
            this.label14.TabIndex = 60;
            this.label14.Text = "Query Old Data(1 months ago)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkOldData
            // 
            this.chkOldData.Location = new System.Drawing.Point(10, 97);
            this.chkOldData.Name = "chkOldData";
            this.chkOldData.Size = new System.Drawing.Size(19, 22);
            this.chkOldData.TabIndex = 59;
            // 
            // chkDate
            // 
            this.chkDate.Location = new System.Drawing.Point(10, 52);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(19, 23);
            this.chkDate.TabIndex = 58;
            // 
            // cmbGRGI
            // 
            this.cmbGRGI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGRGI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGRGI.Items.AddRange(new object[] {
            "G/R",
            "G/I",
            "All Data"});
            this.cmbGRGI.Location = new System.Drawing.Point(144, 75);
            this.cmbGRGI.Name = "cmbGRGI";
            this.cmbGRGI.Size = new System.Drawing.Size(295, 23);
            this.cmbGRGI.TabIndex = 57;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.Location = new System.Drawing.Point(77, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 21);
            this.label11.TabIndex = 56;
            this.label11.Text = "GR/GI";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartMblnr
            // 
            this.txtStartMblnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartMblnr.Location = new System.Drawing.Point(144, 8);
            this.txtStartMblnr.MaxLength = 20;
            this.txtStartMblnr.Name = "txtStartMblnr";
            this.txtStartMblnr.Size = new System.Drawing.Size(330, 21);
            this.txtStartMblnr.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(19, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 21);
            this.label3.TabIndex = 55;
            this.label3.Text = "Document From";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStartHour
            // 
            this.cmbStartHour.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmbStartHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartHour.Location = new System.Drawing.Point(426, 53);
            this.cmbStartHour.Name = "cmbStartHour";
            this.cmbStartHour.Size = new System.Drawing.Size(48, 23);
            this.cmbStartHour.TabIndex = 8;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(144, 53);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(295, 21);
            this.dtpStartDate.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.Location = new System.Drawing.Point(19, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 22);
            this.label9.TabIndex = 53;
            this.label9.Text = "Download From";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartMatnr
            // 
            this.txtStartMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartMatnr.Location = new System.Drawing.Point(144, 31);
            this.txtStartMatnr.MaxLength = 20;
            this.txtStartMatnr.Name = "txtStartMatnr";
            this.txtStartMatnr.Size = new System.Drawing.Size(330, 21);
            this.txtStartMatnr.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.Location = new System.Drawing.Point(29, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 21);
            this.label8.TabIndex = 49;
            this.label8.Text = "Part No. From";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.cmbMType);
            this.panel3.Controls.Add(this.cmbType);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.cmbInsmk);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.cmbLgort);
            this.panel3.Controls.Add(this.cmbWerks);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(202, 124);
            this.panel3.TabIndex = 68;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(19, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(67, 22);
            this.label13.TabIndex = 49;
            this.label13.Text = "Type";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMType
            // 
            this.cmbMType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMType.ItemHeight = 15;
            this.cmbMType.Items.AddRange(new object[] {
            "SAP",
            "QMS",
            "SDS",
            "QSMS(SMT)",
            "S/F(PCBA)"});
            this.cmbMType.Location = new System.Drawing.Point(86, 98);
            this.cmbMType.Name = "cmbMType";
            this.cmbMType.Size = new System.Drawing.Size(106, 23);
            this.cmbMType.TabIndex = 48;
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Items.AddRange(new object[] {
            "Finished",
            "Not Finished",
            "All Data"});
            this.cmbType.Location = new System.Drawing.Point(86, 75);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(106, 23);
            this.cmbType.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.Location = new System.Drawing.Point(19, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 21);
            this.label10.TabIndex = 46;
            this.label10.Text = "Status";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbInsmk
            // 
            this.cmbInsmk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInsmk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsmk.Location = new System.Drawing.Point(86, 53);
            this.cmbInsmk.Name = "cmbInsmk";
            this.cmbInsmk.Size = new System.Drawing.Size(106, 23);
            this.cmbInsmk.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Location = new System.Drawing.Point(19, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 22);
            this.label5.TabIndex = 45;
            this.label5.Text = "Stock";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(86, 31);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(106, 23);
            this.cmbLgort.TabIndex = 1;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(86, 8);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(106, 23);
            this.cmbWerks.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(19, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 39;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(19, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 21);
            this.label1.TabIndex = 38;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.btnConfirm_XL);
            this.tabPage2.Controls.Add(this.tbXLID);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(994, 130);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "矨韓ID脤戙";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbLgort_XL);
            this.panel4.Controls.Add(this.cmbWerks_XL);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label20);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(202, 124);
            this.panel4.TabIndex = 69;
            // 
            // cmbLgort_XL
            // 
            this.cmbLgort_XL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort_XL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort_XL.Location = new System.Drawing.Point(86, 31);
            this.cmbLgort_XL.Name = "cmbLgort_XL";
            this.cmbLgort_XL.Size = new System.Drawing.Size(106, 23);
            this.cmbLgort_XL.TabIndex = 1;
            // 
            // cmbWerks_XL
            // 
            this.cmbWerks_XL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks_XL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks_XL.Location = new System.Drawing.Point(86, 8);
            this.cmbWerks_XL.Name = "cmbWerks_XL";
            this.cmbWerks_XL.Size = new System.Drawing.Size(106, 23);
            this.cmbWerks_XL.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label19.Location = new System.Drawing.Point(19, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 21);
            this.label19.TabIndex = 39;
            this.label19.Text = "Storage";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label20.Location = new System.Drawing.Point(19, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 21);
            this.label20.TabIndex = 38;
            this.label20.Text = "Plant";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm_XL
            // 
            this.btnConfirm_XL.Location = new System.Drawing.Point(222, 103);
            this.btnConfirm_XL.Name = "btnConfirm_XL";
            this.btnConfirm_XL.Size = new System.Drawing.Size(56, 18);
            this.btnConfirm_XL.TabIndex = 2;
            this.btnConfirm_XL.Text = "Confirm";
            this.btnConfirm_XL.UseVisualStyleBackColor = true;
            this.btnConfirm_XL.Click += new System.EventHandler(this.btnConfirm_XL_Click);
            // 
            // tbXLID
            // 
            this.tbXLID.Location = new System.Drawing.Point(286, 11);
            this.tbXLID.Name = "tbXLID";
            this.tbXLID.Size = new System.Drawing.Size(262, 21);
            this.tbXLID.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(220, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 15);
            this.label15.TabIndex = 0;
            this.label15.Text = "GroupID";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbXYGrNo);
            this.tabPage3.Controls.Add(this.label32);
            this.tabPage3.Controls.Add(this.btnConfirm_XY);
            this.tabPage3.Controls.Add(this.tbXYNo);
            this.tabPage3.Controls.Add(this.label31);
            this.tabPage3.Controls.Add(this.panel7);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(994, 130);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "船祑脤戙";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbXYGrNo
            // 
            this.tbXYGrNo.Location = new System.Drawing.Point(292, 16);
            this.tbXYGrNo.Name = "tbXYGrNo";
            this.tbXYGrNo.Size = new System.Drawing.Size(262, 21);
            this.tbXYGrNo.TabIndex = 75;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(238, 16);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(49, 15);
            this.label32.TabIndex = 74;
            this.label32.Text = "GR等擂";
            // 
            // btnConfirm_XY
            // 
            this.btnConfirm_XY.Location = new System.Drawing.Point(241, 103);
            this.btnConfirm_XY.Name = "btnConfirm_XY";
            this.btnConfirm_XY.Size = new System.Drawing.Size(56, 18);
            this.btnConfirm_XY.TabIndex = 73;
            this.btnConfirm_XY.Text = "Confirm";
            this.btnConfirm_XY.UseVisualStyleBackColor = true;
            this.btnConfirm_XY.Click += new System.EventHandler(this.btnConfirm_XY_Click);
            // 
            // tbXYNo
            // 
            this.tbXYNo.Location = new System.Drawing.Point(292, 58);
            this.tbXYNo.Name = "tbXYNo";
            this.tbXYNo.Size = new System.Drawing.Size(262, 21);
            this.tbXYNo.TabIndex = 72;
            this.tbXYNo.Visible = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(236, 58);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(52, 15);
            this.label31.TabIndex = 71;
            this.label31.Text = "GroupID";
            this.label31.Visible = false;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cmbInsmk_XY);
            this.panel7.Controls.Add(this.label23);
            this.panel7.Controls.Add(this.cmbLgort_XY);
            this.panel7.Controls.Add(this.cmbWerks_XY);
            this.panel7.Controls.Add(this.label24);
            this.panel7.Controls.Add(this.label25);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(192, 124);
            this.panel7.TabIndex = 70;
            // 
            // cmbInsmk_XY
            // 
            this.cmbInsmk_XY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInsmk_XY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsmk_XY.Location = new System.Drawing.Point(86, 53);
            this.cmbInsmk_XY.Name = "cmbInsmk_XY";
            this.cmbInsmk_XY.Size = new System.Drawing.Size(96, 23);
            this.cmbInsmk_XY.TabIndex = 2;
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label23.Location = new System.Drawing.Point(19, 53);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(67, 22);
            this.label23.TabIndex = 45;
            this.label23.Text = "Stock";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort_XY
            // 
            this.cmbLgort_XY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort_XY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort_XY.Location = new System.Drawing.Point(86, 31);
            this.cmbLgort_XY.Name = "cmbLgort_XY";
            this.cmbLgort_XY.Size = new System.Drawing.Size(96, 23);
            this.cmbLgort_XY.TabIndex = 1;
            // 
            // cmbWerks_XY
            // 
            this.cmbWerks_XY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks_XY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks_XY.Location = new System.Drawing.Point(86, 8);
            this.cmbWerks_XY.Name = "cmbWerks_XY";
            this.cmbWerks_XY.Size = new System.Drawing.Size(96, 23);
            this.cmbWerks_XY.TabIndex = 0;
            // 
            // label24
            // 
            this.label24.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label24.Location = new System.Drawing.Point(19, 31);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 21);
            this.label24.TabIndex = 39;
            this.label24.Text = "Storage";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label25
            // 
            this.label25.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label25.Location = new System.Drawing.Point(19, 8);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(67, 21);
            this.label25.TabIndex = 38;
            this.label25.Text = "Plant";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tbXNDJ);
            this.tabPage4.Controls.Add(this.label33);
            this.tabPage4.Controls.Add(this.btnConfirm_DID);
            this.tabPage4.Controls.Add(this.tbDIDNo);
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Controls.Add(this.panel8);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(994, 130);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "DID脤戙";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tbXNDJ
            // 
            this.tbXNDJ.Location = new System.Drawing.Point(305, 41);
            this.tbXNDJ.Name = "tbXNDJ";
            this.tbXNDJ.Size = new System.Drawing.Size(262, 21);
            this.tbXNDJ.TabIndex = 80;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(238, 44);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(59, 15);
            this.label33.TabIndex = 79;
            this.label33.Text = "剞攜等擂";
            // 
            // btnConfirm_DID
            // 
            this.btnConfirm_DID.Location = new System.Drawing.Point(241, 103);
            this.btnConfirm_DID.Name = "btnConfirm_DID";
            this.btnConfirm_DID.Size = new System.Drawing.Size(56, 18);
            this.btnConfirm_DID.TabIndex = 78;
            this.btnConfirm_DID.Text = "Confirm";
            this.btnConfirm_DID.UseVisualStyleBackColor = true;
            this.btnConfirm_DID.Click += new System.EventHandler(this.btnConfirm_DID_Click);
            // 
            // tbDIDNo
            // 
            this.tbDIDNo.Location = new System.Drawing.Point(305, 11);
            this.tbDIDNo.Name = "tbDIDNo";
            this.tbDIDNo.Size = new System.Drawing.Size(262, 21);
            this.tbDIDNo.TabIndex = 77;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(238, 13);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(44, 15);
            this.label34.TabIndex = 76;
            this.label34.Text = "DID No";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.cmbInsmk_DID);
            this.panel8.Controls.Add(this.label28);
            this.panel8.Controls.Add(this.cmbLgort_DID);
            this.panel8.Controls.Add(this.cmbWerks_DID);
            this.panel8.Controls.Add(this.label29);
            this.panel8.Controls.Add(this.label30);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(202, 124);
            this.panel8.TabIndex = 70;
            // 
            // cmbInsmk_DID
            // 
            this.cmbInsmk_DID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInsmk_DID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsmk_DID.Location = new System.Drawing.Point(86, 53);
            this.cmbInsmk_DID.Name = "cmbInsmk_DID";
            this.cmbInsmk_DID.Size = new System.Drawing.Size(106, 23);
            this.cmbInsmk_DID.TabIndex = 2;
            // 
            // label28
            // 
            this.label28.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label28.Location = new System.Drawing.Point(19, 53);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(67, 22);
            this.label28.TabIndex = 45;
            this.label28.Text = "Stock";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort_DID
            // 
            this.cmbLgort_DID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort_DID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort_DID.Location = new System.Drawing.Point(86, 31);
            this.cmbLgort_DID.Name = "cmbLgort_DID";
            this.cmbLgort_DID.Size = new System.Drawing.Size(106, 23);
            this.cmbLgort_DID.TabIndex = 1;
            // 
            // cmbWerks_DID
            // 
            this.cmbWerks_DID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks_DID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks_DID.Location = new System.Drawing.Point(86, 8);
            this.cmbWerks_DID.Name = "cmbWerks_DID";
            this.cmbWerks_DID.Size = new System.Drawing.Size(106, 23);
            this.cmbWerks_DID.TabIndex = 0;
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label29.Location = new System.Drawing.Point(19, 31);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(67, 21);
            this.label29.TabIndex = 39;
            this.label29.Text = "Storage";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label30.Location = new System.Drawing.Point(19, 8);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(67, 21);
            this.label30.TabIndex = 38;
            this.label30.Text = "Plant";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.panel9);
            this.tabPage5.Controls.Add(this.btnConfirm_GR);
            this.tabPage5.Controls.Add(this.tbGRID);
            this.tabPage5.Controls.Add(this.label18);
            this.tabPage5.Location = new System.Drawing.Point(4, 24);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(994, 130);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "�踸甂邾銆穔札搚橐�";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.cmbLgort_GR);
            this.panel9.Controls.Add(this.cmbWerks_GR);
            this.panel9.Controls.Add(this.label17);
            this.panel9.Controls.Add(this.label21);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(3, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(192, 124);
            this.panel9.TabIndex = 71;
            // 
            // cmbLgort_GR
            // 
            this.cmbLgort_GR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort_GR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort_GR.Location = new System.Drawing.Point(86, 31);
            this.cmbLgort_GR.Name = "cmbLgort_GR";
            this.cmbLgort_GR.Size = new System.Drawing.Size(96, 23);
            this.cmbLgort_GR.TabIndex = 1;
            // 
            // cmbWerks_GR
            // 
            this.cmbWerks_GR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks_GR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks_GR.Location = new System.Drawing.Point(86, 8);
            this.cmbWerks_GR.Name = "cmbWerks_GR";
            this.cmbWerks_GR.Size = new System.Drawing.Size(96, 23);
            this.cmbWerks_GR.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label17.Location = new System.Drawing.Point(19, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 21);
            this.label17.TabIndex = 39;
            this.label17.Text = "Storage";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label21.Location = new System.Drawing.Point(19, 8);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 21);
            this.label21.TabIndex = 38;
            this.label21.Text = "Plant";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnConfirm_GR
            // 
            this.btnConfirm_GR.Location = new System.Drawing.Point(228, 104);
            this.btnConfirm_GR.Name = "btnConfirm_GR";
            this.btnConfirm_GR.Size = new System.Drawing.Size(56, 18);
            this.btnConfirm_GR.TabIndex = 46;
            this.btnConfirm_GR.Text = "Confirm";
            this.btnConfirm_GR.UseVisualStyleBackColor = true;
            this.btnConfirm_GR.Click += new System.EventHandler(this.btnConfirm_GR_Click);
            // 
            // tbGRID
            // 
            this.tbGRID.Location = new System.Drawing.Point(292, 12);
            this.tbGRID.Name = "tbGRID";
            this.tbGRID.Size = new System.Drawing.Size(262, 21);
            this.tbGRID.TabIndex = 45;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(226, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 15);
            this.label18.TabIndex = 44;
            this.label18.Text = "諶梖等擂";
            // 
            // Alim_SapDataQuery
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1370, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Name = "Alim_SapDataQuery";
            this.Text = "SAP Material Documents Inquiry";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.panel9.ResumeLayout(false);
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
                dtTemp = objAlim_StorageLogQuery.CheckPlantAuthority();
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
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority(strWerks);
				}
				else
				{
					//					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority();
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
                    #region 提供重慶W/H可以By廠區(不選倉別)查詢SAP單據號碼並Download成Excel檔
                    if (Comcd == "9110" || Comcd == "9200" || Comcd =="2281")
                    {
                        cmbLgort.Items.Add("");
                    }
                    #endregion
                    for (int i=0;i<dtTemp.Rows.Count;i++)
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


		private void ShowDdlInsmk()
		{
			DataTable dtTemp = new DataTable();
			try
			{
				cmbInsmk.Items.Clear();
                dtTemp = objAlim_StorageLogQuery.GetDdlInsmk();
				for(int i=0;i<dtTemp.Rows.Count;i++)
				{
					cmbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlInsmk()");
			}
		}

		private void ShowDdlStartEndHour()
		{
			for(int i=1;i<=24;i++)
			{
				cmbStartHour.Items.Add(i.ToString());
				cmbEndHour.Items.Add(i.ToString());
			}
		}
		private void ShowDataGrid()
		{
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
			//dgvData.TableStyles.Clear();
			try
			{
                //DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                //mydtgTableStyle.MappingName = dtData.TableName;

                DataGridViewTextBoxColumn mblnrStyle = new DataGridViewTextBoxColumn();
                mblnrStyle.DataPropertyName = "MBLNR";
				mblnrStyle.HeaderText = "Document No";
				mblnrStyle.Width = 130;
				mblnrStyle.ReadOnly = true;
				dgvData.Columns.Add(mblnrStyle);

                DataGridViewTextBoxColumn zeileStyle = new DataGridViewTextBoxColumn();
                zeileStyle.DataPropertyName = "ZEILE";
				zeileStyle.HeaderText = "Document Item";
				zeileStyle.Width = 70;
				zeileStyle.ReadOnly = true;
				dgvData.Columns.Add(zeileStyle);
//
//				DataGridColumnStyle werksStyle = new DataGridTextBoxColumn();
//				werksStyle.MappingName = "WERKS";
//				werksStyle.HeaderText = "Plant";
//				werksStyle.ReadOnly = true;
//				dgvData.Columns.Add(werksStyle);
//
//				DataGridColumnStyle lgortStyle = new DataGridTextBoxColumn();
//				lgortStyle.MappingName = "LGORT";
//				lgortStyle.HeaderText = "Storage";
//				lgortStyle.ReadOnly = true;
//				dgvData.Columns.Add(lgortStyle);


                DataGridViewTextBoxColumn trntpStyle = new DataGridViewTextBoxColumn();
                trntpStyle.DataPropertyName = "TRNTP";
				trntpStyle.HeaderText = "Type";
				trntpStyle.Width = 50;
				trntpStyle.ReadOnly = true;
				dgvData.Columns.Add(trntpStyle);

                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
				matnrStyle.HeaderText = "Part No";
				matnrStyle.Width = 90;
				matnrStyle.ReadOnly = true;
				dgvData.Columns.Add(matnrStyle);

                DataGridViewTextBoxColumn insmkStyle = new DataGridViewTextBoxColumn();
                insmkStyle.DataPropertyName = "INSMK";
				insmkStyle.HeaderText = "Stock";
				insmkStyle.Width = 50;
				insmkStyle.ReadOnly = true;
				dgvData.Columns.Add(insmkStyle);

                DataGridViewTextBoxColumn chargStyle = new DataGridViewTextBoxColumn();
                chargStyle.DataPropertyName = "CHARG";
				chargStyle.HeaderText = "Version";
                chargStyle.Width = 50;
				chargStyle.ReadOnly = true;
				dgvData.Columns.Add(chargStyle);

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
				mengeStyle.HeaderText = "Doc. Qty";
                mengeStyle.Width = 80;
				mengeStyle.ReadOnly = true;
				dgvData.Columns.Add(mengeStyle);

                DataGridViewTextBoxColumn otqtyStyle = new DataGridViewTextBoxColumn();
                otqtyStyle.DataPropertyName = "OTQTY";
				otqtyStyle.HeaderText = "In/Out Qty";
                otqtyStyle.Width = 80;
				otqtyStyle.ReadOnly = true;
				dgvData.Columns.Add(otqtyStyle);

                DataGridViewTextBoxColumn bwartStyle = new DataGridViewTextBoxColumn();
                bwartStyle.DataPropertyName = "BWART";
				bwartStyle.HeaderText = "Mvt";
                bwartStyle.Width = 50;
				bwartStyle.ReadOnly = true;
				dgvData.Columns.Add(bwartStyle);

                DataGridViewTextBoxColumn lgort1Style = new DataGridViewTextBoxColumn();
                lgort1Style.DataPropertyName = "LGORT";
				lgort1Style.HeaderText = "From S.L.";
                lgort1Style.Width = 50;
				lgort1Style.ReadOnly = true;
				dgvData.Columns.Add(lgort1Style);
                
                DataGridViewTextBoxColumn umlgoStyle = new DataGridViewTextBoxColumn();
                umlgoStyle.DataPropertyName = "UMLGO";
				umlgoStyle.HeaderText = "To S.L.";
                umlgoStyle.Width = 50;
				umlgoStyle.ReadOnly = true;
				dgvData.Columns.Add(umlgoStyle);

                DataGridViewTextBoxColumn usnamStyle = new DataGridViewTextBoxColumn();
                usnamStyle.DataPropertyName = "USNAM";
				usnamStyle.HeaderText = "SAP User";
				usnamStyle.ReadOnly = true;
				dgvData.Columns.Add(usnamStyle);

                DataGridViewTextBoxColumn lifnrStyle = new DataGridViewTextBoxColumn();
                lifnrStyle.DataPropertyName = "LIFNR";
				lifnrStyle.HeaderText = "Vendor";
                lifnrStyle.Width = 90;
				lifnrStyle.ReadOnly = true;
				dgvData.Columns.Add(lifnrStyle);

                DataGridViewTextBoxColumn kostlStyle = new DataGridViewTextBoxColumn();
                kostlStyle.DataPropertyName = "KOSTL";
				kostlStyle.HeaderText = "Dept No.";
                kostlStyle.Width = 90;
				kostlStyle.ReadOnly = true;
				dgvData.Columns.Add(kostlStyle);
				
				//20051013 marc add kdmat field
                DataGridViewTextBoxColumn kdmatStyle = new DataGridViewTextBoxColumn();
                kdmatStyle.DataPropertyName = "KDMAT";
				kdmatStyle.HeaderText = "KDMAT";
                kdmatStyle.Width = 90;
				kdmatStyle.ReadOnly = true;
				dgvData.Columns.Add(kdmatStyle);

                DataGridViewTextBoxColumn crdatStyle = new DataGridViewTextBoxColumn();
                crdatStyle.DataPropertyName = "CRDAT";
				crdatStyle.HeaderText = "Created Date";
				crdatStyle.Width = 110;
				crdatStyle.ReadOnly = true;
				dgvData.Columns.Add(crdatStyle);

                DataGridViewTextBoxColumn dgvcGrloc = new DataGridViewTextBoxColumn();
                dgvcGrloc.DataPropertyName = "GRLOC";
                dgvcGrloc.HeaderText = "101Location";
                dgvcGrloc.ReadOnly = true;
                dgvcGrloc.Width = 100;
                dgvData.Columns.Add(dgvcGrloc);

                #region 依單據類型調整欄位(入庫 or 出庫)

                //出庫單據不變

                //入庫單據
                /*
                var result = from DataRow dRow in dtData.Rows
                             where (dRow.Field<string>("TRNTP") ?? "  ").Substring(1,1)=="+"
                             select dRow;

                DataRow dr = datatable.AsEnumerable().Where(r => ((string)r["code"]).Equals(someCode) && ((string)r["name"]).Equals(someName)).First();
                dr["color"] = someColor;
                */


                //無異動類型(G+,T+,..)單據



                #endregion


                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
				
			}
		}
        private void ShowDataGrid_XL()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            //dgvData.TableStyles.Clear();
            try
            {
                //DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                //mydtgTableStyle.MappingName = dtData.TableName;
                DataGridViewTextBoxColumn GrpidStyle = new DataGridViewTextBoxColumn();
                GrpidStyle.DataPropertyName = "GRPID";
                GrpidStyle.HeaderText = "Group ID";
                GrpidStyle.Width = 130;
                GrpidStyle.ReadOnly = true;
                dgvData.Columns.Add(GrpidStyle);

                DataGridViewTextBoxColumn DidnoStyle = new DataGridViewTextBoxColumn();
                DidnoStyle.DataPropertyName = "DIDNO";
                DidnoStyle.HeaderText = "DID NO";
                DidnoStyle.Width = 130;
                DidnoStyle.ReadOnly = true;
                dgvData.Columns.Add(DidnoStyle);

                DataGridViewTextBoxColumn mblnrStyle = new DataGridViewTextBoxColumn();
                mblnrStyle.DataPropertyName = "MBLNR";
                mblnrStyle.HeaderText = "Document No";
                mblnrStyle.Width = 130;
                mblnrStyle.ReadOnly = true;
                dgvData.Columns.Add(mblnrStyle);

                DataGridViewTextBoxColumn zeileStyle = new DataGridViewTextBoxColumn();
                zeileStyle.DataPropertyName = "ZEILE";
                zeileStyle.HeaderText = "Document Item";
                zeileStyle.Width = 70;
                zeileStyle.ReadOnly = true;
                dgvData.Columns.Add(zeileStyle);

                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                dgvData.Columns.Add(matnrStyle);


                DataGridViewTextBoxColumn chargStyle = new DataGridViewTextBoxColumn();
                chargStyle.DataPropertyName = "CHARG";
                chargStyle.HeaderText = "Version";
                chargStyle.Width = 50;
                chargStyle.ReadOnly = true;
                dgvData.Columns.Add(chargStyle);

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
                mengeStyle.HeaderText = "Doc. Qty";
                mengeStyle.Width = 80;
                mengeStyle.ReadOnly = true;
                dgvData.Columns.Add(mengeStyle);

                DataGridViewTextBoxColumn bwartStyle = new DataGridViewTextBoxColumn();
                bwartStyle.DataPropertyName = "BWART";
                bwartStyle.HeaderText = "Mvt";
                bwartStyle.Width = 50;
                bwartStyle.ReadOnly = true;
                dgvData.Columns.Add(bwartStyle);

                DataGridViewTextBoxColumn lgort1Style = new DataGridViewTextBoxColumn();
                lgort1Style.DataPropertyName = "LGORT";
                lgort1Style.HeaderText = "From S.L.";
                lgort1Style.Width = 50;
                lgort1Style.ReadOnly = true;
                dgvData.Columns.Add(lgort1Style);

                DataGridViewTextBoxColumn umlgoStyle = new DataGridViewTextBoxColumn();
                umlgoStyle.DataPropertyName = "UMLGO";
                umlgoStyle.HeaderText = "To S.L.";
                umlgoStyle.Width = 50;
                umlgoStyle.ReadOnly = true;
                dgvData.Columns.Add(umlgoStyle);


                DataGridViewTextBoxColumn lifnrStyle = new DataGridViewTextBoxColumn();
                lifnrStyle.DataPropertyName = "LIFNR";
                lifnrStyle.HeaderText = "Vendor";
                lifnrStyle.Width = 90;
                lifnrStyle.ReadOnly = true;
                dgvData.Columns.Add(lifnrStyle);

                DataGridViewTextBoxColumn kostlStyle = new DataGridViewTextBoxColumn();
                kostlStyle.DataPropertyName = "KOSTL";
                kostlStyle.HeaderText = "Dept No.";
                kostlStyle.Width = 90;
                kostlStyle.ReadOnly = true;
                dgvData.Columns.Add(kostlStyle);


                DataGridViewTextBoxColumn crdatStyle = new DataGridViewTextBoxColumn();
                crdatStyle.DataPropertyName = "CRDAT";
                crdatStyle.HeaderText = "Created Date";
                crdatStyle.Width = 110;
                crdatStyle.ReadOnly = true;
                dgvData.Columns.Add(crdatStyle);

                DataGridViewTextBoxColumn dgvcGrloc = new DataGridViewTextBoxColumn();
                dgvcGrloc.DataPropertyName = "GRLOC";
                dgvcGrloc.HeaderText = "101Location";
                dgvcGrloc.ReadOnly = true;
                dgvcGrloc.Width = 100;
                dgvData.Columns.Add(dgvcGrloc);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        private void ShowDataGrid_DID()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            //dgvData.TableStyles.Clear();
            try
            {
                //DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                //mydtgTableStyle.MappingName = dtData.TableName;

                DataGridViewTextBoxColumn mblnrStyle = new DataGridViewTextBoxColumn();
                mblnrStyle.DataPropertyName = "MBLNR";
                mblnrStyle.HeaderText = "Document No";
                mblnrStyle.Width = 130;
                mblnrStyle.ReadOnly = true;
                dgvData.Columns.Add(mblnrStyle);

                DataGridViewTextBoxColumn zeileStyle = new DataGridViewTextBoxColumn();
                zeileStyle.DataPropertyName = "DIDNO";
                zeileStyle.HeaderText = "DID No";
                zeileStyle.Width = 160;
                zeileStyle.ReadOnly = true;
                dgvData.Columns.Add(zeileStyle);

                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                dgvData.Columns.Add(matnrStyle);

                DataGridViewTextBoxColumn insmkStyle = new DataGridViewTextBoxColumn();
                insmkStyle.DataPropertyName = "INSMK";
                insmkStyle.HeaderText = "Stock";
                insmkStyle.Width = 50;
                insmkStyle.ReadOnly = true;
                dgvData.Columns.Add(insmkStyle);

                DataGridViewTextBoxColumn chargStyle = new DataGridViewTextBoxColumn();
                chargStyle.DataPropertyName = "CHARG";
                chargStyle.HeaderText = "Version";
                chargStyle.Width = 50;
                chargStyle.ReadOnly = true;
                dgvData.Columns.Add(chargStyle);

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
                mengeStyle.HeaderText = "Doc. Qty";
                mengeStyle.Width = 80;
                mengeStyle.ReadOnly = true;
                dgvData.Columns.Add(mengeStyle);

                DataGridViewTextBoxColumn otqtyStyle = new DataGridViewTextBoxColumn();
                otqtyStyle.DataPropertyName = "OTQTY";
                otqtyStyle.HeaderText = "In/Out Qty";
                otqtyStyle.Width = 80;
                otqtyStyle.ReadOnly = true;
                dgvData.Columns.Add(otqtyStyle);

                DataGridViewTextBoxColumn bwartStyle = new DataGridViewTextBoxColumn();
                bwartStyle.DataPropertyName = "REFID";
                bwartStyle.HeaderText = "REFID";
                bwartStyle.Width = 150;
                bwartStyle.ReadOnly = true;
                dgvData.Columns.Add(bwartStyle);

                DataGridViewTextBoxColumn lgort1Style = new DataGridViewTextBoxColumn();
                lgort1Style.DataPropertyName = "LGORT";
                lgort1Style.HeaderText = "From S.L.";
                lgort1Style.Width = 50;
                lgort1Style.ReadOnly = true;
                dgvData.Columns.Add(lgort1Style);

                DataGridViewTextBoxColumn lifnrStyle = new DataGridViewTextBoxColumn();
                lifnrStyle.DataPropertyName = "LIFNR";
                lifnrStyle.HeaderText = "Vendor";
                lifnrStyle.Width = 90;
                lifnrStyle.ReadOnly = true;
                dgvData.Columns.Add(lifnrStyle);

                DataGridViewTextBoxColumn kostlStyle = new DataGridViewTextBoxColumn();
                kostlStyle.DataPropertyName = "KOSTL";
                kostlStyle.HeaderText = "Dept No.";
                kostlStyle.Width = 90;
                kostlStyle.ReadOnly = true;
                dgvData.Columns.Add(kostlStyle);


                DataGridViewTextBoxColumn crdatStyle = new DataGridViewTextBoxColumn();
                crdatStyle.DataPropertyName = "CRDAT";
                crdatStyle.HeaderText = "Created Date";
                crdatStyle.Width = 110;
                crdatStyle.ReadOnly = true;
                dgvData.Columns.Add(crdatStyle);


                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        private void ShowDataGrid_XY()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            //dgvData.TableStyles.Clear();
            try
            {
                //DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                //mydtgTableStyle.MappingName = dtData.TableName;

                DataGridViewTextBoxColumn mblnrStyle = new DataGridViewTextBoxColumn();
                mblnrStyle.DataPropertyName = "MBLNR";
                mblnrStyle.HeaderText = "Document No";
                mblnrStyle.Width = 130;
                mblnrStyle.ReadOnly = true;
                dgvData.Columns.Add(mblnrStyle);

                DataGridViewTextBoxColumn zeileStyle = new DataGridViewTextBoxColumn();
                zeileStyle.DataPropertyName = "MTYPE";
                zeileStyle.HeaderText = "MTYPE";
                zeileStyle.Width = 160;
                zeileStyle.ReadOnly = true;
                dgvData.Columns.Add(zeileStyle);

                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                dgvData.Columns.Add(matnrStyle);

                DataGridViewTextBoxColumn insmkStyle = new DataGridViewTextBoxColumn();
                insmkStyle.DataPropertyName = "INSMK";
                insmkStyle.HeaderText = "Stock";
                insmkStyle.Width = 50;
                insmkStyle.ReadOnly = true;
                dgvData.Columns.Add(insmkStyle);

                DataGridViewTextBoxColumn chargStyle = new DataGridViewTextBoxColumn();
                chargStyle.DataPropertyName = "CHARG";
                chargStyle.HeaderText = "Version";
                chargStyle.Width = 50;
                chargStyle.ReadOnly = true;
                dgvData.Columns.Add(chargStyle);

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
                mengeStyle.HeaderText = "Doc. Qty";
                mengeStyle.Width = 80;
                mengeStyle.ReadOnly = true;
                dgvData.Columns.Add(mengeStyle);

                DataGridViewTextBoxColumn otqtyStyle = new DataGridViewTextBoxColumn();
                otqtyStyle.DataPropertyName = "OTQTY";
                otqtyStyle.HeaderText = "In/Out Qty";
                otqtyStyle.Width = 80;
                otqtyStyle.ReadOnly = true;
                dgvData.Columns.Add(otqtyStyle);

                DataGridViewTextBoxColumn bwartStyle = new DataGridViewTextBoxColumn();
                bwartStyle.DataPropertyName = "REFID";
                bwartStyle.HeaderText = "REFID";
                bwartStyle.Width = 150;
                bwartStyle.ReadOnly = true;
                dgvData.Columns.Add(bwartStyle);

                DataGridViewTextBoxColumn lgort1Style = new DataGridViewTextBoxColumn();
                lgort1Style.DataPropertyName = "LGORT";
                lgort1Style.HeaderText = "From S.L.";
                lgort1Style.Width = 50;
                lgort1Style.ReadOnly = true;
                dgvData.Columns.Add(lgort1Style);

                DataGridViewTextBoxColumn lifnrStyle = new DataGridViewTextBoxColumn();
                lifnrStyle.DataPropertyName = "LIFNR";
                lifnrStyle.HeaderText = "Vendor";
                lifnrStyle.Width = 90;
                lifnrStyle.ReadOnly = true;
                dgvData.Columns.Add(lifnrStyle);

                DataGridViewTextBoxColumn kostlStyle = new DataGridViewTextBoxColumn();
                kostlStyle.DataPropertyName = "BWART";
                kostlStyle.HeaderText = "BWART";
                kostlStyle.Width = 90;
                kostlStyle.ReadOnly = true;
                dgvData.Columns.Add(kostlStyle);


                DataGridViewTextBoxColumn crdatStyle = new DataGridViewTextBoxColumn();
                crdatStyle.DataPropertyName = "BOXID";
                crdatStyle.HeaderText = "BOX ID";
                crdatStyle.Width = 110;
                crdatStyle.ReadOnly = true;
                dgvData.Columns.Add(crdatStyle);


                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        private void ShowDataGrid_GR()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            //dgvData.TableStyles.Clear();
            try
            {
                //DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                //mydtgTableStyle.MappingName = dtData.TableName;

                DataGridViewTextBoxColumn mblnrStyle = new DataGridViewTextBoxColumn();
                mblnrStyle.DataPropertyName = "MBLNR";
                mblnrStyle.HeaderText = "Document No";
                mblnrStyle.Width = 130;
                mblnrStyle.ReadOnly = true;
                dgvData.Columns.Add(mblnrStyle);


                DataGridViewTextBoxColumn matnrStyle = new DataGridViewTextBoxColumn();
                matnrStyle.DataPropertyName = "MATNR";
                matnrStyle.HeaderText = "Part No";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                dgvData.Columns.Add(matnrStyle);


                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
                mengeStyle.HeaderText = "Doc. Qty";
                mengeStyle.Width = 80;
                mengeStyle.ReadOnly = true;
                dgvData.Columns.Add(mengeStyle);


                DataGridViewTextBoxColumn lgort1Style = new DataGridViewTextBoxColumn();
                lgort1Style.DataPropertyName = "LGORT";
                lgort1Style.HeaderText = "From S.L.";
                lgort1Style.Width = 50;
                lgort1Style.ReadOnly = true;
                dgvData.Columns.Add(lgort1Style);

                DataGridViewTextBoxColumn umlgoStyle = new DataGridViewTextBoxColumn();
                umlgoStyle.DataPropertyName = "ALQTY";
                umlgoStyle.HeaderText = "ALQTY";
                umlgoStyle.Width = 50;
                umlgoStyle.ReadOnly = true;
                dgvData.Columns.Add(umlgoStyle);


                DataGridViewTextBoxColumn crdatStyle = new DataGridViewTextBoxColumn();
                crdatStyle.DataPropertyName = "CRDAT";
                crdatStyle.HeaderText = "Created Date";
                crdatStyle.Width = 110;
                crdatStyle.ReadOnly = true;
                dgvData.Columns.Add(crdatStyle);

                DataGridViewTextBoxColumn dgvcGrloc = new DataGridViewTextBoxColumn();
                dgvcGrloc.DataPropertyName = "CRNAM";
                dgvcGrloc.HeaderText = "CRNAM";
                dgvcGrloc.ReadOnly = true;
                dgvcGrloc.Width = 100;
                dgvData.Columns.Add(dgvcGrloc);

                DataGridViewTextBoxColumn DGVuid = new DataGridViewTextBoxColumn();
                DGVuid.DataPropertyName = "UID";
                DGVuid.HeaderText = "UID";
                DGVuid.ReadOnly = true;
                DGVuid.Width = 100;
                dgvData.Columns.Add(DGVuid);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
		private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			string strStartDate = "";
			string strEndDate = "";
			string strStartHour = "";
			string strEndHour = "";
			string strWerks = "";
			string strLgort = "";
			string strInsmk = "";
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
				if(cmbInsmk.SelectedIndex != -1)
				{
					strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
				}
				if(cmbStartHour.SelectedIndex != -1)
				{
					strStartHour = Convert.ToDecimal(cmbStartHour.Items[cmbStartHour.SelectedIndex]).ToString("00") + ":00:00";
				}
				else
				{
					strStartHour = "00:00:00";
				}
				if(cmbEndHour.SelectedIndex != -1)
				{
					strEndHour = Convert.ToDecimal(cmbEndHour.Items[cmbEndHour.SelectedIndex].ToString()).ToString("00") + ":00:00";
				}
				else
				{
					strEndHour = "23:59:59";
				}

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
				else
				{
					stsWarning.Text = "MType can't be empty!!";
					return;
                }

                #region 提供重慶W/H可以By廠區(不選倉別)查詢SAP單據號碼並Download成Excel檔
                if (Comcd == "9110" || Comcd == "9200"|| Comcd =="2281")
                {
                    if (strWerks == "")
                    {
                        stsWarning.Text = "Plant can't be empty!!";
                        return;
                    }
                }
                else
                {
                    if (strWerks == "" || strLgort == "")
                    {
                        stsWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                }
                #endregion

                string strQueryTable = "WHDWN WITH (NOLOCK)";
				if(chkOldData.Checked)
				{
                    strQueryTable = "WHDWN_BAK WITH (NOLOCK)";
				}

                objAlim_StorageLogQuery = new Alim_StorageLogQuery(UserData, strWerks, strLgort);
				//抓QSMS資料(WHRID)
				if(strMType.ToUpper().Trim() == "QSMS(SMT)")
				{
                    dtData = objAlim_StorageLogQuery.QueryQsmsData(strInsmk, strStartDate, strEndDate, txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), txtStartMblnr.Text.Trim(), txtEndMblnr.Text.Trim(), this.cmbType.SelectedIndex, this.cmbGRGI.SelectedIndex, this.txtBwart.Text.Trim(), strMType.Trim());

				}
				else //抓WHDWN資料
				{
                    dtData = objAlim_StorageLogQuery.QuerySapData(strInsmk, strStartDate, strEndDate, txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), txtStartMblnr.Text.Trim(), txtEndMblnr.Text.Trim(), this.cmbType.SelectedIndex, this.cmbGRGI.SelectedIndex, this.txtBwart.Text.Trim(), strMType.Trim(), strQueryTable);
				}
				if(dtData.Rows.Count == 0)
				{
					stsWarning.Text = "No Data!!";
                    ShowDataGrid();
					return;
				}
				else
				{
						
				}
				dtData.DefaultView.Sort = "MANDT, COMCD, WERKS, LGORT, MBLNR";
				ShowDataGrid();
                strType = "SAPConfirm";
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.cmbInsmk.SelectedIndex = -1;
				this.cmbStartHour.SelectedIndex = -1;
				this.cmbEndHour.SelectedIndex = -1;
				this.cmbMType.SelectedIndex = -1;
				this.cmbType.SelectedIndex = 2;
				this.cmbGRGI.SelectedIndex = 2;
				this.txtStartMblnr.Text = "";
				this.txtEndMblnr.Text = "";
				this.txtStartMatnr.Text = "";
				this.txtEndMatnr.Text = "";
				this.dtpStartDate.Value = DateTime.Now;
				this.dtpEndDate.Value = DateTime.Now;
				this.panel1.Enabled = true;
				this.dgvData.DataSource = null;
				this.dtData.Rows.Clear();
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;
				return;
			}
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void Manage_LogQuery_Resize(object sender, System.EventArgs e)
		{
			panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.25), panel3.Size.Height);
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
				strLine = "Document No\tDocument Item\tType\tPart No\tStock\tVersion\tDoc. Qty\tIn/Out Qty\tMovement Type\tFrom S.L.\tTo S.L.\tSAP User\tVendor\tDept No.\tKDMat\tCreated Date";
				sw.WriteLine(strLine);
                //Reading data
                switch (strType)
                {
                    case "SAPConfirm":
                        #region SAPDownload
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {

                            strLine = "";
                            strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["ZEILE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["TRNTP"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["OTQTY"].ToString() + "\t";
                            strLine += dtData.Rows[i]["BWART"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["UMLGO"].ToString() + "\t";
                            strLine += dtData.Rows[i]["USNAM"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LIFNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["KOSTL"].ToString() + "\t";
                            //20051013 marc add KDMAT Field
                            strLine += dtData.Rows[i]["KDMAT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CRDAT"].ToString();
                            sw.WriteLine(strLine);
                        }
                        #endregion
                        break;
                    case "XLConfirm":
                        #region XLDownload
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {

                            strLine = "";
                            strLine += dtData.Rows[i]["MANDT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["COMCD"].ToString() + "\t";
                            strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["KOSTL"].ToString() + "\t";
                            strLine += dtData.Rows[i]["GRPID"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["charg"].ToString() + "\t";
                            strLine += dtData.Rows[i]["menge"].ToString() + "\t";
                            strLine += dtData.Rows[i]["serno"].ToString() + "\t";
                            strLine += dtData.Rows[i]["lifnr"].ToString() + "\t";
                            strLine += dtData.Rows[i]["dacod"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LOCOD"].ToString() + "\t";
                            //20051013 marc add KDMAT Field
                            strLine += dtData.Rows[i]["DIDNO"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["ZEILE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["UMLGO"].ToString() + "\t";
                            strLine += dtData.Rows[i]["TRDAT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CRNAM"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CRDAT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["FLAGE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REMARK1"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REMARK2"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REMARK3"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REMARK4"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REMARK5"].ToString() + "\t";
                            strLine += dtData.Rows[i]["UID"].ToString();
                            sw.WriteLine(strLine);
                        }
                        #endregion
                        break;
                    case "XYConfirm":
                        #region XYDownload
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {

                            strLine = "";
                            strLine += dtData.Rows[i]["MTYPE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["ZEILE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LIFNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["OTQTY"].ToString() + "\t";
                            strLine += dtData.Rows[i]["KOSTL"].ToString() + "\t";
                            strLine += dtData.Rows[i]["BWART"].ToString() + "\t";
                            strLine += dtData.Rows[i]["KDMAT"].ToString() + "\t";
                            //20051013 marc add KDMAT Field
                            strLine += dtData.Rows[i]["REFID"].ToString() + "\t";
                            strLine += dtData.Rows[i]["COMCD"].ToString() + "\t";
                            strLine += dtData.Rows[i]["BOXID"].ToString();
                            sw.WriteLine(strLine);
                        }
                        #endregion
                        break;
                    case "DIDConfirm":
                        #region DIDDownload
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {

                            strLine = "";
                            strLine += dtData.Rows[i]["MANDT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["COMCD"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REFID"].ToString() + "\t";
                            strLine += dtData.Rows[i]["DIDNO"].ToString() + "\t";
                            strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["RUNNER"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["OTQTY"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["OTWRK"].ToString() + "\t";
                            strLine += dtData.Rows[i]["OTLGT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LIFNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CRDAT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MANDT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["OMBLN"].ToString() + "\t";
                            strLine += dtData.Rows[i]["KOSTL"].ToString() + "\t";
                            //20051013 marc add KDMAT Field
                            strLine += dtData.Rows[i]["UPDAT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["DACOD"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LOCOD"].ToString() + "\t";
                            strLine += dtData.Rows[i]["SERNO"].ToString() + "\t";
                            strLine += dtData.Rows[i]["ULFLG"].ToString() + "\t";
                            strLine += dtData.Rows[i]["Msg"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REMARK1"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REMARK2"].ToString() + "\t";
                            strLine += dtData.Rows[i]["REMARK3"].ToString() + "\t";
                            strLine += dtData.Rows[i]["UID"].ToString();
                            sw.WriteLine(strLine);
                        }
                        #endregion
                        break;
                    case "GRConfirm":
                        #region GRDownload
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {

                            strLine = "";
                            strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                            strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                            strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                            strLine += dtData.Rows[i]["ALQTY"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CRNAM"].ToString() + "\t";
                            strLine += dtData.Rows[i]["CRDAT"].ToString() + "\t";
                            strLine += dtData.Rows[i]["Remark1"].ToString() + "\t";
                            strLine += dtData.Rows[i]["UID"].ToString() + "\t";
                            sw.WriteLine(strLine);
                        }
                        #endregion
                        break;
                    default:
                        return;                
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
        #region 蔥韓
        private void ShowDdlWerks_XL()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks_XL.Items.Clear();
                dtTemp = objAlim_StorageLogQuery.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks_XL.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        private void ShowDdlLgort_XL()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks_XL.SelectedIndex != -1)
                {
                    strWerks = cmbWerks_XL.Items[cmbWerks_XL.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority();
                }
                if (cmbLgort_XL.SelectedIndex != -1)
                {
                    strLgort = cmbLgort_XL.Items[cmbLgort_XL.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort_XL.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort_XL.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort_XL.Items.Clear();
                    #region 提供重慶W/H可以By廠區(不選倉別)查詢SAP單據號碼並Download成Excel檔
                    if (Comcd == "9110" || Comcd == "9200" || Comcd == "2281")
                    {
                        cmbLgort_XL.Items.Add("");
                    }
                    #endregion
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort_XL.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort_XL.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void btnConfirm_XL_Click(object sender, EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            try
            {
                if (cmbWerks_XL.SelectedIndex != -1)
                {
                    strWerks = cmbWerks_XL.Items[cmbWerks_XL.SelectedIndex].ToString();
                }
                if (cmbLgort_XL.SelectedIndex != -1)
                {
                    strLgort = cmbLgort_XL.Items[cmbLgort_XL.SelectedIndex].ToString();
                }
               
                //tbXLID
                dtData = objAlim_StorageLogQuery.QueryXLData(strWerks, strLgort, tbXLID.Text.Trim());
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    //strType = "XLConfirm";
                    ShowDataGrid_XL();
                    return;
                }
                ShowDataGrid_XL();
                strType = "XLConfirm";
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion
        #region 船祑
        private void ShowDdlWerks_XY()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks_XY.Items.Clear();
                dtTemp = objAlim_StorageLogQuery.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks_XY.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        private void ShowDdlLgort_XY()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks_XY.SelectedIndex != -1)
                {
                    strWerks = cmbWerks_XY.Items[cmbWerks_XY.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority();
                }
                if (cmbLgort_XY.SelectedIndex != -1)
                {
                    strLgort = cmbLgort_XY.Items[cmbLgort_XY.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort_XY.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort_XY.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort_XY.Items.Clear();
                    #region 提供重慶W/H可以By廠區(不選倉別)查詢SAP單據號碼並Download成Excel檔
                    if (Comcd == "9110" || Comcd == "9200" || Comcd == "2281")
                    {
                        cmbLgort_XY.Items.Add("");
                    }
                    #endregion
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort_XY.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort_XY.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        private void ShowDdlInsmk_XY()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbInsmk_XY.Items.Clear();
                dtTemp = objAlim_StorageLogQuery.GetDdlInsmk();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbInsmk_XY.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
            }
        }

        private void btnConfirm_XY_Click(object sender, EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            string strInsmk = "";

            try
            {
                if (cmbWerks_XY.SelectedIndex != -1)
                {
                    strWerks = cmbWerks_XY.Items[cmbWerks_XY.SelectedIndex].ToString();
                }
                if (cmbLgort_XY.SelectedIndex != -1)
                {
                    strLgort = cmbLgort_XY.Items[cmbLgort_XY.SelectedIndex].ToString();
                }
                if (cmbInsmk_XY.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk_XY.Items[cmbInsmk_XY.SelectedIndex].ToString();
                }

                //tbXLID
                dtData = objAlim_StorageLogQuery.QueryXYData(strWerks, strLgort, strInsmk, tbXYNo.Text.Trim(), tbXYGrNo.Text.Trim());
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    ShowDataGrid_XY();
                    return;
                }
                ShowDataGrid_XY();
                strType = "XYConfirm";
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion
        #region DID脤戙
        private void ShowDdlWerks_DID()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks_DID.Items.Clear();
                dtTemp = objAlim_StorageLogQuery.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks_DID.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        private void ShowDdlLgort_DID()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks_DID.SelectedIndex != -1)
                {
                    strWerks = cmbWerks_DID.Items[cmbWerks_DID.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority();
                }
                if (cmbLgort_DID.SelectedIndex != -1)
                {
                    strLgort = cmbLgort_DID.Items[cmbLgort_DID.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort_DID.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort_DID.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort_DID.Items.Clear();
                    #region 提供重慶W/H可以By廠區(不選倉別)查詢SAP單據號碼並Download成Excel檔
                    if (Comcd == "9110" || Comcd == "9200" || Comcd == "2281")
                    {
                        cmbLgort_DID.Items.Add("");
                    }
                    #endregion
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort_DID.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort_DID.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        private void ShowDdlInsmk_DID()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbInsmk_DID.Items.Clear();
                dtTemp = objAlim_StorageLogQuery.GetDdlInsmk();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbInsmk_DID.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
            }
        }

        private void btnConfirm_DID_Click(object sender, EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            string strInsmk = "";
            try
            {
                if (cmbWerks_DID.SelectedIndex != -1)
                {
                    strWerks = cmbWerks_DID.Items[cmbWerks_DID.SelectedIndex].ToString();
                }
                if (cmbLgort_DID.SelectedIndex != -1)
                {
                    strLgort = cmbLgort_DID.Items[cmbLgort_DID.SelectedIndex].ToString();
                }
                if (cmbInsmk_DID.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk_DID.Items[cmbInsmk_DID.SelectedIndex].ToString();
                }

                //tbXLID
                dtData = objAlim_StorageLogQuery.QueryDIDData(strWerks, strLgort, strInsmk, tbDIDNo.Text.Trim(), tbXNDJ.Text.Trim());
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    ShowDataGrid_DID();
                    return;
                }
                ShowDataGrid_DID();
                strType = "DIDConfirm";
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion
        #region GR等擂脤戙
        private void ShowDdlWerks_GR()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks_GR.Items.Clear();
                dtTemp = objAlim_StorageLogQuery.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks_GR.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        private void ShowDdlLgort_GR()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks_GR.SelectedIndex != -1)
                {
                    strWerks = cmbWerks_GR.Items[cmbWerks_GR.SelectedIndex].ToString();
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAlim_StorageLogQuery.CheckLgortAuthority();
                }
                if (cmbLgort_GR.SelectedIndex != -1)
                {
                    strLgort = cmbLgort_GR.Items[cmbLgort_GR.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort_GR.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort_GR.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort_GR.Items.Clear();
                    #region 提供重慶W/H可以By廠區(不選倉別)查詢SAP單據號碼並Download成Excel檔
                    if (Comcd == "9110" || Comcd == "9200" || Comcd == "2281")
                    {
                        cmbLgort_GR.Items.Add("");
                    }
                    #endregion
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort_GR.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort_GR.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        private void btnConfirm_GR_Click(object sender, EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            try
            {
                if (cmbWerks_GR.SelectedIndex != -1)
                {
                    strWerks = cmbWerks_GR.Items[cmbWerks_GR.SelectedIndex].ToString();
                }
                if (cmbLgort_GR.SelectedIndex != -1)
                {
                    strLgort = cmbLgort_GR.Items[cmbLgort_GR.SelectedIndex].ToString();
                }

                //tbXLID
                dtData = objAlim_StorageLogQuery.QueryGRData(strWerks, strLgort, tbGRID.Text.Trim());
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    ShowDataGrid_GR();
                    //strType = "XLConfirm";
                    return;
                }
                ShowDataGrid_GR();
                strType = "GRConfirm";
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;

                return;
            }
        }
        #endregion
    }
}
