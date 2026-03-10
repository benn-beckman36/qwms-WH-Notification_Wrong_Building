using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Admin_FixMaterialMap : Form
    {

        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private DataTable dtLgort = new DataTable();
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;
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
        public Admin_FixMaterialMap()
        {
            InitializeComponent();
        }

        public Admin_FixMaterialMap(UserInfo varUserData, string varProgid)
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
                    // Show 数据
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

        #region 绑定厂区
        private void ShowDdlWerks()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region 绑定仓别
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                dtLgort = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtLgort = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtLgort = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort.Items.Clear();
                }

                if (dtLgort.Rows.Count == 0)
                {
                    cmbLgort.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtLgort.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtLgort.Rows[i]["F_TEXT"].ToString());
                        if (dtLgort.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
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
            try
            {
                stsWarning.Text = "";
                ShowDdlLgort();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                dtData = objPlantData.QueryMappingData(Werks, Lgort, Locat, Matnr);
                ShowDataGridView();
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
                stsWarning.Text = "";
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                dtData = objPlantData.QueryMappingData(Werks, Lgort, Locat, Matnr);
                ShowDataGridView();
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

                DataGridViewTextBoxColumn dgvcPlant = new DataGridViewTextBoxColumn();
                dgvcPlant.DataPropertyName = "WERKS";
                dgvcPlant.HeaderText = "Plant";
                dgvcPlant.ReadOnly = true;
                this.gvData.Columns.Add(dgvcPlant);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLocat);

                this.gvData.DataSource = dtData;
   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }
        #endregion

        #region rdo Query Checked Changed Event
        private void rdoQuery_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.gbHeader.Enabled = true;
            this.btnConfirm.Enabled = true;
        }
        #endregion

        #region rdo Delete Checked Changed Event
        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.gbHeader.Enabled = true;
            this.btnSave.Enabled = true;
        }
        #endregion

        #region rdo Add Checked Changed Event
        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.gbFunction.Enabled = false;
            this.gbHeader.Enabled = true;
            this.btnSave.Enabled = true;
        }
        #endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                dtData = objPlantData.QueryMappingData(Werks, Lgort, Locat, Matnr);
                ShowDataGridView();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Download
        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    CountingResult2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region CountingResult2File
        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Plant\tStorage\tPart No\tLocation";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LOCAT"].ToString();
                    sw.WriteLine(strLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }

        }

        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.rdoAdd.Checked = false;
            this.rdoDelete.Checked = false;
            this.rdoQuery.Checked = false;
            this.gbFunction.Enabled = true;
            this.btnSave.Enabled = false;
            this.gbHeader.Enabled = false;
            this.btnConfirm.Enabled = false;
            this.gvData.DataSource = null;
            this.stsWarning.Text = "";
            this.txtLocat.Text = "";
            this.txtMatnr.Text = "";
            if (cmbWerks.Items.Count > 0)
            {
                this.cmbWerks.SelectedIndex = 0;
            }
            if (cmbLgort.Items.Count > 0)
            {
                this.cmbLgort.SelectedIndex = 0;
            }
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No data to print!!";
                    return;
                }
                ReportPrint objReportPrint = new ReportPrint(UserData, "FIXEDLOCATION", dtData);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                 
                DataRow[] foundRow;
                foundRow = dtLgort.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "'");
                if (foundRow.Length == 0)
                {
                    stsWarning.Text = "Can't find storage data!!";
                    return;
                }
                if (foundRow[0]["CTRLC5"].ToString().ToUpper() != "FIXED LOCATION")
                {
                    stsWarning.Text = Lgort + " is not fixed location!!";
                    return;
                }

                //Delete
                if (rdoDelete.Checked)
                {
                    // 检查数据，不能为空
                    if (Locat == "" || Matnr == "" || cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Plant, storage, location and Part No can't be empty!!";
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
                    
                    dtData = objPlantData.QueryMappingData(Werks, Lgort, Locat, Matnr);
                    if (dtData.Rows.Count == 0)
                    {
                        stsWarning.Text = "The mapping data doesn't exist in the system!!";
                        return;
                    }
                    objStorageData = new StorageData(UserData, Werks, Lgort);

                    dtTemp = objStorageData.QueryStorageCheckDetailData("", Locat, "", Matnr, "", "", "", "", "", "");
                    if (dtTemp.Rows.Count > 0)
                    {
                        stsWarning.Text = "The location had the stock of this Part No. now!!";
                        return;
                    }
                    //删除
                    if (objAdmin.DeleteFixedMatnr(Werks, Lgort, Locat, Matnr))
                    {
                        stsWarning.Text = "Delete OK!";
                        this.txtLocat.Text = "";
                        this.txtMatnr.Text = "";
                    }
                    else
                    {
                        stsWarning.Text = "Delete Fail!" + objAdmin.ERRMSG;
                    }
                }

                //Add
                if (rdoAdd.Checked)
                {
                    // 检查数据，不能为空
                    if (Locat == "" || Matnr == "" || cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Plant, storage, location and Part No can't be empty!!";
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
                    
                    if (!objPlantData.CheckExistedMatnr(Matnr))
                    {
                        stsWarning.Text = Matnr + " doesn't exist!!";
                        this.txtMatnr.Focus();
                        return;
                    }
                    // 
                    if (!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
                    {
                        stsWarning.Text = "The location doesn't exist!!";
                        this.txtLocat.Focus();
                        return;
                    }
                    // 
                    dtData = objPlantData.QueryMappingData(Werks, Lgort, Locat, Matnr);
                    if (dtData.Rows.Count > 0)
                    {
                        stsWarning.Text = "The mapping data had existed in the system!!";
                        return;
                    }
                    //新增固定储位料号
                    if (objAdmin.AddFixedMatnr(Werks, Lgort, Locat, Matnr))
                    {
                        stsWarning.Text = "Add OK!";
                        this.txtLocat.Text = "";
                        this.txtMatnr.Text = "";
                    }
                    else
                    {
                        stsWarning.Text = "Add Fail!" + objAdmin.ERRMSG;
                    }
                }
                dtData = objPlantData.QueryMappingData(Werks, Lgort, Locat, Matnr);
                ShowDataGridView();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 调整布局大小
        private void Admin_FixMaterialMap_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion
        
    }
}
