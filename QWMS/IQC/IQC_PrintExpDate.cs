using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.IO.Ports;

namespace QWMS
{
    public partial class IQC_PrintExpDate : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strExpDate = "";
        private string strTaskID = "";

        private string[] strLbInfo = new string[13];
        private string[] strLbInfoP = new string[13];

        private StorageData objStorageData;


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

        public string ExpDate
        {
            get
            {
                return strExpDate;
            }
            set
            {
                strExpDate = value;
            }
        }
        public string TaskID
        {
            get
            {
                return strTaskID;
            }
            set
            {
                strTaskID = value;
            }
        }





        #endregion

        #region 构造函数
        public IQC_PrintExpDate(UserInfo varUserData,string strProgid,string strExpDate, string strTaskID)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            ExpDate = strExpDate;
            TaskID = strTaskID;


            try
            {
                objStorageData = new StorageData(UserData, Werks, Lgort);
                DataTable dtIQC = objStorageData.QueryIQC(TaskID);
                if (dtIQC.Rows.Count > 0)
                {
                    txtExpDate.Text = dtIQC.Rows[0]["EXPDAT_AFTER"].ToString();
                    txtTaskID.Text = TaskID;
                }
                else
                {
                    MessageBox.Show("No Data!");
                    return;
                }

                BindPrintSetting();
            }
            catch
            {
                MessageBox.Show("Abnormal reading of printing parameters！");
                return;
            }
            
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

        #region BarCode
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                if (txtExpDate.Text.ToString().Trim() == "" || txtBarCode.Text.ToString().Trim() == "" || txtTaskID.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Please enter parameters！");
                    return;
                }
                string strflag = "11";
                string strPRTID = "";
                string strMATNR = "";
                string strLIFNR = "";

                DataTable dt = new DataTable();
                dt = objStorageData.QueryIQC_PRTID(txtTaskID.Text.ToString().Trim());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Printing is not allowed in this state！");
                    return;
                }
                strflag = dt.Rows[0]["QCFLG"].ToString().Trim();
                strPRTID = dt.Rows[0]["PRTID"].ToString().Trim();
                strMATNR = dt.Rows[0]["MATNR"].ToString().Trim().ToUpper();
                strLIFNR = dt.Rows[0]["LIFNR"].ToString().Trim().ToUpper();

                char var = ';';
                strLbInfo = txtBarCode.Text.ToString().Trim().ToUpper().Split(var);
                if (strLbInfo.Length == 1)
                {
                    //查询DID
                    DataTable dtData = new DataTable();
                    dtData = objStorageData.QueryIQC_WHRID(strLbInfo[0].ToString().Trim());
                    if (dtData.Rows.Count > 0)
                    {
                        txtPN.Text = dtData.Rows[0]["MATNR"].ToString().Trim();
                        strLbInfoP[0] = txtPN.Text.ToString().Trim();
                        txtDC.Text = dtData.Rows[0]["DACOD"].ToString().Trim();
                        strLbInfoP[1] = txtDC.Text.ToString().Trim();
                        txtVendor.Text = dtData.Rows[0]["LIFNR"].ToString().Trim();
                        strLbInfoP[2] = txtVendor.Text.ToString().Trim();
                        txtLC.Text = dtData.Rows[0]["LOCOD"].ToString().Trim();
                        strLbInfoP[3] = txtLC.Text.ToString().Trim();
                        txtQty.Text = dtData.Rows[0]["MENGE"].ToString().Trim();
                        strLbInfoP[4] = txtQty.Text.ToString().Trim();
                        txtSpec.Text = "";
                        strLbInfoP[5] = "";
                        txtUniqueID.Text = txtTaskID.Text.ToString().Trim() + strflag + strPRTID;
                        strLbInfoP[6] = txtTaskID.Text.ToString().Trim() + strflag + strPRTID;
                        txtDeliveryDate.Text = "";
                        strLbInfoP[7] = "";
                        txtSite.Text = "";
                        strLbInfoP[8] = "";
                        strLbInfoP[9] = txtExpDate.Text.ToString().Trim();
                    }
                    else
                    {
                        MessageBox.Show("No DIDNO Data！");
                        return;
                    }

                }
                else if (strLbInfo.Length == 5)
                {
                    txtPN.Text = strLbInfo[0];
                    strLbInfoP[0] = strLbInfo[0];
                    txtDC.Text = strLbInfo[1];
                    strLbInfoP[1] = strLbInfo[1];
                    txtVendor.Text = strLbInfo[2];
                    strLbInfoP[2] = strLbInfo[2];
                    txtLC.Text = strLbInfo[3];
                    strLbInfoP[3] = strLbInfo[3];
                    txtQty.Text = strLbInfo[4];
                    strLbInfoP[4] = strLbInfo[4];
                    txtSpec.Text = "";
                    strLbInfoP[5] = "";
                    txtUniqueID.Text = txtTaskID.Text.ToString().Trim() + strflag + strPRTID;
                    strLbInfoP[6] = txtTaskID.Text.ToString().Trim() + strflag + strPRTID;
                    txtDeliveryDate.Text = "";
                    strLbInfoP[7] = "";
                    txtSite.Text = "";
                    strLbInfoP[8] = "";
                    strLbInfoP[9] = txtExpDate.Text.ToString().Trim();
                }
                else if (strLbInfo.Length == 9)
                {
                    txtPN.Text = strLbInfo[0];
                    strLbInfoP[0] = strLbInfo[0];
                    txtDC.Text = strLbInfo[1];
                    strLbInfoP[1] = strLbInfo[1];
                    txtVendor.Text = strLbInfo[2];
                    strLbInfoP[2] = strLbInfo[2];
                    txtLC.Text = strLbInfo[3];
                    strLbInfoP[3] = strLbInfo[3];
                    txtQty.Text = strLbInfo[4];
                    strLbInfoP[4] = strLbInfo[4];
                    txtSpec.Text = strLbInfo[5];
                    strLbInfoP[5] = strLbInfo[5];
                    txtUniqueID.Text = txtTaskID.Text.ToString().Trim() + strflag + strPRTID;
                    strLbInfoP[6] = txtTaskID.Text.ToString().Trim() + strflag + strPRTID;
                    txtDeliveryDate.Text = strLbInfo[7];
                    strLbInfoP[7] = strLbInfo[7];
                    txtSite.Text = strLbInfo[8];
                    strLbInfoP[8] = strLbInfo[8];
                    strLbInfoP[9] = txtExpDate.Text.ToString().Trim();
                }

                if (strMATNR != strLbInfoP[0] || strLIFNR != strLbInfoP[2])
                {
                    MessageBox.Show("The Matnr or VendorCode not same！");
                    Clear();

                    return;
                }

                if (strLbInfo.Length == 1)
                {
                    txtQty.Focus();
                }
                else 
                {
                    PrintData();
                    Clear();
                }
            }
        }
        #endregion

        #region 打印
        private void PrintData()
        {
            objStorageData.SaveIQCPrintInfo(txtTaskID.Text.ToString().Trim());



            //打印机串口,如:COM10
            string strCom = null;

            //打印机波特率，如：9600
            int intBaudRate = -1;

            //打印机IP
            string strIP = "";

            #region 打印机参数限制
            if ((txtCom.Text.Trim() == "" || txtBounnd.Text.Trim() == "") && (txtIP.Text.Trim() == ""))
            {
                MessageBox.Show("The printer has not set printing parameters！");
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
                        MessageBox.Show("Baud rate can only be numerical, please check！");
                        return;
                    }
                }
                strIP = txtIP.Text.Trim();
            }
            #endregion 打印机参数限制[END]

            StreamReader sr;
            string AllContexttmp = "";
            string strFilePath = "";

            //打印机模板文件路径
            strFilePath = Application.StartupPath + @"\report\QuantaPN.txt";//发布放指定目录下执行
            //strFilePath = Application.StartupPath + @"\QuantaPN.txt";//debug目录下，调试执行

            //未找到打印机模板文件
            if (!File.Exists(strFilePath))
            {
                MessageBox.Show("Printer parameter file not found！");
                return;
            }
            else
            {
                sr = new StreamReader(strFilePath, System.Text.Encoding.Default);
                AllContexttmp = sr.ReadToEnd();
                sr.Close();
            }

            DataTable dt = new DataTable();
            string DataMatrix = "";

            DataMatrix = strLbInfoP[0] + ";" + strLbInfoP[1] + ";" + strLbInfoP[2] + ";" + strLbInfoP[3] + ";" + strLbInfoP[4] + ";" + strLbInfoP[5] + ";" + strLbInfoP[6] + ";" + strLbInfoP[7] + ";" + strLbInfoP[8] + ";" + strLbInfoP[9];

            //列名
            dt.Columns.Add("1");
            dt.Columns.Add("LOCAT_QR");//二维码
            dt.Columns.Add("strExpDate");//有效期
            dt.Columns.Add("strTaskID");//再检批号


            //行数据
            dt.Rows.Add("", DataMatrix, txtExpDate.Text.ToString().Trim(), txtTaskID.Text.ToString().Trim());

            System.IO.Ports.SerialPort SP = null;
            string strprint = "";
            if (strCom != "" && intBaudRate != -1)
            {
                SP = new System.IO.Ports.SerialPort(strCom, intBaudRate, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            }

            string[] line;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
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
                        if (dt.Columns[x].ToString() == "LOCAT_QR" || dt.Columns[x].ToString() == "strExpDate")
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

                MessageBox.Show("Printing successful！");

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

        private void btnPrintSetting_Click(object sender, EventArgs e)
        {
            //打印机设置文件路径
            //文件格式：串口，波特率，ip
            string strFileSettingPath = Application.StartupPath.ToString() + "\\PrintSetting.txt";

            //打印机设置文件不存在
            if (!File.Exists(strFileSettingPath))
            {
                //保存当前打印设置信息到C盘根目录下
                if (MessageBox.Show("Do you want to save the current printer settings information?", "prompt", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                if (MessageBox.Show("Overwrite existing printer settings information?", "prompt", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtExpDate.Text.ToString().Trim() == "" || txtBarCode.Text.ToString().Trim() == "" || txtTaskID.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Please enter parameters！");
                return;
            }

            txtBarCode_KeyPress(null,null);
        }

        private void Clear()
        {

            txtBarCode.Text = "";
            txtBarCode.Focus();

            txtPN.Text = "";
            txtDC.Text = "";
            txtVendor.Text = "";
            txtLC.Text = "";
            txtQty.Text = "";

            txtSpec.Text = "";
            txtUniqueID.Text = "";
            txtDeliveryDate.Text = "";
            txtSite.Text = "";

            Array.Clear(strLbInfo,0,strLbInfo.Length);
            Array.Clear(strLbInfoP, 0, strLbInfoP.Length);

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtPN.Text.ToString().Trim() == ""||txtDC.Text.ToString().Trim() == "" ||txtVendor.Text.ToString().Trim() == "" ||txtLC.Text.ToString().Trim() == "" ||txtQty.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("No data was retrieved. Please enter the printed data！");
                    return;
                }

                PrintData();
                Clear();
            }
        }

        private void IQC_PrintExpDate_Activated(object sender, EventArgs e)
        {
            if (txtBarCode.Text.ToString().Trim() == "")
            {
                txtBarCode.Text = "";
                txtBarCode.Focus();
            }
        }
    }
}
