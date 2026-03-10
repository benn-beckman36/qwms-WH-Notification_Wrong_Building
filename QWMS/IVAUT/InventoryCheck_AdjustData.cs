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
    public partial class InventoryCheck_AdjustData : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strCycno = "";
        private string strStats = "";
        private string strExportName = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData = new DataTable();
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;
       // private AccessConfig objConfig;

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

        public string Kostl
        {
            get
            {
                return txtKostl.Text.Trim();
            }
            set
            {
                txtKostl.Text = value;
            }
        }
        # endregion

        public InventoryCheck_AdjustData()
        {
            InitializeComponent();
        }

        public InventoryCheck_AdjustData(UserInfo varUserData, string strProgid)
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
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                //檢查權限
                if (!objCounting.CheckAuthority())
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

        # region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        # endregion

        # region Show Storage
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

        # region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcCycno = new DataGridViewTextBoxColumn();
                dgvcCycno.DataPropertyName = "CYCNO";
                dgvcCycno.HeaderText = "Counting No";
                dgvcCycno.ReadOnly = true;
                dgvcCycno.Width = 100;
                dgvData.Columns.Add(dgvcCycno);

                DataGridViewTextBoxColumn dgvcStats = new DataGridViewTextBoxColumn();
                dgvcStats.DataPropertyName = "STATS";
                dgvcStats.HeaderText = "Status";
                dgvcStats.ReadOnly = true;
                dgvcStats.Width = 40;
                dgvData.Columns.Add(dgvcStats);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 90;
                dgvData.Columns.Add(dgvcMatnr);

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
                dgvcCharg.Width = 60;
                dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcCkqty = new DataGridViewTextBoxColumn();
                dgvcCkqty.DataPropertyName = "CKQTY";
                dgvcCkqty.HeaderText = "Counting Qty";
                dgvcCkqty.ReadOnly = true;
                dgvData.Columns.Add(dgvcCkqty);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "CKDAT";
                dgvcIndat.HeaderText = "Counging Date";
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

        # region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
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

                if (strWerks == "" || strLgort == "" || Kostl == "")
                {
                    stsWarning.Text = "Plant,storage and dept no can't be empty!!";
                    return;
                }

                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                dtData = objCounting.QueryCountingData(Kostl);
                if (dtData.Rows.Count == 0)
                {
                    ShowDataGrid();
                    stsWarning.Text = "No Data!!";
                    return;
                }
                else
                {
                    btnAdjust.Enabled = true;
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
                this.txtKostl.Text = "";
                this.btnPrint.Enabled = false;
                this.btnAdjust.Enabled = false;
                this.btnExport.Enabled = false;
                this.btnConfirm.Enabled = true;
                this.dgvData.DataSource = null;
                this.lblCount.Text = "";
                this.dtData.Rows.Clear();
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

        # region Print
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
                ReportPrint objReportPrint = new ReportPrint(UserData, "COUNTADJUST", dtData);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region Click RowHeader
        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {                
                strMatnr = dgvData.CurrentRow.Cells[3].Value.ToString();//Part No
                strInsmk = dgvData.CurrentRow.Cells[4].Value.ToString();//Stock
                strCharg = dgvData.CurrentRow.Cells[5].Value.ToString();//Version
                strCycno = dgvData.CurrentRow.Cells[0].Value.ToString();//Counting No
                strStats = dgvData.CurrentRow.Cells[1].Value.ToString();//Status

                if (strStats == "1" || strStats == "2")
                {
                    InventoryCheck_AdjustData_Qty objInventoryCheck_AdjustData_Qty = new InventoryCheck_AdjustData_Qty(UserData, Progid, strMatnr, strInsmk, strCharg, strCycno, strStats, dtData);
                    objInventoryCheck_AdjustData_Qty.ShowDialog();
                    dtData = objInventoryCheck_AdjustData_Qty.QtyData;
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

        # region 自动调账
        private void btnAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    //未調帳才自動帶入盤點數量
                    if (dtData.Rows[i]["STATS"].ToString() == "1")
                    {
                        dtData.Rows[i]["CKQTY"] = dtData.Rows[i]["MENGE"];
                        dtData.Rows[i]["STATS"] = "3";
                    }
                }
                if (objCounting.UpdateCountingData(Kostl, dtData))
                {
                    ShowDataGrid();
                    btnPrint.Enabled = true;
                    btnExport.Enabled = true;
                }
                else
                {
                    stsWarning.Text = "Update Fail!! " + objCounting.ERRMSG; ;
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

        # region Export File
        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.GetEncoding(936));
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += complement(14, "L", dtData.Rows[i]["WERKS"].ToString());
                    strLine += "," + complement(13, "L", dtData.Rows[i]["STATS"].ToString());
                    strLine += "," + complement(13, "L", dtData.Rows[i]["CYCNO"].ToString());
                    strLine += "," + complement(13, "L", dtData.Rows[i]["INSMK"].ToString());
                    strLine += "," + complement(13, "L", dtData.Rows[i]["LGORT"].ToString());
                    strLine += "," + complement(13, "L", dtData.Rows[i]["LOCAT"].ToString());
                    strLine += "," + complement(20, "L", dtData.Rows[i]["MATNR"].ToString());
                    strLine += "," + complement(13, "L", dtData.Rows[i]["MENGE"].ToString());
                    strLine += "," + complement(13, "L", dtData.Rows[i]["CKQTY"].ToString());
                    strLine += "," + complement(13, "L", "");
                    strLine += "," + complement(13, "L", dtData.Rows[i]["CHARG"].ToString());
                    strLine += "," + complement(13, "L", dtData.Rows[i]["KOSTL"].ToString());
                    strLine += "," + complement(13, "L", "");
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
        # endregion

        # region complement
        private string complement(int intStringLen, string varArrow, string varDataValue)
        {
            string strTemp = "";
            try
            {
                if (varArrow.ToUpper() == "R")
                {
                    if (intStringLen >= varDataValue.Length)
                    {
                        for (int i = 0; i < intStringLen - varDataValue.Length; i++)
                        {
                            strTemp += " ";
                        }
                        strTemp += varDataValue;
                    }
                    else
                    {
                        strTemp = varDataValue.Substring(0, intStringLen);
                    }
                }

                if (varArrow.ToUpper() == "L")
                {
                    if (intStringLen >= varDataValue.Length)
                    {
                        strTemp += varDataValue;
                        for (int i = 0; i < intStringLen - varDataValue.Length; i++)
                        {
                            strTemp += " ";
                        }
                    }
                    else
                    {
                        strTemp = varDataValue.Substring(0, intStringLen);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-complement()");
            }
            return strTemp;

        }
        # endregion

        # region Export
        private void btnExport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                sfdSaveFile.FileName = txtKostl.Text.Trim();
                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    CountingResult2File(strExportName);
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
        private void InventoryCheck_AdjustData_Resize(object sender, EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.28), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40;
        }
        # endregion

    }
}
