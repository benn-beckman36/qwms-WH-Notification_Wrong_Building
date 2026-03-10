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

namespace QWMS
{
    public partial class TransferMaintainMail : Form
    {
        #region 变量

        UserInfo UserData = new UserInfo();
        private string strMailToType = "";
        private string strMailType = "";
        private DataTable dtData = new DataTable();
      


        #endregion
        public TransferMaintainMail(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
           
            try
            {
                QCI.QWMS.Replenishment StorageIn1 = new Replenishment(UserData, strProgid);
                //檢查權限
                if (!StorageIn1.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化

                    ShowMailToType();
                    

                    if (cmbMailToType.Items.Count > 0)
                    {
                        this.cmbMailToType.SelectedIndex = 0;
                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region 初始化
        //厂别       
        private void ShowMailToType()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Transfer objTransfer = new Transfer(UserData);
                cmbMailToType.Items.Clear();

                dtTemp = objTransfer.getMailToType();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbMailToType.Items.Add(dtTemp.Rows[i]["MailToType"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowMailToType()");
            }
        }
        #endregion
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }
        public void Query()
        {
            strMailToType = cmbMailToType.Text.ToString().Trim();
            strMailType = cmbMailType.Text.ToString().Trim();

            QCI.QWMS.Transfer objTransfer = new Transfer(UserData);
            dtData = objTransfer.getMailInfo(strMailToType, strMailType);

            DataColumn cSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(cSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }

            ShowDataGrid();
            
        }

       
        public void ShowDataGrid()
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

                DataGridViewTextBoxColumn dgvMailToType = new DataGridViewTextBoxColumn();
                dgvMailToType.DataPropertyName = "MailToType";
                dgvMailToType.HeaderText = "接收人类别";
                dgvMailToType.Width = 100;
                dgvMailToType.ReadOnly = true;
                this.dgvData.Columns.Add(dgvMailToType);


                DataGridViewTextBoxColumn dgvMailType = new DataGridViewTextBoxColumn();
                dgvMailType.DataPropertyName = "MailType";
                dgvMailType.HeaderText = "接受方式";
                dgvMailType.Width = 80;
                dgvMailType.ReadOnly = true;
                this.dgvData.Columns.Add(dgvMailType);

                DataGridViewTextBoxColumn dgvMail = new DataGridViewTextBoxColumn();
                dgvMail.DataPropertyName = "MailTo";
                dgvMail.HeaderText = "接受邮箱";
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            QCI.QWMS.Transfer objTransfer = new Transfer(UserData);
            int j = 0;
            
            for (int i = 0; i < dtData.Rows.Count; i++)
            {

                if ((bool)dtData.Rows[i]["Select"] == true)
                {
                    strMailToType = dtData.Rows[i]["MailToType"].ToString().Trim();
                    strMailType = dtData.Rows[i]["MailType"].ToString().Trim();
                    string strMail = dtData.Rows[i]["MailTo"].ToString().Trim();

                    objTransfer.updateMailInfo(strMailToType, strMailType, strMail);
                    
                    j++;
                }
                if (j > 0)
                {
                    lblwarning.Text = "保存成功！";
                }
                else
                {
                    lblwarning.Text = "没有选中要保存的行！";
                }
               
            }
             
        }

      

       
       
    }
}
