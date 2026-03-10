using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using QWMS.Common;
using QCI.QWMS;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace QWMS
{
    public partial class Replenishment_SetupMailNotice : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
		private string strUsrnm = "";
		private string strProgid = "";
		private string strWerks = "";
		private string strLgort = "";
		private string strSttyp = "";
		private string strLotyp = "";
		private DataTable dtData;
		private Admin objAdmin;
		private PlantData objPlantData;
		private Authority objAuthority;
		private Replenishment objReplenishment;

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
        public Replenishment_SetupMailNotice()
        {
            InitializeComponent();
        }
        public Replenishment_SetupMailNotice(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objReplenishment = new Replenishment(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                // 检查权限
                if (!objReplenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlType();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    if (cmbType.Items.Count > 0)
                    {
                        this.cmbType.SelectedIndex = 0;
                    }
                    if (cmbWerks.Items.Count > 0)
                    {
                        strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                        strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                        ShowDataGridView(strWerks, strLgort);
                    }
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
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
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

        #region 绑定仓别
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
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

        #endregion

        #region 绑定Replentishment Tyepe
        private void ShowDdlType()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbType.Items.Clear();
                dtTemp = objReplenishment.GetDdlReplentishmentType();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbType.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlType()");
            }
        }
		
        #endregion

        #region ShowDataGridView
        private void ShowDataGridView(string  strWerks, string strLgort)
        {
            try
            {
                objReplenishment = new Replenishment(UserData, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), Progid);
                dtData = objReplenishment.QueryMailUser("", "");

                DataColumn cSelect = new DataColumn("Select", typeof(bool));
                dtData.Columns.Add(cSelect);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["Select"] = false;
                }

                this.dgvData.AutoGenerateColumns = false;
                this.dgvData.Columns.Clear();

                DataGridViewCheckBoxColumn dgcbSelect = new DataGridViewCheckBoxColumn();
                dgcbSelect.DataPropertyName = "Select";
                dgcbSelect.HeaderText = "Select";
                this.dgvData.Columns.Add(dgcbSelect);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcMtype = new DataGridViewTextBoxColumn();
                dgvcMtype.DataPropertyName = "MTYPE";
                dgvcMtype.HeaderText = "Type";
                dgvcMtype.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMtype);


                DataGridViewTextBoxColumn dgvcEmail = new DataGridViewTextBoxColumn();
                dgvcEmail.DataPropertyName = "EMAIL";
                dgvcEmail.HeaderText = "Mail";
                dgvcEmail.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcEmail);


                dgvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }
        #endregion

        #region cmbWerks Selected Index Changed Event
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbLgort.Enabled = true;
                if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    ShowDataGridView(strWerks, strLgort);
                }
                ShowDdlLgort();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion


        #region cmbLgort Selected Index Changed Event
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtMail.Enabled = true;
                if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    ShowDataGridView(strWerks, strLgort);
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            DataTable dtTemp = new DataTable();
            string[] arrTemp;
            string strType = "";
            try
            {
                arrTemp = cmbType.Items[cmbType.SelectedIndex].ToString().Split(new char[] { ':' });
                strType = arrTemp[0].ToString();
                // 
                if (this.txtMail.Text.Trim() == "" || cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
                {
                    stsWarning.Text = "Plant, storage and mail can't be empty!!";
                    return;
                }
                objReplenishment = new Replenishment(UserData, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), Progid);
                dtData = objReplenishment.QueryMailUser(strType, this.txtMail.Text.Trim());

                // 检查维护的Mail 是否已存在
                if (dtData.Rows.Count > 0)
                {
                    stsWarning.Text = "The mail has existed in the system!!";
                    return;
                }
                // 新增 Replenishment Mail
                if (objReplenishment.AddMailUser(strType, this.txtMail.Text.Trim()))
                {
                    stsWarning.Text = "Add OK!";
                    this.txtMail.Text = "";
                }
                else
                {
                    stsWarning.Text = "Add Fail!" + objReplenishment.ERRMSG;
                }

                ShowDataGridView(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString());
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            ArrayList arrMail = new ArrayList();
            ArrayList arrType = new ArrayList();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtData.Rows[i]["Select"]))
                {
                    arrType.Add(dtData.Rows[i]["MTYPE"]);
                    arrMail.Add(dtData.Rows[i]["EMAIL"]);
                }
            }
            if (arrMail.Count == 0)
            {
                MessageBox.Show("Please select one mail!!");
                return;
            }


            objReplenishment = new Replenishment(UserData, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), Progid);
            if (objReplenishment.DeleteMailUserArray(arrType, arrMail))
            {
                stsWarning.Text = "Delete OK!";
                ShowDataGridView(strWerks, strLgort);
            }
            else
            {
                stsWarning.Text = "Delete Fail!" + objReplenishment.ERRMSG;
            }

            ShowDataGridView(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString());
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 调整布局
        private void Replenishment_SetupMailNotice_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion
    }
}
