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
using Microsoft.VisualBasic;

namespace QWMS
{
    public partial class TransferOut_Print : Form
    {
        #region 变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strDriver = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strChecked = "";
        private bool printagain = false;
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

        private Transfer objTransfer;

        #endregion


        #region 构造函数（UserData）  检查权限（UserData）
        public TransferOut_Print(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;
            btnprint.Enabled = false;
            chkagain.Enabled = false;

            try
            {
                objTransfer = new Transfer(UserData);
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

            ShowStatusData();
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


        #region   选择313/303/351
        private void RB313_CheckedChanged(object sender, EventArgs e)
        {
            strChecked = "SAP_313";
            gbType.Enabled = false;
            stsWarning.Text = "";
            chkagain.Enabled = true;
            DataTable dtTemp = new DataTable();
            dtTemp = objTransfer.returnPrintPO(UserData.UserId.ToString(), printagain, strChecked);
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                cmbPO.Items.Add(dtTemp.Rows[i]["MBLNR"].ToString());
            }
            cmbPO.SelectedIndex = -1;
        }

        private void RB303_CheckedChanged(object sender, EventArgs e)
        {
            strChecked = "SAP_303";
            gbType.Enabled = false;
            stsWarning.Text = "";
            chkagain.Enabled = true;
            DataTable dtTemp = new DataTable();
            dtTemp = objTransfer.returnPrintPO(UserData.UserId.ToString(), printagain, strChecked);
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                cmbPO.Items.Add(dtTemp.Rows[i]["MBLNR"].ToString());
            }
            cmbPO.SelectedIndex = -1;
        }

        private void RB351_CheckedChanged(object sender, EventArgs e)
        {
            strChecked = "SAP_351";
            gbType.Enabled = false;
            stsWarning.Text = "";
            chkagain.Enabled = true;
            DataTable dtTemp = new DataTable();
            dtTemp = objTransfer.returnPrintPO(UserData.UserId.ToString(), printagain, strChecked);
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                cmbPO.Items.Add(dtTemp.Rows[i]["MBLNR"].ToString());
            }
            cmbPO.SelectedIndex = -1;
        }
        #endregion

        #region  btnprint_Click
        private void btnprint_Click(object sender, EventArgs e)
        {
            if (strChecked == "")
            {
                stsWarning.Text = "请选择类型";
                return;
            }
            string mblnr = "";
            if (cmbPO.SelectedIndex == -1)
            {
                stsWarning.Text = "请选择正确的单号";
                btnprint.Enabled = false;
                return;
            }
            mblnr = cmbPO.Items[cmbPO.SelectedIndex].ToString();
            if (mblnr.Trim() == "")
            {
                stsWarning.Text = "请选择正确的单号";
                btnprint.Enabled = false;
                return;
            }
            DataTable dtPrint = new DataTable();
            dtPrint = objTransfer.TransferOutPrint(mblnr, strChecked);

            if (dtPrint.Rows.Count > 0)
            {
                #region  新增多次打印时需要输入特定账号和密码
                DataTable dtstatus = new DataTable();
                dtstatus = objTransfer.returnstatus(mblnr, strChecked);
                if (dtstatus.Rows[0]["ULFLG"].ToString() == "N")
                {
                    objTransfer.updatestatus(mblnr, strChecked);
                }
                else if (dtstatus.Rows[0]["ULFLG"].ToString() == "Y")
                {
                    #region  输入账号密码
                    stsWarning.Text = "";
                   // String login = Interaction.InputBox("请输入账号", "输入账号", "", 100, 100);
                    String login = QWMS.Transfer_InputBox.ShowInputBox("输入账号", "请输入账号", Upper: true, X: 100, Y: 100);
                    DataTable dtLogin = new DataTable();
                    Admin objAdmin = new Admin(UserData, Progid);
                    dtLogin = objAdmin.GetPassword("TRANSFER");
                    DataRow[] dr = dtLogin.Select("PASWD='" + login + "'");
                    if (dr.Count() == 0)
                    {
                        MessageBox.Show("请输入正确的账号谢谢！！！！！");
                        return;
                    }
                    else
                    {
                      //  String paswd = Interaction.InputBox("请输入密码", "输入密码", "", 100, 100);
                        String paswd = QWMS.Transfer_InputBox.ShowInputBox("输入密码", "请输入密码", strPassword: "*", X: 100, Y: 100);
                        //DataTable dtPwd = objAdmin.GetPassword(login);
                        //DataRow[] drPwd = dtPwd.Select("PASWD='" + paswd + "'");
                        //if (drPwd.Count() == 0)
                        //{
                        //    MessageBox.Show("请输入正确的密码谢谢！！！！！");
                        //    return;
                        //}
                        ClaCommon claCommon = new ClaCommon();
                        if (!claCommon.CheckAccount(login, paswd).GetAwaiter().GetResult())
                        {
                            MessageBox.Show("请输入正确的密码谢谢！！！！！");
                            return;
                        }
                    }
                    #endregion
                }
                #endregion
                string ReportPrintType = "TRANSFEROUT";
                ReportPrintType = ReportPrintType + strChecked;
                ReportPrint objReportPrint = new ReportPrint(UserData, ReportPrintType, dtPrint);
                objReportPrint.MdiParent = this.ParentForm;
                objReportPrint.Show();
            }
            else
            {
                MessageBox.Show("无数据!");
                return;
            }

        }
        #endregion



        #region btnRefresh_Click
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strChecked = "";
            cmbPO.SelectedIndex = -1;
            RB303.Checked = false;
            RB313.Checked = false;
            RB351.Checked = false;
            btnprint.Enabled = false;
            gbType.Enabled = true;
            chkagain.Checked = false;
            cmbPO.Items.Clear();
        }
        #endregion

        private void chkagain_CheckedChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbPO.SelectedIndex = -1;
            if (chkagain.Checked)
            {
                printagain = true;
            }
            else
            {
                printagain = false;
            }
            cmbPO.Items.Clear();

            DataTable dtTemp = new DataTable();
            dtTemp = objTransfer.returnPrintPO(UserData.UserId.ToString(), printagain, strChecked);
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                cmbPO.Items.Add(dtTemp.Rows[i]["MBLNR"].ToString());
            }
            cmbPO.SelectedIndex = -1;
            btnprint.Enabled = false;
        }

        private void cmbPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnprint.Enabled = true;
        }



    }
}
