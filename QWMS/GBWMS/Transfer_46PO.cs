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
using QCI_QWMS_StorageData;
using System.Collections;


namespace QWMS
{
    public partial class Transfer_46PO : Form
    {
        #region 变量

        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strPo = "";
        DataTable dtData = new DataTable();
        DataTable dtTemp = new DataTable();
        CarData objCarData;
        PlantData objPlantData;
        Authority objAuthority;
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

        #endregion

        #region 构造函数
        public Transfer_46PO(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            QCI.QWMS.Replenishment Replenishment = new Replenishment(UserData, strProgid);
            try
            {
                Admin admin = new Admin(UserData, strProgid);
                objAuthority = new Authority(UserData);
                objPlantData = new PlantData(UserData);
                objCarData = new CarData(UserData);

                if (!Replenishment.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //初始化
                    ShowDdlWerks();
                    ShowDdlLgort();
                    ShowStatusData(); //秀出Status的資料
                    Query();
                    stsWarning.Text = "";
               }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
#endregion

        #region Query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }
        private void Query()
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.SelectedItem.ToString().Trim();
            }
            else
            {
                strWerks = "";
            }

            strPo = txt46PO.Text.ToString().Trim();
            dtData = objCarData.Query46PO(strWerks, strLgort, strPo,"SAP_46P", chkdone.Checked, dtpo1.Value.ToString("yyyy-MM-dd"), dtPO2.Value.ToString("yyyy-MM-dd"));
            DataColumn dcSelect = new DataColumn("Select", typeof(bool));
            dtData.Columns.Add(dcSelect);
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                dtData.Rows[i]["Select"] = false;
            }
            ShowDataGrid();
            if (dtData.Rows.Count < 0)
            {
                stsWarning.Text = "No Data";
                return;
            }
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 初始化公共方法    
        private void ShowDdlWerks()
        {
            try
            {
                cmbWerks.Items.Clear();
                dtTemp = objPlantData.GetDdlWerksData();
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

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion

        #region ShowDataGrid
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
                dgvcSelect.Selected = false;
                this.dgvData.Columns.Add(dgvcSelect);

                DataGridViewTextBoxColumn dgvcItem = new DataGridViewTextBoxColumn();
                dgvcItem.DataPropertyName = "Item";
                dgvcItem.HeaderText = "Item";
                dgvcItem.Width = 60;
                dgvcItem.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcItem);

                DataGridViewTextBoxColumn dgvcWerks = new DataGridViewTextBoxColumn();
                dgvcWerks.DataPropertyName = "WERKS";
                dgvcWerks.HeaderText = "厂区";
                dgvcWerks.Width = 70;
                dgvcWerks.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcWerks);

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "仓别";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 70;
                this.dgvData.Columns.Add(dgvcLgort);

                DataGridViewTextBoxColumn dgvcMblnr = new DataGridViewTextBoxColumn();
                dgvcMblnr.DataPropertyName = "MBLNR";
                dgvcMblnr.HeaderText = "46PO单号";
                dgvcMblnr.Width = 100;
                dgvcMblnr.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMblnr);


                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.Width = 100;
                dgvcMATNR.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.Width = 60;
                dgvcCHARG.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCHARG);


                DataGridViewTextBoxColumn dgvcMenge = new DataGridViewTextBoxColumn();
                dgvcMenge.DataPropertyName = "MENGE";
                dgvcMenge.HeaderText = "数量";
                dgvcMenge.ReadOnly = true;
                dgvcMenge.Width = 100;
                this.dgvData.Columns.Add(dgvcMenge);

                DataGridViewTextBoxColumn dgvcOTQTY = new DataGridViewTextBoxColumn();
                dgvcOTQTY.DataPropertyName = "OTQTY";
                dgvcOTQTY.HeaderText = "扣帐数量";
                dgvcOTQTY.Width = 120;
                dgvcOTQTY.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcOTQTY);


                DataGridViewTextBoxColumn dgvcDLogrt = new DataGridViewTextBoxColumn();
                dgvcDLogrt.DataPropertyName = "REMAK1";
                dgvcDLogrt.HeaderText = "扣帐单号";
                dgvcDLogrt.Width = 90;
                dgvcDLogrt.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcDLogrt);

                DataGridViewTextBoxColumn dgvcCretim = new DataGridViewTextBoxColumn();
                dgvcCretim.DataPropertyName = "CRDAT";
                dgvcCretim.HeaderText = "创建时间";
                dgvcCretim.Width = 100;
                dgvcCretim.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcCretim);


                DataGridViewTextBoxColumn dgvcmsg = new DataGridViewTextBoxColumn();
                dgvcmsg.DataPropertyName = "MSG";
                dgvcmsg.HeaderText = "状态";
                dgvcmsg.Width = 90;
                dgvcmsg.ReadOnly = true;
                this.dgvData.Columns.Add(dgvcmsg);


                dgvData.DataSource = dtData;
                lblCount.Text = dtData.Rows.Count.ToString() + " records";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }
        #endregion

        #endregion


        private void btnDelete_Click(object sender, EventArgs e)
        {
            string delPO="";
            ArrayList al46po=new ArrayList();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                string strMblnr = dtData.Rows[i]["MBLNR"].ToString().Trim();
                if ((bool)dtData.Rows[i]["Select"] == true)
                {
                    al46po.Add(strMblnr);
                 }
                   
             }
            if (al46po.Count > 0)
            {
                foreach (string id in al46po)
                {
                    delPO += "'" + id + "'" + ",";
                }
                delPO = delPO.Remove(delPO.Length - 1, 1);
                if (objCarData.Del46PO(delPO))
                {
                    stsWarning.Text = "删除成功";
                }
                else
                {
                    stsWarning.Text = "删除失败";
                }
            }
            else
            {
                stsWarning.Text = "请选择要删除的数据！";
            }
       
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ArrayList poList=new ArrayList ();
            strLgort = cmbLgort.Text.ToString();
            string mblnrs = "";

            if (strLgort == "")
            {
                stsWarning.Text = "请选择仓别";
                return;
            }
            if (dtData.Rows.Count > 0)
            {
                foreach (DataRow dr in dtData.Rows)
                { 
                    if((bool)dr["Select"]==true)
                    {
                        poList.Add(dr["MBLNR"].ToString().Trim());
                    }
                }
            }
            if (poList.Count > 0)
            {
                foreach (string mblnr in poList)
                {
                    mblnrs = "'" + mblnr + "'" + ",";
                }
                mblnrs = mblnrs.Remove(mblnrs.Length - 1, 1);
                bool flg = objCarData.UpdateLgort(strLgort,mblnrs);
                if (flg)
                {
                    stsWarning.Text = "仓别维护成功";
                }
                else
                {
                    stsWarning.Text = "仓别维护失败";
                }

            }

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            ArrayList faileID = new ArrayList();
            DataRow[] drSync =dtData.Select("Select=True AND MSG='PO已扣帐,仓库未做出库' ");
            foreach (DataRow dr in drSync)
            {
                string mblnr=dr["REMAK1"].ToString();
                DataTable dtResult = new DataTable();
                try
                {
                    dtResult = objCarData.synchronize46POByHand(mblnr);
                    if (dtResult.Rows[0][0].ToString() == "fail")
                    {
                        faileID.Add(mblnr);
                    }
                }
                catch (Exception ex)
                {
                    stsWarning.Text = ex.ToString();
                    continue;
                }
               
            }
            if (faileID.Count > 0)
            {
                string sbid = "";
                foreach (string id in faileID)
                {
                    sbid += id + ",";
                }
                stsWarning.Text = "扣帐编号：" + sbid + "同步失败";
            }
            else
            {
                stsWarning.Text = "同步成功";
            }

        }
    }
}
