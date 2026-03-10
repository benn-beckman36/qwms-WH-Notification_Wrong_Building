using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.Collections;
using QCI.QWMS;

namespace QWMS
{
    public partial class Transfer_LocationSelect : Form
    {

        #region   变量
       private System.Windows.Forms.Button btnReturn;
       private System.Windows.Forms.Button btnSelect;
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
        private string strMblnr = "";
        private DataTable dtData = new DataTable();
        private ArrayList alLocat;
        private ArrayList aryMatnr;
        private System.Windows.Forms.DataGrid dtgData;


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

        public ArrayList CurLocat
        {
            get
            {
                return alLocat;
            }
            set
            {
                alLocat = value;
            }
        }

        public ArrayList CurMatnr
        {
            get
            {
                return aryMatnr;
            }
            set
            {
                aryMatnr = value;
            }
        }

        public string Mblnr
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

        public string Ctbto
        {
            get
            {
                return strCtbto;
            }
            set
            {
                strCtbto = value;
            }
        }

        public string Regon
        {
            get
            {
                return strRegon;
            }
            set
            {
                strRegon = value;
            }
        }


        #endregion

        public Transfer_LocationSelect(UserInfo varUserData, string strProgid, string strWerks, string strLgort)
        {
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Usrnm = varUserData.UserId;
            Comcd = varUserData.CompanyCode;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Ctbto = "";
            Regon = "";


            try
            {
                Replenishment objReplenishment = new Replenishment(UserData, strProgid);
                PlantData objPlantData = new PlantData(UserData);


                //檢查權限
                if (!objReplenishment.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {
                    ShowLocationData();

                }
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

                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                dtData = objPlantData.GetAllLocatData(Werks, Lgort, "", "1", Ctbto, Regon);
                


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
                dtgData.TableStyles.Clear();
                DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                mydtgTableStyle.MappingName = dtData.TableName;

                DataGridColumnStyle locatStyle = new DataGridTextBoxColumn();
                locatStyle.MappingName = "LOCAT";
                locatStyle.HeaderText = "Location";
                locatStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(locatStyle);

                dtgData.DataSource = dtData;
                dtgData.TableStyles.Add(mydtgTableStyle);

                dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
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

        private void dtgData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int intRowNo;
                DataGrid dgClick = (DataGrid)sender;
                DataGrid.HitTestInfo hitRow;
                hitRow = dgClick.HitTest(e.X, e.Y);
                if (hitRow.Type == DataGrid.HitTestType.RowHeader)
                {
                    this.btnSelect.Enabled = true;
                    intRowNo = hitRow.Row;
                    dgClick.CurrentCell = new DataGridCell(intRowNo, 0);
                    Locat = dgClick[dgClick.CurrentCell].ToString();
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
