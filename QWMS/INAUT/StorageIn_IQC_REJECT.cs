using QCI.QWMS;
using QWMS.Common;
using QWMS.PP;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QWMS
{
    public partial class StorageIn_IQC_REJECT : Form
    {

        #region 变量
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strType = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strLocat = "";
        private string strMblnr = "";
        private string strProgid = "";
        UserInfo UserData = new UserInfo();
        QCI.QWMS.StorageIn objStorageIn;
        QCI.QWMS.Authority objAuthority;
        QCI.QWMS.PlantData objPlantData;
        QCI.QWMS.StorageData objStorageData;
        DataTable dtScan = new DataTable();

        private DataGridViewComboBoxColumn dgvcLocat = new DataGridViewComboBoxColumn();
        #endregion

        #region 构造函数
        public StorageIn_IQC_REJECT(UserInfo varUserData, string Progid)
        {
            InitializeComponent();

            UserData = varUserData;
            strMandt = varUserData.Client;
            strComcd = varUserData.CompanyCode;
            strUsrnm = varUserData.UserId;
            strProgid = Progid;

            try
            {
                objStorageIn = new QCI.QWMS.StorageIn(UserData, strProgid);
                objPlantData = new QCI.QWMS.PlantData(UserData);
                objAuthority = new QCI.QWMS.Authority(UserData);
                objStorageData = new QCI.QWMS.StorageData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    //初始化
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
        #endregion

        #region 状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = strMandt;
            this.stsComcd.Text = strComcd;
            this.stsUsrnm.Text = strUsrnm;
        }
        #endregion

        #region 厂区、仓别和储位
        private void ShowDdlWerks()
        {
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
                objAuthority = new QCI.QWMS.Authority(UserData);

                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                //当前Plant
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                    dtTemp = objAuthority.CheckLgortAuthority();
                //现有值
                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    cmbLgort.Items.Clear();

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
                        //去重复值
                        if (!cmbLgort.Items.Contains(dtTemp.Rows[i]["F_TEXT"].ToString()))
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

        private void ShowDdlLocat(string strEvent)
        {
            stsWarning.Text = "";
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    if (strEvent == "DoubleClick")
                    {
                        StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, strProgid, strWerks, strLgort, strType);
                        objStorageIn_LocationSelect.ShowDialog();
                        txtLocat.Text = objStorageIn_LocationSelect.Locat;
                    }
                    else if (strEvent == "CmbLoactDataSource")
                    {
                        string strLocStatus = strType == "NEW" ? "0" : "1";
                        DataTable dtTemp = objPlantData.GetAllLocatData(strWerks, strLgort, "", strLocStatus);
                        if (dtTemp.Rows.Count == 0)
                        {
                            dgvcLocat.Items.Clear();
                            strLocat = "";
                        }
                        else
                        {
                            dgvcLocat.Items.Clear();

                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                //去重复值
                                if (!dgvcLocat.Items.Contains(dtTemp.Rows[i]["LOCAT"].ToString()))
                                {
                                    dgvcLocat.Items.Add(dtTemp.Rows[i]["LOCAT"].ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        strLocat = "";
                        dgvcLocat.Items.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        private void cmbWerks_SelectedValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbLgort.SelectedIndex = -1;
            ShowDdlLgort();
        }

        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
        {
            ShowDdlLocat("DoubleClick");
        }
        #endregion

        #region type
        private void rdoNew_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            gbFunction.Enabled = false;
        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            gbFunction.Enabled = false;
        }
        #endregion

        #region 刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtLocat.Text = "";
            txtBoxid.Text = "";
            rdoAdd.Checked = false;
            rdoNew.Checked = false;
            gbFunction.Enabled = true;
            this.dgvData.Columns.Clear();
            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            txtLocat.Enabled = true;
            stsWarning.Text = "";
            strWerks = "";
            strLgort = "";
            strLocat = "";
            strMblnr = "";
            strType = "";
            dtScan.Rows.Clear();
            btnConfirm.Enabled = true;
            txtBoxid.Enabled = true;
        }
        #endregion

        #region 退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 扫描事件
        private void txtBoxid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (strMblnr == "")//第一次扫描
                {
                    cmbWerks.Enabled = false;
                    cmbLgort.Enabled = false;
                    txtLocat.Enabled = false;
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    strLocat = txtLocat.Text.ToString().Trim();
                    if (strWerks == "" || strLgort == "" || strLocat == "" || strType == "")
                    {
                        MessageBox.Show("请检查类型、厂区、仓别和储位是否已选择");
                        cmbWerks.Enabled = true;
                        cmbLgort.Enabled = true;
                        txtLocat.Enabled = true;
                        return;
                    }
                }
                //扫描结果为：MBLNR;MATNR;CHARG;LIFNR;MENGE
                #region 分解扫描结果、找到BOXID，然后查询WHTIC数据
                string strTempBoxid = string.Empty;
                string strTempMblnr = string.Empty;
                string strTempMatnr = string.Empty;
                string strTempCharg = string.Empty;
                string strTempLifnr = string.Empty;
                string strTempMenge = string.Empty;
                string strTempInsmk = string.Empty;
                string strTempGuid = string.Empty;//只有OA传过来的AP单号才有GUID，涉及到签核
                try
                {
                    strTempBoxid = txtBoxid.Text.ToString().Trim().ToUpper().Replace("；", ";");
                    if (strTempBoxid.Contains(";"))//扫描
                    {
                        strTempBoxid = strTempBoxid.Split(';')[0];
                    }
                    DataTable dtTempData = new DataTable();
                    dtTempData = objStorageData.QueryDocWithNoScan(strWerks, strLgort, strTempBoxid);
                    if (dtTempData.Rows.Count > 0)
                    {
                        strTempMblnr = dtTempData.Rows[0]["BOXID"].ToString();
                        strTempMatnr = dtTempData.Rows[0]["MATNR"].ToString();
                        strTempCharg = dtTempData.Rows[0]["CHARG"].ToString();
                        strTempLifnr = dtTempData.Rows[0]["LIFNR"].ToString();
                        strTempMenge = dtTempData.Rows[0]["MENGE"].ToString();
                        if (strLgort.Substring(0, 2).Equals("RJ"))
                            strTempInsmk = "G";
                        else
                            strTempInsmk = dtTempData.Rows[0]["INSMK"].ToString();
                        strTempGuid = dtTempData.Rows[0]["GUID"].ToString();
                    }
                    else
                    {
                        stsWarning.Text = "无数据";
                        return;
                    }

                }
                catch
                {
                    stsWarning.Text = "格式错误";
                    return;
                }

                #endregion

                #region 检查扫描结果
                if (strTempMblnr == "" || strTempMatnr == "" || strTempLifnr == "" || strTempMenge == "")
                {
                    MessageBox.Show("扫描结果有误，请检查");
                    return;
                }
                if (strMblnr == "")
                {
                    strMblnr = strTempMblnr.Substring(0, 10);
                }
                else if (strTempMblnr.Substring(0, 10) != strMblnr)
                {
                    MessageBox.Show("单号不同，请检查");
                    return;
                }
                if (!IsInt(strTempMenge))
                {
                    MessageBox.Show("数量错误，请检查");
                    return;
                }
                //检查该单号是否与所选厂区、仓别一致
                if (!objPlantData.checkWhticLgort(strTempMblnr, strWerks, strLgort))
                {
                    MessageBox.Show("该单号与所选厂区仓别不匹配或已入库");
                    return;
                }
                //检查是否存在一种料号，多种LIFNR情况
                if (dtScan.Rows.Count > 0)
                {
                    DataRow[] drTemp = dtScan.Select("MATNR='" + strTempMatnr + "' AND LIFNR<>'" + strTempLifnr + "' ");
                    if (drTemp.Length > 0)
                    {
                        MessageBox.Show("同种料号，不同厂商代码，无法入同一储位");
                        txtBoxid.Text = "";
                        txtBoxid.Focus();
                        return;
                    }
                }
                //检查是否重复刷
                if (dtScan.Rows.Count > 0)
                {
                    DataRow[] drTemp = dtScan.Select("MBLNR='" + strTempMblnr + "' ");
                    if (drTemp.Length > 0)
                    {
                        MessageBox.Show("该单号已被刷入");
                        return;
                    }
                }
                #endregion

                if (!dtScan.Columns.Contains("WERKS"))
                {
                    dtScan.Columns.Add("WERKS");
                    dtScan.Columns.Add("LGORT");
                    dtScan.Columns.Add("LOCAT");
                    dtScan.Columns.Add("MBLNR");
                    dtScan.Columns.Add("MATNR");
                    dtScan.Columns.Add("CHARG");
                    dtScan.Columns.Add("LIFNR");
                    dtScan.Columns.Add("MENGE");
                    dtScan.Columns.Add("INSMK");
                    dtScan.Columns.Add("GUID");
                }
                dtScan.Rows.Add(strWerks, strLgort, strLocat, strTempMblnr, strTempMatnr, strTempCharg, strTempLifnr, strTempMenge, strTempInsmk, strTempGuid);
                ShowDataGrid();
                txtBoxid.Text = "";
                this.txtBoxid.Focus();
            }
        }
        #endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {

            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            this.dgvData.AllowUserToAddRows = false;
            try
            {
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "储位";
                dgvcLOCAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "单号";
                dgvcMBLNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "厂商代码";
                dgvcLIFNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMENGE);

                //数据源
                dgvData.DataSource = dtScan;
                lblData.Text = dtScan.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region 检查string是否为数字
        private static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
        #endregion

        #region 确认
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            objStorageData = new StorageData(UserData, strWerks, strLgort);
            btnConfirm.Enabled = false;
            stsWarning.Text = "";
            if (dtScan.Rows.Count == 0)
            {
                MessageBox.Show("无数据");
                btnConfirm.Enabled = true;
                return;
            }
            //再次检查是否已经入库
            foreach (DataRow dr in dtScan.Rows)
            {
                if (!objPlantData.checkWhticLgort(dr["MBLNR"].ToString(), strWerks, strLgort))
                {
                    MessageBox.Show(dr["MBLNR"].ToString() + "该单号已入库");
                    btnConfirm.Enabled = true;
                    return;
                }
            }

            //檢查要入庫的儲位是否已經有相同料號但不同的Vendor code)  田忠老板要求的，不可更改
            foreach (DataRow dr in dtScan.Rows)//通过该界面入库的包括不良品、领料单，先写两个状态
            {
                if (objStorageData.CheckExistedDifferentVendorCode(strLocat, dr["MATNR"].ToString(), "J", "", dr["LIFNR"].ToString()))
                {
                    stsWarning.Text = dr["MATNR"].ToString() + " 已經存在相同的儲位，但有不同的Vendor Code，請確認!!";
                    btnConfirm.Enabled = true;
                    return;
                }
                if (objStorageData.CheckExistedDifferentVendorCode(strLocat, dr["MATNR"].ToString(), "G", "", dr["LIFNR"].ToString()))
                {
                    stsWarning.Text = dr["MATNR"].ToString() + " 已經存在相同的儲位，但有不同的Vendor Code，請確認!!";
                    btnConfirm.Enabled = true;
                    return;
                }
            }

            // 检查异动
            string strBwartFromWhtic = objStorageData.QueryDocWithNoScan(strWerks, strLgort, dtScan.Rows[0]["MBLNR"].ToString()).Rows[0]["BWART"].ToString();

            #region 入库
            if (strBwartFromWhtic == "350" && strLgort == "TW30")
            {
                #region 350异动，TW30仓，直接入库，无需扣账
                if (objStorageIn.IQC_REJECT_IN(dtScan))
                {
                    stsWarning.Text = "入库成功";
                }
                else
                {
                    stsWarning.Text = "入库失败";
                }
                #endregion
            }
            //  else if (strBwartFromWhtic == "325" || strBwartFromWhtic == "350")
            else if (dtScan.Rows[0]["MBLNR"].ToString().Substring(0, 1) == "B")
            {
                #region 先入库，然后判断该单据是否入完，如果入完调用RFC去SAP扣账
                //1、入库
                if (objStorageIn.IQC_REJECT_IN(dtScan))
                {
                    stsWarning.Text = "入库成功";
                }
                else
                {
                    stsWarning.Text = "入库失败";
                    return;
                }
                //2、判断该单据是否入完
                if (objStorageData.CheckOverWhtic(strWerks, strLgort, dtScan.Rows[0]["MBLNR"].ToString()))
                {
                    //3、调用RFC去SAP扣账（有交互文档，但并不是立即执行扣账，SEC045）
                    DataTable dtSend = objStorageData.QueryBadPNFromWhdwn(strWerks, dtScan.Rows[0]["MBLNR"].ToString());
                    if (dtSend.Rows.Count == 0)
                    {
                        stsWarning.Text = "同步文档失败";
                        return;
                    }
                    DataTable dtGetSAPInfo=new DataTable();
                    try
                    {
                        dtGetSAPInfo = SendToSAP(dtSend).Tables[0];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("交互文档出错，请手动在SAP确认。"+ex.ToString());
                    }

                    //判断SAP回执回来
                    for (int j = 0; j < dtGetSAPInfo.Rows.Count; j++)
                    {
                        string strText = dtGetSAPInfo.Rows[j]["TEXT"].ToString().Trim();
                        string strRefDoc = dtGetSAPInfo.Rows[j]["APPNO"].ToString().Trim();

                        if (!string.IsNullOrEmpty(strText))
                        {
                            if (strText == "APPNO Confirm失败!")
                            {
                                MessageBox.Show(strText);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("该单号" + strRefDoc + "在SAP不存在，请确认！");
                            return;
                        }
                    }
                    stsWarning.Text = "入库成功，送SAP扣账成功，该单结束";
                }
                #endregion
            }
            //else if (strBwartFromWhtic == "344" || strBwartFromWhtic == "343")
            else if (dtScan.Rows[0]["MBLNR"].ToString().Substring(0, 1) == "A")
            {
                #region 判断签核状态，然后入库，然后判断该单据是否入完，如果入完调用OA接口签核
                int i = 0;//仓管员在哪一关
                bool check = false;//是否到了签核关卡
                //1、判断签核状态
                iworkflow.WFWebService IWF = new iworkflow.WFWebService();
                DataTable dtOATemp = new DataTable();

                dtOATemp = IWF.GetApplicationApproveFlow("WF0097", dtScan.Rows[0]["MBLNR"].ToString().Substring(0, dtScan.Rows[0]["MBLNR"].ToString().Length - 4), dtScan.Rows[0]["GUID"].ToString()).Tables[0];
                for (int j = 0; j < dtOATemp.Rows.Count; j++)
                {
                    if (dtOATemp.Rows[j]["FlowDescription"].ToString().Contains("仓管") && dtOATemp.Rows[j]["ApproveStatus"].ToString().Contains("未处理"))
                    {
                        check = true;
                        i = j;
                    }
                }
                if (!check)
                {
                    MessageBox.Show("签核关卡错误，无法入库");
                    return;
                }
                //2、入库
                if (objStorageIn.IQC_REJECT_IN(dtScan))
                {
                    stsWarning.Text = "入库成功";
                }
                else
                {
                    stsWarning.Text = "入库失败";
                    return;
                }
                //3、判断该单据是否入完
                if (objStorageData.CheckOverWhtic(strWerks, strLgort, dtScan.Rows[0]["MBLNR"].ToString()))
                {
                    //4、调用OA接口签核
                    try
                    {
                        if (IWF.Approve("WF0097", dtScan.Rows[0]["MBLNR"].ToString().Substring(0, dtScan.Rows[0]["MBLNR"].ToString().Length - 4), dtScan.Rows[0]["GUID"].ToString(), Convert.ToInt32(dtOATemp.Rows[i]["FlowNo"].ToString()), Convert.ToInt32(dtOATemp.Rows[i]["SequenceNo"].ToString()), dtOATemp.Rows[i]["Approver"].ToString(), "1", "System"))
                        {
                            stsWarning.Text = "签核成功";
                        }
                        else
                        {
                            stsWarning.Text = "签核失败，请手动签核";
                        }
                    }
                    catch
                    {
                        stsWarning.Text = "签核失败，请手动签核";
                    }
                }
                #endregion
            }
            else
            {
                stsWarning.Text = "入库失败，异动错误";
            }
            #endregion

            txtBoxid.Enabled = false;
        }
        #endregion

        public DataSet SendToSAP(DataTable vardtSend)
        {
            try
            {
                PP_Service obj = new PP_Service();
                DataSet ds = new DataSet();
                DataTable dtTemp = vardtSend.Copy();
                ds.Tables.Add(dtTemp);
                DataSet dsResult = obj.ZRFC_IQC_ZMWFC(ds);
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
