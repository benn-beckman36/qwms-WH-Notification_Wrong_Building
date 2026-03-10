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
    public partial class Manage_EmptyLocation : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string strComcd = "";
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

        #region 构造函数
        public Manage_EmptyLocation(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strType)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Type = strType;

            try
            {
                objPlantData = new PlantData(UserData);
                objStorageIn = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Progid);

                ShowLocationData();
                lblTotal.Text = "Total: " + dtData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        public Manage_EmptyLocation()
        {
            InitializeComponent();
        }
        #endregion

        #region ShowLocationData
        private void ShowLocationData()
        {
            try
            {
                if (Type.ToUpper() == "NEW") 
                {
                    dtData = objPlantData.GetAllLocatData(Werks, Lgort, "", "0");
                }
                if (Type.ToUpper() == "ADD")	
                {
                    dtData = objPlantData.GetAllLocatData(Werks, Lgort, "", "1");
                }
                if (Type.ToUpper() == "ALL")	
                {
                    dtData = objPlantData.GetAllLocatData(Werks, Lgort, "", "2");
                }
                if (Type.ToUpper() == "MAP")	
                {
                    dtData = objPlantData.GetAllLocatData(Werks, Lgort, "", "3");
                }
                ShowDataGridView();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowLocationData()");
            }

        }
        #endregion

        #region ShowDataGridView
        private void ShowDataGridView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcLocat = new DataGridViewTextBoxColumn();
                dgvcLocat.DataPropertyName = "LOCAT";
                dgvcLocat.HeaderText = "Plant";
                dgvcLocat.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLocat);

                this.gvData.DataSource = dtData;

			}
			catch(Exception ex)
			{
                throw new Exception(ex.Message + "<-ShowDataGridView()");
			}
        }
        #endregion

        #region Print
        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportPrint objReportPrint = new ReportPrint(UserData, "EMPTYLOCATION", dtData);
            objReportPrint.ShowDialog();
            this.Close();
            
        }
        #endregion

        #region Return
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
