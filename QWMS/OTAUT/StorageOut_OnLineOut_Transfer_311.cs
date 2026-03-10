using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using QCI.QWMS;
using QWMS.Common;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace QWMS
{
    public partial class StorageOut_OnLineOut_Transfer_311 : Form
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
        private string strFunction = "";
        private string strType = "";
        private string strCategory = "";
        private string strProgid = "";
        private DataRow drRow;
        private DataTable dtIdData = new DataTable();
        private DataTable dtSmtData = new DataTable();
        private DataTable dtFinData = new DataTable();
        private DataTable dtTempStorage = new DataTable();
        private DataTable dtFinalStorage = new DataTable();
        private DataTable dtSmtStorage = new DataTable();
        private DataTable dtSmtTempData = new DataTable();
        private DataTable dtMblnr = new DataTable();
        private DataTable dtData = new DataTable();
        private DataTable dtReturn = new DataTable();
        private DataTable dtDateTime = new DataTable();
        private DataTable dtSmtDataCopy = new DataTable();


        private ArrayList alMblnrs = new ArrayList();
        bool AllowToClose = true;
        private string strGRRNO = "";
        private DataTable dtSLCLgort = new DataTable();//散料仓仓别
        DataTable dtAddDocToSAP;

        QCI.QWMS.PlantData objPlantData;
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

        public String Function
        {
            get
            {
                return strFunction;
            }
            set
            {
                strFunction = value;
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

        public ArrayList Mblnrs
        {
            get
            {
                return alMblnrs;
            }
            set
            {
                alMblnrs = value;
            }
        }

        #endregion

        public StorageOut_OnLineOut_Transfer_311(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);
                objPlantData = new PlantData(UserData);
                Authority objAuthority = new Authority(UserData);
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
                    cmbLgortTo.Text = "TWDP";

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    dtMblnr.Columns.Add("MBLNR");
                    GetDateTime();  //取得目前系統的日期與時間
                    if (Comcd == "9100")
                    {
                        dtSLCLgort = objAuthority.CheckSLCLgortAuthority();
                    }
                    if (dtSLCLgort.Columns.Count == 0)
                    {
                        dtSLCLgort.Columns.Add("WERKS");
                        dtSLCLgort.Columns.Add("LGORT");
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
        #endregion

        #region txtMatnr_TextChanged
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (chkOverdueInspection.Checked)
            {
                cmbLgort.Items.Clear();
                cmbLgort.Items.Add("TWDP");
                if (strWerks == "CS21")
                {
                    cmbLgort.Items.Add("TWDD");
                }
                ShowDdlLgortCheck();

            }
            else
            {
                ShowDdlLgort();
                cmbLgortTo.Items.Clear();
                cmbLgortTo.Items.Add("TWDP");
                if (strWerks == "CS21")
                {
                    cmbLgortTo.Items.Add("TWDD");
                }

            }
        }
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgortCheck()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgortTo.SelectedIndex != -1)
                {
                    strLgort = cmbLgortTo.Items[cmbLgortTo.SelectedIndex].ToString();
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
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgortTo.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgortCheck()");
            }

        }
        #endregion


        #region 正常出庫(rdoNormal_CheckedChanged)
        private void rdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbLgortTo.Text = "";
            cmbLgortTo.Enabled = true;

        }
        #endregion

        #region dtpCrdat_ValueChanged
        private void dtpCrdat_ValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)
        }
        #endregion

        #region 按鈕設定-儲存中
        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;

        }
        #endregion

        #region 按鈕設定-儲存失敗
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }
        #endregion

        #region ShowSmtDataGrid
        private void ShowSmtDataGrid()
        {
            dgvOutSource.AutoGenerateColumns = false;
            dgvOutSource.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn Select = new DataGridViewCheckBoxColumn();
                Select.DataPropertyName = "Select";
                Select.HeaderText = "Select";
                Select.Name = "Select";
                Select.Width = 50;
                dgvOutSource.Columns.Add(Select);

                //WERKS
                DataGridViewTextBoxColumn dgvcID = new DataGridViewTextBoxColumn();
                dgvcID.DataPropertyName = "ID";
                dgvcID.HeaderText = "ID";
                dgvcID.Width = 30;
                dgvcID.Visible = false;
                dgvOutSource.Columns.Add(dgvcID);

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

                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "LOCAT";
                dgvcLocat.Width = 50;
                dgvcLocat.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcLocat);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMatnr);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Storage out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //INSMK
                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "INSMK";
                dgvcINSMK.Width = 90;
                dgvcINSMK.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcINSMK);

                //CHARG
                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.Width = 90;
                dgvcCHARG.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCHARG);

                //DACOD 
                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "DACOD";
                dgvcDACOD.Width = 90;
                dgvcDACOD.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcDACOD);
                
                //UMLGO
                DataGridViewTextBoxColumn dgvcUmlgo = new DataGridViewTextBoxColumn();
                dgvcUmlgo.DataPropertyName = "UMLGO";
                dgvcUmlgo.HeaderText = "UMLGO";
                dgvcUmlgo.Width = 90;
                dgvcUmlgo.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcUmlgo);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "TASKID";
                dgvcTASKID.Width = 90;
                dgvcTASKID.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcTASKID);

                dgvOutSource.DataSource = dtSmtStorage;
                lblOutSource.Text = dtSmtStorage.Rows.Count.ToString() + " records";


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSmtDataGrid()");
            }
        }
        #endregion


        #region btnQuery_Click
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                StringBuilder sbCombineIndex = new StringBuilder();
                StringBuilder sbFinalCombineIndex = new StringBuilder();
                DataTable dtStorage = new DataTable();
                DataTable dtTmp = new DataTable();
                ArrayList alCombine = new ArrayList();
                ArrayList alAllCombine = new ArrayList();
                string strOrderBy = "";
                string strUMLGO = "";
                stsWarning.Text = "";

                #region 出庫

                #region 获取库存数据
                SapData objSapData = new SapData(UserData, Werks, Lgort);

                dtSmtData.Clear();

                if (cmbLgortTo.Text == "")
                {
                    stsWarning.Text = "Please select transfer out of warehouse!!";
                    return;

                }
                if (cmbWerks.Text == "")
                {
                    stsWarning.Text = "Please select Plant!!";
                    return;

                }

                if (cmbLgortTo.Text == Lgort)
                {
                    stsWarning.Text = "Please select right Storage!!";
                    return;

                }

                if (Lgort == "TWDP"|| Lgort == "TWDD")
                {
                    if (cmbLgortTo.Text == "TWDP" || cmbLgortTo.Text == "TWDD")
                    {
                        stsWarning.Text = "Please select right Storage!!";
                        return;

                    }

                }

                if (chkOverdueInspection.Checked)
                {
                    dtSmtData = objSapData.QueryWhitmOverdueInspection("Y", cmbLgortTo.Text);
                }
                else
                {
                    dtSmtData = objSapData.QueryWhitmOverdueInspection("","");
                }

                if (dtSmtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }

                #endregion

                #region Query資訊
                if (dtSmtData.Rows.Count > 0)
                {
                    dtTempStorage.Clear();
                    dtSmtData.Columns.Add("ID");
                    dtSmtData.Columns.Add("OMBLN");
                    dtSmtData.Columns.Add("UMLGO");
                    dtSmtData.Columns.Add("KOSTL");                    
                    dtTempStorage = dtSmtData.Clone();

                    for (int i = 0; i < dtSmtData.Rows.Count; i++)
                    {

                        drRow = dtTempStorage.NewRow();
                        drRow["ID"] = (i+1).ToString();
                        dtSmtData.Rows[i]["ID"] = (i + 1).ToString();

                        drRow["MANDT"] = UserData.Client;
                        drRow["COMCD"] = UserData.CompanyCode;
                        drRow["WERKS"] = dtSmtData.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = Lgort;
                        drRow["LOCAT"] = dtSmtData.Rows[i]["LOCAT"].ToString();                       
                        drRow["MATNR"] = dtSmtData.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = "G";
                        drRow["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? dtSmtData.Rows[i]["CHARG"].ToString() : "";
                        drRow["MENGE"] = dtSmtData.Rows[i]["MENGE"].ToString();  //實際要出的數量
                        drRow["DACOD"] = dtSmtData.Rows[i]["DACOD"].ToString();  //實際要出的數量
                        
                        drRow["KOSTL"] = "0A434";
                        dtSmtData.Rows[i]["KOSTL"] = "0A434";


                        if (chkOverdueInspection.Checked && cmbLgortTo.Text != "RWDP")
                        {
                            DataTable dtWhlogUMLGO = objSapData.QueryWhlogUMLGO(dtSmtData.Rows[i]["MBLNR"].ToString(), dtSmtData.Rows[i]["CHARG"].ToString(), dtSmtData.Rows[i]["LIFNR"].ToString(), dtSmtData.Rows[i]["MATNR"].ToString(), dtSmtData.Rows[i]["DACOD"].ToString());
                            if (dtWhlogUMLGO.Rows.Count > 0)
                            {
                                strUMLGO = dtWhlogUMLGO.Rows[0]["LGORT"].ToString();
                            }
                            else
                            {
                                strUMLGO = "";
                            }

                            drRow["UMLGO"] = strUMLGO;//收料倉
                            dtSmtData.Rows[i]["UMLGO"] = strUMLGO;

                        }
                        else
                        {
                            drRow["UMLGO"] = cmbLgortTo.Text;  //收料倉
                            dtSmtData.Rows[i]["UMLGO"] = cmbLgortTo.Text;
                        }
                        drRow["TASKID"] = dtSmtData.Rows[i]["TASKID"].ToString();  //實際要出的數量


                        dtTempStorage.Rows.Add(drRow);
                    }



                    #region 转出仓别不一致的資料列不顯示
                    dtSmtTempData.Clear();
                    dtSmtTempData = dtTempStorage.Clone();

                    for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                    {
                        if (dtTempStorage.Rows[i]["UMLGO"].ToString() == cmbLgortTo.Text)
                        {
                            dtSmtTempData.ImportRow(dtTempStorage.Rows[i]);
                        }
                    }

                    //將資料存回dtSmtStorage
                    dtSmtStorage.Clear();
                    dtSmtStorage = dtSmtTempData.Copy();
                    //將資料依料號排序
                    strOrderBy = "MATNR";
                    dtSmtStorage = CommonInfo.SortDataTable(dtSmtStorage, strOrderBy);

                    #endregion

                    ShowSmtDataGrid();  //秀出WHITM的資料
                }
                #endregion

                #endregion

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        #endregion

        public void GetDataTableToSAP()
        {
            dtAddDocToSAP = new DataTable();
            dtAddDocToSAP.Columns.Add("MANDT", Type.GetType());
            dtAddDocToSAP.Columns.Add("ZAPPID", Type.GetType());
            dtAddDocToSAP.Columns.Add("ZITEM", Type.GetType());
            dtAddDocToSAP.Columns.Add("WERKS", Type.GetType());
            dtAddDocToSAP.Columns.Add("WERKS_I", Type.GetType());
            dtAddDocToSAP.Columns.Add("LGORT", Type.GetType());
            dtAddDocToSAP.Columns.Add("LGORT_I", Type.GetType());
            dtAddDocToSAP.Columns.Add("MATNR", Type.GetType());
            dtAddDocToSAP.Columns.Add("MENGE", Type.GetType());
            dtAddDocToSAP.Columns.Add("MBLNR", Type.GetType());
            dtAddDocToSAP.Columns.Add("MJAHR", Type.GetType());
            dtAddDocToSAP.Columns.Add("FLAG", Type.GetType());
            dtAddDocToSAP.Columns.Add("MESSAGE", Type.GetType());
            dtAddDocToSAP.Columns.Add("TXDAT", Type.GetType());
            dtAddDocToSAP.Columns.Add("TXTM", Type.GetType());
            dtAddDocToSAP.Columns.Add("TXEMP", Type.GetType());
            dtAddDocToSAP.Columns.Add("KOSTL", Type.GetType());
            dtAddDocToSAP.Columns.Add("CHARG", Type.GetType());
            dtAddDocToSAP.Columns.Add("BWART", Type.GetType());
            dtAddDocToSAP.Columns.Add("AUFNR", Type.GetType());
        }

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            SetbtnSaveProcess();

            DataTable dtTempData = new DataTable();
            try
            {
                stsWarning.Text = "";
                string strMblnrNew = "";
                string strZeile = "";
                int j = 0;
                GetDataTableToSAP();


                DataTable dataTable1 = dtSmtStorage.Clone();
                dataTable1.Rows.Clear();

                foreach (DataGridViewRow row in dgvOutSource.Rows)
                {
                    DataRow newRow = dataTable1.NewRow();
                    if ((bool)row.Cells[0].EditedFormattedValue == true)
                    {
                        DataRowView drv = (DataRowView)row.DataBoundItem;
                        DataRow dr = drv.Row;
                        dataTable1.ImportRow(dr);
                    }
                }

                var commonRows = from table1 in dtSmtStorage.AsEnumerable()
                                 join table2 in dataTable1.AsEnumerable()
                                 on
                                    new { WERKS = table1.Field<string>("ID") }
                                 equals
                                    new { WERKS = table2.Field<string>("ID") }
                                 select table1;

                DataTable result = commonRows.CopyToDataTable();
                dtSmtStorage = result.Copy();



                var commonRows1 = from table1 in dtSmtStorage.AsEnumerable()
                                 join table2 in dtSmtData.AsEnumerable() 
                                 on 
                                    new { WERKS = table1.Field<string>("ID")} 
                                 equals
                                    new { WERKS = table2.Field<string>("ID")}
                                 select table2; 
                DataTable result1 = commonRows1.CopyToDataTable();
                dtSmtData = result1.Copy();


                //sap交互
                #region 将需要加扣的信息填到加扣表

                for (int i = 0; i < dtSmtStorage.Rows.Count; i++)
                {
                    strMblnrNew = dtSmtStorage.Rows[0]["WERKS"].ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
                    j = j + 1;
                    strZeile = (j).ToString().PadLeft(4, '0');

                    DataRow drN = dtAddDocToSAP.NewRow();
                    drN["MANDT"] = UserData.Client;
                    drN["ZAPPID"] = strMblnrNew;
                    drN["ZITEM"] = strZeile;
                    drN["WERKS"] = dtSmtStorage.Rows[i]["WERKS"].ToString();
                    drN["LGORT"] = dtSmtStorage.Rows[i]["LGORT"].ToString();
                    drN["WERKS_I"] = dtSmtStorage.Rows[i]["WERKS"].ToString();//同厂区
                    drN["LGORT_I"] = dtSmtStorage.Rows[i]["UMLGO"].ToString();//转出仓
                    drN["MATNR"] = dtSmtStorage.Rows[i]["MATNR"].ToString();
                    drN["MENGE"] = dtSmtStorage.Rows[i]["MENGE"].ToString();
                    drN["MBLNR"] = "";
                    drN["MJAHR"] = "";
                    drN["FLAG"] = "";
                    drN["MESSAGE"] = "";
                    drN["TXDAT"] = "";
                    drN["TXTM"] = "";
                    drN["TXEMP"] = UserData.UserId;
                    drN["KOSTL"] = dtSmtStorage.Rows[i]["KOSTL"].ToString();
                    drN["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? dtSmtStorage.Rows[i]["CHARG"].ToString() : "";
                    drN["BWART"] = "311";
                    drN["AUFNR"] = "";
                    dtAddDocToSAP.Rows.Add(drN);
                }

                #endregion

                #region SAP扣账

                DataSet dsData = new DataSet();
                dsData.Tables.Add(dtAddDocToSAP);
                MM.MM_Service obj = new QWMS.MM.MM_Service();
                DataSet dsResultFromSAP = obj.Z_MM_RFC_POSTYCN("A", dsData);
                DataTable dtResultFromSAP = dsResultFromSAP.Tables[0];

                //判断是否成功
                if (dtResultFromSAP.Rows[0]["FLAG"].ToString().Trim() == "Y")
                {
                    MessageBox.Show("sap扣账成功！");
                }
                else
                {
                    MessageBox.Show(dtResultFromSAP.Rows[0]["MESSAGE"].ToString().Trim());
                    return;
                }

                for (int i = 0; i < dtSmtData.Rows.Count; i++)
                {
                    dtSmtData.Rows[i]["OMBLN"] = dtResultFromSAP.Rows[0]["MBLNR"].ToString().Trim();
                }
                #endregion



                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);

                strGRRNO = objStorageOut.wsAddOnLineOutData_311_New(dtSmtData);//QWMS扣账
                if (strGRRNO != "")
                {
                    stsWarning.Text = "QWMS保留庫存扣帳成功!!";

                    this.btnSave.Enabled = true;
                    this.btnDacodPrint.Enabled = true;
                    this.btnRefresh.Enabled = true;
                    this.btnExit.Enabled = true;
                    AllowToClose = true;
                    btnQuery_Click(null,null);
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetbtnSaveException();
                return;
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            this.dtSmtData.Clear();
            this.dtFinData.Clear();
            this.dtIdData.Clear();
            this.dtReturn.Clear();
            this.dtMblnr.Clear();
            this.dtSmtStorage.Clear();
            this.dtFinalStorage.Clear();
            this.cmbLgort.SelectedIndex = 0;
            this.lblOutSource.Text = "0 records";
            this.dgvOutSource.DataSource = null;
            this.btnQuery.Enabled = true;
            this.btnSave.Enabled = true;
            this.chkOverdueInspection.Checked = false;
            ShowDdlWerks();
            cmbLgortTo.Text = "";
            //this.gbFunction.Enabled = true;
            //this.dtpCrdat.Enabled = true;
            //this.panel4.Enabled = true;
            //this.panel5.Enabled = false;
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        #region 判斷是否可關閉Form視窗
        private void StorageOut_OnLineOut_SimulationOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AllowToClose;

            if (e.Cancel = !AllowToClose)
            {
                stsWarning.Text = "已產生扣帳單據，無法關閉視窗，請按Query查詢庫存與Save扣帳!!";
            }
        }
        #endregion

        #region cmbLgort_SelectedIndexChanged
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
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
            //廠區/倉別不能為空
            if (Werks == "" || Lgort == "")
            {
                stsWarning.Text = "廠區/倉別不能為空!!";
                return;
            }

        }
        #endregion


        #region 選定倉別之後，讓滑鼠無法移動(cmbLgort_MouseMove)
        private void cmbLgort_MouseMove(object sender, MouseEventArgs e)
        {
            cmbLgort.MouseWheel += new MouseEventHandler(Form_MouseWheel);
        }
        #endregion

        #region Overload handler
        private void Form_MouseWheel(object sender, EventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }
        #endregion

        #region GetDateTime
        public DataTable GetDateTime()
        {
            dtDateTime.Columns.Add("NowDate");
            dtDateTime.Columns.Add("NowHour");
            dtDateTime.Columns.Add("NowMinute");

            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;

            DataRow drRow = dtDateTime.NewRow();
            dtDateTime.Rows.Add(drRow);
            dtDateTime.Rows[0]["NowDate"] = currentTime.Date.ToString("yyyy-MM-dd");
            dtDateTime.Rows[0]["NowHour"] = currentTime.Hour;
            dtDateTime.Rows[0]["NowMinute"] = currentTime.Minute;

            return dtDateTime;
        }
        #endregion

        #region btnDacodPrint_Click
        private void btnDacodPrint_Click(object sender, EventArgs e)
        {
            DataSet dsData = new DataSet();
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            dsData = objStorageData.QueryOnLineOutGrrno_DateCode(dtData, strGRRNO);

            DataTable dtPrint = new DataTable();
            dtData = dsData.Tables[0].Copy();
            dtPrint = dtData.Clone();

            DataRow drNew;
            DataRow[] drArray;

            drArray = dtData.Select("", "ITEMNUM");
            foreach (DataRow dr in drArray)
            {
                drNew = dtPrint.NewRow();

                for (int i = 0; i < dtData.Columns.Count; i++)
                {
                    drNew[i] = dr[i].ToString();
                }
                dtPrint.Rows.Add(drNew);
            }

            string strOrderBy = "LOCAT";
            dtPrint = CommonInfo.SortDataTable(dtPrint, strOrderBy);


            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_DATECODE_NEW", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
            btnDacodPrint.Enabled = false;
        }
        #endregion

        private void cmbLgort_MouseClick(object sender, MouseEventArgs e)
        {
            cmbWerks_SelectedIndexChanged(null,null);
        }

        private void dgvOutSource_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (DataGridViewRow row in dgvOutSource.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue == true)
                    {
                        row.Cells[0].Value = false;
                    }
                    else
                    {
                        row.Cells[0].Value = true;

                    }
                }
            }
        }
    }
}
