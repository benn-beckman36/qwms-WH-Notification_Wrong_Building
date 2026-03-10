using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Replenishment_MaintainMapping : Form
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

        public string Matnr
        {
            get
            {
                return txtMatnr.Text.Trim();
            }
            set
            {
                txtMatnr.Text = value;
            }
        }

        public string Locat
        {
            get
            {
                return txtLocat.Text.Trim();
            }
            set
            {
                txtLocat.Text = value;
            }
        }
        #endregion

        #region 构造函数
        public Replenishment_MaintainMapping()
        {
            InitializeComponent();
        }

        public Replenishment_MaintainMapping(UserInfo varUserData, string strProgid)
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
                    ShowDdlInsmk();
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
                    if (cmbWerks.Items.Count > 0 && cmbLgort.Items.Count > 0)
                    {
                        strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                        strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                        ShowDataGridView(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString());
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

        #region 绑定库位
        private void ShowDdlInsmk()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbInsmk.Items.Clear();
                dtTemp = objPlantData.GetDdlInsmk();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
            }
        }
        #endregion

        #region cmbWerks Selected Index Changed Event
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region cmbLgort Selected Index Changed Event
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
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

        #region ShowDataGridView
        private void ShowDataGridView(string strWerks, string strLgort)
        {
            try
            {
                objReplenishment = new Replenishment(UserData, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), Progid);
                dtData = objReplenishment.QueryReplenishmentMapping("", "");

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

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Name = "Charg";
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MAXGE";
                dgvcMenge.HeaderText = "Max Qty";
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcRepot = new DataGridViewTextBoxColumn();
                dgvcRepot.DataPropertyName = "REPOT";
                dgvcRepot.HeaderText = "Replenishment Point";
                dgvcRepot.Width = 80;
                dgvcRepot.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRepot);

                DataGridViewTextBoxColumn dgvcCrnam = new DataGridViewTextBoxColumn();
                dgvcCrnam.DataPropertyName = "CRNAM";
                dgvcCrnam.HeaderText = "User";
                dgvcCrnam.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCrnam);

                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "Date";
                dgvcCrdat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCrdat);

                dgvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }
        #endregion

        #region Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            DataTable dtTemp = new DataTable();
            string strInsmk = "";
            try
            {
                // 检查必需输入项不能为空
                if (this.txtLocat.Text.Trim() == "" || this.txtMatnr.Text.Trim() == "" || cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
                {
                    stsWarning.Text = "Plant, storage, Location and Part No can't be empty!!";
                    return;
                }
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (cmbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                }

                // key in的料号系统必需存在
                if (!objPlantData.CheckExistedMatnr(Matnr))
                {
                    stsWarning.Text = Matnr + " doesn't exist!!";
                    this.txtMatnr.Focus();
                    return;
                }
                //key in的储位系统必需存在
                if (!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
                {
                    stsWarning.Text = "The location doesn't exist!!";
                    this.txtLocat.Focus();
                    return;
                }
                // 
                try
                {
                    Int64 intQty = Int64.Parse(this.txtMaxge.Text.Trim());
                    Int64 intQty1 = Int64.Parse(this.txtRepot.Text.Trim());
                }
                catch (Exception ex)
                {
                    stsWarning.Text = "The Max Qty & Replenishment Point should be integer!!";
                    this.txtMaxge.Focus();
                    return;
                }
                
                objReplenishment = new Replenishment(UserData, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), Progid);
                dtData = objReplenishment.QueryReplenishmentMapping(Locat, Matnr);
                if (dtData.Rows.Count > 0)
                {
                    stsWarning.Text = "The replenishment mapping data had existed in the system!!";
                    return;
                }
                if (objReplenishment.AddReplenishmentMapping(Locat, Matnr, strInsmk, this.txtCharge.Text.Trim(), int.Parse(this.txtMaxge.Text.Trim()), int.Parse(this.txtRepot.Text.Trim())))
                {
                    stsWarning.Text = "Add OK!";
                    this.txtLocat.Text = "";
                    this.txtMatnr.Text = "";
                    this.cmbInsmk.SelectedIndex = 0;
                    this.txtMaxge.Text = "0";
                }
                else
                {
                    stsWarning.Text = "Add Fail!" + objAdmin.ERRMSG;
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
            ArrayList arrLocat = new ArrayList();
            ArrayList arrMatnr = new ArrayList();
            ArrayList arrInsmk = new ArrayList();
            ArrayList arrCharg = new ArrayList();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtData.Rows[i]["Select"]))
                {
                    arrLocat.Add(dtData.Rows[i]["LOCAT"]);
                    arrMatnr.Add(dtData.Rows[i]["MATNR"]);
                    arrInsmk.Add(dtData.Rows[i]["INSMK"]);
                    arrCharg.Add(dtData.Rows[i]["CHARG"]);
                }
            }
            if (arrMatnr.Count == 0)
            {
                MessageBox.Show("Please select one data!!");
                return;
            }

            objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);
            if (objReplenishment.DeleteReplenishmentMappingArray(arrLocat, arrMatnr, arrInsmk, arrCharg))
            {
                stsWarning.Text = "Delete OK!";
                ShowDataGridView(Werks, Lgort);
            }
            else
            {
                stsWarning.Text = "Delete Fail!" + objReplenishment.ERRMSG;
            }
        }
        #endregion

        #region  Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 调整布局
        private void Replenishment_MaintainMapping_Resize(object sender, EventArgs e)
        {
            this.panel2.Size = new System.Drawing.Size((int)(this.Size.Width * 0.5), panel2.Size.Height);
            this.panel3.Size = new System.Drawing.Size((int)(panel1.Size.Width * 0.5), panel1.Size.Height);
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion

        #region
        #endregion
        #region
        #endregion
    }
}
