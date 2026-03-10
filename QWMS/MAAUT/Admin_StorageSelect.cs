using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;
using System.Collections;
using System.IO;

namespace QWMS
{
    public partial class Admin_StorageSelect : Form
    {
        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strMatnr = "";
        private string strIntype = "";
        private string strInsmk = "";
        private string strComcd = "";
        //private string strDate = "";
        UserInfo UserData = new UserInfo();
        private DataTable dtData = new DataTable();
        private ArrayList aryLgort = new ArrayList();

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

        public string Matnr
        {
            get
            {
                return strMatnr;
            }
            set
            {
                strMatnr = value;
            }
        }

        public string Intype
        {
            get
            {
                return strIntype;
            }
            set
            {
                strIntype = value;
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

        public ArrayList Lgorts
        {
            get
            {
                return aryLgort;
            }
            set
            {
                aryLgort = value;
            }
        }

        public Admin_StorageSelect()
        {
            InitializeComponent();
        }

        public Admin_StorageSelect(UserInfo varUserData, string strWerks, string strProgid, DataTable dtLgort)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Werks = strWerks;
            Lgort = strLgort;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            Mblnr = strMblnr;
            Intype = strIntype;
            Insmk = strInsmk;
            strComcd = varUserData.CompanyCode;

        //    ShowLgortData(dtLgort);
        //}

        //private void ShowLgortData(DataTable dtLgort)
        //{
            dtData = dtLgort;
            DataColumn dcSelect = new DataColumn("SELECT", typeof(bool));
            dtData.Columns.Add(dcSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["SELECT"] = false;
            }
            ShowDataGrid();
        }

        private void ShowDataGrid()
        {
            try
            {
                dtgData.TableStyles.Clear();
                DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                mydtgTableStyle.MappingName = dtData.TableName;

                DataGridBoolColumn selectStyle = new DataGridBoolColumn();
                selectStyle.MappingName = "Select";
                selectStyle.HeaderText = "Select";
                selectStyle.Width = 40;
                ((DataGridBoolColumn)selectStyle).AllowNull = false;
                mydtgTableStyle.GridColumnStyles.Add(selectStyle);

                DataGridColumnStyle lgortStyle = new DataGridTextBoxColumn();
                lgortStyle.MappingName = "F_TEXT";
                lgortStyle.HeaderText = "Storage";
                lgortStyle.Width = 60;
                lgortStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(lgortStyle);

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
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtData.Rows[i]["SELECT"]))
                {
                    Lgorts.Add(dtData.Rows[i]["F_TEXT"]);
                }
            }

            if (Lgorts.Count==0)
            {
                MessageBox.Show("Please select one Storage!!");
                return;
            }
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //勾选仓别时
        private void dtgData_MouseDown(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    int intRowNo;
            //    DataGrid dgClick = (DataGrid)sender;
            //    DataGrid.HitTestInfo hitRow;
            //    hitRow = dgClick.HitTest(e.X, e.Y);
            //    if (hitRow.Type == DataGrid.HitTestType.RowHeader)
            //    {
            //        this.btnSelect.Enabled = true;
            //        intRowNo = hitRow.Row;
            //        dgClick.CurrentCell = new DataGridCell(intRowNo, 0);
            //        Mblnr = dgClick[dgClick.CurrentCell].ToString();
            //        dgClick.CurrentCell = new DataGridCell(intRowNo, 1);
            //        Matnr = dgClick[dgClick.CurrentCell].ToString();

            //    }
            //    else
            //    {
            //        this.btnSelect.Enabled = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return;
            //}
        }
    }
}
