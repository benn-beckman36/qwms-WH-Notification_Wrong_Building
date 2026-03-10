using QCI.QWMS;
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

namespace QWMS.MAAUT
{
    public partial class Admin_Intelligent_personnel : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
		private string strUsrnm = "";
		private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strUserID = "";
        string strType = "";
        DataTable dtHR = new DataTable();
        DataTable dtData = new DataTable();
        Admin objAdmin;
        Authority objAuthority = null;
        //private BorrowMateria objBorrowMateria;
  

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
        public Admin_Intelligent_personnel(UserInfo _UserData, string strProgid)
        {
            UserData = _UserData;

         
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            InitializeComponent();
            try
            {
                objAuthority = new Authority(UserData);
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, strProgid);
                objAdmin = new Admin(UserData,strProgid);
                //objBorrowMateria = new BorrowMateria(UserData, Progid);
                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    
					//秀出Status的資料
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
                    
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
					if(cmbLgort.Items.Count > 0)
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
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }
        #endregion

        

        #region 绑定厂区
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
        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        #region  初始化
        private void Init()
        {
            strWerks = cmbWerks.Text.ToString();
            strLgort = cmbLgort.Text.ToString();
            txtUserID.Text = "";
            btnSave.Enabled = false;
            gbox3.Enabled = true;
            RBadd.Checked = false;
            RBdel.Checked = false;
            RBmdf.Checked = false;
        }
        #endregion

        #region 绑定仓别

        private void ShowDdlLgort()
        {
            try
            {
                
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

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort.Items.Clear();
                    strLgort = string.Empty;
                }
                else
                {
                    cmbLgort.Items.Clear();
                    cmbLgort.Items.Add("");
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
        #endregion

        #region 操作类型
        private void RBadd_CheckedChanged(object sender, EventArgs e)
        {
            strType = "add";
            gbox3.Enabled = false;
            btnSave.Enabled = true;
        }

        private void RBdel_CheckedChanged(object sender, EventArgs e)
        {
            strType = "del";
            gbox3.Enabled = false;
            btnSave.Enabled = true;
        }

        private void RBmdf_CheckedChanged(object sender, EventArgs e)
        {
            strType = "modify";
            gbox3.Enabled = false;
            btnSave.Enabled = true;
        }
        #endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {


                DataGridViewTextBoxColumn dgvcSite = new DataGridViewTextBoxColumn();
                dgvcSite.DataPropertyName = "SITE";
                dgvcSite.HeaderText = "Site";
                dgvcSite.Width = 50;
                dgvcSite.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcSite);

                DataGridViewTextBoxColumn dgvcUserID = new DataGridViewTextBoxColumn();
                dgvcUserID.DataPropertyName = "USRID";
                dgvcUserID.HeaderText = "工号";
                dgvcUserID.Width = 50;
                dgvcUserID.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcUserID);

                DataGridViewTextBoxColumn dgvcUserName = new DataGridViewTextBoxColumn();
                dgvcUserName.DataPropertyName = "USRNM";
                dgvcUserName.HeaderText = "姓名";
                dgvcUserName.ReadOnly = true;
                dgvcUserName.Width = 50;
                this.dgvData.Columns.Add(dgvcUserName);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "厂区";
                dgvcWerks.Width = 50;
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "仓别";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 100;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "REMAK1";
                dgvcRemark.HeaderText = "备注";
                dgvcRemark.ReadOnly = true;
                dgvcRemark.Width = 100;
                this.dgvData.Columns.Add(dgvcRemark);

                //DataGridViewTextBoxColumn dgvcMQCID = new DataGridViewTextBoxColumn();
                //dgvcMQCID.DataPropertyName = "MQCID";
                //dgvcMQCID.HeaderText = "MQCID";
                //dgvcMQCID.Width = 70;
                //dgvcMQCID.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcMQCID);


                //DataGridViewTextBoxColumn dgvcMQCNM = new DataGridViewTextBoxColumn();
                //dgvcMQCNM.DataPropertyName = "MQCNM";
                //dgvcMQCNM.HeaderText = "MQCNM";
                //dgvcMQCNM.Width = 100;
                //dgvcMQCNM.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcMQCNM);

                //DataGridViewTextBoxColumn dgvcPEID = new DataGridViewTextBoxColumn();
                //dgvcPEID.DataPropertyName = "PEID";
                //dgvcPEID.HeaderText = "PEID";
                //dgvcPEID.Width = 70;
                //dgvcPEID.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcPEID);


                //DataGridViewTextBoxColumn dgvcPENM = new DataGridViewTextBoxColumn();
                //dgvcPENM.DataPropertyName = "PENM";
                //dgvcPENM.HeaderText = "PENM";
                //dgvcPENM.Width = 100;
                //dgvcPENM.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcPENM);


                //DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                //dgvcCRDAT.DataPropertyName = "CRDAT";
                //dgvcCRDAT.HeaderText = "CRDAT";
                //dgvcCRDAT.ReadOnly = true;
                //dgvcCRDAT.Width = 100;
                //this.dgvData.Columns.Add(dgvcCRDAT);

                //DataGridViewTextBoxColumn dgvcMODAT = new DataGridViewTextBoxColumn();
                //dgvcMODAT.DataPropertyName = "MODAT";
                //dgvcMODAT.HeaderText = "MODAT";
                //dgvcMODAT.Width = 100;
                //dgvcMODAT.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcMODAT);


                //DataGridViewTextBoxColumn dgvcREMAK1 = new DataGridViewTextBoxColumn();
                //dgvcREMAK1.DataPropertyName = "REMAK1";
                //dgvcREMAK1.HeaderText = "REMAK1";
                //dgvcREMAK1.Width = 100;
                //dgvcREMAK1.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcREMAK1);


                //DataGridViewTextBoxColumn dgvcULFLG = new DataGridViewTextBoxColumn();
                //dgvcULFLG.DataPropertyName = "ULFLG";
                //dgvcULFLG.HeaderText = "ULFLG";
                //dgvcULFLG.Width = 100;
                //dgvcULFLG.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcULFLG);

                dgvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {

            dtData = objAdmin.PermissionQuery(strMandt, strComcd, cmbWerks.Text.ToString(), txtUserID.Text.ToString());
            if (dtData.Rows.Count > 0)
            {
                ShowDataGrid();
            }
            else
            {
                MessageBox.Show("无数据，请确认!!");
                dtHR.Clear();
                dtData.Clear();
                return;
            }
        }

        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                stsWarning.Text = "";
                if (txtUserID.Text.Trim() == "")
                {
                    stsWarning.Text = "请输入工号！！";
                }
                else
                {
                    try
                    {
                        bool blflag = false;
                        Admin objAdmin = new Admin(UserData, Progid);
                        dtHR = objAdmin.CheckHR(txtUserID.Text.Trim());
                        if (dtHR != null && dtHR.Rows.Count > 0)
                        {
                            if (dtHR.Rows[0]["onjobs"].ToString() != "1")
                            {
                                MessageBox.Show("此员工未在职，请确认!!");
                            }
                            else
                            {
                                blflag = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("HR无工号：" + txtUserID.Text.Trim() + "的资料,验证失败");
                        }
                        if (blflag)
                        {
                            lblIQCCheck.Text = "员工身份验证OK";
                            Permission();
                            ShowDataGrid();
                        }
                        else
                        {
                            lblIQCCheck.Text = "请按Enter键验证员工身份";
                            dtHR.Clear();
                            dtData.Clear();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = ex.Message;
                        return;
                    }
                }
            }
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text.ToString() == "")
            {
                MessageBox.Show("工号不能为空，请确定");
                return;
            }
            else if (txtUserID.Text.Length != 8)
            {
                MessageBox.Show("工号长度为8位，请确定");
                return;
            }
            bool flg; 
            if (dtData.Rows.Count > 0)
            {
                string UserName = dtHR.Rows[0]["ChineseName"].ToString();
                if (strType == "add")
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (dtData.Rows[i]["WERKS"].ToString() == cmbWerks.Text.ToString())
                        {
                            MessageBox.Show("员工已有厂区" + cmbWerks.Text.ToString() + "初始权限，要增加仓别权限请选择‘修改’");
                            return;
                        }
                    }
                }
                else if (strType == "modify")
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (dtData.Rows[i]["WERKS"].ToString() == cmbWerks.Text.ToString())
                        {
                            DataTable dt = objAdmin.PermissionQuery(strMandt, strComcd, strWerks, txtUserID.Text.ToString());
                            if (dt.Rows[0]["LGORT"].ToString().Contains(cmbLgort.Text.ToString()))
                            {
                                stsWarning.Text = "已有该仓别权限";
                                return;
                            }
                            flg = objAdmin.Intelligent_personnel_modify(strMandt, strComcd, cmbWerks.Text.ToString(), cmbLgort.Text.ToString(), strType, txtUserID.Text.ToString(), UserName, strUsrnm);
                            if (flg)
                            {
                                stsWarning.Text = "Update OK!!";
                            }
                            else
                            {
                                stsWarning.Text = "Update fail!";
                            }
                            return;
                        }
                    }
                    MessageBox.Show("员工没有厂区" + cmbWerks.Text.ToString() + "初始权限，请选择‘新增’增加厂区权限");
                    return;

                }
                //for (int i = 0; i < dtData.Rows.Count;i++ )
                //{
                //    if (strType == "add" && dtData.Rows[i]["WERKS"] == cmbWerks.Text.ToString())
                //    {
                //        MessageBox.Show("员工已有厂区" + cmbWerks.Text.ToString() + "初始权限，要增加仓别权限请选择‘修改’");
                //        return;
                //    }
                //    else if (strType == "modify" && dtData.Rows[i]["WERKS"] == "无")
                //    {
                //        MessageBox.Show("员工没有厂区" + cmbWerks.Text.ToString() + "初始权限，请选择‘新增’增加厂区权限");
                //        return;
                //    }
                //}
                //UserName工号对应的名字
                
                flg = objAdmin.Intelligent_personnel_modify(strMandt, strComcd, cmbWerks.Text.ToString(), cmbLgort.Text.ToString(), strType, txtUserID.Text.ToString(), UserName, strUsrnm);
                if (flg)
                {
                    stsWarning.Text = "Save OK!!";
                }
                else 
                {
                    stsWarning.Text = "Save fail!";
                }
            }
            else 
            {
                MessageBox.Show("员工验证未通过，请重试!!");
            }
        }

        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //Init();
            txtUserID.Text = "";
            stsWarning.Text = "";
            lblIQCCheck.Text = "请按Enter键或者点击Query验证员工身份";
            dtHR.Clear();
            dtData.Clear();
            RBadd.Checked = false;
            RBdel.Checked = false;
            RBmdf.Checked = false;
            btnSave.Enabled = false;
            gbox3.Enabled = true;
        }

        #endregion

        //#region 不用 身份验证
        //private bool CheckID(string UserID)
        //{
        //    bool bl = false;
        //    string strNam = "";
        //    DataSet ds = new DataSet();
        //    //string strTextFail =  "刷卡验证失败";
        //    QWMS.EmployeeData.QueryEmployeeData objHR = new EmployeeData.QueryEmployeeData();
        //    ds = objHR.GetEmployeeDataByComcod("QSMC", txtUserID.Text.ToString());
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        dtHR = ds.Tables[0];
        //    }
        //    //dtHR = objBorrowMateria.GetHR(UserID.Trim());
        //    if (dtHR.Rows.Count > 0)
        //    {
        //        if(dtHR.Rows[0]["onjobs"].ToString()=="离职")
        //        {
        //            MessageBox.Show("此员工已离职，请确认!!");
        //            return bl;
        //        }
                
        //        bl=true;
        //        return bl;
        //    }
        //    else
        //    {
        //        MessageBox.Show("HR无工号：" + UserID + "的资料,验证失败");
        //        return bl;
        //    }
        // }
            
        //#endregion

        #region 权限查询
        private void Permission()
        {
            stsWarning.Text = "";
            dtData = objAdmin.PermissionQuery(strMandt, strComcd, cmbWerks.Text.ToString(), txtUserID.Text.ToString());
            DataRow dt = dtData.NewRow();
            if (dtData.Rows.Count > 0)
            {
                for (int i = 0; i < dtData.Rows.Count;i++ )
                {
                    dtData.Rows[i]["SITE"] = dtHR.Rows[0]["CompanyCode"].ToString();
                }
                    
            }
            else 
            {
                dt["SITE"] = dtHR.Rows[0]["CompanyCode"].ToString();
                dt["USRID"] = dtHR.Rows[0]["UserName"].ToString();
                dt["USRNM"] = dtHR.Rows[0]["ChineseName"].ToString();
                dt["WERKS"] = "无";
                dt["LGORT"] = "无";
                dt["REMAK1"] = "无权限，请新增";
                dtData.Rows.Add(dt);
            }
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    


    }
}
