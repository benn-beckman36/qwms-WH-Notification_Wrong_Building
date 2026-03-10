using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using System.Runtime.InteropServices;
using QWMS.Common;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using System.Text;
using System.Drawing.Printing;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Diagnostics;

namespace QWMS
{
    /// <summary>
    /// StorageIn_OnLineIn 的摘要描述。
    /// </summary>
    public class StorageIn_SemiProduct_OnLineInNew : System.Windows.Forms.Form
    {
        #region 变量
        public string strMandt = "";
        public string strUsrnm = "";
        public string strWerks = "";
        public string strLgort = "";
        public string strProgid = "";
        public string strMatnr = "";
        public string strMblnr = "";
        public string strType = "";
        public string strInsmk = "";
        public string strSttyp = "";
        public string strLotyp = "";
        public string strCtbto = "";
        public string strRegon = "";
        public string strMachine = "";
        public string strComcd = "";
        public int intFormIndex = 0;
        public bool bolDuplicate = false;
        public DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        private DataTable dtTmpData = new DataTable();
        private DataTable dtCombineData = new DataTable();
        private DataTable dtCombineInventory = new DataTable();
        private bool AllowToClose = true;//設定能否關閉Form視窗
        private string strVersion = "";
        private int intInvqty = 0;
        private int intAlqty = 0;
        private int intPalqty = 0;
        private string strFindLocat = "";
        private DataTable dtLocat = new DataTable();
        private DataTable dtInventory = new DataTable();
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private ArrayList arrBoxID = new ArrayList();

        int intScanQty = 0;
        int intTotalQty = 0;

        Dictionary<DataRow, DataGridViewRow> dicMapping = new Dictionary<DataRow, DataGridViewRow>();

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

        public string Ctbto
        {
            get
            {
                return strCtbto;
            }
            set
            {
                strCtbto = value;
            }
        }

        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
            }
        }

        public string Machine
        {
            get
            {
                return strMachine;
            }
            set
            {
                strMachine = value;
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

        protected internal System.Windows.Forms.StatusBar stbStatus;
        private System.Windows.Forms.StatusBarPanel stsMandt;
        private System.Windows.Forms.StatusBarPanel stsUsrnm;
        private System.Windows.Forms.StatusBarPanel stsComcd;
        private System.Windows.Forms.StatusBarPanel stsWarning;
        private System.Windows.Forms.StatusBarPanel stsDate;
        private System.Windows.Forms.RadioButton rdoAdd;
        private System.Windows.Forms.RadioButton rdoNew;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.GroupBox gbHeader;
        private System.Windows.Forms.ComboBox cmbWerks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLocat;
        private System.Windows.Forms.DateTimePicker dtpIndat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTotalQty;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtScannedQty;
        private Label lblDataCount;
        private DataGridView dgvData;
        private Label label8;
        private TextBox txtSanLocation;
        private ComboBox cmbLgort;
        private TextBox txtBoxID;
        private CheckBox chkQWMS;
        private QCI.QWMS.LogData objLogData;
        private Label label9;
        private TextBox txtBoxQty;
        private Button btnQWMSIn;
        private System.ComponentModel.IContainer components;
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// 此為設計工具支援所必需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rdoAdd = new System.Windows.Forms.RadioButton();
            this.rdoNew = new System.Windows.Forms.RadioButton();
            this.stbStatus = new System.Windows.Forms.StatusBar();
            this.stsMandt = new System.Windows.Forms.StatusBarPanel();
            this.stsComcd = new System.Windows.Forms.StatusBarPanel();
            this.stsUsrnm = new System.Windows.Forms.StatusBarPanel();
            this.stsWarning = new System.Windows.Forms.StatusBarPanel();
            this.stsDate = new System.Windows.Forms.StatusBarPanel();
            this.gbHeader = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalQty = new System.Windows.Forms.TextBox();
            this.txtScannedQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWerks = new System.Windows.Forms.ComboBox();
            this.cmbLgort = new System.Windows.Forms.ComboBox();
            this.dtpIndat = new System.Windows.Forms.DateTimePicker();
            this.txtLocat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxID = new System.Windows.Forms.TextBox();
            this.txtSanLocation = new System.Windows.Forms.TextBox();
            this.chkQWMS = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBoxQty = new System.Windows.Forms.TextBox();
            this.lblDataCount = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnQWMSIn = new System.Windows.Forms.Button();
            this.gbFunction.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).BeginInit();
            this.gbHeader.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFunction
            // 
            this.gbFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFunction.Controls.Add(this.tableLayoutPanel3);
            this.gbFunction.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFunction.Location = new System.Drawing.Point(3, 3);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(341, 186);
            this.gbFunction.TabIndex = 0;
            this.gbFunction.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.rdoAdd, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.rdoNew, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(335, 161);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // rdoAdd
            // 
            this.rdoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoAdd.Location = new System.Drawing.Point(3, 106);
            this.rdoAdd.Name = "rdoAdd";
            this.rdoAdd.Size = new System.Drawing.Size(329, 28);
            this.rdoAdd.TabIndex = 1;
            this.rdoAdd.Text = "Add In";
            this.rdoAdd.CheckedChanged += new System.EventHandler(this.rdoAdd_CheckedChanged);
            // 
            // rdoNew
            // 
            this.rdoNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoNew.Location = new System.Drawing.Point(3, 24);
            this.rdoNew.Name = "rdoNew";
            this.rdoNew.Size = new System.Drawing.Size(329, 32);
            this.rdoNew.TabIndex = 0;
            this.rdoNew.Text = "New Pallet";
            this.rdoNew.CheckedChanged += new System.EventHandler(this.rdoNew_CheckedChanged);
            // 
            // stbStatus
            // 
            this.stbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stbStatus.Location = new System.Drawing.Point(0, 509);
            this.stbStatus.Name = "stbStatus";
            this.stbStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.stsMandt,
            this.stsComcd,
            this.stsUsrnm,
            this.stsWarning,
            this.stsDate});
            this.stbStatus.ShowPanels = true;
            this.stbStatus.Size = new System.Drawing.Size(1436, 28);
            this.stbStatus.TabIndex = 28;
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
            this.stsWarning.Width = 430;
            // 
            // stsDate
            // 
            this.stsDate.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.stsDate.Name = "stsDate";
            // 
            // gbHeader
            // 
            this.gbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbHeader.Controls.Add(this.tableLayoutPanel4);
            this.gbHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbHeader.Location = new System.Drawing.Point(350, 3);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(1037, 186);
            this.gbHeader.TabIndex = 29;
            this.gbHeader.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.txtTotalQty, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.txtScannedQty, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.label7, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.cmbWerks, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmbLgort, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.dtpIndat, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtLocat, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtBoxID, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.txtSanLocation, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.chkQWMS, 3, 4);
            this.tableLayoutPanel4.Controls.Add(this.label9, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.txtBoxQty, 3, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1031, 161);
            this.tableLayoutPanel4.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(3, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 26);
            this.label8.TabIndex = 36;
            this.label8.Text = "Scan Location";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plant";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Storage";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalQty
            // 
            this.txtTotalQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTotalQty.Location = new System.Drawing.Point(718, 102);
            this.txtTotalQty.MaxLength = 0;
            this.txtTotalQty.Name = "txtTotalQty";
            this.txtTotalQty.Size = new System.Drawing.Size(310, 26);
            this.txtTotalQty.TabIndex = 15;
            // 
            // txtScannedQty
            // 
            this.txtScannedQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScannedQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtScannedQty.Location = new System.Drawing.Point(718, 69);
            this.txtScannedQty.MaxLength = 0;
            this.txtScannedQty.Name = "txtScannedQty";
            this.txtScannedQty.Size = new System.Drawing.Size(310, 26);
            this.txtScannedQty.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(3, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 28);
            this.label5.TabIndex = 12;
            this.label5.Text = "BOX ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(518, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 28);
            this.label6.TabIndex = 16;
            this.label6.Text = "Scanned Qty";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(518, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(194, 27);
            this.label7.TabIndex = 14;
            this.label7.Text = "Total Qty";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "Location";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbWerks
            // 
            this.cmbWerks.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbWerks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWerks.Location = new System.Drawing.Point(203, 3);
            this.cmbWerks.Name = "cmbWerks";
            this.cmbWerks.Size = new System.Drawing.Size(309, 27);
            this.cmbWerks.TabIndex = 2;
            this.cmbWerks.SelectedIndexChanged += new System.EventHandler(this.cmbWerks_SelectedIndexChanged);
            // 
            // cmbLgort
            // 
            this.cmbLgort.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbLgort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLgort.Enabled = false;
            this.cmbLgort.Location = new System.Drawing.Point(203, 69);
            this.cmbLgort.Name = "cmbLgort";
            this.cmbLgort.Size = new System.Drawing.Size(309, 27);
            this.cmbLgort.TabIndex = 3;
            // 
            // dtpIndat
            // 
            this.dtpIndat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dtpIndat.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.dtpIndat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIndat.Location = new System.Drawing.Point(889, 3);
            this.dtpIndat.Name = "dtpIndat";
            this.dtpIndat.Size = new System.Drawing.Size(139, 26);
            this.dtpIndat.TabIndex = 5;
            // 
            // txtLocat
            // 
            this.txtLocat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocat.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtLocat.Location = new System.Drawing.Point(203, 102);
            this.txtLocat.MaxLength = 10;
            this.txtLocat.Name = "txtLocat";
            this.txtLocat.Size = new System.Drawing.Size(309, 26);
            this.txtLocat.TabIndex = 4;
            this.txtLocat.DoubleClick += new System.EventHandler(this.txtLocat_DoubleClick);
            this.txtLocat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocat_KeyDown);
            this.txtLocat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocat_KeyPress);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(518, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 28);
            this.label4.TabIndex = 11;
            this.label4.Text = "In Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxID
            // 
            this.txtBoxID.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBoxID.Location = new System.Drawing.Point(203, 36);
            this.txtBoxID.Name = "txtBoxID";
            this.txtBoxID.Size = new System.Drawing.Size(309, 26);
            this.txtBoxID.TabIndex = 35;
            this.txtBoxID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxID_KeyDown);
            // 
            // txtSanLocation
            // 
            this.txtSanLocation.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSanLocation.Location = new System.Drawing.Point(203, 135);
            this.txtSanLocation.Name = "txtSanLocation";
            this.txtSanLocation.Size = new System.Drawing.Size(309, 26);
            this.txtSanLocation.TabIndex = 37;
            this.txtSanLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSanLocation_KeyDown);
            // 
            // chkQWMS
            // 
            this.chkQWMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkQWMS.AutoSize = true;
            this.chkQWMS.CheckAlign = System.Drawing.ContentAlignment.BottomRight;
            this.chkQWMS.Location = new System.Drawing.Point(718, 135);
            this.chkQWMS.Name = "chkQWMS";
            this.chkQWMS.Size = new System.Drawing.Size(225, 23);
            this.chkQWMS.TabIndex = 38;
            this.chkQWMS.Text = "仅入QWMS(SAP已入账)";
            this.chkQWMS.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(518, 33);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(194, 33);
            this.label9.TabIndex = 39;
            this.label9.Text = "Box Qty";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxQty
            // 
            this.txtBoxQty.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBoxQty.Location = new System.Drawing.Point(718, 36);
            this.txtBoxQty.Name = "txtBoxQty";
            this.txtBoxQty.Size = new System.Drawing.Size(310, 26);
            this.txtBoxQty.TabIndex = 40;
            // 
            // lblDataCount
            // 
            this.lblDataCount.AutoSize = true;
            this.lblDataCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDataCount.Location = new System.Drawing.Point(3, 192);
            this.lblDataCount.Name = "lblDataCount";
            this.lblDataCount.Size = new System.Drawing.Size(81, 17);
            this.lblDataCount.TabIndex = 29;
            this.lblDataCount.Text = "0 records";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgvData, 2);
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.Location = new System.Drawing.Point(3, 214);
            this.dgvData.Name = "dgvData";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(1384, 230);
            this.dgvData.TabIndex = 28;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(279, 462);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(118, 39);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(151, 462);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(118, 39);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(23, 462);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 39);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel2.SetColumnSpan(this.tableLayoutPanel1, 4);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.lblDataCount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbHeader, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbFunction, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvData, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(23, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1390, 447);
            this.tableLayoutPanel1.TabIndex = 32;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.Controls.Add(this.btnSave, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExit, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnRefresh, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnQWMSIn, 3, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(20, 5, 20, 5);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1436, 509);
            this.tableLayoutPanel2.TabIndex = 33;
            // 
            // btnQWMSIn
            // 
            this.btnQWMSIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQWMSIn.Enabled = false;
            this.btnQWMSIn.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQWMSIn.Location = new System.Drawing.Point(407, 462);
            this.btnQWMSIn.Name = "btnQWMSIn";
            this.btnQWMSIn.Size = new System.Drawing.Size(152, 39);
            this.btnQWMSIn.TabIndex = 33;
            this.btnQWMSIn.Text = "转售仅入QWMS";
            this.btnQWMSIn.UseVisualStyleBackColor = true;
            this.btnQWMSIn.Click += new System.EventHandler(this.btnQWMSIn_Click);
            // 
            // StorageIn_SemiProduct_OnLineInNew
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(1436, 537);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.stbStatus);
            this.Name = "StorageIn_SemiProduct_OnLineInNew";
            this.Text = "StorageIn_SemiProduct";
            this.gbFunction.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stsMandt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsComcd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsUsrnm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stsDate)).EndInit();
            this.gbHeader.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Dispose
        /// <summary>
        /// 清除任何使用中的資源。
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
        #endregion

        #region 构造函数
        public StorageIn_SemiProduct_OnLineInNew(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;

            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
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
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }

                rdoNew.Checked = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }
        #endregion

        #region ShowDdlWerks
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
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
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

        private void ShowDdlLgort(string varLogort)
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortWithAuth(strWerks, varLogort);
                    if (dtTemp.Rows.Count == 0)
                    {
                        MessageBox.Show("无该仓别入库权限！");
                        return;
                    }
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortWithAuth(strWerks, varLogort);
                    if (dtTemp.Rows.Count == 0)
                    {
                        MessageBox.Show("无该仓别入库权限！");
                        return;
                    }
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
                        cmbLgort.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                    }
                    cmbLgort.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcChecked = new DataGridViewCheckBoxColumn();
                dgvcChecked.DataPropertyName = "CHKED";
                dgvcChecked.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcChecked);
                this.dgvData.SelectionMode = DataGridViewSelectionMode.CellSelect;


                DataGridViewTextBoxColumn dgvcLoadid = new DataGridViewTextBoxColumn();
                dgvcLoadid.DataPropertyName = "LOADID";
                dgvcLoadid.HeaderText = "LOADID";
                dgvcLoadid.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLoadid);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvckdmat = new DataGridViewTextBoxColumn();
                dgvckdmat.DataPropertyName = "KDMAT";
                dgvckdmat.HeaderText = "CUST Mat";
                dgvckdmat.ReadOnly = true;
                dgvckdmat.Width = 90;
                this.dgvData.Columns.Add(dgvckdmat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn boxidStyle = new DataGridViewTextBoxColumn();
                boxidStyle.DataPropertyName = "BOXID";
                boxidStyle.HeaderText = "Box ID";
                boxidStyle.ReadOnly = true;
                this.dgvData.Columns.Add(boxidStyle);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.Width = 100;
                dgvcAlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 100;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcrmano = new DataGridViewTextBoxColumn();
                dgvcrmano.DataPropertyName = "RMANO";
                dgvcrmano.HeaderText = "RMANO";
                dgvcrmano.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcrmano);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcrmak1 = new DataGridViewTextBoxColumn();
                dgvcrmak1.DataPropertyName = "RMAK1";
                dgvcrmak1.HeaderText = "Remark";
                dgvcrmak1.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcrmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcIndat);

                dgvData.DataSource = dtData;

                lblDataCount.Text = dtData.Rows.Count.ToString() + " records";

                #region Mapping
                dicMapping.Clear();

                foreach (DataGridViewRow dgvr in dgvData.Rows)
                {
                    DataRow dr = (dgvr.DataBoundItem as DataRowView).Row;
                    dicMapping.Add(dr, dgvr);
                }
                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region 控件属性设置
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "NEW";
            Type = strType;
        }

        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strType = "ADD";
            Type = strType;
            txtLocat.Enabled = true;
        }

        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;
        }

        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

        private void SetControlState(bool bolBeforeConfirm)
        {
            this.cmbWerks.Enabled = bolBeforeConfirm;
            this.cmbLgort.Enabled = bolBeforeConfirm;
            this.dtpIndat.Enabled = bolBeforeConfirm;

            this.txtBoxID.Enabled = bolBeforeConfirm;
            this.txtTotalQty.Enabled = false;
            this.txtScannedQty.Enabled = false;
        }
        #endregion

        #region txtBoxID_KeyDown
        private void txtBoxID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            #region 變數宣告
            DataRow drRow;
            stsWarning.Text = string.Empty;
            string strTempMblnrMatnr = string.Empty;
            string strPalletID = "";
            DataTable dtTemp = new DataTable();
            DataTable dtPalData = new DataTable();
            DataTable dtPalData_chk = new DataTable();
            DataTable dtbox = new DataTable();
            dtTmpData = new DataTable();

            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, cmbWerks.Text.Trim(), Lgort);
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            #endregion

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    dtbox = objStorageIn.QueryBoxinfo(Werks, "218", "QMS", txtBoxID.Text.Trim().ToUpper());

                    if (dtbox.Rows.Count > 0)
                    {
                        strPalletID = dtbox.Rows[0]["MBLNR"].ToString().Trim();
                        ShowDdlLgort(dtbox.Rows[0]["LGORT"].ToString().Trim());
                    }
                    else
                    {
                        Sound.Play(@"Sound\ERROR.wav");
                        throw new Exception("没有SF数据!");
                    }

                    #region 检查厂区仓别
                    strWerks = Convert.ToString(cmbWerks.Items[cmbWerks.SelectedIndex]);
                    strLgort = Convert.ToString(cmbLgort.Items[cmbLgort.SelectedIndex]);
                    if (string.IsNullOrEmpty(Werks) || string.IsNullOrEmpty(Lgort))
                    {
                        throw new Exception("Plant 和 storage 不能为空!!");
                    }
                    #endregion

                    #region 取得Sttyp及Lotyp
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                    if (dtTemp.Rows.Count >= 1)
                    {
                        strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                        strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                    }
                    else
                    {
                        throw new Exception("没有 storage 数据!!");
                    }
                    #endregion

                    #region 取出Box ID
                    string strBoxID = txtBoxID.Text.Trim().ToUpper();
                    string strLocat = txtLocat.Text.Trim();

                    if (string.IsNullOrEmpty(strBoxID))
                    {
                        throw new Exception("Box ID 不能为空!!");
                    }
                    #endregion                    

                    #region DataGridView 初始化
                    if (Data.Rows.Count == 0)
                    {
                        #region 确认是否已经SAP扣账，提示使用仅入QWMS系統
                        string strResult = "";
                        strResult = objStorageIn.CheckWHTRS(strPalletID);
                        if (strResult != "" && !chkQWMS.Checked)
                        {
                            MessageBox.Show("此Pallet已经扣账" + strResult + "，请勾选重新刷入QWMS");
                            throw new Exception("此Pallet已经扣账" + strResult + "，请勾选重新刷入QWMS");
                        }
                        #endregion

                        dtTmpData = objSapData.QueryQMSLineInDataByBoxID_ALLNEW(strLocat, strPalletID, "", dtpIndat.Value.ToString("yyyyMMdd"), Insmk, strLgort);

                        if (dtTmpData != null && dtTmpData.Rows.Count > 0)
                        {
                            GetDefaultTable();

                            #region 填值到Data中
                            foreach (DataRow tmpRow in dtTmpData.Rows)
                            {
                                drRow = Data.NewRow();
                                drRow["MANDT"] = tmpRow["MANDT"].ToString();
                                drRow["COMCD"] = tmpRow["COMCD"].ToString();
                                drRow["WERKS"] = tmpRow["WERKS"].ToString();
                                drRow["LGORT"] = tmpRow["LGORT"].ToString();
                                drRow["LOCAT"] = Locat;
                                drRow["MATNR"] = tmpRow["MATNR"].ToString();
                                drRow["INSMK"] = tmpRow["INSMK"].ToString();
                                drRow["CHARG"] = tmpRow["CHARG"].ToString();
                                drRow["MENGE"] = tmpRow["MENGE"].ToString();
                                drRow["ALQTY"] = tmpRow["MENGE"].ToString();
                                drRow["MBLNR"] = tmpRow["MBLNR"].ToString();
                                drRow["BOXID"] = tmpRow["BOXID"].ToString();
                                drRow["EBELN"] = tmpRow["EBELN"].ToString();
                                drRow["LIFNR"] = tmpRow["LIFNR"].ToString();
                                drRow["OMBLNR"] = tmpRow["OMBLNR"].ToString();
                                drRow["MRGID"] = tmpRow["MRGID"].ToString();
                                drRow["KOSTL"] = tmpRow["KOSTL"].ToString();
                                drRow["ARBPL"] = tmpRow["ARBPL"].ToString();
                                drRow["TRNTP"] = tmpRow["TRNTP"].ToString();
                                drRow["RMAK1"] = string.Empty;
                                drRow["INDAT"] = tmpRow["INDAT"].ToString();
                                drRow["KDMAT"] = (dtPalData.Columns.IndexOf("KDMAT") > -1) ? tmpRow["KDMAT"].ToString() : string.Empty;
                                drRow["WO"] = tmpRow["WO"].ToString();
                                drRow["SERNO"] = string.Empty;
                                drRow["LOADID"] = tmpRow["LOADID"].ToString();
                                drRow["MODEL"] = tmpRow["MODEL"].ToString();
                                drRow["REGION"] = tmpRow["REGION"].ToString();
                                drRow["PALQTY"] = tmpRow["PALQTY"].ToString();

                                Data.Rows.Add(drRow);
                            }

                            #endregion

                            ShowDataGrid();

                            foreach (DataRow drData in Data.Rows)
                            {
                                intTotalQty += Convert.ToUInt16(drData["MENGE"].ToString());
                            }
                        }
                        else
                        {
                            txtBoxID.Text = string.Empty;
                            Sound.Play(@"Sound\ERROR.wav");
                            throw new Exception("无数据!!");
                        }
                    }
                    #endregion

                    #region 刷单笔数据
                    int tempQty = 0;
                    foreach (DataGridViewRow dataGridRow in dgvData.Rows)
                    {
                        dataGridRow.DefaultCellStyle.BackColor = Color.Silver;
                        
                        if (txtBoxID.Text.Trim().ToUpper() == dataGridRow.Cells[6].Value.ToString().ToUpper())
                        {
                            if (dataGridRow.Cells[0].Value.ToString() == "True")
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                throw new Exception("BOX ID 已經掃過!!");
                            }
                            else
                            {
                                tempQty += Int32.Parse(dataGridRow.Cells[7].Value.ToString());
                                dataGridRow.Cells[0].Value = true;
                                dataGridRow.DefaultCellStyle.BackColor = Color.Blue;
                            }
                        }
                    }

                    if (tempQty == 0)
                    {
                        throw new Exception("Box ID 不存在!!!");
                    }
                    else
                    {
                        intScanQty += tempQty;
                    }

                    txtScannedQty.Text = intScanQty.ToString();
                    txtTotalQty.Text = intTotalQty.ToString();
                    #endregion

                    DataRow[] drScan = Data.Select("CHKED = true");
                    lblDataCount.Text = string.Format("Scanned {0} of {1} records", drScan.Length, Data.Rows.Count.ToString());

                    if (txtTotalQty.Text == txtScannedQty.Text)
                    {
                        #region 变量
                        DataRow[] combineRow;
                        StringBuilder sbCombineIndex = new StringBuilder();
                        ArrayList alAllCombine = new ArrayList();
                        DataSet dsData = new DataSet();
                        int intCombineQty = 0;
                        #endregion

                        #region 合并资料同一個Pallet ID、料號、版本、庫別的資料
                        dtCombineData.Clear();
                        dtCombineData = dtTmpData.Clone();
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            #region 每次比對的Index (sbCombineIndex)
                            sbCombineIndex.Remove(0, sbCombineIndex.Length);
                            sbCombineIndex.Append("MANDT='" + dtData.Rows[i]["MANDT"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and COMCD='" + dtData.Rows[i]["COMCD"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and WERKS='" + dtData.Rows[i]["WERKS"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and LGORT='" + dtData.Rows[i]["LGORT"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and MBLNR='" + dtData.Rows[i]["MBLNR"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and MATNR='" + dtData.Rows[i]["MATNR"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and CHARG='" + dtData.Rows[i]["CHARG"].ToString().Trim() + "'");
                            sbCombineIndex.Append(" and INSMK='" + dtData.Rows[i]["INSMK"].ToString().Trim() + "'");
                            #endregion

                            if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                            {
                                alAllCombine.Add(sbCombineIndex.ToString());
                                combineRow = dtData.Select(sbCombineIndex.ToString());
                                intCombineQty = 0;
                                for (int j = 0; j < combineRow.Length; j++)
                                {
                                    intCombineQty += Int32.Parse(combineRow[j]["MENGE"].ToString().Trim());
                                }

                                drRow = dtCombineData.NewRow();
                                drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString().Trim();
                                drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString().Trim();
                                drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString().Trim();
                                drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString().Trim();
                                drRow["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString().Trim();
                                drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString().Trim();
                                drRow["INSMK"] = dtData.Rows[i]["INSMK"].ToString().Trim();
                                drRow["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString().Trim();
                                drRow["CHARG"] = dtData.Rows[i]["CHARG"].ToString().Trim();
                                drRow["LIFNR"] = dtData.Rows[i]["LIFNR"].ToString().Trim();
                                drRow["EBELN"] = dtData.Rows[i]["EBELN"].ToString().Trim();
                                drRow["INDAT"] = dtData.Rows[i]["INDAT"].ToString().Trim();
                                drRow["MENGE"] = intCombineQty.ToString().Trim();
                                drRow["ALQTY"] = intCombineQty.ToString().Trim();
                                drRow["KOSTL"] = dtData.Rows[i]["KOSTL"].ToString().Trim();
                                drRow["KDMAT"] = dtData.Rows[i]["KDMAT"].ToString().Trim();
                                drRow["RMANO"] = dtData.Rows[i]["RMANO"].ToString().Trim();
                                drRow["BOXID"] = dtData.Rows[i]["BOXID"].ToString().Trim();
                                drRow["RMAK1"] = dtData.Rows[i]["RMAK1"].ToString().Trim();
                                drRow["WO"] = dtData.Rows[i]["WO"].ToString().Trim();
                                drRow["SERNO"] = dtData.Rows[i]["SERNO"].ToString().Trim();
                                drRow["LOADID"] = dtData.Rows[i]["LOADID"].ToString().Trim();
                                drRow["MODEL"] = dtData.Rows[i]["MODEL"].ToString().Trim();
                                drRow["REGION"] = dtData.Rows[i]["REGION"].ToString().Trim();
                                drRow["PALQTY"] = dtData.Rows[i]["PALQTY"].ToString().Trim();

                                dtCombineData.Rows.Add(drRow);
                            }

                            if (arrBoxID.IndexOf(dtData.Rows[i]["BOXID"].ToString().Trim()) < 0)
                            {
                                arrBoxID.Add(dtData.Rows[i]["BOXID"].ToString().Trim());
                            }
                        }
                        #endregion

                        if (strType == "ADD")
                        {
                            this.txtLocat.Focus();
                        }
                    }
                    else
                    {
                        btnSave.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    this.txtBoxID.Text = "";
                    Sound.Play(@"Sound\ERROR.wav");
                    return;
                }
                finally
                {
                    this.txtBoxID.Text = string.Empty;
                }
            }
        }
        #endregion

        #region txtLocat_DoubleClick
        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    Werks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    Lgort = "";
                }

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    if (dgvData.Rows.Count > 0)
                    {
                        StorageIn_LocationSelectCSMC objStorageIn_LocationSelect = new StorageIn_LocationSelectCSMC(UserData, Progid, Werks, Lgort, Type, dgvData.Rows[0].Cells[2].Value.ToString().Substring(0, 2).ToUpper().Trim());
                        objStorageIn_LocationSelect.ShowDialog();
                        txtLocat.Text = objStorageIn_LocationSelect.Locat;
                    }
                    else
                    {
                        StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type, this.Ctbto, this.Regon);
                        objStorageIn_LocationSelect.ShowDialog();
                        txtLocat.Text = objStorageIn_LocationSelect.Locat;
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region txtLocat_KeyPress
        private void txtLocat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                    DataTable dtbox = new DataTable();
                    dtbox = objStorageIn.QueryInStorageBoxQty(txtLocat.Text.Trim());
                    if (dtbox.Rows.Count > 0)
                    {
                        txtBoxQty.Text = dtbox.Rows.Count.ToString();
                    }
                    else
                    {
                        txtBoxQty.Text = "0";
                    }

                    #region 將Location 填回Data中
                    foreach (DataRow rsTmpRow in this.Data.Rows)
                    {
                        rsTmpRow["LOCAT"] = this.txtLocat.Text.Trim();
                    }
                    this.Data.AcceptChanges();
                    #endregion

                    #region 設定按鈕
                    this.txtBoxID.Text = "";
                    this.txtTotalQty.Text = "";
                    this.txtScannedQty.Text = "";
                    this.txtBoxID.Enabled = true;
                    this.txtTotalQty.Enabled = false;
                    this.txtScannedQty.Enabled = false;
                    this.txtBoxID.Enabled = true;
                    this.txtBoxID.Focus();
                    #endregion
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\Fail.wav");
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        #endregion

        #region txtLocat_KeyDown
        private void txtLocat_KeyDown(object sender, KeyEventArgs e)
        {
            #region 將Location填回CombineData中
            foreach (DataRow rsTmpRow in this.dtCombineData.Rows)
            {
                rsTmpRow["LOCAT"] = this.txtLocat.Text.Trim();
            }
            this.dtCombineData.AcceptChanges();
            #endregion
        }
        #endregion

        #region txtSanLocation_KeyDown
        private void txtSanLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)13)
            {
                if (txtSanLocation.Text.Trim().ToUpper() != "" && txtSanLocation.Text.Trim().ToUpper() == txtLocat.Text.Trim().ToUpper())
                {
                    txtSanLocation.Text = txtLocat.Text.Trim().ToUpper();
                    btnSave.Enabled = true;
                    this.btnQWMSIn.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                    Sound.Play(@"Sound\OO.wav");
                    MessageBox.Show("储位扫描错误！");
                    return;
                }
            }
        }
        #endregion

        #region btnSave
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            #region 参数定义
            SetbtnSaveProcess();
            string strMtype = string.Empty;
            stsWarning.Text = "";
            DataTable dtPalData = new DataTable();
            DataTable dtTemp = new DataTable();
            ArrayList SN = new ArrayList();
            string[] message = new string[2];
            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType,CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType,CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType,CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType,CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);
            #endregion

            try
            {
                #region 防呆机制
                if (txtTotalQty.Text != txtScannedQty.Text)
                {
                    MessageBox.Show("扫入数量与总数不匹配！");
                    txtBoxID.Focus();
                    return;
                }

                this.btnSave.Enabled = false;

                if (txtLocat.Text.Trim() != "")
                {
                    if (!objPlantData.CheckExistedStorageData(Werks, strLgort, Locat))
                    {
                        stsWarning.Text = "The destination location doesn't exist!!";
                        this.txtLocat.Focus();
                        return;
                    }
                }

                if (txtLocat.Text.Trim() == "")
                {
                    MessageBox.Show("Location不能为空！");
                    return;
                }
                else
                {
                    DataTable dtlocation = new DataTable();
                    dtlocation = objStorageIn.QuerywhitmbyLocat(Werks, Lgort, txtLocat.Text.Trim());
                    if (dtlocation.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtlocation.Rows.Count; i++)
                        {
                            if (dtData.Rows.Count > 0)
                            {
                                if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtData.Rows[0]["MATNR"].ToString() ||
                                    dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtData.Rows[0]["CHARG"].ToString())
                                {
                                    stsWarning.Text = "该储位中已经存在不同的料号！";
                                    return;
                                }
                            }
                        }
                    }
                }
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "数据不能为空!!";
                    SetbtnSaveException();
                    return;
                }

                #endregion

                #region SAP扣账

                #region 去除不必要的欄位

                if (dtTmpData.Columns.Count > 0)
                {
                    dtTmpData.Columns.Remove("MANDT");
                    dtTmpData.Columns.Remove("LGORT");
                    dtTmpData.Columns.Remove("LOCAT");
                    dtTmpData.Columns.Remove("INSMK");
                    dtTmpData.Columns.Remove("CHARG");
                    dtTmpData.Columns.Remove("ALQTY");
                    dtTmpData.Columns.Remove("BOXID");
                    dtTmpData.Columns.Remove("EBELN");
                    dtTmpData.Columns.Remove("LIFNR");
                    dtTmpData.Columns.Remove("OMBLNR");
                    dtTmpData.Columns.Remove("MRGID");
                    dtTmpData.Columns.Remove("KOSTL");
                    dtTmpData.Columns.Remove("ARBPL");
                    dtTmpData.Columns.Remove("TRNTP");
                    dtTmpData.Columns.Remove("RMAK1"); 
                    dtTmpData.Columns.Remove("INDAT");
                    dtTmpData.Columns.Remove("KDMAT");
                    dtTmpData.Columns.Remove("LOADID");
                }
                else
                {
                    stsWarning.Text = "请Refresh后重刷";
                    SetbtnSaveException();
                    return;
                }

                #endregion

                #region 获取Pallet id 单据类型是311 QMS_311还是101 QMS 以及归并扣帐数据

                DataTable dt = objStorageIn.QueryDataForSap(dtTmpData.Rows[0]["MBLNR"].ToString(),out strMtype);
                if (string.IsNullOrEmpty(strMtype))
                {
                    MessageBox.Show("未获取到单据类型");
                    return;
                }
                #endregion

                #region SAP 扣账校验

                bool bolSapStatus = false;
                stsWarning.Text = "系統正在扣SAP帳中，請勿關閉視窗!!";
                AllowToClose = false; //強制User無法關閉視窗
          
                string strPalletID = dt.Rows[0]["MBLNR"].ToString().Trim();
                string strGrNo = string.Empty;
            
                if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                {
                    if (chkQWMS.Checked)
                    {
                        strGrNo = objStorageIn.CheckWHTRS(strPalletID);
                        objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "User选择仅入QWMS选项", strGrNo, null);
                        if (string.IsNullOrEmpty(strGrNo))
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "User选择仅入QWMS选项", strGrNo, "SAP未扣帐不能勾选 重新刷入QWMS");
                            stsWarning.Text = strPalletID + @":在SAP未扣帐，不能勾选 仅入QWMS";
                            MessageBox.Show(strPalletID + @":在SAP未扣帐，不能勾选 仅入QWMS");
                            return;
                        }
                        else
                        {
                            bolSapStatus = true;
                        }
                    }

                    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始检查QMS中PalletID是否可用...", null, null);
                    message = objStorageIn.TransferPalletIDToQMS(strPalletID, "WMS", "WH Receiving");
                    objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束检查QMS中PalletID是否可用",
                        message[0].ToString().Trim(), message[1].ToString());
                    //选择仅入QWMS 略过QMS INFO
                    if (!chkQWMS.Checked)
                    {
                        if (!message[0].ToString().Trim().Equals("Pass", StringComparison.CurrentCultureIgnoreCase))
                        {
                            stsWarning.Text = message[1].ToString();
                            MessageBox.Show(strPalletID + ":" + message[1].ToString());
                            return;
                        }
                    }
                    else
                    {
                     objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "选择仅入QWMS，略过检查QMS状态", null, null);
                    }
                }
                #endregion

                #region SAP 扣账

                if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                {
                    if (!chkQWMS.Checked)
                    {
                        #region SAP扣账
                        string strErrorMessage = string.Empty;
                        objLogData.AddQWMSLOG(strPalletID, "SAP", "IN", "开始SAP扣帐", null, null);
                        //需修改311传入栏位和读取回执
                        DataTable dtResponse = objStorageIn.WriteSapFile_CSMC_FGNew(dt,strMtype); //WriteSapFile_CSMC_FG(dt);
                        objLogData.AddQWMSLOG(strPalletID, "SAP", "IN", "结束SAP扣帐",dtResponse.Rows[0]["RESULT"].ToString().Trim() +dtResponse.Rows[0]["REMARK2"].ToString().Trim(), null);

                        string strGRNoTemp = string.Empty;
                        if (dtResponse.Rows[0]["MBLNR"].ToString() == strPalletID &&
                            dtResponse.Rows[0]["REMARK2"].ToString().Trim() == "")
                        {
                            bolSapStatus = true;
                        }
                        if (dtResponse.Rows[0]["MBLNR"].ToString() == strPalletID &&
                            !string.IsNullOrEmpty(dtResponse.Rows[0]["REMARK2"].ToString().Trim()))
                        {
                            string[] GRtemp = dtResponse.Rows[0]["REMARK2"].ToString().Trim().Split(':');
                           // if (GRtemp.Length >= 2 && GRtemp[1].Trim().Substring(0, 2).Contains("50") && objStorageIn.CheckWHBOXInfo(strPalletID, arrBoxID))
                            if (GRtemp.Length >= 2 && "49;50".Contains(GRtemp[1].Trim().Substring(0, 2)) && 
                                objStorageIn.CheckWHBOXInfo(strPalletID, arrBoxID) &&
                                GRtemp[1].Trim().Length == 10 && !GRtemp[1].Trim().Contains("."))
                            {
                                strGRNoTemp = GRtemp[1].Trim();
                                bolSapStatus = true;
                            }
                            if (dtResponse.Rows[0]["REMARK2"].ToString().Trim().Contains(">"))
                            {
                                strErrorMessage = @"請找PMC或产线成管人員協助處理！";
                            }
                            else if (dtResponse.Rows[0]["REMARK2"].ToString().Trim().Contains(":"))
                            {
                                strErrorMessage =  @"请过3分钟后重试！";
                            }
                            else
                            {
                                strErrorMessage = "请找SAP人員協助處理";
                            }
                            strErrorMessage = "SAP未扣帳成功，錯誤訊息: " + dtResponse.Rows[0]["REMARK2"].ToString().Trim() + strErrorMessage;
                          Sound.Play(@"Sound\ERROR.wav");
                        }

                        #region Add By Michael 20150603 for 储位库存防呆
                        DataTable dtlocation = new DataTable();
                        dtlocation = objStorageIn.QuerywhitmbyLocat(Werks, Lgort, txtLocat.Text.Trim());
                        if (dtlocation.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtlocation.Rows.Count; i++)
                            {
                                if (dtData.Rows.Count > 0)
                                {
                                    if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtData.Rows[0]["MATNR"].ToString() ||
                                        dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtData.Rows[0]["CHARG"].ToString())
                                    {
                                        stsWarning.Text = "储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！";
                                        Sound.Play(@"Sound\ERROR.wav");
                                        MessageBox.Show("储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！");
                                        return;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 回传SAP扣帐状态给QMS 解锁
                        //測試庫不執行SAP扣帳
                        if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                            // if (false)
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始回传QMS SAP扣帐状态", null, null);
                                if (!string.IsNullOrEmpty(strGRNoTemp))
                                {
                                    message = objStorageIn.TransferSAPGRToQMS_SN(strPalletID, strGRNoTemp, "");
                                }
                                else
                                {
                                    message = objStorageIn.TransferSAPGRToQMS_SN(dtResponse.Rows[0]["MBLNR"].ToString(),
                                    dtResponse.Rows[0]["RESULT"].ToString().Trim(),
                                    dtResponse.Rows[0]["REMARK2"].ToString().Trim());
                                }

                            objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束回传QMS SAP扣帐状态",message[0].ToString().Trim(), message[1].ToString());

                            //if (!message[0].ToString().Trim().Equals("Pass", StringComparison.CurrentCultureIgnoreCase))
                            //{
                            //    if (string.IsNullOrEmpty(strErrorMessage))
                            //    {
                            //        strErrorMessage = strPalletID + ":" + message[0].ToString().Trim() +message[1].ToString();
                            //    }
                            //    blQMSStatus = false;
                            //}
                            //else
                            //{
                            //    blQMSStatus = true;
                            //}
                        }

                        #endregion
                        if (bolSapStatus)
                        {
                            AllowToClose = true;
                            stsWarning.Text = "SAP Posting OK!!SAP扣帐编号:" +dtResponse.Rows[0]["RESULT"].ToString().Trim();                     
                        }
                        else
                        {
                            stsWarning.Text = strErrorMessage;
                            MessageBox.Show(strErrorMessage);
                            return;
                        }

                        #endregion
                    }
                    else
                    {
                        #region Add By Michael 20150603 for 储位库存防呆
                        DataTable dtlocation = new DataTable();
                        dtlocation = objStorageIn.QuerywhitmbyLocat(Werks, Lgort, txtLocat.Text.Trim());
                        if (dtlocation.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtlocation.Rows.Count; i++)
                            {
                                if (dtData.Rows.Count > 0)
                                {
                                    if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtData.Rows[0]["MATNR"].ToString() ||
                                        dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtData.Rows[0]["CHARG"].ToString())
                                    {
                                        stsWarning.Text = "储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！";
                                        Sound.Play(@"Sound\ERROR.wav");
                                        MessageBox.Show("储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！");                                        
                                        return;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 回传SAP扣帐状态给QMS 解锁

                        if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "开始回传QMS SAP扣帐状态", null, null);

                            message = objStorageIn.TransferSAPGRToQMS_SN(strPalletID,strGrNo,"");

                            objLogData.AddQWMSLOG(strPalletID, "QMS", "IN", "结束回传QMS SAP扣帐状态",message[0].ToString().Trim(), message[1].ToString());
                        }
                        #endregion
                    }
                }
                #endregion

                #endregion

                #region 入QWMS的庫存

                foreach (DataRow row in dtCombineData.Rows)
                {
                    row["LOCAT"] = Locat;
                }

                objLogData.AddQWMSLOG(dtData.Rows[0]["MBLNR"].ToString(), "QWMS", "IN", "开始QWMS入账", null, null);
                //SAP QM 运行通过后QWMS方可入库
                if (!CommonInfo.Instance.DBCode.Contains("TEST"))
                //if (true)
                {
                    if (bolSapStatus)
                    // if (true)
                    {
                        //需修改311入库
                        if (objStorageIn.AddSemiProdOnLineInData(Locat, dtCombineData, arrBoxID, dtData))
                        {
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "OK", null);
                            stsWarning.Text = "QWMS Add OK!!";
                            Sound.Play(@"Sound\OO1.wav");
                        }
                        else
                        {
                            stsWarning.Text = "Add fail!! " + objStorageIn.ERRMSG;
                            objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "Fail:" + objStorageIn.ERRMSG, null);
                            Sound.Play(@"Sound\ERROR.wav");
                        }
                    }
                    else
                    {
                        objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "Fail", "SAP扣账未成功");
                    }
                }
                #endregion
                dtTmpData.Clear();
                return;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnRefresh
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            chkQWMS.Checked = false;
            stsWarning.Text = string.Empty;
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.gbFunction.Enabled = true;
            this.gbHeader.Enabled = true;
            this.txtLocat.Text = string.Empty;
            this.txtBoxID.Text = string.Empty;
            this.txtTotalQty.Text = string.Empty;
            this.txtScannedQty.Text = string.Empty;
            this.txtSanLocation.Text = string.Empty;
            intScanQty = 0;
            intTotalQty = 0;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.lblDataCount.Text = "0 records";
            this.btnSave.Enabled = false;
            this.dgvData.Columns.Clear();
            dtTmpData.Clear();
            SetControlState(true);
            this.cmbLgort.Enabled = false;
            this.cmbLgort.Text = "";
        }
        #endregion

        #region btnExit
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 转售仅入QWMS
        private void btnQWMSIn_Click(object sender, EventArgs e)
        {
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType,
             CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType,
             CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

             DialogResult result = new DialogResult();
             result = MessageBox.Show("该功能是为转售BOXID设计,只会入QWMS库存，不会到SAP扣账和同步QMS,请确认清楚，谢谢！！", "转售BOXID", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                #region Add By Michael 20150603 for 储位库存防呆
                DataTable dtlocation = new DataTable();
                dtlocation = objStorageIn.QuerywhitmbyLocat(Werks, Lgort, txtLocat.Text.Trim());
                if (dtlocation.Rows.Count > 0)
                {
                    for (int i = 0; i < dtlocation.Rows.Count; i++)
                    {
                        if (dtData.Rows.Count > 0)
                        {
                            if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtData.Rows[0]["MATNR"].ToString() ||
                                dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtData.Rows[0]["CHARG"].ToString())
                            {
                                stsWarning.Text = "储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！";
                                Sound.Play(@"Sound\ERROR.wav");
                                MessageBox.Show("储位：" + txtLocat.Text.Trim() + "中已经存在不同的料号，請勾选重新刷入QWMS！");
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region 入QWMS的庫存
                if (dtCombineData.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCombineData.Rows)
                    {
                        row["LOCAT"] = Locat;
                    }
                    string strPalletID = dtData.Rows[0]["MBLNR"].ToString();
                    objLogData.AddQWMSLOG(dtData.Rows[0]["MBLNR"].ToString(), "QWMS", "IN", "开始QWMS入账", null, null);
                    if (objStorageIn.AddSemiProdOnLineInData(Locat, dtCombineData, arrBoxID, dtData))
                    {
                        objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "OK", null);
                        stsWarning.Text = "QWMS Add OK!!";
                        Sound.Play(@"Sound\OO1.wav");
                    }
                    else
                    {
                        stsWarning.Text = "Add fail!! " + objStorageIn.ERRMSG;
                        objLogData.AddQWMSLOG(strPalletID, "QWMS", "IN", "结束QWMS入账", "Fail:" + objStorageIn.ERRMSG, null);

                        Sound.Play(@"Sound\ERROR.wav");
                    }
                    dtTmpData.Clear();
                }
                #endregion
            }

       

        }
        #endregion

        #region GetDefaultTable
        private void GetDefaultTable()
        {
            if (Data.Rows.Count == 0)
            {
                Data = new DataTable();
                Data.Columns.Add("CHKED", typeof(Boolean));
                Data.Columns.Add("MANDT", Type.GetType());
                Data.Columns.Add("COMCD", Type.GetType());
                Data.Columns.Add("WERKS", Type.GetType());
                Data.Columns.Add("LGORT", Type.GetType());
                Data.Columns.Add("LOCAT", Type.GetType());
                Data.Columns.Add("MATNR", Type.GetType());
                Data.Columns.Add("INSMK", Type.GetType());
                Data.Columns.Add("CHARG", Type.GetType());
                Data.Columns.Add("MENGE", Type.GetType());
                Data.Columns.Add("ALQTY", Type.GetType());
                Data.Columns.Add("MBLNR", Type.GetType());
                Data.Columns.Add("BOXID", Type.GetType());
                Data.Columns.Add("EBELN", Type.GetType());
                Data.Columns.Add("LIFNR", Type.GetType());
                Data.Columns.Add("OMBLNR", Type.GetType());
                Data.Columns.Add("MRGID", Type.GetType());
                Data.Columns.Add("KOSTL", Type.GetType());
                Data.Columns.Add("ARBPL", Type.GetType());
                Data.Columns.Add("TRNTP", Type.GetType());
                Data.Columns.Add("RMANO", Type.GetType());
                Data.Columns.Add("RMAK1", Type.GetType());
                Data.Columns.Add("INDAT", Type.GetType());
                Data.Columns.Add("KDMAT", Type.GetType());
                Data.Columns.Add("WO", Type.GetType());
                Data.Columns.Add("SERNO", Type.GetType());
                Data.Columns.Add("LOADID", Type.GetType());
                Data.Columns.Add("MODEL", Type.GetType());
                Data.Columns.Add("REGION", Type.GetType());
                Data.Columns.Add("PALQTY", Type.GetType());

                DataColumn[] dcPrimaryKey = new DataColumn[4];
                dcPrimaryKey[0] = Data.Columns["MBLNR"];
                dcPrimaryKey[1] = Data.Columns["MATNR"];
                dcPrimaryKey[2] = Data.Columns["BOXID"];
                dcPrimaryKey[3] = Data.Columns["WO"];

                Data.PrimaryKey = dcPrimaryKey;
            }
        }
        #endregion
    }
}
