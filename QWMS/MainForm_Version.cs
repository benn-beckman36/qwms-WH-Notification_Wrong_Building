using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QWMS
{
    public partial class MainForm_Version : Form
    {
        private DataTable dtVERSION = new DataTable();
        public bool bolClose = false;
        string strFileName = "";
        public MainForm_Version(string strConfigVersion, DataTable dtNewVersion, string strComcd)
        {

            InitializeComponent();
            txtPUVI.Text = strConfigVersion; //使用者程式版本
            txtPRVI.Text = dtNewVersion.Rows[0]["CTRLNM"].ToString(); //系統最新版本
            txtRVMT.Text = dtNewVersion.Rows[0]["CTRLC3"].ToString(); //新版本發布時間

            switch (strComcd)
            {
                case "9100":
                    rxtReference.Text = "您使用的版本已無法使用，請至以下路徑下載QWMS使用 \n  下載路徑:  http://172.17.0.33/WinAP/QSMCQWMS/publish.htm \n  檔案名稱: " + strFileName + "";
                    break;
                case "9200":
                    rxtReference.Text = "您使用的版本已無法使用，請至以下路徑下載QWMS使用 \n  下載路徑:  http://172.17.0.33/WinAP/QSMCQWMS/publish.htm \n  檔案名稱: " + strFileName + "";
                    break;
                case "9600":
                    rxtReference.Text = "您使用的版本已無法使用，請至以下路徑下載QWMS使用 \n  下載路徑:  http://172.17.0.33/WinAP/QSMCQWMS/publish.htm \n  檔案名稱: " + strFileName + "";
                    break;
                case "9800":
                    rxtReference.Text = "您使用的版本已無法使用，請至以下路徑下載QWMS使用 \n  下載路徑:  http://172.17.0.33/WinAP/QSMCQWMS/publish.htm \n  檔案名稱: " + strFileName + "";
                    break;
                case "9900":
                    rxtReference.Text = "您使用的版本已無法使用，請至以下路徑下載QWMS使用 \n  下載路徑:  http://172.17.0.33/WinAP/QSMCQWMS/publish.htm \n  檔案名稱: " + strFileName + "";
                    break;
                case "9110":
                    rxtReference.Text = "您使用的版本已無法使用，請至以下路徑下載QWMS使用 \n  下載路徑:  http://172.17.0.33/WinAP/QSMCQWMS/publish.htm \n  檔案名稱: " + strFileName + "";
                    break;
                case "7300":
                    rxtReference.DetectUrls = false;
                    rxtReference.Text = "您使用的版本已無法使用，請至以下路徑下載QWMS使用 \n" + "下載路徑: " + @"\\scmdb31\QSMC_QWMS_Pub\QMH_QWMS_Offline\2024\QMH QWMS NEW" + "\n" +"檔案名稱: " + strFileName + "";
                    break;
                default:
                    rxtReference.Text = "您使用的版本已無法使用，請至以下路徑下載QWMS使用 \n  下載路徑:  http://172.17.0.33/WinAP/QSMCQWMS/publish.htm \n  檔案名稱: " + strFileName + "";
                    break;
        
            }
            bolClose = true;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
