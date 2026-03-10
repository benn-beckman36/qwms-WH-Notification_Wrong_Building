using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI_QWMS_StorageData;
using System.Collections;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class AddTruckOrder : Form
    {
        UserInfo UserData = new UserInfo();

        string strTruckOrder = "";
        public string StrTruckOrder
        {
            get { return strTruckOrder; }
            set { strTruckOrder = value; }
        }

        string strCarNo = "";
        public string StrCarNo
        {
            get { return strCarNo; }
            set { strCarNo = value; }
        }

        string strDriver = "";
        public string StrDriver
        {
            get { return strDriver; }
            set { strDriver = value; }
        }

        string strSendTime = "";
        public string StrSendTime
        {
            get { return strSendTime; }
            set { strSendTime = value; }
        }

        string strDport = "";
        public string StrDport
        {
            get { return strDport; }
            set { strDport = value; }
        }

        string strFport = "";
        public string StrFport
        {
            get { return strFport; }
            set { strFport = value; }
        }

        private Admin objAdmin;
        private CarData objCarData;
        private Authority objAuthority;
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";

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

        DataTable dtData = new DataTable();
        string strTransferID = "";

        public AddTruckOrder(UserInfo varUserData, string truckOrder, string carNo, string driver, string sendtime, string dport, string fport)
        {
            InitializeComponent();
            StrTruckOrder = truckOrder;
            StrCarNo = carNo;
            StrFport = fport;
            StrDport = dport;
            StrDriver = driver;
            StrSendTime = sendtime;
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;

            try
            {
                objCarData = new CarData(UserData);
                objAuthority = new Authority(UserData);
                ShowDataGridView();
                Query();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        #region ShowDataGridView
        private void ShowDataGridView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewCheckBoxColumn dgvSelect = new DataGridViewCheckBoxColumn();
                dgvSelect.DataPropertyName = "Select";
                dgvSelect.HeaderText = "选择";
                dgvSelect.ReadOnly = false;
                dgvSelect.Selected = false;
                dgvSelect.Width = 50;
                this.gvData.Columns.Add(dgvSelect);


                DataGridViewTextBoxColumn dgvcOrder = new DataGridViewTextBoxColumn();
                dgvcOrder.DataPropertyName = "ORDERNO";
                dgvcOrder.HeaderText = "派车单号";
                dgvcOrder.ReadOnly = true;
                dgvcOrder.Width = 120;
                this.gvData.Columns.Add(dgvcOrder);

                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "Item";
                dgvcItem.HeaderText = "Item";
                dgvcItem.ReadOnly = true;
                dgvcItem.Width = 40;
                this.gvData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcTransferID = new DataGridViewTextBoxColumn();
                dgvcTransferID.DataPropertyName = "MBLNR";
                dgvcTransferID.HeaderText = "调拨单号";
                dgvcTransferID.ReadOnly = true;
                dgvcTransferID.Width = 120;
                this.gvData.Columns.Add(dgvcTransferID);

                DataGridViewTextBoxColumn dgvcSealNo = new DataGridViewTextBoxColumn();
                dgvcSealNo.DataPropertyName = "SEALNO";
                dgvcSealNo.HeaderText = "封条号";
                dgvcSealNo.ReadOnly = true;
                dgvcSealNo.Width = 70;
                this.gvData.Columns.Add(dgvcSealNo);

               
                this.gvData.DataSource = dtData;
                gvData.ClearSelection();
                gvData.AllowUserToAddRows = false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }

        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSealNo = txtSealNo.Text;
            ArrayList failTID = new ArrayList();
            if (strSealNo == ""||strSealNo.Length!=7)
            {
                stsWaring.Text = "封条号不能为空，并且只能为7码！";
                return;
            }
            else
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if ((bool)dtData.Rows[i]["Select"] == true)
                    {
                        strTransferID = dtData.Rows[i]["MBLNR"].ToString().Trim();

                        if (dtData.Rows[i]["STATUS"].ToString() == "WP")
                        {
                            bool flg = objCarData.AddToBindTransferDataForCarInfo(strTruckOrder, strTransferID, strSealNo, StrSendTime, strCarNo, StrDriver);//将填好的信息与调拨单绑定 
                            if (!flg)
                            {
                                failTID.Add(strTransferID);
                            }
                        }
                        else
                        {
                            failTID.Add(strTransferID);
                        }
                    }
                }
                if (failTID.Count != 0)
                {
                    string sb = "";
                    foreach (string id in failTID)
                    {
                        sb += id + ",";
                    }
                    stsWaring.Text = "调拨单号：" + sb + "追加失败，请确认！";
                }
                else
                {
                    stsWaring.Text = "追加成功";
                }

            }
        }

        #region Query
        public void Query()
        {

            dtData = objCarData.QueryBindPortInfo("", "","",StrFport, StrDport, "", "", "");
            if (dtData.Rows.Count <= 0)
            {
                stsWaring.Text = "没有可追加的调拨单号！";
                return;
            }
            DataColumn cSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(cSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }
            ShowDataGridView();
        }
        #endregion
    }
}
