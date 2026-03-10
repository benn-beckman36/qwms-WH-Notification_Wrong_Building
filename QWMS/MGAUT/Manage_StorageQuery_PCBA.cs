using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;

namespace QWMS
{
    public partial class Manage_StorageQuery_PCBA : Form
    {
        
        #region 变量
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

        public string strMandt = "";
        public string strUsrnm = "";
        public string strComcd = "";
        string strProgid = "";
        string strWerks="";
        string strLgort="";
        string strSnStatus="";
        string strRefID="";
        string strBXID="";
        string strSn="";
        string strMatnr="";
        string strDateFrom = "";
        string strDateTo = "";
        string strUmlgo = "";
        string strHourF = "";
        string strHourT = "";
        UserInfo UserData = new UserInfo();
        DataTable dtData = new DataTable();
        QCI.QWMS.StorageData objStorageData;


        #endregion

        public Manage_StorageQuery_PCBA(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;
            objStorageData = new QCI.QWMS.StorageData(UserData, strWerks, strLgort);
            ShowDdlWerks();
            ShowDdlLgort();
            ShowDdlSnStatus();
        }

     



        #region DataMember

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;
        }

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

        private void ShowDdlLgort()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                { 
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                cmbLgort.Items.Clear();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void ShowDdlSnStatus()
        {
            //DataTable dtTemp = new DataTable();
            //try
            //{
            //    dtTemp = objStorageData.QuerySnStatus();
            //    cmbSnStaus.Items.Clear();
            //    cmbSnStaus.DisplayMember = dtTemp.Columns["F_TEXT"].ToString();
            //    cmbSnStaus.ValueMember = dtTemp.Columns["F_VALUES"].ToString();
            //  //  cmbSnStaus.Items.Add("");
            //    for (int i = 0; i < dtTemp.Rows.Count; i++)
            //    {
            //        cmbSnStaus.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());

            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message + "<-ShowDdlSnStatus()");
            //}
        }

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            this.dgvData.AllowUserToAddRows = false;
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "Plant";
                dgvcWerks.Width = 60;
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Storage";
                dgvcLgort.Width = 60;
                dgvcLgort.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcUmlgo = new DataGridViewTextBoxColumn();
                dgvcUmlgo.DataPropertyName = "UMLGO";
                dgvcUmlgo.HeaderText = "Storage To";
                dgvcUmlgo.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcUmlgo);


                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width=60;
                this.dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                dgvcMblnr.Width = 150;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);
                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();

                DataGridViewTextBoxColumn boxidStyle = new DataGridViewTextBoxColumn();
                boxidStyle.DataPropertyName = "BOXID";
                boxidStyle.HeaderText = "Box ID";
                boxidStyle.Width = 135;
                boxidStyle.ReadOnly = true;
                this.dgvData.Columns.Add(boxidStyle);

                DataGridViewTextBoxColumn SernoStyle = new DataGridViewTextBoxColumn();
                SernoStyle.DataPropertyName = "SERNO";
                SernoStyle.HeaderText = "SN";
                SernoStyle.ReadOnly = true;
                SernoStyle.Width = 145;
                this.dgvData.Columns.Add(SernoStyle); 

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Cost Center";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width=110;
                this.dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.Width = 100;
                dgvcMatnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.Width = 50;
                dgvcInsmk.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.Width = 60;
                dgvcCharg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.Width = 50;
                dgvcMenge.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcSNStatus = new DataGridViewTextBoxColumn();
                dgvcSNStatus.DataPropertyName = "SNStatus";
                dgvcSNStatus.HeaderText = "SN Status";
                dgvcSNStatus.ReadOnly = true;
                dgvcSNStatus.Width = 100;
                this.dgvData.Columns.Add(dgvcSNStatus);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "StatusTim";
                dgvcIndat.HeaderText = "Status Time";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 110;
                this.dgvData.Columns.Add(dgvcIndat);

                dgvData.DataSource = dtData;

                lblCount.Text = dtData.Rows.Count.ToString() + " records"; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        } 
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            strSn = txtSn.Text.Trim().ToString();
            strRefID = txtRefID.Text.Trim().ToString();
            strMatnr = txtMatnr.Text.Trim().ToString();
            strBXID = txtBxID.Text.Trim().ToString();
            strWerks = cmbWerks.Text.ToString();
            if (cmbSnStaus.SelectedIndex != -1)
            {
               strSnStatus = cmbSnStaus.Text.ToString();
            }
            else
            {
                strSnStatus = "";
            }
            strLgort = cmbLgort.Text.ToString();
            strUmlgo = txtUmlgo.Text.ToString().Trim();
            if (cmbHourF.SelectedIndex != -1)
            {
                strHourF = cmbHourF.Text.ToString();
                strDateFrom = DateFrom.Value.ToString("yyyy-MM-dd") + " " + strHourF + ":00";
            }
            else
            {
                strDateFrom = DateFrom.Value.ToString("yyyy-MM-dd") + " 00:00";
            }
            if (cmbHourT.SelectedIndex != -1)
            {
                strHourT = cmbHourT.Text.ToString();
                strDateTo = DateTo.Value.ToString("yyyy-MM-dd") + " " + strHourT + ":59";
            }
            else
            {
                strDateTo = DateTo.Value.ToString("yyyy-MM-dd") + " 23:59";
            }
           
            
            objStorageData = new QCI.QWMS.StorageData(UserData, strWerks, strLgort);
            dtData = objStorageData.QueryStorageData_PCBA(strWerks, strLgort, strRefID, strBXID, strSn, strMatnr, strSnStatus,strUmlgo,strDateFrom,strDateTo);
            lblCount.Text = dtData.Rows.Count + "  Recoders";
            ShowDataGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.txtBxID.Text = "";
            this.txtMatnr.Text = "";
            this.txtRefID.Text = "";
            this.txtSn.Text = "";
            this.txtMatnr.Text = "";
            this.dgvData.DataSource = null;
            this.cmbSnStaus.SelectedIndex = -1;
            strBXID = "";
            strMatnr = "";
            strWerks = "";
            strLgort = "";
            strRefID = "";
            strSn = "";
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDdlLgort();
        }

     
    }
}
