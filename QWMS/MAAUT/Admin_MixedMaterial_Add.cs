using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using QWMS.Common;
using QCI.QWMS;


namespace QWMS
{
    public partial class Admin_MixedMaterial_Add : Form
    {
        # region 变量声明
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strMatnr1 = "";
        private string strMatnr2 = "";
        private string strMatnr3 = "";
        private string strMatnr4 = "";
        private string strAddType = "";
        private DataTable dtNewMixedMaterial;
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

        public string Matnr1
        {
            get
            {
                return strMatnr1;
            }
            set
            {
                strMatnr1 = value;
            }
        }

        public string Matnr2
        {
            get
            {
                return strMatnr2;
            }
            set
            {
                strMatnr2 = value;
            }
        }

        public string Matnr3
        {
            get
            {
                return strMatnr3;
            }
            set
            {
                strMatnr3 = value;
            }
        }

        public string Matnr4
        {
            get
            {
                return strMatnr4;
            }
            set
            {
                strMatnr4 = value;
            }
        }


        public DataTable NewMixedMaterial
        {
            get
            {
                return dtNewMixedMaterial;
            }
            set
            {
                dtNewMixedMaterial = value;
            }
        }

        public string AddType
        {
            get
            {
                return strAddType;
            }
            set
            {
                strAddType = value;
            }
        }
        # endregion

        public Admin_MixedMaterial_Add()
        {
            InitializeComponent();
        }

        public Admin_MixedMaterial_Add(UserInfo varUserData, string strProgid, string strWerks, string strAddType)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            Werks = strWerks;
            AddType = strAddType;
            txtMatnr1.Text = "";
            txtMatnr2.Text = "";
            txtMatnr3.Text = "";
            txtMatnr4.Text = "";

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);

                //檢查權限
                if (!objAdmin.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {
                    //廠區資料
                    ShowDdlWerks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        # region SetMaterialData
        public void SetMaterialData()
        {
            txtMatnr1.Text = Matnr1;
            txtMatnr2.Text = Matnr2;
            txtMatnr3.Text = Matnr3;
            txtMatnr4.Text = Matnr4;
        }
        # endregion

        # region 带出下拉列表资料
        private void ShowDdlWerks()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    if (dtTemp.Rows[i]["F_TEXT"].ToString() == Werks)
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
        # endregion

        # region GetMaterialData
        private void GetMaterialData()
        {
            DataTable objTable = new DataTable();
            DataRow objRow;
            try
            {
                objTable.Columns.Add("MATNR", Type.GetType("System.String"));
                if (Matnr1 != "")
                {
                    objRow = objTable.NewRow();
                    objRow["MATNR"] = Matnr1;
                    objTable.Rows.Add(objRow);
                }
                if (Matnr2 != "")
                {
                    objRow = objTable.NewRow();
                    objRow["MATNR"] = Matnr2;
                    objTable.Rows.Add(objRow);
                }
                if (Matnr3 != "")
                {
                    objRow = objTable.NewRow();
                    objRow["MATNR"] = Matnr3;
                    objTable.Rows.Add(objRow);
                }
                if (Matnr4 != "")
                {
                    objRow = objTable.NewRow();
                    objRow["MATNR"] = Matnr4;
                    objTable.Rows.Add(objRow);
                }

                NewMixedMaterial = objTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-GetMaterialData()");
            }
        }
        # endregion

        # region Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                GetMaterialData();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        # endregion

        # region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int intCount = 0;
            string strTempMatnr = "";
            bool bolDuplicate = false;
            try
            {
                Matnr1 = this.txtMatnr1.Text.Trim();
                Matnr2 = this.txtMatnr2.Text.Trim();
                Matnr3 = this.txtMatnr3.Text.Trim();
                Matnr4 = this.txtMatnr4.Text.Trim();
                
                if (Matnr1 != "")
                {
                    if (!objPlantData.CheckExistedMatnr(Matnr1))
                    {
                        MessageBox.Show(Matnr1 + " doesn't exist!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    intCount++;
                    if (strTempMatnr.IndexOf(Matnr1) == -1)
                    {
                        strTempMatnr += Matnr1 + ";";
                    }
                    else
                    {
                        bolDuplicate = true;
                    }
                }
                if (Matnr2 != "")
                {
                    if (!objPlantData.CheckExistedMatnr(Matnr2))
                    {
                        MessageBox.Show(Matnr2 + " doesn't exist!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    intCount++;
                    if (strTempMatnr.IndexOf(Matnr2) == -1)
                    {
                        strTempMatnr += Matnr2 + ";";
                    }
                    else
                    {
                        bolDuplicate = true;
                    }
                }
                if (Matnr3 != "")
                {
                    if (!objPlantData.CheckExistedMatnr(Matnr3))
                    {
                        MessageBox.Show(Matnr3 + " doesn't exist!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    intCount++;
                    if (strTempMatnr.IndexOf(Matnr3) == -1)
                    {
                        strTempMatnr += Matnr3 + ";";
                    }
                    else
                    {
                        bolDuplicate = true;
                    }
                }
                if (Matnr4 != "")
                {
                    if (!objPlantData.CheckExistedMatnr(Matnr4))
                    {
                        MessageBox.Show(Matnr4 + " doesn't exist!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    intCount++;
                    if (strTempMatnr.IndexOf(Matnr4) == -1)
                    {
                        strTempMatnr += Matnr4 + ";";
                    }
                    else
                    {
                        bolDuplicate = true;
                    }
                }

                //如果有填寫的料號低於兩個
                if (intCount < 2)
                {
                    MessageBox.Show("Please at leat input two Part No!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (bolDuplicate == true)
                {
                    MessageBox.Show("The Part No you input can't be duplicate!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GetMaterialData();
                AddType = "MODIFY";
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        # endregion

        # region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.txtMatnr1.Text = "";
            this.txtMatnr2.Text = "";
            this.txtMatnr3.Text = "";
            this.txtMatnr4.Text = "";
            this.Matnr1 = "";
            this.Matnr2 = "";
            this.Matnr3 = "";
            this.Matnr4 = "";
        }
        # endregion
    }
}
