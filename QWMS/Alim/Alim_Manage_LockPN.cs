using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class Alim_Manage_LockPN : Form
    {
        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        /// <summary>
        /// 该页面异动
        /// </summary>
        private string strProgid = "";//QWMS系统基础资料维护权限
        /// <summary>
        /// 所选厂区
        /// </summary>
        private string strWerks = "";
        /// <summary>
        /// 所选仓别
        /// </summary>
        private string strLgort = "";
        private string strWerks_locat = "";
        private string strLgort_locat = "";
        private string strType = "";
        /// <summary>
        /// DateCode
        /// </summary>
        private string strDateCode
        {
            get { return txtDateCode.Text.ToString().Trim(); }
        }
        /// <summary>
        /// LotCode
        /// </summary>
        private string strLotCode
        {
            get { return txtLotcode.Text.ToString().Trim(); }
        }
        /// <summary>
        /// 所填料号
        /// </summary>
        private string strPN
        {
            get { return txtPN.Text.ToString().Trim(); }
        }
        /// <summary>
        /// 厂商代码
        /// </summary>
        private string strLIFNR
        {
            get { return txtLifnr.Text.ToString().Trim(); }
        }
        /// <summary>
        /// 版本
        /// </summary>
        private string strCharg
        {
            get { return txtCharg.Text.ToString().Trim(); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        private string strRemak
        {
            get { return txtRemak.Text.ToString().Trim(); }
        }

        DataTable dtData = new DataTable();
        DataTable dtData_locat = new DataTable();


        QCI.QWMS.Alim objAlim;
        #endregion

        #region 构造函数
        public Alim_Manage_LockPN(UserInfo varUserData, string Progid)
        {
            InitializeComponent();
            UserData = varUserData;
            strMandt = UserData.Client;
            strComcd = UserData.CompanyCode;
            strUsrnm = UserData.UserId;
            strProgid = Progid;

            objAlim = new QCI.QWMS.Alim(UserData, strProgid);

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
                    ShowDdlWerks_locat();//厂区_locat
                    ShowDdlLgort_locat();//仓别_locat
                    btnRefresh_Click(null,null);
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

        #region 厂区和仓别
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
            cmbLgort.SelectedIndex = -1;
            ShowDdlLgort();
        }

        #region 锁locat界面
        private void ShowDdlWerks_locat()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                cmbWerks_locat.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks_locat.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks_locat()");
            }
        }

        private void ShowDdlLgort_locat()
        {
            try
            {
                stsWarning.Text = string.Empty;
                DataTable dtTemp = new DataTable();
                if (cmbWerks_locat.SelectedIndex != -1)
                {
                    strWerks_locat = cmbWerks_locat.Items[cmbWerks_locat.SelectedIndex].ToString();
                    dtTemp = objAlim.CheckLgortAuthority(strWerks_locat);
                }
                else
                {
                    dtTemp = objAlim.CheckLgortAuthority();
                }
                if (cmbLgort_locat.SelectedIndex != -1)
                {
                    strLgort_locat = cmbLgort_locat.Items[cmbLgort_locat.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort_locat.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort_locat.Items.Clear();
                    strLgort_locat = string.Empty;
                }
                else
                {
                    cmbLgort_locat.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort_locat.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                        if (dtTemp.Rows[i]["CTRLC1"].ToString() == strLgort_locat && strLgort_locat != "")
                        {
                            cmbLgort_locat.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort_locat()");
            }
        }

        private void cmbWerks_locat_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbLgort_locat.SelectedIndex = -1;
            ShowDdlLgort_locat();
        }
        #endregion

        /// <summary>
        /// 获取所选的厂区和仓别
        /// </summary>
        public void GetWerksAndLgort()
        {
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
        }
        #endregion

        #region ShowDataView
        private void ShowDataView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                //if (!dtData.Columns.Contains("Select"))
                //{
                //    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                //    dtData.Columns.Add(cSelect);
                //}

                //DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                //dgvcSelect.DataPropertyName = "Select";
                //dgvcSelect.HeaderText = "选择";
                //dgvcSelect.Width = 50;
                //dgvcSelect.Selected = false;
                //this.gvData.Columns.Add(dgvcSelect);


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

                //DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                //dgvcLOCAT.DataPropertyName = "LOCAT";
                //dgvcLOCAT.HeaderText = "储位";
                //dgvcLOCAT.ReadOnly = true;
                //this.gvData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "厂商代码";
                dgvcLIFNR.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "DateCode";
                dgvcDACOD.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDACOD);

                DataGridViewTextBoxColumn dgvcLOCOD = new DataGridViewTextBoxColumn();
                dgvcLOCOD.DataPropertyName = "LOCOD";
                dgvcLOCOD.HeaderText = "LotCode";
                dgvcLOCOD.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLOCOD);

                DataGridViewTextBoxColumn dgvcREMAK = new DataGridViewTextBoxColumn();
                dgvcREMAK.DataPropertyName = "REMAK";
                dgvcREMAK.HeaderText = "REMAK";
                dgvcREMAK.ReadOnly = true;
                this.gvData.Columns.Add(dgvcREMAK);

                DataGridViewTextBoxColumn dgvcCRNAM = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRNAM";
                dgvcCRNAM.HeaderText = "CRNAM";
                dgvcCRNAM.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCRNAM);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "CRDAT";
                dgvcCRDAT.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCRDAT);

                this.gvData.DataSource = dtData;
                lbrecords.Text = dtData.Rows.Count.ToString() + " records";
                gvData.ClearSelection();
                gvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataView()");
            }
        }
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetWerksAndLgort();
            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "请选择厂区和仓别";
                return;
            }
            dtData = objAlim.QueryAlim_LockPN(strWerks,strLgort,strPN,strRemak,strCharg,strLIFNR,strDateCode,strLotCode);
            ShowDataView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GetWerksAndLgort();
            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "请选择厂区和仓别";
                return;
            }
            if (strPN == "")
            {
                stsWarning.Text = "请输入料号";
                return;
            }
            if(strType=="")
            {
                stsWarning.Text = "请选择Type";
                return;
            }
            else if (strType == "ADD")
            {
                //检查数据
                if (strRemak == "")
                {
                    stsWarning.Text = "备注不能为空";
                    return;
                }
                //检查库存是否有料号或储位信息
                if(objAlim.QuaryAlimAlitm(strWerks,strLgort,strPN,"","","'N','H','L'","'N'").Rows.Count==0)
                {
                    stsWarning.Text = "无库存信息，添加失败";
                    return;
                }
                 
                //insert
                if (objAlim.ADDAlim_LockPN(strWerks,strLgort,strPN,strCharg,strLIFNR,strDateCode,strLotCode,strRemak))
                {
                    stsWarning.Text = "增加禁用信息成功";
                    //查询
                    dtData = objAlim.QueryAlim_LockPN(strWerks, strLgort, strPN,"","","","","");
                    ShowDataView();
                }
                else
                {
                    stsWarning.Text="增加禁用信息失败，请联系QWMS负责人";
                    return;
                }
            }
            else if (strType == "DELETE")
            {
                if (objAlim.QueryAlim_LockPN(strWerks, strLgort, strPN, "", strCharg, strLIFNR, strDateCode, strLotCode).Rows.Count == 0)
                {
                    stsWarning.Text = "无此禁用信息";
                    return;
                }
                string message = "是否删除禁用逻辑\r\n料号:" + strPN + "\r\n版本:" + strCharg + "\r\nLIFNR:" + strLIFNR + "\r\nDACOD:" + strDateCode + "\r\nLOCOD:" + strLotCode + "";
                DialogResult result;
                result = MessageBox.Show(this, message, "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (objAlim.DeleteAlim_LockPN(strWerks, strLgort, strPN, strCharg, strLIFNR, strDateCode, strLotCode))
                    {
                        stsWarning.Text = "删除禁用信息成功";
                    }
                    else
                    {
                        stsWarning.Text = "删除禁用信息失败，请联系QWMS负责人";
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            rbAdd.Checked = false;
            rbDelete.Checked = false;
            gbType.Enabled = true;
            strType = "";
        }

        private void rbAdd_CheckedChanged(object sender, EventArgs e)
        {
            strType = "ADD";
            gbType.Enabled = false;
        }

        private void rbDelete_CheckedChanged(object sender, EventArgs e)
        {
            strType = "DELETE";
            gbType.Enabled = false;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            GetWerksAndLgort();
            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "请选择厂区和仓别";
                return;
            }
            //立即执行
            if (objAlim.Run_NowAlim_LockPN(strWerks,strLgort))
            {
                stsWarning.Text = "执行成功";
            }
            else
            {
                stsWarning.Text = "执行失败，请联系QWMS负责人";
            }
        }

        #region 锁LOCAT界面
        private void ShowDataView_locat()
        {
            try
            {
                this.gvData_locat.AutoGenerateColumns = false;
                this.gvData_locat.Columns.Clear();

                //if (!dtData.Columns.Contains("Select"))
                //{
                //    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                //    dtData.Columns.Add(cSelect);
                //}

                //DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                //dgvcSelect.DataPropertyName = "Select";
                //dgvcSelect.HeaderText = "选择";
                //dgvcSelect.Width = 50;
                //dgvcSelect.Selected = false;
                //this.gvData_locat.Columns.Add(dgvcSelect);


                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.ReadOnly = true;
                this.gvData_locat.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.ReadOnly = true;
                this.gvData_locat.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "储位";
                dgvcLOCAT.ReadOnly = true;
                this.gvData_locat.Columns.Add(dgvcLOCAT);

                this.gvData_locat.DataSource = dtData_locat;
                gvData_locat.ClearSelection();
                gvData_locat.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataView_locat()");
            }
        }
        private void btnQuery_locat_Click(object sender, EventArgs e)
        {
            if (cmbWerks_locat.SelectedIndex != -1)
            {
                strWerks_locat = cmbWerks_locat.SelectedItem.ToString();
            }
            else
            {
                strWerks_locat = string.Empty;
            }
            if (cmbLgort_locat.SelectedIndex != -1)
            {
                strLgort_locat = cmbLgort_locat.SelectedItem.ToString();
            }
            else
            {
                strLgort_locat = string.Empty;
            }
            dtData_locat = objAlim.QuaryAlimALHED_BanLocat(strWerks_locat,strLgort_locat);
            ShowDataView_locat();
        }

        private void btnADD_locat_Click(object sender, EventArgs e)
        {
            if (cmbWerks_locat.SelectedIndex != -1)
            {
                strWerks_locat = cmbWerks_locat.SelectedItem.ToString();
            }
            else
            {
                strWerks_locat = string.Empty;
            }
            if (cmbLgort_locat.SelectedIndex != -1)
            {
                strLgort_locat = cmbLgort_locat.SelectedItem.ToString();
            }
            else
            {
                strLgort_locat = string.Empty;
            }
            string strLocat = txtLocat.Text.ToString().Trim();
            string strRemak_locat = txtRemak_locat.Text.ToString().Trim();
            if (strLocat == "" || strRemak_locat == "")
            {
                stsWarning.Text = "请检查储位和备注，均不能为空";
                return;
            }
            //增加禁用逻辑
            if (objAlim.AddAlimAlhed_BanLocat(strWerks_locat, strLgort_locat, strLocat, strRemak_locat))
            {
                stsWarning.Text = "禁用储位成功";
            }
            else
            {
                stsWarning.Text = "禁用储位失败，请联系QWMS负责人";
            }
        }

        private void btnDelete_locat_Click(object sender, EventArgs e)
        {
            if (cmbWerks_locat.SelectedIndex != -1)
            {
                strWerks_locat = cmbWerks_locat.SelectedItem.ToString();
            }
            else
            {
                strWerks_locat = string.Empty;
            }
            if (cmbLgort_locat.SelectedIndex != -1)
            {
                strLgort_locat = cmbLgort_locat.SelectedItem.ToString();
            }
            else
            {
                strLgort_locat = string.Empty;
            }
            string strLocat = txtLocat.Text.ToString().Trim();
            if (strLocat == "")
            {
                stsWarning.Text = "请检查储位,不能为空";
                return;
            }
            if (objAlim.DeleteAlimAlhed_BanLocat(strWerks_locat,strLgort_locat,strLocat))
            {
                stsWarning.Text = "恢复储位成功";
            }
            else
            {
                stsWarning.Text = "恢复储位失败，请联系QWMS负责人";
            }
        }
        #endregion






    }
}
