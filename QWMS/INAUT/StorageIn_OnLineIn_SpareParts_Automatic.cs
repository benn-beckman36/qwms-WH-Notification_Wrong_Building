using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class StorageIn_OnLineIn_SpareParts_Automatic : Form
    {
        #region 變數宣告

        UserInfo UserData = new UserInfo();
		private DataTable dtData = new DataTable();	

		private string strMandt = "";
        private string strComcd = "";
		private string strUsrnm = "";
		private string strWerks = "";
		private string strLgort = "";
		private string strProgid = "";
		private string strLocat = "";
		private string strMblnr = "";
		private string strMatnr = "";
		private string strType = "";
		private string strInsmk = "";
		private string strSttyp = "";
		private string strLotyp = "";
		private int intFormIndex = 0;
        //private int CurRowIndex;
		private bool bolDuplicate = false;
        private ArrayList alLocat = new ArrayList();
        private ArrayList aryMblnr = new ArrayList();
        private ArrayList aryMatnr = new ArrayList();

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

        public ArrayList Mblnr
        {
            get
            {
                return aryMblnr;
            }
            set
            {
                aryMblnr = value;
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
        #endregion

        public StorageIn_OnLineIn_SpareParts_Automatic()
        {
            InitializeComponent();
        }

        public StorageIn_OnLineIn_SpareParts_Automatic(ref UserInfo varUserData, string strProgid)
        {
            UserData = varUserData;
            InitializeComponent();
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region ShowStatusData(顯示狀態列)
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region ShowDdlWerks(廠區下拉選單)
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
        #endregion

        #region cmbWerks_SelectedIndexChanged(選取廠區)
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region ShowDdlLgort(倉別下拉選單)
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
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

        #region ShowDataGrid(顯示入庫資料)
        public void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                //筆數
                DataGridViewTextBoxColumn dgvcCount = new DataGridViewTextBoxColumn();
                dgvcCount.DataPropertyName = "Item";
                dgvcCount.HeaderText = "Item";
                dgvcCount.ReadOnly = true;
                dgvcCount.Width = 40;
                dgvData.Columns.Add(dgvcCount);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 80;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 80;
                dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 110;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 100;
                dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.ReadOnly = true;
                dgvcZeile.Width = 100;
                dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                dgvcEbeln.Width = 100;
                dgvData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvcKdmat.Width = 100;
                dgvData.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvcRmak1.Width = 100;
                dgvData.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 80;
                dgvData.Columns.Add(dgvcIndat);

                if (Data.Columns.IndexOf("Item") == -1)
                {
                    Data.Columns.Add("Item");
                }

                for (int i = 0; i < Data.Rows.Count; i++)
                {
                    Data.Rows[i]["Item"] = i + 1;
                }

                dgvData.DataSource = Data;
                lblCount.Text = Data.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
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
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
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
        private void txtLocat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    btnConfirm_Click(null, null);
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }
        #endregion

        #region rdoNew_CheckedChanged(入庫方式-新板入庫(New Pallet))
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
        }
        #endregion

        #region rdoAdd_CheckedChanged(入庫方式-加料入庫(Add In))
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            gbFunction.Enabled = false;
            gbHeader.Enabled = true;
            btnConfirm.Enabled = true;
        }
        #endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                strLocat = txtLocat.Text.Trim();

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
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                //取得Sttyp及Lotyp

                dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
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

                //變動儲位才需要檢查儲位有沒有輸入
                if (Lotyp == "DYNAMIC LOCATION")
                {
                    if (strType == "ADD")
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            stsWarning.Text = "Please input a location first!!";
                            this.txtLocat.Focus();
                            return;
                        }
                    }
                    if (strType == "NEW")
                    {
                        if (txtLocat.Text.Trim() == "")
                        {
                            txtLocat.Text = objPlantData.GetEmptyLocation(Werks, Lgort);
                        }
                        else
                        {
                            if (objPlantData.CheckStorageData(Werks, Lgort, Locat))
                            {
                                stsWarning.Text = "The location you input is not a empty location!!";
                                this.txtLocat.Focus();
                                return;
                            }
                        }
                    }

                    if (txtLocat.Text.Trim() == "")
                    {
                        stsWarning.Text = "Please input a location first!!";
                        this.txtLocat.Focus();
                        return;
                    }


                    if (!objPlantData.CheckExistedStorageData(Werks, Lgort, Locat))
                    {
                        stsWarning.Text = "The location doesn't exist!!";
                        this.txtLocat.Focus();
                        return;
                    }
                    //找出庫別

                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                    if (this.rdoAdd.Checked)
                    {

                        dtTemp = objStorageData.QueryLocatInsmk(strLocat);
                        if (dtTemp.Rows.Count > 1)
                        {
                            stsWarning.Text = "The location has different stock and you can't add any new Part No!!";
                            return;
                        }
                        strInsmk = dtTemp.Rows[0]["INSMK"].ToString();
                    }
                }
                else
                {
                    //固定儲位時Location不能輸入
                    if (this.txtLocat.Text.Trim() != "")
                    {
                        stsWarning.Text = "You can't input location because " + strLgort + " is a fixed-Location storage!!";
                        this.txtLocat.Focus();
                        return;
                    }
                }
                gbHeader.Enabled = false;
                btnAdd.Enabled = true;
                btnAdd_Click(null, null);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnSave_Click
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                SetbtnSaveProcess();
                string strTempMblnrMatnr = "";
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "The data can't be empty!!";
                    SetbtnSaveException();
                    return;
                }
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (strTempMblnrMatnr.IndexOf(dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString()) == -1)
                    {
                        strTempMblnrMatnr += dtData.Rows[i]["MBLNR"].ToString() + dtData.Rows[i]["MATNR"].ToString() + ";";
                    }
                    else
                    {
                        stsWarning.Text = "The Document No and Part No you input is duplicate!!";
                        SetbtnSaveException();
                        return;
                    }
                    //檢查要入庫的單號之前是否有使用連板入庫的方式入庫

                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                    dtTemp = objStorageData.QueryNotCombineInData(Locat, dtData.Rows[i]["MBLNR"].ToString(), "0");
                    if (dtTemp.Rows.Count > 0)
                    {
                        stsWarning.Text = "The Document No was stored in with mixed material last time!!";
                        SetbtnSaveException();
                        return;
                    }
                    //檢查要入庫的儲位是有相同的料號但不同連板資料(包含單板)
                    if (objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["MRGID"].ToString()))
                    {
                        stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " has existed in the location with mixed material!";
                        SetbtnSaveException();
                        return;
                    }

                    //檢查要入庫的儲位是否已經有相同料號但不同版本)
                    if (objStorageData.CheckExistedSameMaterial(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString()))
                    {
                        stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的版本!!";
                        SetbtnSaveException();
                        return;
                    }
                    //檢查要入庫的儲位是否已經有相同料號但不同的PO No.)  Smose Liao 20100615
                    if (objStorageData.CheckExistedSameEbeln(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString(), dtData.Rows[i]["EBELN"].ToString()))
                    {
                        stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的PO No.!!";
                        SetbtnSaveException();
                        return;
                    }

                    //檢查要入庫的儲位是否已經有相同料號但不同的客人料號)  Smose Liao 20100615
                    if (objStorageData.CheckExistedSameKdmat(Locat, dtData.Rows[i]["MATNR"].ToString(), dtData.Rows[i]["INSMK"].ToString(), dtData.Rows[i]["CHARG"].ToString(), dtData.Rows[i]["KDMAT"].ToString()))
                    {
                        stsWarning.Text = dtData.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位但有不同的客人料號!!";
                        SetbtnSaveException();
                        return;
                    }
                }
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

                if (objStorageIn.AddSparePartsOnLineInData(Locat, dtData))
                {
                    stsWarning.Text = "Add OK!!";

                    this.btnAdd.Enabled = false;
                    this.btnSave.Enabled = false;

                    #region 增加和ASRS接口

                    QCI.QWMS.AsrsInterface objInterface = new AsrsInterface(UserData);

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

                        foreach (DataRow dr in dtData.Rows)
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
                    return;
                }
                else
                {
                    stsWarning.Text = "Add fail!! " + objStorageIn.ERRMSG;
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

        #region btnAdd_Click
        public void btnAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (Lotyp == "DYNAMIC LOCATION")
                {
                    //如果是選擇New, 則第一次按下Add時不限制庫別, 但只要選擇了一個物料, 之後按下Add就依照第一個物料的庫別為主
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
                }
                else
                {
                    strInsmk = "";
                }
                if (CheckIsOpen("StorageIn_OnLineIn_SpareParts_Add"))
                {
                    this.MdiParent.MdiChildren[intFormIndex].Close();
                }
                StorageIn_OnLineIn_SpareParts_Add objStorageIn_OnLineIn_SpareParts_Add = new StorageIn_OnLineIn_SpareParts_Add(UserData, Progid, Werks, Lgort, Locat, Sttyp, Lotyp, dtpIndat.Value.ToString("yyyyMMdd"), "", "", dtData, "SPARE_PARTS", Insmk, "", Duplicate);
                objStorageIn_OnLineIn_SpareParts_Add.MdiParent = this.ParentForm;
                objStorageIn_OnLineIn_SpareParts_Add.Show();
                dtData = objStorageIn_OnLineIn_SpareParts_Add.SapData;
                ShowDataGrid();
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;
            this.gbFunction.Enabled = true;
            this.txtLocat.Text = "";
            this.gbHeader.Enabled = false;
            this.dtData.Rows.Clear();
            this.dgvData.DataSource = null;
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = false;
            this.lblCount.Text = "0 records";
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

        #region CheckIsOpen(確認視窗是否已經打開)
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

        #region SetbtnSaveProcess(按鈕設定-儲存中)
        private void SetbtnSaveProcess()
        {
            this.btnAdd.Enabled = false;
            this.btnSave.Enabled = false;

        }
        #endregion

        #region SetbtnSaveException(按鈕設定-儲存失敗)
        private void SetbtnSaveException()
        {
            this.btnAdd.Enabled = true;
            this.btnSave.Enabled = true;
        }
        #endregion

        #region dgvData_RowHeaderMouseClick
        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region Deleted  Smose Liao 20100726
            ////取得目前使用者所點選的游標位置
            //CurRowIndex = int.Parse(dgvData.CurrentRow.Index.ToString());

            //try
            //{
            //    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, CurLocat, Type, aryMatnr);
            //    objStorageIn_LocationSelect.ShowDialog();
            //    strLocat = objStorageIn_LocationSelect.Locat;

            //    //換掉使用者所點選的游標位置之儲位   Smose Liao 20091110
            //    if (strLocat.Trim() != "")
            //    {
            //        dtData.Rows[CurRowIndex]["LOCAT"] = strLocat;
            //        dtData.AcceptChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    stsWarning.Text = ex.Message;
            //    return;
            //}
            #endregion

            string strTempLocat = "";
            string strTempCharg = "";
            string strTempEbeln = "";
            string strTempKdmat = "";

            try
            {
                strMblnr = dgvData.CurrentRow.Cells[7].Value.ToString();//Document No.
                strMatnr = dgvData.CurrentRow.Cells[2].Value.ToString();//Part No.
                strTempLocat = dgvData.CurrentRow.Cells[1].Value.ToString();//Location
                strTempCharg = dgvData.CurrentRow.Cells[4].Value.ToString();//Version
                strTempEbeln = dgvData.CurrentRow.Cells[9].Value.ToString();//PO No.
                strTempKdmat = dgvData.CurrentRow.Cells[10].Value.ToString();//Customer P/N

                StorageIn_OnLineIn_SpareParts_Add objStorageIn_OnLineIn_SpareParts_Add = new StorageIn_OnLineIn_SpareParts_Add(UserData, Progid, Werks, Lgort, strTempLocat, Sttyp, Lotyp, dtpIndat.Value.ToString("yyyyMMdd"), strMblnr, strMatnr, strTempEbeln, strTempKdmat, dtData, "SPARE_PARTS", Insmk, strTempCharg, Duplicate);
                objStorageIn_OnLineIn_SpareParts_Add.ShowDialog();
                dtData = objStorageIn_OnLineIn_SpareParts_Add.SapData;
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion
    }
}
