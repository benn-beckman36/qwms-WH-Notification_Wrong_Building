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
    public partial class StorageIn_EC_Document_Compare_DoubleCheck : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private DataTable dtData = new DataTable();
        private String strWerks = "";
        private String strLgort = "";
        bool bolresult = false; //是否要重新點收
        #endregion

        public StorageIn_EC_Document_Compare_DoubleCheck(UserInfo varUserData,  string varWerks, string varLgort, DataTable varECDoc)
        {
            InitializeComponent();
            UserData = varUserData;
            strWerks = varWerks;
            strLgort = varLgort;
            dtData = varECDoc;
        }
        #region btnConfirm_Click(點擊確認要重新盤點實收)
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bolresult = true;
            this.Close();
        }
        #endregion
        #region btnReturn_Click(點擊返回)
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
            bolresult = false;
        }
        #endregion
        #region DoubleCheck(回傳主視窗是否重新點收訊息)
        public bool DoubleCheck()
        {
            return bolresult;

        }
        #endregion
    }
}
