using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI_QWMS_Models;
using QWMS.Common;

namespace QWMS.Models
{
    public partial class MaintainModelBaseInfo : Form
    {
        ModelInfo objData ;
        ModelBaseInfo objfm;
        UserInfo UserData = new UserInfo();
        string ModelNO = string.Empty;
        public MaintainModelBaseInfo(UserInfo varUserInfo,ModelBaseInfo obj, string strModelNO)
        {
            InitializeComponent();
            ModelNO = strModelNO; 
            UserData = varUserInfo;
            objData = new ModelInfo(UserData);
            objfm = obj;
            if (!string.IsNullOrEmpty(strModelNO))
            {
                BindData();
                txtModelNo.Enabled = false;
            }
        }

        private void BindData()
        {
            DataTable dt = new DataTable();
            dt = objData.GetModelInfo(ModelNO, "");
            txtModelNo.Text = dt.Rows[0]["ModelNO"].ToString().Trim();//模具号
            txtAssetsNo.Text = dt.Rows[0]["AssetsNo"].ToString().Trim();//资产编号
            txtItemName.Text = dt.Rows[0]["ItemName"].ToString().Trim();//品名
            txtBU.Text = dt.Rows[0]["BU"].ToString().Trim();//BU
            txtMachine.Text = dt.Rows[0]["Machine"].ToString().Trim();//机种
            txtQuantity.Text = dt.Rows[0]["Quantity"].ToString().Trim();//数量
            txtNetWeight.Text = dt.Rows[0]["NWeight"].ToString().Trim();//净重
            txtPO.Text = dt.Rows[0]["PoNo"].ToString().Trim();//PO
            txtRemark.Text = dt.Rows[0]["Remark"].ToString().Trim();//备注
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strModelNo = txtModelNo.Text.ToString().Trim();//模具号
            if (strModelNo.Length > 20)
            {

            }
            string strAssetsNo = txtAssetsNo.Text.ToString().Trim();//资产编号
            string strItemName = txtItemName.Text.ToString().Trim();//品名
            string strBU = txtBU.Text.ToString().Trim();//BU
            string strMachine = txtMachine.Text.ToString().Trim();//机种
            decimal decQuantity = 0;
            decimal decNetWeight = 0;
            string strQuantity = txtQuantity.Text.ToString().Trim();//数量
            string strNetWeight = txtNetWeight.Text.ToString().Trim();//净重
            if (!string.IsNullOrEmpty(strQuantity))
            {
                decQuantity = Convert.ToDecimal(strQuantity);
            }
            if (!string.IsNullOrEmpty(strNetWeight))
            {
                decNetWeight = Convert.ToDecimal(strNetWeight);
            }
            string strPO = txtPO.Text.ToString().Trim();//PO
            string strRemark = txtRemark.Text.ToString().Trim();//备注
            if (string.IsNullOrEmpty(ModelNO))//新增
            {
                //检查模具是否已经维护
                DataTable dtModel = new DataTable();
                dtModel = objData.GetModelInfo(strModelNo, "");
                if (dtModel.Rows.Count > 0)
                {
                    MessageBox.Show("此模具号已经维护！");
                    return;
                }
                if (!objData.AddModelInfo(strModelNo, strAssetsNo, strItemName, strBU, strMachine, decQuantity, decNetWeight, strPO, strRemark))
                {
                    MessageBox.Show("新增失败！");
                    return;
                }
                else
                {
                    MessageBox.Show("新增成功！");
                    txtModelNo.Text = "";
                    txtAssetsNo.Text = "";
                    txtItemName.Text = "";
                    txtBU.Text = "";
                    txtMachine.Text = "";
                    txtQuantity.Text = "";
                    txtNetWeight.Text = "";
                    txtPO.Text = "";
                    txtRemark.Text = "";
                }
            }
            else//修改
            {
                if (objData.UpdateModelInfo(strModelNo,strAssetsNo,strItemName,strBU,strMachine,decQuantity,decNetWeight,strPO,strRemark))
                {
                    this.FindForm().DialogResult = DialogResult.OK;
                    MessageBox.Show("保存成功！");
                }
                else
                {
                    MessageBox.Show("修改失败!");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
        }
    }
}
