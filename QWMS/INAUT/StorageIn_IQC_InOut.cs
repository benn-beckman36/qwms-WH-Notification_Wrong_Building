using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;
using System.Media;
using System.IO;
using System.Diagnostics;




namespace QWMS
{
    public partial class StorageIn_IQC_InOut : Form
    {
        #region 參數定義
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strWerksTo = "";
        private string strLgort = "";
        private string strLgortTo = "";
        private string strLocat = "";
        private string strProgid = "";
        private string strBoxid = "";
        private string strType = "";
        private string strComcd = "";
        private string strVbeln = "";
        private string strMatnr = "";
        private string strStartDate = "";
        private string strEndDate = "";
        private string strUsrQry = "";


        private bool bolchk=false;
        private bool bolQuery = false;
        private FileInfo fi;
        private StreamWriter sw;
        UserInfo UserData = new UserInfo();
        private DataTable dtData, dtInsertData, dtUpdateData; //顯示gv, 更新DB table 
        private DataTable dtShwData;
        private DataRow drData;
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
        public string WerksTo
        {
            get
            {
                return strWerksTo;
            }
            set
            {
                strWerksTo = value;
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
        public string LgortTo
        {
            get
            {
                return strLgortTo;
            }
            set
            {
                strLgortTo = value;
            }
        }
        public string Locat
        {
            get
            {
                return strLocat;
            }
            set
            {
                strLocat = value;
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


        #region StorageIn_IQC_InOut(畫面)
        public StorageIn_IQC_InOut(ref UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;
            #region 檢查權限
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
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    SetInitial(); //初始設定元件
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #endregion
        }
        #endregion

        #region ShowStatusData(設定視窗狀態列)
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion
        #region ShowDdlWerks(設定廠區選項)
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                cmbWerksTo.Items.Clear();

                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerksTo.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion
        #region ShowInspctSts(顯示檢驗狀態)
        private void ShowInspctSts()
        {
            if (cmbQCtyp.Items.Count == 0)
            {
                cmbQCtyp.Items.Add("所有状态");
                cmbQCtyp.Items.Add("未检验");
                cmbQCtyp.Items.Add("IQC检验中");
                cmbQCtyp.Items.Add("检验完成");
            }
        }
        #endregion
        #region ShowDataGrid(顯示gv內容)
        private void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.AllowUserToResizeColumns = false; 
            this.dgvData.Columns.Clear();
            dtShwData = dtData.Clone();
            #region gv顯示內容轉換
            if (dtShwData.Rows.Count != 0)
            {
                dtShwData.Clear();
            }
            dtShwData= dtData.Copy();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (dtData.Rows[i]["QCTYP"].ToString().Trim() == "Y")
                {
                    dtShwData.Rows[i]["QCTYP"] = "检验完成";
                }
                if (dtData.Rows[i]["QCTYP"].ToString().Trim() == "N")
                {
                    dtShwData.Rows[i]["QCTYP"] = "未检验";
                }
                if (dtData.Rows[i]["QCTYP"].ToString().Trim() == "O")
                {
                    dtShwData.Rows[i]["QCTYP"] = "IQC检验中";
                }
            }
            #endregion
            this.dgvData.DataSource = dtShwData;
            lblCount.Text = dtData.Rows.Count.ToString() + " records";

            #region 設定欄位名稱
            #region 檢驗入庫
            //檢驗入庫
            if (rdoGR.Checked == true)
            {
                //Plant
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 55;
                dgvcWerks.ReadOnly = true;
                dgvcWerks.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                this.dgvData.Columns.Add(dgvcWerks);

                //Storage
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 55;
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);
                dgvcLgort.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Location
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 55;
                dgvcLocat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocat);
                dgvcLocat.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //EC No.
                DataGridViewTextBoxColumn dgvcVbeln = new DataGridViewTextBoxColumn();
                dgvcVbeln.DataPropertyName = "VBELN";
                dgvcVbeln.HeaderText = "EC No.";
                dgvcVbeln.Width = 110;
                dgvcVbeln.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcVbeln);
                dgvcVbeln.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Part No
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 110;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);
                dgvcMatnr.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Qty
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Inventory Qty";
                dgvcAlqty.Width = 55;
                dgvcAlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqty);
                dgvcAlqty.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Inspec. Status
                DataGridViewTextBoxColumn dgvcQctyp = new DataGridViewTextBoxColumn();
                dgvcQctyp.DataPropertyName = "QCTYP";
                dgvcQctyp.HeaderText = "Type";
                dgvcQctyp.Width = 110;
                dgvcQctyp.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQctyp);
                dgvcQctyp.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //IQC user
                DataGridViewTextBoxColumn dgvcQcusr = new DataGridViewTextBoxColumn();
                dgvcQcusr.DataPropertyName = "QCUSR";
                dgvcQcusr.HeaderText = "IQC User";
                dgvcQcusr.Width = 55;
                dgvcQcusr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQcusr);
                dgvcQcusr.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //CRDAT
                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "Inventory In Date";
                dgvcCrdat.Width = 110;
                dgvcCrdat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCrdat);
                dgvcCrdat.DefaultCellStyle.BackColor = Color.WhiteSmoke;



                //W/H user
                DataGridViewTextBoxColumn dgvcWhusr = new DataGridViewTextBoxColumn();
                dgvcWhusr.DataPropertyName = "WHUSR";
                dgvcWhusr.HeaderText = "W/H User";
                dgvcWhusr.Width = 55;
                dgvcWhusr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWhusr);
                dgvcWhusr.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //Remark
                DataGridViewTextBoxColumn dgvcRemak = new DataGridViewTextBoxColumn();
                dgvcRemak.DataPropertyName = "REMAK";
                dgvcRemak.HeaderText = "Remark";
                dgvcRemak.Width = 110;
                dgvcRemak.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRemak);
                dgvcRemak.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //Plant_To
                DataGridViewTextBoxColumn dgvcWerksTo = new DataGridViewTextBoxColumn();
                dgvcWerksTo.DataPropertyName = "WERKS_To";
                dgvcWerksTo.HeaderText = "WH Plant";
                dgvcWerksTo.Width = 55;
                dgvcWerksTo.ReadOnly = true;
                dgvcWerksTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                this.dgvData.Columns.Add(dgvcWerksTo);

                //Storage_To
                DataGridViewTextBoxColumn dgvcLgortTo = new DataGridViewTextBoxColumn();
                dgvcLgortTo.DataPropertyName = "LGORT_To";
                dgvcLgortTo.HeaderText = "WH Storage";
                dgvcLgortTo.Width = 55;
                dgvcLgortTo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgortTo);
                dgvcLgortTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //Location
                DataGridViewTextBoxColumn dgvcLocatTo = new DataGridViewTextBoxColumn();
                dgvcLocatTo.DataPropertyName = "LOCAT_To";
                dgvcLocatTo.HeaderText = "WH Location";
                dgvcLocatTo.Width = 55;
                dgvcLocatTo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocatTo);
                dgvcLocatTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //Qty
                DataGridViewTextBoxColumn dgvcAlqtyTo = new DataGridViewTextBoxColumn();
                dgvcAlqtyTo.DataPropertyName = "ALQTY_To";
                dgvcAlqtyTo.HeaderText = "In Qty";
                dgvcAlqtyTo.Width = 55;
                dgvcAlqtyTo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqtyTo);
                dgvcAlqtyTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //CRDAT
                DataGridViewTextBoxColumn dgvcCrdatTo = new DataGridViewTextBoxColumn();
                dgvcCrdatTo.DataPropertyName = "CRDAT_To";
                dgvcCrdatTo.HeaderText = "In Date";
                dgvcCrdatTo.Width = 110;
                dgvcCrdatTo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCrdatTo);
                /*
                dgvcCrdatTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;




                this.dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                this.dgvData.ColumnHeadersHeight = this.dgvData.ColumnHeadersHeight * 3;

                this.dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

                this.dgvData.CellPainting += new DataGridViewCellPaintingEventHandler(dgvData_CellPainting);

                this.dgvData.Paint += new PaintEventHandler(dgvData_Paint);
                */

            }
            #endregion
            #region 檢驗出庫
            //檢驗出庫
            if (rdoGI.Checked == true)
            {
                //Plant
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 55;
                dgvcWerks.ReadOnly = true;
                dgvcWerks.DefaultCellStyle.BackColor = Color.PowderBlue;
                this.dgvData.Columns.Add(dgvcWerks);

                //Storage
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 55;
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);
                dgvcLgort.DefaultCellStyle.BackColor = Color.PowderBlue;

                //Location
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 55;
                dgvcLocat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocat);
                dgvcLocat.DefaultCellStyle.BackColor = Color.PowderBlue;

                //Part No
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 110;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);
                dgvcMatnr.DefaultCellStyle.BackColor = Color.PowderBlue;

                //Qty
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Qty";
                dgvcAlqty.Width = 55;
                dgvcAlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqty);
                dgvcAlqty.DefaultCellStyle.BackColor = Color.PowderBlue;

                //Inspec. Status
                DataGridViewTextBoxColumn dgvcQctyp = new DataGridViewTextBoxColumn();
                dgvcQctyp.DataPropertyName = "QCTYP";
                dgvcQctyp.HeaderText = "Type";
                dgvcQctyp.Width = 110;
                dgvcQctyp.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQctyp);
                dgvcQctyp.DefaultCellStyle.BackColor = Color.PowderBlue;

                //IQC user
                DataGridViewTextBoxColumn dgvcQcusr = new DataGridViewTextBoxColumn();
                dgvcQcusr.DataPropertyName = "QCUSR";
                dgvcQcusr.HeaderText = "IQC User";
                dgvcQcusr.Width = 55;
                dgvcQcusr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQcusr);
                dgvcQcusr.DefaultCellStyle.BackColor = Color.PowderBlue;

                //CRDAT
                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "In Date";
                dgvcCrdat.Width = 110;
                dgvcCrdat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCrdat);
                dgvcCrdat.DefaultCellStyle.BackColor = Color.PowderBlue;

                //EC No.
                DataGridViewTextBoxColumn dgvcVbeln = new DataGridViewTextBoxColumn();
                dgvcVbeln.DataPropertyName = "VBELN";
                dgvcVbeln.HeaderText = "EC No.";
                dgvcVbeln.Width = 110;
                dgvcVbeln.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcVbeln);
                dgvcVbeln.DefaultCellStyle.BackColor = Color.PowderBlue;

                //W/H user
                DataGridViewTextBoxColumn dgvcWhusr = new DataGridViewTextBoxColumn();
                dgvcWhusr.DataPropertyName = "WHUSR";
                dgvcWhusr.HeaderText = "W/H User";
                dgvcWhusr.Width = 55;
                dgvcWhusr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWhusr);
                dgvcWhusr.DefaultCellStyle.BackColor = Color.PowderBlue;

                //Plant_To
                DataGridViewTextBoxColumn dgvcWerksTo = new DataGridViewTextBoxColumn();
                dgvcWerksTo.DataPropertyName = "WERKS_To";
                dgvcWerksTo.HeaderText = "WH Plant";
                dgvcWerksTo.Width = 55;
                dgvcWerksTo.ReadOnly = true;
                dgvcWerksTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;
                this.dgvData.Columns.Add(dgvcWerksTo);

                //Storage_To
                DataGridViewTextBoxColumn dgvcLgortTo = new DataGridViewTextBoxColumn();
                dgvcLgortTo.DataPropertyName = "LGORT_To";
                dgvcLgortTo.HeaderText = "WH Storage";
                dgvcLgortTo.Width = 55;
                dgvcLgortTo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgortTo);
                dgvcLgortTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //Location
                DataGridViewTextBoxColumn dgvcLocatTo = new DataGridViewTextBoxColumn();
                dgvcLocatTo.DataPropertyName = "LOCAT_To";
                dgvcLocatTo.HeaderText = "WH Location";
                dgvcLocatTo.Width = 55;
                dgvcLocatTo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocatTo);
                dgvcLocatTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //Qty
                DataGridViewTextBoxColumn dgvcAlqtyTo = new DataGridViewTextBoxColumn();
                dgvcAlqtyTo.DataPropertyName = "ALQTY_To";
                dgvcAlqtyTo.HeaderText = "In Qty";
                dgvcAlqtyTo.Width = 55;
                dgvcAlqtyTo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqtyTo);
                dgvcAlqtyTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //CRDAT
                DataGridViewTextBoxColumn dgvcCrdatTo = new DataGridViewTextBoxColumn();
                dgvcCrdatTo.DataPropertyName = "CRDAT_To";
                dgvcCrdatTo.HeaderText = "In Date";
                dgvcCrdatTo.Width = 110;
                dgvcCrdatTo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCrdatTo);
                dgvcCrdatTo.DefaultCellStyle.BackColor = Color.PaleGoldenrod;

                //Remark
                DataGridViewTextBoxColumn dgvcRemak = new DataGridViewTextBoxColumn();
                dgvcRemak.DataPropertyName = "REMAK";
                dgvcRemak.HeaderText = "Remark";
                dgvcRemak.Width = 110;
                dgvcRemak.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRemak);
                dgvcRemak.DefaultCellStyle.BackColor = Color.PaleGoldenrod;
            }
            #endregion
            #region 庫存查詢
            if (rdoQuery.Checked == true)
            {
                //Plant
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 55;
                dgvcWerks.ReadOnly = true;
                dgvcWerks.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                this.dgvData.Columns.Add(dgvcWerks);

                //Storage
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 55;
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);
                dgvcLgort.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Location
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 55;
                dgvcLocat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocat);
                dgvcLocat.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //EC No.
                DataGridViewTextBoxColumn dgvcVbeln = new DataGridViewTextBoxColumn();
                dgvcVbeln.DataPropertyName = "VBELN";
                dgvcVbeln.HeaderText = "EC No.";
                dgvcVbeln.Width = 110;
                dgvcVbeln.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcVbeln);
                dgvcVbeln.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Part No
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 110;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);
                dgvcMatnr.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Qty
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Inventory Qty";
                dgvcAlqty.Width = 55;
                dgvcAlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqty);
                dgvcAlqty.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Inspec. Status
                DataGridViewTextBoxColumn dgvcQctyp = new DataGridViewTextBoxColumn();
                dgvcQctyp.DataPropertyName = "QCTYP";
                dgvcQctyp.HeaderText = "Type";
                dgvcQctyp.Width = 110;
                dgvcQctyp.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcQctyp);
                dgvcQctyp.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //CRDAT
                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "Inventory In Date";
                dgvcCrdat.Width = 110;
                dgvcCrdat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCrdat);
                dgvcCrdat.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //W/H user
                DataGridViewTextBoxColumn dgvcWhusr = new DataGridViewTextBoxColumn();
                dgvcWhusr.DataPropertyName = "WHUSR";
                dgvcWhusr.HeaderText = "W/H User";
                dgvcWhusr.Width = 55;
                dgvcWhusr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWhusr);
                dgvcWhusr.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //Remark
                DataGridViewTextBoxColumn dgvcRemak = new DataGridViewTextBoxColumn();
                dgvcRemak.DataPropertyName = "REMAK";
                dgvcRemak.HeaderText = "Remark";
                dgvcRemak.Width = 110;
                dgvcRemak.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRemak);
                dgvcRemak.DefaultCellStyle.BackColor = Color.WhiteSmoke;

                //dgvData.Columns.Add("WERKS", "厂区\n(Plant)");
                //dgvData.Columns.Add("LGORT", "仓别\n(Storage)");
                //dgvData.Columns.Add("LOCAT", "储位\n(Location)");
                //dgvData.Columns.Add("MATNR", "料号\n(Part No)");
                //dgvData.Columns.Add("ALQTY", "数量\n(Qty)");
                //dgvData.Columns.Add("QCTYP", "检验状态\n(Type)");
                //dgvData.Columns.Add("QCUSR", "IQC人员\n(IQC User)");
                //dgvData.Columns.Add("CRDAT", "入库日期\n(In Date)");
                //dgvData.Columns.Add("VBELN", "EC单号\n(EC No.)");
                //dgvData.Columns.Add("WHUSR", "仓库人员\n(W/H User)");
                //dgvData.Columns.Add("REMAK", "备注\n(Remark)");
            #endregion
            //庫存查詢

            }
            #endregion
       }
        #endregion
        #region dgvData_Paint()
        private void dgvData_Paint(object sender, PaintEventArgs e)
        {

            int width1 = 0;
            int width2=0;
            string[] strheader = { "原始库存", "移转库存" };
            int j = 0;
            for (int i = 0; i < strheader.Length; i++)
            {
                // j控制top header欄從第幾個子欄開始
                Rectangle r1 = this.dgvData.GetCellDisplayRectangle(0, -1, false);
                switch (i)
                {
                    case  0:
                        j = 0; //起始位置(儲存格)
                        r1 = this.dgvData.GetCellDisplayRectangle(j, -1, false); 
                        r1.X += 1;
                        r1.Y += 1;
                        //0-9
                        for (int k = 0; k < 8; k++)
                        {
                            width1 = width1+ dgvData.Columns[k].Width;
                        }
                        r1.Width = width1;
                        r1.Height = r1.Height / 2 - 1;
                        break;
                    case 1:
                        j = 9; //起始位置(儲存格)
                        r1 = this.dgvData.GetCellDisplayRectangle(j, -1, false); 
                        r1.X += 1;
                        r1.Y += 1;
                        for (int k = 9; k < 16; k++)
                        {
                            width2 = width2 + dgvData.Columns[k].Width;
                        }
                        r1.Width = width2;
                        r1.Height = r1.Height / 2 - 1;
                        break;
                }
                

                e.Graphics.FillRectangle(new SolidBrush(this.dgvData.ColumnHeadersDefaultCellStyle.BackColor), r1);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(strheader[i], this.dgvData.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(this.dgvData.ColumnHeadersDefaultCellStyle.ForeColor), r1, format);



            }

        }


        #endregion
        #region dgvData_CellPainting()
        void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {

                e.PaintBackground(e.CellBounds, false);



                Rectangle r2 = e.CellBounds;

                r2.Y += e.CellBounds.Height / 2;

                r2.Height = e.CellBounds.Height / 2;

                e.PaintContent(r2);

                e.Handled = true;

            }
        }
        #endregion
        #region ShowDdlLgort(設定倉別下拉式選單內容)
        private void ShowDdlLgort()
		{
			try
			{
				stsWarning.Text = "";
				DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
				if(cmbWerks.SelectedIndex != -1)
				{
					strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
					//dtTemp = objPlantData.GetDdlLgortData(strWerks);
					dtTemp = objAuthority.CheckLgortAuthority(strWerks);
				}
				else
				{
					//dtTemp = objPlantData.GetDdlLgortData();
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
                    int j=0;
					for(int i=0;i<dtTemp.Rows.Count;i++)
					{
                        
                        if ((dtTemp.Rows[i]["F_TEXT"].ToString()).Substring(0, 2) == "SL")
                        {
                            cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                            if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                            {
                                cmbLgort.SelectedIndex = j;
                                j = j + 1;
                            }
                        }
                       
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDdlLgort()");
			}
        }
        #endregion
        #region ShowDdlLgortTo(設定To倉別下拉式選單內容)
        private void ShowDdlLgortTo()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerksTo.SelectedIndex != -1)
                {
                    strWerksTo = cmbWerksTo.Items[cmbWerksTo.SelectedIndex].ToString();
                    //dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerksTo);
                }
                else
                {
                    //dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgortTo.SelectedIndex != -1)
                {
                    strLgortTo = cmbLgort.Items[cmbLgortTo.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgortTo.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgortTo.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgortTo.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgortTo.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgortTo && strLgortTo != "")
                        {
                            cmbLgortTo.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgortTo()");
            }
        }
        #endregion
        #region ShowOutSourceDataGrid(設定gv欄位)
        #endregion

        #region cmbWerks_SelectedIndexChanged(選取From廠區觸發事件)
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion
        #region cmbLgort_SelectedIndexChanged(選擇From倉別觸發事件)
        private void cmbLgort_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
        }
        #endregion
        #region cmbWerksTo_SelectedIndexChanged(選取To廠區觸發事件)
        private void cmbWerksTo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgortTo();
        }
        #endregion
        #region cmbLgortTo_SelectedIndexChanged(選擇To倉別觸發事件)
        private void cmbLgortTo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            strLgortTo = cmbLgortTo.Items[cmbLgortTo.SelectedIndex].ToString();
        }
        #endregion

        #region btnQuery_Click(點擊Query按鈕)
        private void btnQuery_Click(object sender, EventArgs e)
        {
            bolQuery = true;
            #region 檢驗入庫功能(IQC→WH)
            if (rdoGR.Checked == true)
            {
                #region 防呆
                //檢查是否輸入IQC人員工號
                if (txtUsr.Text.ToString().Trim() == "")
                {
                    bolQuery = false;
                    MessageBox.Show("請先輸入IQC人員工號", "提示信息");
                //stsWarning.Text = "Please input User ID !!";
                }
                //檢查是否輸入八位數
                else
                {
                if (txtUsr.Text.ToString().Trim().Length != 8 )
                    {
                        bolQuery = false;
                        stsWarning.Text = "Wrong User ID !!";
                    }
                }
                #endregion

                if (bolQuery == true)
                {
                    StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                    //預設帶IQC儲位
                    dtData = objStorageData.QueryQuarantinedAreaStorageData("IN","IQC001", "O", "", "", "", "", txtUsr.Text.ToString().Trim());

                    if (dtData.Rows.Count > 0)
                    {
                        #region 設定gv內容
                        //增加欄位
                        dtInsertData = dtData.Clone();
                        dtUpdateData = dtData.Clone();
                        dtData.Columns.Add("WERKS_To");
                        dtData.Columns.Add("LGORT_To");
                        dtData.Columns.Add("LOCAT_To");
                        dtData.Columns.Add("ALQTY_To");
                        dtData.Columns.Add("CRDAT_To");
                        ShowDataGrid();
                        #endregion
                        #region 設定元件狀態
                        SetSelectToLocat();
                        #endregion

                        stsWarning.Text = "Query OK!! Please input '" + gbHeaderTo.Text.ToString() + " details'";
                    }
                    else
                    {
                        stsWarning.Text = "No Data !!";
                    }
                }
            }
            #endregion
            #region 檢驗出庫功能(WH→IQC)
            if (rdoGI.Checked == true)
            {
                strLocat = txtLocat.Text.ToString().Trim();
                if (strLocat == "")
                {
                    stsWarning.Text = "Please input 'Location' !!";
                    bolQuery = false;
                }
                if (strLocat != "")
                {
                    if (bolQuery == true)
                    {
                        #region 查詢庫存
                        //檢驗出庫
                        if (rdoGI.Checked == true)
                        {
                            StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                            //僅查詢未檢驗狀態的庫存
                            dtData = objStorageData.QueryQuarantinedAreaStorageData("OUT",strLocat, "N", txtStartMatnr.Text.ToString().Trim(), txtEndMatnr.Text.ToString().Trim(), "", "", "");
                        }
                        #endregion

                        if (dtData.Rows.Count > 0)
                        {
                            //增加欄位
                            dtInsertData = dtData.Clone();
                            dtUpdateData = dtData.Clone();
                            dtData.Columns.Add("WERKS_To");
                            dtData.Columns.Add("LGORT_To");
                            dtData.Columns.Add("LOCAT_To");
                            dtData.Columns.Add("ALQTY_To");
                            dtData.Columns.Add("CRDAT_To");
                            //將空值改為N
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                if (dtData.Rows[i]["QCTYP"].ToString().Trim() == "")
                                {
                                    dtData.Rows[i]["QCTYP"] = "N";
                                }
                            }
                            #region 設定gv內容
                            //增加欄位
                            ShowDataGrid();
                            #endregion
                            #region 設定元件狀態
                            SetSelectToLocat();
                            #endregion
                            dtInsertData = dtData.Clone();
                            dtUpdateData = dtData.Clone();
                            stsWarning.Text = "Query OK!! Please input '" + gbHeaderTo.Text.ToString() + " details'";
                        }
                        else
                        {
                            stsWarning.Text = "No Data !!";
                        }
                    }
                }
            }
            #endregion
            #region 庫存查詢功能
            if (rdoQuery.Checked==true)
            {
                #region 查詢庫存
                if (chkDate.Checked == true)
                {
                    //起始時間
                    strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
                    //結束時間
                    strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");
                }
                else
                {
                    strStartDate = "";
                    strEndDate="";
                }
                //人員工號
                strUsrQry= txtUsrQry.Text.ToString().Trim();
                //儲位
                strLocat=txtLocat.Text.ToString().Trim();
                StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                //取得檢驗狀態
                if (cmbQCtyp.SelectedIndex == 0 || cmbQCtyp.SelectedIndex == -1)
                {
                    dtData = objStorageData.QueryQuarantinedAreaStorageData("QUERY",strLocat, "", txtStartMatnr.Text.ToString().Trim(), txtEndMatnr.Text.ToString().Trim(), strStartDate, strEndDate, strUsrQry);
                }
                if (cmbQCtyp.SelectedIndex == 1)
                {
                    dtData = objStorageData.QueryQuarantinedAreaStorageData("QUERY",strLocat, "N", txtStartMatnr.Text.ToString().Trim(),  txtEndMatnr.Text.ToString().Trim(), strStartDate, strEndDate, strUsrQry);
                }
                if (cmbQCtyp.SelectedIndex == 2)
                {
                    dtData = objStorageData.QueryQuarantinedAreaStorageData("QUERY",strLocat, "O", txtStartMatnr.Text.ToString().Trim(), txtEndMatnr.Text.ToString().Trim(), strStartDate, strEndDate, strUsrQry);
                }
                if (cmbQCtyp.SelectedIndex == 3)
                {
                    dtData = objStorageData.QueryQuarantinedAreaStorageData("QUERY", strLocat, "Y", txtStartMatnr.Text.ToString().Trim(), txtEndMatnr.Text.ToString().Trim(), strStartDate, strEndDate, strUsrQry);
                }

                #endregion
                //新增欄位
                //修改顯示名稱
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (dtData.Rows[i]["QCTYP"].ToString().Trim() == "")
                    {
                        dtData.Rows[i]["QCTYP"] = "N";
                    }
                }
                    if (dtData.Rows.Count > 0)
                    {
                        #region 設定gv內容
                        ShowDataGrid();
                        #endregion
                        #region 設定元件狀態
                        SetSelectToLocatQuery();
                        cmbWerksTo.Enabled = false;
                        cmbLgortTo.Enabled = false;
                        #endregion
                        dtInsertData = dtData.Clone();
                        dtUpdateData = dtData.Clone();
                        
                        //stsWarning.Text = "Query OK!! Please input '" + gbHeaderTo.Text.ToString() + " details'";
                    }
                    else
                    {
                        stsWarning.Text = "No Data !!";
                    }

            }
            #endregion
        }
        #endregion
        #region btnSave_Click(點選Save按鈕)
        private void btnSave_Click(object sender, EventArgs e)
        {
            //判斷是否儲存成功
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            #region 檢驗出庫
            if (rdoGI.Checked == true)
            {
                if (objStorageIn.AddIQCIndata(dtInsertData, dtUpdateData, "OUT"))
                {
                    stsWarning.Text = "Save OK !!";
                    //設定元件狀態
                    SetSaveOK();
                }
                else
                {
                    stsWarning.Text = "Save Fail !!";
                    SetSaveOK();
                }
            }
           
            #endregion
            #region 檢驗入庫
            if (rdoGR.Checked == true)
            {
                if (objStorageIn.AddIQCIndata(dtInsertData, dtUpdateData, "IN"))
                {
                    stsWarning.Text = "Save OK !!";
                    //設定元件狀態
                    SetSaveOK();
                }
                else
                {
                    stsWarning.Text = "Save Fail !!";
                    SetSaveOK();
                }
            }
           
            #endregion
          
        }
        #endregion
        #region btnRefresh_Click(點選Refresh按鈕)
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetInitial();
        }
        #endregion
        #region btnExit_Click(點選Save按鈕)
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region rdoReq_CheckedChanged(選取檢驗入庫單選紐)
        private void rdoReq_CheckedChanged(object sender, EventArgs e)
        {
            SetSelectLocat();
            ShowDdlWerks();
            gbHeader.Text = "IQC";
            gbHeaderTo.Text = "W/H";
        }
        #endregion
        #region rdoReturn_CheckedChanged(選取檢驗出庫單選紐)
        private void rdoReturn_CheckedChanged(object sender, EventArgs e)
        {
            SetSelectLocat();
            ShowDdlWerks();
            gbHeader.Text = "W/H";
            gbHeaderTo.Text = "IQC";
        }
        #endregion
        #region rdoQuery_CheckedChanged(選取庫存查詢單選紐)
        private void rdoQuery_CheckedChanged(object sender, EventArgs e)
        {
            //設定初始值
            SetSelectLocatQuery();
            ShowDdlWerks();
            ShowInspctSts();
            gbHeader.Text = "Inventory";
            gbHeaderTo.Text = "";
            //設定啟用狀態
            gbHeaderQuery.Enabled = true;
            cmbQCtyp.Enabled = true;
            txtUsrQry.Enabled = true;
            txtStartMatnr.Enabled = true;
            txtEndMatnr.Enabled = true;

            
        }
        #endregion

        #region SetIntial(設定初始狀態)
        private void SetInitial()
        {
            //按鈕
            btnSave.Enabled = false;
            btnRefresh.Enabled = false;
            btnExit.Enabled = true;
            btnQuery.Enabled = false;
            btnReport.Enabled = false;

            //單選紐
            rdoGR.Checked = false;
            rdoGI.Checked = false;
            rdoQuery.Checked = false;

            rdoGR.Enabled = true;
            rdoGI.Enabled = true;
            rdoQuery.Enabled = true;

            gbHeader.Text = "FROM";
            gbHeaderTo.Text = "TO";
            //下拉式選單
            cmbWerks.Enabled = false;
            cmbLgort.Enabled = false;
            cmbWerksTo.Enabled = false;
            cmbLgortTo.Enabled = false;

            cmbWerks.Items.Clear();
            cmbLgort.Items.Clear();
            cmbWerksTo.Items.Clear();
            cmbLgortTo.Items.Clear();

            //文字方塊
            txtLocat.Text = "";
            txtLocatTo.Text = "";
            txtUsr.Text = "";
            txtUsrTo.Text = "";
            txtMatnrSelect.Text = "";

            stsWarning.Text = "";

            txtLocat.Enabled = false;
            txtLocatTo.Enabled = false;
            txtUsr.Enabled = false;
            txtUsrTo.Enabled = false;
            txtMatnrSelect.Enabled = false;

            this.drData = null;
            dtData = null; ;
            dtInsertData = null;
            this.dgvData.DataSource = null;
            this.lblCount.Text = "0 records";
        }
        #endregion
        #region SetSelectLocat(設定選取廠區倉別儲位_檢驗入出庫)
        private void SetSelectLocat()
        {
            //檢驗入庫
            if (rdoGR.Checked == true)
            {
                //按鈕
                btnSave.Enabled = false;
                btnRefresh.Enabled = true;
                btnExit.Enabled = true;
                btnQuery.Enabled = true;
                //下拉式選單
                gbHeader.Enabled = true;
                gbHeaderTo.Enabled = false;
                this.cmbWerks.Enabled = true;
                this.cmbLgort.Enabled = true;
                this.cmbWerksTo.Enabled = false;
                this.cmbLgortTo.Enabled = false;
                //單選紐
                rdoGR.Enabled = false;
                rdoGI.Enabled = false;
                rdoQuery.Enabled = false;
                //文字方塊
                txtLocat.Enabled = false;
                txtLocatTo.Enabled = false;
                txtUsr.Enabled = true;
                txtUsrTo.Enabled = false;
            }
            //檢驗出庫
            if (rdoGI.Checked == true)
            {
                //按鈕
                btnSave.Enabled = false;
                btnRefresh.Enabled = true;
                btnExit.Enabled = true;
                btnQuery.Enabled = true;
                //下拉式選單
                gbHeader.Enabled = true;
                gbHeaderTo.Enabled = false;
                this.cmbWerks.Enabled = true;
                this.cmbLgort.Enabled = true;
                this.cmbWerksTo.Enabled = false;
                this.cmbLgortTo.Enabled = false;
                //單選紐
                rdoGR.Enabled = false;
                rdoGI.Enabled = false;
                rdoQuery.Enabled = false;
                //文字方塊
                txtLocat.Enabled = true;
                txtLocatTo.Enabled = false;
                txtUsr.Enabled = false;
                txtUsrTo.Enabled = false;
            }
            gbHeaderQuery.Enabled = false;
        }
        #endregion
        #region SetSelectLocatQuery(設定選取廠區倉別儲位_庫存查詢)
        private void SetSelectLocatQuery()
        {
            //按鈕
            btnSave.Enabled = false;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
            btnQuery.Enabled = true;
            btnReport.Enabled = true;
            //下拉式選單
            gbHeader.Enabled = true;
            gbHeaderTo.Enabled = false;
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.cmbWerksTo.Enabled = false;
            this.cmbLgortTo.Enabled = false;
            //單選紐
            rdoGR.Enabled = false;
            rdoGI.Enabled = false;
            rdoQuery.Enabled = false;
            //文字方塊
            txtLocat.Enabled = true;
            txtLocatTo.Enabled = false;
            txtUsr.Enabled = false;
            txtUsrTo.Enabled = false;
            //群組
            gbHeaderQuery.Enabled = true;
        }
        #endregion
        #region SetSelectToLocat(設定選取廠區倉別儲位_檢驗入出庫)
        private void SetSelectToLocat()
        {
            //按鈕
            btnSave.Enabled = false;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
            btnQuery.Enabled = false;
            btnReport.Enabled = true;
            //下拉式選單
            this.gbHeader.Enabled = true;
            this.gbHeaderTo.Enabled = true;
            this.cmbWerks.Enabled = false;
            this.cmbLgort.Enabled = false;
            if (rdoGI.Checked == true)
            {
                this.cmbWerksTo.Enabled = false;
                this.cmbLgortTo.Enabled = false;

                txtLocatTo.Enabled = false;
            }
            if (rdoGR.Checked == true)
            {
                this.cmbWerksTo.Enabled = true;
                this.cmbLgortTo.Enabled = true;

                txtLocatTo.Enabled = true;
            }

            
            //單選紐
            rdoGR.Enabled = false;
            rdoGI.Enabled = false;
            rdoQuery.Enabled = false;
            //文字方塊
            txtLocat.Enabled = false;
            txtUsr.Enabled = true;
            txtUsrTo.Enabled = true;
            txtMatnrSelect.Enabled = true;
        }
        #endregion
        #region SetSelectToLocatQuery(設定選取廠區倉別儲位_檢驗入出庫)
        private void SetSelectToLocatQuery()
        {
            //按鈕
            btnSave.Enabled = false;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
            btnQuery.Enabled = false;
            //下拉式選單
            this.gbHeader.Enabled = false;
            this.gbHeaderTo.Enabled = true;
            this.cmbWerks.Enabled = false;
            this.cmbLgort.Enabled = false;
            this.cmbWerksTo.Enabled = true;
            this.cmbLgortTo.Enabled = true;
            //單選紐
            rdoGR.Enabled = false;
            rdoGI.Enabled = false;
            rdoQuery.Enabled = false;
            //文字方塊
            txtLocat.Enabled = false;
            txtLocatTo.Enabled = false;
            txtUsr.Enabled = false;
            txtUsrTo.Enabled = false;
        }
        #endregion
        #region SetSave(設定可儲存)
        private void SetSave()
        {
            btnSave.Enabled = true;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
            btnQuery.Enabled = false;
        }
        #endregion
        #region SetSaveOK(設定儲存完成)
        private void SetSaveOK()
        {
            btnSave.Enabled = false;
            btnRefresh.Enabled = true;
            btnExit.Enabled = true;
            btnQuery.Enabled = false;
        }
        #endregion
        #region dgvData_RowHeaderMouseClick(點選gv row觸發事件)
        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            stsWarning.Text = "";
            int i = e.RowIndex; //gv中點選第iRow 
            if (dtData.Rows[i]["ALQTY_To"].ToString().Trim() == "")
            {
                ShowIQCInOutAdd(i);
            }
            else
            {
               if(rdoGI.Checked==true)
               {
                   stsWarning.Text = "提示讯息：已有检验出库资料，请先保存后再进行操作!!";
               }
               if (rdoGR.Checked == true)
               {
                   stsWarning.Text = "提示讯息：已有检验出库资料，请先保存后再进行操作!!";
               }
            }
           

        }
        #endregion        
        #region ShowIQCInOutAdd(顯示選取gv輸入視窗)
        private void ShowIQCInOutAdd(int intRow)
        {
            #region 檢驗出庫 WH→IQC
            //檢驗出庫 WH→IQC
            if (rdoGI.Checked == true)
            {
                #region 檢查是否為待驗儲位
                //點擊目的儲位提示無法輸入
                if (dtData.Rows[intRow]["LOCAT"].ToString().Trim() != txtLocat.Text.ToString().Trim())
                {
                    bolchk = false;
                    stsWarning.Text = "Part No '" + dtData.Rows[intRow]["MATNR"].ToString().Trim() + "' is not in W/H Location";
                }
                else
                {
                    //檢查訊息是否填寫完整
                    bolchk = CheckInput();
                }
                #endregion

                if (bolchk == true)
                {
                    #region 取得IQC儲位資訊
                    //取得轉入資訊
                    drData = dtInsertData.NewRow();
                    drData["WERKS"] = strWerks;
                    drData["LGORT"] = strLgort;
                    drData["LOCAT"] = "IQC001";
                    drData["MATNR"] = dtData.Rows[intRow]["MATNR"].ToString();
                    drData["WHUSR"] = txtUsr.Text.ToString();
                    drData["QCUSR"] = txtUsrTo.Text.ToString();
                    drData["ALQTY"] = dtData.Rows[intRow]["ALQTY"];
                    drData["VBELN"] = dtData.Rows[intRow]["VBELN"];
                    drData["QCTYP"] = "N";
                    #endregion
                    #region 跳出輸入轉出數量視窗
                    StorageIn_IQC_InOut_Add objStorageIn_Add = new StorageIn_IQC_InOut_Add(drData);
                    objStorageIn_Add.ShowDialog();
                    #endregion
                    #region 取得輸入訊息
                    drData = objStorageIn_Add.GetData();
                    #endregion

                    //若有輸入數量
                    if (drData["ALQTY"].ToString().Trim() != "")
                    {
                        DataRow drNew = dtData.NewRow();
                        drNew["WERKS"] = drData["WERKS"];
                        drNew["LGORT"] = drData["LGORT"];
                        drNew["LOCAT"] = drData["LOCAT"];
                        drNew["MATNR"] = drData["MATNR"];
                        drNew["ALQTY"] = drData["ALQTY"];
                        drNew["WHUSR"] = drData["WHUSR"];
                        drNew["QCUSR"] = drData["QCUSR"];
                        drNew["QCTYP"] = "N";
                        drNew["REMAK"] = drData["REMAK"];
                        drNew["CRDAT"] = drData["CRDAT"];
                        drNew["VBELN"] = drData["VBELN"];
                        #region 更新IQC儲位訊息

                        //若WH→IQC為空值
                        if (dtData.Rows[intRow]["ALQTY_To"].ToString() == "")
                        {
                            dtData.Rows[intRow]["WERKS_To"] = drNew["WERKS"];
                            dtData.Rows[intRow]["LGORT_To"] = drNew["LGORT"];
                            dtData.Rows[intRow]["LOCAT_To"] = drNew["LOCAT"];
                            dtData.Rows[intRow]["ALQTY_To"] = drNew["ALQTY"];
                            dtData.Rows[intRow]["QCTYP"] = drNew["QCTYP"];
                            dtData.Rows[intRow]["REMAK"] = drNew["REMAK"];
                            dtData.Rows[intRow]["CRDAT_To"] = drNew["CRDAT"];
                            dtData.Rows[intRow]["QCUSR"] = drData["QCUSR"];
                            dtData.Rows[intRow]["WHUSR"] = drData["WHUSR"];
                            dtData.Rows[intRow]["VBELN"] = drData["VBELN"];

                            //更新資料表
                            dtInsertData.Rows.Add(drData);
                        }
                        #endregion
                        #region 更新待驗儲位訊息
                        //更新原始庫存量   
                        dtData.Rows[intRow]["ALQTY"] = Decimal.Parse(dtData.Rows[intRow]["ALQTY"].ToString()) - Decimal.Parse(drData["ALQTY"].ToString().Trim());

                        //檢查員tabele是否有資料
                        drNew = null;

                        DataRow[] drTempWH = dtUpdateData.Select("WERKS ='" + dtData.Rows[intRow]["WERKS"].ToString() + "' AND LGORT = '" + dtData.Rows[intRow]["LGORT"].ToString() + "' AND LOCAT = '" + dtData.Rows[intRow]["LOCAT"].ToString() + "' AND MATNR = '" + dtData.Rows[intRow]["MATNR"].ToString() + "'");
                        if (drTempWH.Length > 0)
                        {
                            for (int j = 0; j < dtUpdateData.Rows.Count; j++)
                            {
                                if (dtUpdateData.Rows[j]["WERKS"].ToString() == dtData.Rows[intRow]["WERKS"].ToString() && dtUpdateData.Rows[j]["LGORT"].ToString() == dtData.Rows[intRow]["LGORT"].ToString() && dtUpdateData.Rows[j]["LOCAT"].ToString() == dtData.Rows[intRow]["LOCAT"].ToString() && dtUpdateData.Rows[j]["MATNR"].ToString() == dtData.Rows[intRow]["MATNR"].ToString())
                                {
                                    //更新變動庫存數
                                    dtUpdateData.Rows[j]["ALQTY"] = dtData.Rows[intRow]["ALQTY"];
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //新增變動資訊
                            drNew = dtUpdateData.NewRow();
                            drNew["WERKS"] = dtData.Rows[intRow]["WERKS"];
                            drNew["LGORT"] = dtData.Rows[intRow]["LGORT"];
                            drNew["LOCAT"] = dtData.Rows[intRow]["LOCAT"];
                            drNew["MATNR"] = dtData.Rows[intRow]["MATNR"];
                            drNew["ALQTY"] = dtData.Rows[intRow]["ALQTY"];
                            drNew["WHUSR"] = dtData.Rows[intRow]["WHUSR"];
                            drNew["QCUSR"] = dtData.Rows[intRow]["QCUSR"];
                            drNew["QCTYP"] = dtData.Rows[intRow]["QCTYP"];
                            drNew["REMAK"] = dtData.Rows[intRow]["REMAK"];
                            drNew["CRDAT"] = dtData.Rows[intRow]["CRDAT"];
                            drNew["VBELN"] = dtData.Rows[intRow]["VBELN"];
                            dtUpdateData.Rows.Add(drNew);
                        }
                        #endregion
                        //更新gv內容
                        ShowDataGrid();
                        //設定元件初始狀態
                        SetSave();
                    }
                }
            }
            #endregion
            #region 檢驗入庫 IQC→WH
            //檢驗入庫 IQC→WH
            if (rdoGR.Checked == true)
            {
               //檢查訊息是否填寫完整
                bolchk = CheckInput();
            
                #endregion
                if (bolchk == true)
                {
                    #region 取得WH儲位資訊
                    //取得轉入資訊
                    drData = dtInsertData.NewRow();
                    drData["WERKS"] = strWerksTo;
                    drData["LGORT"] = strLgortTo;
                    drData["LOCAT"] = txtLocatTo.Text;
                    drData["MATNR"] = dtData.Rows[intRow]["MATNR"].ToString();
                    drData["WHUSR"] = txtUsrTo.Text.ToString();
                    drData["QCUSR"] = txtUsr.Text.ToString();
                    drData["ALQTY"] = dtData.Rows[intRow]["ALQTY"];
                    drData["QCTYP"] = "Y";
                    #endregion
                    #region 跳出輸入轉出數量視窗
                    StorageIn_IQC_InOut_Add objStorageIn_Add = new StorageIn_IQC_InOut_Add(drData);
                    objStorageIn_Add.ShowDialog();
                    #endregion
                    #region 取得輸入訊息
                    drData = objStorageIn_Add.GetData();
                    //更新檢驗狀態
                    #endregion
                    //若有輸入數量
                    if (drData["ALQTY"].ToString().Trim() != "")
                    {
                        DataRow drNew = dtData.NewRow();
                        drNew["WERKS"] = drData["WERKS"].ToString().Trim();
                        drNew["LGORT"] = drData["LGORT"].ToString().Trim();
                        drNew["LOCAT"] = drData["LOCAT"].ToString().Trim();
                        drNew["MATNR"] = drData["MATNR"].ToString().Trim();
                        drNew["ALQTY"] = drData["ALQTY"].ToString().Trim();
                        drNew["WHUSR"] = drData["WHUSR"].ToString().Trim();
                        drNew["QCUSR"] = drData["QCUSR"].ToString().Trim();
                        drNew["VBELN"] = drData["VBELN"].ToString().Trim();
                        drNew["QCTYP"] = "Y";
                        drNew["REMAK"] = drData["REMAK"].ToString().Trim();
                        drNew["CRDAT"] = drData["CRDAT"];


                        #region 更新WH儲位訊息
                        //若IQC→WH為空值
                        if (dtData.Rows[intRow]["ALQTY_To"].ToString() == "")
                        {
                            dtData.Rows[intRow]["WERKS_To"] = drNew["WERKS"];
                            dtData.Rows[intRow]["LGORT_To"] = drNew["LGORT"];
                            dtData.Rows[intRow]["LOCAT_To"] = drNew["LOCAT"];
                            dtData.Rows[intRow]["ALQTY_To"] = drNew["ALQTY"];
                            dtData.Rows[intRow]["QCTYP"] = drNew["QCTYP"];
                            dtData.Rows[intRow]["REMAK"] = drNew["REMAK"];
                            dtData.Rows[intRow]["CRDAT_To"] = drNew["CRDAT"];
                            dtData.Rows[intRow]["QCUSR"] = drData["QCUSR"];
                            dtData.Rows[intRow]["WHUSR"] = drData["WHUSR"];

                            //更新資料表
                            dtInsertData.Rows.Add(drData);
                        }

                        #endregion
                        #region 更新IQC儲位訊息
                        //更新原始庫存量   
                        dtData.Rows[intRow]["ALQTY"] = Decimal.Parse(dtData.Rows[intRow]["ALQTY"].ToString()) - Decimal.Parse(drData["ALQTY"].ToString().Trim());

                        //檢查員tabele是否有資料
                        drNew = null;

                        DataRow[] drTempWH = dtUpdateData.Select("WERKS ='" + dtData.Rows[intRow]["WERKS"].ToString() + "' AND LGORT = '" + dtData.Rows[intRow]["LGORT"].ToString() + "' AND LOCAT = '" + dtData.Rows[intRow]["LOCAT"].ToString() + "' AND MATNR = '" + dtData.Rows[intRow]["MATNR"].ToString() + "'");
                        if (drTempWH.Length > 0)
                        {
                            for (int j = 0; j < dtUpdateData.Rows.Count; j++)
                            {
                                if (dtUpdateData.Rows[j]["WERKS"].ToString() == dtData.Rows[intRow]["WERKS"].ToString() && dtUpdateData.Rows[j]["LGORT"].ToString() == dtData.Rows[intRow]["LGORT"].ToString() && dtUpdateData.Rows[j]["LOCAT"].ToString() == dtData.Rows[intRow]["LOCAT"].ToString() && dtUpdateData.Rows[j]["MATNR"].ToString() == dtData.Rows[intRow]["MATNR"].ToString())
                                {
                                    //更新變動庫存數
                                    dtUpdateData.Rows[j]["ALQTY"] = dtData.Rows[intRow]["ALQTY"];
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //新增變動資訊
                            drNew = dtUpdateData.NewRow();
                            drNew["WERKS"] = dtData.Rows[intRow]["WERKS"];
                            drNew["LGORT"] = dtData.Rows[intRow]["LGORT"];
                            drNew["LOCAT"] = dtData.Rows[intRow]["LOCAT"];
                            drNew["MATNR"] = dtData.Rows[intRow]["MATNR"];
                            drNew["ALQTY"] = dtData.Rows[intRow]["ALQTY"];
                            drNew["WHUSR"] = dtData.Rows[intRow]["WHUSR"];
                            drNew["QCUSR"] = dtData.Rows[intRow]["QCUSR"];
                            drNew["QCTYP"] = dtData.Rows[intRow]["QCTYP"];
                            drNew["REMAK"] = dtData.Rows[intRow]["REMAK"];
                            drNew["CRDAT"] = dtData.Rows[intRow]["CRDAT"];
                            drNew["VBELN"] = dtData.Rows[intRow]["VBELN"];
                            dtUpdateData.Rows.Add(drNew);
                        }
                        #endregion
                        //更新gv內容
                        ShowDataGrid();
                        //設定元件初始狀態
                        SetSave();
                    }
                }
            }
        }
            #endregion
       
        
        #region CheckInput(檢查是否輸入訊息)
        private bool CheckInput()
        {
            if (rdoGR.Checked == true)
            {
                //檢查是否選擇廠區
                if (cmbWerksTo.SelectedIndex == -1)
                {
                    stsWarning.Text = "Please select " + gbHeaderTo.Text.ToString() + " Plant !!";
                    return false;
                }
                //檢查是否選擇倉別
                if (cmbLgortTo.SelectedIndex == -1)
                {
                    stsWarning.Text = "Please select " + gbHeaderTo.Text.ToString() + " Storage !!";
                    return false;
                }
                //檢查是否輸入儲位
                if (txtLocatTo.Text.ToString().Trim() == "")
                {
                    stsWarning.Text = "Please input " + gbHeaderTo.Text.ToString() + " Location !!";
                    return false;
                }
                else //檢查是否有此儲位
                {
                    StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                    if (!objStorageData.QueryLocationExistsInStorage(cmbWerksTo.Items[cmbWerksTo.SelectedIndex].ToString(), cmbLgortTo.Items[cmbLgortTo.SelectedIndex].ToString(), txtLocatTo.Text.ToString().Trim()))
                    {
                        stsWarning.Text = "Location is not exist !!";
                        return false;
                    }
                }
            }

            //檢查是否輸入工號
            if (txtUsrTo.Text.ToString().Trim() == "")
            {
                //stsWarning.Text = "Please input " + gbHeaderTo.Text.ToString() + " User ID !!";
                MessageBox.Show("请先输入" + gbHeaderTo.Text.ToString() + "人员工号", "提示信息");
                return false;
            }
            //檢查是否輸入工號
            if (txtUsr.Text.ToString().Trim()=="")
            {
                MessageBox.Show("请先输入" + gbHeader.Text.ToString() + "人员工号", "提示信息");
                //stsWarning.Text = "Please input " + gbHeader.Text.ToString() + " User ID !!";
                return false;
            }
            if ((txtUsr.Text.ToString().Trim()).Length!= 8)
            {
                stsWarning.Text = "Wrong " + gbHeader.Text.ToString() + " User ID !!";
                return false;
            }
            if ((txtUsrTo.Text.ToString().Trim()).Length != 8)
            {
                stsWarning.Text = "Wrong " + gbHeaderTo.Text.ToString() + " User ID !!";
                return false;
            }
            return true;
        }
        #endregion
        #region txtLocat_DoubleClick(雙擊文字方塊Location)
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
                    if (rdoQuery.Checked == true) //帶出所有儲位包含IQC待驗儲位
                    {
                        StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, "QUERY_INSPEC_IQC");
                        objStorageIn_LocationSelect.ShowDialog();
                        txtLocat.Text = objStorageIn_LocationSelect.Locat;
                    }
                    else 
                    {
                        StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, "ADD_INSPEC");
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
        #region txtUsr_DoubleClick(點擊IQC人員工號)
        private void txtUsr_DoubleClick(object sender, EventArgs e)
        {
            //if (rdoGI.Checked == false)//點選查詢和檢驗出庫
            //{
            //
            //}
        }
        #endregion
        #region txtLocatTo_DoubleClick(點擊文字方塊 Location To)
        private void txtLocatTo_DoubleClick(object sender, EventArgs e)
        {
            if (cmbWerksTo.SelectedIndex != -1)
                Werks = cmbWerksTo.Items[cmbWerksTo.SelectedIndex].ToString();
            else
                Werks = "";

            if (cmbLgortTo.SelectedIndex != -1)
                Lgort = cmbLgortTo.Items[cmbLgortTo.SelectedIndex].ToString();
            else
                Lgort = "";

            if (Werks == "" || Lgort == "")
            {
                stsWarning.Text = "Plant and storage can't be empty!!";
                return;
            }
            else
            {
                StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, "ADD_INSPEC");
                objStorageIn_LocationSelect.ShowDialog();
                txtLocatTo.Text = objStorageIn_LocationSelect.Locat;
            }
        }
        #endregion

        #region txtLocat_KeyDown(輸入Enter鍵)
        private void txtLocat_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }
        #endregion
        #region txtMatnrSelect_KeyDown(輸入料號)
        private void txtMatnrSelect_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (txtMatnrSelect.Text.ToString().Trim() != "")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAdd_Click(null, null);
                }
            }
            else
            {
                stsWarning.Text = "Please input Part No !!";
            }

        }
        #endregion
        #region btnReport_Click(按下Report按鈕)
        private void btnReport_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {

                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    CountingResult2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion
        #region CountingResult2File(寫入Excel資料表)
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Plant\tStorage\tLocation\tPart No\tQty\tInspection State\tIQC User\tW/H User\tRemark\tIn Date\tEC No.";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["ALQTY"].ToString() + "\t";
                    strLine += dtData.Rows[i]["QCUSR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["WHUSR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["REMAK"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CRDAT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["VBELN"].ToString();
                    //strLine += dtOutSource.Rows[i]["EXRMK"].ToString(); 紀錄存取時間

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
        #endregion
        #region btnAdd_Click(點選新增按鈕)
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMatnrSelect.Text.ToString().Trim() == "")
            {
                stsWarning.Text = "Please input Part No.!!";
            }
            else
            {
                //從gv中找record
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //檢查gv是否有符合的料號
                    if (dtData.Rows[i]["MATNR"].ToString().Trim() == txtMatnrSelect.Text.ToString().Trim() && Int32.Parse(dtData.Rows[i]["ALQTY"].ToString().Trim())>0)
                    {
                        ShowIQCInOutAdd(i);
                        break;
                    }
                }
            }
        }
        #endregion
        #region
        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDate.Checked == true)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }
        }
        #endregion

    }
}
