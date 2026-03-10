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
using Newtonsoft.Json;

namespace QWMS
{
    public partial class StorageOut_OnLineOut_SimulationOut_DateCode : Form
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
        private string strLgortType = "";//整料仓或者散料仓
        private string strCategory = "";
        private string strProgid = "";
        private DataRow drRow;
        private DataTable dtIdData = new DataTable();
        private DataTable dtSmtData = new DataTable();
        private DataTable dtFinData = new DataTable();
        private DataTable dtSmtSave = new DataTable();//Comfirm的数据汇总
        private DataTable dtFinSave = new DataTable();
        private DataTable dtTempStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private DataTable dtQueryQwmsStorage = new DataTable();
        private DataTable dtFinalStorage = new DataTable();
        private DataTable dtSmtStorage = new DataTable();//Comfirm的数据
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
        private Dictionary<string, string> dlProperty = new Dictionary<string, string>();
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

        public StorageOut_OnLineOut_SimulationOut_DateCode(UserInfo varUserData, string strProgid)
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
                    dlProperty.Add("散料仓", "BULK");
                    dlProperty.Add("整料仓", "FULL");
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
                    if(rdoNormal.Checked)
                    {
                        cmbLgort.Items.Add("散料仓");
                        cmbLgort.Items.Add("整料仓");
                    }
                    if(rdoAdd.Checked)
                    {
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
            PlantData objPlantData = new PlantData(UserData);
            cmbLgort.Enabled = true;
            if (rdoNormal.Checked)
            {
                DataTable dtLog = objPlantData.QueryLogOnXLLog(cmbWerks.SelectedItem.ToString(), "1");
                //if (dtLog.Rows.Count > 0 && (!dtLog.Rows[0]["login_name"].ToString().Equals(UserData.UserId)))
                if (dtLog.Rows.Count > 0)
                {
                    MessageBox.Show(dtLog.Rows[0]["login_name"].ToString() + " (" + dtLog.Rows[0]["ipaddress"].ToString() + ")" + "在" + dtLog.Rows[0]["CDate"].ToString() + "已打开该页面，请关闭此窗口");
                    cmbLgort.Enabled = false;
                }
                else
                {
                    objPlantData.LogOnQWMSLog(cmbWerks.SelectedItem.ToString(), "1");                    
                }
            }
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
            //PlantData objPlantData = new PlantData(UserData);
            //if(rdoNormal.Checked)
            //{
            //    DataTable dtLog = objPlantData.QueryLogOnXLLog("XL", "1");
            //    if (dtLog.Rows.Count > 0 && (!dtLog.Rows[0]["login_name"].ToString().Equals(UserData.UserId)))
            //    {
            //        MessageBox.Show(dtLog.Rows[0]["login_name"].ToString() + "(" + dtLog.Rows[0]["ipaddress"].ToString() + ")" + "在" + dtLog.Rows[0]["CDate"].ToString() + "已打开该页面");
            //        this.Close();
            //    }
            //    else
            //        objPlantData.LogOnQWMSLog("XL", "1");
            //}
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
            stsWarning.Text = string.Empty;
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
                string strMblnr;

                #region 正常出庫
                if (rdoNormal.Checked == true)
                {
                    if(string.IsNullOrEmpty(strLgortType))
                    {
                        stsWarning.Text = "Please select storage";
                        return;
                    }
                    #region 获取SMT数据
                    SapData objSapData = new SapData(UserData, Werks, Lgort);
                    if (cmbType.Text == "SMT")
                    {
                        dtSmtData = objSapData.QuerySmtDateCodeData(strWerks, Grpid);
                        if (dtSmtData.Rows.Count == 0)
                        {
                            stsWarning.Text = "No SMT data!!";
                            return;
                        }
                    }
                    #endregion

                    WriteLog(Grpid, "GrpID", strLgortType, "Confirm", "N");

                    InitdtSmtStorage();

                    #region Query該筆id的資訊，直接计算出储位需求
                    //CS20存在相同料号，所以先针对料号进行需求汇总
                    List<string> lsMatnr = (from matnr in dtSmtData.AsEnumerable() select matnr.Field<string>("MATNR")).Distinct().ToList();

                    foreach (string strMatnr in lsMatnr)
                    {
                        DataRow[] drSmtDataByMatnrs = dtSmtData.Select("MATNR='" + strMatnr + "'", "COSCT"); //获取SMT需求
                        DataTable dtStorageByMatnr = objStorageData.QueryQwmsDCData(strWerks, strLgort, strMatnr, "G", ""); //获取该料号库存
                        if (dtStorageByMatnr.Rows.Count == 0)
                            continue;
                        if (strLgortType == "FULL") //整料发料：刚好大于需求量即可
                        {
                            foreach(DataRow drSmtData in drSmtDataByMatnrs)
                            {
                                int iCurrentDemantQty = int.Parse(drSmtData["DemandQty"].ToString()); //当前该料号需求量
                                foreach (DataRow drStorageByMatnr in dtStorageByMatnr.Rows)
                                {
                                    //当前储位剩余的数量=当前储位数量-已设定出库数量
                                    int iCurrentStockRemainQty = int.Parse(drStorageByMatnr["StockQty"].ToString()) - int.Parse(drStorageByMatnr["StockOutQty"].ToString());
                                    //当前储位出库数量
                                    int iCurrentStockOutQty = 0; //需要出库的数量
                                    if (iCurrentStockRemainQty == 0)  //已被计算需求，直接跳出循环
                                        continue;
                                    //当前储位剩余数量<需求数量，需要出库的数量=剩余数量，需求数量=原需求数量-当前储位剩余数量
                                    //当前储位剩余数量>需求数量，需要出库的数量=需求数量，需求数量=0
                                    if (iCurrentStockRemainQty <= iCurrentDemantQty)
                                    {
                                        iCurrentStockOutQty = iCurrentStockRemainQty;
                                        iCurrentDemantQty = iCurrentDemantQty - iCurrentStockRemainQty;
                                    }
                                    else
                                    {
                                        iCurrentStockOutQty = iCurrentDemantQty;//当前出库数量=需求数量
                                        iCurrentDemantQty = 0;
                                    }
                                    //库存表中出库数量=已出库数量+此次出库数量
                                    drStorageByMatnr["StockOutQty"] = int.Parse(drStorageByMatnr["StockOutQty"].ToString()) + iCurrentStockOutQty;

                                    DataRow drSmtStorage = dtSmtStorage.NewRow();
                                    drSmtStorage["MANDT"] = UserData.Client;
                                    drSmtStorage["COMCD"] = UserData.CompanyCode;
                                    drSmtStorage["WERKS"] = strWerks;
                                    drSmtStorage["LGORT"] = drStorageByMatnr["LGORT"].ToString();
                                    drSmtStorage["COSCT"] = drSmtData["COSCT"].ToString();
                                    drSmtStorage["GRPID"] = drSmtData["GRPID"].ToString();
                                    drSmtStorage["MBLNR"] = drStorageByMatnr["MBLNR"].ToString();
                                    drSmtStorage["MATNR"] = drSmtData["MATNR"].ToString();
                                    drSmtStorage["INSMK"] = "G";
                                    drSmtStorage["CHARG"] = "";
                                    drSmtStorage["LOCAT"] = drStorageByMatnr["LOCAT"].ToString(); 
                                    drSmtStorage["DACOD"] = drStorageByMatnr["DACOD"].ToString();
                                    drSmtStorage["VEDAT"] = drStorageByMatnr["VEDAT"].ToString();
                                    drSmtStorage["INDAT"] = drStorageByMatnr["INDAT"].ToString();
                                    drSmtStorage["RLQTY"] = drSmtData["RLQTY"].ToString();
                                    drSmtStorage["ROVAL"] = drSmtData["ROVAL"].ToString();
                                    drSmtStorage["TOTAL"] = drSmtData["TOTAL"].ToString();
                                    drSmtStorage["ISSUED_MENGE"] = drSmtData["ISSUED_MENGE"].ToString();
                                    drSmtStorage["DemandQty"] = drSmtData["DemandQty"].ToString();
                                    drSmtStorage["StockQty"] = drStorageByMatnr["StockQty"].ToString();
                                    drSmtStorage["LocatSumStock"] = drStorageByMatnr["LocatSumStock"].ToString();
                                    drSmtStorage["RemainQty"] = int.Parse(drStorageByMatnr["LocatSumStock"].ToString()) - int.Parse(drStorageByMatnr["StockOutQty"].ToString());
                                    drSmtStorage["StockOutQty"] = iCurrentStockOutQty;
                                    drSmtStorage["WODAT"] = drSmtData["WODAT"].ToString();
                                    drSmtStorage["SHIFT"] = drSmtData["SHIFT"].ToString();
                                    drSmtStorage["UMLGO"] = drSmtData["UMLGO"].ToString();
                                    drSmtStorage["LOCOD"] = drStorageByMatnr["LOCOD"].ToString();
                                    drSmtStorage["INSPT"] = drStorageByMatnr["INSPT"].ToString();
                                    dtSmtStorage.Rows.Add(drSmtStorage);

                                    if (iCurrentDemantQty == 0)
                                        break;
                                }
                            }
                        }
                        if (strLgortType == "BULK") //卷数>=出库次数&&出库数量>=Total
                        {
                            foreach (DataRow drSmtData in drSmtDataByMatnrs)
                            {
                                int iCurrentDemantQty = int.Parse(drSmtData["TOTAL"].ToString()); //当前该料号需求量
                                foreach (DataRow drStorageByMatnr in dtStorageByMatnr.Rows)
                                {
                                    //当前储位剩余的数量=当前储位数量-已设定出库数量
                                    int iCurrentStockRemainQty = int.Parse(drStorageByMatnr["StockQty"].ToString()) - int.Parse(drStorageByMatnr["StockOutQty"].ToString());
                                    if (iCurrentStockRemainQty == 0)  //已被计算需求，直接跳出循环
                                        continue;

                                    //待出库数量=当前储位数量
                                    int iCurrentStockOutQty = int.Parse(drStorageByMatnr["StockQty"].ToString()); 
                                    //出库次数=ID
                                    int iStockNum = int.Parse(drStorageByMatnr["ID"].ToString());
                                    //需求卷数
                                    int iRlqty = int.Parse(drSmtData["RLQTY"].ToString());
                                    //必出库条件：出库次数<=卷数或者出库次数>卷数且出库数量>Total
                                    if (iStockNum <= iRlqty || (iStockNum > iRlqty && iCurrentDemantQty > 0))
                                    {
                                        drStorageByMatnr["StockOutQty"] = iCurrentStockOutQty;

                                        DataRow drSmtStorage = dtSmtStorage.NewRow();
                                        drSmtStorage["MANDT"] = UserData.Client;
                                        drSmtStorage["COMCD"] = UserData.CompanyCode;
                                        drSmtStorage["WERKS"] = strWerks;
                                        drSmtStorage["LGORT"] = drStorageByMatnr["LGORT"].ToString();
                                        drSmtStorage["COSCT"] = drSmtData["COSCT"].ToString();
                                        drSmtStorage["GRPID"] = drSmtData["GRPID"].ToString();
                                        drSmtStorage["MBLNR"] = drStorageByMatnr["MBLNR"].ToString();
                                        drSmtStorage["MATNR"] = drSmtData["MATNR"].ToString();
                                        drSmtStorage["INSMK"] = "G";
                                        drSmtStorage["CHARG"] = "";
                                        drSmtStorage["LOCAT"] = drStorageByMatnr["LOCAT"].ToString();
                                        drSmtStorage["DACOD"] = drStorageByMatnr["DACOD"].ToString();
                                        drSmtStorage["VEDAT"] = drStorageByMatnr["VEDAT"].ToString();
                                        drSmtStorage["INDAT"] = drStorageByMatnr["INDAT"].ToString();
                                        drSmtStorage["RLQTY"] = drSmtData["RLQTY"].ToString();
                                        drSmtStorage["ROVAL"] = drSmtData["ROVAL"].ToString();
                                        drSmtStorage["TOTAL"] = drSmtData["TOTAL"].ToString();
                                        drSmtStorage["ISSUED_MENGE"] = drSmtData["ISSUED_MENGE"].ToString();
                                        drSmtStorage["DemandQty"] = drSmtData["DemandQty"].ToString();
                                        drSmtStorage["StockQty"] = drStorageByMatnr["StockQty"].ToString();
                                        drSmtStorage["LocatSumStock"] = drStorageByMatnr["LocatSumStock"].ToString();
                                        drSmtStorage["RemainQty"] = int.Parse(drStorageByMatnr["LocatSumStock"].ToString()) - int.Parse(drStorageByMatnr["StockOutQty"].ToString());
                                        drSmtStorage["StockOutQty"] = drStorageByMatnr["StockQty"].ToString();
                                        drSmtStorage["WODAT"] = drSmtData["WODAT"].ToString();
                                        drSmtStorage["SHIFT"] = drSmtData["SHIFT"].ToString();
                                        drSmtStorage["UMLGO"] = drSmtData["UMLGO"].ToString();
                                        drSmtStorage["LOCOD"] = drStorageByMatnr["LOCOD"].ToString();
                                        drSmtStorage["INSPT"] = drStorageByMatnr["INSPT"].ToString();
                                        dtSmtStorage.Rows.Add(drSmtStorage);

                                        iCurrentDemantQty = iCurrentDemantQty - int.Parse(drStorageByMatnr["StockQty"].ToString());
                                    }
                                    if (iStockNum >= iRlqty && iCurrentDemantQty <= 0)
                                        break;
                                }
                            }
                        }
                    }
                    #endregion

                    //將資料依料號排序
                    strOrderBy = "MATNR";
                    dtSmtStorage = CommonInfo.SortDataTable(dtSmtStorage, strOrderBy);
                    ShowSmtDataGrid();  //秀出SMT的資料
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

        #region InitdtSmtStorage
        public void InitdtSmtStorage()
        {
            if(dtSmtStorage.Columns.Count == 0)
            {
                dtSmtStorage.Columns.Add("MANDT");
                dtSmtStorage.Columns.Add("COMCD");
                dtSmtStorage.Columns.Add("WERKS");
                dtSmtStorage.Columns.Add("LGORT");
                dtSmtStorage.Columns.Add("COSCT");
                dtSmtStorage.Columns.Add("GRPID");
                dtSmtStorage.Columns.Add("MBLNR");
                dtSmtStorage.Columns.Add("MATNR");
                dtSmtStorage.Columns.Add("INSMK");
                dtSmtStorage.Columns.Add("CHARG");
                dtSmtStorage.Columns.Add("LOCAT");
                dtSmtStorage.Columns.Add("DACOD");
                dtSmtStorage.Columns.Add("VEDAT");
                dtSmtStorage.Columns.Add("INDAT");
                dtSmtStorage.Columns.Add("RLQTY");
                dtSmtStorage.Columns.Add("ROVAL");
                dtSmtStorage.Columns.Add("TOTAL");
                dtSmtStorage.Columns.Add("ISSUED_MENGE");
                dtSmtStorage.Columns.Add("DemandQty");
                dtSmtStorage.Columns.Add("StockQty");
                dtSmtStorage.Columns.Add("LocatSumStock");
                dtSmtStorage.Columns.Add("RemainQty");
                dtSmtStorage.Columns.Add("StockOutQty");
                dtSmtStorage.Columns.Add("WODAT");
                dtSmtStorage.Columns.Add("SHIFT");
                dtSmtStorage.Columns.Add("UMLGO");
                dtSmtStorage.Columns.Add("LOCOD");
                dtSmtStorage.Columns.Add("INSPT");
            }
            else
            {
                dtSmtStorage.Rows.Clear();
            }
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

                //TLQTY 總需求量
                DataGridViewTextBoxColumn dgvcTlqty = new DataGridViewTextBoxColumn();
                dgvcTlqty.DataPropertyName = "TOTAL";
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

                //MENGE 储位实际要发的数量
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcLocat);

                //MENGE 储位实际要发的数量
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "StockOutQty";
                dgvcMenge.HeaderText = "Storage out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //QWMS MENGE 当前条数储位的库存数量
                DataGridViewTextBoxColumn dgvcQwms_Menge = new DataGridViewTextBoxColumn();
                dgvcQwms_Menge.DataPropertyName = "StockQty"; 
                dgvcQwms_Menge.HeaderText = "QWMS Qty";
                dgvcQwms_Menge.Width = 80;
                dgvcQwms_Menge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcQwms_Menge);

                //QWMS Total Qty 储位的库存总数量
                DataGridViewTextBoxColumn dgvcQwmsTotal_Menge = new DataGridViewTextBoxColumn();
                dgvcQwmsTotal_Menge.DataPropertyName = "LocatSumStock";
                dgvcQwmsTotal_Menge.HeaderText = "QWMS Total Qty";
                dgvcQwmsTotal_Menge.Width = 80;
                dgvcQwmsTotal_Menge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcQwmsTotal_Menge);

                //Stock MENGE 储位的剩余数量
                DataGridViewTextBoxColumn dgvcStock_Menge = new DataGridViewTextBoxColumn();
                dgvcStock_Menge.DataPropertyName = "RemainQty";
                dgvcStock_Menge.HeaderText = "Stock Qty";
                dgvcStock_Menge.Width = 80;
                dgvcStock_Menge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcStock_Menge);

                //Issued MENGE 已发的数量
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

        #region ShowDateCodeStorageDataGrid
        private void ShowDateCodeStorageDataGrid()
        {
            this.dgvStorage.AutoGenerateColumns = false;
            this.dgvStorage.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 90;
                dgvcLgort.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcLgort);

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
                dgvcBlace.DataPropertyName = "BLACE";
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

                dgvStorage.DataSource = dtStorage;
                lblStorage.Text = dtStorage.Rows.Count.ToString() + " records";

                if (dtStorage.Rows.Count > 0)
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
                stsWarning.Text = "";
                string strOrderBy = "";
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();
                DataTable dtTempStorage = new DataTable();
                DataSet dsData = new DataSet();
                if (Grpid == string.Empty && rdoNormal.Checked)
                    Grpid = cmbGrpid.Items[cmbGrpid.SelectedIndex].ToString();
                WriteLog(Grpid, "GrpID", rdoNormal.Checked ? strLgortType : Lgort, "Query", "N");

                SapData objSapData = new SapData(UserData, Werks, Lgort);
                StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                #region 出库重新计算库存
                if (rdoNormal.Checked)
                {
                    dtOutSource = objSapData.QueryPower2LineOutData(Grpid, strLgort);
                    dsData = objStorageData.QuerySimulationDateCodeData_Normal(dtSmtStorage, dtOutSource);
                }
                #endregion
                
                #region 加扣
                if (rdoAdd.Checked)
                {
                    alMblnr.Clear();
                    for (int i = 0; i < dtMblnr.Rows.Count; i++)
                    {
                        alMblnr.Add(dtMblnr.Rows[i]["MBLNR"].ToString());
                    }

                    dtOutSource = objSapData.QueryPower2LineOutDataAdd(alMblnr, "ONLINE");
                    
                    dsData = objStorageData.QuerySimulationDateCodeData(dtOutSource, Grpid, true);

                }
                #endregion


                dtOutSource = dsData.Tables[0].Copy(); //dtDwnSource
                dtTempStorage = dsData.Tables[1].Copy(); //dtOutWhitm
                //dtTempStorage.Columns.Add("BLACE");

                //dtCombineStorage为报表数据
                string strTempStorageJson = JsonConvert.SerializeObject(dtTempStorage);
                string strQueryType = "DACOD";
                DataSet dataSet = objStorageData.QuerySimulationQueryData(strTempStorageJson, strQueryType, strGrpid);
                if (dataSet != null)
                {
                    dtCombineStorage = dataSet.Tables[0].Copy();
                    dtStorage = dataSet.Tables[1].Copy();
                }

                ShowDateCodeStorageDataGrid();
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
                if (Grpid == "")
                {
                    Grpid = dtData.Rows[0]["INTID"].ToString().Trim();
                }
                if (rdoAdd.Checked)
                    WriteLog(Grpid, "GrpID", Lgort, "ADD", "N");
                if (rdoNormal.Checked)
                    WriteLog(Grpid, "GrpID", strLgortType, "Save", "N");

                #region 获取GRRNO单号
                StorageOut objStorageOut = new StorageOut(UserData, Werks, rdoNormal.Checked ? strLgortType : Lgort, Progid);
                List<string> lsLgort = dtStorage.AsEnumerable().Select(x=>x.Field<string>("LGORT")).Distinct().ToList();
                DataTable dtLgort = new DataTable();
                dtLgort.Columns.Add("WERKS");
                dtLgort.Columns.Add("LGORT");
                dtLgort.Columns.Add("Grrno");
                foreach (string sLgort in lsLgort)
                {
                    DataRow drResult = dtLgort.NewRow();
                    drResult["WERKS"] = strWerks;
                    drResult["LGORT"] = sLgort;
                    drResult["Grrno"] = objStorageOut.GetSimulationGrrno();
                    dtLgort.Rows.Add(drResult.ItemArray);
                    if (rdoAdd.Checked)
                    {
                        strGRRNO = drResult["Grrno"].ToString();
                    }
                    WriteLog(Grpid, "GrpID", strLgortType, "Save", drResult["Grrno"].ToString());
                }
                #endregion

                #region 出库
                string strOutSource = JsonConvert.SerializeObject(dtOutSource);
                string strStorage = JsonConvert.SerializeObject(dtStorage);
                string strLgortJson = JsonConvert.SerializeObject(dtLgort);
                //SP_SimulationOut_Save--N:开始 F:库存不足 D:单据已被操作 N:操作失败 Y:操作成功
                string strResult = objStorageOut.AddSinmulationOutSaveData(strOutSource, strStorage, strLgortJson);
                if(strResult.Equals("Y"))
                {
                    string strOrderBy = "LOCAT,MATNR,INDAT";
                    dtPrint = CommonInfo.SortDataTable(dtCombineStorage, strOrderBy);
                    dtPrint.Columns.Add("GRPID");
                }
                switch (strResult)
                {
                    case "Y":
                        stsWarning.Text = "Save Success!!!";
                        btnSave.Enabled = false;
                        //btnDacodPrint.Enabled = true;
                        //btnSummary.Enabled = true;
                        btnRefresh.Enabled = true;
                        btnExit.Enabled = true;
                        AllowToClose = true;
                        if (rdoAdd.Checked)
                        {
                            btnSummary.Enabled = true;
                        }
                        break;
                    case "N":
                        stsWarning.Text = "Save Fail!!!";
                        break;
                    case "F":
                        stsWarning.Text = "The stock is not enough, please Query Again!!";
                        btnQuery.Enabled = true;
                        dtStorage.Clear();
                        btnSave.Enabled = false;
                        AllowToClose = true;
                        break;
                    case "D":
                        stsWarning.Text = "The document has been processed!!!";
                        AllowToClose = true;
                        break;
                    default:
                        stsWarning.Text = "Something Wrong,Please contact MIS!!!";
                        AllowToClose = true;
                        break;
                }
                #endregion

                #region 增加和ASRS接口
                //bool bolResult = objStorageOut.PostStorageOutDataToASRS(Werks, Lgort, dtStorage, "G-");
                //if (bolResult)
                //{
                //    stsWarning.Text = "Update OK!!,数据已同步到ASRS";
                //}
                #endregion
               
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
            WriteLog(Grpid, "GrpID", strLgortType, "Refresh", "N");
            this.stsWarning.Text = "";
            this.dtSmtData.Clear();
            this.dtFinData.Clear();
            this.dtIdData.Clear();
            this.dtReturn.Clear();
            this.dtMblnr.Clear();
            this.dtSmtStorage.Clear();
            this.dtFinalStorage.Clear();
            this.cmbGrpid.Items.Clear();
            this.cmbLgort.SelectedIndex = -1;
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
            PlantData objPlantData = new PlantData(UserData);
            objPlantData.UpdateLogOnXLLog(UserData.UserId, "0");
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
                    SapData objSapData = new SapData(UserData, Werks, strLgortType);
                    DataTable dtTemp = objSapData.getWhsmtCheck(Werks, Grpid);
                    foreach (DataRow drCheck in dtSmtStorage.Rows)
                    {
                        DataRow[] drTemp = dtTemp.Select("MATNR='" + drCheck["MATNR"] + "'");
                        if (drTemp.Length == 0)
                        {
                            stsWarning.Text = "The data has changed,please refresh and confirm again!(当前数据发生变化，请刷新或者退出重做！！！)";
                            return;
                        }
                    }
                    #endregion

                    WriteLog(Grpid, "GrpID", strLgortType, "Produce", "N");

                    #region 產生虛擬扣帳單據

                    StorageOut objStorageOut = new StorageOut(UserData, Werks, strLgortType, Progid);
                    if (cmbType.Text == "SMT")
                    {
                        //将库存数据以厂区、仓别、料号等信息进行汇总
                        dtSmtSave = objStorageOut.CombineSimulationComfirmData(JsonConvert.SerializeObject(dtSmtStorage));

                        string strSmtSaveJson = JsonConvert.SerializeObject(dtSmtSave);

                        #region 获取虚拟单号序列号
                        DataTable dtSernoByLgort = new DataTable();
                        dtSernoByLgort.Columns.Add("WERKS");
                        dtSernoByLgort.Columns.Add("LGORT");
                        dtSernoByLgort.Columns.Add("BeginNum");
                        dtSernoByLgort.Columns.Add("EndNum");

                        var query = from row in dtSmtSave.AsEnumerable()
                                    group row by row.Field<string>("LGORT") into m
                                    select new
                                    {
                                        LGORT = m.Key,
                                        ItemCount = m.Count()
                                    };
                        foreach (var item in query)
                        {
                            int iStep = item.ItemCount / 20 + 1; //需要的虚拟数个数>=实际数
                            DataTable dtSerno = objStorageOut.GetSimulationNumBeginEnd("FlyPlan", Type, iStep);  //取得虛擬單據新的流水號,一次性更新到所需的起始序列和终止序列

                            DataRow drResult = dtSernoByLgort.NewRow();
                            drResult["WERKS"] = strWerks;
                            drResult["LGORT"] = item.LGORT;
                            drResult["BeginNum"] = dtSerno.Rows[0][0].ToString();
                            drResult["EndNum"] = dtSerno.Rows[0][1].ToString();
                            dtSernoByLgort.Rows.Add(drResult.ItemArray);

                            WriteLog(Grpid, "GrpID", strLgortType, "Produce", dtSerno.Rows[0][0].ToString() + ":" + dtSerno.Rows[0][1].ToString());
                        }
                        #endregion

                        string strSmtSave = JsonConvert.SerializeObject(dtSmtSave);
                        string strSernoByLgort = JsonConvert.SerializeObject(dtSernoByLgort);
                        string strResult = objStorageOut.ProduceSimulationData(strSmtSave, Type, Category, Function, strSernoByLgort);
                        if (strResult == "Y")
                        {
                            stsWarning.Text = "MBLNR has been produced(已產生扣帳單據...)";
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
                            stsWarning.Text = "MBLNR has been not produced.Please confirm the msg(產生扣帳單據失敗，請確認!!) (Error: " + objStorageOut.ERRMSG + " )";
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
            PlantData objPlantData = new PlantData(UserData);
            objPlantData.UpdateLogOnXLLog(UserData.UserId, "0");
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
                if (rdoNormal.Checked)
                {
                    PlantData objPlantData = new PlantData(UserData);
                    string strProperty = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    strLgortType = dlProperty[strProperty];
                    DataTable dtLgort = objPlantData.QueryLgortByProperty(strWerks, strLgortType, "");
                    if (dtLgort.Rows.Count <= 0)
                    {
                        stsWarning.Text = "未维护祥龙解欠多仓别定义，请确认！";
                        return;
                    }
                    foreach(DataRow drLgort in dtLgort.Rows)
                    {
                        strLgort += "'" + drLgort["CTRLC1"].ToString() + "',";
                    }
                    strLgort = strLgort.Substring(0, strLgort.Length - 1);
                }
                if(rdoAdd.Checked)
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
            dtPrint = CommonInfo.SortDataTable(dtPrint, "LOCAT");
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
