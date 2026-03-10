using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;

namespace QWMS
{
    public partial class TransferOut_SemiProduct_Query : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";

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

        #endregion
        #region 建構式
        public TransferOut_SemiProduct_Query(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

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
                lblOutSource.Text = dtOutSource.Rows.Count.ToString() + " record(s)";

                if (dtOutSource.Rows.Count > 0)
                {
                    //this.panel1.Enabled = false;
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
                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLocat);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMatnr);

                //KDMAT
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.Width = 90;
                dgvcKdmat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcKdmat);

                //INSMK
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 90;
                dgvcInsmk.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcInsmk);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcCharg);

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
                dgvcBlace.HeaderText = "Blance Qty";
                dgvcBlace.Width = 80;
                dgvcBlace.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcBlace);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcZeile);

                //EBELN
                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.Width = 90;
                dgvcEbeln.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcEbeln);

                //LIFNR
                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.Width = 90;
                dgvcLifnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLifnr);

                //RMANO
                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No#";
                dgvcRmano.Width = 90;
                dgvcRmano.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcRmano);

                //OMBLNR
                DataGridViewTextBoxColumn dgvcOmblnr = new DataGridViewTextBoxColumn();
                dgvcOmblnr.DataPropertyName = "OMBLNR";
                dgvcOmblnr.HeaderText = "Store In Docu. No";
                dgvcOmblnr.Width = 100;
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

                //PKDAT: 棧板滿板時間
                DataGridViewTextBoxColumn dgvcPkdat = new DataGridViewTextBoxColumn();
                dgvcPkdat.DataPropertyName = "PKDAT";
                dgvcPkdat.HeaderText = "Pallet Time";
                dgvcPkdat.ReadOnly = true;
                dgvcPkdat.Width = 140;
                dgvStorage.Columns.Add(dgvcPkdat);

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
                lblStorage.Text = dtCombineStorage.Rows.Count.ToString() + " record(s)";

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
                dtOutSource = objSapData.QuerySapLineOutData(Mblnrs, "ONLINE");

                #region 透過Send id 取得WHSID.SEQNO  Smose Liao 20100426
                string strSeqno = "";
                string strArbpl = "";
                DataColumn cSequence = new DataColumn("SEQNO", typeof(string));
                dtOutSource.Columns.Add(cSequence);
                for (int i = 0; i < dtOutSource.Rows.Count; i++)
                {
                    if (dtOutSource.Rows[i]["REFID"].ToString() != "")
                    {
                        strArbpl = dtOutSource.Rows[i]["ARBPL"].ToString();
                        //若線別為空，則不取Sequence No.
                        if (strArbpl == "")
                        {
                            strSeqno = "";
                        }
                        else
                        {
                            strSeqno = objSapData.QuerySapSeqno(dtOutSource.Rows[i]["WERKS"].ToString(), dtOutSource.Rows[i]["REFID"].ToString(), dtOutSource.Rows[i]["MATNR"].ToString(), strArbpl.Substring(0, 1));
                            dtOutSource.Rows[i]["SEQNO"] = strSeqno;
                        }
                    }
                }
                #endregion

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
        #region txtMblnr_KeyPress
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

                TransferIn_SapDataSelect objStorageOut_SapDataSelect
                    = new TransferIn_SapDataSelect(UserData, Werks, Lgort, Progid, Mblnrs, "TRANSFEROUT");
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
                        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString());

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
        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            SetbtnSaveProcess();

            DataTable dtTempData = new DataTable();
            try
            {

                stsWarning.Text = "";
                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                //SapData objSapData = new SapData();
                SapData objSapData = new SapData(UserData, Werks, Lgort);
                if (dgvStorage.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvStorage.Rows.Count; i++)
                    {
                        DataTable dtSap = new DataTable();
                        dtSap = objSapData.GetBoxIDStorageDataAlter(dgvStorage.Rows[i].Cells[0].Value.ToString().Trim(), dgvStorage.Rows[i].Cells[1].Value.ToString().Trim());
                        if (dtSap.Rows.Count > 0)
                        {
                            MessageBox.Show("Location:" + dgvStorage.Rows[i].Cells[0].Value.ToString().Trim() + " Material No:" + dgvStorage.Rows[i].Cells[1].Value.ToString().Trim() + " 在库已超过5天，不能出库！");
                            return;
                        }
                    }
                }

                if (objStorageOut.AddSemiProductOutData(dtOutSource, dtStorage, "", ""))
                {
                    stsWarning.Text = "Update OK!!";
                    this.btnSave.Enabled = false;
                    this.btnPrint.Enabled = true;


                    #region 列印資料
                    if (chkPrint.Checked)//列印資料
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

                        ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT", dtPrint);
                        objReportPrint.Report.PrintToPrinter(1, true, 0, 0);
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
            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
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
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            //this.panel1.Enabled = true;
            this.rdoLocat.Checked = false;
            this.rdoMatnr.Checked = false;
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
            this.btnSave.Enabled = false;
        }
        #endregion
        #region SetbtnSaveException
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }
        #endregion
    }
}