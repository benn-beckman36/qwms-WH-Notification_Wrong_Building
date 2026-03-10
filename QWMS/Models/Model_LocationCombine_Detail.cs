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

namespace QWMS.Models
{
    public partial class Model_LocationCombine_Detail : Form
    {
        #region Parameters
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private DataTable dtTemp = new DataTable();

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
        #endregion

        public Model_LocationCombine_Detail(UserInfo varUserData, string strProgid, string strWerks, string strLgort)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
        }
        //Confirm event
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                if (string.IsNullOrEmpty(Alqty))
                {
                    MessageBox.Show("Combine Qty can't be empty!!");
                    return;
                }
                try
                {
                    Int64 intAlqty = Int64.Parse(Alqty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Combine Qty must be intergar!!"+ex.Message);
                    return;
                }
                if (Int64.Parse(Alqty) > Int64.Parse(Menge))
                {
                    MessageBox.Show("Combine Qty must be less than Total Qty!!");
                    return;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Messagtge:" + ex.Message);
                return;
            }
        }
        //Return event
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.txtAlqty.Text = "0";
            this.Close();
        }

    }
}
