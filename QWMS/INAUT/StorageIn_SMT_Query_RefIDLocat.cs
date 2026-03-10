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
    public partial class StorageIn_SMT_Query_RefIDLocat : Form
    {
        UserInfo UserData = new UserInfo();
        private DataTable dtRefID = new DataTable();
        private string strRefID = "";
        public StorageIn_SMT_Query_RefIDLocat(UserInfo varUserData)
        {
            InitializeComponent();
            ShowDataGrid();
        }

        #region ShowDataGrid
        public void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "Reference ID.";
                dgvcRefid.ReadOnly = true;
                dgvcRefid.Width = 100;
                dgvData.Columns.Add(dgvcRefid);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "DID No.";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 160;
                dgvData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 80;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOGSQL";
                dgvcLocat.HeaderText = "Locat";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 90;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcTime = new DataGridViewTextBoxColumn();
                dgvcTime.DataPropertyName = "LOGTIM";
                dgvcTime.HeaderText = "上次刷入时间";
                dgvcTime.ReadOnly = true;
                dgvcTime.Width = 100;
                dgvData.Columns.Add(dgvcTime);

                dgvData.DataSource = dtRefID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        private void txtDidNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                string strDidNo = txtDidNo.Text.ToString().Trim();
                txtDidNo.Text = string.Empty;
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT REFID,MBLNR,LOGSQL,MATNR,LOGTIM FROM ERRLOG WITH(NOLOCK) WHERE MBLNR='{0}'", strDidNo);
                dtRefID = objStorageData.GetPrintResult(sb.ToString());
                if (dtRefID.Rows.Count > 0)
                {
                    for (int i = 0; i < dtRefID.Rows.Count; i++)
                    {
                        dtRefID.Rows[i]["LOGSQL"] = dtRefID.Rows[i]["LOGSQL"].ToString().Substring(dtRefID.Rows[i]["LOGSQL"].ToString().LastIndexOf("/") + 1, dtRefID.Rows[i]["LOGSQL"].ToString().Length - dtRefID.Rows[i]["LOGSQL"].ToString().LastIndexOf("/") - 1);
                    }
                }
            }
            ShowDataGrid();
        }
    }
}
