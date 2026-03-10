using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using QCI.QWMS;
using QWMS.Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;

namespace QWMS
{
    public partial class Manage_StorageBatchDown : Form
    {
        #region 初始设定
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strWerks = "";
        private DataTable dtLgortData = new DataTable();
        private DataTable dtData = new DataTable();
        private StorageIn objStorageIn;
        private Authority objAuthority;
        private StorageData objStorageData;
        #endregion

        #region 变量
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
        public DataTable Data
        {
            get
            {
                return dtData;
            }
            set
            {
                dtData = value;
            }
        }
        #endregion

        public Manage_StorageBatchDown(UserInfo varUserData, string strWerks, string strProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Werks = strWerks;
            Progid = strProgid;
            try
            {
                objStorageIn = new StorageIn(UserData, Progid);
                objAuthority = new Authority(UserData);
                //檢查權限
                if (!objStorageIn.CheckAuthority("MANAGE"))
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else
                {
                    QueryLgortData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void QueryLgortData(){
            try
            {
                dtLgortData = objAuthority.CheckLgortWithAuthSelect(Werks);
                if (dtLgortData.Rows.Count == 0)
                {
                    MessageBox.Show("No Data!!");
                }
                else
                {
                    ShowDataGrid();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ShowDataGrid()
        {
            try
            {
                this.dgvData.AutoGenerateColumns = false;
                this.dgvData.Columns.Clear();

                //DataGridViewCheckBoxColumn dgvSelect = new DataGridViewCheckBoxColumn();
                //dgvSelect.DataPropertyName = "Select";
                //dgvSelect.HeaderText = "Select";
                //dgvSelect.Name = "Select";
                //dgvSelect.Width = 40;
                //this.dgvData.Columns.Add(dgvSelect);

                DatagridViewCheckBoxHeaderCell chkcell = new DatagridViewCheckBoxHeaderCell();
                chkcell.OnCheckBoxClicked += new CheckBoxClickedHandler(chkcell_OnCheckBoxClicked);
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderCell = chkcell;
                chk.DataPropertyName = "Select";
                chk.HeaderText = "";
                chk.Width = 30;
                this.dgvData.Columns.Add(chk);
                this.dgvData.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

                DataGridViewTextBoxColumn dgvcLgort = new DataGridViewTextBoxColumn();
                dgvcLgort.DataPropertyName = "LGORT";
                dgvcLgort.HeaderText = "Lgort";
                dgvcLgort.Name = "LGORT";
                dgvcLgort.ReadOnly = true;
                dgvcLgort.Width = 60;
                this.dgvData.Columns.Add(dgvcLgort);

                dgvData.DataSource = dtLgortData;
                this.lblCount.Text = dtLgortData.Rows.Count.ToString() + " records";
                dgvData.ClearSelection();
                dgvData.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            dtData.Columns.Clear();
            dtData.Columns.Add("LGORT");
            for (int i = 0; i < dgvData.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvData.Rows[i].Cells[0].Value))
                {
                    DataRow dr = dtData.NewRow();
                    dr["LGORT"] = dgvData.Rows[i].Cells["LGORT"].Value.ToString();
                    dtData.Rows.Add(dr);
                }
            }
            Data = dtData;

            this.Close();
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
                dgvData.EndEdit();
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = true;
                }
            }
            else
            {
                dgvData.EndEdit();
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = false;
                }
            }
        }
        #endregion
    }
}
