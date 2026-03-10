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
using System.IO;

namespace QWMS
{
    public partial class TransferInWHReceive : Form
    {
        UserInfo UserData = new UserInfo();
        private DataTable dtData = new DataTable();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
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
        public TransferInWHReceive(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.Admin objAdmin = new Admin(UserData, Progid);
                QCI.QWMS.StorageIn StorageIn = new StorageIn(UserData, strProgid);//Replenishment
                QCI.QWMS.Replenishment StorageIn1 = new Replenishment(UserData, strProgid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageIn1.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化

                    ShowDdlWerks();
                    ShowDdlLgort();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                       // this.cmbLgort.SelectedIndex = 0;
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

        //仓别
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                //stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                //当前Plant
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                    dtTemp = objAuthority.CheckLgortAuthority();
                //现有值
                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    cmbLgort.Items.Clear();

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
                        //去重复值
                        if (!cmbLgort.Items.Contains(dtTemp.Rows[i]["F_TEXT"].ToString()))
                        {
                            cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                            if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                            {
                                cmbLgort.SelectedIndex = i;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        public void Query()
        {
            strWerks = cmbWerks.Text.ToString().Trim();
            strLgort = cmbLgort.Text.ToString().Trim();
            string strGRNO = txtGRNo.Text.ToString().Trim();
            lblGrno.Text = strGRNO;

            if (!string.IsNullOrEmpty(strGRNO))
            {
                QCI.QWMS.Transfer objTransfer = new Transfer(UserData, strWerks, strLgort);
                dtData = objTransfer.GetTransferData("","", strGRNO,"I");
                if (dtData.Rows.Count > 0)
                {
                    lblPlantOut.Text = dtData.Rows[0]["FWERKS"].ToString().Trim();
                    lblPlantIn.Text = dtData.Rows[0]["DWERKS"].ToString().Trim();
                    lblSlocOut.Text = dtData.Rows[0]["FLGORT"].ToString().Trim();
                    lblSlocIn.Text = dtData.Rows[0]["DLGORT"].ToString().Trim();
                    lblYear.Text = dtData.Rows[0]["Year"].ToString().Trim();
                    lblTransferRoute.Text = dtData.Rows[0]["Transferroute"].ToString().Trim();
                    lblPostingDate.Text = dtData.Rows[0]["PostingDate"].ToString().Trim();
                    lblPostingUser.Text = dtData.Rows[0]["PostingUser"].ToString().Trim();
                    lblPrintDate.Text = dtData.Rows[0]["PrintDate"].ToString().Trim();
                    lblPrintUser.Text = Usrnm;
                    lblGRNOOut.Text = dtData.Rows[0]["MBLNR"].ToString().Trim();
                    lblPONO.Text = dtData.Rows[0]["PO"].ToString().Trim();
                    lblPallet.Text = dtData.Rows[0]["PALLETS"].ToString().Trim();
                    lblCarton.Text = dtData.Rows[0]["CARTONS"].ToString().Trim();
                    lblMvt.Text = dtData.Rows[0]["BWART"].ToString().Trim();
                    lblPlantOut.Visible = true;
                    lblPlantIn.Visible = true;
                    lblSlocOut.Visible = true;
                    lblSlocIn.Visible = true;
                    lblYear.Visible = true;
                    lblTransferRoute.Visible = true;
                    lblPostingDate.Visible = true;
                    lblPostingUser.Visible = true;
                    lblPrintDate.Visible = true;
                    lblPrintUser.Visible = true;
                    lblGRNOOut.Visible = true;
                    lblPONO.Visible = true;
                    lblPallet.Visible = true;
                    lblCarton.Visible = true;
                    lblMvt.Visible = true;
                    
                    ShowDataGrid();

                }
                else
                {
                    MessageBox.Show("调拨单号不能为空！");
                }
            }
        }

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvItem = new DataGridViewTextBoxColumn();
                dgvItem.DataPropertyName = "Item";
                dgvItem.HeaderText = "Item";
                dgvItem.Width = 40;
                dgvItem.ReadOnly = true;
                this.dgvData.Columns.Add(dgvItem);

                DataGridViewTextBoxColumn dgvMaterial = new DataGridViewTextBoxColumn();
                dgvMaterial.DataPropertyName = "MATNR";
                dgvMaterial.HeaderText = "Material";
                dgvMaterial.Width = 100;
                dgvMaterial.ReadOnly = true;
                this.dgvData.Columns.Add(dgvMaterial);

                //DataGridViewTextBoxColumn dgvcDescription = new DataGridViewTextBoxColumn();
                //dgvcDescription.DataPropertyName = "";
                //dgvcDescription.HeaderText = "Description";
                //dgvcDescription.Width = 50;
                //dgvcDescription.ReadOnly = true;
                //this.dgvData.Columns.Add(dgvcDescription);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "Batch";
                dgvcCHARG.Width = 60;
                dgvcCHARG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "Qty";
                dgvcMENGE.Width = 90;
                dgvcMENGE.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMENGE);


                dgvData.DataSource = dtData;
                //lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string strStatus = dtData.Rows[0]["STATUS"].ToString().Trim();
            if (strStatus == "WR")
            {
                QCI.QWMS.Transfer objTransfer = new Transfer(UserData, strWerks, strLgort);
                DataTable dtDataToSAP = objTransfer.GetTransferDataby351(lblGrno.Text);
                dtDataToSAP.Rows[0]["DLGORT"] = strLgort;

                //DataRow dr = dtDataToSAP.NewRow();
                //dr["DLGORT"] = strLgort;
                //dtDataToSAP.Rows.Add(dr);
                DataSet dsDataToSAP = new DataSet();
                dsDataToSAP.Tables.Add(dtDataToSAP.Copy());
                //MM.MM_Service obj = new QWMS.MM.MM_Service();

                //DataSet dsDataFromSAP = obj.ZRFC_46PO_RECORD("2", dsDataToSAP);
                ///////////////////////////////////////////
                //for test
                DataSet dsDataFromSAP = new DataSet();
                DataTable dt = new DataTable();
                //dt.Columns.Add(new DataColumn("MBLNR",System.Type.GetType("System.String")));
                dt.Columns.Add("MBLNR");
                //dt.Columns.Add(new DataColumn("MJAHR", System.Type.GetType("System.String")));
                dt.Columns.Add("MJAHR");
                //dt.Columns.Add(new DataColumn("MESSAGE", System.Type.GetType("System.String")));
                dt.Columns.Add("MESSAGE");

                for (int i = 0; i < dtDataToSAP.Rows.Count;i++ )
                {
                    DataRow dr = dt.NewRow();
                    dr["MBLNR"] = DateTime.Now.ToString("yyyymmddss");
                    dr["MJAHR"] = "2017";
                    dr["MESSAGE"] = "fortest";
                    dt.Rows.Add(dr);
                }

                dsDataFromSAP.Tables.Add(dt);

                //////////////////////

                DataTable dtDataFromSAP = dsDataFromSAP.Tables[0];

                if (dtDataFromSAP.Rows.Count > 0)
                {
                    string strMBLNR101 = dtDataFromSAP.Rows[0]["MBLNR"].ToString().Trim();
                    string strYEAR101 = dtDataFromSAP.Rows[0]["MJAHR"].ToString().Trim();
                    string strMESSAGE101 = dtDataFromSAP.Rows[0]["MESSAGE"].ToString().Trim();
                    //string strFlag=dtDataFromSAP.Rows[0]["FLAG"].ToString().Trim();
                    if (!string.IsNullOrEmpty(strMBLNR101))
                    {
                        if (objTransfer.UpdateTransfer101MBLNR(lblGrno.Text, strMBLNR101, strYEAR101, strMBLNR101, strLgort))
                        {
                             strStatus = dtData.Rows[0]["STATUS"].ToString().Trim();
                            objTransfer.UpdateStatus(lblGRNOOut.Text, Usrnm, strStatus);
                            MessageBox.Show("Confirm成功");
                            lblGRNOIn.Text = strMBLNR101;
                            lblGRNOIn.ForeColor = Color.Red;
                            lblGRNOIn.Visible = true;
                            lblSlocIn.Text = strLgort;
                            lblSlocIn.ForeColor = Color.Red;
                            txtGRNo.Text = "";
                            txtGRNo.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("确认失败，失败原因" + strMBLNR101 + "");
                    }

                }
                else
                {
                    MessageBox.Show("SAP没有回执");
                }
            }
            else
            {
                MessageBox.Show("当前状态不允许发送SAP扣帐！");
            }

        }

        private void txtGRNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==13)
            {
                Query();
            }
        }

       
         
      
    }
}
