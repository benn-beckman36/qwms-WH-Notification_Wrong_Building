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
    public partial class Pad_MaterialPrepare : System.Windows.Forms.Form
    {
        DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strComcd = "";
        private string strWerks = "";
        private string strLgort = "";

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

        public Pad_MaterialPrepare()
        {
            InitializeComponent();
        }

        public Pad_MaterialPrepare(UserInfo varUserData, string strProgid)
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
                    ShowDdlLgort();
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

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDdlLgort();
        }

        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
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

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 90;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "Item No.";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "StorageOut Quantity";
                dgvcMENGE.Width = 90;
                dgvcMENGE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcSTQTY = new DataGridViewTextBoxColumn();
                dgvcSTQTY.DataPropertyName = "STQTY";
                dgvcSTQTY.HeaderText = "Pre-Quantity";
                dgvcSTQTY.Width = 90;
                dgvcSTQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcSTQTY);

                DataGridViewTextBoxColumn dgvcBLQTY = new DataGridViewTextBoxColumn();
                dgvcBLQTY.DataPropertyName = "BLQTY";
                dgvcBLQTY.HeaderText = "Balance Quantity";
                dgvcBLQTY.Width = 90;
                dgvcBLQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcBLQTY);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "Location";
                dgvcLOCAT.Width = 90;
                dgvcLOCAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLOCAT);                

                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
                                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }


        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strWerks = cmbWerks.Text.Trim();
            string strLgort = cmbLgort.Text.Trim();
            string strDocuNo = txtDocuNo.Text.Trim();
            string strCarno = txtCarno.Text.Trim();
            Authority objMaterial = new Authority(UserData);
            dtData = objMaterial.QueryMaterialPrepare(strWerks, strLgort, strCarno, strDocuNo);
            if (dtData.Rows.Count <= 0)
            {
                stsWarning.Text = "No Material Prepare Data!!";
            }
            else
            {
                ShowDataGrid();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cmbWerks.Text = "";
            cmbLgort.Text = "";
            txtDocuNo.Text = "";
            txtCarno.Text = "";
            stsWarning.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pad_MaterialPrepare_Load(object sender, EventArgs e)
        {
            ShowDdlWerks();
            ShowDdlLgort();
            ShowStatusData();
        }
    }
}
