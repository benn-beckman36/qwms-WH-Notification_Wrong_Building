using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI_QWMS_Models;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS.Models
{
    public partial class Model_TransferOrderReturn : Form
    {
        #region Parameters
        private UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strType = "";
        private string strLocat = "";
        private string inOrder = "";
        private string outOrder = "";
        private DataTable dtDataGrid = new DataTable();
        private DataTable dtDataGrid2 = new DataTable();
        private ModelsData objModelsData;

        #endregion

        public Model_TransferOrderReturn(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            objModelsData = new ModelsData(varUserData, Werks, Lgort, Progid);
            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);


                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
                return strLocat;
            }
            set
            {
                strLocat = value;
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
        #endregion

        #region 設定State Bar中的日期
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        //private void ShowDdlWerks()
        //{
        //    Authority objAuthority = new Authority(UserData);
        //    DataTable dtTemp = new DataTable();
        //    try
        //    {
        //        cmbWerks.Items.Clear();
        //        dtTemp = objAuthority.CheckPlantAuthority();
        //        for (int i = 0; i < dtTemp.Rows.Count; i++)
        //        {
        //            cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowDdlWerks()");
        //    }
        //}

        //private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ShowDdlLgort();
        //}

        //private void ShowDdlLgort()
        //{
        //    try
        //    {
        //        Authority objAuthority = new Authority(UserData);
        //        stsWarning.Text = "";
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
        //            strLgort = "";
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

        //显示DataGridView

        public void ShowDataSource()
        {
            this.dgvSource.AutoGenerateColumns = false;
            this.dgvSource.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "SELECTED";
                dgvcSelect.HeaderText = "选择";
                dgvcSelect.Width = 30;
                this.dgvSource.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcMANDT = new DataGridViewTextBoxColumn();
                dgvcMANDT.DataPropertyName = "IMANDT";
                dgvcMANDT.HeaderText = "Client";
                dgvcMANDT.Width = 50;
                dgvcMANDT.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcMANDT);

                DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                dgvcCOMCD.DataPropertyName = "ICOMCD";
                dgvcCOMCD.HeaderText = "Company Code";
                dgvcCOMCD.Width = 60;
                dgvcCOMCD.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcCOMCD);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "EWERKS";
                dgvcWERKS.HeaderText = "E_Plant";
                dgvcWERKS.Width = 50;
                dgvcWERKS.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "ELGORT";
                dgvcLGORT.HeaderText = "E_Storage";
                dgvcLGORT.Width = 50;
                dgvcLGORT.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcEMBLNR = new DataGridViewTextBoxColumn();
                dgvcEMBLNR.DataPropertyName = "EMBLNR";
                dgvcEMBLNR.HeaderText = "TransferOutOrder";
                dgvcEMBLNR.Width = 100;
                dgvcEMBLNR.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcEMBLNR);

                DataGridViewTextBoxColumn dgvcIWERKS = new DataGridViewTextBoxColumn();
                dgvcIWERKS.DataPropertyName = "IWERKS";
                dgvcIWERKS.HeaderText = "I_Plant";
                dgvcIWERKS.Width = 70;
                dgvcIWERKS.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcIWERKS);

                DataGridViewTextBoxColumn dgvcILGORT = new DataGridViewTextBoxColumn();
                dgvcILGORT.DataPropertyName = "ILGORT";
                dgvcILGORT.HeaderText = "I_Storage";
                dgvcILGORT.Width = 70;
                dgvcILGORT.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcILGORT);

                DataGridViewTextBoxColumn dgvcIMBLNR = new DataGridViewTextBoxColumn();
                dgvcIMBLNR.DataPropertyName = "IMBLNR";
                dgvcIMBLNR.HeaderText = "TransferInOrder";
                dgvcIMBLNR.Width = 100;
                dgvcIMBLNR.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcIMBLNR);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "ModelNo";
                dgvcMATNR.Width = 50;
                dgvcMATNR.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "ICRDAT";
                dgvcCRDAT.HeaderText = "CRDAT";
                dgvcCRDAT.Width = 100;
                dgvcCRDAT.ReadOnly = true;
                this.dgvSource.Columns.Add(dgvcCRDAT);

                dgvSource.DataSource = dtDataGrid;
                lblSource.Text = dtDataGrid.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcMANDT = new DataGridViewTextBoxColumn();
                dgvcMANDT.DataPropertyName = "MANDT";
                dgvcMANDT.HeaderText = "Client";
                dgvcMANDT.Width = 50;
                dgvcMANDT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMANDT);

                DataGridViewTextBoxColumn dgvcCOMCD = new DataGridViewTextBoxColumn();
                dgvcCOMCD.DataPropertyName = "COMCD";
                dgvcCOMCD.HeaderText = "Company Code";
                dgvcCOMCD.Width = 60;
                dgvcCOMCD.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCOMCD);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "E_Plant";
                dgvcWERKS.Width = 50;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "E_Storage";
                dgvcLGORT.Width = 50;
                dgvcLGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcEMBLNR = new DataGridViewTextBoxColumn();
                dgvcEMBLNR.DataPropertyName = "MBLNR";
                dgvcEMBLNR.HeaderText = "TransferOutOrder";
                dgvcEMBLNR.Width = 100;
                dgvcEMBLNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcEMBLNR);

                DataGridViewTextBoxColumn dgvcIWERKS = new DataGridViewTextBoxColumn();
                dgvcIWERKS.DataPropertyName = "DWERK";
                dgvcIWERKS.HeaderText = "I_Plant";
                dgvcIWERKS.Width = 50;
                dgvcIWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcIWERKS);

                DataGridViewTextBoxColumn dgvcILGORT = new DataGridViewTextBoxColumn();
                dgvcILGORT.DataPropertyName = "DLGOR";
                dgvcILGORT.HeaderText = "I_Storage";
                dgvcILGORT.Width = 50;
                dgvcILGORT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcILGORT);

                DataGridViewTextBoxColumn dgvcIMBLNR = new DataGridViewTextBoxColumn();
                dgvcIMBLNR.DataPropertyName = "DMBLN";
                dgvcIMBLNR.HeaderText = "TransferInOrder";
                dgvcIMBLNR.Width = 100;
                dgvcIMBLNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcIMBLNR);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "ModelNo";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcREMARK = new DataGridViewTextBoxColumn();
                dgvcREMARK.DataPropertyName = "REMARK";
                dgvcREMARK.HeaderText = "Remark";
                dgvcREMARK.Width = 80;
                dgvcREMARK.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcREMARK);

                DataGridViewTextBoxColumn dgvcCRNAM = new DataGridViewTextBoxColumn();
                dgvcCRNAM.DataPropertyName = "CRNAM";
                dgvcCRNAM.HeaderText = "CRNAM";
                dgvcCRNAM.Width = 50;
                dgvcCRNAM.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCRNAM);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "CRDAT";
                dgvcCRDAT.Width = 100;
                dgvcCRDAT.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCRDAT);

                dgvData.DataSource = dtDataGrid2;
                lblData.Text = dtDataGrid.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string strStartDate = "";
            string strEndDate = "";
            strStartDate = dtpStartTime.Value.ToString("yyyyMMdd");
            strEndDate = dtpEndTime.Value.ToString("yyyyMMdd");
            inOrder = txtInOrder.Text.ToString().Trim();
            outOrder = txtOutOrder.Text.ToString().Trim();
            ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
            //for (int i = 0; i < dgv.Rows.Count; i++)
            //{
            //    dgv.Rows[i]["Selected"] = false;
            //}
            if (!string.IsNullOrEmpty(inOrder) && string.IsNullOrEmpty(outOrder))
            {
                stsWarning.Text = "inOrder and outOrder not matching!!";
                MessageBox.Show("inOrder and outOrder not matching!!");
                return;
            }
            if (string.IsNullOrEmpty(inOrder) && !string.IsNullOrEmpty(outOrder))
            {
                stsWarning.Text = "inOrder and outOrder not matching!!";
                MessageBox.Show("inOrder and outOrder not matching!!");
                return;
            }
            if (!string.IsNullOrEmpty(inOrder) && !string.IsNullOrEmpty(outOrder))
            {
                if (inOrder.Substring(2) != outOrder.Substring(2))
                {
                    stsWarning.Text = "inOrder and outOrder not matching!!";
                    MessageBox.Show("inOrder and outOrder not matching!!");
                    return;
                }

                dtDataGrid = objModelsData.QueryTransferOrderReturnAble(strStartDate, strEndDate, inOrder.Substring(0, 16), outOrder.Substring(0, 16));
                ShowDataSource();
            }
            if (string.IsNullOrEmpty(inOrder) && string.IsNullOrEmpty(outOrder))
            {
                dtDataGrid = objModelsData.QueryTransferOrderReturnAble(strStartDate, strEndDate, "", "");
                ShowDataSource();
            }
            if (dtDataGrid.Rows.Count > 0)
            {
                this.btnReturn.Enabled = true;
                this.txtRemark.Enabled = true;
            }
            else
            {
                this.stsWarning.Text = "No Data!";
                return;
            }
        }

        //private string GetSelectedOrders()//获取选中的ModelNO
        //{
        //    string strOrders = string.Empty;
        //    DataTable dgv = (DataTable)dgvData.DataSource;
        //    for (int i = 0; i < dgv.Rows.Count; i++)
        //    {
        //        if (dgv.Rows[i]["Selected"].ToString() == "True")
        //        {
        //            strOrders += dgv.Rows[i]["IMBLNR"].ToString().Substring(2,14) + "','";
        //        }
        //    }
        //    if (strOrders.Length > 0)
        //    {
        //        strOrders = strOrders.Substring(0, strOrders.Length - 3);
        //    }
        //    return strOrders;
        //}

        private DataTable GetSelectedOrders()//获取选中的ModelNO
        {
            string strOrders = string.Empty;
            DataTable dgv = (DataTable)dgvSource.DataSource;
            DataTable dtData = new DataTable();
            dtData = dgv.Clone();

            foreach (DataRow dr in dgv.Rows)
            {
                if (dr["SELECTED"].ToString() == "True")
                {
                    dtData.ImportRow(dr);
                }
            }
            return dtData;
        }

        #region delete
        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
        //    string strOrders = GetSelectedOrders();
        //    ArrayList arr = GetSingleModelNo();
        //    DataTable dtCheck = new DataTable();
        //    dtCheck = objModelsData.CheckTransferOrder(strOrders);
        //    if (arr.Count < 1)
        //    {
        //        MessageBox.Show("请勾选需要修改的数据！");
        //        return;
        //    }
        //    if (!string.IsNullOrEmpty(strOrders))
        //    {
        //        if (MessageBox.Show("确定所选调拨单是否正确？一经删除无法恢复！", "Delete Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
        //        {
        //            //检查单号是否可修改或删除
        //            for (int i = 0; i < dtCheck.Rows.Count; i++)
        //            {
        //                if (Convert.ToInt32(dtCheck.Rows[i]["OTQTY"]) == 1)
        //                {
        //                    MessageBox.Show("该单已进行调拨不允许删除！");
        //                    return;
        //                }
        //            }

        //            if (objModelsData.ReturnTransferOrder(strOrders))
        //            {
        //                MessageBox.Show("删除成功！");
        //                this.ShowDataSource();
        //            }
        //        }
        //    }
        //}
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            this.dgvData.DataSource = null;
            this.dgvSource.DataSource = null;
            this.dtDataGrid.Clear();
            this.dtDataGrid2.Clear();
            txtInOrder.Text = "";
            txtOutOrder.Text = "";
            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
            this.btnQuery.Enabled = true;
            this.btnReturn.Enabled = false;
            this.txtRemark.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Print
        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
        //        DataTable dtCheck = new DataTable();
        //        DataTable dgv = (DataTable)dgvData.DataSource;
        //        string strOrders = GetSelectedOrders();
        //        dtCheck = objModelsData.CheckTransferOrder(strOrders);
        //        if (string.IsNullOrEmpty(strOrders))
        //        {
        //            MessageBox.Show("请先选择打印调拨单！");
        //            return;
        //        }

        //        if (dtCheck.Rows.Count > 2)
        //        {
        //            MessageBox.Show("一次只能选择一条进行打印！");
        //            return;
        //        }

        //        for (int i = 0; i < dgv.Rows.Count; i++)
        //        {
        //            if (dgv.Rows[i]["Selected"].ToString() == "True")
        //            {
        //                strWerks = dgv.Rows[i]["EWERKS"].ToString();
        //                strLgort = dgv.Rows[i]["ELGORT"].ToString();
        //                strOrders = dgv.Rows[i]["EMBLNR"].ToString().Substring(0, 16);
        //            }
        //        }

        //        DataTable dtPrint = new DataTable();
        //        dtPrint = objModelsData.GetPrintModelData(strWerks, strLgort, strOrders, "", "", "");
        //        if (dtPrint.Rows.Count > 0)
        //        {
        //            ReportPrint objReportPrint = new ReportPrint(UserData, "MODEL", dtPrint);
        //            objReportPrint.MdiParent = this.ParentForm;
        //            objReportPrint.Show();
        //        }
        //        else
        //        {
        //            throw new Exception("打印无数据！");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
        //    }
        //}
        #endregion

        private ArrayList GetSingleModelNo()//获取单个模具号
        {
            ArrayList arry = new ArrayList();
            DataTable dgv = (DataTable)dgvSource.DataSource;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i]["SELECTED"].ToString() == "True")
                {
                    arry.Add(dgv.Rows[i]["MATNR"].ToString());
                }
            }
            return arry;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
            DataTable dtData = GetSelectedOrders();
            ArrayList arr = GetSingleModelNo();
            string strRemark = this.txtRemark.Text.ToString();

            if (arr.Count < 1)
            {
                MessageBox.Show("请勾选需要回冲的数据！");
                return;
            }
            if (dtData.Rows.Count>0)
            {
                if (MessageBox.Show("确定所选调拨单是否正确？一经删除无法恢复！", "Delete Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (objModelsData.ReturnTransferOrder(dtData, strRemark))
                    {
                        MessageBox.Show("调拨单已回冲成功！");
                        this.ShowDataSource();
                        this.btnQuery.Enabled = true;
                    }
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strStartDate = "";
            string strEndDate = "";
            strStartDate = dtpStartTime.Value.ToString("yyyyMMdd");
            strEndDate = dtpEndTime.Value.ToString("yyyyMMdd");
            inOrder = txtInOrder.Text.ToString().Trim();
            outOrder = txtOutOrder.Text.ToString().Trim();
            ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, strProgid);
            //for (int i = 0; i < dgv.Rows.Count; i++)
            //{
            //    dgv.Rows[i]["Selected"] = false;
            //}
            if (!string.IsNullOrEmpty(inOrder) && string.IsNullOrEmpty(outOrder))
            {
                stsWarning.Text = "inOrder and outOrder not matching!!";
                MessageBox.Show("inOrder and outOrder not matching!!");
                return;
            }
            if (string.IsNullOrEmpty(inOrder) && !string.IsNullOrEmpty(outOrder))
            {
                stsWarning.Text = "inOrder and outOrder not matching!!";
                MessageBox.Show("inOrder and outOrder not matching!!");
                return;
            }
            if (!string.IsNullOrEmpty(inOrder) && !string.IsNullOrEmpty(outOrder))
            {
                if (inOrder.Substring(2) != outOrder.Substring(2))
                {
                    stsWarning.Text = "inOrder and outOrder not matching!!";
                    MessageBox.Show("inOrder and outOrder not matching!!");
                    return;
                }

                dtDataGrid2 = objModelsData.QueryReturnTransferOrder(strStartDate, strEndDate, inOrder.Substring(0, 16), outOrder.Substring(0, 16));
                ShowDataGrid();
            }
            if (string.IsNullOrEmpty(inOrder) && string.IsNullOrEmpty(outOrder))
            {
                dtDataGrid2 = objModelsData.QueryReturnTransferOrder(strStartDate, strEndDate, "", "");
                ShowDataGrid();
            }
            if (dtDataGrid2.Rows.Count > 0)
            {
                this.btnReturn.Enabled = true;
                this.txtRemark.Enabled = true;
            }
            else
            {
                this.stsWarning.Text = "No Data!";
                return;
            }
        }
    }
}
