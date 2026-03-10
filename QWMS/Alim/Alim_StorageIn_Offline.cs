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

namespace QWMS
{
    public partial class Alim_StorageIn_Offline : Form
    {

        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strRunner = "";
        private string strMblnr = "";//虚拟单据
        DataTable dtData = new DataTable();//绑定流道信息

        QCI.QWMS.Alim objAlim;
        QCI.QWMS.Alim_Storage objStorage;
        #endregion

        #region 构造函数
        public Alim_StorageIn_Offline(UserInfo varUserData, string Progid)
        {
            InitializeComponent();
            UserData = varUserData;
            strMandt = UserData.Client;
            strComcd = UserData.CompanyCode;
            strUsrnm = UserData.UserId;
            strProgid = Progid;
            objAlim = new QCI.QWMS.Alim(UserData, strProgid);
            objStorage = new QCI.QWMS.Alim_Storage(UserData, strProgid);
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
                    dtData.Columns.Clear();
                    dtData.Columns.Add("MANDT");
                    dtData.Columns.Add("COMCD");
                    dtData.Columns.Add("WERKS");
                    dtData.Columns.Add("LGORT");
                    dtData.Columns.Add("RUNNER");
                    dtData.Columns.Add("MBLNR");

                    ShowDataView();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = strMandt;
            this.stsUsrnm.Text = strUsrnm;
            this.stsComcd.Text = strComcd;
        }
        # endregion

        #region 厂区、仓别
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

        private void cmbWerks_SelectedValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbLgort.SelectedIndex = -1;
            ShowDdlLgort();
        }
        #endregion

        private void ShowDataView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcMANDT = new DataGridViewTextBoxColumn();
                dgvcMANDT.DataPropertyName = "MANDT";
                dgvcMANDT.HeaderText = "MANDT";
                dgvcMANDT.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMANDT);

                DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                dgvcCOMCD.DataPropertyName = "COMCD";
                dgvcCOMCD.HeaderText = "公司别";
                dgvcCOMCD.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCOMCD);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.ReadOnly = true;
                this.gvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcRUNNER = new DataGridViewTextBoxColumn();
                dgvcRUNNER.DataPropertyName = "RUNNER";
                dgvcRUNNER.HeaderText = "流道";
                dgvcRUNNER.ReadOnly = true;
                this.gvData.Columns.Add(dgvcRUNNER);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "虚拟单号";
                dgvcMBLNR.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMBLNR);

                this.gvData.DataSource = dtData;
                gvData.ClearSelection();
                gvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataView()");
            }
        }

        private void BtnFresh_Click(object sender, EventArgs e)
        {
            cmbLgort.SelectedIndex = -1;
            cmbWerks.SelectedIndex = -1;
            strWerks = "";
            strLgort = "";
            strRunner = "";
            btnStart.Enabled = true;
            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            cmbRunner.Enabled = true;
            lbTempMblnr.Text = "";
            cmbRunner.Items.Clear();
        }


        private void ShowDdRunner(string strWerks, string strLgort)
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdRunner()");
            }
        }

        private void cmbLgort_SelectedValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbRunner.SelectedIndex = -1;
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.SelectedItem.ToString();
            }
            else
            {
                strWerks = string.Empty;
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.SelectedItem.ToString();
            }
            else
            {
                strLgort = string.Empty;
            }
            if (strWerks != "" && strLgort != "")
            {
                ShowDdRunner(strWerks, strLgort);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbRunner.SelectedIndex!=-1)
            {
                strRunner = cmbRunner.SelectedItem.ToString();
            }
            else
            {
                stsWarning.Text = "请选择一个主流道";
                return;
            }
            if(string.IsNullOrEmpty(strMblnr))
            {
                strMblnr = objAlim.GetAlimNo(strWerks);
            }
            lbTempMblnr.Text = strMblnr;
            cmbWerks.Enabled = false;
            cmbLgort.Enabled = false;
            //绑定主流道
            if (objAlim.lockRunner(strWerks, strLgort, strMblnr, strRunner))
            {
                stsWarning.Text = "绑定主流道成功";
                btnEnd.Enabled = true;
                btnFresh.Enabled = false;
                dtData.Rows.Add("218",UserData.CompanyCode,strWerks,strLgort,strRunner,strMblnr);
            }
            else
            {
                stsWarning.Text = "绑定主流道失败,请刷新后重试";
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            //解绑主流道
            bool blResult = true;
            foreach (DataRow dr in dtData.Rows)
            {
                if (!objStorage.openRunner(strWerks, strLgort, strMblnr, dr["RUNNER"].ToString()))
                {
                    stsWarning.Text = "解除失败";
                    blResult = false;
                }
                else
                {
                    stsWarning.Text = "解除成功";
                    btnFresh.Enabled = true;
                    strRunner = "";
                }
            }
            if(blResult)
                dtData.Rows.Clear();
        }

        private void Alim_StorageIn_Offline_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (strRunner != "")//未解绑
            {
                DialogResult drClose = MessageBox.Show("流道信息会自动解绑，您确认退出吗？", "退出系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (drClose == DialogResult.Cancel)
                {   
                    e.Cancel = true;
                }
                else if (drClose == DialogResult.OK)
                {
                    if (objStorage.openRunner(strWerks, strLgort, strMblnr, strRunner))
                    {
                        stsWarning.Text = "解除成功";
                        btnFresh.Enabled = true;
                        btnEnd.Enabled = false;
                        strRunner = "";
                        dtData.Rows.Clear();
                    }
                    else
                    {
                        stsWarning.Text = "解除失败";
                        e.Cancel = true;
                    }
                }
            }
        }




    }
}
