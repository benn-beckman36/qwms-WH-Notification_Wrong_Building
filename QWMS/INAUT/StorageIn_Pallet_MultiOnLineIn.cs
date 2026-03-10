using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using System.Media;
using QWMS.Common;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace QWMS
{
    public partial class StorageIn_Pallet_MultiOnLineIn : Form
    {

        #region 变量
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strCtbto = "";
        private string strRegon = "";
        private string strMachine = "";
        private string strPalid = "";
        private string strComcd = "";
        private string limitWerks = "";
        private string strInputType = "";//输入PID还是输入RID入库
        private StorageData objStorageData;
        private List<string> lsMblnr = new List<string>();//刷入单据
        UserInfo UserData = new UserInfo();
        private bool AllowToClose = true;//設定能否關閉Form視窗（扣账时）
        private bool bolDuplicate = false;
        private DataTable dtData = new DataTable();
        string strMblnrMatnr = "";//PalletID入库标识刷入是否重复
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

        //public string Locat
        //{
        //    get
        //    {
        //        return this.txtLocat.Text.Trim();
        //    }
        //    set
        //    {
        //        this.txtLocat.Text = value;
        //    }
        //}

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
        //20080910 add Palid By Rock
        public string Palid
        {
            get
            {
                return strPalid;
            }
            set
            {
                strPalid = value;
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

        #region 初始化
        public StorageIn_Pallet_MultiOnLineIn(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            strComcd = varUserData.CompanyCode;

            try
            {
                StorageIn objStorageIn = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
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
                    limitWerks = objStorageIn.getLimitWerks();
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

        #endregion

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
                Authority objAuthority = new Authority(UserData);
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
                Authority objAuthority = new Authority(UserData);
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

        #region ShowNewDataGrid
        public void ShowNewDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "MBLNR";
                dgvcRefid.HeaderText = "Mblnr";
                dgvcRefid.Width = 140;
                dgvcRefid.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRefid);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "WO Item";
                dgvcZeile.Width = 100;
                dgvcZeile.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcOlgrt = new DataGridViewTextBoxColumn();
                dgvcOlgrt.DataPropertyName = "OTLGT";
                dgvcOlgrt.HeaderText = "Original Storage";
                dgvcOlgrt.Width = 90;
                dgvcOlgrt.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOlgrt);

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
                throw new Exception(ex.Message + "<-ShowNewDataGrid()");
            }
        }
        #endregion

        #region New\Add-LCM\MLB按钮

        #region rdoNew_CheckedChanged
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            this.rdoLCM.Enabled = true;
            this.rdoMLB.Enabled = true;
        }
        #endregion

        #region rdoAdd_CheckedChanged
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            this.rdoLCM.Enabled = true;
            this.rdoMLB.Enabled = true;
        }
        #endregion

        #region rbtMLB_CheckedChanged
        private void rbtMLB_CheckedChanged(object sender, EventArgs e)
        {
            strInputType = "RID";
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            txtRefid.Enabled = true;
            txtPalid.Enabled = false;

        }
        #endregion

        #region rbtLCM_CheckedChanged
        private void rbtLCM_CheckedChanged(object sender, EventArgs e)
        {
            strInputType = string.Empty;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            txtPalid.Enabled = true;
            txtRefid.Enabled = true;
        }
        #endregion

        #endregion

        #region 辅助方法

        #region GetDefaultTable 无数据时栏位
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
                Data.Columns.Add("COMCD", Type.GetType());
            }
        }
        #endregion

        #region judgeEnter 厂区仓别判断
        //CTRLC4:平置仓Traditional    CTRLC5:变动储位固定储位
        private void judgeEnter()
        {
            try
            {
                #region 检查厂区仓别资料

                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();

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
                PlantData objPlantData = new PlantData(UserData);
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
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }

        #endregion

        #region suggestLocat 抓取建议储位
        private void suggestLocat()
        {
            try
            {
                #region  抓取建议储位
                stsWarning.Text = "";
                PlantData objPlantData = new PlantData(UserData);
                //string strStdMatnr = Data.Rows[0]["MATNR"].ToString().Trim();
                //string strStdKdmat = Data.Rows[0]["KDMAT"].ToString().Trim();
                this.Ctbto = "";
                this.Regon = "";
                this.Machine = "";
                //變動儲位才需要檢查儲位有沒有輸入
                if (Lotyp == "DYNAMIC LOCATION")
                {
                    if (strType == "NEW")//得到空储位
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            txtLocat.Text = objPlantData.GetEmptyLocation(Werks, Lgort, this.Ctbto.Trim(),
                                this.Regon.Trim(), this.Machine.Trim());
                        }
                    }
                }
                #endregion

                txtLocat.Enabled = true;
                txtLocat.Focus();
                txtLocat.SelectAll();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }

        #endregion

        #region successSound 成功声音
        private void successSound()
        {
            try
            {
                SoundPlayer sp = new SoundPlayer(@"Sound\OK1.wav");
                sp.Play();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }

        #endregion

        #region failSound 失败声音
        private void failSound()
        {
            try
            {
                SoundPlayer sp = new SoundPlayer(@"Sound\NG.wav");
                sp.Play();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }

        #endregion

        #region statusPallet PID全选
        private void statusPallet()
        {
            txtPalid.Focus();
            txtPalid.SelectAll();
        }

        #endregion

        #region statusRefid RID全选
        private void statusRefid()
        {
            txtRefid.Focus();
            txtRefid.SelectAll();
        }

        #endregion

        #region statusLocat 储位全选
        private void statusLocat()
        {
            txtLocat.Focus();
            txtLocat.SelectAll();
        }

        #endregion

        #region 改变dt中的单据状态为N
        private void changeProcessing(DataTable dt)
        {
            try
            {
                StorageIn objStorageInQWMS = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                var l1 = (from d in dt.AsEnumerable() select d.Field<string>("MBLNR")).Distinct().ToList();
                for (int i = 0; i < l1.Count; i++)
                {
                    string sm = l1[i].ToString();
                    if (strInputType.Equals("RID"))//ReferenceID有值
                        objStorageInQWMS.UpdateStatus(sm, "RID", "N");
                    else if (strInputType.Equals("PID"))//PalletID有值 
                        objStorageInQWMS.UpdateStatus(sm, "PID", "N");
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }

        #endregion

        #endregion

        #region txtPalid_KeyDown
        private void txtPalid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    #region 变量

                    stsWarning.Text = "";
                    DataTable dtTemp = new DataTable();
                    dtpIndat.Value = DateTime.Today;
                    string strPalid = txtPalid.Text.Trim();
                    string strLocat = txtLocat.Text.Trim();
                    if (rdoLCM.Checked && string.IsNullOrEmpty(strInputType))
                    {
                        strInputType = "PID";
                        txtRefid.Enabled = false;
                    }

                    #endregion

                    #region 防呆—厂区仓别、PID为空、重复刷入以及单据状态processing
                    //PROCESSINGH为"Y"正在被处理，一分钟后重试（JOB自动解锁）

                    judgeEnter();//厂区仓别判断

                    SapData objSapData = new SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    if (strPalid == "")
                    {
                        stsWarning.Text = "Pallet ID. can't be Empty!!";
                        return;
                    }

                    DataTable dtPalData = objSapData.QueryQMSLineInDataByPalid(strLocat, strPalid, dtpIndat.Value.ToString("yyyyMMdd"));//MENGE>OTQTY
                    if (dtPalData.Rows.Count <= 0)
                    {
                        stsWarning.Text = strPalid.Trim() + "No Data!!";//NO Data!=不存在或MENGE=OTQTY
                        statusPallet();
                        return;
                    }

                    for (int i = 0; i < dtPalData.Rows.Count; i++)
                    {
                        if (dtPalData.Rows[i]["PROCESSING"].ToString().Trim() == "Y")
                        {
                            MessageBox.Show("This PID is processing," + Environment.NewLine + "please try again later!!", "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            statusPallet();
                            return;
                        }
                        if (strMblnrMatnr.IndexOf(dtPalData.Rows[i]["MBLNR"].ToString() + dtPalData.Rows[i]["MATNR"].ToString() + dtPalData.Rows[i]["ZEILE"].ToString()) != -1)
                        {
                            stsWarning.Text = "The Pallet ID and Part No you input is duplicate!!";
                            statusPallet();
                            return;
                        }
                        else
                        {
                            strMblnrMatnr = strMblnrMatnr + dtPalData.Rows[i]["MBLNR"].ToString() + dtPalData.Rows[i]["MATNR"].ToString() + dtPalData.Rows[i]["ZEILE"].ToString();
                        }
                    }

                    #endregion

                    #region 格式
                    if (Data.Rows.Count == 0)
                    {
                        GetDefaultTable();
                    }
                    DataRow drRow;
                    foreach (DataRow tmpRow in dtPalData.Rows)
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

                        if (dtPalData.Columns.IndexOf("KDMAT") > -1)
                            drRow["KDMAT"] = tmpRow["KDMAT"].ToString();
                        else
                            drRow["KDMAT"] = "";
                        drRow["SERNO"] = tmpRow["SERNO"].ToString();
                        drRow["PKDAT"] = tmpRow["PKDAT"].ToString();

                        Data.Rows.Add(drRow);
                    }
                    ShowDataGrid();
                    #endregion

                    if (!lsMblnr.Contains(strPalid))
                    {
                        lsMblnr.Add(strPalid);
                    }
                    this.dtpIndat.Enabled = false;
                    this.cmbWerks.Enabled = false;
                    this.cmbLgort.Enabled = false;
                    this.txtPalid.Enabled = false;
                    this.txtRefid.Enabled = false;
                    successSound();//声音
                    suggestLocat();//抓取建议储位

                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    failSound();
                    statusPallet();
                    return;
                }
            }
        }
        #endregion

        #region txtRefid_KeyDown
        private void txtRefid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    #region 变量声明

                    stsWarning.Text = "";
                    DataTable dtTemp = new DataTable();
                    string strRefid = txtRefid.Text.Trim();
                    string strLocat = txtLocat.Text.Trim();
                    if (rdoLCM.Checked && string.IsNullOrEmpty(strInputType))
                    {
                        strInputType = "RID";
                        txtPalid.Enabled = false;
                    }
                    PlantData objPlantData = new PlantData(UserData);

                    #endregion

                    #region 防呆—厂区仓别、RID为空、重复刷入

                    judgeEnter();//厂区仓别判断

                    if (strRefid == "")
                    {
                        stsWarning.Text = "Reference ID. can't be Empty!!";
                        return;
                    }

                    if (Data.Rows.Count > 0)
                    {
                        for (int i = 0; i < Data.Rows.Count; i++)
                        {
                            if (strRefid == Data.Rows[i]["MBLNR"].ToString().Trim())
                            {
                                statusRefid();
                                stsWarning.Text = strRefid.Trim() + "已刷入，请确认!!!";
                                return;
                            }
                        }
                    }

                    #endregion

                    #region 刷入不为空且MENGE>OTQTY，单据状态

                    SapData objSapData = new SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    DataTable dtRefidData = objSapData.QueryQMSLineInDataByRefid(strLocat, strRefid, dtpIndat.Value.ToString("yyyyMMdd"));

                    if (dtRefidData.Rows.Count <= 0)
                    {
                        stsWarning.Text = strRefid.Trim() + "No Data!!";
                        txtRefid.Focus();
                        txtRefid.SelectAll();
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < dtRefidData.Rows.Count; i++)
                        {
                            if (dtRefidData.Rows[i]["PROCESSING"].ToString().Trim() == "Y")
                            {
                                MessageBox.Show("This PID is processing," + Environment.NewLine + "please try again later!!", "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                statusPallet();
                                return;
                            }
                        }
                    }

                    #endregion

                    #region 格式
                    if (Data.Rows.Count == 0)
                    {
                        GetDefaultTable();
                    }

                    DataRow drRow;
                    foreach (DataRow tmpRow in dtRefidData.Rows)
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
                        drRow["MBLNR"] = tmpRow["REFID"].ToString(); //REFID作为MBLNR
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
                        drRow["REFID"] = tmpRow["REFID"].ToString();
                        drRow["NMBLN"] = tmpRow["NMBLN"].ToString();//MBLNR放在NMBLN栏位（以后用于判断是RID还是PID入库）
                        drRow["OTLGT"] = tmpRow["OTLGT"].ToString();
                        drRow["DIDNO"] = tmpRow["DIDNO"].ToString();
                        if (tmpRow["PKDAT"].ToString() != "")
                        {
                            drRow["PKDAT"] = DateTime.Parse(tmpRow["PKDAT"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            drRow["PKDAT"] = "";
                        }

                        if (dtRefidData.Columns.IndexOf("KDMAT") > -1)
                            drRow["KDMAT"] = tmpRow["KDMAT"].ToString();
                        else
                            drRow["KDMAT"] = "";
                        drRow["SERNO"] = tmpRow["SERNO"].ToString();

                        Data.Rows.Add(drRow);
                    }
                    ShowNewDataGrid();
                    #endregion

                    if (!lsMblnr.Contains(strRefid))
                    {
                        lsMblnr.Add(strRefid);
                    }
                    this.dtpIndat.Enabled = false;
                    this.cmbWerks.Enabled = false;
                    this.cmbLgort.Enabled = false;
                    this.txtRefid.Enabled = false;
                    this.txtPalid.Enabled = false;
                    successSound();
                    suggestLocat();//抓取建议储位                    
                }
                catch (Exception ex)
                {
                    failSound();
                    statusRefid();
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }

        #endregion

        #region txtLocat_DoubleClick
        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
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
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type, this.Ctbto, this.Regon);
                    objStorageIn_LocationSelect.ShowDialog();
                    txtLocat.Text = objStorageIn_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region txtLocat_KeyPress
        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    stsWarning.Text = "";
                    DataTable dtTemp = new DataTable();
                    PlantData objPlantData = new PlantData(UserData);

                    #region 储位库别检查
                    //变动储位才需要检查储位有没有输入
                    if (Lotyp == "DYNAMIC LOCATION")
                    {
                        #region 储位检查
                        if (strType == "ADD")
                        {
                            //储位为空
                            if (txtLocat.Text.Trim() == "")
                            {
                                stsWarning.Text = "Location is empty!!";
                                this.txtLocat.Focus();
                                return;
                            }
                        }

                        if (strType == "NEW")
                        {
                            if (txtLocat.Text.Trim() == "")
                            {
                                txtLocat.Text = objPlantData.GetEmptyLocation(Werks, Lgort, this.Ctbto, this.Regon, this.Machine);
                            }
                            else
                            {
                                if (objPlantData.CheckStorageData(Werks, Lgort, txtLocat.Text.ToString().Trim().ToUpper()))
                                {
                                    stsWarning.Text = "The location you input is not a empty location!!";
                                    this.txtLocat.Focus();
                                    return;
                                }
                            }
                        }

                        //储位不能为空
                        if (txtLocat.Text.Trim() == "")
                        {
                            stsWarning.Text = "Please input a location first!!";
                            this.txtLocat.Focus();
                            return;
                        }

                        //储位是否存在
                        if (!objPlantData.CheckExistedStorageData(Werks, Lgort, txtLocat.Text.Trim().ToUpper()))
                        {
                            stsWarning.Text = "The location doesn't exist!!";
                            this.txtLocat.Focus();
                            return;
                        }
                        #endregion

                        #region 找出库别
                        StorageData objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                        if (this.rdoAdd.Checked)
                        {
                            dtTemp = objStorageData.QueryLocatInsmk(txtLocat.Text.Trim().ToUpper());

                            if (dtTemp.Rows.Count == 0)
                            {
                                stsWarning.Text = "The location is not a Add-In Location!! Maybe isn't existed or is empty!!";
                                return;
                            }

                            if (dtTemp.Rows.Count > 1)
                            {
                                stsWarning.Text = "The location has different stock and you can't add any new Part No!!";
                                return;
                            }
                            strInsmk = dtTemp.Rows[0]["INSMK"].ToString();
                        }
                        if (rdoNew.Checked)
                        {
                            if (dtData.Rows.Count == 0 || dtData == null)
                            {
                                strInsmk = "";
                            }
                            else
                            {
                                strInsmk = dtData.Rows[0]["INSMK"].ToString();
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //固定储位时Location不能输入
                        if (this.txtLocat.Text.Trim() != "")
                        {
                            stsWarning.Text = "You can't input location because " + strLgort + " is a fixed-Location storage!!";
                            this.txtLocat.Focus();
                            return;
                        }
                        else
                        {
                            strInsmk = "";
                        }
                    }
                    #endregion

                    #region "將Location 填回Data中"

                    string strMblnr;
                    string strLocat = txtLocat.Text.Trim();

                    if (strInputType == "RID")
                    {
                        strMblnr = txtRefid.Text.ToString().Trim();
                    }
                    else
                    {
                        strMblnr = txtPalid.Text.ToString().Trim();
                    }
                    foreach (DataRow rsTmpRow in this.Data.Rows)
                    {
                        if (rsTmpRow["LOCAT"].ToString().Trim() == "" && rsTmpRow["MBLNR"].ToString().Trim() == strMblnr)
                        {
                            rsTmpRow["LOCAT"] = strLocat;
                        }
                    }
                    this.Data.AcceptChanges();
                    statusLocat();
                    txtLocat.Clear();
                    txtLocat.Enabled = false;

                    if (strInputType == "RID")
                    {
                        ShowNewDataGrid();
                        this.txtRefid.Enabled = true;
                        txtRefid.Clear();
                        txtRefid.Focus();
                    }
                    else
                    {
                        ShowDataGrid();
                        this.txtPalid.Enabled = true;
                        txtPalid.Clear();
                        txtPalid.Focus();
                    }
                    this.btnSave.Enabled = true;

                    #endregion

                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    statusLocat();
                    return;
                }
            }
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            StorageIn objStorageInQWMS = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            btnSave.Enabled = false;
            this.txtPalid.Enabled = false;
            this.txtRefid.Enabled = false;
            LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
            try
            {
                #region 校验入库数据是否有储位信息，若为空则更新储位信息
                foreach (DataRow dr in dtData.Rows)
                {
                    if (string.IsNullOrEmpty(dr["LOCAT"].ToString()))
                    {
                        MessageBox.Show("入库数据储位不能为空!!");
                        return;
                    }
                }
                #endregion

                if (dtData.Rows.Count > 0)
                {
                    #region 變數宣告
                    string strTempMblnrMatnr = "";
                    stsWarning.Text = "";
                    DataTable dtTemp = new DataTable();
                    string OMBLN = "";
                    StorageData objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    DataTable dtData101 = dtData.Clone();//101数据
                    DataTable dtData311 = dtData.Clone();//311数据（OTLGT不为空）
                    DataTable dtDataQwms = dtData.Clone();//仅入QWMS
                    #endregion

                    #region CS12 厂区QWMS请帮忙设定卡关机制，同料号不同版本不能入在相同储位。谢谢，仓别：TW30,TW31,TW33
                    if (strWerks == "CS12" && cmbLgort.Text.ToString() == "TW30" || cmbLgort.Text.ToString() == "TW31" || cmbLgort.Text.ToString() == "TW33")
                    {
                        foreach (DataRow dr1 in dtData.Rows)
                        {
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                if (dr1["MATNR"].ToString().Trim() == dtData.Rows[i]["MATNR"].ToString().Trim() && dr1["CHARG"].ToString().Trim() != dtData.Rows[i]["CHARG"].ToString().Trim())
                                {
                                    stsWarning.Text = "CS12厂区TW30,TW31,TW33下同料号不同版本不能入在相同储位,谢谢!!";
                                    return;
                                }
                            }
                        }

                        objStorageData = new StorageData(UserData, Werks, Lgort);
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            string strloc = dtData.Rows[i]["LOCAT"].ToString();
                            string strmatnr = dtData.Rows[i]["MATNR"].ToString();
                            string strcharg = dtData.Rows[i]["CHARG"].ToString();
                            if (objStorageData.CheckExistedSameLocation(strloc))
                            {
                                bool bl2 = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                                if (bl2)
                                {
                                    bool bl = objStorageData.CheckExistedSameMaterialCharg(strloc, strmatnr, strcharg);
                                    if (!bl)
                                    {
                                        stsWarning.Text = "料号版本不一致不能入储";
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region CS12 厂区QWMS请帮忙设定卡关机制，同料号不同版本不能入在相同储位。谢谢，仓别：TW30,TW31,TW33
                    if (strWerks == "CS42" )
                    {
                        foreach (DataRow dr1 in dtData.Rows)
                        {
                            for (int i = 0; i < dtData.Rows.Count; i++)
                            {
                                if (dr1["MATNR"].ToString().Trim() == dtData.Rows[i]["MATNR"].ToString().Trim() && dr1["CHARG"].ToString().Trim() != dtData.Rows[i]["CHARG"].ToString().Trim())
                                {
                                    stsWarning.Text = "CS42下同料号不同版本不能入在相同储位,谢谢!!";
                                    return;
                                }
                            }
                        }

                        objStorageData = new StorageData(UserData, Werks, Lgort);
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            string strloc = dtData.Rows[i]["LOCAT"].ToString();
                            string strmatnr = dtData.Rows[i]["MATNR"].ToString();
                            string strcharg = dtData.Rows[i]["CHARG"].ToString();
                            if (objStorageData.CheckExistedSameLocation(strloc))
                            {
                                bool bl2 = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                                if (bl2)
                                {
                                    bool bl = objStorageData.CheckExistedSameMaterialCharg(strloc, strmatnr, strcharg);
                                    if (!bl)
                                    {
                                        stsWarning.Text = "料号版本不一致不能入储";
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region 个别仓别有特殊要求
                    if ((strWerks == "CS32" && strLgort == "TW20") || (strWerks == "CS90" && (strLgort == "TW20" || strLgort == "TW70" || strLgort == "TW50")) || (strWerks == "CS91" && (strLgort == "TW20" || strLgort == "TW50" || strLgort == "TW51")) || (strWerks == "CS92" && (strLgort == "TW20" || strLgort == "TW51" || strLgort == "TW50")) || (strWerks == "CS42" && (strLgort == "TW31" || strLgort == "TW34")))
                    {
                        #region  SAM zhang 要求同料号入库并储不能放置在同一储位。 by blank

                        foreach (DataRow dr1 in dtData.Rows)
                        {
                            string strloc = dr1["LOCAT"].ToString();
                            string strinsmk = dr1["INSMK"].ToString();
                            string strmatnr = dr1["MATNR"].ToString();
                            string strvander = dr1["LIFNR"].ToString();
                            string strcharg = dr1["CHARG"].ToString();
                            //判断库存中是否存在同储位同料号数据
                            bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                            if (bl)
                            {
                                stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许入储！";
                                return;
                            }
                        }
                        #endregion
                    }
                    else if ((strWerks == "CS20" && (strLgort == "TW22" || strLgort == "TW25" || strLgort == "TWEJ" || strLgort == "TW91" || strLgort == "TW31" || strLgort == "TW51" || strLgort == "TW15")) || (strWerks == "CS12" && (strLgort == "TW20" || strLgort == "TW11" || strLgort == "TW60" || strLgort == "TW31")))
                    {
                        #region  SAM zhang 要求同料号入库并储不能放置在同一储位。 by blank

                        foreach (DataRow dr1 in dtData.Rows)
                        {
                            string strloc = dr1["LOCAT"].ToString();
                            string strinsmk = dr1["INSMK"].ToString();
                            string strmatnr = dr1["MATNR"].ToString();
                            string strvander = dr1["LIFNR"].ToString();
                            string strcharg = dr1["CHARG"].ToString();

                            //判断库存中是否存在同储位同料号数据
                            bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                            if (bl)
                            {
                                stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许并储！";
                                return;
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region  判断是否重复刷入单据 (在单据回车处已判断过)
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (strTempMblnrMatnr.IndexOf(dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["ZEILE"].ToString()) == -1)
                        {
                            strTempMblnrMatnr += dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + dtData.Rows[i]["ZEILE"].ToString() + ";";
                        }
                        else
                        {
                            stsWarning.Text = "The Pallet ID and Part No you input is duplicate!!";
                            this.btnSave.Enabled = true;
                            return;
                        }
                        ////檢查要入庫的單號之前是否有使用連板入庫的方式入庫
                        //dtTemp = objStorageData.QueryNotCombineInData(Locat, dt.Rows[i]["MBLNR"].ToString(), "0");
                        //if (dtTemp.Rows.Count > 0)
                        //{
                        //    stsWarning.Text = "The Document No was stored in with mixed material last time!!";
                        //    this.btnSave.Enabled = true;
                        //    return;
                        //}
                        ////檢查要入庫的儲位是有相同的料號但不同連板資料(包含單板)
                        //if (objStorageData.CheckExistedSameMaterial(Locat, dt.Rows[i]["MATNR"].ToString(), dt.Rows[i]["MRGID"].ToString()))
                        //{
                        //    stsWarning.Text = dt.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
                        //    this.btnSave.Enabled = true;
                        //    return;
                        //}

                        //如果可以允許同一儲位置放不同版本的料號, Kent 20050130
                        if (this.Duplicate == false)
                        {
                            //檢查要入庫的儲位是否已經有相同料號但不同版本)
                            if (objStorageData.CheckExistedSameMaterial(dtData.Rows[i]["LOCAT"].ToString(), dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
                            {
                                stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with different version!";
                                this.btnSave.Enabled = true;
                                return;
                            }
                        }
                        //MLB同一栈板同料号、同版本、不同Pallet ID入库/并储修改卡控 Bill Yang(杨明朝)
                        if (rdoMLB.Checked && strInputType == "RID" && (strWerks == "CS30 " || strWerks == "CS41" || strWerks == "CS51" || strWerks == "CS50" || strWerks == "CS31" || strWerks == "CS52" || strWerks == "VH10") && strLgort == "TW30")
                        {
                            if (objStorageData.CheckExistedSamePalMblnr(dtData.Rows[i]["LOCAT"].ToString(), dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["NMBLN"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
                            {
                                stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + "-" + dtData.Rows[i]["CHARG"].ToString() + " has existed in the location with different PalletID!";
                                this.btnSave.Enabled = true;
                                return;
                            }
                        }
                    }
                    #endregion

                    #region  MLB输入信息中按储位进行判断 同一栈板同料号、同版本、不同Pallet ID入库/并储修改卡控 Bill Yang(杨明朝)
                    if (rdoMLB.Checked && strInputType == "RID" && (strWerks == "CS30 " || strWerks == "CS41" || strWerks == "CS51" || strWerks == "CS50" || strWerks == "CS31" || strWerks == "CS52" || strWerks == "VH10") && strLgort == "TW30")
                    {
                        var lsLocat = (from d in dtData.AsEnumerable() select d.Field<string>("LOCAT")).Distinct().ToList();
                        for (int i = 0; i < lsLocat.Count; i++)
                        {
                            DataTable dtLocat = dtData.Select("LOCAT='" + lsLocat[i].ToString() + "'").CopyToDataTable();
                            for (int j = 0; j < dtLocat.Rows.Count; j++)
                            {
                                DataRow[] drSelect = dtLocat.Select("LOCAT='" + dtLocat.Rows[j]["LOCAT"] + "' AND MATNR= '" + dtLocat.Rows[j]["MATNR"] + "' AND CHARG= '" + dtLocat.Rows[j]["CHARG"] + "' AND NMBLN<> '" + dtLocat.Rows[j]["NMBLN"] + "'");
                                if (drSelect.Length > 0)
                                {
                                    stsWarning.Text = dtData.Rows[j]["MATNR"].ToString() + "-" + dtData.Rows[j]["CHARG"].ToString() + "在该储位" + lsLocat[i].ToString() + "的刷入信息中存在不同的PalletID!";
                                    this.btnSave.Enabled = true;
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    #region 判断单据状态（在单据回车处已经判断过）
                    string strMblnrs = "";
                    string strMultiMblnr = "";
                    foreach (string mblnr in lsMblnr)
                    {
                        strMblnrs = strMblnrs + "'" + mblnr + "',";
                    }

                    DataTable dtProcessing = new DataTable();
                    strMultiMblnr = "(" + strMblnrs.Substring(0, strMblnrs.Length - 1).Trim() + ")";
                    if (strInputType.Equals("RID"))//刷入ReferenceID
                    {
                        dtProcessing = objStorageInQWMS.QueryMultiStatus(strMultiMblnr, "RID");
                    }
                    else if (strInputType.Equals("PID"))//刷入PalletID
                    {
                        dtProcessing = objStorageInQWMS.QueryMultiStatus(strMultiMblnr, "PID");
                    }
                    if (dtProcessing.Rows.Count > 0)
                    {
                        MessageBox.Show("This RID or PID is processing," + Environment.NewLine + "please try again later!!", "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.btnSave.Enabled = true;
                        return;
                    }
                    #endregion

                    #region 扣账前判断
                    #region 效验去SAP扣账的dtData 与数据库中的条数是否一致
                    try
                    {
                        DataTable dtResouce = new DataTable();                                                            
                        if (strInputType.Equals("RID"))//刷入ReferenceID
                        {
                            dtResouce = objStorageInQWMS.QueryPALITM(strWerks, strLgort, strMultiMblnr,"RID");
                        }
                        else if (strInputType.Equals("PID"))//刷入PalletID
                        {
                            dtResouce = objStorageInQWMS.QueryPALITM(strWerks, strLgort, strMultiMblnr, "PID");
                        }
                        if (dtResouce.Rows.Count != dtData.Rows.Count)
                        {
                            objStorageInQWMS.insertQWMS_LOG(dtResouce.Rows[0]["MBLNR"].ToString(), dtData.Rows.Count.ToString(), dtResouce.Rows.Count.ToString(), "N");
                            MessageBox.Show("去SAP扣账的文档条数" + dtData.Rows.Count + ",与实际不符，请重新Refresh重新作业！");
                            refresh();
                            return;
                        }
                        objStorageInQWMS.insertQWMS_LOG(dtResouce.Rows[0]["MBLNR"].ToString(), dtData.Rows.Count.ToString(), dtResouce.Rows.Count.ToString(), "Y");
                    }
                    catch (Exception ex)
                    {
                        //stsWarning.Text = ex.Message;
                    }
                    #endregion
                    foreach (string strMblnr in lsMblnr)
                    {
                        objLogData.AddQWMSLOG(strMblnr, "PAL", "Save", "PAL开始扣账", "N", Usrnm);
                        DataTable dt = new DataTable();
                        SapData objSapData = new SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                        #region 防呆機制
                        DataRow[] drSelect = dtData.Select("MBLNR='" + strMblnr + "'");
                        if (drSelect.Length == 0)
                        {
                            stsWarning.Text = "The data " + strMblnr + " can't be empty!!";
                            this.btnSave.Enabled = true;
                            return;
                        }
                        else
                        {
                            dt = dtData.Select("MBLNR='" + strMblnr + "'").CopyToDataTable();
                        }
                        #endregion

                        #region 更新PAL_DETAIL的储位信息
                        objStorageInQWMS.UpdatePalDetail(dt);
                        #endregion

                        #region 改變單據Processing狀態為Y,並寫入開始時間

                        if (strInputType.Equals("RID"))//刷入ReferenceID
                            objStorageInQWMS.UpdateStatus(strMblnr, "RID", "Y");
                        else if (strInputType.Equals("PID"))//刷入PalletID
                            objStorageInQWMS.UpdateStatus(strMblnr, "PID", "Y");

                        #endregion

                        #region --備註--

                        //扣帳流程未完整結束,可能情況有以下3種
                        //1.SAP成功,QWMS失敗
                        //2.SAP失敗,QWMS失敗(此狀況可忽略)
                        //3.根本沒做過(此狀況可忽略)
                        //flag_id = false-->扣帳已完成
                        //flag_id_SAPONLY = false-->SAP已扣,QWMS未扣(是否仅入QWMS)

                        #endregion

                        #region 檢查目前ReferenceID or PalletID是否已經扣帳過(若為true則為QWMS端扣帳未完成)

                        bool flag_id = true;
                        bool flag_id_SAPONLY = true;

                        //SapData objSapData = new SapData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                        if (strInputType.Equals("RID"))//刷入ReferenceID
                        {
                            DataTable dtRefidData = objSapData.QueryQMSLineInDataByRefid("", strMblnr, dtpIndat.Value.ToString("yyyyMMdd"));//MENGE>OTQTY

                            if (dtRefidData.Rows.Count == 0)
                                flag_id = false;
                        }
                        else if (strInputType.Equals("PID"))//刷入PalletID
                        {
                            DataTable dtPalData = objSapData.QueryQMSLineInDataByPalid("", strMblnr, dtpIndat.Value.ToString("yyyyMMdd"));

                            if (dtPalData.Rows.Count == 0)
                                flag_id = false;
                        }
                        #endregion

                        #region 檢查SAP已扣QWMS未扣的情況
                        if (flag_id)//檢查SAP已扣QWMS未扣的情況
                        {
                            #region 檢查目前ReferenceID or PalletID是否已經扣帳過(SAP ONLY)

                            OMBLN = "";

                            if (dt.Rows[0]["OTLGT"].ToString() == "")//101
                            {
                                if (strInputType.Equals("RID"))//ReferenceID有值
                                {
                                    OMBLN = objLogData.QuerySAPInDataByPalid101(strMblnr, "RID");//查询扣账编号
                                }
                                else if (strInputType.Equals("PID"))//PalletID有值
                                {
                                    OMBLN = objLogData.QuerySAPInDataByPalid101(strMblnr, "PID");
                                }
                                if (OMBLN == null || OMBLN.Trim().Equals(""))
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        dtData101.Rows.Add(dr.ItemArray);
                                    }
                                }
                            }
                            else//311
                            {
                                if (strInputType.Equals("RID"))//ReferenceID有值
                                {
                                    OMBLN = objLogData.QuerySAPInDataByPalid311(strMblnr, "RID");
                                }
                                else if (strInputType.Equals("PID"))//PalletID有值
                                {
                                    OMBLN = objLogData.QuerySAPInDataByPalid311(strMblnr, "PID");
                                }
                                if (OMBLN == null || OMBLN.Trim().Equals(""))
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        dtData311.Rows.Add(dr.ItemArray);
                                    }
                                }
                            }
                            if (OMBLN != null && !OMBLN.Trim().Equals(""))
                            {
                                flag_id_SAPONLY = false;//SAP已扣，QWMS未扣

                                #region 模拟SAP互传数据
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    dt.Rows[i]["OMBLNR"] = OMBLN;
                                    dt.Rows[i]["SERNO"] = i + 1;
                                    dt.Rows[i]["RMAK1"] = dt.Rows[i]["NMBLN"].ToString();

                                    //若NMBLN欄位不為空，代表user刷入的是Reference id，因此將原先的Pallet ID(MBLNR)取代回原欄位
                                    if (dt.Rows[i]["NMBLN"].ToString() != "")
                                    {
                                        dt.Rows[i]["MBLNR"] = dt.Rows[i]["NMBLN"].ToString();
                                    }
                                }
                                #endregion

                                foreach (DataRow dr in dt.Rows)
                                {
                                    dtDataQwms.Rows.Add(dr.ItemArray);
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion

                    #region 扣账

                    #region 仅入QWMS
                    DialogResult result = new DialogResult();
                    if (dtDataQwms.Rows.Count > 0)//若為SAP扣過帳而QWMS未扣的情況
                    {
                        result = MessageBox.Show("SAP already finish this RID or PID,but QWMS not!!" + "\n" + "Are you sure to continue?", "QWMS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result != DialogResult.Yes)
                        {
                            #region 改變單據Processing狀態為N
                            changeProcessing(dtDataQwms);
                            #endregion
                            return;
                        }
                        else
                        {
                            #region 仅入QWMS
                            StorageToQwms(dtDataQwms, "QWMS");
                            #endregion
                        }
                    }
                    #endregion

                    #region 101扣账
                    if (dtData101.Rows.Count > 0)
                    {
                        this.stsWarning.Text = "";

                        #region SAP扣帳(101工單入庫)

                        ArrayList aryMblnr = new ArrayList();
                        //新版RFC寫法，透過TXT文檔的存取扣SAP帳，並取得SAP回傳的扣帳編號  Smose Liao 20120723
                        stsWarning.Text = "系統正在扣SAP帳中，請勿關閉視窗!!";
                        AllowToClose = false;//強制User無法關閉視窗
                        DataTable dt101 = new DataTable();
                        DataTable dt101qwms = dt101.Clone();
                        dt101 = StorageToSAP_101(dtData101);//扣帳

                        if (dt101.Select("OMBLNR<>''").Length > 0)
                        {
                            dt101qwms = dt101.Select("OMBLNR<>''").CopyToDataTable();
                            //若NMBLN欄位不為空，代表user刷入的是Reference id，因此將原先的Pallet ID(MBLNR)取代回原欄位
                            var ls = (from d in dt101qwms.AsEnumerable() select d.Field<string>("MBLNR")).Distinct().ToList();
                            for (int i = 0; i < ls.Count; i++)
                            {
                                int t = 1;
                                for (int j = 0; j < dt101qwms.Rows.Count; j++)
                                {
                                    if (dt101qwms.Rows[j]["MBLNR"].ToString().Trim() == ls[i].ToString())
                                    {
                                        dt101qwms.Rows[j]["SERNO"] = t;
                                        //若NMBLN欄位不為空，代表user刷入的是Reference id，因此將原先的Pallet ID(MBLNR)取代回原欄位
                                        if (dt101qwms.Rows[j]["NMBLN"].ToString() != "")
                                        {
                                            dt101qwms.Rows[j]["MBLNR"] = dt101qwms.Rows[j]["NMBLN"].ToString();
                                        }
                                        t = t + 1;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 入qwms
                        if (dt101qwms.Rows.Count > 0)
                        {
                            StorageToQwms(dt101qwms, "SAP");
                        }
                        else
                        {
                            stsWarning.Text = "无入库数据！！";
                        }
                        #endregion

                        AllowToClose = true;
                    }
                    #endregion

                    #region 311扣账
                    if (dtData311.Rows.Count > 0)
                    {
                        #region SAP扣帳(311轉倉入庫)

                        this.stsWarning.Text = "";
                        stsWarning.Text = "系統正在扣SAP帳中，請勿關閉視窗!!";
                        AllowToClose = false;//強制User無法關閉視窗
                        ArrayList aryMblnr = new ArrayList();
                        DataTable dt311 = new DataTable();
                        DataTable dt311qwms = dt311.Clone();
                        dt311 = StorageToSAP_311(dtData311);//扣帳

                        if (dt311.Select("OMBLNR<>''").Length > 0)
                        {
                            dt311qwms = dt311.Select("OMBLNR<>''").CopyToDataTable();
                            //若NMBLN欄位不為空，代表user刷入的是Reference id，因此將原先的Pallet ID(MBLNR)取代回原欄位
                            var ls = (from d in dt311qwms.AsEnumerable() select d.Field<string>("MBLNR")).Distinct().ToList();
                            for (int i = 0; i < ls.Count; i++)
                            {
                                int t = 1;
                                for (int j = 0; j < dt311qwms.Rows.Count; j++)
                                {
                                    if (dt311qwms.Rows[j]["MBLNR"].ToString().Trim() == ls[i].ToString())
                                    {
                                        dt311qwms.Rows[j]["SERNO"] = t;
                                        //若NMBLN欄位不為空，代表user刷入的是Reference id，因此將原先的Pallet ID(MBLNR)取代回原欄位
                                        if (dt311qwms.Rows[j]["NMBLN"].ToString() != "")
                                        {
                                            dt311qwms.Rows[j]["MBLNR"] = dt311qwms.Rows[j]["NMBLN"].ToString();
                                        }
                                        t = t + 1;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region 入qwms
                        if (dt311qwms.Rows.Count > 0)
                        {
                            StorageToQwms(dt311qwms, "SAP");
                        }
                        else
                        {
                            stsWarning.Text = "无入库数据！！";
                        }
                        #endregion

                        AllowToClose = true;
                    }
                    #endregion

                    #endregion
                }
            }
            catch (Exception ex)
            {
                AllowToClose = true;
                stsWarning.Text = ex.Message;
                if (!ex.Message.Contains("Object reference not set to an instance of an object."))
                {
                    changeProcessing(dtData);
                    this.btnSave.Enabled = true;
                }
                MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion

        #region SAP扣账
        private DataTable StorageToSAP_101(DataTable dtStorage)
        {
            StringBuilder sbmblnr = new StringBuilder();
            DataTable dtOutTable = new DataTable();
            dtOutTable.TableName = "ITABIN";
            DataColumnCollection columns = dtOutTable.Columns;
            columns.Add("ZPALLETID", typeof(System.String));   //棧板號
            columns.Add("PLANT", typeof(System.String));       //廠區
            columns.Add("COSTCENTER", typeof(System.String));  //Cost Center
            columns.Add("WO", typeof(System.String));          //工單號
            columns.Add("QTY", typeof(System.Decimal));　　　  //數量
            columns.Add("MATERIAL", typeof(System.String));    //料號
            columns.Add("REGION", typeof(System.String));      //州別，空白
            columns.Add("LOCA", typeof(System.String));        //倉別
            columns.Add("REF1", typeof(System.String));        //版本
            columns.Add("REF2", typeof(System.String));        //操作QWMS人員的工號

            for (int i = 0; i < dtStorage.Rows.Count; i++)
            {
                DataRow dr = dtOutTable.NewRow();
                dr[0] = dtStorage.Rows[i]["MBLNR"].ToString().Trim();
                dr[1] = dtStorage.Rows[i]["WERKS"].ToString().Trim();
                dr[2] = dtStorage.Rows[i]["KOSTL"].ToString().Trim();
                dr[3] = dtStorage.Rows[i]["ZEILE"].ToString().Trim();
                dr[4] = dtStorage.Rows[i]["MENGE"].ToString().Trim();
                dr[5] = dtStorage.Rows[i]["MATNR"].ToString().Trim();
                dr[6] = "";
                dr[7] = dtStorage.Rows[i]["LGORT"].ToString().Trim();
                dr[8] = dtStorage.Rows[i]["CHARG"].ToString().Trim();
                dr[9] = "QWMS-" + UserData.UserId.ToString();//記錄操作QWMS人員的工號

                dtOutTable.Rows.Add(dr);
            }

            DataTable dtResponse = new DataTable();
            DataTable dt = new DataTable();
            LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, strProgid);

            try
            {
                int id = objLogData.GenerID_101();
                string type2 = "";

                //判斷目前是以ReferenceID or PalletID做入庫動作
                if (dtStorage.Rows[0]["NMBLN"] != null)
                {
                    if (!dtStorage.Rows[0]["NMBLN"].ToString().Trim().Equals(""))
                        type2 = "RID";
                    else
                        type2 = "PID";
                }
                else
                    type2 = "PID";

                //產生給SAP扣帳的檔案 
                objLogData.multiPALLog101(dtOutTable, dtResponse, id, "send", type2);
                //L101为抛给SAP的单据List
                var l101 = (from d in dtOutTable.AsEnumerable() select d.Field<string>("ZPALLETID")).Distinct().ToList();
                for (int i = 0; i < l101.Count; i++)
                {
                    objLogData.AddQWMSLOG(l101[i].ToString().Trim(), "PAL", "Save", "PAL开始产生扣账文档", "N", Usrnm);
                }
                dtResponse = SendToSAP_101(dtOutTable).Tables[0];
                objLogData.multiPALLog101(dtOutTable, dtResponse, id, "return", type2);
                #region 处理SAP回传的扣账数据
                StringBuilder sbMessage = new StringBuilder();
                for (int i = 0; i < l101.Count; i++)
                {
                    DataTable dtReturn = dtResponse.Select("ZPALLETID='" + l101[i].ToString() + "'").CopyToDataTable();
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["OMBLN1"].ToString()) || !string.IsNullOrEmpty(dtReturn.Rows[0]["OMBLN2"].ToString()))  //SAP已扣账
                    {
                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["OMBLN1"].ToString()))
                        {
                            DataRow[] drs = dtStorage.Select("MBLNR='" + l101[i].ToString() + "'");
                            foreach (DataRow dr in drs)
                            {
                                dr["OMBLNR"] = dtReturn.Rows[0]["OMBLN1"].ToString();
                                dr["RMAK1"] = l101[i].ToString();
                            }
                            sbMessage.AppendFormat("MBLNR:{0} 扣账成功，扣账编号为{1}\n", l101[i].ToString(), dtReturn.Rows[0]["OMBLN1"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["OMBLN2"].ToString()))
                        {
                            DataRow[] drs = dtStorage.Select("MBLNR='" + l101[i].ToString() + "'");
                            foreach (DataRow dr in drs)
                            {
                                dr["OMBLNR"] = dtReturn.Rows[0]["OMBLN2"].ToString();
                                dr["RMAK1"] = l101[i].ToString();
                            }
                            sbMessage.AppendFormat("MBLNR:{0} 扣账成功，扣账编号为{1}\n", l101[i].ToString(), dtReturn.Rows[0]["OMBLN2"].ToString());
                        }
                    }
                    else
                    {
                        DataRow[] drserror = dtReturn.Select("ERRMSG<>''");
                        sbMessage.AppendFormat("MBLNR:{0} 扣账失败，失败原因为{1}\n", l101[i].ToString(), drserror[0]["ERRMSG"].ToString());
                    }
                }
                MessageBox.Show(sbMessage.ToString());
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtStorage;
        }
        public DataSet SendToSAP_101(DataTable dtSap)
        {
            try
            {
                DataSet dsSap = new DataSet();
                dsSap.Tables.Add(dtSap);
                DataSet dsResult = new DataSet();
                //PP_Test.PP_Service objPP = new PP_Test.PP_Service();
                PP.PP_Service objPP = new PP.PP_Service();
                dsResult = objPP.ZRFC_PP_WO_AUTO_GR_M_WithPlant(strWerks, dsSap);
                #region 从ERRMSG中抓取扣账编号
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    if (dsResult.Tables[0].Rows[i]["ERRMSG"].ToString() != "")
                    {
                        string strError = string.Empty;
                        //Error Message有回傳值，跳出迴圈
                        strError = dsResult.Tables[0].Rows[i]["ERRMSG"].ToString();
                        Regex pattern = new Regex(@"50\d{8}");//比對的pattern，SAP回傳的扣帳編號為49開頭
                        Match m = pattern.Match(strError);

                        #region 檢查SAP回傳的扣帳編號是否全為數值型態，避免誤判抓到料號的情況下，誤入了QWMS庫存
                        //【例】SAP回傳的錯誤訊息：3VPJ7AB0000F3AFD Material Batch is not mapping with WO
                        if (m.Success)
                        {
                            int MathIndex = m.Index;//取得SAP回傳的扣帳編號之所在的位置
                            long outNumber = 0;
                            bool bolTryParse = long.TryParse(strError.Substring(MathIndex, 10), out outNumber);

                            if (bolTryParse && (dsResult.Tables[0].Rows[i]["OMBLN1"].ToString() == "" || dsResult.Tables[0].Rows[i]["OMBLN2"].ToString() == ""))
                            {
                                dsResult.Tables[0].Rows[i]["OMBLN1"] = outNumber.ToString();
                            }
                        }
                        #endregion
                    }
                }
                #endregion
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataSet SendToSAP_311(DataTable dtSap)
        {
            try
            {
                DataSet dsSap = new DataSet();
                dsSap.Tables.Add(dtSap);
                DataSet dsResult = new DataSet();
                //PP_Test.PP_Service objPP = new PP_Test.PP_Service();
                PP.PP_Service objPP = new PP.PP_Service();
                dsResult = objPP.ZRFC_PP_311_AUTO_RETURN_M_WithPlant(strWerks, dsSap);
                #region 从ERRMSG中抓取扣账编号
                for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                {
                    if (dsResult.Tables[0].Rows[i]["ERRMSG"].ToString() != "")
                    {
                        string strError = string.Empty;
                        //Error Message有回傳值，跳出迴圈
                        strError = dsResult.Tables[0].Rows[i]["ERRMSG"].ToString();
                        Regex pattern = new Regex(@"49\d{8}");//比對的pattern，SAP回傳的扣帳編號為49開頭
                        Match m = pattern.Match(strError);

                        #region 檢查SAP回傳的扣帳編號是否全為數值型態，避免誤判抓到料號的情況下，誤入了QWMS庫存
                        //【例】SAP回傳的錯誤訊息：3VPJ7AB0000F3AFD Material Batch is not mapping with WO
                        if (m.Success)
                        {
                            int MathIndex = m.Index;//取得SAP回傳的扣帳編號之所在的位置
                            long outNumber = 0;
                            bool bolTryParse = long.TryParse(strError.Substring(MathIndex, 10), out outNumber);

                            if (bolTryParse && dsResult.Tables[0].Rows[i]["OMBLN"].ToString() == "")
                            {
                                dsResult.Tables[0].Rows[i]["OMBLN"] = outNumber.ToString();
                            }
                        }
                        #endregion
                    }
                }
                #endregion
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable StorageToSAP_311(DataTable dtStorage)
        {
            string strStatus = "";
            strStatus = "T";//所有的311轉倉資料，Status狀態都需傳'T'  Smose Liao 20130523
            DataTable dt = new DataTable();

            DataTable dtOutTable = new DataTable();
            dtOutTable.TableName = "SAP";
            DataColumnCollection columns = dtOutTable.Columns;
            columns.Add("REFNO", typeof(System.String));         //Reference ID
            columns.Add("PLANT", typeof(System.String));         //廠區
            columns.Add("MATNR", typeof(System.String));         //料號
            columns.Add("KOSTL", typeof(System.String));　　　　 //Cost Center
            columns.Add("LGORTF", typeof(System.String));        //發料倉別
            columns.Add("LGORTT", typeof(System.String));        //入庫倉別
            columns.Add("RETQTY", typeof(System.Decimal));       //數量
            columns.Add("CHARG", typeof(System.String));         //版本
            columns.Add("STATUS", typeof(System.String));        //狀態

            for (int i = 0; i < dtStorage.Rows.Count; i++)
            {
                DataRow dr = dtOutTable.NewRow();
                dr[0] = dtStorage.Rows[i]["REFID"].ToString().Trim();
                dr[1] = dtStorage.Rows[i]["WERKS"].ToString().Trim();
                dr[2] = dtStorage.Rows[i]["MATNR"].ToString().Trim();
                dr[3] = dtStorage.Rows[i]["KOSTL"].ToString().Trim();
                dr[4] = dtStorage.Rows[i]["OTLGT"].ToString().Trim();
                dr[5] = dtStorage.Rows[i]["LGORT"].ToString().Trim();
                dr[6] = dtStorage.Rows[i]["MENGE"].ToString().Trim();
                if (dtStorage.Rows[i]["CHARG"].ToString() != "")//版本
                {
                    dr[7] = dtStorage.Rows[i]["CHARG"].ToString().Trim();
                }
                dr[8] = strStatus;//狀態

                dtOutTable.Rows.Add(dr);
            }

            DataTable dtResponse = new DataTable();
            LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, strProgid);
            StorageIn objStorageIn = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            try
            {
                #region 記錄UpdateSapInventory開始的時間  Lora
                objStorageIn.AddErrorLog(dtStorage, "Add");
                #endregion

                int id = objLogData.GenerID_311();
                string type2 = "";

                //判斷目前是以ReferenceID or PalletID做入庫動作
                if (dtStorage.Rows[0]["NMBLN"] != null)
                {
                    if (!dtStorage.Rows[0]["NMBLN"].ToString().Trim().Equals(""))
                        type2 = "RID";
                    else
                        type2 = "PID";
                }
                else
                    type2 = "PID";

                //產生給SAP扣帳的檔案
                objLogData.multiPALLog311(dtOutTable, dtResponse, id, "send", type2);
                dtResponse = SendToSAP_311(dtOutTable).Tables[0];
                objLogData.multiPALLog311(dtOutTable, dtResponse, id, "return", type2);//註記部分
                //L311为抛给SAP的单据List
                var l311 = (from d in dtOutTable.AsEnumerable() select d.Field<string>("REFNO")).Distinct().ToList();
                #region 处理SAP回传的扣账数据
                StringBuilder sbMessage = new StringBuilder();
                for (int i = 0; i < l311.Count; i++)
                {
                    DataTable dtReturn = dtResponse.Select("REFNO='" + l311[i].ToString() + "'").CopyToDataTable();
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["OMBLN"].ToString()))  //SAP已扣账
                    {
                        DataRow[] drs = dtStorage.Select("MBLNR='" + l311[i].ToString() + "'");
                        foreach (DataRow dr in drs)
                        {
                            dr["OMBLNR"] = dtReturn.Rows[0]["OMBLN"].ToString();
                            dr["RMAK1"] = l311[i].ToString();
                        }
                        sbMessage.AppendFormat("REFID:{0} 扣账成功，扣账编号为{1}\n", l311[i].ToString(), dtReturn.Rows[0]["OMBLN"].ToString());
                    }
                    else
                    {
                        DataRow[] drserror = dtReturn.Select("ERRMSG<>''");
                        sbMessage.AppendFormat("REFID:{0} 扣账失败，失败原因为{1}\n", l311[i].ToString(), drserror[0]["ERRMSG"].ToString());
                    }
                }
                MessageBox.Show(sbMessage.ToString());
                #endregion

                #region 記錄UpdateSapInventory結束的時間  Smose Liao 20100412
                objStorageIn.AddErrorLog(dtStorage, "Update");
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return dtStorage;
        }

        #endregion

        #region QWMS扣账
        private void StorageToQwms(DataTable dtStorageQwms, string flag)
        {
            StorageIn objStorageInQWMS = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
            try
            {
                if (objStorageInQWMS.AddOnLineInData_PAL("", dtStorageQwms))
                {
                    if (flag == "SAP")
                    {
                        stsWarning.Text = "SAP and QWMS Posting OK!!";
                    }
                    else if (flag == "QWMS")
                    {
                        stsWarning.Text = "QWMS Posting OK!!";
                    }
                    AllowToClose = true;//扣帳成功，恢復可以關閉Form視窗
                    this.btnSave.Enabled = false;

                    #region 增加和ASRS接口

                    AsrsInterface objInterface = new AsrsInterface(UserData);

                    if (objInterface.CheckLGORT(Werks, Lgort))//判断是否为ASRS仓别
                    {

                        DataTable dtASRS = new DataTable();
                        dtASRS.TableName = "QWMS";

                        dtASRS.Columns.Add("TRN_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("SEQ_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("TRN_TYPE", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("LOC", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("ITEM_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("STK", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("VER", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("VENDOR", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("QTY", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PUR_TYPE", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PO_NO", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PLANT", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("PRIORITY", typeof(string)).DefaultValue = string.Empty;
                        dtASRS.Columns.Add("STORAGE_TYPE", typeof(string)).DefaultValue = string.Empty;

                        int j = 1;
                        string strTRN_NO = objInterface.CreateAsrsNo();

                        foreach (DataRow dr in dtStorageQwms.Rows)
                        {
                            DataRow drASRS = dtASRS.NewRow();

                            drASRS["TRN_NO"] = strTRN_NO;
                            drASRS["SEQ_NO"] = objInterface.Createseq_no(j);
                            drASRS["TRN_TYPE"] = "G+";
                            drASRS["LOC"] = dr["LOCAT"];
                            drASRS["ITEM_NO"] = dr["MATNR"];
                            drASRS["STK"] = dr["INSMK"];
                            drASRS["VER"] = dr["CHARG"];
                            drASRS["VENDOR"] = dr["LIFNR"];
                            drASRS["QTY"] = dr["ALQTY"];
                            drASRS["PUR_TYPE"] = string.Empty;
                            drASRS["PO_NO"] = dr["EBELN"];
                            drASRS["PLANT"] = dr["WERKS"];
                            drASRS["PRIORITY"] = string.Empty;
                            drASRS["STORAGE_TYPE"] = dr["LGORT"];

                            dtASRS.Rows.Add(drASRS);
                            j++;
                        }
                        //bool bolresult = objInterface.PostStorageInData(dtASRS) == "SUCCESS" ? true : false;

                        if (objInterface.PostStorageInData(dtASRS) == "SUCCESS" ? true : false)
                        {
                            stsWarning.Text = "Add OK!!,数据已同步到ASRS";
                        }
                    }
                    #endregion
                }
                else
                {
                    if (flag == "SAP")
                    {
                        stsWarning.Text = "SAP Posting OK but QWMS Save Fail!!請立即通知MIS!! " + objStorageInQWMS.ERRMSG;
                    }
                    else if (flag == "QWMS")
                    {
                        stsWarning.Text = "QWMS Save Fail!!請立即通知MIS!! " + objStorageInQWMS.ERRMSG;
                    }
                    AllowToClose = true;
                    //return;
                }

                #region 改變單據Processing狀態為N

                changeProcessing(dtStorageQwms);

                #endregion

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            refresh();
        }
        #endregion

        #region btnQueryRefid
        private void btnQueryRefid_Click(object sender, EventArgs e)
        {
            try
            {
                PlantData objPlantData = new PlantData(UserData);
                DataTable dtRefID = new DataTable();
                string strCrdat = dtpIndat.Value.ToString("yyyy-MM-dd");
                strWerks = cmbWerks.SelectedItem.ToString();
                strLgort = cmbLgort.SelectedItem.ToString();

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                dtRefID = objPlantData.GetAllRefIDData(this.strMandt, this.Comcd, this.strWerks, this.strLgort, strCrdat);
                StorageIn_SMT_Query_RefID objStorageIn_SMT_Query_RefID = new StorageIn_SMT_Query_RefID(dtRefID, dtData, "Pallet", UserData);
                objStorageIn_SMT_Query_RefID.ShowDialog();
                txtRefid.Text = objStorageIn_SMT_Query_RefID.RefID;
                this.txtRefid.Focus();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

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

        #region 界面
        private void StorageIn_OnLineIn_Resize(object sender, System.EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;

        }
        #endregion

        private void refresh()
        {
            #region 刷新操作
            stsWarning.Text = "";
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.rdoLCM.Checked = false;
            this.rdoMLB.Checked = false;
            this.gbFunction.Enabled = true;
            this.txtLocat.Text = "";
            this.txtPalid.Text = "";
            this.txtRefid.Text = "";
            this.txtPalid.Enabled = false;
            this.txtRefid.Enabled = false;
            this.txtLocat.Enabled = false;
            this.gbHeader.Enabled = false;
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.dtpIndat.Enabled = true;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.lblData.Text = "0 records";
            this.btnSave.Enabled = false;
            this.rdoLCM.Enabled = false;
            this.rdoMLB.Enabled = false;
            lsMblnr.Clear();
            strWerks = "";
            strLgort = "";
            strInsmk = "";
            strInputType = "";
            strType = "";
            strMblnrMatnr = "";
            #endregion
        }

    }
}
