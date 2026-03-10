using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;
using QCI_QWMS_Models;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;

namespace QWMS.Models
{
    public partial class Model_TransferOut : Form
    {
        private UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";

        private string strMblnr = "";
        private DataTable dtOutSource = new DataTable();
        private DataTable dtStorage = new DataTable();
        private DataTable dtCombineStorage = new DataTable();
        private ModelsData objModelsData;

        public Model_TransferOut(ref UserInfo varUserData, string strProgid)
        {
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
     

            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageOut.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
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

        public string Mblnr
        {
            get
            {
                return  strMblnr;
            }
            set
            {
                strMblnr = value;
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
                    //					dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //					dtTemp = objPlantData.GetDdlLgortData();
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


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {

                stsWarning.Text = "";
                string[] aryTempMblnr = txtMblnr.Text.Trim().Split(new char[] {','});


                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";
                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                //單號不為空
                Mblnr = txtMblnr.Text.Trim().ToString();
                if (string.IsNullOrEmpty(Mblnr))
                {
                    stsWarning.Text = "Document No can't be empty!!";
                    return;
                }
                objModelsData = new ModelsData(UserData, Werks, Lgort, Progid);
                dtOutSource = objModelsData.ListOrderData(Mblnr, "", "MODEL_E");


                if (dtOutSource.Rows.Count == 0)
                {
                    stsWarning.Text = "No data!!";
                    return;
                }

                ShowOutSourceDataGrid();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void txtMblnr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    strWerks = "";

                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    strLgort = "";

                //廠區倉別不為空
                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }


                Model_OrderSelect objModelOrderSelect = new Model_OrderSelect(UserData, Werks, Lgort, Progid, "MODEL_E");
                objModelOrderSelect.ShowDialog();
                Mblnr = objModelOrderSelect.Mblnr;
                if (Mblnr != "")
                {
                    txtMblnr.Text = Mblnr.Substring(0, 16);
                }
                else
                {
                    txtMblnr.Text = Mblnr;
                }
                
                if (!string.IsNullOrEmpty(Mblnr))
                {
                    this.txtMblnr.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }

        private void ShowOutSourceDataGrid()
        {

            dgvOutSource.AutoGenerateColumns = false;
            dgvOutSource.Columns.Clear();
            try
            {
                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.Width = 90;
                dgvcMblnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcZeile);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "模号";
                dgvcMatnr.Width = 90;
                dgvcMatnr.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMatnr);



                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "版本";
                dgvcCharg.Width = 90;
                dgvcCharg.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcCharg);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store Out Qty";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMenge);



                //KOSTL
                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "BU";
                dgvcKostl.HeaderText = "PU";
                dgvcKostl.Width = 90;
                dgvcKostl.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcKostl);

                //ARBPL
                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ItemName";
                dgvcArbpl.HeaderText = "品名";
                dgvcArbpl.Width = 90;
                dgvcArbpl.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcMachine = new DataGridViewTextBoxColumn();
                dgvcMachine.DataPropertyName = "Machine";
                dgvcMachine.HeaderText = "机种";
                dgvcMachine.Width = 90;
                dgvcMachine.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcMachine);

                //Remark
                DataGridViewTextBoxColumn dgvcRemark = new DataGridViewTextBoxColumn();
                dgvcRemark.DataPropertyName = "Remark";
                dgvcRemark.HeaderText = "备注";
                dgvcRemark.Width = 90;
                dgvcRemark.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcRemark);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "TRNTP";
                dgvcTrntp.Width = 90;
                dgvcTrntp.ReadOnly = true;
                dgvOutSource.Columns.Add(dgvcTrntp);


                dgvOutSource.DataSource = dtOutSource;
                lblStorage.Text = dtOutSource.Rows.Count.ToString() + " records";

                if (dtOutSource.Rows.Count > 0)
                {
                    this.panel1.Enabled = false;
                    this.btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowOutSourceDataGrid()");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                objModelsData.ModelTransforOutByOrder(dtOutSource, Mblnr);
                if(string.IsNullOrEmpty(objModelsData.ERRMSG))
                {
                    stsWarning.Text = "保存成功！";
                    btnSave.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnPrint.Enabled = true;
                }
                else
                {
                    stsWarning.Text = "保存失败！" + objModelsData.ERRMSG;
                    btnSave.Enabled = true ;
                    return;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }
        private void PrintReport()
        {
            try
            {

                DataTable dtPrint = new DataTable();
                ModelsData objModelsData = new ModelsData(UserData, strWerks, strLgort, Progid);
                string modelorder = objModelsData.GetModerOrderBYItems(Mblnr);
                dtPrint = objModelsData.GetPrintModelData(strWerks, strLgort, modelorder, "", "", "");
                if (dtPrint.Rows.Count > 0)
                {
                    ReportPrint objReportPrint = new ReportPrint(UserData, "MODEL", dtPrint);
                    objReportPrint.MdiParent = this.ParentForm;
                    objReportPrint.Show();
                }
                else
                {
                    throw new Exception("打印无数据！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintReport();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtOutSource.Clear();
            this.panel1.Enabled = true;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnConfirm.Enabled = true;
            txtMblnr.Enabled = true;
            txtMblnr.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void txtMblnr_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("双击输入框显示调拨单选择表", txtMblnr);
        }

        private void txtMblnr_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.Show("双击输入框显示调拨单选择表", txtMblnr);
        }
    }
}
