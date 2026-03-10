using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class Transfer_ApplyForCar : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strCrdat = "";
        private string strProgid = "";
        private ArrayList aryReturn = new ArrayList();

        //private DataGridViewComboBoxColumn dgvcType = new DataGridViewComboBoxColumn();

        private DataTable dtHR = new DataTable(); //员工信息
        private DataTable dtData = new DataTable();//Mblnr明细
        private DataTable dtType = new DataTable();//材料明细
        DataTable dtItem = new DataTable();//49明细
        private FileInfo fi;
        private StreamWriter sw;
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;


        private Replenishment objReplenishment;
        private Transfer objTransfer;

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

        public Transfer_ApplyForCar(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            btnSendMail.Visible = false;

            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.sfdSaveFile.FileName = "ApplyFile.xls";
            this.sfdSaveFile.Filter = "Text Files (*.txt)|*.txt|Text Files (*.xls)|*.xls|All Files (*.*)|*.*";

            try
            {
                objReplenishment = new Replenishment(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, strWerks, strLgort, Progid);
                objTransfer = new Transfer(UserData, Werks, Lgort);
                //檢查權限
                if (!objReplenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowApplyType();
                    ShowMaterialType();
                    ShowDdlType();
                    ShowDdlRoute();
                    //GetdtData();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbSRoute.Items.Count > 0 && cmbERoute.Items.Count > 0)
                    {
                        this.cmbSRoute.SelectedIndex = 0;
                        this.cmbERoute.SelectedIndex = 0;
                    }
                    //if (cmbLgort.Items.Count > 0)
                    //{
                    //    this.cmbLgort.SelectedIndex = 0;
                    //}
                    //ShowDdlMblnr();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 設定State Bar中的日期
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
            cmbLgort.SelectedIndex = 0;
            //ShowDdlRoute();
            //cmbSRoute.SelectedIndex = 0;
            //cmbERoute.SelectedIndex = 0;
        }

        #endregion

        #region ShowDdlLgort
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

        private void cmbLgort_TextChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlMblnr();
            if (cmbMblnr.Items.Count > 0)
            {
                this.cmbMblnr.SelectedIndex = 0;
            }
            else
            {
                cmbMblnr.Text = "";
            }
        }

        //private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    stsWarning.Text = "";
        //    ShowDdlMblnr();
        //    if (cmbMblnr.Items.Count > 0)
        //    {
        //        this.cmbMblnr.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        cmbMblnr.Text = "";
        //    }
        //}
        #endregion

        #region ShowApplyType:正常 专车 急件 外包(欣进：付款方式不一样)
        private void ShowApplyType()
        {
            try
            {
                stsWarning.Text = "";
                cmbApplyType.Items.Clear();
                cmbApplyType.Items.Add("正常");
                cmbApplyType.Items.Add("专车");
                cmbApplyType.Items.Add("急件");
                cmbApplyType.Items.Add("外包");
                cmbApplyType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowApplyType()");
            }
        }
        #endregion

        #region ShowMaterialType
        private void ShowMaterialType()
        {
          try
            {
                stsWarning.Text = "";
                cmbMType.Items.Clear();
                cmbMType.Items.Add("材料");
                cmbMType.Items.Add("MLB");
                cmbMType.Items.Add("LCM");
                cmbMType.Text = "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowMaterialType()");
            }
        }
        #endregion

        #region ShowDdlType
        private void ShowDdlType()
        {

            DataTable dtTemp = new DataTable();
            try
            {
                cmbType.Items.Clear();
                cmbType.Items.Add("");
                dtTemp = objTransfer.CheckBwartAuthority();
                string[] aryTemp = dtTemp.Rows[0]["F_TEXT"].ToString().Trim().Split(new char[] { ';' });
                for (int i = 0; i < aryTemp.Length; i++)
                {
                    cmbType.Items.Add(aryTemp[i].ToString().Trim());
                }
                //for (int i = 0; i < dtTemp.Rows.Count; i++)
                //{
                //    cmbType.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlType()");
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlMblnr();
            if (cmbMblnr.Items.Count > 0)
            {
                this.cmbMblnr.SelectedIndex = 0;
            }
            else
            {
                cmbMblnr.Text = "";
            }

        }
        #endregion

        #region ShowDdlMblnr
        private void ShowDdlMblnr()
        {

            DataTable dtTemp = new DataTable();
            try
            {
                cmbMblnr.Items.Clear();
                if (cmbLgort.Text.ToString().Trim().Contains(",") && cmbLgort.Text.ToString().Trim().Contains("AS10"))
                {
                    MessageBox.Show("仓别AS10只能单选，请重新选择！！");
                    cmbLgort.Text = "";
                    return;
                }
                else
                {
                    dtTemp = objTransfer.GetMblnrData(cmbWerks.Text.ToString().Trim(), cmbLgort.Text.ToString().Trim(), cmbType.Text.ToString().Trim(), "");
                }
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbMblnr.Items.Add(dtTemp.Rows[i]["MBLNR"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlMblnr()");
            }
        }
        #endregion

        #region ShowDdlRoute
        private void ShowDdlRoute()
        {

            DataTable dtTemp = new DataTable();
            try
            {
                cmbSRoute.Items.Clear();
                cmbERoute.Items.Clear();
                cmbSRoute.Items.Add("");
                cmbERoute.Items.Add("");
                dtTemp = objTransfer.CheckRouteAuthority(cmbWerks.Text.ToString().Trim());
                string[] aryTemp = dtTemp.Rows[0]["F_TEXT"].ToString().Trim().Split(new char[] { ';' });
                for (int i = 0; i < aryTemp.Length; i++)
                {
                    cmbSRoute.Items.Add(aryTemp[i].ToString().Trim());
                    cmbERoute.Items.Add(aryTemp[i].ToString().Trim());
                }
                //for (int i = 0; i < dtTemp.Rows.Count; i++)
                //{
                //    cmbSRoute.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                //    cmbERoute.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlRoute()");
            }
        }
        private void cmbSRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbERoute.Text != "")
            {
                if (cmbSRoute.Text == cmbERoute.Text)
                {
                    MessageBox.Show("调拨起始厂区不能相同，请重置！！");
                }
            }
        }
        private void cmbERoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSRoute.Text != "")
            {
                if (cmbERoute.Text == cmbSRoute.Text)
                {
                    MessageBox.Show("调拨起始厂区不能相同，请重置！！");
                }
            }
        }
        #endregion

        #region showWhdwnDataGrid
        private void ShowWhdwnDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            this.dgvData.AllowUserToAddRows = false;
            //dgvData.ContextMenuStrip = this.cmsMenu;//增加拆箱和并箱操作

            try
            {
                //ITEM
                DataGridViewTextBoxColumn dgvcITEM = new DataGridViewTextBoxColumn();
                dgvcITEM.DataPropertyName = "ITEM";
                dgvcITEM.HeaderText = "Item";
                dgvcITEM.Width = 50;
                dgvcITEM.ReadOnly = true;
                dgvData.Columns.Add(dgvcITEM);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.Width = 120;
                dgvcMBLNR.ReadOnly = true;
                dgvData.Columns.Add(dgvcMBLNR);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "ZEILE";
                dgvcZEILE.Width = 50;
                dgvcZEILE.ReadOnly = true;
                dgvData.Columns.Add(dgvcZEILE);

                //MATNR
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dgvData.Columns.Add(dgvcMATNR);

                //KDMAT  
                DataGridViewTextBoxColumn dgvcKDMAT = new DataGridViewTextBoxColumn();
                dgvcKDMAT.DataPropertyName = "KDMAT";
                dgvcKDMAT.HeaderText = "Description";
                dgvcKDMAT.Width = 100;
                dgvcKDMAT.ReadOnly = true;
                dgvData.Columns.Add(dgvcKDMAT);

                //CHARG  
                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "Batch";
                dgvcCHARG.Width = 50;
                dgvcCHARG.ReadOnly = true;
                dgvData.Columns.Add(dgvcCHARG);

                //MENGE  
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "Qty";
                dgvcMENGE.Width = 50;
                dgvcMENGE.ReadOnly = true;
                dgvData.Columns.Add(dgvcMENGE);

                //BWART  
                DataGridViewTextBoxColumn dgvcBWART = new DataGridViewTextBoxColumn();
                dgvcBWART.DataPropertyName = "BWART";
                dgvcBWART.HeaderText = "MVT";
                dgvcBWART.Width = 50;
                dgvcBWART.ReadOnly = true;
                dgvData.Columns.Add(dgvcBWART);

                //Out Plant  
                DataGridViewTextBoxColumn dgvcOutPlant = new DataGridViewTextBoxColumn();
                dgvcOutPlant.DataPropertyName = "OutPlant";
                dgvcOutPlant.HeaderText = "OutPlant";
                dgvcOutPlant.Width = 100;
                dgvcOutPlant.ReadOnly = true;
                dgvData.Columns.Add(dgvcOutPlant);

                //In Plant  
                DataGridViewTextBoxColumn dgvcInPlant = new DataGridViewTextBoxColumn();
                dgvcInPlant.DataPropertyName = "InPlant";
                dgvcInPlant.HeaderText = "InPlant";
                dgvcInPlant.Width = 100;
                dgvcInPlant.ReadOnly = true;
                dgvData.Columns.Add(dgvcInPlant);

                DataGridViewTextBoxColumn dgvcType = new DataGridViewTextBoxColumn();
                dgvcType.DataPropertyName = "Type";
                dgvcType.HeaderText = "Type";
                dgvcType.Width = 100;
                dgvcType.ReadOnly = false;
                dgvData.Columns.Add(dgvcType);
                
                //DataGridViewComboBoxColumn dgvcType = new DataGridViewComboBoxColumn();
                //dgvcType.Items.Clear();
                //dgvcType.Items.Add("材料");
                //dgvcType.Items.Add("MLB");
                //dgvcType.Items.Add("LCM");
                //dgvcType.HeaderText = "Type";
                //dgvcType.Name = "Type";
                //dgvcType.Width = 100;
                //dgvcType.ReadOnly = false;
                //dgvcType.DisplayStyleForCurrentCellOnly = true;
                //this.dgvData.Columns.Add(dgvcType);


                dgvData.DataSource = dtData;
                lblSource.Text = dtData.Rows.Count.ToString() + " records";
                //dgvData.ClearSelection();
                dgvData.AllowUserToAddRows = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowWhdwnDataGrid()");
            }

        }
        private void dgvData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = (DataTable)dgvData.DataSource;
            if (e.RowIndex < dt.Rows.Count && e.RowIndex >= 0 && e.ColumnIndex < 11 && e.ColumnIndex > 0)
            {
                if (dgvData.Columns[e.ColumnIndex].HeaderText.ToString().Trim() == "Type" && e.RowIndex >= 0 && e.ColumnIndex == 10)
                {
                    if (dgvData.Rows[e.RowIndex].Cells[10].Value != null)
                    {
                        string strMblnr = "";
                        strMblnr = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString().Substring(0, 10);
                        string strType = "";
                        strType = dgvData.Rows[e.RowIndex].Cells[10].Value.ToString();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dgvData.Rows[i].Cells[1].Value.ToString().Substring(0, 10) == strMblnr)
                            {
                                dgvData.Rows[i].Cells[10].Value = strType;
                            }
                        }
                    }
                }
            }
        }
        
        #endregion

        #region showApplyItemDataGrid 历史调拨单
        private void showApplyItemDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();

            try
            {
                DataGridViewTextBoxColumn dgvcCrdate = new DataGridViewTextBoxColumn();
                dgvcCrdate.DataPropertyName = "CRDAT";
                dgvcCrdate.HeaderText = "Crdate";
                dgvcCrdate.Width = 80;
                dgvcCrdate.ReadOnly = true;
                dgvData.Columns.Add(dgvcCrdate);

                DataGridViewTextBoxColumn dgvcApplyno = new DataGridViewTextBoxColumn();
                dgvcApplyno.DataPropertyName = "APPLYNO";
                dgvcApplyno.HeaderText = "Applyno";
                dgvcApplyno.Width = 150;
                dgvcApplyno.ReadOnly = true;
                dgvData.Columns.Add(dgvcApplyno);

                DataGridViewTextBoxColumn dgvcTrtype = new DataGridViewTextBoxColumn();
                dgvcTrtype.DataPropertyName = "TRTYPE";
                dgvcTrtype.HeaderText = "Trtype";
                dgvcTrtype.Width = 40;
                dgvcTrtype.ReadOnly = true;
                dgvData.Columns.Add(dgvcTrtype);

                DataGridViewTextBoxColumn dgvcApplynm = new DataGridViewTextBoxColumn();
                dgvcApplynm.DataPropertyName = "APPLYNM";
                dgvcApplynm.HeaderText = "Applyname";
                dgvcApplynm.Width = 40;
                dgvcApplynm.ReadOnly = true;
                dgvData.Columns.Add(dgvcApplynm);

                DataGridViewTextBoxColumn dgvcAccount = new DataGridViewTextBoxColumn();
                dgvcAccount.DataPropertyName = "ACCOUNT";
                dgvcAccount.HeaderText = "Account";
                dgvcAccount.Width = 40;
                dgvcAccount.ReadOnly = true;
                dgvData.Columns.Add(dgvcAccount);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Kostl";
                dgvcKostl.Width = 40;
                dgvcKostl.ReadOnly = true;
                dgvData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcSroute = new DataGridViewTextBoxColumn();
                dgvcSroute.DataPropertyName = "SROUTE";
                dgvcSroute.HeaderText = "Sroute";
                dgvcSroute.Width = 40;
                dgvcSroute.ReadOnly = true;
                dgvData.Columns.Add(dgvcSroute);

                DataGridViewTextBoxColumn dgvcEroute = new DataGridViewTextBoxColumn();
                dgvcEroute.DataPropertyName = "EROUTE";
                dgvcEroute.HeaderText = "Eroute";
                dgvcEroute.Width = 40;
                dgvcEroute.ReadOnly = true;
                dgvData.Columns.Add(dgvcEroute);

                DataGridViewTextBoxColumn dgvcTrdat = new DataGridViewTextBoxColumn();
                dgvcTrdat.DataPropertyName = "TRDAT";
                dgvcTrdat.HeaderText = "Trdat";
                dgvcTrdat.Width = 40;
                dgvcTrdat.ReadOnly = true;
                dgvData.Columns.Add(dgvcTrdat);

                DataGridViewTextBoxColumn dgvcPlate = new DataGridViewTextBoxColumn();
                dgvcPlate.DataPropertyName = "PLATE";
                dgvcPlate.HeaderText = "Plate";
                dgvcPlate.Width = 40;
                dgvcPlate.ReadOnly = true;
                dgvData.Columns.Add(dgvcPlate);


                DataGridViewTextBoxColumn dgvcBox = new DataGridViewTextBoxColumn();
                dgvcBox.DataPropertyName = "BOX";
                dgvcBox.HeaderText = "Box";
                dgvcBox.Width = 40;
                dgvcBox.ReadOnly = true;
                dgvData.Columns.Add(dgvcBox);

                DataGridViewTextBoxColumn dgvcPCS = new DataGridViewTextBoxColumn();
                dgvcPCS.DataPropertyName = "PCS";
                dgvcPCS.HeaderText = "PCS";
                dgvcPCS.Width = 40;
                dgvcPCS.ReadOnly = true;
                dgvData.Columns.Add(dgvcPCS);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.Width = 150;
                dgvcMBLNR.ReadOnly = true;
                dgvData.Columns.Add(dgvcMBLNR);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "ZEILE";
                dgvcZEILE.Width = 40;
                dgvcZEILE.ReadOnly = true;
                dgvData.Columns.Add(dgvcZEILE);

                //MBLNR
                DataGridViewTextBoxColumn dgvcPlant = new DataGridViewTextBoxColumn();
                dgvcPlant.DataPropertyName = "WERKS";
                dgvcPlant.HeaderText = "Plant";
                dgvcPlant.Width = 40;
                dgvcPlant.ReadOnly = true;
                dgvData.Columns.Add(dgvcPlant);

                //ZEILE
                DataGridViewTextBoxColumn dgvcStorage = new DataGridViewTextBoxColumn();
                dgvcStorage.DataPropertyName = "LGORT";
                dgvcStorage.HeaderText = "Storage";
                dgvcStorage.Width = 40;
                dgvcStorage.ReadOnly = true;
                dgvData.Columns.Add(dgvcStorage);

                //MATNR
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                dgvData.Columns.Add(dgvcMATNR);

                //KDMAT  
                DataGridViewTextBoxColumn dgvcKDMAT = new DataGridViewTextBoxColumn();
                dgvcKDMAT.DataPropertyName = "KDMAT";
                dgvcKDMAT.HeaderText = "Description";
                dgvcKDMAT.Width = 100;
                dgvcKDMAT.ReadOnly = true;
                dgvData.Columns.Add(dgvcKDMAT);

                //CHARG  
                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "Batch";
                dgvcCHARG.Width = 40;
                dgvcCHARG.ReadOnly = true;
                dgvData.Columns.Add(dgvcCHARG);

                //MENGE  
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "Qty";
                dgvcMENGE.Width = 50;
                dgvcMENGE.ReadOnly = true;
                dgvData.Columns.Add(dgvcMENGE);

                //BWART  
                DataGridViewTextBoxColumn dgvcBWART = new DataGridViewTextBoxColumn();
                dgvcBWART.DataPropertyName = "BWART";
                dgvcBWART.HeaderText = "MVT";
                dgvcBWART.Width = 50;
                dgvcBWART.ReadOnly = true;
                dgvData.Columns.Add(dgvcBWART);

                //In Plant  
                DataGridViewTextBoxColumn dgvcInPlant = new DataGridViewTextBoxColumn();
                dgvcInPlant.DataPropertyName = "DWERKS";
                dgvcInPlant.HeaderText = "InPlant";
                dgvcInPlant.Width = 100;
                dgvcInPlant.ReadOnly = true;
                dgvData.Columns.Add(dgvcInPlant);

                DataGridViewTextBoxColumn dgvcType = new DataGridViewTextBoxColumn();
                dgvcType.DataPropertyName = "TYPE";
                dgvcType.HeaderText = "Type";
                dgvcType.Width = 50;
                dgvcType.ReadOnly = false;
                dgvData.Columns.Add(dgvcType);

                DataGridViewTextBoxColumn dgvcCrname = new DataGridViewTextBoxColumn();
                dgvcCrname.DataPropertyName = "CRNAM";
                dgvcCrname.HeaderText = "Crname";
                dgvcCrname.Width = 80;
                dgvcCrname.ReadOnly = true;
                dgvData.Columns.Add(dgvcCrname);

                DataGridViewTextBoxColumn dgvcFlage = new DataGridViewTextBoxColumn();
                dgvcFlage.DataPropertyName = "FLAGE";
                dgvcFlage.HeaderText = "Flage";
                dgvcFlage.Width = 50;
                dgvcFlage.ReadOnly = false;
                dgvData.Columns.Add(dgvcFlage);

                dgvData.DataSource = dtItem;
                lblSource.Text = dtItem.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-showApplyItemDataGrid()");
            }
        }
        #endregion

        #region txtUserID_KeyDown
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                stsWarning.Text = "";
                if (txtUserID.Text.Trim() == "")
                {
                    stsWarning.Text = "请输入工号！！";
                    return;
                }
                else
                {
                    try
                    {
                        DataTable dtHR = new DataTable();
                        Admin objAdmin = new Admin(UserData, Progid);
                        dtHR = objAdmin.CheckHR(txtUserID.Text.Trim());
                        if (dtHR != null && dtHR.Rows.Count > 0)
                        {
                            if (dtHR.Rows[0]["onjobs"].ToString() != "1")
                            {
                                MessageBox.Show("此员工未在职，请确认!!");
                                return;
                            }
                            txtUserName.Text = dtHR.Rows[0]["ChineseName"].ToString();
                            txtFiCode.Text = dtHR.Rows[0]["Department"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("HR无工号：" + txtUserID.Text.Trim() + "的资料,验证失败");
                            return;
                        }
                    }
                    catch 
                    {
                        try
                        {
                            if (this.CheckID(txtUserID.Text.Trim()))
                            {
                                txtUserName.Text = dtHR.Rows[0]["chinam"].ToString();
                                txtFiCode.Text = dtHR.Rows[0]["depcod"].ToString();
                                return;
                            }
                            else
                            {
                                stsWarning.Text = "员工工号错误，请确认！";
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            stsWarning.Text = ex.Message;
                            return;
                        }
                    }
                }
                //if (this.CheckID(txtUserID.Text.Trim()))
                //{
                //    txtUserName.Text = dtHR.Rows[0]["chinam"].ToString();
                //    txtFiCode.Text = dtHR.Rows[0]["depcod"].ToString();
                //    return;
                //}
                //else
                //{
                //    stsWarning.Text = "员工工号错误，请确认！";
                //    return;
                //}
            }
        }

        #endregion

        #region 不用（接口从OA换成了AlimAPI） 身份验证
        private bool CheckID(string UserID)
        {
            bool bl = false;
            //string strNam = "";
            //DataTable dtHR = new DataTable();
            DataTable dtEmployeeData = new DataTable();
            //string strTextFail =  "刷卡验证失败";
            // QWMS.EmployeeData.QueryEmployeeData objHR = new EmployeeData.QueryEmployeeData();
            Admin objAdmin = new Admin(UserData, Progid);
            dtEmployeeData = objAdmin.GetEmployeeData(txtUserID.Text.ToString());
            if (dtEmployeeData.Rows.Count > 0)
            {
                dtHR = dtEmployeeData;
            }
           
            //dtHR = objBorrowMateria.GetHR(UserID.Trim());
            if (dtHR.Rows.Count > 0)
            {
                if (dtHR.Rows[0]["onjobs"].ToString() != "1")//状态1为在职
                {
                    MessageBox.Show("此员工已离职，请确认!!");
                    return bl;
                }

                bl = true;
                return bl;
            }
            else
            {
                MessageBox.Show("HR无工号：" + UserID + "的资料,验证失败");
                return bl;
            }
        }

        #endregion

        #region more_Click 单据
        private void more_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                }
                else
                {
                    strWerks = string.Empty;
                }

                strLgort = cmbLgort.Text.ToString().Trim();

                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                string strType = cmbType.Text.ToString().Trim();
                //ArrayList alMblnrs = new ArrayList();
                Transfer_MblnrSelect objTransfer_MblnrSelect = new Transfer_MblnrSelect(UserData, strWerks, strLgort, strType, "0");
                objTransfer_MblnrSelect.ShowDialog();
                aryReturn = objTransfer_MblnrSelect.aReturn;
                cmbMblnr.Text = GetMblnrData();

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnQuery --查询单据明细
        private void btnQuery_Click(object sender, EventArgs e)
        {
            txtApplyNo.Text = "";
            Query();
        }

        private void cmbMblnr_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Query();
            }
        }

        private void Query()
        {
            stsWarning.Text = "";
            btnSave.Enabled = true;
            strWerks = cmbWerks.Text.ToString().Trim();
            strLgort = cmbLgort.Text.ToString().Trim();
            string strMblnr = cmbMblnr.Text.ToString().Trim();
            string strType = cmbType.Text.ToString().Trim();
            if (strMblnr.Equals(""))
            {
                stsWarning.Text = "扣账编号不能为空，请确认!!";
                return;
            }
            if (cmbMType.Text.ToString().Trim()=="")
            {
                stsWarning.Text = "类型不能为空，请选择!!";
                return;
            }
            if (dtData.Rows.Count > 0)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (strMblnr.Contains(dtData.Rows[i]["MBLNR"].ToString().Trim().Substring(0, 10)))
                    {
                        stsWarning.Text = dtData.Rows[i]["MBLNR"].ToString().Trim().Substring(0, 10) + "：此单号已带出没明细，请知悉！！";
                        return;
                    }
                }
            }
            DataTable dt = new DataTable();
            //check 10位扣账编号是否有分两次出库，有其他item未出库的情况
            DataTable dtCheck = new DataTable();
            //判断仓别是否为自动仓
            bool isAsrs;
            isAsrs = objTransfer.CheckIsAsrs(strWerks, strLgort);
            if (isAsrs)
            {
                dtCheck = objTransfer.QueryMblnrStatus(strWerks, strLgort, strMblnr);
                if (dtCheck.Rows.Count > 0)
                {
                    string strItem = "";
                    for (int i = 0; i < dtCheck.Rows.Count; i++)
                    {
                        strItem = strItem + dtCheck.Rows[i]["MBLNR"].ToString() + ";";
                    }
                    MessageBox.Show("单据号：" + strItem + "未作业完不能申请调拨车");
                    return;
                }
            }
            dt = objTransfer.QueryMblnrData(strWerks, strLgort, strMblnr, strType);
            if (dt.Rows.Count > 0)
            {
                if (dtData.Columns.Count == 0)
                {
                    dtData = dt.Clone();
                    dtData.Columns.Add("OutPlant");
                    dtData.Columns.Add("InPlant");
                }            
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtData.NewRow();
                    string BWART = dt.Rows[i]["BWART"].ToString();
                    dr["ITEM"] = (dtData.Rows.Count + 1).ToString();
                    dr["WERKS"] = dt.Rows[i]["WERKS"].ToString();
                    dr["LGORT"] = dt.Rows[i]["LGORT"].ToString();
                    dr["MBLNR"] = dt.Rows[i]["MBLNR"].ToString();
                    dr["ZEILE"] = dt.Rows[i]["ZEILE"].ToString();
                    dr["MATNR"] = dt.Rows[i]["MATNR"].ToString();
                    dr["CHARG"] = dt.Rows[i]["CHARG"].ToString();
                    dr["MENGE"] = dt.Rows[i]["MENGE"].ToString();
                    dr["BWART"] = dt.Rows[i]["BWART"].ToString();
                    dr["KDMAT"] = dt.Rows[i]["KDMAT"].ToString();
                    dr["OutPlant"] = dt.Rows[i]["WERKS"].ToString() + "/" + dt.Rows[i]["LGORT"].ToString();
                    dr["InPlant"] = dt.Rows[i]["KOSTL"].ToString();
                    //if (BWART == "303" || BWART == "351" || BWART == "45L" || BWART == "60S" || BWART == "901" || BWART == "911")
                    //{
                    //    dr["InPlant"] = dt.Rows[i]["KOSTL"].ToString();
                    //}
                    //else
                    //{
                    //    dr["InPlant"] = dt.Rows[i]["WERKS"].ToString();
                    //}

                    //dr["InPlant"] = dt.Rows[i]["WERKS"].ToString();
                    dr["Type"] = cmbMType.Text.ToString();
                    dtData.Rows.Add(dr);
                }
                ShowWhdwnDataGrid();
                btnSave.Enabled = true;
                cmbMblnr.Text = "";
            }
            else
            {
                stsWarning.Text = "无数据，请确认!!";
                return;
            }
        }
        #endregion

        #region btnSave --保存数据
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            int PlateNum;
            int BoxNum;
            int PCSNum;
            string strPlate = txtPlate.Text.ToString().Trim();
            string strBox = txtBox.Text.ToString().Trim();
            string strPCS = txtPCS.Text.ToString().Trim();
            string strUserID = txtUserID.Text.ToString().Trim();
            string strUserName = txtUserName.Text.ToString().Trim();
            string strFiCode = txtFiCode.Text.ToString().Trim();

            string strRoute1 = cmbSRoute.Text.ToString().Trim();  //调拨路线起
            string strRoute2 = cmbERoute.Text.ToString().Trim();  //调拨路线终

            string ApplyTime = dtApplyTime.Value.ToString("yyyy-MM-dd");
            string strTime = txtTime.Text.ToString().Trim();//.Insert(2,":")+":00"
            int time;

            string strApplyNo;
            string strApplyType = cmbApplyType.Text.ToString();

            if (!txtApplyNo.Text.ToString().Trim().Equals(""))
            {
                MessageBox.Show("已生成申请单号:" + txtApplyNo.Text.ToString().Trim() + ",请确认！！");
                return;
            }

            if (strUserID.Equals("") || strUserName.Equals("") || strFiCode.Equals(""))
            {
                MessageBox.Show("员工工号、姓名和部门代码不能为空，请知悉！！");
                return;
            }
            if (cmbERoute.Text == cmbSRoute.Text)
            {
                MessageBox.Show("调拨起始厂区不能相同，请重置！！");
                return;
            }
            
            //bool flg = CheckID(txtUserID.Text.Trim());
            if (!strPlate.Equals(""))
            {
                if (!int.TryParse(strPlate, out PlateNum))
                {
                    MessageBox.Show("板数必须为整数，请知悉！！");
                    return;
                }
            }
            else
            {
                strPlate = "0";
            }
            if (strBox.Equals("") && strPCS.Equals(""))
            {
                MessageBox.Show("箱数和PCS不能同时为空");
                return;
            }

            if (!strBox.Equals(""))
            {
                if (!int.TryParse(strBox, out BoxNum))
                {
                    MessageBox.Show("箱数必须为整数，请知悉！！");
                    return;
                }
            }
            else
            {
                strBox = "0";
            }
            if (!strPCS.Equals(""))
            {
                if (!int.TryParse(strPCS, out PCSNum))
                {
                    MessageBox.Show("PCS必须为整数，请知悉！！");
                    return;
                }
            }
            else
            {
                strPCS = "0";
            }

            if (strRoute1.Equals("") || strRoute2.Equals(""))
            {
                MessageBox.Show("调拨路线不能为空，请知悉！！");
                return;
            }

            if (strTime.Equals("")||strTime.Length != 4)
            {
                MessageBox.Show("调拨时间请手动输入4位时分，谢谢！！");
                return;
            }
            else
            {
                try 
                {
                    if ((!int.TryParse(strTime, out time)) || (time < 0 || time > 2400) || int.Parse(strTime.Substring(2, 2)) > 59)
                    {
                        MessageBox.Show("调拨时间手动输入的时分格式不正确，请确认！！");
                        return;
                    }
                    ApplyTime = ApplyTime + " " + strTime.Insert(2, ":") + ":00";
                }
                catch(Exception ex) 
                {
                    MessageBox.Show("调拨时间手动输入的时分格式转换不正确，请重新输入！！");
                    return;
                }                
            }
            if (DateTime.Now > DateTime.Parse(ApplyTime))
            {
                MessageBox.Show("调拨时间需要大于现在时间，请确认！！");
                return;
            }
            if (dtData.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgvData.DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dgvData.Rows[i].Cells[10].Value == null)
                    {
                        stsWarning.Text = "保存失败，请选择第" + (i + 1) + "行明细的类型！！";
                        return;
                    }
                    if (dtData.Rows[i]["KDMAT"].ToString() == "")
                    {
                        stsWarning.Text = "保存失败，请维护第" + (i + 1) + "行料号描述！！";
                        return;
                    }
                    dt.Rows[i]["Type"] = dgvData.Rows[i].Cells[10].Value.ToString().Trim();
                }
                btnSave.Enabled = false;
                strApplyNo = objTransfer.CreateApplyNo(strWerks);
                txtApplyNo.Text = strApplyNo;
                bool Result = objTransfer.InsertTrHeader(strUserID, strUserName, strFiCode, strRoute1, strRoute2, ApplyTime, strPlate, strBox, strPCS, strApplyNo, strApplyType, dt);
                //objTransfer.QueryMblnrData(strWerks, strLgort, strMblnr, strType);
                if (Result)
                {
                    txtApplyNo.Text = strApplyNo;
                    stsWarning.Text = "保存成功！！";
                    bool blReuslt = SendMail(dt);
                    if (blReuslt)
                    {
                        stsWarning.Text = "Send Mail Success!!";
                    }
                    else
                    {
                        btnSendMail.Visible = true;
                        stsWarning.Text = "Send Mail Fail!!,请点击Send Mail重发邮件";
                    }
                    this.dgvData.DataSource = null;
                    this.cmbMblnr.Text = "";
                    dtData.Rows.Clear();
                    lblSource.Text = "";
                    return;
                }
                else
                {
                    stsWarning.Text = "保存失败，请确认！！";
                    return;
                }
            }
            else
            {
                MessageBox.Show("单据为空，请查询！！");
            }
            btnSave.Enabled = true;
        }
        #endregion

        #region btnExit --退出窗口
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnRefresh --刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {

            //txtUserID.Text = "";
            //txtUserName.Text = "";
            //txtFiCode.Text = "";
            txtTime.Text = "";
            txtPlate.Text = "";
            txtBox.Text = "";
            txtPCS.Text = "";
            txtApplyNo.Text = "";
            cmbSRoute.Text = "";
            cmbERoute.Text = "";
            txtPlate.Text = "";
            cmbMblnr.Text = "";
            cmbMType.Text = "";
            cmbType.Text = "";
            dgvData.DataSource = null;
            stsWarning.Text = "";

            dtData.Clear();
            dtItem.Clear();
            btnSave.Enabled = false;
            btnSendMail.Visible = false;
            this.lblSource.Text = "0 records";
        }
        #endregion

        #region GetMblnrData
        private string GetMblnrData()
        {
            try
            {
                StringBuilder sbMblnr = new StringBuilder();
                sbMblnr.Remove(0, sbMblnr.Length);
                for (int i = 0; i < aryReturn.Count; i++)
                {
                    if (i != 0)
                        sbMblnr.Append(",");
                    sbMblnr.Append(aryReturn[i].ToString().Trim());
                }
                return sbMblnr.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetMblnrData()");
            }
        }
        #endregion

        #region btnHistory_Click历史调拨单 按厂区、日期查询已生成的调拨单的49单据明细
        private void btnHistory_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            dtItem.Clear();
            strWerks = cmbWerks.Text.ToString().Trim();
            string dtpTime = dtApplyTime.Value.ToString("yyyy-MM-dd");
            if (strWerks == "")
            {
                stsWarning.Text = "查询调拨单，厂区不为空!!";
            }
            dtItem = objTransfer.QueryMblnrItem(strWerks, dtpTime);
            if (dtItem.Rows.Count > 0)
            {
                showApplyItemDataGrid();
            }
            else
            {
                stsWarning.Text = "无数据，请重置查询条件（厂区和日期）！！";
            }
        }
        #endregion

        #region LGORT more
        private void btnLgort_Click(object sender, EventArgs e)
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
                if (dtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("无仓别数据！");
                }
                else
                {
                    Transfer_MblnrSelect objTransfer_MblnrSelect = new Transfer_MblnrSelect(dtTemp, "1");
                    objTransfer_MblnrSelect.ShowDialog();
                    aryReturn = objTransfer_MblnrSelect.aReturn;
                    cmbLgort.Text = GetMblnrData();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }


        #endregion

        #region MVT more
        private void btnMVT_Click(object sender, EventArgs e)
        {
            try
            {
                Authority objAuthority = new Authority(UserData);
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                dtTemp = objTransfer.CheckBwartAuthority();
                if (dtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("无MVT数据！");
                }
                else
                {
                    Transfer_MblnrSelect objTransfer_MblnrSelect = new Transfer_MblnrSelect(dtTemp, "2");
                    objTransfer_MblnrSelect.ShowDialog();
                    aryReturn = objTransfer_MblnrSelect.aReturn;
                    cmbType.Text = GetMblnrData();
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region btnDownload --下载历史调拨单
        private void btnDownload_Click(object sender, EventArgs e)
        {
            string strExportName = "";
            try
            {
                if (sfdSaveFile.ShowDialog() == DialogResult.OK)
                {
                    strExportName = sfdSaveFile.FileName;
                    CountingResult2File(strExportName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-btnDownload_Click()");
            }
        }

        private void CountingResult2File(string strFilePath)
        {
            //Crdate Applyno Trtype Applyname Account Kostl Sroute Eroute Trdat Plate Box PCS MBLNR ZEILE Plant Storage MATNR Description Batch Qty MVT InPlant Type Crname
            string strLine = "";
            try
            {
                fi = new FileInfo(strFilePath);
                sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);
                strLine = "Crdate\tApplyno\tTrtype\tApplyname\tAccount\tKostl\tSroute\tEroute\tTrdat\tPlate\tBox\tPCS\tMBLNR\tZEILE\tPlant\tStorage\tMATNR\tBatch\tDescription\tQty\tInPlant\tMVT\tType\tCrname\tFlage";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    //H.CRDAT,H.APPLYNO,TRTYPE,APPLYNM,ACCOUNT,KOSTL,SROUTE,EROUTE,TRDAT,PLATE,BOX,PCS,MBLNR,ZEILE,WERKS,LGORT,MATNR,CHARG,KDMAT,MENGE,DWERKS,DLGORT,BWART,I.CRNAM
                    strLine = "";
                    strLine += dtItem.Rows[i]["CRDAT"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["APPLYNO"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["TRTYPE"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["APPLYNM"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["ACCOUNT"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["KOSTL"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["SROUTE"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["EROUTE"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["TRDAT"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["PLATE"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["BOX"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["PCS"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["MBLNR"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["ZEILE"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["WERKS"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["LGORT"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["MATNR"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["CHARG"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["KDMAT"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["MENGE"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["DWERKS"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["BWART"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["TYPE"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["CRNAM"].ToString() + "\t";
                    strLine += dtItem.Rows[i]["FLAGE"].ToString();
                    sw.WriteLine(strLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }
        }
        #endregion

        #region SendMail
        private bool SendMail(DataTable dt)
        {
            bool blResult = false;
            string strPlate = txtPlate.Text.ToString().Trim();
            string strBox = txtBox.Text.ToString().Trim();
            string strPCS = txtPCS.Text.ToString().Trim();
            string strUserID = txtUserID.Text.ToString().Trim();
            string strUserName = txtUserName.Text.ToString().Trim();
            string strFiCode = txtFiCode.Text.ToString().Trim();
            string strRoute1 = cmbSRoute.Text.ToString().Trim();  //调拨路线起
            string strRoute2 = cmbERoute.Text.ToString().Trim();  //调拨路线终
            string ApplyTime = dtApplyTime.Value.ToString("yyyy/MM/dd");
            string strTime = txtTime.Text.ToString().Trim().Insert(2, ":");
            string strApplyNo = txtApplyNo.Text.ToString();
            string strApplyType = cmbApplyType.Text.ToString();
            string strIwkNo = txtIworkflowNo.Text.ToString().Trim();
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
            string strUrl = objStorageData.GetSendMailUrl();
            try
            {
                //MailService.SendMailService objSendMail = new MailService.SendMailService();
                ClaHttpHelper clahttpHelper = new ClaHttpHelper();
                string strMailPSD = "975A8056C8DF50786FB680A96E0CCCBF";
                string strMailFrom = "Web_Notice@quantacn.com";
                string strMailSubject = strRoute1 + "->" + strRoute2 + " QWMS调拨车申请 " + DateTime.Now.ToString("yyyyMMddHHmm");
                string strMailTo = string.Empty;
                string strMailCC = string.Empty;
                string strMges = "";
                DataTable dtTempTo = new DataTable();
                DataTable dtTempCC = new DataTable();
                dtTempTo = objTransfer.CheckSendMailAuthority(strRoute1,"0");
                dtTempCC = objTransfer.CheckSendMailAuthority(strRoute1, "1");
                //strMailTo = "B012A089";
                //strMailCC = "";
                if (dtTempTo.Rows.Count <= 0 && dtTempCC.Rows.Count <= 0)
                {
                    stsWarning.Text = "请先配置邮件相关人员！！";
                    return blResult;
                }
                else 
                {
                    if (dtTempTo.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTempTo.Rows.Count; i++)
                        {
                            strMailTo = strMailTo + dtTempTo.Rows[i]["EMAIL"].ToString().Trim() + ";";
                        }
                        strMailTo = strMailTo + txtUserID.ToString().Trim().ToUpper();
                    }
                    if (dtTempCC.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTempCC.Rows.Count; i++)
                        {
                            strMailCC = strMailCC + dtTempCC.Rows[i]["EMAIL"].ToString().Trim() + ";";
                        }
                    }
                    //strMailTo = dtTempTo.Rows[0]["CS"].ToString() + ";" + txtUserID.ToString().Trim().ToUpper();
                    //strMailCC = dtTempCC.Rows[0]["CC"].ToString();
                }


                StringBuilder sbMailContent = new StringBuilder();
                sbMailContent.Append("<HTML><BODY leftmargin=1 topmargin=1><p>Dear  All：</p><p>&#12288;&#12288;" + strRoute1 + "->" + strRoute2 + " QWMS调拨车申请如下，请协助派车，谢谢!</p><CENTER>");
                sbMailContent.Append("<HR SIZE=1 width=100%><BR>");
                sbMailContent.Append("<TABLE width=100% border=1 align=center cellpadding=4 bordercolor=#3366cc style='border-collapse: collapse'>");
                sbMailContent.Append(" <tr style=\"text-align: center; background-color:#6699cc;height: 34px; color: #FFFFFF;\"><td>Crdate</td><td>Applyno</td><td>IworkflowNo</td><td>TRtype</td><td>Username</td><td>UserID</td><td>Ficode</td><td>SRoute</td><td>ERoute</td><td>TRtime</td><td>PBPCS</td><td>Mblnr</td><td>item</td><td>Matnr</td><td>Batch</td><td>Description</td><td>Qty</td><td>MVT</td><td>OutPlant</td><td>InPlant</td><td>Type</td></tr>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr;
                    dr = dt.Rows[i];
                    if (i == 0)
                    {
                        sbMailContent.Append("<tr style=\" color:Red;\"><td>  " + DateTime.Now.ToString() + "</td><td>" + strApplyNo
                                     + "</td><td>" + strIwkNo+"</td><td>" + strApplyType + "</td><td> " + strUserName
                                     + "</td><td>" + strUserID + "</td><td> " + strFiCode
                                     + "</td><td>  " + strRoute1 + "</td><td>" + strRoute2
                                     + "</td><td>" + ApplyTime + " " + strTime + "</td><td> " + strPlate
                                     + "板=" + strBox + "箱+" + strPCS + "PCS" + "</td><td>  " + dr["MBLNR"].ToString() + "</td><td>" + dr["ZEILE"].ToString()
                                     + "</td><td>" + dr["MATNR"].ToString() + "</td><td> " + dr["CHARG"].ToString()
                                     + "</td><td>" + dr["KDMAT"].ToString() + "</td><td> " + dr["MENGE"].ToString()
                                     + "</td><td>  " + dr["BWART"].ToString() + "</td><td>" + dr["WERKS"].ToString() + "/" + dr["LGORT"].ToString()
                                     + "</td><td>" + dr["KOSTL"].ToString() + "</td><td> " + dr["Type"].ToString()
                                     + "</td></tr>");
                    }
                    else
                    {
                        sbMailContent.Append("<tr style=\" color:Red;\"><td>  " + DateTime.Now.ToString() + "</td><td>" + strApplyNo
                                    + "</td><td>" + strIwkNo + "</td><td>" + strApplyType + "</td><td> " + strUserName
                                    + "</td><td>" + strUserID + "</td><td> " + strFiCode
                                    + "</td><td>  " + strRoute1 + "</td><td>" + strRoute2
                                    + "</td><td>" + ApplyTime + " " + strTime + "</td><td></td><td>  " + dr["MBLNR"].ToString() + "</td><td>" + dr["ZEILE"].ToString()
                                    + "</td><td>" + dr["MATNR"].ToString() + "</td><td> " + dr["CHARG"].ToString()
                                    + "</td><td>" + dr["KDMAT"].ToString() + "</td><td> " + dr["MENGE"].ToString()
                                    + "</td><td>  " + dr["BWART"].ToString() + "</td><td>" + dr["WERKS"].ToString() + "/" + dr["LGORT"].ToString()
                                    + "</td><td>" + dr["KOSTL"].ToString() + "</td><td> " + dr["Type"].ToString()
                                    + "</td></tr>");
                    }
                }
                sbMailContent.Append("</TD></TR></TABLE><BR><CENTER><br><HR SIZE=1 width=100%>");
                sbMailContent.Append("</BODY></HTML>");
                var Data = new
                {
                    Site = "QSMC",
                    TeamPwd = strMailPSD,
                    From = strMailFrom,
                    To = strMailTo,
                    Cc = strMailCC,
                    Bcc = "",
                    Subject = strMailSubject,
                    Body = sbMailContent.ToString(),
                    IsBodyHtml = true,
                    Attachments = "",
                };
                string strData = JsonConvert.SerializeObject(Data);
                JObject result = (JObject)JsonConvert.DeserializeObject(clahttpHelper.HttpPostByHttpWebRequest(strUrl, strData));
                if (result["Result"].ToString().ToUpper() == "TRUE")
                {
                    blResult = true;
                }
                //blResult = objSendMail.SendMail(strMailPSD, true, strMailFrom, strMailTo, strMailCC, "", strMailSubject, sbMailContent.ToString(), "", out strMges);
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return blResult;
            }
            return blResult;
        }
        #endregion   

        #region delete
        private void btndelete_Click(object sender, EventArgs e)
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("MBLNR");
            string strNew = "";
            string strdelete="";
            if (dtData.Rows.Count > 0)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (!strNew.Contains(dtData.Rows[i]["MBLNR"].ToString().Trim().Substring(0, 10)))
                    {
                        DataRow dr = dtnew.NewRow();
                        strNew = strNew + dtData.Rows[i]["MBLNR"].ToString().Trim().Substring(0, 10);
                        dr["MBLNR"] = dtData.Rows[i]["MBLNR"].ToString().Trim().Substring(0, 10);
                        dtnew.Rows.Add(dr);
                    }
                }
                Transfer_MblnrSelect objTransfer_MblnrSelect = new Transfer_MblnrSelect(dtnew, "3");
                objTransfer_MblnrSelect.ShowDialog();
                aryReturn = objTransfer_MblnrSelect.aReturn;
                strdelete = GetMblnrData();
                if (strdelete == "")
                {
                    MessageBox.Show("删除单据明细为空！");
                }
                else
                {
                    DataTable dtnewData = dtData.Clone();
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (!strdelete.Contains(dtData.Rows[i]["MBLNR"].ToString().Trim().Substring(0, 10)))
                        {
                            DataRow drnew = dtData.NewRow();
                            drnew=dtData.Rows[i];
                            dtnewData.Rows.Add(drnew.ItemArray);
                            //dtData.Rows[i].Delete();
                        }
                    }
                    dtData = dtnewData;
                    if (dtData.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtData.Rows.Count; i++)
                        {
                            dtData.Rows[i]["ITEM"] = i + 1;
                            dtData.Rows[i]["Type"] = dgvData.Rows[i].Cells[10].Value;
                        }
                    }
                    ShowWhdwnDataGrid();
                    //dtData.AcceptChanges();
                }
            }
            else
            {
                MessageBox.Show("无单据明细！");
            }
        }
        #endregion

        #region 邮件发送失败开放重发邮件按钮
        private void btnSendMail_Click(object sender, EventArgs e)
        {
            string strApplno = txtApplyNo.Text.ToString().Trim();
            string strKostl = txtFiCode.Text.ToString().Trim();
            DataTable dt = new DataTable();
            dt = objTransfer.QuryTrITME(strApplno, strKostl);//填入调拨单号及部门代码
            bool blReuslt = SendMail(dt);
            if (blReuslt)
            {
                stsWarning.Text = "Send Mail Success!!";
            }
            else
            {
                stsWarning.Text = "Send Mail Fail!!,请联系QWMS负责人，查看邮件接口是否有问题";
            }
        }
        #endregion
    }
}
