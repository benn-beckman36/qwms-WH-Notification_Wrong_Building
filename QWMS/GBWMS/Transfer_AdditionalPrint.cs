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
using QCI_QWMS_StorageData;

namespace QWMS
{
    public partial class Transfer_AdditionalPrint : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strDriver = "";
        private string strUsrnm = "";
        private string strProgid = "";
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
        public string StrDriver
        {
            get { return strDriver; }
            set { strDriver = value; }
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

        private string strTransferID = "";
        public string StrTransferID
        {
            get { return strTransferID; }
            set { strTransferID = value; }
        }

        private Admin objAdmin;
        private CarData objCarData;
        private Authority objAuthority;

           #region 构造函数
        public Transfer_AdditionalPrint(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objCarData = new CarData (UserData);
                objAuthority = new Authority(UserData);
                QCI.QWMS.Replenishment Replenishment = new Replenishment(UserData, strProgid);
                if (!Replenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion


        private void btnPrint_Click(object sender, EventArgs e)
        {
            string mblnr = "";
            mblnr = txtTransferID.Text;
            if (mblnr == "")
            {
                stsWarning.Text = "请输入要补印的调拨单！";
                return;
            }
            else
            {
                DataTable dtPrint = new DataTable();
                dtPrint = objCarData.TransferAdditionalPrint(mblnr);
                if (dtPrint.Rows.Count > 0)
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "TRUCKINGORDER", dtPrint);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
                else
                {
                    MessageBox.Show("无数据!");
                    return;
                }
            }
        }
    }
}
