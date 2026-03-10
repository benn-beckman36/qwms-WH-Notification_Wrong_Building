using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Manage_LocationCombine : Form
    {
        # region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strIsmrg = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private bool bolDuplicate = false;
        private DataTable dtSource = new DataTable();
        private DataTable dtDestination = new DataTable();
        private DataTable dtSelect = new DataTable();
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private StorageData objStorageData;
        private MixedMaterial objMixedMaterial;

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

        public string NewLocat
        {
            get
            {
                return txtNewLocat.Text.Trim();
            }
            set
            {
                txtNewLocat.Text = value;
            }
        }

        public string OldLocat
        {
            get
            {
                return txtOldLocat.Text.Trim();
            }
            set
            {
                txtOldLocat.Text = value;
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
        # endregion


        public Manage_LocationCombine(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objStorageIn = new StorageIn(UserData, Progid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);

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
                    bolDuplicate = objStorageIn.CheckDuplicatLocat();
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

        # region ShowDdlLgort
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

        # region From Location Double Click
        private void txtOldLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
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
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "ADD");
                    objManage_LocationSelect.ShowDialog();
                    txtOldLocat.Text = objManage_LocationSelect.Locat;
                    ShowSourceData();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region To Location Double Click
        private void txtNewLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
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
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, Werks, Lgort, "ALL");
                    objManage_LocationSelect.ShowDialog();
                    txtNewLocat.Text = objManage_LocationSelect.Locat;
                    ShowDestinationData();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region ShowSourceDataGrid
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
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 130;
                dgvSource.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.Name = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvSource.Columns.Add(dgvcCharg);

                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                if (objStorageIn.CheckPlantInType(strWerks,  "Config FIFO"))
                {
                    DataGridViewTextBoxColumn dgvcConfig = new DataGridViewTextBoxColumn();
                    dgvcConfig.DataPropertyName = "CONFIG";
                    dgvcConfig.Name = "CONFIG";
                    dgvcConfig.HeaderText = "CFG Name";
                    dgvcConfig.ReadOnly = true;
                    dgvSource.Columns.Add(dgvcConfig);
                }

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
                dgvcAlqty.ReadOnly = true;
                dgvSource.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.Name = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvSource.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcDecite = new DataGridViewTextBoxColumn();
                dgvcDecite.DataPropertyName = "DECITEM";
                dgvcDecite.HeaderText = "Decitem";
                dgvcDecite.Name = "DECITEM";
                dgvcDecite.ReadOnly = true;
                dgvcDecite.Width = 160;
                dgvSource.Columns.Add(dgvcDecite);

                //客人料號欄位
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvSource.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.Name = "RMANO";
                dgvcRmano.HeaderText = "RMA No.";
                dgvcRmano.ReadOnly = true;
                dgvcRmano.Width = 110;
                dgvSource.Columns.Add(dgvcRmano);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "OMBLNR";
                dgvcMblnr.Name = "OMBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvSource.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcLocked = new DataGridViewTextBoxColumn();
                dgvcLocked.DataPropertyName = "LOCKED";
                dgvcLocked.Name = "LOCKED";
                dgvcLocked.HeaderText = "L-STATUS";
                dgvcLocked.ReadOnly = true;
                dgvcLocked.Width = 90;
                dgvSource.Columns.Add(dgvcLocked);

                DataGridViewTextBoxColumn dgvceEbeln = new DataGridViewTextBoxColumn();
                dgvceEbeln.DataPropertyName = "EBELN";
                dgvceEbeln.HeaderText = "PO No";
                dgvceEbeln.ReadOnly = true;
                dgvceEbeln.Width = 90;
                dgvSource.Columns.Add(dgvceEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvSource.Columns.Add(dgvcLifnr);

                //DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                //dgvcMrgid.DataPropertyName = "MRGID";
                //dgvcMrgid.HeaderText = "Mixed Material ID";
                //dgvcMrgid.ReadOnly = true;
                //dgvcMrgid.Width = 120;
                //dgvSource.Columns.Add(dgvcMrgid);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvSource.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvSource.Columns.Add(dgvcIndat);

                //DataGridViewTextBoxColumn dgvcInspt = new DataGridViewTextBoxColumn();
                //dgvcInspt.DataPropertyName = "INSPT";
                //dgvcInspt.HeaderText = "Insp.Lot No.";
                //dgvcInspt.ReadOnly = true;
                //dgvSource.Columns.Add(dgvcInspt);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "Lot Code";
                dgvcLocod.Name = "LOCOD";
                dgvcLocod.ReadOnly = true;
                dgvSource.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serial No.";
                dgvcSerno.Name = "SERNO";
                dgvcSerno.ReadOnly = true;
                dgvcSerno.Width = 160;
                dgvSource.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.Name = "DACOD";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 160;
                dgvSource.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcExpiryDate = new DataGridViewTextBoxColumn();
                dgvcExpiryDate.DataPropertyName = "ExpiryDate";
                dgvcExpiryDate.HeaderText = "Exp. Date";
                dgvcExpiryDate.ReadOnly = true;
                dgvcExpiryDate.Width = 160;
                dgvSource.Columns.Add(dgvcExpiryDate);

                DataGridViewTextBoxColumn dgvcMAXEXP = new DataGridViewTextBoxColumn();
                dgvcMAXEXP.DataPropertyName = "MAXEXP";
                dgvcMAXEXP.HeaderText = "Max Exp. Date";
                dgvcMAXEXP.ReadOnly = true;
                dgvcMAXEXP.Width = 160;
                dgvSource.Columns.Add(dgvcMAXEXP);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "再检批号";
                dgvcTASKID.ReadOnly = true;
                dgvcTASKID.Width = 160;
                dgvSource.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcPkdat = new DataGridViewTextBoxColumn();
                dgvcPkdat.DataPropertyName = "PKDAT";
                dgvcPkdat.HeaderText = "棧板滿板時間";
                dgvcPkdat.Name = "PKDAT";
                dgvcPkdat.ReadOnly = true;
                dgvcPkdat.Width = 160;
                dgvSource.Columns.Add(dgvcPkdat);



                dgvSource.DataSource = dtSource;
                lblCount.Text = dtSource.Rows.Count.ToString() + " records";

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
        # endregion

        # region ShowDestinationDataGrid
        private void ShowDestinationDataGrid()
        {
            dgvDestination.AutoGenerateColumns = false;
            dgvDestination.Columns.Clear();

            try
            {
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 130;
                dgvDestination.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcCharg);

                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                if (objStorageIn.CheckPlantInType(strWerks, "Config FIFO"))
                {
                    DataGridViewTextBoxColumn dgvcConfig = new DataGridViewTextBoxColumn();
                    dgvcConfig.DataPropertyName = "CONFIG";
                    dgvcConfig.Name = "CONFIG";
                    dgvcConfig.HeaderText = "CFG Name";
                    dgvcConfig.ReadOnly = true;
                    dgvDestination.Columns.Add(dgvcConfig);
                }

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvDestination.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcDecitem = new DataGridViewTextBoxColumn();
                dgvcDecitem.DataPropertyName = "DECITEM";
                dgvcDecitem.HeaderText = "Decitem";
                dgvcDecitem.Name = "DECITEM";
                dgvcDecitem.ReadOnly = true;
                dgvcDecitem.Width = 160;
                dgvDestination.Columns.Add(dgvcDecitem);

                //客人料號欄位
                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMA No.";
                dgvcRmano.ReadOnly = true;
                dgvcRmano.Width = 110;
                dgvDestination.Columns.Add(dgvcRmano);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "OMBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvDestination.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvceLocked = new DataGridViewTextBoxColumn();
                dgvceLocked.DataPropertyName = "LOCKED";
                dgvceLocked.HeaderText = "L-STATUS";
                dgvceLocked.ReadOnly = true;
                dgvceLocked.Width = 90;
                dgvDestination.Columns.Add(dgvceLocked);

                DataGridViewTextBoxColumn dgvceEbeln = new DataGridViewTextBoxColumn();
                dgvceEbeln.DataPropertyName = "EBELN";
                dgvceEbeln.HeaderText = "PO No";
                dgvceEbeln.ReadOnly = true;
                dgvceEbeln.Width = 90;
                dgvDestination.Columns.Add(dgvceEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcLifnr);

                //DataGridViewTextBoxColumn dgvcMrgid = new DataGridViewTextBoxColumn();
                //dgvcMrgid.DataPropertyName = "MRGID";
                //dgvcMrgid.HeaderText = "Mixed Material ID";
                //dgvcMrgid.ReadOnly = true;
                //dgvcMrgid.Width = 120;
                //dgvDestination.Columns.Add(dgvcMrgid);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcIndat);

                //DataGridViewTextBoxColumn dgvcInspt = new DataGridViewTextBoxColumn();
                //dgvcInspt.DataPropertyName = "INSPT";
                //dgvcInspt.HeaderText = "Insp.Lot No.";
                //dgvcInspt.ReadOnly = true;
                //dgvDestination.Columns.Add(dgvcInspt);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "LOCOD";
                dgvcLocod.HeaderText = "Lot Code";
                dgvcLocod.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcSerno = new DataGridViewTextBoxColumn();
                dgvcSerno.DataPropertyName = "SERNO";
                dgvcSerno.HeaderText = "Serial No.";
                dgvcSerno.ReadOnly = true;
                dgvcSerno.Width = 160;
                dgvDestination.Columns.Add(dgvcSerno);

                DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                dgvcDacod.DataPropertyName = "DACOD";
                dgvcDacod.HeaderText = "DateCode";
                dgvcDacod.ReadOnly = true;
                dgvcDacod.Width = 160;
                dgvDestination.Columns.Add(dgvcDacod);

                DataGridViewTextBoxColumn dgvcExpiryDate = new DataGridViewTextBoxColumn();
                dgvcExpiryDate.DataPropertyName = "ExpiryDate";
                dgvcExpiryDate.HeaderText = "Exp. Date";
                dgvcExpiryDate.ReadOnly = true;
                dgvcExpiryDate.Width = 160;
                dgvDestination.Columns.Add(dgvcExpiryDate);

                DataGridViewTextBoxColumn dgvcMAXEXP = new DataGridViewTextBoxColumn();
                dgvcMAXEXP.DataPropertyName = "MAXEXP";
                dgvcMAXEXP.HeaderText = "Max Exp. Date";
                dgvcMAXEXP.ReadOnly = true;
                dgvcMAXEXP.Width = 160;
                dgvDestination.Columns.Add(dgvcMAXEXP);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "再检批号";
                dgvcTASKID.ReadOnly = true;
                dgvcTASKID.Width = 160;
                dgvDestination.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcPkdat = new DataGridViewTextBoxColumn();
                dgvcPkdat.DataPropertyName = "PKDAT";
                dgvcPkdat.HeaderText = "棧板滿板時間";
                dgvcPkdat.Name = "PKDAT";
                dgvcPkdat.ReadOnly = true;
                dgvcPkdat.Width = 160;
                dgvDestination.Columns.Add(dgvcPkdat);


                dgvDestination.DataSource = dtDestination;
                lblCount1.Text = dtDestination.Rows.Count.ToString() + " records";

                if (dtDestination.Rows.Count > 0)
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDestinationDataGrid()");
            }
        }
        # endregion

        # region ShowSourceData
        private void ShowSourceData()
        {
            try
            {
                DataColumn[] dcPrimaryKey = new DataColumn[7];
                objStorageData = new StorageData(UserData, Werks, Lgort);
                if (this.chkMixed.Checked)
                {
                    strIsmrg = "Y";
                }
                else
                {
                    strIsmrg = "N";
                }
                if (OldLocat == "")
                {
                    stsWarning.Text = "Please select one location!!";
                    return;
                }
                dtSource = objStorageData.QueryStorageDetailData(OldLocat, strIsmrg);
                if (dtSource.Rows.Count > 0)
                {

                    dcPrimaryKey[0] = dtSource.Columns["MATNR"];
                    dcPrimaryKey[1] = dtSource.Columns["INSMK"];
                    dcPrimaryKey[2] = dtSource.Columns["CHARG"];
                    dcPrimaryKey[3] = dtSource.Columns["OMBLNR"];
                    dcPrimaryKey[4] = dtSource.Columns["SERNO"];
                    dcPrimaryKey[5] = dtSource.Columns["DACOD"];
                    dcPrimaryKey[6] = dtSource.Columns["LOCOD"];
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
                    stsWarning.Text = "No storage data!!";
                    this.txtOldLocat.Focus();
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

        # region ShowDestinationData
        private void ShowDestinationData()
        {
            try
            {
                if (NewLocat == "")
                {
                    stsWarning.Text = "Please select destination!!";
                    return;
                }
                DataColumn[] dcPrimaryKey = new DataColumn[7];
                objStorageData = new StorageData(UserData, Werks, Lgort);
                dtDestination = objStorageData.QueryStorageDetailData(NewLocat, "");
                dcPrimaryKey[0] = dtDestination.Columns["MATNR"];
                dcPrimaryKey[1] = dtDestination.Columns["INSMK"];
                dcPrimaryKey[2] = dtDestination.Columns["CHARG"];
                dcPrimaryKey[3] = dtDestination.Columns["OMBLNR"];
                dcPrimaryKey[4] = dtDestination.Columns["SERNO"];
                dcPrimaryKey[5] = dtDestination.Columns["DACOD"];
                dcPrimaryKey[6] = dtDestination.Columns["LOCOD"];
                dtDestination.PrimaryKey = dcPrimaryKey;
                ShowDestinationDataGrid();
                this.txtNewLocat.Enabled = false;

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strTempMixedMaterial = "";
            string strTempMatnr = "";
            string strTempMblnr = "";
            string strMrgid = "";
            ArrayList aryMixedMaterial = new ArrayList();
            DataTable dtTemp = new DataTable();
            DataTable dtTemp1 = new DataTable();
            stsWarning.Text = "";
            DataRow[] foundRow;
            objStorageData = new StorageData(UserData, Werks, Lgort);

            GetSelectedRows(dgvSource);
            objMixedMaterial = new MixedMaterial(UserData, Werks);
            if (cmbWerks.SelectedIndex != -1)
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            else
                strWerks = "";

            if (cmbLgort.SelectedIndex != -1)
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            else
                strLgort = "";

            # region 取得Sttyp及Lotyp
            //取得Sttyp及Lotyp
            dtTemp = objPlantData.GetPlantStorageData("LGORT", Werks, Lgort);
            if (dtTemp.Rows.Count >= 1)
            {
                strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
            }
            else
            {
                stsWarning.Text = "Can't find the storage data!!";
                return;
            }
            # endregion

            # region 逻辑验证
            //固定儲位虛檢查要移轉料號在新儲位是否有設定對應資料
            if (strLotyp.ToUpper() == "FIXED LOCATION" && strIsmrg == "Y")
            {
                stsWarning.Text = "You can't merge mixed material because " + Lgort + " is a fixed-location storage!!";
                return;
            }
            if (OldLocat == "" || NewLocat == "")
            {
                stsWarning.Text = "Please input location!!";
                return;
            }
            if (OldLocat == NewLocat)
            {
                stsWarning.Text = "Source location and destination location can't be the same!!";
                return;
            }
            if (dtSource.Rows.Count == 0)
            {
                stsWarning.Text = "The storage of source location is empty!!";
                this.txtOldLocat.Focus();
                return;
            }
            if (!objPlantData.CheckExistedStorageData(Werks, Lgort, NewLocat))
            {
                stsWarning.Text = "The destination location doesn't exist!!";
                this.txtNewLocat.Focus();
                return;
            }
            if (dtSelect.Rows.Count == 0)
            {
                stsWarning.Text = "Please select the data in the source location(left table)!!";
                return;
            }
            # endregion

            if (strIsmrg == "Y")
            {
                # region 如为连板料号
                for (int i = 0; i < dtSelect.Rows.Count; i++)
                {
                    if (strTempMixedMaterial.IndexOf(dtSelect.Rows[i]["MRGID"].ToString()) == -1)
                    {
                        aryMixedMaterial.Clear();
                        strTempMatnr = "";
                        strMrgid = dtSelect.Rows[i]["MRGID"].ToString();
                        strTempMixedMaterial += strMrgid + ";";

                        foundRow = dtSelect.Select("MRGID='" + strMrgid + "'");
                        for (int j = 0; j < foundRow.Length; j++)
                        {
                            if (strTempMatnr.IndexOf(foundRow[j]["MATNR"].ToString()) == -1)
                            {
                                aryMixedMaterial.Add(foundRow[j]["MATNR"].ToString());
                            }
                            //檢查數量有沒有相同
                            if (foundRow[j]["ALQTY"].ToString() != foundRow[0]["ALQTY"].ToString())
                            {
                                stsWarning.Text = "The Qty of mixed material of source location is not the same!!";
                                return;
                            }

                        }
                        //檢查所選的料號是不是等於連板編號的料號組合
                        if (objMixedMaterial.QueryMixedMaterialID(aryMixedMaterial) != strMrgid)
                        {
                            stsWarning.Text = "The materials you select in source location is not mixed material!!";
                            return;
                        }
                        //檢查所選的料號是不是在儲位中已經存在另一種連板料號組合
                        if (objMixedMaterial.QueryExistDifferentMixedMaterial(Lgort, NewLocat, strMrgid))
                        {
                            stsWarning.Text = "The destination location has existed another mixed material!!";
                            return;
                        }
                    }
                    //檢查新儲位是否有同扣帳編號但之前卻使用非連板方式入庫
                    if (strTempMblnr.IndexOf(dtSelect.Rows[i]["OMBLNR"].ToString()) == -1)
                    {
                        strTempMblnr += dtSelect.Rows[i]["OMBLNR"].ToString() + ";";
                        dtTemp = objStorageData.QueryNotCombineInData(NewLocat, dtSelect.Rows[i]["OMBLNR"].ToString(), "1");
                        if (dtTemp.Rows.Count > 0)
                        {
                            stsWarning.Text = "The Document No in destination location was not stored in with mixed material last time!!";
                            return;
                        }
                    }
                    //檢查要新儲位是否有相同的料號但不同連板資料(包含單板)
                    if (objStorageData.CheckExistedSameMaterial(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["MRGID"].ToString()))
                    {
                        stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
                        return;
                    }
                    //如果可以允許同一儲位置放不同版本的料號, Kent 20050130
                    if (this.Duplicate == false)
                    {
                        //檢查要入庫的儲位是否已經有相同料號但不同版本)
                        if (objStorageData.CheckExistedSameMaterial(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
                            return;
                        }
                    }

                    #region 如果有勾選就檢查版本(Charg)、PO No.(EBELN)與客人料號(KDMAT)  Smose Liao 20100617
                    if (chkEbelnKdmat.Checked == true)
                    {
                        //檢查要入庫的儲位是否已經有相同料號但不同版本)
                        if (objStorageData.CheckExistedSameMaterial(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的版本!!";
                            return;
                        }

                        //檢查要入庫的儲位是否已經有相同料號但不同的PO No.)  
                        if (objStorageData.CheckExistedSameEbeln(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString(), dtSelect.Rows[i]["EBELN"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的PO No.!!";
                            return;
                        }

                        //檢查要入庫的儲位是否已經有相同料號但不同的客人料號)  
                        if (objStorageData.CheckExistedSameKdmat(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString(), dtSelect.Rows[i]["KDMAT"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的客人料號!!";
                            return;
                        }
                    }
                    #endregion
                }
                # endregion
            }
            else
            {
                # region 非连板料号
                for (int i = 0; i < dtSelect.Rows.Count; i++)
                {
                    //固定儲位虛檢查要移轉料號在新儲位是否有設定對應資料
                    if (strLotyp.ToUpper() == "FIXED LOCATION")
                    {
                        dtTemp1 = objPlantData.QueryMappingData(Werks, Lgort, NewLocat, dtSelect.Rows[i]["MATNR"].ToString());
                        if (dtTemp1.Rows.Count == 0)
                        {
                            stsWarning.Text = "You can't move " + dtSelect.Rows[i]["MATNR"].ToString() + " because it doesn't map to new location!!";
                            return;
                        }
                    }
                    //檢查新儲位是否有同扣帳編號但之前卻使用連板方式入庫
                    if (strTempMblnr.IndexOf(dtSelect.Rows[i]["OMBLNR"].ToString()) == -1)
                    {
                        strTempMblnr += dtSelect.Rows[i]["OMBLNR"].ToString() + ";";
                        dtTemp = objStorageData.QueryNotCombineInData(NewLocat, dtSelect.Rows[i]["OMBLNR"].ToString(), "0");
                        if (dtTemp.Rows.Count > 0)
                        {
                            stsWarning.Text = "The Document No in destination location was stored in with mixed material last time!!";
                            return;
                        }
                    }
                    //檢查要新儲位是否有相同的料號但不同連板資料(包含單板)
                    if (objStorageData.CheckExistedSameMaterial(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["MRGID"].ToString()))
                    {
                        stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
                        return;
                    }
                    //如果可以允許同一儲位置放不同版本的料號, Kent 20050130
                    if (this.Duplicate == false)
                    {
                        //檢查要入庫的儲位是否已經有相同料號但不同版本)
                        if (Lgort.Substring(0, 2) != "SC") //SC开头废品仓不做管控
                        {
                            if (objStorageData.CheckExistedSameMaterial(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(),
                                dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString()))
                            {
                                stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() +
                                                  " has existed in the location with different version!";
                                return;
                            }
                        }
                    }

                    //MLB同一栈板同料号、同版本、不同Pallet ID入库/并储修改卡控 Bill Yang(杨明朝)\王正
                    if ((strWerks == "CS30 " || strWerks == "CS41" || strWerks == "CS51" || strWerks == "CS50" || strWerks == "CS31" || strWerks == "CS52") && strLgort == "TW30")
                    {
                        if (objStorageData.CheckExistedSamePalMblnr(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["OMBLNR"].ToString(), dtSelect.Rows[i]["CHARG"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + "-" + dtSelect.Rows[i]["CHARG"].ToString() + " has existed in the location with different PalletID!";
                            return;
                        }
                    }


                    #region 如果有勾選就檢查版本(Charg)、PO No.(EBELN)與客人料號(KDMAT)  Smose Liao 20100617
                    if (chkEbelnKdmat.Checked == true)
                    {
                        //檢查要入庫的儲位是否已經有相同料號但不同版本)
                        if (objStorageData.CheckExistedSameMaterial(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的版本!!";
                            return;
                        }

                        //檢查要入庫的儲位是否已經有相同料號但不同的PO No.)  
                        if (objStorageData.CheckExistedSameEbeln(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString(), dtSelect.Rows[i]["EBELN"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的PO No.!!";
                            return;
                        }

                        //檢查要入庫的儲位是否已經有相同料號但不同的客人料號)  
                        if (objStorageData.CheckExistedSameKdmat(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["CHARG"].ToString(), dtSelect.Rows[i]["KDMAT"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的客人料號!!";
                            return;
                        }
                    }
                    #endregion
                }
                # endregion
            }

            #region 檢查併儲的數量是否大於0  Smose Liao 20100412
            for (int i = 0; i < dtSelect.Rows.Count; i++)
            {
                if (int.Parse(dtSelect.Rows[i]["Alqty"].ToString()) <= 0)
                {
                    stsWarning.Text = "The Location of " + dtSelect.Rows[i]["LOCAT"].ToString() + " had zero quantatity to combine. Please Check it!!";
                    return;
                }

                if (Comcd == "9900" || objStorageData.CheckStorageInType(strWerks, strLgort, "Diff Matnr Diff Locat"))
                {
                    //檢查要入庫的儲位是否已經有相同料號但不同Datecode
                    if (dtSelect.Rows[i]["DACOD"].ToString().Trim() != "")
                    {
                        if (objStorageData.CheckExistedSameMaterialDC(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["DACOD"].ToString()))
                        {
                            stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " 已經存在相同料號但有不同的Datecode!!";
                            return;
                        }
                    }
                }
                if (objStorageData.CheckStorageInType(strWerks, strLgort, "Diff DACOD Diff Locat"))
                {
                    //相同料号不同DateCode禁止并入同一储位 Duty 
                    if (dtSelect.Rows[i]["DACOD"].ToString().Trim() != "")
                    {
                        if (objStorageData.CheckExistedSameMaterialDC(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INSMK"].ToString(), dtSelect.Rows[i]["DACOD"].ToString()))
                        {
                            stsWarning.Text = " 相同料号不同DateCode禁止并入同一储位!";
                            return;
                        }
                    }
                }
                #region GB防呆
                //GB 材料防呆 
                if (dtSelect.Rows[i]["MATNR"].ToString().Substring(0, 2) == "20" || dtSelect.Rows[i]["MATNR"].ToString().Substring(0, 2) == "2L")
                {
                    DialogResult result = new DialogResult();
                    result = MessageBox.Show("该材料属于GB材料，请确认是否继续使用GB并储功能？", "GB材料提醒", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        stsWarning.Text = "GB材料请使用GB并储";
                        return;

                    }
                }
                #endregion

            }
            #endregion

            #region 个别仓别有特殊要求
            if ((strWerks == "CS32" && strLgort == "TW20") || (strWerks == "CS90" && (strLgort == "TW20" || strLgort == "TW70" || strLgort == "TW50")) || (strWerks == "CS91" && (strLgort == "TW20" || strLgort == "TW50" || strLgort == "TW51")) || (strWerks == "CS92" && (strLgort == "TW20" || strLgort == "TW51" || strLgort == "TW50")))
            {
                #region  SAM zhang 要求同料号入库并储不能放置在同一储位。 by blank
                if (dtSelect.Rows.Count >= 1)
                {
                    for (int i = 0; i < dtSelect.Rows.Count; i++)
                    {
                        //  string strloc = dtSelect.Rows[i]["LOCAT"].ToString();
                        string strloc = txtNewLocat.Text.Trim().ToString();
                        string strinsmk = dtSelect.Rows[i]["INSMK"].ToString();
                        string strmatnr = dtSelect.Rows[i]["MATNR"].ToString();
                        string strvander = dtSelect.Rows[i]["LIFNR"].ToString();
                        string strcharg = dtSelect.Rows[i]["CHARG"].ToString();
                        //判断库存中是否存在同储位同料号数据
                        bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                        if (bl)
                        {
                            stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许并储！";
                            return;
                        }
                    }
                }
                #endregion
            }
            else if ((strWerks == "CS20" && (strLgort == "TW22" || strLgort == "TW25" || strLgort == "TWEJ" || strLgort == "TW91" || strLgort == "TW31" || strLgort == "TW51" || strLgort == "TW15")) || (strWerks == "CS12" && (strLgort == "TW52" || strLgort == "TW53" || strLgort == "TW12" || strLgort == "TW20" || strLgort == "TW11" || strLgort == "TW60" || strLgort == "TW31")))
            {
                #region  SAM zhang 要求同料号入库并储不能放置在同一储位。 by blank
                if (dtSelect.Rows.Count >= 1)
                {
                    for (int i = 0; i < dtSelect.Rows.Count; i++)
                    {
                        string strloc = txtNewLocat.Text.Trim().ToString();
                        //string strloc = dtSelect.Rows[i]["LOCAT"].ToString();
                        string strinsmk = dtSelect.Rows[i]["INSMK"].ToString();
                        string strmatnr = dtSelect.Rows[i]["MATNR"].ToString();
                        string strvander = dtSelect.Rows[i]["LIFNR"].ToString();
                        string strcharg = dtSelect.Rows[i]["CHARG"].ToString();

                        //判断库存中是否存在同储位同料号数据
                        bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                        if (bl)
                        {
                            stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许并储！";
                            return;
                        }
                    }
                }
                #endregion
            }
            else if ((strWerks == "CS42" && (strLgort == "TW31" || strLgort == "TW34")))
            {
                #region  Jiashan li 同料号同版本不可以并储。
                if (dtSelect.Rows.Count >= 1)
                {
                    for (int i = 0; i < dtSelect.Rows.Count; i++)
                    {
                        string strloc = txtNewLocat.Text.Trim().ToString();
                        string strinsmk = dtSelect.Rows[i]["INSMK"].ToString();
                        string strmatnr = dtSelect.Rows[i]["MATNR"].ToString();
                        string strvander = dtSelect.Rows[i]["LIFNR"].ToString();
                        string strcharg = dtSelect.Rows[i]["CHARG"].ToString();
                        bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                        if (bl)
                        {
                            stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许并储！";
                            return;
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region CS42厂区特殊要求
            if ((strWerks == "CS42" && (strLgort == "TWDE" || strLgort == "TWHD" || strLgort == "TWDM" || strLgort == "TW60" || strLgort == "TWD2" || strLgort == "TWD3" || strLgort == "TWD4" || strLgort == "TWFB" || strLgort == "TWFG" || strLgort == "TWCR")) || (strWerks == "CS12" && (cmbLgort.Text.ToString() == "TW30" || cmbLgort.Text.ToString() == "TW31" || cmbLgort.Text.ToString() == "TW33")))
            {
                string strloc = txtNewLocat.Text.Trim().ToString();
                if (objStorageData.CheckExistedSameLocation(strloc))
                {
                    if (dtSelect.Rows.Count >= 1)
                    {
                        for (int i = 0; i < dtSelect.Rows.Count; i++)
                        {


                            string strinsmk = dtSelect.Rows[i]["INSMK"].ToString();
                            string strmatnr = dtSelect.Rows[i]["MATNR"].ToString();
                            string strvander = dtSelect.Rows[i]["LIFNR"].ToString();
                            string strcharg = dtSelect.Rows[i]["CHARG"].ToString();
                            bool bl2 = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                            if (bl2)
                            {
                                bool bl = objStorageData.CheckExistedSameMaterialCharg(strloc, strmatnr, strcharg);
                                if (!bl)
                                {
                                    stsWarning.Text = "料号版本不一致不能并储";
                                    return;
                                }
                            }
                        }
                    }
                }
              
            }

            #endregion

            for (int i = 0; i < dtSelect.Rows.Count; i++)
            {
                if (Werks == "CS42" && Lgort == "TW33" && objStorageData.CheckExistedSameIndatCharg(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INDAT"].ToString(), dtSelect.Rows[i]["CHARG"].ToString()))
                {
                    stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + " has existed in the location with different Indat and Charg!";
                    return;
                }

                //特殊仓别相同料号不同INDAT不能入同一储位
                if (objStorageData.CheckStorageInType(strWerks, strLgort, "DiffIndatSameMatnrSameLocat") && objStorageData.CheckExistedSameIndatMatnr(NewLocat, dtSelect.Rows[i]["MATNR"].ToString(), dtSelect.Rows[i]["INDAT"].ToString()))
                {
                    stsWarning.Text = dtSelect.Rows[i]["MATNR"].ToString() + "已有不同批次的库存，请确认！";
                    return;
                }
            }
            #region PCB不同版本不允许入库 

            DataTable dtLocMatCar = new DataTable();
            if (objPlantData.CheckCHARGLGORT(Werks))
            {
                string strLocat = txtNewLocat.Text.Trim().ToString();
                if (objStorageData.CheckExistedSameLocation(strLocat))
                {
                    if (dtSelect.Rows.Count >= 1)
                    {
                        for (int i = 0; i < dtSelect.Rows.Count; i++)
                        {
                            string strMatnr = dtSelect.Rows[i]["MATNR"].ToString();
                            string strCharg = dtSelect.Rows[i]["CHARG"].ToString();
                            string strLocod = dtSelect.Rows[i]["LOCOD"].ToString();
                            //判断是否为PCB材料
                            if (dtSelect.Rows[i]["MATNR"].ToString().Substring(0, 2) == "SA" || dtSelect.Rows[i]["MATNR"].ToString().Substring(0, 2) == "DA")
                            {
                                if (objStorageData.CheckExistedDifferentCHARG(strLocat, strMatnr, strCharg))
                                {
                                    stsWarning.Text = strMatnr + "料号版本不一致不能并储!!";
                                    return;
                                }
                            }
                            #region Lot Code不同Lot Code不允许入库 -CS21,CS42                     
                            if (objPlantData.CheckLOCODLGORT(strWerks, strLgort))
                            {
                                if (objStorageData.CheckExistedDifferentLOCAD(strLocat, strMatnr, strLocod))
                                {
                                    stsWarning.Text = strMatnr + "料号Lot Code不一致不能并储!!";
                                    return;
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            #endregion
            #region 不同CONFIG不能入同一个储位 - Michael Kang(康现科)
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            if (objStorageIn.CheckPlantInType(strWerks, "Config FIFO"))
            {
                if (dtSelect.Rows.Count >= 1)
                {
                    for (int i = 0; i < dtSelect.Rows.Count; i++)
                    {
                        string strloc = txtNewLocat.Text.Trim().ToString();                       
                        string strmatnr = dtSelect.Rows[i]["MATNR"].ToString();                     
                        string strConfig = dtSelect.Rows[i]["CONFIG"].ToString();
                        bool bl = objStorageData.CheckExistedDFMaterialConfig(strloc, strmatnr,strConfig);
                        if (bl)
                        {
                            stsWarning.Text = "同料号不同Config,不允许并储！";
                            return;
                        }
                    }
                }
            }
             #endregion


            objStorageIn = new StorageIn(UserData, Werks, Lgort, "", Progid);
            if (objStorageIn.AddLocationCombineData(OldLocat, NewLocat, dtSelect))
            {
                stsWarning.Text = "Update OK!";
                dtDestination = objStorageData.QueryStorageDetailData(NewLocat, "");
                ShowDestinationDataGrid();
                dtSource = objStorageData.QueryStorageDetailData(OldLocat, strIsmrg);
                ShowSourceDataGrid();
                btnSave.Enabled = false;
            }
            else
            {
                stsWarning.Text = "Update fail!! " + objStorageIn.ERRMSG;
                return;
            }

            }
        

        #endregion

        #region GetSelectedRows
        public void GetSelectedRows(DataGridView dgv)
        {
            DataRow drRow;
            DataRow[] foundRow;
            //CurrencyManager cm = (CurrencyManager)this.BindingContext[dgv.DataSource, dgv.DataMember];
            //DataView dv = (DataView)cm.List;
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
            dtSelect.Columns.Add("ExpiryDate", Type.GetType("System.String"));
            dtSelect.Columns.Add("TASKID", Type.GetType("System.String"));
            dtSelect.Columns.Add("MAXEXP", Type.GetType("System.String"));
            dtSelect.Columns.Add("LOCKED", Type.GetType("System.String"));
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            if (objStorageIn.CheckPlantInType(strWerks, "Config FIFO"))
            {
                dtSelect.Columns.Add("CONFIG", Type.GetType("System.String"));
            }

                foreach (DataGridViewRow dgvr in dgv.Rows)
                {

                if (Convert.ToBoolean(dgvr.Cells["Select"].Value)==true)
                {
                    foundRow = dtSource.Select("MATNR='" + dgvr.Cells["MATNR"].Value.ToString() + "' and INSMK='" + dgvr.Cells["INSMK"].Value.ToString() + "' and CHARG='" + dgvr.Cells["CHARG"].Value.ToString() + "' and OMBLNR='" + dgvr.Cells["OMBLNR"].Value.ToString() + "' and SERNO='" + dgvr.Cells["SERNO"].Value.ToString() + "' and DACOD='" + dgvr.Cells["DACOD"].Value.ToString() + "' and LOCOD='" + dgvr.Cells["LOCOD"].Value.ToString() + "'"); 
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
                        drRow["ExpiryDate"] = foundRow[j]["ExpiryDate"].ToString();
                        drRow["TASKID"] = foundRow[j]["TASKID"].ToString();
                        drRow["MAXEXP"] = foundRow[j]["MAXEXP"].ToString();
                        drRow["LOCKED"] = foundRow[j]["LOCKED"].ToString();                     
                        if (objStorageIn.CheckPlantInType(strWerks, "Config FIFO"))
                        {
                            drRow["CONFIG"] = foundRow[j]["CONFIG"].ToString();
                        }
                        dtSelect.Rows.Add(drRow);
                    }
                }
            }

            }
            # endregion

            # region Refresh
            private void btnRefresh_Click(object sender, EventArgs e)
            {
                this.stsWarning.Text = "";
                this.strWerks = "";
                this.txtOldLocat.Text = "";
                this.txtNewLocat.Text = "";
                this.cmbWerks.Enabled = true;
                this.cmbLgort.Enabled = true;
                this.strLgort = "";
                this.txtOldLocat.Enabled = true;
                this.txtNewLocat.Enabled = false;
                this.dtSource.Rows.Clear();
                this.dtDestination.Rows.Clear();
                this.dgvSource.DataSource = null;
                this.dgvDestination.DataSource = null;
                lblCount.Text = "";
                lblCount1.Text = "";
                this.btnSave.Enabled = false;
                this.panel1.Enabled = true;
                this.chkMixed.Checked = false;
                this.chkEbelnKdmat.Checked = false;
                this.strIsmrg = "";
            }
            # endregion

            # region Exit
            private void btnExit_Click(object sender, EventArgs e)
            {
                this.Close();
            }
            # endregion

            # region Resize
            private void Manage_LocationCombine_Resize(object sender, EventArgs e)
            {
                panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
                panel6.Size = new System.Drawing.Size((int)(this.Size.Width * 0.5), panel6.Size.Height);
                if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40 > 0)
                    stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40;
            }
            # endregion

            # region Enter Press
            private void txtOldLocat_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (e.KeyChar == (char)13)
                {
                    try
                    {
                        stsWarning.Text = "";
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
                        ShowSourceData();
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = ex.Message;
                        return;
                    }
                }
            }

            private void txtNewLocat_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (e.KeyChar == (char)13)
                {
                    try
                    {
                        stsWarning.Text = "";
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
                        ShowDestinationData();
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = ex.Message;
                        return;
                    }
                }
            }
            # endregion

            # region Source Row Header Click
            private void dgvSource_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
            {
                try
                {
                    string strMenge = "0";
                    string strAlqty = "0";
                    stsWarning.Text = "";

                    strMenge = dgvSource.Rows[e.RowIndex].Cells["MENGE"].Value.ToString();
                    strAlqty = dgvSource.Rows[e.RowIndex].Cells["ALQTY"].Value.ToString();


                    Manage_LocationCombine_Detail objManage_LocationCombine_Detail = new Manage_LocationCombine_Detail(UserData, Progid, Werks, Lgort);
                    //  Manage_LocationCombine_DetailCSMC objManage_LocationCombine_Detail = new Manage_LocationCombine_DetailCSMC(UserData, Progid, Werks, Lgort, txtOldLocat.Text.Trim(),
                    //dgvSource.Rows[e.RowIndex].Cells["OMBLNR"].Value.ToString(), dgvSource.Rows[e.RowIndex].Cells["INSMK"].Value.ToString(),
                    //dgvSource.Rows[e.RowIndex].Cells["CHARG"].Value.ToString(), dgvSource.Rows[e.RowIndex].Cells["MATNR"].Value.ToString());
                    objManage_LocationCombine_Detail.Menge = strMenge;
                    objManage_LocationCombine_Detail.Alqty = strAlqty;
                    objManage_LocationCombine_Detail.ShowDialog();
                    dtSource.Rows[e.RowIndex]["ALQTY"] = objManage_LocationCombine_Detail.Alqty;
                    //DataTable  dtWhbox = objManage_LocationCombine_Detail.Returndt;
                    ShowSourceDataGrid();

                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
            # endregion

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
