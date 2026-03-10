using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QWMS
{
    public partial class StorageIn_IQC_InOut_Add : Form
    {
        public DataRow drChangeData;
        string strAlqty, strqctyp;
        bool bolSave=false;
        int intQty;
        public StorageIn_IQC_InOut_Add(DataRow drData)
        {
            InitializeComponent();
            drChangeData = drData;
            this.cmbWerks.Items.Add(drData["WERKS"].ToString());
            this.cmbWerks.SelectedIndex = 0;
            this.cmbLgort.Items.Add(drData["LGORT"].ToString());
            this.cmbLgort.SelectedIndex = 0;
            this.txtLocat.Text = drData["LOCAT"].ToString();
            this.txtMatnr.Text = drData["MATNR"].ToString();
            this.txtWhusr.Text = drData["WHUSR"].ToString();
            this.txtQcusr.Text = drData["QCUSR"].ToString();
            this.txtQtyLeft.Text = drData["ALQTY"].ToString();
            this.txtVbeln.Text = drData["VBELN"].ToString();
            strAlqty = drData["ALQTY"].ToString();

            //顯示轉出庫存的檢驗狀態
            if (drData["QCTYP"].ToString() == "Y")
            {
                cmbType.Items.Add("Inspected");
                cmbType.SelectedIndex = 0;
                strqctyp = "Y";
            }
            else
            {
                cmbType.Items.Add("Not Inspected");
                cmbType.SelectedIndex = 0;
                strqctyp = "";
            }
            

        }
        #region btnReturn_Click(點擊Return按鈕)
        private void btnReturn_Click(object sender, EventArgs e)
        {
            bolSave= false;
            this.Close();
        }
        #endregion

        #region btnConfirm_Click(點擊Confirm按鈕)
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bolSave = true;
            //檢查是否輸入數量
            if (txtQty.Text.ToString().Trim() == "")
            {
                txtNotice.Text = "Please Keyin Qty !!";
                bolSave = false;
            }
            //必須是數值
            try
            {
                intQty=Int32.Parse(txtQty.Text.ToString().Trim());
            }
            catch(Exception ex)
            {
                txtNotice.Text = "Please Keyin Qty Error !!";
                bolSave = false;
            }
            //數量值必須大於等於0
            if (Int32.Parse(txtQty.Text.ToString().Trim()) <= 0)
            {
                txtNotice.Text = "Please Keyin Qty > 0 !!";
                bolSave = false;
            }
            //必須小於庫存數量
            if (Int32.Parse(strAlqty.Trim()) < intQty)
            {
                txtNotice.Text = "Qty '" + txtQty.Text.ToString().Trim() + "' Exceed Inventory '" + drChangeData["ALQTY"].ToString() + "' Qty!!";
                bolSave = false;
            }
            //輸入完成才紀錄
            if (bolSave == true)
            {
                drChangeData["ALQTY"] =Decimal.Parse(txtQty.Text.ToString().Trim());
                drChangeData["REMAK"] = tbxRmk.Text.ToString().Trim();
                drChangeData["CRDAT"] = this.dtpIndat.Value;
                drChangeData["QCTYP"] = strqctyp;
                this.Close();
            }
        }
        #endregion

        #region GetData(取得變動record)
        public DataRow GetData()
        {
            return drChangeData;
        }
        #endregion

    }
}
