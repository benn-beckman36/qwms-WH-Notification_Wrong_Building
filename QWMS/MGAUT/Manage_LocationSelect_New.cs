using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Manage_LocationSelect_New : Form
    {
        #region DataMember
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strType = "";
        private DataTable dtData = new DataTable();
        private PlantData objPlantData;
        private StorageIn objStorageIn;

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

        public Manage_LocationSelect_New(UserInfo UserData, string strProgid, string strWerks, string strLgort, string strType)
        {
            InitializeComponent();
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Type = strType;

            try
            {
                QCI.QWMS.Authority objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Progid);
                objPlantData = new PlantData(UserData);

                ShowLocationData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void ShowLocationData()
        {
            try
            {
                if (Type.ToUpper() == "NEW")	//空儲位
                {
                    dtData = objPlantData.GetAllLocatData_New(Werks, Lgort, "", "0");
                }
                if (Type.ToUpper() == "ADD")	//有庫存儲位
                {
                    dtData = objPlantData.GetAllLocatData_New(Werks, Lgort, "", "1");
                }
                if (Type.ToUpper() == "ALL")	//全部儲位
                {
                    dtData = objPlantData.GetAllLocatData_New(Werks, Lgort, "", "2");
                }
                if (Type.ToUpper() == "MAP")	//尚未設定對應關係的儲位
                {
                    dtData = objPlantData.GetAllLocatData_New(Werks, Lgort, "", "3");
                }
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowLocationData()");
            }

        }

        private void ShowDataGrid()
        {
            try
            {
                dgvData.AutoGenerateColumns = false;
                dgvData.Columns.Clear();

                DataGridViewTextBoxColumn locatStyle = new DataGridViewTextBoxColumn();
                locatStyle.DataPropertyName = "LOCAT";
                locatStyle.HeaderText = "Location";
                locatStyle.ReadOnly = true;
                dgvData.Columns.Add(locatStyle);

                DataGridViewTextBoxColumn dgvTotal = new DataGridViewTextBoxColumn();
                dgvTotal.DataPropertyName = "TOTAL";
                dgvTotal.HeaderText = "Total";
                dgvTotal.ReadOnly = true;
                dgvData.Columns.Add(dgvTotal);

                dgvData.DataSource = dtData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (Locat == "")
            {
                MessageBox.Show("Please select one location!!");
                return;
            }
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int intRowNo;
                DataGridView dgClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow; ;
                hitRow = dgClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGridViewHitTestType.RowHeader)
                {
                    this.btnSelect.Enabled = true;
                    intRowNo = hitRow.RowIndex;
                    Locat = this.dgvData.Rows[intRowNo].Cells[0].Value.ToString();
                }
                else
                {
                    Locat = "";
                }
                if (Locat != "")
                {
                    this.btnSelect.Enabled = true;
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
