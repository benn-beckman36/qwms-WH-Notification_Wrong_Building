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
    public partial class Admin_Material_personnel : Form
    {

        #region 全局变量


        private UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMatnr = "";
        private string strMqcID = "";
        private string strMqcnm = "";
        private string strPeID = "";
        private string strPenm = "";
        string strType = "";
        Admin objAdmin;
        private DataTable dtData = new DataTable();


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
        #endregion

        #region 构造函数
        public Admin_Material_personnel(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, strProgid);
                objAdmin = new Admin(UserData, strProgid);
                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    Init();
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = -1;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = -1;
                    }
                    gbox3.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region  初始化
        private void Init()
        {
            strWerks = "";
            strLgort = "";
            strType = "";
            strMatnr = "";
            strMqcID = "";
            strMqcnm = "";
            strPeID = "";
            strPenm = "";
            txtMatnr.Text = "";
            txtMqcID.Text = "";
            txtMqcnm.Text = "";
            txtPeID.Text = "";
            txtPenm.Text = "";
            btnSAVE.Enabled = false;
            gbox3.Enabled = true;
            RBadd.Checked = false;
            RBdel.Checked = false;
            RBmdf.Checked = false;
        }
        #endregion

        #region 操作类型
        private void RBadd_CheckedChanged(object sender, EventArgs e)
        {
            strType = "add";
            gbox3.Enabled = false;
            btnSAVE.Enabled = true;
        }

        private void RBdel_CheckedChanged(object sender, EventArgs e)
        {
            strType = "del";
            gbox3.Enabled = false;
            btnSAVE.Enabled = true;
        }

        private void RBmdf_CheckedChanged(object sender, EventArgs e)
        {
            strType = "modify";
            gbox3.Enabled = false;
            btnSAVE.Enabled = true;
        }
        #endregion

        #region  取值
        /// <summary>
        /// 取值
        /// </summary>
        private void variable()
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            strMatnr = txtMatnr.Text.ToString().Trim();
            strMqcID = txtMqcID.Text.ToString().Trim();
            strMqcnm = txtMqcnm.Text.ToString().Trim();
            strPeID = txtPeID.Text.ToString().Trim();
            strPenm = txtPenm.Text.ToString().Trim();
        }
        #endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                //DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                //dgvcSelect.DataPropertyName = "Select";
                //dgvcSelect.HeaderText = "选择";
                //dgvcSelect.Width = 50;
                //dgvcSelect.Selected = false;
                //this.dgvData.Columns.Add(dgvcSelect);

                //MANDT	COMCD	WERKS	LGORT	MATNR	MQCID	MQCNM	PEID	PENM	CRDAT	MODAT	REMAK1	ULFLG


                DataGridViewTextBoxColumn dgvcMANDT = new DataGridViewTextBoxColumn();
                dgvcMANDT.DataPropertyName = "MANDT";
                dgvcMANDT.HeaderText = "MANDT";
                dgvcMANDT.Width = 50;
                dgvcMANDT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMANDT);

                DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                dgvcCOMCD.DataPropertyName = "COMCD";
                dgvcCOMCD.HeaderText = "COMCD";
                dgvcCOMCD.Width = 50;
                dgvcCOMCD.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCOMCD);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "WERKS";
                dgvcWERKS.ReadOnly = true;
                dgvcWERKS.Width = 50;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "LGORT";
                dgvcLGORT.Width = 50;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.ReadOnly = true;
                dgvcMATNR.Width = 100;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcMQCID = new DataGridViewTextBoxColumn();
                dgvcMQCID.DataPropertyName = "MQCID";
                dgvcMQCID.HeaderText = "MQCID";
                dgvcMQCID.Width = 70;
                dgvcMQCID.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMQCID);


                DataGridViewTextBoxColumn dgvcMQCNM = new DataGridViewTextBoxColumn();
                dgvcMQCNM.DataPropertyName = "MQCNM";
                dgvcMQCNM.HeaderText = "MQCNM";
                dgvcMQCNM.Width = 100;
                dgvcMQCNM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMQCNM);

                DataGridViewTextBoxColumn dgvcPEID = new DataGridViewTextBoxColumn();
                dgvcPEID.DataPropertyName = "PEID";
                dgvcPEID.HeaderText = "PEID";
                dgvcPEID.Width = 70;
                dgvcPEID.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcPEID);


                DataGridViewTextBoxColumn dgvcPENM = new DataGridViewTextBoxColumn();
                dgvcPENM.DataPropertyName = "PENM";
                dgvcPENM.HeaderText = "PENM";
                dgvcPENM.Width = 100;
                dgvcPENM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcPENM);


                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "CRDAT";
                dgvcCRDAT.ReadOnly = true;
                dgvcCRDAT.Width = 100;
                this.dgvData.Columns.Add(dgvcCRDAT);

                DataGridViewTextBoxColumn dgvcMODAT = new DataGridViewTextBoxColumn();
                dgvcMODAT.DataPropertyName = "MODAT";
                dgvcMODAT.HeaderText = "MODAT";
                dgvcMODAT.Width = 100;
                dgvcMODAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMODAT);


                DataGridViewTextBoxColumn dgvcREMAK1 = new DataGridViewTextBoxColumn();
                dgvcREMAK1.DataPropertyName = "REMAK1";
                dgvcREMAK1.HeaderText = "REMAK1";
                dgvcREMAK1.Width = 100;
                dgvcREMAK1.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcREMAK1);


                DataGridViewTextBoxColumn dgvcULFLG = new DataGridViewTextBoxColumn();
                dgvcULFLG.DataPropertyName = "ULFLG";
                dgvcULFLG.HeaderText = "ULFLG";
                dgvcULFLG.Width = 100;
                dgvcULFLG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcULFLG);

                dgvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region  btn动作
        //查询
        private void btnQUERY_Click(object sender, EventArgs e)
        {
            variable();
            dtData = objAdmin.Material_personnel_query(strWerks, strLgort, strMatnr);
            ShowDataGrid();
            if (dtData.Rows.Count < 0)
            {
                MessageBox.Show("NO Data");
                return;
            }
        }

        //刷新
        private void btnREFRESH_Click(object sender, EventArgs e)
        {
            Init();
            cmbWerks.SelectedIndex = -1;
            cmbLgort.SelectedIndex = -1;
            gbox3.Enabled = true;
        }

        //保存
        private void btnSAVE_Click(object sender, EventArgs e)
        {
            variable();
            #region  判断信息是否填写正确
            if (strType == "add" || strType == "modify")
            {
                if (strMatnr == "" || strMqcID == "" || strMqcnm == "" || strPeID == "" || strPenm == "")
                {
                    MessageBox.Show("请确认信息是否填写完整");
                    return;
                }
                if (strMqcID.Length != 8 || strPeID.Length != 8)
                {
                    MessageBox.Show("工号长度为8位！");
                    return;
                }
            }
            else if (strType == "del")
            {
                if (strMatnr == "")
                {
                    MessageBox.Show("请确认料号填写正确");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择类型");
                btnSAVE.Enabled = false;
                return;
            }
            if (strWerks == "" || strLgort == "")
            {
                MessageBox.Show("请确认厂区和仓别是否选择");
                return;
            }
            #endregion
            bool falg = objAdmin.Material_personnel_modify(strWerks, strLgort, strType, strMatnr, strMqcID, strMqcnm, strPeID, strPenm);
            if (falg)
            {
                MessageBox.Show("成功");
            }
            else
            {
                MessageBox.Show("失败");
            }
        }

        #endregion

        #region  自动填充姓名
        private void txtMqcID_TextChanged(object sender, EventArgs e)
        {
            if (txtMqcID.Text.ToString().Trim().Length == 8)
            {
                DataTable dtEmployeeData = new DataTable();
                DataTable depttable = new DataTable();
                //EmployeeData.QueryEmployeeData hr = new EmployeeData.QueryEmployeeData();
                Admin objAdmin = new Admin(UserData, Progid);
                dtEmployeeData = objAdmin.GetEmployeeData(txtMqcID.Text.ToString().Trim());
                depttable = dtEmployeeData;
                if (depttable.Rows.Count > 0)
                {
                    txtMqcnm.Text = depttable.Rows[0]["chinam"].ToString();
                }
            }
        }

        private void txtPeID_TextChanged(object sender, EventArgs e)
        {
            if (txtPeID.Text.ToString().Trim().Length == 8)
            {
                DataTable GetEmployeesData = new DataTable();
                DataTable depttable = new DataTable();
                // EmployeeData.QueryEmployeeData hr = new EmployeeData.QueryEmployeeData();
                Admin objAdmin = new Admin(UserData, Progid);
                GetEmployeesData = objAdmin.GetEmployeeData(txtPeID.Text.ToString().Trim());
                depttable = GetEmployeesData;
                if (depttable.Rows.Count > 0)
                {
                    txtPenm.Text = depttable.Rows[0]["chinam"].ToString();
                }
            }
        }
        #endregion

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
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
                cmbWerks.Items.Clear();
                dtTemp = objStorageData.GetInfoStorageInType("", "", "MQC PE");
                dtTemp.DefaultView.Sort = "WERKS,LGORT";
                DataTable dtSort = dtTemp.DefaultView.ToTable();
                for (int i = 0; i < dtSort.Rows.Count; i++)
                {
                    if (!cmbWerks.Items.Contains(dtSort.Rows[i]["WERKS"].ToString()))
                    {
                        cmbWerks.Items.Add(dtSort.Rows[i]["WERKS"].ToString());
                    }
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
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
                stsWarning.Text = string.Empty;
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objStorageData.GetInfoStorageInType(strWerks, "", "MQC PE");
                }
                else
                {
                    dtTemp = objStorageData.GetInfoStorageInType("", "", "MQC PE");
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

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
            cmbLgort.SelectedIndex = -1;
        }


    }
}
