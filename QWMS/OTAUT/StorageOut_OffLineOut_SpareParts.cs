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
    public partial class StorageOut_OffLineOut_SpareParts : Form
    {
        #region 變數宣告
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
        private int FirstLgortIndex;
        private ArrayList aryMblnr = new ArrayList();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private ArrayList aryMixedMaterial = new ArrayList();
        UserInfo UserData = new UserInfo();
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
        #endregion

        public StorageOut_OffLineOut_SpareParts()
        {
            InitializeComponent();
        }

        public StorageOut_OffLineOut_SpareParts(UserInfo varUserData, string strProgid)
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
				if(!objStorageOut.CheckAuthority())
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{
					ShowStatusData();
					ShowDdlWerks();
					ShowDdlLgort();
					ShowPrintCheckBox();
					if(cmbWerks.Items.Count > 0)
					{
						this.cmbWerks.SelectedIndex = 0;
					}
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = FirstLgortIndex;
                    }
				}
			}
			catch(Exception ex)
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
            if (bolPrint == true)
            {
                this.chkPrint.Checked = true;
            }
            else
            {
                this.chkPrint.Checked = false;
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

        private void btnAdd_Click(object sender, System.EventArgs e)
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
                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                StorageOut_SpareParts_Add objStorageOut_SpareParts_Add = new StorageOut_SpareParts_Add(UserData, Progid, Werks, Lgort, "", "", "", "", dtOutSource);
                objStorageOut_SpareParts_Add.ShowDialog();
                dtOutSource = objStorageOut_SpareParts_Add.SapData;
                ShowOutSourceDataGridView();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void ShowOutSourceDataGridView()
        {
            this.dgvOutSource.AutoGenerateColumns = false;
            this.dgvOutSource.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
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

                //DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                //dgvcMblnr.DataPropertyName = "MBLNR";
                //dgvcMblnr.HeaderText = "Document No";
                //dgvcMblnr.Width = 90;
                //dgvcMblnr.ReadOnly = true;
                //this.dgvOutSource.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcAlqty);

                //DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                //dgvcKostl.DataPropertyName = "KOSTL";
                //dgvcKostl.HeaderText = "Dept No.";
                //dgvcKostl.ReadOnly = true;
                //this.dgvOutSource.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No.";
                dgvcEbeln.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                this.dgvOutSource.Columns.Add(dgvcKdmat);

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

                //DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                //dgvcMrgid.DataPropertyName = "MRGID";
                //dgvcMrgid.HeaderText = "Mixed Material No";
                //dgvcMrgid.Width = 100;
                //dgvcMrgid.ReadOnly = true;
                //this.dgvStorage.Columns.Add(dgvcMrgid);

                //DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                //dgvcMblnr.DataPropertyName = "MBLNR";
                //dgvcMblnr.HeaderText = "Document No";
                //dgvcMblnr.Width = 90;
                //dgvcMblnr.ReadOnly = true;
                //this.dgvStorage.Columns.Add(dgvcMblnr);

                //DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                //dgvcZeile.DataPropertyName = "ZEILE";
                //dgvcZeile.HeaderText = "Document Item";
                //dgvcZeile.Width = 90;
                //dgvcZeile.ReadOnly = true;
                //this.dgvStorage.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcEbeln);

                //DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                //dgvcLifnr.DataPropertyName = "LIFNR";
                //dgvcLifnr.HeaderText = "Vendor";
                //dgvcLifnr.ReadOnly = true;
                //this.dgvStorage.Columns.Add(dgvcLifnr);

                //DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                //dgvcTrntp.DataPropertyName = "OMBLNR";
                //dgvcTrntp.HeaderText = "Store In Docu. No";
                //dgvcTrntp.Width = 100;
                //dgvcTrntp.ReadOnly = true;
                //this.dgvStorage.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcrmak1 = new DataGridViewTextBoxColumn();
                dgvcrmak1.DataPropertyName = "RMAK1";
                dgvcrmak1.HeaderText = "Remark";
                dgvcrmak1.ReadOnly = true;
                this.dgvStorage.Columns.Add(dgvcrmak1);

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

        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataRow[] foundRow;
                DataRow[] combineRow;
                DataRow drRow;
                this.btnAdd.Enabled = false;
                string strOrderBy = "";
                DataSet dsData = new DataSet();
                DataTable dtTempStorage = new DataTable();
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();

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

                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                dsData = objStorageData.QueryOffLineOutData_SpareParts(dtOutSource);
                dtOutSource = dsData.Tables[0].Copy();
                dtTempStorage = dsData.Tables[1].Copy();
                dtOutSource.Columns.Add("BLACE");
                dtTempStorage.Columns.Add("BLACE");

                //將同儲位同料號的資料加總 Kent 20050904
                int intCombineLocalTotal = 0;
                int intCombineLocatOut = 0;
                dtCombineStorage = dtTempStorage.Clone();
                for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                {
                    //strTempCombine = dtTempStorage.Rows[i]["MANDT"].ToString() + dtTempStorage.Rows[i]["COMCD"].ToString() + dtTempStorage.Rows[i]["WERKS"].ToString() + dtTempStorage.Rows[i]["LGORT"].ToString() + dtTempStorage.Rows[i]["LOCAT"].ToString() + dtTempStorage.Rows[i]["MATNR"].ToString() + dtTempStorage.Rows[i]["INSMK"].ToString() + dtTempStorage.Rows[i]["CHARG"].ToString() + ";";
                    #region 每次比對的Index (sbCombineIndex)  
                    //需另外考慮PO No.(WHDWN.EBELN)與客人料號(WHDWN.KDMAT)必須與庫存資料一致(by Smose Liao 20100531)
                    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                    sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                    sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                    sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                    sbCombineIndex.Append(" and LGORT='" + cmbLgort.Items[cmbLgort.SelectedIndex].ToString() + "'");
                    sbCombineIndex.Append(" and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                    sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                    sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                    sbCombineIndex.Append(" and EBELN='" + dtTempStorage.Rows[i]["EBELN"].ToString() + "'");
                    sbCombineIndex.Append(" and KDMAT='" + dtTempStorage.Rows[i]["KDMAT"].ToString() + "'");
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
                        //drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                        drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                        drRow["LIFNR"] = dtTempStorage.Rows[i]["LIFNR"].ToString();
                        drRow["OMBLNR"] = "";
                        drRow["MRGID"] = "";
                        drRow["KOSTL"] = "";
                        drRow["ARBPL"] = "";
                        drRow["TRNTP"] = "";
                        drRow["RMAK1"] = "";
                        drRow["INDAT"] = "";
                        drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                        dtCombineStorage.Rows.Add(drRow);
                    }
                }

                dtStorage = dtTempStorage.Clone();
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
                        //drRow["ZEILE"] = foundRow[i]["ZEILE"].ToString();
                        drRow["EBELN"] = foundRow[i]["EBELN"].ToString();
                        drRow["LIFNR"] = foundRow[i]["LIFNR"].ToString();
                        drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                        drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                        drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                        drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                        drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                        drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                        drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                        drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
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

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            SetbtnSaveProcess();
            try
            {
                stsWarning.Text = "";
                QCI.QWMS.StorageOut objStorageOut = new QCI.QWMS.StorageOut(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);

                if (objStorageOut.AddOffLineOutData_SpareParts(dtOutSource, dtStorage))
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

                    if (chkPrint.Checked)
                    {
                        //列印實際出庫資料
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

                        ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_SPAREPARTS", dtPrint);
                        objReportPrint.Report.PrintToPrinter(1, true, 0, 0);
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

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            //txtLifnr.Text = "";
            //txtLifnr.Enabled = true;
            this.strWerks = "";
            this.strLgort = "";
            ShowPrintCheckBox();
            this.rdoLocat.Checked = false;
            this.rdoMatnr.Checked = false;
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
            this.btnAdd.Enabled = true;
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
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
            //列印實際出庫資料
            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT_SPAREPARTS", dtPrint);
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

        private void dgvOutSource_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int intRowNo;
                stsWarning.Text = "";
                DataGridView dgvClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgvClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    intRowNo = hitRow.RowIndex;
                    strMatnr = dgvOutSource.CurrentRow.Cells[0].Value.ToString();
                    strInsmk = dgvOutSource.CurrentRow.Cells[1].Value.ToString();
                    strCharg = dgvOutSource.CurrentRow.Cells[2].Value.ToString();
                    strMblnr = dgvOutSource.CurrentRow.Cells[3].Value.ToString();

                    StorageOut_SpareParts_Add objStorageOut_SpareParts_Add = new StorageOut_SpareParts_Add(UserData, Progid, Werks, Lgort, Matnr, Insmk, Charg, Mblnr, dtOutSource);
                    objStorageOut_SpareParts_Add.ShowDialog();
                    dtOutSource = objStorageOut_SpareParts_Add.SapData;
                    ShowOutSourceDataGridView();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
    }
}
