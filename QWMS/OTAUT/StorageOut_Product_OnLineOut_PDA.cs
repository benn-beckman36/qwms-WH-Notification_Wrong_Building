using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;
using System.Text;

namespace QWMS
{
    public partial class StorageOut_Product_OnLineOut_PDA : Form
    {
        #region 變數宣告

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        //private string strPickUp = "";
        private string strGRRNO = "";
        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtData = new DataTable();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();

        #endregion

        #region DataMember

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

        public ArrayList Mblnrs
        {
            get
            {
                return alMblnrs;
            }
            set
            {
                alMblnrs = value;
            }
        }
        #endregion

        public StorageOut_Product_OnLineOut_PDA()
        {
            InitializeComponent();
        }

        #region 建構函數
        public StorageOut_Product_OnLineOut_PDA(UserInfo varUserData, string strProgid)
		{
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);

                //檢查權限
                if (!StorageOut.CheckAuthority())
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

        #region 設定State Bar中的日期
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;

        }
        #endregion

        #region 設定預設是否可列印撿料單
        private void ShowPrintCheckBox()
        {
            bool bolPrint = false;
            try
            {
                StorageOut objStorageOut = new StorageOut(UserData, Progid);
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
        #endregion

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            Authority objAuthority = new Authority(UserData);
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

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
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

        private void txtMblnr_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                alMblnrs.Clear();
                if (txtMblnr.Text.Trim() != "")
                {
                    alMblnrs.Add(txtMblnr.Text.Trim());
                }

                StorageOut_SDSDataSelect objStorageOut_SDSDataSelect = new StorageOut_SDSDataSelect(UserData, Werks, Lgort, Progid, Mblnrs, "ONLINEPROD");
                objStorageOut_SDSDataSelect.ShowDialog();
                Mblnrs = objStorageOut_SDSDataSelect.Mblnr;
                txtMblnr.Text = GetMblnrData();
                if (Mblnrs.Count > 0)
                {
                    this.txtMblnr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #region GetMblnrData
        private string GetMblnrData()
        {
            try
            {
                StringBuilder sbMblnr = new StringBuilder();
                sbMblnr.Remove(0, sbMblnr.Length);
                for (int i = 0; i < Mblnrs.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(",");
                    sbMblnr.Append(Mblnrs[i].ToString().Trim());
                }
                return sbMblnr.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetMblnrData()");
            }
        }
        #endregion

        #region ShowOutSourceDataGrid
        private void ShowOutSourceDataGrid()
        {
            this.dgvOutSource.AutoGenerateColumns = false;
            this.dgvOutSource.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcCharg);


                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serial No.";
                dgvcSerno.Width = 90;
                dgvcSerno.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store Out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcTrntp);

                dgvOutSource.DataSource = dtOutSource;
                lblOutSource.Text = dtOutSource.Rows.Count.ToString() + " records";

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
        #endregion

        #region ShowStorageDataGrid
        private void ShowStorageDataGrid()
        {
            this.dgvStorage.AutoGenerateColumns = false;
            this.dgvStorage.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcCharg);

                if (true == rdoSerNo.Checked)
                {
                    DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                    dgvcSerno.DataPropertyName = "SERNO";
                    dgvcSerno.HeaderText = "Serial No.";
                    dgvcSerno.Width = 90;
                    dgvcSerno.ReadOnly = true;
                    this.dgvOutSource.Columns.Add(dgvcSerno);
                }

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "Blace";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 80;
                dgvcBlace.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcBlace);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 100;
                dgvcMblnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMblnr);

                //20060601 若by Serial No.方式出貨才show
                if (true == rdoSerNo.Checked)
                {
                    DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                    dgvcZeile.DataPropertyName = "ZEILE";
                    dgvcZeile.HeaderText = "Document Item";
                    dgvcZeile.Width = 90;
                    dgvcZeile.ReadOnly = true;
                    this.dgvStorage.Columns.Add(dgvcZeile);
                }

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "OMBLNR";
                dgvcTrntp.HeaderText = "Store In Doc. No";
                dgvcTrntp.Width = 100;
                dgvcTrntp.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Vendor Manufactured Date";
                dgvcIndat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcrmak1 = new DataGridViewTextBoxColumn();
                dgvcrmak1.DataPropertyName = "RMAK1";
                dgvcrmak1.HeaderText = "Remark";
                dgvcrmak1.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcrmak1);

                //Order by
                if (rdoMatnr.Checked)
                {
                    dtStorage.DefaultView.Sort = "MATNR";
                }
                else
                {
                    dtStorage.DefaultView.Sort = "LOCAT";
                }

                dgvStorage.DataSource = dtCombineStorage;
                lblStorage.Text = dtCombineStorage.Rows.Count.ToString() + " records";

                if (dtCombineStorage.Rows.Count > 0)
                {
                    this.btnQuery.Enabled = false;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;

        }
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                string[] aryTempMblnr = txtMblnr.Text.Trim().Split(new char[] { ',' });
                Mblnrs.Clear();
                if (Mblnrs.Count == 0)
                {
                    for (int i = 0; i < aryTempMblnr.Length; i++)
                    {
                        Mblnrs.Add(aryTempMblnr[i].ToString().Trim());
                    }
                }

                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";
                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                //單號不為空
                if (Mblnrs.Count == 0)
                {
                    stsWarning.Text = "Document No can't be empty!!";
                    return;
                }

                SapData objSapData = new SapData(UserData, Werks, Lgort);
                dtOutSource = objSapData.QuerySapLineOutData(Mblnrs, "ONLINEPROD");
                if (dtOutSource.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }

                ShowOutSourceDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataRow[] foundRow;
                DataRow[] combineRow;
                DataRow drRow;
                string strOrderBy = "";

                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();

                DataTable dtTempStorage = new DataTable();
                DataSet dsData = new DataSet();
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


                StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                dsData = objStorageData.QueryOnLineOutData(dtOutSource, "", "", false, "", rdoPallet.Checked, rdoSerNo.Checked);

                dtOutSource = dsData.Tables[0].Copy();
                dtTempStorage = dsData.Tables[1].Copy();
                dtTempStorage.Columns.Add("BLACE");


                //將同儲位同料號的資料加總 Kent 20050904
                int intCombineLocalTotal = 0;
                int intCombineLocatOut = 0;
                dtCombineStorage = dtTempStorage.Clone();
                for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                {
                    #region 每次比對的Index (sbCombineIndex)
                    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                    sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                    sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                    sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                    sbCombineIndex.Append(" and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "'");
                    sbCombineIndex.Append(" and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                    sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                    sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                    if (true == rdoSerNo.Checked)
                    {
                        sbCombineIndex.Append(" and MBLNR='" + dtTempStorage.Rows[i]["MBLNR"].ToString() + "'");
                        sbCombineIndex.Append(" and SERNO='" + dtTempStorage.Rows[i]["SERNO"].ToString() + "'");
                    }

                    #endregion

                    if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                    {
                        intCombineLocatOut = 0;
                        alAllCombine.Add(sbCombineIndex.ToString());

                        combineRow = dtTempStorage.Select(sbCombineIndex.ToString());
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                        }
                        string strSerno = "";
                        if (true == rdoSerNo.Checked)
                            strSerno = dtTempStorage.Rows[i]["SERNO"].ToString().Trim();

                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString(), strSerno, "");


                        drRow = dtCombineStorage.NewRow();
                        drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                        drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                        drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                        drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                        drRow["MENGE"] = intCombineLocalTotal.ToString();
                        drRow["ALQTY"] = intCombineLocatOut.ToString();
                        drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                        drRow["MTYPE"] = dtTempStorage.Rows[i]["MTYPE"].ToString();
                        drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                        drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                        drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                        drRow["RMANO"] = "";
                        drRow["OMBLNR"] = dtTempStorage.Rows[i]["OMBLNR"].ToString();
                        drRow["MRGID"] = "";
                        drRow["KOSTL"] = "";
                        drRow["ARBPL"] = "";
                        drRow["TRNTP"] = "";
                        drRow["RMAK1"] = "";
                        drRow["INDAT"] = dtTempStorage.Rows[i]["INDAT"].ToString();
                        //20051021 marc 新增客人料號 KDMAT
                        drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                        //DateCode
                        drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                        drRow["LOCOD"] = dtTempStorage.Rows[i]["LOCOD"].ToString();
                        drRow["INSPT"] = dtTempStorage.Rows[i]["INSPT"].ToString();

                        if (false == rdoSerNo.Checked)
                        {
                            drRow["ZEILE"] = "";
                            drRow["SERNO"] = "";
                        }
                        else
                        {
                            drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                            drRow["SERNO"] = dtTempStorage.Rows[i]["SERNO"].ToString();
                        }

                        dtCombineStorage.Rows.Add(drRow);
                    }
                }


                dtStorage = dtTempStorage.Clone();
                dtStorage.Columns.Add("MENGE1");
                if (strOrderBy != "")
                {
                    foundRow = dtTempStorage.Select("", strOrderBy);
                    for (int i = 0; i < foundRow.Length; i++)
                    {
                        drRow = dtStorage.NewRow();
                        drRow["MANDT"] = foundRow[i]["MANDT"].ToString();
                        drRow["COMCD"] = foundRow[i]["COMCD"].ToString();
                        drRow["WERKS"] = foundRow[i]["WERKS"].ToString();
                        drRow["LGORT"] = foundRow[i]["LGORT"].ToString();
                        drRow["LOCAT"] = foundRow[i]["LOCAT"].ToString();
                        drRow["MATNR"] = foundRow[i]["MATNR"].ToString();
                        drRow["INSMK"] = foundRow[i]["INSMK"].ToString();
                        drRow["CHARG"] = foundRow[i]["CHARG"].ToString();
                        drRow["MENGE"] = foundRow[i]["MENGE"].ToString();
                        drRow["ALQTY"] = foundRow[i]["ALQTY"].ToString();
                        drRow["MTYPE"] = foundRow[i]["MTYPE"].ToString();
                        drRow["MBLNR"] = foundRow[i]["MBLNR"].ToString();
                        drRow["ZEILE"] = foundRow[i]["ZEILE"].ToString();
                        drRow["EBELN"] = foundRow[i]["EBELN"].ToString();
                        drRow["LIFNR"] = foundRow[i]["LIFNR"].ToString();
                        drRow["RMANO"] = foundRow[i]["RMANO"].ToString();
                        drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                        drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                        drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                        drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                        drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                        drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                        drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                        drRow["MENGE1"] = foundRow[i]["MENGE"].ToString();
                        drRow["BLACE"] = foundRow[i]["BLACE"].ToString();
                        //20051021 MARC 新增客人料號 KDMAT
                        drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                        //成品
                        drRow["SERNO"] = foundRow[i]["SERNO"].ToString();
                        //DateCode
                        drRow["DACOD"] = foundRow[i]["DACOD"].ToString();
                        drRow["LOCOD"] = foundRow[i]["LOCOD"].ToString();
                        drRow["INSPT"] = foundRow[i]["INSPT"].ToString();
                        dtStorage.Rows.Add(drRow);
                    }
                }
                else
                {
                    dtStorage = dtTempStorage;
                }

                ShowOutSourceDataGrid();
                ShowStorageDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetbtnSaveProcess();
            DataTable dtTempData = new DataTable();
            try
            {
                stsWarning.Text = "";
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                
                strGRRNO = objStorageOut.AddProductOutData_PickUp(dtOutSource, dtStorage, dtCombineStorage, "", "");
                if (strGRRNO != "")
                {
                    stsWarning.Text = "Update OK!!";
                    this.btnSave.Enabled = false;
                    this.btnPrint.Enabled = true;

                    //取得出庫單資料  Smose Liao 20090930
                    DataSet dsData = new DataSet();
                    DataTable dt1Data = new DataTable();
                    StorageData objStorageData = new StorageData(UserData, Werks, Lgort);

                    dsData = objStorageData.QueryOnLineOutGrrno_Product(dtData, strGRRNO);
                    dt1Data = dsData.Tables[0].Copy();

                    if (chkPrint.Checked)
                    {
                        #region Sort Print的資料
                        DataTable dtPrint = new DataTable();
                        dtPrint = dtCombineStorage.Clone();
                        DataRow drNew;
                        DataRow[] drArray;
                        if (rdoMatnr.Checked)
                        {
                            drArray = dtCombineStorage.Select("", "MATNR,LOCAT,INDAT");
                        }
                        else
                        {
                            drArray = dtCombineStorage.Select("", "LOCAT,MATNR,INDAT");
                        }


                        foreach (DataRow dr in drArray)
                        {
                            drNew = dtPrint.NewRow();

                            for (int i = 0; i < dtCombineStorage.Columns.Count; i++)
                            {
                                drNew[i] = dr[i].ToString();
                            }
                            dtPrint.Rows.Add(drNew);
                        }
                        #endregion

                        ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_PRODUCT", dtPrint);
                        objReportPrint.Report.PrintToPrinter(1, true, 0, 0);
                        dtData = dtPrint.Clone();
                    }
                }
                else
                {
                    stsWarning.Text = "Update fail!! " + objStorageOut.ERRMSG;
                    SetbtnSaveException();
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetbtnSaveException();
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataSet dsData = new DataSet();
            
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData, Werks, Lgort);
            dsData = objStorageData.QueryOnLineOutGrrno_Product(dtData, strGRRNO);
            DataTable dtPrint = new DataTable();
            dtData = dsData.Tables[0].Copy();
            dtPrint = dtData.Clone();

            //DataRow drNew;
            //DataRow[] drArray;

            //drArray = dtData.Select("", "ITEMNUM");
            //foreach (DataRow dr in drArray)
            //{
            //    drNew = dtPrint.NewRow();

            //    for (int i = 0; i < dtData.Columns.Count; i++)
            //    {
            //        drNew[i] = dr[i].ToString();
            //    }
            //    dtPrint.Rows.Add(drNew);
            //}

            #region 將同儲位,料號,庫別,版本的資料作匯總  Smose Liao 20100401
            StringBuilder sbCombinePrint = new StringBuilder();
            ArrayList alCombinePrint = new ArrayList();
            DataRow[] combineRow;
            DataRow drRow;

            int intCombineLocalTotal = 0;
            int intCombineLocatOut = 0;
            dtPrint = dtData.Clone();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                #region 每次比對的Index (sbCombinePrint)
                sbCombinePrint.Remove(0, sbCombinePrint.Length);
                sbCombinePrint.Append("MANDT='" + dtData.Rows[i]["MANDT"].ToString() + "'");
                sbCombinePrint.Append(" and COMCD='" + dtData.Rows[i]["COMCD"].ToString() + "'");
                sbCombinePrint.Append(" and WERKS='" + dtData.Rows[i]["WERKS"].ToString() + "'");
                sbCombinePrint.Append(" and LGORT='" + dtData.Rows[i]["LGORT"].ToString() + "'");
                sbCombinePrint.Append(" and LOCAT='" + dtData.Rows[i]["LOCAT"].ToString() + "'");
                sbCombinePrint.Append(" and MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "'");
                sbCombinePrint.Append(" and INSMK='" + dtData.Rows[i]["INSMK"].ToString() + "'");
                #endregion

                if (alCombinePrint.IndexOf(sbCombinePrint.ToString()) < 0)
                {
                    intCombineLocatOut = 0;
                    intCombineLocalTotal = 0;
                    alCombinePrint.Add(sbCombinePrint.ToString());

                    combineRow = dtData.Select(sbCombinePrint.ToString());
                    for (int j = 0; j < combineRow.Length; j++)
                    {
                        intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());  //出庫數量
                    }

                    //庫存數量
                    intCombineLocalTotal = objStorageData.QueryMatnrQty(dtData.Rows[i]["LOCAT"].ToString(), dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString(), "", "", "", "", "", "", "");

                    drRow = dtPrint.NewRow();
                    drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString();
                    drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString();
                    drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                    drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                    drRow["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString();
                    drRow["GRRNO"] = dtData.Rows[i]["GRRNO"].ToString();
                    drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                    drRow["INSMK"] = dtData.Rows[i]["INSMK"].ToString();
                    drRow["CHARG"] = dtData.Rows[i]["CHARG"].ToString();
                    drRow["BKQTY"] = intCombineLocalTotal.ToString();
                    drRow["ALQTY"] = intCombineLocatOut.ToString();
                    drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                    drRow["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString();
                    drRow["LIFNR"] = dtData.Rows[i]["LIFNR"].ToString();
                    drRow["KDMAT"] = dtData.Rows[i]["KDMAT"].ToString();
                    drRow["INDAT"] = dtData.Rows[i]["INDAT"].ToString();
                    drRow["OMBLN"] = dtData.Rows[i]["OMBLN"].ToString();
                    drRow["NEW_GRRNO"] = dtData.Rows[i]["NEW_GRRNO"].ToString();
                    dtPrint.Rows.Add(drRow);
                }
            }
            #endregion

            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_PRODUCT", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.strWerks = "";
            this.strLgort = "";
            ShowPrintCheckBox();
            this.txtMblnr.Text = "";
            this.alMblnrs.Clear();
            this.txtMblnr.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.lblOutSource.Text = "0 records";
            this.lblStorage.Text = "0 records";
            this.dtOutSource.Clear();
            this.dtStorage.Clear();
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            this.stsWarning.Text = "";
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            this.panel1.Enabled = true;
            this.rdoLocat.Checked = false;
            this.rdoMatnr.Checked = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMblnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    btnConfirm_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    DataTable dtTmp = new DataTable();
        //    DataTable dtTest = new DataTable();
        //    DataSet dsData = new DataSet();
        //    strGRRNO = "100402001";

        //    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData, Werks, Lgort);
        //    dsData = objStorageData.QueryOnLineOutGrrno_Product(dtData, strGRRNO);
        //    DataTable dtPrint = new DataTable();
        //    dtTmp = dsData.Tables[0].Copy();
        //    dtTest = dtTmp.Clone();

        //    dtTest = objStorageData.QueryBlockMatnr(dtTmp.Rows[0]["LOCAT"].ToString(), dtTmp.Rows[0]["MATNR"].ToString(), dtTmp.Rows[0]["INSMK"].ToString(), dtTmp.Rows[0]["CHARG"].ToString(), dtTmp.Rows[0]["OMBLN"].ToString(), dtTmp.Rows[0]["INDAT"].ToString());
        //}

    }
}
