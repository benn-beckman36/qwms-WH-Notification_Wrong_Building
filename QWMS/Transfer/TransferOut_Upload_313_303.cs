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
using System.Diagnostics;

namespace QWMS
{
    public partial class TransferOut_Upload_313_303 : Form
    {
        #region 初始设定
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strType = "";
        private string strFWerks = "";
        private string strDWerks = "";
        private string strFLgort = "";
        private string strDLgort = "";
        private string strLocat = "";
        private string strMatnr = "";
        private string strCharg = "";
        private string strProgid = "";
        private string strZeile = "";
        private string strInsmk = "";
        private bool Savelock = false;
        //private string strDouble = "";//双击类型
        private int i = 0;
        private DataTable dtMblnr = new DataTable();//单据信息
        private DataTable dtImport = new DataTable();//上传数据
        private DataTable dtData = new DataTable();//单笔获取数据
        private DataTable dtCombine = new DataTable();//整合数据
        private DataTable dtTransData = new DataTable();//WHDWN产生单据整合数据

        private Authority objAuthority;
        private PlantData objPlantData;
        private Replenishment objReplenishment;
        private Transfer objTransfer;
        private LogData objLogData;
        #endregion

        #region 变量
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
                return strFWerks;
            }
            set
            {
                strFWerks = value;
            }
        }
        public string Lgort
        {
            get
            {
                return strFLgort;
            }
            set
            {
                strFLgort = value;
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
        #endregion

        public TransferOut_Upload_313_303(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;


            try
            {
                objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);
                objTransfer = new Transfer(UserData, Werks, Lgort);
                objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, Progid);

                //检查权限
                if (!objReplenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlInsmk();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                    SetInit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Add New
        private void btnNew_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            txtLocat.Enabled = true;
            txtMatnr.Enabled = true;
            txtCharg.Enabled = true;
            cmbInsmk.Enabled = true;
        }
        #endregion

        #region 批量处理数据 File/Import 上传Excel
        private void btnFile_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFile.Text = ofdOpenFile.FileName;
                btnImport.Enabled = true;
                btnConfirm.Enabled = true;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            dtCombine.Clear();
            objTransfer = new Transfer(UserData);
            if (dtCombine.Columns.Count > 0)
            {
                dtCombine.Columns.Remove("ZEILE");
                dtCombine.Columns.Remove("FWERKS");
                dtCombine.Columns.Remove("FLGORT");
                dtCombine.Columns.Remove("MATNR");
                dtCombine.Columns.Remove("INSMK");
                dtCombine.Columns.Remove("CHARG");
                dtCombine.Columns.Remove("MENGE");
                dtCombine.Columns.Remove("DWERKS");
                dtCombine.Columns.Remove("DLGORT");
                dtCombine.Columns.Remove("LIFNR");
            }
            dtData.Clear();
            try
            {
                if (string.IsNullOrEmpty(this.txtFile.Text.Trim()))
                {
                    stsWarning.Text = "Please select one file!!";
                    return;
                }
                # region 校验格式
                string strFileName = this.txtFile.Text.Trim();
                string strFileType = strFileName.Substring(strFileName.LastIndexOf(".") + 1).ToLower();

                if (strFileType.ToUpper() != "XLS" && strFileType.ToUpper() != "XLSX")
                {
                    stsWarning.Text = "只能上传xls 和xlsx格式的EXCEL文件，请选择正确的文件格式！";
                }
                # endregion
                stsWarning.Text = "Please don't close the window ,Check the data...";
                QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
                //string strCmd = "select * from [Sheet1$]";
                //dtImport = objExcel.ExcelQuery(this.txtFile.Text.Trim(), strCmd);
                # region 获取EXCEL数据
                dtImport = objExcel.GetDataTableFromExcel(strFileName, true);
                if (dtImport.Rows.Count <= 0)
                {
                    stsWarning.Text = "未获取到Excel数据，请确认表格是否有数据";
                }
                #endregion

                dtCombine.Columns.Add("ZEILE");
                dtCombine.Columns.Add("FWERKS");
                dtCombine.Columns.Add("FLGORT");
                dtCombine.Columns.Add("MATNR");
                dtCombine.Columns.Add("LIFNR");
                dtCombine.Columns.Add("INSMK");
                dtCombine.Columns.Add("CHARG");
                dtCombine.Columns.Add("MENGE");
                dtCombine.Columns.Add("DWERKS");
                dtCombine.Columns.Add("DLGORT");

                foreach (DataRow dr in dtImport.Rows)
                {
                    //檢查是不是空值

                    if (string.IsNullOrEmpty(dr["item"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["料号"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["数量"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["调出仓"].ToString().Trim()) ||
                        string.IsNullOrEmpty(dr["调出厂"].ToString().Trim()))
                    {
                        if (rdb303.Checked)
                        {
                            if (string.IsNullOrEmpty(dr["接收厂"].ToString().Trim()))
                            {
                                stsWarning.Text = "料号、数量、调出厂区、调出仓别、调入厂区有空值，请重新核对";
                                dtCombine.Clear();
                                dtData.Clear();
                                return;
                            }
                            if (string.IsNullOrEmpty(dr["厂商代码"].ToString().Trim()))
                            {
                                stsWarning.Text = "厂商代码为空！";
                                dtCombine.Clear();
                                dtData.Clear();
                                return;
                            }
                        }
                        stsWarning.Text = "料号、数量、调出厂区、调出仓别有空值,请重新核对";
                        return;
                    }
                    else if (dr["item"].ToString() == "1")
                    {
                        i = 1;
                    }
                    else
                    {
                        ++i;
                        if (i > 20)
                        {
                            stsWarning.Text = "单笔调拨超过20，请重新确认上传数据";
                            return;
                        }
                    }

                    DataRow drCombine = dtCombine.NewRow();
                    drCombine["ZEILE"] = i.ToString("0000");
                    drCombine["FWERKS"] = dr["调出厂"];
                    drCombine["FLGORT"] = dr["调出仓"];
                    drCombine["MATNR"] = dr["料号"];
                    drCombine["LIFNR"] = dr["厂商代码"];
                    drCombine["INSMK"] = "";
                    drCombine["CHARG"] = dr["版本"];
                    drCombine["MENGE"] = dr["数量"];
                    if (rdb313.Checked)
                    {
                        drCombine["DWERKS"] = dr["调出厂"];
                    }
                    else
                    {
                        drCombine["DWERKS"] = dr["接收厂"];
                    }
                    drCombine["DLGORT"] = dr["接收仓"];
                    #region 增加判断料号是否存在
                    DataTable dtmatnr = new DataTable();
                    dtmatnr = objTransfer.returnmatnrexists(dr["料号"].ToString(), dr["版本"].ToString(), dr["调出厂"].ToString(), dr["调出仓"].ToString());
                    if (dtmatnr.Rows.Count == 0)
                    {
                        stsWarning.Text = "料号" + dr["料号"].ToString() + "不存在";
                        dtCombine.Clear();
                        dtData.Clear();
                        return;
                    }
                    #endregion
                    dtCombine.Rows.Add(drCombine);


                }
                DataTable dtLgort = dtCombine.DefaultView.ToTable(true, "FLGORT", "DLGORT");
                if (dtLgort.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtLgort.Rows)
                    {
                        #region 校验接收仓数据
                        if (dr["DLGORT"].ToString().Equals(""))
                        {
                        }
                        else
                        {
                            DataTable dtResult = objTransfer.CheckLgort(strType, dr["FLGORT"].ToString(), dr["DLGORT"].ToString());
                            if (dtResult.Rows[0][0].ToString().Equals("N"))
                            {
                                stsWarning.Text = "调出仓和接收仓不匹配，请重新确认数据！！！";
                                dtCombine.Clear();
                                dtData.Clear();
                                return;
                            }


                        }
                        #endregion
                    }
                }
                ShowDataView();
                stsWarning.Text = "Import OK!";
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Save 实时交互扣账，扣账文档回传给QWMS
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.stsWarning.Text = "";
                #region 校验303调拨同厂区无法开单
                foreach (DataRow dr in dtCombine.Rows)
                {
                if (strType == "SAP_303" && dr["FWERKS"].ToString() == dr["DWERKS"].ToString())
                {
                    stsWarning.Text = "303调拨相同厂区无法开单。";
                    return;
                }
                }
                #endregion
                #region 校验数据
                dtTransData.Rows.Clear();
                DataTable dtQueryQwmsStorage = new DataTable();
                DataTable dtSap = new DataTable();

                if (Savelock)//加一个变量，防止用户快速点击而生成两个文档导致扣账失败。
                {
                    return;
                }
                objTransfer = new Transfer(UserData, Werks, Lgort);
                foreach (DataRow dr in dtMblnr.Rows)
                {
                    dtQueryQwmsStorage = objTransfer.GetQwmsStorage(dr["MBLNR"].ToString(), strType);
                    if (dtQueryQwmsStorage.Rows.Count == 0)
                    {
                        #region 同步数据到SAP
                        btnSave.Enabled = false;
                        Savelock = true;
                        this.stsWarning.Text = dr["MBLNR"].ToString() + ":SAP开始入账，请勿关闭视窗";
                        MessageBox.Show("请点击确认，开始扣账");


                        dtSap = objTransfer.GetSapData(dr["MBLNR"].ToString(), strType);
                        #region 判断调入厂区是否一致
                        string kostl = dtSap.Rows[0]["KOSTL"].ToString();
                        foreach (DataRow drtemp in dtSap.Rows)
                        {
                            if (drtemp["KOSTL"].ToString() != kostl)
                            {
                                stsWarning.Text = "调出厂区不一致，请确认！";
                                Savelock = false;
                                return;
                            }
                        }
                        #endregion
                        if (dtSap.Rows.Count > 0)
                        {
                            DataSet ds = new DataSet();
                            DataTable dscopy = dtSap.Copy();
                            ds.Tables.Add(dscopy);
                            DataSet dsrec = new DataSet();
                            dsrec = SendToSAP(strType.Substring(strType.Length - 3, 3), ds);
                            objTransfer.UpdateTransRemark(dsrec, dr["MBLNR"].ToString());

                            if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "Y")
                            {
                                stsWarning.Text = dr["MBLNR"].ToString() + "扣账成功";
                                MessageBox.Show(dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString());
                            }
                            else if (dsrec.Tables["EP_FLAG"].Rows[0]["FLAG"].ToString() == "N")
                            {
                                stsWarning.Text = "扣账失败";
                                MessageBox.Show(dsrec.Tables["EP_MSG"].Rows[0]["MSG"].ToString());
                            }

                        }
                        Savelock = false;
                        #endregion
                    }
                    else
                    {
                        stsWarning.Text = "调拨数量不足";
                        objTransfer.UpdateMengeRemark(dr["MBLNR"].ToString());
                    }
                    dtTransData = objTransfer.QuerySimulationData(dtMblnr, strType);
                    dtTransData.DefaultView.Sort = "MBLNR";
                    dtTransData = dtTransData.DefaultView.ToTable();
                    ShowDataGridView();
                }
                #endregion
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
            #region 扣账失败，到TransferOut_Query页面查询状态
            #endregion
        }
        #endregion

        #region Confirm 产生虚拟单据到WHDWN表
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";               
                dtMblnr.Rows.Clear();
                //313 303开调拨单 当月最后一天20点以后添加弹窗提醒
                DateTime lastDay = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-1")).AddHours(-4);
                DateTime Daynow = Convert.ToDateTime(DateTime.Now.ToString());
                if (Daynow > lastDay)
                {
                    DialogResult result;
                    result = MessageBox.Show("今日月结，请确认是否开单", "提示!", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                #region 产生虚拟单据 dtMblnr
                if (rdb303.Checked || rdb313.Checked)
                {
                    #region 产生303虚拟扣账单据
                    string strType;
                    if (rdb303.Checked)
                    {
                        strType = "SAP_303";
                    }
                    else
                    {
                        strType = "SAP_313";
                    }
                    foreach (DataRow dr in dtCombine.Rows)
                    {
                        if (dr["DLGORT"].ToString() == "")
                        {
                            stsWarning.Text = "调入厂区有空值！请确认！";
                            return;
                        }
                        if (strType == "SAP_303" && dr["LIFNR"].ToString() == "")
                        {
                            stsWarning.Text = "厂商代码为空！";
                            return;
                        }
                        if (Convert.ToInt32(dr["ZEILE"].ToString()) > 20)
                        {
                            stsWarning.Text = "itm不能超过20！请确认！";
                            return;
                        }
                        if (strType == "SAP_313" && dr["FLGORT"].ToString() == dr["DLGORT"].ToString())
                        {
                            stsWarning.Text = "313调拨相同厂区、相同仓别无法开单。";
                            return;
                        }
                        if (strType == "SAP_303" && dr["FWERKS"].ToString() == dr["DWERKS"].ToString())
                        {
                            stsWarning.Text = "303调拨相同厂区无法开单。";
                            return;
                        }
                    }
                    dtMblnr = objTransfer.ProduceTransSimulationData(dtCombine, strType);
                    if (dtMblnr.Rows.Count > 0)
                    {
                        stsWarning.Text = "已产生扣帐单据...";
                        btnConfirm.Enabled = false;
                    }
                    else
                    {
                        stsWarning.Text = "产生扣账单据失败，请确认!! (Error: " + objTransfer.ERRMSG + " )";
                        return;
                    }

                    #region 显示数据 dtTransData
                    dtTransData = objTransfer.QuerySimulationData(dtMblnr, strType);
                    dtTransData.DefaultView.Sort = "MBLNR";
                    dtTransData = dtTransData.DefaultView.ToTable();
                    ShowDataGridView();
                    #endregion

                    #endregion
                }
                else
                {
                    stsWarning.Text = "Please choose a Type";
                    return;
                }
                #endregion
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region Refresh/Exit
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetInit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 单笔数据实现 DoubleClick

        private void txtLocat_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "请输入厂区仓别";
                    return;
                }
                else
                {
                    Transfer_LocationSelect objTransfer_LocationSelect = new Transfer_LocationSelect(UserData, Progid, Werks, Lgort);
                    objTransfer_LocationSelect.ShowDialog();
                    txtLocat.Text = objTransfer_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message + "<-txtLocat_DoubleClick";
                return;
            }
        }

        private void txtMatnr_DoubleClick(object sender, EventArgs e)
        {
            doubleClick();
        }

        private void txtCharg_DoubleClick(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            if (txtMatnr.Text.ToString() != "")
            {
                doubleClick();
            }
            else
            {
                stsWarning.Text = "请输入料号";
                return;
            }
        }
        #endregion

        #region ShowData Status/Werks/Lgort
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
                Authority objAuthority = new Authority(UserData);
                stsWarning.Text = string.Empty;
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strFWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strFWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strFLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort.Items.Clear();
                    strFLgort = string.Empty;
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strFLgort && strFLgort != "")
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

        #region 绑定库位
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
                cmbInsmk.SelectedIndex = 2;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
            }
        }
        #endregion

        #endregion

        #region SelectedIndexChanged

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
            cmbLgort.SelectedIndex = 0;

        }

        private void cmbWerks_SelectedValueChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
            cmbLgort.SelectedIndex = 0;
        }

        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.cmbInsmk.Enabled = true;
            cmbInsmk.SelectedIndex = 2;
            ShowDdlInsmk();
        }

        #endregion

        #region ShowDataGridView WHDWN表数据 dtTransData
        private void ShowDataGridView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "ZEILE";
                dgvcItem.HeaderText = "Item";
                dgvcItem.ReadOnly = true;
                this.gvData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcFwerks = new DataGridViewTextBoxColumn();
                dgvcFwerks.DataPropertyName = "WERKS";
                dgvcFwerks.HeaderText = "调出厂区";
                dgvcFwerks.ReadOnly = true;
                this.gvData.Columns.Add(dgvcFwerks);

                DataGridViewTextBoxColumn dgvcFlgort = new DataGridViewTextBoxColumn();
                dgvcFlgort.DataPropertyName = "LGORT";
                dgvcFlgort.HeaderText = "调出仓别";
                dgvcFlgort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcFlgort);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcVersion = new DataGridViewTextBoxColumn();
                dgvcVersion.DataPropertyName = "CHARG";
                dgvcVersion.HeaderText = "版本";
                dgvcVersion.ReadOnly = true;
                dgvcVersion.Width = 80;
                this.gvData.Columns.Add(dgvcVersion);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "调出数量";
                dgvcMenge.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcDwerks = new DataGridViewTextBoxColumn();
                dgvcDwerks.DataPropertyName = "KOSTL";
                dgvcDwerks.HeaderText = "调入厂区";
                dgvcDwerks.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDwerks);

                DataGridViewTextBoxColumn dgvcDlgort = new DataGridViewTextBoxColumn();
                dgvcDlgort.DataPropertyName = "UMLGO";
                dgvcDlgort.HeaderText = "调入仓别";
                dgvcDlgort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDlgort);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "调拨单号";
                dgvcMblnr.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcOtqty = new DataGridViewTextBoxColumn();
                dgvcOtqty.DataPropertyName = "OTQTY";
                dgvcOtqty.HeaderText = "扣账数量";
                dgvcOtqty.ReadOnly = true;
                this.gvData.Columns.Add(dgvcOtqty);

                DataGridViewTextBoxColumn dgvcRemak1 = new DataGridViewTextBoxColumn();
                dgvcRemak1.DataPropertyName = "REMAK1";
                dgvcRemak1.HeaderText = "扣账编号";
                dgvcRemak1.ReadOnly = true;
                this.gvData.Columns.Add(dgvcRemak1);

                DataGridViewTextBoxColumn dgvcDate = new DataGridViewTextBoxColumn();
                dgvcDate.DataPropertyName = "CRDAT";
                dgvcDate.HeaderText = "创建时间";
                dgvcDate.ReadOnly = true;
                dgvcDate.Width = 100;
                this.gvData.Columns.Add(dgvcDate);

                DataGridViewTextBoxColumn dgvcStatus = new DataGridViewTextBoxColumn();
                dgvcStatus.DataPropertyName = "FLAGE";
                dgvcStatus.HeaderText = "状态";
                dgvcStatus.ReadOnly = true;
                dgvcStatus.Width = 50;
                this.gvData.Columns.Add(dgvcStatus);

                this.gvData.DataSource = dtTransData;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";
                gvData.ClearSelection();
                gvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }

        }
        #endregion

        #region ShowDataView 上传数据 dtCombine
        private void ShowDataView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "ZEILE";
                dgvcItem.HeaderText = "Item";
                dgvcItem.ReadOnly = true;
                this.gvData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "厂商代码";
                dgvcLifnr.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "库别";
                dgvcInsmk.ReadOnly = true;
                this.gvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcVersion = new DataGridViewTextBoxColumn();
                dgvcVersion.DataPropertyName = "CHARG";
                dgvcVersion.HeaderText = "版本";
                dgvcVersion.ReadOnly = true;
                dgvcVersion.Width = 80;
                this.gvData.Columns.Add(dgvcVersion);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "调拨数量";
                dgvcMenge.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcFwerks = new DataGridViewTextBoxColumn();
                dgvcFwerks.DataPropertyName = "FWERKS";
                dgvcFwerks.HeaderText = "调出厂区";
                dgvcFwerks.ReadOnly = true;
                this.gvData.Columns.Add(dgvcFwerks);

                DataGridViewTextBoxColumn dgvcFlgort = new DataGridViewTextBoxColumn();
                dgvcFlgort.DataPropertyName = "FLGORT";
                dgvcFlgort.HeaderText = "调出仓别";
                dgvcFlgort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcFlgort);

                DataGridViewTextBoxColumn dgvcDwerks = new DataGridViewTextBoxColumn();
                dgvcDwerks.DataPropertyName = "DWERKS";
                dgvcDwerks.HeaderText = "接收厂区";
                dgvcDwerks.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDwerks);

                DataGridViewTextBoxColumn dgvcDlgort = new DataGridViewTextBoxColumn();
                dgvcDlgort.DataPropertyName = "DLGORT";
                dgvcDlgort.HeaderText = "接收仓别";
                dgvcDlgort.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDlgort);

                this.gvData.DataSource = dtCombine;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";
                gvData.ClearSelection();
                gvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }
        #endregion

        #region 调整布局大小
        private void Manage_StorageStatus_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion

        private void txtLocat_TextChanged(object sender, EventArgs e)
        {
            strLocat = txtLocat.Text.Trim();
        }

        private void txtMatnr_TextChanged(object sender, EventArgs e)
        {
            strMatnr = txtMatnr.Text.Trim();
        }

        private void txtCharg_TextChanged(object sender, EventArgs e)
        {
            strCharg = txtCharg.Text.Trim();
        }

        private void rdb313_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb313.Checked)
            {
                strType = "SAP_313";
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = true;
                this.txtWerks.Enabled = false;
            }
        }

        private void rdb303_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb303.Checked == true)
            {
                strType = "SAP_303";
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = true;
                this.txtWerks.Enabled = true;
            }
        }

        #region 调用小窗口
        private void doubleClick()
        {
            btnNew.Enabled = true;
            btnConfirm.Enabled = true;
            if (strType == "SAP_303" && cmbWerks.Text.ToString() == txtWerks.Text.ToString())
            {
                stsWarning.Text = "303调拨相同厂区无法开单。";
                return;
            }

            if (txtLgort.Text == "")
            {
                stsWarning.Text = "调入仓别为空！";
                return;
            }
            if (cmbWerks.SelectedIndex != -1)
            {
                strFWerks = cmbWerks.SelectedItem.ToString();
            }
            else
            {
                strFWerks = string.Empty;
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strFLgort = cmbLgort.SelectedItem.ToString();
            }
            else
            {
                strFLgort = string.Empty;
            }
            if (cmbInsmk.SelectedIndex != -1)
            {
                strInsmk = cmbInsmk.SelectedItem.ToString();
            }
            else
            {
                strInsmk = string.Empty;
            }
            if (strFWerks == "" || strFLgort == "" || strInsmk == "")
            {
                stsWarning.Text = "Plant, storage or Insmk can't be empty!!";
                return;
            }
            if (rdb303.Checked)
            {
                if (txtWerks.Text == "")
                {
                    stsWarning.Text = "请输入调入厂区";
                    return;
                }
            }
            else
            {
                //313调拨同公司别同厂区调拨
                strDWerks = cmbWerks.SelectedItem.ToString();
                txtWerks.Text = strDWerks;
            }
            if (txtLgort.Text.Trim() != "")
            {
                #region 校验接收仓数据
                DataTable dtResult = objTransfer.CheckLgort(strType, strFLgort, txtLgort.Text.Trim());
                if (dtResult.Rows[0][0].ToString().Equals("Y"))
                {
                    strDLgort = txtLgort.Text.Trim();
                }
                else
                {
                    stsWarning.Text = "调出仓和接收仓不匹配，请重新选择！！！";
                    return;
                }
                #endregion
                #region 检查调入仓是否存在
                DataTable dtlgort = new DataTable();
                if (strType == "SAP_313")
                {
                    dtlgort = objPlantData.CheckLgort(cmbWerks.SelectedItem.ToString(), txtLgort.Text, Usrnm);
                }
                else if (strType == "SAP_303")
                {
                    dtlgort = objPlantData.CheckLgort(txtWerks.Text, txtLgort.Text, Usrnm);
                }
                if (dtlgort.Rows.Count <= 0)
                {
                    stsWarning.Text = "调入仓不存在，请重试！";
                    return;
                }
                #endregion
            }
            if (txtLocat.Text.ToString() != "")
            {
                strLocat = txtLocat.Text.Trim();
            }
            if (txtMatnr.Text.ToString() != "")
            {
                strMatnr = txtMatnr.Text.Trim();
            }
            if (txtCharg.Text.ToString() != "")
            {
                strCharg = txtCharg.Text.Trim();
            }
            getData(strFWerks, strFLgort, strInsmk, strLocat, strMatnr, strCharg);
        }
        #endregion

        #region 小窗口获取库存数据
        private void getData(string varWerks, string varLgort, string varInsmk, string varLocat, string varMatnr, string varCharg)
        {
            this.cmbWerks.Enabled = false;
            this.cmbLgort.Enabled = false;
            this.cmbInsmk.Enabled = false;
            this.txtWerks.Enabled = false;
            this.txtLgort.Enabled = false;
            this.txtLocat.Enabled = false;
            this.txtMatnr.Enabled = false;
            this.txtCharg.Enabled = false;
            this.txtFile.Enabled = false;
            int intZeile = 0;
            TransferData objTransferData = new TransferData(UserData, varWerks, varLgort, varInsmk, varLocat, varMatnr, varCharg, Progid);
            objTransferData.ShowDialog();
            dtData = objTransferData.TransData;
            if (dtCombine.Rows.Count > 0)
            {
                intZeile = dtCombine.Rows.Count;
            }
            else if (dtCombine.Columns.Contains("ZEILE"))
            {
            }
            else
            {
                dtCombine.Columns.Add("ZEILE");
                dtCombine.Columns.Add("FWERKS");
                dtCombine.Columns.Add("FLGORT");
                dtCombine.Columns.Add("MATNR");
                dtCombine.Columns.Add("LIFNR");
                dtCombine.Columns.Add("INSMK");
                dtCombine.Columns.Add("CHARG");
                dtCombine.Columns.Add("MENGE");
                dtCombine.Columns.Add("DWERKS");
                dtCombine.Columns.Add("DLGORT");
            }
            foreach (DataRow dr in dtData.Rows)
            {
                strZeile = (++intZeile).ToString("0000");
                DataRow drCombine = dtCombine.NewRow();
                drCombine["ZEILE"] = strZeile;
                drCombine["FWERKS"] = dr["WERKS"];
                drCombine["FLGORT"] = dr["LGORT"];
                drCombine["MATNR"] = dr["MATNR"];
                drCombine["LIFNR"] = dr["LIFNR"];
                drCombine["INSMK"] = dr["INSMK"];
                drCombine["CHARG"] = dr["CHARG"];
                drCombine["MENGE"] = dr["ALQTY"];
                if (rdb313.Checked)
                {
                    drCombine["DWERKS"] = strDWerks;
                    drCombine["DLGORT"] = strDLgort;
                }
                else if (rdb303.Checked)
                {
                    drCombine["DWERKS"] = txtWerks.Text;
                    drCombine["DLGORT"] = txtLgort.Text;
                }

                dtCombine.Rows.Add(drCombine);
            }

            ShowDataView();
            this.txtLocat.Text = "";
            this.txtMatnr.Text = "";
            this.txtCharg.Text = "";
        }
        #endregion

        public DataSet SendToSAP(string varTypeToSap, DataSet ds)
        {
            try
            {
                DataSet dsResult = new DataSet();
                MM.MM_Service obj = new QWMS.MM.MM_Service();
                dsResult = obj.Z_MM_RFC_DIAOBO_QWMS_TO_SAP(varTypeToSap, ds);
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SetInit()
        {
            stsWarning.Text = "";
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.txtWerks.Enabled = true;
            this.txtLgort.Enabled = true;
            this.txtLocat.Enabled = true;
            this.txtMatnr.Enabled = true;
            this.txtCharg.Enabled = true;
            this.txtFile.Enabled = true;
            this.rdb303.Checked = false;
            this.rdb313.Checked = false;
            this.dtData.Clear();
            this.dtCombine.Clear();
            this.dtImport.Rows.Clear();
            this.dtMblnr.Rows.Clear();
            this.dtTransData.Rows.Clear();
            //        this.cmbWerks.SelectedIndex = 0;
            //        this.cmbLgort.SelectedIndex = 0;
            //        this.cmbInsmk.SelectedIndex = 2;
            this.txtWerks.Text = "";
            this.txtLgort.Text = "";
            this.txtLocat.Text = "";
            this.txtCharg.Text = "";
            this.txtMatnr.Text = "";
            this.txtFile.Text = "";
            Savelock = false;
            this.groupBox1.Enabled = false;
            this.groupBox2.Enabled = false;
            this.cmbInsmk.Enabled = false;
            this.btnImport.Enabled = false;
            this.btnNew.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnConfirm.Enabled = true;
            this.gvData.Columns.Clear();
            dtCombine.Clear();
            dtData.Clear();
        }

        private void lnkSample_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "313_303模板.xlsx";
                try
                {
                    Process.Start("Excel", strPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"无法打开文件，请手动打开" + strPath);
                }
            }
            else
            {
                MessageBox.Show("未在数据库维护模板路径，请联系QWMS负责人");
            }
        }

        private void LnkSOP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, Progid);
            DataTable dtDocumentPathTemp = new DataTable();
            dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
            if (dtDocumentPathTemp.Rows.Count > 0)
            {
                string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + @"SOP\QWMS_313_303_351开单自动扣帐SOP.docx";
                try
                {
                    Process.Start("winword.exe", strPath);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"无法打开文件，请手动打开" + strPath);
                }
            }
            else
            {
                MessageBox.Show("未在数据库维护模板路径，请联系QWMS负责人");
            }
        }





    }
}
