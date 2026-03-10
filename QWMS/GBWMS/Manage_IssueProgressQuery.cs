using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    //Manage_IssueProgress的摘要描述 by Jack 20150423
    public partial class Manage_IssueProgressQuery : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMatnr = "";
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strComcd = "";
        DataTable dtData = new DataTable();

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

        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
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


        #region Constructor

        public Manage_IssueProgressQuery(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;
            try
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region MemberFunction

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
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
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //dtTemp = objPlantData.GetDdlLgortData();
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
                    strLgort = "";
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

        private void ShowDataGrid()
        {
            try
            {
                this.dgvData.AutoGenerateColumns = false;
                this.dgvData.Columns.Clear();
                
                DataGridViewTextBoxColumn dgvcFLAGE = new DataGridViewTextBoxColumn();
                dgvcFLAGE.DataPropertyName = "FLAGE";
                dgvcFLAGE.HeaderText = "Status";
                dgvcFLAGE.Width = 60;
                dgvcFLAGE.ReadOnly = true;
                dgvcFLAGE.Visible = false;
                this.dgvData.Columns.Add(dgvcFLAGE);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 50;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 50;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcTRNTP = new DataGridViewTextBoxColumn();
                dgvcTRNTP.DataPropertyName = "TRNTP";
                dgvcTRNTP.HeaderText = "Type";
                dgvcTRNTP.Width = 40;                
                this.dgvData.Columns.Add(dgvcTRNTP);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "Docu No.";
                //dgvcMBLNR.DefaultCellStyle.BackColor = Color.Green;
                dgvcMBLNR.Width = 120;
                dgvcMBLNR.ReadOnly = true;
                //this.dgvData.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "Material No.";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "Total Quantity";
                dgvcMENGE.Width = 40;
                dgvcMENGE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcOTQTY = new DataGridViewTextBoxColumn();
                dgvcOTQTY.DataPropertyName = "OTQTY";
                dgvcOTQTY.HeaderText = "Out Quantity";
                dgvcOTQTY.Width = 40;
                dgvcOTQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOTQTY);

                DataGridViewTextBoxColumn dgvcSAPTM = new DataGridViewTextBoxColumn();
                dgvcSAPTM.DataPropertyName = "SAPTM";
                dgvcSAPTM.HeaderText = "SAP Time";
                dgvcSAPTM.Width = 140;
                dgvcSAPTM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcSAPTM);                

                DataGridViewTextBoxColumn dgvcOUTTM = new DataGridViewTextBoxColumn();
                dgvcOUTTM.DataPropertyName = "OUTTM";
                dgvcOUTTM.HeaderText = "Out Time";
                dgvcOUTTM.Width = 130;
                dgvcOUTTM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOUTTM);

                DataGridViewTextBoxColumn dgvcPRETM = new DataGridViewTextBoxColumn();
                dgvcPRETM.DataPropertyName = "PRETM";
                dgvcPRETM.HeaderText = "Prepare Time(minute)";
                dgvcPRETM.Width = 50;
                dgvcPRETM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcPRETM);

                dgvData.DataSource = dtData;
                lblRecords.Text = dtData.Rows.Count.ToString() + " records";


                for (int i = 0; i < this.dgvData.Columns.Count; i++)
                {
                    this.dgvData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgvData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }

                foreach(DataGridViewRow dgvr in dgvData.Rows)
                {
                    if (dgvr.Cells[0].Value != null)
                    {
                        string strStatus = dgvr.Cells[0].Value.ToString();
                        switch (strStatus)
                        {
                            case "GREEN":
                                dgvr.DefaultCellStyle.BackColor = Color.Green;
                                break;
                            case "RED":
                                dgvr.DefaultCellStyle.BackColor = Color.Red;
                                break;
                            case "YELLOW":
                                dgvr.DefaultCellStyle.BackColor = Color.Yellow;
                                break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<--Manage_IssueProgressQuery.ShowDataGrid()");
            }
        }

        #endregion

        #region Button Click

        #region Query事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            #region 定义变量
            this.btnQuery.Enabled = false;
            string strWerks = cmbWerks.Text.ToString().Trim();
            string strLgort = cmbLgort.Text.ToString().Trim();
            strMblnr = txtMblnr.Text.ToString().Trim();
            strMatnr = txtMatnr.Text.ToString().Trim();
            string strDtpFrom = dtpFrom.Value.ToString("yyyy-MM-dd");
            string strDtpTo = dtpTo.Value.ToString("yyyy-MM-dd");
            #endregion
            if (string.IsNullOrEmpty(strLgort))
            {
                MessageBox.Show("请选择仓别!!");
                stsWarning.Text = "请选择仓别!!";
                return;
            }
            if (!string.IsNullOrEmpty(strMblnr) && !strMblnr.Contains("490"))
            {
                stsWarning.Text = "请输入49开头的发料扣帐编号!!";
                txtMblnr.Focus();
                return;
            }
            DateTime dttFrom = DateTime.Parse(dtpFrom.Value.ToString());
            DateTime dttNow = DateTime.Parse(DateTime.Now.ToString());
            double dblDiff = (dttNow - dttFrom).TotalDays;
            if (dblDiff >= 30)
            {
                stsWarning.Text = "请查询一个月之内的信息!";
                dtpFrom.Focus();
                return;
            }
            if (dblDiff < 0)
            {
                stsWarning.Text = "请选择正确的日期区间!";
                dtpFrom.Focus();
                return;
            }

            CSMCStorageData objCSMCData = new CSMCStorageData(UserData);
            dtData = objCSMCData.QueryIsseuProgress(strWerks, strLgort, strMblnr, strMatnr, strDtpFrom, strDtpTo);
            if (dtData.Rows.Count > 0)
            {
                ShowDataGrid();
            }
            else
            {
                stsWarning.Text = "No Data!!";
                return;
            }
        }
        #endregion

        #region Refresh事件
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.btnQuery.Enabled = true;
            txtMblnr.Text = "";
            txtMatnr.Text = "";
            dgvData.DataSource = null;
            stsWarning.Text = "请重新输入查询条件!!";
            txtMblnr.Focus();
        }
        #endregion

        #region Exit事件
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region DoubleClick事件
        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Hello,不要乱点哦!!", "提示", MessageBoxButtons.YesNo);
            return;
        }
        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        #endregion
    }
}
