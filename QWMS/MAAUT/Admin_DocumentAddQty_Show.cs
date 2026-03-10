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

namespace QWMS
{
    public partial class Admin_DocumentAddQty_Show : Form
    {
        public Admin_DocumentAddQty_Show()
        {
            InitializeComponent();
        }

        #region 變數宣告
        UserInfo UserData = new UserInfo();

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private DataTable dtData = new DataTable();
        private Admin objAdmin;

        #endregion

        #region DataMember

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

        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
            }
        }

        #endregion

        public Admin_DocumentAddQty_Show(UserInfo varUserData, string strProgid, string strWerks, string strLgort, DataTable dtData)
		{
			InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Usrnm = varUserData.UserId;
            Comcd = varUserData.CompanyCode;
			Progid = strProgid;
			Werks = strWerks;
			Lgort = strLgort;
            Data = dtData;

			try
			{
                objAdmin = new Admin(UserData, Progid);

                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
				else
				{
					ShowDocumentData();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowDocumentData()
        {
            try
            {
                ShowDataGrid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDocumentData()");
            }

        }

        private void ShowDataGrid()
        {
            try
            {
                dtgData.TableStyles.Clear();
                DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                mydtgTableStyle.MappingName = dtData.TableName;

                DataGridColumnStyle mblnrStyle = new DataGridTextBoxColumn();
                mblnrStyle.MappingName = "MBLNR";
                mblnrStyle.HeaderText = "Document No.";
                mblnrStyle.ReadOnly = true;
                mydtgTableStyle.GridColumnStyles.Add(mblnrStyle);

                dtgData.DataSource = dtData;
                dtgData.TableStyles.Add(mydtgTableStyle);

                dtgData.CaptionText = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
    }
}
