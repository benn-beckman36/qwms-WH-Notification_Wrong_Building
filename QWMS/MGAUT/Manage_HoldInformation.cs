using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using System.Diagnostics;
using QWMS.Common;
using QWMS.Entity;
using Qci.Base.Common;


namespace QWMS
{
    public partial class Manage_HoldInformation : System.Windows.Forms.Form
    {

                #region Constructor

        public Manage_HoldInformation(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;

            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                //檢查權限
            
                ShowDdlWerks();
                ShowDdlLgort();
                
                if (cmbWerks.Items.Count > 0)
                {
                    this.cmbWerks.SelectedIndex = 0;
                }
                if (cmbLgort.Items.Count > 0)
                {
                    this.cmbLgort.SelectedIndex = 0;
                }
               
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region DataMember

        #region 變數宣告
        public string strMandt = "";
        public string strUsrnm = "";
        public string strWerks = "";
        public string strLgort = "";
        public string strProgid = "";
        public string strMatnr = "";
        public string strMblnr = "";
        public string strType = "";
        public string strInsmk = "";
        public string strSttyp = "";
        public string strLotyp = "";
        public string strCtbto = "";
        public string strRegon = "";
        public string strMachine = "";
        public string strComcd = "";
        public int intFormIndex = 0;
        public bool bolDuplicate = false;
        public DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        private DataTable dtTmpData = new DataTable();
        private DataTable dtCombineData = new DataTable();
        private DataTable dtCombineInventory = new DataTable();
        private DataTable dtLocat = new DataTable();
        private DataTable dtInventory = new DataTable();
 
         
        #endregion

        #region 變數
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

        public string Locat
        {
            get
            {
                return this.txtLocat.Text.Trim();
            }
            set
            {
                this.txtLocat.Text = value;
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

        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
            }
        }

        public string Ctbto
        {
            get
            {
                return strCtbto;
            }
            set
            {
                strCtbto = value;
            }
        }

        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
            }
        }

        public string Machine
        {
            get
            {
                return strMachine;
            }
            set
            {
                strMachine = value;
            }
        }

        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
            }
        }

        public bool Duplicate
        {
            get
            {
                return bolDuplicate;
            }
            set
            {
                bolDuplicate = value;
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
        #endregion

        #endregion

        public Manage_HoldInformation()
        {
            InitializeComponent();
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
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    //dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //dtTemp = objPlantData.GetDdlLgortData();
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

            this.dgSN.AutoGenerateColumns = false;
            this.dgSN.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 90;
                dgvcWERKS.ReadOnly = true;
                this.dgSN.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 90;
                dgvcLGORT.ReadOnly = true;
                this.dgSN.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "Location";
                dgvcLOCAT.Width = 90;
                dgvcLOCAT.ReadOnly = true;
                this.dgSN.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcManr = new DataGridViewTextBoxColumn();
                dgvcManr.DataPropertyName = "MATNR";
                dgvcManr.HeaderText = "Part No";
                dgvcManr.Width = 120;
                dgvcManr.ReadOnly = false;
                this.dgSN.Columns.Add(dgvcManr);


                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;

                this.dgSN.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "BOXID";
                dgvcBOXID.HeaderText = "Box ID";
                dgvcBOXID.Width = 120;
                dgvcBOXID.ReadOnly = false;
                this.dgSN.Columns.Add(dgvcBOXID);



                DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                dgvcSERNO.DataPropertyName = "SERNO";
                dgvcSERNO.HeaderText = "Serial No";
                dgvcSERNO.Width = 120;
                dgvcSERNO.ReadOnly = false;
                this.dgSN.Columns.Add(dgvcSERNO);


                
                //DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                //dgvcINSMK.DataPropertyName = "INSMK";
                //dgvcINSMK.HeaderText = "Stock";
                //dgvcINSMK.Width = 90;
                //dgvcINSMK.ReadOnly = true;
                //this.dgSN.Columns.Add(dgvcINSMK);


               

                //DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                //dgvcCHARG.DataPropertyName = "CHARG";
                //dgvcCHARG.HeaderText = "Version";
                //dgvcCHARG.Width = 90;
                //dgvcCHARG.ReadOnly = true;
                //this.dgSN.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcREMAK = new DataGridViewTextBoxColumn();
                dgvcREMAK.DataPropertyName = "REMAK";
                dgvcREMAK.HeaderText = "Hold Info";
                dgvcREMAK.Width = 190;
                dgvcREMAK.ReadOnly = true;
                this.dgSN.Columns.Add(dgvcREMAK);

                dgSN.DataSource = dtData;




            }
            catch (Exception ex)
            {

            }
        }

        public void ShowDataGridByPN()
        {

            this.dgSN.AutoGenerateColumns = false;
            this.dgSN.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "Plant";
                dgvcWERKS.Width = 90;
                dgvcWERKS.ReadOnly = true;
                this.dgSN.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "Storage";
                dgvcLGORT.Width = 90;
                dgvcLGORT.ReadOnly = true;
                this.dgSN.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "Location";
                dgvcLOCAT.Width = 90;
                dgvcLOCAT.ReadOnly = true;
                this.dgSN.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcManr = new DataGridViewTextBoxColumn();
                dgvcManr.DataPropertyName = "MATNR";
                dgvcManr.HeaderText = "Part No";
                dgvcManr.Width = 120;
                dgvcManr.ReadOnly = false;
                this.dgSN.Columns.Add(dgvcManr);


                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;

                this.dgSN.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "BOXID";
                dgvcBOXID.HeaderText = "Box ID";
                dgvcBOXID.Width = 120;
                dgvcBOXID.ReadOnly = false;
                this.dgSN.Columns.Add(dgvcBOXID);



                //DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                //dgvcSERNO.DataPropertyName = "SERNO";
                //dgvcSERNO.HeaderText = "Serial No";
                //dgvcSERNO.Width = 120;
                //dgvcSERNO.ReadOnly = false;
                //this.dgSN.Columns.Add(dgvcSERNO);

 

                DataGridViewTextBoxColumn dgvcREMAK = new DataGridViewTextBoxColumn();
                dgvcREMAK.DataPropertyName = "REMAK";
                dgvcREMAK.HeaderText = "Hold Info";
                dgvcREMAK.Width = 190;
                dgvcREMAK.ReadOnly = true;
                this.dgSN.Columns.Add(dgvcREMAK);

                dgSN.DataSource = dtData;




            }
            catch (Exception ex)
            {

            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool checkPn = false;
            if (checkBox1.Checked)
            {
                checkPn = true;
            }
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), this.UserData, Werks, Lgort, "", Progid);
            dtData = objStorageIn.Querywhlold(cmbWerks.Text, cmbLgort.Text.Trim(), txtLocat.Text.Trim(), txtBoxid.Text.Trim(),checkPn);
            if (checkBox1.Checked)
            {
                ShowDataGridByPN();
            }
            else
            {
                ShowDataGrid();
            }
        }

    }
}
