using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Manage_SimulationOutQuery : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strRegon = "";
        private string strLgort = "";
        private string strGrpid = "";
        private string strKitting = "";
        private DataTable dtData = new DataTable();
        private DataTable dtIdData = new DataTable();
        private DataTable dtInventory = new DataTable();
        private StorageIn objStorageIn;
        private PlantData objPlantData;
        private Authority objAuthority;
        string strStartDate = "";
        string strEndDate = "";

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

        public string Grpid
        {
            get
            {
                return strGrpid;
            }
            set
            {
                strGrpid = value;
            }
        }

        public string Kitting
        {
            get
            {
                return strKitting;
            }
            set
            {
                strKitting = value;
            }
        }

        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
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


        public Manage_SimulationOutQuery(UserInfo varUserData, string strProgid)
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
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
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
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
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

        # region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();

        }
        # endregion

        # region Storage SelectedChange
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowGroupId();

        }
        # endregion

        #region ShowGroupId
        private void ShowGroupId()
        {
            try
            {
                cmbGrpid.Items.Clear();
                strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
                strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");

                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                dtIdData = objStorageData.QuerySimulationOutConfirmSendIdData(strWerks, strLgort, strStartDate, strEndDate);
                if (dtIdData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtIdData.Rows.Count; i++)
                    {
                        cmbGrpid.Items.Add(dtIdData.Rows[i]["GRPID"]);
                    }
                    if (!rdbAllLgort.Checked && !rdbAllGrpid.Checked)
                    {
                        this.cmbGrpid.Enabled = true;
                    }
                }
                else
                {
                    stsWarning.Text = "No id data!!";
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

        # region Send ID SelectedChange
        private void cmbGrpid_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowKittingUser();

        }
        # endregion

        #region ShowKittingUser
        private void ShowKittingUser()
        {
            try
            {
                cmbKitting.Items.Clear();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (cmbGrpid.SelectedIndex != -1)
                {
                    strGrpid = cmbGrpid.Items[cmbGrpid.SelectedIndex].ToString();
                }
                StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                dtIdData = objStorageData.QuerySimulationOutConfirmKittingUserData(strWerks, strLgort, strGrpid);
                if (dtIdData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtIdData.Rows.Count; i++)
                    {
                        cmbKitting.Items.Add(dtIdData.Rows[i]["USRNM"]);
                    }
                    if (!rdbAllLgort.Checked && !rdbAllGrpid.Checked)
                    {
                        this.cmbKitting.Enabled = true;
                    }
                }
                else
                {
                    stsWarning.Text = "No id data!!";
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

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 50;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 50;
                dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                dgvcCrdat.DataPropertyName = "CRDAT";
                dgvcCrdat.HeaderText = "Date";
                dgvcCrdat.ReadOnly = true;
                dgvcCrdat.Width = 90;
                dgvData.Columns.Add(dgvcCrdat);

                DataGridViewTextBoxColumn dgvcClass = new DataGridViewTextBoxColumn();
                dgvcClass.DataPropertyName = "CLASS";
                dgvcClass.HeaderText = "Class";
                dgvcClass.ReadOnly = true;
                dgvcClass.Width = 60;
                dgvData.Columns.Add(dgvcClass);

                DataGridViewTextBoxColumn dgvcGrpid = new DataGridViewTextBoxColumn();
                dgvcGrpid.DataPropertyName = "GRPID";
                dgvcGrpid.HeaderText = "Send ID";
                dgvcGrpid.ReadOnly = true;
                dgvcGrpid.Width = 180;
                dgvData.Columns.Add(dgvcGrpid);

                //料號說明欄位
                DataGridViewTextBoxColumn dgvcUsrnm = new DataGridViewTextBoxColumn();
                dgvcUsrnm.DataPropertyName = "USRNM";
                dgvcUsrnm.HeaderText = "Kitting User";
                dgvcUsrnm.Width = 90;
                dgvcUsrnm.ReadOnly = true;
                dgvData.Columns.Add(dgvcUsrnm);

                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "ITEM";
                dgvcItem.HeaderText = "Location Item";
                dgvcItem.ReadOnly = true;
                dgvcItem.Width = 60;
                dgvData.Columns.Add(dgvcItem);

                //客人料號欄位
                DataGridViewTextBoxColumn dgvcFitem = new DataGridViewTextBoxColumn();
                dgvcFitem.DataPropertyName = "FITEM";
                dgvcFitem.HeaderText = "Kitting Finished Item";
                dgvcFitem.ReadOnly = true;
                dgvcFitem.Width = 60;
                dgvData.Columns.Add(dgvcFitem);

                DataGridViewTextBoxColumn dgvcPitem = new DataGridViewTextBoxColumn();
                dgvcPitem.DataPropertyName = "PITEM";
                dgvcPitem.HeaderText = "Kitting Precessing Item";
                dgvcPitem.ReadOnly = true;
                dgvcPitem.Width = 60;
                dgvData.Columns.Add(dgvcPitem);

                dgvData.DataSource = dtData;

                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtData.Rows[i]["ITEM"].ToString()) != 0)
                    {
                        if (Convert.ToInt32(dtData.Rows[i]["PITEM"].ToString()) == 0)
                        {
                            dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen;
                        }
                    }
                    else
                    {
                        dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }

                lblCount.Text = dtData.Rows.Count + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        # endregion

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                if (cmbGrpid.SelectedIndex != -1)
                {
                    strGrpid = cmbGrpid.Items[cmbGrpid.SelectedIndex].ToString();
                }
                if (cmbKitting.SelectedIndex != -1)
                {
                    strKitting = cmbKitting.Items[cmbKitting.SelectedIndex].ToString();
                }

                strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
                strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");

                if (strWerks == "")
                {
                    stsWarning.Text = "Plant can't be empty!!";
                    return;
                }
                if (strLgort == ""&&!rdbAllLgort.Checked)
                {
                    stsWarning.Text = "Storage can't be empty!!";
                    return;
                }
                if (strGrpid == "" && !rdbAllGrpid.Checked &&!rdbAllLgort.Checked)
                {
                    stsWarning.Text = "Send ID can't be empty!!";
                    return;
                }
                if (strKitting == "" && !rdbAllGrpid.Checked && !rdbAllLgort.Checked)
                {
                    stsWarning.Text = "Kitting User can't be empty!!";
                    return;
                }

                if (rdbAllLgort.Checked)
                {
                    strLgort = "";
                    strGrpid = "";
                    strKitting = "";
                    cmbLgort.SelectedIndex = -1;
                }
                if (rdbAllGrpid.Checked)
                {
                    strGrpid = "";
                    strKitting = "";
                    cmbGrpid.SelectedIndex = -1;
                    cmbKitting.SelectedIndex = -1;
                }

                StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                dtData = objStorageData.QuerySimulationOutConfirmData(strWerks, strLgort, strGrpid, strKitting, strStartDate, strEndDate);
                if (dtData.Rows.Count == 0)
                {
                    ShowDataGrid();
                    stsWarning.Text = "No Data!!";
                    return;
                }
                else
                {
                    ShowDataGrid();
                    stsWarning.Text = "";
                }
                
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void rdbAllLgort_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAllLgort.Checked)
            {
                this.cmbLgort.Enabled = false;
                this.cmbGrpid.Enabled = false;
                this.cmbKitting.Enabled = false;
            }
            else
            {
                this.cmbLgort.Enabled = true;
                this.cmbGrpid.Enabled = true;
                this.cmbKitting.Enabled = true;
            }
        }

        private void rdbAllGrpid_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAllGrpid.Checked)
            {
                this.cmbGrpid.Enabled = false;
                this.cmbKitting.Enabled = false;
            }
            else
            {
                this.cmbGrpid.Enabled = true;
                this.cmbKitting.Enabled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cmbGrpid.SelectedIndex = -1;
            this.cmbKitting.SelectedIndex = -1;
            strGrpid = "";
            strKitting = "";
            this.dtpStartDate.Value = DateTime.Now;
            this.dtpEndDate.Value = DateTime.Now;
            this.dgvData.DataSource = null;
            this.dtData.Rows.Clear();
            this.rdbAllLgort.Checked = false;
            this.rdbAllGrpid.Checked = false;
            this.lblCount.Text = "";
            stsWarning.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                stsWarning.Text = "起始时间不能大于结束时间";
            }
            else
            {
                ShowGroupId();
                stsWarning.Text = "";
            }
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                stsWarning.Text = "结束时间不能小于起始时间";
            }
            else
            {
                ShowGroupId();
                stsWarning.Text = "";
            }
            
        }
    }
}
