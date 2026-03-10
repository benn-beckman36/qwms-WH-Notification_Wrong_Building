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
using QCI_QWMS_StorageOut;

namespace QWMS
{
    public partial class MatainMail : Form
    {
        #region 变量

        private UserInfo UserData = new UserInfo();
        private string strWerks = "";
        private string strMailType = "";
        private string strMailClass = "";
        string strUmlgo = "";
        string strMail = "";
        string strType = "";
        private DataTable dtData = new DataTable();
        AddDoc objAddDoc;
  
        #endregion
        public MatainMail(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            objAddDoc = new AddDoc(UserData);
            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, strProgid);
                Admin objAdmin = new Admin(UserData,strProgid);
                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowDdlWerks();
                    ShowDdlMailType();
                    Query();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        #region 初始化
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        private void ShowDdlMailType()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cmbMailClass.Items.Clear();
                dtTemp = objAddDoc.GetMailType();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbMailClass.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlMailType()");
            }
        }

        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }
        public void Query()
        {
            strMailClass = cmbMailClass.Text.ToString().Trim();
            strMailType = cmbMailType.Text.ToString().Trim();
            strWerks = cmbWerks.Text.ToString().Trim();
            strUmlgo = txtUMLGO.Text;
            strMailType = strMailType == "收件" ? "0" : "1";
            dtData = objAddDoc.GetMailInfo(strWerks,strUmlgo, strMailType,strMailClass);

            DataColumn cSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(cSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }
            ShowMailData();
           
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            #region 新增
            if (strType == "Add")
            {
                strMailClass = cmbMailClass.Text.ToString();
                strMail = txtMail.Text;
                strMailType = cmbMailType.Text.ToString().Trim();
                strWerks = cmbWerks.Text.ToString().Trim();
                strUmlgo = txtUMLGO.Text.Trim() ;


                if (strMailType == "" || strMail == "" || strWerks == "" || strMailClass=="")
                {
                    lblwarning.Text = "收件方式、邮件类型、收件人、仓别、厂区不能为空";
                    return;
                }
                if (strMailClass == "AddDoc Mail Notice" && strUmlgo == "")
                {
                    if (strUmlgo == "")
                    { 
                        lblwarning.Text = "加扣邮件必须维护仓别";
                        return;
                    }

                    if (objAddDoc.CheckUmExist(strWerks, strUmlgo, strMailType))
                    {
                        lblwarning.Text = strUmlgo + ",该仓别已有数据请修改即可！";
                        Query();
                        return;
                    }
                }
                strMailType = strMailType == "收件" ? "0" : "1";
                if (objAddDoc.InsertMailInfo(strWerks, strUmlgo, strMail, strMailType,strMailClass))
                {
                    lblwarning.Text = "新增成功！";
                    Query();
                    return;
                }
                else
                {
                    lblwarning.Text = "新增失败！";
                }
            }
            #endregion

            #region 删除和修改
            if (strType == "Update" || strType == "Delete")
            {
                DataRow[] drSelect = dtData.Select(" Select='true' ");
                if (drSelect.Length > 0)
                {
                    #region 修改
                    if (strType == "Update")
                    {
                        foreach (DataRow dr in drSelect)
                        {
                            strWerks = dr["WERKS"].ToString().Trim();
                            strUmlgo = dr["LGORT"].ToString().Trim();
                            strMailType = dr["MTYPE"].ToString().Trim();
                            strMail = dr["EMAIL"].ToString().Trim();
                            strMailClass=dr["FUNCT"].ToString().Trim();
                            strMailType = strMailType == "收件" ? "0" : "1";
                            if (objAddDoc.updateMailInfo(strWerks, strUmlgo, strMailType, strMail,strMailClass))
                            {
                                lblwarning.Text = "保存成功！";
                                Query();
                            }
                            else
                            {
                                lblwarning.Text = dr["LGORT"].ToString() + "  仓别保存失败，请确认信息是否正确！ ";
                                return;
                            }
                        }

                    }
                    #endregion

                    #region 删除

                    if (strType == "Delete")
                    {
                        foreach (DataRow dr in drSelect)
                        {
                            strWerks = dr["WERKS"].ToString().Trim();
                            strUmlgo = dr["LGORT"].ToString().Trim();
                            strMailType = dr["MTYPE"].ToString().Trim();
                            strMail = dr["EMAIL"].ToString().Trim();
                            strMailClass = dr["FUNCT"].ToString().Trim();
                            strMailType = strMailType == "收件" ? "0" : "1";
                            if (objAddDoc.DeleteMailInfo(strWerks, strUmlgo, strMailType,strMailClass,strMail))
                            {
                                lblwarning.Text = "删除成功！";
                                Query();
                            }
                            else
                            {
                                lblwarning.Text = dr["LGORT"].ToString() + "  仓别删除失败！ ";
                                return;
                            }
                        }
                    }

                    #endregion

                }
                else
                {
                    lblwarning.Text = "请选择要保存的行！";
                }
            }
            #endregion

        }

        #region 邮件通知ShowMailData()
        public void ShowMailData()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "选择";
                dgvcSelect.Width = 50;
                this.dgvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvWerks = new DataGridViewTextBoxColumn();
                dgvWerks.DataPropertyName = "WERKS";
                dgvWerks.HeaderText = "厂区";
                dgvWerks.Width = 80;
                dgvWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvWerks);

                DataGridViewTextBoxColumn dgvMailUMLGO= new DataGridViewTextBoxColumn();
                dgvMailUMLGO.DataPropertyName = "LGORT";
                dgvMailUMLGO.HeaderText = "仓别";
                dgvMailUMLGO.Width = 80;
                dgvMailUMLGO.ReadOnly = true;
                this.dgvData.Columns.Add(dgvMailUMLGO);

                DataGridViewTextBoxColumn dgvMailType = new DataGridViewTextBoxColumn();
                dgvMailType.DataPropertyName = "MTYPE";
                dgvMailType.HeaderText = "接受方式";
                dgvMailType.Width = 80;
                dgvMailType.ReadOnly = true;
                this.dgvData.Columns.Add(dgvMailType);


                DataGridViewTextBoxColumn dgvMail = new DataGridViewTextBoxColumn();
                dgvMail.DataPropertyName = "EMAIL";
                dgvMail.HeaderText = "接收邮箱";
                dgvMail.Width = 1100;
                dgvMail.ReadOnly = false;
                //dgvMail.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dgvData.Columns.Add(dgvMail);


                dgvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        #endregion

        private void rdbtnInsert_CheckedChanged(object sender, EventArgs e)
        {
            lblMail.Visible = true;
            txtMail.Visible = true;
            gbFunction.Enabled = false;
            strType = "Add";

        }

        private void rdbtnDelete_CheckedChanged(object sender, EventArgs e)
        {
            lblMail.Visible = false;
            txtMail.Visible = false;
            gbFunction.Enabled = false;
            strType = "Delete";
        }
        private void rdbtnUpdate_CheckedChanged(object sender, EventArgs e)
        {
            gbFunction.Enabled = false;
            lblMail.Visible = false;
            txtMail.Visible = false;
            strType = "Update";
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gbFunction.Enabled = true;
            lblMail.Visible = false;
            txtMail.Visible = false;
            rdbtnDelete.Checked = false;
            rdbtnInsert.Checked = false;
            rdbtnUpdate.Checked = false;
            gbFunction.Enabled = true;
            lblwarning.Text = "";
            txtMail.Text = "";
            txtUMLGO.Text = "";
            strType = "";
            strMailClass = "";
            cmbMailClass.Text = "";


        }

        private void cmbMailClass_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbMailClass.Text.ToString().Trim() == "TransferCar_Email")
            {
                lblTran.Text = "注：邮件类型为TransferCar_Email时，查询无需选择厂区仓别；新增时无需填写仓别";
            }
        }

  


    }
}
