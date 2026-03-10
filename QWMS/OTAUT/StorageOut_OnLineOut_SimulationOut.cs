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
    public partial class StorageOut_OnLineOut_SimulationOut : Form
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
        private DataTable dtSmtSave = new DataTable();
        private DataTable dtFinSave = new DataTable();
        private DataTable dtTempStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private DataTable dtQueryQwmsStorage = new DataTable();
        private DataTable dtFinalStorage = new DataTable();
        private DataTable dtSmtStorage = new DataTable();
        private DataTable dtSmtTempData = new DataTable();
        private DataTable dtFinalTempData = new DataTable();
        private DataTable dtMblnr = new DataTable();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtData = new DataTable();
        private DataTable dtReturn = new DataTable();
        private DataTable dtDateTime = new DataTable();
        private DataTable dtPrint = new DataTable();
        private ArrayList alMblnr = new ArrayList();
        private ArrayList alMblnrs = new ArrayList();
        bool AllowToClose = true;
        private string strGRRNO = "";
        private DataTable dtSLCLgort = new DataTable();//散料仓仓别

        public struct MatnrTotalQty
        {
            public string[] Matnr;
            public int[] TotalQty;
        }
        MatnrTotalQty mtq;

        private int TotalMatnr = 0;
        private int k = 0;

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

        public StorageOut_OnLineOut_SimulationOut(UserInfo varUserData, string strProgid)
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

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    this.cmbType.SelectedIndex = 0;
                    this.cmbQwms.SelectedIndex = 0;
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
            ShowDdlLgort();
        }
        #endregion

        #region ShowGroupId
        private void ShowGroupId()
        {
            stsWarning.Text = "";

            try
            {
                SapData objSapData = new SapData(UserData, Werks, Lgort);
                if (cmbType.Text == "SMT")
                {
                    //已扣帳的id不show出來
                    if (chkAllId.Checked)
                    {
                        dtIdData = objSapData.QueryGroupIdData(strCrdat, "SMT", true);
                    }
                    else
                    {
                        dtIdData = objSapData.QueryGroupIdData(strCrdat, "SMT", true, dtDateTime);
                    }

                    StorageData objStorageData=new StorageData(UserData);

                    if (dtIdData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIdData.Rows.Count; i++)
                        {
                            //查詢SMT Group ID的倉別是否已扣過帳，已扣過帳的倉別不能再帶相同的id
                            if (objSapData.QueryGroupIdStorage(dtIdData.Rows[i]["GRPID"].ToString(), Lgort))
                            {
                                //F2 廠會單獨區分出一個車機部門出來, 其祥龍SendID 也會區分，倉庫希望在執行power II 時, 出庫倉別選TW52 或TW53時只帶出 SendID 尾碼有3F 的sendID（T. T.Chien）
                                //取消
                                //string strSuffix = dtIdData.Rows[i]["GRPID"].ToString().Substring(dtIdData.Rows[i]["GRPID"].ToString().Length - 5, 2).ToUpper();
                                //DataTable dtTemp = objStorageData.GetInfoStorageOutType(strWerks, strLgort, "SimulationOut_Floor", strSuffix);
                                //if (dtTemp.Rows.Count > 0)
                                //{
                                //    //9100散料仓不显示带A的GroupID
                                //    if (!(Comcd=="9100" && dtSLCLgort.Select("WERKS='"+strWerks+"' AND LGORT='"+strLgort+"'").Length>0 && dtIdData.Rows[i]["GRPID"].ToString().Substring(dtIdData.Rows[i]["GRPID"].ToString().Length - 7, 1)=="A"))
                                //    {
                                //        cmbGrpid.Items.Add(dtIdData.Rows[i]["GRPID"]);
                                //    }
                                //}
                                //else if (!strSuffix.Equals("2F") && !strSuffix.Equals("3F"))
                                //{
                                //    if (!(Comcd == "9100" && dtSLCLgort.Select("WERKS='" + strWerks + "' AND LGORT='" + strLgort + "'").Length > 0 && dtIdData.Rows[i]["GRPID"].ToString().Substring(dtIdData.Rows[i]["GRPID"].ToString().Length - 4, 1) == "A"))
                                //    {
                                //        cmbGrpid.Items.Add(dtIdData.Rows[i]["GRPID"]);
                                //    }
                                //}
                                //9100散料仓不显示带A的GroupID
                                if (!(Comcd == "9100" && dtSLCLgort.Select("WERKS='" + strWerks + "' AND LGORT='" + strLgort + "'").Length > 0 && dtIdData.Rows[i]["GRPID"].ToString().Substring(dtIdData.Rows[i]["GRPID"].ToString().Length - 4, 1) == "A"))
                                {
                                    cmbGrpid.Items.Add(dtIdData.Rows[i]["GRPID"]);
                                }
                            }
                        }

                        if (this.cmbGrpid.Items.Count > 0)
                        {
                            this.cmbGrpid.Enabled = true;
                        }
                        else
                        {
                            stsWarning.Text = "No id data!!";
                            return;
                        }
                    }
                    else
                    {
                        stsWarning.Text = "No id data!!";
                        return;
                    }
                }
                else if (cmbType.Text == "FINAL")
                {
                    #region 判斷是否啟動祥天FINAL的機制
                    //bool bolActive = false;
                    //Admin objAdmin = new Admin(UserData, Progid);
                    //bolActive = objAdmin.CheckPower2FinalSwith(cmbType.Text);
                    //if (bolActive == false)
                    //{
                    //    stsWarning.Text = "祥天FINAL的機制未啟動，請確認!!";
                    //    return;
                    //}
                    #endregion

                    //已扣帳的id不show出來
                    dtIdData = objSapData.QueryGroupIdData(strCrdat, "FINAL", true);
                    if (dtIdData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIdData.Rows.Count; i++)
                        {
                            cmbGrpid.Items.Add(dtIdData.Rows[i]["GRPID"]);
                        }

                        this.cmbGrpid.Enabled = true;
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

        #region cmbGrpid_SelectedIndexChanged
        private void cmbGrpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (cmbGrpid.SelectedItem.ToString() != "")
            {
                #region 帶出完整的id資訊
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
                if (cmbType.Text == "FINAL")
                {
                    dtFinData = objSapData.QuerySmtFinData(Grpid, "FINAL");
                    if (dtFinData.Rows.Count == 0)
                    {
                        stsWarning.Text = "No FINAL data!!";
                        return;
                    }

                    cmbArbpl.Enabled = true;
                    txtMatnr.Enabled = true;
                }
                #endregion

                if (cmbType.Text == "FINAL")
                {
                    if (dtFinData.Rows.Count > 0)
                    {
                        //秀出不重複的線別資訊
                        ShowArbpl();
                    }
                }

                txtMblnr.Enabled = false;
            }
        }
        #endregion

        #region ShowArbpl
        private void ShowArbpl()
        {
            ArrayList aryArbpl = new ArrayList();

            for (int i = 0; i < dtFinData.Rows.Count; i++)
            {
                if (aryArbpl.IndexOf(dtFinData.Rows[i]["ARBPL"].ToString()) == -1)
                {
                    cmbArbpl.Items.Add(dtFinData.Rows[i]["ARBPL"]);
                    aryArbpl.Add(dtFinData.Rows[i]["ARBPL"].ToString());
                }
            }
        }
        #endregion

        #region 正常出庫(rdoNormal_CheckedChanged)
        private void rdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strFunction = "NORMAL";
            Function = strFunction;
            dtpCrdat.Enabled = false;
            gbFunction.Enabled = false;
            panel5.Enabled = true;
            panel6.Enabled = true;
            txtMblnr.Enabled = false;
        }
        #endregion

        #region 加扣功能(rdoAdd_CheckedChanged)
        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            Grpid = "";//修正先對PO2-F2-14041702A-12做ID解欠，然後再對CC-F2-14041702A-12做加扣，Grpid未及時更新ID的bug  Smose Liao 20140418
            stsWarning.Text = "";
            strFunction = "ADD";
            Function = strFunction;
            dtpCrdat.Enabled = false;
            gbFunction.Enabled = false;
            panel5.Enabled = true;
            panel6.Enabled = true;
            cmbGrpid.Enabled = false;
            txtMblnr.Enabled = true;
        }
        #endregion

        #region dtpCrdat_ValueChanged
        private void dtpCrdat_ValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)
            ShowGroupId();  //秀Send/Group id
        }
        #endregion

        #region cmbArbpl_SelectedIndexChanged
        private void cmbArbpl_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMatnr.Enabled = false;
        }
        #endregion

        #region txtMatnr_TextChanged
        private void txtMatnr_TextChanged(object sender, EventArgs e)
        {
            cmbArbpl.Enabled = false;
        }
        #endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                StringBuilder sbCombineIndex = new StringBuilder();
                StringBuilder sbFinalCombineIndex = new StringBuilder();
                DataTable dtStorage = new DataTable();
                DataTable dtTmp = new DataTable();
                int CombineStorageOutQty;
                int RemainQty = 0;
                DataRow[] findRow;
                DataRow[] findRow1;
                ArrayList alCombine = new ArrayList();
                ArrayList alAllCombine = new ArrayList();
                DataRow[] combineRow;
                DataRow[] combineSumRow;
                int StorageOutQty;
                int CombineQmsRequestQty;
                int CombineQwmsTotalQty;
                int CombineRemainQty = 0;
                int CombineSumTotalQty = 0;
                int FoundMaterialLow = 0;
                int IssuedReelQty;
                string strOrderBy = "";
                string strMblnr;

                #region 正常出庫
                if (rdoNormal.Checked == true)
                {
                    #region 获取SMT数据
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
                    #endregion

                    WriteLog(Grpid, "GrpID", Lgort, "Confirm", "N");

                    #region Query該筆id的資訊

                    #region SMT Data

                    #region 先將基數(ROVAL)與卷數(RLQTY)為0的數據刪除掉
                    dtTmp = dtSmtData.Clone();
                    for (int i = 0; i < dtSmtData.Rows.Count; i++)
                    {
                        if (int.Parse(dtSmtData.Rows[i]["ROVAL"].ToString()) > 0 && int.Parse(dtSmtData.Rows[i]["RLQTY"].ToString()) > 0)
                        {
                            dtTmp.ImportRow(dtSmtData.Rows[i]);
                        }
                    }

                    dtSmtData.Clear();
                    dtSmtData = dtTmp.Copy();
                    #endregion

                    if (dtSmtData.Rows.Count > 0)
                    {
                        dtTempStorage = dtSmtData.Clone();
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
                            drRow["COSCT"] = dtSmtData.Rows[i]["COSCT"].ToString();  //Cost Center
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

                        #region 判斷該倉別是否為散料倉
                        Authority objAuthority = new Authority(UserData);
                        string strBulkLgort = objAuthority.QueryBulkStorage(cmbWerks.SelectedItem.ToString(), cmbLgort.SelectedItem.ToString());

                        #endregion

                        #region 將同料號、庫別、版本的SMT資料作加總

                        int ReelQty = 0;
                        int RovalQty = 0;
                        int TotalQty = 0;

                        dtSmtStorage = dtTempStorage.Clone();
                        dtSmtStorage.Columns.Add("QWMS_MENGE");
                        dtSmtStorage.Columns.Add("REMAIN_MENGE");
                        dtSmtStorage.Columns.Add("ISSUED_MENGE");
                        for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                        {

                            #region  每次比對的Index(sbCombineIndex)
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
                            //逾期锁定
                            dtQueryQwmsStorage = objStorageData.QueryQwmsData(dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString());

                            //由於F1有發生同料號不同Cost Center，但只有第一筆資有解欠，第二筆資料解後不到的問題，因此將比對的動作先mark掉  Smose Liao 20130626
                            //if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                            //{
                                //alAllCombine.Add(sbCombineIndex.ToString());

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

                                //總需求量 = QMS需求量 - 已發出的數量  Smose Liao 20100823
                                TotalQty = int.Parse(dtTempStorage.Rows[i]["TLQTY"].ToString()) - int.Parse(dtTempStorage.Rows[i]["MENGE"].ToString());

                                //計算已發出的卷數 = 已發出的數量 / 基數  Smose Liao 20100902
                                IssuedReelQty = 0;
                                IssuedReelQty = (int.Parse(dtTempStorage.Rows[i]["MENGE"].ToString()) / RovalQty);

                                //若為散料倉，只發總需求量的數量  Smose Liao 20110905
                                if (strBulkLgort == "BULK")
                                {
                                    //先將QWMS庫存依料號,入庫日期,儲位排序
                                    strOrderBy = chkDateCode.Checked ? "VEDAT,INDAT,LOCAT" : "MATNR, INDAT, LOCAT";
                                    dtQueryQwmsStorage = CommonInfo.SortDataTable(dtQueryQwmsStorage, strOrderBy);

                                    //計算目前QWMS中該料號的庫存數量，只要剛好大於總需求量即可
                                    CombineSumTotalQty = 0;
                                    FoundMaterialLow = 0;
                                    combineSumRow = dtQueryQwmsStorage.Select(sbCombineIndex.ToString());
                                    for (int k = 0; k < combineSumRow.Length; k++)
                                    {
                                        CombineSumTotalQty += Int32.Parse(combineSumRow[k]["MENGE"].ToString());
                                        FoundMaterialLow++;//該儲位上的同料號每出一次就加1
                                        //總發料數量大於QMS總需求量 && 總發料次數大於卷數
                                        if (CombineSumTotalQty > int.Parse(dtTempStorage.Rows[i]["TLQTY"].ToString()) && FoundMaterialLow >= int.Parse(dtTempStorage.Rows[i]["RLQTY"].ToString()))
                                        {
                                            break;//若剛好滿足上述兩條件便跳出迴圈
                                        }
                                    }
                                    
                                    //檢查記錄準備發料的表(dtSmtStorage)中，是否有相同料號、庫別、版本的資料，如果有的話，就用剩餘的數量發料  Smose Liao 20130628
                                    findRow = dtSmtStorage.Select(sbCombineIndex.ToString());
                                    if (findRow.Length > 0)
                                    {
                                        //發料的數量大於剩餘數量，只發前一次發過的同料號的剩餘數量
                                        if (StorageOutQty > int.Parse(findRow[0]["REMAIN_MENGE"].ToString()))
                                        {
                                            StorageOutQty = int.Parse(findRow[0]["REMAIN_MENGE"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        StorageOutQty = CombineSumTotalQty;
                                    }
                                }
                                //若為整料倉，依據卷數和基數發料
                                else 
                                {
                                    //檢查記錄準備發料的表(dtSmtStorage)中，是否有相同料號、庫別、版本的資料，如果有的話，就用剩餘的數量發料  Smose Liao 20130628
                                    findRow = dtSmtStorage.Select(sbCombineIndex.ToString());
                                    if (findRow.Length > 0)
                                    {
                                        //發的數量: (QMS要的卷數 - 已發出的卷數) * 基數  Smose Liao 20100902
                                        StorageOutQty = (ReelQty - IssuedReelQty) * RovalQty;

                                        //發料的數量大於剩餘數量，只發前一次發過的同料號的剩餘數量
                                        if (StorageOutQty > int.Parse(findRow[0]["REMAIN_MENGE"].ToString()))
                                        {
                                            StorageOutQty = int.Parse(findRow[0]["REMAIN_MENGE"].ToString()); 
                                        }
                                    }
                                    else
                                    {
                                        //發料的數量: (QMS要的卷數 - 已發出的卷數) * 基數  Smose Liao 20100902
                                        StorageOutQty = (ReelQty - IssuedReelQty) * RovalQty;
                                    }
                                }

                                //假如要發料的數量大於QWMS的庫存量，則只發出QWMS實際的庫存量
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
                                drRow["TLQTY"] = TotalQty;  //總需求量
                                drRow["MENGE"] = StorageOutQty;                              //實際要發的數量
                                drRow["QWMS_MENGE"] = CombineQwmsTotalQty;                   //QWMS實際庫存量
                                drRow["REMAIN_MENGE"] = CombineQwmsTotalQty - StorageOutQty; //QWMS實際庫存量 - 實際要發的數量
                                drRow["ISSUED_MENGE"] = int.Parse(dtTempStorage.Rows[i]["MENGE"].ToString());  //先前已發過的數量
                                drRow["WODAT"] = dtTempStorage.Rows[i]["WODAT"].ToString();
                                drRow["SHIFT"] = dtTempStorage.Rows[i]["SHIFT"].ToString();
                                drRow["TRDAT"] = dtTempStorage.Rows[i]["TRDAT"].ToString();
                                drRow["ROVAL"] = dtTempStorage.Rows[i]["ROVAL"].ToString();  //基數
                                drRow["UMLGO"] = dtTempStorage.Rows[i]["UMLGO"].ToString();  //收料倉

                                dtSmtStorage.Rows.Add(drRow);
                            //}
                        }

                        #endregion

                        #region QWMS實際庫存量為0的資料列不顯示
                        dtSmtTempData = dtSmtStorage.Clone();

                        for (int i = 0; i < dtSmtStorage.Rows.Count; i++)
                        {
                            if (int.Parse(dtSmtStorage.Rows[i]["MENGE"].ToString()) != 0)
                            {
                                dtSmtTempData.ImportRow(dtSmtStorage.Rows[i]);
                            }
                        }


                        //將資料存回dtSmtStorage
                        dtSmtStorage.Clear();
                        dtSmtStorage = dtSmtTempData.Copy();

                        //將資料依料號排序
                        strOrderBy = "MATNR";
                        dtSmtStorage = CommonInfo.SortDataTable(dtSmtStorage, strOrderBy);

                        #endregion

                        ShowSmtDataGrid();  //秀出SMT的資料
                    }
                    #endregion

                    #region FINAL Data
                    else if (dtFinData.Rows.Count > 0)
                    {
                        dtTempStorage = dtFinData.Clone();
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

                        #region 將同料號、庫別、版本的Final資料作加總

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
                            //逾期锁定
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
                                drRow["MENGE"] = CombineStorageOutQty;  //實際要發的總數量
                                drRow["QWMS_MENGE"] = CombineQwmsTotalQty;
                                drRow["ARBPL"] = dtTempStorage.Rows[i]["ARBPL"].ToString();
                                drRow["COSCT"] = dtTempStorage.Rows[i]["COSCT"].ToString();

                                dtStorage.Rows.Add(drRow);
                            }

                        }
                        #endregion

                        #region //初始化宣告Struct陣列
                        mtq.Matnr = new string[dtTempStorage.Rows.Count];//料號
                        mtq.TotalQty = new int[dtTempStorage.Rows.Count];//總數量
                        TotalMatnr = 0;
                        bool flagCompare = false;
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

                                CombineQwmsTotalQty = 0;
                                CombineQmsRequestQty = 0;
                                findRow1 = dtStorage.Select(sbCombineIndex.ToString());
                                CombineQwmsTotalQty = int.Parse(findRow1[0]["QWMS_MENGE"].ToString()); //QWMS的庫存總數量
                                CombineQmsRequestQty = int.Parse(findRow1[0]["MENGE"].ToString());     //QMS的總需求數量

                                //實際要發的數量
                                StorageOutQty = 0;
                                StorageOutQty = Int32.Parse(dtTempStorage.Rows[i]["MENGE"].ToString());

                                ////假如要發的數量大於QWMS的庫存量，則只發QWMS的庫存量
                                //if (StorageOutQty > CombineQwmsTotalQty)
                                //{
                                //    //StorageOutQty = CombineQwmsTotalQty;

                                //    if (alMatnrQwmsQty.IndexOf(dtTempStorage.Rows[i]["MATNR"].ToString()) < 0)
                                //    {
                                //        StorageOutQty = CombineQwmsTotalQty;
                                //        alMatnrQwmsQty.Add(dtTempStorage.Rows[i]["MATNR"].ToString());
                                //    }
                                //    else
                                //    {
                                //        //QWMS總庫存量少於總需求量，下次同一個料號不能再發料，同時QWMS總庫存量亦設為0
                                //        CombineQwmsTotalQty = 0;
                                //        StorageOutQty = 0;
                                //    }
                                //}


                                #region New寫法-解決庫存不足卻多發料的問題(Smose Liao 20101005)

                                //第一筆資料先存到Struct陣列中
                                if (i == 0)
                                {
                                    AddMatnr(i, i);

                                    //QWMS的總庫存數量大於0才允許相減存入Struct陣列中
                                    if (int.Parse(findRow1[0]["QWMS_MENGE"].ToString()) > 0)
                                    {
                                        mtq.TotalQty[i] = int.Parse(findRow1[0]["QWMS_MENGE"].ToString()) - StorageOutQty;//QWMS的總庫存數量
                                        CombineRemainQty = CombineQwmsTotalQty - StorageOutQty; //剩餘總數量 = QWMS的庫存總數量 - 實際發料量
                                    }
                                    else  //QWMS的總庫存數量為0
                                    {
                                        StorageOutQty = 0;  //實際要發的數量為0
                                        mtq.TotalQty[i] = 0;
                                    }
                                }

                                //第二筆資料才開始計算剩餘數量
                                if (i > 0)
                                {
                                    for (k = 0; k < TotalMatnr; k++)
                                    {
                                        //比對到已經存在的料號
                                        if (mtq.Matnr[k] == dtTempStorage.Rows[i]["MATNR"].ToString())
                                        {
                                            //QWMS庫存數量大於0才發料
                                            if (int.Parse(mtq.TotalQty[k].ToString()) > 0)
                                            {
                                                CombineQwmsTotalQty = int.Parse(mtq.TotalQty[k].ToString());//取得QWMS的庫存總數量
                                                CombineRemainQty = CombineQwmsTotalQty - StorageOutQty;//剩餘總數量 = QWMS的庫存總數量 - 實際發料量

                                                //庫存不足：只發QWMS的庫存量
                                                if (CombineRemainQty < 0)
                                                {
                                                    StorageOutQty = CombineQwmsTotalQty;
                                                    CombineRemainQty = 0;
                                                }
                                                mtq.TotalQty[k] = CombineRemainQty;//將剩餘總數量更新
                                            }
                                            else
                                            {
                                                StorageOutQty = 0;
                                            }

                                            flagCompare = true;
                                        }
                                        else
                                        {
                                            flagCompare = false;
                                        }
                                    }

                                    #region 沒有比對到已經存在的料號資料
                                    //將料號與總庫存數量存入Struct陣列的下一筆資料中
                                    if (flagCompare == false)
                                    {
                                        AddMatnr(i, k);
                                        CombineRemainQty = CombineQwmsTotalQty - StorageOutQty;//剩餘總數量 = QWMS的庫存總數量 - 實際發料量

                                        //庫存不足：只發QWMS的庫存量，並將剩餘總數量更新為0
                                        if (CombineRemainQty < 0)
                                        {
                                            StorageOutQty = CombineQwmsTotalQty;
                                            CombineRemainQty = 0;
                                            mtq.TotalQty[k] = CombineRemainQty;
                                        }
                                        //庫存足夠：更新剩餘的總數量並存入
                                        else
                                        {
                                            mtq.TotalQty[k] = CombineRemainQty;
                                        }
                                    }
                                    #endregion
                                }

                                #endregion


                                //記錄剩餘的數量
                                if (findRow1.Length > 0)
                                {
                                    if (CombineQwmsTotalQty == 0)
                                    {
                                        RemainQty = 0;
                                    }
                                    else
                                    {
                                        RemainQty = CombineRemainQty;
                                        //剩餘數量若為負，則顯示為0
                                        if (RemainQty < 0)
                                            RemainQty = 0;
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
                                drRow["QMS_QTY"] = dtTempStorage.Rows[i]["MENGE"].ToString(); //QMS原先建議發料的數量
                                drRow["MENGE"] = StorageOutQty;                               //實際要發的數量
                                drRow["QWMS_MENGE"] = CombineQwmsTotalQty;                    //QWMS實際庫存量
                                drRow["REMAIN_MENGE"] = RemainQty;                            //QWMS實際庫存量 - 實際要出的數量
                                drRow["WKORD"] = dtTempStorage.Rows[i]["WKORD"].ToString();
                                drRow["STATS"] = dtTempStorage.Rows[i]["STATS"].ToString();
                                drRow["MTYPE"] = dtTempStorage.Rows[i]["MTYPE"].ToString();
                                drRow["ARBPL"] = dtTempStorage.Rows[i]["ARBPL"].ToString();
                                drRow["COSCT"] = dtTempStorage.Rows[i]["COSCT"].ToString();

                                dtFinalStorage.Rows.Add(drRow);
                            }
                        }

                        #endregion

                        #region 實際發料量為0的資料列不顯示

                        dtFinalTempData = dtFinalStorage.Clone();
  
                        for (int i = 0; i < dtFinalStorage.Rows.Count; i++)
                        {
                            if (int.Parse(dtFinalStorage.Rows[i]["MENGE"].ToString()) != 0)
                            {
                                dtFinalTempData.ImportRow(dtFinalStorage.Rows[i]);
                            }
                        }

                        //將資料存回dtFinalStorage
                        dtFinalStorage.Clear();
                        dtFinalStorage = dtFinalTempData.Copy();

                        //將資料依料號、線別、工單排序
                        strOrderBy = "MATNR, ARBPL, WKORD";
                        dtFinalStorage = CommonInfo.SortDataTable(dtFinalStorage, strOrderBy);

                        #endregion

                        #region 篩選所選取的線別或輸入的料頭資料

                        dtTmp = dtFinalStorage.Clone();
                        int MatnrLenth;
                        if (cmbArbpl.SelectedIndex != -1)
                        {
                            for (int i = 0; i < dtFinalStorage.Rows.Count; i++)
                            {
                                if (dtFinalStorage.Rows[i]["ARBPL"].ToString() == cmbArbpl.SelectedItem.ToString())
                                {
                                    dtTmp.ImportRow(dtFinalStorage.Rows[i]);
                                }
                            }
                            dtFinalStorage.Clear();
                            dtFinalStorage = dtTmp.Copy();
                        }
                        else if (txtMatnr.Text.ToString() != "")
                        {
                            MatnrLenth = txtMatnr.Text.Length;
                            for (int i = 0; i < dtFinalStorage.Rows.Count; i++)
                            {
                                if (dtFinalStorage.Rows[i]["MATNR"].ToString().Substring(0, MatnrLenth) == txtMatnr.Text.ToString())
                                {
                                    dtTmp.ImportRow(dtFinalStorage.Rows[i]);
                                }
                            }
                            dtFinalStorage.Clear();
                            dtFinalStorage = dtTmp.Copy();
                        }
                        #endregion

                        ShowFinDataGrid();  //秀出Final的資料
                    }
                    #endregion

                    #endregion
                }
                #endregion

                #region 加扣功能
                if (rdoAdd.Checked == true)
                {
                    stsWarning.Text = "";
                    strMblnr = txtMblnr.Text.Trim();
                    strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)

                    string[] aryTempMblnr = txtMblnr.Text.Trim().Split(new char[] { ',' });
                    Mblnrs.Clear();
                    if (Mblnrs.Count == 0)
                    {
                        for (int i = 0; i < aryTempMblnr.Length; i++)
                        {
                            Mblnrs.Add(aryTempMblnr[i].ToString().Trim());
                        }
                    }

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
                    //單號不為空
                    if (strMblnr == "")
                    {
                        stsWarning.Text = "Document No can't be empty!!";
                        return;
                    }

                    QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    dtData = objSapData.QueryPower2AddDoc(Mblnrs, strCrdat);
                    if (dtData.Rows.Count == 0)
                    {
                        stsWarning.Text = "No Document data!!";
                        return;
                    }

                    ShowAddQtyDataGrid();
                    this.btnProduce.Enabled = true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region AddMatnr
        public void AddMatnr(int i, int k)
        {
            mtq.Matnr[k] = dtTempStorage.Rows[i]["MATNR"].ToString();
            TotalMatnr++;
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
                dgvcGrpid.Width = 140;
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
                    this.btnProduce.Enabled = true;
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

                if (dtFinalStorage.Rows.Count > 0)
                {
                    this.btnProduce.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowFinDataGrid()");
            }
        }
        #endregion

        #region ShowStorageDataGrid
        private void ShowStorageDataGrid()
        {
            dgvStorage.AutoGenerateColumns = false;
            dgvStorage.Columns.Clear();

            try
            {
                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLocat);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMatnr);

                //KDMAT
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.Width = 90;
                dgvcKdmat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcKdmat);

                //INSMK
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 90;
                dgvcInsmk.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcInsmk);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcCharg);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcAlqty);

                //BLACE
                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Blance Qty";
                dgvcBlace.Width = 80;
                dgvcBlace.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcBlace);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcZeile);

                //EBELN
                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.Width = 90;
                dgvcEbeln.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcEbeln);

                //LIFNR
                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.Width = 90;
                dgvcLifnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLifnr);

                //RMANO
                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No#";
                dgvcRmano.Width = 90;
                dgvcRmano.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcRmano);

                //OMBLNR
                DataGridViewTextBoxColumn dgvcOmblnr = new DataGridViewTextBoxColumn();
                dgvcOmblnr.DataPropertyName = "OMBLNR";
                dgvcOmblnr.HeaderText = "Store In Docu. No";
                dgvcOmblnr.Width = 100;
                dgvcOmblnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcOmblnr);

                //INDAT
                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.Width = 90;
                dgvcIndat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcIndat);

                //RMAK1
                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.Width = 90;
                dgvcRmak1.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcRmak1);

                //REFID
                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "Reference id";
                dgvcRefid.Width = 120;
                dgvcRefid.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcRefid);

                //Sequence No.
                DataGridViewTextBoxColumn dgvcSeqno = new DataGridViewTextBoxColumn();
                dgvcSeqno.DataPropertyName = "SEQNO";
                dgvcSeqno.HeaderText = "Sequence No.";
                dgvcSeqno.Width = 100;
                dgvcSeqno.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcSeqno);

                dgvStorage.DataSource = dtCombineStorage;
                lblStorage.Text = dtCombineStorage.Rows.Count.ToString() + " records";

                if (dtCombineStorage.Rows.Count > 0)
                {
                    this.btnQuery.Enabled = false;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ShowDateCodeStorageDataGrid
        private void ShowDateCodeStorageDataGrid()
        {
            this.dgvStorage.AutoGenerateColumns = false;
            this.dgvStorage.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "Blace";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 80;
                dgvcBlace.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "OMBLNR";
                dgvcTrntp.HeaderText = "Store In Doc. No";
                dgvcTrntp.Width = 100;
                dgvcTrntp.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcrmak1 = new DataGridViewTextBoxColumn();
                dgvcrmak1.DataPropertyName = "RMAK1";
                dgvcrmak1.HeaderText = "Remark";
                dgvcrmak1.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcrmak1);

                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.HeaderText = "Vendor Manufactured Date";
                dgvcVedat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcVedat);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "LotCode";
                dgvcLocod.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcLocod);

                dgvStorage.DataSource = dtCombineStorage;
                lblStorage.Text = dtCombineStorage.Rows.Count.ToString() + " records";

                if (dtCombineStorage.Rows.Count > 0)
                {
                    this.btnQuery.Enabled = false;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDateCodeStorageDataGrid()");
            }
        }
        #endregion

        #region ShowAddQtyDataGrid
        public void ShowAddQtyDataGrid()
        {
            this.dgvOutSource.AutoGenerateColumns = false;
            this.dgvOutSource.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 120;
                dgvcMblnr.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Doc Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcMdqty = new DataGridViewTextBoxColumn();
                dgvcMdqty.DataPropertyName = "MDQTY";
                dgvcMdqty.HeaderText = "Add Qty";
                dgvcMdqty.Width = 90;
                dgvcMdqty.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMdqty);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Cost Center";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width = 110;
                this.dgvOutSource.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcBwart = new DataGridViewTextBoxColumn();
                dgvcBwart.DataPropertyName = "BWART";
                dgvcBwart.HeaderText = "Mvt.";
                dgvcBwart.Width = 50;
                dgvcBwart.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcBwart);

                DataGridViewTextBoxColumn kdmatStyle = new DataGridViewTextBoxColumn();
                kdmatStyle.DataPropertyName = "KDMAT";
                kdmatStyle.HeaderText = "Customer Part No.";
                kdmatStyle.Width = 110;
                kdmatStyle.ReadOnly = true;
                dgvOutSource.Columns.Add(kdmatStyle);

                dgvOutSource.DataSource = dtData;
                lblOutSource.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowAddQtyDataGrid()");
            }
        }
        #endregion

        #region btnQuery_Click
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                #region 正常連線出庫

                if (chkDateCode.Checked != true)
                {
                    stsWarning.Text = "";
                    DataRow[] foundRow;
                    DataRow[] combineRow;
                    DataRow drRow;
                    string strOrderBy = "";
                    StringBuilder sbCombineIndex = new StringBuilder();
                    ArrayList alAllCombine = new ArrayList();
                    DataTable dtTempStorage = new DataTable();
                    DataSet dsData = new DataSet();

                    alMblnr.Clear();
                    for (int i = 0; i < dtMblnr.Rows.Count; i++)
                    {
                        alMblnr.Add(dtMblnr.Rows[i]["MBLNR"].ToString());
                    }

                    SapData objSapData = new SapData(UserData, Werks, Lgort);
                    dtOutSource = objSapData.QueryPower2LineOutDataAdd(alMblnr, "ONLINE");

                    //Order by
                    strOrderBy = "LOCAT, MATNR, INDAT";

                    StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                    dsData = objStorageData.QueryOnLineOutData(dtOutSource, "", "", false, "", false, false);

                    dtOutSource = dsData.Tables[0].Copy();
                    dtTempStorage = dsData.Tables[1].Copy();
                    dtTempStorage.Columns.Add("BLACE");


                    //將同儲位同料號的資料加總
                    int intQwqty = 0;
                    int intCombineLocalTotal = 0;
                    int intCombineLocatOut = 0;
                    dtCombineStorage = dtTempStorage.Clone();
                    for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                    {
                        #region 每次比對的Index (sbCombineIndex)
                        sbCombineIndex.Remove(0, sbCombineIndex.Length);
                        sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                        sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                        sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                        sbCombineIndex.Append(" and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "'");
                        sbCombineIndex.Append(" and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "'");
                        sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                        sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                        sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");

                        #endregion

                        if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                        {
                            intCombineLocatOut = 0;
                            alAllCombine.Add(sbCombineIndex.ToString());

                            combineRow = dtTempStorage.Select(sbCombineIndex.ToString());
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                            }
                            intCombineLocalTotal = objStorageData.QueryMatnrQty(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString(), "", "", "", "");

                            drRow = dtCombineStorage.NewRow();
                            drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                            drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                            drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                            drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                            drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                            drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                            drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                            drRow["MENGE"] = intCombineLocalTotal.ToString();
                            drRow["ALQTY"] = intCombineLocatOut.ToString();
                            drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                            drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                            drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                            drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                            drRow["OMBLNR"] = "";
                            drRow["MRGID"] = "";
                            drRow["KOSTL"] = "";
                            drRow["ARBPL"] = dtTempStorage.Rows[i]["ARBPL"].ToString();
                            drRow["TRNTP"] = "";
                            drRow["RMAK1"] = "";
                            drRow["INDAT"] = "";
                            drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                            drRow["SERNO"] = "";
                            drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                            drRow["LOCOD"] = dtTempStorage.Rows[i]["LOCOD"].ToString();
                            drRow["INSPT"] = dtTempStorage.Rows[i]["INSPT"].ToString();
                            drRow["REFID"] = dtTempStorage.Rows[i]["REFID"].ToString();
                            drRow["SEQNO"] = dtTempStorage.Rows[i]["SEQNO"].ToString();
                            //有效期以及Taskid
                            drRow["EXPDAT"] = dtTempStorage.Rows[i]["EXPDAT"].ToString();
                            drRow["TASKID"] = dtTempStorage.Rows[i]["TASKID"].ToString();
                            dtCombineStorage.Rows.Add(drRow);
                        }
                    }


                    dtStorage = dtTempStorage.Clone();
                    dtStorage.Columns.Add("MENGE1");
                    dtStorage.Columns.Add("QWQTY");
                    if (strOrderBy != "")
                    {
                        foundRow = dtTempStorage.Select("", strOrderBy);
                        for (int i = 0; i < foundRow.Length; i++)
                        {
                            //查詢該料號於該儲位上的庫存數量
                            intQwqty = objStorageData.QueryMatnrQty(foundRow[i]["LOCAT"].ToString(), foundRow[i]["MATNR"].ToString(), foundRow[i]["INSMK"].ToString(), foundRow[i]["CHARG"].ToString(), "", "", "", "");

                            drRow = dtStorage.NewRow();
                            drRow["MANDT"] = foundRow[i]["MANDT"].ToString();
                            drRow["COMCD"] = foundRow[i]["COMCD"].ToString();
                            drRow["WERKS"] = foundRow[i]["WERKS"].ToString();
                            drRow["LGORT"] = foundRow[i]["LGORT"].ToString();
                            drRow["LOCAT"] = foundRow[i]["LOCAT"].ToString();
                            drRow["MATNR"] = foundRow[i]["MATNR"].ToString();
                            drRow["INSMK"] = foundRow[i]["INSMK"].ToString();
                            drRow["CHARG"] = foundRow[i]["CHARG"].ToString();
                            drRow["MENGE"] = foundRow[i]["MENGE"].ToString();
                            drRow["ALQTY"] = foundRow[i]["ALQTY"].ToString();
                            drRow["MBLNR"] = foundRow[i]["MBLNR"].ToString();
                            drRow["ZEILE"] = foundRow[i]["ZEILE"].ToString();
                            drRow["EBELN"] = foundRow[i]["EBELN"].ToString();
                            drRow["LIFNR"] = foundRow[i]["LIFNR"].ToString();
                            drRow["RMANO"] = foundRow[i]["RMANO"].ToString();
                            drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                            drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                            drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                            drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                            drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                            drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                            drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                            drRow["MENGE1"] = foundRow[i]["MENGE"].ToString();
                            drRow["BLACE"] = foundRow[i]["BLACE"].ToString();
                            drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                            drRow["SERNO"] = foundRow[i]["SERNO"].ToString();
                            drRow["DACOD"] = foundRow[i]["DACOD"].ToString();
                            drRow["LOCOD"] = foundRow[i]["LOCOD"].ToString();
                            drRow["INSPT"] = foundRow[i]["INSPT"].ToString();
                            drRow["REFID"] = foundRow[i]["REFID"].ToString();
                            drRow["SEQNO"] = foundRow[i]["SEQNO"].ToString();
                            drRow["QWQTY"] = intQwqty;  //該料號於該儲位上的庫存數量
                            //有效期以及Taskid
                            drRow["EXPDAT"] = foundRow[i]["EXPDAT"].ToString();
                            drRow["TASKID"] = foundRow[i]["TASKID"].ToString();
                            dtStorage.Rows.Add(drRow);
                        }
                    }
                    else
                    {
                        dtStorage = dtTempStorage;
                    }

                    ShowStorageDataGrid();

                }
                #endregion

                #region By DateCode出庫

                if (chkDateCode.Checked == true)
                {
                    stsWarning.Text = "";
                    DataRow[] foundRow;
                    DataRow[] combineRow;
                    DataRow drRow;
                    string strOrderBy = "";
                    string strTempCombine = "";
                    string strAllCombine = "";
                    DataTable dtTempStorage = new DataTable();
                    DataSet dsData = new DataSet();

                    alMblnr.Clear();
                    for (int i = 0; i < dtMblnr.Rows.Count; i++)
                    {
                        alMblnr.Add(dtMblnr.Rows[i]["MBLNR"].ToString());
                    }

                    SapData objSapData = new SapData(UserData, Werks, Lgort);
                    dtOutSource = objSapData.Power2LineOutData_DateCode(alMblnr);

                    //Order by
                    strOrderBy = "LOCAT, MATNR, VEDAT";//VEDAT:廠商生產日期

                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    dsData = objStorageData.QueryOnLineOutData_DateCode(dtOutSource);

                    dtOutSource = dsData.Tables[0].Copy();//dtData1,有INSPT,whdwn
                    dtTempStorage = dsData.Tables[1].Copy();//dtData2,有SERNO,whitm
                    dtTempStorage.Columns.Add("BLACE");

                    //將同儲位同料號的資料加總
                    int intQwqty = 0;
                    int intCombineLocalTotal = 0;
                    int intCombineLocatOut = 0;
                    dtCombineStorage = dtTempStorage.Clone();
                    for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                    {
                        string strDateCode = "";
                        if (dtTempStorage.Rows[i]["DACOD"].ToString().IndexOf("__") > 0)
                        {
                            strDateCode = dtTempStorage.Rows[i]["DACOD"].ToString().Substring(0, dtTempStorage.Rows[i]["DACOD"].ToString().IndexOf("__"));
                        }
                        else if (dtTempStorage.Rows[i]["DACOD"].ToString().IndexOf("*") > 0)
                        {
                            strDateCode = dtTempStorage.Rows[i]["DACOD"].ToString().Replace("*", "&");
                        }
                        else
                        {
                            strDateCode = dtTempStorage.Rows[i]["DACOD"].ToString().Trim();
                        }

                        strTempCombine = dtTempStorage.Rows[i]["MANDT"].ToString() + dtTempStorage.Rows[i]["COMCD"].ToString() + dtTempStorage.Rows[i]["WERKS"].ToString() + dtTempStorage.Rows[i]["LGORT"].ToString() + dtTempStorage.Rows[i]["LOCAT"].ToString() + dtTempStorage.Rows[i]["MATNR"].ToString() + dtTempStorage.Rows[i]["INSMK"].ToString() + dtTempStorage.Rows[i]["CHARG"].ToString() + strDateCode + ";";
                        if (strAllCombine.IndexOf(strTempCombine) < 0)
                        {
                            intCombineLocatOut = 0;
                            strAllCombine += strTempCombine;
                            combineRow = dtTempStorage.Select("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'  and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "' and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "' and DACOD like '" + strDateCode + "%' ");
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                            }
                            //查詢庫存數量(拿掉DateCode當條件，相同儲位,相同料號,但不同DateCode與LockCode的數量也能匯總)  Smose Liao 20100331
                            //intCombineLocalTotal = objStorageData.QueryMatnrSernoQty_DateCode(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString(), strDateCode);
                            intCombineLocalTotal = objStorageData.QueryMatnrQty(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString(), "", "", "", "", "", "", "");

                            drRow = dtCombineStorage.NewRow();
                            drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                            drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                            drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                            drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                            drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                            drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                            drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                            drRow["MENGE"] = intCombineLocalTotal.ToString();
                            drRow["ALQTY"] = intCombineLocatOut.ToString();
                            drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                            drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                            drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                            drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                            drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                            drRow["OMBLNR"] = dtTempStorage.Rows[i]["OMBLNR"].ToString();//WHITM的扣帳編號
                            drRow["MRGID"] = "";
                            drRow["KOSTL"] = "";
                            drRow["ARBPL"] = "";
                            drRow["TRNTP"] = "";
                            drRow["RMAK1"] = "";
                            drRow["INDAT"] = dtTempStorage.Rows[i]["INDAT"].ToString();
                            drRow["VEDAT"] = dtTempStorage.Rows[i]["VEDAT"].ToString();
                            drRow["DACOD"] = strDateCode;
                            drRow["LOCOD"] = dtTempStorage.Rows[i]["LOCOD"].ToString();
                            drRow["INSPT"] = dtTempStorage.Rows[i]["INSPT"].ToString();
                            drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                            //有效期以及TASKID
                            drRow["EXPDAT"] = dtTempStorage.Rows[i]["EXPDAT"].ToString();
                            drRow["TASKID"] = dtTempStorage.Rows[i]["TASKID"].ToString();
                            dtCombineStorage.Rows.Add(drRow);
                        }
                    }


                    dtStorage = dtTempStorage.Clone();
                    dtStorage.Columns.Add("MENGE1");
                    dtStorage.Columns.Add("QWQTY");
                    if (strOrderBy != "")
                    {
                        foundRow = dtTempStorage.Select("", strOrderBy);
                        for (int i = 0; i < foundRow.Length; i++)
                        {
                            //查詢該料號於該儲位上的庫存數量
                            intQwqty = objStorageData.QueryMatnrQty(foundRow[i]["LOCAT"].ToString(), foundRow[i]["MATNR"].ToString(), foundRow[i]["INSMK"].ToString(), foundRow[i]["CHARG"].ToString(), "", "", "", "");

                            drRow = dtStorage.NewRow();
                            drRow["MANDT"] = foundRow[i]["MANDT"].ToString();
                            drRow["COMCD"] = foundRow[i]["COMCD"].ToString();
                            drRow["WERKS"] = foundRow[i]["WERKS"].ToString();
                            drRow["LGORT"] = foundRow[i]["LGORT"].ToString();
                            drRow["LOCAT"] = foundRow[i]["LOCAT"].ToString();
                            drRow["MATNR"] = foundRow[i]["MATNR"].ToString();
                            drRow["INSMK"] = foundRow[i]["INSMK"].ToString();
                            drRow["CHARG"] = foundRow[i]["CHARG"].ToString();
                            drRow["MENGE"] = foundRow[i]["MENGE"].ToString();
                            drRow["ALQTY"] = foundRow[i]["ALQTY"].ToString();
                            drRow["MBLNR"] = foundRow[i]["MBLNR"].ToString();
                            drRow["ZEILE"] = foundRow[i]["ZEILE"].ToString();
                            drRow["EBELN"] = foundRow[i]["EBELN"].ToString();
                            drRow["LIFNR"] = foundRow[i]["LIFNR"].ToString();
                            drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                            drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                            drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                            drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                            drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                            drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                            drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                            drRow["VEDAT"] = foundRow[i]["VEDAT"].ToString();
                            drRow["DACOD"] = foundRow[i]["DACOD"].ToString();
                            drRow["LOCOD"] = foundRow[i]["LOCOD"].ToString();
                            drRow["INSPT"] = foundRow[i]["INSPT"].ToString();
                            drRow["MENGE1"] = foundRow[i]["MENGE"].ToString();
                            drRow["BLACE"] = foundRow[i]["BLACE"].ToString();
                            drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                            drRow["QWQTY"] = intQwqty;  //該料號於該儲位上的庫存數量
                            //有效期以及TASKID
                            drRow["EXPDAT"] = foundRow[i]["EXPDAT"].ToString();
                            drRow["TASKID"] = foundRow[i]["TASKID"].ToString();
                            dtStorage.Rows.Add(drRow);
                        }
                    }
                    else
                    {
                        dtStorage = dtTempStorage;
                    }

                    ShowDateCodeStorageDataGrid();


                    #region  Query 库存跟实际库存不一致，导致少出的情况。需提示检查是否重新Qery
                    if (rdoNormal.Checked == true)
                    {
                        try
                        {
                            if (dtSmtStorage.Rows.Count > 0 && dtStorage.Rows.Count > 0)
                            {
                                #region  克隆表结构，更改栏位数据类型
                                DataTable dtSmtStorageClone = dtSmtStorage.Clone();
                                foreach (DataColumn col in dtSmtStorageClone.Columns)
                                {
                                    if (col.ColumnName == "MENGE")
                                    {
                                        col.DataType = typeof(int);
                                    }
                                }
                                foreach (DataRow row in dtSmtStorage.Rows)
                                {
                                    DataRow rowNew = dtSmtStorageClone.NewRow();
                                    rowNew["MANDT"] = row["MANDT"].ToString();
                                    rowNew["COMCD"] = row["COMCD"].ToString();
                                    rowNew["WERKS"] = row["WERKS"].ToString();
                                    rowNew["LGORT"] = row["LGORT"].ToString();
                                    rowNew["COSCT"] = row["COSCT"].ToString();
                                    rowNew["GRPID"] = row["GRPID"].ToString();
                                    rowNew["MATNR"] = row["MATNR"].ToString();
                                    rowNew["INSMK"] = row["INSMK"].ToString();
                                    rowNew["CHARG"] = row["CHARG"].ToString();
                                    rowNew["RLQTY"] = row["RLQTY"].ToString();
                                    rowNew["TLQTY"] = row["TLQTY"].ToString();
                                    rowNew["MENGE"] = int.Parse(row["MENGE"].ToString().Trim());
                                    rowNew["QWMS_MENGE"] = row["QWMS_MENGE"].ToString();
                                    rowNew["REMAIN_MENGE"] = row["REMAIN_MENGE"].ToString();
                                    rowNew["ISSUED_MENGE"] = row["ISSUED_MENGE"].ToString();
                                    rowNew["WODAT"] = row["WODAT"].ToString();
                                    rowNew["SHIFT"] = row["SHIFT"].ToString();
                                    rowNew["TRDAT"] = row["TRDAT"].ToString();
                                    rowNew["ROVAL"] = row["ROVAL"].ToString();
                                    rowNew["UMLGO"] = row["UMLGO"].ToString();

                                    dtSmtStorageClone.Rows.Add(rowNew);
                                }


                                DataTable dtStorageClone = dtStorage.Clone();
                                foreach (DataColumn col in dtStorageClone.Columns)
                                {
                                    if (col.ColumnName == "ALQTY")
                                    {
                                        col.DataType = typeof(int);
                                    }
                                }
                                foreach (DataRow row1 in dtStorage.Rows)
                                {
                                    DataRow rowNew1 = dtStorageClone.NewRow();
                                    rowNew1["MANDT"] = row1["MANDT"].ToString();
                                    rowNew1["COMCD"] = row1["COMCD"].ToString();
                                    rowNew1["WERKS"] = row1["WERKS"].ToString();
                                    rowNew1["LGORT"] = row1["LGORT"].ToString();
                                    rowNew1["LOCAT"] = row1["LOCAT"].ToString();
                                    rowNew1["MATNR"] = row1["MATNR"].ToString();
                                    rowNew1["INSMK"] = row1["INSMK"].ToString();
                                    rowNew1["CHARG"] = row1["CHARG"].ToString();
                                    rowNew1["MENGE"] = row1["MENGE"].ToString();
                                    rowNew1["ALQTY"] = int.Parse(row1["ALQTY"].ToString());
                                    rowNew1["MBLNR"] = row1["MBLNR"].ToString();
                                    rowNew1["ZEILE"] = row1["ZEILE"].ToString();
                                    rowNew1["EBELN"] = row1["EBELN"].ToString();
                                    rowNew1["LIFNR"] = row1["LIFNR"].ToString();
                                    rowNew1["OMBLNR"] = row1["OMBLNR"].ToString();
                                    rowNew1["MRGID"] = row1["MRGID"].ToString();
                                    rowNew1["KOSTL"] = row1["KOSTL"].ToString();
                                    rowNew1["ARBPL"] = row1["ARBPL"].ToString();
                                    rowNew1["TRNTP"] = row1["TRNTP"].ToString();
                                    rowNew1["RMAK1"] = row1["RMAK1"].ToString();
                                    rowNew1["INDAT"] = row1["INDAT"].ToString();
                                    rowNew1["VEDAT"] = row1["VEDAT"].ToString();
                                    rowNew1["DACOD"] = row1["DACOD"].ToString();
                                    rowNew1["LOCOD"] = row1["LOCOD"].ToString();
                                    rowNew1["INSPT"] = row1["INSPT"].ToString();
                                    rowNew1["MENGE1"] = row1["MENGE"].ToString();
                                    rowNew1["BLACE"] = row1["BLACE"].ToString();
                                    rowNew1["KDMAT"] = row1["KDMAT"].ToString();
                                    rowNew1["QWQTY"] = "";
                                    dtStorageClone.Rows.Add(rowNew1);
                                }

                                #endregion

                                string strInfo = "";
                                //Confirm要出库的料号汇总数据
                                DataTable dtGroupBy = dtSmtStorageClone.AsEnumerable().GroupBy(r => new { MATNR = r["MATNR"] }).Select(g =>
                                {
                                    var row = dtSmtStorageClone.NewRow();
                                    row["MATNR"] = g.Key.MATNR;
                                    row["MENGE"] = g.Sum(r => (int)r["MENGE"]);
                                    return row;
                                }).CopyToDataTable();

                                //Query库存出库的料号汇总数据
                                DataTable dtGroupBy2 = dtStorageClone.AsEnumerable().GroupBy(r => new { MATNR = r["MATNR"] }).Select(g =>
                                {
                                    var row = dtStorageClone.NewRow();
                                    row["MATNR"] = g.Key.MATNR;
                                    row["ALQTY"] = g.Sum(r => (int)r["ALQTY"]);
                                    return row;
                                }).CopyToDataTable();
                                for (int i = 0; i < dtGroupBy.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dtGroupBy2.Rows.Count; j++)
                                    {
                                        if (dtGroupBy.Rows[i]["MATNR"].ToString().Trim() == dtGroupBy2.Rows[j]["MATNR"].ToString().Trim())
                                        {
                                            if (dtGroupBy.Rows[i]["MENGE"].ToString().Trim() != dtGroupBy2.Rows[j]["ALQTY"].ToString().Trim())
                                            {
                                                strInfo = strInfo + dtGroupBy.Rows[i]["MATNR"].ToString().Trim() + ";";
                                            }
                                        }
                                    }
                                }
                                if (strInfo != "")
                                {
                                    DialogResult bolresulst = MessageBox.Show("66单据出库数量与Qury 出的数量不符，请确认是否重新Query,不符的料号为：" + strInfo, "请确认此操作", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (bolresulst == DialogResult.Yes)
                                    {
                                        this.btnQuery.Enabled = true;
                                        this.btnSave.Enabled = false;
                                        dtStorage.Clear();
                                    }
                                }
                            }
                            
                        }
                        
                        catch(Exception ex)
                        {
                            stsWarning.Text=ex.ToString();
                        }
                        //btnQuery.Enabled = true;//测试按钮
                    }                    
                    #endregion




                }

                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            SetbtnSaveProcess();

            DataTable dtTempData = new DataTable();
            try
            {
                stsWarning.Text = "";
                //修正先對PO2-F2-14041702A-12做ID解欠，然後再對CC-F2-14041702A-12做加扣，Grpid未及時更新ID的bug  Smose Liao 20140418
                if (Grpid == "")
                {
                    Grpid = dtData.Rows[0]["INTID"].ToString().Trim();
                }
                if (!dtStorage.Columns.Contains("GRPID"))
                {
                    dtStorage.Columns.Add("GRPID");
                }
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    dtStorage.Rows[i]["GRPID"] = Grpid;
                }

                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                if (chkDateCode.Checked != true)
                {
                    if (objStorageOut.AddOnLineOutData(dtOutSource, dtStorage, "", ""))
                    {
                        stsWarning.Text = "QWMS庫存除帳成功!!";
                        this.btnSave.Enabled = false;
                        this.btnPrint.Enabled = true;
                        this.btnRefresh.Enabled = true;
                        this.btnExit.Enabled = true;
                        AllowToClose = true;

                        #region 增加和ASRS接口
                        bool bolResult = objStorageOut.PostStorageOutDataToASRS(Werks, Lgort, dtStorage, "G-");
                        if (bolResult)
                        {
                            stsWarning.Text = "Update OK!!,数据已同步到ASRS";
                        }
                        #endregion

                        //Order by
                        string strOrderBy = "LOCAT,MATNR,INDAT";
                        dtPrint = CommonInfo.SortDataTable(dtCombineStorage, strOrderBy);
                        dtPrint.Columns.Add("GRPID");
                    }
                }
                else if (chkDateCode.Checked == true)
                {
                    strGRRNO = objStorageOut.wsAddOnLineOutData_DateCode_New(dtOutSource, dtStorage);
                    if (strGRRNO != "")
                    {
                        stsWarning.Text = "QWMS保留庫存帳成功!!";

                        this.btnSave.Enabled = false;
                        this.btnDacodPrint.Enabled = true;
                        this.btnSummary.Enabled = true;
                        this.btnRefresh.Enabled = true;
                        this.btnExit.Enabled = true;
                        AllowToClose = true;

                        #region 增加和ASRS接口
                        bool bolResult = objStorageOut.PostStorageOutDataToASRS(Werks, Lgort, dtStorage, "G-");
                        if (bolResult)
                        {
                            stsWarning.Text = "Update OK!!,数据已同步到ASRS";
                        }
                        #endregion
                        
                    }
                }
                else
                {
                    stsWarning.Text = "QWMS庫存除帳失敗，請確認!! " + objStorageOut.ERRMSG;
                    SetbtnSaveException();
                    return;
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

        #region btnPrint_Click
        private void btnPrint_Click(object sender, EventArgs e)
        {
            #region 列印資料

            StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
            if (Grpid == "")
            {
                Grpid = objStorageData.QueryAddDataId(dtData.Rows[0]["MBLNR"].ToString(), dtData.Rows[0]["MATNR"].ToString(), dtData.Rows[0]["KOSTL"].ToString());
            }

            //加入Group id並於報表中列印
            for (int i = 0; i < dtPrint.Rows.Count; i++)
            {
                dtPrint.Rows[i]["GRPID"] = Grpid;
            }

            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_GRPID", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();

            #endregion
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
            this.cmbGrpid.Items.Clear();
            this.cmbLgort.SelectedIndex = 0;
            this.cmbType.SelectedIndex = 0;
            this.lblOutSource.Text = "0 records";
            this.lblStorage.Text = "0 records";
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            this.rdoNormal.Checked = false;
            this.rdoAdd.Checked = false;
            this.gbFunction.Enabled = true;
            this.dtpCrdat.Enabled = true;
            this.txtMatnr.Text = "";
            this.txtMblnr.Text = "";
            this.cmbArbpl.Items.Clear();
            this.panel4.Enabled = true;
            this.panel5.Enabled = false;
            this.panel6.Enabled = false;
            this.chkAllId.Checked = false;
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region dgvOutSource_RowHeaderMouseClick
        private void dgvOutSource_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (rdoAdd.Checked == true)
            {
                try
                {
                    string strMblnr = "";
                    string strMatnr = "";
                    string strZeile = "";
                    string strInsmk = "";
                    string strCharg = "";
                    int intMenge = 0;

                    strMblnr = Convert.ToString(dgvOutSource.Rows[e.RowIndex].Cells[0].Value);
                    strZeile = Convert.ToString(dgvOutSource.Rows[e.RowIndex].Cells[1].Value);
                    strMatnr = Convert.ToString(dgvOutSource.Rows[e.RowIndex].Cells[2].Value);
                    strInsmk = Convert.ToString(dgvOutSource.Rows[e.RowIndex].Cells[3].Value);
                    strCharg = Convert.ToString(dgvOutSource.Rows[e.RowIndex].Cells[4].Value);
                    intMenge = int.Parse(Convert.ToString(dgvOutSource.Rows[e.RowIndex].Cells[5].Value));

                    //直接傳入加扣功能的Progid(A11)
                    Admin_DocumentAddQty_Modify objAdmin_DocumentAddDecrease_ModifyQty = new Admin_DocumentAddQty_Modify(UserData, Werks, Lgort, "A11", strMatnr, strInsmk, strCharg, strMblnr, strZeile, dtData);
                    objAdmin_DocumentAddDecrease_ModifyQty.Mblnr = strMblnr;
                    objAdmin_DocumentAddDecrease_ModifyQty.Matnr = strMatnr;
                    objAdmin_DocumentAddDecrease_ModifyQty.Zeile = strZeile;
                    objAdmin_DocumentAddDecrease_ModifyQty.Menge = intMenge;
                    objAdmin_DocumentAddDecrease_ModifyQty.ShowDialog();
                    dtData = objAdmin_DocumentAddDecrease_ModifyQty.QtyData;
                    ShowAddQtyDataGrid();
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        #endregion

        #region btnProduce_Click
        private void btnProduce_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {

               #region 正常出庫

                if (rdoNormal.Checked == true)
                {
                    #region 校验dtSmtStorage和当前WHSMT的数据，是否存在WHSMT已完成但dtSmtStorage还存在的料
                    //SMT:dtSmtStorage(ALQTY>MENGE)  TEMP
                    //SELECT * FROM TEMP LEFT JOIN SMT ON XXXX WHERE  SMT.MATNR IS NULL ?
                    //COUNT>0
                    SapData objSapData = new SapData(UserData, Werks, Lgort);
                    DataTable dtTemp = objSapData.getWhsmtCheck(Werks, Grpid);
                    foreach (DataRow drCheck in dtSmtStorage.Rows)
                    {
                        DataRow[] drTemp = dtTemp.Select("MATNR='" + drCheck["MATNR"] + "'");
                        if(drTemp.Length==0)
                        {
                            stsWarning.Text = "当前数据发生变化，请刷新或者退出重做！！！";
                            return;
                        }
                    }
                    #endregion
                    #region 產生虛擬扣帳單據

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

                        dtMblnr = objStorageOut.ProduceSapSimulationData(dtSmtSave, Type, Category, Function);
                        if (dtMblnr.Rows.Count > 0)
                        {
                            stsWarning.Text = "已產生扣帳單據...";
                            AllowToClose = false;
                            this.panel4.Enabled = false;
                            this.panel5.Enabled = false;
                            this.panel6.Enabled = false;
                            this.btnQuery.Enabled = true;
                            this.btnRefresh.Enabled = false;
                            this.btnExit.Enabled = false;
                        }
                        else
                        {
                            stsWarning.Text = "產生扣帳單據失敗，請確認!! (Error: " + objStorageOut.ERRMSG + " )";
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

                        dtMblnr = objStorageOut.ProduceSapSimulationData(dtFinSave, Type, Category, Function);
                        if (dtMblnr.Rows.Count > 0)
                        {
                            stsWarning.Text = "已產生扣帳單據...";
                            AllowToClose = false;
                            this.panel4.Enabled = false;
                            this.panel5.Enabled = false;
                            this.panel6.Enabled = false;
                            this.btnQuery.Enabled = true;
                            this.btnRefresh.Enabled = false;
                            this.btnExit.Enabled = false;
                        }
                        else
                        {
                            stsWarning.Text = "產生扣帳單據失敗，請確認!! (Error: " + objStorageOut.ERRMSG + " )";
                            return;
                        }
                    }

                    #endregion
                }

               #endregion

               #region 加扣功能

                if (rdoAdd.Checked == true)
                {
                    stsWarning.Text = "";
                    int intQwmsQty = 0;
                    DataTable dtSave = new DataTable();
                    dtSave = dtData.Clone();

                    #region 掃瞄DataTable(dtData)中的資料，若加扣數量(MdQty)有變更才儲存
                    StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (int.Parse(dtData.Rows[i]["MdQty"].ToString()) > 0)
                        {
                            dtSave.ImportRow(dtData.Rows[i]);
                        }
                    }

                    if (dtSave.Rows.Count == 0)
                    {
                        stsWarning.Text = "請先選擇欲加扣的料號並輸入數量!!";
                        return;
                    }
                    #endregion

                    #region 需加扣的料號，先查詢該料號在QWMS中的庫存數量
                    for (int i = 0; i < dtSave.Rows.Count; i++)
                    {
                        intQwmsQty = objStorageData.QueryMatnrQty(dtSave.Rows[i]["MATNR"].ToString(), dtSave.Rows[i]["INSMK"].ToString(), dtSave.Rows[i]["CHARG"].ToString());
                        if (intQwmsQty != 0)
                        {
                            dtSave.Rows[i]["QWQTY"] = intQwmsQty;
                        }
                        else
                        {
                            stsWarning.Text = "QWMS庫存數量為0，請確認!!";
                            return;
                        }

                        if (int.Parse(dtSave.Rows[i]["MdQty"].ToString()) > int.Parse(dtSave.Rows[i]["QWQTY"].ToString()))
                        {
                            stsWarning.Text = "加扣的數量大於QWMS目前的庫存數量，請確認!!";
                            return;
                        }
                    }
                    #endregion

                    StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                    dtReturn = objStorageOut.AddAdmin_DocumentAddQty(dtSave);
                    if (dtReturn.Rows.Count > 0)
                    {
                        stsWarning.Text = "已產生扣帳單據...";
                        AllowToClose = false;
                        this.panel4.Enabled = false;
                        this.panel5.Enabled = false;
                        this.panel6.Enabled = false;
                        this.btnQuery.Enabled = true;
                        this.btnRefresh.Enabled = false;
                        this.btnExit.Enabled = false;

                        for (int i = 0; i < dtReturn.Rows.Count; i++)
                        {
                            dtMblnr.Rows.Add();
                            dtMblnr.Rows[i]["MBLNR"] = dtReturn.Rows[i]["MBLNR"].ToString();
                        }
                    }
                    else
                    {
                        stsWarning.Text = "產生扣帳單據失敗，請確認!! (Error: " + objStorageOut.ERRMSG + " )";
                        return;
                    }
                }

               #endregion

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
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

        #region cmbType_SelectedIndexChanged
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region ID出庫
            if (rdoNormal.Checked == true)
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
                //廠區/倉別不能為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "廠區/倉別不能為空!!";
                    return;
                }
                //Type不能為空
                if (cmbType.SelectedItem.ToString() == "")
                {
                    stsWarning.Text = "Type不能為空!!";
                    return;
                }
                //QWMS/ASRS不能為空
                if (cmbQwms.SelectedItem.ToString() == "")
                {
                    stsWarning.Text = "QWMS/ASRS不能為空!!";
                    return;
                }

                //秀Send/Group id
                ShowGroupId();
            }
            #endregion

            #region 加扣功能
            if (rdoAdd.Checked == true)
            {
                if (cmbType.Text.ToString() == "SMT")
                {
                    txtMblnr.Focus();
                }
                else if (cmbType.Text.ToString() == "FINAL")
                {
                    stsWarning.Text = "加扣功能無法使用在Final上面，請確認!!";
                    return;
                }
            }
            #endregion
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

            #region ID出庫
            if (rdoNormal.Checked == true)
            {
                cmbGrpid.Items.Clear();
                strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)

                //Type不能為空
                if (cmbType.SelectedItem.ToString() == "")
                {
                    stsWarning.Text = "Type不能為空!!";
                    return;
                }
                //QWMS/ASRS不能為空
                if (cmbQwms.SelectedItem.ToString() == "")
                {
                    stsWarning.Text = "QWMS/ASRS不能為空!!";
                    return;
                }

                //秀Send/Group id
                ShowGroupId();
            }
            #endregion

            #region 加扣功能
            if (rdoAdd.Checked == true)
            {
                if (cmbType.Text.ToString() == "SMT")
                {
                    txtMblnr.Focus();
                }
                else if (cmbType.Text.ToString() == "FINAL")
                {
                    stsWarning.Text = "加扣功能無法使用在Final上面，請確認!!";
                    return;
                }
            }
            #endregion

            StorageData objStorageData = new StorageData(UserData);
            chkDateCode.Checked = objStorageData.CheckStorageInType(Werks, Lgort, "Diff DACOD Diff Locat") ? true : false;
        }
        #endregion

        #region cmbQwms_SelectedIndexChanged
        private void cmbQwms_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region ID出庫
            if (rdoNormal.Checked == true)
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
                //廠區/倉別不能為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "廠區/倉別不能為空!!";
                    return;
                }
                //Type不能為空
                if (cmbType.SelectedItem.ToString() == "")
                {
                    stsWarning.Text = "Type不能為空!!";
                    return;
                }
                //QWMS/ASRS不能為空
                if (cmbQwms.SelectedItem.ToString() == "")
                {
                    stsWarning.Text = "QWMS/ASRS不能為空!!";
                    return;
                }

                //秀Send/Group id
                ShowGroupId();
            }
            #endregion

            #region 加扣功能
            if (rdoAdd.Checked == true)
            {
                if (cmbType.Text.ToString() == "SMT")
                {
                    txtMblnr.Focus();
                }
                else if (cmbType.Text.ToString() == "FINAL")
                {
                    stsWarning.Text = "加扣功能無法使用在Final上面，請確認!!";
                    return;
                }
            }
            #endregion
        }
        #endregion

        #region txtMblnr_DoubleClick
        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            try
			{
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

				//廠區/倉別不能為空
				if(Werks == "" || Lgort == "")
				{
                    stsWarning.Text = "廠區/倉別不能為空!!";
					return;
				}

				alMblnrs.Clear();
				if(txtMblnr.Text.Trim() != "")
				{
					alMblnrs.Add(txtMblnr.Text.Trim());
				}
                StorageOut_SapDataSelect objStorageOut_SapDataSelect = new StorageOut_SapDataSelect(UserData, Werks, Lgort, Progid, Mblnrs, "SIMULATION_OUT", strCrdat);
				objStorageOut_SapDataSelect.ShowDialog();
                Mblnrs = objStorageOut_SapDataSelect.Mblnr;
				txtMblnr.Text = GetMblnrData();
                if (Mblnrs.Count > 0)
				{
					this.txtMblnr.Enabled = false;
				}
			}
			catch(Exception ex)
			{
				stsWarning.Text = ex.Message;	
				return;
			}
        }
        #endregion

        #region GetMblnrData
        private string GetMblnrData()
        {
            try
            {
                StringBuilder sbMblnr = new StringBuilder();
                sbMblnr.Remove(0, sbMblnr.Length);
                for (int i = 0; i < Mblnrs.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(",");
                    sbMblnr.Append(Mblnrs[i].ToString().Trim());
                }
                return sbMblnr.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetMblnrData()");
            }
        }
        #endregion

        #region 選定Group id之後，讓滑鼠無法移動(cmbGrpid_MouseMove)
        private void cmbGrpid_MouseMove(object sender, MouseEventArgs e)
        {
            cmbGrpid.MouseWheel += new MouseEventHandler(Form_MouseWheel);
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

        #region chkAllId_CheckedChanged
        private void chkAllId_CheckedChanged(object sender, EventArgs e)
        {
            cmbGrpid.Items.Clear();
            ShowGroupId();
        }
        #endregion

        #region btnSummary_Click
        private void btnSummary_Click(object sender, EventArgs e)
        {
            DataSet dsData = new DataSet();

            StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
            dsData = objStorageData.QueryOnLineOutGrrno_DateCode(dtData, strGRRNO);

            DataTable dtPrint = new DataTable();
            dtData = dsData.Tables[0].Copy();
            dtPrint = dtData.Clone();

            //將同儲位同料號的資料加總(不考慮Inspt) 
            StringBuilder sbCombinePrint = new StringBuilder();
            ArrayList alCombinePrint = new ArrayList();
            DataRow[] combineRow;
            DataRow drRow;

            int intCombineLocalTotal = 0;
            int intCombineLocatOut = 0;
            dtPrint = dtData.Clone();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                #region 每次比對的Index (sbCombinePrint)
                sbCombinePrint.Remove(0, sbCombinePrint.Length);
                sbCombinePrint.Append("MANDT='" + dtData.Rows[i]["MANDT"].ToString() + "'");
                sbCombinePrint.Append(" and COMCD='" + dtData.Rows[i]["COMCD"].ToString() + "'");
                sbCombinePrint.Append(" and WERKS='" + dtData.Rows[i]["WERKS"].ToString() + "'");
                sbCombinePrint.Append(" and LGORT='" + dtData.Rows[i]["LGORT"].ToString() + "'");
                sbCombinePrint.Append(" and LOCAT='" + dtData.Rows[i]["LOCAT"].ToString() + "'");
                sbCombinePrint.Append(" and MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "'");
                sbCombinePrint.Append(" and INSMK='" + dtData.Rows[i]["INSMK"].ToString() + "'");
                sbCombinePrint.Append(" and CHARG='" + dtData.Rows[i]["CHARG"].ToString() + "'");
                sbCombinePrint.Append(" and DACOD='" + dtData.Rows[i]["DACOD"].ToString() + "'");
                sbCombinePrint.Append(" and LOCOD='" + dtData.Rows[i]["LOCOD"].ToString() + "'");
                #endregion

                if (alCombinePrint.IndexOf(sbCombinePrint.ToString()) < 0)
                {
                    intCombineLocatOut = 0;
                    intCombineLocalTotal = 0;
                    alCombinePrint.Add(sbCombinePrint.ToString());

                    combineRow = dtData.Select(sbCombinePrint.ToString());
                    for (int j = 0; j < combineRow.Length; j++)
                    {
                        intCombineLocalTotal = Int32.Parse(combineRow[j]["BKQTY"].ToString());  //庫存總數量
                        intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());  //出庫數量
                    }

                    drRow = dtPrint.NewRow();
                    drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString();
                    drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString();
                    drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                    drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                    drRow["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString();
                    drRow["GRRNO"] = dtData.Rows[i]["GRRNO"].ToString();
                    drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                    drRow["INSMK"] = dtData.Rows[i]["INSMK"].ToString();
                    drRow["CHARG"] = dtData.Rows[i]["CHARG"].ToString();
                    drRow["BKQTY"] = intCombineLocalTotal.ToString();
                    drRow["ALQTY"] = intCombineLocatOut.ToString();
                    drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                    drRow["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString();
                    drRow["DACOD"] = dtData.Rows[i]["DACOD"].ToString();
                    drRow["LOCOD"] = dtData.Rows[i]["LOCOD"].ToString();
                    dtPrint.Rows.Add(drRow);
                }
            }

            //列印匯總的報表
            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_DateCode_Summary", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
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

            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_DATECODE_NEW", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        #endregion

        public void WriteLog(string strMblnr, string strMtype, string strFlage, string strOption, string strResult)
        {
            try
            {
                LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
                objLogData.AddQWMSLOG(strMblnr, strMtype, strFlage, strOption, strResult, Usrnm);
            }
            catch
            { }
        }
    }
}
