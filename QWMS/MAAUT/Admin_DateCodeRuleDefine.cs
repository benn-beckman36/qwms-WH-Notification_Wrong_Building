using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{

    public partial class Admin_DateCodeRuleDefine : Form
    {
        private string strVendor = string.Empty;
        private string strDescription = string.Empty;
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strVendorCode = string.Empty;
        private string strUsrnm = string.Empty;
        private string strProgid = string.Empty;
        private string strDescriptionItem = string.Empty;
        private DataTable dtDCRule = new DataTable();
        UserInfo UserData = new UserInfo();
        Admin objAdmin;

        public Admin_DateCodeRuleDefine(UserInfo varUserData, string strFProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            strMandt = UserData.Client;
            strComcd = UserData.CompanyCode;
            strUsrnm = UserData.UserId;
            strProgid = strFProgid;
            try
            {
                objAdmin = new Admin(varUserData, strProgid);

                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDCDescription();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region ShowStatusData()
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = strMandt;
            this.stsComcd.Text = strComcd;
            this.stsUsrnm.Text = strUsrnm;
        }
        #endregion
        private void btnQuery_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            CheckVendor();
            if(cmbDescription.SelectedIndex != -1)
            {
                strDescription = cmbDescription.Items[cmbDescription.SelectedIndex].ToString();
                strDescriptionItem = strDescription.Substring(0, strDescription.IndexOf('-'));
            }
            else
            {
                strDescriptionItem = string.Empty;
            }
            dtDCRule = objAdmin.QueryDCRule(strVendor, strDescriptionItem);
            txtVendor.Text = strVendor = string.Empty;
            cmbDescription.SelectedIndex = -1;
            ShowDateCodeRuleGrid();
            gbFunction.Enabled = false;
        }

        private void CheckVendor()
        {
            strVendor = string.Empty;
            if (!string.IsNullOrEmpty(txtVendor.Text))
            {
                string strVendorText = txtVendor.Text.ToString().ToUpper().Replace("；", ";");
                string[] strVendors = strVendorText.Split(';');
                foreach (string str in strVendors)
                {
                    strVendor += "'" + str + "',";
                }
                strVendor = strVendor.Substring(1, strVendor.Length - 3);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            LogData objLogData = new LogData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, string.Empty, string.Empty, strProgid);
            CheckVendor();
            if (cmbDescription.SelectedIndex == -1)
            {
                stsWarning.Text = "Description can't be empty!!!";
                return;
            }
            if (string.IsNullOrEmpty(strVendor))
            {
                stsWarning.Text = "Vendor can't be empty!!!";
                return;
            }
            strDescription = cmbDescription.Items[cmbDescription.SelectedIndex].ToString();
            strDescriptionItem = strDescription.Substring(0, strDescription.IndexOf('-'));
            string strType = string.Empty;
            if (rdoAdd.Checked)
            {
                strType = "Add";
                if (objAdmin.QueryDCRule(strVendor, strDescriptionItem).Rows.Count > 0)
                {
                    cmbDescription.SelectedIndex = -1;
                    MessageBox.Show("This rule already exists!!!");
                    return;
                }
            }
            if(rdoDelete.Checked)
            {
                strType = "Delete";
            }
            objLogData.AddQWMSLOG("DateCode", strType, strDescriptionItem, "", "", UserData.UserId);
            if (objAdmin.DeleteOrAddDCRule(strType, strVendor, strDescriptionItem))
            {
                stsWarning.Text = "Update OK!!!";
                CheckVendor();
                dtDCRule = objAdmin.QueryDCRule(strVendor, string.Empty);
                ShowDateCodeRuleGrid();
                cmbDescription.SelectedIndex = -1;
            }
            else
                stsWarning.Text = "Update Fail!!!";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = string.Empty;
            gbFunction.Enabled = true;
            dtDCRule.Rows.Clear();
            strVendor = strDescription = strDescriptionItem = string.Empty;
            txtVendor.Text = string.Empty;
            cmbDescription.SelectedIndex = -1;
        }

        public void ShowDateCodeRuleGrid()
        {
            dgvDCData.Columns.Clear();
            dgvDCData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "RuleItem";
                dgvcItem.HeaderText = "Rule Item";
                dgvcItem.ReadOnly = true;
                dgvcItem.Width = 100;
                dgvDCData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcDescribe = new DataGridViewTextBoxColumn();
                dgvcDescribe.DataPropertyName = "Describe";
                dgvcDescribe.HeaderText = "Rule Description";
                dgvcDescribe.ReadOnly = true;
                dgvcDescribe.Width = 400;
                dgvDCData.Columns.Add(dgvcDescribe);

                DataGridViewTextBoxColumn dgvcVedat = new DataGridViewTextBoxColumn();
                dgvcVedat.DataPropertyName = "VEDOR";
                dgvcVedat.HeaderText = "Vendor Code";
                dgvcVedat.ReadOnly = true;
                dgvcVedat.Width = 400;
                dgvDCData.Columns.Add(dgvcVedat);

                dgvDCData.DataSource = dtDCRule;
                lblCount.Text = dtDCRule.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDateCodeRuleGrid()");
            }
        }

        private void ShowDCDescription()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbDescription.Items.Clear();
                dtTemp = objAdmin.QueryDCRule("", "");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbDescription.Items.Add(dtTemp.Rows[i]["RuleItem"].ToString() + "- " + dtTemp.Rows[i]["Describe"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDCDescription()");
            }
        }
    }
}
