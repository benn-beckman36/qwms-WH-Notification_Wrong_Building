using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections;
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
    public partial class Transfer_MblnrSelect : Form
    {
        #region  变量

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strType = "";
        private string flag = "";

        private ArrayList aryReturn = new ArrayList();
        private DataTable dtData = new DataTable();

        private Transfer objTransfer;

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
        public ArrayList aReturn
        {
            get
            {
                return aryReturn;
            }
            set
            {
                aryReturn = value;
            }
        }


        #endregion
        public Transfer_MblnrSelect(UserInfo varUserData, string strWerks, string strLogrt, string strBwart,string strflag)
        {

            InitializeComponent();
            UserData = varUserData;
            Werks = strWerks;
            Lgort = strLogrt;
            Type = strBwart;
            flag = strflag;
            

            objTransfer = new Transfer(UserData, Werks, Lgort);
        }
               

        public Transfer_MblnrSelect(DataTable dtTemp ,string strflag)
        {
            InitializeComponent();
            this.btnQuery.Visible = false;
            this.dtpTime.Visible = false;
            flag = strflag;
            if (flag == "3")
            {
                dtData = dtTemp.Copy();
            }
            else if(flag == "2")
            {
                dtData.Columns.Add("F_TEXT");
                string[] aryTemp = dtTemp.Rows[0]["F_TEXT"].ToString().Trim().Split(new char[] { ';' });
                for (int i = 0; i < aryTemp.Length; i++)
                {
                    DataRow dr = dtData.NewRow();
                    dr = dtData.NewRow();
                    dr["F_TEXT"] = aryTemp[i].ToString();
                    dtData.Rows.Add(dr);
                }
            }
            else
            {
                dtData.Columns.Add("F_TEXT");
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    DataRow dr = dtData.NewRow();
                    dr["F_TEXT"] = dtTemp.Rows[i]["F_TEXT"].ToString();
                    dtData.Rows.Add(dr);
                }
            }
            ShowDataGrid();
        }
        private void ShowDataGrid()
		{			
			try
			{

                dgvData.AutoGenerateColumns = false;
                dgvData.Columns.Clear();
                dgvData.AllowUserToAddRows = false;

                ////选择单选框√
                QWMS.Manage_SapSimulationDataPrint.DatagridViewCheckBoxHeaderCell chkcell = new QWMS.Manage_SapSimulationDataPrint.DatagridViewCheckBoxHeaderCell();
                chkcell.OnCheckBoxClicked += new QWMS.Manage_SapSimulationDataPrint.CheckBoxClickedHandler(chkcell_OnCheckBoxClicked);
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderCell = chkcell;
                chk.DataPropertyName = "Select";
                chk.HeaderText = "";
                //   chk.Name = "chk";
                chk.Width = 30;
                this.dgvData.Columns.Add(chk);
                this.dgvData.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                if (flag == "0"||flag=="3")
                {
                    DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                    dgvcMBLNR.DataPropertyName = "MBLNR";
                    dgvcMBLNR.HeaderText = "MBLNR";
                    dgvcMBLNR.Width = 130;
                    dgvcMBLNR.ReadOnly = true;
                    dgvData.Columns.Add(dgvcMBLNR);
                }
                if (flag == "1")
                {
                    DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                    dgvcLGORT.DataPropertyName = "F_TEXT";
                    dgvcLGORT.HeaderText = "Storage";
                    dgvcLGORT.Width = 50;
                    dgvcLGORT.ReadOnly = true;
                    dgvData.Columns.Add(dgvcLGORT);
                }
                if (flag == "2")
                {
                    DataGridViewTextBoxColumn dgvcMVT = new DataGridViewTextBoxColumn();
                    dgvcMVT.DataPropertyName = "F_TEXT";
                    dgvcMVT.HeaderText = "MVT";
                    dgvcMVT.Width = 50;
                    dgvcMVT.ReadOnly = true;
                    dgvData.Columns.Add(dgvcMVT);
                }

                dgvData.DataSource = dtData;
                lblSource.Text = dtData.Rows.Count.ToString() + " records";

			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message + "<-ShowDataGrid()");
			}
		}

        private void btnQuery_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = null;
            string dtTime = dtpTime.Value.ToString("yyyy-MM-dd");
            dtData = objTransfer.GetMblnrData(Werks, Lgort, Type, dtTime);
            if(dtData.Rows.Count>0)
            {
                ShowDataGrid();
            }
            else
            {
                MessageBox.Show("无数据，请确认!!");
                return;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            
            if (dgvData.Rows.Count == 0)
            {
                MessageBox.Show("无数据，请确认!!");
                return;
            }
            else
            {
                GetCheckData();
                if (aryReturn.Count == 0)
                {
                    MessageBox.Show("未选中任何信息，请确认!!");
                    return;
                }
                this.Close();
            }

            
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 点击单选框全选反选事件
        private void chkcell_OnCheckBoxClicked(bool isChecked)
        {
            if (isChecked == true)
            {
                dgvData.EndEdit();
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = true;
                }
            }
            else
            {
                dgvData.EndEdit();
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = false;
                }
            }
        }
        #endregion

        private void GetCheckData()
        {
            aryReturn.Clear();
            for (int i = 0; i < dgvData.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvData.Rows[i].Cells[0];

                //循环dgv的每一行，判断是否有记录被选中
                if ((bool)dgvData.Rows[i].Cells[0].EditedFormattedValue)
                {
                    //获取当前已选中所有行的判票号(即BOXID)
                    aryReturn.Add(dgvData.Rows[i].Cells[1].Value.ToString().Trim());
                }

            }
        }

        
    }
}
