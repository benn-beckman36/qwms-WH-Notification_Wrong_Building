using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace QWMS.MGAUT
{
    public partial class Manage_SapSimulationDataPrint_Split : Form
    {
        DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        QWMS.Common.ClaCommon claCommon = new ClaCommon();
        string WEKRS = string.Empty;
        string LGORT = string.Empty;
        string BOXID = string.Empty;
        string MBLNR = string.Empty;
        string MENGE = string.Empty;
        string LIFNR = string.Empty;
        public Manage_SapSimulationDataPrint_Split(UserInfo varUserData, string strWerks, string strLgort, string strMblnr, string strBoxid, string strTotalMenge, string strLifnr)
        {
            InitializeComponent();
            UserData = varUserData;
            WEKRS = strWerks;
            LGORT = strLgort;
            MBLNR = strMblnr;
            BOXID = strBoxid;
            MENGE = strTotalMenge;
            LIFNR = strLifnr;
            txtBoxid.Text = BOXID;
            txtTotal.Text = MENGE;
            if (dtData.Columns.Count == 0)
            {
                dtData.TableName = "QWMS";
                dtData.Columns.Add("Item");
                dtData.Columns.Add("BOXID");
                dtData.Columns.Add("MENGE");
                dtData.Columns.Add("DACOD");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(txtSplitTotal.Text) == Convert.ToInt32(txtTotal.Text))
                {
                    StorageData objStorageData = new StorageData(UserData, WEKRS, LGORT);
                    if (dtData.Rows.Count > 0)
                    {
                        string XML = claCommon.ConvertDataTableToXML(dtData);
                        if (objStorageData.ExecuteCombineOrSplit(XML, MBLNR, BOXID, "0"))
                        {
                            MessageBox.Show("拆箱成功");
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先拆分数量");
                    }
                }
                else
                {
                    MessageBox.Show("拆分的数量与原来的数量不一致，请确认");
                }
            }
            catch
            {
                MessageBox.Show("请先拆分数量，输入数字回车即可，拆分完成后再点击SAVE。");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtSplitQty.Text = string.Empty;
            this.txtSplitTotal.Text = string.Empty;
            this.txtDacod.Text = string.Empty;
            this.dgvData.DataSource = null;
        }

        private void txtSplitQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtSplitQty.Text == "")
                {
                    MessageBox.Show("请输入拆分数量");
                    return;
                }

                if (!claCommon.IsNumber(txtSplitQty.Text.Trim()))
                {
                    MessageBox.Show("请输入正确的数量");
                    return;
                }
                if (Convert.ToInt32(txtSplitQty.Text) + Convert.ToInt32(txtSplitTotal.Text == "" ? "0" : txtSplitTotal.Text) > Convert.ToInt32(txtTotal.Text))
                {
                    MessageBox.Show("拆分的数量不能超过BOXID的总数量，请确认");
                    return;
                }
                else
                {
                    DataRow drSplit = dtData.NewRow();
                    drSplit["BOXID"] = txtBoxid.Text;
                    drSplit["MENGE"] = txtSplitQty.Text.Trim();
                    if (!string.IsNullOrEmpty(txtDacod.Text.Trim().ToString()))
                    {
                        if (string.IsNullOrEmpty(LIFNR))
                        {
                            txtDacod.Text = string.Empty;
                            MessageBox.Show("此票无厂商信息，无法输入Date Code，请确认");
                            return;
                        }
                        else
                        {
                            StorageData objStorageData = new StorageData(UserData, WEKRS, LGORT);
                            string DC_After = objStorageData.WHDCR_Query(LIFNR, txtDacod.Text.Trim().ToString());
                            if (string.IsNullOrEmpty(DC_After))
                            {
                                txtDacod.Text = string.Empty;
                                MessageBox.Show("此Date Code无对应转换规则，请确认！");
                                return;
                            }
                            else
                            {
                                drSplit["DACOD"] = txtDacod.Text.Trim();
                            }
                        }
                    }
                    else
                    {
                        drSplit["DACOD"] = "";
                    }
                    dtData.Rows.Add(drSplit.ItemArray);
                    if (txtSplitTotal.Text == "")
                    {
                        txtSplitTotal.Text = txtSplitQty.Text.ToString();
                    }
                    else
                    {
                        txtSplitTotal.Text = (Convert.ToInt32(txtSplitTotal.Text) + Convert.ToInt32(txtSplitQty.Text)).ToString();
                    }
                }
                ShowDataGrid();

            }
        }

        public void ShowDataGrid()
        {
            dgvData.Columns.Clear();
            dgvData.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn dgvBOXID = new DataGridViewTextBoxColumn();
            dgvBOXID.DataPropertyName = "BOXID";
            dgvBOXID.HeaderText = "BOXID";
            dgvBOXID.ReadOnly = true;
            dgvBOXID.Width = 120;
            dgvData.Columns.Add(dgvBOXID);


            DataGridViewTextBoxColumn dgvMENGE = new DataGridViewTextBoxColumn();
            dgvMENGE.DataPropertyName = "MENGE";
            dgvMENGE.HeaderText = "Qty";
            dgvMENGE.ReadOnly = true;
            dgvMENGE.Width = 100;
            dgvData.Columns.Add(dgvMENGE);

            DataGridViewTextBoxColumn dgvDACOD = new DataGridViewTextBoxColumn();
            dgvDACOD.DataPropertyName = "DACOD";
            dgvDACOD.HeaderText = "Date Code";
            dgvDACOD.ReadOnly = true;
            dgvDACOD.Width = 100;
            dgvData.Columns.Add(dgvDACOD);

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Item"] = i + 1;
            }

            dgvData.DataSource = dtData;
        }
    }
}
