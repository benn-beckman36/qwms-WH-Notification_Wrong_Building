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
    public partial class TransferIn_101_Split : Form
    {
        public TransferIn_101_Split()
        {
            InitializeComponent();
        }
        public TransferIn_101_Split(UserInfo varUserData, DataTable dt, string strLgort, string strWerks)
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
            this.lblMatnr.Text = dt.Rows[0]["MATNR"].ToString();
            this.lblStorage.Text = strLgort;
            this.txtLocation.Text = dt.Rows[0]["DLOCAT"].ToString().Trim();
            this.lblToqty.Text = dt.Rows[0]["MENGE"].ToString();
            this.txtSplitTqty.Text = "0";
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

        # region 显示拆分资料
        public void ShowDataGrid()
        {
            dgvSplitData.Columns.Clear();
            dgvSplitData.AutoGenerateColumns = false;
            try
            {
                DataGridViewTextBoxColumn dgvcDWERKS = new DataGridViewTextBoxColumn();
                dgvcDWERKS.DataPropertyName = "DWERKS";
                dgvcDWERKS.HeaderText = "WERKS";
                dgvcDWERKS.Name = "DWERKS";
                dgvcDWERKS.Width = 50;
                dgvcDWERKS.ReadOnly = true;
                dgvSplitData.Columns.Add(dgvcDWERKS);

                DataGridViewTextBoxColumn dgvcDLGORT = new DataGridViewTextBoxColumn();
                dgvcDLGORT.DataPropertyName = "DLGORT";
                dgvcDLGORT.HeaderText = "LGORT";
                dgvcDLGORT.Name = "DLGORT";
                dgvcDLGORT.Width = 50;
                dgvcDLGORT.ReadOnly = true;
                dgvSplitData.Columns.Add(dgvcDLGORT);

                DataGridViewTextBoxColumn dgvcDLOCAT = new DataGridViewTextBoxColumn();
                dgvcDLOCAT.DataPropertyName = "DLOCAT";
                dgvcDLOCAT.HeaderText = "LOCAT";
                dgvcDLOCAT.Name = "DLOCAT";
                dgvcDLOCAT.Width = 50;
                dgvcDLOCAT.ReadOnly = false;
                dgvSplitData.Columns.Add(dgvcDLOCAT);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "调拨单号";
                dgvcMBLNR.Name = "MBLNR";
                dgvcMBLNR.Width = 90;
                dgvcMBLNR.ReadOnly = true;
                dgvSplitData.Columns.Add(dgvcMBLNR);

                //LIFNR
                DataGridViewTextBoxColumn dgvcZEILE = new DataGridViewTextBoxColumn();
                dgvcZEILE.DataPropertyName = "ZEILE";
                dgvcZEILE.HeaderText = "调拨Item";
                dgvcZEILE.Name = "ZEILE";
                dgvcZEILE.Width = 50;
                dgvcZEILE.ReadOnly = true;
                dgvSplitData.Columns.Add(dgvcZEILE);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "MATNR";
                dgvcMATNR.Name = "MATNR";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                dgvSplitData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "CHARG";
                dgvcCHARG.Name = "CHARG";
                dgvcCHARG.Width = 50;
                dgvcCHARG.ReadOnly = true;
                dgvSplitData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "LIFNR";
                dgvcLIFNR.Name = "LIFNR";
                dgvcLIFNR.Width = 50;
                dgvcLIFNR.ReadOnly = true;
                dgvSplitData.Columns.Add(dgvcLIFNR);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "MENGE";
                dgvcMENGE.Name = "MENGE";
                dgvcMENGE.Width = 50;
                dgvSplitData.Columns.Add(dgvcMENGE);
                dgvSplitData.DataSource = dtbindingData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string  strLocatNew = txtLocation.Text.Trim().ToUpper();
            if (strLocatNew == "")
            {
                MessageBox.Show("储位不能为空！");
                return;
            }
            PlantData objPlantData = new PlantData(UserData);
            if (!objPlantData.CheckExistedStorageData(Werks, this.lblStorage.Text.ToString(), txtLocation.Text.ToString()))
            {
                MessageBox.Show("当前储位不存在，请重新输入储位");
                txtLocation.Text = string.Empty;
                return;
            }
            if (txtqty.Text == "")
            {
                MessageBox.Show("拆分数量不能为空！");
                return;
            }
            for (int n = 0; n < dtbindingData.Rows.Count; n++)
            {
                if (strLocatNew == dtbindingData.Rows[n]["DLOCAT"].ToString().ToUpper())
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
                dr["DLOCAT"] = strLocatNew;
                dr["MENGE"] = txtqty.Text.Trim();
                // dtbindingData.Rows.Add(dr.ItemArray);

                #endregion
                if ((Convert.ToInt32(txtSplitTqty.Text.Trim()) + Convert.ToInt32(txtqty.Text.Trim())) > Convert.ToInt32(lblToqty.Text.Trim()))
                {
                    MessageBox.Show("拆分数量大于单据数量！");
                    txtqty.Text = "";
                    txtqty.Focus();
                    return;
                }
                txtSplitTqty.Text = (Convert.ToInt32(txtSplitTqty.Text.Trim()) + Convert.ToInt32(txtqty.Text.Trim())).ToString();
                if (Convert.ToInt32(lblToqty.Text.Trim()) == Convert.ToInt32(txtSplitTqty.Text.Trim()))
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

    }
}
