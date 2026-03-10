using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class Admin_LocationPrint : Form
	{
		UserInfo UserData = new UserInfo();
		private string strMandt = string.Empty;
		private string strComcd = string.Empty;
		private string strUsrnm = string.Empty;
		private string strProgid = string.Empty;

		public Admin_LocationPrint(UserInfo varUserData, string Progid)
        {
            InitializeComponent();
			UserData = varUserData;

			strMandt = UserData.Client;
			strComcd = UserData.CompanyCode;
			strUsrnm = UserData.UserId;
			strProgid = Progid;

			try
			{
				QCI.QWMS.Admin objAdmin = new QCI.QWMS.Admin(UserData, Progid);

				//检查权限
				if (!objAdmin.CheckAuthority())
				{
					throw new Exception("You don't have right to use this program!!");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        private void btPrint_Click(object sender, EventArgs e)
        {
			btPrint.Enabled = false;
			DataTable dtPrint = new DataTable();
			DataTable dtImport = new DataTable();
			string strPrintModel = string.Empty;
			QWMS.Common.ClaExeclHelper objExcel = new QWMS.Common.ClaExeclHelper();
			try
			{
				if (string.IsNullOrEmpty(txtFile.Text.Trim()))
				{
					lblStatus.Text = "请选择一个文件";
					return;
				}
				dtPrint = objExcel.GetDataTableFromExcel(txtFile.Text.Trim(), true);
				if (dtPrint.Rows.Count == 0 || !dtPrint.Columns.Contains("LOCAT"))
				{
					MessageBox.Show("无数据");
					return;
				}
				dtImport.Columns.Add("1");
				dtImport.Columns.Add("LOCAT");
				foreach (DataRow dr in dtPrint.Rows)
				{
					dtImport.Rows.Add("", dr["LOCAT"]);
				}
			}
			catch
			{
				MessageBox.Show("读取文件失败");
				return;
			}
			if (txtIP.Text.Trim() == "")
			{
				MessageBox.Show("请输入IP地址");
				return;
			}
			string strIP = txtIP.Text.Trim();
			if (rdbLFour.Checked) strPrintModel = "Four_Big_Model_Locat.txt";
			if (rdbSFour.Checked) strPrintModel = "Four_Small_Model_Locat.txt";
            //不用
            //if (rdbLSix.Checked) strPrintModel = "Six_Big_Model_Locat.txt";
			if(string.IsNullOrEmpty(strPrintModel))
            {
				MessageBox.Show("请至少选择一种储位类型");
				return;
            }

			string AllContexttmp = "";
			string strFilePath = Application.StartupPath + "\\Templates\\" + strPrintModel;
			if (!File.Exists(strFilePath))
			{
				MessageBox.Show("未找到打印模板");
				return;
			}
			StreamReader sr = new StreamReader(strFilePath, Encoding.Default);
			AllContexttmp = sr.ReadToEnd();
			sr.Close();
			for (int i = 0; i < dtImport.Rows.Count; i++)
			{
				lblStatus.Text = "开始打印";
				string AllContext = AllContexttmp;
				try
				{
					for (int x = 1; x < dtImport.Columns.Count; x++)
					{
						AllContext = AllContext.Replace(dtImport.Columns[x].Caption.ToString(), dtImport.Rows[i][x].ToString());
					}
					string[] line = Regex.Split(AllContext, "\r\n");
					int j = 1;
					string[] array = line;
					foreach (string ss in array)
					{
						if (ss.IndexOf("<NA>") <= -1)
						{
							if (j == 1)
							{
								AllContext = ss;
								j++;
							}
							else
							{
								AllContext = AllContext + "\r\n" + ss;
								j++;
							}
						}
					}
					PrintLabelIP(strIP, AllContext);
					Thread.Sleep(1000);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString() + "-<PrintData()>");
					return;
				}
			}
			lblStatus.Text = "打印完成";
		}
		private void PrintLabelIP(string strIP, string strLabel)
		{
			string strPort = "9100";
			IPEndPoint hostEndPoint = new IPEndPoint(IPAddress.Parse(strIP), Convert.ToInt32(strPort));
			Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			s.Connect(hostEndPoint);
			if (!s.Connected)
			{
				MessageBox.Show("连接不上打印机");
				return;
			}
			byte[] data = Encoding.UTF8.GetBytes(strLabel);
			s.Send(data, data.Length, SocketFlags.None);
			if (s.Connected)
			{
				s.Close();
			}
		}

        private void btFile_Click(object sender, EventArgs e)
        {
			if (ofdOpenFile.ShowDialog() == DialogResult.OK)
			{
				txtFile.Text = ofdOpenFile.FileName;
			}
		}

        private void txtFile_DragEnter(object sender, DragEventArgs e)
        {
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Link;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

        private void txtFile_DragDrop(object sender, DragEventArgs e)
        {
			txtFile.Text = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
		}

        private void lnkSample_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			QCI.QWMS.Admin objTempAdmin = new QCI.QWMS.Admin(UserData, strProgid);
			DataTable dtDocumentPathTemp = new DataTable();
			dtDocumentPathTemp = objTempAdmin.QuaryDocumentPath();
			if (dtDocumentPathTemp.Rows.Count > 0)
			{
				string strPath = dtDocumentPathTemp.Rows[0][0].ToString() + "储位批量打印模板.xlsx";
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            btPrint.Enabled=true;
            rdbLFour.Checked = false;
            rdbSFour.Checked = false;
            txtFile.Text = "";
        }

    }
}
