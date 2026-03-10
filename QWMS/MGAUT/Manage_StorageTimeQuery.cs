using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
	/// <summary>
	/// Manage_StorageTimeQuery 的摘要描述。
	/// </summary>
	public class Manage_StorageTimeQuery : System.Windows.Forms.Form
	{
        UserInfo UserData = new UserInfo();
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnExit;

		private System.Windows.Forms.StatusBarPanel stsDate;
		private System.Windows.Forms.StatusBarPanel stsWarning;
		
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Panel panel5;
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
		protected internal System.Windows.Forms.StatusBar stbStatus;
		private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
		private System.Windows.Forms.SaveFileDialog sfdSaveFile;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtEndMatnr;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cmbType1;
		private System.Windows.Forms.ComboBox cmbComparsion;
		private System.Windows.Forms.ComboBox cmbType2;

		private string strMandt = "";
        private string strComcd = "";
		private string strUsrnm = "";
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
		private StorageData objStorageData;
        //private AccessConfig objConfig;
		private System.Windows.Forms.TextBox txtStartDate;
		private System.Windows.Forms.TextBox txtEndDate;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtStartLocat;
		private System.Windows.Forms.TextBox txtEndLocat;
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

		public Manage_StorageTimeQuery(UserInfo _UserData, string strProgid)
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
                    ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
					ShowDdlInsmk();
					this.cmbType1.SelectedIndex = 0;
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblData = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEndLocat = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEndMatnr = new System.Windows.Forms.TextBox();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtStartLocat = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbComparsion = new System.Windows.Forms.ComboBox();
            this.cmbType1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtStartMatnr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbType2 = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbInsmk = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblData);
            this.panel2.Controls.Add(this.btnDownload);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.dgvData);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(692, 473);
            this.panel2.TabIndex = 43;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblData.Location = new System.Drawing.Point(19, 115);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(61, 14);
            this.lblData.TabIndex = 47;
            this.lblData.Text = "0 records";
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownload.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(19, 420);
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
            this.btnRefresh.Location = new System.Drawing.Point(125, 420);
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
            this.btnExit.Location = new System.Drawing.Point(221, 420);
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
            this.dgvData.Location = new System.Drawing.Point(19, 131);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(659, 284);
            this.dgvData.TabIndex = 46;
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
            // stsUsrnm
            // 
            this.stsUsrnm.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsUsrnm.Name = "stsUsrnm";
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
            this.panel1.Size = new System.Drawing.Size(692, 112);
            this.panel1.TabIndex = 42;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.txtEndLocat);
            this.panel6.Controls.Add(this.btnConfirm);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.txtEndMatnr);
            this.panel6.Controls.Add(this.txtStartDate);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.txtEndDate);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(385, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(307, 112);
            this.panel6.TabIndex = 67;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 21);
            this.label10.TabIndex = 53;
            this.label10.Text = "To";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndLocat
            // 
            this.txtEndLocat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndLocat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndLocat.Location = new System.Drawing.Point(48, 30);
            this.txtEndLocat.MaxLength = 11;
            this.txtEndLocat.Name = "txtEndLocat";
            this.txtEndLocat.Size = new System.Drawing.Size(173, 21);
            this.txtEndLocat.TabIndex = 52;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(144, 60);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(77, 30);
            this.btnConfirm.TabIndex = 14;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 22);
            this.label7.TabIndex = 51;
            this.label7.Text = "To";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndMatnr
            // 
            this.txtEndMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndMatnr.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndMatnr.Location = new System.Drawing.Point(48, 7);
            this.txtEndMatnr.MaxLength = 20;
            this.txtEndMatnr.Name = "txtEndMatnr";
            this.txtEndMatnr.Size = new System.Drawing.Size(173, 21);
            this.txtEndMatnr.TabIndex = 6;
            // 
            // txtStartDate
            // 
            this.txtStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartDate.Location = new System.Drawing.Point(0, 52);
            this.txtStartDate.MaxLength = 14;
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(67, 21);
            this.txtStartDate.TabIndex = 58;
            this.txtStartDate.Text = "9";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Location = new System.Drawing.Point(67, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 22);
            this.label3.TabIndex = 60;
            this.label3.Text = "Day";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtEndDate
            // 
            this.txtEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEndDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEndDate.Location = new System.Drawing.Point(0, 75);
            this.txtEndDate.MaxLength = 14;
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(67, 21);
            this.txtEndDate.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.Location = new System.Drawing.Point(67, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 21);
            this.label4.TabIndex = 61;
            this.label4.Text = "Day";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtStartLocat);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.cmbComparsion);
            this.panel5.Controls.Add(this.cmbType1);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.txtStartMatnr);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.cmbType2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(202, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(490, 112);
            this.panel5.TabIndex = 66;
            // 
            // txtStartLocat
            // 
            this.txtStartLocat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartLocat.Location = new System.Drawing.Point(144, 30);
            this.txtStartLocat.MaxLength = 11;
            this.txtStartLocat.Name = "txtStartLocat";
            this.txtStartLocat.Size = new System.Drawing.Size(25, 21);
            this.txtStartLocat.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.Location = new System.Drawing.Point(29, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 21);
            this.label6.TabIndex = 63;
            this.label6.Text = "Location From";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbComparsion
            // 
            this.cmbComparsion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbComparsion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComparsion.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.cmbComparsion.Location = new System.Drawing.Point(77, 75);
            this.cmbComparsion.Name = "cmbComparsion";
            this.cmbComparsion.Size = new System.Drawing.Size(67, 23);
            this.cmbComparsion.TabIndex = 59;
            // 
            // cmbType1
            // 
            this.cmbType1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType1.Items.AddRange(new object[] {
            ">",
            "=",
            "<"});
            this.cmbType1.Location = new System.Drawing.Point(144, 52);
            this.cmbType1.Name = "cmbType1";
            this.cmbType1.Size = new System.Drawing.Size(25, 23);
            this.cmbType1.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.Location = new System.Drawing.Point(19, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 22);
            this.label9.TabIndex = 53;
            this.label9.Text = "TimePeriod";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartMatnr
            // 
            this.txtStartMatnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartMatnr.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtStartMatnr.Location = new System.Drawing.Point(144, 7);
            this.txtStartMatnr.MaxLength = 20;
            this.txtStartMatnr.Name = "txtStartMatnr";
            this.txtStartMatnr.Size = new System.Drawing.Size(25, 21);
            this.txtStartMatnr.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.Location = new System.Drawing.Point(29, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 22);
            this.label8.TabIndex = 49;
            this.label8.Text = "Part No. From";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbType2
            // 
            this.cmbType2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType2.Items.AddRange(new object[] {
            ">",
            "=",
            "<"});
            this.cmbType2.Location = new System.Drawing.Point(144, 75);
            this.cmbType2.Name = "cmbType2";
            this.cmbType2.Size = new System.Drawing.Size(25, 23);
            this.cmbType2.TabIndex = 59;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbInsmk);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.cmbLgort);
            this.panel3.Controls.Add(this.cmbWerks);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(202, 112);
            this.panel3.TabIndex = 65;
            // 
            // cmbInsmk
            // 
            this.cmbInsmk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInsmk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInsmk.Location = new System.Drawing.Point(86, 52);
            this.cmbInsmk.Name = "cmbInsmk";
            this.cmbInsmk.Size = new System.Drawing.Size(106, 23);
            this.cmbInsmk.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Location = new System.Drawing.Point(19, 52);
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
            this.cmbLgort.Location = new System.Drawing.Point(86, 30);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(106, 23);
            this.cmbLgort.TabIndex = 1;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(86, 7);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(106, 23);
            this.cmbWerks.TabIndex = 0;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.Location = new System.Drawing.Point(19, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 39;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(19, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 22);
            this.label1.TabIndex = 38;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.stbStatus.Size = new System.Drawing.Size(692, 20);
            this.stbStatus.TabIndex = 41;
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
            // sfdSaveFile
            // 
            this.sfdSaveFile.FileName = "SapDocument.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";
            // 
            // Manage_StorageTimeQuery
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(692, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stbStatus);
            this.Controls.Add(this.panel2);
            this.Name = "Manage_StorageTimeQuery";
            this.Text = "Manage_StorageTimeQuery";
            this.Resize += new System.EventHandler(this.Manage_StorageTimeQuery_Resize);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
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
        #region 顯示狀態列
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion
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
					dtTemp = objAuthority.CheckLgortAuthority(strWerks);
				}
				else
				{
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

		private void ShowDataGrid()
		{
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
			//dtgData.TableStyles.Clear();
			try
			{
                //DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                //mydtgTableStyle.MappingName = dtData.TableName;

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
				insmkStyle.Width = 50;
				insmkStyle.ReadOnly = true;
				dgvData.Columns.Add(insmkStyle);

                DataGridViewTextBoxColumn chargStyle = new DataGridViewTextBoxColumn();
                chargStyle.DataPropertyName = "CHARG";
				chargStyle.HeaderText = "Version";
				chargStyle.Width = 60;
				chargStyle.ReadOnly = true;
				dgvData.Columns.Add(chargStyle);

                DataGridViewTextBoxColumn mengeStyle = new DataGridViewTextBoxColumn();
                mengeStyle.DataPropertyName = "MENGE";
				mengeStyle.HeaderText = "Qty";
				mengeStyle.ReadOnly = true;
				dgvData.Columns.Add(mengeStyle);

                DataGridViewTextBoxColumn indatStyle = new DataGridViewTextBoxColumn();
                indatStyle.DataPropertyName = "INDAT";
				indatStyle.HeaderText = "Store In Date";
				indatStyle.ReadOnly = true;
				dgvData.Columns.Add(indatStyle);

                DataGridViewTextBoxColumn crdatStyle = new DataGridViewTextBoxColumn();
                crdatStyle.DataPropertyName = "CRDAT";
                crdatStyle.HeaderText = "Create Date";
                crdatStyle.ReadOnly = true;
                dgvData.Columns.Add(crdatStyle);

                DataGridViewTextBoxColumn mblnrStyle = new DataGridViewTextBoxColumn();
                mblnrStyle.DataPropertyName = "MBLNR";
				mblnrStyle.HeaderText = "Document No";
				mblnrStyle.Width = 90;
				mblnrStyle.ReadOnly = true;
				dgvData.Columns.Add(mblnrStyle);

                DataGridViewTextBoxColumn lifnrStyle = new DataGridViewTextBoxColumn();
                lifnrStyle.DataPropertyName = "LIFNR";
				lifnrStyle.HeaderText = "Vendor";
				lifnrStyle.ReadOnly = true;
				dgvData.Columns.Add(lifnrStyle);

                DataGridViewTextBoxColumn mrgidStyle = new DataGridViewTextBoxColumn();
                mrgidStyle.DataPropertyName = "MRGID";
				mrgidStyle.HeaderText = "Mixed Material ID";
				mrgidStyle.Width = 120;
				mrgidStyle.ReadOnly = true;
				dgvData.Columns.Add(mrgidStyle);

                DataGridViewTextBoxColumn rmak1Style = new DataGridViewTextBoxColumn();
                rmak1Style.DataPropertyName = "RMAK1";
				rmak1Style.HeaderText = "Remark";
				mblnrStyle.Width = 110;
				rmak1Style.ReadOnly = true;
				dgvData.Columns.Add(rmak1Style);

                dgvData.DataSource = dtData;

                //dtgData.DataSource = dtData;	
                //dtgData.TableStyles.Add(mydtgTableStyle);

                //dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
                this.lblData.Text = dtData.Rows.Count.ToString() + " records";
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
				
			}
		}

		private void btnConfirm_Click(object sender, System.EventArgs e)
		{
			string strType1 = "";
			string strType2 = "";
			string strStartDate = "";
			string strEndDate = "";
			string strComparsion = "";
			string strWerks = "";
			string strLgort = "";
			string strInsmk = "";

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
				if(this.txtStartDate.Text.Trim() == "")
				{
					stsWarning.Text = "Time period can not't be empty!!";
					return;
				}
				try
				{
					int intStartDate = Int32.Parse(this.txtStartDate.Text.Trim());
					if(this.txtEndDate.Text.Trim() != "")
					{
						int intEndDate = Int32.Parse(this.txtEndDate.Text.Trim());
					}
				}
				catch(Exception ex)
				{
					stsWarning.Text = "Time period shoud be number!!";
					return;
				}
				strType1 = cmbType1.Items[cmbType1.SelectedIndex].ToString();
				strStartDate = this.txtStartDate.Text.Trim();
				if(this.cmbComparsion.SelectedIndex == 0 || this.cmbType2.SelectedIndex == 0 || this.txtEndDate.Text.Trim() == "")
				{
					strType2 = "";
					strEndDate = "";
					strComparsion = "";
				}
				else
				{
					strType2 = cmbType2.Items[cmbType2.SelectedIndex].ToString();
					strEndDate = this.txtEndDate.Text.Trim();
					strComparsion = cmbComparsion.Items[cmbComparsion.SelectedIndex].ToString();
				}

				if(strWerks == "" || strLgort == "")
				{
					stsWarning.Text = "Plant and storage can't be empty!!";
					return;
				}
				objStorageData = new StorageData(UserData, strWerks, strLgort);
				dtData = objStorageData.QueryStorageTimeData(strInsmk, txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), txtStartLocat.Text.Trim(), txtEndLocat.Text.Trim(), strType1, strStartDate, strComparsion, strType2, strEndDate);
				
				if(dtData.Rows.Count == 0)
				{
					stsWarning.Text = "No Data!!";
                    ShowDataGrid();
					return;
				}
				else
				{
					ShowDataGrid();
				}
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
				this.cmbType1.SelectedIndex = 0;
				this.cmbType2.SelectedIndex = -1;
				this.cmbComparsion.SelectedIndex = -1;
				this.txtStartMatnr.Text = "";
				this.txtEndMatnr.Text = "";
				this.txtStartDate.Text = "9";
                this.lblData.Text = "0 records";
				this.txtEndDate.Text = "";
//				this.panel1.Enabled = true;
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

		private void Manage_StorageTimeQuery_Resize(object sender, System.EventArgs e)
		{
			panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.25), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - this.stsComcd.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
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
                strLine = "Plant\tStorage\tLocat\tPart No\tStock\tVersion\tQty\tIn Date\tCreate Date\tDocument No.\tVendor Code\tMixed No\tRemark";
				sw.WriteLine(strLine);
				//Reading data
				for(int i=0; i< dtData.Rows.Count; i++)
				{
					strLine = "";
					strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
					strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
					strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
					strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
					strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
					strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
					strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
					strLine += dtData.Rows[i]["INDAT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CRDAT"].ToString() + "\t";
					strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
					strLine += dtData.Rows[i]["LIFNR"].ToString() + "\t";
					strLine += dtData.Rows[i]["MRGID"].ToString() + "\t";
					strLine += dtData.Rows[i]["RMAK1"].ToString();
					
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
