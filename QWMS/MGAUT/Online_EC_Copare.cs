using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;
using Qci.Base.Common;
using QWMS.Entity;


namespace QWMS
{
    public partial class Online_EC_Copare : Form
    {
        public Online_EC_Copare()
        {
            InitializeComponent();

        }
        UserInfo UserData = new UserInfo();
        DataTable boxinfo = new DataTable();
        int boxTNum = 0;
        int GTNum = 0;
        int STNum = 0;
        string Lotinfo = "";
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
        private bool bolDuplicate = false;
        private DataTable dtData = new DataTable();

        #region 設定變數
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

        #region 初始化控件属性
        private void SetInitial()
        {
            cmbWerks.SelectedItem = cmbWerks.Items[0];
            cmbLgort.SelectedItem = cmbLgort.Items[0];
            cmbWerks.Enabled = false;
            cmbLgort.Enabled = false;
            panel5.Enabled = false;
            btnCopare.Enabled = false;
            boxinfo = new DataTable();
            DataTable dt = new DataTable();
            dgvBOX.DataSource = boxinfo;
            dgvECData.DataSource = dt;
            GTNum = 0;
            STNum = 0;
            txtECTNum.Enabled = false;
            txtBoxTNum.Enabled = false;
            txtECTNum.Text = "";
            txtSTNum.Text = "";
            txtGTNum.Text = "";
            txtBoxID.Text = "";
            txtPartNo.Text = "";
            txtQTY.Text = "";
            lblwarning.Text = "";
            txtVbeln.Text = "";
            txtVbeln.Enabled = true;
            btnConfirm.Enabled = true;



        }
        #endregion

        #region 主程式
        public Online_EC_Copare(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    cmbWerks.Enabled = false;
                    cmbLgort.Enabled = false;
                    ShowGDDdlWerks();
                    ShowStatusData();

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
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        #region ShowGDDdlWerks

        private void ShowGDDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckGDPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    cmbLgort.Items.Add(dtTemp.Rows[i]["F_VALUE"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        #endregion

        #region ShowDdlWerks 不用
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

        #region cmbWerks_SelectedIndexChanged
        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            lblwarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                lblwarning.Text = "";
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            lblwarning.Text = "";
            if (txtVbeln.Text.Trim() == "")
            {
                lblwarning.Text = "请输入EC单号！";
                return;
            }
            string vbeln = txtVbeln.Text.Trim();
            StorageData objStorageData = new StorageData(UserData);
            DataTable dtECBOX = objStorageData.CheckECbox(vbeln);
            if (dtECBOX.Rows.Count > 0)
            {
                lblwarning.Text = "该EC单已经比对成功过，无需重复比对！";
                return;
            }
            DataTable value = objStorageData.CheckECIFexist(vbeln);
            if (value.Rows.Count == 0)
            {
                lblwarning.Text = "无法找到该EC单信息，请确认该EC单是否存在！";
                txtVbeln.Text = "";
                txtVbeln.Focus();
            }
            else
            {

                int ecTNum = 0;
                panel5.Enabled = true;
                txtPartNo.Enabled = false;
                txtQTY.Enabled = false;
                dgvECData.DataSource = value;

                lblwarning.Text = "";
                btnConfirm.Enabled = false;
                txtVbeln.Enabled = false;
                for (int n = 0; n < value.Rows.Count; n++)
                {
                    ecTNum += Convert.ToInt32(value.Rows[n]["QTY"]);
                }
                txtECTNum.Text = ecTNum.ToString();

                if (Comcd == "2280")
                {
                    txtBoxID.Enabled = true;
                    txtBoxID.Focus();
                    for (int m = 0; m < value.Rows.Count; m++)
                    {
                        if (value.Rows[m]["MATNR"].ToString() == "QMTDQJ8558A002" || value.Rows[m]["MATNR"].ToString() == "QMTDQJ8558A003"
                            || value.Rows[m]["MATNR"].ToString() == "QMTDQJ8559A000" || value.Rows[m]["MATNR"].ToString() == "QMTDQJ8559A001"
                            || value.Rows[m]["MATNR"].ToString() == "QMTDQJ00853604" || value.Rows[m]["MATNR"].ToString() == "QMTDQJ00853605"
                            || value.Rows[m]["MATNR"].ToString() == "QMTDQJ8556A010" || value.Rows[m]["MATNR"].ToString() == "QMTDQJ8556A011")
                        {
                            Lotinfo = "Y";
                            txtLot.Enabled = true;
                            txtBoxID.Enabled = false;
                            txtLot.Focus();

                        }
                    }

                }
                else
                {
                    txtBoxID.Enabled = true;
                    txtBoxID.Focus();
                }
            }
        }
        #endregion

        #region txtBoxID_KeyPress

        private void txtBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                lblwarning.Text = "";

                if (txtBoxID.Text.Trim() == "")
                {
                    lblwarning.Text = "BoxID不能为空！";
                }
                else
                {
                    DataTable dtECBox = GetDgvToTable(dgvBOX);
                    for (int m = 0; m < dtECBox.Rows.Count; m++)
                    {
                        if (txtBoxID.Text.Trim() == dtECBox.Rows[m]["BOXID"].ToString())
                        {
                            lblwarning.Text = "该BOXID已经刷过，请勿重复刷！";
                            txtBoxID.Focus();
                            return;
                        }
                    }
                    txtPartNo.Enabled = true;
                    txtPartNo.Focus();
                    lblwarning.Text = "";
                }

            }
        }
        #endregion

        #region txtQTY_KeyPress
        private void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                lblwarning.Text = "";
                if (txtQTY.Text.Trim() == "")
                {
                    lblwarning.Text = "QTY不能为空！";
                    txtQTY.Focus();

                }
                else if (txtPartNo.Text.Trim() == "")
                {
                    lblwarning.Text = "料号不能为空！";
                    txtPartNo.Focus();
                }
                else if (txtBoxID.Text.Trim() == "")
                {
                    lblwarning.Text = "BoxID不能为空！";
                    txtBoxID.Focus();
                }
                else
                {
                    lblwarning.Text = "";
                    //检查数据中是否包含‘/’，从而判断数据是否异常。
                    if (txtQTY.Text.IndexOf('/') == -1)
                    {
                        lblwarning.Text = "刷入内容格式有误，请确认！";
                        return;
                    }
                    string[] qtys = txtQTY.Text.Split('/');
                    int GNum = Convert.ToInt32(qtys[0]);
                    int SNum = Convert.ToInt32(qtys[1]);
                    int TNum = Convert.ToInt32(qtys[2]);
                    if (dgvBOX.Rows.Count == 0)
                    {
                        boxinfo.Columns.Clear();
                        boxinfo.Columns.Add("BoxID");
                        boxinfo.Columns.Add("PartNo");
                        boxinfo.Columns.Add("GNum");
                        boxinfo.Columns.Add("SNum");
                        boxinfo.Columns.Add("TotalNum");
                        //新增Lot信息
                        boxinfo.Columns.Add("LotCode");

                        boxinfo.Columns.Add("ECNum");
                        boxinfo.Columns.Add("WERKS");
                        boxinfo.Columns.Add("LGORT");
                        boxinfo.Columns.Add("MANDT");
                        boxinfo.Columns.Add("COMCD");
                    }

                    DataRow boxrow = boxinfo.NewRow();
                    boxrow["BoxID"] = txtBoxID.Text.Trim();
                    boxrow["PartNo"] = txtPartNo.Text.Trim();
                    boxrow["LotCode"] = txtLot.Text.Trim();
                    boxrow["GNum"] = GNum;
                    boxrow["SNum"] = SNum;
                    boxrow["TotalNum"] = TNum;
                    boxrow["ECNum"] = txtVbeln.Text.Trim();
                    boxinfo.Rows.Add(boxrow);
                    dgvBOX.DataSource = boxinfo;
                    txtBoxID.Text = "";
                    txtPartNo.Text = "";
                    txtQTY.Text = "";
                    txtLot.Text = "";
                    if (Lotinfo == "Y")
                    {
                        txtLot.Focus();//校验lotcode是否有值
                    }
                    else
                    {
                        txtBoxID.Focus();
                    }
                    txtPartNo.Enabled = false;
                    txtQTY.Enabled = false;
                    //cmbWerks.Enabled = true;
                    //cmbLgort.Enabled = true;
                    boxTNum += TNum;
                    GTNum += GNum;
                    STNum += SNum;
                    txtBoxTNum.Text = boxTNum.ToString();
                    txtGTNum.Text = GTNum.ToString();
                    txtSTNum.Text = STNum.ToString();
                    if (txtBoxTNum.Text.Trim() == txtECTNum.Text.Trim())
                    {
                        btnCopare.Enabled = true;
                    }
                }
            }
        }
        #endregion

        #region txtPartNo_KeyPress
        private void txtPartNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                lblwarning.Text = "";
                if (txtPartNo.Text.Trim() == "")
                {
                    lblwarning.Text = "料号不能为空！";
                }
                else
                {
                    int num = 0;
                    DataTable dtECParNo = GetDgvToTable(dgvECData);
                    //判断料号是否在EC单中存在
                    for (int m = 0; m < dtECParNo.Rows.Count; m++)
                    {
                        if (txtPartNo.Text.Trim() == dtECParNo.Rows[m]["MATNR"].ToString())
                        {
                            num = 1;
                            break;
                        }
                    }
                    if (num == 1)
                    {
                        txtQTY.Enabled = true;
                        txtQTY.Focus();
                        lblwarning.Text = "";
                    }
                    else
                    {
                        lblwarning.Text = "该料号在EC单中不存在,请确认！";
                        txtQTY.Enabled = false;
                    }

                }

            }
        }
        #endregion

        #region GetDgvToTable
        //把dgv编程DataTable   by Blank 2015/05/27
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region btnCopare_Click
        private void btnCopare_Click(object sender, EventArgs e)
        {
            lblwarning.Text = "";
            if (cmbWerks.Items[cmbWerks.SelectedIndex].ToString() == "" || cmbLgort.Items[cmbLgort.SelectedIndex].ToString() == "")
            {
                lblwarning.Text = "请先选择厂区仓别";
                return;
            }
            int GNumber = 0;
            int SNumber = 0;
            int TNumber = 0;
            int boxGNumber = 0;
            int boxSNumber = 0;
            int boxTNumber = 0;
            //把dgv变成DataTable
            DataTable dtECInfo = GetDgvToTable(dgvECData);
            DataTable dtECBox = GetDgvToTable(dgvBOX);
            for (int m = 0; m < dtECInfo.Rows.Count; m++)
            {
                //累加良品数量
                if (dtECInfo.Rows[m]["EBELN"].ToString().Substring(0, 2) == "10" || dtECInfo.Rows[m]["EBELN"].ToString().Substring(0, 4) == "1100")
                {
                    GNumber += Convert.ToInt32(dtECInfo.Rows[m]["QTY"].ToString());
                }
                //累加不良品数量
                if (dtECInfo.Rows[m]["EBELN"].ToString().Substring(0, 2) == "47" || dtECInfo.Rows[m]["EBELN"].ToString().Substring(0, 4) == "1179")
                {
                    SNumber += Convert.ToInt32(dtECInfo.Rows[m]["QTY"].ToString());
                }
                //计算总数量
                TNumber += Convert.ToInt32(dtECInfo.Rows[m]["QTY"].ToString());
            }
            for (int m = 0; m < dtECBox.Rows.Count; m++)
            {
                boxGNumber += Convert.ToInt32(dtECBox.Rows[m]["GNum"].ToString());

                boxSNumber += Convert.ToInt32(dtECBox.Rows[m]["SNum"].ToString());

                boxTNumber += Convert.ToInt32(dtECBox.Rows[m]["TotalNum"].ToString());
            }
            if (GNumber == boxGNumber && SNumber == boxSNumber && TNumber == boxTNumber)
            {

                lblwarning.Text = "比对成功！";

                for (int m = 0; m < dtECBox.Rows.Count; m++)
                {
                    //回填厂区仓别等信息
                    dtECBox.Rows[m]["WERKS"] = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtECBox.Rows[m]["LGORT"] = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    dtECBox.Rows[m]["MANDT"] = dtECInfo.Rows[0]["MANDT"].ToString();
                    dtECBox.Rows[m]["COMCD"] = dtECInfo.Rows[0]["COMCD"].ToString();

                }
                StorageData objStorageData = new StorageData(UserData);
                bool value = objStorageData.InsertECBox(dtECBox);
                if (!value)
                {
                    lblwarning.Text = "比对成功，但执行SP失败，请重新比对(直接点击比对按钮即可)";
                    btnCopare.Enabled = true;
                }
                btnCopare.Enabled = false;
            }
            else
            {
                if (GNumber != boxGNumber)
                {
                    lblwarning.Text = "良品数量不匹配，比对失败！";
                    return;
                }
                if (SNumber != boxSNumber)
                {
                    lblwarning.Text = "不良品数量不匹配，比对失败！";
                    return;
                }
                if (TNumber == boxTNumber)
                {
                    lblwarning.Text = "总数不匹配，比对失败！";
                    return;
                }
                lblwarning.Text = "比对失败！";
                return;
            }
        }
        #endregion

        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetInitial();
        }
        #endregion

        #region btnExit_Click

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion

        #region txtVbeln_KeyPress
        private void txtVbeln_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtVbeln.Text.Trim() == "")
                {
                    lblwarning.Text = "请输入EC单号！";
                }
                else
                {
                    btnConfirm_Click(null, null);
                }

            }
        }
        #endregion

        #region  dgvBOX_RowsRemoved
        private void dgvBOX_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            DataTable dtnowNum = new DataTable();
            boxTNum = 0;
            dtnowNum = GetDgvToTable(dgvBOX);
            for (int m = 0; m < dtnowNum.Rows.Count; m++)
            {
                boxTNum += Convert.ToInt32(dtnowNum.Rows[m]["TotalNum"]);
            }
            txtBoxTNum.Text = boxTNum.ToString();
        }
        # endregion

        #region txtLot_KeyPress
        private void txtLot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                lblwarning.Text = "";

                if (txtLot.Text.Trim() == "")
                {
                    lblwarning.Text = "LotCode不能为空！";
                    txtLot.Focus();
                }
                else
                {
                    txtBoxID.Enabled = true;
                    txtBoxID.Focus();
                    lblwarning.Text = "";
                }
            }
        }
        #endregion

    }
}
