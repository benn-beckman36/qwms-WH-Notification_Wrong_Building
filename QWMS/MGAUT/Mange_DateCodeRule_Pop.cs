using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.Text.RegularExpressions;

namespace QWMS
{
    public partial class Mange_DateCodeRule_Pop : Form
    {

        #region 定义变量

        UserInfo UserData = new UserInfo();
        DataTable dtData = new DataTable();
        private StorageData objStorageData;
        //string[] name = { "Vendor", "DateCode", "DateCode(After Transfer)" };
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strType = "";
        private string strDateCode = "";
        private string strLifnr = "";
        private string strDcAfter = "";

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
        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }
        public DataTable dtData_Pop
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

        public string DcAfter
        {
            get { return strDcAfter; }
            set { strDcAfter = value; }
        }

        public string Lifnr
        {
            get { return strLifnr; }
            set { strLifnr = value; }
        }

        public string DateCode
        {
            get { return strDateCode; }
            set { strDateCode = value; }
        }

        #endregion

        #region 构造函数

        public Mange_DateCodeRule_Pop(UserInfo varUserData, string strProgid, DataTable dtData_temp, string strType)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Type = strType;
            objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);

            dtData = dtData_temp;
           


        }

        #region 空的构造函数
        public Mange_DateCodeRule_Pop(UserInfo varUserData, string strProgid, string strType, string strLifnr, string strDateCode)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Type = strType;
            Lifnr = strLifnr;
            DateCode = strDateCode;
            txtDatecode.Text = strDateCode;
            txtVendor.Text = strLifnr;
            objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);
        }
        #endregion


        #endregion

        #region Confirm

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVendor.Text) && string.IsNullOrEmpty(txtDatecode.Text) && string.IsNullOrEmpty(txtDatecodeAfter.Text))
                {
                    MessageBox.Show("Vendor、 DateCode and DateCode(After Transfer) can't be empty!!");
                }
                if (!Regex.IsMatch(txtDatecodeAfter.Text.Trim().ToString(), "^(\\d{2,4})/(\\d{1,2})/(\\d{1,2})"))
                {
                    MessageBox.Show("请按照正确格式填写转化后日期");
                    return;
                }
                if (Type == "ADD")
                {
                    object[] rowArray = new object[3];
                    DataRow Temp = dtData.NewRow();
                    rowArray[0] = txtVendor.Text.Trim();
                    rowArray[1] = txtDatecode.Text.Trim();
                    rowArray[2] = txtDatecodeAfter.Text.Trim();
                    Temp.ItemArray = rowArray;
                    dtData.Rows.Add(Temp);
                    this.Close();
                }

                if (Type == "UPDATE" || Type == "NEW")
                {
                    DataTable dtNewDateCode = new DataTable();
                    dtNewDateCode.Columns.Add("LIFNR");
                    dtNewDateCode.Columns.Add("DC_Before");
                    dtNewDateCode.Columns.Add("DC_After");

                    DataRow dr = dtNewDateCode.NewRow();
                    dr["LIFNR"] = txtVendor.Text.Trim().ToString();
                    dr["DC_Before"] = txtDatecode.Text.Trim().ToString();
                    dr["DC_After"] = txtDatecodeAfter.Text.Trim().ToString();
                    dtNewDateCode.Rows.Add(dr.ItemArray);
                    objStorageData.WHDCR_DML(dtNewDateCode, Type);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region Cancel

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

     

    }
}
