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
using System.Collections;
using System.Text.RegularExpressions;

namespace QWMS
{
    public partial class StorageIn_SMT_Return_Add : Form
    {
        #region 變數宣告

        private string strMandt = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strLocat = "";
        private string strMatnr = "";
        private string strIndat = "";
        private string strInsmk = "";
        private string strType = "";
        private string strGrpid = "";
        private string strComcd = "";
        UserInfo UserData = new UserInfo();
        private DataTable dtSapData = new DataTable();
        private DataTable dtSumData = new DataTable();
        private int intMatnrIndex = 0;
        private bool DuplicateMatnr = false;
        ArrayList aryDidno = new ArrayList();

        #endregion

        #region DataMember

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

        public string Locat
        {
            get
            {
                return strLocat;
            }
            set
            {
                strLocat = value;
            }
        }

        public string Indat
        {
            get
            {
                return strIndat;
            }
            set
            {
                strIndat = value;
            }
        }

        public string Matnr
        {
            get
            {
                return strMatnr;
            }
            set
            {
                strMatnr = value;
            }
        }

        public string Insmk
        {
            get
            {
                return strInsmk;
            }
            set
            {
                strInsmk = value;
            }
        }

        public DataTable SapData
        {
            get
            {
                return dtSapData;
            }
            set
            {
                dtSapData = value;
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

        public string Grpid
        {
            get
            {
                return strGrpid;
            }
            set
            {
                strGrpid = value;
            }
        }

        #endregion

        public StorageIn_SMT_Return_Add(UserInfo varUserData, string strProgid, string strWerks, string strLgort, string strLocat, DataTable dtSapData, string strGrpid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Usrnm = UserData.UserId;
            Comcd = UserData.CompanyCode;
            Progid = strProgid;
            Werks = strWerks;
            Lgort = strLgort;
            Locat = strLocat;
            Grpid = strGrpid;
            SapData = dtSapData;
            Type = "NEW";
            try
            {
                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Progid);
                //檢查權限
                if (!objStorageIn.CheckAuthority())
                {
                    MessageBox.Show("You don't have right to use this program!!");
                    this.Close();
                }
                else
                {
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowLocat();
                    ShowGrpid();
                    DataColumn[] dcPrimaryKey = new DataColumn[3];
                    dcPrimaryKey[0] = SapData.Columns["LOCAT"];
                    dcPrimaryKey[1] = SapData.Columns["MATNR"];
                    dcPrimaryKey[2] = SapData.Columns["GRPID"];
                    SapData.PrimaryKey = dcPrimaryKey;

                    if (dtSapData.Rows.Count == 0)
                    {
                        dtSapData = new DataTable();
                        dtSapData.Columns.Add("MANDT", Type.GetType());
                        dtSapData.Columns.Add("COMCD", Type.GetType());
                        dtSapData.Columns.Add("WERKS", Type.GetType());
                        dtSapData.Columns.Add("LGORT", Type.GetType());
                        dtSapData.Columns.Add("LOCAT", Type.GetType());
                        dtSapData.Columns.Add("GRPID", Type.GetType());
                        dtSapData.Columns.Add("DIDNO", Type.GetType());
                        dtSapData.Columns.Add("MATNR", Type.GetType());
                        dtSapData.Columns.Add("INSMK", Type.GetType());
                        dtSapData.Columns.Add("MENGE", Type.GetType());
                        dtSapData.Columns.Add("ALQTY", Type.GetType());
                        dtSapData.Columns.Add("RMAK1", Type.GetType());
                        dtSapData.Columns.Add("INDAT", Type.GetType());
                        SapData = dtSapData;

                        dtSumData = SapData.Clone();
                    }

                    txtAlqty.Text = "0";
                    txtMatnr.Focus();
                    txtAlqty.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                DataTable dtTemp1 = new DataTable();
                cmbWerks.Items.Clear();
                dtTemp1 = objAuthority.CheckPlantAuthority();
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

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                DataTable dtTemp1 = new DataTable();
                cmbLgort.Items.Clear();
                dtTemp1 = objPlantData.GetDdlLgortData();
                for (int i = 0; i < dtTemp1.Rows.Count; i++)
                {
                    cmbLgort.Items.Add(dtTemp1.Rows[i]["F_TEXT"].ToString());
                    if (dtTemp1.Rows[i]["F_TEXT"].ToString() == Lgort)
                    {
                        cmbLgort.SelectedIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region ShowLocat
        private void ShowLocat()
        {
            cmbLocat.Items.Add(Locat);
            this.cmbLocat.SelectedIndex = 0;
        }
        #endregion

        #region ShowGrpid
        private void ShowGrpid()
        {
            cmbGrpid.Items.Add(Grpid);
            this.cmbGrpid.SelectedIndex = 0;
        }
        #endregion

        #region btnConfirm_Click
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string strTempMblnrMatnr = "";
                DataRow drRow;
                object[] objFind = new object[3];
                if (Locat == "")
                {
                    MessageBox.Show("儲位不能為空白!!");
                    this.cmbLocat.Focus();
                    return;
                }
                //料號不可空白
                if (this.txtMatnr.Text.Trim() == "")
                {
                    MessageBox.Show("料號不能為空白!!");
                    this.txtMatnr.Focus();
                    return;
                }

                //檢查料號是否存在
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                if (!objPlantData.CheckExistedMatnr(this.txtMatnr.Text.Trim()))
                {
                    MessageBox.Show("料號: " + this.txtMatnr.Text.Trim() + " 不存在，請確認!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //數量不可空白或0
                if (!CheckIsNumber(this.txtAlqty.Text.Trim()) || this.txtAlqty.Text.Trim() == "0")
                {
                    MessageBox.Show("數量需為數字或是大於0，請確認!!");
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    return;
                }

                //數量需為數字
                if (!IsNumeric(txtAlqty.Text.Trim()))
                {
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    throw new Exception("數量需為數字，請確認!!");
                }

                //檢查Didno需大於11碼
                if (txtDidno.Text.Trim() != "")
                {
                    if (txtDidno.Text.Trim().Length < 12)
                    {
                        MessageBox.Show("輸入的Didno需大於11碼，請確認!!");
                        txtDidno.Focus();
                        return;
                    }
                }

                //檢查料號需等於11碼
                if (txtMatnr.Text.Trim().Length != 11)
                {
                    MessageBox.Show("輸入的料號需等於11碼，請確認!!");
                    txtMatnr.Focus();
                    return;
                }

                //檢查料號資料有沒有重複，有重複的話自動加總數量
                object[] obj = new object[SapData.Columns.Count];
                StorageIn_SMT_Return objStorageIn_SMT_Return = new StorageIn_SMT_Return(ref UserData, Progid);
                for (int i = 0; i < SapData.Rows.Count; i++)
                {
                    if (strTempMblnrMatnr.IndexOf(SapData.Rows[i]["LOCAT"].ToString() + SapData.Rows[i]["MATNR"].ToString() + ";") == -1)
                    {
                        strTempMblnrMatnr += SapData.Rows[i]["LOCAT"].ToString() + SapData.Rows[i]["MATNR"].ToString() + ";";
                    }
                }

                if (strTempMblnrMatnr.IndexOf(strLocat + txtMatnr.Text.Trim() + ";") != -1)
                {
                    DuplicateMatnr = true;  //flag:料號重複
                    MessageBox.Show("輸入的料號有重複，已將數量加總!!");
                }

                drRow = SapData.NewRow();
                drRow["MANDT"] = Mandt;
                drRow["COMCD"] = Comcd;
                drRow["WERKS"] = Werks;
                drRow["LGORT"] = Lgort;
                drRow["LOCAT"] = Locat;
                drRow["MATNR"] = this.txtMatnr.Text.Trim();
                drRow["MENGE"] = "0";
                drRow["INSMK"] = "G";
                drRow["ALQTY"] = this.txtAlqty.Text.Trim();
                drRow["GRPID"] = this.cmbGrpid.SelectedItem.ToString();
                drRow["DIDNO"] = this.txtDidno.Text.Trim();
                drRow["RMAK1"] = this.txtRmak1.Text.Trim();
                drRow["INDAT"] = this.dtpIndat.Value.ToString("yyyyMMdd");
                SapData.Rows.Add(drRow);

                if (DuplicateMatnr == false)  //料號未重複
                {
                    //複製一整列到dtSumData
                    SapData.Rows[0].ItemArray.CopyTo(obj, 0);
                    dtSumData.Rows.Add(obj);
                    intMatnrIndex++;
                }
                else if (DuplicateMatnr == true)  //料號重複
                {
                    //將重複的料號之數量作加總
                    for (int i = 0; i < dtSumData.Rows.Count; i++)
                    {
                        if (dtSumData.Rows[i]["MATNR"].ToString() == SapData.Rows[intMatnrIndex]["MATNR"].ToString())
                        {
                            objStorageIn_SMT_Return.Data.Rows[i]["ALQTY"] = int.Parse(dtSumData.Rows[i]["ALQTY"].ToString()) + int.Parse(SapData.Rows[intMatnrIndex]["ALQTY"].ToString());
                            SapData.Rows[intMatnrIndex].Delete();  //刪除重複的料號資料列
                            DuplicateMatnr = false;
                            objStorageIn_SMT_Return.Data.AcceptChanges();
                            break;
                        }
                    }
                }
                //複製DataTable
                dtSumData.Clear();
                dtSumData = SapData.Copy();

                objStorageIn_SMT_Return.Data = SapData;
                objStorageIn_SMT_Return.ShowDataGrid();
                this.txtDidno.Text = "";
                this.txtMatnr.Text = "";
                this.txtRmak1.Text = "";
                this.txtAlqty.Text = "0";
                if (chkReturn.Checked == true)
                {
                    this.txtDidno.Focus();
                }
                else
                {
                    this.txtMatnr.Focus();
                }
                ShowLocat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region btnDelete_Click
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow drRow;
                object[] objFind = new object[3];
                objFind[0] = Locat;
                objFind[1] = Matnr;
                objFind[2] = Grpid;
                drRow = SapData.Rows.Find(objFind);
                SapData.Rows.Remove(drRow);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        #region btnReturn_Click
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region chkReturn_CheckedChanged
        private void chkReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReturn.Checked == true)
            {
                txtDidno.Text = "";
                txtMatnr.Text = "";
                txtMatnr.Enabled = false;
                txtDidno.Enabled = true;
                txtDidno.Focus();
            }
            else
            {
                txtDidno.Text = "";
                txtMatnr.Text = "";
                txtDidno.Enabled = false;
                txtMatnr.Enabled = true;
                txtMatnr.Focus();
            }
        }
        #endregion

        #region txtDidno_KeyDown
        private void txtDidno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    //Didno不能重複刷入
                    if (aryDidno.IndexOf(this.txtDidno.Text.Trim().ToString()) < 0)
                    {
                        aryDidno.Add(this.txtDidno.Text.Trim().ToString());
                    }
                    else
                    {
                        throw new Exception("刷入的Did No.已重複，請確認!!");
                    }

                    if (txtDidno.Text.Trim().Length < 12)
                    {
                        throw new Exception("Did No.長度需大於11碼，請確認!!");
                    }

                    Sound.Play(@"Sound\OK.wav");
                    txtMatnr.Text = txtDidno.Text.Substring(0, 11);
                    txtAlqty.Focus();
                    txtAlqty.SelectAll();
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\OO.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtDidno.Focus();
                    this.txtDidno.SelectAll();
                    return;
                }
            }
        }
        #endregion

        #region txtMatnr_KeyDown
        private void txtMatnr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                    //檢查料號是否存在
                    if (!objPlantData.CheckExistedMatnr(this.txtMatnr.Text.Trim()))
                    {
                        this.txtMatnr.Focus();
                        throw new Exception("該料號不存在，請確認!!");
                    }

                    if (txtMatnr.Text.Trim().Length != 11)
                    {
                        this.txtMatnr.Focus();
                        throw new Exception("料號長度需為11碼，請確認!!");
                    }

                    Sound.Play(@"Sound\OK.wav");
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\OO.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtMatnr.Focus();
                    this.txtMatnr.SelectAll();
                    return;
                }
            }
        }
        #endregion

        #region txtAlqty_KeyDown
        private void txtAlqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                try
                {
                    //數量不可空白或0
                    if (!CheckIsNumber(this.txtAlqty.Text.Trim()) || this.txtAlqty.Text.Trim() == "0")
                    {
                        this.txtAlqty.Focus();
                        this.txtAlqty.SelectAll();
                        throw new Exception("數量需為數字或是大於0，請確認!!");
                    }
                    //數量需為數字
                    if (!IsNumeric(txtAlqty.Text.Trim()))
                    {
                        this.txtAlqty.Focus();
                        this.txtAlqty.SelectAll();
                        throw new Exception("數量需為數字，請確認!!");
                    }
                    //數量必須小於5位數
                    if (txtAlqty.Text.Length > 6)
                    {
                        this.txtAlqty.Focus();
                        this.txtAlqty.SelectAll();
                        throw new Exception("數量不得大於5位數，請確認!!");
                    }

                    Sound.Play(@"Sound\OK.wav");
                    btnConfirm_Click(null, null);
                }
                catch (Exception ex)
                {
                    Sound.Play(@"Sound\OO.wav");
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtAlqty.Focus();
                    this.txtAlqty.SelectAll();
                    return;
                }
            }
        }
        #endregion

        #region CheckIsNumber
        private bool CheckIsNumber(string strValue)
        {
            Regex rgxNumber = new Regex("[0-9]");
            return rgxNumber.IsMatch(strValue);
        }
        #endregion

        #region IsNumeric
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        #endregion
    }
}
