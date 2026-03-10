using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using NPOI.SS.Formula.Functions;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Manage_PalletCombine : Form
    {
        # region 声明变量

        private UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strIsmrg = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private bool bolDuplicate = false;
        private DataTable dtSource = new DataTable();
        private DataTable dtDestination = new DataTable();
        private DataTable dtSelect = new DataTable();
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private StorageData objStorageData;
        private MixedMaterial objMixedMaterial;
        DataTable dtPrint = new DataTable();

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

        public string NewLocat
        {
            get
            {
                return txtDestination.Text.Trim();
            }
            set
            {
                txtDestination.Text = value;
            }
        }

        public string OldLocat
        {
            get
            {
                return txtSource.Text.Trim();
            }
            set
            {
                txtSource.Text = value;
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

        # endregion

        private void InitDataTable()
        {
            dtSource.Columns.Add(new DataColumn("MANDT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("COMCD", typeof(string)));
            dtSource.Columns.Add(new DataColumn("WERKS", typeof(string)));
            dtSource.Columns.Add(new DataColumn("LGORT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("LOCAT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("MATNR", typeof(string)));
            dtSource.Columns.Add(new DataColumn("INSMK", typeof(string)));
            dtSource.Columns.Add(new DataColumn("CHARG", typeof(string)));
            dtSource.Columns.Add(new DataColumn("MENGE", typeof(double)));
            dtSource.Columns.Add(new DataColumn("QCQTY", typeof(double)));
            dtSource.Columns.Add(new DataColumn("MBLNR", typeof(string)));
            dtSource.Columns.Add(new DataColumn("OMBLNR", typeof(string)));
            dtSource.Columns.Add(new DataColumn("ZEILE", typeof(string)));
            dtSource.Columns.Add(new DataColumn("MRGID", typeof(string)));
            dtSource.Columns.Add(new DataColumn("KOSTL", typeof(string)));
            dtSource.Columns.Add(new DataColumn("ARBPL", typeof(string)));
            dtSource.Columns.Add(new DataColumn("TRNTP", typeof(string)));
            dtSource.Columns.Add(new DataColumn("EBELN", typeof(string)));
            dtSource.Columns.Add(new DataColumn("LIFNR", typeof(string)));
            dtSource.Columns.Add(new DataColumn("RMAK1", typeof(string)));
            dtSource.Columns.Add(new DataColumn("INDAT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("VEDAT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("REFNO", typeof(string)));
            dtSource.Columns.Add(new DataColumn("CRDAT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("KDMAT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("SERNO", typeof(string)));
            dtSource.Columns.Add(new DataColumn("LOCOD", typeof(string)));
            dtSource.Columns.Add(new DataColumn("INSPT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("DACOD", typeof(string)));
            dtSource.Columns.Add(new DataColumn("BOXID", typeof(string)));
            dtSource.Columns.Add(new DataColumn("RMANO", typeof(string)));
            dtSource.Columns.Add(new DataColumn("PKDAT", typeof(string)));
            dtSource.Columns.Add(new DataColumn("BalanceQty", typeof(double)));
            dtSource.Columns.Add(new DataColumn("ALQTY", typeof(double)));
            dtSource.Columns.Add(new DataColumn("AddQty", typeof(double)));

            dtDestination.Columns.Add(new DataColumn("MANDT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("COMCD", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("WERKS", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("LGORT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("LOCAT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("MATNR", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("INSMK", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("CHARG", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("MENGE", typeof(double)));
            dtDestination.Columns.Add(new DataColumn("QCQTY", typeof(double)));
            dtDestination.Columns.Add(new DataColumn("MBLNR", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("OMBLNR", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("ZEILE", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("MRGID", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("KOSTL", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("ARBPL", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("TRNTP", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("EBELN", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("LIFNR", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("RMAK1", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("INDAT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("VEDAT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("REFNO", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("CRDAT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("KDMAT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("SERNO", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("LOCOD", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("INSPT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("DACOD", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("BOXID", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("RMANO", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("PKDAT", typeof(string)));
            dtDestination.Columns.Add(new DataColumn("BalanceQty", typeof(double)));
            dtDestination.Columns.Add(new DataColumn("ALQTY", typeof(double)));
            dtDestination.Columns.Add(new DataColumn("AddQty", typeof(double)));
        }


        public Manage_PalletCombine(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            InitDataTable();
            try
            {
                objStorageIn = new StorageIn(UserData, Progid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);

                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    bolDuplicate = objStorageIn.CheckDuplicatLocat();
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
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
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

        # region Plant SelectedChange

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
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
        # region ShowSourceDataGrid

        private void ShowSourceDataGrid()
        {
            dgvSource.AutoGenerateColumns = false;
            dgvSource.AllowUserToAddRows = false;
            dgvSource.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.Name = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvSource.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.Name = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvSource.Columns.Add(dgvcMatnr);



                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvSource.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvSource.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvSource.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Out Qty";
                dgvcAlqty.ReadOnly = true;
                dgvSource.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcBalanceQty = new DataGridViewTextBoxColumn();
                dgvcBalanceQty.DataPropertyName = "BalanceQty";
                dgvcBalanceQty.HeaderText = "Balance Qty";
                dgvcBalanceQty.ReadOnly = true;
                dgvSource.Columns.Add(dgvcBalanceQty);

                DataGridViewTextBoxColumn dgvcLocation = new DataGridViewTextBoxColumn();
                dgvcLocation.DataPropertyName = "LOCAT";
                dgvcLocation.Name = " LOCAT";
                dgvcLocation.HeaderText = "Location";
                dgvcLocation.ReadOnly = true;
                dgvSource.Columns.Add(dgvcLocation);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvSource.Columns.Add(dgvcIndat);


                dgvSource.DataSource = dtSource;
                lblCount.Text = dtSource.Rows.Count.ToString() + " records";

                if (dtSource.Rows.Count > 0)
                {
                    this.txtDestination.Enabled = true;
                }
                else
                {
                    this.txtDestination.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowSourceDataGrid()");
            }
        }

        # endregion

        # region ShowDestinationDataGrid

        private void ShowDestinationDataGrid()
        {
            dgvDestination.AutoGenerateColumns = false;
            dgvDestination.Columns.Clear();

            try
            {
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Pallet ID";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvDestination.Columns.Add(dgvcMblnr);


                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvDestination.Columns.Add(dgvcMatnr);


                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvDestination.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Qty";
                dgvcMenge.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcQty = new DataGridViewTextBoxColumn();
                dgvcQty.DataPropertyName = "AddQty";
                dgvcQty.Name = " AddQty";
                dgvcQty.HeaderText = "IN Qty";
                dgvcQty.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcQty);

                DataGridViewTextBoxColumn dgvcBalanceQty = new DataGridViewTextBoxColumn();
                dgvcBalanceQty.DataPropertyName = "BalanceQty";
                dgvcBalanceQty.Name = " BalanceQty";
                dgvcBalanceQty.HeaderText = "Balance Qty";
                dgvcBalanceQty.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcBalanceQty);

                DataGridViewTextBoxColumn dgvcLocation = new DataGridViewTextBoxColumn();
                dgvcLocation.DataPropertyName = "LOCAT";
                dgvcLocation.Name = " LOCAT";
                dgvcLocation.HeaderText = "Location";
                dgvcLocation.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcLocation);


                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvDestination.Columns.Add(dgvcIndat);


                dgvDestination.DataSource = dtDestination;
                lblCount1.Text = dtDestination.Rows.Count.ToString() + " records";

                if (dtDestination.Rows.Count > 0)
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDestinationDataGrid()");
            }
        }

        # endregion

        # region ShowSourceData

        private void ShowSourceData()
        {
            try
            {
                DataColumn[] dcPrimaryKey = new DataColumn[5];
                objStorageData = new StorageData(UserData, Werks, Lgort);
                string strPallet = txtSource.Text.Trim();
                DataTable dt = objStorageData.QueryStoragePalletData(strPallet,"combine");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //防止source中重复添加
                        if (dtSource.Rows.Count > 0 && dtSource.Select("MBLNR='" + item["MBLNR"] + "'").Length > 0)
                        {
                            MessageBox.Show("Pallet ID 已经存在!");
                            return;
                        }
                        // 防止 source 和destination 中相互增加
                        if (dtDestination.Rows.Count > 0 && dtDestination.Select("MBLNR='" + item["MBLNR"] + "'").Length > 0)
                        {
                            MessageBox.Show("Pallet ID 已经存在!");
                            return;
                        }
                        //检查料号 版本号 入库标志
                        if (dtSource.Rows.Count > 0 &&
                   dtSource.Select(string.Format("MATNR='{0}' and INSMK='{1}' and CHARG='{2}'",
                       item["MATNR"], item["INSMK"],
                      item["CHARG"])).Length < dtSource.Rows.Count)
                        {
                            MessageBox.Show("料号或版本号不一致，不能拼板");
                            return;
                        }
                        DataRow dr = dtSource.NewRow();

                        foreach (DataColumn dc in dtSource.Columns)
                        {
                            dr[dc.ColumnName] = item[dc.ColumnName];
                        }

                            dtSource.Rows.Add(dr);
                    }
                    reflashData();
                    this.txtSource.Text = "";
                    ShowSourceDataGrid();
                    this.panel1.Enabled = false;
                    if (String.IsNullOrEmpty(txtDestination.Text))
                    {
                        this.txtDestination.Enabled = true;
                    }
                    else
                    {
                        txtDestination.Enabled = false;
                    }
                    this.txtSource.Focus();
                }
                else
                {
                    stsWarning.Text = "No storage data!!";
                    this.txtSource.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        # endregion


        private void reflashData()
        {
              if (dtDestination.Rows.Count > 0&&dtSource.Rows.Count>0)
                {
                    //检查料号 版本号 入库标志
                    if (
                        dtSource.Select(string.Format("MATNR='{0}' and INSMK='{1}' and CHARG='{2}'",
                            dtDestination.Rows[0]["MATNR"], dtDestination.Rows[0]["INSMK"],
                            dtDestination.Rows[0]["CHARG"])).Length < dtSource.Rows.Count)
                    {
                        MessageBox.Show("料号或版本号不一致，不能拼板");
                        return;
                    }
                    long  AddQty = 0;
                    foreach (DataRow dr in dtSource.Rows)
                    {
                        AddQty += Convert.ToInt64(dr["ALQTY"]);
                        dr["OMBLNR"] = dtDestination.Rows[0]["MBLNR"].ToString();
                    }
                    dtDestination.Rows[0]["AddQty"] = AddQty;
                    dtDestination.Rows[0]["BalanceQty"] = AddQty + Convert.ToInt64(dtDestination.Rows[0]["MENGE"]);
                }
        }


        # region ShowDestinationData

        private void ShowDestinationData()
        {
            try
            {
                if (NewLocat == "")
                {
                    stsWarning.Text = "Please select destination!!";
                    return;
                }
                DataColumn[] dcPrimaryKey = new DataColumn[5];
                objStorageData = new StorageData(UserData, Werks, Lgort);
                string strPallet = txtDestination.Text;
                if (string.IsNullOrEmpty(strPallet))
                {
                    MessageBox.Show("请输入正确的Pallet ID");
                    return;
                }

                dtDestination = objStorageData.QueryStoragePalletData(strPallet, "combine");
                // 防止 source 和destination 中相互增加
                if (dtSource.Rows.Count > 0 && dtSource.Select("MBLNR='" + dtDestination.Rows[0]["MBLNR"] + "'").Length > 0)
                {
                    MessageBox.Show("Pallet ID 已经存在!");
                    return;
                }
                if (dtDestination.Rows.Count > 0)
                {
                    reflashData();
                    ShowDestinationDataGrid();
                }
                else
                {
                    MessageBox.Show("无数据");
                    return;
                }
                this.txtDestination.Enabled = false;

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        # endregion

        # region Save

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objStorageIn = new StorageIn(UserData, Werks, Lgort, "", Progid);
                DataTable dt1 = (DataTable) dgvSource.DataSource;
                DataTable dt2 = (DataTable) dgvDestination.DataSource;
                if (objStorageIn.PalletDataModify(dt1, dt2))
                {
                    stsWarning.Text = "Update OK!";
                    txtSource.Enabled = true;
                    txtDestination.Enabled = false;
                    txtDestination.Text = "";
                    txtSource.Text = "";
                    btnSave.Enabled = false;
                    btnPrint.Enabled = true;
                    dtPrint = dt2.Copy();
                }
                else
                {
                    stsWarning.Text = "Update fail!! " + objStorageIn.ERRMSG;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        # endregion

        # region Refresh

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            this.strWerks = "";
            this.txtSource.Text = "";
            this.txtDestination.Text = "";
            this.cmbWerks.Enabled = true;
            this.cmbLgort.Enabled = true;
            this.strLgort = "";
            this.txtSource.Enabled = true;
            this.txtDestination.Enabled = false;
            this.dtSource.Rows.Clear();
            this.dtDestination.Rows.Clear();
            this.dgvSource.DataSource = null;
            this.dgvDestination.DataSource = null;
            lblCount.Text = "";
            lblCount1.Text = "";
            this.btnSave.Enabled = false;
            this.panel1.Enabled = true;

            this.strIsmrg = "";
        }

        # endregion

        # region Exit

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        # endregion

        # region Resize

        private void Manage_LocationCombine_Resize(object sender, EventArgs e)
        {
            panel3.Size = new System.Drawing.Size((int) (this.Size.Width*0.3), panel3.Size.Height);
            panel6.Size = new System.Drawing.Size((int) (this.Size.Width*0.5), panel6.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 40 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width -
                                   this.stsDate.Width - 40;
        }

        # endregion

        # region Enter Press

        private void txtOldLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                try
                {
                    stsWarning.Text = "";
                    if (cmbWerks.SelectedIndex != -1)
                        Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    else
                        Werks = "";

                    if (cmbLgort.SelectedIndex != -1)
                        Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    else
                        Lgort = "";

                    if (Werks == "" || Lgort == "")
                    {
                        stsWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                    ShowSourceData();
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }

        private void txtNewLocat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                try
                {
                    stsWarning.Text = "";
                    if (cmbWerks.SelectedIndex != -1)
                        Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    else
                        Werks = "";

                    if (cmbLgort.SelectedIndex != -1)
                        Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                    else
                        Lgort = "";

                    if (Werks == "" || Lgort == "")
                    {
                        stsWarning.Text = "Plant and storage can't be empty!!";
                        return;
                    }
                    ShowDestinationData();
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
        }

        # endregion


        private void dgvSource_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string strMblnr = this.dgvSource.CurrentRow.Cells[0].Value.ToString();
            string Menge = this.dgvSource.CurrentRow.Cells[4].Value.ToString();
            string Alqty = this.dgvSource.CurrentRow.Cells[5].Value.ToString();
            string BalnaceQty = this.dgvSource.CurrentRow.Cells[6].Value.ToString();
            string strLocation =this.dgvSource.CurrentRow.Cells[7].Value.ToString();
            Manage_PalletQtyMaintain managePalletQtyMaintain = new Manage_PalletQtyMaintain(UserData, Progid, Werks, Lgort, strLocation, strMblnr, Menge, Alqty, BalnaceQty,"combine");
            managePalletQtyMaintain.ShowDialog();
            if (managePalletQtyMaintain.DialogResult == DialogResult.Yes)
            {
                //this.dgvSource.CurrentRow.Cells[0].Value = managePalletQtyMaintain.Mblnr;
                this.dgvSource.CurrentRow.Cells[4].Value = managePalletQtyMaintain.Menge;
                this.dgvSource.CurrentRow.Cells[5].Value = managePalletQtyMaintain.Alqty;
                this.dgvSource.CurrentRow.Cells[6].Value = managePalletQtyMaintain.BalanceQty;
                reflashData();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportPrint objReportPrint = new ReportPrint(UserData, "PLASTICMATERIALLOCATLABEL", dtPrint);
            objReportPrint.MdiParent = this.ParentForm;
            objReportPrint.Show();
        }
    }


}
