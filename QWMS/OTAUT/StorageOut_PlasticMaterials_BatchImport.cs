using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using QCI.QWMS;
using QWMS.Common;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace QWMS
{
    public partial class StorageOut_PlasticMaterials_BatchImport : Form
    {
        private string strDataSource = "";
        DataTable dtData = new DataTable();
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";

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
        public string DataSource
        {
            get
            {
                return strDataSource;
            }
            set
            {
                strDataSource = value;
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

        public StorageOut_PlasticMaterials_BatchImport(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client.Trim();
            Usrnm = UserData.UserId.Trim();
            Comcd = UserData.CompanyCode.Trim();
            Progid = strProgid;
            dtData = Data;

            dtData.Columns.Add("MATNR");
            dtData.Columns.Add("MENGE");
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string[] strData = rtxtDataSource.Lines;
            DataRow drRow;
            for (int i = 0; i < strData.Length; i++)
            {
                string[] strTemp;
                strTemp = strData[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (strTemp.Length != 0)
                {
                    drRow = dtData.NewRow();
                    drRow["MATNR"] = strTemp[0].ToString();
                    drRow["MENGE"] = strTemp[1].ToString();
                    dtData.Rows.Add(drRow);
                }
            }
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
