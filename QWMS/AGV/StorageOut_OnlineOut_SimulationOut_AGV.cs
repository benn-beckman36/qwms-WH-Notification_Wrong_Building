using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS.OTAUT
{
    public partial class StorageOut_OnlineOut_SimulationOut_AGV : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();

        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strGrpid = string.Empty;
        private string strCrdat = string.Empty;
        private string strFunction = string.Empty;
        private string strType = string.Empty;
        private string strProgid = string.Empty;
        private string strShelfType = string.Empty;
        private DataTable dtIdData = new DataTable();
        private DataTable dtSmtData = new DataTable();
        private DataTable dtSmtSave = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private DataTable dtSmtStorage = new DataTable();
        private DataTable dtMblnr = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtData = new DataTable();
        private DataTable dtDateTime = new DataTable();
        private DataTable dtPrint = new DataTable();
        private ArrayList alMblnrs = new ArrayList();
        bool AllowToClose = true;

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

        public StorageOut_OnlineOut_SimulationOut_AGV(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                StorageOut_AGV objStorageOut_AGV = new StorageOut_AGV(UserData, strProgid);

                //檢查權限
                if (!objStorageOut_AGV.CheckAuthority())
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
                        this.cmbWerks.SelectedIndex = -1;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = -1;
                    }
                    dtMblnr.Columns.Add("MBLNR");
                    GetDateTime();  //取得目前系統的日期與時間
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Show方法：StatusData,DdlWerks,DdlLgort,GetDateTime,GroupId,WorkStation
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        
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

        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                stsWarning.Text = string.Empty;
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
                    strLgort = string.Empty;
                }
                else
                {
                    cmbLgort.Items.Clear();
                    cmbLgort.Text = string.Empty;
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        //if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != string.Empty)
                        //{
                        //    cmbLgort.SelectedIndex = i;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

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

        private void ShowGroupId()
        {
            stsWarning.Text = "";

            try
            {
                cmbGrpID.Items.Clear();
                SapData objSapData = new SapData(UserData, Werks, Lgort);
                //已扣帳的id不show出來
                if (chkAllId.Checked)
                {
                    dtIdData = objSapData.QueryGroupIdData(strCrdat, "SMT", true);
                }
                else
                {
                    dtIdData = objSapData.QueryGroupIdData(strCrdat, "SMT", true, dtDateTime);
                }

                StorageData objStorageData = new StorageData(UserData);

                if (dtIdData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtIdData.Rows.Count; i++)
                    {
                        //查詢SMT Group ID的倉別是否已扣過帳，已扣過帳的倉別不能再帶相同的id
                        if (objSapData.QueryGroupIdStorage(dtIdData.Rows[i]["GRPID"].ToString(), Lgort))
                        {
                            cmbGrpID.Items.Add(dtIdData.Rows[i]["GRPID"]);
                        }
                    }

                    if (cmbGrpID.Items.Count > 0)
                    {
                        cmbGrpID.Enabled = true;
                    }
                    else
                    {
                        cmbGrpID.Items.Clear();
                        stsWarning.Text = "No id data!!";
                        return;
                    }
                }
                else
                {
                    cmbGrpID.Items.Clear();
                    stsWarning.Text = "No id data!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #endregion

        #region WMS下拉框方法:厂区,仓别,GrpID,WorkStation
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            ShowDdlLgort();
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            //廠區/倉別不能為空
            if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            else
            {
                stsWarning.Text = "Please select plant or storage";
                return;
            }

            strCrdat = dtpCrdat.Value.ToString("yyyy-MM-dd");  //取得user目前所選的日期(預設為當日)

            StorageData objStorageData = new StorageData(UserData);
            chkDateCode.Checked = objStorageData.CheckStorageInType(Werks, Lgort, "Diff DACOD Diff Locat") ? true : false;

            ShowGroupId();
        }
        private void cmbGrpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (cmbGrpID.SelectedItem.ToString() != string.Empty)
            {
                #region 帶出完整的id資訊
                Grpid = cmbGrpID.Items[cmbGrpID.SelectedIndex].ToString();

                //Send/Group id不為空
                if (cmbGrpID.Text.ToString() == string.Empty)
                {
                    stsWarning.Text = "Send/Group id can't be empty!!";
                    return;
                }
                #endregion
            }
        }
        #endregion

        #region WMS Function
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                StorageOut_AGV objStorageOut_AGV = new StorageOut_AGV(UserData, Progid);
                DataTable dtStorage = new DataTable();
                string strOrderBy = string.Empty;

                if (string.IsNullOrEmpty(strLgort))
                {
                    stsWarning.Text = "Please select storage";
                    return;
                }
                #region 获取SMT数据
                SapData objSapData = new SapData(UserData, Werks, Lgort);
                dtSmtData = objSapData.QuerySmtDateCodeData(strWerks, Grpid);
                if (dtSmtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No SMT data!!";
                    return;
                }
                #endregion

                WriteLog(Grpid, "GrpID", Werks + strLgort, "Confirm", "N");

                InitdtSmtStorage();

                #region Query該筆id的資訊，直接计算出储位需求
                strShelfType = Grpid.Substring(Grpid.Length - 4, 1).Equals("A") ? "ALL" : "BULK";

                #region 原调用接口获取整料料架或者散料料架，先修改为根据仓别获取整料或者散料
                string strShelfList = string.Empty;
                //List<string> lsShelfInfo = getShelfInfo(strWerks, strLgort, strShelfType);
                //if (lsShelfInfo.Count == 0)
                //{
                //    MessageBox.Show("There is no shelf information");
                //    return;
                //}
                //else
                //    strShelfList = listToString(lsShelfInfo, "','");
                Authority objAuthority = new Authority(UserData);
                string strLgortType = objAuthority.QueryBulkStorage(cmbWerks.SelectedItem.ToString(), cmbLgort.SelectedItem.ToString()).Equals("BULK") ? "BULK" : "ALL";

                if (!strShelfType.Equals(strLgortType))
                {
                    MessageBox.Show("选择的ID与解欠仓别不符！");
                    return;
                }
                #endregion

                //CS20存在相同料号，所以先针对料号进行需求汇总
                List<string> lsMatnr = (from matnr in dtSmtData.AsEnumerable() select matnr.Field<string>("MATNR")).Distinct().ToList();

                foreach (string strMatnr in lsMatnr)
                {
                    DataRow[] drSmtDataByMatnrs = dtSmtData.Select("MATNR='" + strMatnr + "'", "COSCT"); //获取SMT需求
                    //测试时使用料架表，实际料架需通过接口获取
                    //DataTable dtStorageByMatnr = objStorageOut_AGV.QueryQwmsDCData_AGV(strWerks, strLgort, strMatnr, "G", "", strShelfType); //根据料架是散料还是整料获取该料号库存
                    DataTable dtStorageByMatnr = objStorageOut_AGV.QueryQwmsDCData_AGV(strWerks, strLgort, strMatnr, "G", "", strShelfList, strShelfType); //根据料架是散料还是整料获取该料号库存
                    if (dtStorageByMatnr.Rows.Count == 0)
                        continue;
                    if (strShelfType != "BULK") //整料发料：原为刚好大于需求量即可，现需保证储位全出
                    {
                        foreach (DataRow drSmtData in drSmtDataByMatnrs)
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
                                //当前储位剩余数量>需求数量，需要出库的数量=需求数量，需求数量=0改为需求数量=原需求数量-当前储位剩余数量，确保整个储位出完
                                iCurrentStockOutQty = iCurrentStockRemainQty;
                                iCurrentDemantQty = iCurrentDemantQty - iCurrentStockRemainQty;

                                //库存表中出库数量=已出库数量+此次出库数量
                                drStorageByMatnr["StockOutQty"] = int.Parse(drStorageByMatnr["StockOutQty"].ToString()) + iCurrentStockOutQty;

                                DataRow drSmtStorage = dtSmtStorage.NewRow();
                                drSmtStorage["MANDT"] = UserData.Client;
                                drSmtStorage["COMCD"] = UserData.CompanyCode;
                                drSmtStorage["WERKS"] = strWerks;
                                drSmtStorage["LGORT"] = drStorageByMatnr["LGORT"].ToString();
                                drSmtStorage["COSCT"] = drSmtData["COSCT"].ToString();
                                drSmtStorage["GRPID"] = drSmtData["GRPID"].ToString();
                                drSmtStorage["MBLNR"] = "";//因计算需求时，将同储位的数据合并，所以无单据
                                drSmtStorage["MATNR"] = drSmtData["MATNR"].ToString();
                                drSmtStorage["INSMK"] = "G";
                                drSmtStorage["CHARG"] = "";
                                drSmtStorage["LOCAT"] = drStorageByMatnr["LOCAT"].ToString();
                                drSmtStorage["MARNO"] = drStorageByMatnr["MARNO"].ToString();
                                drSmtStorage["LIFNR"] = drStorageByMatnr["LIFNR"].ToString();
                                drSmtStorage["DACOD"] = drStorageByMatnr["DACOD"].ToString();
                                drSmtStorage["LOCOD"] = drStorageByMatnr["LOCOD"].ToString();
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
                                drSmtStorage["SERNO"] = drStorageByMatnr["SERNO"].ToString();
                                dtSmtStorage.Rows.Add(drSmtStorage);

                                if (iCurrentDemantQty <= 0)
                                    break;
                            }
                        }
                    }
                    if (strShelfType == "BULK") //卷数>=出库次数&&出库数量>=Total
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
                                    drSmtStorage["MBLNR"] = "";//因计算需求时，将同储位的数据合并，所以无单据
                                    drSmtStorage["MATNR"] = drSmtData["MATNR"].ToString();
                                    drSmtStorage["INSMK"] = "G";
                                    drSmtStorage["CHARG"] = "";
                                    drSmtStorage["LOCAT"] = drStorageByMatnr["LOCAT"].ToString();
                                    drSmtStorage["DACOD"] = drStorageByMatnr["DACOD"].ToString();
                                    drSmtStorage["VEDAT"] = drStorageByMatnr["VEDAT"].ToString();
                                    drSmtStorage["LOCOD"] = drStorageByMatnr["LOCOD"].ToString();
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
                                    drSmtStorage["SERNO"] = drStorageByMatnr["SERNO"].ToString();
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
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnProduce_Click(object sender, EventArgs e)
        {
            #region 校验dtSmtStorage和当前WHSMT的数据，是否存在WHSMT已完成但dtSmtStorage还存在的料
            SapData objSapData = new SapData(UserData, Werks, strLgort);
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

            WriteLog(Grpid, "GrpID", Werks + strLgort, "Produce", "N");

            #region 產生虛擬扣帳單據

            StorageOut objStorageOut = new StorageOut(UserData, Werks, strLgort, Progid);
            StorageOut_AGV objStorageOut_AGV = new StorageOut_AGV(UserData, Werks, strLgort, Progid);
            //将库存数据以厂区、仓别、料号等信息进行汇总
            dtSmtSave = objStorageOut_AGV.CombineSimulationComfirmData(JsonConvert.SerializeObject(dtSmtStorage));

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

                WriteLog(Grpid, "GrpID", Werks + strLgort, "Produce", dtSerno.Rows[0][0].ToString() + ":" + dtSerno.Rows[0][1].ToString());
            }
            #endregion

            string strSmtSave = JsonConvert.SerializeObject(dtSmtSave);
            string strSernoByLgort = JsonConvert.SerializeObject(dtSernoByLgort);
            string strResult = objStorageOut.ProduceSimulationData(strSmtSave, Type, "QWMS", Function, strSernoByLgort);
            if (strResult == "Y")
            {
                stsWarning.Text = "MBLNR has been produced(已產生扣帳單據...)";
                AllowToClose = false;
                this.btnQuery.Enabled = true;
                this.btnRefresh.Enabled = false;
            }
            else
            {
                stsWarning.Text = "MBLNR has been not produced.Please confirm the msg(產生扣帳單據失敗，請確認!!) (Error: " + objStorageOut.ERRMSG + " )";
                return;
            }

            #endregion
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();
                DataTable dtTempStorage = new DataTable();
                DataSet dsData = new DataSet();
                WriteLog(Grpid, "GrpID", Werks + Lgort, "Query", "N");

                StorageOut_AGV objStorageOut_AGV = new StorageOut_AGV(UserData, Werks, Lgort, Progid);
                //dsData = objStorageOut_AGV.QuerySimulationDateCodeData_AGV(dtOutSource, Grpid, true);

                //dtOutSource = dsData.Tables[0].Copy(); //dtDwnSource
                //dtTempStorage = dsData.Tables[1].Copy(); //dtOutWhitm
                ////dtTempStorage.Columns.Add("BLACE");

                ////dtCombineStorage 为报表数据
                //string strTempStorageJson = JsonConvert.SerializeObject(dtTempStorage);
                dtStorage = objStorageOut_AGV.QuerySimulationQueryData_AGV(JsonConvert.SerializeObject(dtSmtStorage), strGrpid, strWerks, strLgort, strShelfType);

                ShowDateCodeStorageDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;

            DataTable dtTempData = new DataTable();
            try
            {
                stsWarning.Text = "";
                if (Grpid == "")
                {
                    Grpid = dtData.Rows[0]["INTID"].ToString().Trim();
                }
                WriteLog(Grpid, "GrpID", Werks + Lgort, "Save", "N");

                #region 生成TaskCode
                StorageOut objStorageOut = new StorageOut(UserData, Werks, strLgort, Progid);
                StorageOut_AGV objStorageOut_AGV = new StorageOut_AGV(UserData, Werks, Lgort, Progid);
                var query = from row in dtStorage.AsEnumerable()
                            group row by row.Field<string>("LGORT") into m
                            select new
                            {
                                LGORT = m.Key,
                                ItemCount = m.Count()
                            };
                DataTable dtLgort = new DataTable();
                dtLgort.Columns.Add("WERKS");
                dtLgort.Columns.Add("LGORT");
                dtLgort.Columns.Add("TaskNoBegin");
                dtLgort.Columns.Add("TaskNoEnd");
                foreach (var item in query)
                {
                    int iStep = item.ItemCount / 100 + 1; //需要的虚拟数个数>=实际数
                    DataTable dtSerno = objStorageOut.GetSimulationNumBeginEnd("AGV", "TaskNo", iStep);  //取得虛擬單據新的流水號,一次性更新到所需的起始序列和终止序列

                    DataRow drResult = dtLgort.NewRow();
                    drResult["WERKS"] = strWerks;
                    drResult["LGORT"] = item.LGORT;
                    drResult["TaskNoBegin"] = dtSerno.Rows[0][0].ToString().PadLeft(6, '0');// strWerks + strLgort + DateTime.Now.ToString("yyyyMMdd") + objStorageOut_AGV.GetSimulationTaskNo("AGV", "TASKNO");
                    drResult["TaskNoEnd"] = dtSerno.Rows[0][1].ToString().PadLeft(6, '0');
                    dtLgort.Rows.Add(drResult.ItemArray);

                    WriteLog(Grpid, "GrpID", Werks + strLgort, "Save", drResult["TaskNoBegin"].ToString() + ":" + drResult["TaskNoEnd"].ToString());
                }
                #endregion

                #region 出库
                string strStorage = JsonConvert.SerializeObject(dtStorage);
                string strLgortJson = JsonConvert.SerializeObject(dtLgort);
                //SP_SimulationOut_Save--N:开始 F:库存不足 D:单据已被操作 N:操作失败 Y:操作成功
                string strResult = objStorageOut_AGV.AddSinmulationOutSaveData_AGV(strStorage, strLgortJson);
                //if (strResult.Equals("Y"))
                //{
                //    string strOrderBy = "LOCAT,MATNR,INDAT";
                //    dtPrint = CommonInfo.SortDataTable(dtCombineStorage, strOrderBy);
                //    dtPrint.Columns.Add("GRPID");
                //}
                switch (strResult)
                {
                    case "Y":
                        stsWarning.Text = "保存成功!!!";
                        btnSave.Enabled = false;
                        //btnDacodPrint.Enabled = true;
                        //btnSummary.Enabled = true;
                        btnRefresh.Enabled = true;
                        AllowToClose = true;
                        break;
                    case "N":
                        stsWarning.Text = "保存失败!!!";
                        break;
                    case "F":
                        stsWarning.Text = "库存不足！请重新查询!!";
                        btnQuery.Enabled = true;
                        dtStorage.Clear();
                        btnSave.Enabled = false;
                        AllowToClose = true;
                        break;
                    case "D":
                        stsWarning.Text = "单据已处理!!!";
                        AllowToClose = true;
                        break;
                    default:
                        stsWarning.Text = "问题错误,请联系MIS!!!";
                        AllowToClose = true;
                        break;
                }
                #endregion

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                btnSave.Enabled = true;
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            strWerks = string.Empty;
            strLgort = string.Empty;
            strGrpid = string.Empty;
            strShelfType = string.Empty;
            dtSmtData.Clear();
            dtIdData.Clear();
            dtMblnr.Clear();
            dtSmtStorage.Clear();
            cmbGrpID.Items.Clear();
            cmbGrpID.Text = string.Empty;
            cmbWerks.SelectedIndex = -1;
            cmbLgort.SelectedIndex = -1;
            lblOutSource.Text = "0 records";
            lblStorage.Text = "0 records";
            dgvOutSource.DataSource = null;
            dgvStorage.DataSource = null;
            btnQuery.Enabled = false;
            btnSave.Enabled = false;
            //btnPrint.Enabled = false;
            dtpCrdat.Enabled = true;
            chkAllId.Checked = false;
        }


        #endregion

        #region ShowDataGrid

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
                dgvcMenge.DataPropertyName = "QWMS_MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "StockOutQty";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "REMAIN_MENGE";
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
        #endregion

        #region InitdtSmtStorage
        public void InitdtSmtStorage()
        {
            if (dtSmtStorage.Columns.Count == 0)
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
                dtSmtStorage.Columns.Add("MARNO");
                dtSmtStorage.Columns.Add("LOCAT");
                dtSmtStorage.Columns.Add("LIFNR");
                dtSmtStorage.Columns.Add("DACOD");
                dtSmtStorage.Columns.Add("LOCOD");
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
                dtSmtStorage.Columns.Add("SERNO");
            }
            else
            {
                dtSmtStorage.Rows.Clear();
            }
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

        private void StorageOut_OnlineOut_SimulationOut_AGV_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AllowToClose;

            if (e.Cancel = !AllowToClose)
            {
                stsWarning.Text = "已產生扣帳單據，無法關閉視窗，請按Query查詢庫存與Save扣帳!!";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                StorageOut_AGV objStorageOut_AGV = new StorageOut_AGV(UserData, Progid);
                DataTable dtData = objStorageOut_AGV.QueryStockOutData_AGV(strWerks, strLgort, strGrpid, "Simulation", string.Empty);

                InitPrintData();

                DataRow[] drArray = dtData.Select("", "TASKID,REQID");
                foreach (DataRow dr in drArray)
                {
                    DataRow drNew = dtPrint.NewRow();
                    drNew["GRRNO"] = dr["TASKID"].ToString();
                    drNew["ITEMNUM"] = dr["REQID"].ToString().Substring(22, 6);
                    drNew["COMCD"] = dr["COMCD"].ToString();
                    drNew["WERKS"] = dr["WERKS"].ToString();
                    drNew["LOCAT"] = dr["LOCAT"].ToString();
                    drNew["LGORT"] = dr["LGORT"].ToString();
                    drNew["MBLNR"] = dr["MBLNR"].ToString();
                    drNew["MATNR"] = dr["MATNR"].ToString();
                    drNew["INSMK"] = dr["INSMK"].ToString();
                    drNew["CHARG"] = dr["CHARG"].ToString();
                    drNew["LIFNR"] = dr["LIFNR"].ToString();
                    drNew["VEDAT"] = dr["VEDAT"].ToString();
                    drNew["KDMAT"] = dr["KDMAT"].ToString();
                    drNew["INDAT"] = dr["INDAT"].ToString();
                    drNew["BKQTY"] = dr["MENGE"].ToString();
                    drNew["ALQTY"] = dr["ALQTY"].ToString();
                    drNew["BLACE"] = dr["BLACE"].ToString();
                    drNew["DACOD"] = dr["DACOD"].ToString();
                    drNew["LOCOD"] = dr["LOCOD"].ToString();
                    drNew["INSPT"] = dr["INSPT"].ToString();
                    drNew["BARCODE"] = dr["REQID"].ToString();
                    drNew["NEW_BARCODE"] = '*' + dr["REQID"].ToString() + '*';
                    drNew["GRPID"] = dr["SendID"].ToString();
                    drNew["KOSTL"] = dr["KOSTL"].ToString();
                    dtPrint.Rows.Add(drNew);
                }

                ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_DATECODE_NEW", dtPrint);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch(Exception ex)
            {
                stsWarning.Text = ex.Message + "<-Print()";
                return;
            }
        }

        private void InitPrintData()
        {
            if(dtPrint.Columns.Count==0)
            {
                dtPrint.Columns.Add("GRRNO");
                dtPrint.Columns.Add("ITEMNUM");
                dtPrint.Columns.Add("COMCD");
                dtPrint.Columns.Add("WERKS");
                dtPrint.Columns.Add("LOCAT");
                dtPrint.Columns.Add("LGORT");
                dtPrint.Columns.Add("MBLNR");
                dtPrint.Columns.Add("MATNR");
                dtPrint.Columns.Add("INSMK");
                dtPrint.Columns.Add("CHARG");
                dtPrint.Columns.Add("LIFNR");
                dtPrint.Columns.Add("VEDAT");
                dtPrint.Columns.Add("KDMAT");
                dtPrint.Columns.Add("INDAT");
                dtPrint.Columns.Add("BKQTY");
                dtPrint.Columns.Add("ALQTY");
                dtPrint.Columns.Add("RNDQTY");
                dtPrint.Columns.Add("BLACE");
                dtPrint.Columns.Add("DACOD");
                dtPrint.Columns.Add("LOCOD");
                dtPrint.Columns.Add("INSPT");
                dtPrint.Columns.Add("BARCODE");
                dtPrint.Columns.Add("PRTYP");
                dtPrint.Columns.Add("NEW_BARCODE");
                dtPrint.Columns.Add("GRPID");
                dtPrint.Columns.Add("KOSTL");
            }
            else
                dtPrint.Rows.Clear();
        }

        private void chkAllId_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAllId.Checked )
            {
                cmbGrpID.Items.Clear();
                ShowGroupId();
            }
        }

        private List<string> getShelfInfo(string strWerks,string strLgort,string strShelfType)
        {
            string strRequestType = "getShelf";
            List<string> lsShelfInfo = new List<string>();
            try
            {
                var shelfInfo = "?plant=" + strWerks;
                shelfInfo += "&storage=" + strLgort;
                shelfInfo += "&shelf_type=" + strShelfType;

                AGVApi objAGVApi = new AGVApi(UserData);
                string strRequest = objAGVApi.AGVHttpGetRequest("SimulationOut", strRequestType, shelfInfo);
                if (!string.IsNullOrEmpty(strRequest))
                {
                    DataTable dtShelf = JsonConvert.DeserializeObject<DataTable>(strRequest);
                    foreach (DataRow dr in dtShelf.Rows)
                    {
                        lsShelfInfo.Add(dr["shelf_id"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString() + "<-getShelfInfo()";
            }
            return lsShelfInfo;
        }

        public static string listToString(List<string> lsTrans, string strSeparator)
        {
            string strTrans = string.Empty;
            if (lsTrans.Count > 0)
            {
                foreach (string str in lsTrans)
                {
                    strTrans = strTrans + str + strSeparator;
                }
                strTrans = strTrans.Substring(0, strTrans.Length - strSeparator.Length);
            }
            return strTrans;
        }
    }
}
