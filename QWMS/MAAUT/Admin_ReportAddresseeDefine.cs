using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class Admin_ReportAddresseeDefine : Form
    {
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strReport = "";
        private string strProgid = "";
        private string strComcd = "";
        UserInfo UserData = new UserInfo();
        private DataTable dtData;
        private string strType = "";
        private Admin objAdmin;
        private string[] strAdres = {};
        DataTable dtAddressee = new DataTable();
        string strAddressee = "";

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

        public string Report
        {
            get
            {
                return strReport;
            }
            set
            {
                strReport = value;
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

        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
            }
        }

        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }

        public Admin_ReportAddresseeDefine(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            strComcd = varUserData.CompanyCode;
            Progid = strProgid;
            Data = dtData;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();

                    dtData = new DataTable();
                    dtData.Columns.Add("ADRES", Type.GetType());
                    

                    Data = dtData;

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbReport.Items.Count > 0)
                    {
                        this.cmbReport.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        private void cmbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlReport();
        }

        private void ShowDdlReport()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckReportAuthority(strWerks);
                }
                if (cmbReport.SelectedIndex != -1)
                {
                    strReport = cmbReport.Items[cmbReport.SelectedIndex].ToString();
                }
                else
                {
                    cmbReport.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbReport.Items.Clear();
                    strReport = "";
                }
                else
                {
                    cmbReport.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbReport.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strReport && strReport != "")
                        {
                            cmbReport.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            Data.Rows.Clear();
            DataRow drRow;
            if (cmbWerks.SelectedIndex != -1)
            {
                Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            else
            {
                Werks = "";
            }
            if (cmbReport.SelectedIndex != -1)
            {
                Report = cmbReport.Items[cmbReport.SelectedIndex].ToString();
            }
            else
            {
                Report = "";
            }
            if (Werks == "")
            {
                stsWarning.Text = "Plant can't be empty!!";
                return;
            }

            if (Report == "")
            {
                stsWarning.Text = "请选择报表!!";
                return;
            }
            QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
            dtAddressee = objAuthority.QueryAddressee(Werks, Report);
            strAddressee = dtAddressee.Rows[0]["REMAK2"].ToString();
            strAdres = strAddressee.Split(';');

            for (int i = 0; i < strAdres.Count(); i++)
            {
                if (!string.IsNullOrEmpty(strAdres[i]))
                {
                    drRow = Data.NewRow();
                    drRow["ADRES"] = strAdres[i];
                    Data.Rows.Add(drRow);
                }
            }
            ShowDataGridAll();
            this.txtAdr.Enabled = true;
            this.btnSave.Enabled = true;
            this.rdbAdd.Enabled = true;
            this.rdbDelete.Enabled = true;
        }

        private void ShowDataGridAll()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcAdres = new DataGridViewTextBoxColumn();
                dgvcAdres.DataPropertyName = "ADRES";
                dgvcAdres.HeaderText = "Addreess";
                dgvcAdres.ReadOnly = true;
                dgvcAdres.Width = 600;
                dgvData.Columns.Add(dgvcAdres);

                dgvData.DataSource = Data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Operation = "";
            int count = 0;
            if (this.txtAdr.Text.ToString() == "")
            {
                stsWarning.Text = "收件人不可为空!!";
                return;
            }
            if (rdbAdd.Checked == false && rdbDelete.Checked == false)
            {
                stsWarning.Text = "选择新增或是删除！";
                return;
            }
            if (rdbAdd.Checked == true)
            {
                Operation = "ADD";
            }
            if (rdbDelete.Checked == true)
            {
                Operation = "DELETE";
            }
            for (int i = 0; i < strAdres.Count(); i++)
            {
                if (strAdres[i] == this.txtAdr.Text.ToString().Trim() && rdbAdd.Checked == true)
                {
                    stsWarning.Text = "收件人已存在！";
                    return;
                }
                if (rdbDelete.Checked == true)
                {
                    if (strAdres[i] == this.txtAdr.Text.ToString().Trim())
                    {
                        break;
                    }
                    else
                    {
                        count++;
                    }
                    if (i==strAdres.Count()-1&&count == i+1)
                    {
                        stsWarning.Text = "收件人不存在！";
                        return;
                    }
                }
            }
            if(objAdmin.AddOrDeleteAddressee(Werks, Report,strAddressee, this.txtAdr.Text.Trim().ToString(),Operation))
            {
                if (rdbAdd.Checked == true)
                {
                    stsWarning.Text = "update OK!";
                    this.txtAdr.Enabled = false;
                    btnSave.Enabled = false;
                    btnConfirm_Click(null,null);
                }
                if (rdbDelete.Checked == true)
                {
                    stsWarning.Text = "delete OK!";
                    this.txtAdr.Enabled = false;
                    btnSave.Enabled = false;
                    btnConfirm_Click(null, null);
                }
            }
        }

        private void brnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.txtAdr.Enabled = false;
            this.txtAdr.Text = "";
            this.btnSave.Enabled = false;
            this.rdbAdd.Enabled = false;
            this.rdbDelete.Enabled = false;
            Data.Rows.Clear();
            dgvData.DataSource = null;
            strWerks = "";
            strReport = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
