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
using System.IO;
using System.Collections;
using QCI_QWMS_Models;

namespace QWMS.Models
{
    public partial class Model_InventoryCheck_StorageQuery : Form
    {
         #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strRegon = "";
        private string strLgort = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private DataTable dtLocat = new DataTable();
        private DataTable dtInventory = new DataTable();
        private Counting objCounting;
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

        # endregion

        public Model_InventoryCheck_StorageQuery()
        {
            InitializeComponent();
        }

        public Model_InventoryCheck_StorageQuery(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objCounting = new Counting(UserData, Progid);
                StorageIn objStorageIn = new StorageIn(UserData, Progid);
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
                    ShowDdlInsmk();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    btnLabelPrint.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region     Function
       
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

        # region ShowDdlInsmk
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
        # endregion

        # region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();

        }
        # endregion

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

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 90;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Model No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 90;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcAsset = new DataGridViewTextBoxColumn();
                dgvcAsset.DataPropertyName = "ASSETSNO";
                dgvcAsset.HeaderText = "Asset No";
                dgvcAsset.ReadOnly = true;
                dgvData.Columns.Add(dgvcAsset);

                DataGridViewTextBoxColumn dgvcBU = new DataGridViewTextBoxColumn();
                dgvcBU.DataPropertyName = "BU";
                dgvcBU.HeaderText = "PU";
                dgvcBU.ReadOnly = true;
                dgvData.Columns.Add(dgvcBU);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvData.Columns.Add(dgvcIndat);

                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        # endregion

        #endregion

        #region     Event

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string strStartDate = "";
            string strEndDate = "";
            string strInsmk = "";
            string strIsCombine = "N";
            stsWarning.Text = "";

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
                if (cmbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
                }
                if (chkIsCombine.Checked)
                {
                    strIsCombine = "Y";
                }
                if (chkDate.Checked)
                {
                    strStartDate = dtpStartDate.Value.ToString("yyyyMMdd");
                    strEndDate = dtpEndDate.Value.ToString("yyyyMMdd");
                }
                else
                {
                    strStartDate = "";
                    strEndDate = "";
                }

                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                dtData = objModelsData.QueryDetailCountingData(strInsmk, txtStartLocat.Text.Trim(), txtEndLocat.Text.Trim(), txtStartMatnr.Text.Trim(), txtEndMatnr.Text.Trim(), strStartDate, strEndDate, txtMblnr.Text.Trim(), strIsCombine, strRegon, txtVendorCode.Text.ToString().Trim());
                if (dtData.Rows.Count == 0)
                {
                    ShowDataGrid();
                    stsWarning.Text = "No Data!!";
                    return;
                }
                else
                {

                    #region 抽盤功能

                    double count_Total =0;
                    double count_Sample = 0;
                    //double rate = (double)N2.Value/100;//抽盤比例

                    DataTable dtData_temp = dtData.Clone();
                    dtData_temp.Rows.Clear();

                    DataTable dtData_temp2 = new DataTable();
                    ArrayList a2 = new ArrayList();

                    #endregion

                    btnPrint.Enabled = true;
                    ShowDataGrid();
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
            try
            {
               
                this.cmbInsmk.SelectedIndex = -1;
                this.txtStartLocat.Text = "";
                this.txtEndLocat.Text = "";
                this.txtStartMatnr.Text = "";
                this.txtEndMatnr.Text = "";
                this.txtMblnr.Text = "";
                this.txtVendorCode.Text = "";
                this.dtpStartDate.Value = DateTime.Now;
                this.dtpEndDate.Value = DateTime.Now;
                this.btnPrint.Enabled = false;
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
                this.lblCount.Text = "";
                this.chkIsCombine.Checked = false;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        # region Download
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
        # endregion

        # region Export
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                if (this.chkIsCombine.Checked)
                {
                    strLine = "Plant\tStorage\tLocation\tModel No\tMode Name\tCUST Mat\tStock\tVersion\tQty\tDocument No\tStore In Date\tRMA No.";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["ITEMNAME"].ToString() + "\t";
                        strLine += dtData.Rows[i]["KDMAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += dtData.Rows[i]["CHARG"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MBLNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INDAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["RMANO"].ToString() + "\t";
                        sw.WriteLine(strLine);
                    }
                }
                else
                {
                    strLine = "Plant\tStorage\tLocation\tModel No\tPU\tAssets No\tQty\tStore In Date";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += dtData.Rows[i]["BU"].ToString() + "\t";
                        strLine += dtData.Rows[i]["ASSETSNO"].ToString() + "\t";
                        strLine += dtData.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += dtData.Rows[i]["INDAT"].ToString();
                        sw.WriteLine(strLine);
                    }
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
        # endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string strLocat = "";
                DataTable dtTmpPrint = new DataTable();
                stsWarning.Text = "";
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No data to print!!";
                    return;
                }

                //Order by
                string strOrderBy = "WERKS, LGORT, LOCAT";
                dtData = CommonInfo.SortDataTable(dtData, strOrderBy); 
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "MODELLOCAT", dtData);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        #region btnLabelPrint_Click
        private void btnLabelPrint_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Label to print!!";
                    return;
                }
                ReportPrint objReportPrint = new ReportPrint(UserData, "COUNTINGLABELLOCAT", dtData);
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

        #region 抽盤

        //隨機產生不重複編號
        private Array RandomNO(int count_Sample, int count_Total)
        {
            //參考:http://www.dotblogs.com.tw/ouch1978/archive/2011/10/14/sl-random-with-linq.aspx
            var result = Enumerable.Range(1, count_Total).OrderBy(n => n * n * (new Random()).Next()).Take(count_Sample);
            return result.ToArray();
        }

        protected void RecoverNum(object s, EventArgs e)  
        {  
           var n = (NumericUpDown)s;  
           if (n.Text == "") n.Text = n.Value.ToString();
        }  

        #endregion

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string Location = dgvData.CurrentRow.Cells[2].Value.ToString().Trim();
            string Mandt = "218";
            string Werks = dgvData.CurrentRow.Cells[0].Value.ToString().Trim();
            string Lgort = dgvData.CurrentRow.Cells[1].Value.ToString().Trim();
            string Boxid = dgvData.CurrentRow.Cells[11].Value.ToString().Trim();
            string SN = "";// dgvData.CurrentRow.Cells[10].Value.ToString().Trim();
            string Qty = dgvData.CurrentRow.Cells[10].Value.ToString().Trim();
            string Insmk = dgvData.CurrentRow.Cells[7].Value.ToString().Trim();
            string Kdmat = dgvData.CurrentRow.Cells[6].Value.ToString().Trim();
            string Matno = dgvData.CurrentRow.Cells[3].Value.ToString().Trim();
            string Charg = dgvData.CurrentRow.Cells[8].Value.ToString().Trim();
        }

        #endregion
    }
}
