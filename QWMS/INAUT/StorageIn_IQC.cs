using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;
using System.IO;
using QWMS.PP;

namespace QWMS
{
    public partial class StorageIn_IQC : Form
    {
        #region 变量

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strMatnr = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private DataTable dtData = new DataTable();
        private StreamWriter sw = null;
        private string strType = "";
        private DataTable dtGetSAPInfo = new DataTable();

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
        public string Matnr
        {
            get {
                return strMatnr;
            }
            set {
                strMatnr = value;
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

        //构造函数
        public StorageIn_IQC(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                QCI.QWMS.StorageIn StorageIn = new StorageIn(UserData, strProgid);
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);
                QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(UserData);

                //檢查權限
                if (!StorageIn.CheckAuthority(""))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化

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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
            this.txtLocat.Enabled = true;
            this.txtUsrnm.Enabled = true;
        }

        public void Query()
        {
            strWerks = cmbWerks.Text.ToString().Trim();
            strLgort = cmbLgort.Text.ToString().Trim();
            string strMatnr = textPartNo.Text.ToString().Trim();
            string strRef = txtRefDoc.Text.ToString().Trim();
            //查询Data
            StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
            dtData = objStorageData.QueryIQC(strRef,strMatnr);
            DataColumn cSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(cSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }

            ShowDataGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.cmbWerks.SelectedIndex = 0;
            this.cmbLgort.SelectedIndex = 0;
            this.txtRefDoc.Text = "";
            this.txtLocat.Text = "";
            this.txtUsrnm.Text = "";
            this.btnQuery.Enabled = false;
            this.gbFunction.Enabled = true;
            this.txtLocat.Enabled = false;
            this.txtUsrnm.Enabled = false;
            this.rdoAdd.Checked = false;
            this.rdoNew.Checked = false;

        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region 初始化
        //厂别       
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

        //仓别
        private void ShowDdlLgort()
        {
            try
            {
                QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(UserData);

                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                //当前Plant
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                    dtTemp = objAuthority.CheckLgortAuthority();
                //现有值
                if (cmbLgort.SelectedIndex != -1)
                    strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    cmbLgort.Items.Clear();

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
                        //去重复值
                        if (!cmbLgort.Items.Contains(dtTemp.Rows[i]["F_TEXT"].ToString()))
                        {
                            cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                            if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                            {
                                cmbLgort.SelectedIndex = i;
                            }
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

        #region Double click 儲位欄位
        private void txtLocat_DoubleClick(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedIndex != -1)
                    Werks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                else
                    Werks = "";

                if (cmbLgort.SelectedIndex != -1)
                    Lgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                else
                    Lgort = "";

                if (Werks == "" || Lgort == "")
                {
                    stsWarning.Text = "Plant and storage can't be empty!!";
                    return;
                }
                else
                {
                    StorageIn_LocationSelect objStorageIn_LocationSelect = new StorageIn_LocationSelect(UserData, Progid, Werks, Lgort, Type);
                    objStorageIn_LocationSelect.ShowDialog();
                    txtLocat.Text = objStorageIn_LocationSelect.Locat;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 選擇入庫方式-新板入庫(New Pallet)
        private void rdoNew_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "NEW";
            Type = strType;
            gbFunction.Enabled = false;
            this.btnQuery.Enabled = true;
        }
        #endregion
        #region 選擇入庫方式-加料入庫(Add In)
        private void rdoAdd_CheckedChanged(object sender, System.EventArgs e)
        {
            stsWarning.Text = "";
            strType = "ADD";
            Type = strType;
            gbFunction.Enabled = false;
            this.btnQuery.Enabled = true;
        }
        #endregion

        #region 公共方法



        //DataGrid Columns
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "选择";
                dgvcSelect.Width = 50;
                this.dgvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcRefDoc = new DataGridViewTextBoxColumn();
                dgvcRefDoc.DataPropertyName = "RefDoc";
                dgvcRefDoc.HeaderText = "结单单号";
                dgvcRefDoc.Width = 150;
                dgvcRefDoc.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcRefDoc);

                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "Plant";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.Width = 60;
                dgvcWERKS.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcCostCenter = new DataGridViewTextBoxColumn();
                dgvcCostCenter.DataPropertyName = "CostCenter";
                dgvcCostCenter.HeaderText = "部门代码";
                dgvcCostCenter.Width = 100;
                dgvcCostCenter.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCostCenter);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "Material";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 90;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "Batch";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.Width = 50;
                dgvcCHARG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCHARG);



                //DataGridViewTextBoxColumn dgvcDescription = new DataGridViewTextBoxColumn();
                //dgvcDescription.DataPropertyName = "Description";
                //dgvcDescription.HeaderText = "料号描述";
                //dgvcDescription.ReadOnly = true;
                //dgvcDescription.Width = 120;
                //this.dgvData.Columns.Add(dgvcDescription);

                DataGridViewTextBoxColumn dgvcVendor = new DataGridViewTextBoxColumn();
                dgvcVendor.DataPropertyName = "Vendor";
                dgvcVendor.HeaderText = "厂商代码";
                dgvcVendor.ReadOnly = true;
                dgvcVendor.Width = 100;
                this.dgvData.Columns.Add(dgvcVendor);



                DataGridViewTextBoxColumn dgvcMovementType = new DataGridViewTextBoxColumn();
                dgvcMovementType.DataPropertyName = "MovementType";
                dgvcMovementType.HeaderText = "异动代码";
                dgvcMovementType.ReadOnly = true;
                dgvcMovementType.Width = 100;
                dgvcMovementType.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcMovementType);

                DataGridViewTextBoxColumn dgvcToUnrestricted = new DataGridViewTextBoxColumn();
                dgvcToUnrestricted.DataPropertyName = "ToUnrestricted";
                dgvcToUnrestricted.HeaderText = "结单数量";
                dgvcToUnrestricted.ReadOnly = true;
                dgvcToUnrestricted.Width = 100;
                dgvcToUnrestricted.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcToUnrestricted);

                DataGridViewTextBoxColumn dgvcINSMK = new DataGridViewTextBoxColumn();
                dgvcINSMK.DataPropertyName = "INSMK";
                dgvcINSMK.HeaderText = "状态";
                dgvcINSMK.ReadOnly = true;
                dgvcINSMK.Width = 50;
                dgvcINSMK.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcINSMK);

                DataGridViewTextBoxColumn dgvcIssueSlo = new DataGridViewTextBoxColumn();
                dgvcIssueSlo.DataPropertyName = "IssueSlo";
                dgvcIssueSlo.HeaderText = "起始仓别";
                dgvcIssueSlo.ReadOnly = true;
                dgvcIssueSlo.Width = 100;
                dgvcIssueSlo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcIssueSlo);

                DataGridViewTextBoxColumn dgvcRecvSloc = new DataGridViewTextBoxColumn();
                dgvcRecvSloc.DataPropertyName = "RecvSloc";
                dgvcRecvSloc.HeaderText = "目的仓别";
                dgvcRecvSloc.ReadOnly = true;
                dgvcRecvSloc.Width = 100;
                dgvcRecvSloc.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcRecvSloc);

                DataGridViewTextBoxColumn dgvcCRDAT = new DataGridViewTextBoxColumn();
                dgvcCRDAT.DataPropertyName = "CRDAT";
                dgvcCRDAT.HeaderText = "创建时间";
                dgvcCRDAT.ReadOnly = true;
                dgvcCRDAT.Width = 120;
                dgvcCRDAT.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcCRDAT);

                DataGridViewTextBoxColumn dgvcREMARK1 = new DataGridViewTextBoxColumn();
                dgvcREMARK1.DataPropertyName = "REMAK1";
                dgvcREMARK1.HeaderText = "SAP回执";
                dgvcREMARK1.ReadOnly = true;
                dgvcREMARK1.Width = 100;
                dgvcREMARK1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns.Add(dgvcREMARK1);


                dgvData.DataSource = dtData;
                lblData.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        public DataSet SendToSAP(DataTable vardtSend)
        {
            try
            {
                PP_Service obj = new PP_Service();
                DataSet ds = new DataSet();
                ds.Tables.Add(vardtSend);
                DataSet dsResult = obj.ZRFC_IQC_ZMWFC(ds);
                return dsResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            stsWarning.Text = "";
            ShowDdlLgort();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DataTable dtSend = new DataTable();
            dtSend.Columns.Add("WERKS");
            dtSend.Columns.Add("RefDoc");
            dtSend.Columns.Add("TEXT");
            DataRow drRow;
            string strLocat = this.txtLocat.Text.Trim().ToString();
            string strUsrnm = this.txtUsrnm.Text.Trim().ToString();
            QCI.QWMS.PlantData objPlantData = new QCI.QWMS.PlantData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData);

            if (txtLocat.Text.Trim() != "")
            {
                if (!objPlantData.CheckExistedStorageData(Werks, strLgort, strLocat))
                {
                    stsWarning.Text = "The destination location doesn't exist!!";
                    this.txtLocat.Focus();
                    return;
                }
            }
            if (string.IsNullOrEmpty(strLocat))
            {
                MessageBox.Show("储位不可为空!!!");
                return;
            }

            if (string.IsNullOrEmpty(strUsrnm))
            {
                MessageBox.Show("工号不可为空!!!");
                return;
            }

            for (int i = 0; i < dtData.Rows.Count; i++)
            {

                if ((bool)dtData.Rows[i]["Select"] == true)
                {
                    string strRefDoc = dtData.Rows[i]["RefDoc"].ToString().Trim();
                    if (!string.IsNullOrEmpty(strRefDoc))
                    {
                        drRow = dtSend.NewRow();
                        drRow["WERKS"] = strWerks;
                        drRow["RefDoc"] = strRefDoc;
                        dtSend.Rows.Add(drRow);
                    }
                    else
                    {
                        lblData.Text = "勾选的第" + i + "行的RefDoc为空,不可发送文档，请确认！";
                        return;
                    }
                }
            }

            if (dtSend.Rows.Count > 0)
            {
                string strq = "";

                ////----测试
                //DataTable dtGetSAPInfo = dtSend.Copy();
                //------测试
                DataSet dsGetSAPInfo = SendToSAP(dtSend);
                dtGetSAPInfo = dsGetSAPInfo.Tables[0];

                //判断SAP回执回来
                for (int j = 0; j < dtGetSAPInfo.Rows.Count; j++)
                {
                    string strText = dtGetSAPInfo.Rows[j]["TEXT"].ToString().Trim();
                    string strRefDoc = dtGetSAPInfo.Rows[j]["APPNO"].ToString().Trim();

                    if (!string.IsNullOrEmpty(strText))
                    {
                        //objStorageData.UpdateWHDWN(strRefDoc, strText);
                        if (strText == "APPNO Confirm失败!")
                        {
                            MessageBox.Show(strText);
                            return;
                        }
                        strq = strq + "'" + strRefDoc + "'";
                    }
                    else
                    {
                        MessageBox.Show("该接单单号" + strRefDoc + "在SAP不存在，请确认！");
                        return;
                    }
                    //strq = strq + "'" + strRefDoc + "'";
                }

                dtGetSAPInfo.Columns.Add("MANDT", Type.GetType());
                dtGetSAPInfo.Columns.Add("COMCD", Type.GetType());
                //dtGetSAPInfo.Columns.Add("WERKS", Type.GetType());
                dtGetSAPInfo.Columns.Add("LGORT", Type.GetType());
                dtGetSAPInfo.Columns.Add("LOCAT", Type.GetType());
                dtGetSAPInfo.Columns.Add("MATNR", Type.GetType());
                dtGetSAPInfo.Columns.Add("INSMK", Type.GetType());
                dtGetSAPInfo.Columns.Add("MBLNR", Type.GetType());
                dtGetSAPInfo.Columns.Add("ZEILE", Type.GetType());
                dtGetSAPInfo.Columns.Add("CHARG", Type.GetType());
                dtGetSAPInfo.Columns.Add("LIFNR", Type.GetType());
                dtGetSAPInfo.Columns.Add("RMANO", Type.GetType());
                dtGetSAPInfo.Columns.Add("EBELN", Type.GetType());
                dtGetSAPInfo.Columns.Add("INDAT", Type.GetType());
                dtGetSAPInfo.Columns.Add("OTQTY", Type.GetType());
                dtGetSAPInfo.Columns.Add("ALQTY", Type.GetType());
                dtGetSAPInfo.Columns.Add("TRNTP", Type.GetType());
                dtGetSAPInfo.Columns.Add("DACOD", Type.GetType());
                dtGetSAPInfo.Columns.Add("RMAK1", Type.GetType());
                dtGetSAPInfo.Columns.Add("KDMAT", Type.GetType());
                dtGetSAPInfo.Columns.Add("SERNO", Type.GetType());
                dtGetSAPInfo.Columns.Add("KOSTL", Type.GetType());
                dtGetSAPInfo.Columns.Add("ARBPL", Type.GetType());
                dtGetSAPInfo.Columns.Add("MRGID", Type.GetType());
                dtGetSAPInfo.Columns.Add("OMBLNR", Type.GetType());
                dtGetSAPInfo.Columns.Add("MCDAT", Type.GetType());
                for (int i = 0; i < dtGetSAPInfo.Rows.Count; i++)
                {
                    for (int j = 0; j < dtData.Rows.Count; j++)
                    {
                        if (dtGetSAPInfo.Rows[i]["APPNO"].ToString() == dtData.Rows[j]["RefDoc"].ToString())
                        {
                            dtGetSAPInfo.Rows[i]["MANDT"] = Mandt;
                            dtGetSAPInfo.Rows[i]["COMCD"] = Comcd;
                            dtGetSAPInfo.Rows[i]["WERKS"] = Werks;
                            dtGetSAPInfo.Rows[i]["LGORT"] = Lgort;
                            dtGetSAPInfo.Rows[i]["LOCAT"] = strLocat;
                            dtGetSAPInfo.Rows[i]["MATNR"] = dtData.Rows[j]["Material"].ToString();
                            dtGetSAPInfo.Rows[i]["CHARG"] = dtData.Rows[j]["Batch"].ToString();
                            dtGetSAPInfo.Rows[i]["INSMK"] = dtData.Rows[j]["INSMK"].ToString();
                            dtGetSAPInfo.Rows[i]["MBLNR"] = dtData.Rows[j]["RefDoc"].ToString();
                            dtGetSAPInfo.Rows[i]["ZEILE"] = dtData.Rows[j]["ZEILE"].ToString();
                            dtGetSAPInfo.Rows[i]["LIFNR"] = dtData.Rows[j]["Vendor"].ToString();
                            dtGetSAPInfo.Rows[i]["RMANO"] = dtData.Rows[j]["RMANO"].ToString();
                            dtGetSAPInfo.Rows[i]["EBELN"] = dtData.Rows[j]["EBELN"].ToString();
                            dtGetSAPInfo.Rows[i]["INDAT"] = DateTime.Now.ToString("yyyyMMdd");
                            dtGetSAPInfo.Rows[i]["OTQTY"] = dtData.Rows[j]["OTQTY"].ToString();
                            dtGetSAPInfo.Rows[i]["ALQTY"] = dtData.Rows[j]["ToUnrestricted"].ToString();
                            dtGetSAPInfo.Rows[i]["TRNTP"] = dtData.Rows[j]["TRNTP"].ToString();
                            dtGetSAPInfo.Rows[i]["DACOD"] = dtData.Rows[j]["DACOD"].ToString();
                            dtGetSAPInfo.Rows[i]["RMAK1"] ="";
                            dtGetSAPInfo.Rows[i]["KDMAT"] = dtData.Rows[j]["KDMAT"].ToString();
                            dtGetSAPInfo.Rows[i]["SERNO"] = dtData.Rows[j]["SERNO"].ToString();
                            dtGetSAPInfo.Rows[i]["KOSTL"] = dtData.Rows[j]["CostCenter"].ToString();
                            dtGetSAPInfo.Rows[i]["ARBPL"] = dtData.Rows[j]["ARBPL"].ToString();
                            dtGetSAPInfo.Rows[i]["MRGID"] = "";
                            dtGetSAPInfo.Rows[i]["OMBLNR"] = txtUsrnm.Text.Trim();
                            if (Comcd == "9200" && Werks == "CS31" && (Lgort.Substring(0, 2) == "TW" || Lgort.Substring(0, 2) == "TC"))//添加退料日期
                            {
                                dtGetSAPInfo.Rows[i]["MCDAT"] = DateTime.Now.ToString("yyyyMMdd");
                            }
                            
                        }
                    }
                }

                QCI.QWMS.StorageIn objStorageIn = new QCI.QWMS.StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode.ToString(), CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode.ToString(), UserData, Werks, Lgort, "", Progid);
                if (objStorageIn.IQCStorageIn(strUsrnm, dtGetSAPInfo))
                {
                    stsWarning.Text = "入储成功!!";
                    strq = strq.Replace("''", "','");
                    QueryUpdate(strq);
                    this.txtLocat.Text = "";
                    this.txtLocat.Enabled = true;
                    return;
                }
                else
                {
                    stsWarning.Text = "入储失败!! " + objStorageIn.ERRMSG;
                    this.txtLocat.Text = "";
                    this.txtLocat.Enabled = true;
                    return;
                }
            }
            else
            {
                lblData.Text = "没有可以发送的文档，请确认！";
                this.txtLocat.Text = "";
                this.txtLocat.Enabled = true;
                return;
            }


        }

        public void QueryUpdate(string varSql)
        {
            StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
            dtData = objStorageData.QueryDataForIQC(varSql);

            DataColumn cSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(cSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }

            ShowDataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            StorageData objStorageData = new StorageData(UserData, strWerks, strLgort);
            bool blDelete = false;
            for (int i = 0; i < dtData.Rows.Count; i++)
            {

                if ((bool) dtData.Rows[i]["Select"] == true)
                {
                    if (objStorageData.DeleteToWHDWN_BAK(dtData.Rows[i]["RefDoc"].ToString()))
                    {
                        blDelete = true;
                    }         
                }
            }
            if (blDelete == true)
            {
                MessageBox.Show("已删除单据，请IQC重新down数据!");
                Query();
            }
            else
            {
                MessageBox.Show("请选择一笔删除！");
                return;
            }
        }

    }
}
