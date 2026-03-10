using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Admin_LocationDefine : Form
    {            
        #region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";//QWMS系统基础资料维护权限
        private string strWerks = "";
        private string strLgort = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private FileInfo fi;
        private StreamWriter sw;
        private DataTable dtData;
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;

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
        # endregion   

        #region 构建式
        public Admin_LocationDefine(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;

            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            this.txtFloor.Enabled = false;
            this.txtArea.Enabled = false;
            this.cmbType.Enabled = false;
            chkModify.Enabled = false;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                cmbType.SelectedIndex = 0;
                cmbModel.SelectedIndex = 0;
                cmbVersion.SelectedIndex = 0;
                cmbPdnam.SelectedIndex = 0;

                //检查权限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    gvData.AutoGenerateColumns = false;

                    //CSMC才需要顯示CTO/BTO, REGION, MACHINE等下拉選單  Smose Liao 20100420
                    if (Comcd == "9700")
                    {
                        ShowCtbtoRegonMapid();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        # endregion

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion

        # region 绑定Plant
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
                //cmbWerks.DataSource = dtTemp;
                //cmbWerks.DisplayMember = "F_TEXT";
                //cmbWerks.ValueMember = "F_TEXT";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        # endregion

        # region 绑定Storage
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

        # region 绑定CTO/BTO
        private void ShowDdlCtbto()
        {
            try
            {
                DataTable dt = objAdmin.GetCtoBto();
                this.cmbCTBTO.DataSource = dt;
                this.cmbCTBTO.DisplayMember = "F_TEXT";
                this.cmbCTBTO.ValueMember = "F_VALUE";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlCtbto()");
            }
        }
        # endregion

        # region 绑定洲别
        private void ShowDdlRegon()
        {
           
            try
            {
                DataTable dt = objAdmin.GetRegion();
                this.cmbRegon.DataSource = dt;
                this.cmbRegon.DisplayMember = "F_TEXT";
                this.cmbRegon.ValueMember = "F_VALUE";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlRegon()");
            }
        }
        # endregion

        # region 绑定机种
        private void ShowDdlMapid()
        {
           
            try
            {
                DataTable dt = objAdmin.GetMachine();

                this.cmbMapid.DataSource = dt;
                this.cmbMapid.DisplayMember = "F_TEXT";
                this.cmbMapid.ValueMember = "F_VALUE";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlRegon()");
            }
        }
        # endregion

        # region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbLgort.Enabled = true;
                if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    ShowDataGrid(strWerks, strLgort);
                }
                //倉別下拉選單
                ShowDdlLgort();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region storage SelectChange
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetLocation();
                if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    ShowDataGrid(strWerks, strLgort);
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        #region ShowDataGrid
        private void ShowDataGrid(string strWerks, string strLgort)
        {
            try
            {
                string strCtbto = (cmbCTBTO.SelectedIndex == -1 ? "" : cmbCTBTO.SelectedValue.ToString().Trim());
                string strRegon = (cmbRegon.SelectedIndex == -1 ? "" : cmbRegon.SelectedValue.ToString().Trim());
                string strMapid = (cmbMapid.SelectedIndex == -1 ? "" : cmbMapid.SelectedValue.ToString().Trim());

                dtData = objPlantData.GetAllLocatData(strWerks, strLgort, "", "2", strCtbto, strRegon, strMapid);               
                gvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        # region Function CheckedChange
        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtFloor.Text = null;
                this.txtArea.Text = null;
                this.cmbType.SelectedIndex = 0;
                this.txtFloor.Enabled = false;
                this.txtArea.Enabled = false;
                this.cmbType.Enabled = false;
                this.chkModify.Enabled = false;
                stsWarning.Text = "";
                this.gbFunction.Enabled = false;
                this.gbHeader.Enabled = true;
                this.btnSave.Enabled = true;

                //廠區下拉選單
                ShowDdlWerks();
                ShowDdlCtbto();
                ShowDdlRegon();
                ShowDdlMapid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void rdoAdd_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtFloor.Text = null;
                this.txtArea.Text = null;
                this.cmbType.SelectedIndex = 0;
                this.txtFloor.Enabled = true;
                this.txtArea.Enabled = true;
                this.cmbType.Enabled = true;
                this.chkModify.Enabled = false;
                stsWarning.Text = "";
                this.gbFunction.Enabled = false;
                this.gbHeader.Enabled = true;
                this.btnSave.Enabled = true;
                this.chkAutoRunLocat.Enabled = true;

                //廠區下拉選單
                ShowDdlWerks();
                ShowDdlCtbto();
                ShowDdlRegon();
                ShowDdlMapid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void rdoModify_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtFloor.Text = null;
                this.txtArea.Text = null;
                this.cmbType.SelectedIndex = 0;
                this.txtFloor.Enabled = true;
                this.txtArea.Enabled = true;
                this.cmbType.Enabled = true;
                this.chkModify.Enabled = true;
                stsWarning.Text = "";
                this.gbFunction.Enabled = false;
                this.gbHeader.Enabled = true;
                this.btnSave.Enabled = true;
                this.chkAutoRunLocat.Enabled = true;

                //廠區下拉選單
                ShowDdlWerks();
                ShowDdlCtbto();
                ShowDdlRegon();
                ShowDdlMapid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void chkAutoRunLocat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRunLocat.Checked)
            {
                this.txtIDNum.Enabled = true;
                this.txtIDNum.Text = "";
                this.cmbPdnam.Enabled = true;
            }
            else
            {
                this.txtIDNum.Enabled = false;
                this.txtIDNum.Text = "";
                this.cmbPdnam.Enabled = false;
            }
        }

        # endregion

        # region 布局尺寸调整
        private void Admin_LocationDefine_Resize(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size((int)(this.Size.Width * 0.4), panel5.Size.Height);
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width-60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width-60;

        }
        # endregion

        #region 保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 變數宣告&清空
            string Floor,Area,Type;
            bool bolPNLocat = false;
            string strModel = "";
            string strVersion = "";
            int intPalQty = 0;
            stsWarning.Text = "";
            Floor = Area = Type = "";
            DataTable dtTemp = new DataTable();
            cmbModel.SelectedIndex = 0;
            cmbVersion.SelectedIndex = 0;
            cmbPdnam.SelectedIndex = 0;
            bool blIsAuto = false;
            string strPdnam = "";
            #endregion
            try
            {
                #region 防呆限制
                if (strSttyp == "CARROUSEL")
                {
                    stsWarning.Text = "You can't add or delete or modify location in the Carrousel storage!!";
                    return;
                }

                if (chkModify.Checked)//by倉別修改儲位資訊
                {
                    //檢查是不是空值及長度
                    if (cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
                    {
                        stsWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                }
                else
                {
                    if (chkMultiLocation.Checked == false)//如非批次建立
                    {
                        //檢查是不是空值及長度
                        if (this.txtLocat.Text.Trim() == "" || cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
                        {
                            stsWarning.Text = "Plant, storage and location can't be empty!!";
                            return;
                        }
                    }
                    else//如是批次建立
                    {
                        //檢查是不是空值及長度
                        if (this.txtLocatMulti.Text.Trim() == "" || this.txtLocatFrom.Text.Trim() == "" || this.txtLocatTo.Text.Trim() == "" || cmbWerks.SelectedIndex == -1 || cmbLgort.SelectedIndex == -1)
                        {
                            stsWarning.Text = "Plant, storage , location, From and To can't be empty!!";
                            return;
                        }
                        if (!ValidateNumber(this.txtLocatFrom.Text.Trim()) || !ValidateNumber(this.txtLocatTo.Text.Trim()))
                        {
                            stsWarning.Text = "From and To must be number!!";
                            return;
                        }
                        if (this.txtLocatFrom.Text.Trim().Length != this.txtLocatTo.Text.Trim().Length)
                        {
                            stsWarning.Text = "From and To must have the same length!!";
                            return;
                        }
                    }
                }

                //如果Area有值,Floor也必須有值  --20130819, Gary Lee
                if (!Area.Equals(""))
                {
                    if (Floor.Equals(""))
                    {
                        stsWarning.Text = "Floor can't be empty!!";
                        return;
                    }
                }

                //若勾選料架儲位，則滿板數量不得為空  Smose 20141027
                if (chkPNLocat.Checked)
                {
                    if (txtPalQty.Text.ToString().Trim() == "")
                    {
                        stsWarning.Text = "料架储位的满板数量不得为空，请确认!!";
                        txtPalQty.Focus();
                        return;
                    }
                }

                if (this.chkAutoRunLocat.Checked)
                {
                    if (!ValidateNumber(this.txtIDNum.Text.Trim()))
                    {
                        stsWarning.Text = "输入ID数量必须为数字!!";
                        txtIDNum.Focus();
                        return;
                    }
                }

                #endregion
                #region 變數取值
                //新增樓層,區域,儲位類型資訊 --20130819, Gary Lee
                if (txtFloor != null)
                {
                    if (!txtFloor.ToString().Trim().Equals(""))
                    {
                        Floor = txtFloor.Text.ToString().Trim();
                    }
                }

                if (txtArea != null)
                {
                    if (!txtArea.ToString().Trim().Equals(""))
                    {
                        Area = txtArea.Text.ToString().Trim();
                    }
                }

                if (cmbType.SelectedIndex!=-1 && !cmbType.SelectedItem.ToString().Equals("--NULL--"))
                {
                    Type = cmbType.SelectedItem.ToString();
                }

                //如果是Carrousel不能新增也不能修改
                //取得Sttyp及Lotyp
                dtTemp = objPlantData.GetPlantStorageData("LGORT", strWerks, strLgort);
                if (dtTemp.Rows.Count >= 1)
                {
                    strSttyp = dtTemp.Rows[0]["CTRLC4"].ToString().ToUpper();
                    strLotyp = dtTemp.Rows[0]["CTRLC5"].ToString().ToUpper();
                }
                //取得是否為料架儲位
                if (chkPNLocat.Checked)
                {
                    bolPNLocat = true;
                }
                //取得滿板數量
                if (!txtPalQty.Text.ToString().Equals(""))
                {
                    intPalQty = int.Parse(txtPalQty.Text.ToString().Trim());
                }
                //取得機種的選項
                if (cmbModel.SelectedIndex != -1 && !cmbModel.SelectedItem.ToString().Equals("--NULL--"))
                {
                    strModel = cmbModel.SelectedItem.ToString();
                }
                //取得版本的選項
                if (cmbVersion.SelectedIndex!=-1 && !cmbVersion.SelectedItem.ToString().Equals("--NULL--"))
                {
                    strVersion = cmbVersion.SelectedItem.ToString();
                }
                //是否设置自动跑储
                if (chkAutoRunLocat.Checked)
                {
                    blIsAuto = true;
                }
                //取得品名
                if (cmbPdnam.SelectedIndex!=-1 &&  !cmbPdnam.SelectedItem.ToString().Equals("--NULL--"))
                {
                    strPdnam = cmbPdnam.SelectedItem.ToString();
                }

                #endregion
                if (!chkModify.Checked)
                {
                    #region 檢查是否有勾選為待出貨儲位 by Smose Liao 20100420
                    if (this.chkShipLocat.Checked == true)
                    {
                        //檢查待出貨儲位的長度是否為6碼
                        if (this.txtLocat.Text.Length == 6)
                        {
                            if (this.txtLocat.Text.Substring(0, 1) != "N")
                            {
                                stsWarning.Text = "待出貨儲位的第一個字元需為'N'，請確認!!";
                                return;
                            }
                        }
                        else
                        {
                            stsWarning.Text = "待出貨儲位的長度需為6碼，請確認!!";
                            return;
                        }
                    }
                    #endregion
                    # region 判断组合批次储位
                    ArrayList arlLocation = new ArrayList();
                    if (chkMultiLocation.Checked == true)//如是批次建立
                    {
                        int intlenght = this.txtLocatFrom.Text.Trim().Length;
                        string strZero = "";
                        for (int j = 0; j < intlenght; j++)
                        {
                            strZero += "0";

                        }
                        for (int i = Convert.ToInt16(this.txtLocatFrom.Text.Trim()); i <= Convert.ToInt16(this.txtLocatTo.Text.Trim()); i++)
                        {
                            arlLocation.Add(this.txtLocatMulti.Text.Trim() + i.ToString(strZero));
                        }

                    }
                    else//如非批次建立
                    {
                        arlLocation.Add(this.txtLocat.Text.Trim());

                    }
                    #endregion
                    # region 删除
                    //刪除
                    if (rdoDelete.Checked == true)
                    {
                        for (int i = 0; i < arlLocation.Count; i++)
                        {
                            //檢查儲位是否存在
                            if (!objPlantData.CheckExistedStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), arlLocation[i].ToString()))
                            {
                                stsWarning.Text = arlLocation[i].ToString() + ": The location doesn't exist in the system!!";
                                return;
                            }
                            //檢查儲位是否有庫存
                            if (objPlantData.CheckStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), arlLocation[i].ToString()))
                            {
                                stsWarning.Text = arlLocation[i].ToString() + ": The location has storage now and can't be deleted!!";
                                return;
                            }
                        }

                        //刪除資料
                        if (objAdmin.DeleteLocation(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), arlLocation))
                        {
                            stsWarning.Text = "Delete OK!";
                            this.txtLocat.Text = "";
                            this.txtLocatFrom.Text = "";
                            this.txtLocatMulti.Text = "";
                            this.txtLocatTo.Text = "";
                        }
                        else
                        {
                            stsWarning.Text = "Delete Fail!" + objAdmin.ERRMSG;
                        }
                    }
                    # endregion
                    #region 新增
                    if (rdoAdd.Checked == true)
                    {
                        AsrsInterface objAsrsInterface = new AsrsInterface(UserData);
                        //新增储位，判断仓别是否为ASRS类型，ASRS仓别同一厂区不允许相同储位
                        if (objAsrsInterface.CheckLGORT(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString()))
                            {
                                for (int i = 0; i < arlLocation.Count; i++)
                                {
                                    //ASRS同一厂区不允许相同储位
                                    if (objAsrsInterface.CheckASRSLocExisted(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), arlLocation[i].ToString().Trim()))
                                    {
                                        stsWarning.Text = cmbLgort.Items[cmbLgort.SelectedIndex].ToString() + " is  ASRS logrt, The location " + arlLocation[i].ToString().Trim() + "  has existed !";
                                        return;
                                    }
                                 }
                              }
                        else
                        {
                            for (int i = 0; i < arlLocation.Count; i++)
                            {
                                //檢查儲位是否存在
                                if (objPlantData.CheckExistedStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), arlLocation[i].ToString().Trim()))
                                {
                                    stsWarning.Text = arlLocation[i].ToString().Trim() + " The storage has existed in the system!!";
                                    return;
                                }
                            }
                        }
                        //新增資料
                        string strCtbto = (cmbCTBTO.SelectedIndex == -1 ? "" : cmbCTBTO.SelectedValue.ToString().Trim());
                        string strRegon = (cmbRegon.SelectedIndex == -1 ? "" : cmbRegon.SelectedValue.ToString().Trim());
                        string strMapid = (cmbMapid.SelectedIndex == -1 ? "" : cmbMapid.SelectedValue.ToString().Trim());

                        if (objAdmin.AddLocation(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), arlLocation, "", "", "", "", strCtbto, strRegon, strMapid, Floor, Area, Type, bolPNLocat, intPalQty, strModel, strVersion, blIsAuto, this.txtIDNum.Text.ToString().Trim(),strPdnam))
                        {
                            stsWarning.Text = "Add OK!";
                            Refresh();
                        }
                        else
                        {
                            stsWarning.Text = "Add Fail!" + objAdmin.ERRMSG;
                        }
                    }
                    #endregion
                    #region 更新
                    //更新
                    if (rdoModify.Checked == true)
                    {
                        for (int i = 0; i < arlLocation.Count; i++)
                        {
                            //檢查儲位是否存在
                            if (!objPlantData.CheckExistedStorageData(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), arlLocation[i].ToString().Trim()))
                            {
                                stsWarning.Text = arlLocation[i].ToString().Trim() + " The Location not Exist!!";
                                return;
                            }
                        }

                        //新增資料
                        string strCtbto = (cmbCTBTO.SelectedIndex == -1 ? "" : cmbCTBTO.SelectedValue.ToString().Trim());
                        string strRegon = (cmbRegon.SelectedIndex == -1 ? "" : cmbRegon.SelectedValue.ToString().Trim());
                        string strMapid = (cmbMapid.SelectedIndex == -1 ? "" : cmbMapid.SelectedValue.ToString().Trim());

                        if (objAdmin.UpdateLocation(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), arlLocation, strCtbto, strRegon, strMapid, Floor, Area, Type, bolPNLocat, intPalQty, strModel, strVersion, blIsAuto, this.txtIDNum.Text.ToString().Trim(),strPdnam))
                        {
                            stsWarning.Text = "Modify OK!";
                            Refresh();
                        }
                        else
                        {
                            stsWarning.Text = "Modify Fail!" + objAdmin.ERRMSG;
                        }
                    }

                    #endregion
                }
                else
                {
                    #region 更新儲位資訊by倉別
                    if (rdoModify.Checked && chkModify.Checked)
                    {
                        if (objAdmin.UpdateLocationByStorage(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), Floor, Area, Type))
                        {
                            stsWarning.Text = "Modify by Storage OK!";
                            Refresh();
                        }
                        else
                        {
                            stsWarning.Text = "Modify Fail!" + objAdmin.ERRMSG;
                        }
                    }
                    #endregion
                }

                ShowDataGrid(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString());
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                cmbWerks.SelectedIndex = -1;
                cmbLgort.SelectedIndex = -1;
                this.rdoDelete.Checked = false;
                this.rdoAdd.Checked = false;
                this.rdoModify.Checked = false;
                this.gbHeader.Enabled = false;
                this.gvData.DataSource = null;
                this.btnSave.Enabled = false;
                this.stsWarning.Text = "";
                Refresh();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        # region Download
        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                sfdSaveFile.Filter = "文本文件(*.txt)|*.txt|Excel(*.xls)|*.xls";
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
        # endregion

        # region 写文件
        private void CountingResult2File(string strFilePath)
        {

            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Plant\tStorage\tLocation\tCTO/BTO\tRegion";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += dtData.Rows[i]["WERKS"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["LOCAT"].ToString() + "\t";
                    strLine += dtData.Rows[i]["CTBTONM"].ToString() + "\t";
                    strLine += dtData.Rows[i]["REGONNM"].ToString();
                    sw.WriteLine(strLine);
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

        # region CTBTO SelectedChage
        private void cmbCTBTO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetLocation();
                if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    ShowDataGrid(strWerks, strLgort);
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region Regon SelectedChage
        private void cmbRegon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetLocation();
                if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    ShowDataGrid(strWerks, strLgort);
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        # region Machine SelectedChage
        private void cmbMapid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetLocation();
                if (cmbWerks.SelectedIndex != -1 && cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    ShowDataGrid(strWerks, strLgort);
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
        # endregion

        #region Checked Change
        private void chkMultiLocation_CheckedChanged(object sender, EventArgs e)
        {
            SetLocation();
        }

        //批次改變某倉別下所有儲位的FLOOR,AREA,TYPE
        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkModify.Checked)
            {
                #region 控制項控制
                this.txtLocat.Text = "";
                this.txtLocatFrom.Text = "";
                this.txtLocatTo.Text = "";
                this.txtLocatMulti.Text = "";
                this.chkShipLocat.Checked = false;
                this.chkMultiLocation.Checked = false;
                this.cmbCTBTO.SelectedIndex =-1;
                this.cmbRegon.SelectedIndex =-1;
                this.cmbMapid.SelectedIndex =-1;
                this.txtLocat.Enabled = false;
                this.txtLocatFrom.Enabled = false;
                this.txtLocatTo.Enabled = false;
                this.txtLocatMulti.Enabled = false;
                this.chkShipLocat.Enabled = false;
                this.chkMultiLocation.Enabled = false;
                this.cmbCTBTO.Enabled = false;
                this.cmbRegon.Enabled = false;
                this.cmbMapid.Enabled = false;

                #endregion
            }
            else
            {
                #region 控制項控制
                this.txtLocat.Enabled = true;
                this.chkShipLocat.Enabled = true;
                this.chkMultiLocation.Enabled = true;
                this.cmbCTBTO.Enabled = true;
                this.cmbRegon.Enabled = true;
                this.cmbMapid.Enabled = true;
                #endregion
            }
        }
        #endregion

        private void SetLocation()
        {
            if (!chkModify.Checked)
            {
                if (chkMultiLocation.Checked == true)
                {
                    txtLocat.Text = "";
                    txtLocat.Enabled = false;
                    txtLocatFrom.Text = "";
                    txtLocatMulti.Text = "";
                    txtLocatTo.Text = "";
                    txtLocatFrom.Enabled = true;
                    txtLocatMulti.Enabled = true;
                    txtLocatTo.Enabled = true;
                }
                else
                {
                    txtLocat.Text = "";
                    txtLocat.Enabled = true;
                    txtLocatFrom.Text = "";
                    txtLocatMulti.Text = "";
                    txtLocatTo.Text = "";
                    txtLocatFrom.Enabled = false;
                    txtLocatMulti.Enabled = false;
                    txtLocatTo.Enabled = false;
                }
            }
 
        }

        private bool ValidateNumber(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]*[1-9][0-9]*$");
            return reg1.IsMatch(str);

        }

        private void ShowCtbtoRegonMapid()
        {
            gbJHX.Visible = true;
            chkPNLocat.Visible = true;
            cmbCTBTO.Visible = true;
            cmbRegon.Visible = true;
            cmbMapid.Visible = true;
            lblCTBTO.Visible = true;
            lblMapid.Visible = true;
            lblRegon.Visible = true;
            lblModel.Visible = true;
            lblPalQty.Visible = true;
            lblVersion.Visible = true;
            txtPalQty.Visible = true;
            cmbModel.Visible = true;
            cmbVersion.Visible = true;
        }

        private void txtLocat_KeyUp(object sender, KeyEventArgs e)
        {
            if (rdoModify.Checked)
            {
                //DataTable dtLocat = new DataTable();
                //dtLocat = objAdmin.IsAutoLocation(cmbWerks.Items[cmbWerks.SelectedIndex].ToString(),
                //    cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), this.txtLocat.Text.Trim());
                //if (dtLocat.Rows.Count > 0)
                //{
                //    if (dtLocat.Rows[0]["ISATL"].ToString() == "Y")
                //    {
                //        this.chkAutoRunLocat.Checked = true;
                //        this.txtIDNum.Text = dtLocat.Rows[0]["RIDNO"].ToString().Trim();
                //    }
                //    else
                //    {
                //        this.chkAutoRunLocat.Checked = false;
                //        this.txtIDNum.Text = "";
                //    }
                //}
                //else
                //{
                //    this.chkAutoRunLocat.Checked = false;
                //    this.txtIDNum.Text = "";
                //}
                this.chkAutoRunLocat.Checked = false;
                this.txtIDNum.Text = "";
            }
        }

        private void Refresh()
        {
            this.txtFloor.Text = null;
            this.txtArea.Text = null;
            this.txtFloor.Enabled = false;
            this.txtArea.Enabled = false;
            this.gbFunction.Enabled = true;
            this.chkShipLocat.Checked = false;
            this.txtLocat.Text = "";
            this.txtLocatFrom.Text = "";
            this.txtLocatMulti.Text = "";
            this.txtLocatTo.Text = "";
            this.lblCount.Text = "";
            this.txtPalQty.Text = "";
            this.chkModify.Enabled = false;
            this.chkMultiLocation.Checked = false;
            this.chkPNLocat.Checked = false;
            this.chkAutoRunLocat.Checked = false;
            this.chkAutoRunLocat.Enabled = false;
            this.txtIDNum.Text = "";
            this.txtIDNum.Enabled = false;
            cmbCTBTO.SelectedIndex = -1;
            cmbRegon.SelectedIndex = -1;
            cmbMapid.SelectedIndex = -1;
            cmbModel.SelectedIndex = -1;
            cmbVersion.SelectedIndex = -1;
            cmbPdnam.SelectedIndex = -1;
        }

    }
}
