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
    public partial class Manage_PalletQtyMaintain : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        public string Progid = "";
        public string WERKS { get; set; }
        public string LGORT { get; set; }
        public string Mblnr { get; set; }
        public string Menge { get; set; }
        public string Alqty { get; set; }
        public string  BalanceQty { get; set; }
        public string Location { get; set; }
        public string type { get; set; }
        # endregion

        public Manage_PalletQtyMaintain(UserInfo userData, string strProgid, string Werks, string Lgort, string strLocation, string strMblnr, string strQty, string strAlqty, string strBalanceQty,string strtype)
        {
            InitializeComponent();
            UserData = userData;
            Progid = strProgid;
            txtMblnr.Text = strMblnr;
            txtMenge.Text = strQty;
            txtAlqty.Text = strAlqty;
            txtBalanceQty.Text = strBalanceQty;
            txtMblnr.Enabled = false;
            txtMenge.Enabled = false;
            txtBalanceQty.Enabled = false;
            WERKS = Werks;
            LGORT = Lgort;
            Location = strLocation;
            type = strtype;
            if (!string.IsNullOrEmpty(Location))
            {
                Location = strLocation;
                txtLocation.Text = strLocation;
                txtLocation.Enabled = false;
            }
            else
            {
                txtLocation.Enabled = true;
            }
            //if (type.ToLower().Contains("combine"))
            //{
            //    labQty.Text = "Out Qty";
            //}
            //else
            //{
            //    labQty.Text = "In Qty";
            //}

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Mblnr = txtMblnr.Text;
                Alqty = txtAlqty.Text;
                Menge = txtMenge.Text;
                BalanceQty = txtBalanceQty.Text;
                Location = txtLocation.Text;

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
                
                if (string.IsNullOrEmpty(Location))
                {
                    MessageBox.Show(" Location can't be empty!!");
                    return;
                }
                this.DialogResult = DialogResult.Yes;
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
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void txtAlqty_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAlqty.Text))
            {
                if ((Convert.ToInt64(txtMenge.Text) >= Convert.ToInt64(txtAlqty.Text)))
                {
                    txtBalanceQty.Text = (Convert.ToInt64(txtMenge.Text) - Convert.ToInt64(txtAlqty.Text)).ToString();
                }
                else
                {
                    MessageBox.Show("Out Qty should be less than Location Qty!!");
                    return;
                }
            }
        }

        private void txtLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (WERKS == "" || LGORT == "")
                {
                    MessageBox.Show("Plant and storage can't be empty!!");
                    return;
                }
                else
                {
                    Manage_LocationSelect objManage_LocationSelect = new Manage_LocationSelect(UserData, Progid, WERKS, LGORT, "ADD");
                    objManage_LocationSelect.ShowDialog();
                    txtLocation.Text = objManage_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
                return;
            }
        }
    }
}
