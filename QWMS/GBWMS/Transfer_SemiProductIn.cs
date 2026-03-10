using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using NPOI.DDF;
using NPOI.SS.Formula.Functions;
using QWMS.Common;
using QCI.QWMS;
using System.Text;

namespace QWMS
{
    public partial class Transfer_SemiProcuctIn : Form
    {
        #region 變數宣告

        private UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strType = "";
        private Int32 mengCount = 0;
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

        #endregion

        #region 建構式

        public Transfer_SemiProcuctIn(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageIn StorageIn = new StorageIn(UserData, strProgid);
                //   QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageIn.CheckAuthority())
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
            dgvOutSource.AllowUserToAddRows = false;
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
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
                dgvcInsmk.Width = 90;
                dgvcInsmk.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcInsmk);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 90;
                dgvcCharg.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCharg);

                //LIFNR
                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.Width = 90;
                dgvcLifnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcLifnr);

                //RMANO
                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No#";
                dgvcRmano.Width = 90;
                dgvcRmano.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcRmano);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Store in Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcAlqty);

                //KOSTL
                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.Width = 90;
                dgvcKostl.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcKostl);

                //ARBPL
                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.Width = 90;
                dgvcArbpl.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcArbpl);

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
                    this.panel1.Enabled = false;

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
            dgvStorage.RowTemplate.Height = 24;
            dgvStorage.ReadOnly = true;
            dgvStorage.AllowUserToAddRows = false;
            try
            {
                DataGridViewCheckBoxColumn dgvcChecked = new DataGridViewCheckBoxColumn();
                dgvcChecked.DataPropertyName = "CHKED";
                dgvcChecked.HeaderText = "Select";
                dgvcChecked.ReadOnly = true;
                dgvcChecked.Width = 60;
                this.dgvStorage.Columns.Add(dgvcChecked);


                //MBLNR
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 90;
                dgvcWerks.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcWerks);

              //MATNR
                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Sotrage";
                dgvcLGORT.Width = 90;
                dgvcLGORT.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLGORT);
                //MBLNR
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "Transfer No.";
                dgvcMBLNR.Width = 120;
                dgvcMBLNR.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMBLNR);

                //MBLNR
                DataGridViewTextBoxColumn dgvcPALLETID = new DataGridViewTextBoxColumn();
                dgvcPALLETID.DataPropertyName = "PLTID";
                dgvcPALLETID.HeaderText = "Pallet ID";
                dgvcPALLETID.Width = 120;
                dgvcPALLETID.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcPALLETID);

                //Boxid
                DataGridViewTextBoxColumn dgvcBoxid = new DataGridViewTextBoxColumn();
                dgvcBoxid.DataPropertyName = "BOXID";
                dgvcBoxid.HeaderText = "Box id";
                dgvcBoxid.Width = 140;
                dgvcBoxid.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcBoxid);


                //OMBLNR
                DataGridViewTextBoxColumn dgvcOmblnr = new DataGridViewTextBoxColumn();
                dgvcOmblnr.DataPropertyName = "SERNO";
                dgvcOmblnr.HeaderText = "Serial No";
                dgvcOmblnr.Width = 120;
                dgvcOmblnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcOmblnr);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcCharg);
                //MENGE
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "Part No";
                dgvcMATNR.Width = 120;
                dgvcMATNR.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMATNR);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "StorageIn Qty";
                dgvcMenge.Width = 100;
                dgvcMenge.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMenge);



                //MENGE
                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "Status";
                dgvcINSMK.Width = 80;
                dgvcINSMK.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcINSMK);



                dgvStorage.DataSource = dtStorage;
                lblStorage.Text = @"0/" + mengCount.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }

        #endregion

        #region cmbWerks_SelectedIndexChanged

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        #endregion

        #region btnConfirm_Click

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                string[] aryTempMblnr = txtMblnr.Text.Trim().Split(new char[] {','});
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
                if (Mblnrs.Count > 1)
                {
                    stsWarning.Text = "一次只能选择一条单据";
                    return;
                }

                dtOutSource = objSapData.QuerySapLineOutDataCSMCTransfer(Mblnrs, "TRANSFERIN");

                if (dtOutSource.Rows.Count == 0)
                {
                    stsWarning.Text = "无数据!!";
                    return;
                }

                dtStorage = objSapData.QueryWHTRATransfer_GB(Mblnrs, dtOutSource.Rows[0]["MATNR"].ToString(), dtOutSource.Rows[0]["CHARG"].ToString());

                mengCount = 0;
                foreach (DataRow dr in dtStorage.Rows)
                {
                    mengCount += Convert.ToInt32(dr["MENGE"]);
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

        #endregion

        #region txtMblnr_KeyPress

        private void txtMblnr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
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

        #endregion

        #region txtMblnr_DoubleClick

        private void txtMblnr_DoubleClick(object sender, EventArgs e)
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

                TransferIn_SapDataSelect objStorageOut_SapDataSelect = new TransferIn_SapDataSelect(UserData, Werks,
                    Lgort, Progid, Mblnrs, "TRANSFERIN");
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

        #region btnQuery_Click

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                #region 變數宣告

                stsWarning.Text = "";
                DataRow[] foundRow;
                DataRow[] combineRow;
                DataRow drRow;
                string strOrderBy = "";
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();
                DataTable dtTempStorage = new DataTable();
                DataSet dsData = new DataSet();

                #endregion

                if (strOrderBy == "")
                {
                    strOrderBy = "LOCAT, MATNR, INDAT";
                }

                StorageData objStorageData = new StorageData(UserData, Werks, Lgort);
                dsData = objStorageData.QueryOnLineOutData(dtOutSource, "", "", false, "", false, false);
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
                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtTempStorage.Rows[i]["LOCAT"].ToString(),
                            dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(),
                            dtTempStorage.Rows[i]["CHARG"].ToString());

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
                        drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                        drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                        drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                        drRow["LIFNR"] = "";
                        drRow["RMANO"] = "";
                        drRow["OMBLNR"] = "";
                        drRow["MRGID"] = "";
                        drRow["KOSTL"] = "";
                        drRow["ARBPL"] = "";
                        drRow["TRNTP"] = "";
                        drRow["RMAK1"] = "";
                        drRow["INDAT"] = "";
                        drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                        drRow["SERNO"] = "";
                        drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                        drRow["LOCOD"] = dtTempStorage.Rows[i]["LOCOD"].ToString();
                        drRow["INSPT"] = dtTempStorage.Rows[i]["INSPT"].ToString();
                        drRow["PKDAT"] = dtTempStorage.Rows[i]["PKDAT"].ToString();
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
                        drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                        drRow["SERNO"] = foundRow[i]["SERNO"].ToString();
                        drRow["DACOD"] = foundRow[i]["DACOD"].ToString();
                        drRow["LOCOD"] = foundRow[i]["LOCOD"].ToString();
                        drRow["INSPT"] = foundRow[i]["INSPT"].ToString();
                        drRow["PKDAT"] = dtTempStorage.Rows[i]["PKDAT"].ToString();
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

        #endregion

        #region btnRefresh_Click

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.strWerks = "";
            this.strLgort = "";
            this.txtMblnr.Text = "";
            this.alMblnrs.Clear();
            this.txtMblnr.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.dtOutSource.Clear();
            this.dtStorage.Clear();
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            this.stsWarning.Text = "";
            arrBox = new ArrayList();
            arrSN = new ArrayList();
            dtOutSource = null;
            dtStorage = null;
            dtCombineStorage = null;
            btnAdd.Enabled = true;

            // this.panel1.Enabled = true;
            //this.rdoLocat.Checked = false;
            //this.rdoMatnr.Checked = false;
        }

        #endregion

        #region btnExit_Click

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region SetbtnSaveProcess

        private void SetbtnSaveProcess()
        {
            // this.btnSave.Enabled = false;
        }

        #endregion

        #region SetbtnSaveException

        private void SetbtnSaveException()
        {
            // this.btnSave.Enabled = true;
        }

        #endregion

        private ArrayList arrSN = new ArrayList();
        private ArrayList arrBox = new ArrayList();

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char) 13)
            {
                //if (txtCheck.Checked)
                //{

                //}
                //else
                //{
                StorageIn objsi = new StorageIn(UserData, strProgid);
                if (txtLocation.Text.Trim() == "")
                {
                    MessageBox.Show("请输入储位！");
                    return;
                }
                else
                {
                    if (txtLocation.Text.Trim().Length != 6)
                    {
                        MessageBox.Show("请检查储位输入是否正确！");
                        return;
                    }
                    if (dgvOutSource.Rows.Count == 0)
                    {
                        MessageBox.Show("请输入101SAP扣帐编号！");
                        return;
                    }

                    if (textBox1.Text.Trim() != "")
                    {
                        int count = 0;
                        foreach (DataGridViewRow dataGridRow in dgvStorage.Rows)
                        {
                            if (dataGridRow.Cells[0].Value != null)
                            {
                                if (textBox1.Text.Trim().ToUpper() == dataGridRow.Cells[5].Value.ToString().ToUpper())
                                   
                                {

                                    if (dataGridRow.Cells[0].Value.ToString() == "True")
                                    {
                                        MessageBox.Show("BOX ID 已經掃過!!");
                                        textBox1.Text = "";
                                        return;
                                    }
                                    else
                                    {
                                        dataGridRow.Cells[0].Value = true;
                                    }
                                }
                                if (dataGridRow.Cells[0].Value.ToString().ToLower() == "true")
                                {
                                    count +=Convert.ToInt32( dataGridRow.Cells[9].Value);
                                }
                            }
                        }
                        //   ShowStorageDataGrid();
                        lblStorage.Text = count.ToString() + "/" + mengCount.ToString() + " records";
                        textBox1.Text = "";
                    }
                }
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false; 
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType,
                CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType,
                CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

            if (string.IsNullOrEmpty(txtLocation.Text.ToString().Trim()))
            {
                MessageBox.Show("未选择Location！");
                btnAdd.Enabled = true;
                return;
            }
            else
            {
                DataTable dtlocation = new DataTable();
                dtlocation = objStorageIn.QuerywhitmbyLocat(dtOutSource.Rows[0]["WERKS"].ToString(),
                    dtOutSource.Rows[0]["LGORT"].ToString().Trim(), txtLocation.Text.Trim());
                if (dtlocation.Rows.Count > 0)
                {
                    for (int i = 0; i < dtlocation.Rows.Count; i++)
                    {
                        if (dtOutSource.Rows.Count > 0)
                        {
                            if (dtlocation.Rows[i]["MATNR"].ToString().Trim() != dtOutSource.Rows[0]["MATNR"].ToString() ||
                                dtlocation.Rows[i]["CHARG"].ToString().Trim() != dtOutSource.Rows[0]["CHARG"].ToString())
                            {
                                stsWarning.Text = "该储位中已经存在不同的料号！";
                                btnAdd.Enabled = true;
                                return;
                            }
                        }
                    }
                }
            }

            DataTable dtWhtra = new DataTable();
            dtWhtra = GetDgvToTable(dgvStorage);
            decimal counttemp = 0;
            if (dtWhtra.Rows.Count <= 0)
            {
                MessageBox.Show("未找到入库BOXID");
                btnAdd.Enabled = true;
                return;
            }
            foreach (DataRow  dr in dtWhtra.Rows)
            {
                counttemp += Convert.ToInt32(dr["MENGE"]);
            }

            if ((Convert.ToInt32(dgvOutSource.Rows[0].Cells[8].Value.ToString()) - counttemp) < 0)
            {
                MessageBox.Show("入库数量大于SAP单据数量，不能入库！");
                btnAdd.Enabled = true;
                return;
            }
            DataTable dtIn = new DataTable();
            StorageIn objsi = new StorageIn(UserData, strProgid);

            if (dtOutSource.Rows.Count != 1)
            {
                MessageBox.Show("SAP单据异常：出现多条明细！");
                btnAdd.Enabled = true;
                return;
            }

            #region Add By Michael 20150603 for 调拨入库Log
            foreach (DataRow row in dtOutSource.Rows)
            {
                row["LOCAT"] = txtLocation.Text.Trim();
            }
            dtOutSource.AcceptChanges();    
            #endregion

            if (objStorageIn.AddSemiProdTransferInData_GB(dtOutSource, dtWhtra, txtLocation.Text.Trim()))
            {
                btnConfirm_Click(null, null);
                stsWarning.Text = "Add OK!!";
                arrSN = new ArrayList();
                arrBox = new ArrayList();
                btnAdd.Enabled = false;
            }
            else
            {

                btnAdd.Enabled = true;
            }
        }

        private ArrayList arrserno = new ArrayList();

        private void txtSerno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                //if (txtCheck.Checked)
                //{
                //    if (textBox1.Text.Trim() == "")
                //    {
                //        MessageBox.Show("请输入Box ID！");
                //        return;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("请勾选扫描SN！");
                //    return;
                //}

                StorageIn objsi = new StorageIn(UserData, strProgid);
                if (txtLocation.Text.Trim() == "")
                {
                    MessageBox.Show("请输入储位！");
                    return;
                }
                else
                {
                    if (dgvOutSource.Rows.Count == 0)
                    {
                        MessageBox.Show("请输入101SAP扣帐编号！");
                        return;
                    }

                    if (txtSerno.Text.Trim() != "")
                    {
                        int count = 0;
                        foreach (DataGridViewRow dataGridRow in dgvStorage.Rows)
                        {
                            if (dataGridRow.Cells[0].Value != null)
                            {
                                if (txtSerno.Text.Trim().ToUpper() == dataGridRow.Cells[6].Value.ToString().ToUpper())
                                {

                                    if (dataGridRow.Cells[0].Value.ToString() == "True")
                                    {
                                        MessageBox.Show( "SN ID 已經掃過!!");
                                        return;
                                    }
                                    else
                                    {
                                        dataGridRow.Cells[0].Value = true;
                                    }
                                }
                                if (dataGridRow.Cells[0].Value.ToString().ToLower() == "true")
                                {
                                  count += Convert.ToInt32(dataGridRow.Cells[9].Value); ;
                                }
                            }

                        }
                        // ShowStorageDataGrid();
                        lblStorage.Text = count.ToString() + "/" + mengCount.ToString() + " records";
                        txtSerno.Text = "";

                    }
                }
            }
        }

        private DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            //for (int count = 0; count < dgv.Columns.Count; count++)
            //{
            //    DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
            //    dt.Columns.Add(dc);
            //}'
            dt = dtStorage.Clone();
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                if (dgv.Rows[count].Cells[0].Value != null)
                {
                    if (dgv.Rows[count].Cells[0].Value.ToString().ToUpper().Contains("TRUE"))
                    {
                        DataRow dr = dt.NewRow();
                        dr["MANDT"] = "218";
                        dr["COMCD"] = Comcd;
                        dr["CHKED"] = Convert.ToString(dgv.Rows[count].Cells[0].Value);
                        dr["WERKS"] = Convert.ToString(dgv.Rows[count].Cells[1].Value);  
                        dr["LGORT"] = Convert.ToString(dgv.Rows[count].Cells[2].Value);
                        dr["MBLNR"] = Convert.ToString(dgv.Rows[count].Cells[3].Value);
                        dr["PLTID"] = Convert.ToString(dgv.Rows[count].Cells[4].Value);
                        dr["BOXID"] = Convert.ToString(dgv.Rows[count].Cells[5].Value);
                        dr["SERNO"] = Convert.ToString(dgv.Rows[count].Cells[6].Value);
                        dr["CHARG"] = Convert.ToString(dgv.Rows[count].Cells[7].Value);
                        dr["MATNR"] = Convert.ToString(dgv.Rows[count].Cells[8].Value);
                        dr["MENGE"] = Convert.ToString(dgv.Rows[count].Cells[9].Value);
                        dr["INSMK"] = Convert.ToString(dgv.Rows[count].Cells[10].Value);
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        private void ckbScam_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            if (ckbScam.Checked)
            {
                foreach (DataGridViewRow dataGridRow in dgvStorage.Rows)
                {
                    dataGridRow.Cells[0].Value = true;
                    count++;
                }
                //   ShowStorageDataGrid();
                lblStorage.Text = count.ToString() + "/" + dgvStorage.Rows.Count.ToString() + " records";
                textBox1.Text = "";
            }
            else
            {
                foreach (DataGridViewRow dataGridRow in dgvStorage.Rows)
                {
                    dataGridRow.Cells[0].Value = false;
                }
                //   ShowStorageDataGrid();
                lblStorage.Text = count.ToString() + "/" + dgvStorage.Rows.Count.ToString() + " records";
                textBox1.Text = "";
            }
        }

        private void txtLocation_DoubleClick(object sender, EventArgs e)
        {
            try
            {
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
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
                    objStorageIn_LocationSelect.ShowDialog();
                    txtLocation.Text = objStorageIn_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            txtLocation.Text = "";
            strType = "ADD";
            Type = strType;
            this.txtMblnr.Text = "";
            this.alMblnrs.Clear();
            this.txtMblnr.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            arrBox = new ArrayList();
            arrSN = new ArrayList();
            dtOutSource = null;
            dtStorage = null;
            dtCombineStorage = null;
            btnAdd.Enabled = true;
        }

        private void rdoNew_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            txtLocation.Text = "";
            strType = "NEW";
            Type = strType;
            this.txtMblnr.Text = "";
            this.alMblnrs.Clear();
            this.txtMblnr.Enabled = true;
            this.btnConfirm.Enabled = true;
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            arrBox = new ArrayList();
            arrSN = new ArrayList();
            dtOutSource = null;
            dtStorage = null;
            dtCombineStorage = null;
            btnAdd.Enabled = true;
        }
    }
}
