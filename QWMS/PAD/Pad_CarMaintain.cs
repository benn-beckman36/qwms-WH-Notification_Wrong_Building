using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class Pad_CarMaintain : System.Windows.Forms.Form
    {
        DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strComcd = "";

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

        public Pad_CarMaintain()
        {
            InitializeComponent();
        }

        public Pad_CarMaintain(UserInfo varUserData, string strProgid)
		{
			InitializeComponent();
            UserData = varUserData;
			Mandt = UserData.Client;
			Usrnm = UserData.UserId;
			Progid = strProgid;
            Comcd = UserData.CompanyCode;

			try
			{
                 Admin  objStorageIn =new Admin(UserData, Progid);
				//檢查權限
				if(!objStorageIn.CheckAuthority())
				{
					throw new Exception("You don't have right to use this program!!");
				}
				else
				{				
					ShowDdlWerks();                    
                }
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }

        private void ShowDdlWerks()
        {
            QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
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

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 90;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcCARNO = new DataGridViewTextBoxColumn();
                dgvcCARNO.DataPropertyName = "CARNO";
                dgvcCARNO.HeaderText = "Car NO.";
                dgvcCARNO.Width = 90;
                dgvcCARNO.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCARNO);                              

                DataGridViewTextBoxColumn dgvcFLAGE = new DataGridViewTextBoxColumn();
                dgvcFLAGE.DataPropertyName = "FLAGE";
                dgvcFLAGE.HeaderText = "Flag";
                dgvcFLAGE.Width = 60;
                dgvcFLAGE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcFLAGE);

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strWerks = cmbWerks.Text.Trim();
            string strCarno = txtCarNo.Text.Trim();
            if (strWerks == "" || strCarno == "")
            {
                stsWarning.Text = "请选择厂区、输入车牌号!!";
                return;
            }
            else
            {
                Authority objCarMaintain = new Authority(UserData);
                dtData = objCarMaintain.QueryCarMaintain(strWerks, strCarno);
                if (dtData.Rows.Count > 0)
                {
                    MessageBox.Show("已有此车辆信息,请确认!!");
                    return;
                }
                bool flag = objCarMaintain.CarMaintain(strWerks, strCarno);
                if (flag)
                {
                    stsWarning.Text = "Maintain Success!!";
                }
                else 
                {
                    stsWarning.Text = "Maintain Failed!!";
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cmbWerks.Text = "";
            txtCarNo.Text = "";
            stsWarning.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strWerks = cmbWerks.Text.Trim();
            string strCarno = txtCarNo.Text.Trim();            
            Authority objCarMaitain = new Authority(UserData);
            dtData = objCarMaitain.QueryCarMaintain(strWerks,strCarno);
            if (dtData.Rows.Count <= 0)
            {
                stsWarning.Text = "No Car Data!!";
            }
            else
            {
                ShowDataGrid();
            }
        }

        private void Pad_CarMaintain_Load(object sender, EventArgs e)
        {
            ShowStatusData();
        }

    }
}
