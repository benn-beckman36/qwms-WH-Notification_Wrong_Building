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
    public partial class Manage_PrintReLabel : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strDate = "";

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

        #endregion

        #region 构造函数
        public Manage_PrintReLabel(UserInfo varUserData,string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;

            try
            {
                objStorageData = new StorageData(UserData, Werks, Lgort);

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
                if (txtBarCode.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Please enter parameters！");
                    return;
                }
                DataTable dtReLabel = objStorageData.QueryReLabel(txtBarCode.Text.ToString().Trim());
                if (dtReLabel.Rows.Count > 0)
                {
                    txtBoxID.Text = dtReLabel.Rows[0]["BOXID"].ToString().Trim();
                    txtPartNumber.Text = dtReLabel.Rows[0]["MATNR"].ToString().Trim();
                    txtPartNumberRev.Text = dtReLabel.Rows[0]["CHARG"].ToString().Trim();
                    txtQty.Text = dtReLabel.Rows[0]["MENGE"].ToString().Trim();
                    strDate = dtReLabel.Rows[0]["Date"].ToString().Trim();
                }
                else
                {
                    txtBarCode.Text = "";
                    MessageBox.Show("No Data!");
                    return;
                }
            }
        }
        #endregion

        #region 打印
        private void PrintData()
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
            strFilePath = Application.StartupPath + @"\report\PrintReLabel.txt";//发布放指定目录下执行
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

            dt.Columns.Add("1");
            dt.Columns.Add("CodeBOXID");//一维码
            dt.Columns.Add("strBOXID");//BOXID
            dt.Columns.Add("strMATNR");//MATNR
            dt.Columns.Add("strCHARG");//CHARG
            dt.Columns.Add("strMENGE");//MENGE
            dt.Columns.Add("strUserID");//User
            dt.Columns.Add("strDate");//时间

            //行数据
            dt.Rows.Add("", txtBoxID.Text.ToString().Trim(), txtBoxID.Text.ToString().Trim(), txtPartNumber.Text.ToString().Trim(), txtPartNumberRev.Text.ToString().Trim(), txtQty.Text.ToString().Trim(), Usrnm, strDate);

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
                        if (dt.Columns[x].ToString() == "CodeBOXID")
                        {
                            AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "><"));
                        }
                        else
                        {
                            AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "_5E"));
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
            if (txtBarCode.Text.ToString().Trim() == "" || txtQty.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Please enter parameters！");
                return;
            }

            PrintData();
        }

        private void Clear()
        {

            txtBarCode.Text = "";
            txtBarCode.Focus();
            txtBoxID.Text = "";
            txtPartNumberRev.Text = "";
            txtQty.Text = "";
            txtPartNumber.Text = "";
            txtQty.Text = "";

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtBoxID.Text.ToString().Trim() == ""||txtQty.Text.ToString().Trim() == "")
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
