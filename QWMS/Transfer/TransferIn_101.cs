using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class TransferIn_101 : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strProgid = string.Empty;
        private string strFWerks = string.Empty;
        private string strFLgort = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strLocat = string.Empty;
        private string strMblnr = string.Empty;
        private DataTable dtInStock = new DataTable();
        private DataTable dtCombine = new DataTable();
        private List<string> lsMblnr = new List<string>();
        private Replenishment objReplenishment;
        private Authority objAuthority;
        private Transfer objTransfer;
        private PlantData objPlantData;
        private DataTable dtTransData= new DataTable();
        private string strCheckType = "Tranfer101";
        private string strLgortType = "MLB";
        private DataTable dtScanTable = new DataTable();
        private int ScanMenge= 0;
        private int MblnrMenge = 0;
        public TransferIn_101(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            strMandt = UserData.Client;
            strComcd = UserData.CompanyCode;
            strUsrnm = UserData.UserId;
            strProgid = varProgid;
            try
            {
                objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strProgid);
                objTransfer = new Transfer(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);

                if (!objReplenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    InitData();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ShowStatusData()
        {
            stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            stsMandt.Text = UserData.Client;
            stsComcd.Text = UserData.CompanyCode;
            stsUsrnm.Text = UserData.UserId;
        }

        private void txtMblnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyChar == (char)13)
            {
                try
                {
                    string strMblnr351 = txtMblnr.Text.ToString();
                    if (strMblnr351 == "")
                    {
                        stsWarning.Text = "输入单据不能为空！！";
                    }

                    splitContainer4.Panel1.Enabled = false;
                    rdoNormal.Enabled = false;
                    rdoQWMS.Enabled = false;

                    if (strCheckType == "Tranfer101")
                    {
                        #region 获取调拨单号数据以及调拨入的厂区信息
                        dtTransData = objTransfer.GetTransferNo(string.Empty, strMblnr351);
                        if (dtTransData.Rows.Count > 0)
                        {
                            strMblnr = dtTransData.Rows[0]["MBLNR"].ToString();
                            strWerks = dtTransData.Rows[0]["DWERKS"].ToString();
                            strFWerks = dtTransData.Rows[0]["WERKS"].ToString();
                            strFLgort = dtTransData.Rows[0]["LGORT"].ToString();
                            for (int i = 0; i<dtTransData.Rows.Count; i++)
                            {
                                MblnrMenge += Convert.ToInt32(dtTransData.Rows[i]["MENGE"].ToString());
                            }
                        }
                        else
                        {
                            stsWarning.Text = "无此调拨单数据！";
                            failSound();
                            txtMblnr.Text = string.Empty;
                            return;
                        }
                        lblWerks.Text = strWerks;
                        txtLgort.Text = strFLgort;
                        #region 特殊仓别351调拨接收的时候仓别固定自动回车到输储位步骤
                        if (dtTransData.Rows[0]["COMCD"].ToString() == "9200" && objTransfer.QuryLgortInfo(dtTransData.Rows[0]["LGORT"].ToString()))
                        {
                            txtLgort.Text = dtTransData.Rows[0]["LGORT"].ToString();
                            txtLgort.Enabled = false;
                            LgortKeyChar();
                        }

                        #endregion


                        //if (!string.IsNullOrEmpty(dtTransData.Rows[0]["DLGORT"].ToString()))
                        //{
                        //    strLgort = dtTransData.Rows[0]["DLGORT"].ToString();
                        //    cmbLgort.Enabled = false;
                        //    txtLocat.Enabled = true;
                        //}
                        //else

                        //if (!string.IsNullOrEmpty(strWerks))
                        //{
                        //    ShowDdlLgort();
                        //}
                        #region 非MLB入库
                        if(rdoOthers.Checked)
                        {
                            rdoQWMS.Enabled = false;
                            txtCode.Enabled = false;
                            btnConfirm.Enabled = false;
                            dtTransData.Columns.Add("DLOCAT");

                            //if (strWerks.Equals("CS41")) //ASRS储位
                            //{
                            //    lblasrs.Visible = true;
                            //    cmbasrs.Visible = true;
                            //    cmbasrs.Enabled = true;
                            //}
                            //else
                            //{
                            //    lblasrs.Visible = false;
                            //    cmbasrs.Enabled = false;
                            //    cmbasrs.Visible = false;
                            //}
                        }
                        #endregion

                        successSound();
                        txtMblnr.Enabled = false;
                        if (rdoOthers.Checked && strWerks != "CS42")
                        {
                            ShowOtherMblnrDataGridView(dtTransData);
                        }
                        else
                        {
                            ShowMblnrDataGridView();
                        }
                        #endregion
                    }
                    else if (strCheckType == "OnlyQWMS")
                    {
                        #region 获取调拨单号数据以及调拨入的厂区信息
                        dtTransData = objTransfer.GetTransfer101SAP(strMblnr351,string.Empty,"351DWN");
                        if (dtTransData.Rows.Count > 0)
                        {
                            strMblnr = dtTransData.Rows[0]["MBLNR"].ToString();
                            strWerks = dtTransData.Rows[0]["DWERKS"].ToString();
                            strFWerks = dtTransData.Rows[0]["WERKS"].ToString();
                            strFLgort = dtTransData.Rows[0]["LGORT"].ToString();
                        }
                        else
                        {
                            stsWarning.Text = "无此调拨单数据！";
                            failSound();
                            txtMblnr.Text = string.Empty;
                            return;
                        }
                        #endregion

                        #region 获取101调拨扣账信息
                        if (string.IsNullOrEmpty(dtTransData.Rows[0]["MBLNR101"].ToString().Trim()))
                        {
                            DataTable dt101SAP = new DataTable();
                            dt101SAP = objTransfer.GetTransfer101SAP(strMblnr351, strWerks, "101DWN");
                            if (dt101SAP.Rows.Count>0)
                            {
                                //回传PODWN表扣账信息
                                objTransfer.updateTransfer101SAP(strMblnr351,dt101SAP.Rows[0]["MBLNR101"].ToString().Trim());
                            }
                            else
                            {
                                stsWarning.Text = "查询不到扣账信息不能仅入QWMS！！";
                                return;
                            }
                        }
                        #endregion

                        #region 获取仅入QWMS入库数据
                        lblWerks.Text = strWerks;
                        txtLgort.Enabled = false;
                        txtMblnr.Enabled = false;
                        ShowMblnrDataGridView();
                        successSound();
                        StorageIn objStorageIn = new StorageIn(UserData, strProgid);
                        dtInStock = objStorageIn.GetEcInStock(strMblnr);
                        if (dtInStock.Rows.Count > 0)
                        {
                            ShowScanDataGridView();
                        }
                        else
                        {
                            btnConfirm.Enabled = false;
                            stsWarning.Text = "无入库数据不能仅入QWMS！！";
                            return;
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "<-txtMblnr_KeyPress()");
                }
            }
        }

        private void GetDefaultScanTable()
        {
            dtScanTable = new DataTable();
            dtScanTable.Columns.Add("LOCAT");
            dtScanTable.Columns.Add("MATNR");
            dtScanTable.Columns.Add("DACOD");
            dtScanTable.Columns.Add("LIFNR");
            dtScanTable.Columns.Add("CHARG");
            dtScanTable.Columns.Add("LOCOD");
            dtScanTable.Columns.Add("MENGE");
            dtScanTable.Columns.Add("VEDAT");
            dtScanTable.Columns.Add("ExpiryDate");
            dtScanTable.Columns.Add("TASKID");
            dtScanTable.Columns.Add("SERNO");
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtCode.ToString()))
                {
                    failSound();
                    return;
                }
                if (txtCode.Text.ToUpper().ToString().Equals("OK"))
                {
                    txtLocat.Enabled = true;
                    txtLocat.Text = string.Empty;
                    txtLocat.Focus();
                    txtCode.Text = string.Empty;
                    txtCode.Enabled = false;
                    return;
                }
                if (objPlantData.CheckLOCODLGORT(strWerks, strLgort) && objPlantData.CheckDACODLGORT(strWerks, strLgort) && strWerks == "CS42" && rdoOthers.Checked)
                {
                    string strBarCode = "";
                    strBarCode = txtCode.Text.Trim().ToUpper();
                    string[] str = strBarCode.Split(';');
                    DataRow drScan = dtScanTable.NewRow();
                    drScan["LOCAT"] = strLocat;
                    //正常交料 9项：AL040170001; 202250; TIR-TIC; 5052548MY2; 3000; TPS40170QRGYRQ1; BTIR-TIC230223A0254MY; Made in Malaysia; TPS40170QRGYRQ1
                    //IQC ReLabel 10项：DAV31UECCC0; 0723; AKV-AMV; 0723; 20; PCB V31U ECU/B (12L,200*184,REVC); R7202308280000711004; MADE IN TAIWAN; THIW12C986B; 20240213
                    //IQC ReLabel 10项：CS16982FB07; 20201008; QCI-KOA; 20201008; 9564; ; R7202308240014311001; ; ; 20251008
                    //物料打印 5项：DA0V3HVB4A0; 2423-0D5Q; GRU-ZDT; SP1230608055; 45
                    //IQC检验过渡阶段产生 7项：cs41472fb05;201712;wpk-dal;0007031156;3593;20240320;r72023081700017
                    if (str.Length >= 5)
                    {
                        DataRow[] drTransMatnr = dtTransData.Select("MATNR='" + str[0].ToString().Trim() + "' ");
                        if (drTransMatnr.Length == 0)
                        {
                            stsWarning.Text = "单据内无此料号" + str[0].ToString().Trim() + "，请确认！";
                            SetErrNotice();
                            return;
                        }

                        //09-01 刷入卡控重写，同一储位同料号不能入不同DC，LOCOD管控仓同储位同料号不能入相同的LOCOD
                        DataRow[] drS = dtScanTable.Select("MATNR='" + str[0].ToString().Trim() + "' AND LOCAT='" + strLocat + "' ");
                        if (drS.Length > 0)
                        {
                            #region 同一储位刷入多笔的时候同一料号，但是DateCode不一致，不能入库
                            DataRow drExist = drS[0];
                            if (str[1].ToString().Trim() != drExist["DACOD"].ToString())
                            {
                                stsWarning.Text = "同料号不同Date Code不能入同一个储位，请确认！";
                                SetErrNotice();
                                return;
                            }
                            #endregion

                            #region Lot Code不同Lot Code不允许入库 - Lot code仓别
                            if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                            {
                                if (str[3].ToString().Trim() != drExist["LOCOD"].ToString())
                                {
                                    stsWarning.Text = "同料号不同Lot Code不能入同一个储位，请确认！";
                                    SetErrNotice();
                                    return;
                                }
                            }
                            #endregion
                            drScan["MATNR"] = str[0].ToString().Trim();
                            drScan["DACOD"] = str[1].ToString().Trim();
                            drScan["LIFNR"] = str[2].ToString().Trim();
                            if (str[0].ToString().Trim().Substring(0, 2) == "SA" || str[0].ToString().Trim().Substring(0, 2) == "DA")
                            {
                                drScan["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? str[2].ToString().Trim().Substring((str[2].ToString().Trim().Length) - 3) : "";
                            }
                            else
                            {
                                drScan["CHARG"] = "";
                            }
                            drScan["LOCOD"] = str[3].ToString().Trim();
                            drScan["MENGE"] = Convert.ToInt32(drExist["MENGE"]) + Convert.ToInt32(str[4].ToString().Trim());
                            if (str.Length == 9)
                            {
                                drScan["SERNO"] = str[6].ToString().Trim();
                            }
                            //大于9最后一项为保存期，则为IQC检验过的材料
                            if (str.Length > 9)
                            {
                                string strExpiryDate = str[str.Length - 1].ToString().Trim();
                                if (!ClaCommon.CheckDateValid(strExpiryDate))
                                {
                                    MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strExpiryDate));
                                    return;
                                }
                                drScan["ExpiryDate"] = strExpiryDate;
                                drScan["TASKID"] = str[6].ToString().Trim().Substring(0, 15);
                                drScan["SERNO"] = str[6].ToString().Trim();
                            }
                            //等于7最后一项为检验批号，倒数第二为保存期
                            if (str.Length == 7)
                            {
                                string strExpiryDate = str[5].ToString().Trim();
                                if (!ClaCommon.CheckDateValid(strExpiryDate))
                                {
                                    MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strExpiryDate));
                                    return;
                                }
                                drScan["ExpiryDate"] = strExpiryDate;
                                drScan["TASKID"] = str[6].ToString().Trim();
                            }
                        }
                        else
                        {
                            if (objStorageData.CheckExistedSameMaterialDC(strLocat, str[0].ToString().Trim(), "G", str[1].ToString().Trim()))
                            {
                                stsWarning.Text = str[0].ToString().Trim() + "该料号在该储位已经存在不同DateCode，请确认";
                                SetErrNotice();
                                return;
                            }
                            if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                            {
                                if (objStorageData.CheckExistedDifferentLOCAD(strLocat, str[0].ToString().Trim(), str[3].ToString().Trim()))
                                {
                                    stsWarning.Text = str[0].ToString().Trim() + "该料号在该储位已经存在不同Lot Code，请确认！";
                                    SetErrNotice();
                                    return;
                                }
                            }
                            drScan["MATNR"] = str[0].ToString().Trim();
                            drScan["DACOD"] = str[1].ToString().Trim();
                            drScan["LIFNR"] = str[2].ToString().Trim();
                            if (str[0].ToString().Trim().Substring(0, 2) == "SA" || str[0].ToString().Trim().Substring(0, 2) == "DA")
                            {
                                drScan["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? str[2].ToString().Trim().Substring((str[2].ToString().Trim().Length) - 3) : "";
                            }
                            else
                            {
                                drScan["CHARG"] = "";
                            }
                            drScan["LOCOD"] = str[3].ToString().Trim();
                            drScan["MENGE"] = Convert.ToInt32(str[4].ToString().Trim());
                            if (str.Length == 9)
                            {
                                drScan["SERNO"] = str[6].ToString().Trim();
                            }
                            //大于9最后一项为保存期，则为IQC检验过的材料
                            if (str.Length > 9)
                            {
                                string strExpiryDate = str[str.Length - 1].ToString().Trim();
                                if (!ClaCommon.CheckDateValid(strExpiryDate))
                                {
                                    MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strExpiryDate));
                                    return;
                                }
                                drScan["ExpiryDate"] = strExpiryDate;
                                drScan["TASKID"] = str[6].ToString().Trim().Substring(0, 15);
                                drScan["SERNO"] = str[6].ToString().Trim();
                            }
                            //等于7最后一项为检验批号，倒数第二为保存期
                            if (str.Length == 7)
                            {
                                string strExpiryDate = str[5].ToString().Trim();
                                if (!ClaCommon.CheckDateValid(strExpiryDate))
                                {
                                    MessageBox.Show(string.Format("DateCode(After Transfer):{0} format incorrect!!", strExpiryDate));
                                    return;
                                }
                                drScan["ExpiryDate"] = strExpiryDate;
                                drScan["TASKID"] = str[6].ToString().Trim();
                            }
                        }
                    }
                    //DID退料 1项：DFHD28MR005-CC37MRR0001
                    else
                    {
                        if (str.Length == 1)
                        {
                            //1项则为DIDNO，查询WHRID表，抓取数据
                            DataTable dtData = objStorageData.QueryIQC_WHRID(strBarCode);
                            //MATNR,DACOD,LIFNR,LOCOD,MENGE
                            if (dtData.Rows.Count > 0)
                            {
                                DataRow[] drTrans = dtTransData.Select("MATNR='" + dtData.Rows[0]["MATNR"].ToString() + "' ");
                                if (drTrans.Length == 0)
                                {
                                    stsWarning.Text = "单据内无此料号" + dtData.Rows[0]["MATNR"].ToString() + "，请确认！";
                                    SetErrNotice();
                                    return;
                                }
                                DataRow[] drS = dtScanTable.Select("MATNR='" + dtData.Rows[0]["MATNR"].ToString() + "' AND LOCAT = '"+ strLocat +"' ");
                                if (drS.Length > 0)
                                {
                                    #region 刷入多笔的时候同一料号，但是DateCode不一致，不能入库
                                    DataRow drExist = drS[0];
                                    //if (dtData.Rows[0]["LIFNR"].ToString() != drExist["LIFNR"].ToString())
                                    //{
                                    //    stsWarning.Text = "The same item number with different Vendor Codes cannot be placed in the same storage location. Please confirm!";
                                    //    SetErrNotice();
                                    //    return;
                                    //}
                                    if (dtData.Rows[0]["DACOD"].ToString() != drExist["DACOD"].ToString())
                                    {
                                        stsWarning.Text = "The same item number with different DateCode Codes cannot be placed in the same storage location. Please confirm!";
                                        SetErrNotice();
                                        return;
                                    }
                                    #endregion
                                    #region Lot Code不同Lot Code不允许入库
                                    if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                                    {
                                        if (dtData.Rows[0]["LOCOD"].ToString() != drExist["LOCOD"].ToString())
                                        {
                                            stsWarning.Text = "The same item number with different Lot Code Codes cannot be placed in the same storage location. Please confirm!";
                                            SetErrNotice();
                                            return;
                                        }
                                    }
                                    #endregion
                                    drScan["MATNR"] = dtData.Rows[0]["MATNR"].ToString();
                                    drScan["DACOD"] = dtData.Rows[0]["DACOD"].ToString();
                                    drScan["LIFNR"] = dtData.Rows[0]["LIFNR"].ToString();
                                    if (dtData.Rows[0]["MATNR"].ToString().Substring(0, 2) == "SA" || dtData.Rows[0]["MATNR"].ToString().Substring(0, 2) == "DA")
                                    {
                                        drScan["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? dtData.Rows[0]["LIFNR"].ToString().Substring((dtData.Rows[0]["LIFNR"].ToString().Length) - 3) : "";
                                    }
                                    else
                                    {
                                        drScan["CHARG"] = "";
                                    }
                                    drScan["LOCOD"] = dtData.Rows[0]["LOCOD"].ToString();
                                    drScan["MENGE"] = Convert.ToInt32(drExist["MENGE"]) + Convert.ToInt32(dtData.Rows[0]["MENGE"].ToString());
                                    drScan["ExpiryDate"] = dtData.Rows[0]["EXPDAT"].ToString();
                                    drScan["TASKID"] = dtData.Rows[0]["TASKID"].ToString();
                                    drScan["SERNO"] = dtData.Rows[0]["SERNO"].ToString();
                                }
                                else
                                {
                                    drScan["MATNR"] = dtData.Rows[0]["MATNR"].ToString();
                                    drScan["DACOD"] = dtData.Rows[0]["DACOD"].ToString();
                                    drScan["LIFNR"] = dtData.Rows[0]["LIFNR"].ToString();
                                    if (dtData.Rows[0]["MATNR"].ToString().Substring(0, 2) == "SA" || dtData.Rows[0]["MATNR"].ToString().Substring(0, 2) == "DA")
                                    {
                                        drScan["CHARG"] = objPlantData.CheckCHARGLGORT(strWerks) ? dtData.Rows[0]["LIFNR"].ToString().Substring((dtData.Rows[0]["LIFNR"].ToString().Length) - 3) : "";
                                    }
                                    else
                                    {
                                        drScan["CHARG"] = "";
                                    }
                                    drScan["LOCOD"] = dtData.Rows[0]["LOCOD"].ToString();
                                    drScan["MENGE"] = Convert.ToInt32(dtData.Rows[0]["MENGE"].ToString());
                                    drScan["ExpiryDate"] = dtData.Rows[0]["EXPDAT"].ToString();
                                    drScan["TASKID"] = dtData.Rows[0]["TASKID"].ToString();
                                    drScan["SERNO"] = dtData.Rows[0]["SERNO"].ToString();
                                }
                            }
                            else
                            {
                                stsWarning.Text = "No information was found for the DIDNO document. Please be informed!";
                                return;
                            }
                        }
                        else
                        {
                            stsWarning.Text = "Incorrect number of items for the physical QR code. Please confirm!";
                            return;
                        }
                    }

                    #region 处理DateCode转换问题
                    try
                    {
                        //StorageData objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);
                        string DC_After = objStorageData.WHDCR_Query(drScan["LIFNR"].ToString(), drScan["DACOD"].ToString());
                        if (DC_After != "")
                        {
                            DateTime dtVedat = Convert.ToDateTime(DC_After);
                            DC_After = dtVedat.ToString("yyyyMMdd");
                            drScan["VEDAT"] = DC_After;
                        }
                        else
                        {
                            #region 确认是否要转化DateCode Rule
                            string strTemp = objStorageData.getDCTrans(drScan["LIFNR"].ToString(), drScan["DACOD"].ToString()).ToString();
                            if (!string.IsNullOrEmpty(strTemp))
                            {
                                //strDC = DateTime.Now.ToString("yyyyMMdd");
                                DataTable dtNewDateCode = new DataTable();
                                dtNewDateCode.Columns.Add("LIFNR");
                                dtNewDateCode.Columns.Add("DC_Before");
                                dtNewDateCode.Columns.Add("DC_After");

                                DataRow dr = dtNewDateCode.NewRow();
                                dr["LIFNR"] = drScan["LIFNR"].ToString();
                                dr["DC_Before"] = drScan["DACOD"].ToString();
                                dr["DC_After"] = strTemp;
                                dtNewDateCode.Rows.Add(dr.ItemArray);
                                objStorageData.WHDCR_DML(dtNewDateCode, "NEW", "System");
                                drScan["VEDAT"] = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");
                            }
                            else
                            {
                                MessageBox.Show("无D/C转换信息找D/C管理人员处理");
                                return;
                            }
                            #endregion
                        }
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show("DateCode转化错误！" + e1.ToString());
                        SetErrNotice();
                        return;
                    }
                    #endregion
                    #region 防呆
                    try
                    {
                        if (dtScanTable.Rows.Count > 0)
                        {
                            ScanMenge += Convert.ToInt32(drScan["MENGE"].ToString().Trim());
                            if (ScanMenge > MblnrMenge)
                            {
                                stsWarning.Text = "刷入数量大于单据数量，请确认！";
                                ScanMenge -= Convert.ToInt32(drScan["MENGE"].ToString().Trim());
                                return;
                            }
                            #region 验证是否第一次刷入
                            DataRow[] drExist = dtScanTable.Select(" LOCAT='" + drScan["LOCAT"].ToString().Trim() + "' AND MATNR='" + drScan["MATNR"].ToString().Trim() + "' AND DACOD='" + drScan["DACOD"].ToString().Trim() + "' ");
                            if (drExist.Length > 0)
                            {
                                #region 相同料号，相同DateCode，相同储位，数量相加
                                DataRow[] drInstockExists = dtInStock.Select(" LOCAT='" + drScan["LOCAT"].ToString().Trim() + "' AND MATNR='" + drScan["MATNR"].ToString().Trim() + "' AND DACOD='" + drScan["DACOD"].ToString().Trim() + "' ");
                                dtScanTable.Rows.Remove(drExist[0]);
                                //drScan["MENGE"] = Convert.ToInt32(drInstockExists[0]["MENGE"].ToString().Trim()) + Convert.ToInt32(drScan["MENGE"].ToString().Trim());
                                if (drInstockExists.Length > 0)
                                {
                                    dtInStock.Rows.Remove(drInstockExists[0]);
                                }
                                dtScanTable.Rows.Add(drScan);
                                SetScanTableToInstock(drScan);
                                ShowScanDCDataGridView(dtScanTable);
                                #endregion
                            }
                            else
                            {
                                #region  第一次刷入
                                dtScanTable.Rows.Add(drScan);
                                SetScanTableToInstock(drScan);
                                ShowScanDCDataGridView(dtScanTable);
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region 第一次刷入
                            ScanMenge += Convert.ToInt32(drScan["MENGE"].ToString().Trim());
                            if (ScanMenge > MblnrMenge)
                            {
                                stsWarning.Text = "刷入数量大于单据数量，请确认！";
                                ScanMenge -= Convert.ToInt32(drScan["MENGE"].ToString().Trim());
                                return;
                            }
                            dtScanTable.Rows.Add(drScan);
                            SetScanTableToInstock(drScan);
                            ShowScanDCDataGridView(dtScanTable);
                            #endregion
                        }
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show("库存校验错误" + e2.ToString());
                        SetErrNotice();
                        return;
                    }
                    #endregion
                    SoundPlayer sp = new SoundPlayer(@"Sound\Success.wav");
                    sp.Play();
                    txtCode.Focus();
                }
                else
                {
                    string strCurrentMatnr = string.Empty; //刷入数据的料号
                    string strCurrentCharg = string.Empty; //刷入数据料号的版本
                    string strCurrentMblnr = string.Empty; //刷入数据的单据号
                    string strPallet = txtCode.Text.ToUpper().ToString();
                    DataTable dtPallet = objTransfer.GetPalletPNNum(strPallet);

                    if(dtPallet.Rows.Count>0)
                    {
                        foreach(DataRow drCheck in dtPallet.Rows)
                        {
                            strCurrentMatnr = drCheck["MATNR"].ToString();
                            strCurrentCharg = drCheck["CHARG"].ToString();
                            strCurrentMblnr = drCheck["MBLNR"].ToString();

                            #region 判断当前储位上是否存在同料号同版本不同PalletID的库存
                            if (objStorageData.ExistSameMatnrDifferentPallet(strCurrentMatnr, strCurrentCharg, strLocat, strCurrentMblnr))
                            {
                                MessageBox.Show("该料号" + strCurrentMatnr + "在当前储位已存在另一Pallet，如需更换储位，请输入OK回车");
                                failSound();
                                txtCode.Text = string.Empty;
                                //txtCode.Enabled = false;
                                //txtLocat.Enabled = true;
                                return;
                            }
                            #endregion

                            #region 判断已经确定的入库数据是否存在同料号同版本不同的PalletID
                            try
                            {
                                List<string> lsCheck = (from pallet in dtInStock.AsEnumerable()
                                                        where (pallet.Field<string>("LOCAT") == strLocat && pallet.Field<string>("MATNR") == strCurrentMatnr && pallet.Field<string>("CHARG") == strCurrentCharg)
                                                        select pallet.Field<string>("MBLNR"))
                                                    .Distinct().ToList();
                                if (lsCheck.Count() > 0 && !lsCheck.Contains(strCurrentMblnr))
                                {
                                    MessageBox.Show("该料号" + strCurrentMatnr + "在当前储位已存在另一Pallet，如需更换储位，请输入OK回车");
                                    failSound();
                                    txtCode.Text = string.Empty;
                                    //txtCode.Enabled = false;
                                    //txtLocat.Enabled = true;
                                    return;
                                }
                            }
                            catch(Exception ex)
                            {
                                return;
                            }

                            #endregion

                            #region PCB导Batch判断当前储位上是否存在同料号不同版本的库存
                            if (objStorageData.ExistSameMatnrDifferentCharg(strCurrentMatnr, strCurrentCharg, strLocat, strCurrentMblnr))
                            {
                                MessageBox.Show("该料号" + strCurrentMatnr + "在当前储位版本不同，如需更换储位，请输入OK回车");
                                failSound();
                                txtCode.Text = string.Empty;
                                return;
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        stsWarning.Text = "无此PalletID/BoxID数据";
                        failSound();
                        txtCode.Text = string.Empty;
                        return;
                    }

                    if (lsMblnr.Contains(strPallet))
                    {
                        MessageBox.Show("当前Pallet/BoxID数据已刷入");
                        failSound();
                        txtCode.Text = string.Empty;
                        return;
                    }
                    else
                    {
                        lsMblnr.Add(strPallet);
                        successSound();
                    }

                    #region 更新中间表PAL_DETAIL信息
                    objTransfer.UpdatePalDetailData(strWerks, strLgort, strLocat, strPallet, string.Empty);
                    #endregion

                    DataTable dtPalletConfirm = dtInStock.Clone();
                    foreach(DataRow dr in dtPallet.Rows)
                    {
                        DataRow drPalletConfirm = dtPalletConfirm.NewRow();
                        drPalletConfirm["MANDT"] = strMandt;
                        drPalletConfirm["COMCD"] = strComcd;
                        drPalletConfirm["WERKS"] = strWerks;
                        drPalletConfirm["LGORT"] = strLgort;
                        drPalletConfirm["PNUM"] = strMblnr;//49单号
                        drPalletConfirm["MBLNR"] = dr["MBLNR"].ToString();//PalletID
                        drPalletConfirm["MATNR"] = dr["MATNR"].ToString();
                        drPalletConfirm["CHARG"] = dr["CHARG"].ToString();
                        drPalletConfirm["INSMK"] = dr["INSMK"].ToString();
                        drPalletConfirm["LOCAT"] = strLocat;
                        drPalletConfirm["MENGE"] = dr["MENGE"].ToString();
                        drPalletConfirm["ALQTY"] = 0;
                        drPalletConfirm["LIFNR"] = string.Empty;
                        drPalletConfirm["DACOD"] = string.Empty;
                        drPalletConfirm["LOCOD"] = string.Empty;
                        drPalletConfirm["VEDAT"] = string.Empty;
                        drPalletConfirm["INDAT"] = DateTime.Now.ToString("yyyyMMdd");
                        drPalletConfirm["RMAK1"] = dr["BOXID"].ToString();
                        drPalletConfirm["MRGID"] = string.Empty;
                        drPalletConfirm["ARBPL"] = string.Empty;
                        drPalletConfirm["KOSTL"] = string.Empty;
                        drPalletConfirm["ExpiryDate"] = string.Empty;
                        drPalletConfirm["TASKID"] = string.Empty;
                        drPalletConfirm["SERNO"] = string.Empty;
                        dtPalletConfirm.Rows.Add(drPalletConfirm);
                    }
                    dtInStock.Merge(dtPalletConfirm);
                    ShowScanDataGridView();
                }
                txtCode.Text = string.Empty;

            }
        }

        //刷入数据填充
        public void SetScanTableToInstock(DataRow drScandata)
        {
            DataRow drScanRows = dtInStock.NewRow();
            drScanRows["MANDT"] = strMandt;
            drScanRows["COMCD"] = strComcd;
            drScanRows["WERKS"] = strWerks;
            drScanRows["LGORT"] = strLgort;
            drScanRows["PNUM"] = strMblnr;
            drScanRows["MBLNR"] = string.Empty;//记录扣账sap返回单号，扣账后记录
            drScanRows["MATNR"] = drScandata["MATNR"].ToString();
            drScanRows["CHARG"] = drScandata["CHARG"].ToString();
            drScanRows["INSMK"] = "G";
            drScanRows["LOCAT"] = drScandata["LOCAT"].ToString();
            drScanRows["MENGE"] = drScandata["MENGE"].ToString();
            drScanRows["ALQTY"] = 0;
            drScanRows["LIFNR"] = drScandata["LIFNR"].ToString();
            drScanRows["DACOD"] = drScandata["DACOD"].ToString();
            drScanRows["LOCOD"] = drScandata["LOCOD"].ToString();
            drScanRows["VEDAT"] = drScandata["VEDAT"].ToString();
            drScanRows["INDAT"] = DateTime.Now.ToString("yyyyMMdd");
            drScanRows["RMAK1"] = "StorageIn_101";
            drScanRows["MRGID"] = string.Empty;
            drScanRows["ARBPL"] = string.Empty;
            drScanRows["KOSTL"] = string.Empty;
            drScanRows["ExpiryDate"] = drScandata["ExpiryDate"].ToString();
            drScanRows["TASKID"] = drScandata["TASKID"].ToString();
            drScanRows["SERNO"] = drScandata["SERNO"].ToString();
            dtInStock.Rows.Add(drScanRows);
        }

        public void SetErrNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
            sp.Play();
            txtCode.Focus();
            txtCode.Text = "";
        }

        #region ShowDataGrid
        private void ShowMblnrDataGridView()
        {
            dgvMblnrData.AutoGenerateColumns = false;
            dgvMblnrData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcDWERKS = new DataGridViewTextBoxColumn();
                dgvcDWERKS.DataPropertyName = "DWERKS";
                dgvcDWERKS.HeaderText = "WERKS";
                dgvcDWERKS.Name = "DWERKS";
                dgvcDWERKS.Width = 50;
                dgvcDWERKS.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcDWERKS);

                DataGridViewTextBoxColumn dgvcDLGORT = new DataGridViewTextBoxColumn();
                dgvcDLGORT.DataPropertyName = "DLGORT";
                dgvcDLGORT.HeaderText = "LGORT";
                dgvcDLGORT.Name = "DLGORT";
                dgvcDLGORT.Width = 50;
                dgvcDLGORT.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcDLGORT);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "调拨单号";
                dgvcMBLNR.Name = "MBLNR";
                dgvcMBLNR.Width = 90;
                dgvcMBLNR.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcMBLNR);

                //LIFNR
                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "调拨Item";
                dgvcZEILE.Name = "ZEILE";
                dgvcZEILE.Width = 50;
                dgvcZEILE.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcZEILE);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Name = "MATNR";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.Name = "CHARG";
                dgvcCHARG.Width = 50;
                dgvcCHARG.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "LIFNR";
                dgvcLIFNR.Name = "LIFNR";
                dgvcLIFNR.Width = 50;
                dgvcLIFNR.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.Name = "MENGE";
                dgvcMENGE.Width = 50;
                dgvMblnrData.Columns.Add(dgvcMENGE);

                //lblMblnrCount.Text = dtTransData.Rows.Count + " Records";
                dgvMblnrData.DataSource = dtTransData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowMblnrDataGridView()");
            }
        }

        private void ShowOtherMblnrDataGridView(DataTable dtTransData)
        {
            dgvMblnrData.AutoGenerateColumns = false;
            dgvMblnrData.Columns.Clear();
            try
            {
                DatagridViewCheckBoxHeaderCell chkcell = new DatagridViewCheckBoxHeaderCell();
                chkcell.OnCheckBoxClicked += new CheckBoxClickedHandler(chkcell_OnCheckBoxClicked);
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderCell = chkcell;
                chk.DataPropertyName = "cSelect";
                chk.HeaderText = "";
                chk.Width = 30;
                dgvMblnrData.Columns.Add(chk);
                dgvMblnrData.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

                DataGridViewTextBoxColumn dgvcDWERKS = new DataGridViewTextBoxColumn();
                dgvcDWERKS.DataPropertyName = "DWERKS";
                dgvcDWERKS.HeaderText = "WERKS";
                dgvcDWERKS.Name = "DWERKS";
                dgvcDWERKS.Width = 50;
                dgvcDWERKS.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcDWERKS);

                DataGridViewTextBoxColumn dgvcDLGORT = new DataGridViewTextBoxColumn();
                dgvcDLGORT.DataPropertyName = "DLGORT";
                dgvcDLGORT.HeaderText = "LGORT";
                dgvcDLGORT.Name = "DLGORT";
                dgvcDLGORT.Width = 50;
                dgvcDLGORT.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcDLGORT);

                DataGridViewTextBoxColumn dgvcDLOCAT = new DataGridViewTextBoxColumn();
                dgvcDLOCAT.DataPropertyName = "DLOCAT";
                dgvcDLOCAT.HeaderText = "LOCAT";
                dgvcDLOCAT.Name = "LOCAT";
                dgvcDLOCAT.Width = 50;
                dgvcDLOCAT.ReadOnly = false;
                dgvMblnrData.Columns.Add(dgvcDLOCAT);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "调拨单号";
                dgvcMBLNR.Name = "MBLNR";
                dgvcMBLNR.Width = 90;
                dgvcMBLNR.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcMBLNR);

                //LIFNR
                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "调拨Item";
                dgvcZEILE.Name = "ZEILE";
                dgvcZEILE.Width = 50;
                dgvcZEILE.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcZEILE);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Name = "MATNR";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.Name = "CHARG";
                dgvcCHARG.Width = 50;
                dgvcCHARG.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "LIFNR";
                dgvcLIFNR.Name = "LIFNR";
                dgvcLIFNR.Width = 50;
                dgvcLIFNR.ReadOnly = true;
                dgvMblnrData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.Name = "MENGE";
                dgvcMENGE.Width = 50;
                dgvMblnrData.Columns.Add(dgvcMENGE);

                //lblMblnrCount.Text = dtTransData.Rows.Count + " Records";
                dgvMblnrData.DataSource = dtTransData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowMblnrDataGridView()");
            }
        }

        private void ShowScanDataGridView()
        {
            dgvInStockData.AutoGenerateColumns = false;
            dgvInStockData.Columns.Clear();
            try
            {
                ////笔数
                DataGridViewTextBoxColumn dgvcCount = new DataGridViewTextBoxColumn();
                dgvcCount.DataPropertyName = "Item";
                dgvcCount.HeaderText = "Item";
                dgvcCount.ReadOnly = true;
                dgvcCount.Width = 40;
                dgvInStockData.Columns.Add(dgvcCount);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "LOCAT";
                dgvcLOCAT.Name = "LOCAT";
                dgvcLOCAT.Width = 50;
                dgvcLOCAT.ReadOnly = true;
                dgvInStockData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcDLGORT = new DataGridViewTextBoxColumn();
                dgvcDLGORT.DataPropertyName = "LGORT";
                dgvcDLGORT.HeaderText = "LGORT";
                dgvcDLGORT.Name = "LGORT";
                dgvcDLGORT.Width = 50;
                dgvcDLGORT.ReadOnly = true;
                dgvInStockData.Columns.Add(dgvcDLGORT);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "入库单号";
                dgvcMBLNR.Name = "MBLNR";
                dgvcMBLNR.Width = 270;
                dgvcMBLNR.ReadOnly = true;
                dgvInStockData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "RMAK1";
                dgvcBOXID.HeaderText = "BOXID";
                dgvcBOXID.Name = "BOXID";
                dgvcBOXID.Width = 270;
                dgvcBOXID.ReadOnly = true;
                dgvInStockData.Columns.Add(dgvcBOXID);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Name = "MATNR";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                dgvInStockData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.Name = "CHARG";
                dgvcCHARG.Width = 50;
                dgvcCHARG.ReadOnly = true;
                dgvInStockData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "LIFNR";
                dgvcLIFNR.Name = "LIFNR";
                dgvcLIFNR.Width = 50;
                dgvcLIFNR.ReadOnly = true;
                dgvInStockData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.Name = "MENGE";
                dgvcMENGE.Width = 50;
                //dgvcMENGE.ReadOnly = true;
                dgvInStockData.Columns.Add(dgvcMENGE);

                if (dtInStock.Columns.IndexOf("Item") == -1)
                {
                    dtInStock.Columns.Add("Item");
                }

                for (int i = 0; i < dtInStock.Rows.Count; i++)
                {
                    dtInStock.Rows[i]["Item"] = i + 1;
                }
                //lblStorageDataCount.Text = dtInStock.Rows.Count + " Records";
                dgvInStockData.DataSource = dtInStock;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowScanDataGridView()");
            }
        }

        private void ShowScanDCDataGridView(DataTable dtScanData)
        {
            dgvInStockData.AutoGenerateColumns = false;
            dgvInStockData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 80;
                dgvInStockData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvInStockData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 50;
                dgvInStockData.Columns.Add(dgvcCharg);


                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 50;
                dgvInStockData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 50;
                dgvInStockData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "LockCode";
                dgvcLocod.ReadOnly = true;
                dgvcLocod.Width = 100;
                dgvInStockData.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 100;
                dgvInStockData.Columns.Add(dgvcDacod);


                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.HeaderText = "Vendor Manufactured Date";
                dgvcVedat.ReadOnly = true;
                dgvcVedat.Width = 100;
                dgvInStockData.Columns.Add(dgvcVedat);

                DataGridViewTextBoxColumn dgvcExpiryDate = new DataGridViewTextBoxColumn();
                dgvcExpiryDate.DataPropertyName = "ExpiryDate";
                dgvcExpiryDate.HeaderText = "Exp Date";
                dgvcExpiryDate.ReadOnly = true;
                dgvcExpiryDate.Width = 100;
                dgvInStockData.Columns.Add(dgvcExpiryDate);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "TASKID";
                dgvcTASKID.ReadOnly = true;
                dgvcTASKID.Width = 100;
                dgvInStockData.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                dgvcSERNO.DataPropertyName = "SERNO";
                dgvcSERNO.HeaderText = "SERNO";
                dgvcSERNO.ReadOnly = true;
                dgvcSERNO.Width = 100;
                dgvInStockData.Columns.Add(dgvcSERNO);

                dgvInStockData.DataSource = dtScanData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowScanDataGridView()");
            }
        }
        #endregion

        private DataSet SendToSap(DataSet ds)
        {
            try
            {
                DataSet dsResult = new DataSet();
                MM.MM_Service obj = new QWMS.MM.MM_Service();
                //MM_Test.MM_Service obj = new MM_Test.MM_Service();
                dsResult = obj.Z_MM_RFC_DIAOBO_QWMS_TO_SAP("101", ds);
                return dsResult;
            }
            catch (Exception ex)
            {
                failSound();
                MessageBox.Show("扣账异常");
                throw new Exception(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            string strSap = string.Empty;
            StorageIn objStorageIn = new StorageIn(UserData, strProgid);
            if (strCheckType == "Tranfer101")
            {
                #region SAP扣账
                DataTable dtSap = objTransfer.GetSapData(strMblnr, "101");
                stsWarning.Text = "扣账中,请稍等。";
                if (dtSap.Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    DataTable dscopy = dtSap.Copy();
                    ds.Tables.Add(dscopy);
                    DataSet dsrec = SendToSap(ds);
                    objTransfer.UpdateTransInRemark(dsrec, strMblnr);
                    if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "Y")
                    {
                        stsWarning.Text = strMblnr + "扣账成功:" + dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString();
                        MessageBox.Show(stsWarning.Text.ToString());
                        strSap = dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString();
                        if (objPlantData.CheckLOCODLGORT(strWerks, strLgort) && objPlantData.CheckDACODLGORT(strWerks, strLgort) && strWerks == "CS42")
                        {
                            objStorageIn.UpdateTransNo(strMblnr, strSap);
                            for (int i = 0; i<dtInStock.Rows.Count; i++)
                            {
                                dtInStock.Rows[i]["MBLNR"] = strSap;
                            }
                        }
                    }
                    else if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "N")
                    {
                        stsWarning.Text = strMblnr + "扣账失败:" + dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString();
                        MessageBox.Show(stsWarning.Text.ToString());
                        return;
                    }
                }
                #endregion

                #region QWMS非MLB入库
                if (rdoOthers.Checked && !string.IsNullOrEmpty(strSap)&&!string.IsNullOrEmpty(strLocat) && !objPlantData.CheckLOCODLGORT(strWerks, strLgort) && !objPlantData.CheckDACODLGORT(strWerks, strLgort) && strWerks != "CS42")
                {
                    dtInStock.Rows.Clear();
                    DataTable dtPalletConfirm = dtInStock.Clone();
                    if (rdoOthers.Checked)
                    {
                        dtTransData = (DataTable)dgvMblnrData.DataSource;
                    }
                    foreach (DataRow dr in dtTransData.Rows)
                    {
                        DataRow drPalletConfirm = dtPalletConfirm.NewRow();
                        drPalletConfirm["MANDT"] = strMandt;
                        drPalletConfirm["COMCD"] = strComcd;
                        drPalletConfirm["WERKS"] = strWerks;
                        drPalletConfirm["LGORT"] = strLgort;
                        drPalletConfirm["PNUM"] = strMblnr;//49单号
                        drPalletConfirm["MBLNR"] = strSap;//普通调拨记录101单号，MLB记录PalletID
                        drPalletConfirm["MATNR"] = dr["MATNR"].ToString();
                        drPalletConfirm["CHARG"] = dr["CHARG"].ToString();
                        drPalletConfirm["INSMK"] = "G";
                        if (rdoOthers.Checked)
                        {
                            drPalletConfirm["LOCAT"] = dr["DLOCAT"].ToString();
                        }
                        else
                        {
                            drPalletConfirm["LOCAT"] = strLocat;
                        }
                        drPalletConfirm["MENGE"] = dr["MENGE"].ToString();
                        drPalletConfirm["ALQTY"] = 0;
                        drPalletConfirm["LIFNR"] = string.Empty;
                        drPalletConfirm["DACOD"] = string.Empty;
                        drPalletConfirm["LOCOD"] = string.Empty;
                        drPalletConfirm["VEDAT"] = string.Empty;
                        drPalletConfirm["INDAT"] = DateTime.Now.ToString("yyyyMMdd");
                        drPalletConfirm["RMAK1"] = string.Empty;
                        drPalletConfirm["MRGID"] = string.Empty;
                        drPalletConfirm["ARBPL"] = string.Empty;
                        drPalletConfirm["KOSTL"] = string.Empty;
                        drPalletConfirm["ExpiryDate"] = string.Empty;
                        drPalletConfirm["TASKID"] = string.Empty;
                        drPalletConfirm["SERNO"] = string.Empty;
                        dtPalletConfirm.Rows.Add(drPalletConfirm);
                    }
                    dtInStock.Merge(dtPalletConfirm);
                }
                #endregion

                GetStorageCombine();
            }
            else if (strCheckType == "OnlyQWMS")
            {
                //  QWMS入库
                GetStorageCombine();
            }
        }

        private void GetStorageCombine()
        {
            if (dtInStock.Rows.Count == 0)
                return;

            dtCombine.Rows.Clear();

            #region 整合入库数据dtInStock为dtConfirm
            var query = from row in dtInStock.AsEnumerable()
                        group row by
                        new
                        {
                            r1 = row.Field<string>("MANDT"),
                            r2 = row.Field<string>("COMCD"),
                            r3 = row.Field<string>("WERKS"),
                            r4 = row.Field<string>("LGORT"),
                            r5 = row.Field<string>("PNUM"),
                            r6 = row.Field<string>("MBLNR"),
                            r7 = row.Field<string>("MATNR"),
                            r8 = row.Field<string>("CHARG"),
                            r9  = row.Field<string>("INSMK"),
                            r10 = row.Field<string>("LOCAT"),
                            r11 = row.Field<string>("LIFNR"),
                            r12 = row.Field<string>("DACOD"),
                            r13 = row.Field<string>("LOCOD"),
                            r14 = row.Field<string>("VEDAT"),
                            r15 = row.Field<string>("INDAT"),
                            r16 = row.Field<string>("MRGID"),
                            r17 = row.Field<string>("ARBPL"),
                            r18 = row.Field<string>("KOSTL"),
                            r19 = row.Field<string>("ExpiryDate"),
                            r20 = row.Field<string>("TASKID"),
                            r21 = row.Field<string>("SERNO"),
                        } into m

                        select new
                        {

                            MANDT = m.Key.r1,
                            COMCD = m.Key.r2,
                            WERKS = m.Key.r3,
                            LGORT = m.Key.r4,
                            PNUM = m.Key.r5,
                            MBLNR = m.Key.r6,
                            MATNR = m.Key.r7,
                            CHARG = m.Key.r8,
                            INSMK = m.Key.r9,
                            LOCAT = m.Key.r10,
                            MENGE = m.Sum(n => n.Field<int>("MENGE")),
                            ALQTY = m.Sum(n => n.Field<int>("MENGE")),
                            LIFNR = m.Key.r11,
                            DACOD = m.Key.r12,
                            LOCOD = m.Key.r13,
                            VEDAT = m.Key.r14,
                            INDAT = m.Key.r15,
                            MRGID = m.Key.r16,
                            ARBPL = m.Key.r17,
                            KOSTL = m.Key.r18,
                            ExpiryDate = m.Key.r19,
                            TASKID = m.Key.r20,
                            SERNO = m.Key.r21,
                        };
            foreach (var item in query)
            {
                DataRow drCombine = dtCombine.NewRow();
                drCombine["MANDT"] = item.MANDT;
                drCombine["COMCD"] = item.COMCD;
                drCombine["WERKS"] = item.WERKS;
                drCombine["LGORT"] = item.LGORT;
                drCombine["PNUM"] = item.PNUM;
                drCombine["MBLNR"] = item.MBLNR;
                drCombine["MATNR"] = item.MATNR;
                drCombine["CHARG"] = item.CHARG;
                drCombine["INSMK"] = item.INSMK;
                drCombine["LOCAT"] = item.LOCAT;
                drCombine["MENGE"] = item.MENGE;
                drCombine["ALQTY"] = item.ALQTY;
                drCombine["LIFNR"] = item.LIFNR;
                drCombine["DACOD"] = item.DACOD;
                drCombine["LOCOD"] = item.LOCOD;
                drCombine["VEDAT"] = item.VEDAT;
                drCombine["INDAT"] = item.INDAT;
                drCombine["RMAK1"] = "StorageIn_101";
                drCombine["MRGID"] = item.MRGID;
                drCombine["ARBPL"] = item.ARBPL;
                drCombine["KOSTL"] = item.KOSTL;
                drCombine["ExpiryDate"] = item.ExpiryDate;
                drCombine["TASKID"] = item.TASKID;
                drCombine["SERNO"] = item.SERNO;
                dtCombine.Rows.Add(drCombine.ItemArray);
            }
            #endregion

            StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, strProgid);
            LogData objLogData = new LogData(UserData, strWerks, strLgort, strProgid);
            if (objStorageIn.StorageInWHEC(dtCombine))
            {
                successSound();
                stsWarning.Text = strMblnr + "：入库QWMS成功！";
                btnSave.Enabled = false;
                if (objStorageIn.UpdateEcInStock(strMblnr))
                {
                    stsWarning.Text = strMblnr + "：入库QWMS成功！更新入库数据成功！";

                    #region 增加和ASRS接口

                    QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);

                    if (objInterface.CheckLGORT(strWerks, strLgort)&&!string.IsNullOrEmpty(strLocat))//判断是否为ASRS仓别
                    {

                        DataTable dtASRS = new DataTable();
                        dtASRS.TableName = "QWMS";

                        dtASRS.Columns.Add("TRN_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("SEQ_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("TRN_TYPE", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("LOC", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("ITEM_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("STK", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("VER", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("VENDOR", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("QTY", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PUR_TYPE", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PO_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PLANT", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PRIORITY", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("STORAGE_TYPE", typeof(string)).DefaultValue = string.Empty;

                        int j = 1;

                        string strTRN_NO = objInterface.CreateAsrsNo();

                        foreach (DataRow dr in dtCombine.Rows)
                        {
                            DataRow drASRS = dtASRS.NewRow();

                            drASRS["TRN_NO"] = strWerks.Equals("CS41") && (strLgort.Equals("AS10") || strLgort.Equals("TW90")) && dr["INSMK"].ToString().Equals("J") ? dr["MBLNR"].ToString().Substring(0, 2) + dr["MBLNR"].ToString().Substring(4, dr["MBLNR"].ToString().Length - 4) : strTRN_NO;
                            drASRS["SEQ_NO"] = objInterface.Createseq_no(j);
                            drASRS["TRN_TYPE"] = "G+";
                            drASRS["LOC"] = dr["LOCAT"];
                            drASRS["ITEM_NO"] = dr["MATNR"];
                            drASRS["STK"] = dr["INSMK"];
                            drASRS["VER"] = dr["CHARG"];
                            drASRS["VENDOR"] = dr["LIFNR"];
                            drASRS["QTY"] = dr["ALQTY"];
                            drASRS["PUR_TYPE"] = string.Empty;
                            drASRS["PO_NO"] = string.Empty;
                            drASRS["PLANT"] = dr["WERKS"];
                            drASRS["PRIORITY"] = string.Empty;
                            drASRS["STORAGE_TYPE"] = dr["LGORT"];

                            dtASRS.Rows.Add(drASRS);
                            j++;
                        }
                        //bool bolresult = objInterface.PostStorageInData(dtASRS) == "SUCCESS" ? true : false;
                        string strXML = objInterface.ConvertDataTableToXML(dtASRS);
                        objLogData.XMLLog(strXML);
                        if (objInterface.PostStorageInData(strXML) == "SUCCESS" ? true : false)
                        {
                            MessageBox.Show("Add OK!!,数据已同步到ASRS");
                        }
                    }

                    #endregion
                }
                else
                {
                    stsWarning.Text = strMblnr + "：入库QWMS成功！更新入库数据失败！";
                }
            }
            else
            {
                failSound();
                stsWarning.Text = strMblnr + "：入库QWMS失败！";
            }
        }

        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyChar == (char)13)
            {
                if (objPlantData.CheckLOCODLGORT(strWerks, strLgort) && objPlantData.CheckDACODLGORT(strWerks, strLgort) && strWerks == "CS42")
                {
                    if (string.IsNullOrEmpty(txtLocat.Text.ToString()))
                    {
                        failSound();
                        stsWarning.Text = "储位不能为空";
                        return;
                    }
                }
                #region 判断厂区仓别储位是否存在
                if (!objPlantData.CheckExistedStorageData(strWerks,strLgort,txtLocat.Text.ToString()))
                {
                    failSound();
                    stsWarning.Text="当前储位不存在，请重新输入储位";
                    txtLocat.Text=string.Empty;
                    return;
                }
                #endregion

                strLocat = txtLocat.Text.ToUpper().ToString();

                if (objPlantData.CheckLOCODLGORT(strWerks, strLgort) && objPlantData.CheckDACODLGORT(strWerks, strLgort) && strWerks == "CS42" && rdoOthers.Checked)
                {
                    txtCode.Enabled = true;
                    btnSave.Enabled = false;
                    txtCode.Focus();
                }
                else
                {
                    for (int i=0; i < dgvMblnrData.Rows.Count-1; i++)
                    {
                        dgvMblnrData.Rows[i].Cells[3].Value = strLocat;
                    }
                    txtCode.Enabled = rdoOthers.Checked ? false : true;
                    btnSave.Enabled = rdoOthers.Checked ? true : false;
                }
                txtLocat.Enabled = false;
                txtLgort.Enabled = false;
                cmbasrs.Enabled = false;
                successSound();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            lblWerks.Text = string.Empty;
            txtLgort.Text = string.Empty;
            txtMblnr.Enabled = true;
            txtMblnr.Text = string.Empty;
            txtLocat.Text = string.Empty;
            strWerks = string.Empty;
            strLgort = string.Empty;
            txtLocat.Enabled = false;
            txtCode.Enabled = false;
            txtCode.Text = string.Empty;;
            txtLgort.Enabled = true;
            btnConfirm.Enabled = true;
            btnSave.Enabled = false;
            lsMblnr.Clear();
            dtInStock.Rows.Clear();
            dtScanTable.Rows.Clear();
            dtCombine.Rows.Clear();
            dtTransData.Rows.Clear();
            dgvMblnrData.DataSource = null;
            dgvInStockData.DataSource = null;
            splitContainer4.Panel1.Enabled = true;
            rdoMLB.Checked = false;
            rdoOthers.Checked = false;
            rdoNormal.Checked = true;
            rdoQWMS.Checked = false;
            strCheckType = "Tranfer101";
            lblasrs.Visible = false;
            cmbasrs.Visible = false;
            rdoMLB.Checked = true;
            rdoMLB.Enabled = true;
            rdoOthers.Enabled = true;
            rdoNormal.Enabled = true;
            rdoQWMS.Enabled = true;
            cmbasrs.Enabled = true;
            lblArea.Visible = false;
            cmbArea.Visible = false;
            ScanMenge = 0;
            MblnrMenge = 0;
        }

        private void InitData()
        {
            if (dtInStock.Columns.Count == 0)
            {
                dtInStock.Columns.Add("MANDT");
                dtInStock.Columns.Add("COMCD");
                dtInStock.Columns.Add("WERKS");
                dtInStock.Columns.Add("LGORT");
                dtInStock.Columns.Add("PNUM");
                dtInStock.Columns.Add("MBLNR");
                dtInStock.Columns.Add("MATNR");
                dtInStock.Columns.Add("CHARG");
                dtInStock.Columns.Add("INSMK");
                dtInStock.Columns.Add("LOCAT");
                dtInStock.Columns.Add("MENGE", typeof(int));
                dtInStock.Columns.Add("ALQTY", typeof(int));
                dtInStock.Columns.Add("LIFNR");
                dtInStock.Columns.Add("DACOD");
                dtInStock.Columns.Add("LOCOD");
                dtInStock.Columns.Add("VEDAT");
                dtInStock.Columns.Add("INDAT");
                dtInStock.Columns.Add("RMAK1");
                dtInStock.Columns.Add("MRGID");
                dtInStock.Columns.Add("ARBPL");
                dtInStock.Columns.Add("KOSTL");
                //新增字段
                dtInStock.Columns.Add("ExpiryDate");
                dtInStock.Columns.Add("TASKID");
                dtInStock.Columns.Add("SERNO");
            }
            if (dtCombine.Columns.Count == 0)
            {
                dtCombine = dtInStock.Clone();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            StorageIn objStorageIn = new StorageIn(UserData, strProgid);

            #region 将入库数据dtInStock存放在EC_INSTOCK表中
            if (dtInStock.Rows.Count > 0)
            {
                if (!objStorageIn.DeleteECInStore(strMblnr))
                {
                    stsWarning.Text = "入库数据未删除成功，请联系MIS";
                    failSound();
                    return;
                }
                for (int i = 0; i < dtInStock.Rows.Count; i++)
                {
                    dtInStock.Rows[i]["ALQTY"] = dtInStock.Rows[i]["MENGE"].ToString();
                }
                if (!objStorageIn.ECInStock_Trans101(dtInStock))
                {
                    stsWarning.Text = "入库数据未保存成功，请联系MIS";
                    failSound();
                    return;
                }
            }
            else
            {
                failSound();
                stsWarning.Text = "无入库数据";
            }
            #endregion
                
            DataTable dtCheck = objTransfer.CheckTransfer101ScanQty(strMblnr);
            if (!dtCheck.Rows[0][0].ToString().Equals("0"))
            {
                failSound();
                stsWarning.Text = "调拨单料号数量和刷入料号数量存在不一致情况";
                btnSave.Enabled = false;
                btnConfirm.Enabled = true;
                return;
            }
            else
            {
                successSound();
                stsWarning.Text = "匹配成功";
                btnSave.Enabled = true;
                btnConfirm.Enabled = false;
            }
         }

        private DataTable CombineDataTableByMatnr(DataTable dtData)
        {
            DataTable dtResult = new DataTable();
            try
            {
                dtResult.Columns.Add("MATNR");
                dtResult.Columns.Add("CHARG");
                dtResult.Columns.Add("MENGE");

                var query = from row in dtData.AsEnumerable()
                            group row by row.Field<string>("MATNR") into m
                            select new
                            {
                                MATNR = m.Key,
                                LIFNR = m.FirstOrDefault().Field<string>("CHARG"),
                                MENGE = m.Sum(n => n.Field<decimal>("MENGE"))
                            };
                foreach (var item in query)
                {
                    DataRow drResult = dtResult.NewRow();
                    drResult["MATNR"] = item.MATNR;
                    drResult["CHARG"] = item.LIFNR;
                    drResult["MENGE"] = item.MENGE;
                    dtResult.Rows.Add(drResult.ItemArray);
                }
            }
            catch (Exception e)
            {
                failSound();
                stsWarning.Text = dtData.TableName + "Table转化异常";
            }

            return dtResult;
        }

        private void dgvInStockData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            stsWarning.Text = string.Empty;
            string strChangePallet = dgvInStockData.Rows[e.RowIndex].Cells["MBLNR"].Value.ToString();
            string strChangeBoxID = dgvInStockData.Rows[e.RowIndex].Cells["BOXID"].Value.ToString();
            string strChangeMatnr = dgvInStockData.Rows[e.RowIndex].Cells["MATNR"].Value.ToString();
            string strChangeCharg = dgvInStockData.Rows[e.RowIndex].Cells["CHARG"].Value.ToString();
            int intMenge = int.Parse(dgvInStockData.Rows[e.RowIndex].Cells["MENGE"].Value.ToString());
            if (objTransfer.UpdatePalletBOXIDNum(strChangePallet, strChangeBoxID, strChangeMatnr, strChangeCharg, intMenge, "ChangeMenge"))
            {
                successSound();
                stsWarning.Text = "更新成功";
                dtInStock = dgvInStockData.DataSource as DataTable;
                ShowScanDataGridView();
            }
        }

        private void txtLgort_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyChar == (char)13)
            {
                LgortKeyChar();
            }
        }

        #region successSound 成功声音
        private void successSound()
        {
            try
            {
                SoundPlayer sp = new SoundPlayer(@"Sound\OK1.wav");
                sp.Play();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }

        #endregion

        #region  输入仓别回车动作执行方法
        private void LgortKeyChar()
        {
            if (strWerks != "" && txtLgort.Text.ToString().Trim() != "")
            {
                strLgort = txtLgort.Text.ToString().Trim().ToUpper();
                if (objAuthority.CheckLgortAuthority(strWerks, strLgort)) //用户是否有该厂区仓别的权限
                {
                    if (objTransfer.UpdateTransfer101Lgort(strFWerks, strFLgort, strMblnr, strLgort))
                    {
                        //dtTransData = objTransfer.GetTransferNo(strMblnr, string.Empty);
                        foreach(DataRow dr in dtTransData.Rows)
                        {
                            dr["DLGORT"] = strLgort;
                        }
                        stsWarning.Text = "仓别设定成功";
                        txtLocat.Enabled = true;
                        txtLocat.Focus();
                        lblasrs.Visible = false;
                        cmbasrs.Visible = false;

                        if(strWerks.Equals("CS41"))
                        {
                            #region ASRS储位设定
                            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                            DataTable dtAsrsLgort = objPlantData.GetAsrsLgort();
                            for (int i = 0; i < dtAsrsLgort.Rows.Count; i++)
                            {
                                if (dtAsrsLgort.Rows[i]["LGORT"].ToString().Trim() == strLgort)
                                {
                                    lblasrs.Visible = true;
                                    cmbasrs.Visible = true;

                                    DataTable dtleft = objPlantData.GetAsrsLeft(strWerks, strLgort);
                                    if (dtleft.Rows.Count == 0)
                                    {
                                        cmbasrs.Items.Clear();
                                    }
                                    else
                                    {
                                        cmbasrs.Items.Clear();
                                        cmbasrs.Items.Add("");
                                        for (int j = 0; j < dtleft.Rows.Count; j++)
                                        {
                                            cmbasrs.Items.Add(dtleft.Rows[j]["F_TEXT"].ToString());
                                        }
                                        cmbasrs.SelectedIndex = 0;
                                    }
                                }
                            }
                            #endregion
                        }

                        #region MLB调拨接收区域设定--确认MLB入库仓别都是TW30仓，如下为各厂厂区别:F4 (CS41)F5(CS51)F6(CS50)F7(CS31)
                        if ((strWerks.Equals("CS50") || strWerks.Equals("CS41") || strWerks.Equals("CS51") || strWerks.Equals("CS31")) && strLgort.Equals("TW30"))
                        {
                            lblArea.Visible = true;
                            cmbArea.Visible = true;
                        }
                        #endregion

                        if (objPlantData.CheckLOCODLGORT(strWerks, strLgort) && objPlantData.CheckDACODLGORT(strWerks, strLgort) && strWerks == "CS42")
                        {
                            GetDefaultScanTable();
                            btnConfirm.Enabled = true;
                        }

                        successSound();
                    }
                    else
                    {
                        stsWarning.Text = "仓别设定失败";
                        txtLgort.Enabled = true;
                        txtLgort.Focus();
                        txtLgort.SelectAll();
                        failSound();
                    }
                }
                else
                {
                    failSound();
                    stsWarning.Text = "请检查厂区仓别是否正确且有权限！！";
                    txtLgort.Focus();
                    txtLgort.SelectAll();
                }

                if (rdoOthers.Checked && !objPlantData.CheckLOCODLGORT(strWerks, strLgort) && !objPlantData.CheckDACODLGORT(strWerks, strLgort) && strWerks != "CS42")
                {
                    ShowOtherMblnrDataGridView(dtTransData);
                }
                else
                {
                    ShowMblnrDataGridView();
                }
            }

        }

        #endregion

        #region failSound 失败声音
        private void failSound()
        {
            try
            {
                SoundPlayer sp = new SoundPlayer(@"Sound\NG.wav");
                sp.Play();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }

        #endregion

        #region 单选按钮
        private void rdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoNormal.Checked)
            {
                strCheckType = "Tranfer101";
                rdoNormal.Enabled = false;
                rdoQWMS.Enabled = false;
            }
        }

        private void rdoQWMS_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoQWMS.Checked)
            {
                strCheckType = "OnlyQWMS";
                rdoNormal.Enabled = false;
                rdoQWMS.Enabled = false;
            }
        }
        #endregion

        private void rdoMLB_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMLB.Checked)
            {
                strLgortType = "MLB";
                rdoQWMS.Enabled = true;
                rdoQWMS.Visible = true;
            }
        }

        private void rdoOthers_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoOthers.Checked)
            {
                //选择其他则默认是101入库，不可选择仅入QWMS
                strLgortType = "Others";
                strCheckType = "Tranfer101";
                rdoNormal.Checked = true;
                rdoQWMS.Checked = false;
                rdoQWMS.Visible = false;
            }
        }

        private void cmbasrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                string strasrs = "";
                if (cmbasrs.SelectedIndex != -1)
                {
                    strasrs = cmbasrs.Items[cmbasrs.SelectedIndex].ToString();

                    if (strWerks == "" || strLgort == "")
                    {
                        stsWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                    else
                    {
                        if (strasrs != "")
                        {
                            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                            DataTable dtasrsLocat = new DataTable();
                            dtasrsLocat = objPlantData.GetAsrsLocat(strWerks, strLgort, strasrs);
                            if (dtasrsLocat.Rows.Count > 0)
                            {
                                txtLocat.Text = dtasrsLocat.Rows[0]["LOCAT"].ToString().Trim();
                                txtLocat.Focus();
                            }
                            else
                            {
                                stsWarning.Text = "无空储位，请重新选择AsrsLocat！！";
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            //Weijun Chen: 确认MLB入库仓别都是TW30仓，F4 (CS41)F5(CS51)F6(CS50)F7(CS31)
            if ((strWerks.Equals("CS50") || strWerks.Equals("CS41") || strWerks.Equals("CS51") || strWerks.Equals("CS31")) && strLgort.Equals("TW30"))
            {
                string strArea = string.Empty;
                if (cmbArea.SelectedIndex != -1)
                {
                    strArea = cmbArea.Items[cmbArea.SelectedIndex].ToString();
                }
                else
                {
                    MessageBox.Show("请先选择区域");
                    return;
                }
                StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, strProgid, strWerks, strLgort, "NEW", strArea);
                objStorageIn_LocationSelect.ShowDialog();
                strLocat = objStorageIn_LocationSelect.Locat;
                txtLocat.Text = strLocat;

                txtCode.Enabled = rdoOthers.Checked ? false : true;
                btnSave.Enabled = rdoOthers.Checked ? true : false;
                txtLocat.Enabled = false;
                txtLgort.Enabled = false;
                successSound();
            }
        }

        #region 重绘单选框表头
        //定义继承于DataGridViewColumnHeaderCell的类，用于绘制checkbox，定义checkbox鼠标单击事件  
        public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
        {
            Point checkBoxLocation;
            Size checkBoxSize;
            bool _checked = false;
            Point _cellLocation = new Point();
            System.Windows.Forms.VisualStyles.CheckBoxState _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

            public event CheckBoxClickedHandler OnCheckBoxClicked;

            public DatagridViewCheckBoxHeaderCell()
            {

            }

            //绘制列头checkbox 
            protected override void Paint(System.Drawing.Graphics graphics,
                                          System.Drawing.Rectangle clipBounds,
                                          System.Drawing.Rectangle cellBounds,
                                          int rowIndex,
                                          DataGridViewElementStates dataGridViewElementState,
                                          object value,
                                          object formattedValue,
                                          string errorText,
                                          DataGridViewCellStyle cellStyle,
                                          DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                          DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value,
                           formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                Point p = new Point();

                Size s = CheckBoxRenderer.GetGlyphSize(graphics, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

                //列头checkbox的X坐标
                p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2) - 1;

                //列头checkbox的Y坐标
                p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2) - 1;

                _cellLocation = cellBounds.Location;
                checkBoxLocation = p;
                checkBoxSize = s;

                if (_checked)
                {
                    _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                }
                else
                {
                    _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                }

                CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, _cbState);
            }

            //点击列头checkbox单击事件
            protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
            {
                Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);

                if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                    && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
                {
                    _checked = !_checked;

                    if (OnCheckBoxClicked != null)
                    {
                        //触发单击事件
                        OnCheckBoxClicked(_checked);
                        this.DataGridView.InvalidateCell(this);
                    }

                }

                base.OnMouseClick(e);
            }

        }

        //定义触发单击事件的委托
        public delegate void CheckBoxClickedHandler(bool state);

        public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
        {
            bool isChecked;

            public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
            {
                isChecked = bChecked;
            }

            public bool Checked
            {
                get
                {
                    return isChecked;
                }
                set
                {
                    isChecked = value;
                }
            }
        }

        #endregion

        #region 点击单选框全选反选事件
        private void chkcell_OnCheckBoxClicked(bool isChecked)
        {
            if (isChecked == true)
            {
                dgvMblnrData.EndEdit();
                for (int i = 0; i < dgvMblnrData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvMblnrData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = true;
                }
            }
            else
            {
                dgvMblnrData.EndEdit();
                for (int i = 0; i < dgvMblnrData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvMblnrData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = false;
                }
            }
        }
        #endregion

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                if (strLocat == "")
                {
                    stsWarning.Text = "Location can't be empty!!";
                    return;
                }
                else
                {
                    int strcount = dgvMblnrData.SelectedRows.Count;
                    //DataRow[] dr = dtTransData.Select("cSelect = 'True' ");
                    if (strcount == 1)
                    {
                        int intSelectIndex = dgvMblnrData.CurrentRow.Index;
                        DataTable dtSplitAfterData = (DataTable)dgvMblnrData.DataSource;
                        DataTable dt = dtSplitAfterData.Clone();
                        dt.Rows.Add(dtSplitAfterData.Rows[intSelectIndex].ItemArray);
                        TransferIn_101_Split splitform = new TransferIn_101_Split(UserData, dt, strLgort, strWerks);
                        splitform.ShowDialog();

                        #region 显示分割后的结果
                        DataTable dtss = new DataTable();
                        splitform.getSplitDate(ref dtss);
                        if (dtss.Rows.Count > 0)
                        {
                            for (int x = 0; x < dtSplitAfterData.Rows.Count; x++)
                            {
                                if (dtSplitAfterData.Rows[x]["ZEILE"].ToString() == dt.Rows[0]["ZEILE"].ToString() && dtSplitAfterData.Rows[x]["MATNR"].ToString() == dt.Rows[0]["MATNR"].ToString() && dtSplitAfterData.Rows[x]["CHARG"].ToString() == dt.Rows[0]["CHARG"].ToString())
                                {
                                    dtSplitAfterData.Rows.RemoveAt(x);
                                }
                            }
                            for (int n = 0; n < dtss.Rows.Count; n++)
                            {
                                dtSplitAfterData.Rows.Add(dtss.Rows[n].ItemArray);
                            }
                            dtSplitAfterData = CommonInfo.SortDataTable(dtSplitAfterData, "ZEILE");
                            dgvMblnrData.DataSource = dtSplitAfterData;
                            ShowOtherMblnrDataGridView(dtSplitAfterData);
                            btnSave.Enabled = true;

                        }
                        #endregion

                    }
                    else if (strcount == 0)
                    {
                        stsWarning.Text = "请选择一行需要拆分的数据！";
                        return;
                    }
                    else
                    {
                        stsWarning.Text = "一次只能拆分一个扣帐编号！";
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

        #region 不用了 下拉改为刷入（Bill杨明朝）
        //private void ShowDdlLgort()
        //{
        //    try
        //    {
        //        stsWarning.Text = "";
        //        DataTable dtTemp = new DataTable();
        //        dtTemp = objAuthority.CheckLgortAuthority(strWerks);
        //        if (cmbLgort.SelectedIndex != -1)
        //        {
        //            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
        //        }
        //        else
        //        {
        //            cmbLgort.Items.Clear();
        //        }

        //        if (dtTemp.Rows.Count == 0)
        //        {
        //            cmbLgort.Items.Clear();
        //            strLgort = "";
        //        }
        //        else
        //        {
        //            cmbLgort.Items.Clear();
        //            for (int i = 0; i < dtTemp.Rows.Count; i++)
        //            {
        //                cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
        //                if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
        //                {
        //                    cmbLgort.SelectedIndex = i;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowDdlLgort()");
        //    }
        //}
        //private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    stsWarning.Text = string.Empty;
        //    if (cmbLgort.SelectedIndex > -1)
        //    {
        //        strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
        //        if (objTransfer.UpdateTransfer101Lgort(strFWerks, strFLgort, strMblnr, strLgort))
        //        {
        //            dtTransData = objTransfer.GetTransferNo(strMblnr, string.Empty);
        //            ShowMblnrDataGridView();
        //            stsWarning.Text = "仓别设定成功";
        //            successSound();
        //        }
        //        else
        //        {
        //            stsWarning.Text = "仓别设定失败";
        //            failSound();
        //        }
        //        txtLocat.Enabled = true;
        //    }
        //}
        #endregion

        private void dgvMblnrData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            this.dgvMblnrData.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvMblnrData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dtnew = (DataTable)dgvMblnrData.DataSource;

            string tmp = dgvMblnrData.Columns[e.ColumnIndex].DataPropertyName;
            int index = dgvMblnrData.Rows[e.RowIndex].Index;
            if (tmp == "DLOCAT")
            {
                #region 判断储位是否存在
                PlantData objPlantData = new PlantData(UserData);
                if (!objPlantData.CheckExistedStorageData(strWerks, strLgort, dgvMblnrData.Rows[index].Cells["LOCAT"].Value.ToString().Trim().ToUpper()))
                {
                    failSound();
                    stsWarning.Text = "当前储位不存在，请重新输入储位";
                    return;
                }
                #endregion
                dgvMblnrData.Rows[index].Cells["LOCAT"].Value = dgvMblnrData.Rows[index].Cells["LOCAT"].Value.ToString().Trim().ToUpper();
            }
        }
    }
}
    