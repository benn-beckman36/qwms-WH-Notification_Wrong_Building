using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Admin_OldDataDelete : Form
    {
        #  region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private Admin objAdmin;
        private Authority objAuthority;
        private ArrayList aryLgort = new ArrayList();

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

        public ArrayList Lgorts
        {
            get
            {
                return aryLgort;
            }
            set
            {
                aryLgort = value;
            }
        }
        # endregion

        public Admin_OldDataDelete()
        {
            InitializeComponent();
        }

        public Admin_OldDataDelete(UserInfo varUserData, string strWerks, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Werks = strWerks;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objAuthority = new Authority(UserData);

                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        # region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        # endregion

        # region 带出plant下拉框资料
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
        # endregion

        # region FORM尺寸变化
        private void Admin_OldDataDelete_Resize(object sender, EventArgs e)
        {
            panel4.Size = new System.Drawing.Size((int)(this.Size.Width * 0.5), panel4.Size.Height);
            panel6.Size = new System.Drawing.Size((int)(this.Size.Width * 0.33), panel6.Size.Height);
            panel8.Size = new System.Drawing.Size((int)(this.Size.Width * 0.33), panel8.Size.Height);
            panel9.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel9.Size.Height);
            panel10.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel10.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }
        # endregion

        # region Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ArrayList aryType = new ArrayList();
            try
            {
                //Log
                if (chkLog.Checked == true)
                    aryType.Add("0");
                //盤點票
                if (chkCounting.Checked == true)
                    aryType.Add("1");
                //SAP下載資料
                if (chkDownload.Checked == true)
                    aryType.Add("2");

                if (cmbWerks.SelectedIndex == -1 || string.IsNullOrEmpty(cmbLgort.Text))
                {
                    stsWarning.Text = "Plant and Storage can't be empty!!";
                    return;
                }

                if (aryType.Count == 0)
                {
                    stsWarning.Text = "Please select the data type that you want to delete!!";
                    return;
                }
                //Modify by Jack 20150915 循环执行多仓别删除操作
                string[] strLgorts = cmbLgort.Text.Trim().ToString().Split(',');
                for (int i = 0; i < strLgorts.Length; i++)
                {
                    //if (objAdmin.DeleteOldLog(aryType, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), dtpStartDate.Value.ToString("yyyyMMdd") + " 00:00:00", dtpEndDate.Value.ToString("yyyyMMdd") + " 23:59:59"))
                    if (objAdmin.DeleteOldLog(aryType, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), strLgorts[i], dtpStartDate.Value.ToString("yyyyMMdd") + " 00:00:00", dtpEndDate.Value.ToString("yyyyMMdd") + " 23:59:59"))
                    {
                        stsWarning.Text = "Delete OK!";
                    }
                    else
                    {
                        stsWarning.Text = strLgorts[i] + " Delete fail!" + objAdmin.ERRMSG;
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            chkLog.Checked = false;
            chkDownload.Checked = false;
            chkCounting.Checked = false;
            cmbWerks.SelectedIndex = -1;
            cmbLgort.Text="";
            panel11.Enabled = false;
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
        }
        # endregion

        # region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # endregion

        # region Plant SelectedChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWerks.SelectedIndex > -1)
            {
                panel11.Enabled = true;
                chkLog.Enabled = true;
                chkCounting.Enabled = true;
                chkDownload.Enabled = true;
                stsWarning.Text = "";
                ShowDdlLgort();
            }
        }
        # endregion

        # region ShowDdlLgort
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
        # endregion

        //仓别多选按钮
        private void btnMore_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex > -1)//判断是否选择了厂区
                {
                    DataTable dtLgort = new DataTable();
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtLgort = objAuthority.CheckLgortAuthority(strWerks);//此列包含WHCTRL表的所有列

                    Admin_StorageSelect objAdmin_StorageSelect = new Admin_StorageSelect(UserData, Werks, Progid, dtLgort);
                    objAdmin_StorageSelect.ShowDialog();
                    Lgorts = objAdmin_StorageSelect.Lgorts;
                    cmbLgort.Text = GetLgortData();
                    if (Lgorts.Count > 0)
                    {
                        this.cmbLgort.Enabled = false;
                    }
                }
                else
                {
                    stsWarning.Text = "Plant can't be empty!!";
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #region GetMblnrData 多选仓别加","带到cmbLgort
        private string GetLgortData()
        {
            try
            {
                StringBuilder sbLgort = new StringBuilder();
                sbLgort.Remove(0, sbLgort.Length);
                for (int i = 0; i < Lgorts.Count; i++)
                {
                    if (i != 0)
                        sbLgort.Append(",");
                    sbLgort.Append(Lgorts[i].ToString().Trim());
                }
                return sbLgort.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetLgortData()");
            }
        }
        #endregion
    }
}
