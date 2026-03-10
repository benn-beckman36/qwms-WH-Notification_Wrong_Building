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
    public partial class StorageIn_EC_Document_Compare_OverShipped : Form
    {
        private string strPartno; //料號
        private string strECQty;  //收料數量
        private string strblace;  //剩餘收料數
        private string strQty;    //實收數量
        private string strExQty;    //實收數量
        private string strEXTyp;  //異常類型
        private int intQty;       //溢收數量
        private bool bolOverShipped;  //是否為溢收
        private DataRow drOverShipped; //溢收record

        public StorageIn_EC_Document_Compare_OverShipped(DataRow varOverShipped, int varQty)
        {
            InitializeComponent();

            drOverShipped = varOverShipped;
            strPartno = drOverShipped["MATNR"].ToString();
            strECQty = drOverShipped["MENGE"].ToString();
            strblace = drOverShipped["BLACE"].ToString();//剩餘收料數
            strQty = drOverShipped["BKQTY"].ToString(); 
            strExQty = drOverShipped["EXQTY"].ToString();
            strEXTyp = drOverShipped["EXTYP"].ToString();

            txtPartno.Text = strPartno;
            txtECQty.Text = strECQty;
            txtQty.Text = strblace;
            txtQtyLeft.Text = "0";

            intQty = varQty - Convert.ToInt32(strblace); //計算溢收數量
            txtOverQty.Text =intQty.ToString();

            #region 檢查是否有短裝
            if (strEXTyp == "S")
            {
                btnConfirm.Enabled = false;
            }
            #endregion

        }
        #region btnConfirm_Click(點擊Confirm按鈕)
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtOverRmk.Text.ToString() != "")
            {
                drOverShipped["BLACE"] = "0";
                drOverShipped["EXQTY"] = intQty.ToString(); //溢收數量
                drOverShipped["BKQTY"] = drOverShipped["MENGE"].ToString();
                drOverShipped["EXTYP"] = "O";
                drOverShipped["EXRMK"] = txtOverRmk.Text.ToString() + "(" + intQty.ToString() + ")";
                drOverShipped["SELECT"] = true;

                txtNotice.Text = "Recorded Over Shippment OK!!! ";
                btnConfirm.Enabled = false;
                btnReset.Enabled = false;
            }
            else
            {
                txtNotice.Text = "Please Text Over-Shipped Reason!!!";
            }

        }
        #endregion


        #region btnReset_Click(點擊Reset按鈕)
        private void btnReset_Click(object sender, EventArgs e)
        {
            drOverShipped["SELECT"] = false;
            drOverShipped["BKQTY"] = "0";
            drOverShipped["BLACE"] = drOverShipped["MENGE"].ToString();
            drOverShipped["EXQTY"] = DBNull.Value;
            drOverShipped["EXTYP"] = DBNull.Value;
            drOverShipped["EXRMK"] = DBNull.Value;

            txtNotice.Text = "Recorded Reset OK!!!";
            btnConfirm.Enabled = false;
            btnReset.Enabled = false;
        }
        #endregion
        #region btnReturn_Click(點擊Return按鈕)
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region GetOverShippedInfo(取得溢收訊息)
        public DataRow GetOverShippedInfo()
        {
            return drOverShipped;
        }
        #endregion
    }
}
