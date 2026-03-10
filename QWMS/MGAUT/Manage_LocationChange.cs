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
    public partial class Manage_LocationChange : Form
    {
        # region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
		private string strUsrnm = "";
		private string strWerks = "";
		private string strLgort = "";
		private string strProgid = "";
		private string strOldLocat = "";
		private string strNewLocat = "";
		private string strSttyp = "";
		private string strLotyp = "";
		private DataTable dtData = new DataTable();
		private PlantData objPlantData;
		private Authority objAuthority;
		private StorageIn objStorageIn;
        private StorageData objStorageData;

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

        public string NewLocat
        {
            get
            {
                return txtNewLocat.Text.Trim();
            }
            set
            {
                txtNewLocat.Text = value;
            }
        }

        public string OldLocat
        {
            get
            {
                return txtOldLocat.Text.Trim();
            }
            set
            {
                txtOldLocat.Text = value;
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
        # endregion

        public Manage_LocationChange(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            try
            {
                objStorageIn = new StorageIn(UserData, Progid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
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

        # region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        # endregion

        # region ShowDdlWerks
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
        # endregion

        # region Plant Selected Change
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        # endregion

        # region ShowDdlLgort
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
        # endregion

        # region Double Click
        private void txtOldLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";

                if (cmbLgort.SelectedIndex != -1)
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    Lgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant, storage and from location can't be empty!!";
                    return;
                }
                else
                {
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "ADD");
                    objManage_LocationSelect.ShowDialog();
                    txtOldLocat.Text = objManage_LocationSelect.Locat;
                    strOldLocat = txtOldLocat.Text;
                    if (strOldLocat.Trim() != "")
                        ShowSourceData();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        
        private void txtNewLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";

                if (cmbLgort.SelectedIndex != -1)
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    Lgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "NEW");
                    objManage_LocationSelect.ShowDialog();
                    txtNewLocat.Text = objManage_LocationSelect.Locat;
                    strNewLocat = txtNewLocat.Text;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvData.Columns.Add(dgvcMatnr);

                //客人料號欄位
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvData.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serial No.";
                dgvcSerno.ReadOnly = true;
                dgvData.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "OMBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvceEbeln = new DataGridViewTextBoxColumn();
                dgvceEbeln.DataPropertyName = "EBELN";
                dgvceEbeln.HeaderText = "PO No";
                dgvceEbeln.ReadOnly = true;
                dgvceEbeln.Width = 100;
                dgvData.Columns.Add(dgvceEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                dgvcMrgid.DataPropertyName = "MRGID";
                dgvcMrgid.HeaderText = "Mixed Material ID";
                dgvcMrgid.ReadOnly = true;
                dgvcMrgid.Width = 120;
                dgvData.Columns.Add(dgvcMrgid);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvData.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvData.Columns.Add(dgvcIndat);              

                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";

                if (dtData.Rows.Count > 0)
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        # endregion

        # region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                stsWarning.Text = "";

                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";
                //取得Sttyp及Lotyp
                dtTemp = objPlantData.GetPlantStorageData("LGORT", Werks, Lgort);
                if (dtTemp.Rows.Count >= 1)
                {
                    strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                    strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                }
                else
                {
                    stsWarning.Text = "Can't find the storage data!!";
                    return;
                }
                //固定儲位不能使用此功能
                if (strLotyp.ToUpper() == "FIXED LOCATION")
                {
                    stsWarning.Text = "You can't use the function because " + Lgort + " is a fixed-location storage!!";
                    return;
                }

                if (txtOldLocat.Text.Trim() == "")
                {
                    stsWarning.Text = "The source location can't be empty!!";
                    this.txtOldLocat.Focus();
                    return;
                }
                if (txtNewLocat.Text.Trim() == "")
                {
                    stsWarning.Text = "The destination location can't be empty!!";
                    this.txtNewLocat.Focus();
                    return;
                }
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "The storage of source location is empty!!";
                    this.txtOldLocat.Focus();
                    return;
                }
                if (!objPlantData.CheckExistedStorageData(Werks, Lgort, NewLocat))
                {
                    stsWarning.Text = "The destination location doesn't exist!!";
                    this.txtNewLocat.Focus();
                    return;
                }
                if (objPlantData.CheckStorageData(Werks, Lgort, NewLocat))
                {
                    stsWarning.Text = "The destination location is not a empty location!!";
                    this.txtNewLocat.Focus();
                    return;
                }
                objStorageIn = new StorageIn(UserData, Werks, Lgort, "", Progid);
                if (objStorageIn.AddLocationChangeData(OldLocat, NewLocat, dtData))
                {
                    stsWarning.Text = "Update OK!";
                    this.btnSave.Enabled = false;
                }
                else
                {
                    stsWarning.Text = "Update fail!" + objStorageIn.ERRMSG;
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            this.strWerks = "";
            this.txtOldLocat.Text = "";
            this.txtNewLocat.Text = "";
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.strLgort = "";
            this.txtOldLocat.Enabled = true;
            this.txtNewLocat.Enabled = false;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.btnSave.Enabled = false;
            this.lblCount.Text = "";
        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        # region ShowSourceData
        private void ShowSourceData()
        {
            try
            {
                objStorageData = new StorageData(UserData, Werks, Lgort);
                dtData = objStorageData.QueryStorageDetailData(OldLocat, "");
                if (dtData.Rows.Count > 0)
                {
                    ShowDataGrid();
                    this.cmbWerks.Enabled = false;
                    this.cmbLgort.Enabled = false;
                    this.txtOldLocat.Enabled = false;
                    this.txtNewLocat.Enabled = true;
                    this.btnSave.Enabled = true;
                }
                else
                {
                    stsWarning.Text = "No storage data!!";
                    this.txtOldLocat.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Resize
        private void Manage_LocationChange_Resize(object sender, EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.5), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40;
        }
        # endregion

        # region Enter Press
        private void txtOldLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    stsWarning.Text = "";
                    if (cmbWerks.SelectedIndex != -1)
                        Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    else
                        Werks = "";

                    if (cmbLgort.SelectedIndex != -1)
                        Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    else
                        Lgort = "";

                    if (Werks == "" || Lgort == "" || this.txtOldLocat.Text.Trim() == "")
                    {
                        stsWarning.Text = "Plant, storage and from location can't be empty!!";
                        return;
                    }
                    ShowSourceData();
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        # endregion



    }
}
