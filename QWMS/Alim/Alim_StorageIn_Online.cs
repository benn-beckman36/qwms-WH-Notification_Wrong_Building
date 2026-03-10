using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;

namespace QWMS
{
    public partial class Alim_StorageIn_Online : Form
    {
        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strBwart = "";
        private string strContrno = "";//柜号
        private string strMainRun = "";//主流道
        private string strMblnr = "";
        private string strFdate = "";
        private string strTdate = "";

        QCI.QWMS.Alim_Storage objStorage;
        QCI.QWMS.Alim objAlim;
        #endregion

        #region 构造函数
        public Alim_StorageIn_Online(UserInfo varUserData, string Progid)
        {
            InitializeComponent();
            UserData = varUserData;

            strMandt = UserData.Client;
            strComcd = UserData.CompanyCode;
            strUsrnm = UserData.UserId;
            strProgid = Progid;

            objAlim = new QCI.QWMS.Alim(UserData, Progid);
            objStorage = new QCI.QWMS.Alim_Storage(UserData, Progid);

            try
            {
                 //检查权限
                if (!objAlim.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();//厂区
                    ShowDdlLgort();//仓别
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 厂区、仓别、流道
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = strMandt;
            this.stsUsrnm.Text = strUsrnm;
            this.stsComcd.Text = strComcd;

        }

        private void ShowDdlWerks()
        {
            stsWarning.Text = string.Empty;
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
                stsWarning.Text = string.Empty;
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAlim.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAlim.CheckLgortAuthority();
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
                        cmbLgort.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                        if (dtTemp.Rows[i]["CTRLC1"].ToString() == strLgort && strLgort != "")
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

        private void ShowDdRunner(string strWerks,string strLgort)
        {
            stsWarning.Text = string.Empty;
            DataTable dtTemp = new DataTable();
            try
            {
                cmbRunner.Items.Clear();
                dtTemp = objStorage.getMainRun(strWerks, strLgort);
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbRunner.Items.Add(dtTemp.Rows[i]["MAINRUN"].ToString());
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdRunner()");
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            start(false);
            ShowDdRunner(strWerks, strLgort);
        }
        private void cmbRunner_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (cmbRunner.SelectedIndex != -1)
            {
                strMainRun = cmbRunner.Items[cmbRunner.SelectedIndex].ToString();
            }
        }

        #endregion

        private void btnLockRun_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            start(true);
            lockRun();
            DataTable dtRunner = new DataTable();
            dtRunner = objStorage.getMblnrRun(strWerks, strLgort, strMblnr, "");
            ShowRunnerView(dtRunner);
            txtMblnr.Enabled = false;
            btStart.Enabled = true;
            cmbRunner.Text = string.Empty;
        }

        private void lockRun()
        {
            //校验单据号和异动
            DataTable dtTemp = objStorage.getWhdwnMblnr(strWerks, strLgort, strMblnr, strBwart, "getOnlineInMblnr", "", "");
            if (dtTemp.Rows.Count == 0)
            {
                stsWarning.Text = "无此单据信息，请重新确认";
                return;
            }
            if (cmbRunner.SelectedIndex != -1)
            {
                strMainRun = cmbRunner.Items[cmbRunner.SelectedIndex].ToString();
            }
            else
            {
                stsWarning.Text = "请选择主流道";
                return;
            }
            try
            {
                #region 判断该流道是否已绑定
                dtTemp = objStorage.getMblnrRun(strWerks, strLgort, strMblnr, strMainRun);
                if (dtTemp.Rows.Count > 0)
                {
                    stsWarning.Text = "该流道已被绑定";
                    return;
                }
                #endregion

                #region 绑定流道数据
                if (objStorage.lockRunner(strWerks, strLgort, strMblnr, strMainRun))
                {
                    stsWarning.Text = "该流道绑定成功";
                    ShowDdRunner(strWerks, strLgort);
                }
                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            DataTable dtExit = objStorage.getAlGinMblnr(strWerks, strLgort, strMblnr);
            if(dtExit.Rows.Count>0)
            {
                stsWarning.Text = "当前单据尚未结束，请点击End！！！";
                return;
            }
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            this.txtBwart.Text = string.Empty;
            this.txtMblnr.Text = string.Empty;
            this.dgvMblnr.DataSource = null;
            this.dgvRunner.DataSource = null;
            this.lblCount.Text = "0 records";
            this.txtMblnr.Enabled = true;
            if(!this.checkLock.Checked)
            {
                cmbLgort.SelectedIndex = -1;
                cmbWerks.SelectedIndex = -1;
                strWerks = "";
                strLgort = "";
            }
        }


        #region showRunnerView
        private void ShowRunnerView(DataTable dtRunner)
        {
            try
            {
                this.dgvRunner.AutoGenerateColumns = false;
                this.dgvRunner.Columns.Clear();
                if (!dtRunner.Columns.Contains("Select"))
                {
                    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                    dtRunner.Columns.Add(cSelect);
                }

                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "选择";
                dgvcSelect.Name = "Select";
                dgvcSelect.Width = 50;
                dgvcSelect.Selected = false;
                this.dgvRunner.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Name = "WERKS";
                dgvcWerks.ReadOnly = true;
                this.dgvRunner.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Name = "LGORT";
                dgvcLgort.ReadOnly = true;
                this.dgvRunner.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Name = "MBLNR";
                dgvcMblnr.ReadOnly = true;
                this.dgvRunner.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcRunner = new DataGridViewTextBoxColumn();
                dgvcRunner.DataPropertyName = "MAINRUN";
                dgvcRunner.HeaderText = "RUNNER";
                dgvcRunner.Name = "MAINRUN";
                dgvcRunner.ReadOnly = true;
                this.dgvRunner.Columns.Add(dgvcRunner);

                this.dgvRunner.DataSource = dtRunner;
                dgvRunner.ClearSelection();
                dgvRunner.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowRunnerView()");
                return;
            }
        }
        #endregion

        #region showMblnrView
        private void ShowMblnrView(DataTable dtMblnr)
        {
            try
            {
                this.dgvMblnr.AutoGenerateColumns = false;
                this.dgvMblnr.Columns.Clear();

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                this.dgvMblnr.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                this.dgvMblnr.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                this.dgvMblnr.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "MATNR";
                dgvcMatnr.ReadOnly = true;
                this.dgvMblnr.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "MENGE";
                dgvcMenge.ReadOnly = true;
                this.dgvMblnr.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "ALQTY";
                dgvcAlqty.ReadOnly = true;
                this.dgvMblnr.Columns.Add(dgvcAlqty);

                this.dgvMblnr.DataSource = dtMblnr;
                dgvMblnr.ClearSelection();
                dgvMblnr.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowMblnrView()");
                return;
            }
        }

        public void ShowDataGrid(DataTable dtWhdwnMblnr)
        {
            dgvMblnr.Columns.Clear();
            dgvMblnr.AutoGenerateColumns = false;
            try
            {
                ////筆數
                //DataGridViewTextBoxColumn dgvcCount = new DataGridViewTextBoxColumn();
                //dgvcCount.DataPropertyName = "Item";
                //dgvcCount.HeaderText = "Item";
                //dgvcCount.ReadOnly = true;
                //dgvcCount.Width = 40;
                //dgvMblnr.Columns.Add(dgvcCount);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvMblnr.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.ReadOnly = true;
                dgvcZeile.Width = 100;
                dgvMblnr.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvMblnr.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvMblnr.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 80;
                dgvMblnr.Columns.Add(dgvcCharg);


                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Total Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 110;
                dgvMblnr.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "OTQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 100;
                dgvMblnr.Columns.Add(dgvcAlqty);


                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                dgvcEbeln.Width = 100;
                dgvMblnr.Columns.Add(dgvcEbeln);

                dgvMblnr.DataSource = dtWhdwnMblnr;
                lblCount.Text = dtWhdwnMblnr.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        #endregion

        #region 锁定厂区仓别
        private void checkLock_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkLock.Checked)
            {
                this.cmbWerks.Enabled = false;
                this.cmbLgort.Enabled = false;
            }
            else
            {
                this.cmbWerks.Enabled = true;
                this.cmbLgort.Enabled = true;
            }
        }
        #endregion

        #region 厂区仓别单据号初始化
        private void start(bool mblnr)
        {
            if (cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1 )
            {
                stsWarning.Text = "厂区/仓别不可以为空";
                return;
            }
            if (mblnr)
            {
                if (txtMblnr.Text.ToString() == "")
                {
                    stsWarning.Text = "单据号不可以为空";
                    return;
                }
            }
            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            strMblnr = txtMblnr.Text.ToString();
            strBwart = txtBwart.Text.ToString();
        }
        #endregion

        private void btStart_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            start(true);
            DataTable dtMblnr = new DataTable();
            DataTable dtRun = new DataTable();
            txtMblnr.Enabled = false;
            try
            {
                //判断是否已有绑定流道，若无，则不允许作业
                dtRun=objStorage.getMblnrRun(strWerks,strLgort,strMblnr,"");
                if(dtRun.Rows.Count==0)
                {
                    stsWarning.Text = "尚未绑定流道，不可以开始作业";
                    return;
                }
                // 判断中间表是否已有单据数量信息，若无，则插入数据
                dtMblnr = objStorage.getMblnrIn(strWerks, strLgort, strMblnr);
                ShowMblnrView(dtMblnr);
                btStart.Enabled = false;
            }
            catch(Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
        }

        private void btEnd_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                #region 判断该单据是否有流道未解锁，若有，解锁流道，但不允许结束单据
                foreach (DataGridViewRow dr in dgvRunner.Rows)
                {
                    if (dr.Cells["Select"].EditedFormattedValue.ToString()=="True")
                    {
                        //判断是否已解除
                        DataTable dtTemp = new DataTable();
                        dtTemp = objStorage.getMblnrRun(dr.Cells["WERKS"].Value.ToString(), dr.Cells["LGORT"].Value.ToString(), dr.Cells["MBLNR"].Value.ToString(), dr.Cells["MAINRUN"].Value.ToString());
                        if (dtTemp.Rows.Count == 0)
                        {
                            stsWarning.Text = "该流道已解除绑定";
                            return;
                        }
                        if (objStorage.openRunner(dr.Cells["WERKS"].Value.ToString(), dr.Cells["LGORT"].Value.ToString(), dr.Cells["MBLNR"].Value.ToString(), dr.Cells["MAINRUN"].Value.ToString()))
                        {
                            stsWarning.Text = "解除成功";
                        }
                        else
                        {
                            stsWarning.Text = "解除失败";
                            return;
                        }
                    }
                    //else
                    //{
                    //    stsWarning.Text = "请勾选流道！！！";
                    //    return;
                    //}
                }
                DataTable dtDocument = objStorage.getMblnrRun(strWerks, strLgort, strMblnr, "");
                if (dtDocument.Rows.Count > 0)
                {
                    MessageBox.Show("该单据存在绑定流道未解锁，不结束单据");
                    ShowRunnerView(dtDocument);
                    ShowDdRunner(strWerks, strLgort);
                    return;
                }
                #endregion

                #region 回退中间表数据ALGIN到WHDWN
                DataTable dtReturn = objStorage.getAlGinMblnr(strWerks, strLgort, strMblnr);
                DataTable dtOrigin = objStorage.getWhdwnMblnr(strWerks, strLgort, strMblnr, strBwart, "getOnlineInMblnr", "", "");
                if (dtReturn.Rows.Count > 0 )
                {
                    if (objStorage.backWhdwn(dtReturn, dtOrigin))
                    {
                        stsWarning.Text = "该单据已完成";
                    }
                }
                else
                {
                    stsWarning.Text = "此次操作无实物入库";
                }
                dgvRunner.DataSource = null;
                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message.ToString();
                return;
            }
            txtMblnr.Enabled = true;
        }

        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            DataTable dtWhdwnMblnr = new DataTable();
            start(false);
            try
            {
                if(txtBwart.Text.ToString()!="101")
                {
                    return;
                }
                else
                {
                    strBwart = txtBwart.Text.ToString();
                }
                strFdate = dtpFdate.Value.ToString("yyyyMMdd");
                strTdate = dtpTdate.Value.ToString("yyyyMMdd");
                QWMS.Alim_StorageInMblnrSelect objDocument = new Alim_StorageInMblnrSelect(UserData, strProgid, strWerks, strLgort, strBwart, strFdate, strTdate);
                objDocument.ShowDialog();
                strMblnr = objDocument.MBLNR;
                strBwart = txtBwart.Text.ToString();
                dtWhdwnMblnr = objStorage.getWhdwnMblnr(strWerks, strLgort, strMblnr, strBwart, "getOnlineInMblnr", "", "");
                ShowDataGrid(dtWhdwnMblnr);
                txtMblnr.Text = strMblnr;
            }
            catch(Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }

        private void txtMblnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                if(txtMblnr.Text=="")
                {
                    stsWarning.Text = "请输入单据号";
                    return;
                }
                else
                {
                    strMblnr = txtMblnr.Text.ToString();
                }
                strFdate = dtpFdate.Value.ToString();
                strTdate = dtpTdate.Value.ToString();
                strBwart = txtBwart.Text.ToString();
                DataTable dtWhdwnMblnr = objStorage.getWhdwnMblnr(strWerks, strLgort, strMblnr, strBwart, "getOnlineInMblnr", "", "");
                ShowDataGrid(dtWhdwnMblnr);
            }
            catch(Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void Alim_StorageIn_Online_FormClosing(object sender, FormClosingEventArgs e)
        {
            stsWarning.Text = string.Empty;
            DataTable dtExit = objStorage.getAlGinMblnr(strWerks, strLgort, strMblnr);
            if (dtExit.Rows.Count > 0)
            {
                stsWarning.Text = "当前单据尚未结束，请点击End！！！";
                e.Cancel = true;
            }
        }

    }
}
