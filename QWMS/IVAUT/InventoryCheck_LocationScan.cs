using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using QCI.QWMS;
using QWMS.Common;
using System.IO;

namespace QWMS
{
    public partial class InventoryCheck_LocationScan : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strIndat = "";
        private string strLgort = "";
        private string strMatnr = "";
        private string strInsmk = "";
        private string strFromLocat = "";
        private string strToLocat = "";
        private string strLocat = "";
        private string strLocod = "";
        private string strLifnr = "";
        private string strType = "";
        private string strScanNo = "";
        private string strQty = "";
        private string strDidno = "";
        private string strPalletID = "";
        private int intFormIndex = 0;
        private Counting objCounting;
        private PlantData objPlantData;
        private Authority objAuthority;
        private DataTable dtData;
        private DataTable dtTemp;
        private DataTable dtStorage;
        private DataTable dtPrint;
        private FileInfo fi;
        private StreamWriter sw;
        int LocatCount = 0;
        private StreamReader srFileReader;

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

        public string FromLocat
        {
            get
            {
                return strFromLocat;
            }
            set
            {
                strFromLocat = value;
            }
        }

        public string ToLocat
        {
            get
            {
                return strToLocat;
            }
            set
            {
                strToLocat = value;
            }
        }

        public string Locat
        {
            get
            {
                return strLocat;
            }
            set
            {
                strLocat = value;
            }
        }

        public string ScanNo
        {
            get
            {
                return this.txtScan.Text.Trim();
            }
            set
            {
                this.txtScan.Text = value;
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

        public DataTable Storage
        {
            get
            {
                return dtStorage;
            }
            set
            {
                dtStorage = value;
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

        public string Qty
        {
            get
            {
                return strQty;
            }
            set
            {
                strQty = value;
            }
        }

        public string Didno
        {
            get
            {
                return strDidno;
            }
            set
            {
                strDidno = value;
            }
        }

        #endregion

        public InventoryCheck_LocationScan(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            Data = dtData;

            try
            {
                objCounting = new Counting(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                //檢查權限
                if (!objCounting.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlInsmk();

                    dtData = new DataTable();
                    dtData.Columns.Add("MANDT", Type.GetType());
                    dtData.Columns.Add("COMCD", Type.GetType());
                    dtData.Columns.Add("WERKS", Type.GetType());
                    dtData.Columns.Add("LGORT", Type.GetType());
                    dtData.Columns.Add("LOCAT", Type.GetType());
                    dtData.Columns.Add("MATNR", Type.GetType());
                    dtData.Columns.Add("INSMK", Type.GetType());
                    dtData.Columns.Add("CHARG", Type.GetType());
                    dtData.Columns.Add("LIFNR", Type.GetType());
                    dtData.Columns.Add("MENGE", Type.GetType());
                    dtData.Columns.Add("SCQTY", Type.GetType());
                    dtData.Columns.Add("DIFQTY", Type.GetType());
                    dtData.Columns.Add("BKQTY", Type.GetType());
                    dtData.Columns.Add("MBLNR", Type.GetType());
                    dtData.Columns.Add("INDAT", Type.GetType());
                    dtData.Columns.Add("KDMAT", Type.GetType());
                    dtData.Columns.Add("LOCOD", Type.GetType());
                    dtData.Columns.Add("RMANO", Type.GetType());
                    dtData.Columns.Add("MAKTX", Type.GetType());
                    dtData.Columns.Add("PKDAT", Type.GetType());
                    dtData.Columns.Add("DACOD", Type.GetType());   
                    Data = dtData;


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
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
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

        # region ShowDdlInsmk
        private void ShowDdlInsmk()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbInsmk.Items.Clear();
                dtTemp = objPlantData.GetDdlInsmk();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            if (cmbInsmk.SelectedIndex != -1)
            {
                strInsmk = cmbInsmk.Items[cmbInsmk.SelectedIndex].ToString();
            }
            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "Plant and storage can't be empty!!";
                return;
            }
            if (strInsmk == "")
            {
                stsWarning.Text = "Stock can't be empty!!";
                return;
            }
            if (this.rdbScanDIDNO.Checked)
            {
                this.txtDidno.Enabled = true;
                this.txtQty.Enabled = true;
                this.txtScan.Enabled = false;
                this.txtLocat.Enabled = true;
                this.txtLocat.Focus();
            }
            if (this.rdbScanQRcode.Checked)
            {
                this.txtScan.Enabled = true;
                this.txtDidno.Enabled = false;
                this.txtQty.Enabled = false;
                this.txtLocat.Enabled = true;
                this.txtLocat.Focus();
            }
            strFromLocat = this.txtFromLocat.Text.ToString();
            strToLocat = this.txtToLocat.Text.ToString();
            dtStorage = new DataTable();
            objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                        CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
            dtStorage = objCounting.QueryStorageInDataBySMT(Insmk, FromLocat, ToLocat);

            dtStorage.Columns.Add("SCQTY");
            dtStorage.Columns.Add("DIFQTY");
            dtStorage.Columns.Add("DACOD");
            for (int i = 0; i < dtStorage.Rows.Count; i++)
            {
                dtStorage.Rows[i]["SCQTY"] = 0;
                dtStorage.Rows[i]["DIFQTY"] = 0;
            }
            dtPrint = dtStorage.Copy();
            ShowDataGridAll();
            this.btnConfirm.Enabled = false;
            this.txtFromLocat.Enabled = false;
            this.txtToLocat.Enabled = false;
        }


        # region 確認視窗是否已經打開
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

        private void txtFromLocat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    Werks = this.cmbWerks.Text.ToString();
                    Lgort = this.cmbLgort.Text.ToString();
                    Insmk = this.cmbInsmk.Text.ToString();
                    if (Werks == "" || Lgort == "")
                    {
                        stsWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                    if (Insmk == "")
                    {
                        stsWarning.Text = "Stock can't be empty!!";
                        return;
                    }
                    this.txtToLocat.Focus();
                    this.txtToLocat.SelectAll();
                    strFromLocat = this.txtFromLocat.Text.ToString();
                    if (strFromLocat != "")
                    {
                        if (!objPlantData.CheckExistedStorageData(Werks, Lgort, strFromLocat))
                        {
                            this.txtFromLocat.Focus();
                            this.txtFromLocat.SelectAll();
                            this.txtFromLocat.Text = "";
                            throw new Exception("The location doesn't exist!!");
                        }
                    }
                }
                catch (Exception ex)
                {

                    Sound.Play(@"Sound\ERROR.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtFromLocat.Focus();
                    this.txtFromLocat.SelectAll();
                    return;
                }
            }
        }

        private void txtToLocat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    strToLocat = this.txtToLocat.Text.ToString();
                    if (strToLocat != "")
                    {
                        if (!objPlantData.CheckExistedStorageData(Werks, Lgort, strToLocat))
                        {
                            this.txtToLocat.Focus();
                            this.txtToLocat.SelectAll();
                            this.txtToLocat.Text = "";
                            throw new Exception("The location doesn't exist!!");
                        }
                    }
                    btnConfirm_Click(null,null);
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\ERROR.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtToLocat.Focus();
                    this.txtToLocat.SelectAll();
                    return;
                }
            }
        }

        private void txtLocat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    strLocat = this.txtLocat.Text.ToString().Trim();
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        if (dtStorage.Select("LOCAT = '"+strLocat+"'").Length==0)
                        {
                            this.txtDidno.Enabled = false;
                            this.txtQty.Enabled = false;
                            throw new Exception("该储位不在储位区间中!!");
                        }
                    }
                    if (this.rdbScanDIDNO.Checked)
                    {
                        this.txtDidno.Enabled = true;
                        this.txtQty.Enabled = true;
                        this.txtScan.Enabled = false;
                        this.txtLocat.Enabled = true;
                        this.txtDidno.Focus();
                    }
                    if (this.rdbScanQRcode.Checked)
                    {
                        this.txtScan.Enabled = true;
                        this.txtDidno.Enabled = false;
                        this.txtQty.Enabled = false;
                        this.txtLocat.Enabled = true;
                        this.txtScan.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\ERROR.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtDidno.Focus();
                    this.txtDidno.SelectAll();
                    return;
                }
            }
        }

        private void txtDidno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    strDidno = this.txtDidno.Text.ToString().Trim();
                    this.txtQty.Focus();
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\ERROR.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtDidno.Focus();
                    this.txtDidno.SelectAll();
                    return;
                }
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    strMandt = Mandt;
                    strComcd = Comcd;
                    Werks = this.cmbWerks.Text.ToString();
                    Lgort = this.cmbLgort.Text.ToString();
                    strLocat = this.txtLocat.Text.ToString();
                    strInsmk = this.cmbInsmk.Text.ToString();
                    strQty = "";
                    strDidno = "";
                    strQty = this.txtQty.Text.ToString();
                    strDidno = this.txtDidno.Text.ToString().Trim();
                    string strTempMblnrMatnr = "";
                    dtTemp= new DataTable();
                    DataRow drRow;
                    if (strDidno == "")
                    {
                        this.txtDidno.Focus();
                        this.txtDidno.SelectAll();
                        throw new Exception("DIDNO can't be empty!!");
                    }
                    if (!CheckIsNumber(strQty) || strQty == "0")
                    {
                        this.txtQty.Focus();
                        this.txtQty.SelectAll();
                        throw new Exception("Store in Qty should be numeric and greater than 0!!");
                    }
                   
                    objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                                CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                    dtTemp = objCounting.QueryDIDPIDData(strLocat, strDidno.Substring(0, 11), strInsmk, "DIDNO");
                    dtTemp.Columns.Add("SCQTY");
                    dtTemp.Columns.Add("DIFQTY");
                    
                    if (dtTemp.Rows.Count == 0)   
                    {
                        this.txtDidno.Focus();
                        this.txtDidno.SelectAll();
                        throw new Exception("No Data!!");
                    }
                   
                    //if (dtTemp.Rows.Count > 1)
                    //{
                    //    this.txtScan.Focus();
                    //    this.txtScan.SelectAll();
                    //    throw new Exception("此一维码对应了多个料号!!");
                    //}
                    dtTemp.Rows[0]["SCQTY"] = strQty;

                    if (strLgort != "TW50")
                    {
                        for (int i = 0; i < Data.Rows.Count; i++)
                        {
                            if (strTempMblnrMatnr.IndexOf(Data.Rows[i]["MBLNR"].ToString() + ";") == -1)
                            {
                                strTempMblnrMatnr += Data.Rows[i]["MBLNR"].ToString() + ";";
                            }
                        }
                        if (strTempMblnrMatnr.IndexOf(strDidno + ";") != -1)
                        {
                            this.txtDidno.Focus();
                            this.txtDidno.SelectAll();
                            throw new Exception("The data you input is duplicate in the location!!");
                        }
                    }

                    for (int i = dtStorage.Rows.Count-1; i >= 0; i--)
                    {
                        //刷入的料号与查出的料号匹配时
                        if (dtStorage.Rows[i]["LOCAT"].ToString() == dtTemp.Rows[0]["LOCAT"].ToString() && dtStorage.Rows[i]["MATNR"].ToString() == dtTemp.Rows[0]["MATNR"].ToString())
                        {
                            //刷入数量为0，即第一次刷入料号时
                            if (Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) == 0||dtStorage.Rows[i]["SCQTY"].ToString()=="")
                            {
                                //当库存数量小于刷入数量
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) < Convert.ToInt32(dtTemp.Rows[0]["SCQTY"]))
                                {
                                    dtStorage.Rows[i]["SCQTY"] = strQty;   //获得刷入数量放在SCQTY栏位中
                                    dtStorage.Rows[i]["DIFQTY"] =  Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) - Convert.ToInt32(strQty); //获得差异数量放在DIFQTY栏位中
                                    LocatCount++;
                                }
                                //当库存数量大于刷入数量
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) >Convert.ToInt32(dtTemp.Rows[0]["SCQTY"]))
                                {
                                    dtStorage.Rows[i]["SCQTY"] = strQty;   //获得刷入数量放在SCQTY栏位中
                                    dtStorage.Rows[i]["DIFQTY"] =   Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -Convert.ToInt32(strQty);//获得差异数量放在DIFQTY栏位中
                                    LocatCount++;
                                }
                                //当库存数量与刷入数量相等时删除该条明细
                                if (dtStorage.Rows[i]["MENGE"].ToString() == dtTemp.Rows[0]["SCQTY"].ToString())
                                {
                                    dtStorage.Rows.RemoveAt(i);
                                    LocatCount++;
                                }
                            }
                            //当dtStorage刷入数量不为0，即已多次刷入时
                            else if (Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) != 0 || dtStorage.Rows[i]["SCQTY"].ToString() != "")
                            {
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) < Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                {
                                    dtStorage.Rows[i]["SCQTY"] = Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                    dtStorage.Rows[i]["DIFQTY"] = Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) - Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);   //获得新差异数量
                                    LocatCount++;
                                }
                                //当库存数量仍大于刷入数量时
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) >Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                {
                                    dtStorage.Rows[i]["SCQTY"] =Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                    dtStorage.Rows[i]["DIFQTY"] =Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) - Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);  //获得新差异数量
                                    LocatCount++;
                                }
                                //消失库存数量与刷入数量相等的明细
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) == Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                {
                                    dtStorage.Rows.RemoveAt(i);
                                }
                            }
                        }
                    }

                    for (int i = dtPrint.Rows.Count - 1; i >= 0; i--)
                    {
                        //刷入的料号与查出的料号匹配时
                        if (dtPrint.Rows[i]["LOCAT"].ToString() == dtTemp.Rows[0]["LOCAT"].ToString() && dtPrint.Rows[i]["MATNR"].ToString() == dtTemp.Rows[0]["MATNR"].ToString())
                        {
                            //刷入数量为0，即第一次刷入料号时
                            if (Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) == 0 || dtPrint.Rows[i]["SCQTY"].ToString() == "")
                            {
                                dtPrint.Rows[i]["SCQTY"] = strQty;   //获得刷入数量放在SCQTY栏位中
                                dtPrint.Rows[i]["DIFQTY"] =     //获得差异数量放在DIFQTY栏位中
                                    Convert.ToInt32(dtPrint.Rows[i]["MENGE"].ToString()) -
                                    Convert.ToInt32(strQty);
                            }
                            //当dtStorage刷入数量不为0，即已多次刷入时
                            else if (Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) != 0 || dtPrint.Rows[i]["SCQTY"].ToString() != "")
                            {
                                dtPrint.Rows[i]["SCQTY"] = Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                dtPrint.Rows[i]["DIFQTY"] =     //获得新差异数量
                                    Convert.ToInt32(dtPrint.Rows[i]["MENGE"].ToString()) -
                                    Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]);
                            }
                        }
                    }

                    drRow = Data.NewRow();
                    drRow["MANDT"] = dtTemp.Rows[0]["MANDT"].ToString();
                    drRow["COMCD"] = dtTemp.Rows[0]["COMCD"].ToString();
                    drRow["WERKS"] = dtTemp.Rows[0]["WERKS"].ToString();
                    drRow["LGORT"] = dtTemp.Rows[0]["LGORT"].ToString();
                    drRow["LOCAT"] = dtTemp.Rows[0]["LOCAT"].ToString();
                    drRow["MATNR"] = dtTemp.Rows[0]["MATNR"].ToString();
                    drRow["INSMK"] = dtTemp.Rows[0]["INSMK"].ToString();
                    drRow["CHARG"] = dtTemp.Rows[0]["CHARG"].ToString();
                    drRow["LIFNR"] = dtTemp.Rows[0]["LIFNR"].ToString();
                    drRow["MENGE"] = dtTemp.Rows[0]["MENGE"].ToString();
                    drRow["SCQTY"] = dtTemp.Rows[0]["SCQTY"].ToString();
                    drRow["DIFQTY"] = dtTemp.Rows[0]["DIFQTY"].ToString();
                    drRow["BKQTY"] = dtTemp.Rows[0]["BKQTY"].ToString();
                    drRow["MBLNR"] = strDidno;
                    drRow["INDAT"] = dtTemp.Rows[0]["INDAT"].ToString();
                    drRow["KDMAT"] = dtTemp.Rows[0]["KDMAT"].ToString();
                    drRow["RMANO"] = dtTemp.Rows[0]["RMANO"].ToString();
                    drRow["MAKTX"] = dtTemp.Rows[0]["MAKTX"].ToString();
                    drRow["PKDAT"] = dtTemp.Rows[0]["PKDAT"].ToString();
                    Data.Rows.Add(drRow);

                    this.txtScan.Text = "";
                    this.txtDidno.Text = "";
                    this.txtQty.Text = "";
                    ShowDataGridAll();
                    txtDidno.Focus();
                    if (dtStorage.Select("LOCAT='" + strLocat + "'").Length == 0)
                    {
                        if (Data.Select("LOCAT ='" + strLocat + "'").Length == LocatCount)
                        {
                            this.txtLocat.Text = "";
                            this.txtLocat.SelectAll();
                            this.txtLocat.Focus();
                            LocatCount = 0;
                        }
                    }

                    this.btnPrint.Enabled = true;
                    this.btnDownload.Enabled = true;
                    Sound.Play(@"Sound\Success.wav");
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\ERROR.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtDidno.Text = "";
                    this.txtQty.Text = "";
                    this.txtDidno.Focus();
                    this.txtDidno.SelectAll();
                    return;
                }
            }
        }

        private void txtScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    strMandt = Mandt;
                    strComcd = Comcd;
                    Werks = this.cmbWerks.Text.ToString();
                    Lgort = this.cmbLgort.Text.ToString();
                    strLocat = this.txtLocat.Text.ToString();
                    strIndat = "";
                    strInsmk = "";
                    strMatnr = "";
                    strLocod = "";
                    strLifnr = "";
                    strScanNo = txtScan.Text.Trim();
                    strQty = "";
                    string strTempMblnrMatnr = "";
                    if (strScanNo == "")
                    {
                        this.txtScan.Focus();
                        this.txtScan.SelectAll();
                        throw new Exception("Scan can't be empty!!");
                    }
                    if (cmbInsmk.Text == "")
                    {
                        this.txtScan.Focus();
                        this.txtScan.SelectAll();
                        throw new Exception( "Stock can't be empty!!");
                    }
                    if (txtScan.TextLength < 10)
                    {
                        this.txtScan.Focus();
                        this.txtScan.SelectAll();
                        throw new Exception("Error Data!!");
                    }
                    if (txtScan.TextLength > 10)
                    {
                        
                        if (strScanNo.Length == 23)
                        {
                            this.txtScan.Focus();
                            this.txtScan.SelectAll();
                            throw new Exception("请刷正确的二维码!!");
                        }
                        
                        string[] strSplit = strScanNo.Split(new char[]{';'});


                        strMandt = Mandt;
                        strComcd = Comcd;
                        strMatnr = strSplit[0];
                        strIndat = strSplit[1];
                        strInsmk = this.cmbInsmk.Text.ToString();
                        strWerks = this.cmbWerks.Text.ToString();
                        strLgort = this.cmbLgort.Text.ToString();
                        strLifnr = strSplit[2];
                        strLocod = strSplit[3];
                        strQty = strSplit[4];

                        dtTemp = new DataTable();
                        DataRow drRow;
                        dtTemp.Clear();
                        objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                                CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                        dtTemp = objCounting.QueryQRCode(strLocat, strMatnr, strInsmk);
                        dtTemp.Columns.Add("SCQTY");
                        dtTemp.Columns.Add("DIFQTY");
                        if (dtTemp.Rows.Count == 0)
                        {
                            this.txtScan.Focus();
                            this.txtScan.SelectAll();
                            throw new Exception("No Data!!");
                        }
                        if (!CheckIsNumber(strQty) || strQty == "0")
                        {
                            this.txtScan.Focus();
                            this.txtScan.SelectAll();
                            throw new Exception("Store in Qty should be numeric and greater than 0!!");
                        }
                        //if (dtTemp.Rows.Count > 1)
                        //{
                        //    this.txtScan.Focus();
                        //    this.txtScan.SelectAll();
                        //    throw new Exception("此二维码对应了多个料号!!");
                        //}
                        dtTemp.Rows[0]["SCQTY"] = strQty;

                        if (strLgort != "TW50")
                        {
                            for (int i = 0; i < Data.Rows.Count; i++)
                            {
                                if (
                                    strTempMblnrMatnr.IndexOf(Data.Rows[i]["MATNR"].ToString() +
                                                              Data.Rows[i]["INDAT"].ToString() +
                                                              Data.Rows[i]["LIFNR"].ToString() +
                                                              Data.Rows[i]["LOCOD"].ToString() +
                                                              Data.Rows[i]["SCQTY"].ToString() + ";") ==
                                    -1)
                                {
                                    strTempMblnrMatnr += Data.Rows[i]["MATNR"].ToString() +
                                                         Data.Rows[i]["INDAT"].ToString() +
                                                         Data.Rows[i]["LIFNR"].ToString() +
                                                         Data.Rows[i]["LOCOD"].ToString() +
                                                         Data.Rows[i]["SCQTY"].ToString() + ";";
                                }
                            }
                            if (strTempMblnrMatnr.IndexOf(strMatnr + strIndat + strLifnr + strLocod + strQty + ";") !=
                                -1)
                            {
                                this.txtScan.Focus();
                                this.txtScan.SelectAll();
                                throw new Exception("The data you input is duplicate in the location!!");
                            }
                        }



                        for (int i = dtStorage.Rows.Count - 1; i >= 0; i--)
                        {
                            //刷入的料号与查出的料号匹配时
                            if (dtStorage.Rows[i]["LOCAT"].ToString() == dtTemp.Rows[0]["LOCAT"].ToString() && dtStorage.Rows[i]["MATNR"].ToString() == dtTemp.Rows[0]["MATNR"].ToString())
                            {
                                dtStorage.Rows[i]["DACOD"] = strIndat;
                                //刷入数量为0，即第一次刷入料号时
                                if (Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) == 0 || dtStorage.Rows[i]["SCQTY"].ToString() == "")
                                {
                                    if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) <
                                        Convert.ToInt32(dtTemp.Rows[0]["SCQTY"]))
                                    {
                                        dtStorage.Rows[i]["SCQTY"] = strQty;   //获得刷入数量放在SCQTY栏位中
                                        dtStorage.Rows[i]["DIFQTY"] =     //获得差异数量放在DIFQTY栏位中
                                            Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -
                                            Convert.ToInt32(strQty);
                                        LocatCount++;
                                    }
                                    //当库存数量大于刷入数量
                                    if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) >
                                        Convert.ToInt32(dtTemp.Rows[0]["SCQTY"]))
                                    {
                                        dtStorage.Rows[i]["SCQTY"] = strQty;   //获得刷入数量放在SCQTY栏位中
                                        dtStorage.Rows[i]["DIFQTY"] =     //获得差异数量放在DIFQTY栏位中
                                            Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -
                                            Convert.ToInt32(strQty);
                                        LocatCount++;
                                    }
                                    //当库存数量与刷入数量相等时删除该条明细
                                    if (dtStorage.Rows[i]["MENGE"].ToString() == dtTemp.Rows[0]["SCQTY"].ToString())
                                    {
                                        dtStorage.Rows.RemoveAt(i);
                                        LocatCount++;
                                    }
                                }
                                //当dtStorage刷入数量不为0，即已多次刷入时
                                else if (Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) != 0 || dtStorage.Rows[i]["SCQTY"].ToString() != "")
                                {
                                    if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) <
                                        Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                    {
                                        dtStorage.Rows[i]["SCQTY"] = Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                        dtStorage.Rows[i]["DIFQTY"] =     //获得新差异数量
                                            Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -
                                            Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);
                                        LocatCount++;
                                    }
                                    //当库存数量仍大于刷入数量时
                                    if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) >
                                        Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                    {
                                        dtStorage.Rows[i]["SCQTY"] = Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                        dtStorage.Rows[i]["DIFQTY"] =     //获得新差异数量
                                            Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -
                                            Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);
                                        LocatCount++;
                                    }
                                    //消失库存数量与刷入数量相等的明细
                                    if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) ==
                                        Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                    {
                                        dtStorage.Rows.RemoveAt(i);
                                    }
                                }
                            }
                        }

                        for (int i = dtPrint.Rows.Count - 1; i >= 0; i--)
                        {
                            //刷入的料号与查出的料号匹配时
                            if (dtPrint.Rows[i]["LOCAT"].ToString() == dtTemp.Rows[0]["LOCAT"].ToString() && dtPrint.Rows[i]["MATNR"].ToString() == dtTemp.Rows[0]["MATNR"].ToString())
                            {
                                dtPrint.Rows[i]["DACOD"] = strIndat;
                                //刷入数量为0，即第一次刷入料号时
                                if (Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) == 0 || dtPrint.Rows[i]["SCQTY"].ToString() == "")
                                {
                                    dtPrint.Rows[i]["SCQTY"] = strQty;   //获得刷入数量放在SCQTY栏位中
                                    dtPrint.Rows[i]["DIFQTY"] =     //获得差异数量放在DIFQTY栏位中
                                        Convert.ToInt32(dtPrint.Rows[i]["MENGE"].ToString()) -
                                        Convert.ToInt32(strQty);
                                }
                                //当dtStorage刷入数量不为0，即已多次刷入时
                                else if (Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) != 0 || dtPrint.Rows[i]["SCQTY"].ToString() != "")
                                {
                                    dtPrint.Rows[i]["SCQTY"] = Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                    dtPrint.Rows[i]["DIFQTY"] =     //获得新差异数量
                                        Convert.ToInt32(dtPrint.Rows[i]["MENGE"].ToString()) -
                                        Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]);
                                }
                            }
                        }

                        drRow = Data.NewRow();
                        drRow["MANDT"] = dtTemp.Rows[0]["MANDT"].ToString();
                        drRow["COMCD"] = dtTemp.Rows[0]["COMCD"].ToString();
                        drRow["WERKS"] = dtTemp.Rows[0]["WERKS"].ToString();
                        drRow["LGORT"] = dtTemp.Rows[0]["LGORT"].ToString();
                        drRow["LOCAT"] = dtTemp.Rows[0]["LOCAT"].ToString();
                        drRow["MATNR"] = dtTemp.Rows[0]["MATNR"].ToString();
                        drRow["INSMK"] = dtTemp.Rows[0]["INSMK"].ToString();
                        drRow["CHARG"] = dtTemp.Rows[0]["CHARG"].ToString();
                        drRow["LIFNR"] = strLifnr;
                        drRow["MENGE"] = dtTemp.Rows[0]["MENGE"].ToString();
                        drRow["SCQTY"] = dtTemp.Rows[0]["SCQTY"].ToString();
                        drRow["DIFQTY"] = dtTemp.Rows[0]["DIFQTY"].ToString();
                        drRow["BKQTY"] = dtTemp.Rows[0]["BKQTY"].ToString();
                        drRow["MBLNR"] = dtTemp.Rows[0]["MBLNR"].ToString();
                        drRow["INDAT"] = strIndat;
                        drRow["LOCOD"] = strLocod;
                        drRow["KDMAT"] = dtTemp.Rows[0]["KDMAT"].ToString();
                        drRow["RMANO"] = dtTemp.Rows[0]["RMANO"].ToString();
                        drRow["MAKTX"] = dtTemp.Rows[0]["MAKTX"].ToString();
                        drRow["PKDAT"] = dtTemp.Rows[0]["PKDAT"].ToString();
                        Data.Rows.Add(drRow);

                        this.txtScan.Text = "";
                        this.txtDidno.Text = "";
                        this.txtQty.Text = "" ;
                        ShowDataGridAll();
                        txtScan.Focus();

                        if (dtStorage.Select("LOCAT='" + strLocat + "'").Length == 0)
                        {
                            if (Data.Select("LOCAT ='" + strLocat + "'").Length == LocatCount)
                            {
                                this.txtLocat.Text = "";
                                this.txtLocat.SelectAll();
                                this.txtLocat.Focus();
                                LocatCount = 0;
                            }
                        }

                        this.btnPrint.Enabled = true;
                        this.btnDownload.Enabled = true;
                        Sound.Play(@"Sound\Success.wav");

                    }
                        
                }
                catch (Exception ex)
                {

                    Sound.Play(@"Sound\ERROR.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtScan.Text = "";
                    this.txtScan.Focus();
                    this.txtScan.SelectAll();
                    return;

                }
            }
        }

        #region ShowDataGridAll
        private void ShowDataGridAll()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 50;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 50;
                dgvData.Columns.Add(dgvcLgort);

                if (rdbScanDIDNO.Checked || rdbScanQRcode.Checked)
                {
                    DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                    dgvcLocat.DataPropertyName = "LOCAT";
                    dgvcLocat.HeaderText = "Location";
                    dgvcLocat.ReadOnly = true;
                    dgvcLocat.Width = 80;
                    dgvData.Columns.Add(dgvcLocat);
                }

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 90;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvData.Columns.Add(dgvcInsmk);

                //DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                //dgvcMblnr.DataPropertyName = "MBLNR";
                //dgvcMblnr.HeaderText = "Scan Code";
                //dgvcMblnr.ReadOnly = true;
                //dgvcMblnr.Width = 120;
                //dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcScqty = new DataGridViewTextBoxColumn();
                dgvcScqty.DataPropertyName = "SCQTY";
                dgvcScqty.HeaderText = "Scan Qty";
                dgvcScqty.ReadOnly = true;
                dgvData.Columns.Add(dgvcScqty);

                DataGridViewTextBoxColumn dgvcDifqty = new DataGridViewTextBoxColumn();
                dgvcDifqty.DataPropertyName = "DIFQTY";
                dgvcDifqty.HeaderText = "QWMS-Scan";
                dgvcDifqty.ReadOnly = true;
                dgvData.Columns.Add(dgvcDifqty);

                if (strLgort=="TW50")
                {
                    DataGridViewTextBoxColumn dgvcDacod = new DataGridViewTextBoxColumn();
                    dgvcDacod.DataPropertyName = "DACOD";
                    dgvcDacod.HeaderText = "DateCode";
                    dgvcDacod.ReadOnly = true;
                    dgvData.Columns.Add(dgvcDacod);
                }

                dgvData.DataSource = dtStorage;
                lblCount.Text = dtStorage.Rows.Count + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        # endregion

        //#region ShowDataGridDif
        //private void ShowDataGridDif()
        //{
        //    dgvData.AutoGenerateColumns = false;
        //    dgvData.Columns.Clear();
        //    try
        //    {
        //        DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
        //        dgvcWerks.DataPropertyName = "WERKS";
        //        dgvcWerks.HeaderText = "Plant";
        //        dgvcWerks.ReadOnly = true;
        //        dgvcWerks.Width = 50;
        //        dgvData.Columns.Add(dgvcWerks);

        //        DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
        //        dgvcLgort.DataPropertyName = "LGORT";
        //        dgvcLgort.HeaderText = "Storage";
        //        dgvcLgort.ReadOnly = true;
        //        dgvcLgort.Width = 50;
        //        dgvData.Columns.Add(dgvcLgort);

        //        DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
        //        dgvcLocat.DataPropertyName = "LOCAT";
        //        dgvcLocat.HeaderText = "Location";
        //        dgvcLocat.ReadOnly = true;
        //        dgvcLocat.Width = 90;
        //        dgvData.Columns.Add(dgvcLocat);

        //        DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
        //        dgvcMatnr.DataPropertyName = "MATNR";
        //        dgvcMatnr.HeaderText = "Part No";
        //        dgvcMatnr.ReadOnly = true;
        //        dgvcMatnr.Width = 90;
        //        dgvData.Columns.Add(dgvcMatnr);

        //        //料號說明欄位
        //        DataGridViewTextBoxColumn dgvcMaktx = new DataGridViewTextBoxColumn();
        //        dgvcMaktx.DataPropertyName = "MAKTX";
        //        dgvcMaktx.HeaderText = "Part# Description";
        //        dgvcMaktx.Width = 90;
        //        dgvcMaktx.ReadOnly = true;
        //        dgvData.Columns.Add(dgvcMaktx);

        //        DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
        //        dgvcInsmk.DataPropertyName = "INSMK";
        //        dgvcInsmk.HeaderText = "Stock";
        //        dgvcInsmk.ReadOnly = true;
        //        dgvcInsmk.Width = 50;
        //        dgvData.Columns.Add(dgvcInsmk);

        //        DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
        //        dgvcCharg.DataPropertyName = "CHARG";
        //        dgvcCharg.HeaderText = "Version";
        //        dgvcCharg.ReadOnly = true;
        //        dgvcCharg.Width = 60;
        //        dgvData.Columns.Add(dgvcCharg);

        //        //DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
        //        //dgvcMblnr.DataPropertyName = "MBLNR";
        //        //dgvcMblnr.HeaderText = "Scan Code";
        //        //dgvcMblnr.ReadOnly = true;
        //        //dgvcMblnr.Width = 120;
        //        //dgvData.Columns.Add(dgvcMblnr);

        //        DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
        //        dgvcMenge.DataPropertyName = "MENGE";
        //        dgvcMenge.HeaderText = "Qty";
        //        dgvcMenge.ReadOnly = true;
        //        dgvData.Columns.Add(dgvcMenge);

        //        DataGridViewTextBoxColumn dgvcScqty = new DataGridViewTextBoxColumn();
        //        dgvcScqty.DataPropertyName = "SCQTY";
        //        dgvcScqty.HeaderText = "Scan Qty";
        //        dgvcScqty.ReadOnly = true;
        //        dgvData.Columns.Add(dgvcScqty);

        //        DataGridViewTextBoxColumn dgvcDifqty = new DataGridViewTextBoxColumn();
        //        dgvcDifqty.DataPropertyName = "DIFQTY";
        //        dgvcDifqty.HeaderText = "QWMS-Scan";
        //        dgvcDifqty.ReadOnly = true;
        //        dgvData.Columns.Add(dgvcDifqty);

        //        DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
        //        dgvcIndat.DataPropertyName = "INDAT";
        //        dgvcIndat.HeaderText = "Store In Date";
        //        dgvcIndat.ReadOnly = true;
        //        dgvData.Columns.Add(dgvcIndat);

        //        dgvData.DataSource = Data2;
        //        lblCount.Text = dtData.Rows.Count + " records";

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowDataGrid()");

        //    }
        //}
        //# endregion

        private void btnAdjust_Click(object sender, EventArgs e)
        {
            DataTable dtAdjust = new DataTable();
            objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                                CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);

            dtAdjust = objCounting.QueryLocationScanAdjustData(dtStorage);
            for (int i = dtStorage.Rows.Count - 1; i >= 0; i--)
            {
                if ( dtAdjust.Rows[i]["SCQTY"].ToString() != "")
                {
                    if (dtAdjust.Rows[i]["SCQTY"].ToString() == dtAdjust.Rows[i]["MENGE"].ToString())
                    {

                        dtAdjust.Rows.RemoveAt(i);
                    }
                }
            }
            dgvData.DataSource = dtAdjust;
            ShowDataGridAll();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string strLocat = "";
                stsWarning.Text = "";
                if (Data.Rows.Count == 0)
                {
                    stsWarning.Text = "No data to print!!";
                    return;
                }

                //Order by
                string strOrderBy = "WERKS, LGORT, LOCAT";
                Print = CommonInfo.SortDataTable(Print, strOrderBy);


                ReportPrint objReportPrint = new ReportPrint(UserData, "LOCATIONSCAN", Print);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
                
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    CountingResult2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        # region Export
        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);

                if (strLgort == "TW50")
                {
                    strLine = "Plant\tStorage\tLocation\tPart No\tStock\tQty\tScan Qty\tQWMS-Scan\tDate Code";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < Print.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += Print.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += Print.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += Print.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += Print.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += Print.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += Print.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += Print.Rows[i]["SCQTY"].ToString() + "\t";
                        strLine += Print.Rows[i]["DIFQTY"].ToString()+"\t";
                        strLine += Print.Rows[i]["DACOD"].ToString();
                        sw.WriteLine(strLine);
                    }
                }
                else
                {
                    strLine = "Plant\tStorage\tLocation\tPart No\tStock\tQty\tScan Qty\tQWMS-Scan";
                    sw.WriteLine(strLine);
                    //Reading data
                    for (int i = 0; i < Print.Rows.Count; i++)
                    {
                        strLine = "";
                        strLine += Print.Rows[i]["WERKS"].ToString() + "\t";
                        strLine += Print.Rows[i]["LGORT"].ToString() + "\t";
                        strLine += Print.Rows[i]["LOCAT"].ToString() + "\t";
                        strLine += Print.Rows[i]["MATNR"].ToString() + "\t";
                        strLine += Print.Rows[i]["INSMK"].ToString() + "\t";
                        strLine += Print.Rows[i]["MENGE"].ToString() + "\t";
                        strLine += Print.Rows[i]["SCQTY"].ToString() + "\t";
                        strLine += Print.Rows[i]["DIFQTY"].ToString();
                        sw.WriteLine(strLine);
                    }
                }        
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }

        }
        # endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定退出？", "Caution", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定刷新？", "Caution", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                stsWarning.Text = "";
                this.txtLocat.Enabled = false;
                this.txtDidno.Enabled = false;
                this.txtQty.Enabled = false;
                this.txtScan.Enabled = false;
                this.dgvData.DataSource = null;
                this.dtData.Rows.Clear();
                this.dtStorage.Clear();
                this.rdbScanDIDNO.Checked = true;
                this.txtFromLocat.Text = "";
                this.txtToLocat.Text = "";
                this.txtScan.Text = "";
                this.txtLocat.Text = "";
                this.txtDidno.Text = "";
                this.txtQty.Text = "";
                this.lblCount.Text = "0 records";
                this.btnConfirm.Enabled = true;
                this.btnImport.Enabled = false;
                this.btnPrint.Enabled = false;
                this.btnDownload.Enabled = false;
                this.txtFromLocat.Enabled = true;
                this.txtToLocat.Enabled = true;
            }            
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确定退出？", "Caution", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Dispose();
                this.Close();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void rdbScanDIDNO_Click(object sender, EventArgs e)
        {
                this.txtDidno.Enabled = true;
                this.txtScan.Enabled  = false;
                this.txtQty.Enabled = true;
                this.txtPalletID.Enabled = false;
                this.txtFilePath.Enabled = false;
                this.txtDidno.Focus();
        }

        private void rdbScanQRcode_Click(object sender, EventArgs e)
        {
                this.txtDidno.Enabled = false;
                this.txtScan.Enabled = true;
                this.txtQty.Enabled = false;
                this.txtPalletID.Enabled = false;
                this.txtFilePath.Enabled = false;
                this.txtScan.Focus();
        }
        
        private void rdbScanPalletID_Click(object sender, EventArgs e)
        {
            this.txtDidno.Enabled = false;
            this.txtScan.Enabled = false;
            this.txtQty.Enabled = false;
            this.txtPalletID.Enabled = true;
            this.txtFilePath.Enabled = true;
            this.btnConfirm.Enabled = false;
            this.btnImport.Enabled = true;
            this.btnFile.Enabled = true;
            this.txtPalletID.Focus();
        }

        #region 是否為數字
        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }
        #endregion

        private void btnImport_Click(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            try
            {
                if (this.txtFilePath.Text.Trim() == "")
                {
                    stsWarning.Text = "Please select one file!!";
                    return;
                }
                ProcessContrastFiles(this.txtFilePath.Text.Trim());
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                this.panel1.Enabled = true;
                this.btnImport.Enabled = true;
                return;
            }
        }

        public void ProcessContrastFiles(string strContrastFilePath)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //Parse file
                ParseContrastFiles(strContrastFilePath);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                throw new Exception(ex.Message + "<-ProcessContrastFiles()");
            }
        }

        public void ParseContrastFiles(string varFileName)
        {

            string strFileLine = "";
            int intLineNum = 0;
            string[] aryData;
            DataRow drRow;
            DataTable dtSource = new DataTable();

            //File Data
            string strWerks = "";
            string strLgort = "";
            string strComcd = "";
            string strMatnr = "";
            string strCharg = "";
            string strMenge_0 = "";
            string strMenge_G = "";
            string strMenge_J = "";
            string strMenge_S = "";

            try
            {

                srFileReader = File.OpenText(varFileName);
                srFileReader = File.OpenText(varFileName);
                dtSource = Data.Clone();
                while (srFileReader.Peek() != -1)
                {
                    intLineNum++;
                    strFileLine = srFileReader.ReadLine();

                    if (strFileLine.Length > 0)
                    {
                        aryData = strFileLine.Split(new char[] { ',' });
                        strMandt = aryData[0].Trim();
                        strComcd = UserData.CompanyCode.ToString().Trim();
                        strWerks = aryData[1].Trim();
                        strLgort = aryData[2].Trim();
                        strMatnr = aryData[3].Trim();
                        strMenge_0 = aryData[4].Trim();
                        strMenge_G = aryData[5].Trim();
                        strMenge_J = aryData[6].Trim();
                        strMenge_S = aryData[7].Trim();
                        strCharg = aryData[8].Trim();

                        drRow = dtSource.NewRow();
                        drRow["MANDT"] = strMandt;
                        drRow["WERKS"] = strWerks;
                        drRow["LGORT"] = strLgort;
                        drRow["MATNR"] = strMatnr;
                        drRow["MENGE"] = strMenge_G;
                        dtSource.Rows.Add(drRow);
                    }
                }
                dtStorage = dtSource.Copy();
                for (int i = 0; i < dtStorage.Rows.Count; i++)
                {
                    dtStorage.Rows[i]["SCQTY"] = 0;
                    dtStorage.Rows[i]["DIFQTY"] = 0;
                }
                dtPrint = dtStorage.Copy();
                ShowDataGridAll();
                this.btnImport.Enabled = false;
                this.txtFromLocat.Enabled = false;
                this.txtToLocat.Enabled = false;
                srFileReader.Close();
            }
            catch (Exception ex)
            {
                //Move to error folder
                srFileReader.Close();
                throw new Exception(ex.Message + "<-ParseContrastFiles()");
            }

        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            string strWerks = "";
            string strLgort = "";
            stsWarning.Text = "";
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "Plant and storage can't be empty!!";
                return;
            }
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = ofdOpenFile.FileName;
            }
        }

        private void txtPalletID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    strMandt = Mandt;
                    strComcd = Comcd;
                    Werks = this.cmbWerks.Text.ToString();
                    Lgort = this.cmbLgort.Text.ToString();
                    strLocat = this.txtLocat.Text.ToString();
                    strInsmk = this.cmbInsmk.Text.ToString();
                    strPalletID = "";
                    strPalletID = this.txtPalletID.Text.ToString().Trim();
                    string strTempMblnrMatnr = "";
                    dtTemp= new DataTable();
                    DataRow drRow;
                    if (strPalletID == "")
                    {
                        this.txtDidno.Focus();
                        this.txtDidno.SelectAll();
                        throw new Exception("Pallet ID can't be empty!!");
                    }
                   
                    objCounting = new Counting(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode,
                                CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks, strLgort, Progid);
                    dtTemp = objCounting.QueryDIDPIDData(strLocat, strPalletID, strInsmk, "PALID");
                    dtTemp.Columns.Add("SCQTY");
                    dtTemp.Columns.Add("DIFQTY");
                    
                    if (dtTemp.Rows.Count == 0)   
                    {
                        this.txtPalletID.Focus();
                        this.txtPalletID.SelectAll();
                        throw new Exception("No Data!!");
                    }
                    //if (dtTemp.Rows.Count > 1)
                    //{
                    //    this.txtScan.Focus();
                    //    this.txtScan.SelectAll();
                    //    throw new Exception("此一维码对应了多个料号!!");
                    //}
                    dtTemp.Rows[0]["SCQTY"] = dtTemp.Rows[0]["MENGE"].ToString();

                    if (strLgort != "TW50")
                    {
                        for (int i = 0; i < Data.Rows.Count; i++)
                        {
                            if (strTempMblnrMatnr.IndexOf(Data.Rows[i]["MBLNR"].ToString() + ";") ==
                                -1)
                            {
                                strTempMblnrMatnr += Data.Rows[i]["MBLNR"].ToString() + ";";
                            }
                        }
                        if (strTempMblnrMatnr.IndexOf(strPalletID + ";") != -1)
                        {
                            this.txtPalletID.Focus();
                            this.txtPalletID.SelectAll();
                            throw new Exception("The data you input is duplicate in the location!!");
                        }
                    }

                    for (int i = dtStorage.Rows.Count-1; i >= 0; i--)
                    {
                        //刷入的料号与查出的料号匹配时
                        if (dtStorage.Rows[i]["MATNR"].ToString() == dtTemp.Rows[0]["MATNR"].ToString())
                        {
                            //刷入数量为0，即第一次刷入料号时
                            if (Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) == 0||dtStorage.Rows[i]["SCQTY"].ToString()=="")
                            {
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) <
                                    Convert.ToInt32(dtTemp.Rows[0]["SCQTY"]))
                                {
                                    dtStorage.Rows[i]["SCQTY"] = dtTemp.Rows[0]["SCQTY"].ToString();   //获得刷入数量放在SCQTY栏位中
                                    dtStorage.Rows[i]["DIFQTY"] =     //获得差异数量放在DIFQTY栏位中
                                        Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -
                                        Convert.ToInt32(dtTemp.Rows[0]["SCQTY"].ToString());
                                }
                                //当库存数量大于刷入数量
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) >
                                    Convert.ToInt32(dtTemp.Rows[0]["SCQTY"]))
                                {
                                    dtStorage.Rows[i]["SCQTY"] = dtTemp.Rows[0]["SCQTY"].ToString();   //获得刷入数量放在SCQTY栏位中
                                    dtStorage.Rows[i]["DIFQTY"] =     //获得差异数量放在DIFQTY栏位中
                                        Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -
                                        Convert.ToInt32(dtTemp.Rows[0]["SCQTY"].ToString());
                                }
                                //当库存数量与刷入数量相等时删除该条明细
                                if (dtStorage.Rows[i]["MENGE"].ToString() == dtTemp.Rows[0]["SCQTY"].ToString())
                                {
                                    dtStorage.Rows.RemoveAt(i);
                                }
                            }
                            //当dtStorage刷入数量不为0，即已多次刷入时
                            else if (Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) != 0 || dtStorage.Rows[i]["SCQTY"].ToString() != "")
                            {
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) <
                                    Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                {
                                    dtStorage.Rows[i]["SCQTY"] = Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                    dtStorage.Rows[i]["DIFQTY"] =     //获得新差异数量
                                        Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -
                                        Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);
                                }
                                //当库存数量仍大于刷入数量时
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) >
                                    Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                {
                                    dtStorage.Rows[i]["SCQTY"] =Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                    dtStorage.Rows[i]["DIFQTY"] =     //获得新差异数量
                                        Convert.ToInt32(dtStorage.Rows[i]["MENGE"].ToString()) -
                                        Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]);
                                }
                                //消失库存数量与刷入数量相等的明细
                                if (Convert.ToInt32(dtStorage.Rows[i]["MENGE"]) ==
                                    Convert.ToInt32(dtStorage.Rows[i]["SCQTY"]))
                                {
                                    dtStorage.Rows.RemoveAt(i);
                                }
                            }
                        }
                    }

                    for (int i = dtPrint.Rows.Count - 1; i >= 0; i--)
                    {
                        //刷入的料号与查出的料号匹配时
                        if (dtPrint.Rows[i]["LOCAT"].ToString() == dtTemp.Rows[0]["LOCAT"].ToString() && dtPrint.Rows[i]["MATNR"].ToString() == dtTemp.Rows[0]["MATNR"].ToString())
                        {
                            //刷入数量为0，即第一次刷入料号时
                            if (Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) == 0 || dtPrint.Rows[i]["SCQTY"].ToString() == "")
                            {
                                dtPrint.Rows[i]["SCQTY"] = strQty;   //获得刷入数量放在SCQTY栏位中
                                dtPrint.Rows[i]["DIFQTY"] =     //获得差异数量放在DIFQTY栏位中
                                    Convert.ToInt32(dtPrint.Rows[i]["MENGE"].ToString()) -
                                    Convert.ToInt32(strQty);
                            }
                            //当dtStorage刷入数量不为0，即已多次刷入时
                            else if (Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) != 0 || dtPrint.Rows[i]["SCQTY"].ToString() != "")
                            {
                                dtPrint.Rows[i]["SCQTY"] = Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]) + Convert.ToInt32(strQty); //获得总刷入数量
                                dtPrint.Rows[i]["DIFQTY"] =     //获得新差异数量
                                    Convert.ToInt32(dtPrint.Rows[i]["MENGE"].ToString()) -
                                    Convert.ToInt32(dtPrint.Rows[i]["SCQTY"]);
                            }
                        }
                    }

                    drRow = Data.NewRow();
                    drRow["MANDT"] = dtTemp.Rows[0]["MANDT"].ToString();
                    drRow["COMCD"] = dtTemp.Rows[0]["COMCD"].ToString();
                    drRow["WERKS"] = dtTemp.Rows[0]["WERKS"].ToString();
                    drRow["LGORT"] = dtTemp.Rows[0]["LGORT"].ToString();
                    drRow["LOCAT"] = dtTemp.Rows[0]["LOCAT"].ToString();
                    drRow["MATNR"] = dtTemp.Rows[0]["MATNR"].ToString();
                    drRow["INSMK"] = dtTemp.Rows[0]["INSMK"].ToString();
                    drRow["CHARG"] = dtTemp.Rows[0]["CHARG"].ToString();
                    drRow["LIFNR"] = dtTemp.Rows[0]["LIFNR"].ToString();
                    drRow["MENGE"] = dtTemp.Rows[0]["MENGE"].ToString();
                    drRow["SCQTY"] = dtTemp.Rows[0]["SCQTY"].ToString();
                    drRow["DIFQTY"] = dtTemp.Rows[0]["DIFQTY"].ToString();
                    drRow["BKQTY"] = dtTemp.Rows[0]["BKQTY"].ToString();
                    drRow["MBLNR"] = strPalletID;
                    drRow["INDAT"] = dtTemp.Rows[0]["INDAT"].ToString();
                    drRow["KDMAT"] = dtTemp.Rows[0]["KDMAT"].ToString();
                    drRow["RMANO"] = dtTemp.Rows[0]["RMANO"].ToString();
                    drRow["MAKTX"] = dtTemp.Rows[0]["MAKTX"].ToString();
                    drRow["PKDAT"] = dtTemp.Rows[0]["PKDAT"].ToString();
                    Data.Rows.Add(drRow);

                    this.txtPalletID.Text = "";
                    ShowDataGridAll();
                    txtPalletID.Focus();

                    this.btnPrint.Enabled = true;
                    this.btnDownload.Enabled = true;
                    Sound.Play(@"Sound\Success.wav");
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\ERROR.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtDidno.Text = "";
                    this.txtQty.Text = "";
                    this.txtDidno.Focus();
                    this.txtDidno.SelectAll();
                    return;
                }
            }
        }
    }
}
