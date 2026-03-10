using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QWMS.Common;
using QCI.QWMS;

namespace QWMS
{
    public partial class Manage_StorageStatus : Form
    {
        #region 定义变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strUsrnm = "";
        private string strComcd = "";
        private string strProgid = "";
        private string strWerks = "";
        private string strLgort = "";
        private string strStlen = "";
        private string strStwid = "";
        private string strSttyp = "";
        private string strLotyp = "";
        private string strSourceType = "";
        private int intTotalRow;
        private int intTotalColumn;
        private DataTable dtData = new DataTable();
        private DataTable dtLgort = new DataTable();
        private DataTable dtSthgh = new DataTable();
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn;
        private StorageData objStorageData;
        private Point pointInCell00;

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

        public string Sttyp
        {
            get
            {
                return strSttyp;
            }
            set
            {
                strSttyp = value;
            }
        }

        public string Lotyp
        {
            get
            {
                return strLotyp;
            }
            set
            {
                strLotyp = value;
            }
        }

        public string SourceType
        {
            get
            {
                return strSourceType;
            }
            set
            {
                strSourceType = value;
            }
        }
        #endregion

        #region 构造函数
        public Manage_StorageStatus()
        {
            InitializeComponent();
        }

        public Manage_StorageStatus(UserInfo varUserData, string varProgid, string strType)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;
            SourceType = strType;
            try
            {

                objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Progid);

                if (strType.ToUpper() == "ADMIN")
                {
                    if (!objAdmin.CheckAuthority())
                    {
                        throw new Exception("You don't have right to use this program!!");
                    }
                }
                else if (strType.ToUpper() == "MANAGE")
                {
                    if (!objStorageIn.CheckAuthority("MANAGE"))
                    {
                        throw new Exception("You don't have right to use this program!!");
                    }
                }
                if (strType.ToUpper() == "ADMIN")
                {
                    this.Text = "Storage Layout Maintenance";
                }

                //Status
                ShowStatusData();
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
                this.cmbStset.Enabled = false;
                this.cmbSthgh.Enabled = false;
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
            this.stsMandt.Text = UserData.Client;
            this.stsUsrnm.Text = UserData.UserId;
            this.stsComcd.Text = UserData.CompanyCode;

        }
        # endregion

        private void ShowDdlWerks()
        {
            DataTable dtTemp = new DataTable();
            try
            {
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
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (cmbWerks.SelectedIndex != -1)
                {
                    strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
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
                    strLgort = "";
                }
                else
                {
                    cmbLgort.Items.Clear();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        cmbLgort.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
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

        private void ShowDdlStset(string strStset)
        {
            try
            {
                for (int i = 0; i < dtLgort.Rows.Count; i++)
                {
                    cmbStset.Items.Add(Convert.ToString(i + 1));
                    if (strStset != "" && Convert.ToString(i + 1) == strStset)
                    {
                        cmbStset.SelectedIndex = i;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlStset()");
            }
        }

        private void ShowDdlSthgh(string strSthgh)
        {
            try
            {
                cmbSthgh.Items.Clear();
                DataRow[] foundRow;
                DataColumn[] dcPrimaryKey = new DataColumn[2];
                dcPrimaryKey[0] = dtLgort.Columns["LGORT"];
                dcPrimaryKey[1] = dtLgort.Columns["STSET"];
                if (cmbStset.SelectedIndex != -1)
                {
                    foundRow = dtLgort.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "'");
                    if (foundRow.Length > 0)
                    {
                        for (int i = 0; i < Convert.ToInt32(foundRow[0]["STHGH"].ToString()); i++)
                        {
                            cmbSthgh.Items.Add(Convert.ToString(i + 1));
                            if (strSthgh != "" && Convert.ToString(i + 1) == strSthgh)
                            {
                                cmbSthgh.SelectedIndex = i;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlSthgh()");
            }
        }

        #region Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                dtLgort = objPlantData.GetPlantStorageData("LGORT", cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString());
                strSttyp = dtLgort.Rows[0]["CTRLC4"].ToString();
                strLotyp = dtLgort.Rows[0]["CTRLC5"].ToString();
                strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
                strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();
                cmbStset.Items.Clear();
                ShowDdlStset("");
                this.btnConfirm.Enabled = false;
                this.gbHeader.Enabled = false;
                this.cmbStset.Enabled = true;
                this.cmbSthgh.Enabled = true;
                this.btnEmpty.Enabled = true;
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                ShowDdlLgort();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void cmbStset_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                stsWarning.Text = "";
                DataRow[] foundRow;
                if (cmbSthgh.SelectedIndex == -1)
                {
                    ShowDdlSthgh("");
                }
                else
                {
                    ShowDdlSthgh(cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString());
                }

                if (cmbStset.SelectedIndex != -1 && cmbSthgh.SelectedIndex != -1)
                {
                    
                    objStorageData=new StorageData(CommonInfo.Instance.DBType,CommonInfo.Instance.DBCode,CommonInfo.Instance.ErrType,CommonInfo.Instance.ErrCode,UserData,Werks,Lgort);

                    //string test = "MANDT='" + Mandt + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "'";
                    dtData = objStorageData.QueryStorageStatus(cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString());
                    foundRow = dtLgort.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "'");
                    if (foundRow.Length > 0)
                    {
                        strStlen = foundRow[0]["STLEN"].ToString();
                        strStwid = foundRow[0]["STWID"].ToString();
                        //ShowDataGrid(foundRow[0]["STLEN"].ToString(), foundRow[0]["STWID"].ToString());
                        ShowDataGridView(foundRow[0]["STLEN"].ToString(), foundRow[0]["STWID"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void cmbSthgh_SelectedIndexChanged(object sender, EventArgs e)
        {
 
            try
            {
                stsWarning.Text = "";
                DataRow[] foundRow;
                if (cmbStset.SelectedIndex == -1 || cmbSthgh.SelectedIndex == -1)
                {
                    return;
                }

                if (cmbStset.SelectedIndex != -1 && cmbSthgh.SelectedIndex != -1)
                {
                    objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Werks, Lgort);

                    dtData = objStorageData.QueryStorageStatus(cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString());
                    //string test = "MANDT='" + Mandt + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "' and STHGH='" + cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString() + "'";
                    foundRow = dtLgort.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "'");
                    if (foundRow.Length > 0)
                    {
                        strStlen = foundRow[0]["STLEN"].ToString();
                        strStwid = foundRow[0]["STWID"].ToString();
                        ShowDataGridView(foundRow[0]["STLEN"].ToString(), foundRow[0]["STWID"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #region ShowDataGridView
        private void ShowDataGridView(string strStlen, string strStwid)
        {
            try
            {
                //dtgData.TableStyles.Clear();
                //DataGridTableStyle mydtgTableStyle = new DataGridTableStyle();
                ////mydtgTableStyle.MappingName = dtgData.TableName;

                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();
                

                DataTable dtTemp = new DataTable();
                DataTable dtRowCol = new DataTable();
                DataRow drRow;
                int intStlen = Convert.ToInt32(strStlen);
                int intStwid = Convert.ToInt32(strStwid);
                intTotalRow = intStwid;
                intTotalColumn = intStlen;
                //DataGridTextBoxColumn aColumnTextColumn;
                DataGridViewTextBoxColumn aColumnTextColumn;
                DataRow[] drFound1;
                dtRowCol = objPlantData.QueryRowColName(Werks, Lgort, cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString());

                // 设置DataGridView的列名
                for (int i = 1; i <= intStlen; i++)
                {
                    drFound1 = dtRowCol.Select("RCTYP='R' and RCNUM='" + i.ToString() + "'");
                    dtTemp.Columns.Add(new DataColumn(i.ToString(), typeof(string)));
                    aColumnTextColumn = new DataGridViewTextBoxColumn();
                    if (drFound1.Length > 0)
                    {
                        aColumnTextColumn.HeaderText = drFound1[0]["RCNAM"].ToString();
                    }
                    else
                    {
                        aColumnTextColumn.HeaderText = i.ToString();
                    }
 
                    //aColumnTextColumn.DataPropertyName = i.ToString();
                    if (aColumnTextColumn.HeaderText.Length > 2)
                    {
                        aColumnTextColumn.Width = 13 * aColumnTextColumn.HeaderText.Length;
                    }
                    else
                    {
                        aColumnTextColumn.Width = 25;
                    }
                    aColumnTextColumn.ReadOnly = true;
                    aColumnTextColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                     
                    this.gvData.Columns.Add(aColumnTextColumn);
                }
                // 处理dtData，使数据按一定格式显示
                for (int i = intStwid; i <= intStwid && i >= 1; i--)
                {
                    drRow = dtTemp.NewRow();
 
                    gvData.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
                    for (int j = 1; j <= intStlen; j++)
                    {
                        drRow[j.ToString()] = "";
                        DataRow[] foundRow;
                        //string test = "MANDT='" + Mandt + "' and WERKS='" + Werks + "' and LGORT='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "' and STHGH='" + cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString() + "' and STLEN='" + j.ToString() + "' and STWID='" + i.ToString() + "'";
                        foundRow = dtData.Select("MANDT='" + Mandt + "' and COMCD='" + Comcd + "' and WERKS='" + Werks + "' and LGORT='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "' and STHGH1='" + cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString() + "' and STLEN1='" + j.ToString() + "' and STWID1='" + i.ToString() + "'");
                        if (foundRow.Length > 0)
                        {
                            if (foundRow[0]["LOSTS"].ToString() != "0")
                            {
                                if (foundRow[0]["ISMRG"].ToString() == "N")
                                {
                                    drRow[j.ToString()] = "S";
                                }
                                else
                                {
                                    drRow[j.ToString()] = "M";
                                }
                            }
                            else
                            {
                                drRow[j.ToString()] = ".";
                            }
                        }
                        else
                        {
                            drRow[j.ToString()] = "";
                        }
                    }
                    dtTemp.Rows.Add(drRow);
                }

                //dtgData.TableStyles.Add(mydtgTableStyle);
 
                gvData.DataSource = dtTemp;
                pointInCell00 = new Point(gvData.GetCellDisplayRectangle(0,0,true).X + 4, gvData.GetCellDisplayRectangle(0,0,true).Y + 4);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }
        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.stsWarning.Text = "";
            this.gbHeader.Enabled = true;
            this.cmbSthgh.Enabled = false;
            this.cmbStset.Enabled = false;
            this.btnConfirm.Enabled = true;
            this.btnEmpty.Enabled = false;
            this.gvData.DataSource = null;
        }
        #endregion

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Empty
        private void btnEmpty_Click(object sender, EventArgs e)
        {
            try
            {
                Manage_EmptyLocation objManage_EmptyLocation = new Manage_EmptyLocation(UserData, Progid, cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString(), "NEW");
                objManage_EmptyLocation.ShowDialog();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion



        #region DataGridView Paint Event
        private void gvData_Paint(object sender, PaintEventArgs e)
        {
            // 绘制行号
            DataTable dtRowCol = new DataTable();
            DataRow[] drFound1;
            if (cmbStset.SelectedIndex >= 0 && cmbSthgh.SelectedIndex >= 0)
            {
                dtRowCol = objPlantData.QueryRowColName(Werks, Lgort, cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString());
            }
            try
            {

                if (dtData != null && dtData.Rows.Count >= 0 && this.gvData.DataSource != null)
                {
                    
                    //DataGrid.HitTestInfo hti = dtgData.HitTest(pointInCell00);
                    DataGridView.HitTestInfo hti = gvData.HitTest(pointInCell00.X, pointInCell00.Y);
                    int row = hti.RowIndex;
                    //int yDelta = dtgData.GetCellBounds(row, 0).Height + 1;
                    //int y = dtgData.GetCellBounds(row, 0).Top + 2;

                    int yDelta = gvData.GetCellDisplayRectangle(row, 0, true).Height + 1;
                    int y = gvData.GetCellDisplayRectangle(row, 0, true).Top + 2;
                    
                    //CurrencyManager cm = (CurrencyManager)this.BindingContext[dtgData.DataSource, dtgData.DataMember];
                    CurrencyManager cm = (CurrencyManager)this.BindingContext[gvData.DataSource, gvData.DataMember];
                    ((DataView)cm.List).AllowNew = false;
                    while (y < this.gvData.Height - yDelta && row < cm.Count)
                    {
                        string text = string.Format("{0}", cm.Count - row);
                        drFound1 = dtRowCol.Select("RCTYP='C' and RCNUM='" + text + "'");
                        if (drFound1.Length > 0)
                        {
                            text = drFound1[0]["RCNAM"].ToString();
                        }
                        if (text.Length == 1)
                        {
                            e.Graphics.DrawString(text, this.gvData.Font, new SolidBrush(Color.Black), 12, y);
                        }
                        else if (text.Length == 2)
                        {
                            e.Graphics.DrawString(text, this.gvData.Font, new SolidBrush(Color.Black), 9, y);
                        }
                        else
                        {
                            e.Graphics.DrawString(text, this.gvData.Font, new SolidBrush(Color.Black), 6, y);
                        }
                        y += yDelta;
                        row++;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region DataGridView Mouse Event
        private void gvData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                int intStwid;
                int intStlen;
                DataRow[] foundRow;
                stsWarning.Text = "";
                //DataGrid dgClick = (DataGrid)sender;
                //DataGrid.HitTestInfo hitRow;
                DataGridView dgvClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgvClick.HitTest(e.X, e.Y);
                 
                if (hitRow.Type == DataGridViewHitTestType.Cell && hitRow.RowIndex < Convert.ToInt32(strStwid))
                {
                    intStwid = Convert.ToInt32(strStwid) - hitRow.RowIndex;
                    intStlen = hitRow.ColumnIndex + 1;
                     
                    Manage_StorageStatus_Detail objManage_StorageStatus_Detail = new Manage_StorageStatus_Detail(UserData, Progid, Werks, Lgort, Sttyp, Lotyp, cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString(), intStlen.ToString(), intStwid.ToString(), SourceType);
                    objManage_StorageStatus_Detail.ShowDialog();
                    if (objManage_StorageStatus_Detail.Type == "RELOAD")
                    {
                        objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Werks, Lgort);

                        dtData = objStorageData.QueryStorageStatus(cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString());
                        foundRow = dtLgort.Select("MANDT='" + Mandt + "' and Comcd='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "'");
                        if (foundRow.Length > 0)
                        {
                            ShowDataGridView(foundRow[0]["STLEN"].ToString(), foundRow[0]["STWID"].ToString());
                        }
                    }
                }
                else if (hitRow.Type == DataGridViewHitTestType.ColumnHeader && hitRow.ColumnIndex < Convert.ToInt32(strStlen))
                {
                     
                    Manage_StorageStatus_SetupRowCloumn objManage_StorageStatus_SetupRowCloumn = new Manage_StorageStatus_SetupRowCloumn(UserData, Werks, Lgort, cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString(), "R", hitRow.ColumnIndex + 1);
                    objManage_StorageStatus_SetupRowCloumn.ShowDialog();
                    objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Werks, Lgort);
                    dtData = objStorageData.QueryStorageStatus(cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString());
                    foundRow = dtLgort.Select("MANDT='" + Mandt + "'  and Comcd='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "'");
                    if (foundRow.Length > 0)
                    {
                        ShowDataGridView(foundRow[0]["STLEN"].ToString(), foundRow[0]["STWID"].ToString());
                    }
                }
                else if (hitRow.Type == DataGridViewHitTestType.RowHeader && hitRow.RowIndex < Convert.ToInt32(strStwid))
                {
                    Manage_StorageStatus_SetupRowCloumn objManage_StorageStatus_SetupRowCloumn = new Manage_StorageStatus_SetupRowCloumn(UserData, Werks, Lgort, cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString(), "C", intTotalRow - hitRow.RowIndex);
                    objManage_StorageStatus_SetupRowCloumn.ShowDialog();
                    objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Werks, Lgort);
                    dtData = objStorageData.QueryStorageStatus(cmbStset.Items[cmbStset.SelectedIndex].ToString(), cmbSthgh.Items[cmbSthgh.SelectedIndex].ToString());
                    foundRow = dtLgort.Select("MANDT='" + Mandt + "'  and Comcd='" + Comcd + "' and CTRLNM='" + Werks + "' and CTRLC2='" + Lgort + "' and STSET='" + cmbStset.Items[cmbStset.SelectedIndex].ToString() + "'");
                    if (foundRow.Length > 0)
                    {
                        ShowDataGridView(foundRow[0]["STLEN"].ToString(), foundRow[0]["STWID"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No Data");
                    return;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }
        #endregion

        #region 调整布局大小
        private void Manage_StorageStatus_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion



    }
}
