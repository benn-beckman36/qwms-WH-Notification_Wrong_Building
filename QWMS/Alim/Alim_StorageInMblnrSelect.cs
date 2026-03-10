using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;

namespace QWMS
{
    public partial class Alim_StorageInMblnrSelect : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strCtbto = "";
        private string strRegon = "";
        private string strType = "";
        private string strMblnr = "";
        private string strBwart = "";
        private string strFdate = "";
        private string strTdate = "";
        private DataTable dtData = new DataTable();
        private QCI.QWMS.Alim objAlim;

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
        public string MBLNR
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
            }
        }
        public string BWART
        {
            get
            {
                return strBwart;
            }
            set
            {
                strBwart = value;
            }
        }
        public string FDATE
        {
            get
            {
                return strFdate;
            }
            set
            {
                strFdate = value;
            }
        }
        public string TDATE
        {
            get
            {
                return strTdate;
            }
            set
            {
                strTdate = value;
            }
        }
        public Alim_StorageInMblnrSelect(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strBwart, string strFdate, string strTdate)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Usrnm = varUserData.UserId;
            Comcd = varUserData.CompanyCode;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            BWART = strBwart;
            FDATE = strFdate;
            TDATE = strTdate;
            try
            {
                objAlim = new QCI.QWMS.Alim(UserData, strProgid);

                //檢查權限
                if (!objAlim.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {
                    ShowMblnrData();
                }
                if (dtData.Rows.Count == 0)
                {
                    MBLNR = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ShowMblnrData()
        {
            try
            {
                QCI.QWMS.Alim_Storage objStorage = new QCI.QWMS.Alim_Storage(UserData, strProgid);
                dtData = objStorage.getWhdwnMblnr(Werks, Lgort, MBLNR, BWART, "listSapInData", strFdate, strTdate);

                ShowDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowMblnrData()");
            }

        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (MBLNR == "")
            {
                MessageBox.Show("Please select one Document!!");
                return;
            }
            this.Close();
        }

        private void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();

            DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
            dgvcMblnr.DataPropertyName = "MBLNR";
            dgvcMblnr.HeaderText = "DocumentNO";
            dgvcMblnr.Name = "MBLNR";
            dgvcMblnr.ReadOnly = true;
            dgvcMblnr.Width = 150;
            this.dgvData.Columns.Add(dgvcMblnr);

            
            this.dgvData.DataSource = dtData;
            this.lblCount.Text = dtData.Rows.Count.ToString() + " records";
            this.dgvData.ClearSelection();
            this.dgvData.AllowUserToAddRows = false;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    DataGridView.HitTestInfo hit = dgvData.HitTest(e.X, e.Y);
                    if (hit.Type == DataGridViewHitTestType.Cell)
                    {
                        DataGridViewCell clickedCell = dgvData.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                        MBLNR = clickedCell.Value.ToString();
                        this.btnSelect.Enabled = true;
                    }
                }
                else
                {
                    this.btnSelect.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
