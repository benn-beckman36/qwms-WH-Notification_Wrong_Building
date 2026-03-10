using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class StorageIn_SMT_MultiOnLineIn : Form
    {
        #region 变量
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;
        private string strLocat = string.Empty;
        private string strMatnr = string.Empty;
        private string strType = string.Empty;
        private string strMrgid = string.Empty;
        private string strInsmk = string.Empty;
        private string strCharg = string.Empty;
        private string strSttyp = string.Empty;
        private string strLotyp = string.Empty;
        private string strCurrentRefid = string.Empty;  //当前操作的REFID数据
        private List<string> lsRefID = new List<string>();
        private string strCurrentDid = string.Empty;//当前操作的DID数据
        private string strVedat = string.Empty;
        private bool AllowToClose = true;
        private int intFormIndex = 0;
        private DataTable dtData = new DataTable(); //待入库数据
        private DataTable dtTmpCheckDID = new DataTable(); //存放刷入的当前DID数据
        private QCI.QWMS.PlantData objPlantData;
        private QCI.QWMS.StorageData objStorageData;
        #endregion

        #region  Get/Set
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

        #endregion

        public StorageIn_SMT_MultiOnLineIn(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            btnERRO.Visible = false;
            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                objPlantData = new QCI.QWMS.PlantData(UserData);
                objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData);

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
                    initStorageInData();
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

        #region ShowData
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
                if (strWerks == "CS12")
                {
                    btnERRO.Visible = true;
                }
                else
                {
                    btnERRO.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void initStorageInData()
        {
            stsWarning.Text = string.Empty;
            if (dtData.Columns.Count == 0)
            {
                dtData.Columns.Add("MANDT");
                dtData.Columns.Add("COMCD");
                dtData.Columns.Add("WERKS");
                dtData.Columns.Add("LGORT");
                dtData.Columns.Add("LOCAT");
                dtData.Columns.Add("MATNR");
                dtData.Columns.Add("INSMK");
                dtData.Columns.Add("CHARG");
                dtData.Columns.Add("MENGE");
                dtData.Columns.Add("ALQTY");
                dtData.Columns.Add("MBLNR");
                dtData.Columns.Add("ZEILE");
                dtData.Columns.Add("EBELN");
                dtData.Columns.Add("LIFNR");
                dtData.Columns.Add("INSPT");
                dtData.Columns.Add("LOCOD");
                dtData.Columns.Add("SERNO");
                dtData.Columns.Add("OMBLNR");
                dtData.Columns.Add("MRGID");
                dtData.Columns.Add("KOSTL");
                dtData.Columns.Add("ARBPL");
                dtData.Columns.Add("TRNTP");
                dtData.Columns.Add("RMAK1");
                dtData.Columns.Add("INDAT");
                //新增厂商生产日期
                dtData.Columns.Add("VEDAT");
                dtData.Columns.Add("REFID");
                dtData.Columns.Add("OTLGT");
                dtData.Columns.Add("DACOD");
                // 新增保存期
                dtData.Columns.Add("EXPDAT");
                dtData.Columns.Add("TASKID");
                dtData.Columns.Add("MAXEXP");

            }
        }

        #endregion

        #region SelectChange
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion
        private void btnQueryRefid_Click(object sender, EventArgs e)
        {
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

            if (dtData.Rows.Count == 0)
            {
                MessageBox.Show("Please insert data first!");
            }
            else
            {
                string strRefID = dtData.Rows[0]["REFID"].ToString();
                DataTable dtRefID = new DataTable();

                dtRefID = objPlantData.GetRefIDData(this.strMandt, this.Comcd, this.strWerks, this.strLgort, strRefID);
                StorageIn_SMT_Query_RefID objStorageIn_SMT_Query_RefID = new StorageIn_SMT_Query_RefID(dtRefID, dtData, "SMT", UserData);
                objStorageIn_SMT_Query_RefID.Show();
            }
        }

        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            try
            {
                if (cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    Manage_LocationSelect_New objManage_LocationSelect_New = new Manage_LocationSelect_New(UserData, Progid, Werks, Lgort, Type);
                    objManage_LocationSelect_New.ShowDialog();
                    txtLocat.Text = objManage_LocationSelect_New.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void txtLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyChar == (char)13)
            {
                KeyInLocat();
            }
        }

        private void KeyInLocat()
        {
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

            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
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

            cmbWerks.Enabled = false;
            cmbLgort.Enabled = false;
            txtLocat.Enabled = false;
            txtDidNo.Enabled = true;
            txtDidNo.Focus();
            //gbHeader.Enabled = false;
        }

        private void txtDidNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            stsWarning.Text = string.Empty;
            if (e.KeyChar == (char)13)
            {
                if (txtDidNo.Text.ToString().Equals("OK"))
                {
                    txtLocat.Enabled = true;
                    txtLocat.Text = string.Empty;
                    txtLocat.Focus();
                    txtDidNo.Text = string.Empty;
                    txtDidNo.Enabled = false;
                    return;
                }
                else
                {
                    try
                    {
                        //gbHeader.Enabled = false;
                        strCurrentDid = txtDidNo.Text.ToString().Trim().ToUpper();
                        txtDidNo.Text = string.Empty;

                        #region 判断刷入的DID是否为重复刷入
                        if(dtData.Rows.Count>0)
                        {
                            List<string> ls = (from dr in dtData.AsEnumerable()
                                               where dr.Field<string>("MBLNR") == strCurrentDid
                                               select dr.Field<string>("MBLNR")).ToList();
                            if (ls.Count > 0)
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                MessageBox.Show("该DidNo数据已刷入");
                                return;
                            }
                        }
                        #endregion

                        #region 检查DID是否存在
                        dtTmpCheckDID = objPlantData.GetDIDInfo(this.Mandt, this.Comcd, this.Werks, this.Lgort, "", strCurrentDid);
                        if (dtTmpCheckDID.Rows.Count <= 0)
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            MessageBox.Show(strCurrentDid + " doesn't exist!!");
                            return;
                        }
                        else
                        {
                            if (dtTmpCheckDID.Rows[0]["MENGE"].ToString().Trim() == dtTmpCheckDID.Rows[0]["OTQTY"].ToString().Trim())
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                MessageBox.Show(strCurrentDid + " has been processed!! \n Please check it!!");
                                return;
                            }
                        }
                        #endregion

                        #region 判断当前刷入的DID数据是否是属于当前REFID，若不是，判断当前REFID数据是否已经刷完
                        if (string.IsNullOrEmpty(strCurrentRefid)) //第一次刷入DID信息
                        {
                            strCurrentRefid = dtTmpCheckDID.Rows[0]["REFID"].ToString().Trim();
                        }
                        else
                        {
                            //刷入信息和当前REFID信息不等时，判断REFID信息是否已全部刷入，若全部刷入，则更新当前REID为新的REFID，否则，提示当前REFID信息还未完全刷入
                            if (strCurrentRefid != dtTmpCheckDID.Rows[0]["REFID"].ToString().Trim())
                            {
                                if (CompareRefIDData(strCurrentRefid))
                                {
                                    strCurrentRefid = dtTmpCheckDID.Rows[0]["REFID"].ToString().Trim(); //更新当前操作的REFID信息
                                    #region 判断储位是否有更新
                                    if (strLocat == dtData.Rows[0]["LOCAT"].ToString())
                                    {
                                        Sound.Play(@"Sound\ERROR.wav");
                                        MessageBox.Show("不同REFID不能再同一储位，请更换储位，谢谢！");
                                        txtDidNo.Text = string.Empty;
                                        txtDidNo.Enabled = false;
                                        txtLocat.Enabled = true;
                                        txtLocat.Text = string.Empty;
                                        txtLocat.Focus();
                                        return;
                                    }
                                    #endregion
                                }
                                else
                                    return;
                            }
                        }
                        #endregion

                        #region 储位提醒 DateCode提醒

                        //判断库存中是否存在同储位同料号数据
                        DataTable dtLocMatVer = new DataTable();
                        string strlocat = this.txtLocat.Text.Trim();
                        string strwerks = this.cmbWerks.Text.Trim();
                        string strlgort = this.cmbLgort.Text.Trim();
                        string strMatnr = dtTmpCheckDID.Rows[0]["MATNR"].ToString();              
                        dtLocMatVer = objStorageData.CheckDacod(strlocat, strMatnr, strwerks, strlgort);//先判断料号是否存在
                        if (int.Parse(dtLocMatVer.Rows[0][0].ToString()) > 0)
                        {                          
                            MessageBox.Show("该储位:" + txtLocat.Text + "有与该DID相同料号的库存,【不允许入库到该储位】,请更换储位！");
                            return;
                        }
                        #endregion


                        #region PCB材料不同版本不允许入库

                        string strCharg = dtTmpCheckDID.Rows[0]["CHARG"].ToString();
                        //string strVendor= dtTmpCheckDID.Rows[0]["LIFNR"].ToString();
                        if (objPlantData.CheckCHARGLGORT(Werks))
                        {
                            //PCB料号
                            if (strMatnr.Substring(0, 2) == "SA" || strMatnr.Substring(0, 2) == "DA")
                            {
                                if (objStorageData.CheckExistedDifferentCHARG(strLocat, strMatnr, strCharg))
                                {
                                    MessageBox.Show("该储位:" + txtLocat.Text + "有与该DID不同版本的库存,【不允许入库到该储位】,请更换储位！");
                                    return;
                                }                             
                            }                           
                        }
                        #endregion

                        #region Lot Code不同Lot Code不允许入库 Lot code管控仓别

                        string strLocod = dtTmpCheckDID.Rows[0]["LOCOD"].ToString();
                        if (objPlantData.CheckLOCODLGORT(Werks, Lgort))
                        {

                            if (objStorageData.CheckExistedDifferentLOCAD(strLocat, strMatnr, strLocod))
                            {
                                MessageBox.Show("该储位:" + txtLocat.Text + "有与该DID不同Lot Code的库存,【不允许入库到该储位】,请更换储位！");
                                return;
                            }
                            
                        }
                        #endregion

                        //DateCode管控 判断是否DateCode转换
                        //厂商生产日期进行转换
                        AddNewDidNo(strCurrentDid);
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = ex.Message;
                        return;
                    }
                }
                SetbtnSaveException();
            }
        }
        private bool CompareRefIDData(string strRefID)
        {
            #region 判斷1.刷條碼的數據與QSMS給的一致,2.DID Item數量、項目一致 by Rock Tzeng

            DataTable dtQsmsData = objPlantData.GetRefIDData(this.Mandt, this.Comcd, this.Werks, this.Lgort, strRefID);
            DataTable dtCurrentData = dtData.Select("REFID='" + strRefID + "'").CopyToDataTable();

            if (dtQsmsData.Rows.Count != dtCurrentData.Rows.Count)
            {
                MessageBox.Show("当前REFID处理笔数与QSMS给的不一致!!请确认是否有遗漏或多刷资料!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetbtnSaveException();
                return false;
            }
            DataTable dtTmpSort;
            string strSort = "";
            DataRow[] arrdrTmpSort;

            #region Sort dtCurrentData(處理中的資料)
            dtTmpSort = dtCurrentData.Clone();
            strSort = "REFID,MBLNR";
            arrdrTmpSort = dtCurrentData.Select("", strSort);
            for (int i = 0; i < arrdrTmpSort.Length; i++)
            {
                dtTmpSort.Rows.Add(arrdrTmpSort[i].ItemArray);
            }
            dtCurrentData = dtTmpSort;
            #endregion

            #region Sort dtQsmsData(QSMS給的資料)
            dtTmpSort = dtQsmsData.Clone();
            strSort = "REFID,DIDNO";
            arrdrTmpSort = dtQsmsData.Select("", strSort);
            for (int i = 0; i < arrdrTmpSort.Length; i++)
            {
                dtTmpSort.Rows.Add(arrdrTmpSort[i].ItemArray);
            }
            dtQsmsData = dtTmpSort;
            #endregion

            for (int i = 0; i < dtCurrentData.Rows.Count; i++)
            {
                if (dtCurrentData.Rows[i]["REFID"].ToString().Trim().ToUpper() != dtQsmsData.Rows[i]["REFID"].ToString().Trim().ToUpper())
                {
                    MessageBox.Show("REFID '" + dtCurrentData.Rows[i]["REFID"].ToString() + "'  doesn't match!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return false;
                }
                if (dtCurrentData.Rows[i]["MBLNR"].ToString().Trim().ToUpper() != dtQsmsData.Rows[i]["DIDNO"].ToString().Trim().ToUpper())
                {
                    MessageBox.Show("DID No. '" + dtCurrentData.Rows[i]["MBLNR"].ToString() + "'  doesn't match!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return false;
                }
                if (dtCurrentData.Rows[i]["ALQTY"].ToString().Trim().ToUpper() != dtQsmsData.Rows[i]["MENGE"].ToString().Trim().ToUpper())
                {
                    MessageBox.Show("DID No. '" + dtCurrentData.Rows[i]["MBLNR"].ToString() + "' Q'ty  doesn't match!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return false;
                }

                //檢查目前單據數量是否足夠
                int QTY_remain = 0;//剩餘可扣數量

                if (Convert.ToInt32(dtQsmsData.Rows[i]["MENGE"].ToString()) > Convert.ToInt32(dtQsmsData.Rows[i]["OTQTY"].ToString()))
                    QTY_remain = Convert.ToInt32(dtQsmsData.Rows[i]["MENGE"].ToString()) - Convert.ToInt32(dtQsmsData.Rows[i]["OTQTY"].ToString());

                if (QTY_remain < Convert.ToInt32(dtCurrentData.Rows[i]["ALQTY"].ToString()))
                {
                    MessageBox.Show("DID No. '" + dtCurrentData.Rows[i]["MBLNR"].ToString() + "' Q'ty  doesn't enough!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return false;
                }
            }
            return true;
            #endregion
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewDidNo(string strDidNo)
        {
            stsWarning.Text = string.Empty;
            try
            {
                #region DateCode 转换
                strVedat = string.Empty;
                //判断是否为DateCode管控仓,管控仓DateCode进行转换
                if (objPlantData.CheckDACODLGORT(Werks, Lgort))
                {
                    //根據Datecode帶出store in date
                    string DC_After = objStorageData.WHDCR_Query(dtTmpCheckDID.Rows[0]["LIFNR"].ToString().Trim(), dtTmpCheckDID.Rows[0]["DACOD"].ToString().Trim());

                    if (DC_After == "")
                    {
                        #region 确认是否要转化DateCode Rule
                        string strTemp = objStorageData.getDCTrans(dtTmpCheckDID.Rows[0]["LIFNR"].ToString().Trim(), dtTmpCheckDID.Rows[0]["DACOD"].ToString().Trim()).ToString();
                        if (!string.IsNullOrEmpty(strTemp))
                        {
                            //strDC = DateTime.Now.ToString("yyyyMMdd");
                            DataTable dtNewDateCode = new DataTable();
                            dtNewDateCode.Columns.Add("LIFNR");
                            dtNewDateCode.Columns.Add("DC_Before");
                            dtNewDateCode.Columns.Add("DC_After");

                            DataRow dr = dtNewDateCode.NewRow();
                            dr["LIFNR"] = dtTmpCheckDID.Rows[0]["LIFNR"].ToString().Trim();
                            dr["DC_Before"] = dtTmpCheckDID.Rows[0]["DACOD"].ToString().Trim();
                            dr["DC_After"] = strTemp;
                            dtNewDateCode.Rows.Add(dr.ItemArray);
                            objStorageData.WHDCR_DML(dtNewDateCode, "NEW", "System");
                            strVedat = Convert.ToDateTime(strTemp).ToString("yyyyMMdd");            
                        }
                        else
                        {
                            MessageBox.Show("无D/C转换信息找D/C管理人员处理");
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        DateTime dtTime = new DateTime();

                        if (DateTime.TryParse(DC_After, out dtTime))
                        {
                            strVedat = dtTime.ToString("yyyyMMdd");
                        }
                        else
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            MessageBox.Show("维护的DateCode转换数据格式不正确");
                            return;
                        }
                    }
                }
                #endregion

                Add();

                #region 已刷入的数据更换颜色
                ShowDataGrid();
                lsRefID = (from t in dtData.AsEnumerable() select t.Field<string>("REFID")).Distinct().ToList();
                if (lsRefID.Count < 2)
                    return;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (dgvData.Rows[i].Cells["REFID"].Value.ToString().ToUpper() == lsRefID.First().ToString())
                        dgvData.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                #endregion
            }
            catch(Exception ex)
            {
                Sound.Play(@"Sound\ERROR.wav");
                MessageBox.Show("添加DID数据失败，请联系QWMS负责人");
                return;
            }
        }

        #region CheckChanged
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            txtDidNo.Enabled = false;
            //gbFunction.Enabled = false;
            gbHeader.Enabled = true;
        }
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            txtDidNo.Enabled = false;
            //gbFunction.Enabled = false;
            gbHeader.Enabled = true;
        }
        #endregion

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = false;
                if (strWerks != "CS12")
                {
                    dgvcLocat.ReadOnly = true;
                }
                dgvcLocat.Name = "LOCAT";
                dgvcLocat.Width = 80;
                dgvData.Columns.Add(dgvcLocat);


                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "Referance ID.";
                dgvcRefid.Name = "REFID";
                dgvcRefid.ReadOnly = true;
                dgvcRefid.Width = 120;
                dgvData.Columns.Add(dgvcRefid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "DID No.";
                dgvcMblnr.Name = "MBLNR";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 160;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcOtlgt = new DataGridViewTextBoxColumn();
                dgvcOtlgt.DataPropertyName = "OTLGT";
                dgvcOtlgt.HeaderText = "Storage From.";
                dgvcOtlgt.ReadOnly = true;
                dgvcOtlgt.Width = 80;
                dgvData.Columns.Add(dgvcOtlgt);

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


                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 100;
                dgvData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 100;
                dgvData.Columns.Add(dgvcLifnr);

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
                dgvcIndat.Width = 100;
                dgvData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcInpst = new DataGridViewTextBoxColumn();
                dgvcInpst.DataPropertyName = "INSPT";
                dgvcInpst.HeaderText = "Inspection Lot No.";
                dgvcInpst.ReadOnly = true;
                dgvcInpst.Width = 100;
                dgvData.Columns.Add(dgvcInpst);

                DataGridViewTextBoxColumn dgvcLocod = new DataGridViewTextBoxColumn();
                dgvcLocod.DataPropertyName = "Locod";
                dgvcLocod.HeaderText = "Lock Code";
                dgvcLocod.ReadOnly = true;
                dgvcLocod.Width = 100;
                dgvData.Columns.Add(dgvcLocod);

                DataGridViewTextBoxColumn dgvcdDacod = new DataGridViewTextBoxColumn();
                dgvcdDacod.DataPropertyName = "Dacod";
                dgvcdDacod.HeaderText = "Date Code";
                dgvcdDacod.ReadOnly = true;
                dgvcdDacod.Width = 100;
                dgvData.Columns.Add(dgvcdDacod);

                DataGridViewTextBoxColumn dgvcdExpdat = new DataGridViewTextBoxColumn();
                dgvcdExpdat.DataPropertyName = "EXPDAT";
                dgvcdExpdat.HeaderText = "Expiry Date";
                dgvcdExpdat.ReadOnly = true;
                dgvcdExpdat.Width = 100;
                dgvData.Columns.Add(dgvcdExpdat);

                DataGridViewTextBoxColumn dgvcdTaskid = new DataGridViewTextBoxColumn();
                dgvcdTaskid.DataPropertyName = "TASKID";
                dgvcdTaskid.HeaderText = "Task ID";
                dgvcdTaskid.ReadOnly = true;
                dgvcdTaskid.Width = 100;
                dgvData.Columns.Add(dgvcdTaskid);

                DataGridViewTextBoxColumn dgvcdMaxexp = new DataGridViewTextBoxColumn();
                dgvcdMaxexp.DataPropertyName = "MAXEXP";
                dgvcdMaxexp.HeaderText = "Max Expiry Date";
                dgvcdMaxexp.ReadOnly = true;
                dgvcdMaxexp.Width = 100;
                dgvData.Columns.Add(dgvcdMaxexp);

                dgvData.DataSource = dtData;
                dgvData.FirstDisplayedScrollingRowIndex = dgvData.Rows.Count - 1;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cmbWerks.Enabled = true;
            cmbLgort.Enabled = true;
            cmbWerks.SelectedIndex = -1;
            cmbLgort.SelectedIndex = -1;
            strLocat = string.Empty;
            strCurrentRefid = string.Empty;
            strCurrentDid = string.Empty;
            lsRefID.Clear();
            dtData.Rows.Clear();
            dtTmpCheckDID.Rows.Clear();
            txtLocat.Enabled = true;
            txtLocat.Text = string.Empty;
            txtDidNo.Text = string.Empty;
            txtDidNo.Enabled = false;
            gbFunction.Enabled = true;
            gbHeader.Enabled = true;
            btnERRO.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort);
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
            QCI.QWMS.MixedMaterial objMixedMaterial = new QCI.QWMS.MixedMaterial(UserData, Werks);
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);

            #region 判断刷入数据是否完整
            foreach(string strCheckRefID in lsRefID)
            {
                if (!CompareRefIDData(strCheckRefID))
                    return;
            }

            #endregion


            #region QWMS原本的判斷式
            SetbtnSaveProcess();
            ArrayList aryMixedMaterial = new ArrayList();
            DataTable dtTemp = new DataTable();
            stsWarning.Text = "";
            if(dtData.Rows.Count == 0)
            {
                stsWarning.Text = "The data can't be empty!!";
                MessageBox.Show(stsWarning.Text,"QWMS System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                SetbtnSaveException();
                return;
            }

            #region Add check logic:dtData[0]["LOCAT"],["MATNR"],["CHARG"] compare with WHITM.LOCAT,MATNR,CHARG by Jack 20151123
            if (rdoAdd.Checked && Comcd == "9900" && ((Werks == "CS90" && (Lgort == "TW51" || Lgort == "TW10" || Lgort == "TW50")) || (Werks == "CS91" && (Lgort == "TW51")) || (Werks == "CS92" && (Lgort == "TW60" || Lgort == "TW70" || Lgort == "TW51"))))
            {
                DataTable dtLocMatVer = new DataTable();//reason Plant,Storage,Locat,Matnr,SELECT * FROM WHITM
                StringBuilder sbMatnrs = new StringBuilder();
                sbMatnrs.Append("'" + dtData.Rows[0]["MATNR"].ToString());
                for (int i = 1; i < dtData.Rows.Count; i++)
                {
                    sbMatnrs.Append("','" + dtData.Rows[i]["MATNR"]);
                }
                sbMatnrs.Append("'");
                //检查这几颗料在该仓别、储位是否有库存.
                dtLocMatVer = objStorageData.CheckLocatMatnrCharg(dtData.Rows[0]["LOCAT"].ToString(), sbMatnrs.ToString().Trim());
                //若有,则提示不允许入库.
                if (int.Parse(dtLocMatVer.Rows[0][0].ToString()) > 0)//have same P/N or Version stock,not allow in.
                {
                    DialogResult drResult = MessageBox.Show("该储位:" + txtLocat.Text + "有与该DID相同料号版本的库存,【不允许入库到该储位】,请确认是否继续入库?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (drResult == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            #endregion

            #region 个别仓别有特殊要求
            if ((strWerks == "CS32" && strLgort == "TW20") || (strWerks == "CS90" && (strLgort == "TW20" || strLgort == "TW70" || strLgort == "TW50")) || (strWerks == "CS91" && (strLgort == "TW20" || strLgort == "TW50" || strLgort == "TW51")) || (strWerks == "CS92" && (strLgort == "TW20" || strLgort == "TW51" || strLgort == "TW50")))
            {
                #region  SAM zhang 要求同料号入库不能放置在同一储位。 by blank
                if (dtData.Rows.Count >= 1)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        string strloc = dtData.Rows[i]["LOCAT"].ToString();
                        string strinsmk = dtData.Rows[i]["INSMK"].ToString();
                        string strmatnr = dtData.Rows[i]["MATNR"].ToString();
                        string strvander = dtData.Rows[i]["LIFNR"].ToString();
                        string strcharg = dtData.Rows[i]["CHARG"].ToString();
                        //判断库存中是否存在同储位同料号数据
                        bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                        if (bl)
                        {
                            stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许入储！";
                            return;
                        }

                    }
                }
                #endregion
            }
            else if ((strWerks == "CS12" && (strLgort == "TW60" || strLgort == "TW20" || strLgort == "TW11" || strLgort == "TW52" || strLgort == "TW53" || strLgort == "TW12")) || (strWerks == "CS20" && (strLgort == "TW22" || strLgort == "TW25" || strLgort == "TWEJ" || strLgort == "TW91" || strLgort == "TW31" || strLgort == "TW51" || strLgort == "TW15")) || (Comcd == "9200" && strWerks == "CS42" && (strLgort == "TW60" || strLgort == "TW61")))
            {
                #region  SAM zhang 要求同料号入库不能放置在同一储位。 by blank
                if (dtData.Rows.Count >= 1)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        string strloc = dtData.Rows[i]["LOCAT"].ToString();
                        string strinsmk = dtData.Rows[i]["INSMK"].ToString();
                        string strmatnr = dtData.Rows[i]["MATNR"].ToString();
                        string strvander = dtData.Rows[i]["LIFNR"].ToString();
                        string strcharg = dtData.Rows[i]["CHARG"].ToString();

                        //判断库存中是否存在同储位同料号数据
                        bool bl = objStorageData.CheckExistedSameMaterial(strloc, strmatnr);
                        if (bl)
                        {
                            stsWarning.Text = "料号：" + strmatnr + "在储位：" + strloc + "中已存在,不允许入储！";
                            return;
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region 检测保存期
            //foreach(DataRow dr in dtData.Rows) {
            //    if (!string.IsNullOrEmpty(dr["EXPDAT"].ToString())) {
            //        int expdate = int.Parse(dr["EXPDAT"].ToString());
            //        int maxexp = string.IsNullOrEmpty(dr["MAXEXP"].ToString()) ? 0 : int.Parse(dr["MAXEXP"].ToString());
            //        if (expdate > maxexp) {
            //            dr["MAXEXP"] = expdate.ToString();
            //        }
            //    }
            //}
            #endregion
            #endregion

            DataTable dtStorageByPatnum = new DataTable();
            DataTable dtSap = new DataTable();
            DataTable dtStorage = new DataTable();//入库数据
            DataTable dtSapInventory = new DataTable();
            StringBuilder sbRefID = new StringBuilder();
            List<string> lssucRefID = new List<string>();//成功扣账的REFID

            try
            {
                #region 只能有两个Reference ID
                lsRefID = (from t in dtData.AsEnumerable() select t.Field<string>("REFID")).Distinct().ToList();
                if (lsRefID.Count > 2)
                {
                    MessageBox.Show("You can not process more than two Reference ID!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return;
                }
                else
                {
                    foreach (string strRefID in lsRefID)
                    {
                        if (sbRefID.Length > 0)
                            sbRefID.Append("','" + strRefID);
                        else
                            sbRefID.Append(strRefID);
                    }
                }

                #endregion

                this.pbrProgressBar.Maximum = 10;
                this.pbrProgressBar.Value =0;
                this.pbrProgressBar.Visible = true;
                this.tmProgressTimer.Interval = 1;
                this.tmProgressTimer.Enabled = true;
                this.btnRefresh.Enabled = false;
                this.btnExit.Enabled = false;
                Application.DoEvents();

                #region SAP扣帳

                try
                {
                    dtSap = objPlantData.GetSAPDataByRefID(this.Mandt, this.Comcd, this.Werks, this.Lgort, sbRefID.ToString());
                    stsWarning.Text = "系統正在扣SAP帳中，請勿關閉視窗!!";
                    AllowToClose = false;//強制User無法關閉視窗
                    dtSap = StorageToSAP(dtSap);
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetbtnSaveException();
                    return;
                }
                #region 处理SAP回传的扣账数据
                StringBuilder sbMessage = new StringBuilder();
                foreach (string strRefID in lsRefID)
                {
                    DataTable dtReturn = dtSap.Select("REFNO='" + strRefID + "'").CopyToDataTable();
                    if(!string.IsNullOrEmpty( dtReturn.Rows[0]["OMBLN"].ToString()))  //SAP已扣账
                    {
                        DataRow[] drs = dtData.Select("REFID='" + strRefID + "'");
                        foreach(DataRow dr in drs)
                        {
                            dr["OMBLNR"] = dtReturn.Rows[0]["OMBLN"].ToString();
                            dr["RMAK1"] = strRefID;
                        }
                        lssucRefID.Add(strRefID);
                        sbMessage.AppendFormat("REFID:{0} 扣账成功，扣账编号为{1}\n", strRefID, dtReturn.Rows[0]["OMBLN"].ToString());
                    }
                    else
                    {
                        DataRow[] drs = dtReturn.Select("ERRMSG<>''");
                        sbMessage.AppendFormat("REFID:{0} 扣账失败，失败原因为{1}\n", strRefID, drs[0]["ERRMSG"].ToString());
                        //扣账失败更新颜色
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            if (dgvData.Rows[i].Cells["REFID"].Value.ToString().ToUpper() == strRefID)
                            {
                                dgvData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }
                        }
                        #region SAP扣账失败发送邮件提醒物料人员SAP调账
                        if (objPlantData.CheckSMTSendMail(Werks))
                            {
                                DataTable dtSendEmail = new DataTable();
                                dtSendEmail.Columns.Add("REFID", typeof(string));
                                dtSendEmail.Columns.Add("ERRMSG", typeof(string));
                                for (int j = 0; j < drs.Count(); j++)
                                {
                                    DataRow dr = dtSendEmail.NewRow();
                                    dr["REFID"] = strRefID;
                                    dr["ERRMSG"] = drs[j]["ERRMSG"].ToString();
                                    dtSendEmail.Rows.Add(dr);
                                }
                                if (SendMail(dtSendEmail))
                                {
                                    MessageBox.Show("SAP扣账失败，已邮件通知物料人员！");
                                }
                                else
                                {
                                    MessageBox.Show("SAP扣账失败，邮件通知物料人员发送失败！");
                                }
                                dtSendEmail.Clear();
                            }
                        #endregion
                    }
                }
                MessageBox.Show(sbMessage.ToString());
                stsWarning.Text = "SAP Posting OK!!";
                #endregion

                #endregion

                QCI.QWMS.StorageIn objStorageIn2 = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
                if (lssucRefID.Count == 0)
                    stsWarning.Text = "无数据可入库";
                else
                {
                    dtStorage = dtData.Clone();
                    foreach (string strRefID in lssucRefID)
                    {
                        dtStorage.Merge(dtData.Select("REFID='" + strRefID + "'").CopyToDataTable());
                    }
                }
                if (objStorageIn2.AddOnLineSMTInData(Mrgid, dtStorage))
                {
                    StringBuilder sbMessages = new StringBuilder();
                    foreach(string str in lssucRefID)
                    {
                        sbMessages.AppendFormat("{0}：入库成功\n", str);
                    }
                    MessageBox.Show(sbMessages.ToString());
                    AllowToClose = true;//扣帳成功，恢復可以關閉Form視窗
                    this.btnSave.Enabled = false;
                    this.tmProgressTimer.Enabled = false;
                    this.pbrProgressBar.Visible = false;
                    this.btnRefresh.Enabled = true;
                    this.btnExit.Enabled = true;

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

                        if (objInterface.PostStorageInData(dtASRS) == "SUCCESS" )
                        {
                            stsWarning.Text = "Add OK!!,数据已同步到ASRS";
                        }
                    }

                    #endregion
                    return;
                }
                else
                {
                    stsWarning.Text = "SAP Posting OK but QWMS Save Fail!!Reference id連線入庫作業失敗，SAP已扣帳但QWMS未扣帳，請使用SAP單據連線入庫!! ";
                    SetbtnSaveException();
                    MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.btnSave.Enabled = false;
                    this.tmProgressTimer.Enabled = false;
                    this.pbrProgressBar.Visible = false;
                    this.btnRefresh.Enabled = true;
                    this.btnExit.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                MessageBox.Show(stsWarning.Text, "QWMS System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetbtnSaveException();
                return;
            }
        }

        #region 设定btnSave
        private void SetbtnSaveProcess()
        {
            this.btnSave.Enabled = false;
        }

        private void SetbtnSaveException()
        {
            this.btnSave.Enabled = true;
            this.tmProgressTimer.Enabled = false;
            this.pbrProgressBar.Visible = false;
            this.btnRefresh.Enabled = true;
            this.btnExit.Enabled = true;
        }
        #endregion

        #region Add方法 处理刷入数据showdata
        private void Add()
        {
            try
            {
                #region 判断料号是否存在
                if (!objPlantData.CheckExistedMatnr(dtTmpCheckDID.Rows[0]["MATNR"].ToString().Trim()))
                {
                    MessageBox.Show(dtTmpCheckDID.Rows[0]["MATNR"].ToString().Trim() + " doesn't exist!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #endregion

                #region DYNAMIC LOCATION 判定 Insmk的值
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
                #endregion

                #region FIXED LOCATION 判定Insmk的值
                if (Lotyp.ToUpper() == "FIXED LOCATION")
                {
                    DataTable dtTemp1 = objStorageData.QueryLocatInsmk(Locat);
                    if (dtTemp1.Rows.Count > 1)
                    {
                        MessageBox.Show("The location has different stock and you can't add any new Part No!!");
                        return;
                    }
                    if (dtTemp1.Rows.Count > 0)
                    {
                        strInsmk = dtTemp1.Rows[0]["INSMK"].ToString();
                    }
                    DataRow[] foundRow = dtData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and WERKS='" + Werks + "' and LGORT='" + Lgort + "' and LOCAT='" + Locat + "'");
                    if (foundRow.Length > 0)
                    {
                        if (foundRow[0]["INSMK"].ToString() != strInsmk)
                        {
                            MessageBox.Show("You can't store different Part No with different stock in the location!!");
                            return;
                        }
                    }
                }
                #endregion

                #region add 9900-->CS90/TW51,TW10 & CS92/TW60,TW70 D/C control logic: whether the current DID is the same MATNR & DACOD as SapData.Rows[0]["DACOD"] by Jack 20151123
                if (dtData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (((Comcd == "9900" && ((Werks == "CS90" && (Lgort == "TW51" || Lgort == "TW10")) || (Werks == "CS92" && (Lgort == "TW60" || Lgort == "TW70")))) || (Comcd == "9100" && Werks == "CS12" && (Lgort == "TW52" || Lgort == "TW53" || Lgort == "TW12")) || (Comcd == "9200" && strWerks == "CS42" && (strLgort == "TW60" || strLgort == "TW61")))
                            && (dtData.Rows[i]["MATNR"].ToString() == dtTmpCheckDID.Rows[0]["MATNR"].ToString().Trim() && dtData.Rows[i]["DACOD"].ToString() != dtTmpCheckDID.Rows[0]["DACOD"].ToString().Trim()))
                        {
                            DialogResult drResult = MessageBox.Show("此笔DID与已刷入的【料号相同，D/C不同】,请确认是否继续刷入?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (drResult == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region add 9900-->CS90/TW51,TW10 & CS92/TW60,TW70 D/C control logic: whether the current DID is the same MATNR & DACOD as SapData.Rows[0]["DACOD"] by Jack 20151123
                if (dtData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if ((Comcd == "9200" && strWerks == "CS21" && (strLgort == "TW61" || strLgort == "TWDP" || strLgort == "TW50" || strLgort == "TW51" || strLgort == "TW60" || strLgort == "TW10" || strLgort == "TWDD" || strLgort == "TW11" || strLgort == "TW70"))
                            && (dtData.Rows[i]["MATNR"].ToString() == dtTmpCheckDID.Rows[0]["MATNR"].ToString().Trim() && dtData.Rows[i]["DACOD"].ToString() != dtTmpCheckDID.Rows[0]["DACOD"].ToString().Trim()))
                        {
                            DialogResult drResult = MessageBox.Show("此笔DID与已刷入的【料号相同，D/C不同】,请确认是否继续刷入?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (drResult == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region add 9200-->CS20/TW51  
                if (dtData.Rows.Count > 0)
                {
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if ((Comcd == "9200" && strWerks == "CS20" && (strLgort == "TW51") )
                            && (dtData.Rows[i]["MATNR"].ToString() == dtTmpCheckDID.Rows[0]["MATNR"].ToString().Trim() && dtData.Rows[i]["DACOD"].ToString() != dtTmpCheckDID.Rows[0]["DACOD"].ToString().Trim()))
                        {
                            if (txtLocat.Text.Trim().ToString() == dtData.Rows[i]["LOCAT"].ToString())
                            {
                                stsWarning.Text = "此笔DID与已刷入的【料号相同，D/C不同】,请更改储位！";
                                return;
                            }
                        }
                    }
                }
                #endregion


                #region 添加dtTmpCheckDID 数据到dtData
                //如果输入储位处理异常REFID时则按输入的来，没输入就按第一次入库的LOCAT来
                //
                if (txtLocat.Text.ToString() == "")
                {
                    DataRow drRow = dtData.NewRow();
                    drRow["MANDT"] = Mandt;
                    drRow["COMCD"] = Comcd;
                    drRow["WERKS"] = Werks;
                    drRow["LGORT"] = Lgort;
                    drRow["LOCAT"] = dtTmpCheckDID.Rows[0]["LOCAT"];
                    drRow["MATNR"] = dtTmpCheckDID.Rows[0]["MATNR"].ToString().Trim();
                    drRow["INSMK"] = dtTmpCheckDID.Rows[0]["INSMK"].ToString().Trim();//默认为良品
                    drRow["CHARG"] = dtTmpCheckDID.Rows[0]["CHARG"].ToString().Trim();
                    drRow["MENGE"] = "0";
                    drRow["ALQTY"] = dtTmpCheckDID.Rows[0]["MENGE"].ToString().Trim();
                    drRow["MBLNR"] = strCurrentDid;
                    drRow["ZEILE"] = "";
                    drRow["EBELN"] = "";
                    drRow["LIFNR"] = dtTmpCheckDID.Rows[0]["LIFNR"].ToString().Trim();
                    drRow["INSPT"] = "";
                    drRow["LOCOD"] = dtTmpCheckDID.Rows[0]["LOCOD"].ToString().Trim();
                    drRow["SERNO"] = "";
                    drRow["OMBLNR"] = "";
                    drRow["MRGID"] = "";
                    drRow["KOSTL"] = "";
                    drRow["ARBPL"] = "";
                    drRow["TRNTP"] = "";
                    drRow["RMAK1"] = "";
                    drRow["INDAT"] = DateTime.Now.ToString("yyyyMMdd"); ;//若为DateCode仓别，则是DateCode转换之后的数值，否则，是当天日期
                    drRow["VEDAT"] = strVedat;
                    drRow["REFID"] = dtTmpCheckDID.Rows[0]["REFID"].ToString().Trim();
                    drRow["OTLGT"] = dtTmpCheckDID.Rows[0]["OTLGT"].ToString().Trim();
                    drRow["DACOD"] = dtTmpCheckDID.Rows[0]["DACOD"].ToString().Trim();
                    // 添加再检入库的数据 20230823 by Claud
                    drRow["EXPDAT"] = dtTmpCheckDID.Rows[0]["EXPDAT"].ToString().Trim();
                    if (dtTmpCheckDID.Rows[0]["TASKID"].ToString().Trim() == "")
                    {
                        drRow["TASKID"] = "";
                    }
                    else
                    {
                        drRow["TASKID"] = dtTmpCheckDID.Rows[0]["TASKID"].ToString().ToUpper().Trim().Substring(0, 2) == "R7" ? dtTmpCheckDID.Rows[0]["TASKID"].ToString().ToUpper().Trim() : "";

                    }
                    drRow["MAXEXP"] = dtTmpCheckDID.Rows[0]["MAXEXP"].ToString().Trim();
                    dtData.Rows.Add(drRow);
                }
                else
                {
                    DataRow drRow = dtData.NewRow();
                    drRow["MANDT"] = Mandt;
                    drRow["COMCD"] = Comcd;
                    drRow["WERKS"] = Werks;
                    drRow["LGORT"] = Lgort;
                    drRow["LOCAT"] = Locat;
                    drRow["MATNR"] = dtTmpCheckDID.Rows[0]["MATNR"].ToString().Trim();
                    drRow["INSMK"] = dtTmpCheckDID.Rows[0]["INSMK"].ToString().Trim();//默认为良品
                    drRow["CHARG"] = dtTmpCheckDID.Rows[0]["CHARG"].ToString().Trim();
                    drRow["MENGE"] = "0";
                    drRow["ALQTY"] = dtTmpCheckDID.Rows[0]["MENGE"].ToString().Trim();
                    drRow["MBLNR"] = strCurrentDid;
                    drRow["ZEILE"] = "";
                    drRow["EBELN"] = "";
                    drRow["LIFNR"] = dtTmpCheckDID.Rows[0]["LIFNR"].ToString().Trim();
                    drRow["INSPT"] = "";
                    drRow["LOCOD"] = dtTmpCheckDID.Rows[0]["LOCOD"].ToString().Trim();
                    drRow["SERNO"] = "";
                    drRow["OMBLNR"] = "";
                    drRow["MRGID"] = "";
                    drRow["KOSTL"] = "";
                    drRow["ARBPL"] = "";
                    drRow["TRNTP"] = "";
                    drRow["RMAK1"] = "";
                    drRow["INDAT"] = DateTime.Now.ToString("yyyyMMdd");//若为DateCode仓别，则是DateCode转换之后的数值，否则，是当天日期
                    drRow["VEDAT"] = strVedat;

                    drRow["REFID"] = dtTmpCheckDID.Rows[0]["REFID"].ToString().Trim();
                    drRow["OTLGT"] = dtTmpCheckDID.Rows[0]["OTLGT"].ToString().Trim();
                    drRow["DACOD"] = dtTmpCheckDID.Rows[0]["DACOD"].ToString().Trim();
                    // 添加再检入库的数据 20230823 by Claud
                    drRow["EXPDAT"] = dtTmpCheckDID.Rows[0]["EXPDAT"].ToString().Trim();
                    if (dtTmpCheckDID.Rows[0]["TASKID"].ToString().Trim()=="")
                    {
                        drRow["TASKID"] = "";
                    }
                    else
                    {
                        drRow["TASKID"] = dtTmpCheckDID.Rows[0]["TASKID"].ToString().ToUpper().Trim().Substring(0, 2) == "R7" ? dtTmpCheckDID.Rows[0]["TASKID"].ToString().ToUpper().Trim() : "";

                    }
                    drRow["MAXEXP"] = dtTmpCheckDID.Rows[0]["MAXEXP"].ToString().Trim();
                    dtData.Rows.Add(drRow);
                }

                #endregion

                Sound.Play(@"Sound\Success.wav");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion



        private DataTable StorageToSAP(DataTable dtStorage)
        {
            string strError = "";
            string strStatus = "";
            string strException = "";
            strStatus = "T";//所有的311轉倉資料，Status狀態都需傳'T'  Smose Liao 20130523

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
            QCI.QWMS.LogData objLogData = new QCI.QWMS.LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, strProgid);
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);

            try
            {
                #region 記錄UpdateSapInventory開始的時間  Lora
                foreach(string strSapRefID in lsRefID)
                {
                    string strSapLocat = dtData.Select("REFID='" + strSapRefID + "'").CopyToDataTable().Rows[0]["LOCAT"].ToString();
                    objStorageIn.AddErrorLog(dtStorage.Select("REFID='" + strSapRefID + "'").CopyToDataTable(), strSapLocat);
                }
                #endregion

                dtResponse = SendToSAP(dtOutTable).Tables[0];

                for (int i = 0; i < dtResponse.Rows.Count; i++)
                {
                    if (dtResponse.Rows[i]["ERRMSG"].ToString() != "")
                    {
                        //Error Message有回傳值，跳出迴圈
                        strError = dtResponse.Rows[i]["ERRMSG"].ToString();
                        Regex pattern = new Regex(@"49\d{8}");//比對的pattern，SAP回傳的扣帳編號為49開頭
                        Match m = pattern.Match(strError);

                        #region 檢查SAP回傳的扣帳編號是否全為數值型態，避免誤判抓到料號的情況下，誤入了QWMS庫存
                        //【例】SAP回傳的錯誤訊息：3VPJ7AB0000F3AFD Material Batch is not mapping with WO
                        if (m.Success)
                        {
                            int MathIndex = m.Index;//取得SAP回傳的扣帳編號之所在的位置
                            long outNumber = 0;
                            bool bolTryParse = long.TryParse(strError.Substring(MathIndex, 10), out outNumber);

                            if (bolTryParse && dtResponse.Rows[i]["OMBLN"].ToString() == "")
                            {
                                dtResponse.Rows[i]["OMBLN"] = outNumber.ToString();
                            }
                        }
                        #endregion
                    }
                }

                #region 記錄UpdateSapInventory結束的時間  Smose Liao 20100412
                objStorageIn.AddErrorLog(dtStorage, "Update");
                #endregion
            }
            catch (Exception ex)
            {
                strException = ex.Message;
                throw new Exception(ex.Message);
            }

            return dtResponse;
        }

        public DataSet SendToSAP(DataTable dtSap)
        {
            try
            {
                DataSet dsSap = new DataSet();
                dsSap.Tables.Add(dtSap);
                DataSet dsResult = new DataSet();
                //PP_Test.PP_Service objPP = new PP_Test.PP_Service();
                PP.PP_Service objPP = new PP.PP_Service();
                dsResult = objPP.ZRFC_PP_311_AUTO_RETURN_M_WithPlant(strWerks, dsSap);
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btQueryLocat_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            StorageIn_SMT_Query_RefIDLocat objStorageIn_SMT_Query_RefIDLocat = new StorageIn_SMT_Query_RefIDLocat(UserData);
            objStorageIn_SMT_Query_RefIDLocat.Show();
        }

        #region 查询处理SAP扣账失败的REFID
        private void btnERRO_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = string.Empty;
                DataTable dtDID = new DataTable();
                strWerks = cmbWerks.Text.ToString();
                strLgort = cmbLgort.Text.ToString();
                StorageIn_SMT_QueryERRO objStorageIn_SMT_QueryERRO = new StorageIn_SMT_QueryERRO(UserData, Werks, Lgort);
                objStorageIn_SMT_QueryERRO.ShowDialog();
                dtDID = objStorageIn_SMT_QueryERRO.dtRefids;
                if (dtDID != null && dtDID.Rows.Count > 0)
                {
                    ERROREFID(dtDID);
                    txtLocat.Enabled = false;
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.ToString();
            }

        }
        #endregion

        #region SAP扣账失败REFID自动刷入方法（默认刷入是上次失败的储位，可更改）
        private void ERROREFID(DataTable dtDID)
        {
            try
            {
                //循环进行刷入DID的动作
                for (int i = 0; i < dtDID.Rows.Count; i++)
                {
                    #region 原刷入动作
                    try
                    {
                        //gbHeader.Enabled = false;
                        strCurrentDid = txtDidNo.Text.ToString().Trim();
                        strCurrentDid = dtDID.Rows[i]["DIDNO"].ToString().Trim();
                        txtDidNo.Text = string.Empty;

                        #region 判断刷入的DID是否为重复刷入
                        if (dtData.Rows.Count > 0)
                        {
                            List<string> ls = (from dr in dtData.AsEnumerable()
                                               where dr.Field<string>("MBLNR") == strCurrentDid
                                               select dr.Field<string>("MBLNR")).ToList();
                            if (ls.Count > 0)
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                MessageBox.Show("该DidNo数据已刷入");
                                return;
                            }
                        }
                        #endregion

                        #region 检查DID是否存在
                        dtTmpCheckDID = objPlantData.GetDIDInfo(this.Mandt, this.Comcd, this.Werks, this.Lgort, "", strCurrentDid);
                        if (dtTmpCheckDID.Rows.Count <= 0)
                        {
                            Sound.Play(@"Sound\ERROR.wav");
                            MessageBox.Show(strCurrentDid + " doesn't exist!!");
                            return;
                        }
                        else
                        {
                            DataColumn LOCAT = new DataColumn("LOCAT", typeof(string));
                            dtTmpCheckDID.Columns.Add(LOCAT);
                            for (int j = 0; j < dtTmpCheckDID.Rows.Count; j++)
                            {
                                dtTmpCheckDID.Rows[j]["LOCAT"] = dtDID.Rows[0]["LOCAT"];
                            }
                            if (dtTmpCheckDID.Rows[0]["MENGE"].ToString().Trim() == dtTmpCheckDID.Rows[0]["OTQTY"].ToString().Trim())
                            {
                                Sound.Play(@"Sound\ERROR.wav");
                                MessageBox.Show(strCurrentDid + " has been processed!! \n Please check it!!");
                                return;
                            }
                        }
                        #endregion

                        #region 判断当前刷入的DID数据是否是属于当前REFID，若不是，判断当前REFID数据是否已经刷完
                        if (string.IsNullOrEmpty(strCurrentRefid)) //第一次刷入DID信息
                        {
                            strCurrentRefid = dtTmpCheckDID.Rows[0]["REFID"].ToString().Trim();
                        }
                        else
                        {
                            //刷入信息和当前REFID信息不等时，判断REFID信息是否已全部刷入，若全部刷入，则更新当前REID为新的REFID，否则，提示当前REFID信息还未完全刷入
                            if (strCurrentRefid != dtTmpCheckDID.Rows[0]["REFID"].ToString().Trim())
                            {
                                if (CompareRefIDData(strCurrentRefid))
                                {
                                    strCurrentRefid = dtTmpCheckDID.Rows[0]["REFID"].ToString().Trim(); //更新当前操作的REFID信息
                                    #region 判断储位是否有更新
                                    if (strLocat == dtData.Rows[0]["LOCAT"].ToString())
                                    {
                                        Sound.Play(@"Sound\ERROR.wav");
                                        MessageBox.Show("不同REFID不能再同一储位，请更换储位，谢谢！");
                                        txtDidNo.Text = string.Empty;
                                        txtDidNo.Enabled = false;
                                        txtLocat.Enabled = true;
                                        txtLocat.Text = string.Empty;
                                        txtLocat.Focus();
                                        return;
                                    }
                                    #endregion
                                }
                                else
                                    return;
                            }
                        }
                        #endregion

                        AddNewDidNo(strCurrentDid);
                    }
                    catch (Exception ex)
                    {
                        stsWarning.Text = ex.Message;
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region 二次作业编辑修改LOCAT
        private void dgvData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                stsWarning.Text = string.Empty;
                if (e.RowIndex != -1)
                {

                    string strAlterLocat = dgvData.Rows[e.RowIndex].Cells["LOCAT"].Value.ToString();
                    dtData.Rows[e.RowIndex]["LOCAT"] = strAlterLocat;
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }
        #endregion

        #region SendMail
        private bool SendMail(DataTable dt)
        {
            bool blResult = false;
            try
            {
                ClaHttpHelper clahttpHelper = new ClaHttpHelper();
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
                string strUrl = objStorageData.GetSendMailUrl();
                //MailService.SendMailService objSendMail = new MailService.SendMailService();
                string strMailPSD = "975A8056C8DF50786FB680A96E0CCCBF";
                string strMailFrom = "Web_Notice@quantacn.com";
                string strMailSubject = strWerks + " SMT-Ref ID扣账失败Report- " + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                string strMailTo = string.Empty;
                string strMailCC = string.Empty;
                string strMges = "";
                DataTable dtTempTo = new DataTable();
                DataTable dtTempCC = new DataTable();
                dtTempTo = objStorageData.CheckSendMailAuthority(strWerks, "0");
                dtTempCC = objStorageData.CheckSendMailAuthority(strWerks, "1");
                if (dtTempTo.Rows.Count <= 0 && dtTempCC.Rows.Count <= 0)
                {
                    MessageBox.Show("请先配置邮件相关人员！！");
                    return blResult;
                }
                else
                {
                    if (dtTempTo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTempTo.Rows.Count; i++)
                        {
                            strMailTo = strMailTo + dtTempTo.Rows[i]["EMAIL"].ToString().Trim() + ";";
                        }
                    }
                    if (dtTempCC.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTempCC.Rows.Count; i++)
                        {
                            strMailCC = strMailCC + dtTempCC.Rows[i]["EMAIL"].ToString().Trim() + ";";
                        }
                    }
                }


                StringBuilder sbMailContent = new StringBuilder();
                sbMailContent.Append("<HTML><BODY leftmargin=1 topmargin=1><p>Dear  All：</p><p>&#12288;&#12288;" + " 以下为QWMS SMT-Ref ID祥龙退料扣账失败Report，请相关人员及时确认处理，谢谢！</p><CENTER>");
                sbMailContent.Append("<TABLE width=100% border=1 align=center cellpadding=4 bordercolor=#000000 style='border-collapse: collapse'>");
                sbMailContent.Append("<tr style=\"text-align: center;background-color:#D8C0B1;font-family: 微软雅黑体;width:100%;\"><td colspan=13><FONT style=\"  COLOR: #333333;font-weight:bold;  FONT-FAMILY: &quot;微软雅黑体&quot;; FONT-SIZE: 20pt; position: relative; filter: blur(add=1, direction=45, strength=3);\">SMT-Ref ID Auto Post For Paperless Error Report</FONT></td></tr>");
                sbMailContent.Append(" <tr style=\"text-align: center; COLOR: #333333; font-weight:bold;;FONT-SIZE: 15pt; background-color:#D8C0B1;height: 34px;font-family: 微软雅黑体;width:100%;\"><td>作业类型</td><td>Ref ID</td><td>收料厂区</td><td>退料仓别</td><td>作业工号</td><td>报错信息</td><td>remark</td></tr>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr;
                    dr = dt.Rows[i];
                    sbMailContent.Append("<tr style=\" color:black;width:100%;\"><td>  " + "QWMS-OGR（SMT Batch）" + "</td><td>  " + dr["REFID"].ToString() + "</td><td>" + strWerks
                                 + "</td><td>" + strLgort + "</td><td>" + Usrnm + "</td><td>  " + dr["ERRMSG"].ToString() + "</td><td style=\" color:red;\">" + "NG"
                                 + "</td></tr>");

                }
                sbMailContent.Append("</TD></TR></TABLE><BR><CENTER><br><HR SIZE=1 width=100%>");
                sbMailContent.Append("</BODY></HTML>");
                string emil = sbMailContent.ToString();
                var Data = new
                {
                    Site = "QSMC",
                    TeamPwd = strMailPSD,
                    From = strMailFrom,
                    To = strMailTo,
                    Cc = strMailCC,
                    Bcc = "",
                    Subject = strMailSubject,
                    Body = sbMailContent.ToString(),
                    IsBodyHtml = true,
                    Attachments = "",
                };
                string strData = JsonConvert.SerializeObject(Data);
                JObject result = (JObject)JsonConvert.DeserializeObject(clahttpHelper.HttpPostByHttpWebRequest(strUrl, strData));
                if (result["Result"].ToString().ToUpper() == "TRUE")
                {
                    blResult = true;
                }
                //blResult = objSendMail.SendMail(strMailPSD, true, strMailFrom, strMailTo, strMailCC, "", strMailSubject, sbMailContent.ToString(), "", out strMges);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return blResult;
            }
            return blResult;
        }
        #endregion
    }
}
