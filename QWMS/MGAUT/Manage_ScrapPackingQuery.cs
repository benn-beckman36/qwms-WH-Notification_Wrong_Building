using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;
using QWMS.Common;
using QCI.QWMS;
using QWMS.PP;
using QWMS.MM;

namespace QWMS
{
    public partial class Manage_ScrapPackingQuery : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strPN = "";
        private string strCharg = "";
        private string strPacking = "";

        private DataTable dtData;
        private DataTable dtData1;
        private DataTable SapData;
        private StorageIn objStorageIn;
        private PlantData objPlantData;
        private Authority objAuthority;

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
        public string PN
        {
            get
            {
                return strPN;
            }
            set
            {
                strPN = value;
            }
        }
        public string Charg
        {
            get
            {
                return strCharg;
            }
            set
            {
                strCharg = value;
            }
        }
        public string Packing
        {
            get
            {
                return strPacking;
            }
            set
            {
                strPacking = value;
            }
        }

        public Manage_ScrapPackingQuery(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Comcd = UserData.CompanyCode;

            try
            {
                objStorageIn = new StorageIn(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出status的资料
                    ShowStatusData();
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        # region ShowStatusData
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsComcd.Text = Comcd;
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
        }
        # endregion

        # region ShowDdlWerks
        private void ShowDdlWerks()
        {
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
        # endregion

        # region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
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
        # endregion

        #region Plant SelectChange
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        # region Storage SelectedChange
        private void cmbLgort_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
        }
        # endregion

        #region Pn TextChange
        private void txtPn_TextChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
        }
        #endregion

        #region ShowDataGrid
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "厂区";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 70;
                dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "仓别";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 70;
                dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Packing料号";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "版本";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 70;
                dgvData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcPacking = new DataGridViewTextBoxColumn();
                dgvcPacking.DataPropertyName = "PACKINGQTY";
                dgvcPacking.HeaderText = "Packing数量";
                dgvcPacking.ReadOnly = true;
                dgvcPacking.Width = 100;
                dgvData.Columns.Add(dgvcPacking);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "状态";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 60;
                dgvData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "储位";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 70;
                dgvData.Columns.Add(dgvcLocat);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "库存数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 90;
                dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "入库日期";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 90;
                dgvData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcDecitem = new DataGridViewTextBoxColumn();
                dgvcDecitem.DataPropertyName = "DCITEM";
                dgvcDecitem.HeaderText = "底账序号";
                dgvcDecitem.ReadOnly = true;
                dgvcDecitem.Width = 90;
                dgvData.Columns.Add(dgvcDecitem);

                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");

            }
        }
        # endregion

        #region 发送PackingNo到Sap，返回详细废品信息
        public DataTable SendToSAP(string strPN)
        {
            try
            {
                DataSet dsResult = new DataSet();
                DataTable dtResult = new DataTable();
                //PP_Service obj = new PP_Service();
                MM.MM_Service obj = new QWMS.MM.MM_Service();
                dsResult = obj.Z_MM_RFC_PACKING_TO_QWMS(strPN);
                dtResult = dsResult.Tables[0];
                return dtResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            dtData = new DataTable();
            dtData1 = new DataTable();
            //string strWerks = "";
            //string strLgort = "";
            string strPN = "";
            string strMatnr = "";
            string strCharg = "";
            string strPacking = "";

            if ((cmbWerks.SelectedIndex != -1)&&(cmbLgort.SelectedIndex != -1)&&(txtPn.Text != null))
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                strPN = txtPn.Text.ToString();
            }
            else
            {
                stsWarning.Text = "No Werks or Lgort or Packing No";
                return;
            }
  
            try
            {
                StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                //判断QWMS数据库中是否有数据，有的话就先删除，因为SAP数据是变化的，每次查询的数据都需要重新同步
                bool flg = objStorageData.QueryWhticData(strPN);
                if (!flg)
                {
                    stsWarning.Text = "清除数据失败!!";
                    return;
                }

                //strPN号调用SAP接口获取DT
                DataTable dtSap = SendToSAP(strPN);
                SapData = dtSap.Copy();
                //获取料号，版本，Packing数量
                //DataTable dtMatnr = new DataTable();

                //dtMatnr.Columns.Add("PCKNO");
                //dtMatnr.Columns.Add("MATNR");
                //dtMatnr.Columns.Add("CHARG");
                //dtMatnr.Columns.Add("MENGE");
                //dtMatnr.Rows.Add("1","32A65UB0000", "A1AF1", "12");
                //dtMatnr.Rows.Add("1","3BFFKSB0000", "B3AF1", "12");


                //for (int i = 0; i < dtMatnr.Rows.Count; i++)
                foreach(DataRow row in dtSap.Rows)
                //foreach(DataRow row in dtMatnr.Rows)
                {
                    stsWarning.Text = "";
                    
                    strPN = row["PCKNO"].ToString().Trim();
                    strMatnr = row["MATNR"].ToString().Trim();
                    strCharg = row["CHARG"].ToString().Trim();
                    strPacking = row["MENGE"].ToString().Trim();

                    
                    dtData1 = objStorageData.QueryScrapPackingData(strWerks, strLgort, strMatnr, strPN, strCharg, strPacking);
                    dtData.Merge(dtData1);

                }
                if (dtData.Rows.Count == 0)
                {
                    ShowDataGrid();
                    stsWarning.Text = "No Data!!";
                    return;
                }
                else
                {
                    ShowDataGrid();
                    stsWarning.Text = "";
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            ReportPrint objReportPrint = new ReportPrint(UserData, "SCRAPPACKING", dtData);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SapData.Rows.Count > 0)
                {
                    if (!SapData.Columns.Contains("ZEILE"))
                    {
                        //SapData.Columns.Add("MANDT");
                        SapData.Columns.Add("MTYPE");
                        SapData.Columns.Add("ZEILE");
                        SapData.Columns.Add("USNAM");
                        SapData.Columns.Add("BUDAT");
                        SapData.Columns.Add("CRDAT");
                        SapData.Columns.Add("MODAT");
                        SapData.Columns.Add("COMCD");
                        SapData.Columns.Add("BOXID");
                        SapData.Columns.Add("LIFNR");
                    }

                    for (int i = 0; i < SapData.Rows.Count; i++)
                    {
                        string strZEILE = (i + 1).ToString().PadLeft(4, '0');
                        string time = DateTime.Now.ToString();
                        SapData.Rows[i]["MANDT"] = "218";
                        SapData.Rows[i]["MTYPE"] = "SAP_PACK";
                        SapData.Rows[i]["ZEILE"] = strZEILE;
                        if (dtData.Select("MATNR='" + SapData.Rows[i]["MATNR"].ToString().Trim() + "' AND CHARG='" + SapData.Rows[i]["CHARG"].ToString().Trim() + "'").Length > 0)
                        {
                            SapData.Rows[i]["LGORT"] = strLgort;
                        }
                        SapData.Rows[i]["USNAM"] = Usrnm;
                        SapData.Rows[i]["BUDAT"] = DateTime.Today.ToString("yyyyMMdd");
                        SapData.Rows[i]["CRDAT"] = time;
                        SapData.Rows[i]["MODAT"] = time;
                        SapData.Rows[i]["COMCD"] = Comcd;
                        SapData.Rows[i]["BOXID"] = SapData.Rows[i]["PCKNO"].ToString() + strZEILE;
                        SapData.Rows[i]["MBLNR"] = SapData.Rows[i]["PCKNO"].ToString();
                        SapData.Rows[i]["LIFNR"] = "";
                    }
                    StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
                    bool flg = objStorageData.BulkCopyWhtic(SapData);
                    if (flg)
                    {
                        stsWarning.Text = "保存成功";
                    }
                    else
                    {
                        stsWarning.Text = "保存失败";
                    }

                }
            }

            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.gbOption.Enabled = true;
            this.txtPn.Text = string.Empty;
            this.dgvData.DataSource = null;
            this.dtData.Rows.Clear();
            this.lblCount.Text = "";     
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 调整布局大小
        private void Manage_StorageStatus_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion



    }
}
