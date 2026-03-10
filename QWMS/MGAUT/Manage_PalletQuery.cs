using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class Manage_PalletQuery : Form
    {
        #region 变量
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strProgid = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strFromDate = string.Empty;
        private string strToDate = string.Empty;
        private string strMachine = string.Empty;
        private DataTable dtPallet = new DataTable();
        private DataTable dtStock = new DataTable();
        private DataTable dtDetail = new DataTable();
        private StorageData objStorageData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private LogData objLogData;

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
        public Manage_PalletQuery(UserInfo _UserData, string strProgid)
        {
            InitializeComponent();
            UserData = _UserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;

            Progid = strProgid;

            try
            {
                UserData = _UserData;
                objStorageData = new StorageData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(UserData, Progid);

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
                    ShowStockData();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = -1;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region ShowStatusData/ShowDdlWerks/ShowDdlLgort
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }

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

        private void ShowStockData()
        {
            if(dtStock.Rows.Count==0)
            {
                //WERKS,LGORT,LOCAT,MBLNR,MATNR,CHARG,INSMK,LIFNR,EBELN,MENGE,RMAK1
                dtStock.Columns.Add("WERKS");
                dtStock.Columns.Add("LGORT");
                dtStock.Columns.Add("LOCAT");
                dtStock.Columns.Add("MBLNR");
                dtStock.Columns.Add("MATNR");
                dtStock.Columns.Add("CHARG");
                dtStock.Columns.Add("INSMK");
                dtStock.Columns.Add("LIFNR");
                dtStock.Columns.Add("EBELN");
                dtStock.Columns.Add("MENGE", typeof(decimal));
                dtStock.Columns.Add("RMAK1");
            }
        }
        #endregion

        #region ShowDataGridView
        public void ShowPalletGrid()
        {
            this.dgvPallet.AutoGenerateColumns = false;
            this.dgvPallet.Columns.Clear();

            DataGridViewTextBoxColumn dgvcPallet = new DataGridViewTextBoxColumn();
            dgvcPallet.DataPropertyName = "PALLET";
            dgvcPallet.HeaderText = "PALLET";
            dgvcPallet.Width = 250;
            dgvcPallet.ReadOnly = true;
            this.dgvPallet.Columns.Add(dgvcPallet);

            this.dgvPallet.DataSource = dtPallet;
            lblPal.Text = dtPallet.Rows.Count.ToString() + "records";
            dgvPallet.ClearSelection();
            dgvPallet.AllowUserToAddRows = false;
        }
        public void ShowStockGrid()
        {
            try
            {
                this.dgvStockData.AutoGenerateColumns = false;
                this.dgvStockData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "厂区";
                dgvcWerks.Name = "WERKS";
                dgvcWerks.Width = 90;
                dgvcWerks.ReadOnly = true;
                this.dgvStockData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "仓别";
                dgvcLgort.Name = "LGORT";
                dgvcLgort.Width = 90;
                dgvcLgort.ReadOnly = true;
                this.dgvStockData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.Name = "LOCAT";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvStockData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet";
                dgvcMblnr.Name = "Pallet";
                dgvcMblnr.Width = 250;
                dgvcMblnr.ReadOnly = true;
                this.dgvStockData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.Name = "MATNR";
                dgvcMatnr.Width = 150;
                dgvcMatnr.ReadOnly = true;
                this.dgvStockData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcVersion = new DataGridViewTextBoxColumn();
                dgvcVersion.DataPropertyName = "CHARG";
                dgvcVersion.HeaderText = "版本";
                dgvcVersion.Name = "CHARG";
                dgvcVersion.ReadOnly = true;
                dgvcVersion.Width = 80;
                this.dgvStockData.Columns.Add(dgvcVersion);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "数量";
                dgvcMenge.Name = "MENGE";
                dgvcMenge.Width = 70;
                dgvcMenge.ReadOnly = true;
                this.dgvStockData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcCrdate = new DataGridViewTextBoxColumn();
                dgvcCrdate.DataPropertyName = "CRDAT";
                dgvcCrdate.HeaderText = "入库日期";
                dgvcCrdate.Width = 100;
                dgvcCrdate.ReadOnly = true;
                this.dgvStockData.Columns.Add(dgvcCrdate);

                this.dgvStockData.DataSource = dtStock;
                lblStock.Text = dtStock.Rows.Count.ToString() + " records";
                dgvStockData.ClearSelection();
                dgvStockData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }

        public void ShowStockDetailGrid()
        {
            try
            {
                this.dgvStockDetail.AutoGenerateColumns = false;
                this.dgvStockDetail.Columns.Clear();

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "厂区";
                dgvcWerks.Name = "WERKS";
                dgvcWerks.Width = 90;
                dgvcWerks.ReadOnly = true;
                this.dgvStockDetail.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "仓别";
                dgvcLgort.Name = "LGORT";
                dgvcLgort.Width = 90;
                dgvcLgort.ReadOnly = true;
                this.dgvStockDetail.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.Name = "LOCAT";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvStockDetail.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "PALLENT";
                dgvcMblnr.HeaderText = "Pallet";
                dgvcMblnr.Name = "Pallet";
                dgvcMblnr.Width = 220;
                dgvcMblnr.ReadOnly = true;
                this.dgvStockDetail.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcBoxID = new DataGridViewTextBoxColumn();
                dgvcBoxID.DataPropertyName = "BoxID";
                dgvcBoxID.HeaderText = "BoxID";
                dgvcBoxID.Width = 200;
                dgvcBoxID.ReadOnly = true;
                this.dgvStockDetail.Columns.Add(dgvcBoxID);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.Name = "MATNR";
                dgvcMatnr.Width = 150;
                dgvcMatnr.ReadOnly = true;
                this.dgvStockDetail.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcVersion = new DataGridViewTextBoxColumn();
                dgvcVersion.DataPropertyName = "CHARG";
                dgvcVersion.HeaderText = "版本";
                dgvcVersion.Name = "CHARG";
                dgvcVersion.ReadOnly = true;
                dgvcVersion.Width = 80;
                this.dgvStockDetail.Columns.Add(dgvcVersion);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "数量";
                dgvcMenge.Name = "MENGE";
                dgvcMenge.Width = 70;
                dgvcMenge.ReadOnly = true;
                this.dgvStockDetail.Columns.Add(dgvcMenge);

                this.dgvStockDetail.DataSource = dtDetail;
                //lblStock.Text = dtStock.Rows.Count.ToString() + " records";
                dgvStockDetail.ClearSelection();
                dgvStockDetail.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStockDetailGrid()");
            }
        }

        #endregion
        private void lnkSample_Click(object sender, EventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "Pallet查询模板.xlsx";
                try
                {
                    Process.Start("Excel", strPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"无法打开文件，请手动打开" + strPath);
                }
            }
            else
            {
                MessageBox.Show("未在数据库维护模板路径，请联系QWMS负责人");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            StartVarible();
            if (string.IsNullOrEmpty(strWerks))
            {
                stsWarning.Text = "厂区不能为空！！！";
                return;
            }
            QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
            try
            {
                if (this.txtFilePath.Text.Trim() == "")
                {
                    stsWarning.Text = "Please select one file!!";
                    return;
                }
                # region 校验格式
                string strFileName = this.txtFilePath.Text.Trim();
                string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                {
                    stsWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                    return;
                }
                # endregion
                dgvStockData.DataSource = null;
                txtPallet.Text = string.Empty;
                txtPallet.Enabled = false;
                //string strCmd = "select * from [Sheet1$]";
               // DataTable dtExcel = objExcel.ExcelQuery(txtFilePath.Text.ToString(), strCmd);
                # region 获取EXCEL数据
                DataTable dtExcel = objExcel.GetDataTableFromExcel(strFileName, true);
                #endregion
                if (dtExcel.Rows.Count > 0)
                {
                    dtPallet = new DataTable();
                    dtPallet.Columns.Add("PALLET");
                    foreach(DataRow dr in dtExcel.Rows)
                    {
                        DataRow drPallet = dtPallet.NewRow();
                        drPallet["PALLET"] = dr["PALLET"].ToString();
                        dtPallet.Rows.Add(drPallet);
                    }
                    ShowPalletGrid();
                    txtFilePath.Text = string.Empty;
                }
                else
                {
                    stsWarning.Text = "读取文档无数据,请确认!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            StartVarible();
            dtStock.Rows.Clear();
            dgvStockData.DataSource = null;
            strMachine = txtMachine.Text.ToString();
            //if(string.IsNullOrEmpty(strWerks))
            //{
            //    stsWarning.Text = "厂区不能为空！！！";
            //    return;
            //}
            if(!chkDate.Checked)
            {
                strFromDate = string.Empty;
                strToDate = string.Empty;
            }
            if(string.IsNullOrEmpty(txtPallet.Text.ToString()))
            {
                if (dtPallet.Rows.Count > 0)
                {
                    string strPal = String.Join("','", dtPallet.AsEnumerable().Select(d => d.Field<string>("PALLET")).ToArray());
                    dtStock = objStorageData.GetPalletStock(strPal, strWerks, strLgort, string.Empty, strFromDate, strToDate, string.Empty);
                }
                else
                {
                    dtStock = objStorageData.GetPalletStock(string.Empty, strWerks, strLgort, strMachine, strFromDate, strToDate, "ALL");
                }
            }
            else
            {
                dtStock = objStorageData.GetPalletStock(txtPallet.Text.ToString(), strWerks, strLgort, strMachine, strFromDate, strToDate, string.Empty);
            }
            if(dtStock.Rows.Count>0)
            {
                ShowStockGrid();
            }
            else
            {
                stsWarning.Text = "No Data";
            }
        }

        private void StartVarible()
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            strFromDate = dtpFromDate.Value.ToString("yyyyMMdd");
            strToDate = dtpToDate.Value.ToString("yyyyMMdd");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            txtPallet.Enabled = true;
            txtPallet.Text = string.Empty;
            strWerks = string.Empty;
            strLgort = string.Empty;
            strFromDate = string.Empty;
            strToDate = string.Empty;
            this.cmbWerks.SelectedIndex = -1;
            this.cmbLgort.SelectedIndex = -1;
            dgvPallet.DataSource = null;
            dgvStockData.DataSource = null;
            dgvStockDetail.DataSource = null;
            dtPallet.Rows.Clear();
            dtStock.Rows.Clear();
            dtDetail.Rows.Clear();
            txtFilePath.Text = string.Empty;
            lblStock.Text = "0 records";
            lblPal.Text = "0 records";
        }

        private void dgvPallet_MouseDown(object sender, MouseEventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                string strPallet = string.Empty;
                strMachine = string.Empty;
                dtStock.Rows.Clear();
                dgvStockData.DataSource = null;
                DataGridView.HitTestInfo hit = dgvPallet.HitTest(e.X, e.Y);
                StartVarible();
                //if (string.IsNullOrEmpty(strWerks))
                //{
                //    stsWarning.Text = "厂区不能为空！！！";
                //    return;
                //}

                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    DataGridViewCell clickedCell =
                        dgvPallet.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                    try
                    {
                        strPallet = clickedCell.Value.ToString();
                        if (!chkDate.Checked)
                        {
                            strFromDate = string.Empty;
                            strToDate = string.Empty;
                        }
                    }
                    catch
                    {
                        strPallet = "";
                    }
                    dtStock = objStorageData.GetPalletStock(strPallet, strWerks, strLgort, strMachine, strFromDate, strToDate, string.Empty);
                    ShowStockGrid();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            ShowDdlLgort();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            StartVarible();
            //if (string.IsNullOrEmpty(strWerks))
            //{
            //    stsWarning.Text = "厂区不能为空！！！";
            //    return;
            //}
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = ofdOpenFile.FileName;
                dtStock.Rows.Clear();
                dgvStockData.DataSource = null;
                txtPallet.Text = string.Empty;
                txtPallet.Enabled = false;
            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            string strPath = string.Empty;
            string strSheetName = string.Empty;
            ClaExeclHelper objExcel = new ClaExeclHelper();
            if(dtStock.Rows.Count>0)
            {
                if(sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strPath = sfdSaveFile.FileName;
                    objExcel.DatatableToExcel(dtStock, strPath, "PalletID");
                }
            }
            else
            {
                stsWarning.Text = "无数据需要导出";
            }
        }

        private void dgvStockData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                #region 查询Pallet的BOXID记录
                string strCurrentWerks = dgvStockData.Rows[e.RowIndex].Cells["WERKS"].Value.ToString();
                string strCurrentLgort = dgvStockData.Rows[e.RowIndex].Cells["LGORT"].Value.ToString();
                string strCurrentMblnr = dgvStockData.Rows[e.RowIndex].Cells["Pallet"].Value.ToString();
                string strCurrentMatnr = dgvStockData.Rows[e.RowIndex].Cells["MATNR"].Value.ToString();
                string strCurrentLocat = dgvStockData.Rows[e.RowIndex].Cells["LOCAT"].Value.ToString();
                Transfer objTransfer = new Transfer(UserData);
                dtDetail = objTransfer.GetPalletDetail(strCurrentWerks, strCurrentLgort, strCurrentMblnr, strCurrentMatnr,strCurrentLocat);
                ShowStockDetailGrid();
                #endregion
            }
        }

    }
}
