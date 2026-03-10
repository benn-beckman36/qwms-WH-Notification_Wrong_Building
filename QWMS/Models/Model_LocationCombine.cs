using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI_QWMS_Models;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;

namespace QWMS.Models
{
    public partial class Model_LocationCombine : Form
    {
        #region Parameters
        private UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strOldLocat = "";
        private string strNewLocat = "";
        private string strProgid = "";
        private string strMatnr = "";
        private string strMblnr = "";
        private string strIsmrg = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private bool bolDuplicate = false;
        private DataTable dtSelect = new DataTable();
        private DataTable dtSource = new DataTable();
        private DataTable dtDestination = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private ModelsData objModelsData;
        private Authority objAuthority;
        private StorageData objStorageData;
        private PlantData objPlantData;
        private StorageIn objStorageIn;
        
        #endregion

        #region Constructor
        public Model_LocationCombine(UserInfo varUserData, string strProgid)
        {
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            objModelsData = new ModelsData(varUserData, Werks, Lgort, Progid);

            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
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

        public string OldLocat
        {
            get
            {
                return strOldLocat;
            }
            set
            {
                strOldLocat = value;
            }
        }

        public string NewLocat
        {
            get
            {
                return strNewLocat;
            }
            set
            {
                strNewLocat = value;
            }
        }

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
            }
        }

        public bool Duplicate
        {
            get
            {
                return bolDuplicate;
            }
            set
            {
                bolDuplicate = value;
            }
        }
        #endregion

        #region ShowStatusData()
        private void ShowStatusData()
        {
            this.tsslDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.tsslMandt.Text = Mandt;
            this.tsslComcd.Text = Comcd;
            this.tsslUsrnm.Text = Usrnm;
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

        #region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            tsslWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                tsslWarning.Text = "";
                Authority objAuthority = new Authority(UserData);
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

        #region Event
        //LocatFrom doubleclick event
        private void txtOldLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                tsslWarning.Text = "";
                strWerks = cmbWerks.Text.ToString().Trim();
                strLgort = cmbLgort.Text.ToString().Trim();
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";
                if (cmbLgort.SelectedIndex != -1)
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    Lgort = "";
                if (string.IsNullOrEmpty(strWerks) || string.IsNullOrEmpty(strLgort))
                {
                    tsslWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "ADD");
                    objManage_LocationSelect.ShowDialog();
                    txtOldLocat.Text = objManage_LocationSelect.Locat;
                    ShowSourceData();
                    txtNewLocat.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                tsslWarning.Text = ex.Message;
                return;
            }
        }
        //LocatTo doubleclick event
        private void txtNewLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                tsslWarning.Text = "";
                strWerks = cmbWerks.Text.ToString().Trim();
                strLgort = cmbLgort.Text.ToString().Trim();
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";

                if (cmbLgort.SelectedIndex != -1)
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    Lgort = "";

                if (strWerks == "" || strLgort == "")
                {
                    tsslWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "ALL");
                    objManage_LocationSelect.ShowDialog();
                    txtNewLocat.Text = objManage_LocationSelect.Locat;
                    ShowDestinationData();
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                tsslWarning.Text = ex.Message;
                return;
            }
        }

        #region ShowSourceDataGrid
        private void ShowSourceDataGrid()
        {
            dgvSource.AutoGenerateColumns = false;
            dgvSource.AllowUserToAddRows = false;
            dgvSource.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "Select";
                dgvcSelect.Name = "Select";
                dgvcSelect.Width = 40;
                dgvSource.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.Name = "MATNR";
                dgvcMatnr.HeaderText = "Model No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvSource.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "Assets No";
                dgvcAssetsNo.ReadOnly = true;
                dgvcAssetsNo.Width = 100;
                dgvSource.Columns.Add(dgvcAssetsNo);

                //客人料號欄位
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvcKdmat.Visible = false;
                dgvSource.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.Name = "RMANO";
                dgvcRmano.HeaderText = "RMA No.";
                dgvcRmano.ReadOnly = true;
                dgvcRmano.Visible = false;
                dgvcRmano.Width = 90;
                dgvSource.Columns.Add(dgvcRmano);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.Name = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Visible = false;
                dgvcInsmk.Width = 50;
                dgvSource.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.Name = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Visible = false;
                dgvcCharg.ReadOnly = true;
                dgvSource.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.Name = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvSource.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.Name = "ALQTY";
                dgvcAlqty.HeaderText = "Out Qty";
                dgvcAlqty.Visible = false;
                dgvcAlqty.ReadOnly = true;
                dgvSource.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "OMBLNR";
                dgvcMblnr.Name = "OMBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Visible = false;
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 100;
                dgvSource.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvceEbeln = new DataGridViewTextBoxColumn();
                dgvceEbeln.DataPropertyName = "EBELN";
                dgvceEbeln.HeaderText = "PO No";
                dgvceEbeln.ReadOnly = true;
                dgvceEbeln.Visible = false;
                dgvceEbeln.Width = 80;
                dgvSource.Columns.Add(dgvceEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Visible = false;
                dgvSource.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                dgvcMrgid.DataPropertyName = "MRGID";
                dgvcMrgid.HeaderText = "Mixed Material ID";
                dgvcMrgid.ReadOnly = true;
                dgvcMrgid.Width = 80;
                dgvcMrgid.Visible = false;
                dgvSource.Columns.Add(dgvcMrgid);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvcRmak1.Visible = false;
                dgvSource.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvSource.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcInspt = new DataGridViewTextBoxColumn();
                dgvcInspt.DataPropertyName = "INSPT";
                dgvcInspt.HeaderText = "Insp.Lot No.";
                dgvcInspt.ReadOnly = true;
                dgvcInspt.Visible = false;
                dgvSource.Columns.Add(dgvcInspt);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "LockCode";
                dgvcLocod.ReadOnly = true;
                dgvcLocod.Visible = false;
                dgvSource.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serial No.";
                dgvcSerno.Name = "SERNO";
                dgvcSerno.ReadOnly = true;
                dgvcSerno.Width = 80;
                dgvcSerno.Visible = false;
                dgvSource.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.Name = "DACOD";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 80;
                dgvcDacod.Visible = false;
                dgvSource.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcPkdat = new DataGridViewTextBoxColumn();
                dgvcPkdat.DataPropertyName = "PKDAT";
                dgvcPkdat.HeaderText = "棧板滿板時間";
                dgvcPkdat.Name = "PKDAT";
                dgvcPkdat.ReadOnly = true;
                dgvcPkdat.Width = 80;
                dgvcPkdat.Visible = false;
                dgvSource.Columns.Add(dgvcPkdat); 

                dgvSource.DataSource = dtSource;
                lblFrom.Text = dtSource.Rows.Count.ToString() + " records";
                if (dtSource.Rows.Count > 0)
                {
                    this.txtNewLocat.Enabled = true;
                }
                else
                {
                    this.txtNewLocat.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }
        #endregion

        #region ShowDestinationDataGrid
        private void ShowDestinationDataGrid()
        {
            dgvDestination.AutoGenerateColumns = false;
            dgvDestination.AllowUserToAddRows = false;
            dgvDestination.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Model No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvDestination.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcAssetsNo = new DataGridViewTextBoxColumn();
                dgvcAssetsNo.DataPropertyName = "AssetsNo";
                dgvcAssetsNo.HeaderText = "Assets No";
                dgvcAssetsNo.ReadOnly = true;
                dgvcAssetsNo.Width = 100;
                dgvDestination.Columns.Add(dgvcAssetsNo);

                //客人料號欄位
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvcKdmat.Visible = false;
                dgvDestination.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No.";
                dgvcRmano.ReadOnly = true;
                dgvcRmano.Width = 110;
                dgvcRmano.Visible = false;
                dgvDestination.Columns.Add(dgvcRmano);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvcInsmk.Visible = false;
                dgvDestination.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Visible = false;
                dgvDestination.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "OMBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvcMblnr.Visible = false;
                dgvDestination.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvceEbeln = new DataGridViewTextBoxColumn();
                dgvceEbeln.DataPropertyName = "EBELN";
                dgvceEbeln.HeaderText = "PO No";
                dgvceEbeln.ReadOnly = true;
                dgvceEbeln.Width = 90;
                dgvceEbeln.Visible = false;
                dgvDestination.Columns.Add(dgvceEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Visible = false;
                dgvDestination.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                dgvcMrgid.DataPropertyName = "MRGID";
                dgvcMrgid.HeaderText = "Mixed Material ID";
                dgvcMrgid.ReadOnly = true;
                dgvcMrgid.Width = 120;
                dgvcMrgid.Visible = false;
                dgvDestination.Columns.Add(dgvcMrgid);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvcRmak1.Visible = false;
                dgvDestination.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcInspt = new DataGridViewTextBoxColumn();
                dgvcInspt.DataPropertyName = "INSPT";
                dgvcInspt.HeaderText = "Insp.Lot No.";
                dgvcInspt.ReadOnly = true;
                dgvcInspt.Visible = false;
                dgvDestination.Columns.Add(dgvcInspt);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "LockCode";
                dgvcLocod.ReadOnly = true;
                dgvcLocod.Visible = false;
                dgvDestination.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serial No.";
                dgvcSerno.ReadOnly = true;
                dgvcSerno.Width = 160;
                dgvcSerno.Visible = false;
                dgvDestination.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 160;
                dgvcDacod.Visible = false;
                dgvDestination.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcPkdat = new DataGridViewTextBoxColumn();
                dgvcPkdat.DataPropertyName = "PKDAT";
                dgvcPkdat.HeaderText = "棧板滿板時間";
                dgvcPkdat.Name = "PKDAT";
                dgvcPkdat.ReadOnly = true;
                dgvcPkdat.Width = 160;
                dgvcPkdat.Visible = false;
                dgvDestination.Columns.Add(dgvcPkdat); 

                dgvDestination.DataSource = dtDestination;
                lblTo.Text = dtDestination.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }
        #endregion

        #region ShowSourceData
        private void ShowSourceData()
        {
            try
            {
                strOldLocat = txtOldLocat.Text.ToString().Trim();
                DataColumn[] dcPrimaryKey = new DataColumn[5];
                objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
                if (strOldLocat == "")
                {
                    tsslWarning.Text = "Please select one location!!";
                    return;
                }
                dtSource = objModelsData.QueryStorageDetailData(OldLocat, strIsmrg);
                if (dtSource.Rows.Count > 0)
                {
                    dcPrimaryKey[0] = dtSource.Columns["MATNR"];
                    dcPrimaryKey[1] = dtSource.Columns["INSMK"];
                    dcPrimaryKey[2] = dtSource.Columns["CHARG"];
                    dcPrimaryKey[3] = dtSource.Columns["OMBLNR"];
                    dcPrimaryKey[4] = dtSource.Columns["SERNO"];
                    dtSource.PrimaryKey = dcPrimaryKey;
                    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                    dtSource.Columns.Add(cSelect);
                    for (int i = 0; i < dtSource.Rows.Count; i++)
                    {
                        dtSource.Rows[i]["Select"] = false;
                    }
                    ShowSourceDataGrid();
                    this.panel1.Enabled = false;
                    this.txtOldLocat.Enabled = false;
                    this.txtNewLocat.Enabled = true;
                    this.txtNewLocat.Focus();
                }
                else
                {
                    tsslWarning.Text = "No storage data!!";
                    this.txtOldLocat.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                tsslWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        # region ShowDestinationData
        private void ShowDestinationData()
        {
            try
            {
                strNewLocat = txtNewLocat.Text.ToString().Trim();
                if (strNewLocat == "")
                {
                    tsslWarning.Text = "Please select To Location!!";
                    return;
                }
                DataColumn[] dcPrimaryKey = new DataColumn[5];
                objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
                dtDestination = objModelsData.QueryStorageDetailData(NewLocat, "");

                dcPrimaryKey[0] = dtDestination.Columns["MATNR"];
                dcPrimaryKey[1] = dtDestination.Columns["INSMK"];
                dcPrimaryKey[2] = dtDestination.Columns["CHARG"];
                dcPrimaryKey[3] = dtDestination.Columns["OMBLNR"];
                dcPrimaryKey[4] = dtDestination.Columns["SERNO"];
                dtDestination.PrimaryKey = dcPrimaryKey;
                ShowDestinationDataGrid();
                this.txtNewLocat.Enabled = false;
            }
            catch (Exception ex)
            {
                tsslWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        //Save event
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strTempMblnr = "";
            DataTable dtTemp = new DataTable();
            DataTable dtTemp1 = new DataTable();
            this.btnSave.Enabled = false;
            strWerks = cmbWerks.Text.ToString().Trim();
            strLgort = cmbLgort.Text.ToString().Trim();
            strOldLocat = txtOldLocat.Text.ToString().Trim();
            strNewLocat = txtNewLocat.Text.ToString().Trim();
            objPlantData = new PlantData(UserData);
            GetSelectedRows(dgvSource);
            if (string.IsNullOrEmpty(strWerks)||string.IsNullOrEmpty(strLgort))
            {
                tsslWarning.Text = "Plant and Storage can't be empty!!";
                return;
            }
            if (string.IsNullOrEmpty(strOldLocat) || string.IsNullOrEmpty(strNewLocat))
            {
                tsslWarning.Text = "From Location and To Location can't be empty!!";
                return;
            }
            if (strOldLocat == strNewLocat)
            {
                tsslWarning.Text = "From Location and To Location can't be the same!!";
                return;
            }
            #region 逻辑验证
            //验证Storage和Location类型
            dtTemp = objPlantData.GetPlantStorageData("LGORT", Werks, Lgort);
            if (dtTemp.Rows.Count > 0)
            {
                strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
            }
            else
            {
                tsslWarning.Text = "Can't find the storage data!!";
                return;
            }
            //固定储位需检查要转移资料在新储位是否有设定对应资料
            if (strLotyp.ToUpper() == "FIXED LOCATION" && strIsmrg == "Y")
            {
                tsslWarning.Text = "You can't merge mixed material because " + Lgort + " is a fixed-location storage!!";
                return;
            }
            if (dtSource.Rows.Count == 0)
            {
                tsslWarning.Text = "The storage of source location is empty!!";
                this.txtOldLocat.Focus();
                return;
            }
            if (!objPlantData.CheckExistedStorageData(Werks, Lgort, NewLocat))
            {
                tsslWarning.Text = "The destination location doesn't exist!!";
                this.txtNewLocat.Focus();
                return;
            }
            if (dtSelect.Rows.Count == 0)
            {
                tsslWarning.Text = "Please select the data in the source location(left table)!!";
                return;
            }
            #endregion
            //for (int i = 0; i < dtSelect.Rows.Count; i++)
            //{
            //    if (strTempMblnr.IndexOf(dtSelect.Rows[i]["OMBLNR"].ToString()) == -1)
            //    {
            //        strTempMblnr += dtSelect.Rows[i]["OMBLNR"].ToString() + ";";
            //        dtTemp = objStorageData.QueryNotCombineInData(NewLocat, dtSelect.Rows[i]["OMBLNR"].ToString(), "0");
            //        if (dtTemp.Rows.Count > 0)
            //        {
            //            tsslWarning.Text = "The Document No in destination location was stored in with mixed material last time!!";
            //            return;
            //        }
            //    }
            //    //檢查要新儲位是否有相同的料號但不同連板資料(包含單板)
            //    if (objStorageData.CheckExistedSameMaterial(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["MRGID"].ToString()))
            //    {
            //        tsslWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
            //        return;
            //    }
            //    //檢查要入庫的儲位是否已經有相同料號但不同版本)
            //    if (objStorageData.CheckExistedSameMaterial(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString()))
            //    {
            //        tsslWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
            //        return;
            //    }
            //}
            #region 检查并储的数量是否大于0
            for (int i = 0; i < dtSelect.Rows.Count; i++)
            {
                if (int.Parse(dtSelect.Rows[i]["ALQTY"].ToString()) <= 0)
                {
                    tsslWarning.Text = "The Location of " + dtSelect.Rows[i]["LOCAT"].ToString() + " had zero quantatity to combine. Please Check it!!";
                    return;
                }
            }
            #endregion

            objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
            objStorageIn = new StorageIn(UserData, Werks, Lgort, "", Progid);
            if (objStorageIn.AddLocationCombineData(OldLocat, NewLocat, dtSelect))
            {
                tsslWarning.Text = "Update OK!";
            dtDestination = objModelsData.QueryStorageDetailData(NewLocat, "");
                ShowDestinationDataGrid();
                dtSource = objModelsData.QueryStorageDetailData(OldLocat, strIsmrg);
                ShowSourceDataGrid();
                btnSave.Enabled = false;
            }
            else
            {
                tsslWarning.Text = "Update fail!! " + objStorageIn.ERRMSG;
                return;
            }
        }

        #region GetSelectedRows
        public void GetSelectedRows(DataGridView dgv)
        {
            DataRow drRow;
            DataRow[] foundRow;
            dtSelect = new DataTable();
            dtSelect.Columns.Add("MANDT", Type.GetType("System.String"));
            dtSelect.Columns.Add("COMCD", Type.GetType("System.String"));
            dtSelect.Columns.Add("WERKS", Type.GetType("System.String"));
            dtSelect.Columns.Add("LGORT", Type.GetType("System.String"));
            dtSelect.Columns.Add("LOCAT", Type.GetType("System.String"));
            dtSelect.Columns.Add("MATNR", Type.GetType("System.String"));
            dtSelect.Columns.Add("INSMK", Type.GetType("System.String"));
            dtSelect.Columns.Add("CHARG", Type.GetType("System.String"));
            dtSelect.Columns.Add("MENGE", Type.GetType("System.String"));
            dtSelect.Columns.Add("ALQTY", Type.GetType("System.String"));
            dtSelect.Columns.Add("QCQTY", Type.GetType("System.String"));
            dtSelect.Columns.Add("MBLNR", Type.GetType("System.String"));
            dtSelect.Columns.Add("ZEILE", Type.GetType("System.String"));
            dtSelect.Columns.Add("REFNO", Type.GetType("System.String"));
            dtSelect.Columns.Add("EBELN", Type.GetType("System.String"));
            dtSelect.Columns.Add("LIFNR", Type.GetType("System.String"));
            dtSelect.Columns.Add("OMBLNR", Type.GetType("System.String"));
            dtSelect.Columns.Add("MRGID", Type.GetType("System.String"));
            dtSelect.Columns.Add("KOSTL", Type.GetType("System.String"));
            dtSelect.Columns.Add("ARBPL", Type.GetType("System.String"));
            dtSelect.Columns.Add("TRNTP", Type.GetType("System.String"));
            dtSelect.Columns.Add("RMAK1", Type.GetType("System.String"));
            dtSelect.Columns.Add("INDAT", Type.GetType("System.String"));
            dtSelect.Columns.Add("VEDAT", Type.GetType("System.String"));
            dtSelect.Columns.Add("CRDAT", Type.GetType("System.String"));
            dtSelect.Columns.Add("KDMAT", Type.GetType("System.String"));
            dtSelect.Columns.Add("SERNO", Type.GetType("System.String"));
            dtSelect.Columns.Add("LOCOD", Type.GetType("System.String"));
            dtSelect.Columns.Add("INSPT", Type.GetType("System.String"));
            dtSelect.Columns.Add("DACOD", Type.GetType("System.String"));
            dtSelect.Columns.Add("BOXID", Type.GetType("System.String"));
            dtSelect.Columns.Add("RMANO", Type.GetType("System.String"));
            dtSelect.Columns.Add("PKDAT", Type.GetType("System.String"));

            foreach (DataGridViewRow dgvr in dgv.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Select"].Value) == true)
                {
                    dgvr.Cells["MATNR"].Value.ToString();
                    dgvr.Cells["INSMK"].Value.ToString();
                    foundRow = dtSource.Select("MATNR='" + dgvr.Cells["MATNR"].Value.ToString() + "' and INSMK='" + dgvr.Cells["INSMK"].Value.ToString() + "' and CHARG='" + dgvr.Cells["CHARG"].Value.ToString() + "' and OMBLNR='" + dgvr.Cells["OMBLNR"].Value.ToString() + "' and SERNO='" + dgvr.Cells["SERNO"].Value.ToString() + "'");
                    for (int j = 0; j < foundRow.Length; j++)
                    {
                        drRow = dtSelect.NewRow();
                        drRow["MANDT"] = foundRow[j]["MANDT"].ToString();
                        drRow["COMCD"] = foundRow[j]["COMCD"].ToString();
                        drRow["WERKS"] = foundRow[j]["WERKS"].ToString();
                        drRow["LGORT"] = foundRow[j]["LGORT"].ToString();
                        drRow["LOCAT"] = foundRow[j]["LOCAT"].ToString();
                        drRow["MATNR"] = foundRow[j]["MATNR"].ToString();
                        drRow["INSMK"] = foundRow[j]["INSMK"].ToString();
                        drRow["CHARG"] = foundRow[j]["CHARG"].ToString();
                        drRow["MENGE"] = foundRow[j]["MENGE"].ToString();
                        drRow["ALQTY"] = foundRow[j]["ALQTY"].ToString();
                        drRow["QCQTY"] = foundRow[j]["QCQTY"].ToString();
                        drRow["MBLNR"] = foundRow[j]["MBLNR"].ToString();
                        drRow["ZEILE"] = foundRow[j]["ZEILE"].ToString();
                        drRow["REFNO"] = foundRow[j]["REFNO"].ToString();
                        drRow["EBELN"] = foundRow[j]["EBELN"].ToString();
                        drRow["LIFNR"] = foundRow[j]["LIFNR"].ToString();
                        drRow["OMBLNR"] = foundRow[j]["OMBLNR"].ToString();
                        drRow["MRGID"] = foundRow[j]["MRGID"].ToString();
                        drRow["KOSTL"] = foundRow[j]["KOSTL"].ToString();
                        drRow["ARBPL"] = foundRow[j]["ARBPL"].ToString();
                        drRow["TRNTP"] = foundRow[j]["TRNTP"].ToString();
                        drRow["RMAK1"] = foundRow[j]["RMAK1"].ToString();
                        drRow["INDAT"] = foundRow[j]["INDAT"].ToString();
                        drRow["VEDAT"] = foundRow[j]["VEDAT"].ToString();
                        drRow["CRDAT"] = foundRow[j]["CRDAT"].ToString();
                        drRow["KDMAT"] = foundRow[j]["KDMAT"].ToString();
                        drRow["SERNO"] = foundRow[j]["SERNO"].ToString();
                        drRow["LOCOD"] = foundRow[j]["LOCOD"].ToString();
                        drRow["INSPT"] = foundRow[j]["INSPT"].ToString();
                        drRow["DACOD"] = foundRow[j]["DACOD"].ToString();
                        drRow["BOXID"] = foundRow[j]["BOXID"].ToString();
                        drRow["RMANO"] = foundRow[j]["RMANO"].ToString();
                        drRow["PKDAT"] = foundRow[j]["PKDAT"].ToString();
                        dtSelect.Rows.Add(drRow);
                    }
                }
            }
        }
        #endregion

        //Refresh event
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.panel1.Enabled = true;
            this.cmbWerks.SelectedIndex = -1;
            this.cmbLgort.SelectedIndex = -1;
            this.txtOldLocat.Text = "";
            this.txtOldLocat.Enabled = true;
            this.txtNewLocat.Text = "";
            this.txtNewLocat.Enabled = false;
            this.btnSave.Enabled = false;
            this.dgvSource.DataSource = null;
            this.dgvDestination.DataSource = null;
        }
        //Exit event
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Resize event
        private void Model_LocationCombine_Resize(object sender, EventArgs e)
        {
            panel2.Size = new System.Drawing.Size((int)(this.Size.Width * 0.5), panel2.Size.Height);
            if (this.Size.Width - tsslMandt.Width - tsslComcd.Width - this.tsslUsrnm.Width - this.tsslDate.Width - 40 > 0)
                tsslWarning.Width = this.Size.Width - tsslMandt.Width - tsslComcd.Width - this.tsslUsrnm.Width - this.tsslDate.Width - 40;
        }
        //Locat From EnterPress event
        private void txtOldLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    tsslWarning.Text = "";
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
                        tsslWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                    ShowSourceData();
                    txtNewLocat.Enabled = true;
                }
                catch (Exception ex)
                {
                    tsslWarning.Text = ex.Message;
                    return;
                }
            }
        }
        //Locat To EnterPress event
        private void txtNewLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    tsslWarning.Text = "";
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
                        tsslWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                    ShowDestinationData();
                    btnSave.Enabled = true;
                }
                catch (Exception ex)
                {
                    tsslWarning.Text = ex.Message;
                    return;
                }
            }
        }

        //dgvSource RowHeaderClick
        private void dgvSource_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tsslWarning.Text = "";
            string strMenge = "0";
            string strAlqty = "0";
            try
            {
                strMenge = dgvSource.Rows[e.RowIndex].Cells[6].Value.ToString();
                strAlqty = dgvSource.Rows[e.RowIndex].Cells[7].Value.ToString();
                Manage_LocationCombine_Detail objManage_LocationCombine_Detail = new Manage_LocationCombine_Detail(UserData, Progid, Werks, Lgort);
                objManage_LocationCombine_Detail.Menge = strMenge;
                objManage_LocationCombine_Detail.Alqty = strAlqty;
                objManage_LocationCombine_Detail.ShowDialog();
                dgvSource.Rows[e.RowIndex].Cells[7].Value = objManage_LocationCombine_Detail.Alqty;
                ShowSourceDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        # region Select All/Clear All
        private void SelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvSource.Rows)
            {
                dgvr.Cells["Select"].Value = true;
            }
        }
        
        private void ClearAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvSource.Rows)
            {
                dgvr.Cells["Select"].Value = false;
            }
        }
        # endregion

        #region 20130808 連板料號檢查及訊息提示--Gary

        private void dgvSource_CellContentClick_1(object sender, DataGridViewCellEventArgs e)//fire dgvSource_CellValueChanged
        {
            dgvSource.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvSource_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)//checkbox欄被改變
            {
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                DataGridViewTextBoxCell txt1 = new DataGridViewTextBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvSource.Rows[dgvSource.CurrentRow.Index].Cells[0];
                txt1 = (DataGridViewTextBoxCell)dgvSource.Rows[dgvSource.CurrentRow.Index].Cells[1];

                if (ch1.Value.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase))//checkbox被勾選
                {
                    DataTable dtData = new DataTable();
                    DataTable dtData2 = new DataTable();
                    string MATNR = "";
                    string MRGID = "";
                    string MixMessage = "";
                    StorageData objStorageData1 = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort);

                    MATNR = txt1.Value.ToString();
                    dtData = objStorageData1.QueryMRGID(MATNR);//查詢目前勾選料件,所有的MRGID(多個ID情況:組合材料1-->A+B,組合材料2-->A+E,A會有兩個ID)

                    if (dtData.Rows.Count != 0)
                    {
                        //針對每個MRGID,抓出不包括目前勾選料件的料號清單,並寫入提示訊息
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            MRGID = dtData.Rows[i][0].ToString();
                            dtData2 = objStorageData1.QueryMix(MATNR, MRGID);

                            for (int j = 0; j < dtData2.Rows.Count; j++)
                            {
                                if (j == 0)//第一列
                                {
                                    if (dtData2.Rows.Count == 1)
                                        MixMessage = MixMessage + "MixedMaterial " + (i + 1) + " : " + MATNR + " + " + dtData2.Rows[j][0].ToString() + "\n";
                                    else
                                        MixMessage = MixMessage + "MixedMaterial " + (i + 1) + " : " + MATNR + " + " + dtData2.Rows[j][0].ToString() + " + ";
                                }
                                else
                                {
                                    if (!(j == dtData2.Rows.Count - 1))//非最後一列                                       
                                        MixMessage = MixMessage + dtData2.Rows[j][0].ToString() + " + ";
                                    else//最後一列                                                              
                                        MixMessage = MixMessage + dtData2.Rows[j][0].ToString() + "\n";
                                }
                            }
                        }

                        MessageBox.Show(MixMessage, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        #endregion
    }
}
