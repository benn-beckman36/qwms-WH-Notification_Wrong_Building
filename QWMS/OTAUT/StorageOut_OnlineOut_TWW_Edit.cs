using NPOI.SS.UserModel;
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

namespace QWMS.OTAUT
{
    public partial class StorageOut_OnlineOut_TWW_Edit : Form
    {
        #region constractor
        private StorageOut_OnlineOut_TWW_Edit() {
            InitializeComponent();
        }

        public StorageOut_OnlineOut_TWW_Edit(UserInfo varUserData, string strProgid) : this() {
            this.UserData = varUserData;
            Mandt = this.UserData.Client;
            Usrnm = this.UserData.UserId;
            Comcd = this.UserData.CompanyCode;
            Progid = strProgid;
        }
        #endregion
        #region variables
        private UserInfo UserData { get; set; }
        private string Usrnm { get; set; }
        private string Mandt { get; set; }
        private string Comcd { get; set; }
        private string Progid { get; set; }
        private string strWerks { get; set; }
        private string strContent { get; set; }
        private string strType { get; set; }
        public DataTable dtSource { get; set; }
        private DataTable dtView { get; set; }
        private List<string> lstZeile { get; set; }
        // 初始化列
        Dictionary<string, string> colMapping = new Dictionary<string, string>();
        #endregion

        #region events
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e) {
            // 重置cmbVbeln的值
            this.cmbVbeln.SelectedIndex = -1;
            // 初始化选项
            this.lstZeile.Clear();
            // 类型共分为填写PO和填写GR两种
            this.strType = this.cmbType.Text.Trim();
            // 类型为GR时，允许单条填写，enable item选框
            if (this.strType == "GR") {
                this.dtView.Rows.Clear();
                this.cmbVbeln.Enabled = true;
                this.txtContent.Enabled = true;
            } else if (this.strType == "PO") {
                // 类型为PO时，不允许单条填写，disable item选框
                this.cmbVbeln.Enabled = false;
                this.txtContent.Enabled = true;
                //清空cmbVbeln的值
                this.cmbVbeln.SelectedIndex = -1;
            } else if (this.strType == "Storage") {
                // 类型为Storage时，允许单条填写，enable item选框
                this.cmbVbeln.Enabled = true;
                this.txtContent.Enabled = true;
                this.lstZeile.AddRange(this.dtSource.AsEnumerable().Select(s => s.Field<string>("ZEILE")).ToList());
            }
            this.ShowDataGridView();
        }
        private void cmbVbeln_SelectedIndexChanged(object sender, EventArgs e) {
            // 根据选择的类型和选择的EC单号进行筛选
            //修改dtSource里面这一行，重置PONUM和GRNUM为""
            if (this.strType == "PO") {
                this.dtSource.AsEnumerable().Where(w => w.Field<string>("ZEILE") == this.cmbVbeln.Text.Trim()).ToList().ForEach(f => {
                    f.SetField("PONUM", "");
                    f.SetField("GRNUM", "");
                });
            } else if (this.strType == "Storage" && string.IsNullOrEmpty(this.cmbVbeln.Text.Trim())) {
                this.lstZeile.AddRange(this.dtSource.AsEnumerable().Select(s => s.Field<string>("ZEILE")).ToList());
            } else if (this.strType == "GR") {
                if (new List<int>() { 0, -1 }.Contains(this.cmbVbeln.SelectedIndex) || this.cmbVbeln.Text.Trim().Equals("ALL")) {
                    this.lstZeile.AddRange(this.dtSource.AsEnumerable().Select(s => s.Field<string>("ZEILE")).ToList());
                } else {
                    this.lstZeile.Clear();
                    this.lstZeile.Add(this.cmbVbeln.Text.Trim());
                }
            }
            this.ShowDataGridView();
        }
        private void txtContent_KeyPress(object sender,KeyPressEventArgs e) {
            // 不为回车键则不执行
            if(e.KeyChar != 13) {
                return;
            }
            // 获取输入的值
            this.strContent = this.txtContent.Text.Trim().ToUpper();
            if(string.IsNullOrEmpty(this.strContent)) {
                MessageBox.Show("请输入值");
                return;
            }
            // 根据类型进行筛选
            if(this.strType == "PO") {
                // 检测是否为10位数字
                if(this.strContent.Length != 10 || !this.strContent.All(char.IsDigit)) {
                    MessageBox.Show("请输入10位数字");
                    return;
                }
                // 一个EC/发运订单只能对应一个PO
                this.dtSource.AsEnumerable().ToList().ForEach(f => f.SetField<string>("PONUM", this.strContent));
            }
            else if(this.strType == "GR") {
                // 检测是否为10位数字
                if (this.strContent.Length != 10 || !this.strContent.All(char.IsDigit)) {
                    MessageBox.Show("请输入10位数字");
                    return;
                }
                // 一个发运订单Item对应一个GR
                this.dtSource.AsEnumerable().Where(w => this.lstZeile.Contains(w.Field<string>("ZEILE"))).ToList().ForEach(f => f.SetField<string>("GRNUM", this.strContent));
            }
            else if (this.strType == "Storage") {
                // 必须是4位无空格
                if (this.strContent.Length != 4 || this.strContent.Any(char.IsWhiteSpace)) {
                    this.txtContent.Text = "";
                    MessageBox.Show("请输入4位无空格的仓别");
                    return;
                }
                // 检测是否有当前仓库的权限
                Authority objAuthority = new Authority(this.UserData);
                PlantData objPlant = new PlantData(this.UserData);
                //if (objAuthority.CheckLgortAuthority(this.strWerks).AsEnumerable().Where(w => w.Field<string>("F_VALUE").Equals(this.strContent)).Count() <= 0) {
                //    MessageBox.Show("没有当前仓库的权限");
                //    return;
                //}
                // 查询仓别是否存在
                if (!objPlant.GetDdlLgortData().AsEnumerable().Any(w => w.Field<string>("F_VALUE").Equals(this.strContent))) {
                    MessageBox.Show("仓别不存在");
                    return;
                }
                if (lstZeile.Count > 0) {
                    this.dtSource.AsEnumerable().Where(w => this.lstZeile.Contains(w.Field<string>("ZEILE"))).ToList().ForEach(f => f.SetField<string>("UMLGO", this.strContent));
                } else {
                    this.dtSource.AsEnumerable().ToList().ForEach(f => f.SetField<string>("UMLGO", this.strContent));
                }
            }
            else {
                MessageBox.Show("请选择类型");
                return;
            }
            this.ShowDataGridView();
        }

        private void btnConfirm_Click(object sender, EventArgs e) {
            //if(this.dtSource.AsEnumerable().Any(w => 
            //string.IsNullOrEmpty(w.Field<string>("GRNUM")) 
            //    && string.IsNullOrEmpty(w.Field<string>("PONUM"))
            //    && string.IsNullOrEmpty(w.Field<string>("UMLGO"))
            //)) {
            //    MessageBox.Show("有未填写完成的资料，请确认！");
            //    return;
            //}
            // 添加MVT （311/321/351）
            this.dtSource.AsEnumerable().ToList().ForEach(f => {
                // 46po和50GR不能同时存在
                // 先判断
                //MVT栏位判断条件：Insmk为G，同时无46PO，MVT为311
                if (f.Field<string>("INSMK") == "G" && string.IsNullOrEmpty(f.Field<string>("PONUM")) && string.IsNullOrEmpty(f.Field<string>("GRNUM"))) {
                    f.SetField<string>("BWART", "311");
                }
                // Insmk 为 G，同时有46PO，MVT为351
                else if (f.Field<string>("INSMK") == "G" && !string.IsNullOrEmpty(f.Field<string>("PONUM")) && string.IsNullOrEmpty(f.Field<string>("GRNUM"))) {
                    f.SetField("BWART", "351");
                }
                //Insmk为0，有GR，MVT为321
                else if (f.Field<string>("INSMK") == "0" && !string.IsNullOrEmpty(f.Field<string>("GRNUM")) && string.IsNullOrEmpty(f.Field<string>("PONUM"))) {
                    f.SetField("BWART", "321");
                }
            });
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e) {
            this.dtSource.AsEnumerable().ToList().ForEach(f => {
                f.SetField("PONUM", "");
                f.SetField("GRNUM", "");
                f.SetField("UMLGO", "");
                f.SetField("BWART", "");
            });
            //// 添加MVT （311/321/351）
            this.dtSource.AsEnumerable().ToList().ForEach(f => {
                // 46po和50GR不能同时存在
                //MVT栏位判断条件：Insmk为G，同时无46PO，MVT为311
                if (f.Field<string>("INSMK") == "G" && string.IsNullOrEmpty(f.Field<string>("PONUM")) && string.IsNullOrEmpty(f.Field<string>("GRNUM"))) {
                    f.SetField<string>("BWART", "311");
                }
                // Insmk 为 G，同时有46PO，MVT为351
                else if (f.Field<string>("INSMK") == "G" && !string.IsNullOrEmpty(f.Field<string>("PONUM")) && string.IsNullOrEmpty(f.Field<string>("GRNUM"))) {
                    f.SetField("BWART", "351");
                }
                //Insmk为0，有GR，MVT为321
                else if (f.Field<string>("INSMK") == "0" && !string.IsNullOrEmpty(f.Field<string>("GRNUM")) && string.IsNullOrEmpty(f.Field<string>("PONUM"))) {
                    f.SetField("BWART", "321");
                }
            });
            this.Close();
        }
        #endregion

        #region methods
        public void InitData() {
            // 循环写入EC/发运订单号到cmbVbeln
            this.cmbVbeln.Items.Add("ALL");
            foreach(DataRow dr in this.dtSource.Rows) {
                this.cmbVbeln.Items.Add(dr["ZEILE"].ToString());
            }
            // 循环写入类型到cmbType
            this.cmbType.Items.Add("Storage");
            this.cmbType.Items.Add("PO");
            this.cmbType.Items.Add("GR");
            // 初始化列
            this.colMapping.Add("MBLNR", "Doc. No.");
            this.colMapping.Add("ZEILE", "Item");
            this.colMapping.Add("LGORT", "Storage");
            this.colMapping.Add("MATNR", "Material No.");
            this.colMapping.Add("MENGE", "Qty");
            this.colMapping.Add("Content", "PO/GR");
            // 初始化dtView
            this.dtView = new DataTable();
            // 初始化dtView列
            foreach(var item in colMapping) {
                this.dtView.Columns.Add(item.Key);
            }
            // 初始化接收列表
            this.lstZeile = new List<string>();
        }
        private void ShowDataGridView() {
            // 检查类型是否选择
            if(string.IsNullOrEmpty(this.strType)) {
                MessageBox.Show("请选择类型");
                return;
            }
            // 清除展示数据
            this.dtView.Rows.Clear();
            // 数据源添加数据
            // 当类型为PO时，显示全部
            if (this.cmbType.Text.Trim() == "PO") {
                foreach (DataRow dr in this.dtSource.Rows) {
                    DataRow drv = this.dtView.NewRow();
                    drv["MBLNR"] = dr["MBLNR"];
                    drv["ZEILE"] = dr["ZEILE"];
                    drv["LGORT"] = dr["UMLGO"];
                    drv["MATNR"] = dr["MATNR"];
                    drv["MENGE"] = dr["MENGE"];
                    drv["Content"] = dr["PONUM"].ToString().Trim() + dr["GRNUM"].ToString().Trim();
                    this.dtView.Rows.Add(drv);
                }
            } else if (this.cmbType.Text.Trim() == "GR") {
                // 类型为GR时，显示选择的EC/发运订单号
                this.dtSource.AsEnumerable().Where(w => this.lstZeile.Contains(w.Field<string>("ZEILE"))).ToList().ForEach(f => {
                    DataRow drv = this.dtView.NewRow();
                    drv["MBLNR"] = f["MBLNR"];
                    drv["ZEILE"] = f["ZEILE"];
                    drv["LGORT"] = f["UMLGO"];
                    drv["MATNR"] = f["MATNR"];
                    drv["MENGE"] = f["MENGE"];
                    drv["Content"] = f["PONUM"].ToString().Trim() + f["GRNUM"].ToString().Trim();
                    this.dtView.Rows.Add(drv);
                });
            } else if (this.cmbType.Text.Trim() == "Storage") {
                // 类型为Storage时，显示选择的EC/发运订单号
                this.dtSource.AsEnumerable().Where(w => this.lstZeile.Contains(w.Field<string>("ZEILE"))).ToList().ForEach(f => {
                    DataRow drv = this.dtView.NewRow();
                    drv["MBLNR"] = f["MBLNR"];
                    drv["ZEILE"] = f["ZEILE"];
                    drv["LGORT"] = f["UMLGO"];
                    drv["MATNR"] = f["MATNR"];
                    drv["MENGE"] = f["MENGE"];
                    drv["Content"] = f["PONUM"].ToString().Trim() + f["GRNUM"].ToString().Trim();
                    this.dtView.Rows.Add(drv);
                });
            }
            // 检查数据源是否为空
            if ((this.dtView == null || this.dtView.Rows.Count == 0) && this.lstZeile.Count > 0) {
                MessageBox.Show("没有数据");
                return;
            }
            // 检查数据源是否包含列
            if(this.dtView.Columns.Count == 0) {
                MessageBox.Show("没有数据");
                return;
            }
            // 设置gridview 属性
            this.dgv.AutoGenerateColumns = false;
            this.dgv.Columns.Clear();
            // 循环添加列名到gridview
            foreach(var item in colMapping) {
                DataGridViewTextBoxColumn dgvCol = new DataGridViewTextBoxColumn();
                dgvCol.DataPropertyName = item.Key;
                dgvCol.HeaderText = item.Value;
                dgvCol.Width = 100;
                dgvCol.ReadOnly = true;
                this.dgv.Columns.Add(dgvCol);
            }
            this.dgv.DataSource = this.dtView;
        }
        #endregion

    }
}
