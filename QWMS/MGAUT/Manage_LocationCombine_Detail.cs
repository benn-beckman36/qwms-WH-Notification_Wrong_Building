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
    public partial class Manage_LocationCombine_Detail : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private DataTable dtStorageData = new DataTable();
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
        # endregion

        public Manage_LocationCombine_Detail(UserInfo varUserData, string strProgid, string strWerks, string strLgort)
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
    }
}
