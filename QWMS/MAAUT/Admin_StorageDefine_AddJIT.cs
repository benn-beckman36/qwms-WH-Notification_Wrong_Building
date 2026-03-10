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
    public partial class Admin_StorageDefine_AddJIT : Form
    {
        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strStset = "";
        private string strSthgh = "";
        private string strStlen = "";
        private string strStwid = "";
        private string strType = "";
        private string strRemak = "";
        private string strChange = "NEW";
        private DataTable dtStorageData = new DataTable();
        private DataTable dtTemp = new DataTable();
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;

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

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
            }
        }

        public string Stset
        {
            get
            {
                return strStset;
            }
            set
            {
                strStset = value;
            }
        }

        public string Sthgh
        {
            get
            {
                return strSthgh;
            }
            set
            {
                strSthgh = value;
            }
        }

        public string Stlen
        {
            get
            {
                return strStlen;
            }
            set
            {
                strStlen = value;
            }
        }

        public string Stwid
        {
            get
            {
                return strStwid;
            }
            set
            {
                strStwid = value;
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

        public string Remak
        {
            get
            {
                return strRemak;
            }
            set
            {
                strRemak = value;
            }
        }

        public DataTable StorageData
        {
            get
            {
                return dtStorageData;
            }
            set
            {
                dtStorageData = value;
            }
        }

        #endregion

        #region 建構函數
        public Admin_StorageDefine_AddJIT(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strSttyp, string strLotyp, string strStset, DataTable dtStorageData, string strType)
        {
            InitializeComponent();
            DataRow drRow;
            DataRow[] foundRow;
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Sttyp = strSttyp;
            Lotyp = strLotyp;
            Stset = strStset;
            Type = strType;
            Remak = strRemak;
            StorageData = dtStorageData;
            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                if (!objAdmin.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {
                    ShowDdlWerks();
                    txtLgort.Text = Lgort;
                    dtTemp.Rows.Clear();
                    DataColumn[] dcPrimaryKey = new DataColumn[2];
                    dcPrimaryKey[0] = StorageData.Columns["CTRLC2"];
                    dcPrimaryKey[1] = StorageData.Columns["STSET"];
                    StorageData.PrimaryKey = dcPrimaryKey;

                    if (Stset != "" && StorageData.Rows.Count > 0)
                    {
                        strChange = "UPDATE";
                        dtTemp = dtStorageData.Clone();
                        foundRow = StorageData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + Stset + "'");
                        for (int i = 0; i < foundRow.Length; i++)
                        {
                            drRow = dtTemp.NewRow();
                            drRow["MANDT"] = foundRow[0]["MANDT"].ToString();
                            drRow["COMCD"] = foundRow[0]["COMCD"].ToString();
                            drRow["CTRLNM"] = foundRow[0]["CTRLNM"].ToString();
                            drRow["CTRLC2"] = foundRow[0]["CTRLC2"].ToString();
                            drRow["CTRLC4"] = foundRow[0]["CTRLC4"].ToString();
                            drRow["CTRLC5"] = foundRow[0]["CTRLC5"].ToString();
                            drRow["STSET"] = foundRow[0]["STSET"].ToString();
                            drRow["STHGH"] = foundRow[0]["STHGH"].ToString();
                            drRow["STLEN"] = foundRow[0]["STLEN"].ToString();
                            drRow["STWID"] = foundRow[0]["STWID"].ToString();
                            drRow["CRNAM"] = foundRow[0]["CRNAM"].ToString();
                            drRow["CRDAT"] = foundRow[0]["CRDAT"].ToString();
                            drRow["REMAK"] = foundRow[0]["REMAK"].ToString();
                            dtTemp.Rows.Add(drRow);

                            strStset = foundRow[0]["STSET"].ToString();
                            strSthgh = foundRow[0]["STHGH"].ToString();
                            strStlen = foundRow[0]["STLEN"].ToString();
                            strStwid = foundRow[0]["STWID"].ToString();
                            strRemak = foundRow[0]["REMAK"].ToString();
                        }
                        ShowDdlStset();
                        ShowDdlSthgh();
                        ShowDdlStlen();
                        ShowDdlStwid();

                        if (foundRow[0]["REMAK"].ToString() == "JIT")
                        {
                            chkJIT.Checked = true;
                        }
                        else
                        {
                            chkJIT.Checked = false;
                        }
                    }
                    else
                    {
                        if (StorageData.Rows.Count == 0)
                        {
                            strChange = "NEW";
                            dtStorageData = new DataTable();
                            dtStorageData.Columns.Add("MANDT", Type.GetType());
                            dtStorageData.Columns.Add("COMCD", Type.GetType());
                            dtStorageData.Columns.Add("CTRLNM", Type.GetType());
                            dtStorageData.Columns.Add("CTRLC2", Type.GetType());
                            dtStorageData.Columns.Add("CTRLC4", Type.GetType());
                            dtStorageData.Columns.Add("CTRLC5", Type.GetType());
                            dtStorageData.Columns.Add("STSET", Type.GetType());
                            dtStorageData.Columns.Add("STHGH", Type.GetType());
                            dtStorageData.Columns.Add("STLEN", Type.GetType());
                            dtStorageData.Columns.Add("STWID", Type.GetType());
                            dtStorageData.Columns.Add("CRNAM", Type.GetType());
                            dtStorageData.Columns.Add("CRDAT", Type.GetType());
                            dtStorageData.Columns.Add("REMAK", Type.GetType());
                            StorageData = dtStorageData;
                        }
                        ShowDdlStset();
                        ShowDdlSthgh();
                        ShowDdlStlen();
                        ShowDdlStwid();
                    }
                    if (cmbStset.SelectedIndex == -1)
                        cmbStset.SelectedIndex = 0;
                    if (cmbSthgh.SelectedIndex == -1)
                        cmbSthgh.SelectedIndex = 0;
                    if (cmbStlen.SelectedIndex == -1)
                        cmbStlen.SelectedIndex = 0;
                    if (cmbStwid.SelectedIndex == -1)
                        cmbStwid.SelectedIndex = 0;
                    this.cmbWerks.Enabled = false;
                    this.txtLgort.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region 绑定Plant
        private void ShowDdlWerks()
        {
            try
            {
                DataTable dtTemp1 = new DataTable();
                cmbWerks.Items.Clear();
                dtTemp1 = objPlantData.GetDdlWerksData();
                for (int i = 0; i < dtTemp1.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp1.Rows[i]["F_TEXT"].ToString());
                    if (dtTemp1.Rows[i]["F_TEXT"].ToString() == Werks)
                    {
                        cmbWerks.SelectedIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion

        public Admin_StorageDefine_AddJIT()
        {
            InitializeComponent();
        }

        private void ShowDdlStset()
        {
            try
            {
                if (Sttyp.ToUpper() == "TRADITIONAL")
                {
                    for (int i = 1; i <= 1; i++)
                    {
                        cmbStset.Items.Add(i.ToString());
                        if (Stset != "" && i.ToString() == Stset)
                        {
                            cmbStset.SelectedIndex = i - 1;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 99; i++)
                    {
                        cmbStset.Items.Add(i.ToString());
                        if (Stset != "" && i.ToString() == Stset)
                        {
                            cmbStset.SelectedIndex = i - 1;
                        }
                    }
                    if (cmbStset.SelectedIndex == -1)
                    {
                        cmbStset.SelectedIndex = StorageData.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStset()");
            }
        }

        private void ShowDdlSthgh()
        {
            try
            {
                for (int i = 1; i <= 99; i++)
                {
                    cmbSthgh.Items.Add(i.ToString());
                    if (Sthgh != "" && i.ToString() == Sthgh)
                    {
                        cmbSthgh.SelectedIndex = i - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlSthgh()");
            }
        }

        private void ShowDdlStlen()
        {
            try
            {
                for (int i = 1; i <= 99; i++)
                {
                    cmbStlen.Items.Add(i.ToString());
                    if (Stlen != "" && i.ToString() == Stlen)
                    {
                        cmbStlen.SelectedIndex = i - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStlen()");
            }
        }

        private void ShowDdlStwid()
        {
            try
            {
                if (Sttyp.ToUpper() == "TRADITIONAL")
                {
                    for (int i = 1; i <= 99; i++)
                    {
                        cmbStwid.Items.Add(i.ToString());
                        if (Stwid != "" && i.ToString() == Stwid)
                        {
                            cmbStwid.SelectedIndex = i - 1;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        cmbStwid.Items.Add(i.ToString());
                        if (Stwid != "" && i.ToString() == Stwid)
                        {
                            cmbStwid.SelectedIndex = i - 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStwid()");
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow drRow;
                object[] objFind = new object[2];
                //如果是Traditional, stset一定為1
                if (Sttyp.ToUpper() == "TRADITIONAL")
                {
                    if (cmbStset.Items[cmbStset.SelectedIndex].ToString() != "1")
                    {
                        MessageBox.Show("Crane should be 1 because the storage is a traditional storage!!");
                        return;
                    }
                }
                if (Sttyp.ToUpper() == "CARROUSEL")
                {
                    if (Convert.ToInt32(cmbStwid.Items[cmbStwid.SelectedIndex].ToString()) > 9)
                    {
                        MessageBox.Show("Depth should be less than 10 because the storage is a carrousel storage!!");
                        return;
                    }
                }

                if (cmbStset.SelectedIndex == -1 || cmbSthgh.SelectedIndex == -1 || cmbStlen.SelectedIndex == -1 || cmbStwid.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select crane, height, length and width!!");
                    return;
                }

                if (Type == "MODIFY")
                {

                    if (strChange == "UPDATE")
                    {
                        dtTemp.Rows[0]["STHGH"] = this.cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString();
                        dtTemp.Rows[0]["STLEN"] = this.cmbStlen.Items[cmbStlen.SelectedIndex].ToString();
                        dtTemp.Rows[0]["STWID"] = this.cmbStwid.Items[cmbStwid.SelectedIndex].ToString();
                        if (chkJIT.Checked == true)
                        {
                            dtTemp.Rows[0]["REMAK"] = "JIT";
                            strRemak = "JIT";
                        }
                        else
                        {
                            dtTemp.Rows[0]["REMAK"] = "";
                            strRemak = "";
                        }

                        if (objAdmin.ModifyStorage_JIT(Werks, Lgort, Stset, dtTemp))
                        {
                            MessageBox.Show("Update OK!!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Update Faile!!" + objAdmin.ERRMSG);
                            return;
                        }
                    }
                }
                else
                {
                    if (strChange == "NEW")
                    {
                        drRow = StorageData.NewRow();
                        drRow["MANDT"] = Mandt;
                        drRow["COMCD"] = Comcd;
                        drRow["CTRLNM"] = Werks;
                        drRow["CTRLC2"] = Lgort;
                        drRow["CTRLC4"] = Sttyp;
                        drRow["CTRLC5"] = Lotyp;
                        //drRow["REMAK"] = "JIT";
                        drRow["STSET"] = this.cmbStset.Items[cmbStset.SelectedIndex].ToString();
                        drRow["STHGH"] = this.cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString();
                        drRow["STLEN"] = this.cmbStlen.Items[cmbStlen.SelectedIndex].ToString();
                        drRow["STWID"] = this.cmbStwid.Items[cmbStwid.SelectedIndex].ToString();
                        if (chkJIT.Checked == true)
                        {
                            drRow["REMAK"] = "JIT";
                            strRemak = "JIT";
                        }
                        else
                        {
                            drRow["REMAK"] = "";
                            strRemak = "";
                        }

                        StorageData.Rows.Add(drRow);
                    }
                    else
                    {
                        objFind[0] = Lgort;
                        objFind[1] = Stset;
                        drRow = StorageData.Rows.Find(objFind);
                        drRow["STHGH"] = this.cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString();
                        drRow["STLEN"] = this.cmbStlen.Items[cmbStlen.SelectedIndex].ToString();
                        drRow["STWID"] = this.cmbStwid.Items[cmbStwid.SelectedIndex].ToString();
                        StorageData.AcceptChanges();

                    }
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
