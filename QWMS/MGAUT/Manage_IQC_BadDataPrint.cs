using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using System.IO;
using QCI.QWMS;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace QWMS
{
    public partial class Manage_IQC_BadDataPrint : Form
    {
        #region 变量
        UserInfo UserData = new UserInfo();
        public string strMandt = "";
        public string strComcd = "";
        public string strUsrnm = "";
        public string strProgid = "";
        public string strWerks = "";
        public string strLgort = "";
        public string strGRno = "";
        public string strDateFrom = "";
        public string strDateTo = "";
        public string strIP = "";

        //打印数据表
        DataTable dtPrint = new DataTable();

        //打印数据集合
        List<string> listPrint = new List<string>();

        //使用者厂区仓别表
        DataTable dtWerksAndLgort = new DataTable();

        StorageData objStorageData;
        #endregion

        #region 构造函数
        public Manage_IQC_BadDataPrint()
        {
            InitializeComponent();
        }

        public Manage_IQC_BadDataPrint(UserInfo varUserData, string Progid)
            : this()
        {
            UserData = varUserData;
            strMandt = varUserData.Client;
            strComcd = varUserData.CompanyCode;
            strUsrnm = varUserData.UserId;
            strProgid = Progid;

            objStorageData = new StorageData(varUserData);

            //显示状态栏信息
            ShowStatusData();

            //获取当前使用者可操作的厂区、仓别项
            //BindWerksAndLgorts();
            ShowDdlWerks();
            ShowDdlLgort();


            //读取打印机设置文件
            BindPrintSetting();
        }

        #endregion

        #region ShowDdlWerks
        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
                cbWerks.Items.Clear();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                dtTemp = objAuthority.CheckPlantAuthority();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    cbWerks.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }
        #endregion

        #region cmbWerks_SelectedIndexChanged
        private void cbWerks_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }
        #endregion

        #region ShowDdlLgort
        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                if (cbWerks.SelectedIndex != -1)
                {
                    strWerks = cbWerks.Items[cbWerks.SelectedIndex].ToString();
                    //dtTemp = objPlantData.GetDdlLgortData(strWerks);
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    //dtTemp = objPlantData.GetDdlLgortData();
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (cbLgort.SelectedIndex != -1)
                {
                    strLgort = cbLgort.Items[cbLgort.SelectedIndex].ToString();
                }
                else
                {
                    cbLgort.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    cbLgort.Items.Clear();
                    strLgort = "";
                }
                else
                {
                    cbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            cbLgort.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }
        #endregion

        #region
        //#region 获取当前使用者可操作的厂区、仓别项
        //private void BindWerksAndLgorts()
        //{
        //    try
        //    {
        //        QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

        //        //获取当前使用者可操作的厂区、仓别项
        //        dtWerksAndLgort = objAuthority.GetWerksAndLgorts();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString() + "BindLgorts()");
        //    }

        //    //初始化绑定厂区
        //    BindWerks();
        //}
        //#endregion

        //#region 初始化或刷新时，绑定厂区
        //private void BindWerks()
        //{
        //    for (int i = 0; i < dtWerksAndLgort.Rows.Count; i++)
        //    {
        //        string[] arrayWerks = dtWerksAndLgort.Rows[i]["WERKSandLGORTS"].ToString().Split(',');
        //        cbWerks.Items.Add(arrayWerks[0].ToString());
        //    }
        //}
        //#endregion

        //#region 厂区下拉框改变事件
        //private void cbWerks_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //清空仓别下拉框
        //    cbLgort.Items.Clear();

        //    for (int i = 0; i < dtWerksAndLgort.Rows.Count; i++)
        //    {
        //        //根据当前选中的厂区，绑定厂区对应的仓别可选项
        //        if (dtWerksAndLgort.Rows[i]["WERKSandLGORTS"].ToString().Contains(cbWerks.Items[cbWerks.SelectedIndex].ToString()))
        //        {
        //            string[] arrayLgorts = dtWerksAndLgort.Rows[i]["WERKSandLGORTS"].ToString().Split(',');
        //            for (int j = 1; j < arrayLgorts.Length; j++)
        //            {
        //                cbLgort.Items.Add(arrayLgorts[j].ToString());
        //            }
        //        }

        //    }
        //}
        //#endregion

        #endregion

        #region 显示状态栏信息
        private void ShowStatusData()
        {
            stsMandt.Text = strMandt;
            stsComcd.Text = strComcd;
            stsUsrnm.Text = strUsrnm;
            stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
        #endregion

        #region 窗体载入时读取打印机设置文件
        private void BindPrintSetting()
        {
            //打印机设置文件默认路径
            //文件格式：串口，波特率，ip
            string strFileSettingPath = Application.StartupPath.ToString() + "\\PrintSetting.txt";

            if (File.Exists(strFileSettingPath))
            {
                //读取文件
                using (StreamReader sr = new StreamReader(strFileSettingPath, System.Text.Encoding.Default))
                {
                    string[] arrayPrintSetting = sr.ReadToEnd().Split(',');

                    //设置默认打印设置参数
                    txtCom.Text = arrayPrintSetting[0].ToString();
                    txtBounnd.Text = arrayPrintSetting[1].ToString();
                    txtIP.Text = arrayPrintSetting[2].ToString();
                }
            }
        }
        #endregion

        #region 打印机设置
        private void btSetting_Click(object sender, EventArgs e)
        {
            //打印机设置文件路径
            //文件格式：串口，波特率，ip
            string strFileSettingPath = Application.StartupPath.ToString() + "\\PrintSetting.txt";

            //打印机设置文件不存在
            if (!File.Exists(strFileSettingPath))
            {
                //保存当前打印设置信息到E盘根目录下
                if (MessageBox.Show("是否保存当前打印机设置信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                        try
                    {
                        using (StreamWriter sw = File.CreateText(strFileSettingPath))
                        {
                            sw.WriteLine(txtCom.Text.Trim() + "," + txtBounnd.Text.Trim() + "," + txtIP.Text.Trim());
                            //保存IP信息
                            //sw.WriteLine(",," + txtIP.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString() + "File.CreateText(strFileSettingPath)");
                    }         
                }
            }
            //打印机设置文件存在
            else
            {
                if (MessageBox.Show("是否覆盖现有打印机设置信息?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        string[] arrayPrintSetting;
                        using (StreamReader sr = new StreamReader(strFileSettingPath, System.Text.Encoding.Default))
                        {
                            arrayPrintSetting = sr.ReadToEnd().Split(',');
                        }
                        //覆盖现有的打印机设置文件
                        using (StreamWriter sw = new StreamWriter(strFileSettingPath, false))
                        {
                            sw.WriteLine(txtCom.Text.Trim() + "," + txtBounnd.Text.Trim() + "," + txtIP.Text.Trim());         
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString() + "-<StreamWriter(strFileSettingPath, false)>");
                    }
                }
            }
        }
        #endregion

        #region 刷新
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //   cbLgort.SelectedIndex = -1;
            //cbWerks.SelectedIndex = -1;
            //strWerks = "";
            //strLgort = "";
            txtGRno.Text = "";
            // txtUsnam.Text = "";
            dgvGridData.Columns.Clear();
            ckPrintAgain.Checked = false;
            ckPrintAgain.Enabled = true;
            chkCombine.Checked = false;
            chkCombine.Enabled = true;
            lbrecords.Text = "0 records";
        }
        #endregion

        #region 查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cbWerks.SelectedIndex != -1)
            {
                strWerks = cbWerks.SelectedItem.ToString();
            }
            else
            {
                strWerks = string.Empty;
            }
            if (cbLgort.SelectedIndex != -1)
            {
                strLgort = cbLgort.SelectedItem.ToString();
            }
            else
            {
                strLgort = string.Empty;
            }

            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "请选择厂区和仓别";
                return;
            }
            ckPrintAgain.Enabled = false;
            chkCombine.Enabled = false;
            strGRno = txtGRno.Text.ToString().Trim();
            strDateFrom = dtpBegin.Value.ToString("yyyyMMdd");
            strDateTo = dtpEnd.Value.ToString("yyyyMMdd");

            if (chkCombine.Checked)
            {
                if (strGRno == "")
                {
                    stsWarning.Text = "请填入GR单号";
                    return;
                }
                dtPrint = objStorageData.GetIQCReturnData(strWerks, strLgort, strGRno, strDateFrom, strDateTo, ckPrintAgain.Checked, "合并");
            }
            else
            {
                dtPrint = objStorageData.GetIQCReturnData(strWerks, strLgort, strGRno, strDateFrom, strDateTo, ckPrintAgain.Checked, "");
            }

            ShowdgvGridData();
        }
        #endregion

        #region 显示dgv数据
        private void ShowdgvGridData()
        {
            //清空提示信息
            stsWarning.Text = "";

            dgvGridData.AutoGenerateColumns = false;
            dgvGridData.Columns.Clear();
            dgvGridData.AllowUserToAddRows = false;
            dgvGridData.ContextMenuStrip = this.cmsMenu;//增加拆箱操作

            try
            {
                ////选择单选框√
                DatagridViewCheckBoxHeaderCell chkcell = new DatagridViewCheckBoxHeaderCell();
                chkcell.OnCheckBoxClicked += new CheckBoxClickedHandler(chkcell_OnCheckBoxClicked);
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderCell = chkcell;
                chk.DataPropertyName = "cSelect";
                chk.HeaderText = "";
                chk.Width = 30;
                this.dgvGridData.Columns.Add(chk);
                this.dgvGridData.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

                //单号√
                DataGridViewTextBoxColumn dgvcMBLNR = new DataGridViewTextBoxColumn();
                dgvcMBLNR.DataPropertyName = "MBLNR";
                dgvcMBLNR.HeaderText = "单号";//Pallet_ID
                dgvcMBLNR.Width = 130;
                dgvcMBLNR.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcMBLNR);

                //判票号√
                DataGridViewTextBoxColumn dgvcBOXID = new DataGridViewTextBoxColumn();
                dgvcBOXID.DataPropertyName = "BOXID";
                dgvcBOXID.HeaderText = "判票号";//Box_ID
                dgvcBOXID.Name = "BOXID";
                dgvcBOXID.Width = 130;
                dgvcBOXID.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcBOXID);

                //料号√
                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";//PN
                //dgvcMATNR.Width = 120;
                dgvcMATNR.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcMATNR);

                //版本√
                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本";//Batch_Type
                //dgvcCHARG.Width = 65;
                dgvcCHARG.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcCHARG);

                //数量√
                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "数量";//Qty
                //dgvcMENGE.Width = 65;
                dgvcMENGE.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcMENGE);

                //开单人√
                DataGridViewTextBoxColumn dgvcUSNAM = new DataGridViewTextBoxColumn();
                dgvcUSNAM.DataPropertyName = "USNAM";
                dgvcUSNAM.HeaderText = "结单人";//USNAM
                //dgvcUSNAM.Width = 80;
                dgvcUSNAM.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcUSNAM);

                //Cost Center√
                DataGridViewTextBoxColumn dgvcKOSTL = new DataGridViewTextBoxColumn();
                //dgvcKOSTL.DataPropertyName = "KOSTL";
                //dgvcKOSTL.HeaderText = "Cost Center";//KOSTL
                //dgvcKOSTL.Width = 120;
                //dgvcKOSTL.ReadOnly = true;
                //dgvGridData.Columns.Add(dgvcKOSTL);

                //厂商代码√
                DataGridViewTextBoxColumn dgvcLIFNR = new DataGridViewTextBoxColumn();
                dgvcLIFNR.DataPropertyName = "LIFNR";
                dgvcLIFNR.HeaderText = "厂商代码";//LIFNR
                //dgvcLIFNR.Width = 100;
                dgvcLIFNR.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcLIFNR);

                //NG Code√
                DataGridViewTextBoxColumn dgvcREFID = new DataGridViewTextBoxColumn();
                dgvcREFID.DataPropertyName = "REFID";
                dgvcREFID.HeaderText = "NG Code";//REFID
                //dgvcREFID.Width = 90;
                dgvcREFID.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcREFID);

                //料号描述√
                DataGridViewTextBoxColumn dgvcKDMAT = new DataGridViewTextBoxColumn();
                dgvcKDMAT.DataPropertyName = "KDMAT";
                dgvcKDMAT.HeaderText = "料号描述";//Cust_PN
                //dgvcKDMAT.Width = 200;
                dgvcKDMAT.Width = 130;
                dgvcKDMAT.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcKDMAT);

                //日期√
                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "日期";//TransDate
                //dgvcBUDAT.Width = 90;
                dgvcCRDAT.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcCRDAT);

                //厂商全码√
                DataGridViewTextBoxColumn dgvcDELNO = new DataGridViewTextBoxColumn();
                dgvcDELNO.DataPropertyName = "DELNO";
                dgvcDELNO.HeaderText = "厂商全码";
                dgvcDELNO.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcDELNO);

                //料号描述√
                DataGridViewTextBoxColumn dgvcDEITM = new DataGridViewTextBoxColumn();
                dgvcDEITM.DataPropertyName = "DEITM";
                dgvcDEITM.HeaderText = "料号详细描述";              
                dgvcDEITM.ReadOnly = true;
                dgvGridData.Columns.Add(dgvcDEITM);

                dgvGridData.DataSource = dtPrint;
                lbrecords.Text = dtPrint.Rows.Count.ToString()+" records";
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString() + "ShowdgvGridData()");
            }
        }
        #endregion

        #region 拆箱
        private void tsmiSplit_Click(object sender, EventArgs e)
        {
            //     QWMS.Common.ClaCommon claCommon = new ClaCommon();
            objStorageData = new StorageData(UserData, strWerks, strLgort);

            //DataGridViewRow row = dgvBottom.CurrentRow;

            //string strBoxid = row.Cells["BOXID"].ToString().Trim();
            //string strMblnr = row.Cells["MBLNR"].ToString().Trim();
            //string strMenge = row.Cells["MENGE"].ToString().Trim();
            //Manage_SapSimulationDataPrint_Split objSplit = new Manage_SapSimulationDataPrint_Split(UserData, strWerks, strLgort, strMblnr, strBoxid, strMenge);
            //objSplit.ShowDialog();
            //dtPrint = objStorageData.GetMCReturnData(strMblnr, "", "", "", "", "", false);
            //ShowdgvBottom();
            dgvGridData.EndEdit();
            dtPrint.AcceptChanges();
            if (ckPrintAgain.Checked)
            {
                stsWarning.Text = "补印数据无法拆分";
                return;
            }
            if (dtPrint.Rows.Count > 0)
            {
                DataRow[] drSelect = dtPrint.Select(" cSelect = true ");
                if (drSelect.Length != 1)
                {
                    stsWarning.Text = "请选择一条数据进行拆箱，并且只能选一条";
                    return;
                }
                else
                {
                    string strBoxid = drSelect[0]["BOXID"].ToString();
                    string strMblnr = drSelect[0]["MBLNR"].ToString();
                    string strMenge = drSelect[0]["MENGE"].ToString();
                    string strLifnr = drSelect[0]["LIFNR"].ToString();
                    //bool bol = false;
                    //bol = objStorageData.QueryCITSplit(drSelect[0]["MBLNR"].ToString(), drSelect[0]["BOXID"].ToString());
                    //if (bol)
                    //{
                    //    stsWarning.Text = "拆箱过后不能再次拆箱";
                    //    return;
                    //}
                    QWMS.MGAUT.Manage_SapSimulationDataPrint_Split objSplit = new QWMS.MGAUT.Manage_SapSimulationDataPrint_Split(UserData, strWerks, strLgort, strMblnr, strBoxid, strMenge, strLifnr);
                    objSplit.ShowDialog();
                    dtPrint = objStorageData.GetIQCReturnData(strWerks, strLgort, strGRno, "", "", false, "");
                    ShowdgvGridData();
                }
            }
        }
        #endregion

        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            stsWarning.Text = "";

            //打印限制
            if (dgvGridData.Rows.Count == 0)
            {
                stsWarning.Text = "";
                stsWarning.Text = "无打印数据!";
                return;
            }
            else
            {
                GetCheckData();
                if (listPrint.Count == 0)
                {
                    stsWarning.Text = "";
                    stsWarning.Text = "未选中任何打印信息!";
                    return;
                }
                else
                {
                    PrintData(listPrint);

                    listPrint.Clear();
                    dtPrint = objStorageData.GetIQCReturnData(strWerks, strLgort, strGRno, strDateFrom, strDateTo, ckPrintAgain.Checked, "");

                }
            }
        }
        #endregion

        #region 打印判票
        /// <summary>
        /// 打印判票
        /// </summary>
        /// <param name="listPrint">所有选中行的BOXID集合,string类型</param>
        private bool PrintData(List<string> listPrint)
        {
            #region 打印机参数限制
            string strCom = null;
            int intBaudRate = -1;
            string strIP = "";
            if (txtIP.Text.Trim() == "")
            {
                //增设打印机串口
                if((txtCom.Text.Trim() == "" || txtBounnd.Text.Trim() == "") )
                {
                    stsWarning.Text = "请输入打印IP或设置打印参数！";
                    return false;
                }
                else
                {

                    {
                        strCom = txtCom.Text.Trim();
                        if (txtBounnd.Text.Trim().ToString() != "")
                        {
                            try
                            {
                                intBaudRate = Convert.ToInt32(txtBounnd.Text.Trim());
                            }
                            catch
                            {
                                stsWarning.Text = "波特率只能是数字，请检查";
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                strIP = txtIP.Text.Trim();
            }
            #endregion 打印机参数限制[END]


            DataTable dt = new DataTable();
            StringBuilder sbSQL = new StringBuilder();

            //修改BOXID格式
            string strwherelist = "(";
            if (listPrint.Count > 0)
            {
                for (int i = 0; i < listPrint.Count; i++)
                {
                    strwherelist = strwherelist + "'" + listPrint[i].ToString() + "',";
                }
                strwherelist = strwherelist.Substring(0, strwherelist.Length - 1);
                strwherelist = strwherelist + ")";
            }
            else
            {
                return false;
            }

            #region 查询T-SQL
            string strBwartFromWhtic = objStorageData.QueryDocWithNoScan(strWerks, strLgort, listPrint[0]).Rows[0]["BWART"].ToString();
            //if (strBwartFromWhtic == "350" && strLgort == "TW30")
            if (strLgort == "TW30")
            {
                sbSQL.AppendFormat("SELECT '' AS QQQ ,BOXID AS MBLNR,LIFNR,MATNR,CHARG,MENGE,LEFT(REFID,20) AS PCODE ,SUBSTRING(REFID,21,20) AS PCODA,SUBSTRING(REFID,41,20) AS PCODB,LGORT,DELNO,DEITM,USNAM,BOXID+';'+MATNR+';'+CHARG+';'+LIFNR+';'+CONVERT(VARCHAR(8), MENGE) AS BarCodeTTL FROM WHTIC WITH(NOLOCK) WHERE BOXID IN " + strwherelist + "  ORDER BY BOXID ");
            }
            else
            {
                sbSQL.AppendFormat("SELECT '' AS QQQ ,BOXID AS MBLNR,BUDAT,KDMAT,KOSTL,LGORT,INSMK,ISNULL(REGION,'') AS REGION,LIFNR,MATNR,CHARG,MENGE,LEFT(REFID,20) AS PCODE ,SUBSTRING(REFID,21,20) AS PCODA,SUBSTRING(REFID,41,20) AS PCODB,LGORT,DELNO,DEITM,USNAM,BOXID+';'+MATNR+';'+CHARG+';'+LIFNR+';'+CONVERT(VARCHAR(8), MENGE) AS BarCodeTTL FROM WHTIC WITH(NOLOCK) WHERE BOXID IN " + strwherelist + " ORDER BY BOXID ");
            }
            try
            {
                dt = objStorageData.GetPrintResult(sbSQL.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "-<PrintData()>");
                return false;
            }
            finally
            {
                //清空sbSQL缓冲字符串
                sbSQL.Length = 0;
            }
            #endregion 查询T-SQL[END]


            StreamReader sr;
            string AllContexttmp = "";

            //打印机模板文件路径
            string strFilePath ;
            //if ((strBwartFromWhtic == "350" || strBwartFromWhtic == "325") && strLgort == "TW30")
            if (strLgort == "TW30")
            {
                strFilePath = Application.StartupPath + "\\report\\REJECT.txt";
            }
            else
            {
                strFilePath = Application.StartupPath + "\\report\\IQC_REJECT.txt";
            }
            //未找到打印机模板文件
            if (!File.Exists(strFilePath))
            {
                stsWarning.Text = "";
                stsWarning.Text = "未找到打印机参数文件!";
                return false;
            }
            else
            {
                sr = new StreamReader(strFilePath, System.Text.Encoding.Default);
                AllContexttmp = sr.ReadToEnd();
                sr.Close();
            }
            //增设打印机串口
            System.IO.Ports.SerialPort SP = null;
            string strprint = "";
            if (strCom != " " && intBaudRate != -1)
            {
                SP = new System.IO.Ports.SerialPort(strCom, intBaudRate, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
            }
            string[] line;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stsWarning.Text = "开始打印";
                string AllContext = AllContexttmp;
                #region 打印
                //增设打印机串口
                if (strCom != "" && intBaudRate != -1 && SP.IsOpen == false)
                {
                    SP.PortName = strCom;
                    SP.BaudRate = intBaudRate;
                    SP.DataBits = 8;
                    SP.Parity = System.IO.Ports.Parity.None;
                    SP.StopBits = System.IO.Ports.StopBits.One;
                    SP.Open();
                }
                try
                {
                    for (int x = 1; x < dt.Columns.Count; x++)
                    {
                        //Zebra 打印机特殊字符^  ，字符和二维码要转化为'_5E' ,条形码要转化为><
                        if (dt.Columns[x].ToString() == "BarCodeTTL" || dt.Columns[x].ToString() == "MATNR")
                        {
                            //AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "><"));
                            AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString().Replace("^", "_5E"));
                        }
                        else
                        {
                            AllContext = AllContext.Replace(dt.Columns[x].Caption.ToString(), dt.Rows[i][x].ToString());
                        }
                    }

                    //利用正则表达式来分解
                    line = System.Text.RegularExpressions.Regex.Split(AllContext, "\r\n");

                    int j = 1;
                    foreach (string ss in line)
                    {
                        if (ss.IndexOf("<NA>") > -1)
                        {
                            continue;
                        }
                        if (j == 1)
                        {
                            AllContext = ss;
                            j = j + 1;
                        }
                        else
                        {
                            AllContext = AllContext + "\r\n" + ss;
                            j = j + 1;
                        }
                    }
                    if(strIP != "")
                    {
                        PrintLabelIP(strIP, AllContext);
                        System.Threading.Thread.Sleep(3500);//粘包
                    }
                    //增设打印机串口
                    else
                    {
                        SP.WriteLine(AllContext);
                        strprint = strprint + i.ToString() + ",";
                        SP.DiscardOutBuffer();
                        SP.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "-<PrintData()>");
                    return false;
                }
                #endregion 打印[END]
            }

            //更新打印状态 ADFLG
            QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(UserData, strWerks, strLgort, "", strProgid);
            objStorageIn.UpdateIQCPrintFalg(strwherelist);
            return true;
        }
            #endregion

        #region IP打印
        private void PrintLabelIP(string strIP, string strLabel)
        {
            string strPort = "9100";
            IPEndPoint hostEndPoint = new IPEndPoint(IPAddress.Parse(strIP), Convert.ToInt32(strPort));
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(hostEndPoint);
            if (!s.Connected)
            {
                MessageBox.Show("Not Connected!");
            }
            else
            {
                byte[] data = Encoding.UTF8.GetBytes(strLabel);
                s.Send(data, data.Length, 0);
                if (s.Connected)
                    s.Close();
            }
        }
        #endregion



        private void GetCheckData()
        {
            for (int i = 0; i < dgvGridData.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvGridData.Rows[i].Cells[0];
                //循环dgv的每一行，判断是否有记录被选中
                if ((bool)dgvGridData.Rows[i].Cells[0].EditedFormattedValue)
                {
                    //获取当前已选中所有行的判票号(即BOXID)
                    listPrint.Add(dgvGridData.Rows[i].Cells["BOXID"].Value.ToString().Trim());
                }
            }
        }


        #region 重绘单选框表头
        //定义继承于DataGridViewColumnHeaderCell的类，用于绘制checkbox，定义checkbox鼠标单击事件  
        public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
        {
            Point checkBoxLocation;
            Size checkBoxSize;
            bool _checked = false;
            Point _cellLocation = new Point();
            System.Windows.Forms.VisualStyles.CheckBoxState _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;

            public event CheckBoxClickedHandler OnCheckBoxClicked;

            public DatagridViewCheckBoxHeaderCell()
            {

            }

            //绘制列头checkbox 
            protected override void Paint(System.Drawing.Graphics graphics,
                                          System.Drawing.Rectangle clipBounds,
                                          System.Drawing.Rectangle cellBounds,
                                          int rowIndex,
                                          DataGridViewElementStates dataGridViewElementState,
                                          object value,
                                          object formattedValue,
                                          string errorText,
                                          DataGridViewCellStyle cellStyle,
                                          DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                          DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value,
                           formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                Point p = new Point();

                Size s = CheckBoxRenderer.GetGlyphSize(graphics, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);

                //列头checkbox的X坐标
                p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2) - 1;

                //列头checkbox的Y坐标
                p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2) - 1;

                _cellLocation = cellBounds.Location;
                checkBoxLocation = p;
                checkBoxSize = s;

                if (_checked)
                {
                    _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
                }
                else
                {
                    _cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
                }

                CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, _cbState);
            }

            //点击列头checkbox单击事件
            protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
            {
                Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);

                if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width
                    && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
                {
                    _checked = !_checked;

                    if (OnCheckBoxClicked != null)
                    {
                        //触发单击事件
                        OnCheckBoxClicked(_checked);
                        this.DataGridView.InvalidateCell(this);
                    }

                }

                base.OnMouseClick(e);
            }

        }

        //定义触发单击事件的委托
        public delegate void CheckBoxClickedHandler(bool state);

        public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
        {
            bool isChecked;

            public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
            {
                isChecked = bChecked;
            }

            public bool Checked
            {
                get
                {
                    return isChecked;
                }
                set
                {
                    isChecked = value;
                }
            }
        }

        #endregion

        #region 点击单选框全选反选事件
        private void chkcell_OnCheckBoxClicked(bool isChecked)
        {
            if (isChecked == true)
            {
                dgvGridData.EndEdit();
                for (int i = 0; i < dgvGridData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvGridData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = true;
                }
            }
            else
            {
                dgvGridData.EndEdit();
                for (int i = 0; i < dgvGridData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvGridData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = false;
                }
            }
        }
        #endregion



    }
}
      