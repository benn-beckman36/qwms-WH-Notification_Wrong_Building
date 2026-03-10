using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using System.Media;
using QWMS.Common;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;

namespace QWMS
{
    public partial class StorageIn_Pallet_PlasticMaterial : Form
    {
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        //private string strMblnr = "";
        //private string strMatnr = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strCtbto = "";
        private string strRegon = "";
        private string strMachine = "";
        private string strBoxid = "";
        private string strComcd = "";
        private StorageData objStorageData;

        UserInfo UserData = new UserInfo();
        private int intFormIndex = 0;
        private bool AllowToClose = true;//設定能否關閉Form視窗
        private bool bolDuplicate = false;
        private DataTable dtData = new DataTable();
        private DataTable dtPrint = new DataTable();
        DataTable dtCombineStorage = new DataTable();
        DataTable dtPalData = new DataTable();
        int ScanCount = 0;
        int PartNoSum = 0;
        string strPdnam = "";
        
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

        public string Locat
        {
            get
            {
                return this.txtLocat.Text.Trim();
            }
            set
            {
                this.txtLocat.Text = value;
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

        public string Ctbto
        {
            get
            {
                return strCtbto;
            }
            set
            {
                strCtbto = value;
            }
        }

        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
            }
        }

        public string Machine
        {
            get
            {
                return strMachine;
            }
            set
            {
                strMachine = value;
            }
        }

        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
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
        
        public string Boxid
        {
            get
            {
                return strBoxid;
            }
            set
            {
                strBoxid = value;
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
        public DataTable Print
        {
            get
            {
                return dtPrint;
            }
            set
            {
                dtPrint = value;
            }
        }

        public StorageIn_Pallet_PlasticMaterial(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            strComcd = varUserData.CompanyCode;

            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);

                //檢查權限
                if (!objStorageIn.CheckAuthority())
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
                    dtPrint.Columns.Add("Item");
                    dtPrint.Columns.Add("BOXCOUNT");
                    dtPrint.Columns.Add("LOCAT");
                    dtPrint.Columns.Add("MBLNR");
                    dtPrint.Columns.Add("MATNR");
                    dtPrint.Columns.Add("MENGE");
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
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }
        #endregion

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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
        #endregion

        #region ShowDataGridSource
        public void ShowDataGridSource()
        {
            this.dgvSource.AutoGenerateColumns = false;
            this.dgvSource.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                dgvcMblnr.Width = 100;
                dgvcMblnr.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "WO Item";
                dgvcZeile.Width = 100;
                dgvcZeile.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcBoxid = new DataGridViewTextBoxColumn();
                dgvcBoxid.DataPropertyName = "BOXID";
                dgvcBoxid.HeaderText = "Box id";
                dgvcBoxid.Width = 60;
                dgvcBoxid.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcBoxid);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcIndat);

                dgvSource.DataSource = dtPalData;
                lblSource.Text = dtPalData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                dgvcMblnr.Width = 100;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "WO Item";
                dgvcZeile.Width = 100;
                dgvcZeile.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.Width = 90;
                dgvcLocat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcBoxid = new DataGridViewTextBoxColumn();
                dgvcBoxid.DataPropertyName = "BOXID";
                dgvcBoxid.HeaderText = "Box id";
                dgvcBoxid.Width = 60;
                dgvcBoxid.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBoxid);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.Width = 90;
                dgvcAlqty.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcIndat);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #region rdbInteger_CheckedChanged
        private void rdbInteger_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "Integer";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
            SetControlState(true);
        }
        #endregion

        #region rdbScattered_CheckedChanged
        private void rdbScattered_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "Scattered";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
            SetControlState(true);
        }
        #endregion

        //#region rdoAutoRunLocat_CheckedChanged
        //private void rdoAutoRunLocat_CheckedChanged(object sender, EventArgs e)
        //{
        //    stsWarning.Text = "";
        //    strType = "AUTO";
        //    Type = strType;
        //    gbFunction.Enabled = false;
        //    gbHeader.Enabled = true;
        //    btnConfirm.Enabled = true;
        //    SetControlState(true);
        //}
        //#endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                #region "將Location 填回Data中"
                foreach (DataRow rsTmpRow in this.Data.Rows)
                {
                    rsTmpRow["LOCAT"] = this.txtLocat.Text.Trim();
                    rsTmpRow["COMCD"] = Comcd;  //加入Company Code欄位   Smose Liao 20091026
                }
                this.Data.AcceptChanges();

                #endregion

                this.btnConfirm.Enabled = false;
                btnAdd_Click(null, null);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region SetControlState
        private void SetControlState(bool bolBeforeConfirm)
        {
            this.cmbWerks.Enabled = bolBeforeConfirm;
            this.cmbLgort.Enabled = bolBeforeConfirm;
            this.dtpIndat.Enabled = bolBeforeConfirm;
            this.btnConfirm.Enabled = !bolBeforeConfirm;

            this.txtLocat.Enabled = false;
            this.txtBoxid.Enabled = bolBeforeConfirm;
        }
        #endregion

        #region btnAdd_Click
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                #region Old Method(Deleted)
                //if (dtData.Rows[0]["REFID"].ToString().Substring(0, 3) == "SR-")
                //{
                //    ShowNewDataGrid(); 
                //}
                //else
                //{
                //    ShowDataGrid();
                //}
                #endregion
                ShowDataGrid();
                if (rdbInteger.Checked)
                {
                    this.txtBoxid.Enabled = true;
                    this.txtBoxid.Text = "";
                    this.txtBoxid.Focus();
                    this.txtBoxid.SelectAll();
                }
                if (rdbScattered.Checked)
                {
                    this.txtBoxid.Enabled = true;
                    this.txtBoxid.Text = "";
                    this.txtBoxid.Focus();
                    this.txtBoxid.SelectAll();
                }
                btnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region SetbtnSaveProcess
        private void SetbtnSaveProcess()
        {
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = false;

        }
        #endregion

        #region SetbtnSaveException
        private void SetbtnSaveException()
        {
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = true;
        }
        #endregion

        #region btnSave_Click

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            QCI.QWMS.StorageIn objStorageInQWMS = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

            try
            {
                #region TimeOut機制(開始計時)
                /*
                System.Timers.Timer t = new System.Timers.Timer();                                
                t.AutoReset = true;
                t.Interval = 1000;
                t.Enabled = false;
                t.Elapsed += new System.Timers.ElapsedEventHandler(theout);
                t.SynchronizingObject = this;
                t.Start();
                */

                /*
                System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();                
                t.Interval = 1000;
                t.Enabled = true;
                t.Tick += new EventHandler(TimerTrick);
                t.Start();
                */
                //Application.DoEvents();
                //this.Invoke(new MethodInvoker(t.Start));

                #endregion

                #region 檢查單據狀態

                string PROCESSING = "";

                if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals("")) //PalletID有值 
                    PROCESSING = objStorageInQWMS.QueryStatus(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID");

                if (PROCESSING != null)
                {
                    if (!PROCESSING.Trim().Equals(""))
                    {
                        if (PROCESSING.Equals("Y"))
                        {
                            SoundPlayer sp = new SoundPlayer(@"Sound\NG.wav");
                            sp.Play();
                            MessageBox.Show("This BID or PID is processing," + Environment.NewLine + "please try again later!!", "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                #endregion

                #region 改變單據Processing狀態為Y,並寫入開始時間

                if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值 
                    objStorageInQWMS.UpdateStatus(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID", "Y");

                #endregion

                #region 變數宣告
                string strOMBLNR = "";
                SetbtnSaveProcess();
                string strTempMblnrMatnr = "";
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                string OMBLN = "";
                DataRow drPrint;

                #endregion
                #region 防呆機制
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "The data can't be empty!!";
                    SetbtnSaveException();
                    return;
                }
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (strTempMblnrMatnr.IndexOf(dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["ZEILE"].ToString()+dtData.Rows[i]["BOXID"].ToString()) == -1)
                    {
                        strTempMblnrMatnr += dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["ZEILE"].ToString() + dtData.Rows[i]["BOXID"].ToString() + ";";
                    }
                    else
                    {
                        SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                        sp.Play();
                        stsWarning.Text = "The Pallet ID and Part No and Box ID you input is duplicate!!";
                        SetbtnSaveException();
                        return;
                    }
                    //檢查要入庫的單號之前是否有使用連板入庫的方式入庫
                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    dtTemp = objStorageData.QueryNotCombineInData(Locat, dtData.Rows[i]["MBLNR"].ToString(), "0");
                    if (dtTemp.Rows.Count > 0)
                    {
                        SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                        sp.Play();
                        stsWarning.Text = "The Document No was stored in with mixed material last time!!";
                        SetbtnSaveException();
                        return;
                    }
                    //檢查要入庫的儲位是有相同的料號但不同連板資料(包含單板)
                    if (objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["MRGID"].ToString()))
                    {
                        SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                        sp.Play();
                        stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
                        SetbtnSaveException();
                        return;
                    }

                    //如果可以允許同一儲位置放不同版本的料號, Kent 20050130
                    if (this.Duplicate == false)
                    {
                        //檢查要入庫的儲位是否已經有相同料號但不同版本)
                        if (objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
                        {
                            SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                            sp.Play();
                            stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
                            SetbtnSaveException();
                            return;
                        }
                    }
                }
                #endregion
                #region 檢查目前ReferenceID or PalletID是否已經扣帳過(若為true則為QWMS端扣帳未完成)

                bool flag_id = true;
                bool flag_id_SAPONLY = true;

                QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值
                {
                    DataTable dtPalData = objSapData.QueryQMSLineInDataByPalid("", dtData.Rows[0]["MBLNR"].ToString().Trim(), dtpIndat.Value.ToString("yyyyMMdd"));

                    if (dtPalData.Rows.Count == 0)
                        flag_id = false;
                }

                #endregion

                #region 合并同Pallet ID同工单号单据以扣账
                dtCombineStorage = CombineStorage(dtData);
                #endregion

                #region --備註--

                //扣帳流程未完整結束,可能情況有以下3種
                //1.SAP成功,QWMS失敗
                //2.SAP失敗,QWMS失敗(此狀況可忽略)
                //3.根本沒做過(此狀況可忽略)
                //flag_id = false-->扣帳已完成
                //flag_id_SAPONLY = false-->SAP已扣,QWMS未扣

                #endregion
                if (flag_id)//檢查SAP已扣QWMS未扣的情況
                {
                    #region 檢查目前ReferenceID or PalletID是否已經扣帳過(SAP ONLY)

                    OMBLN = "";
                    QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);

                    if (dtData.Rows[0]["OTLGT"].ToString() == "")//101
                    {
                        if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值
                        {
                            OMBLN = objLogData.QuerySAPInDataByPalid101(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID");
                            if (OMBLN != null && !OMBLN.Trim().Equals(""))
                                flag_id_SAPONLY = false;
                        }
                    }
                    else//311
                    {
                        if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值
                        {
                            OMBLN = objLogData.QuerySAPInDataByPalid311(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID");
                            if (OMBLN != null && !OMBLN.Trim().Equals(""))
                                flag_id_SAPONLY = false;
                        }
                    }

                    #endregion
                }

                DialogResult result = new DialogResult();

                if (!flag_id_SAPONLY)//若為SAP扣過帳而QWMS未扣的情況
                {
                    SoundPlayer sp = new SoundPlayer(@"Sound\NG.wav");
                    sp.Play();
                    result = MessageBox.Show("SAP already finish this RID or PID,but QWMS not!!" + "\n" + "Are you sure to continue?", "QWMS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result != DialogResult.Yes)
                    {
                        #region 改變單據Processing狀態為N

                        if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值 
                            objStorageInQWMS.UpdateStatus(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID", "N");

                        #endregion

                        return;
                    }

                }

                if (flag_id)
                {
                    if (flag_id_SAPONLY)
                    {
                        #region SAP扣帳
                        this.stsWarning.Text = "";

                        #region SAP扣帳(101工單入庫)

                        ArrayList aryMblnr = new ArrayList();
                        QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                        //try
                        //{
                        //dtData = objStorageIn.wsUpdateSapInventory_PAL(dtData);//扣帳

                        //新版RFC寫法，透過TXT文檔的存取扣SAP帳，並取得SAP回傳的扣帳編號  Smose Liao 20120723
                        stsWarning.Text = "系統正在扣SAP帳中，請勿關閉視窗!!";
                        AllowToClose = false;//強制User無法關閉視窗

                        dtCombineStorage = objStorageIn.wsUpdateSapInventory_PAL_ByTxt_101(dtCombineStorage); //扣帳
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            dtData.Rows[i]["OMBLNR"] = dtCombineStorage.Rows[0]["OMBLNR"].ToString();
                        }

                        //}
                        //catch (Exception ex)
                        //{
                        //stsWarning.Text = ex.Message;
                        //MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //SetbtnSaveException();
                        //return;
                        //}

                        if (dtData.Rows[0]["OMBLNR"].ToString().Trim() == "")
                        {
                            #region 改變單據Processing狀態為N

                            if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值 
                                objStorageInQWMS.UpdateStatus(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID", "N");

                            #endregion

                            stsWarning.Text = objStorageIn.ERRMSG;
                            AllowToClose = true;
                            MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            SetbtnSaveException();
                            return;
                        }
                        else
                        {
                            //更新扣帳編號
                            aryMblnr.Clear();
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                aryMblnr.Add(dtData.Rows[i]["OMBLNR"].ToString().Trim());
                            }
                        }

                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            dtData.Rows[i]["OMBLNR"] = aryMblnr[i].ToString();
                            dtData.Rows[i]["SERNO"] = i + 1;
                            dtData.Rows[i]["RMAK1"] = this.Boxid.Trim();

                            //若NMBLN欄位不為空，代表user刷入的是Reference id，因此將原先的Pallet ID(MBLNR)取代回原欄位
                            if (dtData.Rows[i]["NMBLN"].ToString() != "")
                            {
                                dtData.Rows[i]["MBLNR"] = dtData.Rows[i]["NMBLN"].ToString();
                            }
                        }

                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region 模擬SAP回傳的Datatable

                        #region 101工單入庫

                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            dtData.Rows[i]["OMBLNR"] = OMBLN;
                            dtData.Rows[i]["SERNO"] = i + 1;
                            dtData.Rows[i]["RMAK1"] = this.Boxid.Trim();

                            //若NMBLN欄位不為空，代表user刷入的是Reference id，因此將原先的Pallet ID(MBLNR)取代回原欄位
                            if (dtData.Rows[i]["NMBLN"].ToString() != "")
                            {
                                dtData.Rows[i]["MBLNR"] = dtData.Rows[i]["NMBLN"].ToString();
                            }
                        }

                        #endregion

                        #endregion

                    }


                    #region QWMS扣帳

                    if (objStorageInQWMS.AddOnLineInData_PlasticMaterial(Locat, dtData,dtCombineStorage))
                    {
                        if (flag_id_SAPONLY)
                        {
                            SoundPlayer sp = new SoundPlayer(@"Sound\OK2.wav");
                            sp.Play();
                            stsWarning.Text = "SAP Posting OK(" + strOMBLNR + ")!!";
                        }
                        else
                        {
                            stsWarning.Text = "QWMS Posting OK!!";
                        }

                        AllowToClose = true;//扣帳成功，恢復可以關閉Form視窗
                        this.btnAdd.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.btnPrint.Enabled = true;
                        #region 改變單據Processing狀態為N

                        if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值 
                            objStorageInQWMS.UpdateStatus(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID", "N");

                        #endregion

                        if (rdbInteger.Checked)
                        {
                            dtPrint = dtCombineStorage.Copy();
                            ReportPrint objReportPrint = new ReportPrint(UserData, "PLASTICMATERIALLOCATLABEL_INTEGER", dtPrint);
                            objReportPrint.Report.PrintToPrinter(1, true, 0, 0);
                        }
                        if (rdbScattered.Checked)
                        {
                            for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                            {
                                drPrint = dtPrint.NewRow();
                                drPrint["BOXCOUNT"] = ScanCount.ToString();
                                drPrint["LOCAT"] = dtCombineStorage.Rows[i]["LOCAT"].ToString();
                                drPrint["MATNR"] = dtCombineStorage.Rows[i]["MATNR"].ToString();
                                drPrint["MBLNR"] = dtCombineStorage.Rows[i]["MBLNR"].ToString();
                                drPrint["MENGE"] = dtCombineStorage.Rows[i]["MENGE"].ToString();
                                dtPrint.Rows.Add(drPrint);
                            }
                            for (int i = 0; i < dtPrint.Rows.Count; i++)
                            {
                                dtPrint.Rows[i]["Item"] = (i + 1).ToString().PadLeft(2, '0');
                            }
                            ReportPrint objReportPrint = new ReportPrint(UserData, "PLASTICMATERIALLOCATLABEL_SCATTERED", dtPrint);
                            objReportPrint.Report.PrintToPrinter(1, true, 0, 0);
                        }
                        return;
                    }
                    else
                    {
                        if (flag_id_SAPONLY)
                        {
                            Sound.Play(@"Sound\NG.wav");
                            stsWarning.Text = "SAP Posting OK(" + strOMBLNR + ") but QWMS Save Fail!!請立即通知MIS!! " +
                                              objStorageInQWMS.ERRMSG;
                        }
                        else
                        {
                            Sound.Play(@"Sound\NG.wav");
                            stsWarning.Text = "QWMS Save Fail!!請立即通知MIS!! " + objStorageInQWMS.ERRMSG;
                        }

                        AllowToClose = true;
                        SetbtnSaveException();

                        #region 改變單據Processing狀態為N

                        if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值 
                            objStorageInQWMS.UpdateStatus(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID", "N");

                        #endregion
                        return;
                    }
                    #endregion
                }
                else if (!flag_id)
                {
                    #region 改變單據Processing狀態為N

                    if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值 
                        objStorageInQWMS.UpdateStatus(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID", "N");

                    #endregion
                    this.btnRefresh_Click(null, null);
                    throw new Exception("No Data!!");
                }
            }
            catch (Exception ex)
            {
                #region 改變單據Processing狀態為N
                Sound.Play(@"Sound\NG.wav");
                if (!dtData.Rows[0]["MBLNR"].ToString().Trim().Equals(""))//PalletID有值 
                    objStorageInQWMS.UpdateStatus(dtData.Rows[0]["MBLNR"].ToString().Trim(), "PID", "N");

                #endregion

                AllowToClose = true;
                stsWarning.Text = ex.Message;
                MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetbtnSaveException();
                return;
            }
        }

        #endregion

        #region TimeOut

        public void TimerTrick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer t = (System.Windows.Forms.Timer)sender;
            this.Invoke(new MethodInvoker(t.Stop));
            t.Enabled = false;
            t.Dispose();

            this.Close();
        }


        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer t = (System.Timers.Timer)source;
            t.Stop();
            t.Enabled = false;
            t.Close();

            StorageIn_Pallet_OnLineIn newform = (StorageIn_Pallet_OnLineIn)t.SynchronizingObject;
            newform.Close();
            //StorageIn_Pallet_OnLineIn newform = (StorageIn_Pallet_OnLineIn)Application.OpenForms["StorageIn_Pallet_OnLineIn"];
            //newform.Close();




            //IntPtr ptr = FindWindow(null, "x.txt - 記事本");   
            //if(ptr != IntPtr.Zero)//不為空值的內容時  
            //PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);


            //Application.Exit();
            //System.Environment.Exit(System.Environment.ExitCode);            
            //MessageBox.Show("Session Timeout!!", "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion

        #region FindWindow Function

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;

        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            this.rdbInteger.Checked = false;
            this.rdbScattered.Checked = false;
            this.gbFunction.Enabled = true;
            this.txtLocat.Text = "";
            this.txtBoxid.Text = "";
            this.gbHeader.Enabled = false;
            this.dtData.Clear();
            this.dtPalData.Clear();
            this.dgvData.DataSource = null;
            this.dgvSource.DataSource = null;
            this.lblData.Text = "0 records";
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = false;
            this.txtBoxidCount.Text = "";
            this.txtPartNoSum.Text = "";
            this.chkLocat.Checked = false;
            ScanCount = 0;
            PartNoSum = 0;
            strWerks = "";
            strLgort = "";
            strInsmk = "";
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void StorageIn_OnLineIn_Resize(object sender, System.EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
            panel6.Size = new System.Drawing.Size((int)(this.Size.Width * 0.5), panel6.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;

        }

        #region CheckIsOpen
        private bool CheckIsOpen(string strForm)
        {
            bool bolOpened = false;
            string strTest = "";
            try
            {
                for (int i = 0; i < this.MdiParent.MdiChildren.Length; i++)
                {
                    strTest = MdiParent.MdiChildren[i].ToString();
                    if (MdiParent.MdiChildren[i].ToString().IndexOf(strForm) != -1)
                    {
                        bolOpened = true;
                        intFormIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CheckIsOpen()");
            }
            return bolOpened;
        }
        #endregion

        #region txtBoxid_KeyDown
        private void txtBoxid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    #region "檢查廠區倉別資料"

                    stsWarning.Text = "";
                    DataTable dtTemp = new DataTable();
                    DataRow drRow;
                    DataRow[] drPalletID;

                    if (cmbWerks.SelectedIndex != -1)
                        strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    else
                        strWerks = "";

                    if (cmbLgort.SelectedIndex != -1)
                        strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    else
                        strLgort = "";

                    if (Werks == "" || Lgort == "")
                    {

                        throw new Exception("Plant and storage can't be empty!!");
                    }

                    //取得Sttyp及Lotyp
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                    if (dtTemp.Rows.Count >= 1)
                    {
                        strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                        strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                    }
                    else
                    {

                        throw new Exception("Can't find the storage data!!");
                    }

                    #endregion

                    txtBoxid.Enabled = false;
                    string strBoxid = txtBoxid.Text.Trim();
                    string strLocat = txtLocat.Text.Trim();

                    if (strBoxid == "")
                        throw new Exception("BOX ID. can't be Empty!!");

                    #region 防呆 20080924 Rock

                    DataTable dtTmpCheckPAL = new DataTable();
                    dtTmpCheckPAL = objPlantData.GetBOXIDInfo(this.Mandt, this.Werks, this.Lgort, strBoxid);
                    if (dtTmpCheckPAL.Rows.Count <= 0)
                    {

                        this.txtBoxid.Focus();
                        this.txtBoxid.SelectAll();
                        SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                        sp.Play();
                        throw new Exception(strBoxid.Trim() + " doesn't exist!!");
                    }
                    if (Mandt.ToString() != strMandt)
                    {

                        this.txtBoxid.Focus();
                        this.txtBoxid.SelectAll();
                        throw new Exception("Client " + Mandt.ToString() + " doesn't match!!");

                    }

                    #endregion

                    QCI.QWMS.SapData objSapData = new QCI.QWMS.SapData(CommonInfo.Instance.DBType,
                        CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType,
                        CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                    string strTempMblnrMatnr = "";
                    for (int i = 0; i < Data.Rows.Count; i++)
                    {
                        if (
                            strTempMblnrMatnr.IndexOf(Data.Rows[i]["MBLNR"].ToString() +
                                                      Data.Rows[i]["MATNR"].ToString() +
                                                      Data.Rows[i]["ZEILE"].ToString() + Data.Rows[i]["BOXID"].ToString()) == -1)
                        {
                            strTempMblnrMatnr += Data.Rows[i]["MBLNR"].ToString() + Data.Rows[i]["MATNR"].ToString() +
                                                 Data.Rows[i]["ZEILE"].ToString() + Data.Rows[i]["BOXID"].ToString()+";";
                        }
                    }
                    if (rdbInteger.Checked)
                    {
                        dtPalData = objSapData.QueryQMSLineInDataByBoxid(strLocat, strBoxid,
                            dtpIndat.Value.ToString("yyyyMMdd"), "Integer");

                        if (Data.Rows.Count == 0)//获得总数量及箱数，初始化Data
                        {
                            this.txtBoxidCount.Text = dtPalData.Rows.Count.ToString();

                            DataTable dtPartNoSum = CombineStorage(dtPalData);
                            if (dtPartNoSum.Rows.Count > 1)
                            {
                                txtBoxid.Text = "";
                                SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                                sp.Play();
                                throw new Exception("此Pallet ID有多个料号!!");
                            }
                            for (int i = 0; i < dtPartNoSum.Rows.Count; i++)
                            {
                                PartNoSum = PartNoSum + Convert.ToInt32(dtPartNoSum.Rows[i]["MENGE"].ToString());
                            }
                            this.txtPartNoSum.Text = PartNoSum.ToString();
                            GetDefaultTable();
                        }

                        if (dtPalData.Rows.Count == 0)
                        {
                            txtBoxid.Text = "";
                            SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                            sp.Play();
                            throw new Exception("No Data!!");
                        }
                        if (Data.Rows.Count > 0)
                        {
                            drPalletID =  dtPalData.Select("MBLNR='" + Data.Rows[0]["MBLNR"].ToString() + "'");
                            if (drPalletID.Length > 0)
                            {
                                this.txtBoxid.Enabled = false;
                                btnSave_Click(null,null);
                                Refresh();
                                this.txtLocat.Enabled = false;
                                this.chkLocat.Checked = false;
                                return;
                            }
                            else
                            {
                                stsWarning.Text = "两次刷入Boxid属于不同栈板号!!";
                                SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                                sp.Play();
                                return;
                            }
                        }

                        foreach (DataRow tmpRow in dtPalData.Rows)
                        {
                            if (
                                strTempMblnrMatnr.IndexOf(tmpRow["MBLNR"].ToString() + tmpRow["MATNR"].ToString() +
                                                          tmpRow["ZEILE"].ToString() + tmpRow["BOXID"].ToString()) == -1)
                            {
                                drRow = Data.NewRow();
                                drRow["MANDT"] = tmpRow["MANDT"].ToString();
                                drRow["COMCD"] = tmpRow["COMCD"].ToString();
                                drRow["WERKS"] = tmpRow["WERKS"].ToString();
                                drRow["LGORT"] = tmpRow["LGORT"].ToString();
                                drRow["MATNR"] = tmpRow["MATNR"].ToString();
                                drRow["INSMK"] = tmpRow["INSMK"].ToString();
                                drRow["CHARG"] = tmpRow["CHARG"].ToString();
                                drRow["MENGE"] = tmpRow["MENGE"].ToString();
                                drRow["OTQTY"] = tmpRow["OTQTY"].ToString();
                                drRow["ALQTY"] = tmpRow["MENGE"].ToString();
                                drRow["MBLNR"] = tmpRow["MBLNR"].ToString();
                                drRow["ZEILE"] = tmpRow["ZEILE"].ToString();
                                drRow["EBELN"] = tmpRow["EBELN"].ToString();
                                drRow["LIFNR"] = tmpRow["LIFNR"].ToString();
                                drRow["OMBLNR"] = tmpRow["OMBLNR"].ToString();
                                drRow["MRGID"] = tmpRow["MRGID"].ToString();
                                drRow["KOSTL"] = tmpRow["KOSTL"].ToString();
                                drRow["ARBPL"] = tmpRow["ARBPL"].ToString();
                                drRow["TRNTP"] = tmpRow["TRNTP"].ToString();
                                drRow["RMAK1"] = "";
                                drRow["INDAT"] = tmpRow["INDAT"].ToString();
                                drRow["BOXID"] = tmpRow["BOXID"].ToString();

                                if (dtPalData.Columns.IndexOf("KDMAT") > -1)
                                    drRow["KDMAT"] = tmpRow["KDMAT"].ToString();
                                else
                                    drRow["KDMAT"] = "";
                                drRow["SERNO"] = tmpRow["SERNO"].ToString();
                                drRow["PKDAT"] = tmpRow["PKDAT"].ToString();

                                Data.Rows.Add(drRow);
                            }
                        }
                        strPdnam = GetPdnam(Data);
                        ShowDataGrid();
                        stsWarning.Text = "";

                        SetControlState(false);
                        this.txtBoxid.Enabled = false;
                        if (chkLocat.Checked)
                        {
                            strLocat = txtLocat.Text.Trim();
                            if (txtLocat.Text.Trim() == "")
                            {
                                stsWarning.Text = "储位不可为空!!";
                                SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                                sp.Play();
                                return;
                            }
                        }
                        if (chkLocat.Checked == false)
                        {
                            objStorageData = new StorageData(CommonInfo.Instance.DBType,
                                CommonInfo.Instance.DBCode.ToString(),
                                CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks,
                                Lgort);
                            //1.根据品名查询已存放料号储位
                            strLocat = objStorageData.QueryLocatByPdnam(Data.Rows[0]["MATNR"].ToString(),
                                Data.Rows[0]["CHARG"].ToString(), Data.Rows[0]["INSMK"].ToString(), "P", strPdnam);

                            //1.已有库存存在同料号、版本且储位存放板数小于储位及可放板数  则优先跑入该库存储位。
                            //strLocat = objStorageData.QueryLocatByMatnr(Data.Rows[0]["MATNR"].ToString(),
                            //    Data.Rows[0]["CHARG"].ToString(), Data.Rows[0]["INSMK"].ToString());

                            //2.已有库存不存在相同料号、版本情况下有空储位直接跑入新储位（按储位优先顺序，储位排序）
                            if (string.IsNullOrEmpty(strLocat) || strLocat == "")
                            {
                                strLocat = objPlantData.GetAutoRunEmptyLocation(Werks, Lgort, "P", strPdnam);
                            }
                            //3.无新储位情况下，计算储位剩余可存放板数，按储位剩余可存放板数>=1排序推荐一个储位。
                            if (string.IsNullOrEmpty(strLocat) || strLocat == "")
                            {
                                strLocat = objPlantData.GetAutoRunAddLocation(Werks, Lgort, "P", strPdnam);
                                dtTemp = objStorageData.QueryLocatInsmk(strLocat);
                                if (string.IsNullOrEmpty(strLocat) && strLocat == "")
                                {
                                    stsWarning.Text = "分储失败!!";
                                    SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                                    sp.Play();
                                    return;
                                }
                                if (dtTemp.Rows.Count == 0)
                                {
                                    SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                                    sp.Play();
                                    stsWarning.Text =
                                        "The location is not a Add-In Location!! Maybe isn't existed or is empty!!";
                                    return;
                                }

                                if (dtTemp.Rows.Count > 1)
                                {
                                    SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                                    sp.Play();
                                    stsWarning.Text =
                                        "The location has different stock and you can't add any new Part No!!";
                                    return;
                                }
                                strInsmk = dtTemp.Rows[0]["INSMK"].ToString();
                            }
                        }

                        txtLocat.Text = strLocat;
                        btnConfirm_Click(null, null);
                    }
                    else if (rdbScattered.Checked)
                    {
                        if (Data.Rows.Count > 0)
                        {
                            drPalletID = dtPalData.Select("BOXID='" + strBoxid + "'");
                            if (drPalletID.Length == 0)
                            {
                                stsWarning.Text = "两次刷入Boxid属于不同栈板号!!";
                                SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                                sp.Play();
                                this.txtBoxid.Enabled = true;
                                this.txtBoxid.Text = "";
                                this.txtBoxid.Focus();
                                return;
                            }
                        }

                        DataTable dtBoxData = objSapData.QueryQMSLineInDataByBoxid(strLocat, strBoxid,
                            dtpIndat.Value.ToString("yyyyMMdd"), "Scattered");

                        dtPalData = objSapData.QueryQMSLineInDataByBoxid(strLocat, strBoxid,
                            dtpIndat.Value.ToString("yyyyMMdd"), "Integer");

                        if (Data.Rows.Count == 0)
                        {
                            ShowDataGridSource();
                            this.txtBoxidCount.Text = dtPalData.Rows.Count.ToString();

                            DataTable dtPartNoSum = CombineStorage(dtPalData);
                            for (int i = 0; i < dtPartNoSum.Rows.Count; i++)
                            {
                                PartNoSum = PartNoSum + Convert.ToInt32(dtPartNoSum.Rows[i]["MENGE"].ToString());
                            }
                            this.txtPartNoSum.Text = PartNoSum.ToString();
                            GetDefaultTable();
                        }
                        

                        if (dtBoxData.Rows.Count == 0)
                        {
                            txtBoxid.Text = "";
                            SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                            sp.Play();
                            throw new Exception("No Data!!");
                        }

                        foreach (DataRow tmpRow in dtBoxData.Rows)
                        {
                            if (
                                strTempMblnrMatnr.IndexOf(tmpRow["MBLNR"].ToString() + tmpRow["MATNR"].ToString() +
                                                          tmpRow["ZEILE"].ToString() + tmpRow["BOXID"].ToString()) == -1)
                            {
                                drRow = Data.NewRow();
                                drRow["MANDT"] = tmpRow["MANDT"].ToString();
                                drRow["COMCD"] = tmpRow["COMCD"].ToString();
                                drRow["WERKS"] = tmpRow["WERKS"].ToString();
                                drRow["LGORT"] = tmpRow["LGORT"].ToString();
                                drRow["MATNR"] = tmpRow["MATNR"].ToString();
                                drRow["INSMK"] = tmpRow["INSMK"].ToString();
                                drRow["CHARG"] = tmpRow["CHARG"].ToString();
                                drRow["MENGE"] = tmpRow["MENGE"].ToString();
                                drRow["OTQTY"] = tmpRow["OTQTY"].ToString();
                                drRow["ALQTY"] = tmpRow["MENGE"].ToString();
                                drRow["MBLNR"] = tmpRow["MBLNR"].ToString();
                                drRow["ZEILE"] = tmpRow["ZEILE"].ToString();
                                drRow["EBELN"] = tmpRow["EBELN"].ToString();
                                drRow["LIFNR"] = tmpRow["LIFNR"].ToString();
                                drRow["OMBLNR"] = tmpRow["OMBLNR"].ToString();
                                drRow["MRGID"] = tmpRow["MRGID"].ToString();
                                drRow["KOSTL"] = tmpRow["KOSTL"].ToString();
                                drRow["ARBPL"] = tmpRow["ARBPL"].ToString();
                                drRow["TRNTP"] = tmpRow["TRNTP"].ToString();
                                drRow["RMAK1"] = "";
                                drRow["INDAT"] = tmpRow["INDAT"].ToString();
                                drRow["BOXID"] = tmpRow["BOXID"].ToString();

                                if (dtBoxData.Columns.IndexOf("KDMAT") > -1)
                                    drRow["KDMAT"] = tmpRow["KDMAT"].ToString();
                                else
                                    drRow["KDMAT"] = "";
                                drRow["SERNO"] = tmpRow["SERNO"].ToString();
                                drRow["PKDAT"] = tmpRow["PKDAT"].ToString();

                                Data.Rows.Add(drRow);
                            }
                            else
                            {
                                stsWarning.Text = "重复刷入Boxid!!";
                                this.txtBoxid.Enabled = true;
                                this.txtBoxid.Text = "";
                                this.txtBoxid.Focus();
                                this.txtBoxid.SelectAll();
                                SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                                sp.Play();
                                return;
                            }
                        }
                        strPdnam = GetPdnam(Data);
                        ShowDataGrid();
                        stsWarning.Text = "";

                        SetControlState(false);
                        this.txtBoxid.Enabled = false;
                        if (chkLocat.Checked)
                        {
                            strLocat = txtLocat.Text.Trim();
                            if (txtLocat.Text.Trim() == "")
                            {
                                stsWarning.Text = "储位不可为空!!";
                                SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                                sp.Play();
                                return;
                            }
                        }
                        if (chkLocat.Checked == false)
                        {
                            //1.已有库存存在同料号、版本且储位存放板数小于储位及可放板数  则优先跑入该库存储位。
                            objStorageData = new StorageData(CommonInfo.Instance.DBType,
                                CommonInfo.Instance.DBCode.ToString(),
                                CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks,
                                Lgort);
                            strLocat = objStorageData.QueryLocatByMatnr(Data.Rows[0]["MATNR"].ToString(),
                                Data.Rows[0]["CHARG"].ToString(), Data.Rows[0]["INSMK"].ToString());
                            dtTemp = objStorageData.QueryLocatInsmk(strLocat);

                            //2.已有库存不存在相同料号、版本情况下有空储位直接跑入新储位（按储位优先顺序，储位排序）
                            if (string.IsNullOrEmpty(strLocat) && strLocat == "")
                            {
                                strLocat = objPlantData.GetAutoRunEmptyLocation(Werks, Lgort, "NP", "");
                            }
                            //3.无新储位情况下，计算储位剩余可存放板数，按储位剩余可存放板数>=1排序推荐一个储位。
                            if (string.IsNullOrEmpty(strLocat) && strLocat == "")
                            {
                                strLocat = objPlantData.GetAutoRunAddLocation(Werks, Lgort, "NP", "");
                                dtTemp = objStorageData.QueryLocatInsmk(strLocat);
                                if (string.IsNullOrEmpty(strLocat) && strLocat == "")
                                {
                                    stsWarning.Text = "分储失败!!";
                                    SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                                    sp.Play();
                                    return;
                                }
                                if (dtTemp.Rows.Count == 0)
                                {
                                    SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                                    sp.Play();
                                    stsWarning.Text =
                                        "The location is not a Add-In Location!! Maybe isn't existed or is empty!!";
                                    return;
                                }

                                if (dtTemp.Rows.Count > 1)
                                {
                                    SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
                                    sp.Play();
                                    stsWarning.Text =
                                        "The location has different stock and you can't add any new Part No!!";
                                    return;
                                }
                                strInsmk = dtTemp.Rows[0]["INSMK"].ToString();
                            }
                        }
                        
                        for (int j = 0; j < Data.Rows.Count; j++)
                        {
                            for (int i = dtPalData.Rows.Count - 1; i >= 0; i--)
                            {
                                if (dtPalData.Rows[i]["BOXID"].ToString() == Data.Rows[j]["BOXID"].ToString())
                                {
                                    dtPalData.Rows.RemoveAt(i);
                                }
                            }
                        }
                        
                        
                        for (int i = dtPalData.Rows.Count-1 ; i>=0 ; i--)
                        {
                            if (dtPalData.Rows[i]["BOXID"].ToString() == strBoxid)
                            {
                                dtPalData.Rows.RemoveAt(i);
                            }
                        }
                        ShowDataGridSource();
                        ScanCount++;
                        txtLocat.Text = strLocat;
                        btnConfirm_Click(null, null);
                        if (Convert.ToInt32(this.txtBoxidCount.Text.ToString()) == ScanCount)
                        {
                            SoundPlayer sp = new SoundPlayer(@"Sound\OK2.wav");
                            sp.Play();
                            this.txtBoxid.Enabled = false;
                            btnSave_Click(null, null);
                            Refresh();
                            ScanCount = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    this.txtBoxid.Text = "";
                    SoundPlayer sp = new SoundPlayer(@"Sound\rescan.wav");
                    sp.Play();
                    return;
                }
            }
        }
        #endregion

        #region GetDefaultTable
        private void GetDefaultTable()
        {
            if (Data.Rows.Count == 0)
            {
                DataColumn[] dcPrimaryKey = new DataColumn[3];
                dcPrimaryKey[0] = Data.Columns["MBLNR"];
                dcPrimaryKey[1] = Data.Columns["MATNR"];
                dcPrimaryKey[2] = Data.Columns["SERNO"];
                Data.PrimaryKey = dcPrimaryKey;

                Data = new DataTable();
                Data.Columns.Add("MANDT", Type.GetType());
                Data.Columns.Add("COMCD", Type.GetType());
                Data.Columns.Add("WERKS", Type.GetType());
                Data.Columns.Add("LGORT", Type.GetType());
                Data.Columns.Add("LOCAT", Type.GetType());
                Data.Columns.Add("MATNR", Type.GetType());
                Data.Columns.Add("INSMK", Type.GetType());
                Data.Columns.Add("CHARG", Type.GetType());
                Data.Columns.Add("MENGE", Type.GetType());
                Data.Columns.Add("OTQTY", Type.GetType());
                Data.Columns.Add("ALQTY", Type.GetType());
                Data.Columns.Add("MBLNR", Type.GetType());
                Data.Columns.Add("ZEILE", Type.GetType());
                Data.Columns.Add("EBELN", Type.GetType());
                Data.Columns.Add("LIFNR", Type.GetType());
                Data.Columns.Add("OMBLN", Type.GetType());
                Data.Columns.Add("OMBLNR", Type.GetType());
                Data.Columns.Add("MRGID", Type.GetType());
                Data.Columns.Add("KOSTL", Type.GetType());
                Data.Columns.Add("ARBPL", Type.GetType());
                Data.Columns.Add("TRNTP", Type.GetType());
                Data.Columns.Add("RMAK1", Type.GetType());
                Data.Columns.Add("INDAT", Type.GetType());
                Data.Columns.Add("KDMAT", Type.GetType());
                Data.Columns.Add("SERNO", Type.GetType());
                Data.Columns.Add("NMBLN", Type.GetType());
                Data.Columns.Add("REFID", Type.GetType());
                Data.Columns.Add("OTLGT", Type.GetType());
                Data.Columns.Add("DIDNO", Type.GetType());
                Data.Columns.Add("PKDAT", Type.GetType()); 
                Data.Columns.Add("BOXID", Type.GetType());
            }
        }
        #endregion

        //private void btnQueryRefid_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
        //        DataTable dtRefID = new DataTable();
        //        string strCrdat = dtpIndat.Value.ToString("yyyy-MM-dd");
        //        strWerks = cmbWerks.SelectedItem.ToString();
        //        strLgort = cmbLgort.SelectedItem.ToString();

        //        if (Werks == "" || Lgort == "")
        //        {
        //            stsWarning.Text = "Plant and storage can't be empty!!";
        //            return;
        //        }

        //        dtRefID = objPlantData.GetAllRefIDData(this.strMandt, this.Comcd, this.strWerks, this.strLgort, strCrdat);
        //        StorageIn_SMT_Query_RefID objStorageIn_SMT_Query_RefID = new StorageIn_SMT_Query_RefID(dtRefID, dtData, "Pallet", UserData);
        //        objStorageIn_SMT_Query_RefID.Show();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        #region 判斷是否可關閉Form視窗
        private void StorageIn_Pallet_OnLineIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AllowToClose;

            if (e.Cancel = !AllowToClose)
            {
                stsWarning.Text = "扣SAP帳正在處理中，無法關閉視窗!!";
            }
        }
        #endregion

        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    #region "將Location 填回Data中"
                    foreach (DataRow rsTmpRow in this.Data.Rows)
                    {
                        rsTmpRow["LOCAT"] = this.txtLocat.Text.Trim();
                    }
                    this.Data.AcceptChanges();

                    #endregion

                    this.txtLocat.Enabled = true;
                    this.btnConfirm.Enabled = false;
                    btnAdd_Click(null, null);
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rdbScattered.Checked)
            {
                ReportPrint objReportPrint = new ReportPrint(UserData, "PLASTICMATERIALLOCATLABEL_SCATTERED", dtPrint);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            if (rdbInteger.Checked)
            {
                ReportPrint objReportPrint = new ReportPrint(UserData, "PLASTICMATERIALLOCATLABEL_INTEGER", dtPrint);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
        }

        private DataTable CombineStorage(DataTable dtData)
        {
            StringBuilder sbCombineIndex = new StringBuilder();
            ArrayList alAllCombine = new ArrayList();
            DataRow[] combineRow;
            DataRow drRow;
            dtCombineStorage = new DataTable();
            dtCombineStorage = dtData.Clone();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                //strTempCombine = dtTempStorage.Rows[i]["MANDT"].ToString() + dtTempStorage.Rows[i]["COMCD"].ToString() + dtTempStorage.Rows[i]["WERKS"].ToString() + dtTempStorage.Rows[i]["LGORT"].ToString() + dtTempStorage.Rows[i]["LOCAT"].ToString() + dtTempStorage.Rows[i]["MATNR"].ToString() + dtTempStorage.Rows[i]["INSMK"].ToString() + dtTempStorage.Rows[i]["CHARG"].ToString() + ";";

                #region 每次比對的Index (sbCombineIndex)

                sbCombineIndex.Remove(0, sbCombineIndex.Length);
                sbCombineIndex.Append("MANDT='" + dtData.Rows[i]["MANDT"].ToString() + "'");
                sbCombineIndex.Append(" and COMCD='" + dtData.Rows[i]["COMCD"].ToString() + "'");
                sbCombineIndex.Append(" and WERKS='" + dtData.Rows[i]["WERKS"].ToString() + "'");
                sbCombineIndex.Append(" and LGORT='" + dtData.Rows[i]["LGORT"].ToString() + "'");
                sbCombineIndex.Append(" and LOCAT='" + dtData.Rows[i]["LOCAT"].ToString() + "'");
                sbCombineIndex.Append(" and MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "'");
                sbCombineIndex.Append(" and INSMK='" + dtData.Rows[i]["INSMK"].ToString() + "'");
                sbCombineIndex.Append(" and CHARG='" + dtData.Rows[i]["CHARG"].ToString() + "'");
                #endregion

                if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
                {
                    alAllCombine.Add(sbCombineIndex.ToString());
                    int intCombineLocalTotal = 0;

                    combineRow = dtData.Select(sbCombineIndex.ToString());
                    for (int j = 0; j < combineRow.Length; j++)
                    {
                        intCombineLocalTotal += Int32.Parse(combineRow[j]["MENGE"].ToString());
                    }
                    drRow = dtCombineStorage.NewRow();

                    drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString();
                    drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString();
                    drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                    drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                    drRow["LOCAT"] = dtData.Rows[i]["LOCAT"].ToString();
                    drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                    drRow["INSMK"] = dtData.Rows[i]["INSMK"].ToString();
                    drRow["CHARG"] = dtData.Rows[i]["CHARG"].ToString();
                    drRow["MENGE"] = intCombineLocalTotal;
                    drRow["OTQTY"] = dtData.Rows[i]["OTQTY"].ToString();
                    drRow["ALQTY"] = intCombineLocalTotal;
                    drRow["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString();
                    drRow["ZEILE"] = dtData.Rows[i]["ZEILE"].ToString();
                    drRow["EBELN"] = dtData.Rows[i]["EBELN"].ToString();
                    drRow["LIFNR"] = dtData.Rows[i]["LIFNR"].ToString();
                    drRow["OMBLNR"] = dtData.Rows[i]["OMBLNR"].ToString();
                    drRow["MRGID"] = dtData.Rows[i]["MRGID"].ToString();
                    drRow["KOSTL"] = dtData.Rows[i]["KOSTL"].ToString();
                    drRow["ARBPL"] = dtData.Rows[i]["ARBPL"].ToString();
                    drRow["TRNTP"] = dtData.Rows[i]["TRNTP"].ToString();
                    drRow["RMAK1"] = "";
                    drRow["INDAT"] = dtData.Rows[i]["INDAT"].ToString();
                    drRow["BOXID"] = dtData.Rows[i]["BOXID"].ToString();

                    if (dtData.Columns.IndexOf("KDMAT") > -1)
                        drRow["KDMAT"] = dtData.Rows[i]["KDMAT"].ToString();
                    else
                        drRow["KDMAT"] = "";
                    drRow["SERNO"] = dtData.Rows[i]["SERNO"].ToString();
                    drRow["PKDAT"] = dtData.Rows[i]["PKDAT"];
                    dtCombineStorage.Rows.Add(drRow);
                }
            }
            return dtCombineStorage;
        }

        private void Refresh()
        {
            this.dtData.Rows.Clear();
            this.dtPrint.Rows.Clear();
            this.dtPalData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.dgvSource.DataSource = null;
            this.txtBoxid.Enabled = true;
            this.txtBoxid.Text = "";
            this.txtBoxid.Focus();
            this.txtBoxid.SelectAll();
            this.lblSource.Text = "0 records";
            this.lblData.Text = "0 records";
            this.txtBoxidCount.Text = "";
            this.txtPartNoSum.Text = "";
            PartNoSum = 0;
        }

        private string GetPdnam(DataTable dtData)
        {
            string strPdnam67 = dtData.Rows[0]["MATNR"].ToString().Substring(5, 2);
            if (strPdnam67 == "TA" || strPdnam67 == "TC")
            {
                strPdnam = "TOP:TA/TC";
            }
            else if (strPdnam67 == "LA" || strPdnam67 == "LC")
            {
                strPdnam = "COVER: LA/LC";
            }
            else if (strPdnam67 == "BA")
            {
                strPdnam = "BASE: BA";
            }
            else if (strPdnam67 == "LB")
            {
                strPdnam = "BEZEL:LB";
            }
            else
            {
                strPdnam = "OTHER";
            }
            return strPdnam;
        }

        private void chkLocat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLocat.Checked)
            {
                this.txtLocat.Enabled = true;
            }
            else
            {
                this.txtLocat.Enabled = false;
            }
        }
    }
}
