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
    public partial class Admin_DateCodeExtendStockTime : Form
    {

        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strInsmk = "";
        private string strMatnr = "";
        private string strCharg = "";
        private string strMblnr = "";
        private string strIsmrg = "";
        private string strComcd = "";
        private string strDacod = "";
        private DataTable dtData = new DataTable();
        private DataTable dtStoreOut = new DataTable();
        UserInfo UserData = new UserInfo();
        private Admin objAdmin;
        private int Aretdat = 0;
        private int Totaldat = 0;

        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
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

        public string Dacod
        {
            get
            {
                return txtDacod.Text.Trim();
            }
            set
            {
                txtDacod.Text = value;
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

        public Admin_DateCodeExtendStockTime(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            strComcd = varUserData.CompanyCode;
            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlInsmk();
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
                cmbWerks.Items.Clear();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
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

        private void ShowDdlInsmk()
        {
            DataTable dtTemp = new DataTable();
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (cmbWerks.SelectedIndex != -1)
                {
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    Werks = "";
                }

                if (cmbLgort.SelectedIndex != -1)
                {
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    Lgort = "";
                }

                if (cmbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                }
                else
                {
                    strInsmk = "";
                }

                if (Werks == "" )
                {
                    stsWarning.Text = "Plant can't be empty!!";
                    return;
                }

                if (this.txtMatnr.Text.Trim() == "")
                {
                    stsWarning.Text = "Part No. can't be empty!!";
                    return;
                }
                if (this.txtDacod.Text.Trim() == "")
                {
                    stsWarning.Text = "Date Code can't be empty!!";
                    return;
                }
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                dtData = objStorageData.QueryH95DateCodeDateExtend(Werks, Lgort, Matnr, strInsmk, Dacod);
                dtData.Columns.Add("TOTAL");
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                }
                if (dtData.Rows[0]["ETDAT"].ToString() =="")
                {
                    stsWarning.Text = "该料号未设置过延长期限!!";
                }
                if (dtData.Rows[0]["ETDAT"].ToString() != "")
                {
                    //总延长期限
                    Totaldat = Convert.ToInt32(dtData.Rows[0]["ETDAT"].ToString()) +
                              Convert.ToInt32(dtData.Rows[0]["ARETDAT"].ToString());

                    dtData.Rows[0]["TOTAL"] = Totaldat.ToString();
                }
                
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 90;
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 90;
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 90;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "Date Code";
                dgvcDacod.Width = 90;
                dgvcDacod.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcDacodTime = new DataGridViewTextBoxColumn();
                dgvcDacodTime.DataPropertyName = "StockTime";
                dgvcDacodTime.HeaderText = "库存期限";
                dgvcDacodTime.Width = 90;
                dgvcDacodTime.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDacodTime);

                DataGridViewTextBoxColumn dgvcEtdat = new DataGridViewTextBoxColumn();
                dgvcEtdat.DataPropertyName = "ETDAT";
                dgvcEtdat.HeaderText = "延长期限";
                dgvcEtdat.Width = 90;
                dgvcEtdat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcEtdat);

                DataGridViewTextBoxColumn dgvcAretdat = new DataGridViewTextBoxColumn();
                dgvcAretdat.DataPropertyName = "ARETDAT";
                dgvcAretdat.HeaderText = "已延长期限";
                dgvcAretdat.Width = 90;
                dgvcAretdat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAretdat);

                DataGridViewTextBoxColumn dgvcTotaldat = new DataGridViewTextBoxColumn();
                dgvcTotaldat.DataPropertyName = "TOTAL";
                dgvcTotaldat.HeaderText = "总延长期限";
                dgvcTotaldat.Width = 90;
                dgvcTotaldat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcTotaldat);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";

                if (dtData.Rows.Count > 0)
                {
                    this.txtExtend.Enabled = true;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            DataTable dtTemp = new DataTable();
            try
            {

                //檢查是不是空值
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                if (this.txtMatnr.Text.Trim() == "")
                {
                    stsWarning.Text = "Part No. can't be empty!!";
                    return;
                }
                if (this.txtDacod.Text.Trim() == "")
                {
                    stsWarning.Text = "Date Code不能为空!!";
                    return;
                }
                //檢查料號是否已存在
                if (!objAdmin.CheckExistedPart(txtMatnr.Text.Trim()))
                {
                    stsWarning.Text = "Part No. is not existed!!";
                    return;
                }

                if (cmbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                }
                else
                {
                    strInsmk = "";
                }
                if (dtData.Rows[0]["ETDAT"].ToString() != "")
                {
                    Aretdat = Convert.ToInt32(dtData.Rows[0]["ETDAT"].ToString()) +
                              Convert.ToInt32(dtData.Rows[0]["ARETDAT"].ToString());
                }
                //新增
                if (objAdmin.ExtendDateCodeStockTime( Werks,Lgort,txtMatnr.Text.Trim(),strInsmk,txtDacod.Text.Trim(),txtExtend.Text.Trim(),Aretdat.ToString(),dtData))
                {
                    if (dtData.Rows[0]["MATNR"].ToString() == "")
                    {
                        stsWarning.Text = "Add OK!";
                        this.txtMatnr.Enabled = false;
                        this.txtDacod.Enabled = false;
                        this.txtExtend.Enabled = false;
                        this.btnConfirm.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    if (dtData.Rows[0]["MATNR"].ToString() != "")
                    {
                        stsWarning.Text = "update OK!";
                        this.txtMatnr.Enabled = false;
                        this.txtDacod.Enabled = false;
                        this.txtExtend.Enabled = false;
                        this.btnConfirm.Enabled = false;
                        btnSave.Enabled = false;
                    }
                    btnConfirm_Click(null,null);
                }
                else
                {
                    stsWarning.Text = "Save Fail!" + objAdmin.ERRMSG;
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void brnRefresh_Click(object sender, EventArgs e)
        {
            this.txtMatnr.Text = "";
            this.strWerks = "";
            this.strLgort = "";
            this.strInsmk = "";
            this.dtData.Clear();
            this.lblData.Text = "0 records";
            this.dgvData.DataSource = null;
            this.stsWarning.Text = "";
            this.txtDacod.Text = "";
            this.txtExtend.Text = "";
            this.btnSave.Enabled = false;
            this.txtExtend.Enabled = false;
            this.panel1.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.txtMatnr.Enabled = true;
            this.txtDacod.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
