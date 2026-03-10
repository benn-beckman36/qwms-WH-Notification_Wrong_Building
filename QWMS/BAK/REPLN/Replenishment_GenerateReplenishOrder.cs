using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Replenishment_GenerateReplenishOrder : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strCharg = "";
        private string strMblnr = "";
        private string strMrgid = "";
        private string strIsmrg = "N";
        private ArrayList aryMblnr = new ArrayList();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private ArrayList aryMixedMaterial = new ArrayList();
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageOut objStorageOut;
        private StorageData objStorageData;
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

        public string Mrgid
        {
            get
            {
                return strMrgid;
            }
            set
            {
                strMrgid = value;
            }
        }

        public string Mblnr
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
            }
        }

        public string Matnr
        {
            get
            {
                return strMatnr;
            }
            set
            {
                strMatnr = value;
            }
        }

        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }
        public string Charg
        {
            get
            {
                return strCharg;
            }
            set
            {
                strCharg = value;
            }
        }
        #endregion

        #region 构造函数
        public Replenishment_GenerateReplenishOrder()
        {
            InitializeComponent();
        }

        public Replenishment_GenerateReplenishOrder(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;
            try
            {
                objReplenishment = new Replenishment(UserData, Progid);
                
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageOut = new StorageOut(UserData, Progid);



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
                    ShowPrintCheckBox();
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

        # endregion

        #region 检查用户是否有使用打印功能的权限
        private void ShowPrintCheckBox()
        {
            bool bolPrint = false;
            try
            {
                bolPrint = objStorageOut.CheckPrintCheckBox();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowPrintCheckBox()");
            }
            if (bolPrint == true)
            {
                this.chkPrint.Checked = true;
            }
            else
            {
                this.chkPrint.Checked = false;
            }
        }
        # endregion

        #region cmbWerks Selected Index Changed Event
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        # endregion

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";

                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";
                // Plant 和Storage 不能为空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);

                dtOutSource = objReplenishment.QueryNeedReplenishment();
                if (dtOutSource.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }

                ShowOutSourceDataGridView();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        #region ShowOutSourceDataGridView
        private void ShowOutSourceDataGridView()
        { 
            try
            {
                this.dtgOutSource.AutoGenerateColumns = false;
                this.dtgOutSource.Columns.Clear();

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Name = "Matnr";  // 设置列的名称
                dgvcMatnr.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Name = "Insmk";
                dgvcInsmk.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Name = "Charg";
                dgvcCharg.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Inventory Qty";
                dgvcBlace.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcRepot = new DataGridViewTextBoxColumn();
                dgvcRepot.DataPropertyName = "REPOT";
                dgvcRepot.HeaderText = "Replenishment Point";
                dgvcRepot.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcRepot);

                DataGridViewTextBoxColumn dgvcMaxge = new DataGridViewTextBoxColumn();
                dgvcMaxge.DataPropertyName = "MAXGE";
                dgvcMaxge.HeaderText = "Max Qty";
                dgvcMaxge.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcMaxge);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Replenish Qty";
                dgvcMenge.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcMenge);


                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcAlqty);



                DataGridViewTextBoxColumn dgvcTotal = new DataGridViewTextBoxColumn();
                dgvcTotal.DataPropertyName = "TOTAL";
                dgvcTotal.HeaderText = "W/H Qty";
                dgvcTotal.ReadOnly = true;
                this.dtgOutSource.Columns.Add(dgvcTotal);

                dtgOutSource.DataSource = dtOutSource;
  

                if (dtOutSource.Rows.Count > 0)
                {
                    this.panel1.Enabled = false;
                    this.btnQuery.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }

        }
        # endregion

        #region ShowStorageDataGridView
        private void ShowStorageDataGridView()
        {
            try
            {
                this.dtgStorage.AutoGenerateColumns = false;
                this.dtgStorage.Columns.Clear();

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcMgrid = new DataGridViewTextBoxColumn();
                dgvcMgrid.DataPropertyName = "MRGID";
                dgvcMgrid.HeaderText = "Mixed Material No";
                dgvcMgrid.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcMgrid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcOmblnr = new DataGridViewTextBoxColumn();
                dgvcOmblnr.DataPropertyName = "OMBLNR";
                dgvcOmblnr.HeaderText = "Store In Docu. No";
                dgvcOmblnr.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcOmblnr);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcRmak = new DataGridViewTextBoxColumn();
                dgvcRmak.DataPropertyName = "RMAK1";
                dgvcRmak.HeaderText = "Remark";
                dgvcRmak.ReadOnly = true;
                this.dtgStorage.Columns.Add(dgvcRmak);

                dtgStorage.DataSource = dtStorage;

                if (dtStorage.Rows.Count > 0)
                {
                    this.btnQuery.Enabled = false;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }
        # endregion

        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataRow[] foundRow;
                DataRow[] combineRow;
                DataRow drRow;
                strIsmrg = "N";
                string strTempMixedMaterial = "";
                string strOrderBy = "";
                string strTempCombine = "";
                string strAllCombine = "";
                aryMixedMaterial.Clear();
                DataSet dsData = new DataSet();
                DataTable dtTempStorage = new DataTable();

                //Order by
                if (rdoMatnr.Checked)
                {
                    strOrderBy = "MATNR, LOCAT, INDAT";
                }
                if (rdoLocat.Checked)
                {
                    strOrderBy = "LOCAT, MATNR, INDAT";
                }
                if (strOrderBy == "")
                {
                    strOrderBy = "LOCAT, MATNR, INDAT";
                }

                if (dtOutSource.Rows.Count == 0)
                {
                    stsWarning.Text = "Please input data first!!";
                    return;
                }

                objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);
                dsData = objReplenishment.QueryReplenishmentOutData(dtOutSource);
                dtOutSource = dsData.Tables[0].Copy();
                dtTempStorage = dsData.Tables[1].Copy();
                //dtOutSource.Columns.Add("BLACE");
                dtTempStorage.Columns.Add("BLACE");


                objStorageData = new StorageData(UserData, Werks, Lgort);
                
                int intCombineLocalTotal = 0;
                int intCombineLocatOut = 0;
                dtCombineStorage = dtTempStorage.Clone();
                for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                {
                    strTempCombine = dtTempStorage.Rows[i]["MANDT"].ToString() + dtTempStorage.Rows[i]["COMCD"].ToString() + dtTempStorage.Rows[i]["WERKS"].ToString() + dtTempStorage.Rows[i]["LGORT"].ToString() + dtTempStorage.Rows[i]["LOCAT"].ToString() + dtTempStorage.Rows[i]["MATNR"].ToString() + dtTempStorage.Rows[i]["INSMK"].ToString() + dtTempStorage.Rows[i]["CHARG"].ToString() + ";";
                    if (strAllCombine.IndexOf(strTempCombine) < 0)
                    {
                        intCombineLocatOut = 0;
                        strAllCombine += strTempCombine;
                        combineRow = dtTempStorage.Select("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "' and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "' and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "' and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                        }
                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString());
                        drRow = dtCombineStorage.NewRow();
                        drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                        drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                        drRow["TOLOC"] = dtTempStorage.Rows[i]["TOLOC"].ToString();
                        drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                        drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                        drRow["MENGE"] = intCombineLocalTotal.ToString();
                        drRow["ALQTY"] = intCombineLocatOut.ToString();
                        drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                        drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                        drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                        drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                        drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                        drRow["OMBLNR"] = "";
                        drRow["MRGID"] = "";
                        drRow["KOSTL"] = "";
                        drRow["ARBPL"] = "";
                        drRow["TRNTP"] = "";
                        drRow["RMAK1"] = "";
                        drRow["INDAT"] = "";
                        dtCombineStorage.Rows.Add(drRow);
                    }
                }

                dtStorage = dtCombineStorage.Clone();
                if (strOrderBy != "")
                {
                    foundRow = dtCombineStorage.Select("", strOrderBy);
                    for (int i = 0; i < foundRow.Length; i++)
                    {
                        drRow = dtStorage.NewRow();
                        drRow["MANDT"] = foundRow[i]["MANDT"].ToString();
                        drRow["COMCD"] = foundRow[i]["COMCD"].ToString();
                        drRow["WERKS"] = foundRow[i]["WERKS"].ToString();
                        drRow["LGORT"] = foundRow[i]["LGORT"].ToString();
                        drRow["LOCAT"] = foundRow[i]["LOCAT"].ToString();
                        drRow["TOLOC"] = foundRow[i]["TOLOC"].ToString();
                        drRow["MATNR"] = foundRow[i]["MATNR"].ToString();
                        drRow["INSMK"] = foundRow[i]["INSMK"].ToString();
                        drRow["CHARG"] = foundRow[i]["CHARG"].ToString();
                        drRow["MENGE"] = foundRow[i]["MENGE"].ToString();
                        drRow["ALQTY"] = foundRow[i]["ALQTY"].ToString();
                        drRow["MBLNR"] = foundRow[i]["MBLNR"].ToString();
                        drRow["ZEILE"] = foundRow[i]["ZEILE"].ToString();
                        drRow["EBELN"] = foundRow[i]["EBELN"].ToString();
                        drRow["LIFNR"] = foundRow[i]["LIFNR"].ToString();
                        drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                        drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                        drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                        drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                        drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                        drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                        drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                        dtStorage.Rows.Add(drRow);
                    }
                }
                else
                {
                    dtStorage = dtTempStorage;
                }
                ShowOutSourceDataGridView();
                ShowStorageDataGridView();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                aryMixedMaterial.Clear();

                objReplenishment = new Replenishment(UserData, Werks, Lgort, Progid);
                if (objReplenishment.GenerateReplenishmentOrder(ref dtStorage))
                {
                    stsWarning.Text = "Update OK!!";
                    this.btnSave.Enabled = false;
                    this.btnPrint.Enabled = true;
                    if (chkPrint.Checked)
                    {

                        ReportPrint objReportPrint = new ReportPrint(UserData, "REPLENISHOUT", dtStorage);
                        objReportPrint.Report.PrintToPrinter(1, true, 0, 0);
                    }
                }
                else
                {
                    stsWarning.Text = "Update fail!! " + objReplenishment.ERRMSG;
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

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportPrint objReportPrint = new ReportPrint(UserData, "REPLENISHOUT", dtStorage);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        # endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.strWerks = "";
            this.strLgort = "";
            ShowPrintCheckBox();
            this.rdoLocat.Checked = false;
            this.rdoMatnr.Checked = false;
            this.dtOutSource.Clear();
            this.dtStorage.Clear();
            this.dtgOutSource.DataSource = null;
            this.dtgStorage.DataSource = null;
            this.stsWarning.Text = "";
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            this.panel1.Enabled = true;
        }
        # endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        #region dtgOutSource Mouse Down Event
        private void dtgOutSource_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int intRowNo;
                stsWarning.Text = "";
                //DataGrid dgClick = (DataGrid)sender;
                //DataGrid.HitTestInfo hitRow;
                DataGridView dgvClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgvClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;
                    //dgvClick.CurrentCell = new DataGridCell(intRowNo, 0);
                    //strMatnr = dgClick[dgClick.CurrentCell].ToString();
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 1);
                    //strInsmk = dgClick[dgClick.CurrentCell].ToString();
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 2);
                    //strCharg = dgClick[dgClick.CurrentCell].ToString();
                    //dgClick.CurrentCell = new DataGridCell(intRowNo, 3);
                    //strMblnr = dgClick[dgClick.CurrentCell].ToString();

                    //strMatnr = dgvClick.Rows[intRowNo].Cells["Matnr"].Value.ToString();
                    //strInsmk = dgvClick.Rows[intRowNo].Cells["Insmk"].Value.ToString();
                    //strCharg = dgvClick.Rows[intRowNo].Cells["Charg"].Value.ToString();
                    //strMblnr = dgvClick.Rows[intRowNo].Cells["Mblnr"].Value.ToString();

                    //StorageOut_StorageOut_Add objStorageOut_StorageOut_Add = new StorageOut_StorageOut_Add(Mandt, Usrnm, Progid, Werks, Lgort, Matnr, Insmk, Charg, Mblnr, dtOutSource);
                    //objStorageOut_StorageOut_Add.ShowDialog();
                    //dtOutSource = objStorageOut_StorageOut_Add.SapData;
                    ShowOutSourceDataGridView();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        #region 调整布局大小
        private void Replenishment_GenerateReplenishOrder_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        # endregion

    }
}
