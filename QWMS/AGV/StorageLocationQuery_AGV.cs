using Newtonsoft.Json;
using QCI.QWMS;
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
    public partial class StorageLocationQuery_AGV : Form
    {
        #region DataMember

        #region 變數宣告
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strProgid = string.Empty;

        private string strWorkStation = string.Empty;//工作站
        private string strTaskId = string.Empty;//任务编号
        private int intTaskSequence = 900;//任务序号

        QCI.QWMS.AGVStorageIn objAGVStorageIn;
        QCI.QWMS.StorageLocation_AGV objStorageLocation_AGV;
        QCI.QWMS.AGVApi objAGVApi;

        #endregion

        #region
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
        #endregion
        public StorageLocationQuery_AGV(UserInfo varUserData, string strProgid)
        {
            InitializeComponent();

            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;
            try
            {
                objAGVStorageIn = new QCI.QWMS.AGVStorageIn(UserData, Progid);
                objStorageLocation_AGV = new QCI.QWMS.StorageLocation_AGV(UserData, Progid);
                objAGVApi = new QCI.QWMS.AGVApi(UserData);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ShowDdlLgort(string strWerks, string strLgort, string strPLACE)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                dtTemp = objStorageLocation_AGV.queryPLACE(strWerks, strLgort, strPLACE);

                cmbTaskID.DisplayMember = "F_VALUE";
                cmbTaskID.ValueMember = "F_VALUE";
                cmbTaskID.DataSource = dtTemp;
                cmbTaskID.SelectedValue = "";
                cmbTaskID.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-cmbTaskID()");
            }
        }


        private void btnEnd_Click(object sender, EventArgs e)
        {
            string strWerks = txtWerksQ.Text.ToString().Trim();
            string strLgort = txtLgortQ.Text.ToString().Trim();
            string strWorkStation = cmbWorkStation.Text.ToString().Trim();
            string strTaskId = cmbTaskID.Text.ToString().Trim();
            if (strWerks == "" || strLgort == "" || strWorkStation == "" || strTaskId == "")
            {
                stsWarning.Text = "请输入完整的条件!!";
                return;
            }

            intTaskSequence = intTaskSequence + 1;
            var data = new
            {
                plant = strWerks,//厂区
                storage = strLgort,//仓别
                work_station = strWorkStation,//工作站
                task_id = strTaskId,//任务编号
                task_sequence = intTaskSequence//任务序号
            };

            // 将对象转换为 JSON 字符串
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            string strResult = objAGVApi.AGVHttpRequest("StorageLocationQuery_AGV", "endTask", json);

            if (string.IsNullOrEmpty(strResult))
            {
                stsWarning.Text = "货架举升中，请稍后结束！";
                return;
            }

            if (objAGVStorageIn.DeleteAGVShelf(strWerks, strLgort, strTaskId, strWorkStation))
            {
                stsWarning.Text = "任务已结束！";
            }


        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strWerks = txtWerks.Text.ToString().Trim();
            string strLgort = txtLgort.Text.ToString().Trim();
            string strSize = cmbSize.Text.ToString().Trim();
            string strMarNo = txtMarNo.Text.ToString().Trim();

            if (strWerks == "" && strLgort == "" && strSize == "" && strMarNo == "")
            {
                stsWarning.Text = "请输入查询条件!!";
                return;
            }
            if (strSize == "" && strMarNo == "")
            {
                stsWarning.Text = "请选择尺寸或料架查询!!";
                return;
            }
            DataTable dtAGVQuery = new DataTable();
            dtAGVQuery = objStorageLocation_AGV.queryLocat(strWerks, strLgort, strSize, strMarNo);
            dgvAGV.DataSource = dtAGVQuery;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtWerks.Text = "";
            txtLgort.Text = "";
            cmbSize.Text = "";
            txtMarNo.Text = "";
        }

        private void btnCheckLocat_Click(object sender, EventArgs e)
        {
            string strWerks = txtWerks_C.Text.ToString().Trim();
            string strLgort = txtLgort_C.Text.ToString().Trim();
            string strLocat = txtLocat_C.Text.ToString().Trim();

            if (strWerks == "" || strLgort == "" || strLocat == "")
            {
                stsWarning.Text = "请输入完整的校验条件!!";
                return;
            }
            DataTable dtGetWhHedLOCAT = new DataTable();
            dtGetWhHedLOCAT = objAGVStorageIn.GetWhHedLOCAT(strWerks, strLgort, strLocat);
            for (int i = 0; i < dtGetWhHedLOCAT.Rows.Count; i++)
            {
                objAGVStorageIn.CheckLocation(strWerks, strLgort, dtGetWhHedLOCAT.Rows[i]["LOCAT"].ToString().Trim());
            }
            txtLocat_C.Text = "";
            txtLocat_C.Focus();
            stsWarning.Text = "储位比对完成!!";
            return;
        }

        private void btnUpdateLocat_Click(object sender, EventArgs e)
        {
            string strWerks = txtWerks_C.Text.ToString().Trim();
            string strLgort = txtLgort_C.Text.ToString().Trim();
            string strLocat = txtLocat_C.Text.ToString().Trim();
            if (strWerks == "" || strLgort == "" || strLocat == "")
            {
                stsWarning.Text = "请输入完整的更新条件!!";
                return;
            }
            objStorageLocation_AGV.updateWhitm(strWerks, strLgort, strLocat);
            stsWarning.Text = "更新成功!!";

        }

        private void cmbWorkStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strWerks = txtWerksQ.Text.ToString().Trim();
            string strLgort = txtLgortQ.Text.ToString().Trim();
            if (strWerks == "" || strLgort == "" )
            {
                cmbWorkStation.Text = "";
                stsWarning.Text = "请输入厂区、仓别!!";
                return;
            }
            string strPLACE = cmbWorkStation.Text.ToString().Trim();
            if (strPLACE == "" )
            {
                cmbWorkStation.Text = "";
                stsWarning.Text = "请输入工作站!!";
                return;
            }

            ShowDdlLgort(strWerks, strLgort, strPLACE);
        }

    }
}
