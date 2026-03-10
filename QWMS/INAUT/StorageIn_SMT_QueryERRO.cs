using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class StorageIn_SMT_QueryERRO : Form
    {
        #region  初始化
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strLgort = string.Empty;
        private string strDate = "";
        private ArrayList alRefids = new ArrayList();
        private DataTable dtData = new DataTable();

        #endregion
        #region  Get/Set
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
        public DataTable dtRefids
        {
            get;
            set;
        }

        #endregion

        public StorageIn_SMT_QueryERRO(UserInfo varUserData, string varWerks, string varLgort)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            strWerks = varWerks;
            strLgort = varLgort;
            strDate = DateTime.Now.ToString("yyyy-MM-dd");
            btnSelect.Enabled = false;

            //ShowDataGrid();

        }
        

        #region 加一个checkbox控件跟datagridview组合来实现全选反选功能
        #region
        private void chkcell_OnCheckBoxClicked(bool isChecked)
        {
            if (isChecked == true)
            {
                dgvData.EndEdit();
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = true;
                    //dgvRequests.Rows[i].Cells[0].Value = 1;
                }
            }
            else
            {
                dgvData.EndEdit();
                for (int i = 0; i < dgvData.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvData.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                    chk.Value = false;
                    //dgvRequests.Rows[i].Cells[0].Value = 0;
                }
            }
        }
        #endregion
        #region 重绘全选表头
        //重绘表头
        public class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
        {
            Point checkBoxLocation;
            Size checkBoxSize;
            bool _checked = false;
            Point _cellLocation = new Point();
            System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
            public event CheckBoxClickedHandler OnCheckBoxClicked;

            public DatagridViewCheckBoxHeaderCell()
            {
            }

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
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    dataGridViewElementState, value,
                    formattedValue, errorText, cellStyle,
                    advancedBorderStyle, paintParts);
                Point p = new Point();
                Size s = CheckBoxRenderer.GetGlyphSize(graphics,
                System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                p.X = cellBounds.Location.X +
                    (cellBounds.Width / 2) - (s.Width / 2);
                p.Y = cellBounds.Location.Y +
                    (cellBounds.Height / 2) - (s.Height / 2);
                _cellLocation = cellBounds.Location;
                checkBoxLocation = p;
                checkBoxSize = s;
                if (_checked)
                    _cbState = System.Windows.Forms.VisualStyles.
                        CheckBoxState.CheckedNormal;
                else
                    _cbState = System.Windows.Forms.VisualStyles.
                        CheckBoxState.UncheckedNormal;
                CheckBoxRenderer.DrawCheckBox
                (graphics, checkBoxLocation, _cbState);
            }


            protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
            {
                Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
                if (p.X >= checkBoxLocation.X && p.X <=
                    checkBoxLocation.X + checkBoxSize.Width
                && p.Y >= checkBoxLocation.Y && p.Y <=
                    checkBoxLocation.Y + checkBoxSize.Height)
                {
                    _checked = !_checked;
                    if (OnCheckBoxClicked != null)
                    {
                        OnCheckBoxClicked(_checked);
                        this.DataGridView.InvalidateCell(this);
                    }

                }
                base.OnMouseClick(e);
            }

        }

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
                get { return isChecked; }
                set { isChecked = value; }
            }
        }

        #endregion
        #endregion


        #region ShowDataGrid
        public void ShowDataGrid()
        {
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.Columns.Clear();
            try
            {
                DatagridViewCheckBoxHeaderCell chkcell = new DatagridViewCheckBoxHeaderCell();
                chkcell.OnCheckBoxClicked += new CheckBoxClickedHandler(chkcell_OnCheckBoxClicked);
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderCell = chkcell;
                chk.DataPropertyName = "Select";
                chk.HeaderText = "";//Multilanguage.Instance.ResourceManager.GetString("TransactionID");
                chk.Name = "chk";
                chk.Frozen = true;
                chk.Width = 40;
                this.dgvData.Columns.Add(chk);
                this.dgvData.MultiSelect = false;
                this.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                DataGridViewTextBoxColumn dgvcRefid = new DataGridViewTextBoxColumn();
                dgvcRefid.DataPropertyName = "REFID";
                dgvcRefid.HeaderText = "Referance ID.";
                dgvcRefid.Name = "REFID";
                dgvcRefid.ReadOnly = true;
                dgvcRefid.Width = 120;
                dgvData.Columns.Add(dgvcRefid);

                dgvData.DataSource = dtData;
                dgvData.FirstDisplayedScrollingRowIndex = dgvData.Rows.Count - 1;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGrid()");
            }
        }   
        #endregion

        #region 点击query
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
                strDate = DateTime.Parse(dtpondat.Text).ToString("d");
                strDate = strDate.Replace("/", "-");
                dtData = objStorageData.QueryWHRIDWERRO(Werks, Lgort, strDate);
                if (dtData.Rows.Count == 0)
                {
                    MessageBox.Show("该日期下无SAP扣账失败的REFID资料！");
                    return;
                }
                //DataColumn cSelect = new DataColumn("SELECT", typeof(bool));
                if (!dtData.Columns.Contains("SELECT"))
                    dtData.Columns.Add("SELECT", typeof(bool));
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dtData.Rows[i]["SELECT"] = false;
                }
                ShowDataGrid();
                btnSelect.Enabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-btnQuery_Click()");
            }
        }
        #endregion

        #region 点击select
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                QCI.QWMS.StorageData objStorageData = new QCI.QWMS.StorageData(UserData);
                alRefids.Clear();
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtData.Rows[i]["SELECT"]))
                    {
                        alRefids.Add(dtData.Rows[i]["REFID"]);
                    }
                }
                if (alRefids.Count == 0)
                {
                    MessageBox.Show("请选择REFID!");
                    return;
                }
                if (alRefids.Count > 1)
                {
                    MessageBox.Show("一次只能处理一个REFID!");
                    return;
                }

                dtRefids = objStorageData.QueryWHRIDITM(Werks, Lgort, alRefids);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }

}
