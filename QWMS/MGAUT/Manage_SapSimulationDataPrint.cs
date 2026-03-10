using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Configuration;
using QCI.QWMS;
using QWMS.MGAUT;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;


namespace QWMS
{
    public partial class Manage_SapSimulationDataPrint : Form
    {
        #region Declare Variables
        UserInfo UserData = new UserInfo();
        public string strMandt = "";
        public string strComcd = "";
        public string strUsrnm = "";
        public string strWerks = "";
        public string strLgort = "";
        public string strPPNo = "";
        private string strProgid = "";
        StorageData objStorageData = null;
        private StorageIn objStorageIn;

        //scmdb20数据库配置文件
        //String strPrint = ConfigurationManager.AppSettings["DBCodeTCC_9200"];

        //打印数据表
        DataTable dtPrint = new DataTable();

        //打印数据集合
        List<string> listPrint = new List<string>();

        //使用者厂区仓别表
        DataTable dtWerksAndLgort = new DataTable();

        //判票号输入方式参数
        //int KeyTimes = 0;
        //DateTime beginTime = System.DateTime.Now;

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

        public string PPNo
        {
            get
            {
                return strPPNo;
            }
            set
            {
                strPPNo = value;
            }
        }
        #endregion

        #region Constructor
        public Manage_SapSimulationDataPrint()
        {
            InitializeComponent();
            objStorageIn = new StorageIn(UserData, Progid);
        }

        public Manage_SapSimulationDataPrint(UserInfo varUserData, string strProgid)
            : this()
        {
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;

            //显示状态栏信息
            ShowStatusData();

            //获取当前使用者可操作的厂区、仓别项
            BindWerksAndLgorts();

            //读取打印机设置文件
            BindPrintSetting();

        }
        #endregion

        #region 窗体载入时读取打印机设置文件
        private void BindPrintSetting()
        {
            //打印机设置文件默认路径
            //文件格式：串口，波特率，ip
            string strFileSettingPath = Application.StartupPath.ToString() + "\\PrintSetting.txt";


            if (File.Exists(strFileSettingPath))
            {
                //读取文件
                using (StreamReader sr = new StreamReader(strFileSettingPath, System.Text.Encoding.Default))
                {
                    string[] arrayPrintSetting = sr.ReadToEnd().Split(',');

                    //设置默认打印设置参数
                    txtCom.Text = arrayPrintSetting[0].ToString();
                    txtBounnd.Text = arrayPrintSetting[1].ToString();
                    txtIP.Text = arrayPrintSetting[2].ToString();
                }
            }
        }
        #endregion

        #region 获取当前使用者可操作的厂区、仓别项
        private void BindWerksAndLgorts()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                //获取当前使用者可操作的厂区、仓别项
                dtWerksAndLgort = objAuthority.GetWerksAndLgorts();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString() + "BindLgorts()");
            }

            //初始化绑定厂区
            BindWerks();
        }
        #endregion

        #region 初始化或刷新时，绑定厂区
        private void BindWerks()
        {
            for (int i = 0; i < dtWerksAndLgort.Rows.Count; i++)
            {
                string[] arrayWerks = dtWerksAndLgort.Rows[i]["WERKSandLGORTS"].ToString().Split(',');
                cbWerks.Items.Add(arrayWerks[0].ToString());
            }
        }
        #endregion

        #region 厂区下拉框改变事件
        private void cbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清空仓别下拉框
            cbLgorts.Items.Clear();

            for (int i = 0; i < dtWerksAndLgort.Rows.Count; i++)
            {
                //根据当前选中的厂区，绑定厂区对应的仓别可选项
                if (dtWerksAndLgort.Rows[i]["WERKSandLGORTS"].ToString().Contains(cbWerks.Items[cbWerks.SelectedIndex].ToString()))
                {
                    string[] arrayLgorts = dtWerksAndLgort.Rows[i]["WERKSandLGORTS"].ToString().Split(',');
                    for (int j = 1; j < arrayLgorts.Length; j++)
                    {
                        cbLgorts.Items.Add(arrayLgorts[j].ToString());
                    }
                }

            }
        }
        #endregion

        #region 显示状态栏信息
        private void ShowStatusData()
        {
            stsMandt.Text = Mandt;
            stsComcd.Text = Comcd;
            stsUsrnm.Text = Usrnm;
            stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
        #endregion

        #region 判票号扫描事件
        private void tbPPNo_KeyDown(object sender, KeyEventArgs e)
       {
            //设置盘票号
            //strPPNo = tbPPNo.Text.Trim();
            Array arr = tbPPNo.Text.Trim().Split(';');
            if (arr.Length > 1)
            {
                strPPNo = arr.GetValue(0).ToString().Trim();
            }

        }
        #endregion

        #region 查询按钮
        private void btnQuery_Click(object sender, EventArgs e)
        {
            lblprint.Text = "";
            dtPrint.Clear();
            //清空提示信息
            stsWarning.Text = "";
            if (string.IsNullOrEmpty(cbWerks.Text))
            {
                stsWarning.Text = "厂区不能为空!";
                return;
            }
            strWerks = cbWerks.Items[cbWerks.SelectedIndex].ToString();
            strLgort = cbLgorts.Text;
            objStorageData = new StorageData(UserData, strWerks, strLgort);
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
            string strMblnr = tbOrderNo.Text.Trim();
            string strBoxid = tbPPNo.Text.Trim();
            string strDateFrom = dtpBegin.Value.ToString("yyyyMMdd");
            string strDateTo = dtpEnd.Value.ToString("yyyyMMdd");
            string strCostCenter = tbCostCenter.Text.Trim();
            string strUser = tbPerson.Text.Trim();
            string strCheckMblnr = string.Empty;
            //packingno
            string strpackingno = tbpackingno.Text.Trim();
            if ((!string.IsNullOrEmpty(strMblnr)||!string.IsNullOrEmpty(strBoxid)) && !string.IsNullOrEmpty(strpackingno))
            {
                stsWarning.Text = "选择一种查询的单号类型!";
                return;
            }
            if (string.IsNullOrEmpty(strpackingno))
            {
                cmsMenu.Items[1].Visible = true;
                #region BPM打印数据需和现有数据做校验
                if (!string.IsNullOrEmpty(strMblnr) || !string.IsNullOrEmpty(strBoxid))
                {
                    if (string.IsNullOrEmpty(strMblnr))
                    {
                        strCheckMblnr = strBoxid.Substring(0, strBoxid.Length - 3);
                    }
                    else if (string.IsNullOrEmpty(strBoxid))
                    {
                        strCheckMblnr = strMblnr;
                    }
                    if ((strCheckMblnr.Substring(0, 1).Equals("1") || strCheckMblnr.Substring(0, 1).Equals("F")) && !ckPrintAgain.Checked)
                    {
                        #region 判断WHDWN是否有处理数据
                        DataTable dtWhticTemp = objStorageIn.GetWhitcBPM_TIC(strWerks, strLgort, strCheckMblnr);
                        if ((dtWhticTemp.Rows.Count == 0) || (dtWhticTemp.Rows.Count > 0 && string.IsNullOrEmpty(dtWhticTemp.Rows[0]["PRCDE"].ToString())))
                        {
                            if (!GetDataFromBPM(strCheckMblnr, objStorageIn))
                            {
                                MessageBox.Show("同步数据失败");
                            }
                        }
                        #endregion
                    }
                }  
                dtPrint = objStorageData.GetMCReturnData(strMblnr, strBoxid, strDateFrom, strDateTo, strCostCenter, strUser, ckPrintAgain.Checked);
                if (dtPrint.Rows.Count < 1)
                {
                    stsWarning.Text = "无数据(NO DATA)!";
                    return;
                }
                else
                {
                    ShowdgvBottom();
                }
                #endregion
            }
            else
            {
                if (string.IsNullOrEmpty(cbLgorts.Text))
                {
                    stsWarning.Text = "PackingNO单据类型仓别不能为空!";
                    return;
                }
                cmsMenu.Items[1].Visible = false;
                DataTable dtData = new DataTable();       
                try
                {                
                     dtData = objStorageData.QueryPackingDataNew(strWerks, strLgort, strpackingno);
                     dtPrint = dtData.Copy();
                    if (dtData.Rows.Count == 0)
                    {
                        ShowdgvBottom();
                        stsWarning.Text = "No Data!!";
                        return;
                    }
                    else
                    {
                        ShowdgvBottom();
                        stsWarning.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
         
        }
        #endregion

        #region 显示dgv数据
        private void ShowdgvBottom()
        {
            //清空提示信息
            stsWarning.Text = "";

            dgvBottom.AutoGenerateColumns = false;
            dgvBottom.Columns.Clear();
            dgvBottom.AllowUserToAddRows = false;
            dgvBottom.ContextMenuStrip = this.cmsMenu;//增加拆箱和并箱操作

            try
            {
                    ////选择单选框√
                    DatagridViewCheckBoxHeaderCell chkcell = new DatagridViewCheckBoxHeaderCell();
                    chkcell.OnCheckBoxClicked += new CheckBoxClickedHandler(chkcell_OnCheckBoxClicked);
                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderCell = chkcell;
                    chk.DataPropertyName = "cSelect";
                    chk.HeaderText = "";
                    //   chk.Name = "chk";
                    chk.Width = 30;
                    this.dgvBottom.Columns.Add(chk);
                    this.dgvBottom.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
         

                    if (tbpackingno.Text.Trim() != "")
                    {
                        DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                        dgvcMBLNR.DataPropertyName = "PACKINGNO";
                        dgvcMBLNR.HeaderText = "单号";//Packingno
                        dgvcMBLNR.Width = 130;
                        dgvcMBLNR.ReadOnly = true;
                        dgvBottom.Columns.Add(dgvcMBLNR);

                        //BOXID√
                        DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                        dgvcBOXID.DataPropertyName = "BOXID";
                        dgvcBOXID.HeaderText = "箱号";//Box_ID
                        dgvcBOXID.Name = "BOXID";
                        dgvcBOXID.Width = 130;
                        dgvcBOXID.ReadOnly = true;
                        dgvBottom.Columns.Add(dgvcBOXID);
                    }
                    else
                    {
                        //单号√
                        DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                        dgvcMBLNR.DataPropertyName = "MBLNR";
                        dgvcMBLNR.HeaderText = "单号";//Pallet_ID
                        dgvcMBLNR.Width = 130;
                        dgvcMBLNR.ReadOnly = true;
                        dgvBottom.Columns.Add(dgvcMBLNR);

                        //判票号√
                        DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                        dgvcBOXID.DataPropertyName = "BOXID";
                        dgvcBOXID.HeaderText = "判票号";//Box_ID
                        dgvcBOXID.Name = "BOXID";
                        dgvcBOXID.Width = 130;
                        dgvcBOXID.ReadOnly = true;
                        dgvBottom.Columns.Add(dgvcBOXID);
                    }

                    //料号√
                    DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                    dgvcMATNR.DataPropertyName = "MATNR";
                    dgvcMATNR.HeaderText = "料号";//PN
                    dgvcMATNR.Width = 120;
                    dgvcMATNR.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcMATNR);

                    //版本√
                    DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                    dgvcCHARG.DataPropertyName = "CHARG";
                    dgvcCHARG.HeaderText = "版本";//Batch_Type
                    dgvcCHARG.Width = 65;
                    dgvcCHARG.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcCHARG);

                    //数量√
                    DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                    dgvcMENGE.DataPropertyName = "MENGE";
                    dgvcMENGE.HeaderText = "数量";//Qty
                    dgvcMENGE.Width = 65;
                    dgvcMENGE.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcMENGE);

                    //开单人√
                    DataGridViewTextBoxColumn dgvcUSNAM = new DataGridViewTextBoxColumn();
                    dgvcUSNAM.DataPropertyName = "USNAM";
                    dgvcUSNAM.HeaderText = "开单人";//USNAM
                    dgvcUSNAM.Width = 80;
                    dgvcUSNAM.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcUSNAM);

                    //Cost Center√
                    DataGridViewTextBoxColumn dgvcKOSTL = new DataGridViewTextBoxColumn();
                    dgvcKOSTL.DataPropertyName = "KOSTL";
                    dgvcKOSTL.HeaderText = "Cost Center";//KOSTL
                    dgvcKOSTL.Width = 120;
                    dgvcKOSTL.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcKOSTL);

                    //厂商代码√
                    DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                    dgvcLIFNR.DataPropertyName = "LIFNR";
                    dgvcLIFNR.HeaderText = "厂商代码";//LIFNR
                    dgvcLIFNR.Width = 100;
                    dgvcLIFNR.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcLIFNR);

                    //NG Code√
                    DataGridViewTextBoxColumn dgvcREFID = new DataGridViewTextBoxColumn();
                    dgvcREFID.DataPropertyName = "REFID";
                    dgvcREFID.HeaderText = "NG Code";//REFID
                    dgvcREFID.Width = 90;
                    dgvcREFID.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcREFID);

                    //原产地√
                    DataGridViewTextBoxColumn dgvcREGION = new DataGridViewTextBoxColumn();
                    dgvcREGION.DataPropertyName = "REGION";
                    dgvcREGION.HeaderText = "原产地";//Region
                    dgvcREGION.Width = 80;
                    dgvcREGION.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcREGION);

                    //Date Code√
                    DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                    dgvcDACOD.DataPropertyName = "DACOD";
                    dgvcDACOD.HeaderText = "Date Code";//Region
                    dgvcDACOD.Width = 80;
                    dgvcDACOD.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcDACOD);

                    //料号描述√
                    DataGridViewTextBoxColumn dgvcKDMAT = new DataGridViewTextBoxColumn();
                    dgvcKDMAT.DataPropertyName = "KDMAT";
                    dgvcKDMAT.HeaderText = "料号描述";//Cust_PN
                    dgvcKDMAT.Width = 200;
                    dgvcKDMAT.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcKDMAT);

                    //日期√
                    DataGridViewTextBoxColumn dgvcBUDAT = new DataGridViewTextBoxColumn();
                    dgvcBUDAT.DataPropertyName = "BUDAT";
                    dgvcBUDAT.HeaderText = "日期";//TransDate
                    dgvcBUDAT.Width = 90;
                    dgvcBUDAT.ReadOnly = true;
                    dgvBottom.Columns.Add(dgvcBUDAT);

                dgvBottom.DataSource = dtPrint;
                labelRecords.Text = dtPrint.Rows.Count.ToString() + " records";
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString() + "ShowdgvBottom()"); ;
            }
        }
        #endregion

        #region 刷新按钮
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lblprint.Text = "";
            //清空dgv
            dgvBottom.Columns.Clear();

            //清空厂区、仓别下拉框，刷新绑定厂区
            cbWerks.Items.Clear();
            cbLgorts.Items.Clear();
            BindWerks();

            //清空文本框
            tbCostCenter.Text = "";
            tbOrderNo.Text = "";
            tbPPNo.Text = "";
            tbPerson.Text = "";
            stsWarning.Text = "";

            //隐藏dgv行数标签
            labelRecords.Visible = false;

            //时间控件重置为当前时间
            dtpBegin.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
        }
        #endregion

        #region 打印按钮
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //清空提示信息
            stsWarning.Text = "";

            //打印限制
            if (dgvBottom.Rows.Count == 0)
            {
                stsWarning.Text = "";
                stsWarning.Text = "无打印数据!";
                return;
            }
            else
            {
                GetCheckData();
                if (listPrint.Count == 0)
                {
                    stsWarning.Text = "";
                    stsWarning.Text = "未选中任何打印信息!";
                    return;
                }
                else
                {
                    PrintData(listPrint);
                    listPrint.Clear();
                }
            }

        }
        #endregion

        #region 打印判票
        /// <summary>
        /// 打印判票
        /// </summary>
        /// <param name="listPrint">所有选中行的BOXID集合,string类型</param>
        private void PrintData(List<string> listPrint)
        {
            //打印机串口,如:COM10
            string strCom = null;

            //打印机波特率，如：9600
            int intBaudRate = -1;

            //打印机IP
            string strIP = "";

            #region 打印机参数限制
            if ((txtCom.Text.Trim() == "" || txtBounnd.Text.Trim() == "") && (txtIP.Text.Trim() == ""))
            {
                stsWarning.Text = "打印机未设置打印参数！";
                return;
            }
            else
            {
                strCom = txtCom.Text.Trim();
                if (txtBounnd.Text.Trim().ToString() != "")
                {
                    try
                    {
                        intBaudRate = Convert.ToInt32(txtBounnd.Text.Trim());
                    }
                    catch
                    {
                        stsWarning.Text = "波特率只能是数字，请检查";
                        return;
                    }
                }
                strIP = txtIP.Text.Trim();
            }
            #endregion 打印机参数限制[END]

            string strwherelist = "(";
            if (listPrint.Count > 0)
            {
                for (int i = 0; i < listPrint.Count; i++)
                {
                    strwherelist = strwherelist + "'" + listPrint[i].ToString() + "',";
                }
                strwherelist = strwherelist.Substring(0, strwherelist.Length - 1);
                strwherelist = strwherelist + ")";
            }
            else
            {
                return;
            }

            DataTable dt = new DataTable();
            StringBuilder sbSQL = new StringBuilder();

            #region 查询T-SQL
            sbSQL.AppendFormat(@"SELECT 'IQC_REJECT.txt' AS FileName, MBLNR+ZEILE AS MBLNR,ZEILE, KOSTL, MATNR, SUBSTRING( KDMAT,0,30) AS KDMAT,LIFNR, BUDAT, USNAM, CHARG, CASE WHEN R.FULLRE IS NULL THEN C.REGION ELSE R.FULLRE END AS REGION, MENGE, INSMK,LGORT,DACOD,REFID AS PCODE, C.DELNO,
CASE WHEN C.REGION IS NULL THEN CASE WHEN DACOD IS NULL THEN MBLNR+ZEILE+';'+MATNR+';'+CHARG+';'+LIFNR+';'+CONVERT(VARCHAR(8), MENGE) +';' 
ELSE MBLNR+ZEILE+';'+MATNR+';'+CHARG+';'+LIFNR+';'+CONVERT(VARCHAR(8), MENGE) +';'+';'+DACOD END
ELSE CASE WHEN DACOD IS NULL THEN MBLNR+ZEILE+';'+MATNR+';'+CHARG+';'+LIFNR+';'+CONVERT(VARCHAR(8), MENGE) +';'+ C.REGION
ELSE MBLNR+ZEILE+';'+MATNR+';'+CHARG+';'+LIFNR+';'+CONVERT(VARCHAR(8), MENGE) +';'+ C.REGION+';'+DACOD END END  AS BarCodeTTL
FROM WHTIC AS C WITH(NOLOCK) LEFT JOIN RECTRL AS R WITH(NOLOCK) ON C.REGION=R.REGION WHERE C.MTYPE IN ('BPM_TIC','SAP_TIC','SAP_PACK') 
            AND    C.BOXID  in {0}", strwherelist);

            try
            {
                dt = objStorageData.GetPrintResult(sbSQL.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "-<PrintData()>");
                return;
            }
            finally
            {
                //清空sbSQL缓冲字符串
                sbSQL.Length = 0;
            }
            #endregion 查询T-SQL[END]


            StreamReader sr;
            string AllContexttmp = "";

            //打印机模板文件路径
            string strFilePath = Application.StartupPath + "\\report\\" + dt.Rows[0][0].ToString();

            //未找到打印机模板文件
            if (!File.Exists(strFilePath))
            {
                stsWarning.Text = "";
                stsWarning.Text = "未找到打印机参数文件!";
                return;
            }
            else
            {
                sr = new StreamReader(strFilePath, System.Text.Encoding.Default);
                AllContexttmp = sr.ReadToEnd();
                sr.Close();
            }

            System.IO.Ports.SerialPort SP=null;
            string strprint = "";
            if (strCom != "" && intBaudRate != -1)
            {
                SP = new System.IO.Ports.SerialPort(strCom, intBaudRate, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            }

            string[] line;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lblprint.Text = "开始打印:";
                string AllContext = AllContexttmp;
                #region 打印

                if (strCom != "" && intBaudRate != -1 && SP.IsOpen == false)
                {
                    SP.PortName = strCom;
                    SP.BaudRate = intBaudRate;
                    SP.DataBits = 8;
                    SP.Parity = Parity.None;
                    SP.StopBits = StopBits.One;
                    SP.Open();
                }

                try
                {
                    for (int x = 1; x < dt.Columns.Count; x++)
                    {
                        //Zebra 打印机特殊字符^  ，字符和二维码要转化为'_5E' ,条形码要转化为><
                        if (dt.Columns[x].ToString() == "BarCodeTTL" || dt.Columns[x].ToString() == "MATNR")
                        {
                            //AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "><"));
                            AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "_5E"));
                        }
                        else
                        {
                            AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString());
                        }
                    }

                    //利用正则表达式来分解
                    line = System.Text.RegularExpressions.Regex.Split(AllContext, "\r\n");

                    int j = 1;
                    foreach (string ss in line)
                    {
                        if (ss.IndexOf("<NA>") > -1)
                        {
                            continue;
                        }
                        if (j == 1)
                        {
                            AllContext = ss;
                            j = j + 1;
                        }
                        else
                        {
                            AllContext = AllContext + "\r\n" + ss;
                            j = j + 1;
                        }
                    }
                    if (strCom != "" && intBaudRate != -1)
                    {
                        SP.WriteLine(AllContext);
                    }
                    else
                    {
                        PrintLabelIP(strIP, AllContext);
                        System.Threading.Thread.Sleep(3500);//粘包
                    }
                    strprint = strprint + i.ToString() + "，";
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.ToString() + "-<PrintData()>");
                    MessageBox.Show(ex.ToString() + "-<PrintData()>");
                    return;
                }
                if (strCom != "" && intBaudRate != -1)
                {
                    SP.DiscardOutBuffer();
                    SP.Close();
                }

                //打印完更新WHTIC数量栏位
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Werks, Lgort, "", Progid);
                objStorageIn.UpdateSCQty(strwherelist);

                lblprint.Text = strprint + "已发送打印机";

                #endregion 打印[END]

            }
        }
        #endregion

        #region IP打印
        private void PrintLabelIP(string strIP, string strLabel)
        {
            string strPort = "9100";
            IPEndPoint hostEndPoint = new IPEndPoint(IPAddress.Parse(strIP), Convert.ToInt32(strPort));
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(hostEndPoint);
            if (!s.Connected)
            {
                MessageBox.Show("Not Connected!");
            }
            else
            {
                byte[] data = Encoding.UTF8.GetBytes(strLabel);
                s.Send(data, data.Length, 0);
                if (s.Connected)
                    s.Close();
            }
        }
        #endregion

        #region 点击单选框全选反选事件
        private void chkcell_OnCheckBoxClicked(bool isChecked)
        {
            if (isChecked == true)
            {
                dgvBottom.EndEdit();
                for (int i = 0; i < dgvBottom.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvBottom.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = true;
                }
            }
            else
            {
                dgvBottom.EndEdit();
                for (int i = 0; i < dgvBottom.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvBottom.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = false;
                }
            }
        }
        #endregion

        #region 重绘单选框表头
        //定义继承于DataGridViewColumnHeaderCell的类，用于绘制checkbox，定义checkbox鼠标单击事件  
        public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
        {
            Point checkBoxLocation;
            Size checkBoxSize;
            bool _checked = false;
            Point _cellLocation = new Point();
            System.Windows.Forms.VisualStyles.CheckBoxState _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

            public event CheckBoxClickedHandler OnCheckBoxClicked;

            public DatagridViewCheckBoxHeaderCell()
            {

            }

            //绘制列头checkbox 
            protected override void Paint(System.Drawing.Graphics graphics,
                                          System.Drawing.Rectangle clipBounds,
                                          System.Drawing.Rectangle cellBounds,
                                          int rowIndex,
                                          DataGridViewElementStates dataGridViewElementState,
                                          object value,
                                          object formattedValue,
                                          string errorText,
                                          DataGridViewCellStyle cellStyle,
                                          DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                          DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value,
                           formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                Point p = new Point();

                Size s = CheckBoxRenderer.GetGlyphSize(graphics, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

                //列头checkbox的X坐标
                p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2) - 1;

                //列头checkbox的Y坐标
                p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2) - 1;

                _cellLocation = cellBounds.Location;
                checkBoxLocation = p;
                checkBoxSize = s;

                if (_checked)
                {
                    _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                }
                else
                {
                    _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                }

                CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, _cbState);
            }

            //点击列头checkbox单击事件
            protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
            {
                Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);

                if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                    && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
                {
                    _checked = !_checked;

                    if (OnCheckBoxClicked != null)
                    {
                        //触发单击事件
                        OnCheckBoxClicked(_checked);
                        this.DataGridView.InvalidateCell(this);
                    }

                }

                base.OnMouseClick(e);
            }

        }

        //定义触发单击事件的委托
        public delegate void CheckBoxClickedHandler(bool state);

        public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
        {
            bool isChecked;

            public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
            {
                isChecked = bChecked;
            }

            public bool Checked
            {
                get
                {
                    return isChecked;
                }
                set
                {
                    isChecked = value;
                }
            }
        }

        #endregion

        #region 打印机设置
        private void btnPrintSetting_Click(object sender, EventArgs e)
        {
            //打印机设置文件路径
            //文件格式：串口，波特率，ip
            string strFileSettingPath = Application.StartupPath.ToString() + "\\PrintSetting.txt";

            //打印机设置文件不存在
            if (!File.Exists(strFileSettingPath))
            {
                //保存当前打印设置信息到C盘根目录下
                if (MessageBox.Show("是否保存当前打印机设置信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        using (StreamWriter sw = File.CreateText(strFileSettingPath))
                        {
                            sw.WriteLine(txtCom.Text.Trim() + "," + txtBounnd.Text.Trim() + "," + txtIP.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString() + "File.CreateText(strFileSettingPath)");
                    }
                }
            }
            //打印机设置文件存在
            else
            {
                if (MessageBox.Show("是否覆盖现有打印机设置信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        //覆盖现有的打印机设置文件
                        using (StreamWriter sw = new StreamWriter(strFileSettingPath, false))
                        {
                            sw.WriteLine(txtCom.Text.Trim() + "," + txtBounnd.Text.Trim() + "," + txtIP.Text.Trim());
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString() + "-<StreamWriter(strFileSettingPath, false)>");
                    }
                }
            }
        }
        #endregion

        #region 拆箱
        private void tsmiSplit_Click(object sender, EventArgs e)
        {
            QWMS.Common.ClaCommon claCommon = new ClaCommon();
            objStorageData = new StorageData(UserData, strWerks, strLgort);

            //DataGridViewRow row = dgvBottom.CurrentRow;

            //string strBoxid = row.Cells["BOXID"].ToString().Trim();
            //string strMblnr = row.Cells["MBLNR"].ToString().Trim();
            //string strMenge = row.Cells["MENGE"].ToString().Trim();
            //Manage_SapSimulationDataPrint_Split objSplit = new Manage_SapSimulationDataPrint_Split(UserData, strWerks, strLgort, strMblnr, strBoxid, strMenge);
            //objSplit.ShowDialog();
            //dtPrint = objStorageData.GetMCReturnData(strMblnr, "", "", "", "", "", false);
            //ShowdgvBottom();
            dgvBottom.EndEdit();
            dtPrint.AcceptChanges();
            if (dtPrint.Rows.Count > 0)
            {
                string strpackingno = tbpackingno.Text.Trim();
                if (string.IsNullOrEmpty(strpackingno))
                {
                    DataRow[] drSelect = dtPrint.Select(" cSelect = true ");
                    if (drSelect.Length != 1)
                    {
                        stsWarning.Text = "请选择一条数据进行拆箱，并且只能选一条";
                        return;
                    }
                    else
                    {
                        string strBoxid = drSelect[0]["BOXID"].ToString();
                        string strMblnr = drSelect[0]["MBLNR"].ToString();
                        string strMenge = drSelect[0]["MENGE"].ToString();
                        string strLifnr = drSelect[0]["LIFNR"].ToString();
                        bool bol = false;
                        bol = objStorageData.QueryCITSplit(drSelect[0]["MBLNR"].ToString(), drSelect[0]["BOXID"].ToString());
                        if (bol)
                        {
                            stsWarning.Text = "拆箱过后不能再次拆箱";
                            return;
                        }
                        Manage_SapSimulationDataPrint_Split objSplit = new Manage_SapSimulationDataPrint_Split(UserData, strWerks, strLgort, strMblnr, strBoxid, strMenge, strLifnr);
                        objSplit.ShowDialog();
                        dtPrint = objStorageData.GetMCReturnData(strMblnr, "", "", "", "", "", false);
                        ShowdgvBottom();
                    }
                }
                else
                {
                    DataRow[] drSelect = dtPrint.Select(" cSelect = true ");
                    if (drSelect.Length != 1)
                    {
                        stsWarning.Text = "请选择一条数据进行拆箱，并且只能选一条";
                        return;
                    }
                    else
                    {
                        string strBoxid = drSelect[0]["BOXID"].ToString();
                        string strMblnr = drSelect[0]["PACKINGNO"].ToString();
                        string strMenge = drSelect[0]["MENGE"].ToString();
                        string strLifnr = drSelect[0]["LIFNR"].ToString();
                        bool bol = false;
                        bol = objStorageData.QueryCITSplit(drSelect[0]["PACKINGNO"].ToString(), drSelect[0]["BOXID"].ToString());
                        if (bol)
                        {
                            stsWarning.Text = "拆箱过后不能再次拆箱";
                            return;
                        }
                        Manage_SapSimulationDataPrint_Split objSplit = new Manage_SapSimulationDataPrint_Split(UserData, strWerks, strLgort, strMblnr, strBoxid, strMenge, strLifnr);
                        objSplit.ShowDialog();
                        btnQuery_Click(null,null);

                    }
                }
            }
        }
        #endregion

        #region 并箱
        private void tsmiCombine_Click(object sender, EventArgs e)
        {
            QWMS.Common.ClaCommon claCommon = new ClaCommon();
            objStorageData = new StorageData(UserData, strWerks, strLgort);
            dtPrint.AcceptChanges();
            if (dtPrint.Rows.Count > 0)
            {
                DataTable dtCombine = dtPrint.Select(" cSelect = true ").CopyToDataTable();
                if (dtCombine.Rows.Count > 1)
                {
                    for (int i = 0; i < dtCombine.Rows.Count; i++)
                    {
                        bool bol = false;
                        bol = objStorageData.QueryCITCombine(dtCombine.Rows[i]["MBLNR"].ToString(), dtCombine.Rows[i]["BOXID"].ToString());
                        if (bol)
                        {
                            stsWarning.Text = "合箱过后不能再次合箱";
                            return;
                        }
                    }
                    DataTable dtBoxs = new DataTable();
                    dtBoxs.Columns.Add("BOXID");
                    DataView dv = new DataView(dtCombine);
                    DataTable dtMblnr = dv.ToTable(true, "MBLNR", "MATNR", "CHARG");
                    if (dtMblnr.Rows.Count > 1)
                    {
                        stsWarning.Text = "同一个判票单号，且相同料号，相同版本才能并箱，请重新选择";
                        return;
                    }
                    else if (dtMblnr.Rows.Count == 1)
                    {
                        string strMblnr = dtMblnr.Rows[0]["MBLNR"].ToString();
                        foreach (DataRow dr in dtCombine.Rows)
                        {
                            DataRow drBox = dtBoxs.NewRow();
                            drBox["BOXID"] = dr["BOXID"].ToString();
                            dtBoxs.Rows.Add(drBox.ItemArray);
                        }
                        if (dtBoxs.Rows.Count > 0)
                        {
                            dtBoxs.TableName = "QWMS";
                            string strXml = claCommon.ConvertDataTableToXML(dtBoxs);
                            if (objStorageData.ExecuteCombineOrSplit(strXml, strMblnr, "", "1"))
                            {
                                stsWarning.Text = "并箱成功";
                                dtPrint = objStorageData.GetMCReturnData(strMblnr, "", "", "", "", "", false);
                                ShowdgvBottom();
                            }
                        }
                    }
                }
                else
                {
                    stsWarning.Text = "请勾选要合并的数据,并且数据要超过一条";
                    return;
                }
            }
            else
            {
                stsWarning.Text = "无要打印的数据";
            }
        }
        #endregion

        private void GetCheckData()
        {
            for (int i = 0; i < dgvBottom.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvBottom.Rows[i].Cells[0];

                //循环dgv的每一行，判断是否有记录被选中
                if ((bool)dgvBottom.Rows[i].Cells[0].EditedFormattedValue)
                {
                    //获取当前已选中所有行的判票号(即BOXID)
                    listPrint.Add(dgvBottom.Rows[i].Cells["BOXID"].Value.ToString().Trim());
                }

            }
        }


        private bool GetDataFromBPM(string RefID, QCI.QWMS.StorageIn objStorageIn)
        {
            bool blResult = false;
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };//web service认证
                BPMTIC.GWF074_wsUploadApvsta objBPMTIC = new BPMTIC.GWF074_wsUploadApvsta();
                DataSet dsSet = objBPMTIC.GWF074_ProvideQWMSApproveScrapItem(string.Empty, RefID);
                DataTable dtMessage = dsSet.Tables[0];
                if (dtMessage.Rows[0]["Result"].ToString().Equals("Y"))
                {
                    #region dtResult同步到WHDWN和WHTIC
                    DataTable dtResult = dsSet.Tables[1];
                    dtResult.TableName = "QWMS";
                    string strBpmData = JsonConvert.SerializeObject(dtResult);
                    return objStorageIn.SynBPMticData(strBpmData);
                    #endregion
                }
                else
                {
                    MessageBox.Show(dtMessage.Rows[0]["ErrMsg"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return blResult;
        }


    }
}
