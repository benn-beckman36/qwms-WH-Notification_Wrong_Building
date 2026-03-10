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
using System.Data.SqlClient;
using QCI_QWMS_StorageData;

namespace QWMS
{
    public partial class Manage_ReceivePlatform : Form
    {
        //public StorageIn_ReceivePlatform()
        //{
        //    InitializeComponent();
        //}
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        //private string strLgort = "";
        private string strProgid = "";

        private CarData objCarData;
        private ArrayList alMblnrs = new ArrayList();
        private DataTable dtECSource = new DataTable();
        private DataTable dtCarECSource = new DataTable();
        //private DataTable dtStorage = new DataTable();
        //private DataTable dtCombineStorage = new DataTable();

        #endregion

        #region DataMember

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

        //public string Lgort
        //{
        //    get
        //    {
        //        return strLgort;
        //    }
        //    set
        //    {
        //        strLgort = value;
        //    }
        //}

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

        public Manage_ReceivePlatform(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            dtECSource.Columns.Add("ITEM");
            dtECSource.Columns.Add("VBELN");
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            //lblCompany.Text = Comcd;
            //lblUserid.Text = Usrnm;

            try
            {
                //QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                objCarData = new CarData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                //if (false)
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    //ShowDdlLgort();
                    //ShowPrintCheckBox();

                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    //if (cmbLgort.Items.Count > 0)
                    //{
                    //    this.cmbLgort.SelectedIndex = 0;
                    //}

                    // ResetPage();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        //public bool OpenDataBase()
        //{
        //    try
        //    {
        //        //创建数据库连接对象
        //        using (SqlConnection sqlConn = new SqlConnection("server=10.243.19.114;database=QEC;uid=cagec;pwd=1l4ej0m/4 "))
        //        {
        //            // 创建SqlCommand
        //            sqlConn.Open();
        //            //SqlCommand mySqlCommand = new SqlCommand();
        //            SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT TOP 10 VBELN,BELNR,BUDAT+'-'+CPUTM AS DAT FROM EKBE", sqlConn);
        //            DataTable dt = new DataTable();
        //            sqlDa.Fill(dt);


                 
        //            return true;
        //            //}
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}



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

        //#region ShowDdlLgort
        //private void ShowDdlLgort()
        //{
        //    try
        //    {
        //        Authority objAuthority = new Authority(UserData);
        //        // stsWarning.Text = string.Empty;
        //        DataTable dtTemp = new DataTable();
        //        if (cmbWerks.SelectedIndex != -1)
        //        {
        //            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
        //            dtTemp = objAuthority.CheckLgortAuthority(strWerks);
        //        }
        //        else
        //        {
        //            dtTemp = objAuthority.CheckLgortAuthority();
        //        }
        //        if (cmbLgort.SelectedIndex != -1)
        //        {
        //            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
        //        }
        //        else
        //        {
        //            cmbLgort.Items.Clear();
        //        }

        //        if (dtTemp.Rows.Count == 0)
        //        {
        //            cmbLgort.Items.Clear();
        //            strLgort = string.Empty;
        //        }
        //        else
        //        {
        //            cmbLgort.Items.Clear();
        //            for (int i = 0; i < dtTemp.Rows.Count; i++)
        //            {
        //                cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
        //                if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
        //                {
        //                    cmbLgort.SelectedIndex = i;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowDdlLgort()");
        //    }
        //}
        //#endregion

        #region ShowStorageDataGrid

        private void ShowStorageDataGrid()
        {
            dgvEC.AutoGenerateColumns = false;
            dgvEC.Columns.Clear();
            try
            {
                //ITEM
                DataGridViewTextBoxColumn dgvcITEM = new DataGridViewTextBoxColumn();
                dgvcITEM.DataPropertyName = "ITEM";
                dgvcITEM.HeaderText = "序号";
                dgvcITEM.Width = 100;
                dgvcITEM.ReadOnly = true;
                dgvEC.Columns.Add(dgvcITEM);

                //VBELN   EC单号
                DataGridViewTextBoxColumn dgvcVBELN = new DataGridViewTextBoxColumn();
                dgvcVBELN.DataPropertyName = "VBELN";
                dgvcVBELN.HeaderText = "EC单号";
                dgvcVBELN.Width = 130;
                dgvcVBELN.ReadOnly = true;
                dgvEC.Columns.Add(dgvcVBELN);

                //DataGridViewTextBoxColumn dgvcCHARG1 = new DataGridViewTextBoxColumn();
                //dgvcCHARG1.DataPropertyName = "CHARG1";
                //dgvcCHARG1.HeaderText = "版本";
                //dgvcCHARG1.Width = 90;
                //dgvcCHARG1.ReadOnly = true;
                //dgvEC.Columns.Add(dgvcCHARG1);

                ////MATNR
                //DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                //dgvcMENGE.DataPropertyName = "MENGE";
                //dgvcMENGE.HeaderText = "数量";
                //dgvcMENGE.Width = 90;
                //dgvcMENGE.ReadOnly = true;
                //dgvEC.Columns.Add(dgvcMENGE);

                dgvEC.DataSource = dtECSource;
                lblOutSource.Text = dtECSource.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowStorageDataGrid()");
            }
        }
        #endregion

        #region ShowOrderNoDataGrid
        private void ShowOrderNoDataGrid()
        {
            enddgvEC.AutoGenerateColumns = false;
            enddgvEC.Columns.Clear();

            try
            {
                //MANDT   
                DataGridViewTextBoxColumn dgvcMANDT = new DataGridViewTextBoxColumn();
                dgvcMANDT.DataPropertyName = "MANDT";
                dgvcMANDT.HeaderText = "MANDT";
                dgvcMANDT.Width = 50;
                dgvcMANDT.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcMANDT);

                //COMCD
                DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                dgvcCOMCD.DataPropertyName = "COMCD";
                dgvcCOMCD.HeaderText = "COMCD";
                dgvcCOMCD.Width = 50;
                dgvcCOMCD.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcCOMCD);

                //WERKS
                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "WERKS";
                dgvcWERKS.Width = 80;
                dgvcWERKS.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcWERKS);

                //ORDERNO 车辆信息
                DataGridViewTextBoxColumn dgvcORDERNO = new DataGridViewTextBoxColumn();
                dgvcORDERNO.DataPropertyName = "ORDERNO";
                dgvcORDERNO.HeaderText = "CarNo";
                dgvcORDERNO.Width = 80;
                dgvcORDERNO.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcORDERNO);

                //VBELN   EC单号
                DataGridViewTextBoxColumn dgvcVBELN = new DataGridViewTextBoxColumn();
                dgvcVBELN.DataPropertyName = "VBELN";
                dgvcVBELN.HeaderText = "EC单号";
                dgvcVBELN.Width = 80;
                dgvcVBELN.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcVBELN);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.Width = 80;
                dgvcMBLNR.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcMBLNR);

                //CRDAT   创建时间
                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "CRDATE";
                dgvcCRDAT.Width = 100;
                dgvcCRDAT.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcCRDAT);

                //CRNAM   创建人
                DataGridViewTextBoxColumn dgvcCRNAM = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRNAM";
                dgvcCRNAM.HeaderText = "CRNAME";
                dgvcCRNAM.Width = 80;
                dgvcCRNAM.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcCRNAM);

                //ENDAT   离厂时间
                DataGridViewTextBoxColumn dgvcENDAT = new DataGridViewTextBoxColumn();
                dgvcENDAT.DataPropertyName = "ENDAT";
                dgvcENDAT.HeaderText = "ENDDATE";
                dgvcENDAT.Width = 100;
                dgvcENDAT.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcENDAT);

                //ENNAM  离厂操作人
                DataGridViewTextBoxColumn dgvcENNAM = new DataGridViewTextBoxColumn();
                dgvcENNAM.DataPropertyName = "ENNAM";
                dgvcENNAM.HeaderText = "ENDNAME";
                dgvcENNAM.Width = 80;
                dgvcENNAM.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcENNAM);

                //SAPDAT  SAP扣账时间
                DataGridViewTextBoxColumn dgvcSAPDAT = new DataGridViewTextBoxColumn();
                dgvcSAPDAT.DataPropertyName = "SAPDAT";
                dgvcSAPDAT.HeaderText = "SAPDAT";
                dgvcSAPDAT.Width = 100;
                dgvcSAPDAT.ReadOnly = true;
                enddgvEC.Columns.Add(dgvcSAPDAT);

                enddgvEC.DataSource = dtCarECSource;
                endOutSource.Text = dtCarECSource.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOrderNoDataGrid()");
            }

        }
        #endregion
        #region ScanEC
        private void txtEC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {

                foreach (DataRow item in dtECSource.Rows)
                {
                    if (item["VBELN"].ToString() == txtEC.Text.Trim())
                    {
                        MessageBox.Show("该EC单已被刷入，请确认！");
                        txtEC.Text = "";
                        return;
                    }
                }

                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);

                DataTable dtData = new DataTable();
                dtData = StorageIn.ScanEC(Mandt, Comcd, txtEC.Text.Trim(), cmbWerks.Text.ToString());

                

                if (dtData.Rows.Count > 0)
                {
                    MessageBox.Show("该EC单已经完成入库，不能重复作业！");
                    txtEC.Text = "";
                    return;
                }


                DataRow dr = dtECSource.NewRow();
                dr["ITEM"] = dtECSource.Rows.Count + 1;
                dr["VBELN"] = txtEC.Text.Trim();
                dtECSource.Rows.Add(dr);
                ShowStorageDataGrid();
                txtEC.Text = "";


            }

        }
        #endregion

        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //if (endtxtEC.Text.Trim() == "")
            //{
            //    MessageBox.Show("请输入EC单号!!");
            //    return;
            //}
            string StartTime;
            string EndTime;
            if (Time1.Text.Trim() == "")
            {
                StartTime = " 00:00:01";
            }
            else if (Time1.Text.Trim().Length == 4)
            {
                StartTime = " " + Time1.Text.Trim().Insert(2, ":") + ":01";
            }
            else
            {
                MessageBox.Show("开始时间格式不对，请确认!!");
                return;
            }

            if (Time2.Text.Trim() == "")
            {
                EndTime = " 23:59:59 ";
            }
            else if (Time2.Text.Trim().Length == 4)
            {
                EndTime=" "+Time2.Text.Trim().Insert(2,":")+":59";
            }
            else
            {
                MessageBox.Show("结束时间格式不对，请确认!!");
                return;
            }

            QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);

            dtCarECSource = StorageIn.ScanECOne(Mandt, Comcd, endtxtEC.Text.Trim(), cmbWerks.Text.ToString(), dtPO1.Value.ToString("yyyy-MM-dd"), dtPO2.Value.ToString("yyyy-MM-dd"),StartTime,EndTime);
            if (dtCarECSource.Rows.Count > 0)
            {
                ShowOrderNoDataGrid();
                stsWarning.Text = "";


            }
            else
            {
                stsWarning.Text = "No Data!!!";
                enddgvEC.DataSource = null;
            }
            endtxtEC.Text = "";

        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEC.Text = "";
            endtxtEC.Text = "";
            dtECSource.Clear();
            dtCarECSource.Clear();
            dgvEC.DataSource = null;
            enddgvEC.DataSource = null;
            lblOutSource.Text = "0" + " records";
            endOutSource.Text = "0" + " records";
            stsWarning.Text = "";
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        #region Save
        private void bntSave_Click(object sender, EventArgs e)
        {
            //dtECSource.Rows.Count > 0
            if (dtECSource.Rows.Count > 0)
            {
                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                string orderNo = StorageIn.CreateCarOrder(cmbWerks.Text.ToString());
                bool flg = StorageIn.BindCarOrderNo(dtECSource, Mandt, Comcd, Usrnm, cmbWerks.Text.ToString(), orderNo);
                if (flg)
                {
                    stsWarning.Text = "EC单绑定车辆信息成功！";
                    dtECSource.Clear();
                    lblOutSource.Text = "0" + " records";
                }
                else
                {
                    stsWarning.Text = "EC单绑定车辆信息失败！";
                   
                }

            }

        }

        #endregion

        #region 离厂刷EC单 //离厂刷同一车辆中的任一EC单带出所有EC单
        private void endtxtEC_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {

                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);

                dtCarECSource = StorageIn.ScanECOne(Mandt, Comcd, endtxtEC.Text.Trim(), cmbWerks.Text.ToString(), dtPO1.Value.ToString("yyyy-MM-dd"), dtPO2.Value.ToString("yyyy-MM-dd"),"","");
                if (dtCarECSource.Rows.Count > 0)
                {
                    ShowOrderNoDataGrid();
                    

                }
                else 
                {
                    stsWarning.Text = "No Data!!!";
                }
                endtxtEC.Text = "";

            }

        }
        #endregion

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dtCarECSource.Rows.Count > 0)
            {
                foreach (DataRow item in dtCarECSource.Rows)
                {
                    if (item["ENDAT"].ToString() != "" && item["ENDAT"].ToString() != "")
                    {
                        MessageBox.Show(string.Format("{0}已经出厂，不能重复作业！",item["VBELN"].ToString()));
                        return;
                    }
                }

                QCI.QWMS.StorageIn StorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                bool flg = StorageIn.UpdateEndDate(dtCarECSource, Usrnm);
                if (flg)
                {
                    stsWarning.Text = "离厂确认成功!!";
                    dtCarECSource.Clear();
                    endOutSource.Text = "0" + " records";

                }
                else
                {
                    stsWarning.Text = "离厂确认失败!!";
                }
                
            }
        }

        

    }
}
