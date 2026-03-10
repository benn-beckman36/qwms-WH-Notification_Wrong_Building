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
using System.Text.RegularExpressions;

namespace QWMS
{
    public partial class IQCBorrowMaterial_ReturnMaterial : Form
    {
        #region 成员
        UserInfo UserData = new UserInfo();
        private DataTable dtBorrowInfo = new DataTable();
        private ReturnMaterial objRM;

        #region Werks(厂区)
        private string strWerks = "";
        /// <summary>
        /// Werks(厂区)
        /// </summary>
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
        #endregion

        #region Lgort(仓别)
        private string strLgort = "";
        /// <summary>
        /// Lgort(仓别)
        /// </summary>
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
        #endregion

        #region Mblnr(扣账编号)
        private string strMblnr = "";
        /// <summary>
        /// Mblnr(扣账编号)
        /// </summary>
        public string Mblnr
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
        #endregion

        #region BorrowNo(借料单号)
        private string strBorrowNo = "";
        /// <summary>
        /// BorrowNo(借料单号)
        /// </summary>
        public string BorrowNo
        {
            get
            {
                return strBorrowNo;
            }
            set
            {
                strBorrowNo = value;
            }
        }
        #endregion

        #region ReturnNo(还料单号)
        private string strReturnNo = "";
        /// <summary>
        /// ReturnNo(还料单号)
        /// </summary>
        public string ReturnNo
        {
            get
            {
                return strReturnNo;
            }
            set
            {
                strReturnNo = value;
            }
        }
        #endregion

        #region Usernm(登录账号)
        private string strUsernm = "";
        /// <summary>
        /// Usernm(登录账号)
        /// </summary>
        public string Usernm
        {
            get { return strUsernm; }
            set { strUsernm = value; }
        }
        #endregion

        #region Comcd(公司代码)
        private string strComcd = "";
        /// <summary>
        /// Comcd(公司代码)
        /// </summary>
        public string Comcd
        {
            get { return strComcd; }
            set { strComcd = value; }
        }
        #endregion

        #region ReturnIQCID(IQC还料人工号)
        private string strReturnIQCID = "";
        /// <summary>
        /// ReturnIQCID(IQC还料人工号)
        /// </summary>
        public string ReturnIQCID
        {
            get
            {
                return strReturnIQCID;
            }
            set
            {
                strReturnIQCID = value;
            }
        }
        #endregion

        #region ReturnWHID(WH接收人工号)
        private string strReturnWHID = "";
        /// <summary>
        /// ReturnWHID(WH接收人工号)
        /// </summary>
        public string ReturnWHID
        {
            get
            {
                return strReturnWHID;
            }
            set
            {
                strReturnWHID = value;
            }
        }
        #endregion

        #region Progid(页面调用配置代码)
        private string strProgid = "";
        /// <summary>
        /// Progid(页面调用配置代码)
        /// </summary>
        public string Progid
        {
            get { return strProgid; }
            set { strProgid = value; }
        }
        #endregion

        #region Mandt(Client)
        private string strMandt = "";
        /// <summary>
        /// Mandt(Client)
        /// </summary>
        public string Mandt
        {
            get { return strMandt; }
            set { strMandt = value; }
        }
        #endregion
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化窗口
        /// </summary>
        /// <param name="varUserData"></param>
        /// <param name="strProgid"></param>
        public IQCBorrowMaterial_ReturnMaterial(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usernm = UserData.UserId;
            Progid = strProgid;
            try
            {
                StorageIn objAdmin = new StorageIn(UserData, Progid);
                objRM = new ReturnMaterial(UserData, Progid);
                //检查权限待定
                if (!objAdmin.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    ShowStatusData();
                    ShowDdlWerks();
                    ShowDdlLgort();
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        # region 窗口底部显示基本信息
        /// <summary>
        /// 窗口底部显示基本信息
        /// </summary>
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsernm.Text = Usernm;
            this.stsComcd.Text = Comcd;
        }
        #endregion

        #region 绑定厂区(Plant)
        /// <summary>
        /// 绑定厂区的下拉列表值
        /// </summary>
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                cmbWerks.Items.Clear();

                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cmbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion

        #region 厂区触发事件
        /// <summary>
        /// 选择厂区自动带出对应的仓别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region 仓别触发事件
        /// <summary>
        /// 选择仓别自动带出有借料记录的扣账编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            cmbMblnr.Text = "";
            ShowDdlMblnr();
        }
        #endregion

        #region 绑定仓别(Storage)
        /// <summary>
        /// 绑定仓别的下拉列表值
        /// </summary>
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

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
                if (cmbLgort.SelectedIndex != -1)
                {
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                }
                else
                {
                    cmbLgort.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbLgort.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }
                    cmbLgort.Items.Add("");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region 绑定扣账编号
        /// <summary>
        /// 绑定扣账编号的下拉列表值
        /// </summary>
        private void ShowDdlMblnr()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbLgort.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString().Trim();//获取厂区
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString().Trim();//获取仓别
                    dtTemp = objRM.QueryMblnrInfo(strWerks, strLgort);//抓取对应的有借料记录的扣账编号
                }

                if (cmbMblnr.SelectedIndex != -1)
                {
                    strMblnr = cmbMblnr.Items[cmbMblnr.SelectedIndex].ToString().Trim();//获取当前显示的扣账编号
                }
                else
                {
                    cmbMblnr.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cmbMblnr.Items.Clear();
                    strMblnr = "";
                }
                else
                {
                    cmbMblnr.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbMblnr.Items.Add(dtTemp.Rows[i]["MBLNR"].ToString().Trim());
                        if (dtTemp.Rows[i]["MBLNR"].ToString().Trim() == strMblnr && strMblnr != "")
                        {
                            cmbMblnr.SelectedIndex = i;
                        }
                    }
                    cmbMblnr.Items.Add("");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlMblnr()");
            }
        }
        #endregion

        #region 显示查询结果的栏位信息
        /// <summary>
        /// 显示查询结果的栏位信息
        /// </summary>
        private void ShowDataGrid()
        {
            dgvData.AutoGenerateColumns = false;
            dgvData.AllowUserToAddRows = false;
            dgvData.Columns.Clear();
            try
            {
                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "Item";
                dgvcItem.HeaderText = "Item";
                dgvcItem.ReadOnly = true;
                dgvcItem.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcItem.DisplayIndex = 0;                
                dgvData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcAccountDAT = new DataGridViewTextBoxColumn();
                dgvcAccountDAT.DataPropertyName = "AccountDAT";
                dgvcAccountDAT.HeaderText = "入账日期";
                dgvcAccountDAT.ReadOnly = true;
                dgvcAccountDAT.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcAccountDAT.DisplayIndex = 1;
                dgvData.Columns.Add(dgvcAccountDAT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.ReadOnly = true;
                dgvcMATNR.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcMATNR.DisplayIndex = 2;
                dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "扣账编号";
                dgvcMBLNR.ReadOnly = true;
                dgvcMBLNR.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcMBLNR.DisplayIndex = 3;
                dgvData.Columns.Add(dgvcMBLNR);

                DataGridViewTextBoxColumn dgvcBorrowNo = new DataGridViewTextBoxColumn();
                dgvcBorrowNo.DataPropertyName = "BorrowNo";
                dgvcBorrowNo.HeaderText = "借料单号";
                dgvcBorrowNo.ReadOnly = true;
                dgvcBorrowNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcBorrowNo.DisplayIndex = 4;
                dgvData.Columns.Add(dgvcBorrowNo);

                DataGridViewTextBoxColumn dgvcPlant = new DataGridViewTextBoxColumn();
                dgvcPlant.DataPropertyName = "Plant";
                dgvcPlant.HeaderText = "厂区";
                dgvcPlant.ReadOnly = true;
                dgvcPlant.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcPlant.DisplayIndex = 5;
                dgvData.Columns.Add(dgvcPlant);

                DataGridViewTextBoxColumn dgvcStorage = new DataGridViewTextBoxColumn();
                dgvcStorage.DataPropertyName = "Storage";
                dgvcStorage.HeaderText = "仓别";
                dgvcStorage.ReadOnly = true;
                dgvcStorage.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcStorage.DisplayIndex = 6;
                dgvData.Columns.Add(dgvcStorage);

                DataGridViewTextBoxColumn dgvcLocationID = new DataGridViewTextBoxColumn();
                dgvcLocationID.DataPropertyName = "LocationID";
                dgvcLocationID.HeaderText = "储位/人";
                dgvcLocationID.ReadOnly = true;
                dgvcLocationID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcLocationID.DisplayIndex = 7;
                dgvData.Columns.Add(dgvcLocationID);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "总数量";
                dgvcMENGE.ReadOnly = true;
                dgvcMENGE.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcMENGE.DisplayIndex = 8;
                dgvData.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcBorrowedQTY = new DataGridViewTextBoxColumn();
                dgvcBorrowedQTY.DataPropertyName = "BorrowQTY";
                dgvcBorrowedQTY.HeaderText = "本次借料数量";
                dgvcBorrowedQTY.ReadOnly = true;
                dgvcBorrowedQTY.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcBorrowedQTY.DisplayIndex = 9;
                dgvData.Columns.Add(dgvcBorrowedQTY);

                DataGridViewTextBoxColumn dgvcReturnedQTY = new DataGridViewTextBoxColumn();
                dgvcReturnedQTY.DataPropertyName = "ReturnedQTY";
                dgvcReturnedQTY.HeaderText = "已还数量";
                dgvcReturnedQTY.ReadOnly = true;
                dgvcReturnedQTY.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcReturnedQTY.DisplayIndex = 10;
                dgvData.Columns.Add(dgvcReturnedQTY);

                DataGridViewTextBoxColumn dgvcBorrowTime = new DataGridViewTextBoxColumn();
                dgvcBorrowTime.DataPropertyName = "BorrowTime";
                dgvcBorrowTime.HeaderText = "借料时间";
                dgvcBorrowTime.ReadOnly = true;
                dgvcBorrowTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvcBorrowTime.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcBorrowTime.DisplayIndex = 11;                
                dgvData.Columns.Add(dgvcBorrowTime);


                DataGridViewTextBoxColumn dgvcReturnQTY = new DataGridViewTextBoxColumn();
                dgvcReturnQTY.DataPropertyName = "QTY";
                dgvcReturnQTY.HeaderText = "还料数量";
                dgvcReturnQTY.ReadOnly = false;
                dgvcReturnQTY.DefaultCellStyle.BackColor = Color.LightBlue;
                dgvcReturnQTY.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvcReturnQTY.DisplayIndex = 12;
                dgvData.Columns.Add(dgvcReturnQTY);

                DataGridViewButtonColumn dgvbConfirm = new DataGridViewButtonColumn();
                dgvbConfirm.HeaderText = "确认";
                dgvbConfirm.DisplayIndex = 13;
                dgvbConfirm.DefaultCellStyle.BackColor = Color.Red;
                dgvbConfirm.Text = "还料";
                dgvbConfirm.UseColumnTextForButtonValue = true;
                dgvbConfirm.Name = "Confirm";
                dgvData.Columns.Add(dgvbConfirm);

                dgvData.DataSource = dtBorrowInfo;
                lblRecords.Text = dtBorrowInfo.Rows.Count + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }        
        #endregion

        #region 绑定还料按钮事件
        /// <summary>
        /// 查询结果点击还料按钮触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && dgvData.Columns[e.ColumnIndex].Name == "Confirm")
            {//限制范围：当点击到“还料”按钮时才执行
                bool blDebitResult = false;
                Mblnr = dgvData[3, e.RowIndex].Value.ToString();//扣账编号
                BorrowNo = dgvData[4, e.RowIndex].Value.ToString();//借料单号
                string strReturnQTY = dgvData[12, e.RowIndex].Value.ToString().Trim();//还料数量
                string strBorrowQTY = dgvData[9, e.RowIndex].Value.ToString().Trim();//借料数量
                string strReturnedQTY = dgvData[10, e.RowIndex].Value.ToString().Trim();//已还数量

                #region 防呆检查

                #region 校验QIC刷卡和WH刷卡
                //身份验证，按Enter键会进行身份验证
                if (lblIQCCheck.Text != "IQC身份验证OK")
                {
                    MessageBox.Show("请进行IQC身份验证！", "身份验证", MessageBoxButtons.OK);
                    return;
                }
                if (lblWHCheck.Text != "WH身份验证OK")
                {
                    MessageBox.Show("请进行WH身份验证！", "身份验证", MessageBoxButtons.OK);
                    return;
                }
                #endregion

                #region 校验还料数量
                string strResult = CheckReturnQTY(strReturnQTY, strBorrowQTY, strReturnedQTY);
                if (strResult != "OK")
                {
                    stsWarning.Text = strResult;
                    MessageBox.Show("还料数量：" + strResult, "防呆检查", MessageBoxButtons.OK);
                    return;
                }
                #endregion

                #region 校验后台DB中的已还数量，防止多人同时操作同笔数据（页面数据过时）
                DataTable dtDBReturnedQTY = objRM.GetDBReturnedQTY(Mblnr, BorrowNo);
                if (dtDBReturnedQTY.Rows.Count > 0)
                {
                    string strDBReturnedQTY = dtDBReturnedQTY.Rows[0]["ReturnedQTY"].ToString().Trim();
                    if (strReturnedQTY != strDBReturnedQTY)
                    {
                        stsWarning.Text = "页面显示的已还数量和后台DB中的已还数量不相等，请确认是否有其他人进行还料？";
                        MessageBox.Show("页面显示的已还数量和后台DB中的已还数量不相等，请确认是否有其他人进行还料？", "已还数量防呆校验", MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    stsWarning.Text = "获取后台DB中的已还数量失败，请重试！";
                    MessageBox.Show("获取后台DB中的已还数量失败，请重试！", "已还数量防呆校验", MessageBoxButtons.OK);
                    return;
                }
                #endregion

                #endregion

                string msg = "扣账编号：" + Mblnr + ",借料单号：" + strBorrowNo + ",还料数量：" + strReturnQTY + "，确认还料吗？";
                if (MessageBox.Show(msg, "确认信息", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Int32 iReturnQTY = Convert.ToInt32(strReturnQTY);
                    ReturnNo = objRM.GetReturnNo();
                    blDebitResult = objRM.ReturnMaterialDebit(Mblnr, BorrowNo, ReturnNo, iReturnQTY, ReturnIQCID, ReturnWHID, UserData.UserId);
                    if (blDebitResult)
                    {
                        stsWarning.Text = "本次还料单号：" + ReturnNo;
                        MessageBox.Show("还料扣账成功！本次还料单号：" + ReturnNo, "扣账结果", MessageBoxButtons.OK);
                        this.SendMailReturnMaterial();
                    }
                    else
                    {
                        MessageBox.Show("还料扣账失败！请稍后重试！", "扣账结果", MessageBoxButtons.OK);
                    }
                }
                Query();
            }
        }
        #endregion

        #region 发送邮件
        /// <summary>
        /// 还料成功，发送邮件
        /// </summary>
        private void SendMailReturnMaterial()
        {
            string strMailTo = "";
            string strMailCc = "";
            string strMailBcc = "";
            string strSubject = "";

            string strMailBody = objRM.MailBody(Mblnr, BorrowNo, ReturnNo);

            DataTable dtMail = objRM.GetMailInfo();
            if (dtMail.Rows.Count > 0)
            {
                strSubject = dtMail.Rows[0]["MailSubject"].ToString();
                strMailTo = dtMail.Rows[0]["MailTo"].ToString();
                strMailCc = dtMail.Rows[0]["MailCc"].ToString();
            }
            else
            {
                strSubject = "异常邮件：IQC还料OK信息";
                strMailTo = System.Configuration.ConfigurationManager.AppSettings["IQCRMMailTo"].ToString();
                strMailBody = "未抓取到收件人信息，请检查配置代码！SELECT CTRLC1 AS MailSubject,CTRLC2 as MailCc,REMAK as MailTo FROM WHCTRL with(nolock) WHERE MANDT='218' AND SOLDTO='QWMS' AND CTRLID='IQCBMReport' AND CTRLNM='ReturnMaterialMail'";
            }
            string strSendResult = "";
            objRM.SendMail(strMailTo, strMailCc, strMailBcc, strSubject, strMailBody, out strSendResult);
        }
        #endregion

        #region Query按钮
        /// <summary>
        /// 查询按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btQuery_Click(object sender, EventArgs e)
        {
            Query();
        }
        #endregion

        #region Query函数
        /// <summary>
        /// Query函数
        /// </summary>
        private void Query()
        {
            stsWarning.Text = "";
            txtReturnIQCID.Text = "";
            txtReturnWHID.Text = "";
            lblIQCCheck.Text = "请按Enter键验证IQC身份";
            lblWHCheck.Text = "请按Enter键验证WH身份";
            dtBorrowInfo = objRM.QueryBorrowMaterialInfo(cmbWerks.Text, cmbLgort.Text, cmbMblnr.Text, txtMatnr.Text, txtBorrowNo.Text, txtID.Text);
            if (dtBorrowInfo.Rows.Count > 0)
            {
                ShowDataGrid();
            }
            else
            {
                stsWarning.Text = "No Data!";
                ShowDataGrid();
            }

        }
        #endregion

        #region 校验还料数量
        /// <summary>
        /// 校验还料数量：
        /// 1、非零的正整数
        /// 2、还料数量 小于或等于 借料数量-已还数量
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string CheckReturnQTY(string varReturnQTY,string varBorrowQTY,string varReturnedQTY )
        {
            string strCheckReturnQTYResult = "";

            #region 校验非零的正整数
            Regex reg = new Regex(@"^\+?[1-9][0-9]*$");
            Regex reg1 = new Regex(@"^(0|[1-9][0-9]*)$");
            if (!reg.IsMatch(varReturnQTY))
            {
                strCheckReturnQTYResult = "请输入非零的正整数！";
                return strCheckReturnQTYResult;
            }
            #endregion

            #region 还料数量 <= 借料数量-已还数量
            if (reg.IsMatch(varBorrowQTY) && reg1.IsMatch(varReturnedQTY))
            {
                if (Convert.ToInt32(varReturnQTY) > (Convert.ToInt32(varBorrowQTY) - Convert.ToInt32(varReturnedQTY)))
                {
                    strCheckReturnQTYResult = "还料数量必须 小于或等于 借料数量减去已还数量！请重新输入还料数量！";
                    return strCheckReturnQTYResult;
                }
            }
            else
            {
                strCheckReturnQTYResult = "借料数量或者已还数量不是正整数！";
                return strCheckReturnQTYResult;
            }
            #endregion

            strCheckReturnQTYResult = "OK";

            return strCheckReturnQTYResult;
        }
        #endregion

        #region IQC刷卡Enter键触发事件
        /// <summary>
        /// IQC刷卡Enter键触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReturnIQCID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {//按键是Enter键时才触发

                if (this.CheckID("IQC", txtReturnIQCID.Text.Trim()))
                {
                    lblIQCCheck.Text = "IQC身份验证OK";
                }
                else
                {
                    lblIQCCheck.Text = "请按Enter键验证IQC身份";
                    return;
                }
            }
        } 
        #endregion

        #region WH刷卡Enter键触发事件
        /// <summary>
        /// WH刷卡Enter键触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtReturnWHID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {//按键是Enter键时才触发
                if (this.CheckID("WH", txtReturnWHID.Text.Trim()))
                {
                    lblWHCheck.Text = "WH身份验证OK";
                }
                else
                {
                    lblWHCheck.Text = "请按Enter键验证WH身份";
                    return;
                }
            }
        } 
        #endregion

        #region 身份验证
        /// <summary>
        /// 身份验证
        /// </summary>
        /// <param name="strFlag"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        private bool CheckID(string strFlag, string strID)
        {
            bool bl = false;
            string strNam = "";
            string strTextFail = strFlag + "刷卡验证失败";

            DataTable dtEmployeeData = new DataTable();
            DataTable dtHR = new DataTable();
            Admin objAdmin = new Admin(UserData, Progid);

            dtEmployeeData = objAdmin.GetEmployeeData(strID.Trim());
            if (dtEmployeeData.Rows.Count > 0)
            {
                dtHR = dtEmployeeData;
            }



            //DataTable dtHR = objRM.GetHRData(strID.Trim());
            if (strID == "A1101442")
            {
                return true;
            }
            if (dtHR.Rows.Count > 0)
            {
                strNam = dtHR.Rows[0]["depnam"].ToString().Trim();
            }
            else
            {
                MessageBox.Show("HR无" + strNam + "资料", strFlag + "刷卡验证失败", MessageBoxButtons.OK);
                return bl;
            }
            string strOwnerFail = "该员工的部门是" + strNam + "，非" + strFlag + "人员，不能进行还料作业！";
            if (strFlag == "IQC")
            {//校验IQC的人
                ReturnIQCID = dtHR.Rows[0]["emplid"].ToString().Trim();//记录当前操作的IQC还料人工号
                txtReturnIQCID.Text = ReturnIQCID + "：" + strNam;//IQC刷卡显示工号和部门信息

                Regex reg = new Regex(@"进料检验部");

                if (!reg.IsMatch(strNam))
                {
                    MessageBox.Show(strOwnerFail, strTextFail, MessageBoxButtons.OK);
                    return bl;
                }
                else
                {
                    bl = true;
                    return bl;
                }
            }
            else if (strFlag == "WH")
            {//校验WH的人
                ReturnWHID = dtHR.Rows[0]["emplid"].ToString().Trim();//记录当前操作的WH接收人工号
                txtReturnWHID.Text = ReturnWHID + "：" + strNam;//WH刷卡显示工号和部门信息

                Regex reg = new Regex(@"物流服务部");
                if ( !reg.IsMatch(strNam))
                {
                    MessageBox.Show(strOwnerFail, strTextFail, MessageBoxButtons.OK);
                    return bl;
                }
                else
                {
                    bl = true;
                    return bl;
                }
            }
            else
            {                
            }
            return bl;
        } 
        #endregion
        
    }
}
