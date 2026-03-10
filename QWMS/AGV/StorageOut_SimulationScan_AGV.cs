using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.Util;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using QCI.QWMS;
using QWMS.Common;
using QWMS_CommonInfo_Biz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Util;
using System.Windows.Forms;
using static NPOI.HSSF.Util.HSSFColor;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QWMS.OTAUT
{
    public partial class StorageOut_SimulationScan_AGV : Form
    {
        #region variable
        UserInfo UserData = new UserInfo();
        private string strWorkStation = string.Empty;
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strGrpID = string.Empty;
        private string strUsrnm = string.Empty;
        private string strProgid = string.Empty;
        private string strType = string.Empty;
        private string strPreShelf = string.Empty;//上一次料架
        private string strCurrentShelf = string.Empty; //当前料架号
        private string strFunctionName = string.Empty;
        private int intReqNO = 0; //任务序号
        private string strTaskNO = string.Empty; //任务单号
        private string strLocat = string.Empty;
        private string strDID = string.Empty;//DID数据
        private string strScanMatnr = string.Empty; //刷入料号
        private int intScanMenge = 0;//刷入数量
        private string strDateCode = string.Empty; //DateCode
        private string strVendorCode = string.Empty; //VendorCode
        private string strLotCode = string.Empty; //LotCode
        private string strUniqueId = string.Empty;//唯一码
        private string strRTaskID = string.Empty;//检验标签
        private DataTable dtStorageOutInfo = new DataTable();
        private DataTable dtCurrentShelf = new DataTable();//当前料架信息
        //private DataTable dtAddData = new DataTable();
        private DataTable dtAGVData = new DataTable();
        StorageOut_AGV objStorageOut_AGV;
        #endregion

        #region
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
        #endregion
        public StorageOut_SimulationScan_AGV(UserInfo varUserData, string varProgid)
        {
            InitializeComponent(); 
            UserData = varUserData;
            strMandt = varUserData.Client;
            strComcd = varUserData.CompanyCode;
            strUsrnm = varUserData.UserId;
            strProgid = varProgid;
            strFunctionName = "SimulationOut";

            try
            {
                objStorageOut_AGV = new StorageOut_AGV(UserData, strProgid);
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
                    ShowWorkStation();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = -1;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region SelectedIndexChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            ShowDdlLgort();
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            else
            {
                strWerks = string.Empty;
            }

            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                ShowGroupId();
            }
            else
            {
                strLgort = string.Empty;
            }
            //Plant or Storage can't be empty
            if (strWerks == string.Empty || strLgort == string.Empty)
            {
                stsWarning.Text = "Plant or Storage can't be empty!!";
                return;
            }
        }

        private void cmbWorkStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (cmbWorkStation.SelectedIndex != -1)
            {
                #region 判断该工作站是否在使用
                strWorkStation = cmbWorkStation.Items[cmbWorkStation.SelectedIndex].ToString();
                if (objStorageOut_AGV.GetAGVData(strWerks, strLgort, strWorkStation).Rows.Count > 0)
                {
                    strWorkStation = string.Empty;
                    cmbWorkStation.SelectedIndex = -1;
                    MessageBox.Show("工作站被占用!");
                    return;
                }
                #endregion
            }
        }

        private void cmbGrpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (cmbGrpID.SelectedIndex != -1)
            {
                if (cmbGrpID.Text.ToString() == string.Empty)
                {
                    stsWarning.Text = "SendID can't be empty!!";
                    return;
                }
                strGrpID = cmbGrpID.Items[cmbGrpID.SelectedIndex].ToString();
                ShowTaskNo();
            }
        }

        private void cmbTaskNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (cmbTaskNo.SelectedIndex != -1)
            {
                if (cmbTaskNo.Text.ToString() == string.Empty)
                {
                    stsWarning.Text = "TaskNO can't be empty!!";
                    return;
                }
                strTaskNO = cmbTaskNo.Items[cmbTaskNo.SelectedIndex].ToString();
            }
        }

        #endregion

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyCode == Keys.Enter)
            {
                //if (string.IsNullOrEmpty(strCurrentShelf))
                //{
                //    MessageBox.Show("Please click Call AGV first!");
                //    return;
                //}

                #region 料架切换时,判断料架是否已经抵达 (0-在途中;1-已抵达)
                strCurrentShelf = txtLocation.Text.ToString().Substring(0, 3);
                if (string.IsNullOrEmpty(strPreShelf) || !strPreShelf.Equals(strCurrentShelf))
                {
                    DataTable dtCurrentShelfState = objStorageOut_AGV.GetAGVData(strWerks, strLgort, strWorkStation, strTaskNO, strCurrentShelf);
                    if (dtCurrentShelfState.Rows.Count == 0 || !dtCurrentShelfState.Rows[0]["Shelf_state"].ToString().Equals("1"))
                    {
                        txtLocation.Text = string.Empty;
                        MessageBox.Show("The shelf didn't arrive yet.");
                        return;
                    }
                    InitAGVData();

                    txtCurrentShelf.Text = strCurrentShelf;
                    strPreShelf = strCurrentShelf;

                    #region 料架信息单独显示
                    DataRow[] drCurrentShlfInfo = dtStorageOutInfo.Select("ShelfNo='" + strCurrentShelf + "' AND Select=false");
                    if (drCurrentShlfInfo.Count() == 0)
                    {
                        MessageBox.Show("No stock at the shelf:" + strCurrentShelf + " need to be scand");
                        strCurrentShelf = string.Empty;
                        txtCurrentShelf.Text = string.Empty;
                        return;
                    }
                    dtCurrentShelf = drCurrentShlfInfo.CopyToDataTable();
                    ShowCurrentShelfInfo();
                    #endregion
                }
                #endregion

                strLocat = txtLocation.Text.ToString(); //储位数据取前八码
                DataRow[] drSendID = dtCurrentShelf.Select("SUBSTRING(LOCAT,1,8)='" + strLocat + "'");
                if (drSendID.Length == 0)
                {
                    MessageBox.Show("This location doesn't exist!");
                    strLocat = string.Empty;
                    txtLocation.Enabled = true;
                    txtLocation.Text = string.Empty;
                    return;
                }
                else
                {
                    txtLocation.Enabled = false;
                    Sound.Play(@"Sound\BIU.wav");
                    txtBarCode.Focus();
                }
            }
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (dtCurrentShelf.Rows.Count == 0)
                    {
                        playError();
                        MessageBox.Show("Please click QUERY!");
                        return;
                    }
                    if (string.IsNullOrEmpty(strLocat))
                    {
                        MessageBox.Show("Please scan the Location(请先扫描储位)");
                        txtLocation.Enabled = true;
                        txtLocation.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtBarCode.Text.ToString()))
                    {
                        MessageBox.Show("Please scan the barcode!");
                        return;
                    }
                    #region 处理刷入的数据
                    string strBarCode = txtBarCode.Text.ToString();
                    string[] str = strBarCode.Replace('；', ';').Split(';');
                    if (str.Length >= 5)  //二维码
                    {
                        strScanMatnr = str[0].ToString();//料号
                        strDateCode = str[1].ToString().Trim();//DateCode
                        strVendorCode = str[2].ToString().Trim(); //VendorCode
                        strLotCode = str[3].ToString().Trim();
                        intScanMenge = int.Parse(str[4].ToString().Trim()); //Menge
                        if (str.Length > 9)
                        {
                            strUniqueId = str[6].ToString().Trim();
                            strRTaskID = str[6].ToString().Trim().Substring(0, 15);
                        }
                        else if (str.Length > 6)
                        {
                            strUniqueId = str[6].ToString().Trim();
                        }
                    }
                    else if (strBarCode.Length > 20) //DID
                    {
                        #region 刷入DID，修改为实际DIDNO的数据（DIDNO不会拆分）
                        strDID = strBarCode;
                        DataTable dtDidInfo = objStorageOut_AGV.GetStorageOutDidData(strDID);
                        if (dtDidInfo.Rows.Count > 0)  //该DID存在
                        {
                            strScanMatnr = dtDidInfo.Rows[0]["MATNR"].ToString();//料号
                            strDateCode = dtDidInfo.Rows[0]["DACOD"].ToString();
                            strVendorCode = dtDidInfo.Rows[0]["LIFNR"].ToString();
                            strLotCode = dtDidInfo.Rows[0]["LOCOD"].ToString();
                            intScanMenge = int.Parse(dtDidInfo.Rows[0]["MENGE"].ToString());//刷入数量
                        }
                        else
                        {
                            MessageBox.Show("DIDNO doesn't exist(无此DID数据)!!!");
                            return;
                        }
                        #endregion
                    }
                    #endregion

                    DataCheckAndSave();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }
        }

        private void playError()
        {
            Sound.Play(@"Sound\ERROR.wav");
            initData();
        }

        private void DataCheckAndSave()
        {
            #region 校验数据，料号是否存在，存在再检标签时，检验前15码
            DataRow[] drSelectRows;
            if (string.IsNullOrEmpty(strRTaskID))
                drSelectRows = dtCurrentShelf.Select("MATNR='" + strScanMatnr + "' AND SUBSTRING(LOCAT,1,8)='" + strLocat + "' AND DACOD='" + strDateCode + "' AND LOCOD='" + strLotCode + "' AND LIFNR='" + strVendorCode + "' AND SERNO='" + strUniqueId + "' AND MENGE='" + intScanMenge + "' AND Select=false");
            else
                drSelectRows = dtCurrentShelf.Select("MATNR='" + strScanMatnr + "' AND SUBSTRING(LOCAT,1,8)='" + strLocat + "' AND DACOD='" + strDateCode + "' AND LOCOD='" + strLotCode + "' AND LIFNR='" + strVendorCode + "' AND SUBSTRING(SERNO,1,15)='" + strRTaskID + "' AND MENGE='" + intScanMenge + "' AND Select=false");
            if (drSelectRows.Length == 0)
            {
                playError();
                MessageBox.Show("The material doesn't exist!");
                txtBarCode.Text = string.Empty;
                txtLocation.Text = string.Empty;
                txtLocation.Focus();
                txtLocation.Enabled = true;
                return;
            }
            else
            {
                #region 更新备料单信息(FLAGE:N->Y),删除待下架库存,同时更新储位信息(LOSTS:1->0)
                if (!objStorageOut_AGV.UpdateScanOutSource(strWerks, strLgort, strTaskNO, strWorkStation, strGrpID, strCurrentShelf, drSelectRows[0]["RealLocat"].ToString()))
                {
                    MessageBox.Show("Update Stock Error");
                    txtBarCode.Text = string.Empty;
                    return;
                }
                #endregion
                drSelectRows[0]["Select"] = true;
                SetChecked();
                Sound.Play(@"Sound\BIU.wav");

                initData();
                txtLocation.Enabled = true;
                txtLocation.Text = string.Empty;
                txtLocation.Focus();
            }
            #endregion

            #region 出库数据回传敏照
            Agv_shelfMaterialRenewal agvItem = new Agv_shelfMaterialRenewal
            {
                plant = Werks, //所属厂区 32 (必填)
                storage = Lgort, //仓库别 32 (必填)
                shelf_id = strCurrentShelf, //货架编号 32 (非必填)
                action = "DEL", //操作行为 32 (必填)(ADD-入储；DEL- 出储)
                location = strLocat, //储位 32 (非必填)
                location_detail = new List<LocationDetailItem>
                            {
                                new LocationDetailItem
                                {
                                    pn = strScanMatnr,
                                    version = string.Empty,
                                    vendor_code = strVendorCode,
                                    date_code = strDateCode,
                                    quantity = intScanMenge
                                }
                            }
            };
            string strRequestType = "shelfMaterialRenewal";
            AGVApi objAGVApi = new AGVApi(UserData);
            string strResult = objAGVApi.AGVHttpRequest(strFunctionName, strRequestType, JsonConvert.SerializeObject(agvItem));

            if (!string.IsNullOrEmpty(strResult))
            {
                //待确认
            }
            #endregion
        }

        #region 判断智能料架中间表中是否有数据，若无则增加表数据
        private void InitAGVData()
        {
            dtAGVData = objStorageOut_AGV.GetAGVData(strWerks, strLgort, strWorkStation);
            if (dtAGVData.Rows.Count == 0)
            {
                MessageBox.Show("No AGV Data");
                return;
            }
            ShowAGVInfoDataGrid();
            for (int i = 0; i < dtAGVData.Rows.Count; i++)
            {
                if (dgvAGVInfo.Rows[i].Cells["ShelfNO"].Value.ToString().ToUpper() == strCurrentShelf)
                {
                    dgvAGVInfo.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                else if (dgvAGVInfo.Rows[i].Cells["Shelf State"].Value.ToString().ToUpper() == "1")
                {
                    dgvAGVInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }
        #endregion

        private void initData()
        {
            txtBarCode.Text = string.Empty;
            strDID = string.Empty;
            strScanMatnr = string.Empty;
            intScanMenge = 0;
            strDateCode = string.Empty;
            strVendorCode = string.Empty;
            strLotCode = string.Empty;
            strUniqueId = string.Empty;
            strRTaskID = string.Empty;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                btnQuery.Enabled = false;
                dtStorageOutInfo = objStorageOut_AGV.QueryStockOutData_AGV(strWerks, strLgort, strGrpID, "Simulation", "N", strTaskNO);
                if(dtStorageOutInfo.Rows.Count==0)
                {
                    MessageBox.Show("No SendID Data");
                    return;
                }

                if (!dtStorageOutInfo.Columns.Contains("Select"))
                    dtStorageOutInfo.Columns.Add("Select", typeof(bool));
                foreach (DataRow dr in dtStorageOutInfo.Rows)
                {
                    dr["Select"] = false;
                }

                ShowSendIDInfoDataGrid();
                //strTaskNO = dtStorageOutInfo.Rows[0]["TASKID"].ToString();

                cmbWorkStation.Enabled = false;
                cmbWerks.Enabled = false;
                cmbLgort.Enabled = false;
                cmbGrpID.Enabled = false;
                cmbTaskNo.Enabled = false;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message + "<-Query()";
            }
        }

        #region AGV Function

        private void btnCallAGV_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            btnQuery.Enabled = false;
            try
            {
                #region 第一次请求，将料架信息放在调度中间表
                if (intReqNO == 0)
                {
                    DataTable dtAGVTemp = dtStorageOutInfo.DefaultView.ToTable(true, new string[] { "MANDT", "WERKS", "LGORT", "TASKID", "REQID", "ShelfNo", "COMCD" });
                    if (!objStorageOut_AGV.BulkCopyWHAGV(dtAGVTemp, strWorkStation))
                    {
                        MessageBox.Show("Please click Call AGV again!");
                        return;
                    }
                    InitAGVData();
                }
                #endregion

                if (dtAGVData.Select("Shelf_state IS NULL").Count() == 0)
                {
                    MessageBox.Show("There no shelf need to work");
                    return;
                }

                #region 判断是否还有未刷入的数据，若有,提示无法call下一辆，若无，则更新备料单数据状态为Y
                if (dtCurrentShelf.Rows.Count > 0 )
                {
                    if(dtCurrentShelf.Select("Select=false").Count() > 0)
                    {
                        playError();
                        MessageBox.Show("The current shelf doesn't complete!");
                        return;
                    }
                }
                #endregion

                #region 开始呼叫AGV API，仅第一次呼叫需要单据号
                DataTable dtMblnr = new DataTable();
                dtMblnr.Columns.Add("doc_number");
                if (intReqNO == 0)
                {
                    foreach (DataRow dr in dtStorageOutInfo.Rows)
                    {
                        if (dtMblnr.Select("doc_number='" + dr["MBLNR"] + "'").Length == 0)
                        {
                            DataRow drMblnr = dtMblnr.NewRow();
                            drMblnr["doc_number"] = dr["MBLNR"].ToString();
                            dtMblnr.Rows.Add(drMblnr);
                        }
                    }
                }
                string strRequestType = "outboundTask";
                intReqNO++;
                var outboundTask = new
                {
                    plant = strWerks,
                    storage = strLgort,
                    work_station = strWorkStation,
                    doc_type = strFunctionName,
                    detail = dtMblnr,
                    task_id = strTaskNO,
                    task_sequence = intReqNO,
                    priority = 2
                };

                AGVApi objAGVApi = new AGVApi(UserData);
                string strRequest = objAGVApi.AGVHttpRequest(strFunctionName, strRequestType, JsonConvert.SerializeObject(outboundTask));

                if (!string.IsNullOrEmpty(objAGVApi.ERRMSG))
                {
                    MessageBox.Show(objAGVApi.ERRMSG.ToString());
                    return;
                }
                #endregion

                //JObject Request = (JObject)JsonConvert.DeserializeObject(strRequest);
                //strCurrentShelf = Request["shelf_id"].ToString();
                //txtCurrentShelf.Text = strCurrentShelf;

                //test
                //strCurrentShelf = "005";
                //txtCurrentShelf.Text = strCurrentShelf;

                txtLocation.Focus();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString() + "<-CallAGV()";
            }
        }

        private void btnFlip_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                #region 翻面API
                string strRequestType = "shelfFlip";
                intReqNO++;
                var shelfFlip = new
                {
                    plant = strWerks,
                    storage = strLgort,
                    work_station = strWorkStation,
                    shelf_id = strCurrentShelf,
                    task_id = strTaskNO,
                    task_sequence = intReqNO
                };

                AGVApi objAGVApi = new AGVApi(UserData);
                string strRequest = objAGVApi.AGVHttpRequest(strFunctionName, strRequestType, JsonConvert.SerializeObject(shelfFlip));
                #endregion

                if (string.IsNullOrEmpty(strRequest))
                {
                    MessageBox.Show("Filp fail");
                    return;
                }
                txtLocation.Focus();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString() + "<-Flip()";
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                //没有完成全部作业时，无法结束
                if (!complateCurrentShelf() || !complateAllWork())
                    return;

                #region 全部结束作业
                string strRequestType = "endTask";
                intReqNO++;
                var endTask = new
                {
                    plant = strWerks,
                    storage = strLgort,
                    work_station = strWorkStation,
                    task_id = strTaskNO,
                    task_sequence = intReqNO
                };
                AGVApi objAGVApi = new AGVApi(UserData);
                string strRequest = objAGVApi.AGVHttpRequest(strFunctionName, strRequestType, JsonConvert.SerializeObject(endTask));
                #endregion

                if (string.IsNullOrEmpty(strRequest))
                {
                    MessageBox.Show("Please click End Jobs again.");
                    return;
                }

                //删除料架调度中间表数据
                if (objStorageOut_AGV.DeleteAGVShelf(strWerks, strLgort, strTaskNO, strWorkStation))
                {
                    dtStorageOutInfo.Rows.Clear();
                    dtAGVData.Rows.Clear();
                    ShowSendIDInfoDataGrid();
                    ShowAGVInfoDataGrid();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString() + "<-End()";
            }
        }

        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!complateCurrentShelf() || !complateAllWork())
            {
                return;
            }
            stsWarning.Text = string.Empty;
            btnQuery.Enabled = true;
            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            cmbGrpID.Enabled = true;
            cmbTaskNo.Enabled = true;
            cmbTaskNo.Items.Clear();
            cmbWorkStation.Enabled = true;
            txtLocation.Enabled = true;
            cmbWerks.SelectedIndex = -1;
            cmbLgort.SelectedIndex = -1;
            cmbWorkStation.SelectedIndex = -1;
            cmbTaskNo.SelectedIndex = -1;
            cmbGrpID.SelectedIndex = -1;
            cmbGrpID.Text = string.Empty;
            cmbTaskNo.Text = string.Empty;
            txtBarCode.Text = string.Empty;
            txtCurrentShelf.Text = string.Empty;
            txtLocation.Text = string.Empty;
            strWerks = string.Empty;
            strLgort = string.Empty;
            strGrpID = string.Empty;
            strLocat = string.Empty;
            strPreShelf = string.Empty;
            strCurrentShelf = string.Empty;
            strWorkStation = string.Empty;
            intReqNO = 0;
            dtStorageOutInfo.Rows.Clear();
            dtAGVData.Rows.Clear();
            dtCurrentShelf.Rows.Clear();
            ShowAGVInfoDataGrid();
            ShowSendIDInfoDataGrid();
            ShowCurrentShelfInfo();
        }

        #region showBasicData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = strMandt;
            this.stsComcd.Text = strComcd;
            this.stsUsrnm.Text = strUsrnm;
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
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != string.Empty)
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

        private void ShowWorkStation()
        {
            stsWarning.Text = string.Empty;
            try
            {
                DataTable dtWorkStation = objStorageOut_AGV.QueryWorkStation(strComcd);
                if (dtWorkStation.Rows.Count > 0)
                {
                    cmbWorkStation.Items.Clear();

                    foreach (DataRow dr in dtWorkStation.Rows)
                        cmbWorkStation.Items.Add(dr["CTRLNM"].ToString());
                }
                else
                {
                    cmbWorkStation.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void ShowGroupId()
        {
            stsWarning.Text = string.Empty;
            try
            {
                DataTable dtIdData = objStorageOut_AGV.GetSimulationSendID(strWerks, strLgort);

                if (dtIdData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtIdData.Rows.Count; i++)
                    {
                        cmbGrpID.Items.Add(dtIdData.Rows[i]["SendID"]);
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
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        private void ShowTaskNo()
        {
            try
            {
                stsWarning.Text = string.Empty;
                if (!string.IsNullOrEmpty(strGrpID))
                {
                    DataTable dtTaskNoData = objStorageOut_AGV.GetSimulationSendID(strWerks, strLgort, strGrpID);

                    if (dtTaskNoData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTaskNoData.Rows.Count; i++)
                        {
                            cmbTaskNo.Items.Add(dtTaskNoData.Rows[i]["TASKID"]);
                        }
                    }
                    else
                    {
                        cmbTaskNo.Items.Clear();
                        stsWarning.Text = "No id data!!";
                        return;
                    }
                }
            }
            catch(Exception ex) 
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }
        #endregion

        #region ShowDataGrid
        private void ShowCurrentShelfInfo()
        {
            dgvCurrentShelfInfo.AutoGenerateColumns = false;
            dgvCurrentShelfInfo.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "Select";
                dgvcSelect.Name = "Select";
                dgvcSelect.ReadOnly = true;
                dgvcSelect.Width = 40;
                dgvCurrentShelfInfo.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 100;
                dgvCurrentShelfInfo.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 80;
                dgvCurrentShelfInfo.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "MATNR";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 80;
                dgvCurrentShelfInfo.Columns.Add(dgvcMatnr);

                //DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                //dgvcCharg.DataPropertyName = "CHARG";
                //dgvcCharg.HeaderText = "Batch";
                //dgvcCharg.ReadOnly = true;
                //dgvCurrentShelfInfo.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "StockOutQty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 60;
                dgvCurrentShelfInfo.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcSacnQty = new DataGridViewTextBoxColumn();
                dgvcSacnQty.DataPropertyName = "ScanQty";
                dgvcSacnQty.HeaderText = "ScanQty";
                dgvcSacnQty.ReadOnly = true;
                dgvcSacnQty.Width = 60;
                dgvCurrentShelfInfo.Columns.Add(dgvcSacnQty);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 60;
                dgvCurrentShelfInfo.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvVedat = new DataGridViewTextBoxColumn();
                dgvVedat.DataPropertyName = "VEDAT";
                dgvVedat.HeaderText = "VendorDate";
                dgvVedat.ReadOnly = true;
                dgvVedat.Width = 65;
                dgvCurrentShelfInfo.Columns.Add(dgvVedat);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 60;
                dgvCurrentShelfInfo.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "CostCenter";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width = 60;
                dgvCurrentShelfInfo.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serno";
                dgvcSerno.ReadOnly = true;
                dgvcSerno.Width = 160;
                dgvCurrentShelfInfo.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "LotCode";
                dgvcLocod.ReadOnly = true;
                dgvcLocod.Width = 80;
                dgvCurrentShelfInfo.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcLine = new DataGridViewTextBoxColumn();
                dgvcLine.DataPropertyName = "LINE";
                dgvcLine.HeaderText = "Line";
                dgvcLine.ReadOnly = true;
                dgvCurrentShelfInfo.Columns.Add(dgvcLine);

                DataGridViewTextBoxColumn dgvcSide = new DataGridViewTextBoxColumn();
                dgvcSide.DataPropertyName = "SIDE";
                dgvcSide.HeaderText = "Side";
                dgvcSide.ReadOnly = true;
                dgvCurrentShelfInfo.Columns.Add(dgvcSide);

                dgvCurrentShelfInfo.DataSource = dtCurrentShelf;
                tabCurrentInfo.TabPages[0].Text = "Current Shelf Info:" + dtCurrentShelf.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSendIDInfoDataGrid()");
            }
        }

        private void ShowSendIDInfoDataGrid()
        {
            dgvSendIDInfo.AutoGenerateColumns = false;
            dgvSendIDInfo.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 100;
                dgvSendIDInfo.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcShelfNo = new DataGridViewTextBoxColumn();
                dgvcShelfNo.DataPropertyName = "ShelfNo";
                dgvcShelfNo.HeaderText = "ShelfNo";
                dgvcShelfNo.ReadOnly = true;
                dgvcShelfNo.Width = 50;
                dgvSendIDInfo.Columns.Add(dgvcShelfNo);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 80;
                dgvSendIDInfo.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "MATNR";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 80;
                dgvSendIDInfo.Columns.Add(dgvcMatnr);

                //DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                //dgvcCharg.DataPropertyName = "CHARG";
                //dgvcCharg.HeaderText = "Batch";
                //dgvcCharg.ReadOnly = true;
                //dgvSendIDInfo.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "StockOutQty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 60;
                dgvSendIDInfo.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcSacnQty = new DataGridViewTextBoxColumn();
                dgvcSacnQty.DataPropertyName = "ScanQty";
                dgvcSacnQty.HeaderText = "ScanQty";
                dgvcSacnQty.ReadOnly = true;
                dgvcSacnQty.Width = 60;
                dgvSendIDInfo.Columns.Add(dgvcSacnQty);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 60;
                dgvSendIDInfo.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvVedat = new DataGridViewTextBoxColumn();
                dgvVedat.DataPropertyName = "VEDAT";
                dgvVedat.HeaderText = "VendorDate";
                dgvVedat.ReadOnly = true;
                dgvVedat.Width = 65;
                dgvSendIDInfo.Columns.Add(dgvVedat);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 60;
                dgvSendIDInfo.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "CostCenter";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width = 60;
                dgvSendIDInfo.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serno";
                dgvcSerno.ReadOnly = true;
                dgvcSerno.Width = 100;
                dgvSendIDInfo.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "LotCode";
                dgvcLocod.ReadOnly = true;
                dgvcLocod.Width = 80;
                dgvSendIDInfo.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcLine = new DataGridViewTextBoxColumn();
                dgvcLine.DataPropertyName = "LINE";
                dgvcLine.HeaderText = "Line";
                dgvcLine.ReadOnly = true;
                dgvSendIDInfo.Columns.Add(dgvcLine);

                DataGridViewTextBoxColumn dgvcSide = new DataGridViewTextBoxColumn();
                dgvcSide.DataPropertyName = "SIDE";
                dgvcSide.HeaderText = "Side";
                dgvcSide.ReadOnly = true;
                dgvSendIDInfo.Columns.Add(dgvcSide);

                dgvSendIDInfo.DataSource = dtStorageOutInfo;

                tabTotalInfo.TabPages[0].Text = "StorageOut Info:" + dtStorageOutInfo.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSendIDInfoDataGrid()");
            }
        }

        private void ShowAGVInfoDataGrid()
        {
            dgvAGVInfo.AutoGenerateColumns = false;
            dgvAGVInfo.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcStation = new DataGridViewTextBoxColumn();
                dgvcStation.DataPropertyName = "PLACE";
                dgvcStation.HeaderText = "WorkStation";
                dgvcStation.ReadOnly = true;
                dgvcStation.Width = 80;
                dgvAGVInfo.Columns.Add(dgvcStation);

                DataGridViewTextBoxColumn dgvcTaskNo = new DataGridViewTextBoxColumn();
                dgvcTaskNo.DataPropertyName = "TASKID";
                dgvcTaskNo.HeaderText = "TaskNO";
                dgvcTaskNo.ReadOnly = true;
                dgvcTaskNo.Width = 150;
                dgvAGVInfo.Columns.Add(dgvcTaskNo);

                //DataGridViewTextBoxColumn dgvcReqNo = new DataGridViewTextBoxColumn();
                //dgvcReqNo.DataPropertyName = "REQNO";
                //dgvcReqNo.HeaderText = "ReqNo";
                //dgvcReqNo.ReadOnly = true;
                //dgvAGVInfo.Columns.Add(dgvcReqNo);

                DataGridViewTextBoxColumn dgvcShelf = new DataGridViewTextBoxColumn();
                dgvcShelf.DataPropertyName = "MARNO";
                dgvcShelf.HeaderText = "ShelfNO";
                dgvcShelf.Name = "ShelfNO";
                dgvcShelf.ReadOnly = true;
                dgvcShelf.Width = 80;
                dgvAGVInfo.Columns.Add(dgvcShelf);

                DataGridViewTextBoxColumn dgvcShelfState = new DataGridViewTextBoxColumn();
                dgvcShelfState.DataPropertyName = "Shelf_state";
                dgvcShelfState.HeaderText = "Shelf State";
                dgvcShelfState.Name = "Shelf State";
                dgvcShelfState.ReadOnly = true;
                dgvcShelfState.Width = 90;
                dgvAGVInfo.Columns.Add(dgvcShelfState);

                dgvAGVInfo.DataSource = dtAGVData;
                tabAGVInfo.TabPages[0].Text = "AGV Order:" + dtAGVData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowAGVInfoDataGrid()");
            }
        }
        #endregion

        public void SetChecked()
        {
            for (int i = 0; i < dtCurrentShelf.Rows.Count; i++)
            {
                if (dgvCurrentShelfInfo.Rows[i].Cells["Select"].Value.ToString().ToUpper() == "TRUE")
                {
                    dgvCurrentShelfInfo.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }
        private DataTable GetAddData(DataRow[] drAdd)
        {
            var query = from row in drAdd.AsEnumerable()
                        group row by new
                        {
                            COMCD = row.Field<string>("COMCD"),
                            WERKS = row.Field<string>("WERKS"),
                            LGORT = row.Field<string>("LGORT"),
                            SENDID = row.Field<string>("SendID"),
                            MBLNR = row.Field<string>("MBLNR"),
                            MATNR = row.Field<string>("MATNR"),
                            CHARG = row.Field<string>("CHARG")
                        } into m
                        select new
                        {
                            COMCD = m.FirstOrDefault().Field<string>("COMCD"),
                            WERKS = m.FirstOrDefault().Field<string>("WERKS"),
                            LGORT = m.FirstOrDefault().Field<string>("LGORT"),
                            SENDID = m.FirstOrDefault().Field<string>("SENDID"),
                            MBLNR = m.FirstOrDefault().Field<string>("MBLNR"),
                            MATNR = m.FirstOrDefault().Field<string>("MATNR"),
                            MENGE = m.Sum(n => n.Field<int>("MENGE")),
                            AddQty= m.Sum(n => n.Field<int>("AddQty")),
                            CHARG = m.FirstOrDefault().Field<string>("CHARG")
                        };

            DataTable dtAddByShelf = new DataTable();
            dtAddByShelf.Columns.Add("COMCD");
            dtAddByShelf.Columns.Add("WERKS");
            dtAddByShelf.Columns.Add("LGORT");
            dtAddByShelf.Columns.Add("SENDID");
            dtAddByShelf.Columns.Add("MBLNR");
            dtAddByShelf.Columns.Add("MATNR");
            dtAddByShelf.Columns.Add("MENGE");
            dtAddByShelf.Columns.Add("AddQty");
            dtAddByShelf.Columns.Add("CHARG");
            dtAddByShelf.Columns.Add("AddMBLNR");

            foreach (var item in query)
            {
                DataRow drAddByShelf = dtAddByShelf.NewRow();
                drAddByShelf["COMCD"] = item.COMCD;
                drAddByShelf["WERKS"] = item.WERKS;
                drAddByShelf["LGORT"] = item.LGORT;
                drAddByShelf["SENDID"] = item.SENDID;
                drAddByShelf["MBLNR"] = item.MBLNR;
                drAddByShelf["MATNR"] = item.MATNR;
                drAddByShelf["MENGE"] = item.MENGE;
                drAddByShelf["AddQty"] = item.AddQty;
                drAddByShelf["CHARG"] = item.CHARG;
                drAddByShelf["AddMBLNR"] = "";
                dtAddByShelf.Rows.Add(drAddByShelf);
            }

            //取得虛擬單據新的流水號,一次性更新到所需的起始序列和终止序列
            DataTable dtSerno = objStorageOut_AGV.GetSimulationNumBeginEnd(dtAddByShelf.Rows.Count);
            foreach (DataRow drAddByShelf in dtAddByShelf.Rows)
            {
                int i = 0;
                int strSerno = i + int.Parse(dtSerno.Rows[0][0].ToString());
                drAddByShelf["AddMBLNR"] = "66" + strWerks.Substring(2, 2) + strSerno.ToString("000000");
                i++;
            }
            return dtAddByShelf;
        }

        private void StorageOut_SimulationScan_AGV_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(dtAGVData != null && dtAGVData.Columns.Contains("Shelf_state") && dtAGVData.Select("Shelf_state IS NULL").Count()>0)
            {
                MessageBox.Show("There has shelf doesn't work!");
                playError();
                e.Cancel = true;
            }
        }

        #region 判断是否已完成全部作业
        private bool complateAllWork()
        {
            bool blResult = true;
            if (dtAGVData.Rows.Count > 0 && dtAGVData.Select("Shelf_state IS NULL").Count() > 0)
            {
                blResult = false;
                playError();
                MessageBox.Show("There has shelf doesn't work!");
            }
            return blResult;
        }
        #endregion

        private bool complateCurrentShelf()
        {
            bool blResult = true;
            if (dtCurrentShelf.Rows.Count > 0 && dtCurrentShelf.Select("Select=false").Count() > 0)
            {
                blResult = false;
                playError();
                MessageBox.Show("The current shelf doesn't complete!");
            }
            return blResult;
        }
    }
}
