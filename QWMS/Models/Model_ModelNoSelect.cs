using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QCI_QWMS_Models;
using QWMS.Common;

namespace QWMS.Models
{
    public partial class Model_ModelNoSelect : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        //private string strSendID = "";
        private ArrayList aryMatnr = new ArrayList();

        private DataTable dtData = new DataTable();
        //private StorageOut objStorageOut;
        //private SapData objSapData;
        private QCI_QWMS_Models.ModelsData objModelsData;

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


        public ArrayList Matnrs
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



        public Model_ModelNoSelect(UserInfo varUserData, string varWerks, string varLgort, string varProgid)
        {
            try{
            UserData = varUserData;
            InitializeComponent();
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = varProgid;
            Werks = varWerks;
            Lgort = varLgort;
            objModelsData = new ModelsData(UserData, Werks, Lgort,Progid);
            ShowModelsData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            aryMatnr.Clear();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtData.Rows[i]["Select"]))
                {
                    if (aryMatnr.IndexOf(dtData.Rows[i]["MATNR"].ToString()) < 0)
                    {
                        aryMatnr.Add(dtData.Rows[i]["MATNR"]);
                    }
                }
            }

            if (aryMatnr.Count < 1)
            {
                MessageBox.Show("请选择单号！");
                return;
            }
            this.Close();
        }

        private void ShowModelsData()
        {

            dtData = objModelsData.GetModelsListInWhitm(txtQuery.Text.Trim());

            DataColumn cSelect = new DataColumn("SELECT", typeof(bool));
            dtData.Columns.Add(cSelect);
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

                DataGridColumnStyle mblnrStyle = new DataGridTextBoxColumn();
                mblnrStyle.MappingName = "MATNR";
                mblnrStyle.HeaderText = "模号";
                mblnrStyle.Width = 100;
                mblnrStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(mblnrStyle);

                DataGridColumnStyle CHARGStyle = new DataGridTextBoxColumn();
                CHARGStyle.MappingName = "CHARG";
                CHARGStyle.HeaderText = "版本号";
                CHARGStyle.Width = 90;
                CHARGStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(CHARGStyle);

                DataGridColumnStyle BUStyle = new DataGridTextBoxColumn();
                BUStyle.MappingName = "BU";
                BUStyle.HeaderText = "PU";
                BUStyle.Width = 90;
                BUStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(BUStyle);

                DataGridColumnStyle ItemNameStyle = new DataGridTextBoxColumn();
                ItemNameStyle.MappingName = "ItemName";
                ItemNameStyle.HeaderText = "品名";
                ItemNameStyle.Width = 90;
                ItemNameStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(ItemNameStyle);

                DataGridColumnStyle MachineStyle = new DataGridTextBoxColumn();
                MachineStyle.MappingName = "Machine";
                MachineStyle.HeaderText = "机种";
                MachineStyle.Width = 90;
                MachineStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(MachineStyle);


                DataGridColumnStyle matnrStyle = new DataGridTextBoxColumn();
                matnrStyle.MappingName = "LGORT";
                matnrStyle.HeaderText = "仓别";
                matnrStyle.Width = 90;
                matnrStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(matnrStyle);

                DataGridColumnStyle BalanceStyle = new DataGridTextBoxColumn();
                BalanceStyle.MappingName = "LOCAT";
                BalanceStyle.HeaderText = "储位";
                BalanceStyle.Width = 90;
                BalanceStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(BalanceStyle);

                DataGridColumnStyle sendidStyle = new DataGridTextBoxColumn();
                sendidStyle.MappingName = "MENGE";
                sendidStyle.HeaderText = "数量";
                sendidStyle.Width = 90;
                sendidStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(sendidStyle);


                DataGridColumnStyle RemarkStyle = new DataGridTextBoxColumn();
                RemarkStyle.MappingName = "Remark";
                RemarkStyle.HeaderText = "备注";
                RemarkStyle.Width = 90;
                RemarkStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(RemarkStyle);
                dtgData.DataSource = dtData;
                dtgData.TableStyles.Add(mydtgTableStyle);
                dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowModelsData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
