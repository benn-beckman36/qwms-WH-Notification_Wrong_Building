using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class StorageIn_OnLineIn_SpareParts_Hub : Form
    {
        #region 變數宣告

        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strBoxid = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private int CurRowIndex;
        private bool bolDuplicate = false;
        private string strPalletId;
        private DataTable dtData = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private ArrayList alLocat = new ArrayList();
        private ArrayList aryMblnr = new ArrayList();
        private ArrayList aryMatnr = new ArrayList();

        public struct MatnrLocat
        {
            public string[] Location;
            public string[] Part;
            public string[] PoNo;
            public string[] Kdmat;
            public string[] Version;
        }

        MatnrLocat sct;

        private int TotalLocation = 0;
        
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
                return this.txtBoxid.Text.Trim();
            }
            set
            {
                this.txtBoxid.Text = value;
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

        public StorageIn_OnLineIn_SpareParts_Hub()
        {
            InitializeComponent();
        }

        public StorageIn_OnLineIn_SpareParts_Hub(ref UserInfo varUserData, string strProgid)
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
                    txtBoxid.Focus();
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

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                stsWarning.Text = "";
                string strMblnr;
                string strZeile;
                string strOrderBy = "";
                strBoxid = txtBoxid.Text.Trim();
                DataTable dtPallet = new DataTable();
                strLocat = txtBoxid.Text.Trim();

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

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                QCI.QWMS.StorageData objStorage = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
                dtPallet = objStorage.QuerySpareParts_PalletId(strBoxid);
                if (dtPallet.Rows.Count == 0)
                {
                    stsWarning.Text = "找不到這一筆Box id的Pallet id，請確認!!";
                    return;
                }

                //依據Pallet id查詢整個棧板的所有Box id資料
                strMblnr = dtPallet.Rows[0]["MBLNR"].ToString();
                strZeile = dtPallet.Rows[0]["ZEILE"].ToString();
                strPalletId = strMblnr.Replace(strZeile, "");
                dtData = objStorage.QuerySparePartsAllBoxid(strPalletId);
                if (dtData.Rows.Count == 0)
                {
                    stsWarning.Text = "No Data!!";
                    return;
                }

                //Order by
                if (rdoEbeln.Checked)
                {
                    strOrderBy = "EBELN, MATNR, KDMAT, MBLNR";
                }
                if (rdoKdmat.Checked)
                {
                    strOrderBy = "KDMAT, MATNR, EBELN, MBLNR";
                }
                if (strOrderBy == "")
                {
                    strOrderBy = "MATNR, EBELN, KDMAT, MBLNR";
                }

                dtData = CommonInfo.SortDataTable(dtData, strOrderBy);


                #region //將相同料號、版本、PO No.、客人料號的資料加總成一筆記錄  Smose Liao 20100804
				int intCombinePartTotal = 0;
                StringBuilder sbCombineIndex = new StringBuilder();
                ArrayList alAllCombine = new ArrayList();
                DataRow[] combineRow;
                DataRow drRow;

                dtCombineStorage = dtData.Clone();
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    #region 每次比對的Index (sbCombineIndex)
                    sbCombineIndex.Remove(0, sbCombineIndex.Length);
                    sbCombineIndex.Append("MANDT='" + dtData.Rows[i]["MANDT"].ToString() + "'");
                    sbCombineIndex.Append(" and COMCD='" + dtData.Rows[i]["COMCD"].ToString() + "'");
                    sbCombineIndex.Append(" and WERKS='" + dtData.Rows[i]["WERKS"].ToString() + "'");
                    sbCombineIndex.Append(" and LGORT='" + dtData.Rows[i]["LGORT"].ToString() + "'");
                    sbCombineIndex.Append(" and MATNR='" + dtData.Rows[i]["MATNR"].ToString() + "'");
                    sbCombineIndex.Append(" and INSMK='" + dtData.Rows[i]["INSMK"].ToString() + "'");
                    sbCombineIndex.Append(" and CHARG='" + dtData.Rows[i]["CHARG"].ToString() + "'");
                    sbCombineIndex.Append(" and EBELN='" + dtData.Rows[i]["EBELN"].ToString() + "'");
                    sbCombineIndex.Append(" and KDMAT='" + dtData.Rows[i]["KDMAT"].ToString() + "'");
                    #endregion

                    if (alAllCombine.IndexOf(sbCombineIndex.ToString()) < 0)
					{
                        alAllCombine.Add(sbCombineIndex.ToString());
                        intCombinePartTotal = 0;

                        combineRow = dtData.Select(sbCombineIndex.ToString());
						for(int j=0; j<combineRow.Length;j++)
						{
                            intCombinePartTotal += Int32.Parse(combineRow[j]["MENGE"].ToString());
						}

                        //取得Pallet id
                        strMblnr = dtData.Rows[i]["MBLNR"].ToString();
                        strZeile = dtData.Rows[i]["ZEILE"].ToString();
                        strPalletId = strMblnr.Replace(strZeile, "");

						drRow = dtCombineStorage.NewRow();
                        drRow["MANDT"] = dtData.Rows[i]["MANDT"].ToString();
                        drRow["COMCD"] = dtData.Rows[i]["COMCD"].ToString();
                        drRow["WERKS"] = dtData.Rows[i]["WERKS"].ToString();
                        drRow["LGORT"] = dtData.Rows[i]["LGORT"].ToString();
                        drRow["MATNR"] = dtData.Rows[i]["MATNR"].ToString();
                        drRow["MTYPE"] = dtData.Rows[i]["MTYPE"].ToString();
                        //drRow["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString();
                        drRow["MBLNR"] = strPalletId;     //改存放Pallet id
                        drRow["ZEILE"] = dtData.Rows[i]["ZEILE"].ToString();
                        drRow["INSMK"] = dtData.Rows[i]["INSMK"].ToString();
                        drRow["CHARG"] = dtData.Rows[i]["CHARG"].ToString();
                        drRow["MENGE"] = intCombinePartTotal.ToString();
                        drRow["EBELN"] = dtData.Rows[i]["EBELN"].ToString();
                        drRow["KDMAT"] = dtData.Rows[i]["KDMAT"].ToString();
						dtCombineStorage.Rows.Add(drRow);
					}
                }

                //dtData.Clear();
                //dtData = dtCombineStorage.Copy();
                #endregion

                #region 新增欄位
                //dtData.Columns.Add("SELECT");
                //dtData.Columns.Add("NEWLGT");
                //dtData.Columns.Add("INDAT");
                //dtData.Columns.Add("LOCAT");
                //for (int i = 0; i < dtData.Rows.Count; i++)
                //{
                //    dtData.Rows[i]["SELECT"] = false;
                //    dtData.Rows[i]["NEWLGT"] = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                //    dtData.Rows[i]["INDAT"] = dtpIndat.Value.ToString("yyyyMMdd");
                //}

                dtCombineStorage.Columns.Add("SELECT");
                dtCombineStorage.Columns.Add("NEWLGT");
                dtCombineStorage.Columns.Add("INDAT");
                dtCombineStorage.Columns.Add("LOCAT");
                for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                {
                    dtCombineStorage.Rows[i]["SELECT"] = false;
                    dtCombineStorage.Rows[i]["NEWLGT"] = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    dtCombineStorage.Rows[i]["INDAT"] = dtpIndat.Value.ToString("yyyyMMdd");
                }
                #endregion

                this.gbHeader.Enabled = false;
                this.gbHeaderRight.Enabled = false; 
                this.btnSave.Enabled = true;
                ShowDataGrid();

                //取得空白儲位供W/H選取
                this.cmbLgort.Enabled = true;
                this.cmbLocat.Enabled = true;
                ShowLocat();
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
                stsWarning.Text = "";

                #region 檢查每筆資料是否都有選擇儲位
                if (dtCombineStorage.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                    {
                        if (dtCombineStorage.Rows[i]["LOCAT"].ToString() == "")
                        {
                            stsWarning.Text = "請先選擇要入庫的儲位!!";
                            SetbtnSaveException();
                            return;
                        }
                    }
                }
                #endregion

                #region 檢查每筆資料是否都有選擇外倉
                if (dtCombineStorage.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                    {
                        if (dtCombineStorage.Rows[i]["NEWLGT"].ToString() == "")
                        {
                            stsWarning.Text = "請先選擇要入庫的外倉!!";
                            SetbtnSaveException();
                            return;
                        }
                    }
                }
                #endregion

                for (int i = 0; i < dtCombineStorage.Rows.Count; i++)
                {
                    #region //宣告Struct陣列
                    sct.Location = new string[dtCombineStorage.Rows.Count];//儲位
                    sct.Part = new string[dtCombineStorage.Rows.Count];//料號
                    sct.PoNo = new string[dtCombineStorage.Rows.Count];//PO No.
                    sct.Kdmat = new string[dtCombineStorage.Rows.Count];//客人料號
                    sct.Version = new string[dtCombineStorage.Rows.Count];//版本
                    #endregion

                    #region //檢查是否有相同的料號但有不同PO No.
                    TotalLocation = 0;
                    for (int j = 0; j < dtCombineStorage.Rows.Count; j++)
                    {
                        if (CheckDifferentPoNo(j) == false)
                        {
                            stsWarning.Text = "相同的料號:" + dtCombineStorage.Rows[j]["MATNR"].ToString() + " 存在不同的PO No.，請放在不同的儲位!!";
                            SetbtnSaveException();
                            return;
                        }
                    }
                    #endregion

                    #region //檢查是否有相同的料號但有不同客人料號
                    TotalLocation = 0;
                    for (int j = 0; j < dtCombineStorage.Rows.Count; j++)
                    {
                        if (CheckDifferentCustomerPN(j) == false)
                        {
                            stsWarning.Text = "相同的料號:" + dtCombineStorage.Rows[j]["MATNR"].ToString() + " 存在不同的客人料號，請放在不同的儲位!!";
                            SetbtnSaveException();
                            return;
                        }
                    }
                    #endregion

                    #region //檢查是否有相同的料號但有不同版本
                    TotalLocation = 0;
                    for (int j = 0; j < dtCombineStorage.Rows.Count; j++)
                    {
                        if (CheckDifferentVersion(j) == false)
                        {
                            stsWarning.Text = "相同的料號:" + dtCombineStorage.Rows[j]["MATNR"].ToString() + " 存在不同的版本，請放在不同的儲位!!";
                            SetbtnSaveException();
                            return;
                        }
                    }
                    #endregion

                    QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);

                    #region //檢查要入庫的儲位是否已經有相同料號但不同版本(Smose Liao 20100615)
                    if (objStorageData.CheckExistedSameMaterial(Locat, dtCombineStorage.Rows[i]["MATNR"].ToString(), dtCombineStorage.Rows[i]["INSMK"].ToString(), dtCombineStorage.Rows[i]["CHARG"].ToString()))
                    {
                        stsWarning.Text = "料號:" + dtCombineStorage.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位(" + Locat + ")但有不同的版本!!";
                        SetbtnSaveException();
                        return;
                    }
                    #endregion

                    #region //檢查要入庫的儲位是否已經有相同料號但不同的PO No.(Smose Liao 20100615)
                    if (objStorageData.CheckExistedSameEbeln(Locat, dtCombineStorage.Rows[i]["MATNR"].ToString(), dtCombineStorage.Rows[i]["INSMK"].ToString(), dtCombineStorage.Rows[i]["CHARG"].ToString(), dtCombineStorage.Rows[i]["EBELN"].ToString()))
                    {
                        stsWarning.Text = "料號:" + dtCombineStorage.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位(" + Locat + ")但有不同的PO No.!!";
                        SetbtnSaveException();
                        return;
                    }
                    #endregion

                    #region //檢查要入庫的儲位是否已經有相同料號但不同的客人料號(Smose Liao 20100615)
                    if (objStorageData.CheckExistedSameKdmat(Locat, dtCombineStorage.Rows[i]["MATNR"].ToString(), dtCombineStorage.Rows[i]["INSMK"].ToString(), dtCombineStorage.Rows[i]["CHARG"].ToString(), dtCombineStorage.Rows[i]["KDMAT"].ToString()))
                    {
                        stsWarning.Text = "料號:" + dtCombineStorage.Rows[i]["MATNR"].ToString() + " 已經存在相同儲位(" + Locat + ")但有不同的客人料號!!";
                        SetbtnSaveException();
                        return;
                    }
                    #endregion
                }
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

                if (objStorageIn.AddSparePartsCannibalizeData(dtCombineStorage, dtData, strPalletId))
                {
                    stsWarning.Text = "Add OK!!";

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

        #region CheckDifferentPoNo(檢查是否有相同的料號，但有不同的PO No.)
        public bool CheckDifferentPoNo(int j)
        {
            bool chkFlag = true;
            bool addItem = true;

            if (TotalLocation <= 0)
            {
                addItem = true;
                goto Add;
            }

            for (int k = 0; k < TotalLocation; k++)
            {
                //與存在Struct陣列中的資料相比：料號相同，儲位也相同
                if (sct.Location[k].ToString() == dtCombineStorage.Rows[j]["LOCAT"].ToString() && sct.Part[k].ToString() == dtCombineStorage.Rows[j]["MATNR"].ToString())
                {
                    //PO不同，需放不同的儲位：return false
                    if (sct.PoNo[k].ToString() != dtCombineStorage.Rows[j]["EBELN"].ToString())
                    {
                        chkFlag = false;
                        break;
                    }
                }
            }

        Add:
            if (addItem == true)
            {
                AddDifferentPartLocation(TotalLocation, j, "Ebeln");
                TotalLocation++;
            }

            return chkFlag;
        }
        #endregion

        #region CheckDifferentCustomerPN(檢查是否有相同的料號，但有不同的客人料號)
        public bool CheckDifferentCustomerPN(int j)
        {
            bool chkFlag = true;
            bool addItem = true;

            if (TotalLocation <= 0)
            {
                addItem = true;
                goto Add;
            }

            for (int k = 0; k < TotalLocation; k++)
            {
                //與存在Struct陣列中的資料相比：料號相同，儲位也相同
                if (sct.Location[k].ToString() == dtCombineStorage.Rows[j]["LOCAT"].ToString() && sct.Part[k].ToString() == dtCombineStorage.Rows[j]["MATNR"].ToString())
                {
                    //客人料號不同，需放不同的儲位：return false
                    if (sct.Kdmat[k].ToString() != dtCombineStorage.Rows[j]["KDMAT"].ToString())
                    {
                        chkFlag = false;
                        break;
                    }
                }
            }

        Add:
            if (addItem == true)
            {
                AddDifferentPartLocation(TotalLocation, j, "Kdmat");
                TotalLocation++;
            }

            return chkFlag;
        }
        #endregion

        #region CheckDifferentVersion(檢查是否有相同的料號，但有不同的版本)
        public bool CheckDifferentVersion(int j)
        {
            bool chkFlag = true;
            bool addItem = true;

            if (TotalLocation <= 0)
            {
                addItem = true;
                goto Add;
            }

            for (int k = 0; k < TotalLocation; k++)
            {
                //與存在Struct陣列中的資料相比：料號相同，儲位也相同
                if (sct.Location[k].ToString() == dtCombineStorage.Rows[j]["LOCAT"].ToString() && sct.Part[k].ToString() == dtCombineStorage.Rows[j]["MATNR"].ToString())
                {
                    //版本不同，需放不同的儲位：return false
                    if (sct.Version[k].ToString() != dtCombineStorage.Rows[j]["CHARG"].ToString())
                    {
                        chkFlag = false;
                        break;
                    }
                }
            }

        Add:
            if (addItem == true)
            {
                AddDifferentPartLocation(TotalLocation, j, "Charg");
                TotalLocation++;
            }

            return chkFlag;
        }
        #endregion

        #region AddDifferentPartLocation(新增儲位、料號、PO No.、客人料號、版本至Struct陣列中)
        public void AddDifferentPartLocation(int p, int q, string strVar)
        {
            sct.Location[p] = dtCombineStorage.Rows[q]["LOCAT"].ToString();
            sct.Part[p] = dtCombineStorage.Rows[q]["MATNR"].ToString();
            if (strVar == "Ebeln")
            {
                sct.PoNo[p] = dtCombineStorage.Rows[q]["EBELN"].ToString();
            }
            else if (strVar == "Kdmat")
            {
                sct.Kdmat[p] = dtCombineStorage.Rows[q]["KDMAT"].ToString();
            }
            else if (strVar == "Charg")
            {
                sct.Version[p] = dtCombineStorage.Rows[q]["CHARG"].ToString();
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            this.gbHeader.Enabled = true;
            this.gbHeaderRight.Enabled = true;
            this.cmbWerks.SelectedIndex = 0;
            this.cmbLgort.SelectedIndex = 0;
            this.chkSelectAll.Checked = false;
            this.chkClearAll.Checked = false;
            this.cmbLgort.Enabled = false;
            this.txtBoxid.Text = "";
            this.dtData.Rows.Clear();
            this.dtCombineStorage.Rows.Clear();
            this.dgvData.DataSource = null;
            this.btnSave.Enabled = false;
            this.btnModify.Enabled = false;
            this.lblCount.Text = "0 records";
            this.txtCharg.Text = "";
            this.txtKdmat.Text = "";
            this.txtEbeln.Text = "";
        }
        #endregion

        #region btnExit_Click
        private void btnExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion 
 
        #region SetbtnSaveProcess(按鈕設定-儲存中)
        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;
        }
        #endregion

        #region SetbtnSaveException(按鈕設定-儲存失敗)
        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
        }
        #endregion

        #region ShowDataGrid(顯示入庫資料)
        public void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "SELECT";
                dgvcSelect.HeaderText = "Select";
                dgvcSelect.Width = 50;
                dgvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "NEWLGT";
                dgvcLgort.HeaderText = "Transfer Storage";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 80;
                dgvData.Columns.Add(dgvcLgort);

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

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                dgvcEbeln.Width = 120;
                dgvData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                dgvcKdmat.DataPropertyName = "KDMAT";
                dgvcKdmat.HeaderText = "Customer P/N";
                dgvcKdmat.ReadOnly = true;
                dgvcKdmat.Width = 120;
                dgvData.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 80;
                dgvData.Columns.Add(dgvcCharg);


                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                //dgvcMblnr.HeaderText = "Document No.";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 150;
                dgvData.Columns.Add(dgvcMblnr);

                //DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                //dgvcZeile.DataPropertyName = "ZEILE";
                //dgvcZeile.HeaderText = "Document Item";
                //dgvcZeile.ReadOnly = true;
                //dgvcZeile.Width = 80;
                //dgvData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Storage In Qty";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 80;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 80;
                dgvData.Columns.Add(dgvcIndat);

                //dgvData.DataSource = Data;
                //lblCount.Text = Data.Rows.Count.ToString() + " records";
                dgvData.DataSource = dtCombineStorage;
                lblCount.Text = dtCombineStorage.Rows.Count.ToString() + " records";

                this.btnModify.Enabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //取得目前使用者點選的游標位置
            CurRowIndex = int.Parse(dgvData.CurrentRow.Index.ToString());

            try
            {
                StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, CurLocat, "NEW", aryMatnr);
                objStorageIn_LocationSelect.ShowDialog();
                strLocat = objStorageIn_LocationSelect.Locat;

                if (strLocat.Trim() != "")
                {
                    dtCombineStorage.Rows[CurRowIndex]["LOCAT"] = strLocat;
                    dtCombineStorage.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }	
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            chkClearAll.Checked = false;
        }

        private void chkSelectAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                for (int i = 0; i < dgvData.Rows.Count - 1; i++)
                {
                    dtCombineStorage.Rows[i]["SELECT"] = true;
                }

                dtCombineStorage.AcceptChanges();
            }
            else if (chkSelectAll.Checked == false)
            {
                for (int i = 0; i < dgvData.Rows.Count - 1; i++)
                {
                    dtCombineStorage.Rows[i]["SELECT"] = false;
                }

                dtCombineStorage.AcceptChanges();
            }
        }

        private void chkClearAll_CheckedChanged(object sender, EventArgs e)
        {
            chkSelectAll.Checked = false;
            for (int i = 0; i < dgvData.Rows.Count - 1; i++)
            {
                dtCombineStorage.Rows[i]["SELECT"] = false;
            }

            dtCombineStorage.AcceptChanges();

            this.txtCharg.Text = "";
            this.txtEbeln.Text = "";
            this.txtKdmat.Text = "";
        }

        private void cmbLocat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowSelectFalse = 0;
            stsWarning.Text = "";
            for (int i = 0; i < dgvData.Rows.Count - 1; i++)
            {
                //使用者已勾選資料列，更換儲位
                if (dgvData.Rows[i].Cells[0].Value.ToString() == "True")
                {
                    dtCombineStorage.Rows[i]["LOCAT"] = cmbLocat.Items[cmbLocat.SelectedIndex].ToString();
                }
                else
                {
                    rowSelectFalse++;
                }
            }

            dtCombineStorage.AcceptChanges();

            //使用者沒有勾選任何一筆資料列
            if (rowSelectFalse == dgvData.Rows.Count - 1)
            {
                stsWarning.Text = "請先勾選要選擇儲位的資料列!!";
            }
        }

        private void chkAddIn_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtLocat = new DataTable();
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

            cmbLocat.Items.Clear();
            if (chkAddIn.Checked == true)
            {
                //取得有庫存的儲位(不包含待出貨儲位)
                dtLocat = objPlantData.GetAllLocatNoneShip(Werks, Lgort, "", "1", "", "");
                for (int i = 0; i < dtLocat.Rows.Count; i++)
                {
                    cmbLocat.Items.Add(dtLocat.Rows[i]["LOCAT"].ToString());
                }
            }
            else
            {
                //取得空庫存的儲位(不包含待出貨儲位)
                dtLocat = objPlantData.GetAllLocatNoneShip(Werks, Lgort, "", "0", "", "");
                for (int i = 0; i < dtLocat.Rows.Count; i++)
                {
                    cmbLocat.Items.Add(dtLocat.Rows[i]["LOCAT"].ToString());
                }
            }
        }

        #region ShowLocat
        private void ShowLocat()
        {
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                DataTable dtLocat = new DataTable();
                cmbLocat.Items.Clear();
                //取得空儲位(不包含待出貨儲位)
                dtLocat = objPlantData.GetAllLocatNoneShip(Werks, Lgort, "", "0", "", "");
                for (int i = 0; i < dtLocat.Rows.Count; i++)
                {
                    cmbLocat.Items.Add(dtLocat.Rows[i]["LOCAT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowLocat()");
            }
        }
        #endregion

        private void btnModify_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvData.Rows.Count - 1; i++)
            {
                if (dgvData.Rows[i].Cells[0].Value.ToString() == "True")
                {
                    if (txtCharg.Text.Trim() != "")
                        dtCombineStorage.Rows[i]["CHARG"] = txtCharg.Text.Trim().ToString();

                    if (txtKdmat.Text.Trim() != "")
                        dtCombineStorage.Rows[i]["KDMAT"] = txtKdmat.Text.Trim().ToString();

                    if (txtEbeln.Text.Trim() != "")
                        dtCombineStorage.Rows[i]["EBELN"] = txtEbeln.Text.Trim().ToString();
                }
                else
                {
                    stsWarning.Text = "請先勾選要修改的資料列!!";
                }
            }

            dtCombineStorage.AcceptChanges();
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //選擇外倉的倉別
            for (int i = 0; i < dgvData.Rows.Count - 1; i++)
            {
                dtCombineStorage.Rows[i]["NEWLGT"] = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }

            dtCombineStorage.AcceptChanges();

            //秀新的儲位供W/H人員選取
            Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            ShowLocat();
        }

    }
}
