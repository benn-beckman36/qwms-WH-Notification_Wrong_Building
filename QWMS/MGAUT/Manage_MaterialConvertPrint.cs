using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.IO;
using System.IO.Ports;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualBasic;

namespace QWMS
{
    public partial class Manage_MaterialConvertPrint : Form
    {

        #region Declare Variables
        UserInfo UserData = new UserInfo();
        public string strMandt = "";
        public string strComcd = "";
        public string strUsrnm = "";
        public string strWerks = "";
        public string strLgort = "";
        public string strInsmk = "";
        public string strTrntp = "";
        public string strMblnr = "";
        public string strDate = "";
        public string strCMatnr = "";
        public string strOMatnr = "";
        private string strProgid = "";
        public string strIP = "";
        public int num;//打印数量
        StorageIn objStorageIn = null;
        PlantData objPlantData = null;
        SapData objSapData = null;
        Authority objAuthority = null;
        private DataTable dtOutSource = new DataTable();
        private DataTable dtData = new DataTable();
        private DataTable ScanData = new DataTable();
        private DataTable dtLocat = new DataTable();
        public int Count = 0;

        //打印数据集合
        List<string> listPrint = new List<string>();

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
        public string Trntp
        {
            get
            {
                return strTrntp;
            }
            set
            {
                strTrntp = value;
            }
        }
        public string Date
        {
            get
            {
                return strDate;
            }
            set
            {
                strDate = value;
            }
        }
        #endregion

        #region 构造函数
        public Manage_MaterialConvertPrint(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objStorageIn = new StorageIn(UserData, Progid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);

                //检查是否有管理作业功能的权限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowDdlInsmk();
                    if (cbWerks.Items.Count > 0)
                    {
                        this.cbWerks.SelectedIndex = 0;
                    }
                    if (cbLgorts.Items.Count > 0)
                    {
                        this.cbLgorts.SelectedIndex = 0;
                    }
                    if (cbInsmk.Items.Count > 0)
                    {
                        this.cbInsmk.SelectedIndex = 0;
                    }
                    BindPrintSetting();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ShowOutSourceDataGrid
        private void ShowOutSourceDataGrid()
        {
            dgvOutSource.AllowUserToAddRows = false;
            dgvOutSource.AutoGenerateColumns = false;
            dgvOutSource.Columns.Clear();

            try
            {
                //OMBLNR
                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "ITEM";
                dgvcItem.HeaderText = "序号";
                dgvcItem.Width = 60;
                dgvcItem.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcItem);

                //CMBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "CMBLNR";
                //dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.HeaderText = "转入单据";
                dgvcMblnr.Width = 100;
                dgvcMblnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMblnr);

                //OMBLNR
                DataGridViewTextBoxColumn dgvoMblnr = new DataGridViewTextBoxColumn();
                dgvoMblnr.DataPropertyName = "OMBLNR";
                //dgvcMblnr.HeaderText = "Document No";
                dgvoMblnr.HeaderText = "转出单据";
                dgvoMblnr.Width = 100;
                dgvoMblnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvoMblnr);


                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                //dgvcMatnr.HeaderText = "转 P/N";
                dgvcMatnr.HeaderText = "转入料号";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMatnr);

                //WERKS
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                //dgvcWerks.HeaderText = "PLANT";
                dgvcWerks.HeaderText = "厂区";
                dgvcWerks.Width = 60;
                dgvcWerks.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcWerks);

                //LGORT
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                //dgvcLgort.HeaderText = "Storage";
                dgvcLgort.HeaderText = "仓别";
                dgvcLgort.Width = 60;
                dgvcLgort.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcLgort);

                //LOCAT
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                //dgvcLocat.HeaderText = "Location";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.Width = 60;
                dgvcLocat.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcLocat);

                //LIFNR
                //DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                //dgvcLifnr.DataPropertyName = "LIFNR";
                //dgvcLifnr.HeaderText = "Vendor";
                //dgvcLifnr.Width = 90;
                //dgvcLifnr.ReadOnly = true;
                //dgvOutSource.Columns.Add(dgvcLifnr);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                //dgvcMenge.HeaderText = "Un-Store Out Qty";
                dgvcMenge.HeaderText = "单据总数量";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);

                //MENGE-OTQTY
                DataGridViewTextBoxColumn dgvcResidue = new DataGridViewTextBoxColumn();
                dgvcResidue.DataPropertyName = "RESIDUE";
                //dgvcMenge.HeaderText = "Residue Qty";
                dgvcResidue.HeaderText = "待转数量";
                dgvcResidue.Width = 90;
                dgvcResidue.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcResidue);

                //ALMNG
                DataGridViewTextBoxColumn dgvcAlmng = new DataGridViewTextBoxColumn();
                dgvcAlmng.DataPropertyName = "ALMNG";
                //dgvcAlmng.HeaderText = "Storage Out Qty";
                dgvcAlmng.HeaderText = "库存数量";
                dgvcAlmng.Width = 90;
                dgvcAlmng.ReadOnly = false;
                dgvcAlmng.DefaultCellStyle.BackColor = Color.Aquamarine;
                dgvOutSource.Columns.Add(dgvcAlmng);

                //Scan Qty
                DataGridViewTextBoxColumn dgvcScan = new DataGridViewTextBoxColumn();
                dgvcScan.DataPropertyName = "SCQTY";
                //dgvcScan.HeaderText = "Scanned Qty";
                dgvcScan.HeaderText = "扫描数量";
                dgvcScan.Width = 90;
                dgvcScan.ReadOnly = false;
                dgvcScan.DefaultCellStyle.BackColor = Color.Aquamarine;
                dgvOutSource.Columns.Add(dgvcScan);

                dgvOutSource.DataSource = dtLocat;
                //lblOutSource.Text = dtOutSource.Rows.Count.ToString() + " record(s)";
                lblOutSource.Text = dtLocat.Rows.Count.ToString() + " records";

                //if (dtData.Rows.Count > 0)
                //{
                //    //this.panel1.Enabled = false;
                //    this.btnQuery.Enabled = true;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }
        #endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.AllowUserToAddRows = false;
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();

            try
            {
                //OMatnr
                DataGridViewTextBoxColumn dgvOMblnr = new DataGridViewTextBoxColumn();
                dgvOMblnr.DataPropertyName = "MBLNR";
                //dgvOMblnr.HeaderText = "Document No";
                dgvOMblnr.HeaderText = "原单据";
                dgvOMblnr.Width = 110;
                dgvOMblnr.ReadOnly = true;
                dgvData.Columns.Add(dgvOMblnr);

                //CMantr
                DataGridViewTextBoxColumn dgvOMantr = new DataGridViewTextBoxColumn();
                dgvOMantr.DataPropertyName = "MATNR";
                //dgvOMantr.HeaderText = "原 P/N";
                dgvOMantr.HeaderText = "原料号";
                dgvOMantr.Width = 100;
                dgvOMantr.ReadOnly = true;
                dgvData.Columns.Add(dgvOMantr);

                //LOCAT
                DataGridViewTextBoxColumn dgvLocat = new DataGridViewTextBoxColumn();
                dgvLocat.DataPropertyName = "LOCAT";
                dgvLocat.HeaderText = "储位";
                dgvLocat.Width = 100;
                dgvLocat.ReadOnly = true;
                dgvData.Columns.Add(dgvLocat);

                //MENGE
                DataGridViewTextBoxColumn dgvMenge = new DataGridViewTextBoxColumn();
                dgvMenge.DataPropertyName = "MENGE";
                dgvMenge.HeaderText = "扫描数量";
                dgvMenge.Width = 100;
                dgvMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvMenge);

                dgvData.DataSource = ScanData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }
        #endregion

        private void btConfirm_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            try
            {
                if (cbWerks.SelectedIndex != -1)
                {
                    strWerks = cbWerks.Items[cbWerks.SelectedIndex].ToString();
                }
                if (cbLgorts.SelectedIndex != -1)
                {
                    strLgort = cbLgorts.Items[cbLgorts.SelectedIndex].ToString();
                }
                if (cbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cbInsmk.Items[cbInsmk.SelectedIndex].ToString();
                }

                if (strWerks == "" || strLgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                objSapData = new SapData(UserData, strWerks, strLgort);
                strMblnr = txtDocument.Text.ToString().Trim().Substring(0, 10);
                strDate = dtpDate.Value.ToString("yyyyMMdd");
                getData(strMblnr, strInsmk, strDate);
                ShowOutSourceDataGrid();
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }

        private void getData(string strMblnr, string strInsmk, string strDate)
        {
            objSapData = new SapData(UserData, strWerks, strLgort);
            strTrntp = "M+";//获取转料号信息
            dtOutSource = objSapData.GetMatnrConvert(strMblnr, strInsmk, strDate, strTrntp);
            if (dtOutSource.Rows.Count == 0)
            {
                stsWarning.Text = "No Data";
            }
            else
            {
                strCMatnr = dtOutSource.Rows[0]["MATNR"].ToString();
                txtDCode.Text = dtOutSource.Rows[0]["DACOD"].ToString();
                txtQty.Text = dtOutSource.Rows[0]["MENGE"].ToString();
                txtCMatnr.Text = strCMatnr;
                strTrntp = "M-";//获取原料号
                dtData = objSapData.GetMatnrConvert(strMblnr, strInsmk, strDate, strTrntp);

                dtLocat = objSapData.GetLocatCount(dtData);

                if (dtLocat.Rows.Count > 0)
                {
                    for (int i = 0; i < dtLocat.Rows.Count; i++)
                    {
                        dtLocat.Rows[i]["ITEM"] = i + 1;
                        dtLocat.Rows[i]["CMBLNR"] = dtOutSource.Rows[0]["MBLNR"];
                        dtLocat.Rows[i]["OMBLNR"] = dtData.Rows[0]["MBLNR"];
                        dtLocat.Rows[i]["MATNR"] = dtOutSource.Rows[0]["MATNR"];
                        dtLocat.Rows[i]["MENGE"] = dtOutSource.Rows[0]["MENGE"];
                        dtLocat.Rows[i]["RESIDUE"] = dtOutSource.Rows[0]["RESIDUE"];
                    }
                }
                strOMatnr = dtData.Rows[0]["MATNR"].ToString();
                txtOMatnr.Text = strOMatnr;
                ScanData = dtLocat.Clone();
            }
            txtDocument.Text = "";
        }


        private void btRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
            //stsWarning.Text = "";
            //this.cbWerks.SelectedIndex = 0;
            //this.cbLgorts.SelectedIndex = 0;
            //this.cbInsmk.SelectedIndex = 0;
            //this.rbConvert.Checked = false;
            //this.rbOriginal.Checked = false;
            //this.gbPrint.Enabled = false;
            //this.gbPrintMachine.Enabled = false;
            //this.gbFunction.Enabled = false;
            //this.btPrint.Enabled = false;
            //this.txtDocument.Text = "";
            //this.txtBarcode.Text = "";
            //this.txtOMatnr.Text = "";
            //this.txtCMatnr.Text = "";
            //this.txtDCode.Text = "";
            //this.txtVCode.Text = "";
            //this.txtLCode.Text = "";
            //this.txtQty.Text = "";

            //this.dtLocat.Clear();
            //this.dtOutSource.Clear();
            //this.ScanData.Clear();
            //this.txtLocat.Text = "";
            //this.lblOutSource.Text = "0" + " records";
            //this.btSave.Enabled = false;
        }

        private void Refresh()
        {
            Count = 0;
            stsWarning.Text = "";
            this.cbWerks.SelectedIndex = 0;
            this.cbLgorts.SelectedIndex = 0;
            this.cbInsmk.SelectedIndex = 0;
            this.rbConvert.Checked = false;
            this.rbOriginal.Checked = false;
            this.gbPrint.Enabled = false;
            this.gbPrintMachine.Enabled = false;
            this.gbFunction.Enabled = false;
            //this.btPrint.Enabled = false;
            this.txtDocument.Text = "";
            this.txtBarcode.Text = "";
            this.txtOMatnr.Text = "";
            this.txtCMatnr.Text = "";
            this.txtDCode.Text = "";
            this.txtVCode.Text = "";
            this.txtLCode.Text = "";
            this.txtQty.Text = "";

            this.dtLocat.Clear();
            this.dtOutSource.Clear();
            this.ScanData.Clear();
            this.txtLocat.Text = "";
            this.lblOutSource.Text = "0" + " records";
            this.btSave.Enabled = false;
        }
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

        private bool btPrint_Click()//object sender, EventArgs e
        {

            #region 打印机参数限制
            if (txtIP.Text.Trim() == "")
            {
                stsWarning.Text = "请输入打印IP！";
                return false;
            }
            else
            {
                strIP = txtIP.Text.Trim();
            }
            if (txtNum.Text.Trim() == "")
            {
                stsWarning.Text = "请输入打印数量！";
                return false;
            }
            else
            {
                num = Convert.ToInt32(txtNum.Text.ToString().Trim());
            }
            #endregion 打印机参数限制[END]


            #region 设置打印字段内容
            DataTable dt = new DataTable();
            dt.Columns.Add("MATNR");
            dt.Columns.Add("Date Code");
            dt.Columns.Add("Vendor Code");
            dt.Columns.Add("Lot Code");
            dt.Columns.Add("Qty");
            dt.Columns.Add("QR_CODE");
            dt.Columns.Add("CRTime");
            dt.Columns.Add("Header");
            StringBuilder print = new StringBuilder("");
            DataRow dr = dt.NewRow();
            if (rbOriginal.Checked == true)
            {
                dr["MATNR"] = txtOMatnr.Text.ToString().Trim();
                print.Append(txtOMatnr.Text.ToString().Trim() + ";");
            }
            else
            {
                dr["MATNR"] = txtCMatnr.Text.ToString().Trim();
                print.Append(txtCMatnr.Text.ToString().Trim() + ";");
            }
            dr["Date Code"] = txtDCode.Text.ToString().Trim();
            dr["Vendor Code"] = txtVCode.Text.ToString().Trim();
            dr["Lot Code"] = txtLCode.Text.ToString().Trim();
            dr["Qty"] = txtQty.Text.ToString().Trim();
            print.Append(txtDCode.Text.ToString().Trim() + ";" + txtVCode.Text.ToString().Trim() + ";" + txtLCode.Text.ToString().Trim() + ";" + txtQty.Text.ToString().Trim());
            dr["QR_CODE"] = print;//二维码内容
            dr["CRTime"] = DateTime.Now.ToString();
            if (dtOutSource.Rows.Count > 0)
            {
                dr["Header"] = dtOutSource.Rows[0]["REFID"].ToString().Split('/')[0];
            }
            else
            {
                dr["Header"] = "";
            }
            dt.Rows.Add(dr);
            #endregion

            StreamReader sr;
            string AllContexttmp = "";

            //打印机模板文件路径
            string strFilePath = Application.StartupPath + "\\report\\MaterialConvert_Model.txt";

            //未找到打印机模板文件
            if (!File.Exists(strFilePath))
            {
                stsWarning.Text = "";
                stsWarning.Text = "未找到打印机参数文件!";
                return false;
            }
            else
            {
                sr = new StreamReader(strFilePath, System.Text.Encoding.Default);
                AllContexttmp = sr.ReadToEnd();
                sr.Close();
            }

            string[] line;

            lblprint.Text = "开始打印:";
            string AllContext = AllContexttmp;

            #region 打印机打印内容

            try
            {
                for (int x = 0; x < dt.Columns.Count; x++)
                {
                    //Zebra 打印机特殊字符^  ，字符和二维码要转化为'_5E' ,条形码要转化为><
                    if (dt.Columns[x].ToString() == "MATNR" || dt.Columns[x].ToString() == "QR_CODE")
                    {
                        //AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "><"));
                        AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[0][x].ToString().Replace("^", "_5E"));
                    }
                    else
                    {
                        AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[0][x].ToString());
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
                //SP.WriteLine(AllContext);
                for (int i = 1; i <= num; i++)
                {
                    PrintLabelIP(strIP, AllContext);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "-<PrintData()>");
                return false;
            }
            //SP.DiscardOutBuffer();
            //SP.Close();

            lblprint.Text = "已发送打印机";
            return true;
            #endregion 打印[END]
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
        }

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
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
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cbWerks.SelectedIndex != -1)
                {
                    strWerks = cbWerks.Items[cbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cbLgorts.SelectedIndex != -1)
                {
                    strLgort = cbLgorts.Items[cbLgorts.SelectedIndex].ToString();
                }
                else
                {
                    cbLgorts.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cbLgorts.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cbLgorts.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cbLgorts.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cbLgorts.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void ShowDdlInsmk()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cbInsmk.Items.Clear();
                dtTemp = objPlantData.GetDdlInsmk();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cbInsmk.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlInsmk()");
            }
        }

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
                    txtIP.Text = arrayPrintSetting[2].ToString();
                }
            }
        }
        #endregion

        #region 打印机设置
        private void btSetting_Click(object sender, EventArgs e)
        {
            //打印机设置文件路径
            //文件格式：串口，波特率，ip
            string strFileSettingPath = Application.StartupPath.ToString() + "\\PrintSetting.txt";

            //打印机设置文件不存在
            if (!File.Exists(strFileSettingPath))
            {
                //保存当前打印设置信息到E盘根目录下
                if (MessageBox.Show("是否保存当前打印机设置信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        using (StreamWriter sw = File.CreateText(strFileSettingPath))
                        {
                            sw.WriteLine(",," + txtIP.Text.Trim());
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
                        string[] arrayPrintSetting;
                        using (StreamReader sr = new StreamReader(strFileSettingPath, System.Text.Encoding.Default))
                        {
                            arrayPrintSetting = sr.ReadToEnd().Split(',');
                        }
                        //覆盖现有的打印机设置文件
                        using (StreamWriter sw = new StreamWriter(strFileSettingPath, false))
                        {
                            sw.WriteLine(arrayPrintSetting[0] + "," + arrayPrintSetting[1] + "," + txtIP.Text.Trim());
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

        private void rbOriginal_CheckedChanged(object sender, EventArgs e)
        {
            this.gbPrint.Enabled = true;
            this.gbFunction.Enabled = true;
            this.gbPrintMachine.Enabled = true;
            this.txtDocument.Enabled = false;
            this.txtBarcode.Enabled = true;
            this.stsWarning.Text = "";
            this.txtDocument.Text = "";
            this.txtBarcode.Text = "";
            this.txtOMatnr.Text = "";
            this.txtCMatnr.Text = "";
            this.txtDCode.Text = "";
            this.txtVCode.Text = "";
            this.txtLCode.Text = "";
            this.txtQty.Text = "";
            this.txtNum.Text = "";
        }

        private void rbConvert_CheckedChanged(object sender, EventArgs e)
        {
            this.gbPrint.Enabled = true;
            this.gbFunction.Enabled = true;
            this.gbPrintMachine.Enabled = true;
            this.txtDocument.Enabled = true;
            this.txtBarcode.Enabled = true;
            this.txtOMatnr.Enabled = false;
            this.txtCMatnr.Enabled = false;
            this.stsWarning.Text = "";
            this.txtDocument.Text = "";
            this.txtBarcode.Text = "";
            this.txtOMatnr.Text = "";
            this.txtCMatnr.Text = "";
            this.txtDCode.Text = "";
            this.txtVCode.Text = "";
            this.txtLCode.Text = "";
            this.txtQty.Text = "";
            this.txtNum.Text = "";
        }

        private void txtDocument_DoubleClick(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strDate = dtpDate.Value.ToString("yyyyMMdd");
            try
            {
                if (cbWerks.SelectedIndex != -1)
                {
                    strWerks = cbWerks.Items[cbWerks.SelectedIndex].ToString();
                }
                if (cbLgorts.SelectedIndex != -1)
                {
                    strLgort = cbLgorts.Items[cbLgorts.SelectedIndex].ToString();
                }
                if (cbInsmk.SelectedIndex != -1)
                {
                    strInsmk = cbInsmk.Items[cbInsmk.SelectedIndex].ToString();
                }

                if (strWerks == "" || strLgort == "")
                {
                    this.stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }

                Manage_MaterialConvertSelect objManage_SapDataSelect = new Manage_MaterialConvertSelect(UserData, strWerks, strLgort, Progid, "", "", strInsmk, strDate);
                objManage_SapDataSelect.ShowDialog();
                strMblnr = objManage_SapDataSelect.Mblnr.Substring(0, 10);
                if (strMblnr == "")
                {
                    stsWarning.Text = "No Data";
                }
                else
                {
                    txtDocument.Text = strMblnr;
                    getData(strMblnr, strInsmk, strDate);
                    txtBarcode.Focus();
                }
            }

            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.stsWarning.Text = "";
                Array arr = txtBarcode.Text.Trim().Split(';');
                string strMatnr = "";
                if (arr.Length > 1)
                {
                    strMatnr = arr.GetValue(0).ToString().Trim();
                    if (rbConvert.Checked == true)
                    {
                        if (strOMatnr != strMatnr)
                        {
                            this.stsWarning.Text = "该料号与选定账单的原料号不符,请重新刷入";
                        }
                        else
                        {
                            txtDCode.Text = arr.GetValue(1).ToString().Trim();
                            txtVCode.Text = arr.GetValue(2).ToString().Trim();
                            txtLCode.Text = arr.GetValue(3).ToString().Trim();
                            txtQty.Text = arr.GetValue(4).ToString().Trim();
                        }
                    }
                    else
                    {
                        txtOMatnr.Text = arr.GetValue(0).ToString().Trim();
                        txtDCode.Text = arr.GetValue(1).ToString().Trim();
                        txtVCode.Text = arr.GetValue(2).ToString().Trim();
                        txtLCode.Text = arr.GetValue(3).ToString().Trim();
                        txtQty.Text = arr.GetValue(4).ToString().Trim();
                    }
                    txtBarcode.Text = "";
                }
                else
                {
                    txtOMatnr.Text = arr.GetValue(0).ToString().Trim();
                    this.gbPrint.Enabled = true;
                    this.txtOMatnr.Enabled = false;
                    this.txtCMatnr.Enabled = false;
                    this.stsWarning.Text = "请输入原料号的相关信息";
                }
            }

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            strDate = dtpDate.Value.ToString("yyyyMMdd");
        }

        private void cbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            //btPrint.Enabled = false;
            if (txtNum.Text.ToString().Trim() != "")
            {
                num = Convert.ToInt32(txtNum.Text.ToString().Trim());
                if (rbConvert.Checked == true && num > 1)
                {
                    PasswordDetection();
                }

            }
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (num > 1)
                {
                    PasswordDetection();
                    //#region 设置打印密码
                    //Admin objAdmin = new Admin(UserData, Progid);
                    //String PM = Interaction.InputBox("请输入密码", "输入密码", "", 100, 100);
                    //DataTable dtPWD = new DataTable();
                    //dtPWD = objAdmin.GetPassword();
                    //DataRow[] dr = dtPWD.Select("PASWD='" + PM + "'");
                    //if (dr.Count() == 0)
                    //{
                    //    MessageBox.Show("请输入正确的密码谢谢！！！！！");
                    //    txtNum.Text = String.Empty;
                    //    //btPrint.Enabled = false;
                    //    return;
                    //}
                    //else
                    //{
                    //    //btPrint.Enabled = true;
                    //}
                    //#endregion
                }
                else
                {
                    //btPrint.Enabled = true;
                }

            }

        }

        private void PasswordDetection()
        {
            #region 设置打印密码
            Admin objAdmin = new Admin(UserData, Progid);
            String PM = Interaction.InputBox("请输入密码", "输入密码", "", 100, 100);
            ClaCommon claCommon = new ClaCommon();
            if (!claCommon.CheckAccount("02090444", PM).GetAwaiter().GetResult())
            {
                MessageBox.Show("请输入正确的密码谢谢！！！！！");
                return;
            }
            //DataTable dtPWD = new DataTable();
            //dtPWD = objAdmin.GetPassword();
            //DataRow[] dr = dtPWD.Select("PASWD='" + PM + "'");
            //if (dr.Count() == 0)
            //{
            //    MessageBox.Show("请输入正确的密码谢谢！！！！！");
            //    txtNum.Text = String.Empty;
            //    //btPrint.Enabled = false;
            //    return;
            //}
            //else
            //{
            //    //btPrint.Enabled = true;
            //}
            #endregion
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                bool print = btPrint_Click();
                //print = btPrint_Click();
                if (print && rbConvert.Checked == true)
                {

                    ShowDataGrid();

                    //foreach (DataRow item in ScanData.Rows)
                    //{
                    //    if (txtLocat.Text.ToString().Trim() == item["Locat"].ToString().Trim())
                    //    {
                    //        MessageBox.Show(string.Format("{0}储位已扫描，請確認!!", txtLocat.Text.Trim()), "提醒");
                    //        return;
                    //    }
                    //}

                    int SumScan = 0;

                    for (int i = 0; i < dtLocat.Rows.Count; i++)
                    {
                        SumScan += Convert.ToInt32(dtLocat.Rows[i]["SCQTY"]);
                    }
                    if (Count > 0)
                    {
                        if (Convert.ToInt32(dtLocat.Rows[Count - 1]["SCQTY"]) != Convert.ToInt32(dtLocat.Rows[Count - 1]["ALMNG"]))
                        {
                            if (txtLocat.Text.ToString() == dtLocat.Rows[Count - 1]["LOCAT"].ToString().Trim())
                            {
                                Count = Count - 1;
                            }
                        }
                    }

                    if (txtLocat.Text.ToString() == dtLocat.Rows[Count]["LOCAT"].ToString().Trim())
                    {
                        if (SumScan + Convert.ToInt32(txtQty.Text) > Convert.ToInt32(dtLocat.Rows[0]["RESIDUE"]))
                        {
                            MessageBox.Show("已扫描数量大于要转料号数量，请确认！");
                            return;
                        }
                        if (Convert.ToInt32(dtLocat.Rows[Count]["SCQTY"]) + Convert.ToInt32(txtQty.Text) > Convert.ToInt32(dtLocat.Rows[Count]["ALMNG"]))
                        {
                            MessageBox.Show(string.Format("{0}储位的库存数量不足，請確認!!", txtLocat.Text.Trim()), "提醒");
                            return;
                        }
                        if ((int)dtLocat.Rows[Count]["SCQTY"] > 0)
                        {
                            dtLocat.Rows[Count]["SCQTY"] = (int)dtLocat.Rows[Count]["SCQTY"] + Convert.ToInt32(txtQty.Text);
                            ScanData.Rows[Count]["MENGE"] = (int)ScanData.Rows[Count]["MENGE"] + Convert.ToInt32(txtQty.Text);
                        }
                        else
                        {
                            dtLocat.Rows[Count]["SCQTY"] = txtQty.Text;
                            DataRow dr = ScanData.NewRow();
                            dr["MBLNR"] = dtLocat.Rows[Count]["MBLNR"];
                            dr["MATNR"] = dtData.Rows[0]["MATNR"];
                            dr["Locat"] = txtLocat.Text;
                            dr["MENGE"] = txtQty.Text;
                            ScanData.Rows.Add(dr);
                            //item["SCQTY"] = txtQty.Text;
                        }
                        txtLocat.Focus();
                        Count += 1;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("请按照先进先出的顺序刷储位，现在应刷第{0}行，請確認!!", (Count + 1).ToString()));
                        return;
                    }
                    //foreach (DataRow item in dtLocat.Rows)
                    //{

                    //    if (SumScan + Convert.ToInt32(txtQty.Text) > Convert.ToInt32(dtLocat.Rows[0]["RESIDUE"]))
                    //    {
                    //        MessageBox.Show("已扫描数量大于要转料号数量，请确认！");
                    //        return;
                    //    }
                    //    if (txtLocat.Text.ToString().Trim() == item["Locat"].ToString().Trim())
                    //    {
                    //        if (Convert.ToInt32(txtQty.Text) > Convert.ToInt32(item["ALMNG"]))
                    //        {
                    //            MessageBox.Show(string.Format("{0}储位下的库存数量不足，請確認!!", txtLocat.Text.Trim()), "提醒");
                    //            return;
                    //        }
                    //        item["SCQTY"] = txtQty.Text;
                    //        DataRow dr = ScanData.NewRow();
                    //        dr["MBLNR"] = dtData.Rows[0]["MBLNR"];
                    //        dr["MATNR"] = dtData.Rows[0]["MATNR"];
                    //        dr["Locat"] = txtLocat.Text;
                    //        dr["MENGE"] = txtQty.Text;
                    //        ScanData.Rows.Add(dr);
                    //        item["SCQTY"] = txtQty.Text;
                    //        txtLocat.Focus();

                    //    }
                    //else 
                    //{
                    //    MessageBox.Show("储位不存在，请确认！");
                    //    return ;
                    //}
                    //}
                }

                if (ScanData.Rows.Count > 0)
                {
                    btSave.Enabled = true;
                }





            }

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否提交，请确认！", "提醒", MessageBoxButtons.OKCancel);

            if (dr == DialogResult.OK)
            {
                int SumScan = 0;
                for (int i = 0; i < dtLocat.Rows.Count; i++)
                {
                    SumScan += Convert.ToInt32(dtLocat.Rows[i]["SCQTY"]);
                }

                bool dt = objSapData.UpdateStatus(dtLocat, ScanData, SumScan, Progid);

                if (dt)
                {
                    this.stsWarning.Text = "保存成功";
                    Refresh();
                }
                else
                {
                    this.stsWarning.Text = "保存失败";
                }

            }
            else
            {
                this.stsWarning.Text = "取消保存";
            }
        }



    }
}
