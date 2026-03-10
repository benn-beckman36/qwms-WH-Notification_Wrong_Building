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
    public partial class TransferInGuardConfirm : Form
    {
        #region 变量

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private DataTable dtData = new DataTable();
        private DataTable dtDriverInfo = new DataTable();
        private DataTable dtRelation = new DataTable();

        private string strstatus = "JL";
        int m = 0;
        int n= 0;
        int k = 0;
        //private StreamWriter sw = null;

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

        #endregion
        public TransferInGuardConfirm(UserInfo varUserData, string strProgid)
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
                QCI.QWMS.StorageIn StorageIn = new StorageIn(UserData, strProgid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化

                    ShowDdlWerks();
                    

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
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
        #endregion
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }
        public void Query()
        {
            strWerks = cmbWerks.Text.ToString().Trim();
            string strCPNO = txtCPNo.Text.ToString().Trim();
            string strDriver = txtDriverID.Text.ToString().Trim();
            string strPCID = txtPCID.Text.ToString().Trim();
            string strGRNO = txtGRNo.Text.ToString().Trim();

            if (strCPNO != lblCPNO.Text)
            {
                m = 0;
            }
            #region 查询司机信息
            if (m == 0)
            {
                if (!string.IsNullOrEmpty(strCPNO))
                {
                    if (!string.IsNullOrEmpty(strDriver))
                    {
                        QCI.QWMS.Transfer objTransfer = new Transfer(UserData, strWerks, strLgort);
                        dtDriverInfo = objTransfer.GetTransferDriverInfo(strCPNO, strDriver);
                        if (dtDriverInfo.Rows.Count > 0)
                        {
                            lblName.Text = dtDriverInfo.Rows[0]["DriverName"].ToString().Trim();
                            lblLicenseNo.Text = dtDriverInfo.Rows[0]["CARID"].ToString().Trim();
                            string strImgUrl = dtDriverInfo.Rows[0]["IMGURL"].ToString().Trim();
                            pictureBox1.Image = Image.FromFile(strImgUrl);
                            //Image img = Image.FromFile(strImgUrl);
                            //Bitmap bmp = new Bitmap(img, 150, 300);
                            //pictureBox1.Image = bmp;

                            m++;
                            lblCPNO.Text = strCPNO;

                        }
                    }
                    

                }
                
            }
            #endregion

            if (strPCID != lblPCID.Text)
            {
                n = 0;
            }
            #region 查询派车单信息
            if (n == 0)
            {
                if (!string.IsNullOrEmpty(strPCID))
                {
                    QCI.QWMS.Transfer objTransfer = new Transfer(UserData, strWerks, strLgort);
                    dtRelation = objTransfer.GetTransferRelation(lblLicenseNo.Text, strPCID, strstatus);
                    k = dtRelation.Rows.Count;
                    if (k < 1)
                    {
                        MessageBox.Show("该派车单和车牌号不一致，请确认！");
                        return;
                    }
                    else
                    {
                        lblPCID.Text = strPCID;
                        n++;
                        lblhint.Text = "车牌号" + lblCPNO.Text + "下的派车单" + lblPCID.Text + "下共有" + k + "条调拨单";
                    }
                }
            }
            #endregion

            #region 查询调拨单信息
            if (!string.IsNullOrEmpty(strGRNO))
            {
                QCI.QWMS.Transfer objTransfer = new Transfer(UserData, strWerks, strLgort);
                dtData = objTransfer.GetTransferData(lblLicenseNo.Text,lblPCID.Text,strGRNO,"I");
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
                    //k--;
                    DataTable dtTemp = objTransfer.GetTransferRelation(lblLicenseNo.Text, lblPCID.Text, strstatus);
                    int t = dtTemp.Rows.Count - 1;
                    lblhint.Text = "车牌号" + lblCPNO.Text + "下的派车单" + lblPCID.Text + "下还有" + t + "条调拨单";
                }
                else
                {
                    MessageBox.Show("该调拨单和派车单、车牌号不一致，请确认！");
                }
            }
            #endregion
            
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            QCI.QWMS.Transfer objTransfer = new Transfer(UserData, strWerks, strLgort);
            DataTable dtStatus = objTransfer.GetTransferStatus(lblGRNOOut.Text);
            string strStatus = dtStatus.Rows[0]["STATUS"].ToString();
            
            if (strStatus == "JL")
            {
                MessageBox.Show("确认信息无误，准许入厂");
                objTransfer.UpdateStatus(lblGRNOOut.Text, Usrnm, strStatus);
                MessageBox.Show("入厂成功");
            }
            else
            {
                MessageBox.Show("目前状态是" + dtStatus.Rows[0]["ST"].ToString() + "，不可经过警卫，请确认！");
            }

           
        }

        private void txtGRNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Query();
            }
        }

        private void txtCPNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Query();
                txtDriverID.Focus();
            }
        }

        private void txtPCID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Query();
                txtGRNo.Focus();
            }
        }

       

        private void txtDriverID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Query();
                txtPCID.Focus();
            }
        }

       
    }
}
