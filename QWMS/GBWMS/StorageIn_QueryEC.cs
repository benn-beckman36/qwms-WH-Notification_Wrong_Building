using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;
using System.Text.RegularExpressions;

namespace QWMS
{
    public partial class StorageIn_QueryEC : Form
    {
        //public StorageIn_QueryEC()
        //{
        //    InitializeComponent();
        //}

        
        #region DataMember

        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;

        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtECSource = new DataTable();
        private DataTable dtStorageLocation = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtBox = new DataTable();

        #endregion

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

        public ArrayList Mblnrs
        {
            get
            {
                return alMblnrs;
            }
            set
            {
                alMblnrs = value;
            }
        }

        #endregion

        #region Constructor

        public StorageIn_QueryEC(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            lblCompany.Text = Comcd;
            lblUserid.Text = Usrnm;

            try
            {
                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageIn.CheckAuthority())
                //if (false)
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                   // ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    //ShowPrintCheckBox();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }

                   // ResetPage();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion


        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            Authority objAuthority = new Authority(UserData);
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
                cmbWerks.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
               // stsWarning.Text = string.Empty;
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
                    strLgort = string.Empty;
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
        #endregion

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
            dtECSource = StorageIn.GetPacingInfo(cmbWerks.Text.Trim(), cmbLgort.Text.Trim(), cmbScan.Text.Trim(), ddBegin.Value.ToString(), ddEnd.Value.ToString(), txtEC.Text.Trim(), txtCustid.Text.Trim());
            ShowStorageDataGrid();
        }

        #region ShowStorageDataGrid
        private void ShowStorageDataGrid()
        {
            gvEC.AutoGenerateColumns = false;
            gvEC.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 130;
                dgvcWERKS.ReadOnly = true;
                gvEC.Columns.Add(dgvcWERKS);

                //BOXID
                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 100;
                dgvcLGORT.ReadOnly = true;
                gvEC.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcPNUM = new DataGridViewTextBoxColumn();
                dgvcPNUM.DataPropertyName = "PNUM";
                dgvcPNUM.HeaderText = "EC";
                dgvcPNUM.Width = 90;
                dgvcPNUM.ReadOnly = true;
                gvEC.Columns.Add(dgvcPNUM);

                //MATNR
                DataGridViewTextBoxColumn dgvcCLRTYP = new DataGridViewTextBoxColumn();
                dgvcCLRTYP.DataPropertyName = "CLRTYP";
                dgvcCLRTYP.HeaderText = "清关类型";
                dgvcCLRTYP.Width = 90;
                dgvcCLRTYP.ReadOnly = true;
                gvEC.Columns.Add(dgvcCLRTYP);

                DataGridViewTextBoxColumn dgvcSTATUS = new DataGridViewTextBoxColumn();
                dgvcSTATUS.DataPropertyName = "STATUS";
                dgvcSTATUS.HeaderText = "状态";
                dgvcSTATUS.Width = 90;
                dgvcSTATUS.ReadOnly = true;
                gvEC.Columns.Add(dgvcSTATUS);


                DataGridViewTextBoxColumn dgvcSCANBOX = new DataGridViewTextBoxColumn();
                dgvcSCANBOX.DataPropertyName = "SCANBOX";
                dgvcSCANBOX.HeaderText = "Barcode";
                dgvcSCANBOX.Width = 130;
                dgvcSCANBOX.ReadOnly = true;
                gvEC.Columns.Add(dgvcSCANBOX);


                DataGridViewTextBoxColumn dgvcCUSTID = new DataGridViewTextBoxColumn();
                dgvcCUSTID.DataPropertyName = "CUSTID";
                dgvcCUSTID.HeaderText = "报关No.";
                dgvcCUSTID.Width = 90;
                dgvcCUSTID.ReadOnly = true;
                gvEC.Columns.Add(dgvcCUSTID);

                DataGridViewTextBoxColumn dgvcSGTXT = new DataGridViewTextBoxColumn();
                dgvcSGTXT.DataPropertyName = "SGTXT";
                dgvcSGTXT.HeaderText = "扣帐人员";
                dgvcSGTXT.Width = 90;
                dgvcSGTXT.ReadOnly = true;
                gvEC.Columns.Add(dgvcSGTXT);

                DataGridViewTextBoxColumn dgvcBELNR = new DataGridViewTextBoxColumn();
                dgvcBELNR.DataPropertyName = "BELNR";
                dgvcBELNR.HeaderText = "扣帐单号";
                dgvcBELNR.Width = 130;
                dgvcBELNR.ReadOnly = true;
                gvEC.Columns.Add(dgvcBELNR);

                DataGridViewTextBoxColumn dgvcUPTIME = new DataGridViewTextBoxColumn();
                dgvcUPTIME.DataPropertyName = "UPTIME";
                dgvcUPTIME.HeaderText = "下载时间";
                dgvcUPTIME.Width = 90;
                dgvcUPTIME.ReadOnly = true;
                gvEC.Columns.Add(dgvcUPTIME);

                gvEC.DataSource = dtECSource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion
    }
}
