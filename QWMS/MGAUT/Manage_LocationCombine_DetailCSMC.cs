using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Manage_LocationCombine_DetailCSMC : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private DataTable dtStorageData = new DataTable();
        private DataTable dtTemp = new DataTable();
        private DataTable dtBox = new DataTable();
        private DataTable dtBoxtemp = new DataTable();
        private string strmblnr = "";
        private string strinsmk = "";
        private string strcharg = "";
        private string strmatnr = "";
        private string strBoxid = "";

        public string Boxid
        {
            get
            {
                return strBoxid;
            }
            set
            {
                strBoxid = value;
            }
        }

        public string Insmk
        {
            get
            {
                return strinsmk;
            }
            set
            {
                strinsmk = value;
            }
        }

        public string Charg
        {
            get
            {
                return strcharg;
            }
            set
            {
                strcharg = value;
            }
        }

        public string Matnr
        {
            get
            {
                return strmatnr;
            }
            set
            {
                strmatnr = value;
            }
        }


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

        public string Menge
        {
            get
            {
                return txtMenge.Text.Trim();
            }
            set
            {
                txtMenge.Text = value;
            }
        }

        public string Alqty
        {
            get
            {
                return txtAlqty.Text.Trim();
            }
            set
            {
                txtAlqty.Text = value;
            }
        }

        public DataTable Returndt
        {
            get
            {
                return dtBox;
            }
            set
            {
                dtBox = value;
            }
        }
        # endregion

            public Manage_LocationCombine_DetailCSMC(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, string strBoxid, string strinsmk, string strcharg, string strmatnr)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Locat = strLocat;
            Boxid = strBoxid;
            Insmk = strinsmk;
            Charg = strcharg;
            Matnr = strmatnr;
            btnConfirm.Enabled = false;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                //Out Qty不能为空
                if (Alqty == "")
                {
                    MessageBox.Show("Out Qty can't be empty!!");
                    return;
                }
                try
                {
                    Int64 intTest = Int64.Parse(Alqty);
                }
                catch
                {
                    MessageBox.Show("Out Qty should be integer!!");
                    return;
                }
                //Out Qty必能大于Location Qty
                if (Int64.Parse(Alqty) > Int64.Parse(Menge))
                {
                    MessageBox.Show("Out Qty should be less than Location Qty!!");
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.txtAlqty.Text = "0";
            this.Close();
        }

        public void ShowDataGrid()
        {

            this.dgvSN.AutoGenerateColumns = false;
            this.dgvSN.Columns.Clear();
            try
            {

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "WERKS";
                dgvcWERKS.Width = 90;
                dgvcWERKS.ReadOnly = true;
                this.dgvSN.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "LGORT";
                dgvcLGORT.Width = 90;
                dgvcLGORT.ReadOnly = true;
                this.dgvSN.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "LOCAT";
                dgvcLOCAT.Width = 90;
                dgvcLOCAT.ReadOnly = true;
                this.dgvSN.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "MBLNR";
                dgvcMBLNR.Width = 90;
                dgvcMBLNR.ReadOnly = true;
                this.dgvSN.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.Width = 90;
                dgvcMENGE.ReadOnly = true;
                this.dgvSN.Columns.Add(dgvcMENGE);


                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "INSMK";
                dgvcINSMK.Width = 90;
                dgvcINSMK.ReadOnly = true;
                this.dgvSN.Columns.Add(dgvcINSMK);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.Width = 90;
                dgvcCHARG.ReadOnly = true;
                this.dgvSN.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLOADID = new DataGridViewTextBoxColumn();
                dgvcLOADID.DataPropertyName = "LOADID";
                dgvcLOADID.HeaderText = "LOADID";
                dgvcLOADID.Width = 90;
                dgvcLOADID.ReadOnly = true;
                this.dgvSN.Columns.Add(dgvcLOADID);

                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "BOXID";
                dgvcBOXID.HeaderText = "BOXID";
                dgvcBOXID.Width = 90;
                dgvcBOXID.ReadOnly = false;
                this.dgvSN.Columns.Add(dgvcBOXID);

                DataGridViewTextBoxColumn dgvcSERNO = new DataGridViewTextBoxColumn();
                dgvcSERNO.DataPropertyName = "SERNO";
                dgvcSERNO.HeaderText = "SERNO";
                dgvcSERNO.Width = 90;
                dgvcSERNO.ReadOnly = false;
                this.dgvSN.Columns.Add(dgvcSERNO);


                dgvSN.DataSource = dtBox;




            }
            catch (Exception ex)
            {

            }
        }


        private void txtBoxid_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataRow[] arrrow;
            if (e.KeyChar == (char)13)
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), this.UserData, Werks, Lgort, "", Progid);

                DataTable dt = new DataTable();
                if (string.IsNullOrEmpty(txtBoxid.Text.Trim()))
                {
                    MessageBox.Show("请刷入BOXID!");
                    return;
                }

                dt = objStorageIn.Querywhbox_GB(Werks, Lgort, Locat, txtBoxid.Text.Trim(), Insmk, Charg, Matnr, "");
                dtBoxtemp = dt.Copy();
                //防止多次刷入 更新掉原有数据
                if (dtBox.Columns.Count == 0 || dtBox == null || dtBox.Rows.Count < 0)
                {
                    dtBox = dt.Clone();
                }
                if (dtBox.Columns.Count == 0)
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.AppendFormat(" BOXID='{0}'", txtBoxid.Text.Trim());
                    arrrow = dtBoxtemp.Select(strsql.ToString());
                    if (arrrow.Length > 0)
                    {
                        for (int i = 0; i < arrrow.Length; i++)
                        {

                            dtBox.Rows.Add(arrrow[i].ItemArray);
                        }
                    }
                    ShowDataGrid();
                    txtBoxid.Text = "";
                }
                else
                {
                    if (dtBox.Select("BOXID='" + txtBoxid.Text.Trim() + "'").Length > 0)
                    {
                        txtBoxid.Text = "";
                        MessageBox.Show("BoxID已存在！");
                        return;
                    }
                    foreach (DataRow drBox in dtBox.Rows)
                    {
                        if (dtBox.Select("BOXID='" + txtBoxid.Text.Trim() + "' AND SERNO='" + drBox["SERNO"].ToString() + "' ").Length > 0)
                        {
                            txtBoxid.Text = "";
                            MessageBox.Show(string.Format("此Box ID：{0} 已经有刷过SN，请确认！", txtBoxid.Text.Trim()));
                            return;
                        }
                    }

                    StringBuilder strsql = new StringBuilder();
                    strsql.AppendFormat(" BOXID='{0}'", txtBoxid.Text.Trim());
                    arrrow = dtBoxtemp.Select(strsql.ToString());
                    if (arrrow.Length > 0)
                    {
                        for (int i = 0; i < arrrow.Length; i++)
                        {
                            dtBox.Rows.Add(arrrow[i].ItemArray);
                        }
                    }
                    ShowDataGrid();
                    txtBoxid.Text = "";

                    int qty = 0;
                    if (dtBox.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtBox.Rows.Count; i++)
                        {
                            qty = qty + Convert.ToInt32(dtBox.Rows[i]["MENGE"].ToString().Trim());
                        }
                    }
                    txtAlqty.Text = qty.ToString().Trim();
                }
                btnConfirm.Enabled = true;
            }
        }

        //Add By Michael 20150810 for 增加刷SN
        private void txtSerNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataRow[] arrrow;
            if (e.KeyChar == (char)13)
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), this.UserData, Werks, Lgort, "", Progid);

                DataTable dt = new DataTable();
                if (string.IsNullOrEmpty(txtSerNo.Text.Trim()))
                {
                    MessageBox.Show("请刷入SERNO!");
                    return;
                }

                dt = objStorageIn.Querywhbox_GB(Werks, Lgort, Locat, "", Insmk, Charg, Matnr, txtSerNo.Text.Trim());
                dtBoxtemp = dt.Copy();
                //防止多次刷入 更新掉原有数据
                if (dtBox.Columns.Count == 0 || dtBox == null || dtBox.Rows.Count < 0)
                {
                    dtBox = dt.Clone();
                }
                if (dtBox.Columns.Count == 0)
                {
                    StringBuilder strsql = new StringBuilder();
                    strsql.AppendFormat(" SERNO='{0}'", txtSerNo.Text.Trim());
                    arrrow = dtBoxtemp.Select(strsql.ToString());
                    if (arrrow.Length > 0)
                    {
                        for (int i = 0; i < arrrow.Length; i++)
                        {

                            dtBox.Rows.Add(arrrow[i].ItemArray);
                        }
                    }
                    ShowDataGrid();
                    txtSerNo.Text = "";
                }
                else
                {
                    foreach (DataRow drBox in dtBox.Rows)
                    {
                        if (dtBox.Select("BOXID='" + drBox["BOXID"].ToString() + "' AND SERNO='" + txtSerNo.Text.Trim() + "' ").Length > 0)
                        {
                            txtSerNo.Text = "";
                            MessageBox.Show(string.Format("此SERNO：{0} 所在Box ID：{1} 已经存在，请确认！", txtSerNo.Text.Trim(), drBox["BOXID"].ToString()));
                            return;
                        }
                    }

                    StringBuilder strsql = new StringBuilder();
                    strsql.AppendFormat(" SERNO='{0}'", txtSerNo.Text.Trim());
                    arrrow = dtBoxtemp.Select(strsql.ToString());
                    if (arrrow.Length > 0)
                    {
                        for (int i = 0; i < arrrow.Length; i++)
                        {
                            dtBox.Rows.Add(arrrow[i].ItemArray);
                        }
                    }
                    ShowDataGrid();
                    txtSerNo.Text = "";

                    int qty = 0;
                    if (dtBox.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtBox.Rows.Count; i++)
                        {
                            qty = qty + Convert.ToInt32(dtBox.Rows[i]["MENGE"].ToString().Trim());
                        }
                    }
                    txtAlqty.Text = qty.ToString().Trim();
                }
                btnConfirm.Enabled = true;
            }
        }

        private void txtSerNo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                MessageBox.Show("单个SN禁止并储！");
                txtSerNo.Text = "";
                txtBoxid.Focus();
                return;
            }
        }
    }
}
