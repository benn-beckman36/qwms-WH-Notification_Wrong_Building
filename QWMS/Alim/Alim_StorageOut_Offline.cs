using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class Alim_StorageOut_Offline : Form
    {

        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        /// <summary>
        /// 该页面异动
        /// </summary>
        private string strProgid = "";//QWMS系统基础资料维护权限
        /// <summary>
        /// 所选厂区
        /// </summary>
        private string strWerks = "";
        /// <summary>
        /// 所选仓别
        /// </summary>
        private string strLgort = "";
        /// <summary>
        /// DateCode
        /// </summary>
        private string strDateCode = "";
        /// <summary>
        /// 所填料号
        /// </summary>
        private string strPN = "";
        /// <summary>
        /// 储位
        /// </summary>
        private string strLocat = "";
        /// <summary>
        /// 生成的流水号
        /// </summary>
        private string strNum = "";
        /// <summary>
        /// 查询库存的数据
        /// </summary>
        DataTable dtData = new DataTable();

        QCI.QWMS.Alim objAlim;
        #endregion

        #region 构造函数
        public Alim_StorageOut_Offline(UserInfo varUserData, string Progid)
        {
            InitializeComponent();
            UserData = varUserData;
            strMandt = UserData.Client;
            strComcd = UserData.CompanyCode;
            strUsrnm = UserData.UserId;
            strProgid = Progid;


            objAlim = new QCI.QWMS.Alim(UserData, strProgid);

            try
            {
                //检查权限
                if (!objAlim.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    //秀出Status的資料
                    ShowStatusData();
                    ShowDdlWerks();//厂区
                    ShowDdlLgort();//仓别
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        # region 显示状态栏
        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = strMandt;
            this.stsUsrnm.Text = strUsrnm;
            this.stsComcd.Text = strComcd;

        }
        # endregion

        #region 厂区和仓别
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

        private void ShowDdlLgort()
        {
            try
            {
                stsWarning.Text = string.Empty;
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAlim.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAlim.CheckLgortAuthority();
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
                    strLgort = string.Empty;
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["CTRLC1"].ToString());
                        if (dtTemp.Rows[i]["CTRLC1"].ToString() == strLgort && strLgort != "")
                        {
                            cmbLgort.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        private void cmbWerks_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbLgort.SelectedIndex = -1;
            ShowDdlLgort();
        }

        /// <summary>
        /// 获取所选的厂区和仓别
        /// </summary>
        public void GetWerksAndLgort()
        {
            if (cmbWerks.SelectedIndex != -1)
            {
                strWerks = cmbWerks.SelectedItem.ToString();
            }
            else
            {
                strWerks = string.Empty;
            }
            if (cmbLgort.SelectedIndex != -1)
            {
                strLgort = cmbLgort.SelectedItem.ToString();
            }
            else
            {
                strLgort = string.Empty;
            }
        }
        #endregion

        #region ShowDataView
        private void ShowDataView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                if (!dtData.Columns.Contains("Select"))
                {
                    DataColumn cSelect = new DataColumn("Select", typeof(bool));
                    dtData.Columns.Add(cSelect);
                }

                DataGridViewCheckBoxColumn dgvcSelect = new DataGridViewCheckBoxColumn();
                dgvcSelect.DataPropertyName = "Select";
                dgvcSelect.HeaderText = "选择";
                dgvcSelect.Width = 50;
                dgvcSelect.Selected = false;
                this.gvData.Columns.Add(dgvcSelect);


                DataGridViewTextBoxColumn dgvcWERKS = new DataGridViewTextBoxColumn();
                dgvcWERKS.DataPropertyName = "WERKS";
                dgvcWERKS.HeaderText = "厂区";
                dgvcWERKS.ReadOnly = true;
                this.gvData.Columns.Add(dgvcWERKS);

                DataGridViewTextBoxColumn dgvcLGORT = new DataGridViewTextBoxColumn();
                dgvcLGORT.DataPropertyName = "LGORT";
                dgvcLGORT.HeaderText = "仓别";
                dgvcLGORT.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLGORT);

                DataGridViewTextBoxColumn dgvcLOCAT = new DataGridViewTextBoxColumn();
                dgvcLOCAT.DataPropertyName = "LOCAT";
                dgvcLOCAT.HeaderText = "储位";
                dgvcLOCAT.ReadOnly = true;
                this.gvData.Columns.Add(dgvcLOCAT);

                DataGridViewTextBoxColumn dgvcMATNR = new DataGridViewTextBoxColumn();
                dgvcMATNR.DataPropertyName = "MATNR";
                dgvcMATNR.HeaderText = "料号";
                dgvcMATNR.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMATNR);

                DataGridViewTextBoxColumn dgvcCHARG = new DataGridViewTextBoxColumn();
                dgvcCHARG.DataPropertyName = "CHARG";
                dgvcCHARG.HeaderText = "版本";
                dgvcCHARG.ReadOnly = true;
                this.gvData.Columns.Add(dgvcCHARG);

                DataGridViewTextBoxColumn dgvcMENGE = new DataGridViewTextBoxColumn();
                dgvcMENGE.DataPropertyName = "MENGE";
                dgvcMENGE.HeaderText = "数量";
                dgvcMENGE.ReadOnly = true;
                this.gvData.Columns.Add(dgvcMENGE);

                DataGridViewTextBoxColumn dgvcDACOD = new DataGridViewTextBoxColumn();
                dgvcDACOD.DataPropertyName = "DACOD";
                dgvcDACOD.HeaderText = "DateCode";
                dgvcDACOD.ReadOnly = true;
                this.gvData.Columns.Add(dgvcDACOD);

                DataGridViewTextBoxColumn dgvcREMAK = new DataGridViewTextBoxColumn();
                dgvcREMAK.DataPropertyName = "REMAK";
                dgvcREMAK.HeaderText = "REMAK";
                dgvcREMAK.ReadOnly = true;
                this.gvData.Columns.Add(dgvcREMAK);

                this.gvData.DataSource = dtData;
                lbrecords.Text = dtData.Rows.Count.ToString() + " records";
                gvData.ClearSelection();
                gvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataView()");
            }
        }
        #endregion

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            GetWerksAndLgort();
            if (strWerks == "" || strLgort == "")
            {
                stsWarning.Text = "请选择厂区和仓别";
                return;
            }
            strPN = txtPN.Text.ToString().Trim();
            strDateCode = txtDateCode.Text.ToString().Trim();
            strLocat = txtLocat.Text.ToString().Trim();


            dtData = objAlim.QuaryAlimAlitm(strWerks, strLgort, strPN, strDateCode, strLocat);
            ShowDataView();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow[] drOut = dtData.Select("Select='True'");
            if (drOut.Length == 0)
            {
                stsWarning.Text = "请选择至少一条数据出库";
                return;
            }
            btnSave.Enabled = false;
            chkPRI.Enabled = false;
            //获取流水单号
            strNum = objAlim.GetAlimNo(drOut[0]["WERKS"].ToString());

            //优先出取消
            //增加是否同步Alim

            //whitm 更改材料状态（待下架）、whlog,出库临时表
            if (objAlim.StorageOut_Offline(drOut,chkPRI.Checked,strNum))
            {
                stsWarning.Text = "出库成功";
            }
            else
            {
                stsWarning.Text = "出库失败，请联系QWMS负责人";
            }            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtDateCode.Text = "";
            txtPN.Text = "";
            lbrecords.Text = "0 records";
            strWerks = "";
            strLgort = "";
            stsWarning.Text = "";
            dtData.Rows.Clear();
            cmbWerks.SelectedIndex = -1;
            btnSave.Enabled = true;
            chkPRI.Checked = false;
            chkPRI.Enabled = true;
            strNum = "";
            txtLocat.Text = "";
        }

    }
}
