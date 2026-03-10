using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class StorageOut_OnLineOut_DOA : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strType = "";
        private int FirstLgortIndex;
        private ArrayList alLocat = new ArrayList();
        private ArrayList aryMatnr = new ArrayList();
        private ArrayList alMblnrs = new ArrayList();
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

        public ArrayList CurLocat
        {
            get
            {
                return alLocat;
            }
            set
            {
                alLocat = value;
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

        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }

        public string Locat
        {
            get
            {
                return strLocat;
            }
            set
            {
                strLocat = value;
            }
        }

        #endregion

        public StorageOut_OnLineOut_DOA()
        {
            InitializeComponent();
        }

        public StorageOut_OnLineOut_DOA(UserInfo varUserData, string strProgid)
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

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = FirstLgortIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
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

        #region cmbWerks_SelectedIndexChanged
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

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
                        //系統給定預設的倉別為FGSP  Smose Liao 20100506
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == "FGSP")
                        {
                            FirstLgortIndex = i;
                        }

                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }

                    //系統給定預設的倉別為FGSP  Smose Liao 20100506
                    cmbLgort.SelectedIndex = FirstLgortIndex;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region txtMblnr_DoubleClick
        private void txtMblnr_DoubleClick(object sender, System.EventArgs e)
        {
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

                //廠區不為空
                if (Werks == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                alMblnrs.Clear();
                if (txtMblnr.Text.Trim() != "")
                {
                    alMblnrs.Add(txtMblnr.Text.Trim());
                }
                StorageOut_SapDataSelect objStorageOut_SapDataSelect = new StorageOut_SapDataSelect(UserData, Werks, Lgort, Progid, Mblnrs, "DOA");
                objStorageOut_SapDataSelect.ShowDialog();
                Mblnrs = objStorageOut_SapDataSelect.Mblnr;
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
        #endregion

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

            dgvOutSource.AutoGenerateColumns = false;
            dgvOutSource.Columns.Clear();
            try
            {
                //WERKS
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 60;
                dgvcWerks.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcWerks);

                //LGORT
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 60;
                dgvcLgort.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcLgort);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "SI No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcZeile);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMatnr);

                //RMANO
                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No.";
                dgvcRmano.Width = 90;
                dgvcRmano.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcRmano);

                //EBELN
                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No.";
                dgvcEbeln.Width = 90;
                dgvcEbeln.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcEbeln);

                //KDMAT
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.Width = 90;
                dgvcKdmat.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcKdmat);

                //INSMK
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 60;
                dgvcInsmk.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcInsmk);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCharg);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store Out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcAlqty);

                //BLACE
                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 80;
                dgvcBlace.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcBlace);

                //TRNTP
                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.Width = 90;
                dgvcTrntp.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcTrntp);


                dgvOutSource.DataSource = dtOutSource;
                lblOutSource.Text = dtOutSource.Rows.Count.ToString() + " records";

                if (dtOutSource.Rows.Count > 0)
                {
                    this.panel5.Enabled = true;
                    this.panel4.Enabled = false;
                    this.panel6.Enabled = false;
                    this.cmbLgort.Enabled = true;
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
            dgvStorage.AutoGenerateColumns = false;
            dgvStorage.Columns.Clear();

            try
            {
                //SELECT 
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "SELECT";
                dgvcSelect.HeaderText = "Select";
                dgvcSelect.Width = 50;
                dgvStorage.Columns.Add(dgvcSelect);

                //WERKS
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 60;
                dgvcWerks.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcWerks);

                //LGORT
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 60;
                dgvcLgort.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLgort);

                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLocat);

                //Shipment Location
                DataGridViewTextBoxColumn dgvcNloca = new DataGridViewTextBoxColumn();
                dgvcNloca.DataPropertyName = "NLOCA";
                dgvcNloca.HeaderText = "Shipment Location";
                dgvcNloca.Width = 90;
                dgvcNloca.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcNloca);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMatnr);

                //RMANO
                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No.";
                dgvcRmano.Width = 90;
                dgvcRmano.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcRmano);

                //KDMAT
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.Width = 90;
                dgvcKdmat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcKdmat);

                //SERNO
                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serial No.";
                dgvcSerno.Width = 110;
                dgvcSerno.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcSerno);

                //INSMK
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 60;
                dgvcInsmk.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcInsmk);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcAlqty);

                //BLACE
                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Balance Qty";
                dgvcBlace.Width = 80;
                dgvcBlace.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcBlace);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width =110;
                dgvcMblnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMblnr);

                //OMBLNR
                DataGridViewTextBoxColumn dgvcOmblnr = new DataGridViewTextBoxColumn();
                dgvcOmblnr.DataPropertyName = "OMBLNR";
                dgvcOmblnr.HeaderText = "Store In Docu. No";
                dgvcOmblnr.Width = 110;
                dgvcOmblnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcOmblnr);

                //INDAT
                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.Width = 90;
                dgvcIndat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcIndat);

                //RMAK1
                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.Width = 90;
                dgvcRmak1.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcRmak1);

                //Order by
                if (rdoMatnr.Checked)
                {
                    dtStorage.DefaultView.Sort = "MATNR";
                }
                else
                {
                    dtStorage.DefaultView.Sort = "LOCAT";
                }

                dgvStorage.DataSource = dtStorage;
                lblStorage.Text = dtStorage.Rows.Count.ToString() + " records";

                if (dtStorage.Rows.Count > 0)
                {
                    this.btnQuery.Enabled = false;
                    this.btnSave.Enabled = true;
                    this.cmbLgort.Enabled = false;
                    this.chkRmano.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region btnQuery_Click
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
                if (chkRmano.Checked == true)
                {
                    dsData = objStorageData.QueryOnLineOutData_DOA(dtOutSource, "", "", false, "", false, false, true);
                }
                else
                {
                    dsData = objStorageData.QueryOnLineOutData_DOA(dtOutSource, "", "", false, "", false, false, false);
                }

                dtOutSource = dsData.Tables[0].Copy();
                dtTempStorage = dsData.Tables[1].Copy();
                dtTempStorage.Columns.Add("BLACE");

                //將同儲位同料號的資料加總
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
                    sbCombineIndex.Append(" and LGORT='" + cmbLgort.Items[cmbLgort.SelectedIndex].ToString() + "'");
                    sbCombineIndex.Append(" and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                    sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                    sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
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
                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString(), "", "", "", "");

                        drRow = dtCombineStorage.NewRow();
                        drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                        drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                        drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                        drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                        drRow["MTYPE"] = dtTempStorage.Rows[i]["MTYPE"].ToString();
                        drRow["MENGE"] = intCombineLocalTotal.ToString();
                        drRow["ALQTY"] = intCombineLocatOut.ToString();
                        drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                        drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                        drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                        drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                        drRow["OMBLNR"] = dtTempStorage.Rows[i]["OMBLNR"].ToString();
                        drRow["ARBPL"] = dtTempStorage.Rows[i]["ARBPL"].ToString();
                        drRow["TRNTP"] = dtTempStorage.Rows[i]["TRNTP"].ToString();
                        drRow["INDAT"] = dtTempStorage.Rows[i]["INDAT"].ToString();
                        drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                        drRow["RMANO"] = dtTempStorage.Rows[i]["RMANO"].ToString();
                        drRow["SERNO"] = "";
                        drRow["MRGID"] = "";
                        drRow["KOSTL"] = "";
                        drRow["RMAK1"] = "";
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
                        drRow["MTYPE"] = foundRow[i]["MTYPE"].ToString();
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
                        drRow["MENGE1"] = foundRow[i]["MENGE"].ToString();
                        drRow["BLACE"] = int.Parse(foundRow[i]["MENGE"].ToString()) - int.Parse(foundRow[i]["ALQTY"].ToString());
                        drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                        drRow["SERNO"] = foundRow[i]["SERNO"].ToString();
                        drRow["RMANO"] = foundRow[i]["RMANO"].ToString();
                        dtStorage.Rows.Add(drRow);
                    }
                }
                else
                {
                    dtStorage = dtTempStorage;
                }

                //新增待出貨儲位
                DataColumn cNloca = new DataColumn("NLOCA", typeof(string));
                dtStorage.Columns.Add(cNloca);

                DataColumn Nlocat = new DataColumn("NLOCA", typeof(string));
                dtCombineStorage.Columns.Add(Nlocat);

                //新增勾選的核取方塊
                DataColumn cSelect = new DataColumn("SELECT", typeof(bool));
                dtStorage.Columns.Add(cSelect);
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    dtStorage.Rows[i]["SELECT"] = false;
                }

                //秀出待出貨儲位供W/H選取
                this.cmbLocat.Enabled = true;
                ShowLocat();

                ShowOutSourceDataGrid();
                ShowStorageDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            SetbtnSaveProcess();

            DataTable dtSave = new DataTable();
            DataRow[] combineRow;
            DataRow drRow;
            int intCombineLocalTotal = 0;
            int intCombineLocatOut = 0;
            StringBuilder sbCombineIndex = new StringBuilder();
            ArrayList alAllCombine = new ArrayList();

            StringBuilder sbCompareLocatIndex = new StringBuilder();
            ArrayList alCompareLocat = new ArrayList();

            ArrayList aryDwnMatnr = new ArrayList();
            ArrayList aryItmMatnr = new ArrayList();

            try
            {
                stsWarning.Text = "";

                #region 檢查每筆要出庫的庫存是否有選擇待出庫儲位(dtStorage.NLOCA)
                if (dtStorage.Rows.Count > 0)
                {
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        if (dtStorage.Rows[i]["NLOCA"].ToString() == "")
                        {
                            stsWarning.Text = "請先選擇待出貨儲位!!";
                            SetbtnSaveException();
                            return;
                        }
                    }
                }
                #endregion

                #region 檢查出庫的數量是否正確
                for (int i = 0; i < dtOutSource.Rows.Count; i++)
                {
                    if (int.Parse(dtOutSource.Rows[i]["ALQTY"].ToString()) == 0)
                    {
                        stsWarning.Text = "出庫的數量不正確，請確認!!";
                        SetbtnSaveException();
                        return;
                    }

                    if (int.Parse(dtOutSource.Rows[i]["ALQTY"].ToString()) < int.Parse(dtOutSource.Rows[i]["MENGE"].ToString()))
                    {
                        stsWarning.Text = "出庫的數量不正確，請確認!!";
                        SetbtnSaveException();
                        return;
                    }

                    if (aryDwnMatnr.IndexOf(dtOutSource.Rows[i]["MATNR"].ToString().Trim()) == -1)
                    {
                        aryDwnMatnr.Add(dtOutSource.Rows[i]["MATNR"].ToString().Trim());
                    }
                }
                #endregion

                #region 檢查出庫的料號是否與單據的料號一致
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    if (aryItmMatnr.IndexOf(dtStorage.Rows[i]["MATNR"].ToString().Trim()) == -1)
                    {
                        aryItmMatnr.Add(dtStorage.Rows[i]["MATNR"].ToString().Trim());
                    }
                }

                if (aryDwnMatnr.Count != aryItmMatnr.Count)
                {
                    stsWarning.Text = "出庫的料號與單據不一致，請確認!!";
                    SetbtnSaveException();
                    return;
                }
                #endregion

                #region 將同待出貨儲位同料號加總

                dtSave = dtStorage.Clone();
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    #region 每次比對的Index (sbCombineIndex)
                    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                    sbCombineIndex.Append("MANDT='" + dtStorage.Rows[i]["MANDT"].ToString() + "'");
                    sbCombineIndex.Append(" and COMCD='" + dtStorage.Rows[i]["COMCD"].ToString().Trim() + "'");
                    sbCombineIndex.Append(" and WERKS='" + dtStorage.Rows[i]["WERKS"].ToString().Trim() + "'");
                    sbCombineIndex.Append(" and LGORT='" + dtStorage.Rows[i]["LGORT"].ToString().Trim() + "'");
                    sbCombineIndex.Append(" and NLOCA='" + dtStorage.Rows[i]["NLOCA"].ToString().Trim() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtStorage.Rows[i]["MATNR"].ToString().Trim() + "'");
                    sbCombineIndex.Append(" and INSMK='" + dtStorage.Rows[i]["INSMK"].ToString().Trim() + "'");
                    sbCombineIndex.Append(" and CHARG='" + dtStorage.Rows[i]["CHARG"].ToString().Trim() + "'");
                    #endregion

                    if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                    {
                        intCombineLocalTotal = 0;
                        intCombineLocatOut = 0;
                        alAllCombine.Add(sbCombineIndex.ToString());

                        combineRow = dtStorage.Select(sbCombineIndex.ToString());
                        for (int j = 0; j < combineRow.Length; j++)
                        {
                            intCombineLocalTotal += Int32.Parse(combineRow[j]["MENGE"].ToString());
                            intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                        }

                        drRow = dtSave.NewRow();
                        drRow["MANDT"] = dtStorage.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtStorage.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtStorage.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = dtStorage.Rows[i]["LGORT"].ToString();
                        drRow["LOCAT"] = dtStorage.Rows[i]["NLOCA"].ToString();
                        drRow["NLOCA"] = dtStorage.Rows[i]["NLOCA"].ToString();
                        drRow["MATNR"] = dtStorage.Rows[i]["MATNR"].ToString();
                        drRow["INSMK"] = dtStorage.Rows[i]["INSMK"].ToString();
                        drRow["CHARG"] = dtStorage.Rows[i]["CHARG"].ToString();
                        drRow["MTYPE"] = dtStorage.Rows[i]["MTYPE"].ToString();
                        drRow["MENGE"] = intCombineLocalTotal.ToString();
                        drRow["ALQTY"] = intCombineLocatOut.ToString();
                        drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                        drRow["MBLNR"] = dtStorage.Rows[i]["MBLNR"].ToString();
                        drRow["ZEILE"] = dtStorage.Rows[i]["ZEILE"].ToString();
                        drRow["EBELN"] = dtStorage.Rows[i]["EBELN"].ToString();
                        drRow["KDMAT"] = dtStorage.Rows[i]["KDMAT"].ToString();
                        drRow["OMBLNR"] = dtStorage.Rows[i]["OMBLNR"].ToString();
                        drRow["INDAT"] = dtStorage.Rows[i]["INDAT"].ToString();
                        drRow["RMANO"] = dtStorage.Rows[i]["RMANO"].ToString();

                        dtSave.Rows.Add(drRow);
                    }
                }

                #endregion

                #region 檢查相同料號的資料是否都移到相同的待出貨儲位
                for (int i = 0; i < dtSave.Rows.Count; i++)
                {
                    #region 每次比對的Index (sbCompareLocatIndex)
                    sbCompareLocatIndex.Remove(0, sbCompareLocatIndex.Length);
                    sbCompareLocatIndex.Append("MANDT='" + dtSave.Rows[i]["MANDT"].ToString() + "'");
                    sbCompareLocatIndex.Append(" and COMCD='" + dtSave.Rows[i]["COMCD"].ToString() + "'");
                    sbCompareLocatIndex.Append(" and WERKS='" + dtSave.Rows[i]["WERKS"].ToString() + "'");
                    sbCompareLocatIndex.Append(" and LGORT='" + dtSave.Rows[i]["LGORT"].ToString() + "'");
                    sbCompareLocatIndex.Append(" and MATNR='" + dtSave.Rows[i]["MATNR"].ToString() + "'");
                    sbCompareLocatIndex.Append(" and INSMK='" + dtSave.Rows[i]["INSMK"].ToString() + "'");
                    sbCompareLocatIndex.Append(" and CHARG='" + dtSave.Rows[i]["CHARG"].ToString() + "'");
                    #endregion

                    if (alCompareLocat.IndexOf(sbCompareLocatIndex.ToString()) < 0)
                    {
                        alCompareLocat.Add(sbCompareLocatIndex.ToString());
                    }
                    else
                    {
                        stsWarning.Text = "相同的料號必須選擇相同的待出貨儲位!!";
                        SetbtnSaveException();
                        return;
                    }
                }
                #endregion

                #region 檢查是否有相同料號已經存在待出貨儲位中，若有則提示不能再移到相同的待出貨儲位
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    if (objStorageData.QueryDataExistsInStorage_SpareParts(dtStorage.Rows[i]["NLOCA"].ToString(), dtStorage.Rows[i]["MATNR"].ToString()))
                    {
                        stsWarning.Text = dtStorage.Rows[i]["MATNR"].ToString() + " 已經存在相同的待出貨儲位，請選擇不同的待出貨儲位!!";
                        SetbtnSaveException();
                        return;
                    }
                }
                #endregion

                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                if (objStorageOut.AddDOASILineOutData(dtOutSource, dtStorage, dtSave))
                {
                    stsWarning.Text = "Update OK!!";
                    this.btnSave.Enabled = false;
                    this.btnPrint.Enabled = true;

                    #region 增加和ASRS接口
                    bool bolResult = objStorageOut.PostStorageOutDataToASRS(Werks, Lgort, dtStorage, "G-");
                    if (bolResult)
                    {
                        stsWarning.Text = "Update OK!!,数据已同步到ASRS";
                    }
                    #endregion
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
        #endregion

        #region btnPrint_Click
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Order by
            string strOrderBy = "";
            if (rdoMatnr.Checked)
            {
                strOrderBy = "MATNR,LOCAT,INDAT";
            }
            else
            {
                strOrderBy = "LOCAT,MATNR,INDAT";
            }

            DataTable dtPrint = CommonInfo.SortDataTable(dtCombineStorage, strOrderBy);

            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_SPAREPARTS", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.strWerks = "";
            this.strLgort = "";
            this.lblOutSource.Text = "0 records";
            this.lblStorage.Text = "0 records";
            this.txtMblnr.Text = "";
            this.alMblnrs.Clear();
            this.txtMblnr.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.dtOutSource.Clear();
            this.dtStorage.Clear();
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            this.stsWarning.Text = "";
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            this.panel4.Enabled = true;
            this.panel5.Enabled = true;
            this.panel6.Enabled = true;
            this.chkRmano.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.chkSelectAll.Checked = false;
            this.chkClearAll.Checked = false;
            this.rdoLocat.Checked = false;
            this.rdoMatnr.Checked = false;
            this.chkAddIn.Checked = false;
            this.chkRmano.Checked = false;
            this.cmbLocat.Items.Clear();
            this.txtMblnr.Focus();
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region SetbtnSaveProcess()
        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;
        }
        #endregion

        #region SetbtnSaveException()
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }
        #endregion

        #region cmbLocat_SelectedIndexChanged
        private void cmbLocat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowSelectFalse = 0;
            stsWarning.Text = "";
            for (int i = 0; i < dgvStorage.Rows.Count - 1; i++)
            {
                //使用者已勾選資料列，更換待出貨儲位
                if (dgvStorage.Rows[i].Cells[0].Value.ToString() == "True")
                {
                    dtStorage.Rows[i]["NLOCA"] = cmbLocat.Items[cmbLocat.SelectedIndex].ToString();
                }
                else
                {
                    rowSelectFalse++;
                }
            }

            dtStorage.AcceptChanges();

            //使用者沒有勾選任何一筆資料列
            if (rowSelectFalse == dgvStorage.Rows.Count - 1)
            {
                stsWarning.Text = "請先勾選要選擇待出貨儲位的資料列!!";
            }
        }
        #endregion

        #region cmbLgort_SelectedIndexChanged
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            for (int i = 0; i < dtOutSource.Rows.Count; i++)
            {
                dtOutSource.Rows[i]["LGORT"] = strLgort;
            }
            dtOutSource.AcceptChanges();
        }
        #endregion

        #region ShowLocat
        private void ShowLocat()
        {
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                DataTable dtTemp1 = new DataTable();
                cmbLocat.Items.Clear();
                dtTemp1 = objPlantData.GetShipmentLocation(Werks, Lgort, this.chkAddIn.Checked);
                for (int i = 0; i < dtTemp1.Rows.Count; i++)
                {
                    cmbLocat.Items.Add(dtTemp1.Rows[i]["LOCAT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowLocat()");
            }
        }
        #endregion

        #region btnConfirm_Click
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

                //廠區不為空
                if (Werks == "")
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
                dtOutSource = objSapData.QuerySinoOutData(Mblnrs);

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
        #endregion

        #region chkSelectAll_CheckedChanged
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            chkClearAll.Checked = false;
        }
        #endregion

        #region chkSelectAll_CheckStateChanged
        private void chkSelectAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                for (int i = 0; i < dgvStorage.Rows.Count - 1; i++)
                {
                    dtStorage.Rows[i]["SELECT"] = true;
                }

                dtStorage.AcceptChanges();
            }
            else if (chkSelectAll.Checked == false)
            {
                for (int i = 0; i < dgvStorage.Rows.Count - 1; i++)
                {
                    dtStorage.Rows[i]["SELECT"] = false;
                }

                dtStorage.AcceptChanges();
            }
        }
        #endregion

        #region chkClearAll_CheckedChanged
        private void chkClearAll_CheckedChanged(object sender, EventArgs e)
        {
            chkSelectAll.Checked = false;
            for (int i = 0; i < dgvStorage.Rows.Count - 1; i++)
            {
                dtStorage.Rows[i]["SELECT"] = false;
            }

            dtStorage.AcceptChanges();
        }
        #endregion

        #region chkAddIn_CheckedChanged
        private void chkAddIn_CheckedChanged(object sender, EventArgs e)
        {
            ShowLocat();
        }
        #endregion
    }
}
