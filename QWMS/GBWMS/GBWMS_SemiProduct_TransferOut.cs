using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;
using System.Text.RegularExpressions;

namespace QWMS
{
    public partial class GBWMS_SemiProduct_TransferOut : Form
    {
        #region Constructor

        public GBWMS_SemiProduct_TransferOut(UserInfo varUserData, string strProgid)
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

                    ShowDdlWerksFr();
                    ShowDdlLgortFr();

                    if (cmbWerksFr.Items.Count > 0)
                    {
                        this.cmbWerksFr.SelectedIndex = 0;
                    }
                    if (cmbLgortFr.Items.Count > 0)
                    {
                        this.cmbLgortFr.SelectedIndex = 0;
                    }

                    string strWerksFr = Convert.ToString(cmbWerksFr.SelectedItem);
                    ShowDdlWerksTo(strWerksFr);
                    //ShowDdlWerksTo(strWerksFr);
                    //ShowDdlLgortTo();

                    if (cmbWerksTo.Items.Count > 0)
                    {
                        this.cmbWerksTo.SelectedIndex = 0;
                    }
                    //if (cmbLgortTo.Items.Count > 0)
                    //{
                    //    this.cmbLgortTo.SelectedIndex = 0;
                    //}
                    cmbWerksTo.Enabled = false;

                    ResetPage();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region DataMember

        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;

        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtOutSource = new DataTable();
        private DataTable dtStorageLocation = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtTmpData = new DataTable();

        #endregion

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

        #region MemberFunction

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

        private void ShowDdlWerksFr()
        {
            Authority objAuthority = new Authority(UserData);
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerksFr.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerksFr.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void ShowDdlWerksTo(string strWerks)
        {
            Authority objAuthority = new Authority(UserData);
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerksTo.Items.Clear();
                dtTemp = objAuthority.CheckPlantWithoutAuthority("");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerksTo.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    if (dtTemp.Rows[i]["F_TEXT"].ToString() == strWerks && strWerks != "")
                    {
                        cmbWerksTo.SelectedIndex = i;
                    }
                }
                //cmbWerksTo.Items.Clear();
                //dtTemp = objAuthority.CheckPlantWithoutAuthority(strWerks);
                //for (int i = 0; i < dtTemp.Rows.Count; i++)
                //{
                //    cmbWerksTo.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        #endregion

        #region ShowDdlLgort

        private void ShowDdlLgortFr()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                stsWarning.Text = string.Empty;
                DataTable dtTemp = new DataTable();
                if (cmbWerksFr.SelectedIndex != -1)
                {
                    strWerks = cmbWerksFr.Items[cmbWerksFr.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgortFr.SelectedIndex != -1)
                {
                    strLgort = cmbLgortFr.Items[cmbLgortFr.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgortFr.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgortFr.Items.Clear();
                    strLgort = string.Empty;
                }
                else
                {
                    cmbLgortFr.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgortFr.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgortFr.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        //private void ShowDdlLgortTo()
        //{
        //    try
        //    {
        //        Authority objAuthority = new Authority(UserData);
        //        stsWarning.Text = string.Empty;
        //        DataTable dtTemp = new DataTable();
        //        if (cmbWerksTo.SelectedIndex != -1)
        //        {
        //            strWerks = cmbWerksTo.Items[cmbWerksTo.SelectedIndex].ToString();
        //            dtTemp = objAuthority.CheckLgortAuthority(strWerks);
        //        }
        //        else
        //        {
        //            dtTemp = objAuthority.CheckLgortAuthority();
        //        }
        //        if (cmbLgortTo.SelectedIndex != -1)
        //        {
        //            strLgort = cmbLgortTo.Items[cmbLgortTo.SelectedIndex].ToString();
        //        }
        //        else
        //        {
        //            cmbLgortTo.Items.Clear();
        //        }

        //        if (dtTemp.Rows.Count == 0)
        //        {
        //            cmbLgortTo.Items.Clear();
        //            strLgort = string.Empty;
        //        }
        //        else
        //        {
        //            cmbLgortTo.Items.Clear();
        //            for (int i = 0; i < dtTemp.Rows.Count; i++)
        //            {
        //                cmbLgortTo.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
        //                if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
        //                {
        //                    cmbLgortTo.SelectedIndex = i;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowDdlLgort()");
        //    }
        //}

        #endregion

        //#region 設定預設是否可列印撿料單
        //private void ShowPrintCheckBox()
        //{
        //    bool bolPrint = false;
        //    try
        //    {
        //        StorageOut objStorageOut = new StorageOut(UserData, Progid);
        //        bolPrint = objStorageOut.CheckPrintCheckBox();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowPrintCheckBox()");
        //    }
        //    if (bolPrint == true)
        //    {
        //        this.chkPrint.Checked = true;
        //    }
        //    else
        //    {
        //        this.chkPrint.Checked = false;
        //    }
        //}
        //#endregion

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
            dgvOutSource.AllowUserToAddRows = false;
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

                //DCQTY
                DataGridViewTextBoxColumn dgvcDcqty = new DataGridViewTextBoxColumn();
                dgvcDcqty.DataPropertyName = "DCQTY";
                dgvcDcqty.HeaderText = "Document Qty";
                dgvcDcqty.Width = 90;
                dgvcDcqty.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcDcqty);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store Out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //ALMNG
                DataGridViewTextBoxColumn dgvcAlmng = new DataGridViewTextBoxColumn();
                dgvcAlmng.Name = "ALMNG";
                dgvcAlmng.DataPropertyName = "ALMNG";
                dgvcAlmng.HeaderText = "Storage Out Qty";
                dgvcAlmng.Width = 90;
                dgvcAlmng.ReadOnly = true;
                //dgvcAlmng.ReadOnly = false;
                //dgvcAlmng.DefaultCellStyle.BackColor = Color.Aquamarine;
                dgvOutSource.Columns.Add(dgvcAlmng);

                //SCQTY
                DataGridViewTextBoxColumn dgvcScqty = new DataGridViewTextBoxColumn();
                dgvcScqty.DataPropertyName = "SCQTY";
                dgvcScqty.HeaderText = "Scanned Qty";
                dgvcScqty.Width = 90;
                dgvcScqty.ReadOnly = true;
                dgvcScqty.Visible = !chkNoSnOut.Checked;
                dgvOutSource.Columns.Add(dgvcScqty);

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
                    this.btnQuery.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }
        #endregion

        #region ShowStorageLocationDataGrid
        private void ShowStorageLocationDataGrid()
        {
            dgvStorageLocation.AutoGenerateColumns = false;
            dgvStorageLocation.Columns.Clear();

            try
            {
                //CHKED
                DataGridViewCheckBoxColumn dgvcChecked = new DataGridViewCheckBoxColumn();
                dgvcChecked.Name = "CHKED";
                dgvcChecked.DataPropertyName = "CHKED";
                dgvcChecked.HeaderText = "Select";
                //dgvcChecked.Width = 90;
                //dgvcChecked.ReadOnly = false;
                dgvStorageLocation.Columns.Add(dgvcChecked);

                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcLocat);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcMatnr);

                //KDMAT
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.Width = 90;
                dgvcKdmat.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcKdmat);

                //INSMK
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 90;
                dgvcInsmk.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcInsmk);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcCharg);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Location Qty";
                dgvcMenge.Width = 80;
                dgvcMenge.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcMenge);

                //ALQTY
                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.Name = "ALQTY";
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage Out Qty";
                dgvcAlqty.Width = 80;
                dgvcAlqty.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcAlqty);

                //SCQTY
                DataGridViewTextBoxColumn dgvcScqty = new DataGridViewTextBoxColumn();
                dgvcScqty.DataPropertyName = "SCQTY";
                dgvcScqty.HeaderText = "Scanned Qty";
                dgvcScqty.Width = 90;
                dgvcScqty.ReadOnly = true;
                dgvcScqty.Visible = !chkNoSnOut.Checked;
                dgvStorageLocation.Columns.Add(dgvcScqty);

                //BLACE
                DataGridViewTextBoxColumn dgvcBlace = new DataGridViewTextBoxColumn();
                dgvcBlace.DataPropertyName = "BLACE";
                dgvcBlace.HeaderText = "Blance Qty";
                dgvcBlace.Width = 80;
                dgvcBlace.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcBlace);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcZeile);

                //EBELN
                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.Width = 90;
                dgvcEbeln.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcEbeln);

                //LIFNR
                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.Width = 90;
                dgvcLifnr.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcLifnr);

                //RMANO
                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No#";
                dgvcRmano.Width = 90;
                dgvcRmano.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcRmano);

                //OMBLNR
                DataGridViewTextBoxColumn dgvcOmblnr = new DataGridViewTextBoxColumn();
                dgvcOmblnr.DataPropertyName = "OMBLNR";
                dgvcOmblnr.HeaderText = "Store In Docu. No";
                dgvcOmblnr.Width = 100;
                dgvcOmblnr.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcOmblnr);

                //INDAT
                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.Width = 90;
                dgvcIndat.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcIndat);

                //RMAK1
                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.Width = 90;
                dgvcRmak1.ReadOnly = true;
                dgvStorageLocation.Columns.Add(dgvcRmak1);

                //PKDAT: 棧板滿板時間
                DataGridViewTextBoxColumn dgvcPkdat = new DataGridViewTextBoxColumn();
                dgvcPkdat.DataPropertyName = "PKDAT";
                dgvcPkdat.HeaderText = "Pallet Time";
                dgvcPkdat.ReadOnly = true;
                dgvcPkdat.Width = 140;
                dgvStorageLocation.Columns.Add(dgvcPkdat);

                //Order by
                if (rdoMatnr.Checked)
                {
                    dtStorageLocation.DefaultView.Sort = "MATNR";
                }
                else
                {
                    dtStorageLocation.DefaultView.Sort = "LOCAT";
                }

                dgvStorageLocation.DataSource = dtStorageLocation;
                lblStorageLocation.Text = dtStorageLocation.Rows.Count.ToString() + " record(s)";

              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageLocationDataGrid()");
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
                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 130;
                dgvcMblnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMblnr);

                //BOXID
                DataGridViewTextBoxColumn dgvcBoxid = new DataGridViewTextBoxColumn();
                dgvcBoxid.DataPropertyName = "BOXID";
                dgvcBoxid.HeaderText = "Box ID";
                dgvcBoxid.Width = 100;
                dgvcBoxid.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcBoxid);

                DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                dgvcSERNO.DataPropertyName = "SERNO";
                dgvcSERNO.HeaderText = "Serial NO";
                dgvcSERNO.Width = 90;
                dgvcSERNO.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcSERNO);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMatnr);

                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcLocat);

                //INSMK
                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 60;
                dgvcInsmk.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcInsmk);

                //LOCAT
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 90;
                dgvcCharg.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcCharg);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "ALQTY";
                dgvcMenge.HeaderText = "BoxID Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvStorage.Columns.Add(dgvcMenge);

                dgvStorage.DataSource = dtStorage;
                lblStorage.Text = dtStorage.Rows.Count.ToString() + " record(s)";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ResetPage

        private void ResetPage()
        {
            this.strWerks = string.Empty;
            this.strLgort = string.Empty;
            this.txtMblnr.Text = string.Empty;
            this.alMblnrs.Clear();
            this.txtMblnr.Enabled = true;
            this.chkNoSnOut.Checked = false;
            this.chkNoSnOut.Enabled = true;
            this.btnConfirm.Enabled = true;

            this.dtOutSource.Clear();
            this.dtStorageLocation.Clear();
            this.dtStorage.Clear();

            this.dgvOutSource.DataSource = null;
            this.dgvStorageLocation.DataSource = null;
            this.dgvStorage.DataSource = null;

            this.stsWarning.Text = string.Empty;
            this.btnQuery.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnPrint.Enabled = false;
            //this.panel1.Enabled = true;
            this.rdoLocat.Checked = false;
            this.rdoMatnr.Checked = false;

            lblOutSource.Text = string.Format("0 record(s)");
            lblStorageLocation.Text = string.Format("0 record(s)");
            lblStorage.Text = string.Format("0 record(s)");

            tabStorage.TabPages.Remove(tpScan);
        }

        #endregion

        #endregion

        #region Event

        #region cmbWerksFr_SelectedIndexChanged
        private void cmbWerksFr_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            string strWerksFr = Convert.ToString(cmbWerksFr.SelectedItem);
          
            ShowDdlLgortFr();

            if (cmbLgortFr.Items.Count > 0)
            {
                this.cmbLgortFr.SelectedIndex = 0;
            }

            //ShowDdlWerksTo(strWerksFr);

            if (cmbWerksTo.Items.Count > 0)
            {
                this.cmbWerksTo.SelectedIndex = 0;
            }
        }
        #endregion

        //#region cmbWerksTo_SelectedIndexChanged
        //private void cmbWerksTo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    stsWarning.Text = string.Empty;
        //    ShowDdlLgortTo();
        //}
        //#endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = string.Empty;
                string[] aryTempMblnr = txtMblnr.Text.Trim().Split(new char[] { ',' });
                Mblnrs.Clear();
                if (Mblnrs.Count == 0)
                {
                    for (int i = 0; i < aryTempMblnr.Length; i++)
                    {
                        Mblnrs.Add(aryTempMblnr[i].ToString().Trim());
                    }
                }

                if (cmbWerksFr.SelectedIndex != -1)
                {
                    strWerks = cmbWerksFr.Items[cmbWerksFr.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = string.Empty;
                }

                if (cmbLgortFr.SelectedIndex != -1)
                {
                    strLgort = cmbLgortFr.Items[cmbLgortFr.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = string.Empty;
                }

                string strWerksTo = string.Empty;

                if (cmbWerksTo.SelectedIndex != -1)
                {
                    strWerksTo = cmbWerksTo.Items[cmbWerksTo.SelectedIndex].ToString();
                }
                else
                {
                    strWerksTo = string.Empty;
                }

                //string strLgortTo = string.Empty;
                //if (cmbLgortTo.SelectedIndex != -1)
                //{
                //    strLgortTo = cmbLgortTo.Items[cmbLgortTo.SelectedIndex].ToString();
                //}
                //else
                //{
                //    strLgortTo = string.Empty;
                //}


                //廠區倉別不為空
                if (string.IsNullOrEmpty(Werks) || string.IsNullOrEmpty(Lgort))
                {
                    stsWarning.Text = "From Plant and storage can't be empty!!";
                    return;
                }

                //廠區倉別不為空
                if (string.IsNullOrEmpty(strWerksTo))// || string.IsNullOrEmpty(strLgortTo)
                {
                    stsWarning.Text = "To Plant can't be empty!!";
                    return;
                }

                //單號不為空
                if (Mblnrs.Count == 0)
                {
                    stsWarning.Text = "Document No can't be empty!!";
                    return;
                }

                SapData objSapData = new SapData(UserData, Werks, Lgort);
                dtOutSource = objSapData.QuerySapLineOutData(Mblnrs, "TRANSFEROUT");

                //Add By Micheal 20150629 for 拨入厂区防呆
                if (dtOutSource != null && dtOutSource.Rows.Count > 0)
                {
                    ArrayList aryKOSTL = new ArrayList();
                    foreach (DataRow dr in dtOutSource.Rows)
                    {
                        if (!aryKOSTL.Contains(dr["KOSTL"].ToString()))
                        {
                            aryKOSTL.Add(dr["KOSTL"].ToString());
                        }
                    }
                    if (aryKOSTL.Count == 0)
                    {
                        stsWarning.Text = "请检查所选单据是否为调拨单!";
                        MessageBox.Show("请检查所选单据是否为调拨单!");
                        return;
                    }
                    else if (aryKOSTL.Count > 1)
                    {
                        stsWarning.Text = "请检查所选单据的拨入厂区是否一致!";
                        MessageBox.Show("请检查所选单据的拨入厂区是否一致!");
                        return;
                    }
                    else
                    {
                        ShowDdlWerksTo(aryKOSTL[0].ToString());
                    }
                }

                #region 透過Send id 取得WHSID.SEQNO  Smose Liao 20100426
                string strSeqno = string.Empty;
                string strArbpl = string.Empty;
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
                            strSeqno = string.Empty;
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

                btnConfirm.Enabled = false;
                chkNoSnOut.Enabled = false;
                txtMblnr.Enabled = false;
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
                if (cmbWerksFr.SelectedIndex != -1)
                {
                    strWerks = cmbWerksFr.Items[cmbWerksFr.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = string.Empty;
                }
                if (cmbLgortFr.SelectedIndex != -1)
                {
                    strLgort = cmbLgortFr.Items[cmbLgortFr.SelectedIndex].ToString();
                }
                else
                {
                    strLgort = string.Empty;
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

                TransferIn_SapDataSelect objTransferIn_SapDataSelect
                                 = new TransferIn_SapDataSelect(UserData, Werks, Lgort, Progid, Mblnrs, "TRANSFEROUT");
                objTransferIn_SapDataSelect.ShowDialog();
                if (objTransferIn_SapDataSelect.KOSTL.Count > 1)
                {
                    stsWarning.Text = "请检查所选单据的拨入厂区是否一致!";
                    MessageBox.Show("请检查所选单据的拨入厂区是否一致!");
                    return;
                }

                Mblnrs = objTransferIn_SapDataSelect.Mblnr;
                txtMblnr.Text = GetMblnrData();

                if (objTransferIn_SapDataSelect.KOSTL.Count > 0)
                {
                    ShowDdlWerksTo(objTransferIn_SapDataSelect.KOSTL[0].ToString());
                }
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
                for (int i = 0; i < dtOutSource.Rows.Count; i++)
                {
                    int intMenge = int.Parse(dtOutSource.Rows[i]["MENGE"].ToString().Trim());
                    int intAlmng = int.Parse(dtOutSource.Rows[i]["ALMNG"].ToString().Trim());

                    if (intAlmng > intMenge)
                    {
                        string strMblnr = dtOutSource.Rows[i]["MBLNR"].ToString().Trim();
                        MessageBox.Show(string.Format("SAP單據：{0} 數量不足，請確認!!", strMblnr), "提醒");
                        return;
                    }
                }

                #region 變數宣告

                stsWarning.Text = string.Empty;
                string strOrderBy = string.Empty;
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();

                //DataSet dsData = new DataSet();

                //DataTable dtTempStorage = new DataTable();   
                //DataRow[] foundRow;
                //DataRow[] combineRow;
                //DataRow drRow;

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
                DataSet dsData = objStorageData.GetOnLineOutData(dtOutSource, "", "", false, "", false, false);

                dtOutSource = dsData.Tables[0].Copy();
                dtStorageLocation = dsData.Tables[1].Copy();
                //dtStorageLocation.Columns.Add("BLACE");

                if (dtStorageLocation == null || dtStorageLocation.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    return;
                }

                if (!string.IsNullOrEmpty(strOrderBy))
                {
                    dtStorageLocation = dtStorageLocation.Select(string.Empty, strOrderBy).CopyToDataTable();
                }

                #region //Brian mark 20150328
                ////將同儲位同料號的資料加總 Kent 20050904
                //int intCombineLocalTotal = 0;
                //int intCombineLocatOut = 0;
                //dtCombineStorage = dtTempStorage.Clone();
                //for (int i = 0; i < dtTempStorage.Rows.Count; i++)
                //{
                //    #region 每次比對的Index (sbCombineIndex)
                //    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                //    sbCombineIndex.Append("MANDT='" + dtTempStorage.Rows[i]["MANDT"].ToString() + "'");
                //    sbCombineIndex.Append(" and COMCD='" + dtTempStorage.Rows[i]["COMCD"].ToString() + "'");
                //    sbCombineIndex.Append(" and WERKS='" + dtTempStorage.Rows[i]["WERKS"].ToString() + "'");
                //    sbCombineIndex.Append(" and LGORT='" + dtTempStorage.Rows[i]["LGORT"].ToString() + "'");
                //    sbCombineIndex.Append(" and LOCAT='" + dtTempStorage.Rows[i]["LOCAT"].ToString() + "'");
                //    sbCombineIndex.Append(" and MATNR='" + dtTempStorage.Rows[i]["MATNR"].ToString() + "'");
                //    sbCombineIndex.Append(" and INSMK='" + dtTempStorage.Rows[i]["INSMK"].ToString() + "'");
                //    sbCombineIndex.Append(" and CHARG='" + dtTempStorage.Rows[i]["CHARG"].ToString() + "'");
                //    #endregion

                //    if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                //    {
                //        intCombineLocatOut = 0;
                //        alAllCombine.Add(sbCombineIndex.ToString());

                //        combineRow = dtTempStorage.Select(sbCombineIndex.ToString());
                //        for (int j = 0; j < combineRow.Length; j++)
                //        {
                //            intCombineLocatOut += Int32.Parse(combineRow[j]["ALQTY"].ToString());
                //        }
                //        intCombineLocalTotal = objStorageData.QueryMatnrQty(dtTempStorage.Rows[i]["LOCAT"].ToString(), dtTempStorage.Rows[i]["MATNR"].ToString(), dtTempStorage.Rows[i]["INSMK"].ToString(), dtTempStorage.Rows[i]["CHARG"].ToString());

                //        drRow = dtCombineStorage.NewRow();
                //        drRow["MANDT"] = dtTempStorage.Rows[i]["MANDT"].ToString();
                //        drRow["COMCD"] = dtTempStorage.Rows[i]["COMCD"].ToString();
                //        drRow["WERKS"] = dtTempStorage.Rows[i]["WERKS"].ToString();
                //        drRow["LGORT"] = dtTempStorage.Rows[i]["LGORT"].ToString();
                //        drRow["LOCAT"] = dtTempStorage.Rows[i]["LOCAT"].ToString();
                //        drRow["MATNR"] = dtTempStorage.Rows[i]["MATNR"].ToString();
                //        drRow["INSMK"] = dtTempStorage.Rows[i]["INSMK"].ToString();
                //        drRow["CHARG"] = dtTempStorage.Rows[i]["CHARG"].ToString();
                //        drRow["MENGE"] = intCombineLocalTotal.ToString();
                //        drRow["ALQTY"] = intCombineLocatOut.ToString();
                //        drRow["BLACE"] = Convert.ToString(intCombineLocalTotal - intCombineLocatOut);
                //        drRow["MBLNR"] = dtTempStorage.Rows[i]["MBLNR"].ToString();
                //        drRow["ZEILE"] = dtTempStorage.Rows[i]["ZEILE"].ToString();
                //        drRow["EBELN"] = dtTempStorage.Rows[i]["EBELN"].ToString();
                //        drRow["LIFNR"] = string.Empty;
                //        drRow["RMANO"] = string.Empty;
                //        drRow["OMBLNR"] = string.Empty;
                //        drRow["MRGID"] = string.Empty;
                //        drRow["KOSTL"] = string.Empty;
                //        drRow["ARBPL"] = string.Empty;
                //        drRow["TRNTP"] = string.Empty;
                //        drRow["RMAK1"] = string.Empty;
                //        drRow["INDAT"] = string.Empty;
                //        drRow["KDMAT"] = dtTempStorage.Rows[i]["KDMAT"].ToString();
                //        drRow["SERNO"] = string.Empty;
                //        drRow["DACOD"] = dtTempStorage.Rows[i]["DACOD"].ToString();
                //        drRow["LOCOD"] = dtTempStorage.Rows[i]["LOCOD"].ToString();
                //        drRow["INSPT"] = dtTempStorage.Rows[i]["INSPT"].ToString();
                //        drRow["PKDAT"] = dtTempStorage.Rows[i]["PKDAT"].ToString();

                //        //Brian bak 20150326
                //        drRow["CHKED"] = dtTempStorage.Rows[i]["CHKED"];

                //        dtCombineStorage.Rows.Add(drRow);
                //    }
                //}

                //dtStorage = dtTempStorage.Clone();
                //dtStorage.Columns.Add("MENGE1");
                //if (strOrderBy != "")
                //{
                //    foundRow = dtTempStorage.Select("", strOrderBy);
                //    for (int i = 0; i < foundRow.Length; i++)
                //    {
                //        drRow = dtStorage.NewRow();
                //        drRow["MANDT"] = foundRow[i]["MANDT"].ToString();
                //        drRow["COMCD"] = foundRow[i]["COMCD"].ToString();
                //        drRow["WERKS"] = foundRow[i]["WERKS"].ToString();
                //        drRow["LGORT"] = foundRow[i]["LGORT"].ToString();
                //        drRow["LOCAT"] = foundRow[i]["LOCAT"].ToString();
                //        drRow["MATNR"] = foundRow[i]["MATNR"].ToString();
                //        drRow["INSMK"] = foundRow[i]["INSMK"].ToString();
                //        drRow["CHARG"] = foundRow[i]["CHARG"].ToString();
                //        drRow["MENGE"] = foundRow[i]["MENGE"].ToString();
                //        drRow["ALQTY"] = foundRow[i]["ALQTY"].ToString();
                //        drRow["MBLNR"] = foundRow[i]["MBLNR"].ToString();
                //        drRow["ZEILE"] = foundRow[i]["ZEILE"].ToString();
                //        drRow["EBELN"] = foundRow[i]["EBELN"].ToString();
                //        drRow["LIFNR"] = foundRow[i]["LIFNR"].ToString();
                //        drRow["RMANO"] = foundRow[i]["RMANO"].ToString();
                //        drRow["OMBLNR"] = foundRow[i]["OMBLNR"].ToString();
                //        drRow["MRGID"] = foundRow[i]["MRGID"].ToString();
                //        drRow["KOSTL"] = foundRow[i]["KOSTL"].ToString();
                //        drRow["ARBPL"] = foundRow[i]["ARBPL"].ToString();
                //        drRow["TRNTP"] = foundRow[i]["TRNTP"].ToString();
                //        drRow["RMAK1"] = foundRow[i]["RMAK1"].ToString();
                //        drRow["INDAT"] = foundRow[i]["INDAT"].ToString();
                //        drRow["MENGE1"] = foundRow[i]["MENGE"].ToString();
                //        drRow["BLACE"] = foundRow[i]["BLACE"].ToString();
                //        drRow["KDMAT"] = foundRow[i]["KDMAT"].ToString();
                //        drRow["SERNO"] = foundRow[i]["SERNO"].ToString();
                //        drRow["DACOD"] = foundRow[i]["DACOD"].ToString();
                //        drRow["LOCOD"] = foundRow[i]["LOCOD"].ToString();
                //        drRow["INSPT"] = foundRow[i]["INSPT"].ToString();
                //        drRow["PKDAT"] = dtTempStorage.Rows[i]["PKDAT"].ToString();

                //        //Brian bak 20150326
                //        drRow["CHKED"] = dtTempStorage.Rows[i]["CHKED"];

                //        dtStorage.Rows.Add(drRow);
                //    }
                //}
                //else
                //{
                //    dtStorage = dtTempStorage;
                //} 
                #endregion

                ShowOutSourceDataGrid();
                ShowStorageLocationDataGrid();

                #region Brian 20150327

                DataGridViewTextBoxColumn dgvcALMNG = dgvOutSource.Columns["ALMNG"] as DataGridViewTextBoxColumn;
                dgvcALMNG.ReadOnly = true;
                dgvcALMNG.DefaultCellStyle.BackColor = Color.White;

                #endregion

                if (dtStorageLocation.Rows.Count > 0)
                {
                    this.btnQuery.Enabled = false;
                    this.btnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnPrint_Click
        private void btnPrint_Click(object sender, EventArgs e)
        {
            #region Brian 20150328

            for (int i = 0; i < dtStorageLocation.Rows.Count; i++)
            {
                int intMenge = int.Parse(dtStorageLocation.Rows[i]["MENGE"].ToString().Trim());
                int intAlqty = int.Parse(dtStorageLocation.Rows[i]["ALQTY"].ToString().Trim());

                if (intAlqty > intMenge)
                {
                    string strLocat = dtStorageLocation.Rows[i]["LOCAT"].ToString().Trim();
                    MessageBox.Show(string.Format("儲位：{0} 數量不足，請確認!!", strLocat), "提醒");
                    return;
                }

                dtStorageLocation.Rows[i]["BLACE"] = intMenge - intAlqty;
            }

            for (int i = 0; i < dtOutSource.Rows.Count; i++)
            {
                string strMatnr = dtOutSource.Rows[i]["MATNR"].ToString().Trim();
                string strInsmk = dtOutSource.Rows[i]["INSMK"].ToString().Trim();
                string strCharg = dtOutSource.Rows[i]["CHARG"].ToString().Trim();

                int intAlmng = int.Parse(dtOutSource.Rows[i]["ALMNG"].ToString().Trim());
                int intAlqty = 0;

                DataRow[] drsStorageLocation = dtStorageLocation.Select(string.Format("MATNR = '{0}' AND INSMK = '{1}' AND CHARG = '{2}'", strMatnr, strInsmk, strCharg));

                for (int j = 0; j < drsStorageLocation.Length; j++)
                {
                    intAlqty += int.Parse(drsStorageLocation[j]["ALQTY"].ToString().Trim());
                }

                if (intAlmng != intAlqty)
                {
                    string strMblnr = dtOutSource.Rows[i]["MBLNR"].ToString().Trim();
                    MessageBox.Show(string.Format("備料信息總數量不等於SAP單據的數量，SAP單據：{0}，請確認!!", strMblnr), "提醒");
                    return;
                }
            }

            dtStorageLocation = dtStorageLocation.Select("CHKED = true").CopyToDataTable();
            ShowStorageLocationDataGrid();

            DataGridViewCheckBoxColumn dgvcCHKED = dgvStorageLocation.Columns["CHKED"] as DataGridViewCheckBoxColumn;
            dgvcCHKED.ReadOnly = true;
            dgvcCHKED.Visible = false;

            btnPrint.Enabled = false;
            btnSave.Enabled = true;

            #endregion

            if (!chkNoSnOut.Checked)
            {
                if (tabStorage.TabPages.Count == 1)
                {
                    tabStorage.TabPages.Add(tpScan);
                }
                tabStorage.SelectTab(tpScan);

                txtBoxid.Enabled = true;
                txtfixSn.Enabled = true;
                txtSn.Enabled = true;
            }

            string strOrderBy = string.Empty;
            if (rdoMatnr.Checked)
            {
                strOrderBy = "MATNR,LOCAT,INDAT";
            }
            else
            {
                strOrderBy = "LOCAT,MATNR,INDAT";
            }

            DataTable dtPrint = CommonInfo.SortDataTable(dtStorageLocation, strOrderBy);
            ReportPrint objReportPrint = new ReportPrint(UserData, "STOREOUT", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, EventArgs e)
        {

            DataTable dtTempData = new DataTable();
            try
            {
                stsWarning.Text = string.Empty;

                string strWerksTo = string.Empty;
                if (cmbWerksTo.SelectedIndex != -1)
                {
                    strWerksTo = cmbWerksTo.Items[cmbWerksTo.SelectedIndex].ToString();
                }
                else
                {
                    strWerksTo = string.Empty;
                }

                //string strLgortTo = string.Empty;
                //if (cmbLgortTo.SelectedIndex != -1)
                //{
                //    strLgortTo = cmbLgortTo.Items[cmbLgortTo.SelectedIndex].ToString();
                //}
                //else
                //{
                //    strLgortTo = string.Empty;
                //}

                StorageOut objStorageOut = new StorageOut(UserData, Werks, Lgort, Progid);
                SapData objSapData = new SapData(UserData, Werks, Lgort);

                //Modify By Michael 20150518 for 防止调拨时选错
                if (!string.IsNullOrEmpty(txtMblnr.Text.Trim()))
                {
                    DataTable dtMBLNR = objStorageOut.GetMBLNRWerks(txtMblnr.Text.Trim());
                    if (dtMBLNR != null && dtMBLNR.Rows.Count > 0)
                    {
                        if (strWerks != dtMBLNR.Rows[0]["WERKS"].ToString() ||
                            strWerksTo != dtMBLNR.Rows[0]["KOSTL"].ToString() ||
                            strLgort != dtMBLNR.Rows[0]["LGORT"].ToString())
                        {
                            MessageBox.Show(string.Format("所选厂区、仓别与SAP扣帐信息不一致，SAP單據：{0}，請確認!!", txtMblnr.Text.Trim()), "提醒");
                            return;
                        }
                    }
                }

                bool bolNoScan = chkNoSnOut.Checked;

                if (!bolNoScan)
                {
                    for (int i = 0; i < dtStorageLocation.Rows.Count; i++)
                    {
                        int intAlqty = int.Parse(dtStorageLocation.Rows[i]["ALQTY"].ToString().Trim());
                        int intScqty = int.Parse(dtStorageLocation.Rows[i]["SCQTY"].ToString().Trim());

                        if (intAlqty != intScqty)
                        {
                            string strLocat = dtStorageLocation.Rows[i]["LOCAT"].ToString().Trim();
                            MessageBox.Show(string.Format("刷入的BOX ID/SN數量不等於備料信息數量，儲位：{0}，請確認!!", strLocat), "提醒");
                            return;
                        }
                    }

                    for (int i = 0; i < dtOutSource.Rows.Count; i++)
                    {
                        int intAlmng = int.Parse(dtOutSource.Rows[i]["ALMNG"].ToString().Trim());
                        int intScqty = int.Parse(dtOutSource.Rows[i]["SCQTY"].ToString().Trim());

                        if (intAlmng != intScqty)
                        {
                            string strMblnr = dtOutSource.Rows[i]["MBLNR"].ToString().Trim();
                            MessageBox.Show(string.Format("刷入的BOX ID總數量不等於SAP單據的數量，SAP單據：{0}，請確認!!", strMblnr), "提醒");
                            return;
                        }
                    }
                }
                else 
                {
                    dtStorage = dtStorageLocation;
                }

                SetbtnSaveProcess();

                //必须同厂区调拨
                if (objStorageOut.AddSemiProductTransferOut_Confirm(strWerks, strLgort, strWerksTo, strLgort, dtOutSource, dtStorage, bolNoScan))
                {

                    //stsWarning.Text = "Update OK!!";
                    //this.btnSave.Enabled = false;
                    //this.btnPrint.Enabled = true;
                    MessageBox.Show("调拨扣帐OK！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetPage();
                }
                else
                {
                    MessageBox.Show("调拨扣帐Failed！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetPage();
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

        private void dgvStorage_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgvData = sender as DataGridView;

            if (dgvData.IsCurrentCellDirty)
            {
                dgvData.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvStorage_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgvData = sender as DataGridView;

            if (e.RowIndex != -1)
            {
                DataRow drCurrent = (dgvData.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;

                switch (dgvData.Columns[e.ColumnIndex].Name)
                {
                    case "CHKED":
                        {

                            DataGridViewTextBoxCell dgvcALQTY = dgvData.Rows[e.RowIndex].Cells["ALQTY"] as DataGridViewTextBoxCell;

                            string strCHKED = drCurrent["CHKED"].ToString();

                            if (strCHKED.ToUpper() == "TRUE")
                            {
                                dgvcALQTY.ReadOnly = false;
                                dgvcALQTY.Style.BackColor = Color.Aquamarine;
                            }
                            else
                            {
                                drCurrent["ALQTY"] = 0;
                                drCurrent["BLACE"] = drCurrent["MENGE"];

                                dgvcALQTY.ReadOnly = true;
                                dgvcALQTY.Style.BackColor = Color.White;
                            }
                        }
                        break;
                    case "ALQTY":
                        {
                            Regex rx = new Regex(@"^\d+$");

                            if (!rx.IsMatch(drCurrent["ALQTY"].ToString()))
                            {
                                MessageBox.Show("输入的数字不合法，请确认！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                drCurrent["ALQTY"] = 0;
                                drCurrent["BLACE"] = drCurrent["MENGE"];
                            }
                            else
                            {

                                int intMENGE = Convert.ToInt32(drCurrent["MENGE"].ToString());
                                int intALQTY = Convert.ToInt32(drCurrent["ALQTY"].ToString());

                                int intBLACE = intMENGE - intALQTY;

                                if (intBLACE >= 0)
                                {
                                    drCurrent["BLACE"] = intBLACE;
                                }
                                else
                                {
                                    MessageBox.Show("储位库存不足，请确认！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    drCurrent["ALQTY"] = 0;
                                    drCurrent["BLACE"] = drCurrent["MENGE"];
                                }
                            }
                        }
                        break;
                }
            }
        }

        #endregion

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            stsWarning.Text = string.Empty;

            if (e.KeyChar == (char)13)
            {
                if (checkSn.Checked == true)
                {
                    #region 指定SN
                    if (txtfixSn.Text.Trim() != string.Empty)
                    {
                        if (txtSn.Text.Length > 0)
                        {
                            if (txtSn.Text.Trim().Substring(0, 1).ToUpper() == "S")
                            {
                                txtSn.Text = txtSn.Text.Trim().Substring(1, txtSn.Text.Trim().Length - 1);
                            }

                            if (txtfixSn.Text.Trim().Substring(0, 1).ToUpper() == "S")
                            {
                                txtfixSn.Text = txtfixSn.Text.Trim().Substring(1, txtSn.Text.Trim().Length - 1);
                            }

                            if (txtSn.Text.Trim() == txtfixSn.Text.Trim())
                            {
                                if (!objSapData.GetBoxIDHoldStatusBySn(txtSn.Text.Trim()))
                                {
                                    DataTable dtTemp = new DataTable();
                                    dtTemp = objSapData.GetStorageDataBySn(txtSn.Text.Trim());

                                    if (dtTemp.Rows.Count > 0)
                                    {
                                        string strMatnr = dtTemp.Rows[0]["MATNR"].ToString().Trim();
                                        string strInsmk = dtTemp.Rows[0]["INSMK"].ToString().Trim();
                                        string strCharg = dtTemp.Rows[0]["CHARG"].ToString().Trim();

                                        StringBuilder sbSelectConditions = new StringBuilder();
                                        sbSelectConditions.AppendFormat("MATNR = '{0}' AND INSMK='{1}' AND CHARG='{2}'", strMatnr, strInsmk, strCharg);

                                        DataRow[] drsOutSource = dtOutSource.Select(sbSelectConditions.ToString());
                                        //brian 20150304 增加比对SAP单据与刷入的料号版本是否一致
                                        if (drsOutSource.Length > 0)
                                        {
                                            string strLocat = dtTemp.Rows[0]["LOCAT"].ToString().Trim();

                                            sbSelectConditions.AppendFormat(" AND LOCAT = '{0}'", strLocat);
                                            DataRow[] drsStorageLocation = dtStorageLocation.Select(sbSelectConditions.ToString());

                                            if (drsStorageLocation.Length > 0)
                                            {
                                                if (dtStorage.Rows.Count == 0)
                                                {
                                                    dtStorage = dtTemp.Clone();
                                                }

                                                #region MyRegion
                                                for (int i = 0; i < dtTemp.Rows.Count; i++)
                                                {
                                                    DataRow[] drsSelect;
                                                    string strBoxID = dtTemp.Rows[i]["BOXID"].ToString().Trim();
                                                    string strSerNO = dtTemp.Rows[i]["SERNO"].ToString().Trim();

                                                    drsSelect = dtStorage.Select("BOXID = '" + strBoxID + "' AND SERNO = ''");

                                                    if (drsSelect.Length > 0)
                                                    {
                                                        Sound.Play(@"Sound\OO.wav");
                                                        MessageBox.Show(string.Format("此SN：{0} 所在Box ID：{1} 已经存在，请确认！", strSerNO, strBoxID), "警告");

                                                        txtSn.Text = string.Empty;
                                                        txtSn.Focus();
                                                        return;
                                                    }

                                                    drsSelect = dtStorage.Select("SERNO = '" + strSerNO + "'");

                                                    if (drsSelect.Length > 0)
                                                    {
                                                        Sound.Play(@"Sound\OO.wav");
                                                        stsWarning.Text = "刷入的Serial NO重複，請確認!!";

                                                        txtSn.Text = string.Empty;
                                                        txtSn.Focus();
                                                        return;
                                                    }

                                                    drsOutSource[0]["SCQTY"] = Convert.ToInt32(drsOutSource[0]["SCQTY"]) + Convert.ToInt32(dtTemp.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;
                                                    drsStorageLocation[0]["SCQTY"] = Convert.ToInt32(drsStorageLocation[0]["SCQTY"]) + Convert.ToInt32(dtTemp.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;
                                                   
                                                    //Brian Add 反串库位资料
                                                    dtTemp.Rows[i]["INDAT"] = drsStorageLocation[0]["INDAT"].ToString().Trim();
                                                    dtTemp.Rows[i]["MBLNR"] = drsStorageLocation[0]["MBLNR"].ToString().Trim();

                                                    dtStorage.ImportRow(dtTemp.Rows[i]);

                                                    //dtStorage.ImportRow(dtTemp.Rows[i]);
                                                    //drsOutSource[0]["SCQTY"] = Convert.ToInt32(drsOutSource[0]["SCQTY"]) + Convert.ToInt32(dtTemp.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;
                                                }

                                                #endregion

                                                ShowStorageDataGrid();
                                                txtSn.Text = string.Empty;
                                                txtSn.Focus();
                                            }
                                            else
                                            {
                                                Sound.Play(@"Sound\OO.wav");
                                                stsWarning.Text = "刷入的Serial NO，备料信息不存在，请确认！！";
                                                txtBoxid.Text = string.Empty;
                                                txtBoxid.Focus();

                                            }
                                        }
                                        else
                                        {
                                            Sound.Play(@"Sound\OO.wav");
                                            stsWarning.Text = "刷入的Serial NO，料号或版本与SAP单据不符，请确认！！";
                                            txtSn.Text = string.Empty;
                                            txtSn.Focus();
                                        }
                                    }
                                    else
                                    {
                                        Sound.Play(@"Sound\OO.wav");
                                        stsWarning.Text = "查無此票Serial NO，請確認!!";
                                        txtSn.Text = string.Empty;
                                        txtSn.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    Sound.Play(@"Sound\OO.wav");
                                    MessageBox.Show("SN:" + txtSn.Text.Trim() + "被Hold，不允许出库！");
                                    return;
                                }
                            }
                            else
                            {
                                txtSn.Text = string.Empty;
                                txtSn.Focus();
                                MessageBox.Show(string.Empty);
                                return;
                            }
                        }
                    }
                    else
                    {
                        Sound.Play(@"Sound\OO.wav");
                        MessageBox.Show("请输入SN!");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region 刷入SN

                    if (txtSn.Text.Trim() == string.Empty)
                    {
                        Sound.Play(@"Sound\OO.wav");
                        stsWarning.Text = "刷入的Serial NO不能為空，請確認!!";
                        txtSn.Focus();
                        return;
                    }

                    if (!objSapData.GetBoxIDHoldStatusBySn(txtSn.Text.Trim()))
                    {
                        DataTable dtTemp = new DataTable();
                        dtTemp = objSapData.GetStorageDataBySn(txtSn.Text.Trim());

                        if (dtTemp.Rows.Count > 0)
                        {
                            string strMatnr = dtTemp.Rows[0]["MATNR"].ToString().Trim();
                            string strInsmk = dtTemp.Rows[0]["INSMK"].ToString().Trim();
                            string strCharg = dtTemp.Rows[0]["CHARG"].ToString().Trim();

                            StringBuilder sbSelectConditions = new StringBuilder();
                            sbSelectConditions.AppendFormat("MATNR = '{0}' AND INSMK='{1}' AND CHARG='{2}'", strMatnr, strInsmk, strCharg);

                            DataRow[] drsOutSource = dtOutSource.Select(sbSelectConditions.ToString());
                            //brian 20150304 增加比对SAP单据与刷入的料号版本是否一致
                            if (drsOutSource.Length > 0)
                            {
                                string strLocat = dtTemp.Rows[0]["LOCAT"].ToString().Trim();

                                sbSelectConditions.AppendFormat(" AND LOCAT = '{0}'", strLocat);
                                DataRow[] drsStorageLocation = dtStorageLocation.Select(sbSelectConditions.ToString());

                                if (drsStorageLocation.Length > 0)
                                {
                                    if (dtStorage.Rows.Count == 0)
                                    {
                                        dtStorage = dtTemp.Clone();
                                    }

                                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                                    {
                                        DataRow[] drsSelect;
                                        string strBoxID = dtTemp.Rows[i]["BOXID"].ToString().Trim();
                                        string strSerNO = dtTemp.Rows[i]["SERNO"].ToString().Trim();

                                        drsSelect = dtStorage.Select("BOXID = '" + strBoxID + "' AND SERNO = ''");

                                        if (drsSelect.Length > 0)
                                        {
                                            Sound.Play(@"Sound\OO.wav");
                                            MessageBox.Show(string.Format("此Serial NO：{0} 所在Box ID：{1} 已经存在，请确认！", strSerNO, strBoxID), "警告");

                                            txtSn.Text = string.Empty;
                                            txtSn.Focus();
                                            return;
                                        }

                                        drsSelect = dtStorage.Select("SERNO = '" + strSerNO + "'");

                                        if (drsSelect.Length > 0)
                                        {
                                            Sound.Play(@"Sound\OO.wav");
                                            stsWarning.Text = "刷入的Serial NO已存在，请确认！！";

                                            txtSn.Text = string.Empty;
                                            txtSn.Focus();
                                            return;
                                        }

                                        drsOutSource[0]["SCQTY"] = Convert.ToInt32(drsOutSource[0]["SCQTY"]) + Convert.ToInt32(dtTemp.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;
                                        drsStorageLocation[0]["SCQTY"] = Convert.ToInt32(drsStorageLocation[0]["SCQTY"]) + Convert.ToInt32(dtTemp.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;

                                        //Brian Add 反串库位资料
                                        dtTemp.Rows[i]["INDAT"] = drsStorageLocation[0]["INDAT"].ToString().Trim();
                                        dtTemp.Rows[i]["MBLNR"] = drsStorageLocation[0]["MBLNR"].ToString().Trim();

                                        dtStorage.ImportRow(dtTemp.Rows[i]);
                                    }


                                    ShowStorageDataGrid();
                                    txtSn.Text = string.Empty;
                                    txtSn.Focus();
                                }
                                else
                                {
                                    Sound.Play(@"Sound\OO.wav");
                                    stsWarning.Text = "刷入的Serial NO，备料信息不存在，请确认！！";
                                    txtBoxid.Text = string.Empty;
                                    txtBoxid.Focus();

                                }
                            }
                            else
                            {
                                Sound.Play(@"Sound\OO.wav");
                                stsWarning.Text = "刷入的Serial NO，料号或版本与SAP单据不符，请确认！！";
                                txtSn.Text = string.Empty;
                                txtSn.Focus();
                            }
                        }
                        else
                        {
                            Sound.Play(@"Sound\OO.wav");
                            stsWarning.Text = "查無此票Serial NO，請確認!!";
                            txtSn.Text = string.Empty;
                            txtSn.Focus();
                            return;
                        }
                    }
                    else
                    {
                        Sound.Play(@"Sound\OO.wav");
                        MessageBox.Show("SN:" + txtSn.Text.Trim() + "被Hold，不允许出库！");
                        return;
                    }

                    #endregion
                }
            }
        }

        private void txtBoxid_KeyDown(object sender, KeyEventArgs e)
        {
            QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                stsWarning.Text = string.Empty;

                #region 防呆限制

                if (dtOutSource.Rows.Count == 0)
                {
                    Sound.Play(@"Sound\OO.wav");
                    stsWarning.Text = "SAP单据不能为空，请确认！！";
                    txtBoxid.Focus();
                    return;
                }

                if (txtBoxid.Text.Trim() == string.Empty)
                {
                    Sound.Play(@"Sound\OO.wav");
                    stsWarning.Text = "刷入的Box ID不能為空，請確認!!";
                    txtBoxid.Focus();
                    return;
                }

                if (objSapData.GetBoxIDHoldStatus(this.txtBoxid.Text.Trim()))
                {
                    Sound.Play(@"Sound\OO.wav");
                    stsWarning.Text = "刷入的Box ID被Hold住，請確認!!";
                    txtBoxid.Text = string.Empty;
                    txtBoxid.Focus();
                    return;
                }

                #endregion

                try
                {
                    dtTmpData = objSapData.GetStorageDataByBoxID(this.txtBoxid.Text.Trim());
                    if (dtTmpData.Rows.Count > 0)
                    {
                        string strMatnr = dtTmpData.Rows[0]["MATNR"].ToString().Trim();
                        string strInsmk = dtTmpData.Rows[0]["INSMK"].ToString().Trim();
                        string strCharg = dtTmpData.Rows[0]["CHARG"].ToString().Trim();

                        StringBuilder sbSelectConditions = new StringBuilder();
                        sbSelectConditions.AppendFormat("MATNR = '{0}' AND INSMK='{1}' AND CHARG='{2}'", strMatnr, strInsmk, strCharg);

                        DataRow[] drsOutSource = dtOutSource.Select(sbSelectConditions.ToString());
                        //brian 20150304 增加比对SAP单据与刷入的料号版本是否一致
                        if (drsOutSource.Length > 0)
                        {
                            string strLocat = dtTmpData.Rows[0]["LOCAT"].ToString().Trim();

                            sbSelectConditions.AppendFormat(" AND LOCAT = '{0}'", strLocat);
                            DataRow[] drsStorageLocation = dtStorageLocation.Select(sbSelectConditions.ToString());
                            if (drsStorageLocation.Length > 0)
                            {
                                if (dtStorage.Rows.Count == 0)
                                {
                                    dtStorage = dtTmpData.Clone();
                                }

                                for (int i = 0; i < dtTmpData.Rows.Count; i++)
                                {
                                    //dtStorage.ImportRow(dtTmpData.Rows[i]);
                                    DataRow[] drsSelected;
                                    //防呆：已刷入SN，再刷SN所在的BOX
                                    string strBOXID = dtTmpData.Rows[i]["BOXID"].ToString().Trim();

                                    drsSelected = dtStorage.Select("BOXID = '" + strBOXID + "' AND SERNO <> ''");

                                    if (drsSelected.Length > 0)
                                    {
                                        Sound.Play(@"Sound\OO.wav");
                                        MessageBox.Show(string.Format("此Box ID：{0} 已经有刷过SN，请确认！", strBOXID), "警告");

                                        txtBoxid.Text = string.Empty;
                                        txtBoxid.Focus();
                                        return;
                                    }

                                    drsSelected = dtStorage.Select("BOXID = '" + strBOXID + "'");

                                    if (drsSelected.Length > 0)
                                    {
                                        Sound.Play(@"Sound\OO.wav");
                                        stsWarning.Text = "刷入的Box ID重複，請確認!!";
                                        txtBoxid.Text = string.Empty;
                                        txtBoxid.Focus();
                                        return;
                                    }

                                    drsOutSource[0]["SCQTY"] = Convert.ToInt32(drsOutSource[0]["SCQTY"]) + Convert.ToInt32(dtTmpData.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;
                                    drsStorageLocation[0]["SCQTY"] = Convert.ToInt32(drsStorageLocation[0]["SCQTY"]) + Convert.ToInt32(dtTmpData.Rows[i]["ALQTY"]);//dtStorage.Select(strSelectConditions).Length;
                                    
                                    //Brian Add 反串库位资料
                                    dtTmpData.Rows[i]["INDAT"] = drsStorageLocation[0]["INDAT"].ToString().Trim();
                                    dtTmpData.Rows[i]["MBLNR"] = drsStorageLocation[0]["MBLNR"].ToString().Trim();

                                    dtStorage.ImportRow(dtTmpData.Rows[i]);
                                }

                                ShowStorageDataGrid();
                                txtBoxid.Text = string.Empty;
                                txtBoxid.Focus();
                            }
                            else
                            {
                                Sound.Play(@"Sound\OO.wav");
                                stsWarning.Text = "刷入的BOXID，备料信息不存在，请确认！！";
                                txtBoxid.Text = string.Empty;
                                txtBoxid.Focus();
                            }
                        }
                        else
                        {
                            Sound.Play(@"Sound\OO.wav");
                            stsWarning.Text = "刷入的BOXID，料号或版本与SAP单据不符，请确认！！";
                            txtBoxid.Text = string.Empty;
                            txtBoxid.Focus();
                        }
                    }
                    else
                    {
                        Sound.Play(@"Sound\OO.wav");
                        stsWarning.Text = "查無此票Box ID，請確認!!";
                        txtBoxid.Text = string.Empty;
                        txtBoxid.Focus();
                        return;
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
}
