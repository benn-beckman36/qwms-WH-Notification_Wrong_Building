using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; //引用数字公式类
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;


namespace QWMS
{
    public partial class StorageIn_OnLineIn_101Location_Split : Form
    {
        public StorageIn_OnLineIn_101Location_Split()
        {
            InitializeComponent();
        }
        public StorageIn_OnLineIn_101Location_Split(UserInfo varUserData, DataTable dt, string strLgort, string strWerks)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
            dtdata = dt;
            Werks = strWerks;
            dtbindingData = dtdata.Clone();
            this.txtDocumentNO.Text = dt.Rows[0]["MBLNR"].ToString();
            this.txtpart.Text = dt.Rows[0]["MATNR"].ToString();
            this.txtLgort.Text      = strLgort;
            this.txtLocation.Text = dt.Rows[0]["Location"].ToString().Trim();
            this.txtTqty.Text = dt.Rows[0]["MENGE"].ToString();
            this.txtSplitTqty.Text = "0";
            this.txtDocumentNO.Enabled = false;
            this.txtpart.Enabled = false;
            this.txtLgort.Enabled = false;
            this.txtTqty.Enabled = false;
            this.txtSplitTqty.Enabled = false;
            this.txtqty.Focus();
            this.txtqty.SelectAll();
            this.btnSave.Enabled = false;


        }
        UserInfo UserData = new UserInfo();
        DataTable dtdata = new DataTable();
        public DataTable dtbindingData = new DataTable();
        private string Werks = "";
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strType = "";
        private string strInsmk = "";
        private string strSttyp = "";
        private string strLotyp = "";
        DataTable dts = new DataTable();

        #region 設定變數
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
        public string Werkss
        {
            get
            {
                return Werks;
            }
            set
            {
                Werks = value;
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
                return this.txtLocation.Text.Trim();
            }
            set
            {
                this.txtLocation.Text = value;
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
        public void getSplitDate(ref DataTable dt)
        {
            dt = dtbindingData;
        }
        #endregion
        

        #region 是否为数字
        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }
        #endregion

        #region 把dgv变成DataTable
        //把dgv编程DataTable   by Blank 2015/05/27
        //public DataTable GetDgvToTable(DataGridView dgv)
        //{
        //    DataTable dt = new DataTable();
        //    for (int count = 0; count < dgv.Columns.Count; count++)
        //    {
        //        DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
        //        dt.Columns.Add(dc);
        //    }
        //    for (int count = 0; count < dgv.Rows.Count; count++)
        //    {
        //        DataRow dr = dt.NewRow();
        //        for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
        //        {
        //            dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}
        #endregion

        # region 显示拆分资料BY101Location
        public void ShowDataGrid()
        {
            dgvSplitData.Columns.Clear();
            dgvSplitData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "Location";
                dgvcLocat.HeaderText = "Location";
                dgvcLocat.ReadOnly = true;
                dgvcLocat.Width = 80;
                dgvSplitData.Columns.Add(dgvcLocat);


                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "Part No";
                dgvcMatnr.ReadOnly = true;
                dgvcMatnr.Width = 100;
                dgvSplitData.Columns.Add(dgvcMatnr);

                DataGridViewTextBoxColumn dgvcInsmk = new DataGridViewTextBoxColumn();
                dgvcInsmk.DataPropertyName = "INSMK";
                dgvcInsmk.HeaderText = "Stock";
                dgvcInsmk.ReadOnly = true;
                dgvcInsmk.Width = 50;
                dgvSplitData.Columns.Add(dgvcInsmk);

                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "Version";
                dgvcCharg.ReadOnly = true;
                dgvcCharg.Width = 80;
                dgvSplitData.Columns.Add(dgvcCharg);

                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "Un-Store In Qty";
                dgvcMenge.ReadOnly = false;
                dgvcMenge.Width = 110;
                dgvSplitData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcAlqty = new DataGridViewTextBoxColumn();
                dgvcAlqty.DataPropertyName = "ALQTY";
                dgvcAlqty.HeaderText = "Storage In Qty";
                dgvcAlqty.ReadOnly = true;
                dgvcAlqty.Width = 100;
                dgvSplitData.Columns.Add(dgvcAlqty);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "Document No";
                dgvcMblnr.ReadOnly = true;
                dgvcMblnr.Width = 110;
                dgvSplitData.Columns.Add(dgvcMblnr);

                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Document Item";
                dgvcZeile.ReadOnly = true;
                dgvcZeile.Width = 100;
                dgvSplitData.Columns.Add(dgvcZeile);

                DataGridViewTextBoxColumn dgvcEbeln = new DataGridViewTextBoxColumn();
                dgvcEbeln.DataPropertyName = "EBELN";
                dgvcEbeln.HeaderText = "PO No";
                dgvcEbeln.ReadOnly = true;
                dgvcEbeln.Width = 100;
                dgvSplitData.Columns.Add(dgvcEbeln);

                DataGridViewTextBoxColumn dgvcLifnr = new DataGridViewTextBoxColumn();
                dgvcLifnr.DataPropertyName = "LIFNR";
                dgvcLifnr.HeaderText = "Vendor";
                dgvcLifnr.ReadOnly = true;
                dgvcLifnr.Width = 100;
                dgvSplitData.Columns.Add(dgvcLifnr);

                DataGridViewTextBoxColumn dgvcRmano = new DataGridViewTextBoxColumn();
                dgvcRmano.DataPropertyName = "RMANO";
                dgvcRmano.HeaderText = "RMANO";
                dgvcRmano.ReadOnly = true;
                dgvcRmano.Width = 100;
                dgvSplitData.Columns.Add(dgvcRmano);

                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "Dept No.";
                dgvcKostl.ReadOnly = true;
                dgvcKostl.Width = 100;
                dgvSplitData.Columns.Add(dgvcKostl);

                DataGridViewTextBoxColumn dgvcArbpl = new DataGridViewTextBoxColumn();
                dgvcArbpl.DataPropertyName = "ARBPL";
                dgvcArbpl.HeaderText = "PD Line";
                dgvcArbpl.ReadOnly = true;
                dgvcArbpl.Width = 100;
                dgvSplitData.Columns.Add(dgvcArbpl);

                DataGridViewTextBoxColumn dgvcTrntp = new DataGridViewTextBoxColumn();
                dgvcTrntp.DataPropertyName = "TRNTP";
                dgvcTrntp.HeaderText = "Trn-Type";
                dgvcTrntp.ReadOnly = true;
                dgvcTrntp.Width = 80;
                dgvSplitData.Columns.Add(dgvcTrntp);

                DataGridViewTextBoxColumn dgvcRmak1 = new DataGridViewTextBoxColumn();
                dgvcRmak1.DataPropertyName = "RMAK1";
                dgvcRmak1.HeaderText = "Remark";
                dgvcRmak1.ReadOnly = true;
                dgvcRmak1.Width = 100;
                dgvSplitData.Columns.Add(dgvcRmak1);

                DataGridViewTextBoxColumn dgvcIndat = new DataGridViewTextBoxColumn();
                dgvcIndat.DataPropertyName = "INDAT";
                dgvcIndat.HeaderText = "Store In Date";
                dgvcIndat.ReadOnly = true;
                dgvcIndat.Width = 80;
                dgvSplitData.Columns.Add(dgvcIndat);

                DataGridViewTextBoxColumn dgvcGrloc = new DataGridViewTextBoxColumn();
                dgvcGrloc.DataPropertyName = "GRLOC";
                dgvcGrloc.HeaderText = "101Location";
                dgvcGrloc.ReadOnly = true;
                dgvcGrloc.Width = 80;
                dgvSplitData.Columns.Add(dgvcGrloc);
                for (int i = 0; i < dtbindingData.Rows.Count; i++)
                {
                    dtbindingData.Rows[i]["Item"] = i + 1;
                }
                dgvSplitData.DataSource = dtbindingData;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        private void txtLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string  strLocatNew = txtLocation.Text.Trim();
            if (strLocatNew == "")
            {
                MessageBox.Show("储位不能为空！");
                return;
            }
            if (txtqty.Text == "")
            {
                MessageBox.Show("拆分数量不能为空！");
                return;
            }
            for (int n = 0; n < dtbindingData.Rows.Count; n++)
            {
                if (strLocatNew == dtbindingData.Rows[n]["Location"].ToString())
                {
                    MessageBox.Show("拆分储位重复，请确认！");
                    return;
                }
            }
            try
            {
                if (!CheckIsNumber(txtqty.Text.Trim()) || this.txtqty.Text.Trim() == "0")
                {
                    MessageBox.Show("填写数量必须为数字并且大于0！");
                    txtqty.SelectAll();
                    return;
                }

                  #region 循环拆分的行
                  //  DataRow dr = dtbindingData.NewRow();
                    DataRow dr = dtdata.Rows[0];
                    dr["Location"] = strLocatNew;
                    dr["ALQTY"] = txtqty.Text.Trim();
                   // dtbindingData.Rows.Add(dr.ItemArray);

                    #endregion
                if ((Convert.ToInt32(txtSplitTqty.Text.Trim()) + Convert.ToInt32(txtqty.Text.Trim())) > Convert.ToInt32(txtTqty.Text.Trim()))
                {
                    MessageBox.Show("拆分数量大于单据数量！");
                    txtqty.Text = "";
                    txtqty.Focus();
                    return;
                }
                txtSplitTqty.Text = (Convert.ToInt32(txtSplitTqty.Text.Trim()) + Convert.ToInt32(txtqty.Text.Trim())).ToString();
                if (Convert.ToInt32(txtTqty.Text.Trim()) == Convert.ToInt32(txtSplitTqty.Text.Trim()))
                {
                    btnSave.Enabled = true;
                    txtqty.Enabled = false;
                    txtLocation.Enabled = false;
                    btnAdd.Enabled = false;
                }
                dtbindingData.Rows.Add(dr.ItemArray);
                dgvSplitData.DataSource = dtbindingData;
                ShowDataGrid();
                txtqty.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            getSplitDate( ref dtbindingData);
            this.Close();
        }



        private void btnexit_Click(object sender, EventArgs e)
        {
            dtbindingData.Clear();
            this.Close();
            this.Dispose();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            dtbindingData.Clear();
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            txtqty.Text = "";
            txtSplitTqty.Text = "0";
        }

        private void txtLocation_DoubleClick(object sender, EventArgs e)
        {

            //StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(Mandt,txtLgort.Text.Trim(), Werks);
            //objStorageIn_LocationSelect.ShowDialog();
            //txtLocation.Text = objStorageIn_LocationSelect.Locat;

        }
    }
}
