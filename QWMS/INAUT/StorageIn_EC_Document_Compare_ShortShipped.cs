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
    public partial class StorageIn_EC_Document_Compare_ShortShipped : Form
    {
        private string strPartno; //料號
        private string strECQty;  //收料數量
        private string strblace;  //剩餘收料數
        private string strQty;    //實收數量
        private string strExQty;    //實收數量
        private int intQty;       //短收數量
        private bool bolShortGR;  //是否為短收
        private DataRow drShortGR; //短收record

        public StorageIn_EC_Document_Compare_ShortShipped(DataRow varShortGR)
        {
            InitializeComponent();

            drShortGR = varShortGR;
            strPartno = drShortGR["MATNR"].ToString();
            strECQty =  drShortGR["MENGE"].ToString();
            strblace =  drShortGR["BLACE"].ToString();
            strQty =    drShortGR["BKQTY"].ToString(); //剩餘收料數
            strExQty =  drShortGR["EXQTY"].ToString();

            txtPartno.Text = strPartno;
            txtECQty.Text = strECQty;
            txtQty.Text = strblace;       
        }

        #region btnConfirm_Click(按下Confirm按鈕)
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bolShortGR = true; //短收狀態
            txtNotice2.Text = "";
            txtNotice1.Text = "";

            #region 防呆1: 檢查是否有輸入短收數量
            if (txtShortQty.Text.ToString().Trim() == "")
            {
                Sound.Play(@"Sound\OO.wav");
                txtNotice1.Text = "未輸入短收數量!!";
                bolShortGR = false;
            }
            else
            {
                try
                {
                     intQty = Convert.ToInt32(txtShortQty.Text.ToString().Trim());
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\OO.wav");
                    txtNotice1.Text = "輸入錯誤!!";
                    bolShortGR = false;
                }
               
                if (intQty == 0)
                {
                    Sound.Play(@"Sound\OO.wav");
                    txtNotice1.Text = "輸入錯誤!!";
                    bolShortGR = false;
                }
            }
            #endregion 
            #region 防呆2: 檢查是否有輸入原因
            if (txtShortRmk.Text.ToString().Trim() == "")
            {
                Sound.Play(@"Sound\OO.wav");
                txtNotice2.Text = "未輸入短收原因!!";
                bolShortGR = false;
            }
            #endregion
            #region 防呆3: 檢查輸入短缺數量是否正確(必須小於等於剩餘收料數量)
            if (strExQty == "")
            {
                strExQty = "0";
            }

            int intBalance = Convert.ToInt32(strECQty) - Convert.ToInt32(strQty) - Convert.ToInt32(strExQty);
            if (intQty > intBalance)
            {
                Sound.Play(@"Sound\OO.wav");
                txtNotice1.Text = "短收數量錯誤!!";
                bolShortGR = false;
            }
            #endregion

            if (bolShortGR== true)
            {
                #region 更新剩餘收料數
                intBalance = intBalance - intQty; //剩餘數量-短收數量
                drShortGR["BLACE"] = intBalance.ToString();
                drShortGR["EXQTY"] = (Convert.ToInt32(strExQty) + intQty).ToString(); //累加短收數量
                drShortGR["EXTYP"] = "S";
                drShortGR["EXRMK"] = drShortGR["EXRMK"].ToString() + txtShortRmk.Text.ToString() + "(" + intQty.ToString() + ")"; 

                if (intBalance == 0)
                {
                    drShortGR["SELECT"] = true;
                }
                #endregion
                txtNotice.Text = "Recorded Short Shippment OK!!!";
                btnConfirm.Enabled = false;
                btnReset.Enabled = false;
            }
        }
        #endregion

        #region btnReturn_Click(按下Return按鈕)
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region btnReset_Click(按下Reset按鈕)
        private void btnReset_Click(object sender, EventArgs e)
        {
            drShortGR["SELECT"] = false;
            drShortGR["BKQTY"] = "0";
            drShortGR["BLACE"] = drShortGR["MENGE"].ToString();
            drShortGR["EXQTY"] = "";
            drShortGR["EXTYP"] = "";
            drShortGR["EXRMK"] = "";
        }
        #endregion

        #region GetShortGRInfo(取得短收訊息)
        public DataRow GetShortGRInfo()
        {
            return drShortGR;
        }
        #endregion
    }
}
