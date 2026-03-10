using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Admin_LocationBlock : Form
    {

        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strStatus = "";
        private DataTable dtData = new DataTable();
        private DataTable dtSave = new DataTable();
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;

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

        public string Status
        {
            get
            {
                return strStatus;
            }
            set
            {
                strStatus = value;
            }
        }
        #endregion

        #region 构造函数
        public Admin_LocationBlock()
        {
            InitializeComponent();
        }

        public Admin_LocationBlock(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;
            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                // 检查权限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
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
        # endregion

        #region 绑定菜单
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
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
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

        #region cmbWerks Selected Index Changed Event
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = "";
                }

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                strStatus = Convert.ToString(cmbStatus.SelectedIndex);
                dtData = objPlantData.QueryLocationStatus(strWerks, strLgort, strStatus);
                DataColumn cSelect = new DataColumn("Select", typeof(bool));
                dtData.Columns.Add(cSelect);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["Select"] = false;
                }
                ShowDataGridView();
                btnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region ShowDataGridView
        private void ShowDataGridView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "Select";
                this.gvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                this.gvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcLosts = new DataGridViewTextBoxColumn();
                dgvcLosts.DataPropertyName = "LOSTS";
                dgvcLosts.HeaderText = "Location Status";
                dgvcLosts.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLosts);

                DataGridViewTextBoxColumn dgvcBlock = new DataGridViewTextBoxColumn();
                dgvcBlock.DataPropertyName = "BLOCK";
                dgvcBlock.HeaderText = "Block status";
                dgvcBlock.ReadOnly = true;
                this.gvData.Columns.Add(dgvcBlock);

                this.gvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow drRow;
            try
            {
                //Status ='Un-Block'
                if (int.Parse(strStatus) == 0)
                {
                    dtSave = dtData.Clone();
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        DataRow[] drTmp = this.dtData.Select("LOCAT='" + dtData.Rows[i]["LOCAT"].ToString().Trim() + "'");
                        if ((bool)dtData.Rows[i]["Select"] == true)
                        {
                            drRow = dtSave.NewRow();
                            drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                            drRow["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString();
                            drRow["LOSTS"] = dtData.Rows[i]["LOSTS"].ToString();
                            drRow["BLOCK"] = dtData.Rows[i]["BLOCK"].ToString();
                            dtSave.Rows.Add(drRow);
                        }
                    }
                }
                //Status='Block'
                else if (int.Parse(strStatus) == 1)
                {
                    dtSave = dtData.Clone();
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        DataRow[] drTmp = this.dtData.Select("LOCAT='" + dtData.Rows[i]["LOCAT"].ToString().Trim() + "'");
                        if ((bool)dtData.Rows[i]["Select"] == true)
                        {
                            drRow = dtSave.NewRow();
                            drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                            drRow["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString();
                            drRow["LOSTS"] = dtData.Rows[i]["LOSTS"].ToString();
                            drRow["BLOCK"] = dtData.Rows[i]["BLOCK"].ToString();
                            dtSave.Rows.Add(drRow);
                        }
                    }
                }

                // 更改Block状态
                if (objAdmin.UpdateLocationBlock(strWerks, strLgort, dtSave, strStatus))
                {
                    stsWarning.Text = "Add OK!";
                    this.btnSave.Enabled = false;
                }
                else
                {
                    stsWarning.Text = "Add Fail!" + objAdmin.ERRMSG;
                    return;
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnSave.Enabled = false;
                this.stsWarning.Text = "";
                this.gvData.DataSource = null;
                this.dtData.Clear();
                cmbWerks.SelectedIndex = -1;
                cmbLgort.SelectedIndex = -1;
                cmbStatus.SelectedIndex = -1;
                cmbWerks.Focus();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 调整布局大小
        private void Admin_LocationBlock_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion
        

    }
}
