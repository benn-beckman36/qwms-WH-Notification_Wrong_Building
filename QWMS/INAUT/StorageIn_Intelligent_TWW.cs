using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;
using System.Text.RegularExpressions;
using System.Media;
namespace QWMS
{
    public partial class StorageIn_Intelligent_TWW : Form
    {

        #region DataMember

        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;
        private string strLocat = string.Empty;
        private string strECNo = string.Empty;
        private string strTWWNOFlag = string.Empty;
        private string strStatus = string.Empty;
        private int item; //BOXID的Item

        private DataTable dtECHeadSource = new DataTable();//EC单表头
        private DataTable dtECItemSource = new DataTable();//EC单表体
        private DataTable dtStorage = new DataTable();//入库数据
        private DataTable dtLotCode = new DataTable();//BOXID数据
        QCI.QWMS.StorageIn objStorageIn;
        QCI.QWMS.StorageData objStorageData;
        QCI.QWMS.Admin objAdmin;
        QCI.QWMS.PlantData objPlantData;
        QCI.QWMS.LogData objLogData;

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

        #endregion
        #endregion
        public StorageIn_Intelligent_TWW(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            lblUsrnm.Text = Usrnm;
            try
            {
                objStorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);
                objLogData = new LogData(UserData, string.Empty, string.Empty, Progid);


                ShowStatusData();
                ShowStorageInData();
                ShowECHeadDataGrid();
                ShowECItemDataGrid(dtECItemSource);
                strStatus = "N";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region ShowStatus/Lgort/StorageInData

        #region ShowStatusData()
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
            this.stsWarning.Width = 1000;
        }
        #endregion

        #region ShowStorageInData
        public void ShowStorageInData()
        {
            if (dtECHeadSource.Columns.Count == 0)
            {
                //WERKS,PNUM,LIFNR,BKTXT,CASE CLRTYP WHEN 'A' THEN N'逐票报关' ELSE N'汇总报关'END AS CLRNAM,CUSTID
                dtECHeadSource.Columns.Add("WERKS");
                dtECHeadSource.Columns.Add("PNUM");
                dtECHeadSource.Columns.Add("LIFNR");
                dtECHeadSource.Columns.Add("BKTXT");
                dtECHeadSource.Columns.Add("CLRNAM");
                dtECHeadSource.Columns.Add("CUSTID");
                dtECHeadSource.Columns.Add("STATUS");
                dtECHeadSource.Columns.Add("BOXID");
            }
            if (dtECItemSource.Columns.Count == 0)
            {
                // select  PNUM,PITEM,MATNR,CHARG1,MENGE,LGORT,KOSTL,AEC,CHECKSTATS
                dtECItemSource.Columns.Add("PNUM");
                dtECItemSource.Columns.Add("PITEM");
                dtECItemSource.Columns.Add("MATNR");
                dtECItemSource.Columns.Add("LOCAT");
                dtECItemSource.Columns.Add("LIFNR");
                dtECItemSource.Columns.Add("CHARG1");
                dtECItemSource.Columns.Add("MENGE", typeof(int));
                dtECItemSource.Columns.Add("MENGEC", typeof(int));
                dtECItemSource.Columns.Add("LGORT");
                dtECItemSource.Columns.Add("KOSTL");
                dtECItemSource.Columns.Add("AEC");
                dtECItemSource.Columns.Add("CHECKSTATS");
                dtECItemSource.Columns.Add("MATCH");
            }
            if (dtLotCode.Columns.Count == 0)
            {
                dtLotCode.Columns.Add("WERKS");
                dtLotCode.Columns.Add("LGORT");
                dtLotCode.Columns.Add("LOCAT");
                dtLotCode.Columns.Add("MATNR");
                dtLotCode.Columns.Add("DACOD");
                dtLotCode.Columns.Add("LIFNR");
                dtLotCode.Columns.Add("LOCOD");
                dtLotCode.Columns.Add("MENGE", typeof(int));
                dtLotCode.Columns.Add("OTQTY", typeof(int));
                dtLotCode.Columns.Add("VEDAT");
                dtLotCode.Columns.Add("INDAT");
                dtLotCode.Columns.Add("REMAK");
            }
            if (dtStorage.Columns.Count == 0)
            {
                dtStorage.Columns.Add("MANDT");
                dtStorage.Columns.Add("COMCD");
                dtStorage.Columns.Add("WERKS");
                dtStorage.Columns.Add("LGORT");
                dtStorage.Columns.Add("PNUM");
                dtStorage.Columns.Add("MBLNR");
                dtStorage.Columns.Add("MATNR");
                dtStorage.Columns.Add("CHARG");
                dtStorage.Columns.Add("INSMK");
                dtStorage.Columns.Add("LOCAT");
                dtStorage.Columns.Add("MENGE", typeof(int));
                dtStorage.Columns.Add("ALQTY", typeof(int));
                dtStorage.Columns.Add("LIFNR");
                dtStorage.Columns.Add("DACOD");
                dtStorage.Columns.Add("LOCOD");
                dtStorage.Columns.Add("VEDAT");
                dtStorage.Columns.Add("INDAT");
                dtStorage.Columns.Add("RMAK1");
                dtStorage.Columns.Add("MRGID");
                dtStorage.Columns.Add("ARBPL");
                dtStorage.Columns.Add("KOSTL");
            }

        }
        #endregion

        #endregion

        #region ShowDataGridView
        #region ShowECHeadDataGrid
        private void ShowECHeadDataGrid()
        {
            dgvECHead.AutoGenerateColumns = false;
            dgvECHead.Columns.Clear();
            try
            {
                //PNUM
                DataGridViewTextBoxColumn dgvcPNUM = new DataGridViewTextBoxColumn();
                dgvcPNUM.DataPropertyName = "PNUM";
                dgvcPNUM.HeaderText = "EC单号";
                dgvcPNUM.Name = "PNUM";
                dgvcPNUM.Width = 130;
                dgvcPNUM.ReadOnly = true;
                dgvECHead.Columns.Add(dgvcPNUM);

                //LIFNR
                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "VendorCode";
                dgvcLIFNR.Name = "LIFNR";
                dgvcLIFNR.Width = 90;
                dgvcLIFNR.ReadOnly = true;
                dgvECHead.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcBKTXT = new DataGridViewTextBoxColumn();
                dgvcBKTXT.DataPropertyName = "BKTXT";
                dgvcBKTXT.HeaderText = "报关单号";
                dgvcBKTXT.Name = "BKTXT";
                dgvcBKTXT.Width = 130;
                dgvcBKTXT.ReadOnly = true;
                dgvECHead.Columns.Add(dgvcBKTXT);

                DataGridViewTextBoxColumn dgvcCRLNAM = new DataGridViewTextBoxColumn();
                dgvcCRLNAM.DataPropertyName = "CLRNAM";
                dgvcCRLNAM.HeaderText = "类型";
                dgvcCRLNAM.Name = "CLRNAM";
                dgvcCRLNAM.Width = 90;
                dgvcCRLNAM.ReadOnly = true;
                dgvECHead.Columns.Add(dgvcCRLNAM);

                //DataGridViewTextBoxColumn dgvcCUSTID = new DataGridViewTextBoxColumn();
                //dgvcCUSTID.DataPropertyName = "CUSTID";
                //dgvcCUSTID.HeaderText = "报关单号";
                //dgvcCUSTID.Name = "CUSTID";
                //dgvcCUSTID.Width = 130;
                //dgvcCUSTID.ReadOnly = true;
                //dgvECHead.Columns.Add(dgvcCUSTID);

                dgvECHead.DataSource = dtECHeadSource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ShowECItemDataGrid
        private void ShowECItemDataGrid(DataTable dtData)
        {
            //select  PNUM,PITEM,MATNR,CHARG1,MENGE,LGORT,KOSTL,AEC,CHECKSTATS
            dgvECItem.AutoGenerateColumns = false;
            dgvECItem.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcPITEM = new DataGridViewTextBoxColumn();
                dgvcPITEM.DataPropertyName = "PITEM";
                dgvcPITEM.Name = "PITEM";
                dgvcPITEM.HeaderText = "序号";
                dgvcPITEM.Width = 50;
                dgvcPITEM.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcPITEM);

                DataGridViewTextBoxColumn dgvcPNUM = new DataGridViewTextBoxColumn();
                dgvcPNUM.DataPropertyName = "PNUM";
                dgvcPNUM.Name = "PNUM";
                dgvcPNUM.HeaderText = "EC单号";
                dgvcPNUM.Width = 90;
                dgvcPNUM.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcPNUM);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.Name = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcLGORT);

                //BOXID
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.Name = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG1 = new DataGridViewTextBoxColumn();
                dgvcCHARG1.DataPropertyName = "CHARG1";
                dgvcCHARG1.Name = "CHARG1";
                dgvcCHARG1.HeaderText = "版本";
                dgvcCHARG1.Width = 70;
                dgvcCHARG1.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcCHARG1);

                //MATNR
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.Name = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.Width = 70;
                dgvcMENGE.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcMENGEC = new DataGridViewTextBoxColumn();
                dgvcMENGEC.DataPropertyName = "MENGEC";
                dgvcMENGEC.Name = "ALQTY";
                dgvcMENGEC.HeaderText = "已刷数量";
                dgvcMENGEC.Width = 70;
                dgvcMENGEC.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcMENGEC);

                //AEC
                DataGridViewTextBoxColumn dgvcAEC = new DataGridViewTextBoxColumn();
                dgvcAEC.DataPropertyName = "AEC";
                dgvcAEC.Name = "AEC";
                dgvcAEC.HeaderText = "AEC";
                dgvcAEC.Width = 50;
                dgvcAEC.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcAEC);

                //CHECKSTATS
                DataGridViewTextBoxColumn dgvcCHECKSTATS = new DataGridViewTextBoxColumn();
                dgvcCHECKSTATS.DataPropertyName = "CHECKSTATS";
                dgvcCHECKSTATS.Name = "CHECKSTATS";
                dgvcCHECKSTATS.HeaderText = "检验";
                dgvcCHECKSTATS.Width = 50;
                dgvcCHECKSTATS.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcCHECKSTATS);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.Name = "LIFNR";
                dgvcLifnr.HeaderText = "厂商代码";
                dgvcLifnr.Visible = false;
                dgvcLifnr.ReadOnly = true;
                dgvECItem.Columns.Add(dgvcLifnr);

                dgvECItem.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ShowBoxDataGrid
        private void ShowBoxDataGrid(DataTable dtData)
        {
            dvBox.AutoGenerateColumns = false;
            dvBox.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.Name = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dvBox.Columns.Add(dgvcMATNR);

                //MATNR
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.Name = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.Width = 90;
                dgvcMENGE.ReadOnly = true;
                dvBox.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvLocat = new DataGridViewTextBoxColumn();
                dgvLocat.DataPropertyName = "LOCAT";
                dgvLocat.Name = "LOCAT";
                dgvLocat.HeaderText = "储位";
                //dgvLocat.ReadOnly = true;
                dgvLocat.Width = 90;
                dvBox.Columns.Add(dgvLocat);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.Name = "LIFNR";
                dgvcLifnr.HeaderText = "VendorCode";
                dgvcLifnr.Width = 100;
                dgvcLifnr.ReadOnly = true;
                dvBox.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcLOCOD = new DataGridViewTextBoxColumn();
                dgvcLOCOD.DataPropertyName = "DACOD";
                dgvcLOCOD.Name = "DACOD";
                dgvcLOCOD.HeaderText = "DACOD";
                dgvcLOCOD.Width = 90;
                dgvcLOCOD.ReadOnly = true;
                dvBox.Columns.Add(dgvcLOCOD);

                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDAT";
                dgvcVedat.Name = "VEDAT";
                dgvcVedat.HeaderText = "VEDAT";
                dgvcVedat.Width = 90;
                dgvcVedat.ReadOnly = true;
                dvBox.Columns.Add(dgvcVedat);

                dvBox.DataSource = dtData;
                dvBox.FirstDisplayedScrollingRowIndex = dvBox.Rows.Count - 1; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion
        #endregion

        #region EC单操作
        private void txtEC_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyChar == (char)13)
            {


                strECNo = txtEC.Text.Trim();
                DataTable dt = new DataTable();
                dt = objStorageIn.getECNO(strECNo);
                if (dt.Rows.Count > 0)
                {
                    strTWWNOFlag = dt.Rows[0]["MBFLG"].ToString().Trim();
                }
                else
                {
                    stsWarning.Text = "该EC单，达伟未传入！！！";
                    txtEC.Text = string.Empty;
                    SetErrNotice();
                    return;
                }

                //txtLgort.Text = dt.Rows[0]["LGORT"].ToString().Trim();

                DataTable dtstatus = new DataTable();
                #region 判断EC单是否已完成，若无，则同步EC单数据
                if(txtEC.Text.ToString().Trim()=="")
                {
                    stsWarning.Text = "请刷入EC单！！！";
                    SetErrNotice();
                    return;
                }
                #region 校验是否该EC单已经刷入
                List<string> lsPnum = (from pnum in dtECHeadSource.AsEnumerable() select pnum.Field<string>("PNUM")).ToList();
                if (lsPnum.Contains(txtEC.Text.Trim().ToUpper()))
                {
                    stsWarning.Text = "该EC单已刷入，请选择未刷入的EC单！！！";
                    txtEC.Text = string.Empty;
                    SetErrNotice();
                    return;
                }
                #endregion
                if (strTWWNOFlag == "ECNO")
                {
                    dtstatus = objStorageIn.GetPacingStatus(txtEC.Text.Trim());//查看当前EC单数据
                }
                else
                { 
                    dtstatus = objStorageIn.GetPacingStatus_TWW(txtEC.Text.Trim());//查看当前EC单数据
                    DataTable dtASNNo = new DataTable();
                    dtASNNo = objStorageIn.GetPacingStatus_TWW(txtEC.Text.Trim(),"E");//查看311是否完成作业
                    if (dtASNNo.Rows[0]["STATUS"].ToString().Trim() != "Y")
                    {
                        MessageBox.Show("发运订单状态不对，请稍后作业！");
                        SetErrNotice();
                        txtEC.Text = string.Empty;
                        return;
                    }
                }
                if (dtstatus.Rows.Count > 0)
                {
                    if (dtstatus.Rows[0]["STATUS"].ToString().Trim() == "Y")
                    {
                        MessageBox.Show("已经完成扣账，不能重新作业！");
                        SetErrNotice();
                        txtEC.Text = string.Empty;
                        return;
                    }
                    if (dtstatus.Rows[0]["STATUS"].ToString().Trim() == "P")
                    {
                        if(!chkProcessing.Checked)
                        {
                            MessageBox.Show("EC单正在被处理！");
                            SetErrNotice();
                            //txtEC.Text = string.Empty;
                            return;
                        }
                        else
                        {
                            string strUser = "";
                            #region 判断正在处理中的EC单操作人和当前操作人是否相同，若相同，则带出数据
                            if (strTWWNOFlag == "ECNO")
                            {
                                strUser = objStorageIn.getECUser(txtEC.Text.Trim(), string.Empty);
                            }
                            else
                            {
                                strUser = objStorageIn.getECUser_TWW(txtEC.Text.Trim(), string.Empty);
                            }
                            if (!Usrnm.Equals(strUser))
                            {
                                MessageBox.Show(txtEC.Text.Trim() + "的操作人是" + strUser);
                                return;
                            }
                            else
                            {
                                getProcessingEC(txtEC.Text.Trim(), Usrnm);//带出同一个BOXID下的数据
                                return;
                            }
                            #endregion
                        }

                    }
                }
                else
                {
                    if (strTWWNOFlag == "ECNO")
                    {
                        if (!objStorageIn.GetECFromQEC(txtEC.Text.Trim()))
                        {
                            txtEC.Text = string.Empty;
                            MessageBox.Show("EC单不存在，请确认！");
                            return;
                        }
                    }
                   
                }
                #endregion      

                #region EC单表头明细信息
                DataTable dtECHead = new DataTable();
                DataTable dtECItem = new DataTable();
                if (strTWWNOFlag == "ECNO")
                {
                    dtECHead = objStorageIn.GetECHead(strWerks, txtEC.Text.Trim(), "", "", "", strStatus, "");
                    dtECItem = objStorageIn.GetECItem(txtEC.Text.Trim());
                }
                else
                {
                    dtECHead = objStorageIn.GetECHead_TWW(strWerks, txtEC.Text.Trim(), "", "", "", strStatus, "");
                    dtECItem = objStorageIn.GetECItem_TWW(txtEC.Text.Trim());

                }

                if(dtECHead.Rows.Count==0)
                {
                    stsWarning.Text = "无此EC单";
                    SetErrNotice();
                    return;
                }
                DataTable dtTemp = objAdmin.PermissionQuery(Mandt, Comcd, dtECHead.Rows[0]["WERKS"].ToString(), Usrnm);
                if (dtTemp.Rows.Count == 0)
                {
                    stsWarning.Text = "当前用户无该厂区EC单操作权限！！！";
                    SetErrNotice();
                    return;
                }
                if (strWerks != "")
                {
                    if (strWerks != dtECHead.Rows[0]["WERKS"].ToString())
                    {
                        stsWarning.Text = "不同厂区EC单不能同时刷入！！！";
                        SetErrNotice();
                        return;
                    }
                }
                else
                {
                    //首次刷入EC单
                    strWerks = dtECHead.Rows[0]["WERKS"].ToString();
                    lblWerks.Text = strWerks;
                }
                if(objLogData.AddQWMSLOG(txtEC.Text.Trim(),"EC","N","刷入EC单","Y",Usrnm))
                {
                    dtECHeadSource.Merge(dtECHead);
                    //if(dtECHeadSource.Rows.Count==5)
                    //{
                    //    txtEC.Enabled = false;
                    //}
                    dtECItemSource.Merge(dtECItem);
                    txtEC.Text = string.Empty;
                    //SetOKNotice();
                    ShowECHeadDataGrid();
                    getShowEcItem();
                    ShowBoxDataGrid(dtLotCode);
                }
                #endregion

                btnReset.Enabled = false;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                #region 若无EC数据，则带出当前用户所有正在处理中的EC数据
                if (dtECHeadSource.Rows.Count == 0)
                {
                    MessageBox.Show("无EC单数据需要预检");
                    return;
                }
                else
                {
                    getShowEcItem();
                }
                #endregion
                #region 再传给SAP预检是否符合扣帐条件，不符合发送异常邮件，帐务人员联系相关单位处理问题单，不符合的EC不取出只是提前处理问题单，继续作业
                //sendToSap("QWMS_ZNSL", "X"); //预设仓别TW10
                #endregion
                txtLgort.Enabled = true;
                txtLgort.Focus();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        private void getProcessingEC(string strEC, string Usrnm)
        {
            stsWarning.Text = string.Empty;
            DataTable dtEC = new DataTable();
            if (strTWWNOFlag == "ECNO")
            {
                dtEC = objStorageIn.GetECHead(string.Empty, strEC, string.Empty, string.Empty, string.Empty, strStatus, strUsrnm);
            }
            else
            {
                dtEC = objStorageIn.GetECHead_TWW(string.Empty, strEC, string.Empty, string.Empty, string.Empty, strStatus, strUsrnm);
            }
            strECNo = dtEC.Rows[0]["PNUM"].ToString();
            strWerks = dtEC.Rows[0]["WERKS"].ToString();
            lblWerks.Text = strWerks;
            //lblBoxID.Text = strECNo;
            dtECHeadSource = dtEC.Copy();
            //if(!InitEC())//数据初始化
            //{
            //    return;
            //}
            DataTable dtECItem = new DataTable();
            if (dtECHeadSource.Rows.Count>0)
            {
                foreach (DataRow dr in dtECHeadSource.Rows)
                {
                    if (strTWWNOFlag == "ECNO")
                    {
                        dtECItem = objStorageIn.GetECItem(dr["PNUM"].ToString());
                    }
                    else
                    {
                        dtECItem = objStorageIn.GetECItem_TWW(dr["PNUM"].ToString());
                    }
                    dtECItemSource.Merge(dtECItem);
                    DataTable dtStock = objStorageIn.GetEcInStock(dr["PNUM"].ToString());
                    dtStorage.Merge(dtStock);
                }
                dtLotCode = objStorageIn.getECNO(strECNo);
                txtEC.Text = string.Empty;
                txtEC.Enabled = false;
                txtLgort.Enabled = true;
                txtLgort.Focus();
               
                ShowECHeadDataGrid();
                getShowEcItem();
                getShowBox();
                SetChecked(strLocat);
            }
            else
            {
                txtEC.Text = string.Empty;
                return;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;

            string strMessage = string.Empty;
            //if(!InitEC())
            //{
            //    return;
            //}
            //objLogData.AddQWMSLOG(strECNo, "ECNo", "Confirm","开始匹配","N",Usrnm);
            if (txtLgort.Text.ToString().Trim() == "")
            {
                MessageBox.Show(" 请输入仓别！ ");
                return;
            }

            dtLotCode = objStorageIn.getECNO(strECNo);
            if (dtLotCode.Rows.Count==0)
            {
                MessageBox.Show(" 未查询到达伟单证信息 ");
                return;
            }
            objStorageIn.updateECstatus(strECNo, "P", "");
            string strMatnr = "";
            int intMenge = 0;
            string strVendorcode = "";
            string strDCbefore = "";
            string strLotCode = "";
            string strLocat = "";
            string strDC = "";

            for (int i = 0; i < dtLotCode.Rows.Count; i++)
            {
                strMatnr = dtLotCode.Rows[i]["MATNR"].ToString().Trim();
                intMenge = Convert.ToInt32(dtLotCode.Rows[i]["MENGE"].ToString().Trim());
                strVendorcode = dtLotCode.Rows[i]["LIFNR"].ToString().Trim();
                strDCbefore = dtLotCode.Rows[i]["DACOD"].ToString().Trim();
                strLotCode = dtLotCode.Rows[i]["LOCOD"].ToString().Trim();
                strLocat = dtLotCode.Rows[i]["LOCAT"].ToString().Trim();
                strDC = dtLotCode.Rows[i]["VEDAT"].ToString().Trim();


                if (objStorageIn.checkMatbox(strWerks, "", strLocat, strMatnr, strVendorcode, strDCbefore))
                {
                    if (!objStorageIn.updateMatbox(strWerks, "", strLocat, strMatnr, intMenge, strVendorcode, strDCbefore))
                    {
                        SetErrNotice();

                        MessageBox.Show("达伟信息校验异常！！！");
                        return;
                    }
                    else
                    {
                        SetOKNotice();
                    }
                }
                else
                {
                    if (!objStorageIn.ScanBoxid_NEW(strWerks, "", "", strMatnr, intMenge, strVendorcode, strDCbefore, strLotCode, strLocat, strDC, ""))
                    {
                        SetErrNotice();
                        MessageBox.Show("达伟信息校验异常！！！");
                        return;
                    }
                    else
                    {
                        SetOKNotice();
                    }
                }

            }

            getShowBox();
            SetChecked(strLocat);
            txtLocat.Text = strLocat;


            try
            {
                #region 比对EC单数据和刷入数据
                if (dtLotCode.Rows.Count > 0 && dtECItemSource.Rows.Count > 0)
                {
                    DataTable dtECCombine = CombineDataTableByMatnr(dtECItemSource);
                    DataTable dtMatnrCombine = CombineDataTableByMatnr(dtLotCode); 
                    List<string> lsECError = new List<string>();
                    DataTable dtMatnrError = new DataTable();
                    dtMatnrError.Columns.Add("MATNR");
                    dtMatnrError.Columns.Add("MESSAGE");
                    foreach (DataRow dr in dtECCombine.Rows)
                    {
                        DataRow[] drSelect = dtMatnrCombine.Select(" MATNR='" + dr["MATNR"].ToString() + "' AND LIFNR='" + dr["LIFNR"].ToString() + "' ");
                        if(drSelect.Length>0)
                        {
                            if (int.Parse(dr["MENGE"].ToString()) != int.Parse(drSelect[0]["MENGE"].ToString()))
                            {
                                int diff = int.Parse(dr["MENGE"].ToString()) - int.Parse(drSelect[0]["MENGE"].ToString());
                                #region 刷入数量与EC但数量不匹配
                                if (diff > 0)
                                {
                                    //20201129 Yan He 少刷数量、少刷料号，不管仓库点是还是否，都设为“作业中的EC单”
                                    MessageBox.Show(dr["MATNR"].ToString() + "：该料号刷入数量小于EC单数量" + diff );
                                    objLogData.AddQWMSLOG(strECNo, "ECNo", "Comfirm", "刷入数量小于EC单数量，不标记为问题单", "N", Usrnm);
                                    ShowErrorNumber(dr["MATNR"].ToString(), dr["LIFNR"].ToString());
                                    return;
                                }
                                if(diff<0)
                                {
                                    //20201129 Yan He 多刷数量，不管仓管点是还是否，都设为问题单
                                    MessageBox.Show(dr["MATNR"].ToString() + "：该料号刷入数量大于EC单数量" + Math.Abs(diff) );
                                }
                                #endregion
                                #region 标记，将问题料号及其数量信息存放到drMatnrError
                                foreach (DataRow drEcItem in dtECItemSource.Rows)
                                {
                                    if (drEcItem["MATNR"].Equals(dr["MATNR"].ToString()) && dr["LIFNR"].Equals(drEcItem["LIFNR"].ToString()) && (!lsECError.Contains(drEcItem["PNUM"].ToString())))
                                    {
                                        lsECError.Add(drEcItem["PNUM"].ToString());
                                    }
                                }
                                //MessageBox.Show(dr["MATNR"].ToString() + " " + dr["LIFNR"].ToString() + "EC单料号与刷入的BOXID数量不一致，相差" + diff + "，请确认！");
                                DataRow drMatnrError = dtMatnrError.NewRow();
                                drMatnrError["MATNR"] = dr["MATNR"].ToString();
                                drMatnrError["MESSAGE"] = "EC单数量-刷入数量:" + diff;
                                dtMatnrError.Rows.Add(drMatnrError);
                                #endregion
                            }
                        } 
                        else
                        {
                            #region 存在未刷入料号时，继续刷入
                            DialogResult result = new DialogResult();
                            result = MessageBox.Show(dr["MATNR"].ToString() + "未刷入，是否将相关EC单标记为问题单?", "料号匹配", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                foreach (DataRow drEcItem in dtECItemSource.Rows)
                                {
                                    if (drEcItem["MATNR"].Equals(dr["MATNR"].ToString()) && dr["LIFNR"].Equals(drEcItem["LIFNR"].ToString()) && (!lsECError.Contains(drEcItem["PNUM"].ToString())))
                                    {
                                        lsECError.Add(drEcItem["PNUM"].ToString());
                                    }
                                }
                                DataRow drMatnrError = dtMatnrError.NewRow();
                                drMatnrError["MATNR"] = dr["MATNR"].ToString();
                                drMatnrError["MESSAGE"] = "无刷入数量:" ;
                                dtMatnrError.Rows.Add(drMatnrError);
                            }
                            else
                            {
                                objLogData.AddQWMSLOG(strECNo, "ECNo", "Comfirm", "存在料号未刷入，未标记为问题单", "N", Usrnm);
                                ShowErrorNumber(dr["MATNR"].ToString(), dr["LIFNR"].ToString());
                                return;
                            }
                            #endregion
                        }
                    }
                    foreach(DataRow drMatnrErr in dtMatnrError.Rows)
                    {
                        strMessage += "\r\n" + drMatnrErr["MATNR"].ToString() + drMatnrErr["MESSAGE"].ToString();
                    }
                    if (!string.IsNullOrEmpty(strMessage))
                        MessageBox.Show(strMessage);
                    strMessage = string.Empty;
                    foreach(string strECError in lsECError)
                    {
                        strMessage += "\r\n" + strECError + "单据数量异常";
                        //objLogData.AddQWMSLOG(strECError, "EC", "Comfirm", "标记为问题单", "T", Usrnm);
                        //if (objStorageIn.updateECstatus(strECError, "T",string.Empty))
                        //{
                        //    strMessage += "\r\n" + strECError + "已标记为问题单据";
                        //}
                        foreach(DataRow dr in dtECHeadSource.Rows)
                        {
                            if(dr["PNUM"].Equals(strECError))
                            {
                                dr["STATUS"] = "T";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(strMessage))
                        MessageBox.Show(strMessage);
                    strMessage = string.Empty;
                }
                else
                {
                    MessageBox.Show(" 请刷入EC单信息和BarCode信息 ");
                    return;
                }
                #endregion

                #region EC单匹配
                btnConfirm.Enabled = false;
                objLogData.AddQWMSLOG(strECNo, "ECNo", "Confirm", "matchBox()", "N", Usrnm);
                dtStorage.Rows.Clear();

                if (!matchBox())
                {
                    objLogData.AddQWMSLOG(strECNo, "ECNo", "Confirm", "Match不成功!", "N", Usrnm);
                    return;
                }
                #endregion

                #region dtLotCode数据回退到MATBOX表中
                if (!objStorageIn.backMatbox(dtLotCode))
                {
                    stsWarning.Text = "保存异常，请重新确认！！！";
                    return;
                }
                #endregion 

                #region 将入库数据dtStorage存放在EC_INSTOCK表中
                if(dtStorage.Rows.Count>0)
                {
                    List<string> lsPnum = (from d in dtStorage.AsEnumerable() select d.Field<string>("PNUM")).Distinct().ToList();
                    foreach(string strPnum in lsPnum)
                    {
                        if(!objStorageIn.DeleteECInStore(strPnum))
                        {
                            stsWarning.Text = "入库数据未删除成功，请联系MIS";
                            objLogData.AddQWMSLOG(strPnum, "EC", "Confirm", strECNo + "入库数据未删除成功", "N", Usrnm);
                            return;
                        }
                    }
                    if (!objStorageIn.ECinStock(dtStorage))
                    {
                        stsWarning.Text = "入库数据未保存成功，请联系MIS";
                        return;
                    }
                    else
                    {
                        objLogData.AddQWMSLOG(strECNo, "ECNo", "Confirm", dtStorage.Rows[0]["PNUM"].ToString() + "入库数据保存成功", "N", Usrnm);
                    }
                }
                #endregion

                #region 显示ECITEM数据
                getShowEcItem();
                GetEcItemColor();
                ShowConfirmData();
                #endregion

                txtEC.Enabled = false;
                txtLgort.Enabled = false;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        #endregion

        #region 刷入信息操作

        private void getShowEcItem()
        {
            int sumMenge = 0;
            int sumMengec = 0;
            foreach (DataRow drItem in dtECItemSource.Rows)
            {
                drItem["LGORT"] = txtLgort.Text;

                sumMenge += int.Parse(drItem["MENGE"].ToString());
                sumMengec += int.Parse(drItem["MENGEC"].ToString());
            }
            DataTable dtData = dtECItemSource.Copy();
            DataRow drNew = dtData.NewRow();
            drNew["MENGE"] = sumMenge;
            drNew["MENGEC"] = sumMengec;
            dtData.Rows.Add(drNew);
            ShowECItemDataGrid(dtData);
        }

        private void GetEcItemColor()
        {
            btnSave.Enabled = true;
            for (int i = 0; i < dgvECItem.RowCount-2 ; i++)
            {
                if (int.Parse(dgvECItem.Rows[i].Cells["MENGE"].Value.ToString()) > int.Parse(dgvECItem.Rows[i].Cells["ALQTY"].Value.ToString()))
                {
                    dgvECItem.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    btnSave.Enabled = false;
                }
            }
        }
        private void getShowBox()
        {
            dtLotCode = objStorageIn.getECNO(strECNo);
            DataTable dtData = dtLotCode.Copy();
            DataRow drSum = dtData.NewRow();
            drSum["MENGE"] = objStorageIn.GetBoxSumMenge(dtData);
            dtData.Rows.Add(drSum);
            ShowBoxDataGrid(dtData);
        }
        private void changeLocat()
        {
            txtLocat.Text = string.Empty;
            txtLocat.Enabled = true;
            txtLocat.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                #region 判断ECItem数量TTL、EC已刷数量TTL和BOXID数量TTL是否相符，相符才可以丢给SAP扣账才可以丢给SAP扣帐
                int intEcItem = int.Parse(dgvECItem.Rows[dgvECItem.Rows.Count - 2].Cells["MENGE"].Value.ToString());
                int intEcItemScan = int.Parse(dgvECItem.Rows[dgvECItem.Rows.Count - 2].Cells["ALQTY"].Value.ToString());
                int intBox = int.Parse(dvBox.Rows[dvBox.Rows.Count - 2].Cells["MENGE"].Value.ToString());
                if (intEcItem != intEcItemScan || intEcItem != intBox)
                {
                    MessageBox.Show("ECItem数量TTL、EC已刷数量TTL和BOXID数量TTL不相符!!!");
                    return;
                }
                #endregion

                foreach (DataGridViewRow dgRow in dgvECHead.Rows)
                {
                    if (string.IsNullOrEmpty(dgRow.Cells["BKTXT"].Value.ToString()) && dgRow.Cells["CLRNAM"].Value.Equals("逐票报关"))
                    {
                        MessageBox.Show("逐票报关未报关完成，不能扣帐！");
                        return;
                    }
                }
                sendToSap("QWMS_ZNSL", "");
                foreach (DataRow drEC in dtECHeadSource.Rows)
                {
                    if (strTWWNOFlag == "DocNO")
                    {
                        if (objStorageIn.updateECstatus_TWW(drEC["PNUM"].ToString(), "Y", string.Empty))
                        {
                            drEC["STATUS"] = "Y";
                        }
                    }
                    else
                    {
                        if (objStorageIn.updateECstatus(drEC["PNUM"].ToString(), "Y", string.Empty))
                        {
                            drEC["STATUS"] = "Y";
                        }
                    }

                }

                #region QWMS入库
                string strMessage = string.Empty;
                foreach (DataRow dr in dtECHeadSource.Rows)
                {

                    objLogData.AddQWMSLOG(dr["PNUM"].ToString(), "EC", "Save", "QWMS开始入库", "N", Usrnm);
                    if (dr["STATUS"].ToString().Equals("Y"))
                    {
                        DataTable dtStoragein = objStorageIn.GetEcInStock(dr["PNUM"].ToString());
                        if(dtStoragein.Rows.Count>0)
                        {
                            if (objStorageIn.StorageInWHEC(dtStoragein))//更新库存
                            {
                                if (objStorageIn.UpdateEcInStock(dr["PNUM"].ToString()))
                                {
                                    objLogData.AddQWMSLOG(dr["PNUM"].ToString(), "EC", "Save", "QWMS入库成功", "Y", Usrnm);
                                    strMessage += "\r\n" + dr["PNUM"].ToString() + "：入库QWMS成功！";
                                }
                            }
                            else
                            {
                                strMessage += "\r\n" + dr["PNUM"].ToString() + "：入库QWMS失败！";
                            }
                        }
                        else
                        {
                            strMessage += "\r\n" + dr["PNUM"].ToString() + "：无入库数据！";
                        }
                    }
                }
                if (string.IsNullOrEmpty(strMessage))
                {
                    MessageBox.Show("无入库数据！");
                }
                else
                {
                    MessageBox.Show(strMessage);
                    ReSet();
                }

                #endregion

                btnReset.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        #region SAP扣账
        private void sendToSap(string strType,string strRun)
        {
            stsWarning.Text = string.Empty;
            string stsMessage = string.Empty;
            foreach (DataRow drEC in dtECHeadSource.Rows)
            {
                #region 问题单据不扣账
                if (drEC["STATUS"].ToString().Equals("T"))
                {
                    break;
                }
                #endregion
                DataSet ds = new DataSet();
                if (strTWWNOFlag == "DocNO")
                {
                    strType = "QWMS_ZNSLTWW";

                    DataTable dtitem = objStorageIn.GetTAB_ZM46PO_TWW(drEC["PNUM"].ToString(), txtLgort.Text.ToString().Trim());

                    dtitem.TableName = "TAB_ZM46PO";

                    objLogData.AddQWMSLOG(drEC["PNUM"].ToString(), "SAP", "Send", "扣账", "N", Usrnm);


                    ds.Tables.Add(dtitem.Copy());
                }
                else
                {

                    DataTable dthead = objStorageIn.GetTAB_ZM000(drEC["PNUM"].ToString(),"","","","");
                    DataTable dtitem = objStorageIn.GetTAB_ZM001(drEC["PNUM"].ToString(), "", "", "", "");
                    dthead.TableName = "TAB_ZM000";
                    dtitem.TableName = "TAB_ZM001";

                    objLogData.AddQWMSLOG(drEC["PNUM"].ToString(), "SAP", "Send", "扣账", "N", Usrnm);


                    ds.Tables.Add(dthead.Copy());
                    ds.Tables.Add(dtitem.Copy());
                }
                ArrayList sqlarr = new ArrayList();
                DataSet dsreturn = new DataSet();

                MM.MM_Service objMM = new MM.MM_Service();
                //MM_TestNew.MM_Service objMM = new MM_TestNew.MM_Service();
                dsreturn = objMM.Z_RFC_PACKING_POST(Usrnm, drEC["PNUM"].ToString(), strType, strRun, ds);

                #region 扣账
                DataTable dtTAB_ZM025 = dsreturn.Tables["TAB_ZM025"];
                if (dtTAB_ZM025.Rows.Count > 0)
                {
                    if (dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim() != "")
                    {
                        StringBuilder strsql = new StringBuilder();

                        if (strTWWNOFlag == "DocNO")
                        {
                            strsql.AppendFormat("UPDATE WHTWW SET OMFLG='Y' WHERE MBLNR='{0}' AND IEFLG ='I'", drEC["PNUM"].ToString());

                        }
                        else
                        {
                            strsql.AppendFormat("UPDATE EC_HEAD SET STATUS='Y' WHERE PNUM='{0}'", drEC["PNUM"].ToString());
                            strsql.AppendFormat("UPDATE WHTWW SET OMFLG='Y' WHERE MBLNR='{0}' AND IEFLG ='I'", drEC["PNUM"].ToString());

                        }
                        strsql.AppendFormat("UPDATE EC_INSTOCK SET MBLNR='{0}' WHERE PNUM='{1}'", dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim(), drEC["PNUM"].ToString());
                        sqlarr.Add(strsql);
                        foreach (DataRow dr in dtStorage.Rows)
                        {
                            if (dr["PNUM"].Equals(drEC["PNUM"].ToString()))
                            {
                                dr["MBLNR"] = dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim();
                            }
                        }
                    }
                    for (int i = 0; i < dtTAB_ZM025.Rows.Count; i++)
                    {
                        if (dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim() != "")
                        {
                            if (strTWWNOFlag == "DocNO")
                            {
                                StringBuilder strsql = new StringBuilder();
                                strsql.AppendFormat("UPDATE WHTWW SET GRNUM='{0}',RMAK1='{1}' WHERE MBLNR='{2}' AND ZEILE='{3}' AND IEFLG ='I'",
                                dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MESSAGE"].ToString().Trim(), dtTAB_ZM025.Rows[i]["PNUM"].ToString().Trim(), dtTAB_ZM025.Rows[i]["PITEM"].ToString().Trim());
                                sqlarr.Add(strsql);
                            }
                            else
                            {
                                StringBuilder strsql = new StringBuilder();
                                strsql.AppendFormat("UPDATE EC_ITEM SET SGTXT='{0}',GDREC='{1}',BKTXT='{2}',GJAHR='{3}',BELNR='{4}',BUZEI='{5}',BUDAT='{6}',UNAME='{7}',MESSAGE=N'{8}' WHERE MANDT='{9}' AND PNUM='{10}' AND PITEM='{11}'",
                                    dtTAB_ZM025.Rows[i]["SGTXT"].ToString().Trim(), dtTAB_ZM025.Rows[i]["GDREC"].ToString().Trim(), dtTAB_ZM025.Rows[i]["BKTXT"].ToString().Trim(),
                                    dtTAB_ZM025.Rows[i]["MJAHR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MBLNR"].ToString().Trim(), dtTAB_ZM025.Rows[i]["ZEILE"].ToString().Trim(), dtTAB_ZM025.Rows[i]["BUDAT"].ToString().Trim(),
                                    dtTAB_ZM025.Rows[i]["UNAME"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MESSAGE"].ToString().Trim(), dtTAB_ZM025.Rows[i]["MANDT"].ToString().Trim(), dtTAB_ZM025.Rows[i]["PNUM"].ToString().Trim(),
                                    dtTAB_ZM025.Rows[i]["PITEM"].ToString().Trim());
                                sqlarr.Add(strsql);
                            }

                        }
                    }

                }
                if (sqlarr.Count > 0)
                {
                    if (objStorageIn.updateZM025(sqlarr))
                    {
                        stsMessage = stsMessage + "\r\n" + drEC["PNUM"].ToString() + "扣账成功，扣账编号为：" + dtTAB_ZM025.Rows[0]["MBLNR"].ToString().Trim();
                    }
                }
                else
                {
                    stsMessage = stsMessage + "\r\n" + drEC["PNUM"].ToString() + "扣账失败，失败原因为：" + dtTAB_ZM025.Rows[0]["MESSAGE"].ToString().Trim();
                    return;
                }

                #endregion
            }
            if (string.IsNullOrEmpty(stsMessage))
            {
                MessageBox.Show("无单据过SAP账");
            }
            else
            {
                MessageBox.Show(stsMessage);
            }
        }
        #endregion;

        private void btnReset_Click(object sender, EventArgs e)
        {
            ReSet();
        }

        private void ReSet()
        {
            strStatus = "N";
            strWerks = string.Empty;
            strLgort = string.Empty;
            strLocat = string.Empty;
            stsWarning.Text = string.Empty;
            item = 0;
            btnSave.Enabled = false;
            btnConfirm.Enabled = true;
            txtLocat.Enabled = false;

            txtLgort.Enabled = true;
            txtLgort.Text = "";
            lblWerks.Text = "";
            txtLocat.Text = "";

            txtEC.Enabled = true;
            txtEC.Text = string.Empty;
            dtLotCode.Rows.Clear();
            dtECHeadSource.Rows.Clear();
            dtECItemSource.Rows.Clear();
            dgvECHead.DataSource = null;
            dgvECItem.DataSource = null;
            dvBox.DataSource = null;
            chkProcessing.Checked = false;
        }

        #endregion

        #region 显示操作
        public void SetChecked(string strLocat)
        {
            for (int i = 0; i < dtLotCode.Rows.Count; i++)
            {
                if (dvBox.Rows[i].Cells["LOCAT"].Value.ToString().ToUpper() == strLocat)
                {
                    dvBox.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        public void ShowConfirmData()
        {
            #region EC单
            DataRow[] drsEC = dtECHeadSource.Select("STATUS='T'");
            foreach (DataRow dr in drsEC)
            {
                for (int i = 0; i < dtECHeadSource.Rows.Count; i++)
                {
                    if (dgvECHead.Rows[i].Cells["PNUM"].Value.Equals(dr["PNUM"].ToString()))
                    {
                        dgvECHead.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
                for (int j = 0; j < dtECItemSource.Rows.Count; j++)
                {
                    if (dgvECItem.Rows[j].Cells["PNUM"].Value.Equals(dr["PNUM"].ToString()))
                    {
                        dgvECItem.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            #endregion
            #region BOXID信息
            DataRow[] drsBOXID = dtLotCode.Select("MENGE>OTQTY");
            foreach(DataRow drBOXID in drsBOXID)
            {
                for (int i = 0; i < dtLotCode.Rows.Count; i++)
                {
                    if (dvBox.Rows[i].Cells["LOCAT"].Value.Equals(drBOXID["LOCAT"].ToString()) && dvBox.Rows[i].Cells["MATNR"].Value.Equals(drBOXID["MATNR"].ToString()) && dvBox.Rows[i].Cells["LIFNR"].Value.Equals(drBOXID["LIFNR"].ToString()))
                    {
                        dvBox.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        btnSave.Enabled = false;
                    }
                }
            }
            #endregion
        }
        #endregion

        public void ShowErrorNumber(string strMatnr,string strVendor)
        {
            for (int i = 0; i < dtECItemSource.Rows.Count; i++)
            {
                if (dgvECItem.Rows[i].Cells["MATNR"].Value.ToString().Equals(strMatnr) && dgvECItem.Rows[i].Cells["LIFNR"].Value.ToString().Equals(strVendor))
                {
                    dgvECItem.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            for (int i = 0; i < dtLotCode.Rows.Count; i++)
            {
                if (dvBox.Rows[i].Cells["MATNR"].Value.ToString().Equals(strMatnr) && dvBox.Rows[i].Cells["LIFNR"].Value.ToString().Equals(strVendor))
                {
                    dvBox.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            if (string.IsNullOrEmpty(strLgort))
            {
                stsWarning.Text = "当前厂区为空，请先输入厂区";
                txtLgort.Enabled = true;
                txtLgort.Focus();

            }
            else if (string.IsNullOrEmpty(strLocat))
            {
                stsWarning.Text = "当前储位为空，请先输入储位数据";
                txtLocat.Enabled = true;
                txtLocat.Focus();
            }
        }

        #region SetNotice
        public void SetErrNotice()
        {
            SoundPlayer sp = new SoundPlayer(Application.StartupPath + @"\Sound\ERROR.wav");
            
            sp.Play();
        }
        public void SetOKNotice()
        {
            SoundPlayer sp = new SoundPlayer(Application.StartupPath + @"\Sound\BIU.wav");
            sp.Play();
        }
        #endregion

        private DataTable CombineDataTableByMatnr(DataTable dtData)
        {
            DataTable dtResult = new DataTable();
            try
            {
                dtResult.Columns.Add("MATNR");
                dtResult.Columns.Add("LIFNR");
                dtResult.Columns.Add("MENGE");

                var query = from row in dtData.AsEnumerable()
                            group row by 
                            new 
                            {
                                p1= row.Field<string>("MATNR"),
                                p2=row.Field<string>("LIFNR"),
                            }into m

                            select new
                            {
                                MATNR = m.Key.p1,
                                LIFNR = m.Key.p2,
                                MENGE = m.Sum(n => n.Field<int>("MENGE"))
                            };
                foreach (var item in query)
                {
                    DataRow drResult = dtResult.NewRow();
                    drResult["MATNR"] = item.MATNR;
                    drResult["LIFNR"] = item.LIFNR;
                    drResult["MENGE"] = item.MENGE;
                    dtResult.Rows.Add(drResult.ItemArray);
                }
            }
            catch (Exception e)
            {
                stsWarning.Text = dtData.TableName + "Table转化异常";
            }

            return dtResult;
        }

        #region 选取仓别
        private void txtLgort_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                if (e.KeyChar == (char)13)
                {
                    DataTable dtLgort = objAdmin.PermissionQuery("218", Comcd, strWerks, strUsrnm);
                    if(dtLgort.Rows[0]["LGORT"].ToString().Contains(txtLgort.Text.ToString().Trim().ToUpper()))
                    {
                        strLgort = txtLgort.Text.ToString().Trim().ToUpper();
                        for (int i = 0; i < dtECItemSource.Rows.Count; i++)
                        {
                            dtECItemSource.Rows[i]["LGORT"] = strLgort;
                        }
                        if (objStorageIn.updateTECItem(dtECItemSource))
                        {
                            stsWarning.Text = "EC单信息保存成功";
                            getShowEcItem();
                            txtLgort.Enabled = false;
                        }
                        else
                        {
                            stsWarning.Text = "请重新key入仓别";
                            txtLgort.Text = string.Empty;
                            SetErrNotice();
                            return;
                        }                        
                    }
                    else
                    {
                        SetErrNotice();
                        MessageBox.Show("您没有当前仓别的操作权限！");
                        txtLgort.Text = string.Empty;
                        return;
                    }
                    if(!string.IsNullOrEmpty(strLgort))
                    {
                        SetOKNotice();
                        txtLocat.Enabled = true;
                        txtLocat.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
            }
        }
        #endregion

        #region 选取储位
        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, strWerks, strLgort, "NEW");
                    objStorageIn_LocationSelect.ShowDialog();
                    strLocat = objStorageIn_LocationSelect.Locat;
                    txtLocat.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                if (e.KeyChar == (char)13)
                {
                    if (strWerks == "" || strLgort == "" || txtLocat.Text.ToString().ToUpper()=="")
                    {
                        stsWarning.Text = "Plant and storage and location can't be empty!!";
                        return;
                    }
                    if (txtLocat.Text.ToString().ToUpper().Replace("；", ";").Contains(";"))
                    {
                        SetErrNotice();
                        stsWarning.Text = "输入储位不正确";
                        txtLocat.Text = string.Empty;
                        txtLocat.Focus();
                        return;
                    }
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);
                    #region 判断该储位是否是QWMS储位
                    if(!objPlantData.CheckExistedStorageData(strWerks,strLgort,txtLocat.Text.ToString().ToUpper()))
                    {
                        SoundPlayer sp = new SoundPlayer(Application.StartupPath + @"\Sound\ERROR.wav");
                        sp.Play();
                        txtLocat.Text = string.Empty;
                        txtLocat.Focus();
                        stsWarning.Text = "该储位不存在！！！";
                        return;
                    }
                    #endregion
                    strLocat = txtLocat.Text.ToString().ToUpper();
                    //txtLocat.Text = string.Empty;
                    txtLocat.Enabled = false;
                    SetOKNotice();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
            }
        }

        #endregion

        private void dgvECItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < (dgvECItem.Rows.Count); i++)
            {
               # region 特殊料头的DateCode材料入DateCode仓别
                //if (dgvECItem.Rows[i].Cells["AEC"].Value.Equals("AECS"))
                //{
                //    if(!objStorageData.CheckStorageInType(strWerks, dgvECItem.Rows[i].Cells["LGORT"].Value.ToString(),"Diff Vendor Diff Locat"))
                //    {
                //        MessageBox.Show("DateCode材料需入DateCode仓别");
                //        return;
                //    }
                //}
                dtECItemSource.Rows[i]["LGORT"] = dgvECItem.Rows[i].Cells["LGORT"].Value.ToString();
                #endregion
            }
        }

        private void dgvECHead_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < (dgvECHead.Rows.Count); i++)
            {
                if(dgvECHead.Rows[i].Cells["PNUM"]==dtECHeadSource.Rows[i]["PNUM"])
                {
                    dtECHeadSource.Rows[i]["BKTXT"] = dgvECHead.Rows[i].Cells["BKTXT"].ToString();
                }
            }

        }

        #region EC单和BOXID匹配
        private bool matchBox() //入库数据以dtstorage为主
        {
            string strMessage = string.Empty;
            //int count = dtECHeadSource.AsEnumerable().Where(x => x.Field<string>("STATUS").Equals("P")).Count();
            //if (count == 0)
            //    return false;
            foreach (DataRow drEC in dtECHeadSource.Rows)
            {
                #region dtLotCode和ECitem匹配
                if (!drEC["STATUS"].Equals("T")) //问题单据不匹配
                {
                    DataRow[] drEcItems = dtECItemSource.Select("PNUM='" + drEC["PNUM"].ToString() + "' AND MENGEC<MENGE");
                    foreach(DataRow drEcItem in drEcItems)
                    {
                        int sum = int.Parse(drEcItem["MENGE"].ToString()) - int.Parse(drEcItem["MENGEC"].ToString());//需求数量
                        DataRow[] drBoxIDS = dtLotCode.Select("MATNR='" + drEcItem["MATNR"].ToString() + "' AND MENGE>OTQTY AND LIFNR='" + drEcItem["LIFNR"].ToString() + "' ", "MENGE ASC");
                        foreach (DataRow drBoxID in drBoxIDS)
                        {
                            string strStorageLocat = drBoxID["LOCAT"].ToString();
                            int Alqty = int.Parse(drBoxID["MENGE"].ToString()) - int.Parse(drBoxID["OTQTY"].ToString());//待处理数量
                            int Otqty = 0;

                            #region 判断已处理数量
                            if (sum>=Alqty)
                            {
                                drEcItem["MENGEC"] = int.Parse(drEcItem["MENGEC"].ToString()) + Alqty;
                                drBoxID["OTQTY"] = int.Parse(drBoxID["OTQTY"].ToString()) + Alqty;
                                drBoxID["LGORT"] = drEcItem["LGORT"].ToString();
                                if (!drBoxID["REMAK"].ToString().Contains(drEC["PNUM"].ToString()))
                                {
                                    drBoxID["REMAK"] = ";" + drEC["PNUM"].ToString();
                                }
                                sum = sum - Alqty;
                                Otqty = Alqty;//已处理数量
                            }
                            else
                            {
                                drEcItem["MENGEC"] = int.Parse(drEcItem["MENGEC"].ToString()) + sum;
                                drBoxID["OTQTY"] = int.Parse(drBoxID["OTQTY"].ToString()) + sum;
                                drBoxID["LGORT"] = drEcItem["LGORT"].ToString(); 
                                if (!drBoxID["REMAK"].ToString().Contains(drEC["PNUM"].ToString()))
                                {
                                    drBoxID["REMAK"] = ";" + drEC["PNUM"].ToString();
                                }
                                Otqty = sum;//已处理数量
                                sum = 0;
                            }
                            #endregion

                            #region EC单储位修改
                            if (string.IsNullOrEmpty(drEcItem["LOCAT"].ToString()))
                            {
                                drEcItem["LOCAT"] = strStorageLocat;
                            }
                            else if (!drEcItem["LOCAT"].ToString().Contains(strStorageLocat))
                            {
                                drEcItem["LOCAT"] = drEcItem["LOCAT"] + ";" + strStorageLocat;
                            }
                            #endregion

                            #region 若已存在该储位该料号，该EC单，则并入相同入库数据数量,否则重新增加一条入库数据dtStorage
                            DataRow[] drM = dtStorage.Select("LGORT='" + drEcItem["LGORT"].ToString() + "' AND PNUM='" + drEcItem["PNUM"].ToString() + "' AND MATNR='" + drEcItem["MATNR"].ToString() + "'  AND LOCAT='" + strStorageLocat + "' ");
                            if (drM.Length > 0)
                            {
                                DataRow drExist = drM[0];
                                int CombineMenge = int.Parse(drExist["MENGE"].ToString());
                                drExist["MENGE"] = CombineMenge + Otqty;
                                drExist["ALQTY"] = CombineMenge + Otqty;
                            }
                            else
                            {
                                DataRow drStorage = dtStorage.NewRow();
                                drStorage["MANDT"] = Mandt;
                                drStorage["COMCD"] = Comcd;
                                drStorage["WERKS"] = strWerks;
                                drStorage["LGORT"] = txtLgort.Text;
                                drStorage["LOCAT"] = strStorageLocat;
                                drStorage["PNUM"] = drEcItem["PNUM"].ToString();
                                drStorage["MBLNR"] = "";
                                if (strTWWNOFlag == "DocNO")
                                {
                                    drStorage["INSMK"] = "G";
                                }
                                else
                                {
                                    drStorage["INSMK"] = "0";
                                }
                                drStorage["MATNR"] = drEcItem["MATNR"].ToString();
                                drStorage["DACOD"] = drBoxID["VEDAT"].ToString();
                                drStorage["LIFNR"] = drBoxID["LIFNR"].ToString();
                                drStorage["LOCOD"] = drBoxID["LOCOD"].ToString();
                                drStorage["MENGE"] = Otqty;
                                drStorage["ALQTY"] = Otqty;
                                drStorage["VEDAT"] = drBoxID["DACOD"].ToString();
                                drStorage["INDAT"] = DateTime.Now.ToString("yyyyMMdd");
                                drStorage["RMAK1"] = "StorageInEC";
                                dtStorage.Rows.Add(drStorage);
                            }
                            if(sum==0)
                            {
                                break;
                            }
                            #endregion
                        }

                    }

                    #region 保存EC单数据
                    //DataRow[] drECItem = dtECItemSource.Select("PNUM='" + drEC["PNUM"].ToString() + "'");
                    if (objStorageIn.updateECItem(drEcItems))
                    {
                        objLogData.AddQWMSLOG(drEC["PNUM"].ToString(), "EC", "Confirm", "匹配成功", "N", Usrnm);
                        strMessage += "\r\n" + drEC["PNUM"].ToString() + ":EC单匹配完成";
                    }
                    else
                    {
                        strMessage += "\r\n" + drEC["PNUM"].ToString() + ":EC单未匹配完成，请重新操作";
                        return false;
                    }
                    #endregion
                }
                #endregion
            }
            if(!string.IsNullOrEmpty(strMessage))
            {
                MessageBox.Show(strMessage);
            }
            return true;

        }
        #endregion

        private void chkProcessing_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if(chkProcessing.Checked)
            {
                strStatus = "P";
            }
            else
            {
                strStatus = "N";
            }
            txtEC.Focus();
        }

        private void dvBox_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            stsWarning.Text = string.Empty;
            int Alqty = int.Parse(dvBox.Rows[e.RowIndex].Cells["MENGE"].Value.ToString());
            string strLocat = dvBox.Rows[e.RowIndex].Cells["LOCAT"].Value.ToString();
            string strMatnr = dvBox.Rows[e.RowIndex].Cells["MATNR"].Value.ToString();
            string strBoxItem = dvBox.Rows[e.RowIndex].Cells["BOXITEM"].Value.ToString();
            string strLifnr = dvBox.Rows[e.RowIndex].Cells["LIFNR"].Value.ToString();
            DataRow[] dr = dtECItemSource.Select("MATNR='" + strMatnr + "'");
            if (dr[0]["CHECKSTATS"].Equals("Y") && (!strLocat.ToUpper().Substring(0, 2).Equals("DY")))
            {
                MessageBox.Show("该料号为待验材料，请使用待验储位");
                return;
            }
            else
            {
                if (objStorageIn.updateTMatbox(strECNo, strBoxItem, strLocat, strMatnr, Alqty, strLifnr))
                {
                    stsWarning.Text = "更新成功";
                    getShowBox();
                }
            }
        }

        private void dvBox_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.RowIndex != -1)
            {
                foreach(DataRow dr in dtECHeadSource.Rows)
                {
                    if(dr["STATUS"].ToString().Equals("Y"))
                    {
                        stsWarning.Text = "已有EC单完成扣账，不可更改BOXID数据";
                        e.Cancel = true;
                        return;
                    }
                }
                //if(!InitEC())
                //{
                //    return;
                //}
                btnConfirm.Enabled = true;
                btnSave.Enabled = false;
            }
        }

    }
}
