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
    public partial class Alim_StorageIn_SMT_Return : Form
    {
        UserInfo UserData = new UserInfo();        
        private string strMandt = "";
        private string strUsrnm = "";
        private string strComcd = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strMainRun = "";
        public DataTable dtData = new DataTable();
        private string strMblnr = "";
        QCI.QWMS.Alim objAlim;
        #region
        public string Mandt
        {
            get { return strMandt; }
            set { strMandt = value; }
        }
        public string Usrnm
        {
            get { return strUsrnm; }
            set { strUsrnm = value; }
        }
        public string Comcd
        {
            get { return strComcd; }
            set { strComcd = value; }
        }
        public string Progid
        {
            get { return strProgid; }
            set { strProgid = value; }
        }
        public string Werks
        {
            get { return strWerks; }
            set { strWerks = value; }
        }
        public string Lgort
        {
            get { return strLgort; }
            set { strLgort = value; }
        }
        public string MainRun
        {
            get { return strMainRun; }
            set { strMainRun = value; }
        }
        #endregion

        public Alim_StorageIn_SMT_Return(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;
            try
            {
                objAlim = new QCI.QWMS.Alim(UserData, Progid);
                //检查权限
                if (!objAlim.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDataGrid();
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

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }
        #endregion

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            try
            {
                dtTemp = objAlimStorageInSMT.GetPlant();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["WERKS"].ToString());
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
            DataTable dtTemp = new DataTable();
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAlimStorageInSMT.GetLgort(strWerks);
                }
                else
                {
                    dtTemp = objAlimStorageInSMT.GetLgort("");
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
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["LGORT"].ToString());
                        if (dtTemp.Rows[i]["LGORT"].ToString() == strLgort && strLgort != "")
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

        #region ShowDdlMainRun
        private void ShowDdlMainRun()
        {
            DataTable dtTemp = new DataTable();
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            try
            {
                if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    dtTemp = objAlimStorageInSMT.GetMainRun(strWerks, strLgort);
                }
                else
                {
                    dtTemp = objAlimStorageInSMT.GetMainRun("", "");
                }
                if (dtTemp.Rows.Count == 0)
                {
                    cmbMainRun.Items.Clear();
                    strMainRun = "";
                }
                else
                {
                    cmbMainRun.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbMainRun.Items.Add(dtTemp.Rows[i]["MAINRUN"].ToString());
                        if (dtTemp.Rows[i]["MAINRUN"].ToString() == strMainRun && strMainRun != "")
                        {
                            cmbMainRun.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlMainRun()");
            }
        }
        #endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvMANDT = new DataGridViewTextBoxColumn();
                dgvMANDT.DataPropertyName = "MANDT";
                dgvMANDT.HeaderText = "MANDT";
                dgvMANDT.ReadOnly = true;
                dgvMANDT.Width = 150;
                dgvData.Columns.Add(dgvMANDT);

                DataGridViewTextBoxColumn dgvWERKS = new DataGridViewTextBoxColumn();
                dgvWERKS.DataPropertyName = "WERKS";
                dgvWERKS.HeaderText = "厂区";
                dgvWERKS.Width = 150;
                dgvWERKS.ReadOnly = true;
                dgvData.Columns.Add(dgvWERKS);

                DataGridViewTextBoxColumn dgvLGORT = new DataGridViewTextBoxColumn();
                dgvLGORT.DataPropertyName = "LGORT";
                dgvLGORT.HeaderText = "仓别";
                dgvLGORT.Width = 150;
                dgvLGORT.ReadOnly = true;
                dgvData.Columns.Add(dgvLGORT);

                DataGridViewTextBoxColumn dgvMAINRUN = new DataGridViewTextBoxColumn();
                dgvMAINRUN.DataPropertyName = "MAINRUN";
                dgvMAINRUN.HeaderText = "主流道";
                dgvMAINRUN.Width = 150;
                dgvMAINRUN.ReadOnly = true;
                dgvData.Columns.Add(dgvMAINRUN);

                DataGridViewTextBoxColumn dgvMBLNR = new DataGridViewTextBoxColumn();
                dgvMBLNR.DataPropertyName = "MBLNR";
                dgvMBLNR.HeaderText = "单号";
                dgvMBLNR.Width = 150;
                dgvMBLNR.ReadOnly = true;
                dgvData.Columns.Add(dgvMBLNR);

                DataGridViewTextBoxColumn dgvCRNAM = new DataGridViewTextBoxColumn();
                dgvCRNAM.DataPropertyName = "CRNAM";
                dgvCRNAM.HeaderText = "CRNAM";
                dgvCRNAM.ReadOnly = true;
                dgvCRNAM.Width = 150;
                dgvData.Columns.Add(dgvCRNAM);

                DataGridViewTextBoxColumn dgvCOMCD = new DataGridViewTextBoxColumn();
                dgvCOMCD.DataPropertyName = "COMCD";
                dgvCOMCD.HeaderText = "公司别";
                dgvCOMCD.ReadOnly = true;
                dgvCOMCD.Width = 150;
                dgvData.Columns.Add(dgvCOMCD);

                dgvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            DataTable dtMainRunCtr = new DataTable();
            DataTable dtAlrin = new DataTable();
            DataTable dtAlrid = new DataTable();
            stsWarning.Text = "";

            #region 生成虚拟单号，保存中间表(ALRIN)
            //厂区
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.SelectedItem.ToString();
            }
            else
            {
                strWerks = string.Empty;
            }
            //仓别
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.SelectedItem.ToString();
            }
            else
            {
                strLgort = string.Empty;
            }
            //主流道
            if (cmbMainRun.SelectedIndex != -1)
            {
                strMainRun = cmbMainRun.SelectedItem.ToString();
                #region 校验主流道是否被占用
                dtMainRunCtr = objAlimStorageInSMT.GetMainRunCtr();
                for (int i = 0; i < dtMainRunCtr.Rows.Count; i++)
                {
                    string strMainRunCtr = dtMainRunCtr.Rows[i]["MAINRUN"].ToString();
                    if (strMainRun == strMainRunCtr)
                    {
                        stsWarning.Text = "主流道被占用，请确认！";
                        return;
                    }
                }
                #endregion
            }
            else
            {
                strMainRun = string.Empty;
                stsWarning.Text = "请选择一个流道";
                return;
            }
            //生成唯一虚拟单据号
            if (string.IsNullOrEmpty(strMblnr))
            {
                strMblnr = objAlimStorageInSMT.GetMblnr(UserData.Client, UserData.CompanyCode, strWerks, strLgort, strMainRun, strProgid, UserData.UserId);
            }
            if (objAlim.lockRunner(strWerks, strLgort, strMblnr, strMainRun))
            {
                stsWarning.Text = "绑定主流道成功";
            }
            dtData = objAlimStorageInSMT.GetMainRun(UserData.Client, UserData.CompanyCode, strWerks, strLgort, strProgid);
            ShowDataGrid();
            #endregion
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            AlimStorageInSMT objAlimStorageInSMT = new AlimStorageInSMT(UserData, strProgid);
            bool bolresult = true;
            //厂区
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.SelectedItem.ToString();
            }
            else
            {
                strWerks = string.Empty;
            }
            //仓别
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.SelectedItem.ToString();
            }
            else
            {
                strLgort = string.Empty;
            }
            //主流道
            //if (cmbMainRun.SelectedIndex != -1)
            //{
            //    strMainRun = cmbMainRun.SelectedItem.ToString();
            //}
            //else
            //{
            //    strMainRun = string.Empty;
            //}
            //释放流道
            foreach(DataRow dr in dtData.Rows)
            {
                if (!objAlimStorageInSMT.DeleteAlrinMainRunInfo(UserData.Client, UserData.CompanyCode, strWerks, strLgort, dr["MAINRUN"].ToString()))
                {
                    bolresult = false;
                }
            }
            if (bolresult == true)
            {
                dtData.Clear();
                this.dgvData.DataSource = null;
                stsWarning.Text = "释放流道成功！";
            }
            else
            {
                stsWarning.Text = "释放流道失败！";                
                return;
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlMainRun();
        }
    }
}
