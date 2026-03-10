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
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QWMS.Entity;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace QWMS
{
    public partial class Manage_TransferProgress_Query : Form
    {
        #region  初始化
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        //private string strWerks = string.Empty;
        //private string strDWerks = string.Empty;
        private string strDate = "";
        private string strDateEnd = "";
        private string strProgid = "";
        private DataTable dtData = new DataTable();
        private DataTable dtPoHead = new DataTable();
        private DataTable dtPoItem = new DataTable();
        private DataTable dtPoItems = new DataTable();
        DataTable dtTransferCar = new DataTable();
        DataTable dtApplynoSchedule = new DataTable();
        //private SQLAccess objDB;
        private Authority objAuthority;
        private StorageData objStorageData;

        #endregion

        #region  Get/Set
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

        //public string Werks
        //{
        //    get
        //    {
        //        return strWerks;
        //    }
        //    set
        //    {
        //        strWerks = value;
        //    }
        //}
        //public string DWerks
        //{
        //    get
        //    {
        //        return strDWerks;
        //    }
        //    set
        //    {
        //        strDWerks = value;
        //    }
        //}
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

        #endregion

        #region Main
        public Manage_TransferProgress_Query(UserInfo varUserData, string strProgid)
        {
            try
            {
                InitializeComponent();
                UserData = varUserData;
                Mandt = UserData.Client;
                Comcd = UserData.CompanyCode;
                Usrnm = UserData.UserId;
                Progid = strProgid;
                strDate = DateTime.Now.ToString("yyyy-MM-dd");
                objAuthority = new Authority(UserData);
                objStorageData = new StorageData(UserData);
                //秀出Status的資料
                ShowStatusData();
                //ShowDdlWerks();
                //if (cmbWerks.Items.Count > 0)
                //{
                //    this.cmbWerks.SelectedIndex = 0;
                //}
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
            this.stsMandt.Text = UserData.Client;
            this.stsComcd.Text = UserData.CompanyCode;
            this.stsUsrnm.Text = UserData.UserId;
        }
        #endregion

        #region ShowDdlWerks
        //private void ShowDdlWerks()
        //{
        //    DataTable dtTemp = new DataTable();
        //    try
        //    {
        //        cmbWerks.Items.Clear();
        //        dtTemp = objAuthority.CheckPlantAuthority();
        //        for (int i = 0; i < dtTemp.Rows.Count; i++)
        //        {
        //            cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
        //            cmbDWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-ShowDdlWerks()");
        //    }
        //}
        #endregion

        #region  清除任何使用中的資源
        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region 点击查询PODWNHEAD
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                strDate = DateTime.Parse(dtpondat.Text).ToString("d");
                strDate = strDate.Replace("/", "-");
                strDateEnd = DateTime.Parse(dtpondatend.Text).ToString("d");
                strDateEnd = strDateEnd.Replace("/", "-");
                dtPoHead = objStorageData.QureyPodwnHead(txt46PO.Text.Trim(), strDate, strDateEnd);
                if (dtPoHead.Rows.Count == 0)
                {
                    MessageBox.Show("无PO信息！");
                    return;
                }
                if (!dtPoHead.Columns.Contains("SELECT"))
                    dtPoHead.Columns.Add("SELECT", typeof(bool));
                for (int i = 0; i < dtPoHead.Rows.Count; i++)
                {
                    dtPoHead.Rows[i]["SELECT"] = false;
                }
                ShowdtPoHeadGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-btnQuery_Click");
            }
        }
        #endregion

        #region 调出厂区
        //private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        stsWarning.Text = "";
        //        if (cmbWerks.SelectedIndex != -1)
        //        {
        //            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
        //        }
        //        else
        //        {
        //            strWerks = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-btnQuery_Click");
        //    }
        //}
        #endregion

        #region 调入厂区
        //private void cmbDWerks_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        stsWarning.Text = "";
        //        if (cmbWerks.SelectedIndex != -1)
        //        {
        //            strDWerks = cmbDWerks.Items[cmbDWerks.SelectedIndex].ToString();
        //        }
        //        else
        //        {
        //            strDWerks = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message + "<-btnQuery_Click");
        //    }
        //}
        #endregion

        #region ShowdtPoHeadGrid
        public void ShowdtPoHeadGrid()
        {
            this.gvData_PoHead.AutoGenerateColumns = false;
            this.gvData_PoHead.Columns.Clear();
            try
            {
                DatagridViewCheckBoxHeaderCell chkcell = new DatagridViewCheckBoxHeaderCell();
                chkcell.OnCheckBoxClicked += new CheckBoxClickedHandler(chkcell_OnCheckBoxClicked);
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderCell = chkcell;
                chk.DataPropertyName = "Select";
                chk.HeaderText = "";//Multilanguage.Instance.ResourceManager.GetString("TransactionID");
                chk.Name = "chk";
                chk.Frozen = true;
                chk.Width = 40;
                this.gvData_PoHead.Columns.Add(chk);
                this.gvData_PoHead.MultiSelect = false;

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "虚拟调拨单.";
                dgvcMblnr.Name = "MBLNR";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 100;
                gvData_PoHead.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "46PO.";
                dgvcEbeln.Name = "EBELN";
                dgvcEbeln.ReadOnly = true;
                dgvcEbeln.Width = 90;
                gvData_PoHead.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcMblnr351 = new DataGridViewTextBoxColumn();
                dgvcMblnr351.DataPropertyName = "MBLNR351";
                dgvcMblnr351.HeaderText = "351调拨单.";
                dgvcMblnr351.Name = "MBLNR351";
                dgvcMblnr351.ReadOnly = true;
                dgvcMblnr351.Width = 110;
                gvData_PoHead.Columns.Add(dgvcMblnr351);

                DataGridViewTextBoxColumn dgvcMblnr101 = new DataGridViewTextBoxColumn();
                dgvcMblnr101.DataPropertyName = "MBLNR101";
                dgvcMblnr101.HeaderText = "101调拨单.";
                dgvcMblnr101.Name = "MBLNR101";
                dgvcMblnr101.ReadOnly = true;
                dgvcMblnr101.Width = 110;
                gvData_PoHead.Columns.Add(dgvcMblnr101);

                gvData_PoHead.DataSource = dtPoHead;
                gvData_PoHead.FirstDisplayedScrollingRowIndex = gvData_PoHead.Rows.Count - 1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowdtPoHeadGrid()");
            }
        }
        #endregion

        #region ShowdtPodwnItemGrid
        public void ShowdtPodwnItemGrid()
        {
            this.dvg_PoItem.AutoGenerateColumns = false;
            this.dvg_PoItem.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcId = new DataGridViewTextBoxColumn();
                dgvcId.DataPropertyName = "ID";
                dgvcId.HeaderText = "序号.";
                dgvcId.Name = "ID";
                dgvcId.ReadOnly = true;
                dgvcId.Width = 20;
                dvg_PoItem.Columns.Add(dgvcId);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "虚拟调拨单.";
                dgvcMblnr.Name = "MBLNR";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 100;
                dvg_PoItem.Columns.Add(dgvcMblnr);

                //DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                //dgvcEbeln.DataPropertyName = "EBELN";
                //dgvcEbeln.HeaderText = "46PO.";
                //dgvcEbeln.Name = "EBELN";
                //dgvcEbeln.ReadOnly = true;
                //dgvcEbeln.Width = 90;
                //dvg_PoItem.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "调出厂区.";
                dgvcWerks.Name = "WERKS";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 60;
                dvg_PoItem.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "调出仓别.";
                dgvcLgort.Name = "LGORT";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 60;
                dvg_PoItem.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号.";
                dgvcMatnr.Name = "MATNR";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 120;
                dvg_PoItem.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "版本.";
                dgvcCharg.Name = "CHARG";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 60;
                dvg_PoItem.Columns.Add(dgvcCharg);


                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "MENGE.";
                dgvcMenge.Name = "MENGE";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 60;
                dvg_PoItem.Columns.Add(dgvcMenge);

                //DataGridViewTextBoxColumn dgvcOtqty = new DataGridViewTextBoxColumn();
                //dgvcOtqty.DataPropertyName = "OTQTY";
                //dgvcOtqty.HeaderText = "OTQTY.";
                //dgvcOtqty.Name = "OTQTY";
                //dgvcOtqty.ReadOnly = true;
                //dgvcOtqty.Width = 60;
                //dvg_PoItem.Columns.Add(dgvcOtqty);

                DataGridViewTextBoxColumn dgvcDwerks = new DataGridViewTextBoxColumn();
                dgvcDwerks.DataPropertyName = "DWERKS";
                dgvcDwerks.HeaderText = "调入厂区.";
                dgvcDwerks.Name = "DWERKS";
                dgvcDwerks.ReadOnly = true;
                dgvcDwerks.Width = 60;
                dvg_PoItem.Columns.Add(dgvcDwerks);

                //DataGridViewTextBoxColumn dgvcDlgort = new DataGridViewTextBoxColumn();
                //dgvcDlgort.DataPropertyName = "DLGORT";
                //dgvcDlgort.HeaderText = "调入仓别.";
                //dgvcDlgort.Name = "DLGORT";
                //dgvcDlgort.ReadOnly = true;
                //dgvcDlgort.Width = 60;
                //dvg_PoItem.Columns.Add(dgvcDlgort);

                //DataGridViewTextBoxColumn dgvcCrdat = new DataGridViewTextBoxColumn();
                //dgvcCrdat.DataPropertyName = "CRDAT";
                //dgvcCrdat.HeaderText = "351扣账时间.";
                //dgvcCrdat.Name = "CRDAT";
                //dgvcCrdat.ReadOnly = true;
                //dgvcCrdat.Width = 150;
                //dvg_PoItem.Columns.Add(dgvcCrdat);

                //DataGridViewTextBoxColumn dgvcMblnr351 = new DataGridViewTextBoxColumn();
                //dgvcMblnr351.DataPropertyName = "MBLNR351";
                //dgvcMblnr351.HeaderText = "351调拨单.";
                //dgvcMblnr351.Name = "MBLNR351";
                //dgvcMblnr351.ReadOnly = true;
                //dgvcMblnr351.Width = 100;
                //dvg_PoItem.Columns.Add(dgvcMblnr351);

                DataGridViewTextBoxColumn dgvcOutmatnr = new DataGridViewTextBoxColumn();
                dgvcOutmatnr.DataPropertyName = "TRANSFEROUT";
                dgvcOutmatnr.HeaderText = "351调拨状态.";
                dgvcOutmatnr.Name = "OUTMATNR";
                dgvcOutmatnr.ReadOnly = true;
                dgvcOutmatnr.Width = 90;
                dvg_PoItem.Columns.Add(dgvcOutmatnr);

                //DataGridViewTextBoxColumn dgvc101Crdat = new DataGridViewTextBoxColumn();
                //dgvc101Crdat.DataPropertyName = "INCRDAT";
                //dgvc101Crdat.HeaderText = "101扣账时间.";
                //dgvc101Crdat.Name = "INCRDAT";
                //dgvc101Crdat.ReadOnly = true;
                //dgvc101Crdat.Width = 150;
                //dvg_PoItem.Columns.Add(dgvc101Crdat);

                DataGridViewTextBoxColumn dgvcInmatnr = new DataGridViewTextBoxColumn();
                dgvcInmatnr.DataPropertyName = "TRANSFERIN";
                dgvcInmatnr.HeaderText = "101调拨状态.";
                dgvcInmatnr.Name = "INMATNR";
                dgvcInmatnr.ReadOnly = true;
                dgvcInmatnr.Width = 90;
                dvg_PoItem.Columns.Add(dgvcInmatnr);

                dvg_PoItem.DataSource = dtPoItems;
                // dvg_PoItem.FirstDisplayedScrollingRowIndex = dvg_PoItem.Rows.Count - 1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowdtPodwnItemGrid()");
            }
        }
        #endregion

        #region ShowdtdtTransferCarGrid
        public void ShowdtdtTransferCarGrid()
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcId = new DataGridViewTextBoxColumn();
                dgvcId.DataPropertyName = "ID";
                dgvcId.HeaderText = "序号.";
                dgvcId.Name = "ID";
                dgvcId.ReadOnly = true;
                dgvcId.Width = 30;
                dataGridView1.Columns.Add(dgvcId);

                //DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                //dgvcMblnr.DataPropertyName = "MBLNR";
                //dgvcMblnr.HeaderText = "351调拨单.";
                //dgvcMblnr.Name = "MBLNR";
                //dgvcMblnr.ReadOnly = true;
                //dgvcMblnr.Width = 120;
                //dataGridView1.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcApplyno = new DataGridViewTextBoxColumn();
                dgvcApplyno.DataPropertyName = "APPLYNO";
                dgvcApplyno.HeaderText = "调拨车单号.";
                dgvcApplyno.Name = "APPLYNO";
                dgvcApplyno.ReadOnly = true;
                dgvcApplyno.Width = 120;
                dataGridView1.Columns.Add(dgvcApplyno);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "调出厂区.";
                dgvcWerks.Name = "WERKS";
                dgvcWerks.ReadOnly = true;
                dgvcWerks.Width = 60;
                dataGridView1.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "调出仓别.";
                dgvcLgort.Name = "LGORT";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 60;
                dataGridView1.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcManr = new DataGridViewTextBoxColumn();
                dgvcManr.DataPropertyName = "MATNR";
                dgvcManr.HeaderText = "料号.";
                dgvcManr.Name = "MATNR";
                dgvcManr.ReadOnly = true;
                dgvcManr.Width = 120;
                dataGridView1.Columns.Add(dgvcManr);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "版本.";
                dgvcCharg.Name = "CHARG";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 60;
                dataGridView1.Columns.Add(dgvcCharg);

                //DataGridViewTextBoxColumn dgvcKdmat = new DataGridViewTextBoxColumn();
                //dgvcKdmat.DataPropertyName = "KDMAT";
                //dgvcKdmat.HeaderText = "KDMAT.";
                //dgvcKdmat.Name = "KDMAT";
                //dgvcKdmat.ReadOnly = true;
                //dgvcKdmat.Width = 120;
                //dataGridView1.Columns.Add(dgvcKdmat);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "MENGE.";
                dgvcMenge.Name = "MENGE";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 60;
                dataGridView1.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcDwerks = new DataGridViewTextBoxColumn();
                dgvcDwerks.DataPropertyName = "DWERKS";
                dgvcDwerks.HeaderText = "调入厂区.";
                dgvcDwerks.Name = "DWERKS";
                dgvcDwerks.ReadOnly = true;
                dgvcDwerks.Width = 60;
                dataGridView1.Columns.Add(dgvcDwerks);

                DataGridViewTextBoxColumn dgvcDlgort = new DataGridViewTextBoxColumn();
                dgvcDlgort.DataPropertyName = "DLGORT";
                dgvcDlgort.HeaderText = "调入仓别.";
                dgvcDlgort.Name = "DLGORT";
                dgvcDlgort.ReadOnly = true;
                dgvcDlgort.Width = 60;
                dataGridView1.Columns.Add(dgvcDlgort);

                DataGridViewTextBoxColumn dgvcTrtype = new DataGridViewTextBoxColumn();
                dgvcTrtype.DataPropertyName = "TRTYPE";
                dgvcTrtype.HeaderText = "调拨类型.";
                dgvcTrtype.Name = "TRTYPE";
                dgvcTrtype.ReadOnly = true;
                dgvcTrtype.Width = 60;
                dataGridView1.Columns.Add(dgvcTrtype);

                DataGridViewTextBoxColumn dgvcApplynm = new DataGridViewTextBoxColumn();
                dgvcApplynm.DataPropertyName = "APPLYNM";
                dgvcApplynm.HeaderText = "申请人.";
                dgvcApplynm.Name = "APPLYNM";
                dgvcApplynm.ReadOnly = true;
                dgvcApplynm.Width = 60;
                dataGridView1.Columns.Add(dgvcApplynm);

                //DataGridViewTextBoxColumn dgvcAconut = new DataGridViewTextBoxColumn();
                //dgvcAconut.DataPropertyName = "ACCOUNT";
                //dgvcAconut.HeaderText = "工号.";
                //dgvcAconut.Name = "ACCOUNT";
                //dgvcAconut.ReadOnly = true;
                //dgvcAconut.Width = 60;
                //dataGridView1.Columns.Add(dgvcAconut);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "部门代码.";
                dgvcKostl.Name = "KOSTL";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width = 60;
                dataGridView1.Columns.Add(dgvcKostl);
         

                dataGridView1.DataSource = dtTransferCar;
                // dvg_PoItem.FirstDisplayedScrollingRowIndex = dvg_PoItem.Rows.Count - 1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowdtPodwnItemGrid()");
            }

        }
        

        #endregion

        #region ShowdtApplynoScheduleCarGrid
        public void ShowdtApplynoScheduleCarGrid()
        {
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcId = new DataGridViewTextBoxColumn();
                dgvcId.DataPropertyName = "ID";
                dgvcId.HeaderText = "序号.";
                dgvcId.Name = "ID";
                dgvcId.ReadOnly = true;
                dgvcId.Width = 30;
                dataGridView3.Columns.Add(dgvcId);

                DataGridViewTextBoxColumn dgvcTransferNo = new DataGridViewTextBoxColumn();
                dgvcTransferNo.DataPropertyName = "TransferNo";
                dgvcTransferNo.HeaderText = "调拨车单号.";
                dgvcTransferNo.Name = "TransferNo";
                dgvcTransferNo.ReadOnly = true;
                dgvcTransferNo.Width = 120;
                dataGridView3.Columns.Add(dgvcTransferNo);

                DataGridViewTextBoxColumn dgvcDispatchNo = new DataGridViewTextBoxColumn();
                dgvcDispatchNo.DataPropertyName = "DispatchNo";
                dgvcDispatchNo.HeaderText = "派车单号.";
                dgvcDispatchNo.Name = "DispatchNo";
                dgvcDispatchNo.ReadOnly = true;
                dgvcDispatchNo.Width = 120;
                dataGridView3.Columns.Add(dgvcDispatchNo);

                DataGridViewTextBoxColumn dgvcStatus = new DataGridViewTextBoxColumn();
                dgvcStatus.DataPropertyName = "Status";
                dgvcStatus.HeaderText = "状态.";
                dgvcStatus.Name = "Status";
                dgvcStatus.ReadOnly = true;
                dgvcStatus.Width = 120;
                dataGridView3.Columns.Add(dgvcStatus);

                dataGridView3.DataSource = dtApplynoSchedule;
                // dvg_PoItem.FirstDisplayedScrollingRowIndex = dvg_PoItem.Rows.Count - 1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowdtApplynoScheduleCarGrid()");
            }
        }
             #endregion


        #region 加一个checkbox控件跟datagridview组合来实现全选反选功能
        #region
        private void chkcell_OnCheckBoxClicked(bool isChecked)
        {
            if (isChecked == true)
            {
                gvData_PoHead.EndEdit();
                for (int i = 0; i < gvData_PoHead.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = gvData_PoHead.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = true;
                    //dgvRequests.Rows[i].Cells[0].Value = 1;
                }
            }
            else
            {
                gvData_PoHead.EndEdit();
                for (int i = 0; i < gvData_PoHead.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = gvData_PoHead.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = false;
                    //dgvRequests.Rows[i].Cells[0].Value = 0;
                }
            }
        }
        #endregion
        #region 重绘全选表头
        //重绘表头
        public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
        {
            Point checkBoxLocation;
            Size checkBoxSize;
            bool _checked = false;
            Point _cellLocation = new Point();
            System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
            public event CheckBoxClickedHandler OnCheckBoxClicked;

            public DatagridViewCheckBoxHeaderCell()
            {
            }

            protected override void Paint(System.Drawing.Graphics graphics,
                System.Drawing.Rectangle clipBounds,
                System.Drawing.Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates dataGridViewElementState,
                object value,
                object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    dataGridViewElementState, value,
                    formattedValue, errorText, cellStyle,
                    advancedBorderStyle, paintParts);
                Point p = new Point();
                Size s = CheckBoxRenderer.GetGlyphSize(graphics,
                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                p.X = cellBounds.Location.X +
                    (cellBounds.Width / 2) - (s.Width / 2);
                p.Y = cellBounds.Location.Y +
                    (cellBounds.Height / 2) - (s.Height / 2);
                _cellLocation = cellBounds.Location;
                checkBoxLocation = p;
                checkBoxSize = s;
                if (_checked)
                    _cbState = System.Windows.Forms.VisualStyles.
                        CheckBoxState.CheckedNormal;
                else
                    _cbState = System.Windows.Forms.VisualStyles.
                        CheckBoxState.UncheckedNormal;
                CheckBoxRenderer.DrawCheckBox
                (graphics, checkBoxLocation, _cbState);
            }


            protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
            {
                Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
                if (p.X >= checkBoxLocation.X && p.X <=
                    checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <=
                    checkBoxLocation.Y + checkBoxSize.Height)
                {
                    _checked = !_checked;
                    if (OnCheckBoxClicked != null)
                    {
                        OnCheckBoxClicked(_checked);
                        this.DataGridView.InvalidateCell(this);
                    }

                }
                base.OnMouseClick(e);
            }

        }

        public delegate void CheckBoxClickedHandler(bool state);

        public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
        {
            bool isChecked;
            public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
            {
                isChecked = bChecked;
            }
            public bool Checked
            {
                get { return isChecked; }
                set { isChecked = value; }
            }
        }

        #endregion
        #endregion

        #region 点击单元格自动show调拨明细
        private void gvData_PoHead_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dtPoItems.Clear();
                for (int i = 0; i < dtPoHead.Rows.Count; i++)
                {

                    if (Convert.ToBoolean(dtPoHead.Rows[i]["SELECT"]))
                    {

                        dtPoItem = objStorageData.QureyPodwnItem(dtPoHead.Rows[i]["MBLNR351"].ToString(), dtPoHead.Rows[i]["MBLNR101"].ToString());
                        dtPoItems.Merge(dtPoItem);
                    }
                }
                if (dtPoItems.Rows.Count > 0)
                {
                    if (!dtPoItems.Columns.Contains("ID"))
                        dtPoItems.Columns.Add("ID", typeof(int));
                    for (int j = 0; j < dtPoItems.Rows.Count; j++)
                    {
                        dtPoItems.Rows[j]["ID"] = j + 1;
                    }
                    ShowdtPodwnItemGrid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-gvData_PoHead_CellValidating");
            }
        }
        #endregion

        #region 开立调拨Query
        private void btnApplyno_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMblnr351.Text.ToString().Trim() == "")
                {
                    stsWarning.Text = "351MBLNR不能为空！";
                    return;
                }
                dtTransferCar.Clear();
                string str351Mblnr = txtMblnr351.Text.ToString().Trim();
                dtTransferCar = objStorageData.QureyTritem(str351Mblnr);
                if (dtTransferCar.Rows.Count == 0)
                {
                    MessageBox.Show("351调拨单:" + str351Mblnr + "还未开立调拨车！");
                    return;
                }
                if (dtTransferCar.Rows.Count > 0)
                {
                    if (!dtTransferCar.Columns.Contains("ID"))
                        dtTransferCar.Columns.Add("ID", typeof(int));
                    for (int j = 0; j < dtTransferCar.Rows.Count; j++)
                    {
                        dtTransferCar.Rows[j]["ID"] = j + 1;
                    }
                }
                ShowdtdtTransferCarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()+">开立调拨Query");
            }
        }
        #endregion

        #region 派车单进度查询
        private void btnSendCar_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                if (txtApplyno.Text.ToString().Trim() == "")
                {
                    stsWarning.Text = "调拨车申请单号不能为空！";
                    return;
                }
                ArrayList strsApplyno = new ArrayList();
                DataTable dtDataApply = new DataTable();
                dtApplynoSchedule.Clear();
                string strTxt = txtApplyno.Text.ToString().ToUpper().Replace("；", ";");
                string[] str = strTxt.Split(';');
                for (int i = 0; i < str.Length; i++)
                {
                    strsApplyno.Add(str[i].ToString());
                    dtDataApply = objStorageData.QueryApplynoSchedule(str[i].ToString());
                    dtApplynoSchedule.Merge(dtDataApply);
                }
                if (dtApplynoSchedule.Columns.Contains("message"))
                {
                    MessageBox.Show(dtApplynoSchedule.Rows[0]["message"].ToString());
                    return;
                }
                if (dtApplynoSchedule.Rows.Count > 0)
                {
                    if (!dtApplynoSchedule.Columns.Contains("ID"))
                        dtApplynoSchedule.Columns.Add("ID", typeof(int));
                    for (int j = 0; j < dtApplynoSchedule.Rows.Count; j++)
                    {
                        dtApplynoSchedule.Rows[j]["ID"] = j + 1;
                        if (dtApplynoSchedule.Rows[j]["Status"].ToString() == "0")
                        {
                            dtApplynoSchedule.Rows[j]["Status"] = "刚派车";
                        }
                        if (dtApplynoSchedule.Rows[j]["Status"].ToString() == "1")
                        {
                            dtApplynoSchedule.Rows[j]["Status"] = "已打印";
                        }
                        if (dtApplynoSchedule.Rows[j]["Status"].ToString() == "2")
                        {
                            dtApplynoSchedule.Rows[j]["Status"] = "仓库出厂";
                        }
                        if (dtApplynoSchedule.Rows[j]["Status"].ToString() == "3" || dtApplynoSchedule.Rows[j]["Status"].ToString() == "4")
                        {
                            dtApplynoSchedule.Rows[j]["Status"] = "仓库已出库出厂";
                        }
                        if (dtApplynoSchedule.Rows[j]["Status"].ToString() == "5")
                        {
                            dtApplynoSchedule.Rows[j]["Status"] = "仓库入厂";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("SDS无此调拨车申请单数据！");
                    return;
                }

                ShowdtApplynoScheduleCarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ">派车单查询Query");
            }
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //this.cmbWerks.SelectedIndex = 0;
            //this.cmbDWerks.SelectedIndex = 0;
            this.gvData_PoHead.DataSource = null;
            this.dvg_PoItem.DataSource = null;
            this.dataGridView1.DataSource = null;
            this.dataGridView3.DataSource = null;
            this.gvData_PoHead.Columns.Clear();
            this.dvg_PoItem.Columns.Clear();
            this.dataGridView1.Columns.Clear();
            this.dataGridView3.Columns.Clear();
            txt46PO.Text = "";
            txtApplyno.Text = "";
            txtMblnr351.Text = "";
            stsWarning.Text = "";
        }

        #endregion

        #region Exit

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion





    }
}
