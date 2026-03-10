using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using System.Collections;
using QCI_QWMS_StorageData;
using QWMS.Common;
using Microsoft.VisualBasic;

namespace QWMS
{
    public partial class TransferOut_Query : Form
    {
        #region 变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strPo = "";
        private string strType = "";
        DataTable dtData = new DataTable();
        DataTable dtTemp = new DataTable();
        CarData objCarData;
        PlantData objPlantData;
        Authority objAuthority;
        Transfer objTransfer;
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

        #region 构造函数
        public TransferOut_Query(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            QCI.QWMS.Replenishment Replenishment = new Replenishment(UserData, strProgid);
            try
            {
                Admin admin = new Admin(UserData, strProgid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);
                objCarData = new CarData(UserData);
                objTransfer = new Transfer(UserData, Werks, Lgort);

                if (!Replenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowStatusData(); //秀出Status的資料
                    stsWarning.Text = "";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        # region 显示厂区、仓别、状态栏
        private void ShowDdlWerks()
        {
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objPlantData.GetDdlWerksData();
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

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "选择";
                dgvcSelect.Name = "Select";
                dgvcSelect.Width = 50;
                dgvcSelect.Selected = false;
                this.dgvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "Item";
                dgvcItem.HeaderText = "Item";
                dgvcItem.Width = 60;
                dgvcItem.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcFwerks = new DataGridViewTextBoxColumn();
                dgvcFwerks.DataPropertyName = "FWERKS";
                dgvcFwerks.HeaderText = "调出厂区";
                dgvcFwerks.Width = 70;
                dgvcFwerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcFwerks);

                DataGridViewTextBoxColumn dgvcFlgort = new DataGridViewTextBoxColumn();
                dgvcFlgort.DataPropertyName = "FLGORT";
                dgvcFlgort.HeaderText = "调出仓别";
                dgvcFlgort.ReadOnly = true;
                dgvcFlgort.Width = 70;
                this.dgvData.Columns.Add(dgvcFlgort);

                DataGridViewTextBoxColumn dgvcDwerks = new DataGridViewTextBoxColumn();
                dgvcDwerks.DataPropertyName = "DWERKS";
                dgvcDwerks.HeaderText = "调入厂区";
                dgvcDwerks.Width = 70;
                dgvcDwerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDwerks);

                DataGridViewTextBoxColumn dgvcDlgort = new DataGridViewTextBoxColumn();
                dgvcDlgort.DataPropertyName = "DLGORT";
                dgvcDlgort.HeaderText = "调入仓别";
                dgvcDlgort.ReadOnly = true;
                dgvcDlgort.Width = 70;
                this.dgvData.Columns.Add(dgvcDlgort);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "单据号";
                dgvcMblnr.Name = "MBLNR";
                dgvcMblnr.Width = 100;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.Width = 60;
                dgvcCHARG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCHARG);

                if (strType == "SAP_351" || strType == "SAP_60S")
                {
                    DataGridViewTextBoxColumn dgvcEBELN = new DataGridViewTextBoxColumn();
                    dgvcEBELN.DataPropertyName = "EBELN";
                    dgvcEBELN.HeaderText = "PO";
                    dgvcEBELN.Width = 90;
                    dgvcEBELN.ReadOnly = true;
                    this.dgvData.Columns.Add(dgvcEBELN);

                    DataGridViewTextBoxColumn dgvcEBELP = new DataGridViewTextBoxColumn();
                    dgvcEBELP.DataPropertyName = "EBELP";
                    dgvcEBELP.HeaderText = "POItem";
                    dgvcEBELP.Width = 60;
                    dgvcEBELP.ReadOnly = true;
                    this.dgvData.Columns.Add(dgvcEBELP);
                }

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "厂商代码";
                dgvcLIFNR.Width = 80;
                dgvcLIFNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 80;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcOTQTY = new DataGridViewTextBoxColumn();
                dgvcOTQTY.DataPropertyName = "OTQTY";
                dgvcOTQTY.HeaderText = "扣帐数量";
                dgvcOTQTY.Width = 80;
                dgvcOTQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOTQTY);


                DataGridViewTextBoxColumn dgvcDLogrt = new DataGridViewTextBoxColumn();
                dgvcDLogrt.DataPropertyName = "REMAK1";
                dgvcDLogrt.HeaderText = "扣帐单号";
                dgvcDLogrt.Width = 90;
                dgvcDLogrt.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDLogrt);

                DataGridViewTextBoxColumn dgvcUsnam = new DataGridViewTextBoxColumn();
                dgvcUsnam.DataPropertyName = "USNAM";
                dgvcUsnam.HeaderText = "开单人";
                dgvcUsnam.Width = 90;
                dgvcUsnam.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcUsnam);

                DataGridViewTextBoxColumn dgvcCretim = new DataGridViewTextBoxColumn();
                dgvcCretim.DataPropertyName = "CRDAT";
                dgvcCretim.HeaderText = "创建时间";
                dgvcCretim.Width = 100;
                dgvcCretim.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCretim);


                if (strType == "SAP_60S")
                {
                    DataGridViewTextBoxColumn dgvcWstate = new DataGridViewTextBoxColumn();
                    dgvcWstate.DataPropertyName = "WSTATE";
                    dgvcWstate.HeaderText = "是否维护净重";
                    dgvcWstate.Width = 100;
                    dgvcWstate.ReadOnly = true;
                    this.dgvData.Columns.Add(dgvcWstate);
                }

                DataGridViewTextBoxColumn dgvcmsg = new DataGridViewTextBoxColumn();
                dgvcmsg.DataPropertyName = "MSG";
                dgvcmsg.HeaderText = "状态";
                dgvcmsg.Width = 200;
                dgvcmsg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcmsg);


                dgvData.DataSource = dtData;
                dgvData.AllowUserToAddRows = false;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion


        #region 功能
        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }
        private void Query()
        {
            stsWarning.Text = "";
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.SelectedItem.ToString().Trim();
            }
            else
            {
                strWerks = "";
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.SelectedItem.ToString().Trim();
            }
            else
            {
                strLgort = "";
            }
            if (rdb303.Checked)
            {
                strType = "SAP_303";
            }
            if (rdb313.Checked)
            {
                strType = "SAP_313";
            }
            if (rdb351.Checked)
            {
                strType = "SAP_351";
            }
            if (rdb60S.Checked)
            {
                strType = "SAP_60S";
                this.btnSave.Enabled = false;
            }

            strPo = txtMblnr.Text.ToString().Trim();
            dtData = objCarData.Query46PO(strWerks, strLgort, strPo, strType, chkdone.Checked, dtPO1.Value.ToString("yyyy-MM-dd"), dtPO2.Value.ToString("yyyy-MM-dd"));

            DataColumn dcSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(dcSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }
            ShowDataGrid();
            if (dtData.Rows.Count < 0)
            {
                stsWarning.Text = "No Data";
                return;
            }
        }
        #endregion

        #region SAP扣账，适用于未扣帐成功的数据
        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose plant and storage!!");
                return;
            }
            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            if (strWerks == "" || strLgort == "")
            {
                MessageBox.Show("Please choose plant and storage!!");
                return;
            }
            DataRow[] drSync = dtData.Select("Select=True AND MSG='PO未扣帐'");
            DataTable dtMblnr = new DataTable();
            dtMblnr.Columns.Add("MBLNR");
            foreach (DataRow dr in drSync)
            {
                DataRow drMblnr = dtMblnr.NewRow();
                drMblnr["MBLNR"] = dr["MBLNR"].ToString();
                dtMblnr.Rows.Add(drMblnr);
            }
            DataView dv = dtMblnr.DefaultView;
            DataTable dtCombine = dv.ToTable(true, "MBLNR");//去重

            DataTable dtQueryQwmsStorage = new DataTable();
            DataTable dtSap = new DataTable();
            if (dtCombine.Rows.Count > 0)
            {
                objTransfer = new Transfer(UserData, strWerks, strLgort);
                foreach (DataRow dr in dtCombine.Rows)
                {
                    dtQueryQwmsStorage = objTransfer.GetQwmsStorage(dr["MBLNR"].ToString(), strType);
                    if (dtQueryQwmsStorage.Rows.Count == 0)
                    {
                        #region 扣账
                        dtSap = objTransfer.GetSapData(dr["MBLNR"].ToString(), strType);

                        if (dtSap.Rows.Count > 0)
                        {
                            DataSet ds = new DataSet();
                            DataTable dtcopy = dtSap.Copy();
                            ds.Tables.Add(dtcopy);
                            DataSet dsrec = new DataSet();
                            dsrec = SendToSAP(strType.Substring(strType.Length - 3, 3), ds);
                            objTransfer.UpdateTransRemark(dsrec, dr["MBLNR"].ToString());
                            if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "Y")
                            {
                                stsWarning.Text = dr["MBLNR"].ToString() + "扣账成功";
                                MessageBox.Show(dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString());
                                btnSave.Enabled = false;
                            }
                            else if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "N")
                            {
                                stsWarning.Text = "扣账失败";
                                MessageBox.Show(dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString());
                            }

                        }
                        #endregion
                    }
                    else
                    {
                        stsWarning.Text = "调拨数量不足";
                        objTransfer.UpdateMengeRemark(dr["MBLNR"].ToString());
                        return;
                    }
                }
            }
        }
        #endregion

        #region 同步TRDWN数据，适用于出库完成，TRDWN表无数据
        private void btnInsert_Click(object sender, EventArgs e)
        {
            ArrayList faileID = new ArrayList();
            DataRow[] drSync = dtData.Select("Select=True AND MSG='PO已扣帐,仓库未做出库' ");
            if (drSync.Length == 0)
            {
                stsWarning.Text = "请选择已经扣账，但未出库的数据";
                return;
            }
            foreach (DataRow dr in drSync)
            {
                string mblnr = dr["REMAK1"].ToString();
                DataTable dtResult = new DataTable();
                try
                {
                    dtResult = objCarData.synchronize46POByHand(mblnr);
                    if (dtResult.Rows[0][0].ToString() == "fail")
                    {
                        faileID.Add(mblnr);
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.ToString();
                    continue;
                }

            }
            if (faileID.Count > 0)
            {
                string sbid = "";
                foreach (string id in faileID)
                {
                    sbid += id + ",";
                }
                stsWarning.Text = "扣帐编号：" + sbid + "同步失败";
            }
            else
            {
                stsWarning.Text = "同步成功";
            }

        }
        #endregion

        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region radiobutton_Changed
        private void rdb303_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb303.Checked)
            {
                strType = "SAP_303";
                btnInsert.Enabled = false;
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
                if (chkdone.Checked)
                {
                    btnInsert.Enabled = true;
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void rdb313_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb313.Checked)
            {
                strType = "SAP_313";
                btnInsert.Enabled = false;
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
                if (chkdone.Checked)
                {
                    btnInsert.Enabled = true;
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void rdb46P_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb351.Checked)
            {
                strType = "SAP_351";
                btnInsert.Enabled = false;
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
                if (chkdone.Checked)
                {
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void rdb60S_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb60S.Checked)
            {
                strType = "SAP_60S";
                btnDelete.Enabled = false;
                btnInsert.Enabled = false;
                btnSave.Enabled = false;
            }
        }
        #endregion

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

        private void chkdone_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdone.Checked)
            {
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
            }
            if ((strType == "SAP_313" || strType == "SAP_303") && chkdone.Checked)
            {
                btnInsert.Enabled = true;
            }
            else if ((strType == "SAP_313" || strType == "SAP_303") && !chkdone.Checked)
            {
                btnInsert.Enabled = false;
            }
            if (strType == "SAP_60S")
            {
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            //String login = Interaction.InputBox("请输入账号", "输入账号", "", 100, 100);
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
                //String paswd = Interaction.InputBox("请输入密码", "输入密码", "", 100, 100);
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
                #region 删除
                else
                {
                    stsWarning.Text = "正在删除，请勿关闭视窗";
                    Delete();
                }
                #endregion
            }
        }

        private void Delete()
        {
            DataRow[] drSync = dtData.Select("Select=True AND MSG='PO未扣帐' ");
            DataTable dtMblnr = new DataTable();
            dtMblnr.Columns.Add("MBLNR");
            foreach (DataRow dr in drSync)
            {
                DataRow drMblnr = dtMblnr.NewRow();
                drMblnr["MBLNR"] = dr["MBLNR"].ToString();
                dtMblnr.Rows.Add(drMblnr);
            }
            DataView dv = dtMblnr.DefaultView;
            DataTable dtCombine = dv.ToTable(true, "MBLNR");//去重

            foreach (DataRow drMblnr in dtCombine.Rows)
            {
                if (objTransfer.delTransData(drMblnr["MBLNR"].ToString(), strType))
                {
                    stsWarning.Text = "删除成功";
                }
                else
                {
                    stsWarning.Text = "删除失败";
                    return;
                }
            }
            Query();
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbLgort.SelectedIndex = -1;
            ShowDdlLgort();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetInit();
        }

        public void SetInit()
        {
            strMandt = "";
            strComcd = "";
            strProgid = "";
            strWerks = "";
            strLgort = "";
            strPo = "";
            strType = "";
            dtData.Clear();
            dtTemp.Clear();
            cmbWerks.Text = "";
            cmbWerks.SelectedIndex = -1;
            cmbLgort.Text = "";
            cmbLgort.SelectedIndex = -1;
            txtMblnr.Text = "";
            dtPO1.Value = DateTime.Now;
            dtPO2.Value = DateTime.Now;
            chkdone.Checked = false;
            rdb313.Checked = false;
            rdb351.Checked = false;
            rdb60S.Checked = false;
            rdb303.Checked = true;
            this.dgvData.Columns.Clear();
            lblCount.Text = "";
            stsWarning.Text = "";
            btnSave.Enabled = true;
            btnInsert.Enabled = true;
            btnDelete.Enabled = true;
        }


        //选中一个item，其余item自动勾选
        private void dgvData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    if (dgvData.Rows[e.RowIndex].Cells["MBLNR"].Value.ToString() == dgvData.Rows[i].Cells["MBLNR"].Value.ToString() && dtData.Rows[i]["Select"].ToString() != dtData.Rows[e.RowIndex]["Select"].ToString())
                    {
                        dgvData.Rows[i].Cells["Select"].Value = Convert.ToBoolean(dtData.Rows[e.RowIndex]["Select"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        //提交数据,每次DataGridView有事件触发之前和之后执行
        private void dgvData_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            this.dgvData.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }



    }
}
