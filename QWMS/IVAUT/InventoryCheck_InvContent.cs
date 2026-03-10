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
    public partial class InventoryCheck_InvContent : Form
    {
        DataTable dtInvContent = new DataTable();
        public InventoryCheck_InvContent(DataTable dtInv,int Flag)
        {
            InitializeComponent();
            dtInvContent = dtInv;                      
            ShowDataGrid(Flag);
        }

        #region  ShowData
        private void ShowDataGrid(int Flag)
        {
            dgvInvContent.AutoGenerateColumns = false;
            dgvInvContent.Columns.Clear();
            dgvInvContent.RowTemplate.Height = 20;
            try
            {
                if (Flag == 0)
                {
                    DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                    dgvcLocat.DataPropertyName = "LOCAT";
                    dgvcLocat.HeaderText = "储位";
                    dgvcLocat.ReadOnly = true;
                    dgvcLocat.Width = 70;
                    dgvInvContent.Columns.Add(dgvcLocat);

                    DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                    dgvcMatnr.DataPropertyName = "MATNR";
                    dgvcMatnr.HeaderText = "料号";
                    dgvcMatnr.ReadOnly = true;
                    dgvcMatnr.Width = 90;
                    dgvInvContent.Columns.Add(dgvcMatnr);

                    DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                    dgvcCharg.DataPropertyName = "CHARG";
                    dgvcCharg.HeaderText = "版本";
                    dgvcCharg.ReadOnly = true;
                    dgvcCharg.Width = 90;
                    dgvInvContent.Columns.Add(dgvcCharg);

                    DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                    dgvcMenge.DataPropertyName = "MENGE";
                    dgvcMenge.HeaderText = "QWMS数量";
                    dgvcMenge.ReadOnly = true;
                    dgvcMenge.Width = 70;
                    dgvInvContent.Columns.Add(dgvcMenge);

                    DataGridViewTextBoxColumn dgvcScqty = new DataGridViewTextBoxColumn();
                    dgvcScqty.DataPropertyName = "SCMENGE";
                    dgvcScqty.HeaderText = "刷入数量";
                    dgvcScqty.ReadOnly = true;
                    dgvcScqty.Width = 70;
                    dgvInvContent.Columns.Add(dgvcScqty);

                    DataGridViewTextBoxColumn dgvcDifqty = new DataGridViewTextBoxColumn();
                    dgvcDifqty.DataPropertyName = "MGDIF";
                    dgvcDifqty.HeaderText = "差异";
                    dgvcDifqty.ReadOnly = true;
                    dgvcDifqty.Width = 70;
                    dgvInvContent.Columns.Add(dgvcDifqty);

                    DataGridViewTextBoxColumn dgvcStats = new DataGridViewTextBoxColumn();
                    dgvcStats.DataPropertyName = "STATS";
                    dgvcStats.HeaderText = "状态";
                    dgvcStats.ReadOnly = true;
                    dgvcStats.Width = 60;
                    dgvInvContent.Columns.Add(dgvcStats);

                    DataGridViewTextBoxColumn dgvcRmark = new DataGridViewTextBoxColumn();
                    dgvcRmark.DataPropertyName = "RMARK";
                    dgvcRmark.HeaderText = "异常";
                    dgvcRmark.ReadOnly = true;
                    dgvcRmark.Width = 90;
                    dgvInvContent.Columns.Add(dgvcRmark);

                    DataGridViewTextBoxColumn dgvcCftim = new DataGridViewTextBoxColumn();
                    dgvcCftim.DataPropertyName = "CFTIM";
                    dgvcCftim.HeaderText = "盘点时间";
                    dgvcCftim.ReadOnly = true;
                    dgvcCftim.Width = 120;
                    dgvInvContent.Columns.Add(dgvcCftim);

                    DataGridViewTextBoxColumn dgvcCrwho = new DataGridViewTextBoxColumn();
                    dgvcCrwho.DataPropertyName = "CRWHO";
                    dgvcCrwho.HeaderText = "盘点人";
                    dgvcCrwho.ReadOnly = true;
                    dgvcCrwho.Width = 80;
                    dgvInvContent.Columns.Add(dgvcCrwho);
                }
                if(Flag==1)
                {
                    DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                    dgvcLocat.DataPropertyName = "LOCAT";
                    dgvcLocat.HeaderText = "储位";
                    dgvcLocat.ReadOnly = true;
                    dgvcLocat.Width = 70;
                    dgvInvContent.Columns.Add(dgvcLocat);
                }

                dgvInvContent.DataSource = dtInvContent;
                lblCount.Text = dtInvContent.Rows.Count + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion
    }
}
