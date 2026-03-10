using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QCI_QWMS_StorageOut;
using QWMS.Common;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace QWMS
{
    public partial class StorageOut_AddDoc : Form
    {
        #region 变数宣告

        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strProgid = "";
        private string strMblnr = "";
        private string strType = "";
        string strZeile = "";

        UserInfo UserData = new UserInfo();
        AddDoc objAddDoc;
        Authority objAuthority;
        DataTable dtData = new DataTable();
        DataTable dtAddDocToSAP;
  
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

        public string  Mblnrs
        {
            get
            {
                return strMblnr;
            }
            set
            {
                strMblnr = value;
            }
        }

        public string StrZeile
        {
            get { return strZeile; }
            set { strZeile = value; }
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

   
        #endregion

#endregion

        public StorageOut_AddDoc(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageOut StorageOut = new QCI.QWMS.StorageOut(UserData, Progid);
                objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);
                objAddDoc = new AddDoc(UserData);

                //檢查權限
                if (!StorageOut.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    Query();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #region 設定State Bar中的日期
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsComcd.Text = Comcd;
            this.stsUsrnm.Text = Usrnm;
        }
        #endregion

        private void ShowDdlWerks()
        {
            Authority objAuthority = new Authority(UserData);
            DataTable dtTemp = new DataTable();
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();
                if (dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                    }
                    //this.cmbWerks.SelectedIndex = 0;
                    //cmbWerks.SelectedIndex = 0;
                }
                else
                {
                    cmbWerks.Items.Clear();
                    strWerks = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

      
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (dtTemp.Rows.Count > 0)
                {
                    cmbLgort.Items.Clear();
                    cmbLgort.Items.Add("");
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        if (!cmbLgort.Items.Contains(dtTemp.Rows[i]["F_TEXT"].ToString()))
                        {
                            cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        }
                    }
                }
                else
                {
                    cmbLgort.Items.Clear();
                    strLgort = "";
                }
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void ShowDataGrid()
        {
            try
            {

            dgvData.AutoGenerateColumns = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvSelect = new DataGridViewCheckBoxColumn();
                dgvSelect.DataPropertyName = "cSelect";
                dgvSelect.HeaderText = "选择";
                dgvSelect.ReadOnly = false;
                dgvSelect.Selected = false;
                dgvSelect.Width = 50;
                this.dgvData.Columns.Add(dgvSelect);

                //MBLNR
                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "扣账编号";
                dgvcMblnr.Width = 120;
                dgvcMblnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMblnr);

                //ZEILE
                DataGridViewTextBoxColumn dgvcZeile = new DataGridViewTextBoxColumn();
                dgvcZeile.DataPropertyName = "ZEILE";
                dgvcZeile.HeaderText = "Item";
                dgvcZeile.Width = 90;
                dgvcZeile.ReadOnly = true;
                dgvData.Columns.Add(dgvcZeile);

                //MATNR
                DataGridViewTextBoxColumn dgvcMatnr = new DataGridViewTextBoxColumn();
                dgvcMatnr.DataPropertyName = "MATNR";
                dgvcMatnr.HeaderText = "料号";
                dgvcMatnr.Width = 120;
                dgvcMatnr.ReadOnly = true;
                dgvData.Columns.Add(dgvcMatnr);

                //CHARG
                DataGridViewTextBoxColumn dgvcCharg = new DataGridViewTextBoxColumn();
                dgvcCharg.DataPropertyName = "CHARG";
                dgvcCharg.HeaderText = "版本";
                dgvcCharg.Width = 90;
                dgvcCharg.ReadOnly = true;
                dgvData.Columns.Add(dgvcCharg);

                //LIFNR
                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "出库仓别";
                dgvcLgort.Width = 90;
                dgvcLgort.ReadOnly = true;
                dgvData.Columns.Add(dgvcLgort);

                //MENGE
                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "出库数量";
                dgvcMenge.Width = 90;
                dgvcMenge.ReadOnly = true;
                dgvData.Columns.Add(dgvcMenge);


                //KOSTL
                DataGridViewTextBoxColumn dgvcKostl = new DataGridViewTextBoxColumn();
                dgvcKostl.DataPropertyName = "KOSTL";
                dgvcKostl.HeaderText = "CostCenter";
                dgvcKostl.Width = 90;
                dgvcKostl.ReadOnly = true;
                dgvData.Columns.Add(dgvcKostl);


                //UMLGO
                DataGridViewTextBoxColumn dgvcUmlgo = new DataGridViewTextBoxColumn();
                dgvcUmlgo.DataPropertyName = "UMLGO";
                dgvcUmlgo.HeaderText = "目的仓别";
                dgvcUmlgo.Width = 90;
                dgvcUmlgo.ReadOnly = true;
                dgvData.Columns.Add(dgvcUmlgo);

                //Sequence No.
                DataGridViewTextBoxColumn dgvcAddQTY = new DataGridViewTextBoxColumn();
                dgvcAddQTY.DataPropertyName = "AddQTY";
                dgvcAddQTY.HeaderText = "加扣数量";
                dgvcAddQTY.Name = "CAddQty";
                dgvcAddQTY.Width = 100;
                dgvcAddQTY.ReadOnly = false;
                dgvData.Columns.Add(dgvcAddQTY);

                //扣账时间
                DataGridViewTextBoxColumn dgvcCradt = new DataGridViewTextBoxColumn();
                dgvcCradt.DataPropertyName = "CRDAT";
                dgvcCradt.HeaderText = "扣账时间";
                dgvcCradt.Width = 180;
                dgvcCradt.ReadOnly = true;
                dgvData.Columns.Add(dgvcCradt);


            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cmbWerks.Items[cmbWerks.SelectedIndex].ToString()=="")
            {
                stsWarning.Text = "请选择厂区！";
                return;
            }
            Query();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strLgort = txtDlgort.Text;
            if (cmbWerks.SelectedIndex == -1)
            {
                stsWarning.Text = "请选择厂区";
                return;
            }

            if (dtData.Rows.Count > 0)
            {
                string strDLgort=txtDlgort.Text.ToString().Trim();
                try
                {

                    #region 校验是否有261扣帐程式
                    DataRow[] drSelect = dtData.Select("cSelect='True' AND BWART='261'  ");

                    if (drSelect.Length > 0)
                    {
                        if (strDLgort == "" && strDLgort.Length!=4)
                        {
                            stsWarning.Text = drSelect[0]["MBLNR"].ToString() + " 为261扣帐，请维护4码目的仓别";
                            return;
                        }
                    }
                   
                    #endregion

                    #region 循环获取数据并写入到数据库
                    GetDataTableToSAP();//初始化要传给SAP 的DataTable
                    string strMblnrNew = dtData.Rows[0]["WERKS"].ToString() + DateTime.Now.ToString("yyyyMMddHHmmss");
                    int j = 0;
                    DataRow [] drSelects =dtData.Select("cSelect='True'");
                    if (drSelects.Length > 0)
                    {
                        foreach (DataRow dr in drSelects)
                        {
                            j = j + 1;
                            string a = dr["AddQTY"].ToString();
                            //string a = this.dgvData.Rows[i].Cells["CAddQty"].Value.ToString();
                            if (a != "0")
                            {
                                #region 将需要加扣的信息填到加扣表
                                strMblnr = dr["MBLNR"].ToString();
                                strZeile = (j).ToString().PadLeft(4, '0');
                                if (dr["BWART"].ToString() == "261")
                                {
                                    strDLgort = txtDlgort.Text;
                                }
                                else
                                {
                                    strDLgort = dr["UMLGO"].ToString();
                                }
                                DataRow drN = dtAddDocToSAP.NewRow();
                                drN["MANDT"] = dr["MANDT"].ToString();
                                drN["ZAPPID"] = strMblnrNew;
                                drN["ZITEM"] = strZeile;
                                drN["WERKS"] = dr["WERKS"].ToString();
                                drN["LGORT"] = dr["LGORT"].ToString();
                                drN["WERKS_I"] = dr["WERKS"].ToString();
                                drN["LGORT_I"] = strDLgort;
                                drN["MATNR"] = dr["MATNR"].ToString();
                                drN["MENGE"] = dr["AddQty"].ToString();
                                drN["MBLNR"] = "";
                                drN["MJAHR"] = "";
                                drN["FLAG"] = "";
                                drN["MESSAGE"] = "";
                                drN["TXDAT"] = "";
                                drN["TXTM"] = "";
                                drN["TXEMP"] = UserData.UserId;
                                //dr["KOSTL"] = dtData.Rows[i]["KOSTL"].ToString();
                                if (dr["KOSTL"].ToString().Length == 4)
                                {
                                    drN["KOSTL"] = "";
                                }
                                else
                                {
                                    drN["KOSTL"] = dr["KOSTL"].ToString();
                                }
                                drN["CHARG"] = dr["CHARG"].ToString();
                                drN["BWART"] = "311";
                                drN["AUFNR"] = "";
                                dtAddDocToSAP.Rows.Add(drN);
                                
                                objAddDoc.InsertAddDoc(strMblnrNew, strMblnr, strZeile, strDLgort, Convert.ToInt32(a));
                                objAddDoc.InsertAddDocLog(strMblnr, "BEGIN", "OK", "");
                                #endregion

                            }
                            else
                            {
                                stsWarning.Text = strMblnr + "请输入加扣数量，且加扣数量为数值";
                                return;
                            }
                        }
                    }
                    else
                    {
                        stsWarning.Text = "请选择行";
                        return;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.Message;
                    return;
                }
            }
            if (dtAddDocToSAP.Rows.Count > 0 && dtAddDocToSAP != null)
            {
                DialogResult result = new DialogResult();
                result = MessageBox.Show("确认加扣？", "QWMS加扣", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    for (int i = 0; i < dtAddDocToSAP.Rows.Count; i++)
                    {
                        string OMBLN=objAddDoc.getWhdwn_Add(dtAddDocToSAP.Rows[i]["ZAPPID"].ToString(), dtAddDocToSAP.Rows[i]["ZITEM"].ToString());
                        objAddDoc.InsertAddDocLog(OMBLN,"SAP","OK","");
                    }
                    #region 加扣信息同步给SAP
                    DataSet dsData = new DataSet();
                    dsData.Tables.Add(dtAddDocToSAP);
                    MM.MM_Service obj = new QWMS.MM.MM_Service();
                    DataSet dsResultFromSAP = obj.Z_MM_RFC_POSTYCN("A", dsData);
                    DataTable dtResultFromSAP = dsResultFromSAP.Tables[0];
                    //DataTable dtResultFromSAP = dtAddDocToSAP;//测试
                    stsWarning.Text = "";
                    if (dtResultFromSAP.Rows.Count > 0)
                    {
                        DataTable dtSuccess = dtResultFromSAP.Clone();
                        DataRow[] drSuccess = dtResultFromSAP.Select("FLAG='Y'");
                        DataRow[] drFail = dtResultFromSAP.Select("FLAG='N' ");

                        #region  加扣成功发送邮件
                        if (drSuccess.Length > 0)
                        {
                            foreach (DataRow dr in drSuccess)
                            {
                                string strZappid = dr["ZAPPID"].ToString();
                                string strItem = dr["ZITEM"].ToString().PadLeft(4, '0');
                                string strMblnr =dr["MBLNR"].ToString();
                                string strMESSAGE = dr["MESSAGE"].ToString().Trim();
                                string strFlag = dr["FLAG"].ToString().Trim();
                                objAddDoc.UpdateAddDocReturn(strMblnr, strZappid); //更新加扣扣帐单号到加扣表
                                string OMBLN = objAddDoc.getWhdwn_Add(strZappid, strItem);
                                objAddDoc.InsertAddDocLog(OMBLN, "END", "S", strMblnr);

                                dtSuccess.Rows.Add(dr.ItemArray);
                            }
                             if (SendMail(dtSuccess))
                            {
                                stsWarning.Text = "加扣成功,邮件发送成功！";
                                return;
                            }
                            else
                            {
                                stsWarning.Text = "加扣成功,邮件发送失败！";
                                return;
                            }

                        }
                        #endregion

                        #region 加扣失败
                        if (drFail.Length > 0)
                        {
                            string matnrs = "";
                            foreach (DataRow dr in drFail)
                            {
                                matnrs += dr["MATNR"].ToString()+",";
                                string strZappid = dr["ZAPPID"].ToString();
                                string strItem = dr["ZITEM"].ToString().PadLeft(4, '0');
                                string strMESSAGE = dr["MESSAGE"].ToString().Trim();
                                objAddDoc.UpdateAddDocReturnErr(strMESSAGE, strZappid); //更新错误信息到REMAK1 栏位
                                string OMBLN = objAddDoc.getWhdwn_Add(strZappid, strItem);
                                objAddDoc.InsertAddDocLog(OMBLN, "END", "E",strMESSAGE);
                            }
                            MessageBox.Show(matnrs + "加扣失败");
                        }
                    #endregion

                    }
                    #endregion
                }
                else
                {
                    for (int i = 0; i < dtAddDocToSAP.Rows.Count; i++)
                    {
                        string OMBLN = objAddDoc.getWhdwn_Add(dtAddDocToSAP.Rows[i]["ZAPPID"].ToString(), dtAddDocToSAP.Rows[i]["ZITEM"].ToString());
                        objAddDoc.InsertAddDocLog(OMBLN, "SAP","NO","加扣取消");
                    }
                    return;
                }
            }
        }

        public void Query()
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
            }
            strMblnr = txtMblnr.Text.Trim().ToString();
            dtData = objAddDoc.QueryAddDoc(strWerks, strLgort, strMblnr);
            ShowDataGrid();

            if (dtData.Rows.Count < 0)
            {
                stsWarning.Text = "当前没有可以加扣的数据";
            }
        }

        private void dgvData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvData.EditMode = DataGridViewEditMode.EditOnEnter;
            if (e.ColumnIndex == 0)
            {
                if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "True")
                {
                    dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
          
        }

        public bool CheckNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            Regex regex = new Regex(@"^(-)?\d+(\.\d+)?$");
            if (regex.IsMatch(number))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            ShowDdlLgort();
            Query();
        }

        private void StorageOut_AddDoc_Resize(object sender, EventArgs e)
        {
           // panel3.Size = new System.Drawing.Size((int)(this.Size.Width * 0.3), panel3.Size.Height);
            if (this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width;
        }

        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            stsWarning.Text = "请输入正确的加扣数量！";
            return;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = null;
            dtData.Rows.Clear();
            stsWarning.Text = "";
            txtMblnr.Text = "";
            lblCount.Text = "";
            txtDlgort.Text = "";
            strWerks = "";
            strLgort = "";
            strMblnr = "";
            Query();
        }

        public void GetDataTableToSAP()
        {
            dtAddDocToSAP = new DataTable();
            dtAddDocToSAP.Columns.Add("MANDT", Type.GetType());
            dtAddDocToSAP.Columns.Add("ZAPPID", Type.GetType());
            dtAddDocToSAP.Columns.Add("ZITEM", Type.GetType());
            dtAddDocToSAP.Columns.Add("WERKS", Type.GetType());
            dtAddDocToSAP.Columns.Add("WERKS_I", Type.GetType());
            dtAddDocToSAP.Columns.Add("LGORT", Type.GetType());
            dtAddDocToSAP.Columns.Add("LGORT_I", Type.GetType());
            dtAddDocToSAP.Columns.Add("MATNR", Type.GetType());
            dtAddDocToSAP.Columns.Add("MENGE", Type.GetType());
            dtAddDocToSAP.Columns.Add("MBLNR", Type.GetType());
            dtAddDocToSAP.Columns.Add("MJAHR", Type.GetType());
            dtAddDocToSAP.Columns.Add("FLAG", Type.GetType());
            dtAddDocToSAP.Columns.Add("MESSAGE", Type.GetType());
            dtAddDocToSAP.Columns.Add("TXDAT", Type.GetType());
            dtAddDocToSAP.Columns.Add("TXTM", Type.GetType());
            dtAddDocToSAP.Columns.Add("TXEMP", Type.GetType());
            dtAddDocToSAP.Columns.Add("KOSTL", Type.GetType());
            dtAddDocToSAP.Columns.Add("CHARG", Type.GetType());
            dtAddDocToSAP.Columns.Add("BWART", Type.GetType());
            dtAddDocToSAP.Columns.Add("AUFNR", Type.GetType());
        }

        private bool SendMail(DataTable  dt)
        {
            bool bolResult = false;
            strWerks = dt.Rows[0]["WERKS_I"].ToString().Trim();
            string strUMLGOS = "";//获取加扣的线上仓别
            ArrayList arrUMLGO = new ArrayList();

            ClaHttpHelper clahttpHelper = new ClaHttpHelper();
            QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
            string strUrl = objStorageData.GetSendMailUrl();
            foreach (DataRow dr in dt.Rows)
            {
                if (!arrUMLGO.Contains(dr["LGORT_I"].ToString()))
                {
                    arrUMLGO.Add(dr["LGORT_I"].ToString());
                }
            }
            if (arrUMLGO.Count > 0)
            {
                foreach (string str in arrUMLGO)
                {
                    strUMLGOS += "'" + str + "',";
                }
                strUMLGOS = strUMLGOS.Substring(0, strUMLGOS.Length - 1);
            }
            string strMges = "";
            string strMailTo = objAddDoc.GetMail(strWerks, strUMLGOS, "0");
            string strMailCc = objAddDoc.GetMail(strWerks, strUMLGOS, "1");
            if (string.IsNullOrEmpty(strMailTo) && string.IsNullOrEmpty(strMailCc))
            {
                stsWarning.Text ="该线上仓别未配置邮件相关人员！！";
                return bolResult;
            }
            else
            {
                string strPassWord = "975A8056C8DF50786FB680A96E0CCCBF";//邮件发送密码
                string strFromAddress = "Web_Notice@quantacn.com";
                string strSubject = "加扣邮件通知";
                string strMailBody = "<body> <style type='text/css'> td {text-align:left; font-family:'Arial'; font-size:15px; }</style> ";
                strMailBody += "<p><table><tr><td>Dear All,</td></tr> <tr><td>&nbsp &nbsp  如下为加扣信息，请参阅，谢谢！</td></tr></table></p>";

                #region Create HTML
                StringBuilder sbMailContent = new StringBuilder();
                sbMailContent.Append("<TABLE cellSpacing=2 cellPadding=3 align=center border=1><TR bgColor=#14DCFF>"
                   + " <TD>厂区</TD><TD>出库仓别</TD><TD>入库仓别</TD><TD>料号</TD><TD>版本</TD><TD>数量</TD><TD>扣帐编号</TD><TD>扣帐日期</TD>");
                foreach (DataRow dr in dt.Rows)
                {
                    sbMailContent.AppendFormat("<TR><TD>" + dr["WERKS"].ToString().Trim() + "</TD><TD>" + dr["LGORT"].ToString().Trim() + "</TD><TD>" + dr["LGORT_I"].ToString().Trim() + "</TD>"
                        + "<TD>" + dr["MATNR"].ToString().Trim() + "</TD><TD>" + dr["CHARG"].ToString().Trim() + "</TD><TD>" + Convert.ToInt32(Convert.ToDouble(dr["MENGE"].ToString().Trim().Substring(0, dr["MENGE"].ToString().Trim().Length - 4))) + "</TD>"
                        + "<TD>" + dr["MBLNR"].ToString().Trim() + "</TD><TD>" + dr["TXDAT"].ToString().Trim() + "</TD>");
                }
                sbMailContent.Append("</TABLE>");
                sbMailContent.Append("<br/>");
                sbMailContent.Append("<br/>");
                #endregion
                strMailBody += sbMailContent.ToString().Trim();
                try
                {
                    var Data = new
                    {
                        Site = "QSMC",
                        TeamPwd = strPassWord,
                        From = strFromAddress,
                        To = strMailTo,
                        Cc = strMailCc,
                        Bcc = "",
                        Subject = strSubject,
                        Body = strMailBody,
                        IsBodyHtml = true,
                        Attachments = "",
                    };
                    string strData = JsonConvert.SerializeObject(Data);
                    JObject result = (JObject)JsonConvert.DeserializeObject(clahttpHelper.HttpPostByHttpWebRequest(strUrl, strData));
                    if (result["Result"].ToString().ToUpper() == "TRUE")
                    {
                        bolResult = true;
                    }
                    //QCI_QWMS_IQCBRMaterial.MailService.SendMailService objSendMail = new QCI_QWMS_IQCBRMaterial.MailService.SendMailService();
                    //bolResult = objSendMail.SendMail(strPassWord, true, strFromAddress, strMailTo, strMailCc, "", strSubject, strMailBody, "", out strMges);
                }
                catch (Exception ex)
                {
                    strMges = ex.Message;
                }
                return bolResult;
            }
  
        }

      

   

    

    }
}
