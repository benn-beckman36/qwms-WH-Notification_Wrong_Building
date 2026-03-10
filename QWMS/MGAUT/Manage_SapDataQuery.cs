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

namespace QWMS
{
	/// <summary>
	/// Manage_SapDataQuery 的摘要描述。
	/// </summary>
	public class Manage_SapDataQuery : System.Windows.Forms.Form
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
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.ComboBox cmbEndHour;
		private System.Windows.Forms.DateTimePicker dtpEndDate;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtEndMatnr;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.ComboBox cmbStartHour;
		private System.Windows.Forms.DateTimePicker dtpStartDate;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtStartMatnr;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ComboBox cmbInsmk;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cmbLgort;
		private System.Windows.Forms.ComboBox cmbWerks;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
		protected internal System.Windows.Forms.StatusBar stbStatus;
		private System.Windows.Forms.TextBox txtStartMblnr;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtEndMblnr;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox cmbType;
		private System.Windows.Forms.ComboBox cmbGRGI;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox chkDate;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtBwart;

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
		private StorageIn objStorageIn;
		private PlantData objPlantData;
		private Authority objAuthority;
		private SapData objSapData;
        //private AccessConfig objConfig;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ComboBox cmbMType;
		private System.Windows.Forms.CheckBox chkOldData;
		private System.Windows.Forms.Label label14;
        private DataGridView dgvData;
        private Label lblData;
		

		/// <summary>
		/// 設計工具所需的變數。
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

		public Manage_SapDataQuery(UserInfo _UserData, string strProgid)
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
				objStorageIn = new StorageIn(UserData, Progid);
				objPlantData = new PlantData(UserData);
				objAuthority = new Authority(UserData);

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
					ShowDdlInsmk();
					ShowDdlStartEndHour();
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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
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
            this.panel2.Location = new System.Drawing.Point(0, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1373, 291);
            this.panel2.TabIndex = 40;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Location = new System.Drawing.Point(25, 4);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(81, 17);
            this.lblData.TabIndex = 47;
            this.lblData.Text = "0 records";
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(25, 239);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(128, 39);
            this.btnDownload.TabIndex = 44;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(167, 239);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 39);
            this.btnRefresh.TabIndex = 16;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(295, 239);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 39);
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
            this.dgvData.Location = new System.Drawing.Point(25, 37);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(1336, 196);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1373, 156);
            this.panel1.TabIndex = 39;
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
            this.panel6.Location = new System.Drawing.Point(963, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(410, 156);
            this.panel6.TabIndex = 67;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.Location = new System.Drawing.Point(0, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 27);
            this.label12.TabIndex = 58;
            this.label12.Text = "Mvt";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBwart
            // 
            this.txtBwart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBwart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBwart.Location = new System.Drawing.Point(64, 96);
            this.txtBwart.MaxLength = 14;
            this.txtBwart.Name = "txtBwart";
            this.txtBwart.Size = new System.Drawing.Size(115, 25);
            this.txtBwart.TabIndex = 57;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.Location = new System.Drawing.Point(0, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 28);
            this.label6.TabIndex = 56;
            this.label6.Text = "To";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndMblnr
            // 
            this.txtEndMblnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndMblnr.Location = new System.Drawing.Point(64, 9);
            this.txtEndMblnr.MaxLength = 20;
            this.txtEndMblnr.Name = "txtEndMblnr";
            this.txtEndMblnr.Size = new System.Drawing.Size(231, 25);
            this.txtEndMblnr.TabIndex = 55;
            // 
            // cmbEndHour
            // 
            this.cmbEndHour.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmbEndHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndHour.Location = new System.Drawing.Point(231, 67);
            this.cmbEndHour.Name = "cmbEndHour";
            this.cmbEndHour.Size = new System.Drawing.Size(64, 26);
            this.cmbEndHour.TabIndex = 10;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(64, 67);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(167, 25);
            this.dtpEndDate.TabIndex = 9;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(295, 105);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(102, 39);
            this.btnConfirm.TabIndex = 14;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.Location = new System.Drawing.Point(0, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 27);
            this.label7.TabIndex = 51;
            this.label7.Text = "To";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndMatnr
            // 
            this.txtEndMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndMatnr.Location = new System.Drawing.Point(64, 39);
            this.txtEndMatnr.MaxLength = 20;
            this.txtEndMatnr.Name = "txtEndMatnr";
            this.txtEndMatnr.Size = new System.Drawing.Size(231, 25);
            this.txtEndMatnr.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(0, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 28);
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
            this.panel5.Location = new System.Drawing.Point(269, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1104, 156);
            this.panel5.TabIndex = 66;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.Location = new System.Drawing.Point(39, 125);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(294, 28);
            this.label14.TabIndex = 60;
            this.label14.Text = "Query Old Data(1 months ago)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkOldData
            // 
            this.chkOldData.Location = new System.Drawing.Point(13, 125);
            this.chkOldData.Name = "chkOldData";
            this.chkOldData.Size = new System.Drawing.Size(26, 28);
            this.chkOldData.TabIndex = 59;
            // 
            // chkDate
            // 
            this.chkDate.Location = new System.Drawing.Point(13, 67);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(26, 29);
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
            this.cmbGRGI.Location = new System.Drawing.Point(192, 96);
            this.cmbGRGI.Name = "cmbGRGI";
            this.cmbGRGI.Size = new System.Drawing.Size(450, 26);
            this.cmbGRGI.TabIndex = 57;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.Location = new System.Drawing.Point(103, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 27);
            this.label11.TabIndex = 56;
            this.label11.Text = "GR/GI";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartMblnr
            // 
            this.txtStartMblnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartMblnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartMblnr.Location = new System.Drawing.Point(192, 9);
            this.txtStartMblnr.MaxLength = 20;
            this.txtStartMblnr.Name = "txtStartMblnr";
            this.txtStartMblnr.Size = new System.Drawing.Size(497, 25);
            this.txtStartMblnr.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(25, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 28);
            this.label3.TabIndex = 55;
            this.label3.Text = "Document From";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbStartHour
            // 
            this.cmbStartHour.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmbStartHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartHour.Location = new System.Drawing.Point(625, 67);
            this.cmbStartHour.Name = "cmbStartHour";
            this.cmbStartHour.Size = new System.Drawing.Size(64, 26);
            this.cmbStartHour.TabIndex = 8;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(192, 67);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(450, 25);
            this.dtpStartDate.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.Location = new System.Drawing.Point(25, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(167, 28);
            this.label9.TabIndex = 53;
            this.label9.Text = "Download From";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartMatnr
            // 
            this.txtStartMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartMatnr.Location = new System.Drawing.Point(192, 39);
            this.txtStartMatnr.MaxLength = 20;
            this.txtStartMatnr.Name = "txtStartMatnr";
            this.txtStartMatnr.Size = new System.Drawing.Size(497, 25);
            this.txtStartMatnr.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.Location = new System.Drawing.Point(39, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 27);
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
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(269, 156);
            this.panel3.TabIndex = 65;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label13.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(25, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 28);
            this.label13.TabIndex = 49;
            this.label13.Text = "Type";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMType
            // 
            this.cmbMType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMType.ItemHeight = 18;
            this.cmbMType.Items.AddRange(new object[] {
            "SAP",
            "QMS",
            "SDS",
            "QSMS(SMT)",
            "S/F(PCBA)"});
            this.cmbMType.Location = new System.Drawing.Point(115, 125);
            this.cmbMType.Name = "cmbMType";
            this.cmbMType.Size = new System.Drawing.Size(141, 26);
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
            this.cmbType.Location = new System.Drawing.Point(115, 96);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(141, 26);
            this.cmbType.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.Location = new System.Drawing.Point(25, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 27);
            this.label10.TabIndex = 46;
            this.label10.Text = "Status";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbInsmk
            // 
            this.cmbInsmk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInsmk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsmk.Location = new System.Drawing.Point(115, 67);
            this.cmbInsmk.Name = "cmbInsmk";
            this.cmbInsmk.Size = new System.Drawing.Size(141, 26);
            this.cmbInsmk.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Location = new System.Drawing.Point(25, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 28);
            this.label5.TabIndex = 45;
            this.label5.Text = "Stock";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLgort
            // 
            this.cmbLgort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Location = new System.Drawing.Point(115, 39);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(141, 26);
            this.cmbLgort.TabIndex = 1;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(115, 9);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(141, 26);
            this.cmbWerks.TabIndex = 0;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(25, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 27);
            this.label2.TabIndex = 39;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 28);
            this.label1.TabIndex = 38;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.stbStatus.Location = new System.Drawing.Point(0, 447);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1373, 26);
            this.stbStatus.TabIndex = 38;
            this.stbStatus.Text = "Status";
            // 
            // Manage_SapDataQuery
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(1373, 473);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Name = "Manage_SapDataQuery";
            this.Text = "SAP Material Documents Inquiry";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
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
				dtTemp = objPlantData.GetDdlInsmk();
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

				objSapData = new SapData(UserData, strWerks, strLgort);
				//抓QSMS資料(WHRID)
				if(strMType.ToUpper().Trim() == "QSMS(SMT)")
				{
					dtData = objSapData.QueryQsmsData(strInsmk, strStartDate, strEndDate, txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), txtStartMblnr.Text.Trim(), txtEndMblnr.Text.Trim(), this.cmbType.SelectedIndex, this.cmbGRGI.SelectedIndex, this.txtBwart.Text.Trim(),strMType.Trim());

				}
				else //抓WHDWN資料
				{
					dtData = objSapData.QuerySapData(strInsmk, strStartDate, strEndDate, txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), txtStartMblnr.Text.Trim(), txtEndMblnr.Text.Trim(), this.cmbType.SelectedIndex, this.cmbGRGI.SelectedIndex, this.txtBwart.Text.Trim(),strMType.Trim(), strQueryTable);
				}
				if(dtData.Rows.Count == 0)
				{
					stsWarning.Text = "No Data!!";
					return;
				}
				else
				{
						
				}
				dtData.DefaultView.Sort = "MANDT, COMCD, WERKS, LGORT, MBLNR";
				ShowDataGrid();
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
				for(int i=0; i< dtData.Rows.Count; i++)
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
