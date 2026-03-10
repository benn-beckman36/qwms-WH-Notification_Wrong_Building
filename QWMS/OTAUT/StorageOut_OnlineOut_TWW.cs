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
    public partial class StorageOut_OnlineOut_TWW : Form
    {
        #region variables
        // 设定标记，用于判断是否关闭窗体
        private bool blnClose = true;
        private UserInfo UserData { get; set; }
        private string Usrnm { get; set; }
        private string Mandt { get; set; }
        private string Comcd { get; set; }
        private string Progid { get; set; }
        private string strWerks { get; set; }
        private string strMblnr { get; set; }
        private string strLgort { get; set; }
        private string strUmlgo { get; set; }
        private DataTable dtData { get; set; }
        private DataTable dtPlant { get; set; }
        private DataTable dtStorage { get; set; }
        private DataTable dtSend { get; set; }
        private Dictionary<string, string> colMapping = new Dictionary<string, string>();
        #endregion

        #region constractor
        public StorageOut_OnlineOut_TWW() {
            InitializeComponent();
        }
        public StorageOut_OnlineOut_TWW(UserInfo varUserData,string strProgid) : this() {
            this.UserData = varUserData;
            Mandt = this.UserData.Client;
            Usrnm = this.UserData.UserId;
            Comcd = this.UserData.CompanyCode;
            Progid = strProgid;
            this.InitData();
            this.ButtonStatus("StandBy");
            this.DataStatus("StandBy");
        }
        #endregion
        #region events

        private void btnFresh_Click(object sender, EventArgs e) {
            this.ButtonStatus("StandBy");
            this.DataStatus("StandBy");
        }
        private void btnConfirm_Click(object sender, EventArgs e) {
            this.ButtonStatus("Processing");
            this.DataStatus("Processing");
            if(string.IsNullOrEmpty(this.strMblnr)) {
                MessageBox.Show("请输入发运单号或EC单号");
                return;
            }
            if(string.IsNullOrEmpty(this.strWerks)) {
                MessageBox.Show("请选择厂区");
                return;
            }
            if(string.IsNullOrEmpty(this.strLgort)) {
                MessageBox.Show("请选择仓别");
                return;
            }
            PlantData plantData = new PlantData(this.UserData);
            this.dtData = plantData.GetOutDataFromTWW(this.strWerks,this.strMblnr);
            // 检查是否已扣账成功、未扣账、扣账失败
            if (dtData.AsEnumerable().Any(w => w.Field<string>("OMFLG").Equals("Y"))) {
                MessageBox.Show("此单已扣账完成，请勿重复操作！");
                this.dtData.Clear();
                return;
            } else if (dtData.AsEnumerable().Any(a => !string.IsNullOrEmpty(a.Field<string>("BWART")) && string.IsNullOrEmpty(a.Field<string>("OMFLG")))) {
                MessageBox.Show("此单正在扣账中，请勿重复操作！");
                this.dtData.Clear();
                return;
            } else if (dtData.AsEnumerable().Any(a => !string.IsNullOrEmpty(a.Field<string>("BWART")) && a.Field<string>("OMFLG").Equals("N"))) {
                MessageBox.Show("此单扣账失败，请检查并重新填写数据！");
            }
            // 获取当前厂区对应costcenter
            string costcenter = plantData.GetPlantCostcenter(this.strWerks);
            dtData.AsEnumerable().ToList().ForEach(f => { f.SetField<string>("LGORT", this.strLgort); f.SetField<string>("KOSTL", costcenter); f.SetField<string>("COMCD", this.Comcd); });
            // 新增Total行
            DataRow drTotal = this.dtData.NewRow();
            drTotal["MBLNR"] = "Total";
            drTotal["MENGE"] = this.dtData.AsEnumerable().Sum(s => s.Field<int>("MENGE"));
            this.dtData.Rows.Add(drTotal);
            this.DataGridViewShows();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            this.ButtonStatus("Sending");
            try {
                // dtData 去掉Total行
                this.dtData.Rows.RemoveAt(this.dtData.Rows.Count - 1);
                //出储时，MVT为351，则46PO不能为空；MVT为321则50GR不能为空
                if (this.dtData.AsEnumerable().Any(a => a.Field<string>("BWART") == "351" && string.IsNullOrEmpty(a.Field<string>("PONUM")))) {
                    MessageBox.Show("351异动需提供PO单据信息！");
                    return;
                }
                if(this.dtData.AsEnumerable().Any(a => a.Field<string>("BWART") == "321" && string.IsNullOrEmpty(a.Field<string>("GRNUM")))) {
                    MessageBox.Show("321异动需提供GR单号信息！");
                    return;
                }
                // MVT 必须有值
                if(this.dtData.AsEnumerable().Any(a => string.IsNullOrEmpty(a.Field<string>("BWART")))) {
                    MessageBox.Show("MVT信息为空，请检查！");
                    return;
                }
                // 必须输入接收仓别
                if(this.dtData.AsEnumerable().Any(a => string.IsNullOrEmpty(a.Field<string>("UMLGO")))) {
                    MessageBox.Show("请提供接收仓别！");
                    return;
                }
                // 检测是否已处理
                PlantData plantData = new PlantData(this.UserData);
                DataTable dtProcessed = plantData.GetOutDataFromTWW(this.strWerks, this.strMblnr);
                if (!dtProcessed.AsEnumerable().Any(a => !string.IsNullOrEmpty(a.Field<string>("BWART")))) {
                    // 获取库存数据
                    StorageData storageData = new StorageData(UserData);
                    DataTable dtInStock = storageData.GetTwwStockData(this.strWerks, this.strLgort, dtProcessed.AsEnumerable().Select(s => s.Field<string>("LOCAT")).ToList());
                    //组合库存和发运订单数据,发运订单数据添加列INDAT 、INSPT、KDMAT、RMANO、TASKID
                    new List<string>() { "INDAT", "INSPT", "KDMAT", "RMANO", "TASKID" }.ForEach(f => { if (!dtData.Columns.Contains(f)) { dtData.Columns.Add(f, typeof(string)); } });
                    //通过MBLNR和ZEILE的对应关系，把库存的INDAT 、INSPT、KDMAT、RMANO、TASKID写入发运订单数据
                    dtInStock.AsEnumerable().ToList().ForEach(f => {
                        this.dtData.AsEnumerable().Where(w => w.Field<string>("LGORT").Trim() == f.Field<string>("LGORT").Trim() && w.Field<string>("LOCAT").Trim() == f.Field<string>("LOCAT").Trim() && w.Field<string>("MATNR").Trim() == f.Field<string>("MATNR")).ToList().ForEach(fe => {
                            fe.SetField("INDAT", f.Field<string>("INDAT"));
                            fe.SetField("INSPT", f.Field<string>("INSPT"));
                            fe.SetField("KDMAT", f.Field<string>("KDMAT"));
                            fe.SetField("RMANO", f.Field<string>("RMANO"));
                            fe.SetField("TASKID", f.Field<string>("TASKID"));
                        });
                    });
                    //检测是否库存与发运订单数据一致
                    if (this.dtData.AsEnumerable().Any(a => string.IsNullOrEmpty(a.Field<string>("INDAT")))) {
                        MessageBox.Show("库存与发运订单数据不一致，请检查！");
                        return;
                    }
                }
                //发运单号、发运单item、MVT（311/321/351）、50GR年份（默认当年）、50GR、46PO、料号、版本、数量、厂区（转出）、转出仓别、接收仓别、CostCenter。
                // 调用StorageOutData类
                StorageOut objStorageOut = new StorageOut(this.UserData,this.Progid);
                objStorageOut.AddTwwStorageOutSaveData(strWerks, strLgort, strMblnr, dtData);
                this.ButtonStatus("Finished");
                this.DataStatus("StandBy");
                MessageBox.Show("保存成功！");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message,ex.StackTrace);
            } finally {
                this.ButtonStatus("Finished");
                // 取消关闭限制
                this.FormClosing -= delegate (object sender1, FormClosingEventArgs e1) {
                    if(e1.CloseReason == CloseReason.UserClosing) {
                        e1.Cancel = true;
                    }
                };
            }
        }
        //txt key press
        private void txtMblnr_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == 13) {
                this.ButtonStatus("Processing");
                this.strMblnr = this.txtMblnr.Text.Trim();
                if (!string.IsNullOrEmpty(this.strMblnr)) {
                    this.btnConfirm_Click(null, null);
                }
            }
        }
        // edit button click
        private void btnEdit_Click(object sender, EventArgs e) {
            StorageOut_OnlineOut_TWW_Edit edit = new StorageOut_OnlineOut_TWW_Edit(this.UserData, this.Progid);
            edit.dtSource = this.dtData;
            edit.InitData();
            edit.ShowDialog();
            this.dtData = edit.dtSource;
            edit.Dispose();
            this.DataGridViewShows();
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e) {
            this.strWerks = this.cmbWerks.Text.Trim();
            if (string.IsNullOrEmpty(strWerks)) { return; }
            QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(this.UserData);
            dtStorage = objAuthority.CheckLgortAuthority(strWerks);
            // 循环写入仓别信息
            foreach (DataRow dr in this.dtStorage.Rows) {
                this.cmbLgort.Items.Add(dr["F_VALUE"].ToString());
            }
        }
        #endregion
        #region Data Functions
        private void InitData() {
            this.dtData = new DataTable();
            // 获取厂区、仓别信息
            //PlantData plantData = new PlantData(UserData);
            // 手动添加厂区、仓别列名
            this.dtPlant = new DataTable();
            this.dtStorage = new DataTable();
            this.dtPlant.Columns.Add("WERKS", typeof(string));
            this.dtStorage.Columns.Add("LGORT", typeof(string));
            // 手动添加厂区、仓别信息
            QCI.QWMS.Authority objAuthority = new QCI.QWMS.Authority(this.UserData);
            dtPlant = objAuthority.CheckPlantAuthority();
            dtStorage = objAuthority.CheckLgortAuthority(strWerks);
            // 循环写入厂区信息
            foreach (DataRow dr in this.dtPlant.Rows) {
                this.cmbWerks.Items.Add(dr["F_VALUE"].ToString());
            }
            // 循环写入仓别信息
            foreach (DataRow dr in this.dtStorage.Rows) {
                this.cmbLgort.Items.Add(dr["F_VALUE"].ToString());
            }
            // 初始化显示列名
            colMapping.Add("COMCD", "Company Code");
            colMapping.Add("MBLNR", "Doc. No.");
            colMapping.Add("ZEILE", "Item");
            colMapping.Add("INSMK", "INSMK");
            colMapping.Add("WERKS", "Plant");
            colMapping.Add("LGORT", "Sour. Storage");
            colMapping.Add("UMLGO", "Dest. Storage");
            colMapping.Add("BWART", "BWART");
            colMapping.Add("PONUM", "PO No.");
            colMapping.Add("GRNUM", "GR No.");
            colMapping.Add("MATNR", "Material No.");
            colMapping.Add("CHARG", "Version");
            colMapping.Add("MENGE", "Quantity");
            colMapping.Add("LOCAT", "Location");
            colMapping.Add("VEDAT", "Pre. DateCode");
            colMapping.Add("DACOD", "DataCode"); 
            colMapping.Add("LOCOD", "LOT Code");
            colMapping.Add("LIFNR", "Vendor");
            colMapping.Add("VBELN", "EC No.");
            colMapping.Add("VBILE", "EC Item");

            // 利用委托，传入blnClose，判断是否关闭窗体,false则不关闭,true则关闭
            this.FormClosing += delegate (object sender1, FormClosingEventArgs e1) {
                if (e1.CloseReason == CloseReason.UserClosing) {
                    e1.Cancel = !this.blnClose;
                }
                if (!this.blnClose) {
                    MessageBox.Show("正在保存，请勿关闭窗体！");
                }
            };


        }
        private void ButtonStatus(string type) {
            switch (type) {
                case "StandBy":
                    btnConfirm.Enabled = true;
                    btnSave.Enabled = false;
                    btnEdit.Enabled = false;
                    btnFresh.Enabled = true;
                    this.blnClose = true;
                    break;
                case "Processing":
                    btnFresh.Enabled = true;
                    btnConfirm.Enabled = false;
                    btnSave.Enabled = true;
                    btnEdit.Enabled = true;
                    this.blnClose = true;
                    break;
                case "Finished":
                    btnConfirm.Enabled = true;
                    btnSave.Enabled = false;
                    btnEdit.Enabled = true;
                    btnFresh.Enabled = true;
                    this.blnClose = true;
                    break;
                case "Sending":
                    btnConfirm.Enabled = false;
                    btnSave.Enabled = false;
                    btnEdit.Enabled = false;
                    btnFresh.Enabled = false;
                    this.blnClose = false;
                    break;
            }
        }
        private void DataStatus(string type) {
            switch (type) {
                case "StandBy":
                    txtMblnr.Enabled = true;
                    txtMblnr.Text = "";
                    cmbWerks.Enabled = true;
                    cmbLgort.Enabled = true;
                    this.dtData.Columns.Clear();
                    this.dtData.Rows.Clear();
                    this.dtData.Clear();
                    break;
                case "Processing":
                    txtMblnr.Enabled = false;
                    cmbWerks.Enabled = false;
                    cmbLgort.Enabled = false;
                    this.strLgort = cmbLgort.Text;
                    this.strMblnr = txtMblnr.Text;
                    this.strWerks = cmbWerks.Text;
                    break;
            }
            this.DataGridViewShows();
        }
        private void DataGridViewShows() {
            this.dgView.Columns.Clear();
            this.dgView.AutoGenerateColumns = false;
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            foreach (var keyValues in colMapping) {
                DataGridViewTextBoxColumn dgvCol = new DataGridViewTextBoxColumn();
                dgvCol.DataPropertyName = keyValues.Key;
                dgvCol.HeaderText = keyValues.Value;
                dgvCol.Width = 100;
                dgvCol.ReadOnly = true;
                this.dgView.Columns.Add(dgvCol);
            }
            this.dgView.DataSource = this.dtData;
        }
        #endregion

    }
}
