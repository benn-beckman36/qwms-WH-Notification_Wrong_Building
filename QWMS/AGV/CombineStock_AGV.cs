using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using NPOI.Util.Collections;
using QCI.QWMS;
using QWMS.Common;
using QWMS_CommonInfo_Biz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NPOI.HSSF.Util.HSSFColor;
using static System.Windows.Forms.LinkLabel;

namespace QWMS.AGV
{
    public partial class CombineStock_AGV : Form
    {
        #region 定义变量

        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";

        private string strWorkstation = ""; //工作站
        private string strStoragetype = ""; //入库类型
        private string strShelfsize = ""; //料架尺寸
        private string strShelftype = ""; //料架类型
        private string strMARNO = ""; //料架编码
        private string strLocat = ""; //储位

        private string strTaskNo = ""; //任务单号
        private int strreqCode = 0; //请求单号

        private string strAGVSTATE = ""; //当前货架使用状态

        private DataTable dtBarCodeInfo;
        private DataRow drBarCodeInfo;
        private DataTable dtTempNew;
        private DataTable dtAGVOrder;

        //private SQLAccess objDB;
        private StorageIn objStorageIn;
        private StorageData objStorageData;
        private PlantData objPlantData;
        private Authority objAuthority;
        private AGVStorageIn objAGVStorageIn;
        private AGVApi objAGVApi;
        private Replenishment objReplenishment;

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

        public string Workstation
        {
            get
            {
                return strWorkstation;
            }
            set
            {
                strWorkstation = value;
            }
        }

        public string Storagetype
        {
            get
            {
                return strStoragetype;
            }
            set
            {
                strStoragetype = value;
            }
        }

        public string Shelfsize
        {
            get
            {
                return strShelfsize;
            }
            set
            {
                strShelfsize = value;
            }
        }

        public string Shelftype
        {
            get
            {
                return strShelftype;
            }
            set
            {
                strShelftype = value;
            }
        }
        #endregion

        #region 构造函数
        public CombineStock_AGV()
        {
            InitializeComponent();
        }

        public CombineStock_AGV(UserInfo _UserData, string strProgid)
            : this()
        {
            UserData = _UserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;

            try
            {
                objStorageIn = new StorageIn(UserData, strProgid);
                objStorageData = new StorageData(UserData, strWerks, strLgort);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objAGVStorageIn = new AGVStorageIn(UserData, strProgid);
                objAGVApi = new AGVApi(UserData);
                objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strProgid);

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
                    ShowDdlWorkstation();
                    ShowDdlStoragetype();
                    ShowDdlShelfsize();
                    GetBarCodeInfo();

                    if (cmbWorkstation.Items.Count > 0)
                    {
                        this.cmbWorkstation.SelectedIndex = 0;
                    }
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

        #region ShowDataGrid
        public void ShowDataGridAGVOrder()
        {
            this.dgvAGVOrder.AutoGenerateColumns = false;
            this.dgvAGVOrder.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 60;
                dgvcWERKS.ReadOnly = true;
                this.dgvAGVOrder.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                this.dgvAGVOrder.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcPLACE = new DataGridViewTextBoxColumn();
                dgvcPLACE.DataPropertyName = "PLACE";
                dgvcPLACE.HeaderText = "Workstation";
                dgvcPLACE.Width = 60;
                dgvcPLACE.ReadOnly = true;
                this.dgvAGVOrder.Columns.Add(dgvcPLACE);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "TASKNO";
                dgvcTASKID.Width = 180;
                dgvcTASKID.ReadOnly = true;
                this.dgvAGVOrder.Columns.Add(dgvcTASKID);

                DataGridViewTextBoxColumn dgvcREQNO = new DataGridViewTextBoxColumn();
                dgvcREQNO.DataPropertyName = "REQNO";
                dgvcREQNO.HeaderText = "REQNO";
                dgvcREQNO.ReadOnly = true;
                dgvcREQNO.Width = 60;
                this.dgvAGVOrder.Columns.Add(dgvcREQNO);

                DataGridViewTextBoxColumn dgvcMARNO = new DataGridViewTextBoxColumn();
                dgvcMARNO.DataPropertyName = "MARNO";
                dgvcMARNO.HeaderText = "MARNO";
                dgvcMARNO.ReadOnly = true;
                dgvcMARNO.Width = 60;
                this.dgvAGVOrder.Columns.Add(dgvcMARNO);

                DataGridViewTextBoxColumn dgvcSTATE = new DataGridViewTextBoxColumn();
                dgvcSTATE.DataPropertyName = "Shelf_state";
                dgvcSTATE.HeaderText = "Status";
                dgvcSTATE.ReadOnly = true;
                dgvcSTATE.Width = 60;
                this.dgvAGVOrder.Columns.Add(dgvcSTATE);

                DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                dgvcCOMCD.DataPropertyName = "COMCD";
                dgvcCOMCD.HeaderText = "COMCD";
                dgvcCOMCD.ReadOnly = true;
                dgvcCOMCD.Width = 60;
                this.dgvAGVOrder.Columns.Add(dgvcCOMCD);

                dgvAGVOrder.DataSource = dtAGVOrder;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridAGVOrder()");
            }
        }

        public void ShowDataGridBarCodeInfo()
        {
            this.dgvBarCodeInfo.AutoGenerateColumns = false;
            this.dgvBarCodeInfo.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 60;
                dgvcWERKS.ReadOnly = true;
                this.dgvBarCodeInfo.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 60;
                dgvcLGORT.ReadOnly = true;
                this.dgvBarCodeInfo.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "LOCAT";
                dgvcLOCAT.ReadOnly = true;
                dgvcLOCAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.ReadOnly = true;
                dgvcMATNR.Width = 120;
                this.dgvBarCodeInfo.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "INSMK";
                dgvcINSMK.ReadOnly = true;
                dgvcINSMK.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcINSMK);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.ReadOnly = true;
                dgvcCHARG.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.ReadOnly = true;
                dgvcMENGE.Width = 60;
                this.dgvBarCodeInfo.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.ReadOnly = true;
                dgvcMBLNR.Width = 120;
                this.dgvBarCodeInfo.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "LIFNR";
                dgvcLIFNR.ReadOnly = true;
                dgvcLIFNR.Width = 80;
                this.dgvBarCodeInfo.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcINDAT = new DataGridViewTextBoxColumn();
                dgvcINDAT.DataPropertyName = "INDAT";
                dgvcINDAT.HeaderText = "INDAT";
                dgvcINDAT.ReadOnly = true;
                dgvcINDAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcINDAT);

                DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                dgvcSERNO.DataPropertyName = "SERNO";
                dgvcSERNO.HeaderText = "SERNO";
                dgvcSERNO.ReadOnly = true;
                dgvcSERNO.Width = 180;
                this.dgvBarCodeInfo.Columns.Add(dgvcSERNO);

                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "DACOD";
                dgvcDACOD.ReadOnly = true;
                dgvcDACOD.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcDACOD);

                DataGridViewTextBoxColumn dgvcVEDAT = new DataGridViewTextBoxColumn();
                dgvcVEDAT.DataPropertyName = "VEDAT";
                dgvcVEDAT.HeaderText = "VEDAT";
                dgvcVEDAT.ReadOnly = true;
                dgvcVEDAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcVEDAT);

                DataGridViewTextBoxColumn dgvcLOCOD = new DataGridViewTextBoxColumn();
                dgvcLOCOD.DataPropertyName = "LOCOD";
                dgvcLOCOD.HeaderText = "LOCOD";
                dgvcLOCOD.ReadOnly = true;
                dgvcLOCOD.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcLOCOD);

                DataGridViewTextBoxColumn dgvcEXPDAT = new DataGridViewTextBoxColumn();
                dgvcEXPDAT.DataPropertyName = "EXPDAT";
                dgvcEXPDAT.HeaderText = "EXPDAT";
                dgvcEXPDAT.ReadOnly = true;
                dgvcEXPDAT.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcEXPDAT);

                DataGridViewTextBoxColumn dgvcTASKID = new DataGridViewTextBoxColumn();
                dgvcTASKID.DataPropertyName = "TASKID";
                dgvcTASKID.HeaderText = "TASKID";
                dgvcTASKID.ReadOnly = true;
                dgvcTASKID.Width = 90;
                this.dgvBarCodeInfo.Columns.Add(dgvcTASKID);

                dgvBarCodeInfo.DataSource = dtBarCodeInfo;
                dgvBarCodeInfo.Sort(dgvcMBLNR, ListSortDirection.Ascending); //按照单号升序排序

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridBarCodeInfo()");
            }
        }
        #endregion

        #region Basic Option
        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }
        #endregion

        #region 绑定厂区
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

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region 绑定仓别
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
        #endregion

        #region 绑定工作站
        private void ShowDdlWorkstation()
        {
            DataTable dtWorkstation = new DataTable();
            try
            {
                cmbWorkstation.Items.Clear();
                dtWorkstation = objAGVStorageIn.GetAGVWorkStation();
                for (int i = 0; i < dtWorkstation.Rows.Count; i++)
                {
                    cmbWorkstation.Items.Add(dtWorkstation.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWorkstation()");
            }
        }
        #endregion

        #region 绑定入库类型

        private void ShowDdlStoragetype()
        {
            try
            {
                DataTable dtStoragetype = new DataTable();

                dtStoragetype.Columns.AddRange(new DataColumn[] { new DataColumn("CNTEXT") });

                dtStoragetype.Rows.Add(new string[] { "CombineStock" });

                cmbStoragetype.DisplayMember = "CNTEXT";
                cmbStoragetype.ValueMember = "CNTEXT";
                cmbStoragetype.DataSource = dtStoragetype;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStoragetype()");
            }
        }
        #endregion

        #region 绑定料架尺寸

        private void ShowDdlShelfsize()
        {
            try
            {
                DataTable dtShelfsize = new DataTable();

                dtShelfsize.Columns.AddRange(new DataColumn[] { new DataColumn("CNTEXT") });

                dtShelfsize.Rows.Add(new string[] { "7" });
                dtShelfsize.Rows.Add(new string[] { "13" });

                cmbShelfsize.DisplayMember = "CNTEXT";
                cmbShelfsize.ValueMember = "CNTEXT";
                cmbShelfsize.DataSource = dtShelfsize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlShelfsize()");
            }
        }
        #endregion

        #region 绑定料架类型

        private void ShowDdlShelftype()
        {
            try
            {
                string strShelftype = objAGVStorageIn.getShelftype(Werks, Lgort);
                if (strShelftype == "BULK")
                {
                    txtShelftype.Text = "BULK";
                }
                else
                {
                    txtShelftype.Text = "ALL";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlShelftype()");
            }
        }
        #endregion

        #region 开始作业
        private void btnStartJobs_Click(object sender, EventArgs e)
        {
            ShowDdlShelftype();

            List<string> docNumbers = new List<string>();
            DataTable dtMARNO = new DataTable();
            strWorkstation = cmbWorkstation.Text.ToString().Trim();
            strStoragetype = cmbStoragetype.SelectedValue.ToString().Trim();
            strShelfsize = cmbShelfsize.SelectedValue.ToString().Trim();
            strShelftype = txtShelftype.Text.ToString().Trim();
            strMARNO = txtMARNO.Text.ToString().Trim(); //料架编码

            this.stsWarning.Text = string.Empty;
            try
            {
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
                    stsWarning.Text = "厂区仓别不能为空!";
                    return;
                }
                if (strWorkstation == "" || strStoragetype == "" || strShelfsize == "" || strShelftype == "" || strMARNO=="")
                {
                    stsWarning.Text = "工作站料架属性不能为空!";
                    return;
                }

                if (strMARNO == "DY0")
                {
                    stsWarning.Text = "当前为DY储位不叫小车!";
                }
                else 
                { 

                    //1、查询工作站，判断是否分配
                    dtAGVOrder = objAGVStorageIn.QueryAGVWorkStation("", "", Workstation, Comcd, "", 0);
                    if (dtAGVOrder.Rows.Count <= 0)
                    {
                        //生成任务编号 厂区+仓别+年月日+item(6码)
                        //生成任务序号 厂区+仓别+年月日+item(6码) + item(6码)
                        string strHeader = Werks + Lgort + DateTime.Now.ToString("yyyyMMdd");
                        int strSerno = objAGVStorageIn.GetAGVTaskNo();
                        strTaskNo = strHeader + (strSerno).ToString("000000"); //任务编号
                        strreqCode = 1; //任务序号

                        // 校验货架编号是否正确
                        if (ValidateShelfNumber(strMARNO))
                        {
                            docNumbers.Add(strMARNO);
                        }
                        else
                        {
                            stsWarning.Text = "货架编码错误";
                            SetErrNotice();
                            return;
                        }

                        var outboundTask = new
                        {
                            plant = Werks,
                            storage = Lgort,
                            work_station = Workstation,
                            doc_type = strStoragetype,
                            detail = docNumbers.Select(docNumber => new {shelf_number = docNumber }).ToList(),//料架编号,
                            task_id = strTaskNo,
                            task_sequence = strreqCode,
                            priority = "4"
                        };

                        string strResult = objAGVApi.AGVHttpRequest(strStoragetype, "outboundTask", JsonConvert.SerializeObject(outboundTask));
                        if (!string.IsNullOrEmpty(strResult))
                        {
                            inboundTaskTaskData response = JsonConvert.DeserializeObject<inboundTaskTaskData>(strResult);
                            string strtask_id = response.task_id;
                            int ittask_sequence = response.task_status;
                            int strtask_sequence = response.task_sequence;
                            if (strtask_id == strTaskNo && strtask_sequence == strreqCode)
                            {
                                DataTable emptyTable = new DataTable();
                                //回执成功，创建AGV指令到WHAGV，锁定扣账单据WHDWN
                                if (objAGVStorageIn.InsertWHAGV(Werks, Lgort, Workstation, strTaskNo, strreqCode, "", "0", emptyTable))
                                {
                                    dtAGVOrder = objAGVStorageIn.QueryAGVWorkStation(Werks, Lgort, Workstation, Comcd, strTaskNo, 0);
                                    ShowDataGridAGVOrder();
                                    SetOKNotice();
                                    ChangeBarcodeToLocat();
                                    stsWarning.Text = "创建AGV任务成功";
                                }
                                else
                                {
                                    stsWarning.Text = "创建AGV任务失败";
                                    SetErrNotice();
                                    return;
                                }
                            }
                            else
                            {
                                stsWarning.Text = "响应失败，taskid: " + strtask_id + " task_sequence: " + strtask_sequence;
                                SetErrNotice();
                                return;
                            }
                        }
                    }
                    else
                    {
                        stsWarning.Text = "工作站: " + Workstation + " 被占用";
                        SetErrNotice();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetErrNotice();
                return;
            }
        }
        #endregion

        #region 校验货架编号是否正确的方法
        public static bool ValidateShelfNumber(string strMARNO)
        {
            // 校验是否为数字
            if (!IsNumeric(strMARNO))
            {
                return false;
            }

            // 校验是否为三码
            if (strMARNO.Length != 3)
            {
                return false;
            }
            return true;
        }

        // 判断字符串是否为数字的方法
        public static bool IsNumeric(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        // 将字符串转换为 DataTable 的方法
        static DataTable ConvertToDataTable(string strMARNO)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("shelf_number", typeof(string));
            dataTable.Rows.Add(strMARNO);
            return dataTable;
        }
        #endregion

        #region Sound Remind
        public void SetErrNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\ERROR.wav");
            sp.Play();
        }

        public void SetOKNotice()
        {
            SoundPlayer sp = new SoundPlayer(@"Sound\BIU.wav");
            sp.Play();
        }

        public void ChangeBarcodeToLocat()
        {
            this.txtBarCode.Text = "";
            this.txtBarCode.Enabled = false;
            this.txtLocat.Text = "";
            this.txtLocat.Enabled = true;
            this.txtLocat.Focus();
        }

        public void ChangeLocatToBarcode()
        {
            this.txtBarCode.Text = "";
            this.txtBarCode.Enabled = true;
            this.txtLocat.Text = "";
            this.txtLocat.Enabled = false;
            this.txtBarCode.Focus();
        }
        #endregion

        #endregion

        #region btnFlip_Click
        private void btnFlip_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            try
            {
                //更新WHAGV任务序号
                objAGVStorageIn.UpdateAGVShelf(strWerks, strLgort, strTaskNo, strreqCode);
                strreqCode = strreqCode + 1; //任务序号累加

                //1、调用AGV货架反面接口
                Agv_shelfFlip head = new Agv_shelfFlip
                {
                    plant = Werks, //所属厂区 32 (必填)
                    storage = Lgort, //仓库别 32 (必填)
                    work_station = Workstation, //工作站别 32 (必填)
                    shelf_id = strStoragetype, //货架编号 32 (非必填)
                    task_id = strTaskNo, //任务编号 32 (必填)
                    task_type = "", //任务类型 32 (非必填)
                    task_sequence = strreqCode, //任务序号 (必填)
                };
                string strResult = objAGVApi.AGVHttpRequest(strStoragetype, "shelfFlip", JsonConvert.SerializeObject(head));

                if (!string.IsNullOrEmpty(strResult))
                {
                    inboundTaskTaskData response = JsonConvert.DeserializeObject<inboundTaskTaskData>(strResult);
                    string strtask_id = response.task_id;
                    int ittask_sequence = response.task_status;
                    int strtask_sequence = response.task_sequence;
                    if (strtask_id == strTaskNo && strtask_sequence == strreqCode)
                    {
                        stsWarning.Text = "翻面成功！";
                    }
                    else
                    {
                        stsWarning.Text = "响应失败，taskid: " + strtask_id + " task_sequence: " + strtask_sequence;
                        SetErrNotice();
                        return;
                    }
                    ChangeBarcodeToLocat();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                SetErrNotice();
                return;
            }
        }
        #endregion

        #region btnLocat_Click
        private void txtLocat_KeyDown(object sender, KeyEventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            strLocat = string.Empty;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    drBarCodeInfo = dtBarCodeInfo.NewRow();
                    strLocat = txtLocat.Text.Trim().ToUpper();
                    //根据储位前3码，是否为指定的货架编码
                    if (strMARNO != strLocat.Substring(0, 3))
                    {
                        stsWarning.Text = "料架: " + strMARNO + " 不匹配，请确认!";
                        SetErrNotice();
                        return;
                    }
                    //校验储位是否存在
                    DataTable dtLocat = objAGVStorageIn.QueryWHHED(Werks, Lgort, strLocat, Comcd);

                    if (dtLocat.Rows.Count > 0)
                    {
                        drBarCodeInfo["LOCAT"] = strLocat;
                    }
                    else
                    {
                        stsWarning.Text = "储位: " + strLocat + " 不存在或不为空!";
                        SetErrNotice();
                        return;
                    }

                    //储位不可重复
                    if (dtBarCodeInfo.Rows.Count > 0)
                    {
                        DataRow[] drTemp = dtBarCodeInfo.Select(" LOCAT='" + drBarCodeInfo["LOCAT"].ToString().Trim() + "' ");
                        if (drTemp.Length > 0)
                        {
                            stsWarning.Text = "Locat: " + drBarCodeInfo["LOCAT"].ToString().Trim() + " already exists!";
                            SetErrNotice();
                            ChangeBarcodeToLocat();
                            return;
                        }
                    }
                    txtBarCode.Text = "";
                    txtBarCode.Enabled = true;
                    txtBarCode.Focus();


                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    ChangeBarcodeToLocat();
                    return;
                }
            }
        }
        #endregion

        #region btnBarCode_Click
        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            if (e.KeyCode == Keys.Enter)
            {
                #region 处理刷入的数据
                string strBarCode = txtBarCode.Text.Trim().ToUpper();
                string[] str = strBarCode.Split(';');

                //正常交料 9项：AL040170001; 202250; TIR-TIC; 5052548MY2; 3000; TPS40170QRGYRQ1; BTIR-TIC230223A0254MY; Made in Malaysia; TPS40170QRGYRQ1
                //IQC ReLabel 10项：DAV31UECCC0; 0723; AKV-AMV; 0723; 20; PCB V31U ECU/B (12L,200*184,REVC); R7202308280000711004; MADE IN TAIWAN; THIW12C986B; 20240213
                //物料打印 5项：DA0V3HVB4A0; 2423-0D5Q; GRU-ZDT; SP1230608055; 45
                if (str.Length >= 5)
                {
                    drBarCodeInfo["MATNR"] = str[0].ToString().Trim();
                    drBarCodeInfo["DACOD"] = str[1].ToString().Trim();
                    drBarCodeInfo["LIFNR"] = str[2].ToString().Trim();
                    drBarCodeInfo["LOCOD"] = str[3].ToString().Trim();
                    drBarCodeInfo["MENGE"] = Convert.ToInt32(str[4].ToString().Trim());
                    drBarCodeInfo["SERNO"] = str.Length > 6 ? str[6].ToString().Trim() : "";

                }
                //DID退料 1项：DFHD28MR005-CC37MRR0001
                else
                {
                    if (str.Length == 1)
                    {
                        //1项则为DIDNO，查询WHRID表，抓取数据
                        DataTable dtData = objStorageData.QueryIQC_WHRID(strBarCode);
                        //MATNR,DACOD,LIFNR,LOCOD,MENGE
                        if (dtData.Rows.Count > 0)
                        {
                            drBarCodeInfo["MATNR"] = dtData.Rows[0]["MATNR"].ToString();
                            drBarCodeInfo["DACOD"] = dtData.Rows[0]["DACOD"].ToString();
                            drBarCodeInfo["LIFNR"] = dtData.Rows[0]["LIFNR"].ToString();
                            drBarCodeInfo["LOCOD"] = dtData.Rows[0]["LOCOD"].ToString();
                            drBarCodeInfo["MENGE"] = Convert.ToInt32(dtData.Rows[0]["MENGE"].ToString());
                            drBarCodeInfo["SERNO"] = dtData.Rows[0]["SERNO"].ToString();

                        }
                        else
                        {
                            stsWarning.Text = "无DIDNO数据!";
                            SetErrNotice();
                            return;
                        }
                    }
                    else
                    {
                        stsWarning.Text = "格式错误!";
                        SetErrNotice();
                        return;
                    }
                }
                #endregion

                #region 匹配实物对应储位，显示界面
                //根据储位 strLocat 和扫描的实物，进行库存匹配，显示在界面上
                DataTable dtTemp = objAGVStorageIn.QueryAGVCombineWHITM(
                    Werks,
                    Lgort,
                    drBarCodeInfo["LOCAT"].ToString().Trim(),
                    Comcd,
                    drBarCodeInfo["MATNR"].ToString().Trim(),
                    drBarCodeInfo["LIFNR"].ToString().Trim(),
                    drBarCodeInfo["DACOD"].ToString().Trim(),
                    drBarCodeInfo["LOCOD"].ToString().Trim(),
                    drBarCodeInfo["SERNO"].ToString().Trim());

                if (dtTemp == null || dtTemp.Rows.Count <= 0)
                {
                    stsWarning.Text = "未匹配到库存，请确认";
                    SetErrNotice();
                    return;
                }
                else
                {
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        //更新dtBarCodeInfo                       
                        dtBarCodeInfo.Rows.Add(dr.ItemArray);
                    }
                }
                #endregion
                turnOffLight(drBarCodeInfo["LOCAT"].ToString().Trim());
                ChangeBarcodeToLocat();
                SetOKNotice();
                ShowDataGridBarCodeInfo();
                txtBarCode.Enabled = false; 
            }
        }
        #endregion

        public void turnOffLight(string strLocation)
        {
            var data = new
            {
                plant = strWerks,//厂区
                storage = strLgort,//仓别
                action = "DEL",//操作行为(ADD - 入储；DEL - 出储)
                location = strLocation,//储位
                location_detail = new List<object> { }
            };

            // 将对象转换为 JSON 字符串
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            string respLight = objAGVApi.AGVHttpRequest(strStoragetype, "shelfMaterialRenewal", json);
            //MessageBox.Show(respLight);
        }


        #region btnEndJobs_Click
        private void btnEndJobs_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = string.Empty;
            try
            {
                DialogResult result = MessageBox.Show("确认结束任务？", "确认", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    if (txtMARNO.Text.ToString().Trim() == "DY0")
                    {
                        if (objAGVStorageIn.Agv_CombineStorageOutData(dtBarCodeInfo))
                        {
                            SetOKNotice();
                            stsWarning.Text = "出库成功";
                        }
                        else
                        {
                            stsWarning.Text = "出库失败";
                            SetErrNotice();
                            return;
                        }
                    }
                    else
                    {
                        strreqCode = strreqCode + 1; //任务序号累加

                        Agv_endTask head = new Agv_endTask
                        {
                            plant = Werks, //所属厂区 32 (必填)
                            storage = Lgort, //仓库别 32 (必填)
                            work_station = Workstation, //工作站别 32 (必填)
                            task_id = strTaskNo, //任务编号 32 (必填)
                            task_type = "", //任务类型 32 (非必填)
                            task_sequence = strreqCode, //任务序号 (必填)
                        };
                        string strResult = objAGVApi.AGVHttpRequest(strStoragetype, "endTask", JsonConvert.SerializeObject(head));

                        if (!string.IsNullOrEmpty(strResult))
                        {
                            endTaskTaskData response = JsonConvert.DeserializeObject<endTaskTaskData>(strResult);
                            string strtask_id = response.task_id;
                            int ittask_sequence = response.task_status;
                            int strtask_sequence = response.task_sequence;
                            if (strtask_id == strTaskNo && strtask_sequence == strreqCode)
                            {
                                // dtBatCodeInfo增加UniqueID栏位，值为TaskNo
                                //for (int i = 0; i < dtBarCodeInfo.Rows.Count; i++)
                                //{
                                //    dtBarCodeInfo.Rows[i]["UniqueID"] = strTaskNo;
                                //}

                                //库存正式出储到临时表
                                if (objAGVStorageIn.Agv_CombineStorageOutData(dtBarCodeInfo))
                                {
                                    //删除料架调度中间表数据
                                    if (objAGVStorageIn.DeleteAGVShelf(strWerks, strLgort, strTaskNo, strWorkstation))
                                    {
                                        dtAGVOrder.Clear();
                                        dtBarCodeInfo.Clear();
                                        ShowDataGridAGVOrder();
                                        ShowDataGridBarCodeInfo();

                                        strWorkstation = ""; //清空工作站
                                        strStoragetype = ""; //清空入库类型
                                        strShelfsize = ""; //清空料架尺寸
                                        strShelftype = ""; //清空料架类型
                                        strAGVSTATE = ""; //清空货架使用状态
                                        strTaskNo = ""; //清空任务单号
                                        strreqCode = 0; //清空任务序号
                                        strMARNO = ""; //清空料架编号

                                        ChangeBarcodeToLocat();
                                        stsWarning.Text = "结束任务成功";
                                        SetOKNotice();
                                    }
                                }
                                else
                                {
                                    stsWarning.Text = "入库失败";
                                    SetErrNotice();
                                    return;
                                }
                            }
                            else
                            {
                                stsWarning.Text = "响应错误，taskid: " + strtask_id + " task_sequence: " + strtask_sequence;
                                SetErrNotice();
                                return;
                            }
                        }
                        else
                        {
                            stsWarning.Text = "呼叫接口失败,请稍后重试";
                            return;
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 初始化BarCodeInfo Table
        public void GetBarCodeInfo()
        {
            dtBarCodeInfo = new DataTable();
            dtBarCodeInfo.Columns.Add("MANDT");
            dtBarCodeInfo.Columns.Add("WERKS");
            dtBarCodeInfo.Columns.Add("LGORT");
            dtBarCodeInfo.Columns.Add("LOCAT");
            dtBarCodeInfo.Columns.Add("MATNR");
            dtBarCodeInfo.Columns.Add("INSMK");
            dtBarCodeInfo.Columns.Add("MBLNR");
            dtBarCodeInfo.Columns.Add("CHARG");
            dtBarCodeInfo.Columns.Add("LIFNR");
            dtBarCodeInfo.Columns.Add("RMANO");
            dtBarCodeInfo.Columns.Add("EBELN");
            dtBarCodeInfo.Columns.Add("INDAT");
            dtBarCodeInfo.Columns.Add("MENGE");
            dtBarCodeInfo.Columns.Add("QCQTY");
            dtBarCodeInfo.Columns.Add("REFNO");
            dtBarCodeInfo.Columns.Add("MRGID");
            dtBarCodeInfo.Columns.Add("ISPTM");
            dtBarCodeInfo.Columns.Add("REQTY");
            dtBarCodeInfo.Columns.Add("KDMAT");
            dtBarCodeInfo.Columns.Add("RMAK1");
            dtBarCodeInfo.Columns.Add("CRNAM");
            dtBarCodeInfo.Columns.Add("CRDAT");
            dtBarCodeInfo.Columns.Add("MONAM");
            dtBarCodeInfo.Columns.Add("MODAT");
            dtBarCodeInfo.Columns.Add("SERNO");
            dtBarCodeInfo.Columns.Add("LOCOD");
            dtBarCodeInfo.Columns.Add("INSPT");
            dtBarCodeInfo.Columns.Add("COMCD");
            dtBarCodeInfo.Columns.Add("BKQTY");
            dtBarCodeInfo.Columns.Add("DACOD");
            dtBarCodeInfo.Columns.Add("VEDAT");
            dtBarCodeInfo.Columns.Add("BOXID");
            dtBarCodeInfo.Columns.Add("NLOCA");
            dtBarCodeInfo.Columns.Add("SIDNO");
            dtBarCodeInfo.Columns.Add("REFID");
            dtBarCodeInfo.Columns.Add("SEQNO");
            dtBarCodeInfo.Columns.Add("PKDAT");
            dtBarCodeInfo.Columns.Add("ExpiryDate");
            dtBarCodeInfo.Columns.Add("TASKID");
            dtBarCodeInfo.Columns.Add("MAXEXP");
            dtBarCodeInfo.Columns.Add("MRBNO");
            dtBarCodeInfo.Columns.Add("LOCKED");
            dtBarCodeInfo.Columns.Add("LKDAT");
            dtBarCodeInfo.Columns.Add("MARNO");
            dtBarCodeInfo.Columns.Add("ITEMSTATES");
            dtBarCodeInfo.Columns.Add("STOCSTATES");
            dtBarCodeInfo.Columns.Add("CONFIG");
            dtBarCodeInfo.Columns.Add("MCDAT");

        }
        #endregion

    }
}
