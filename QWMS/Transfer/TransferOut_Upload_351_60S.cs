using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace QWMS
{
    public partial class TransferOut_Upload_351_60S : Form
    {
        UserInfo UserData = new UserInfo();
        private string Mandt = "";
        private string Comcd = "";
        private string Usrnm = "";
        private string strType = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strmblnr = "";
        private string strlocat = "";
        private string Progid = "";
        private bool Savelock = false;//生成文档时加锁

        private DataTable dtsap = new DataTable(); //DGV数据源
        private DataTable dtqwms = new DataTable();  //DGV数据源
        private DataTable dtfile = new DataTable(); //读取文件内容
        private DataSet dsrec = new DataSet();//扣账回执
        private DataTable dtcheck = new DataTable();  //检查同一po是否多个厂区



        private Authority objAuthority;
        private PlantData objPlantData;
        private Replenishment objReplenishment;
        private Transfer objTransfer;
        private LogData objLogData;


        public TransferOut_Upload_351_60S(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);
                objTransfer = new Transfer(UserData, strWerks, strLgort);
                objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);

                //检查权限
                if (!objReplenishment.CheckAuthority())
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
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #region ShowData Status/Werks/Lgort
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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
                if (strType == "SAP_60S")
                {
                    //cmbLgort.Items.Clear();
                    if (!cmbLgort.Items.Contains("FGTM"))
                    {
                        cmbLgort.Items.Add("FGTM");
                    }
                }
                else if (dtTemp.Rows.Count == 0)
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

        private void cmbWerks_SelectedValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ////if (cmbWerks.Items[cmbWerks.SelectedIndex].ToString() == "CS1A")
            ////{
            ////    stsWarning.Text = "调出厂区错误,不能从CS1A调出";
            ////    return;
            ////}
            ShowDdlLgort();
            cmbLgort.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            strType = "";
            btnQuary.Enabled = true;
            btnFile.Enabled = true;
            btnConfirm.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            rb351.Checked = false;
            rb60S.Checked = false;
            strWerks = "";
            strLgort = "";
            stsWarning.Text = "";
            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            gbType.Enabled = true;
            txtmblnr.Text = "";
            txtlocat.Text = "";
            txtFile.Text = "";
            Savelock = false;
            txtmblnr.Enabled = true;
            txtlocat.Enabled = true;
            dsrec.Clear();

            this.gvData_sap.Columns.Clear();
            this.gvData_qwms.Columns.Clear();
        }

        private void btnQuary_Click(object sender, EventArgs e)
        {

            btnSave.Enabled = false;
            txtlocat.Enabled = false;
            btnFile.Enabled = false;
            stsWarning.Text = "";
            if (strType == "")
            {
                stsWarning.Text = "请选择类型!";
                return;
            }
            DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-1")).AddHours(-4);
            DateTime Daynow = Convert.ToDateTime(DateTime.Now.ToString());
            //351开调拨单 当月最后一天20点以后添加弹窗提醒
            if (strType == "SAP_351")
            {
                if (Daynow > lastDay)
                {
                    DialogResult result;
                    result = MessageBox.Show("今日月结，请确认是否开单", "提示!", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }
            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            strmblnr = txtmblnr.Text;
            strlocat = txtlocat.Text;
            if (strmblnr == "")
            {
                stsWarning.Text = "请输入单号";
                return;
            }
            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "请选择厂区和仓别";
                return;
            }
            //页面卡控厂区,不允许调出
            if(strType== "SAP_351")
            { 
                if (objAuthority.CheckWerksSelectable(strWerks).Rows.Count > 0)
                {
                    stsWarning.Text = "调出厂区错误,不能从" + strWerks + "调出";
                    return;
                }
            }
            cmbWerks.Enabled = false;
            cmbLgort.Enabled = false;
            dtsap.Clear();
            dtsap = objTransfer.Query46Pdata(strWerks, strLgort, strlocat, strmblnr, strType);
            if (dtsap.Rows.Count == 0)
            {
                stsWarning.Text = "No Data!";
                return;
            }

            if (strType == "SAP_351")
            {
                #region 自动调整数量
                string item = dtsap.Rows[0]["ZEILE"].ToString();
                int menge = Convert.ToInt32(dtsap.Rows[0]["MENGE"].ToString());
                for (int i = 0; i < dtsap.Rows.Count; i++)
                {
                    DataRow dr = dtsap.Rows[i];
                    if (menge == 0 && item == dr["ZEILE"].ToString())
                    {
                        dtsap.Rows.Remove(dr);
                        i--;
                        continue;
                    }
                    if (item != dr["ZEILE"].ToString())
                    {
                        item = dr["ZEILE"].ToString();
                        menge = Convert.ToInt32(dr["MENGE"].ToString());
                    }
                    if (Convert.ToInt32(dr["OTQTY"].ToString()) >= menge)
                    {
                        dr["OTQTY"] = menge.ToString();
                        menge = 0;
                    }
                    else if (Convert.ToInt32(dr["OTQTY"].ToString()) < menge && Convert.ToInt32(dr["OTQTY"].ToString()) >= 0)
                    {
                        menge = menge - Convert.ToInt32(dr["OTQTY"].ToString());
                    }
                    else
                    {
                        stsWarning.Text = strWerks + "  " + strLgort + "  " + strlocat + "中有料号库存为负数，请联系QWMS负责人！";
                        return;
                    }
                    if (dr["LGORT"].ToString() != "" && dr["LGORT"].ToString() != strLgort)
                    {
                        MessageBox.Show("该PO的" + dr["ZEILE"].ToString() + "Item的自带仓别与所选仓别不同！");
                    }
                    dr["LGORT"] = strLgort;
                }
                #endregion
            }
            if (strType == "SAP_60S")
            {
                #region 检查重量是否维护
                foreach (DataRow dr in dtsap.Rows)
                {
                    if (dr["WSTATE"].ToString() == "N")
                    {
                        DialogResult result;
                        result = MessageBox.Show(this, "Item" + dr["ZEILE"].ToString() + "的净重未维护,是否继续？", "Warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                #endregion
            }

            txtmblnr.Enabled = false;
            btnQuary.Enabled = false;
            ShowDataView_sap();
            AutoSelect_Sap();
            btnConfirm.Enabled = true;
        }

        private void ShowDataView_sap()
        {
            try
            {
                this.gvData_sap.AutoGenerateColumns = false;
                this.gvData_sap.Columns.Clear();

                if (!dtsap.Columns.Contains("Select"))
                {
                    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                    dtsap.Columns.Add(cSelect);
                }

                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "选择";
                dgvcSelect.Width = 50;
                dgvcSelect.Selected = false;
                this.gvData_sap.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.ReadOnly = true;
                this.gvData_sap.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "Item";
                dgvcZEILE.ReadOnly = true;
                this.gvData_sap.Columns.Add(dgvcZEILE);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "调出厂区";
                dgvcWERKS.ReadOnly = true;
                this.gvData_sap.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "调出仓别";
                dgvcLGORT.ReadOnly = true;
                this.gvData_sap.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.ReadOnly = true;
                this.gvData_sap.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.ReadOnly = false;
                dgvcCHARG.Width = 80;
                this.gvData_sap.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.ReadOnly = true;
                this.gvData_sap.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcOTQTY = new DataGridViewTextBoxColumn();
                dgvcOTQTY.DataPropertyName = "OTQTY";
                dgvcOTQTY.HeaderText = "调出数量";
                dgvcOTQTY.ReadOnly = false;
                this.gvData_sap.Columns.Add(dgvcOTQTY);

                DataGridViewTextBoxColumn dgvcKOSTL = new DataGridViewTextBoxColumn();
                dgvcKOSTL.DataPropertyName = "KOSTL";
                dgvcKOSTL.HeaderText = "调入厂区";
                dgvcKOSTL.ReadOnly = false;
                this.gvData_sap.Columns.Add(dgvcKOSTL);

                DataGridViewTextBoxColumn dgvcUMLGO = new DataGridViewTextBoxColumn();
                dgvcUMLGO.DataPropertyName = "UMLGO";
                dgvcUMLGO.HeaderText = "调入仓别";
                dgvcUMLGO.ReadOnly = false;
                this.gvData_sap.Columns.Add(dgvcUMLGO);

                if (strType == "SAP_60S")
                {
                    //60S不允许修改数据
                    dgvcCHARG.ReadOnly = true;
                    dgvcOTQTY.ReadOnly = true;
                    dgvcKOSTL.ReadOnly = true;
                    dgvcUMLGO.ReadOnly = true;

                    DataGridViewTextBoxColumn dgvcWSTATE = new DataGridViewTextBoxColumn();
                    dgvcWSTATE.DataPropertyName = "WSTATE";
                    dgvcWSTATE.HeaderText = "Weight State";
                    dgvcWSTATE.ReadOnly = true;
                    dgvcWSTATE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//居中
                    this.gvData_sap.Columns.Add(dgvcWSTATE);
                }

                this.gvData_sap.DataSource = dtsap;
                gvData_sap.ClearSelection();
                gvData_sap.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView_sap()");
            }
        }

        private void ShowDataView_QWMS()
        {
            try
            {
                this.gvData_qwms.AutoGenerateColumns = false;
                this.gvData_qwms.Columns.Clear();

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "Item";
                dgvcZEILE.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcZEILE);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "调出厂区";
                dgvcWERKS.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "调出仓别";
                dgvcLGORT.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.ReadOnly = true;
                dgvcCHARG.Width = 80;
                this.gvData_qwms.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcOTQTY = new DataGridViewTextBoxColumn();
                dgvcOTQTY.DataPropertyName = "OTQTY";
                dgvcOTQTY.HeaderText = "扣账数量";
                dgvcOTQTY.ReadOnly = false;
                this.gvData_qwms.Columns.Add(dgvcOTQTY);

                DataGridViewTextBoxColumn dgvcDWERKS = new DataGridViewTextBoxColumn();
                dgvcDWERKS.DataPropertyName = "DWERKS";
                dgvcDWERKS.HeaderText = "调入厂区";
                dgvcDWERKS.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcDWERKS);

                DataGridViewTextBoxColumn dgvcDLGORT = new DataGridViewTextBoxColumn();
                dgvcDLGORT.DataPropertyName = "DLGORT";
                dgvcDLGORT.HeaderText = "调入仓别";
                dgvcDLGORT.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcDLGORT);

                DataGridViewTextBoxColumn dgvcREMAK1 = new DataGridViewTextBoxColumn();
                dgvcREMAK1.DataPropertyName = "REMAK1";
                dgvcREMAK1.HeaderText = "扣账信息";
                dgvcREMAK1.ReadOnly = true;
                this.gvData_qwms.Columns.Add(dgvcREMAK1);

                this.gvData_qwms.DataSource = dtqwms;
                gvData_qwms.ClearSelection();
                gvData_qwms.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView_QWMS()");
            }
        }

        private void AutoSelect_Sap()
        {
            foreach (DataRow drsap in dtsap.Rows)
            {
                if (Convert.ToInt32(drsap["OTQTY"].ToString()) != 0)
                {
                    drsap["Select"] = true;
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (strType == "")
            {
                stsWarning.Text = "请选择类型!";
                return;
            }
            DataTable dtnew = (DataTable)gvData_sap.DataSource;
            DataRow[] drSelect = dtnew.Select(" Select='True' ");
            DataTable dtData = new DataTable();
            dtData.Columns.Clear();
            dtData.Columns.Add("MBLNR");
            dtData.Columns.Add("ZEILE");
            dtData.Columns.Add("WERKS");
            dtData.Columns.Add("LGORT");
            dtData.Columns.Add("MATNR");
            dtData.Columns.Add("CHARG");
            dtData.Columns.Add("MANGE");
            dtData.Columns.Add("OTQTY");
            dtData.Columns.Add("KOSTL");
            dtData.Columns.Add("UMLGO");
            dtData.Columns.Add("Select");
            if (dtnew.Rows.Count == 0 || drSelect.Length <= 0)
            {
                stsWarning.Text = "The data can't be empty and you must selected more than one data !!";
                return;
            }
            if (strType == "SAP_351")
            {
                #region  检查数据是否正确
                string Item = drSelect[0]["ZEILE"].ToString();
                int Menge = Convert.ToInt32(drSelect[0]["MENGE"]);
                string Kostl = drSelect[0]["KOSTL"].ToString();
                foreach (DataRow dr in drSelect)
                {
                    try
                    {
                        #region 检查调入厂区和调入仓别格式
                        if (dr["KOSTL"].ToString().Trim().Length != 4 || dr["KOSTL"].ToString().Length != 4 || (dr["UMLGO"].ToString().Length != 4 && dr["UMLGO"].ToString().Length != 0))
                        {
                            stsWarning.Text = "请正确填写调入厂区和仓别！";
                            return;
                        }
                        #endregion

                        #region 检查调入厂区是否一致且不能和调出厂区相同
                        if (dr["KOSTL"].ToString() == dr["WERKS"].ToString() || dr["KOSTL"].ToString() != Kostl)
                        {
                            stsWarning.Text = "调入厂区错误";
                            return;
                        }
                        #endregion

                        #region 检查仓别是否存在（未使用）
                        //DataTable dtlgort = new DataTable();

                        //dtlgort = objPlantData.CheckLgort(dr["KOSTL"].ToString(), dr["UMLGO"].ToString(), Usrnm);

                        //if (dtlgort.Rows.Count <= 0)
                        //{
                        //    stsWarning.Text = "调入仓不存在，请重试！";
                        //    return;
                        //}
                        #endregion

                        #region 校验特殊厂对应规则 CS1A
                        //DataTable dtResult = objTransfer.CheckLgort(strType, dr["LGORT"].ToString(), dr["UMLGO"].ToString());
                        //if (dtResult.Rows[0][0].ToString().Equals("N"))
                        //{
                        //    stsWarning.Text = "调出仓和接收仓不匹配，请重新选择！！！";
                        //    return;
                        //}

                        //if (dr["KOSTL"].ToString() == "CS1A" )
                        //{
                        //    stsWarning.Text = "调入厂区错误,不能调入CS1A";
                        //    return;
                        //}
                        #endregion


                        #region 351调入厂区卡控
                        if (strType == "SAP_351")
                        {
                            if (objAuthority.CheckWerksTransIn(Kostl).Rows.Count > 0)
                            {
                                stsWarning.Text = "调入厂区错误,不能调入" + Kostl ;
                                return;
                            }
                            string strUMLGO = objAuthority.CheckTransLgort(dr["LGORT"].ToString());
                            if(!string.IsNullOrEmpty(strUMLGO))
                            {
                                if (!objAuthority.CheckLgortExist(Kostl, strUMLGO))
                                {
                                    stsWarning.Text = "该PO调入厂区" + Kostl + "无对应接收仓，请确认！";
                                    return;
                                }
                            }
                        }
                        #endregion


                        #region 检查调出数量是否合理
                        if (Item != dr["ZEILE"].ToString())
                        {
                            Item = dr["ZEILE"].ToString();
                            Menge = Convert.ToInt32(dr["MENGE"]);
                        }
                        int Otqty = Convert.ToInt32(dr["OTQTY"].ToString().Trim());
                        if (Menge < Otqty || Otqty == 0)
                        {
                            stsWarning.Text = "Item" + dr["ZEILE"].ToString() + "   料号" + dr["MATNR"].ToString() + "   版本" + dr["CHARG"].ToString() + "  调出数量有误!";
                            return;
                        }
                        Menge = Menge - Otqty;

                        #endregion

                        #region 检查料号是否存在
                        //DataTable dtmatnr = new DataTable();
                        //dtmatnr = objTransfer.returnmatnrexists(dr["MATNR"].ToString(), dr["CHARG"].ToString(), dr["WERKS"].ToString(), dr["LGORT"].ToString());
                        //if (dtmatnr.Rows.Count == 0)
                        //{
                        //    stsWarning.Text = "Item" + dr["ZEILE"].ToString() + "   料号" + dr["MATNR"].ToString() + "   版本" + dr["CHARG"].ToString() + "  不存在";
                        //    return;
                        //}
                        #endregion

                        DataRow drTarget = dtData.NewRow();
                        drTarget.ItemArray = dr.ItemArray;
                        dtData.Rows.Add(drTarget);
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = ex.Message;
                        return;
                    }
                }
                #endregion

                dtqwms = objTransfer.insert46Pdata(dtData, Usrnm);

                dtcheck.Clear();
                dtcheck = dtqwms;
                

                #region 对比库存
                DataTable dtQueryQwmsStorage = new DataTable();
                dtQueryQwmsStorage = objTransfer.GetQwmsStorage(dtqwms.Rows[0]["MBLNR"].ToString(), strType);
                if (dtQueryQwmsStorage.Rows.Count != 0)
                {
                    stsWarning.Text = "QWMS库存不足";
                    DialogResult result;
                    result = MessageBox.Show(this, "单据已产生，QWMS库存不足,是否继续？", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                #endregion
            }
            else if (strType == "SAP_60S")
            {
                dtqwms = objTransfer.Query46Pdata(strWerks, strLgort, "", strmblnr, "SAP_60S2");
            }

            if (dtqwms.Rows.Count == 0)
            {
                stsWarning.Text = "NO Data";
                return;
            }
            btnConfirm.Enabled = false;
            btnSave.Enabled = true;
            ShowDataView_QWMS();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (strType == "")
            {
                stsWarning.Text = "请选择类型!";
                return;
            }
            if (strType == "SAP_351")
            {
                string chmblnr = dtcheck.Rows[0]["MBLNR"].ToString();
                dtcheck = objTransfer.CheckDIffWerks(chmblnr);
                bool hasDifferentWerks = dtcheck.AsEnumerable().Select(row => row.Field<string>("WERKS")).Distinct().Count() > 1;

                if (hasDifferentWerks)
                {
                    MessageBox.Show("同一调拨单不能存在多个厂区！！");
                    return;
                }
                #region 对比库存
                DataTable dtQueryQwmsStorage = new DataTable();
                dtQueryQwmsStorage = objTransfer.GetQwmsStorage(dtqwms.Rows[0]["MBLNR"].ToString(), strType);
                if (dtQueryQwmsStorage.Rows.Count != 0)
                {
                    stsWarning.Text = "QWMS库存不足";
                    DialogResult result;
                    result = MessageBox.Show(this, "QWMS库存不足,是否继续？", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                #endregion
            }
            if (strType == "SAP_60S")
            {
                #region 检查重量是否维护
                foreach (DataRow dr in dtsap.Rows)
                {
                    if (dr["WSTATE"].ToString() == "N")
                    {
                        DialogResult result;
                        result = MessageBox.Show(this, "Item" + dr["ZEILE"].ToString() + "的净重未维护,是否继续？", "Warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
                #endregion
            }
            if (Savelock)//加一个变量，防止用户快速点击而生成两个文档导致扣账失败。
            {
                return;
            }
            try
            {
                #region 去SAP扣账
                DataTable dtSap = new DataTable();
                btnSave.Enabled = false;
                Savelock = true;
                dtSap = objTransfer.GetSapData(dtqwms.Rows[0]["MBLNR"].ToString(), strType);
                dtSap = objTransfer.GetSapData(dtqwms.Rows[0]["MBLNR"].ToString(), strType);
                //获取DTSAP条数，获取需要调拨的条数，进行效验，并记录日志
                #region 效验dtSap条数
                try
                {
                    DataTable dtQwmstoSap = (DataTable)gvData_qwms.DataSource;
                    if (dtSap.Rows.Count != dtQwmstoSap.Rows.Count)
                    {
                        btnSave.Enabled = true;
                        Savelock = false;
                        objTransfer.insertQWMS_LOG(dtqwms.Rows[0]["MBLNR"].ToString(), dtSap.Rows.Count.ToString(), dtQwmstoSap.Rows.Count.ToString(),"N");
                        MessageBox.Show("去SAP扣账的文档条数" + dtSap.Rows.Count + ",与实际不符，请重新save扣账");
                        return;
                    }
                    objTransfer.insertQWMS_LOG(dtqwms.Rows[0]["MBLNR"].ToString(), dtSap.Rows.Count.ToString(), dtQwmstoSap.Rows.Count.ToString(),"Y");
                }
                catch (Exception ex)
                {

                }
                #endregion
                stsWarning.Text = "扣账中,请稍等。";
                if (dtSap.Rows.Count > 0)
                {
                    DataSet ds = new DataSet();
                    DataTable dscopy = dtSap.Copy();
                    ds.Tables.Add(dscopy);
                    dsrec = SendToSAP(strType.Substring(strType.Length - 3, 3), ds);
                    objTransfer.UpdateTransRemark(dsrec, dtqwms.Rows[0]["MBLNR"].ToString());

                    if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "Y")
                    {
                        stsWarning.Text = dtqwms.Rows[0]["MBLNR"].ToString() + "扣账成功";
                        #region 更新结果在界面
                        foreach (DataRow drqwms in dtqwms.Rows)
                        {
                            drqwms["OTQTY"] = drqwms["MENGE"];
                            drqwms["REMAK1"] = dsrec.Tables["EP_MBLNR"].Rows[0]["MBLNR"].ToString().Trim();
                        }
                        #endregion
                        MessageBox.Show(dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString());
                        btnPrint.Enabled = true;
                    }
                    else if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "N")
                    {
                        stsWarning.Text = "扣账失败";
                        #region 更新结果在界面
                        foreach (DataRow drqwms in dtqwms.Rows)
                        {
                            drqwms["REMAK1"] = dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString();
                        }
                        #endregion
                        MessageBox.Show(dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString());
                    }

                }
                Savelock = false;
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("去SAP扣账异常:"+ex.ToString());
            }

        }

        public DataSet SendToSAP(string varTypeToSap, DataSet ds)
        {
            try
            {
                DataSet dsResult = new DataSet();
                MM.MM_Service obj = new QWMS.MM.MM_Service();
                dsResult = obj.Z_MM_RFC_DIAOBO_QWMS_TO_SAP(varTypeToSap, ds);
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region  Type
        private void rb351_CheckedChanged(object sender, EventArgs e)
        {
            strType = "SAP_351";
            label1.Text = "    PO:";
            gbType.Enabled = false;
            txtlocat.Enabled = true;
        }

        private void rb60S_CheckedChanged(object sender, EventArgs e)
        {
            strType = "SAP_60S";
            label1.Text = "Delivery:";
            gbType.Enabled = false;
            txtlocat.Enabled = false;
            btnFile.Enabled = false;
            btnImport.Enabled = false;
        }
        #endregion

        private void txtlocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (strType == "")
                {
                    stsWarning.Text = "请选择类型";
                    return;
                }
                stsWarning.Text = "";
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                strmblnr = txtmblnr.Text;
                if (strmblnr == "")
                {
                    stsWarning.Text = "请先输入46PO单号！";
                    return;
                }

                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "请选择厂区仓别";
                    return;
                }
                else
                {
                    Transfer_LocationSelectBymblnr objTransfer_LocationSelectBymblnr = new Transfer_LocationSelectBymblnr(UserData, Progid, strWerks, strLgort, strmblnr);
                    objTransfer_LocationSelectBymblnr.ShowDialog();
                    txtlocat.Text = objTransfer_LocationSelectBymblnr.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message + "<-txtLocat_DoubleClick";
                return;
            }
        }

        private void lnkSample_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "351调拨模板.xlsx";
                try
                {
                    Process.Start("Excel", strPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"无法打开文件，请手动打开" + strPath);
                }
            }
            else
            {
                MessageBox.Show("未在数据库维护模板路径，请联系QWMS负责人");
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFile.Text = ofdOpenFile.FileName;
                btnImport.Enabled = true;
                btnConfirm.Enabled = true;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (strType == "")
            {
                stsWarning.Text = "请选择类型";
                return;
            }
            //if (dtsap.Rows.Count == 0)
            //{
            //    stsWarning.Text = "请先输入SAP的单据并点击查询";
            //    return;
            //}
            dtfile.Clear();
            if (string.IsNullOrEmpty(this.txtFile.Text.Trim()))
            {
                stsWarning.Text = "Please select one file!!";
                return;
            }
            # region 校验格式
            string strFileName = this.txtFile.Text.Trim();
            string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

            if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
            {
                stsWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
            }
            # endregion

            stsWarning.Text = "Please don't close the window ,Check the data...";
            QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
            // string strCmd = "select * from [Sheet1$]";
            // dtfile = objExcel.ExcelQuery(this.txtFile.Text.Trim(), strCmd);
            # region 获取EXCEL数据
            dtfile = objExcel.GetDataTableFromExcel(strFileName, true);
            #endregion
            if (dtfile.Rows.Count == 0)
            {
                stsWarning.Text = "请填写正确的文件";
                return;
            }
            //排序
            DataView dvfile = dtfile.DefaultView;
            dvfile.Sort = "PO,料号,版本";
            dtfile = dvfile.ToTable();

            #region 检查读取文件的内容
            string PO = dtfile.Rows[0]["PO"].ToString();
            string Matnr = "";
            string Charg = "";
            string fwerks = dtfile.Rows[0]["调出厂"].ToString();
            string flgort = dtfile.Rows[0]["调出仓"].ToString();
            string dwerks = dtfile.Rows[0]["接收厂"].ToString();
            foreach (DataRow dr in dtfile.Rows)
            {
                #region 是否有空值
                if (string.IsNullOrEmpty(dr["PO"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["料号"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["数量"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["调出厂"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["调出仓"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["接收厂"].ToString().Trim()))
                {
                    stsWarning.Text = "请编写正确的文件！";
                    return;
                }
                #endregion

                #region 确保数量中是数字并不为0
                try
                {
                    if (Convert.ToInt32(dr["数量"].ToString().Trim()) <= 0)
                    {
                        stsWarning.Text = "数量不能为0或小于0";
                        return;
                    }
                }
                catch (Exception)
                {
                    stsWarning.Text = "数量栏请填入数字";
                    return;
                }

                #endregion

                #region 相同信息是否合并
                if (Matnr == dr["料号"].ToString() && Charg == dr["版本"].ToString())
                {
                    stsWarning.Text = "相同料号，相同版本请自行合并成一行。";
                    return;
                }
                Matnr = dr["料号"].ToString();
                Charg = dr["版本"].ToString();
                #endregion
                if (PO != dr["PO"].ToString())
                {
                    stsWarning.Text = "批量上传只能是同一PO";
                    return;
                }
                if (fwerks != dr["调出厂"].ToString() || flgort != dr["调出仓"].ToString() || dwerks != dr["接收厂"].ToString())
                {
                    stsWarning.Text = "请再次确认文档中调出仓别和调出厂区、接收厂区";
                    return;
                }
            }
            #endregion

            #region 对比已查出的dtsap检查数据是否合理

            dtsap.Clear();
            dtsap = objTransfer.Query46Pdata(dtfile.Rows[0]["调出厂"].ToString(), "", "", dtfile.Rows[0]["PO"].ToString(), strType);
            if (dtsap.Rows.Count <= 0)
            {
                stsWarning.Text = "该PO没有信息";
                return;
            }
            if (dtfile.Rows[0]["调出厂"].ToString() != dtsap.Rows[0]["WERKS"].ToString())
            {
                stsWarning.Text = "调出厂区填写错误";
                return;
            }
            if (dtfile.Rows[0]["接收厂"].ToString() != dtsap.Rows[0]["KOSTL"].ToString())
            {
                stsWarning.Text = "接收厂区填写错误";
                return;
            }
            if (dtsap.Rows[0]["LGORT"].ToString() != "" && dtfile.Rows[0]["调出仓"].ToString() != dtsap.Rows[0]["LGORT"].ToString())
            {
                MessageBox.Show("调出仓别不一致，请注意！");
            }
            foreach (DataRow dr in dtfile.Rows)
            {
                DataRow[] drSync = dtsap.Select("MATNR='" + dr["料号"].ToString().Trim() + "'");
                if (drSync.Length == 0)
                {
                    stsWarning.Text = "该PO没有" + dr["料号"].ToString().Trim() + "需要调出";
                    return;
                }
            }
            #endregion

            btnImport.Enabled = false;
            btnFile.Enabled = false;

            #region 更改dtsap的数据
            DataTable dtsaptemp = new DataTable();
            dtsaptemp.Columns.Add("MBLNR");
            dtsaptemp.Columns.Add("ZEILE");
            dtsaptemp.Columns.Add("WERKS");
            dtsaptemp.Columns.Add("LGORT");
            dtsaptemp.Columns.Add("MATNR");
            dtsaptemp.Columns.Add("CHARG");
            dtsaptemp.Columns.Add("MENGE");
            dtsaptemp.Columns.Add("OTQTY");
            dtsaptemp.Columns.Add("KOSTL");
            dtsaptemp.Columns.Add("UMLGO");
            //将文件中的Item加入临时表
            foreach (DataRow dr in dtfile.Rows)
            {
                foreach (DataRow drsap in dtsap.Rows)
                {
                    bool falg = false;
                    if (dr["料号"].ToString().Trim() == drsap["MATNR"].ToString())
                    {
                        if (drsap["CHARG"].ToString() != "" && dr["版本"].ToString() == drsap["CHARG"].ToString())
                        {
                            if (Convert.ToInt32(dr["数量"].ToString()) > Convert.ToInt32(drsap["MENGE"].ToString()))
                            {
                                stsWarning.Text = "料号" + drsap["MATNR"] + "大于PO数量";
                                return;
                            }
                            dtsaptemp.Rows.Add(drsap["MBLNR"], drsap["ZEILE"], drsap["WERKS"], dr["调出仓"], drsap["MATNR"], dr["版本"], drsap["MENGE"], dr["数量"], dr["接收厂"], drsap["UMLGO"]);
                            falg = true;
                        }
                        else if (drsap["CHARG"].ToString() == "")
                        {
                            if (Convert.ToInt32(dr["数量"].ToString()) > Convert.ToInt32(drsap["MENGE"].ToString()))
                            {
                                stsWarning.Text = "料号" + drsap["MATNR"] + "大于PO数量";
                                return;
                            }
                            dtsaptemp.Rows.Add(drsap["MBLNR"], drsap["ZEILE"], drsap["WERKS"], dr["调出仓"], drsap["MATNR"], dr["版本"], drsap["MENGE"], dr["数量"], dr["接收厂"], drsap["UMLGO"]);
                            falg = true;
                        }
                    }
                    if (falg)
                    {
                        break;
                    }
                }
            }
            //将不属于文件中的Item加入临时表
            foreach (DataRow drsap in dtsap.Rows)
            {
                bool falg = false;
                foreach (DataRow dr in dtfile.Rows)
                {
                    if (dr["料号"].ToString().Trim() == drsap["MATNR"].ToString())
                    {
                        falg = true;
                    }
                }
                if (!falg)
                {
                    dtsaptemp.Rows.Add(drsap["MBLNR"], drsap["ZEILE"], drsap["WERKS"], drsap["LGORT"], drsap["MATNR"], drsap["CHARG"], drsap["MENGE"], drsap["OTQTY"], drsap["KOSTL"], drsap["UMLGO"]);
                }
            }
            //排序
            DataView dv = dtsaptemp.DefaultView;
            dv.Sort = "ZEILE";
            dtsaptemp = dv.ToTable();
            dtsap = dtsaptemp;
            #endregion


            ShowDataView_sap();
            AutoSelect_Sap();
            stsWarning.Text = "OK";
            btnQuary.Enabled = false;
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            if (txtFile.Text.Trim() == "")
            {
                btnImport.Enabled = false;
            }
            if (txtFile.Text.Length != 0)
            {
                txtlocat.Enabled = false;
            }
        }

        private void txtlocat_TextChanged(object sender, EventArgs e)
        {
            if (txtlocat.Text.Length != 0)
            {
                btnFile.Enabled = false;
            }
        }

        private void LnkSOP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + @"SOP\QWMS_313_303_351开单自动扣帐SOP.docx";
                try
                {
                    Process.Start("winword.exe", strPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"无法打开文件，请手动打开" + strPath);
                }
            }
            else
            {
                MessageBox.Show("未在数据库维护模板路径，请联系QWMS负责人");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint = new DataTable();
            dtPrint = objTransfer.TransferOutPrint(dtqwms.Rows[0]["MBLNR"].ToString(), strType);
            if (dtPrint.Rows.Count > 0)
            {
                #region  多次打印时需要输入特定账号和密码
                DataTable dtstatus = new DataTable();
                dtstatus = objTransfer.returnstatus(dtqwms.Rows[0]["MBLNR"].ToString(), strType);
                if (dtstatus.Rows[0]["ULFLG"].ToString() == "N")
                {
                    objTransfer.updatestatus(dtqwms.Rows[0]["MBLNR"].ToString(), strType);
                }
                else if (dtstatus.Rows[0]["ULFLG"].ToString() == "Y")
                {
                    #region  输入账号密码
                    stsWarning.Text = "";
                    String login = QWMS.Transfer_InputBox.ShowInputBox("输入账号", "请输入账号", Upper: true, X: 100, Y: 100);
                    DataTable dtLogin = new DataTable();
                    Admin objAdmin = new Admin(UserData, Progid);
                    dtLogin = objAdmin.GetPassword("TRANSFER");
                    DataRow[] dr = dtLogin.Select("PASWD='" + login + "'");
                    if (dr.Count() == 0)
                    {
                        MessageBox.Show("请输入正确的账号谢谢！！！！！");
                        return;
                    }
                    else
                    {
                        String paswd = QWMS.Transfer_InputBox.ShowInputBox("输入密码", "请输入密码", strPassword: "*", X: 100, Y: 100);
                        ClaCommon claCommon = new ClaCommon();
                        if (!claCommon.CheckAccount(login, paswd).GetAwaiter().GetResult())
                        {
                            MessageBox.Show("请输入正确的密码谢谢！！！！！");
                            return;
                        }
                        //DataTable dtPwd = objAdmin.GetPassword(login);
                        //DataRow[] drPwd = dtPwd.Select("PASWD='" + paswd + "'");
                        //if (drPwd.Count() == 0)
                        //{
                        //    MessageBox.Show("请输入正确的密码谢谢！！！！！");
                        //    return;
                        //}
                    }
                    #endregion
                }
                #endregion

                if (strType == "SAP_60S")
                {
                    #region  拼接SAP的回执信息到dtPrint
                    dtPrint.Columns.Add("Packing");
                    dtPrint.Columns.Add("Item");
                    dtPrint.Columns.Add("Material");
                    dtPrint.Columns.Add("Batch");
                    dtPrint.Columns.Add("Quantity");
                    dtPrint.Columns.Add("Rev_Stor");
                    dtPrint.Columns.Add("Description");
                    dtPrint.Columns.Add("Vendor_Code");
                    dtPrint.Columns.Add("Country_of_Origin");
                    dtPrint.Columns.Add("lst_base_unit");//字母l
                    dtPrint.Columns.Add("lst_base_QTY");
                    dtPrint.Columns.Add("qnd_base_unit");//由于数据库中不能以数字开头，1和2改成了字母l和字母q
                    dtPrint.Columns.Add("qnd_base_QTY");

                    foreach (DataRow drprint in dtPrint.Rows)
                    {
                        bool falg = false;
                        foreach (DataRow drsap in dsrec.Tables["STO_ITEM"].Rows)
                        {
                            if (drsap["EBELN"].ToString() == drprint["PO"].ToString() && drsap["EBELP"].ToString() == drprint["POITEM"].ToString())
                            {
                                drprint["Packing"] = @"*" + drsap["Packing"].ToString() + @"*";//条形码必须要加*号
                                drprint["Item"] = drsap["ZEILE"].ToString();
                                drprint["Material"] = drsap["MATNR"].ToString();
                                drprint["Batch"] = drsap["CHARG"].ToString();
                                drprint["Quantity"] = drsap["MENGE"].ToString();
                                drprint["Rev_Stor"] = drsap["DLGORT"].ToString();
                                drprint["Description"] = drsap["MAKTX"].ToString();
                                drprint["Vendor_Code"] = drsap["VendorCode"].ToString();
                                drprint["Country_of_Origin"] = drsap["OriginCountry"].ToString();
                                drprint["lst_base_unit"] = drsap["LegalUnit"].ToString();
                                drprint["lst_base_QTY"] = drsap["LegalQty"].ToString();
                                drprint["qnd_base_unit"] = drsap["LegalUnit2"].ToString();
                                drprint["qnd_base_QTY"] = drsap["LegalQty2"].ToString();
                                falg = true;
                            }
                            if (falg)
                                break;

                        }
                    }


                    #endregion
                }
                //增加351调拨储位编码
                if(strType=="SAP_351")
                {
                    dtPrint.Columns.Add("LOCAT");
                    foreach(DataRow dr in dtPrint.Rows)
                    {
                        dr["LOCAT"] = txtlocat.Text;
                    }
                }
                string ReportPrintType = "TRANSFEROUT";
                ReportPrintType = ReportPrintType + strType;
                ReportPrint objReportPrint = new ReportPrint(UserData, ReportPrintType, dtPrint);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }

        }




    }
}
