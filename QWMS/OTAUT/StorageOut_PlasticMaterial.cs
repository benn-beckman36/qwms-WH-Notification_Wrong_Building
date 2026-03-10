using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using NPOI.SS.Formula.Functions;
using QCI.QWMS;
using QWMS.Common;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace QWMS
{
    public partial class StorageOut_PlasticMaterial : Form
    {
        private string strMandt = "";
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
        private string strComcd = "";
        private ArrayList aryMblnr = new ArrayList();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtMatnrSource = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private ArrayList aryMixedMaterial = new ArrayList();
        //private MixedMaterial objMixedMaterial;
        UserInfo UserData = new UserInfo();
        private DataTable dtData = new DataTable();
        private string strType = "";
        string strAllCombine = "";
        string strTempPalid = "";
        int intBlaceQty = 0;

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

        public StorageOut_PlasticMaterial(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                //檢查權限
                if (!objStorageOut.CheckAuthority())
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
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void ShowPrintCheckBox()
        {
            bool bolPrint = false;
            try
            {
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                bolPrint = objStorageOut.CheckPrintCheckBox();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowPrintCheckBox()");
            }
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

        private void ShowOutSourceDataGridView()
        {
            this.dgvOutSource.AutoGenerateColumns = false;
            this.dgvOutSource.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 120;
                dgvcMatnr.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMatnr);

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

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                dgvcMblnr.Width = 150;
                dgvcMblnr.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.Width = 90;
                dgvcIndat.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcIndat);

                dgvOutSource.DataSource = dtOutSource;
                lblOutSource.Text = dtOutSource.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }

        private void ShowStorageDataGridView()
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

                DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                dgvcMrgid.DataPropertyName = "MRGID";
                dgvcMrgid.HeaderText = "Mixed Material No";
                dgvcMrgid.Width = 100;
                dgvcMrgid.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMrgid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcZeile);

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
                dgvcTrntp.HeaderText = "Store In Docu. No";
                dgvcTrntp.Width = 100;
                dgvcTrntp.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcdatecode = new DataGridViewTextBoxColumn();
                dgvcdatecode.DataPropertyName = "DACOD";
                dgvcdatecode.HeaderText = "DateCode";
                dgvcdatecode.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcdatecode);

                DataGridViewTextBoxColumn dgvcrmak1 = new DataGridViewTextBoxColumn();
                dgvcrmak1.DataPropertyName = "RMAK1";
                dgvcrmak1.HeaderText = "Remark";
                dgvcrmak1.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcrmak1);

                dgvStorage.DataSource = dtCombineStorage;
                lblStorage.Text = dtCombineStorage.Rows.Count.ToString() + " records";

                if (dtCombineStorage.Rows.Count > 0)
                {
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }

        private void btnQuery_Click(object sender, System.EventArgs e)
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
                aryMixedMaterial.Clear();
                DataSet dsData = new DataSet();
                DataTable dtTempStorage = new DataTable();
                string strMatnr = txtMatnr.Text.Trim();
                string strMenge = txtQty.Text.Trim();
                string strPalid = txtPALID.Text.Trim();
                string strLocat = txtLocat.Text.Trim();
                
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
                //Order by

                strOrderBy = "LOCAT, MATNR,INDAT";

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                if (rdbInteger.Checked)
                {
                    if (strTempPalid.IndexOf(strPalid) == 0)
                    {
                        stsWarning.Text = "重复刷入Pallet ID!!";
                        return;
                    }
                    else
                    {
                        strTempPalid += strPalid + ";";
                    }
                    dsData = objStorageData.QueryPlasticMaterialOutData(strPalid, dtOutSource);

                    dtOutSource = dsData.Tables[0].Copy();
                    dtTempStorage = dsData.Tables[1].Copy();
                    
                    dtTempStorage.Columns.Add("BLACE");

                    int intCombineLocalTotal = 0;
                    int intCombineLocatOut = 0;
                    if (dtCombineStorage.Rows.Count == 0)
                    {
                        dtCombineStorage = dtTempStorage.Clone();
                    }
                    for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                    {
                        strTempCombine = dtTempStorage.Rows[i]["MANDT"].ToString() + dtTempStorage.Rows[i]["COMCD"].ToString() + dtTempStorage.Rows[i]["WERKS"].ToString() + dtTempStorage.Rows[i]["LGORT"].ToString() + dtTempStorage.Rows[i]["LOCAT"].ToString() + dtTempStorage.Rows[i]["MATNR"].ToString() + dtTempStorage.Rows[i]["INSMK"].ToString() + dtTempStorage.Rows[i]["CHARG"].ToString() + ";";
                        if (strAllCombine.IndexOf(strTempCombine) < 0)
                        {
                            intCombineLocatOut = 0;
                            strAllCombine += strTempCombine;
                            combineRow = dtTempStorage.Select("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "' and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'  and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "' and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                            }
                            intCombineLocalTotal = objStorageData.QueryMatnrQty_DateCode(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString(),"");
                            intBlaceQty = intCombineLocalTotal - intCombineLocatOut;
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
                            drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                            drRow["OMBLNR"] = "";
                            drRow["MRGID"] = "";
                            drRow["SERNO"] = dtTempStorage.Rows[i]["SERNO"].ToString();
                            drRow["KOSTL"] = "";
                            drRow["ARBPL"] = "";
                            drRow["TRNTP"] = "";
                            drRow["RMAK1"] = "";
                            drRow["INDAT"] = dtTempStorage.Rows[i]["INDAT"].ToString();
                            //20051020 MARC 新增客人料號欄位 KDMAT
                            drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                            drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                            dtCombineStorage.Rows.Add(drRow);
                        }
                        else
                        {
                            intCombineLocatOut = 0;
                            combineRow = dtTempStorage.Select("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "' and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'  and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "' and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                            }
                            
                            drRow = dtCombineStorage.NewRow();
                            drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                            drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                            drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                            drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                            drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                            drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                            drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                            drRow["MENGE"] = intBlaceQty.ToString();
                            drRow["ALQTY"] = intCombineLocatOut.ToString();
                            drRow["BLACE"] = Convert.ToString(intBlaceQty - intCombineLocatOut);
                            drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                            drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                            drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                            drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                            drRow["OMBLNR"] = "";
                            drRow["MRGID"] = "";
                            drRow["SERNO"] = dtTempStorage.Rows[i]["SERNO"].ToString();
                            drRow["KOSTL"] = "";
                            drRow["ARBPL"] = "";
                            drRow["TRNTP"] = "";
                            drRow["RMAK1"] = "";
                            drRow["INDAT"] = dtTempStorage.Rows[i]["INDAT"].ToString();
                            //20051020 MARC 新增客人料號欄位 KDMAT
                            drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                            drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                            dtCombineStorage.Rows.Add(drRow);
                            intBlaceQty = intBlaceQty - intCombineLocatOut;
                        }
                    }
                    if (dtStorage.Rows.Count == 0)
                    {
                        dtStorage = dtTempStorage.Clone();
                    }
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
                            drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                            drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                            drRow["SERNO"] = foundRow[i]["SERNO"].ToString();
                            drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                            drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                            drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                            drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                            drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                            //20051020 MARC 新增客人料號 KDMAT
                            drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                            drRow["BLACE"] = Convert.ToInt32(foundRow[i]["MENGE"].ToString())-Convert.ToInt32(foundRow[i]["ALQTY"].ToString());
                            drRow["DACOD"] = foundRow[i]["DACOD"].ToString();
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
                else if(rdbScatter.Checked)
                {
                    if (strMenge == "")
                    {
                        stsWarning.Text = "Qty can't be empty!!";
                        return;
                    }
                    if (strLocat == "")
                    {
                        stsWarning.Text = "Location can't be empty!!";
                        return;
                    }
                    foundRow = dtOutSource.Select("MATNR='" + strMatnr + "' AND LOCAT='" + strLocat + "' AND ALQTY=''");
                    dsData = objStorageData.QueryPlasticMaterialOutData(foundRow[0]["MATNR"].ToString(), strMenge, foundRow[0]["LOCAT"].ToString(), foundRow[0]["MBLNR"].ToString(), dtOutSource);
                    dtOutSource = dsData.Tables[0].Copy();
                    dtTempStorage = dsData.Tables[1].Copy();
                    
                    dtTempStorage.Columns.Add("BLACE");

                    int intCombineLocalTotal = 0;
                    int intCombineLocatOut = 0;
                    if (dtCombineStorage.Rows.Count == 0)
                    {
                        dtCombineStorage = dtTempStorage.Clone();
                    }
                    for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                    {
                        strTempCombine = dtTempStorage.Rows[i]["MANDT"].ToString() + dtTempStorage.Rows[i]["COMCD"].ToString() + dtTempStorage.Rows[i]["WERKS"].ToString() + dtTempStorage.Rows[i]["LGORT"].ToString() + dtTempStorage.Rows[i]["LOCAT"].ToString() + dtTempStorage.Rows[i]["MATNR"].ToString() + dtTempStorage.Rows[i]["INSMK"].ToString() + dtTempStorage.Rows[i]["CHARG"].ToString() + ";";
                        if (strAllCombine.IndexOf(strTempCombine) < 0)
                        {
                            intCombineLocatOut = 0;
                            strAllCombine += strTempCombine;
                            combineRow = dtTempStorage.Select("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "' and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'  and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "' and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                            }
                            intCombineLocalTotal = objStorageData.QueryMatnrQty_DateCode(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString(), "");
                            intBlaceQty = intCombineLocalTotal - intCombineLocatOut;
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
                            drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                            drRow["OMBLNR"] = "";
                            drRow["MRGID"] = "";
                            drRow["SERNO"] = dtTempStorage.Rows[i]["SERNO"].ToString();
                            drRow["KOSTL"] = "";
                            drRow["ARBPL"] = "";
                            drRow["TRNTP"] = "";
                            drRow["RMAK1"] = "";
                            drRow["INDAT"] = dtTempStorage.Rows[i]["INDAT"].ToString();
                            //20051020 MARC 新增客人料號欄位 KDMAT
                            drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                            drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                            dtCombineStorage.Rows.Add(drRow);
                        }
                        else
                        {
                            intCombineLocatOut = 0;
                            combineRow = dtTempStorage.Select("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "' and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'  and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "' and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "' and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "' and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "' and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "' and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                            for (int j = 0; j < combineRow.Length; j++)
                            {
                                intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                            }

                            drRow = dtCombineStorage.NewRow();
                            drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                            drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                            drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                            drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                            drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                            drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                            drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                            drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                            drRow["MENGE"] = intBlaceQty.ToString();
                            drRow["ALQTY"] = intCombineLocatOut.ToString();
                            drRow["BLACE"] = Convert.ToString(intBlaceQty - intCombineLocatOut);
                            drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                            drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                            drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                            drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                            drRow["OMBLNR"] = "";
                            drRow["MRGID"] = "";
                            drRow["SERNO"] = dtTempStorage.Rows[i]["SERNO"].ToString();
                            drRow["KOSTL"] = "";
                            drRow["ARBPL"] = "";
                            drRow["TRNTP"] = "";
                            drRow["RMAK1"] = "";
                            drRow["INDAT"] = dtTempStorage.Rows[i]["INDAT"].ToString();
                            //20051020 MARC 新增客人料號欄位 KDMAT
                            drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                            drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                            dtCombineStorage.Rows.Add(drRow);
                            intBlaceQty = intBlaceQty - intCombineLocatOut;
                        }
                    }
                    if (dtStorage.Rows.Count == 0)
                    {
                        dtStorage = dtTempStorage.Clone();
                    }
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
                            drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                            drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                            drRow["SERNO"] = foundRow[i]["SERNO"].ToString();
                            drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                            drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                            drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                            drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                            drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                            //20051020 MARC 新增客人料號 KDMAT
                            drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                            drRow["BLACE"] = Convert.ToInt32(foundRow[i]["MENGE"].ToString()) - Convert.ToInt32(foundRow[i]["ALQTY"].ToString());
                            drRow["DACOD"] = foundRow[i]["DACOD"].ToString();
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
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            SetbtnSaveProcess();
            try
            {
                stsWarning.Text = "";
                string strTempMixedMaterial = "";
                aryMixedMaterial.Clear();
                DataRow[] drRows;
                for (int i = 0; i < dtMatnrSource.Rows.Count; i++)
                {
                    int StorageOutMenge = 0;
                    drRows = dtStorage.Select("MATNR = '" + dtMatnrSource.Rows[i]["MATNR"].ToString() + "'");
                    for (int j = 0; j < drRows.Length; j++)
                    {
                        StorageOutMenge += Convert.ToInt32(drRows[j]["ALQTY"].ToString());
                    }
                    if (Convert.ToInt32(dtMatnrSource.Rows[i]["MENGE"].ToString()) != StorageOutMenge)
                    {
                        stsWarning.Text = "出库数量与需求量不符!!";
                        return;
                    }
                }

                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);

                if (objStorageOut.AddOffLineOutData_DateCode(strIsmrg, dtOutSource, dtStorage, ""))
                {
                    stsWarning.Text = "Update OK!!";
                    this.btnSave.Enabled = false;
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

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            this.strWerks = "";
            this.strLgort = "";
            ShowPrintCheckBox();
            this.rdbInteger.Checked = false;
            this.rdbScatter.Checked = false;
            this.lblOutSource.Text = "0 records";
            this.lblStorage.Text = "0 records";
            this.dtOutSource.Clear();
            this.dtStorage.Clear();
            this.dtCombineStorage.Clear();
            this.dtMatnrSource.Clear();
            this.dgvOutSource.DataSource = null;
            this.dgvStorage.DataSource = null;
            this.stsWarning.Text = "";

            this.btnQuery.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            this.panel1.Enabled = true;
            this.txtSourceData.Text = "";
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.txtPALID.Enabled = false;
            this.txtQty.Enabled = false;
            this.txtSourceData.Enabled = true;
            this.txtMatnr.Enabled = true;
            this.txtLocat.Enabled = false;
            this.btnConfirm.Enabled = true;
            this.txtPALID.Text = "";
            this.txtQty.Text = "";
            this.txtSourceData.Text = "";
            this.txtMatnr.Text = "";
            this.txtLocat.Text = "";
            strAllCombine = "";
            strTempPalid = "";
            intBlaceQty = 0;
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void StorageOut_PlasticMaterial_Resize(object sender, System.EventArgs e)
        {
            panel4.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel4.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            #region Sort Print的資料
            DataTable dtPrint = new DataTable();
            dtPrint = dtOutSource.Clone();
            DataRow drNew;
            DataRow[] drArray;

            drArray = dtOutSource.Select("", "LOCAT,MATNR,INDAT");

            foreach (DataRow dr in drArray)
            {
                drNew = dtPrint.NewRow();

                for (int i = 0; i < dtOutSource.Columns.Count; i++)
                {
                    drNew[i] = dr[i].ToString();
                }
                dtPrint.Rows.Add(drNew);
            }
            #endregion
            //列印實際出庫資料
            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;

        }
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }

        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }

        private void rdbInteger_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPALID.Enabled = true;
            this.txtQty.Enabled = false;
            this.txtLocat.Enabled = false;
        }

        private void rdbScatter_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPALID.Enabled = false;
            this.txtQty.Enabled = true;
            this.txtLocat.Enabled = true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DataRow[] drOutSource;
            DataRow drRow;
            DataTable dtTemp = new DataTable();
            int StorageMenge = 0;

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

            if (dtMatnrSource == null || dtMatnrSource.Rows.Count == 0)
            {
                stsWarning.Text = "Please import data first!!";
                return;
            }
            
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            dtOutSource = objStorageData.QueryPlasticMaterialData(dtMatnrSource);//查询料号库存信息

            if (dtOutSource.Rows.Count == 0)
            {
                stsWarning.Text = "No Data!!";
                return;
            }
            dtTemp = dtOutSource.Clone();

            for (int i = 0; i < dtMatnrSource.Rows.Count; i++)
            {
                drOutSource = dtOutSource.Select(" MATNR='" + dtMatnrSource.Rows[i]["MATNR"].ToString() + "' AND MENGE =" + dtMatnrSource.Rows[i]["MENGE"].ToString() + " ",
                    "MENGE,LOCAT, MATNR,INDAT"); //查询满足需求数量的料号，并按数量从小到大排序
                if (drOutSource.Length > 0) //单个Pallet ID中，若料号库存数量满足需求量只取第一条数据
                {
                    drRow = dtTemp.NewRow();
                    drRow["MANDT"] = drOutSource[0]["MANDT"].ToString();
                    drRow["COMCD"] = drOutSource[0]["COMCD"].ToString();
                    drRow["WERKS"] = drOutSource[0]["WERKS"].ToString();
                    drRow["LGORT"] = drOutSource[0]["LGORT"].ToString();
                    drRow["LOCAT"] = drOutSource[0]["LOCAT"].ToString();
                    drRow["MBLNR"] = drOutSource[0]["MBLNR"].ToString();
                    drRow["ZEILE"] = drOutSource[0]["ZEILE"].ToString();
                    drRow["OMBLNR"] = drOutSource[0]["OMBLNR"].ToString();
                    drRow["MATNR"] = drOutSource[0]["MATNR"].ToString();
                    drRow["INSMK"] = drOutSource[0]["INSMK"].ToString();
                    drRow["CHARG"] = drOutSource[0]["CHARG"].ToString();
                    drRow["MENGE"] = drOutSource[0]["MENGE"].ToString();
                    drRow["ALQTY"] = drOutSource[0]["ALQTY"].ToString();
                    drRow["MRGID"] = drOutSource[0]["MRGID"].ToString();
                    drRow["SERNO"] = drOutSource[0]["SERNO"].ToString();
                    drRow["KOSTL"] = drOutSource[0]["KOSTL"].ToString();
                    drRow["ARBPL"] = drOutSource[0]["ARBPL"].ToString();
                    drRow["TRNTP"] = drOutSource[0]["TRNTP"].ToString();
                    drRow["EBELN"] = drOutSource[0]["EBELN"].ToString();
                    drRow["LIFNR"] = drOutSource[0]["LIFNR"].ToString();
                    drRow["RMAK1"] = drOutSource[0]["RMAK1"].ToString();
                    drRow["INDAT"] = drOutSource[0]["INDAT"].ToString();
                    drRow["KDMAT"] = drOutSource[0]["KDMAT"].ToString();
                    dtTemp.Rows.Add(drRow);
                }
                if (drOutSource.Length == 0) //若单个Pallet ID库存数量不满足
                {
                    //1.多个Pallet ID数量相加正好满足需求数量，取这些Pallet ID数据放入Table
                    drOutSource = dtOutSource.Select(" MATNR='" + dtMatnrSource.Rows[i]["MATNR"].ToString() + "'", "MENGE,LOCAT, MATNR,INDAT");
                    try
                    {
                        //取任意几个值相加等于特定值算法
                        long start = 0;
                        long end = (long)Math.Pow(2, drOutSource.Length);
                        for (; start < end; start++)
                        {
                            int temp = 0;
                            for (int j = 0; j < drOutSource.Length; j++)
                            {
                                if (((start >> j) & 1) == 1)
                                {
                                    temp += Convert.ToInt32(drOutSource[j]["MENGE"].ToString());//累加
                                }
                            }
                            if (temp == Convert.ToInt32(dtMatnrSource.Rows[i]["MENGE"].ToString()))//当累加数量等于需求量才放入Table
                            {
                                for (int j = 0; j < drOutSource.Length; j++)
                                {
                                    if (((start >> j) & 1) == 1)
                                    {
                                        drRow = dtTemp.NewRow();
                                        drRow["MANDT"] = drOutSource[j]["MANDT"].ToString();
                                        drRow["COMCD"] = drOutSource[j]["COMCD"].ToString();
                                        drRow["WERKS"] = drOutSource[j]["WERKS"].ToString();
                                        drRow["LGORT"] = drOutSource[j]["LGORT"].ToString();
                                        drRow["LOCAT"] = drOutSource[j]["LOCAT"].ToString();
                                        drRow["MBLNR"] = drOutSource[j]["MBLNR"].ToString();
                                        drRow["ZEILE"] = drOutSource[j]["ZEILE"].ToString();
                                        drRow["OMBLNR"] = drOutSource[j]["OMBLNR"].ToString();
                                        drRow["MATNR"] = drOutSource[j]["MATNR"].ToString();
                                        drRow["INSMK"] = drOutSource[j]["INSMK"].ToString();
                                        drRow["CHARG"] = drOutSource[j]["CHARG"].ToString();
                                        drRow["MENGE"] = drOutSource[j]["MENGE"].ToString();
                                        drRow["ALQTY"] = drOutSource[j]["ALQTY"].ToString();
                                        drRow["MRGID"] = drOutSource[j]["MRGID"].ToString();
                                        drRow["SERNO"] = drOutSource[j]["SERNO"].ToString();
                                        drRow["KOSTL"] = drOutSource[j]["KOSTL"].ToString();
                                        drRow["ARBPL"] = drOutSource[j]["ARBPL"].ToString();
                                        drRow["TRNTP"] = drOutSource[j]["TRNTP"].ToString();
                                        drRow["EBELN"] = drOutSource[j]["EBELN"].ToString();
                                        drRow["LIFNR"] = drOutSource[j]["LIFNR"].ToString();
                                        drRow["RMAK1"] = drOutSource[j]["RMAK1"].ToString();
                                        drRow["INDAT"] = drOutSource[j]["INDAT"].ToString();
                                        drRow["KDMAT"] = drOutSource[j]["KDMAT"].ToString();
                                        dtTemp.Rows.Add(drRow);
                                    }
                                }
                                break;//只取一组数据
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //2.多个Pallet ID相加不等于需求量，即相加库存比需求多，也可能库存比需求少
                    if (dtTemp.Select("MATNR = '" + dtMatnrSource.Rows[i]["MATNR"].ToString() + "'").Length == 0)
                    {
                        StorageMenge = 0;
                        for (int j = 0; j < dtOutSource.Rows.Count; j++)
                        {
                            if (dtMatnrSource.Rows[i]["MATNR"].ToString() == dtOutSource.Rows[j]["MATNR"].ToString())
                            {
                                if (StorageMenge < Convert.ToInt32(dtMatnrSource.Rows[i]["MENGE"].ToString()))
                                {
                                    StorageMenge += Convert.ToInt32(dtOutSource.Rows[j]["MENGE"].ToString());
                                    drRow = dtTemp.NewRow();
                                    drRow["MANDT"] = dtOutSource.Rows[j]["MANDT"].ToString();
                                    drRow["COMCD"] = dtOutSource.Rows[j]["COMCD"].ToString();
                                    drRow["WERKS"] = dtOutSource.Rows[j]["WERKS"].ToString();
                                    drRow["LGORT"] = dtOutSource.Rows[j]["LGORT"].ToString();
                                    drRow["LOCAT"] = dtOutSource.Rows[j]["LOCAT"].ToString();
                                    drRow["MBLNR"] = dtOutSource.Rows[j]["MBLNR"].ToString();
                                    drRow["ZEILE"] = dtOutSource.Rows[j]["ZEILE"].ToString();
                                    drRow["OMBLNR"] = dtOutSource.Rows[j]["OMBLNR"].ToString();
                                    drRow["MATNR"] = dtOutSource.Rows[j]["MATNR"].ToString();
                                    drRow["INSMK"] = dtOutSource.Rows[j]["INSMK"].ToString();
                                    drRow["CHARG"] = dtOutSource.Rows[j]["CHARG"].ToString();
                                    drRow["MENGE"] = dtOutSource.Rows[j]["MENGE"].ToString();
                                    drRow["ALQTY"] = dtOutSource.Rows[j]["ALQTY"].ToString();
                                    drRow["MRGID"] = dtOutSource.Rows[j]["MRGID"].ToString();
                                    drRow["SERNO"] = dtOutSource.Rows[j]["SERNO"].ToString();
                                    drRow["KOSTL"] = dtOutSource.Rows[j]["KOSTL"].ToString();
                                    drRow["ARBPL"] = dtOutSource.Rows[j]["ARBPL"].ToString();
                                    drRow["TRNTP"] = dtOutSource.Rows[j]["TRNTP"].ToString();
                                    drRow["EBELN"] = dtOutSource.Rows[j]["EBELN"].ToString();
                                    drRow["LIFNR"] = dtOutSource.Rows[j]["LIFNR"].ToString();
                                    drRow["RMAK1"] = dtOutSource.Rows[j]["RMAK1"].ToString();
                                    drRow["INDAT"] = dtOutSource.Rows[j]["INDAT"].ToString();
                                    drRow["KDMAT"] = dtOutSource.Rows[j]["KDMAT"].ToString();
                                    dtTemp.Rows.Add(drRow);
                                }
                                if (StorageMenge >= Convert.ToInt32(dtMatnrSource.Rows[i]["MENGE"].ToString()))
                                {
                                    break;
                                }
                                if (j == dtOutSource.Rows.Count - 1)
                                {
                                    if (StorageMenge < Convert.ToInt32(dtMatnrSource.Rows[i]["MENGE"].ToString()))
                                    {
                                        MessageBox.Show("料号" + dtMatnrSource.Rows[i]["MATNR"].ToString() + "库存数量不足!!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            dtOutSource = dtTemp.Copy();
            dtOutSource.Columns.Add("BLACE");
            this.rdbInteger.Enabled = true;
            this.rdbScatter.Enabled = true;
            this.btnConfirm.Enabled = false;
            this.btnQuery.Enabled = true;
            this.btnPrint.Enabled = true;
            ShowOutSourceDataGridView();
        }

        private void txtPALID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                this.btnQuery_Click(null,null);
                this.txtPALID.SelectAll();
                this.txtPALID.Focus();
            }
        }

        private void txtSourceData_DoubleClick(object sender, EventArgs e)
        {
            StorageOut_PlasticMaterials_BatchImport objStorageOut_PlasticMaterials_BatchImport = new StorageOut_PlasticMaterials_BatchImport(UserData, Progid);
            objStorageOut_PlasticMaterials_BatchImport.ShowDialog();
            dtMatnrSource = objStorageOut_PlasticMaterials_BatchImport.Data;
            if (dtMatnrSource.Rows.Count > 0)
            {
                this.txtSourceData.Text = dtMatnrSource.Rows[0]["MATNR"].ToString() + ";" +
                                          dtMatnrSource.Rows[0]["MENGE"].ToString() + "...";
            }
        }
    }
}
