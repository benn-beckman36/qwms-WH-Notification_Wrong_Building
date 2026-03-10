using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;
using System.Text;

namespace QWMS
{
    /// <summary>
    /// StorageOut_OnLineOut 的摘要描述。
    /// </summary>
    public partial class StorageOut_Produce_SimulationDocument : System.Windows.Forms.Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strGrpid = "";
        private string strCrdat = "";
        private string strType = "";
        private string strQwms = "";
        private string strCategory = "";
        private string strProgid = "";
        private DataTable dtIdData = new DataTable();
        private DataTable dtSmtData = new DataTable();
        private DataTable dtFinData = new DataTable();
        private DataRow drRow;
        private DataTable dtTempStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private DataTable dtQueryQwmsStorage = new DataTable();
        private DataTable dtFinalStorage = new DataTable();
        private DataTable dtSmtStorage = new DataTable();
        private DataTable dtSmtTempData = new DataTable();
        private DataTable dtFinalTempData = new DataTable();

        #endregion

        #region DataMember

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

        public string Grpid
        {
            get
            {
                return strGrpid;
            }
            set
            {
                strGrpid = value;
            }
        }

        public String Type
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

        public String Category
        {
            get
            {
                return strCategory;
            }
            set
            {
                strCategory = value;
            }
        }

        #endregion

        public StorageOut_Produce_SimulationDocument(UserInfo varUserData, string strProgid)
        {
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);

                //檢查權限
                if (!StorageOut.CheckAuthority())
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #region 設定State Bar中的日期
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            Authority objAuthority = new Authority(UserData);
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
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
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

        #region ShowSmtDataGrid
        private void ShowSmtDataGrid()
        {
            dgvOutSource.AutoGenerateColumns = false;
            dgvOutSource.Columns.Clear();
            try
            {
                //WERKS
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 50;
                dgvcWerks.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcWerks);

                //LGORT
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 50;
                dgvcLgort.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcLgort);

                //COSCT
                DataGridViewTextBoxColumn dgvcCosct = new DataGridViewTextBoxColumn();
                dgvcCosct.DataPropertyName = "COSCT";
                dgvcCosct.HeaderText = "Material_Cost_Center";
                dgvcCosct.Width = 110;
                dgvcCosct.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCosct);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMatnr);

                //RLQTY
                DataGridViewTextBoxColumn dgvcRlqty = new DataGridViewTextBoxColumn();
                dgvcRlqty.DataPropertyName = "RLQTY";
                dgvcRlqty.HeaderText = "ReelQty";
                dgvcRlqty.Width = 50;
                dgvcRlqty.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcRlqty);

                //TLQTY
                DataGridViewTextBoxColumn dgvcTlqty = new DataGridViewTextBoxColumn();
                dgvcTlqty.DataPropertyName = "TLQTY";
                dgvcTlqty.HeaderText = "Total request Qty";
                dgvcTlqty.Width = 90;
                dgvcTlqty.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcTlqty);

                //ROVAL
                DataGridViewTextBoxColumn dgvcRoval = new DataGridViewTextBoxColumn();
                dgvcRoval.DataPropertyName = "ROVAL";
                dgvcRoval.HeaderText = "Rounding Value";
                dgvcRoval.Width = 90;
                dgvcRoval.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcRoval);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Storage out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //QWMS MENGE
                DataGridViewTextBoxColumn dgvcQwms_Menge = new DataGridViewTextBoxColumn();
                dgvcQwms_Menge.DataPropertyName = "QWMS_MENGE";
                dgvcQwms_Menge.HeaderText = "QWMS Qty";
                dgvcQwms_Menge.Width = 80;
                dgvcQwms_Menge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcQwms_Menge);

                //Stock MENGE
                DataGridViewTextBoxColumn dgvcStock_Menge = new DataGridViewTextBoxColumn();
                dgvcStock_Menge.DataPropertyName = "REMAIN_MENGE";
                dgvcStock_Menge.HeaderText = "Stock Qty";
                dgvcStock_Menge.Width = 80;
                dgvcStock_Menge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcStock_Menge);

                //Issued MENGE
                DataGridViewTextBoxColumn dgvcIssuedMenge = new DataGridViewTextBoxColumn();
                dgvcIssuedMenge.DataPropertyName = "ISSUED_MENGE";
                dgvcIssuedMenge.HeaderText = "Issued Qty";
                dgvcIssuedMenge.Width = 80;
                dgvcIssuedMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcIssuedMenge);

                //Receipt Storage
                DataGridViewTextBoxColumn dgvcUmlgo = new DataGridViewTextBoxColumn();
                dgvcUmlgo.DataPropertyName = "UMLGO";
                dgvcUmlgo.HeaderText = "Receipt Storage";
                dgvcUmlgo.Width = 90;
                dgvcUmlgo.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcUmlgo);

                //WODAT
                DataGridViewTextBoxColumn dgvcWodat = new DataGridViewTextBoxColumn();
                dgvcWodat.DataPropertyName = "WODAT";
                dgvcWodat.HeaderText = "WorkDate";
                dgvcWodat.Width = 90;
                dgvcWodat.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcWodat);

                //SHIFT
                DataGridViewTextBoxColumn dgvcShift = new DataGridViewTextBoxColumn();
                dgvcShift.DataPropertyName = "SHIFT";
                dgvcShift.HeaderText = "Shift";
                dgvcShift.Width = 40;
                dgvcShift.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcShift);

                //GRPID
                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Group ID";
                dgvcGrpid.Width = 90;
                dgvcGrpid.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcGrpid);

                //TRDAT
                DataGridViewTextBoxColumn dgvcTrdat = new DataGridViewTextBoxColumn();
                dgvcTrdat.DataPropertyName = "TRDAT";
                dgvcTrdat.HeaderText = "TransDate Time";
                dgvcTrdat.Width = 90;
                dgvcTrdat.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcTrdat);

                dgvOutSource.DataSource = dtSmtStorage;
                lblOutSource.Text = dtSmtStorage.Rows.Count.ToString() + " records";

                if (dtSmtStorage.Rows.Count > 0)
                {
                    this.panel1.Enabled = false;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSmtDataGrid()");
            }
        }
        #endregion

        #region ShowFinDataGrid
        private void ShowFinDataGrid()
        {
            dgvOutSource.AutoGenerateColumns = false;
            dgvOutSource.Columns.Clear();
            try
            {
                //WERKS
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 50;
                dgvcWerks.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcWerks);

                //LGORT
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 50;
                dgvcLgort.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcLgort);

                //GRPID
                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Group ID";
                dgvcGrpid.Width = 140;
                dgvcGrpid.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcGrpid);

                //FMATN
                DataGridViewTextBoxColumn dgvcFmatn = new DataGridViewTextBoxColumn();
                dgvcFmatn.DataPropertyName = "FMATN";
                dgvcFmatn.HeaderText = "Father Material";
                dgvcFmatn.Width = 110;
                dgvcFmatn.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcFmatn);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 110;
                dgvcMatnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMatnr);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 50;
                dgvcCharg.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCharg);

                //QMS_QTY
                DataGridViewTextBoxColumn dgvcQmsqty = new DataGridViewTextBoxColumn();
                dgvcQmsqty.DataPropertyName = "QMS_QTY";
                dgvcQmsqty.HeaderText = "QMS Qty";
                dgvcQmsqty.Width = 90;
                dgvcQmsqty.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcQmsqty);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Storage Out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //QWMS QTY
                DataGridViewTextBoxColumn dgvcQwms_Menge = new DataGridViewTextBoxColumn();
                dgvcQwms_Menge.DataPropertyName = "QWMS_MENGE";
                dgvcQwms_Menge.HeaderText = "QWMS Qty";
                dgvcQwms_Menge.Width = 90;
                dgvcQwms_Menge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcQwms_Menge);

                //REMAIN QTY
                DataGridViewTextBoxColumn dgvcRemain_Menge = new DataGridViewTextBoxColumn();
                dgvcRemain_Menge.DataPropertyName = "REMAIN_MENGE";
                dgvcRemain_Menge.HeaderText = "Remain Qty";
                dgvcRemain_Menge.Width = 90;
                dgvcRemain_Menge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcRemain_Menge);

                //WKORD
                DataGridViewTextBoxColumn dgvcWkord = new DataGridViewTextBoxColumn();
                dgvcWkord.DataPropertyName = "WKORD";
                dgvcWkord.HeaderText = "WO";
                dgvcWkord.Width = 90;
                dgvcWkord.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcWkord);

                //ARBPL
                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.Width = 90;
                dgvcArbpl.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcArbpl);

                //COSCT
                DataGridViewTextBoxColumn dgvcCosct = new DataGridViewTextBoxColumn();
                dgvcCosct.DataPropertyName = "COSCT";
                dgvcCosct.HeaderText = "Cost Center";
                dgvcCosct.Width = 90;
                dgvcCosct.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCosct);

                //Status
                DataGridViewTextBoxColumn dgvcStatus = new DataGridViewTextBoxColumn();
                dgvcStatus.DataPropertyName = "STATUS";
                dgvcStatus.HeaderText = "Status";
                dgvcStatus.Width = 90;
                dgvcStatus.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcStatus);

                dgvOutSource.DataSource = dtFinalStorage;
                lblOutSource.Text = dtFinalStorage.Rows.Count.ToString() + " records";
                //dgvOutSource.DataSource = dtFinalData;
                //lblOutSource.Text = dtFinalData.Rows.Count.ToString() + " records";

                if (dtFinalStorage.Rows.Count > 0)
                //if (dtFinalData.Rows.Count > 0)
                {
                    this.panel1.Enabled = false;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowFinDataGrid()");
            }
        }
        #endregion


        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;

        }

        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

        #region ShowGroupId
        private void ShowGroupId()
        {
            try
            {
                SapData objSapData = new SapData(UserData, Werks, Lgort);
                if (cmbType.Text == "SMT")
                {
                    //已扣帳的id不show出來
                    dtIdData = objSapData.QueryGroupIdData(strCrdat, "SMT", true);
                    if (dtIdData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIdData.Rows.Count; i++)
                        {
                            cmbGrpid.Items.Add(dtIdData.Rows[i]["GRPID"]);
                        }

                        this.cmbGrpid.Enabled = true;
                        this.btnQuery.Enabled = true;
                    }
                    else
                    {
                        stsWarning.Text = "No id data!!";
                        return;
                    }
                }
                else if (cmbType.Text == "FINAL")
                {
                    //已扣帳的id不show出來
                    dtIdData = objSapData.QueryGroupIdData(strCrdat, "FINAL", true);
                    if (dtIdData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIdData.Rows.Count; i++)
                        {
                            cmbGrpid.Items.Add(dtIdData.Rows[i]["GRPID"]);
                        }

                        this.cmbGrpid.Enabled = true;
                        this.btnQuery.Enabled = true;
                    }
                    else
                    {
                        stsWarning.Text = "No id data!!";
                        return;
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbGrpid.Items.Clear();
            strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)

            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            else
            {
                strWerks = "";
            }

            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            else
            {
                strLgort = "";
            }
            //廠區倉別不為空
            if (Werks == "" || Lgort == "")
            {
                stsWarning.Text = "Plant and storage can't be empty!!";
                return;
            }
            //Type不為空
            if (cmbType.Text.ToString() == "")
            {
                stsWarning.Text = "Type can't be empty!!";
                return;
            }
            //QWMS/ASRS不為空
            if (cmbQwms.Text.ToString() == "")
            {
                stsWarning.Text = "QWMS/ASRS can't be empty!!";
                return;
            }

            //秀Group id
            ShowGroupId();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                Grpid = cmbGrpid.Items[cmbGrpid.SelectedIndex].ToString();

                //Send/Group id不為空
                if (cmbGrpid.Text.ToString() == "")
                {
                    stsWarning.Text = "Send/Group id can't be empty!!";
                    return;
                }

                Type = cmbType.Text.ToString();
                Category = cmbQwms.Text.ToString();

                SapData objSapData = new SapData(UserData, Werks, Lgort);
                if (cmbType.Text == "SMT")
                {
                    dtSmtData = objSapData.QuerySmtFinData(Grpid, "SMT");
                    if (dtSmtData.Rows.Count == 0)
                    {
                        stsWarning.Text = "No SMT data!!";
                        return;
                    }
                }
                else if (cmbType.Text == "FINAL")
                {
                    dtFinData = objSapData.QuerySmtFinData(Grpid, "FINAL");
                    if (dtFinData.Rows.Count == 0)
                    {
                        stsWarning.Text = "No FINAL data!!";
                        return;
                    }
                }

                StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                StringBuilder sbCombineIndex = new StringBuilder();
                StringBuilder sbFinalCombineIndex = new StringBuilder();
                DataTable dtStorage = new DataTable();   
                int CombineStorageOutQty;                
                DataRow[] findRow;                       
                DataRow[] findRow1;                     
                int RemainQty;                          
                ArrayList alCombine = new ArrayList();  
                ArrayList alAllCombine = new ArrayList();
                DataRow[] combineRow;
                DataRow[] drNoneZero;
                int StorageOutQty;
                int IssuedReelQty;
                string strOderBy = "";
                ArrayList alMatnrQwmsQty = new ArrayList();
                int CombineQwmsTotalQty;

                if (dtSmtData.Rows.Count > 0)
                {
                    dtTempStorage = dtSmtData.Clone();
                    dtTempStorage.Columns.Add("MANDT");
                    dtTempStorage.Columns.Add("COMCD");
                    dtTempStorage.Columns.Add("LGORT");
                    dtTempStorage.Columns.Add("INSMK");
                    dtTempStorage.Columns.Add("CHARG");
                    for (int i = 0; i < dtSmtData.Rows.Count; i++)
                    {
                        drRow = dtTempStorage.NewRow();
                        drRow["MANDT"] = UserData.Client;
                        drRow["COMCD"] = UserData.CompanyCode;
                        drRow["WERKS"] = dtSmtData.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = Lgort;
                        drRow["COSCT"] = dtSmtData.Rows[i]["COSCT"].ToString();
                        drRow["MATNR"] = dtSmtData.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = "G";
                        drRow["CHARG"] = "";
                        drRow["RLQTY"] = dtSmtData.Rows[i]["RLQTY"].ToString();  //卷數
                        drRow["TLQTY"] = dtSmtData.Rows[i]["TLQTY"].ToString();  //需求量
                        drRow["MENGE"] = dtSmtData.Rows[i]["MENGE"].ToString();  //實際要出的數量
                        drRow["WODAT"] = dtSmtData.Rows[i]["WODAT"].ToString();
                        drRow["SHIFT"] = dtSmtData.Rows[i]["SHIFT"].ToString();
                        drRow["GRPID"] = dtSmtData.Rows[i]["GRPID"].ToString();
                        drRow["TRDAT"] = dtSmtData.Rows[i]["TRDAT"].ToString();
                        drRow["ROVAL"] = dtSmtData.Rows[i]["ROVAL"].ToString();  //基數
                        drRow["UMLGO"] = dtSmtData.Rows[i]["UMLGO"].ToString();  //收料倉

                        dtTempStorage.Rows.Add(drRow);
                    }

                    #region 將同料號、庫別、版本的SMT資料作加總  Smose Liao 20100211

                    int ReelQty = 0;
                    int RovalQty = 0;
                    int TotalQty = 0;

                    dtSmtStorage = dtTempStorage.Clone();
                    dtSmtStorage.Columns.Add("QWMS_MENGE");
                    dtSmtStorage.Columns.Add("REMAIN_MENGE");
                    dtSmtStorage.Columns.Add("ISSUED_MENGE");
                    for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                    {

                        #region  //每次比對的Index(sbCombineIndex)  Smose Liao 20100210
                        sbCombineIndex.Remove(0, sbCombineIndex.Length);
                        sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                        sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                        sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                        sbCombineIndex.Append(" and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "'");
                        sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                        sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                        sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                        #endregion

                        //查詢QWMS中的庫存
                        dtQueryQwmsStorage = objStorageData.QueryQwmsData(dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString());

                        if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                        {
                            alAllCombine.Add(sbCombineIndex.ToString());

                            //計算目前QWMS中該料號的庫存數量
                            CombineQwmsTotalQty = 0;
                            combineRow = dtQueryQwmsStorage.Select(sbCombineIndex.ToString());
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                CombineQwmsTotalQty += Int32.Parse(combineRow[j]["MENGE"].ToString());
                            }

                            //計算實際要發的數量(規則：基數(RovalQty)*倍數的數量必須大於需求量(TotalQty))
                            StorageOutQty = 0;
                            ReelQty = int.Parse(dtTempStorage.Rows[i]["RLQTY"].ToString());//卷數
                            RovalQty = int.Parse(dtTempStorage.Rows[i]["ROVAL"].ToString());//基數
                            //TotalQty = int.Parse(dtTempStorage.Rows[i]["TLQTY"].ToString());//總需求量

                            //總需求量 = QMS需求量 - 已發出的數量  Smose Liao 20100823
                            TotalQty = int.Parse(dtTempStorage.Rows[i]["TLQTY"].ToString()) - int.Parse(dtTempStorage.Rows[i]["MENGE"].ToString());

                            //計算已發出的卷數 = 已發出的數量 / 基數  Smose Liao 20100902
                            IssuedReelQty = 0;
                            IssuedReelQty = (int.Parse(dtTempStorage.Rows[i]["MENGE"].ToString()) / RovalQty);

                            //if (RovalQty < TotalQty)  //基數(RovalQty)小於總需求量(TotalQty)
                            //{
                            //    if (TotalQty % RovalQty != 0)
                            //    {
                            //        StorageOutQty = ((TotalQty / RovalQty) + 1) * RovalQty;
                            //    }
                            //    else
                            //    {
                            //        StorageOutQty = (TotalQty / RovalQty) * RovalQty;
                            //    }
                            //}
                            //else  //基數(RovalQty)大於總需求量(TotalQty)
                            //{
                            //    StorageOutQty = RovalQty;
                            //}

                            //發的數量： 卷數 * 基數  //Smose Liao 20100817
                            //StorageOutQty = ReelQty * RovalQty;

                            //發的數量: (QMS要的卷數 - 已發出的卷數) * 基數  //Smose Liao 20100902
                            StorageOutQty = (ReelQty - IssuedReelQty) * RovalQty;

                            //假如要發的數量大於QWMS的庫存量，則只發QWMS的庫存量
                            if (StorageOutQty > CombineQwmsTotalQty)
                            {
                                StorageOutQty = CombineQwmsTotalQty;
                            }

                            drRow = dtSmtStorage.NewRow();
                            drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                            drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                            drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                            drRow["COSCT"] = dtTempStorage.Rows[i]["COSCT"].ToString();
                            drRow["GRPID"] = dtTempStorage.Rows[i]["GRPID"].ToString();
                            drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                            drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                            drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                            drRow["RLQTY"] = dtTempStorage.Rows[i]["RLQTY"].ToString();  //卷數
                            //drRow["TLQTY"] = dtTempStorage.Rows[i]["TLQTY"].ToString();  //需求量
                            drRow["TLQTY"] = TotalQty;  //總需求量
                            drRow["MENGE"] = StorageOutQty;                              //實際要出的數量
                            drRow["QWMS_MENGE"] = CombineQwmsTotalQty;                   //QWMS實際庫存量
                            drRow["REMAIN_MENGE"] = CombineQwmsTotalQty - StorageOutQty; //QWMS實際庫存量 - 實際要出的數量
                            drRow["ISSUED_MENGE"] = int.Parse(dtTempStorage.Rows[i]["MENGE"].ToString());  //先前已發過的數量
                            drRow["WODAT"] = dtTempStorage.Rows[i]["WODAT"].ToString();
                            drRow["SHIFT"] = dtTempStorage.Rows[i]["SHIFT"].ToString();
                            drRow["TRDAT"] = dtTempStorage.Rows[i]["TRDAT"].ToString();
                            drRow["ROVAL"] = dtTempStorage.Rows[i]["ROVAL"].ToString();  //基數
                            drRow["UMLGO"] = dtTempStorage.Rows[i]["UMLGO"].ToString();  //收料倉

                            dtSmtStorage.Rows.Add(drRow);
                        }
                    }

                    #endregion

                    #region //QWMS實際庫存量為0的資料列不顯示
                    dtSmtTempData = dtSmtStorage.Clone();

                    drNoneZero = dtSmtStorage.Select("QWMS_MENGE <> '0'");
                    for (int k = 0; k < drNoneZero.Length; k++)
                    {
                        drRow = dtSmtTempData.NewRow();

                        drRow["MANDT"] = drNoneZero[k]["MANDT"].ToString();
                        drRow["COMCD"] = drNoneZero[k]["COMCD"].ToString();
                        drRow["WERKS"] = drNoneZero[k]["WERKS"].ToString();
                        drRow["LGORT"] = drNoneZero[k]["LGORT"].ToString();
                        drRow["COSCT"] = drNoneZero[k]["COSCT"].ToString();
                        drRow["GRPID"] = drNoneZero[k]["GRPID"].ToString();
                        drRow["MATNR"] = drNoneZero[k]["MATNR"].ToString();
                        drRow["INSMK"] = drNoneZero[k]["INSMK"].ToString();
                        drRow["CHARG"] = drNoneZero[k]["CHARG"].ToString();
                        drRow["RLQTY"] = drNoneZero[k]["RLQTY"].ToString();
                        drRow["TLQTY"] = drNoneZero[k]["TLQTY"].ToString();
                        drRow["MENGE"] = drNoneZero[k]["MENGE"].ToString();
                        drRow["QWMS_MENGE"] = drNoneZero[k]["QWMS_MENGE"].ToString();
                        drRow["REMAIN_MENGE"] = drNoneZero[k]["REMAIN_MENGE"].ToString();
                        drRow["ISSUED_MENGE"] = drNoneZero[k]["ISSUED_MENGE"].ToString();
                        drRow["WODAT"] = drNoneZero[k]["WODAT"].ToString();
                        drRow["SHIFT"] = drNoneZero[k]["SHIFT"].ToString();
                        drRow["TRDAT"] = drNoneZero[k]["TRDAT"].ToString();
                        drRow["ROVAL"] = drNoneZero[k]["ROVAL"].ToString();
                        drRow["UMLGO"] = drNoneZero[k]["UMLGO"].ToString();

                        dtSmtTempData.Rows.Add(drRow);
                    }

                    //將資料存回dtSmtStorage
                    dtSmtStorage.Clear();
                    dtSmtStorage = dtSmtTempData.Copy();

                    //將資料依料號排序
                    strOderBy = "MATNR";
                    dtSmtStorage = CommonInfo.SortDataTable(dtSmtStorage, strOderBy);

                    #endregion

                    ShowSmtDataGrid();  //秀出SMT的資料
                }
                else if (dtFinData.Rows.Count > 0)
                {
                    dtTempStorage = dtFinData.Clone();
                    dtTempStorage.Columns.Add("MANDT");
                    dtTempStorage.Columns.Add("COMCD");
                    dtTempStorage.Columns.Add("LGORT");
                    dtTempStorage.Columns.Add("INSMK");
                    for (int i = 0; i < dtFinData.Rows.Count; i++)
                    {
                        drRow = dtTempStorage.NewRow();
                        drRow["MANDT"] = UserData.Client;
                        drRow["COMCD"] = UserData.CompanyCode;
                        drRow["WERKS"] = dtFinData.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = Lgort;
                        drRow["GRPID"] = dtFinData.Rows[i]["GRPID"].ToString();
                        drRow["FMATN"] = dtFinData.Rows[i]["FMATN"].ToString();
                        drRow["MATNR"] = dtFinData.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = "G";
                        drRow["CHARG"] = dtFinData.Rows[i]["CHARG"].ToString();
                        drRow["MENGE"] = dtFinData.Rows[i]["MENGE"].ToString();
                        drRow["WKORD"] = dtFinData.Rows[i]["WKORD"].ToString();
                        drRow["STATS"] = dtFinData.Rows[i]["STATS"].ToString();
                        drRow["MTYPE"] = dtFinData.Rows[i]["MTYPE"].ToString();
                        drRow["ARBPL"] = dtFinData.Rows[i]["ARBPL"].ToString();
                        drRow["COSCT"] = dtFinData.Rows[i]["COSCT"].ToString();

                        dtTempStorage.Rows.Add(drRow);
                    }

                    #region 將同料號、庫別、版本的Final資料作加總  Smose Liao 20100211

                    #region 計算實際要出的數量與QWMS庫存量
                    dtStorage = dtTempStorage.Clone();  
                    dtStorage.Columns.Add("QWMS_MENGE");
                    for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                    {
                        #region  //每次比對的Index(sbCombineIndex)  Smose Liao 20100210
                        sbCombineIndex.Remove(0, sbCombineIndex.Length);
                        sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                        sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                        sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                        sbCombineIndex.Append(" and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "'");
                        sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                        sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                        sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                        #endregion

                        //查詢QWMS中的庫存
                        dtQueryQwmsStorage = objStorageData.QueryQwmsData(dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString());

                        //計算目前QWMS中該料號的庫存數量
                        CombineQwmsTotalQty = 0;
                        combineRow = dtQueryQwmsStorage.Select(sbCombineIndex.ToString());
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            CombineQwmsTotalQty += Int32.Parse(combineRow[j]["MENGE"].ToString());
                        }

                        if (alCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                        {
                            alCombine.Add(sbCombineIndex.ToString());

                            //by料號、庫別、版本匯總，並計算該料號總共要出的數量
                            CombineStorageOutQty = 0;
                            findRow = dtTempStorage.Select(sbCombineIndex.ToString());
                            for (int j = 0; j < findRow.Length; j++)
                            {
                                CombineStorageOutQty += Int32.Parse(findRow[j]["MENGE"].ToString());
                            }

                            drRow = dtStorage.NewRow();
                            drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                            drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                            drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                            drRow["GRPID"] = dtTempStorage.Rows[i]["GRPID"].ToString();
                            drRow["FMATN"] = dtTempStorage.Rows[i]["FMATN"].ToString();
                            drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                            drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                            drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                            drRow["MENGE"] = CombineStorageOutQty;  //記錄實際要出的總數量
                            drRow["QWMS_MENGE"] = CombineQwmsTotalQty;
                            drRow["ARBPL"] = dtTempStorage.Rows[i]["ARBPL"].ToString();

                            dtStorage.Rows.Add(drRow);
                        }

                    }
                    #endregion

                    dtFinalStorage = dtTempStorage.Clone();
                    dtFinalStorage.Columns.Add("QMS_QTY");
                    dtFinalStorage.Columns.Add("QWMS_MENGE");
                    dtFinalStorage.Columns.Add("REMAIN_MENGE");
                    for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                    {

                        #region  //每次比對的Index(sbCombineIndex)  Smose Liao 20100210
                        sbCombineIndex.Remove(0, sbCombineIndex.Length);
                        sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                        sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                        sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                        sbCombineIndex.Append(" and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "'");
                        sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                        sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                        sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                        #endregion

                        #region Final還要考慮同一個料號，不同的上一階材料(FMATN)、線別(ARBPL)與工單(WKORD)  Add by Smose Liao 20100309
                        sbFinalCombineIndex.Remove(0, sbFinalCombineIndex.Length);
                        sbFinalCombineIndex.Append(sbCombineIndex);
                        sbFinalCombineIndex.Append(" and FMATN='" + dtTempStorage.Rows[i]["FMATN"].ToString() + "'");
                        sbFinalCombineIndex.Append(" and ARBPL='" + dtTempStorage.Rows[i]["ARBPL"].ToString() + "'");
                        sbFinalCombineIndex.Append(" and WKORD='" + dtTempStorage.Rows[i]["WKORD"].ToString() + "'");
                        #endregion

                        if (alAllCombine.IndexOf(sbFinalCombineIndex.ToString()) < 0)
                        {
                            alAllCombine.Add(sbFinalCombineIndex.ToString());

                            //取得目前QWMS中該料號的庫存數量
                            CombineQwmsTotalQty = 0;
                            findRow1 = dtStorage.Select(sbCombineIndex.ToString());
                            CombineQwmsTotalQty = int.Parse(findRow1[0]["QWMS_MENGE"].ToString());

                            //Final實際要出的數量
                            StorageOutQty = 0;
                            StorageOutQty = Int32.Parse(dtTempStorage.Rows[i]["MENGE"].ToString());

                            //假如要出的數量大於QWMS的庫存量，則只出QWMS的庫存量
                            if (StorageOutQty > CombineQwmsTotalQty)
                            {
                                StorageOutQty = CombineQwmsTotalQty;
                            }

                            //取得該料號要出的總數量
                            RemainQty = 0;
                            if (findRow1.Length > 0)
                            {
                                //記錄剩餘的總數量
                                if (CombineQwmsTotalQty == 0)
                                {
                                    RemainQty = 0;
                                }
                                else
                                {
                                    RemainQty = CombineQwmsTotalQty - int.Parse(findRow1[0]["MENGE"].ToString());
                                }
                            }

                            drRow = dtFinalStorage.NewRow();
                            drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                            drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                            drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                            drRow["GRPID"] = dtTempStorage.Rows[i]["GRPID"].ToString();
                            drRow["FMATN"] = dtTempStorage.Rows[i]["FMATN"].ToString();
                            drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                            drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                            drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                            drRow["QMS_QTY"] = dtTempStorage.Rows[i]["MENGE"].ToString(); //QMS原先建議要出的數量
                            drRow["MENGE"] = StorageOutQty;                               //實際要出的數量
                            drRow["QWMS_MENGE"] = CombineQwmsTotalQty;                    //QWMS實際庫存量
                            drRow["REMAIN_MENGE"] = RemainQty;  //QWMS實際庫存量 - 實際要出的數量
                            drRow["WKORD"] = dtTempStorage.Rows[i]["WKORD"].ToString();
                            drRow["STATS"] = dtTempStorage.Rows[i]["STATS"].ToString();
                            drRow["MTYPE"] = dtTempStorage.Rows[i]["MTYPE"].ToString();
                            drRow["ARBPL"] = dtTempStorage.Rows[i]["ARBPL"].ToString();
                            drRow["COSCT"] = dtTempStorage.Rows[i]["COSCT"].ToString();

                            dtFinalStorage.Rows.Add(drRow);
                        }
                    }

                    #endregion

                    #region //QWMS實際庫存量為0的資料列不顯示
                    dtFinalTempData = dtFinalStorage.Clone();

                    drNoneZero = dtFinalStorage.Select("QWMS_MENGE <> '0'");
                    for (int k = 0; k < drNoneZero.Length; k++)
                    {
                        drRow = dtFinalTempData.NewRow();

                        drRow["MANDT"] = drNoneZero[k]["MANDT"].ToString();
                        drRow["COMCD"] = drNoneZero[k]["COMCD"].ToString();
                        drRow["WERKS"] = drNoneZero[k]["WERKS"].ToString();
                        drRow["LGORT"] = drNoneZero[k]["LGORT"].ToString();
                        drRow["GRPID"] = drNoneZero[k]["GRPID"].ToString();
                        drRow["FMATN"] = drNoneZero[k]["FMATN"].ToString();
                        drRow["MATNR"] = drNoneZero[k]["MATNR"].ToString();
                        drRow["INSMK"] = drNoneZero[k]["INSMK"].ToString();
                        drRow["CHARG"] = drNoneZero[k]["CHARG"].ToString();
                        drRow["QMS_QTY"] = drNoneZero[k]["QMS_QTY"].ToString();
                        drRow["MENGE"] = drNoneZero[k]["MENGE"].ToString();
                        drRow["QWMS_MENGE"] = drNoneZero[k]["QWMS_MENGE"].ToString();
                        drRow["REMAIN_MENGE"] = drNoneZero[k]["REMAIN_MENGE"].ToString();
                        drRow["WKORD"] = drNoneZero[k]["WKORD"].ToString();
                        drRow["STATS"] = drNoneZero[k]["STATS"].ToString();
                        drRow["MTYPE"] = drNoneZero[k]["MTYPE"].ToString();
                        drRow["ARBPL"] = drNoneZero[k]["ARBPL"].ToString();

                        dtFinalTempData.Rows.Add(drRow);
                    }

                    //將資料存回dtFinalStorage
                    dtFinalStorage.Clear();
                    dtFinalStorage = dtFinalTempData.Copy();

                    #endregion

                    ShowFinDataGrid();  //秀出Final的資料
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.strWerks = "";
            this.strLgort = "";
            this.stsWarning.Text = "";
            this.cmbWerks.SelectedIndex = 0;
            this.cmbLgort.SelectedIndex = 0;
            this.cmbGrpid.SelectedIndex = -1;
            this.dtSmtData.Clear();
            this.dtFinData.Clear();
            this.dtIdData.Clear();
            this.dtSmtStorage.Clear();
            this.dtFinalStorage.Clear();
            this.cmbGrpid.Items.Clear();
            this.lblOutSource.Text = "0 records";
            this.dgvOutSource.DataSource = null;
            this.panel1.Enabled = true;
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            this.cmbWerks.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtSmtSave = new DataTable();
                DataTable dtFinSave = new DataTable();

                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                if (cmbType.Text == "SMT")
                {
                    dtSmtSave = dtSmtStorage.Clone();
                    //判斷QWMS的實際庫存數量是否為0，若為0則不儲存，亦不產生虛擬單據編號
                    for (int i = 0; i < dtSmtStorage.Rows.Count; i++)
                    {
                        if (int.Parse(dtSmtStorage.Rows[i]["QWMS_MENGE"].ToString()) != 0)
                        {
                            dtSmtSave.ImportRow(dtSmtStorage.Rows[i]);
                        }
                    }

                    if (dtSmtSave.Rows.Count == 0)
                    {
                        stsWarning.Text = "所有Item的QWMS庫存數量皆為0，請重新確認!!";
                        SetbtnSaveException();
                        return;
                    }

                    if (objStorageOut.AddSimulationDocData(dtSmtSave, Type, Category))
                    {
                        stsWarning.Text = "Update OK!!";
                        this.btnQuery.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.btnPrint.Enabled = true;
                    }
                    else
                    {
                        stsWarning.Text = "Update fail!! " + objStorageOut.ERRMSG;
                        SetbtnSaveException();
                        return;
                    }
                }
                else if (cmbType.Text == "FINAL")
                {
                    dtFinSave = dtFinalStorage.Clone();
                    //判斷QWMS的實際庫存數量是否為0，若為0則不儲存，亦不產生虛擬單據編號
                    for (int i = 0; i < dtFinalStorage.Rows.Count; i++)
                    {
                        if (int.Parse(dtFinalStorage.Rows[i]["QWMS_MENGE"].ToString()) != 0)
                        {
                            dtFinSave.ImportRow(dtFinalStorage.Rows[i]);
                        }
                    }

                    if (dtFinSave.Rows.Count == 0)
                    {
                        stsWarning.Text = "所有Item的QWMS庫存數量皆為0，請重新確認!!";
                        SetbtnSaveException();
                        return;
                    }

                    if (objStorageOut.AddSimulationDocData(dtFinSave, Type, Category))
                    {
                        stsWarning.Text = "Update OK!!";
                        this.btnQuery.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.btnPrint.Enabled = true;
                    }
                    else
                    {
                        stsWarning.Text = "Update fail!! " + objStorageOut.ERRMSG;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint = new DataTable();

            QCI.QWMS.StorageData objStorage = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            dtPrint = objStorage.QuerySimulationData(Type, Grpid);

            if (Type == "SMT")
            {
                ReportPrint objReportPrint = new ReportPrint(UserData, "SMTData", dtPrint);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            else if (Type == "FINAL")
            {
                ReportPrint objReportPrint = new ReportPrint(UserData, "FINData", dtPrint);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
        }

        private void cmbGrpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnQuery.Enabled = true;
        }
    }
}
