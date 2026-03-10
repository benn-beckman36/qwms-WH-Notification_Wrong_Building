using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using QWMS.Common;
using QCI.QWMS;
using System.IO;
using System.Diagnostics;
using NPOI;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.SS.UserModel;
using NPOI.Util;

namespace QWMS
{
    public partial class Manage_LocationPallet : Form
    {
        #region Initial

        #region 定義變數

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
        private string strWerks_M = "";
        private string strLgort_M = "";
        private string strWerks_Q = "";
        private string strLgort_Q = "";
        private int intTotalRow;
        private int intTotalColumn;
        private System.Data.DataTable dtData = new System.Data.DataTable();
        private System.Data.DataTable dtData2 = new System.Data.DataTable();
        private System.Data.DataTable dtData3 = new System.Data.DataTable();
        private System.Data.DataTable dtData4 = new System.Data.DataTable();
        private System.Data.DataTable dtLgort = new System.Data.DataTable();
        private System.Data.DataTable dtSthgh = new System.Data.DataTable();
        private System.Data.DataTable dtTemp = new System.Data.DataTable();
        private Admin objAdmin;
        private PlantData objPlantData;
        private Authority objAuthority;
        private StorageIn objStorageIn_1, objStorageIn_2, objStorageIn_3, objStorageIn_4;
        private StorageData objStorageData;
        private Point pointInCell00;
        bool flagCheck;
        bool _CheckChange = false;
        bool flag_Query = false;//儲位使用率查詢權限
        bool flag_Maintain_Total = false;//維護總棧板數權限
        bool flag_Maintain_Used = false;//維護有無庫存棧板數權限
        bool flag_Maintain_Upload = false;//批次維護庫存棧板數權限
        bool flag_refersh1 = false;
        bool flag_refersh2 = false;
        bool flag_numberic = true;
        bool flag_PGB1 = false;
        bool flag_PGB2 = false;
        ArrayList arr_CHANGE = new ArrayList();//紀錄user目前輸入錯誤的CHANGE
        ArrayList a1 = new ArrayList();//紀錄輸入錯誤儲位的詳細資料(儲位名稱,CHANGE)
        ArrayList M_Row = new ArrayList();//紀錄維護資料個的列名稱
        ArrayList arySQL = new ArrayList();//批次上傳紀錄的SQL
        int redcell_Total = -1;//紀錄目前被標紅色cell(Total)
        int redcell_Used = -1;//紀錄目前被標紅色cell(Used)
        int ScrollPosition = 0;

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
        
        #region Construstor

        public Manage_LocationPallet()
        {
            InitializeComponent();
        }

        public Manage_LocationPallet(UserInfo varUserData, string varProgid, string strType)
        {
            InitializeComponent();
            //this.N1.Height = this.gvData.Height;

            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;
            SourceType = strType;
            try
            {

                //objAdmin = new Admin(UserData, Progid);
                objPlantData = new PlantData(UserData);
                objAuthority = new Authority(UserData);
                objStorageIn_1 = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, "FG_1");
                objStorageIn_2 = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, "FG_2");
                objStorageIn_3 = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, "FG_3");
                objStorageIn_4 = new StorageIn(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, "FG_4");

                #region 抓出目前使用者的權限(4種權限 : Query,Maintain_Total,Maintain_Used,Maintain_Upload)

                if (objStorageIn_1.CheckAuthority("MANAGE"))   
                    flag_Maintain_Total = true;
                else
                    flag_Maintain_Total = false;

                if (objStorageIn_2.CheckAuthority("MANAGE"))
                    flag_Maintain_Used = true;
                else
                    flag_Maintain_Used = false;

                if (objStorageIn_3.CheckAuthority("MANAGE"))
                    flag_Maintain_Upload = true;
                else
                    flag_Maintain_Upload = false;

                if (objStorageIn_4.CheckAuthority("MANAGE"))
                    flag_Query = true;
                else
                    flag_Query = false;              
                                
                #endregion

                #region 根據使用者權限,決定動作

                if (!(flag_Query || flag_Maintain_Total || flag_Maintain_Used || flag_Maintain_Upload))//沒有權限
                    throw new Exception("You don't have right to use this program!!");

                //顯示狀態列
                ShowStatusData();

                if (flag_Maintain_Total && flag_Maintain_Used && flag_Query && flag_Maintain_Upload)//擁有全部權限
                {
                    #region 初始化所有ComboBox

                    ShowDdlWerks(cmbWerks);
                    ShowDdlWerks(cmbWerks2);
                    cmbLgort = AddItem(cmbLgort);
                    cmbLgort2 = AddItem(cmbLgort2);
                                            
                    if (cmbWerks.Items.Count > 0)
                    {
                        this.cmbWerks.SelectedIndex = 0;
                    }
                    if (cmbLgort.Items.Count > 0)
                    {
                        this.cmbLgort.SelectedIndex = 0;
                    }

                    #endregion

                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    this.Text = "Location Usage Rate Query and Pallet Information Maintenance";

                    if (flag_Maintain_Total || flag_Maintain_Used || flag_Maintain_Upload)//至少擁有一個Maintain權限
                    {
                        if (flag_Maintain_Total && flag_Maintain_Used && flag_Maintain_Upload)//擁有全部Maintain權限
                        {
                            #region 初始化所有ComboBox

                            ShowDdlWerks(cmbWerks);
                            cmbLgort = AddItem(cmbLgort);

                            if (cmbWerks.Items.Count > 0)
                            {
                                this.cmbWerks.SelectedIndex = 0;
                            }
                            if (cmbLgort.Items.Count > 0)
                            {
                                this.cmbLgort.SelectedIndex = 0;
                            }

                            #endregion

                            if (flag_Maintain_Total)//Total
                                chk1.Enabled = true;
                            else
                                chk1.Enabled = false;

                            if (flag_Maintain_Used)//Used
                                chk2.Enabled = true;
                            else
                                chk2.Enabled = false;

                            if (flag_Maintain_Upload)//Upload
                                chk6.Enabled = true;
                            else
                                chk6.Enabled = false;

                            tabControl1.SelectedIndex = 0;
                        }
                        else
                        {
                            if (flag_Query)//至少擁有一個Maintain權限+Query權限
                            {
                                #region 初始化所有ComboBox

                                ShowDdlWerks(cmbWerks);
                                ShowDdlWerks(cmbWerks2);
                                cmbLgort = AddItem(cmbLgort);
                                cmbLgort2 = AddItem(cmbLgort2);

                                if (cmbWerks.Items.Count > 0)
                                {
                                    this.cmbWerks.SelectedIndex = 0;
                                }
                                if (cmbLgort.Items.Count > 0)
                                {
                                    this.cmbLgort.SelectedIndex = 0;
                                }

                                #endregion

                                if (flag_Maintain_Total)//Total
                                    chk1.Enabled = true;
                                else
                                    chk1.Enabled = false;

                                if (flag_Maintain_Used)//Used
                                    chk2.Enabled = true;
                                else
                                    chk2.Enabled = false;

                                if (flag_Maintain_Upload)//Upload
                                    chk6.Enabled = true;
                                else
                                    chk6.Enabled = false;

                                tabControl1.SelectedIndex = 0;
                            }
                            else//只有Maintain權限
                            {
                                #region 初始化Maintain ComboBox

                                ShowDdlWerks(cmbWerks);
                                cmbLgort = AddItem(cmbLgort);

                                if (cmbWerks.Items.Count > 0)
                                {
                                    this.cmbWerks.SelectedIndex = 0;
                                }
                                if (cmbLgort.Items.Count > 0)
                                {
                                    this.cmbLgort.SelectedIndex = 0;
                                }

                                #endregion

                                tabControl1.SelectedIndex = 0;

                                if (flag_Maintain_Total)//Total
                                    chk1.Enabled = true;
                                else
                                    chk1.Enabled = false;

                                if (flag_Maintain_Used)//Used
                                    chk2.Enabled = true;
                                else
                                    chk2.Enabled = false;

                                if (flag_Maintain_Upload)//Upload
                                    chk6.Enabled = true;
                                else
                                    chk6.Enabled = false;
                            }
                        }
                    }                        
                    else//只有Query權限
                    {
                        #region 初始化Query ComboBox

                        ShowDdlWerks(cmbWerks2);
                        cmbLgort2 = AddItem(cmbLgort2);
                        
                        #endregion

                        #region 關閉所有checkbox

                        chk1.Enabled = false;
                        chk2.Enabled = false;
                        chk6.Enabled = false;

                        #endregion

                        tabControl1.SelectedIndex = 1;
                    }
                }

	            #endregion

                Initial_Control();//控制項初始化
                               
                //this.cmbStset.Enabled = false;
                //this.cmbSthgh.Enabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //控制項初始化
        private void Initial_Control()
        {
            #region 控制項控制

            #region 過濾條件

            this.chk3.Checked = false;
            this.chk4.Checked = false;
            this.chk5.Checked = false;
            this.txtLocat.Text = null;
            this.N2.Value = 0;
            this.N3.Value = 0;
            //this.chk3.ForeColor = System.Drawing.SystemColors.ControlDark;
            //this.chk4.ForeColor = System.Drawing.SystemColors.ControlDark;
            //this.chk5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.chk3.Enabled = false;
            this.chk4.Enabled = false;
            this.chk5.Enabled = false;
            this.txtLocat.Enabled = false;
            this.N2.Enabled = false;
            this.N3.Enabled = false;
            this.cmbOption1.SelectedIndex = 0;
            this.cmbOption2.SelectedIndex = 0;
            this.cmbOption1.Enabled = false;
            this.cmbOption2.Enabled = false;
            this.label20.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbHeader3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.gbHeader4.ForeColor = System.Drawing.SystemColors.ControlDark;
            
            #endregion

            this.gbHeader.Enabled = true;
            this.gbHeader2.Enabled = true;           
            this.btnConfirm.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnImport.Enabled = false;
            this.btnRefresh.Enabled = true;
            this.btnExit.Enabled = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel2.Enabled = false;
            this.linkLabel3.Enabled = false;
            this.txtUpload.Enabled = false;
            this.btnUpload.Enabled = false;
            N1.Visible = false;
            //pictureBox1.ImageLocation = @"Images\arrows1.gif";
            //pictureBox1.Visible = false;
            //pictureBox2.ImageLocation = @"Images\arrows2.gif";
            pictureBox2.Visible = false;
            label11.Visible = false;

            gvData.Columns.Clear();
            gvData2.Columns.Clear();

            this.stsWarning.Text = "";

            if (flag_Maintain_Total || flag_Maintain_Used || flag_Maintain_Upload)
            {
                cmbWerks.SelectedIndex = 0;
                cmbLgort.SelectedIndex = 0;
                chk1.Checked = false;
                chk2.Checked = false;
                chk6.Checked = false;

                if(flag_Maintain_Total)
                    chk1.Enabled = true;

                if (flag_Maintain_Used)
                    chk2.Enabled = true;

                if (flag_Maintain_Upload)
                    chk6.Enabled = true;
            }

            if (flag_Query)
            {
                cmbWerks2.SelectedIndex = 0;
                cmbLgort2.SelectedIndex = 0;
            }
            
            #endregion
        }

        #endregion
        
        #endregion

        #region FormLoad

        private void Manage_LocationPallet_Load(object sender, EventArgs e)
        {
                AsyncTemplate.OnInvokeStarting =
                () =>
                {
                    if (flag_PGB1)
                        Start_Progress();
                    else if (flag_PGB2)
                        Start_Progress2();
                };

                AsyncTemplate.OnInvokeEnding =
                    () =>
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(
                                ()
                                =>
                                {
                                    if (flag_PGB1)
                                    {
                                        Stop_Progress();
                                        flag_PGB1 = false;
                                    }
                                    else if (flag_PGB2)
                                    {
                                        Stop_Progress2();
                                        flag_PGB2 = false;
                                    }
                                    
                                })
                            );
                        }
                        else
                        {
                            if (flag_PGB1)
                            {
                                Stop_Progress();
                                flag_PGB1 = false;
                            }
                            else if (flag_PGB2)
                            {
                                Stop_Progress2();
                                flag_PGB2 = false;
                            }
                        }
                    };
        }

        #endregion

        #region Button

        #region Query

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                #region 檢查User要使用的功能(Maintain,Query),並做出對應的動作

                string message = "";
                bool flag1 = false;
                bool flag2 = false;
                bool flag_gridview1 = true;
                bool flag_gridview2 = true;
                arr_CHANGE.Clear();

                if (flag_Maintain_Total || flag_Maintain_Used)
                {
                    #region Maintain

                    if (DdlCheck(cmbWerks) || DdlCheck(cmbLgort) || ChkCheck(chk1) || ChkCheck(chk2))//其中之一有選擇,視為要維護
                    {
                        flag1 = true;
                        if (DdlCheck(cmbWerks) && DdlCheck(cmbLgort))//combobox皆有值
                        {
                            if (ChkCheck(chk1) || ChkCheck(chk2))//checkbox其中之一有值
                            {
                                flag_gridview1= true;
                            }
                            else
                            {
                                message = message + "Please Choose Total or USED/UNUSE in Maintain Interface!" + "\n";
                                flag_gridview1 = false;
                            }

                        }
                        else
                        {
                            message = message + "Please Choose Plant and Storage in Maintain Interface!" + "\n";
                            flag_gridview1 = false;
                        }
                    }

                    #endregion
                }


                if (flag_Query)
                {
                    #region Query

                    if (DdlCheck(cmbWerks2) || DdlCheck(cmbLgort2))//其中之一有選擇,視為要查詢
                    {
                        flag2 = true;
                        if (DdlCheck(cmbWerks2))
                        {
                            flag_gridview2 = true;
                        }
                        else
                        {
                            message = message + "Please Choose Plant in Query Interface!" + "\n";
                            flag_gridview2 = false;
                        }                           
                    }

                    #endregion
                }                 

                //什麼都沒有選擇
                if (!(flag1 || flag2))
                    message = message + "Please Choose Any Option First! ";
                else
                {
                    if (flag_gridview1 && flag_gridview2)
                    {
                        #region Maintain

                        if (flag1)
                        {
                            //存入Maintain頁面所選擇的廠區,倉別
                            strWerks_M = cmbWerks.SelectedItem.ToString();
                            strLgort_M = cmbLgort.SelectedItem.ToString();

                            //帶出維護datagridview
                            MaintainGrid();
                            btnSave.Enabled = true;
                        }
                     
                        #endregion

                        #region Query

                        if (flag2)
                        {
                            if (DdlCheck(cmbLgort2))//以廠區倉別為查詢條件
                            {
                                //存入Query頁面所選擇的廠區,倉別
                                strWerks_Q = cmbWerks2.SelectedItem.ToString();
                                strLgort_Q = cmbLgort2.SelectedItem.ToString();

                                //帶出查詢datagridview
                                QueryGrid(1);

                            }
                            else//只以廠區為查詢條件
                            {
                                //存入Query頁面所選擇的廠區
                                strWerks_Q = cmbWerks2.SelectedText.ToString();

                                if (!cmbWerks2.SelectedItem.ToString().Equals("--ALL--"))
                                    QueryGrid(2);
                                else
                                    QueryGrid(3);//ALL選項
                            }
                            //this.linkLabel1.Enabled = true;
                        }

                        #endregion

                        Query_Control();
                    }
                }


                if (!message.Equals(""))
                    MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                #endregion

            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        //查詢成功後,控制項做的動作
        private void Query_Control()
        {
            #region 控制項控制

            this.gbHeader.Enabled = false;
            this.gbHeader2.Enabled = false;
            chk1.Enabled = false;
            chk2.Enabled = false;
            chk6.Enabled = false;
            this.btnConfirm.Enabled = false;           
            this.btnRefresh.Enabled = true;
            this.btnExit.Enabled = true;

            //Query提示label
            if (!cmbWerks2.SelectedItem.ToString().Equals("--ALL--") && gvData2.Rows.Count != 0)//不為全部廠區
                label11.Visible = true;
            
            #region 過濾條件

            if (gvData.RowCount != 0)//Maintain介面有資料
            {
                this.gbHeader3.ForeColor = System.Drawing.SystemColors.ControlText;
                this.chk3.Enabled = true;
                this.chk4.Enabled = true;

                AutoCompleteStringCollection acc = new AutoCompleteStringCollection();
                //加入Location自動完成選單
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    acc.Add(HeaderSplit(gvData.Rows[i].HeaderCell.Value.ToString()));
                }

                txtLocat.AutoCompleteCustomSource = acc;
            }

            if (gvData2.RowCount != 0)//Query介面有資料
            {
                this.gbHeader4.ForeColor = System.Drawing.SystemColors.ControlText;
                this.chk5.Enabled = true;
            }

            #endregion

            #endregion
        }

        #endregion

        #region Save

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gvData.Rows.Count != 0)
            {
                #region 重新抓取WHPAL join WHHED的資料

                objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks_M, strLgort_M);
                dtData3 = objStorageData.QueryStorageStatus2();

                #endregion

                #region 將帶出的資料(TOTAL,USED,UNUSE)與目前gvdata中的資料(CHANGE)做整合,並產生新的datatable

                //特別注意 : 若在按下Query鈕到按下Save鈕中這段時間,
                //有人做了儲位的刪除或改名,則被刪除或改名的儲位的CHANGE若有改動,
                //將無法有作用(因為該儲位已經不存在)
                //所以要先檢查要修改的儲位在不在

                int TOTAL, USED, UNUSE, CHANGE;
                TOTAL = USED = UNUSE = CHANGE = 0;
                string LOCATION;
                string message = "";
                bool flag_rule = true;
                bool flag_first_total = false;

                arr_CHANGE.Clear();
                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    if (this.gvData.Rows[i].Cells[3].Value != null && !Convert.IsDBNull(this.gvData.Rows[i].Cells[3].Value))
                    {
                        if (!this.gvData.Rows[i].Cells[3].Value.ToString().Equals(""))
                        {
                            //檢查要修改的儲位在不在
                            if (LocatCheck(HeaderSplit(this.gvData.Rows[i].HeaderCell.Value.ToString())))
                            {
                                #region 格式檢查

                                #region 取值

                                flag_first_total = false;

                                if (gvData.Rows[i].Cells[0].Value != null && !Convert.IsDBNull(gvData.Rows[i].Cells[0].Value))
                                {
                                    if (!gvData.Rows[i].Cells[0].Value.ToString().Equals(""))
                                        TOTAL = Convert.ToInt32(gvData.Rows[i].Cells[0].Value);
                                    else
                                    {
                                        flag_first_total = true;
                                        TOTAL = 0;
                                    }
                                }
                                else
                                {
                                    flag_first_total = true;
                                    TOTAL = 0;
                                }


                                if (gvData.Rows[i].Cells[1].Value != null && !Convert.IsDBNull(gvData.Rows[i].Cells[1].Value))
                                {
                                    if (!gvData.Rows[i].Cells[1].Value.ToString().Equals(""))
                                        USED = Convert.ToInt32(gvData.Rows[i].Cells[1].Value);
                                    else
                                        USED = 0;
                                }
                                else
                                    USED = 0;

                                if (gvData.Rows[i].Cells[2].Value != null && !Convert.IsDBNull(gvData.Rows[i].Cells[2].Value))
                                {
                                    if (!gvData.Rows[i].Cells[2].Value.ToString().Equals(""))
                                        UNUSE = Convert.ToInt32(gvData.Rows[i].Cells[2].Value);
                                    else
                                        UNUSE = 0;
                                }

                                else
                                    UNUSE = 0;

                                if (gvData.Rows[i].Cells[3].Value != null && !Convert.IsDBNull(gvData.Rows[i].Cells[3].Value))
                                {
                                    if (!gvData.Rows[i].Cells[3].Value.ToString().Equals(""))
                                        CHANGE = Convert.ToInt32(gvData.Rows[i].Cells[3].Value);
                                    else
                                        CHANGE = 0;
                                }
                                else
                                    CHANGE = 0;

                                LOCATION = this.HeaderSplit(gvData.Rows[i].HeaderCell.Value.ToString());

                                #endregion

                                #region 檢查

                                flag_rule = true;
                                if (chk1.Checked)//Total棧板數維護
                                {
                                    #region 規則檢查
                                    //CHANGE>0-->NO RULE
                                    //CHANGE<0-->CHANGE扣除數量不能>UNUSE數量
                                    if (CHANGE < 0)
                                    {
                                        if (flag_first_total)//第一次做
                                        {
                                            flag_rule = false;
                                            message = message + "Location '" + this.gvData.Rows[i].HeaderCell.Value.ToString() + "' CHANGE<0!" + "\n";
                                            Rec_Locat(i);
                                        }
                                        else
                                        {
                                            if (-(CHANGE) > UNUSE)
                                            {
                                                flag_rule = false;
                                                message = message + "Location '" + this.gvData.Rows[i].HeaderCell.Value.ToString() + "' CHANGE+UNUSE<0!" + "\n";
                                                Rec_Locat(i);
                                            }
                                        }
                                    }

                                    #endregion
                                }
                                else//USED,UNUSE棧板數維護
                                {
                                    #region 規則檢查(TOTAL=USED+UNUSE)
                                    //CHANGE>0-->CHANGE增加數量不能>UNUSE數量
                                    //CHANGE<0-->CHANGE扣除數量不能>USED數量
                                    if (CHANGE > 0)
                                    {
                                        if (CHANGE > UNUSE)
                                        {
                                            flag_rule = false;
                                            message = message + "Location '" + this.gvData.Rows[i].HeaderCell.Value.ToString() + "' CHANGE>UNUSE!" + "\n";
                                            Rec_Locat(i);
                                        }
                                    }
                                    else
                                    {
                                        if (-(CHANGE) > USED)
                                        {
                                            flag_rule = false;
                                            message = message + "Location '" + this.gvData.Rows[i].HeaderCell.Value.ToString() + "' CHANGE+USED<0!!" + "\n";
                                            Rec_Locat(i);
                                        }
                                    }

                                    #endregion
                                }

                                #endregion

                                if (flag_rule)
                                {
                                    #region 計算出改變後的(TOTAL,USED,UNUSE值)

                                    if (chk1.Checked)//Total棧板數維
                                    {
                                        if (flag_first_total)//第一次做
                                        {
                                            TOTAL = TOTAL + CHANGE;
                                            UNUSE = UNUSE + CHANGE;
                                        }
                                        else
                                        {
                                            TOTAL = TOTAL + CHANGE;
                                            UNUSE = UNUSE + CHANGE;
                                        }
                                    }
                                    else//USED,UNUSE棧板數維護
                                    {
                                        USED = USED + CHANGE;
                                        UNUSE = UNUSE - CHANGE;
                                    }

                                    #endregion

                                    #region DB寫入 

                                    objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Werks, Lgort);

                                    if (TOTAL == 0 && USED == 0 && UNUSE == 0)//此儲位已無棧板
                                    {
                                        objStorageData.Update_WHPAL(LOCATION, TOTAL, USED, UNUSE, CHANGE, "delete", chk1.Checked,false);
                                    }
                                    else
                                    {
                                        if (flag_first_total)//第一次做
                                            objStorageData.Update_WHPAL(LOCATION, TOTAL, USED, UNUSE, CHANGE, "insert", chk1.Checked, false);
                                        else
                                            objStorageData.Update_WHPAL(LOCATION, TOTAL, USED, UNUSE, CHANGE, "update", chk1.Checked, false);
                                    }

                                    #endregion
                                }

                                #endregion
                            }
                            else
                                message = message + "Location '" + this.gvData.Rows[i].HeaderCell.Value.ToString() + "' is not exist!" + "\n";
                        }
                        else
                            continue;
                    }
                    else
                        continue;
                }

                flag_numberic = false;

                //紀錄目前scrollbar的位置
                ScrollPosition = gvData.FirstDisplayedScrollingRowIndex;

                //顯示更新後DataGridView
                MaintainGrid();

                //寫入之前scrollbar的位置
                gvData.FirstDisplayedScrollingRowIndex = ScrollPosition;

                //Show出訊息
                if (message == "")
                    MessageBox.Show("Save Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                #region 將錯誤的CHANGE欄位標註紅色

                for (int i = 0; i < gvData.Rows.Count; i++)
                {
                    if (this.gvData.Rows[i].Cells[3].Value != null && !Convert.IsDBNull(this.gvData.Rows[i].Cells[3].Value))
                    {
                        if (!this.gvData.Rows[i].Cells[3].Value.ToString().Equals(""))
                        {
                            this.gvData.Rows[i].Cells[3].Style.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }

                #endregion

                #endregion            
            }
            else
                MessageBox.Show("No Data! ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //檢查要修改的儲位在不在
        private bool LocatCheck(string l1)
        {
            bool flag1 = false;

            for (int i = 0; i < dtData3.Rows.Count; i++)
            {
                if (dtData3.Rows[i]["LOCAT"].Equals(l1))
                {
                    flag1 = true;
                    break;
                }
            }

            return flag1;
        }

        //紀錄CHANGE檢查有誤的LOCAT
        private void Rec_Locat(int index)
        {
            a1 = new ArrayList();
            a1.Add(this.gvData.Rows[index].HeaderCell.Value.ToString());//儲位名稱
            a1.Add(this.gvData.Rows[index].Cells[3].Value.ToString());//CHANGE
            arr_CHANGE.Add(a1);
        }

        //將儲位header拆掉floor & area資訊
        private string HeaderSplit(string l1)
        {
            int pointer = 0;//找出"("位置
            bool flag = false;

            for (int i = 0; i < l1.Length; i++)
            {
                if (l1.Substring(i, 1).Equals("("))
                {
                    pointer = i;
                    flag = true;
                    break;
                }
            }

            if (flag)
            l1 = l1.Substring(0, pointer);

            return l1;
        }

        #endregion

        #region Refresh

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            flag_refersh1 = true;
            flag_refersh2 = true;
            flag_numberic = false;
            Initial_Control();
           //this.gvData.DataSource = null;
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

        #region Import

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (DdlCheck(cmbWerks) && DdlCheck(cmbLgort))//combobox皆有值
                {
                    //存入Maintain頁面所選擇的廠區,倉別
                    strWerks_M = cmbWerks.SelectedItem.ToString();
                    strLgort_M = cmbLgort.SelectedItem.ToString();

                    if (this.txtUpload.Text.Trim() == "")
                        MessageBox.Show("Please Select File Path!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        //抓出檔案路徑及file extension
                        string path = txtUpload.Text.Trim();
                        string file_extension = SplitExtension(path);

                        //檢查上傳檔案是否為excel檔
                        if (file_extension.Equals(".xls", StringComparison.CurrentCultureIgnoreCase) || file_extension.Equals(".xlsx", StringComparison.CurrentCultureIgnoreCase))
                        //if (file_extension.Equals(".xls", StringComparison.CurrentCultureIgnoreCase))
                        {
                            this.Cursor = Cursors.WaitCursor;
                            System.Data.DataTable Excel_Data = new DataTable();

                            //將excel轉換成datatable
                            Excel_Data = RenderDataTableFromExcel(path, 0, 0, file_extension);

                            //excel檔案檢查,db寫入
                            CheckExcel(Excel_Data);

                            this.Cursor = Cursors.Default;
                        }
                        else
                            MessageBox.Show("Please Import Excel Format File!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    MessageBox.Show("Please Choose Plant and Storage in Maintain Interface!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                stsWarning.Text = ex.Message;
                return;
            }
        }

        #endregion

        #region Upload

        private void btnUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtUpload.Text = openFileDialog1.FileName.ToString();
            }
        }

        #endregion
        
        #endregion

        #region LinkLabel

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = "";
            string Date;            
            Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyyMMdd");

            bool flag_ALL = false;

            #region 檢查目前是PLANT是選擇ALL or 單一廠區

            for (int i = 0; i < gvData2.Columns.Count; i++)
            {
                if (gvData2.Columns[i].HeaderText.Equals("STORAGE(QUANTITY)"))//ALL選項
                {
                    flag_ALL = true;
                    break;
                }
                else
                    continue;
            }

            #endregion

            if (flag_ALL)//ALL選項
                saveFileDialog1.FileName = "Location Usage Report(ALL PLANT)_" + Date + ".xls";
            else//單一廠區
                saveFileDialog1.FileName = "Location Usage Report_" + Date + ".xls";
                           
            //FolderBrowserDialog win = new FolderBrowserDialog(); 
            //win.ShowDialog();
            //path = win.SelectedPath;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                #region 處理中

                flag_PGB1 = true;

                AsyncTemplate.DoWorkAsync(
                () =>
                {                    
                    //stsWarning.Text = "";
                    //將gvdata2中的資料寫入excel
                    fToExcel(saveFileDialog1.FileName);
                    Thread.Sleep(2000);
                    return (true);                  
                },
                (result) =>
                {
                    MessageBox.Show("Report Download Successful", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //stsWarning.Text = "Report Download Successful!";
                },
                (exception) =>
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //stsWarning.Text = exception.Message;
                });

                #endregion              
            }

            /*
            if (flag_complete.Equals("complete"))
                MessageBox.Show("Report Download Successful", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (flag_complete.Equals("fail"))
                MessageBox.Show(ex_message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            */
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = "";
            string Date;
            Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyyMMdd");

            saveFileDialog1.FileName = "Pallet Information Report_" + Date + ".xls";

            //FolderBrowserDialog win = new FolderBrowserDialog(); 
            //win.ShowDialog();
            //path = win.SelectedPath;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                #region 處理中

                flag_PGB2 = true;

                AsyncTemplate.DoWorkAsync(
                () =>
                {
                    //stsWarning.Text = "";
                    //將gvdata2中的資料寫入excel
                    fToExcel2(saveFileDialog1.FileName);
                    Thread.Sleep(2000);
                    return (true);
                },
                (result) =>
                {
                    MessageBox.Show("Report Download Successful", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //stsWarning.Text = "Report Download Successful!";
                },
                (exception) =>
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //stsWarning.Text = exception.Message;
                });

                #endregion
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("Excel", "Manage_LocationPallet(Sample).xls");
        }

        private void Excel(string path)
        {
            #region Interop
            
            //string path = System.IO.Directory.GetCurrentDirectory();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;
            Excel.XlSaveAsAccessMode mode = new Excel.XlSaveAsAccessMode();

            string Date;
            Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd");

            path = path + @"\Location Usage Report_" + Date + ".xls"; 

            int Count_Row;

            ws.Name = "Location Usage Report";
            wb.Sheets.Add(Type.Missing, wb.Worksheets[1], 1, Type.Missing);


            string[] Column = {"WERKS","LGORT","TOTAL","USED","UNUSE","PRECENT" };//欄位

            for (int i = 0; i < Column.Length; i++)
            {
                ws.Cells[1, i + 1] = Column[i];
            }

            Count_Row = 2;
            foreach (DataRow r1 in dtData2.Rows)
            {
                ws.Cells[Count_Row, 1] = r1[0];//WERKS
                ws.Cells[Count_Row, 2] = r1[1];//LGORT
                ws.Cells[Count_Row, 3] = r1[2];//TOTAL
                ws.Cells[Count_Row, 4] = r1[3];//USED
                ws.Cells[Count_Row, 5] = r1[4];//UNUSE
                ws.Cells[Count_Row, 6] = r1[5];//PRECENT

                Count_Row++;
            }            

            //儲存Excel檔
            wb._SaveAs(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, mode, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            wb.Close(false, Type.Missing, Type.Missing);
            xlApp.Workbooks.Close();
            xlApp.Quit();

            //刪除 Excel.exe Process  
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(aRange);
            xlApp = null;
            wb = null;
            ws = null;
            //aRange = null;

            //Garbege Collection 
            GC.Collect();
            
            #endregion
        }

        private void CountingResult2File(string strFilePath)
        {
            string strLine = "";
            FileInfo fi = new FileInfo(strFilePath);
            StreamWriter sw = new StreamWriter(strFilePath, false, System.Text.Encoding.Unicode);

            try
            {
                //string Date;
                //Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy/MM/dd");

                //strFilePath = strFilePath + @"\Location Usage Report_" + Date + ".xls"; 

                strLine = "WERKS\tLGORT\tTOTAL\tUSED\tUNUSE\tPRECENT";
                sw.WriteLine(strLine);
                //Reading data
                for (int i = 0; i < dtData2.Rows.Count; i++)
                 {
                        strLine = "";
                        strLine += dtData2.Rows[i][0].ToString() + "\t";
                        strLine += dtData2.Rows[i][1].ToString() + "\t";
                        strLine += dtData2.Rows[i][2].ToString() + "\t";
                        strLine += dtData2.Rows[i][3].ToString() + "\t";
                        strLine += dtData2.Rows[i][4].ToString() + "\t";
                        strLine += dtData2.Rows[i][5].ToString() + "\t";
                        sw.WriteLine(strLine);
                 }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }

        }

        private void fToExcel(string path)
        {
            string Date;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, System.Text.Encoding.Unicode);//路徑,可否覆寫,編碼,存至援充區            
            Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyyMMdd");

            try
            {
             sw.WriteLine("<table border=1 cellspacing=0 cellpadding=0>");//excel表的邊框大小

             bool flag_ALL = false;

             #region 檢查目前是PLANT是選擇ALL or 單一廠區

             for (int i = 0; i < gvData2.Columns.Count; i++)
             {
                 if (gvData2.Columns[i].HeaderText.Equals("STORAGE(QUANTITY)"))//ALL選項
                 {
                     flag_ALL = true;
                     break;
                 }
                 else
                     continue;
             }

             #endregion

             if (flag_ALL)//ALL選項
             {
                 sw.WriteLine("<tr><td colspan=7 align=center>" + "Location Usage Report(ALL PLANT)--" + "Date:" + Date + "</td></tr>");
                 sw.WriteLine("<tr><td>PLANT</td><td>STORAGE(QUANTITY)</td><td>DESCRIPTION</td><td>TOTAL</td><td>USED</td><td>UNUSE</td><td>USAGE RATE(%)</td></tr>");//設定表格標題

                 string strLine = "";

                 for (int i = 0; i < dtData2.Rows.Count; i++)
                 {
                     strLine = "";
                     strLine += "<tr><td>" + dtData2.Rows[i]["PLANT"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["STORAGE(QUANTITY)"].ToString() + "";
                     strLine += "</td><td>" + "" + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["TOTAL"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["USED"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["UNUSE"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["USAGE RATE"].ToString() + "";
                     strLine += "</td></tr>";
                     sw.WriteLine(strLine);
                 }
             }
             else//單一廠區
             {
                 sw.WriteLine("<tr><td colspan=9 align=center>" + "Location Usage Report--" + "Date:" + Date + "</td></tr>");
                 sw.WriteLine("<tr><td>PLANT</td><td>STORAGE</td><td>FLOOR</td><td>AREA</td><td>DESCRIPTION</td><td>TOTAL</td><td>USED</td><td>UNUSE</td><td>USAGE RATE(%)</td></tr>");//設定表格標題

                 string strLine = "";

                 for (int i = 0; i < dtData2.Rows.Count; i++)
                 {
                     strLine = "";
                     strLine += "<tr><td>" + dtData2.Rows[i]["PLANT"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["STORAGE"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["FLOOR"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["AREA"].ToString() + "";
                     strLine += "</td><td>" + "" + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["TOTAL"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["USED"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["UNUSE"].ToString() + "";
                     strLine += "</td><td>" + dtData2.Rows[i]["USAGE RATE"].ToString() + "";
                     strLine += "</td></tr>";
                     sw.WriteLine(strLine);
                 }
             }

            sw.WriteLine("</table>");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }
        }

        private void fToExcel2(string path)
        {
            //重新抓取WHPAL join WHHED的資料
            objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData,strWerks_M, strLgort_M);           
            dtData4 = objStorageData.QueryStorageStatus2();


            string Date;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, System.Text.Encoding.Unicode);//路徑,可否覆寫,編碼,存至援充區            
            Date = Convert.ToDateTime(System.DateTime.Now).ToString("yyyyMMdd");

            try
            {
                sw.WriteLine("<table border=1 cellspacing=0 cellpadding=0>");//excel表的邊框大小
                sw.WriteLine("<tr><td colspan=9 align=center>" + "Pallet Information Report--" + "Date:" + Date + "</td></tr>");
                sw.WriteLine("<tr><td>PLANT</td><td>STORAGE</td><td>FLOOR</td><td>AREA</td><td>LOCATION</td><td>TYPE</td><td>TOTAL</td><td>USED</td><td>UNUSE</td></tr>");//設定表格標題

                string strLine = "";

                for (int i = 0; i < dtData4.Rows.Count; i++)
                {
                    strLine = "";
                    strLine += "<tr><td>" + dtData4.Rows[i]["WERKS"].ToString() + "";
                    strLine += "</td><td>" + dtData4.Rows[i]["LGORT"].ToString() + "";
                    strLine += "</td><td>" + dtData4.Rows[i]["FLOOR"].ToString() + "";
                    strLine += "</td><td>" + dtData4.Rows[i]["AREA"].ToString() + "";
                    strLine += "</td><td>" + dtData4.Rows[i]["LOCAT"].ToString() + "";
                    strLine += "</td><td>" + dtData4.Rows[i]["TYPE"].ToString() + "";
                    strLine += "</td><td>" + dtData4.Rows[i]["TOTAL"].ToString() + "";
                    strLine += "</td><td>" + dtData4.Rows[i]["USED"].ToString() + "";
                    strLine += "</td><td>" + dtData4.Rows[i]["UNUSE"].ToString() + "";
                    strLine += "</td></tr>";
                    sw.WriteLine(strLine);
                }

                sw.WriteLine("</table>");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-CountingResult2File()");
            }
            finally
            {
                sw.Close();
            }
        }

        #endregion

        #region ProgressBart

        private void Start_Progress()
        {
                PGB1.Style = ProgressBarStyle.Marquee;
                PGB1.MarqueeAnimationSpeed = 50;
                this.Cursor = Cursors.WaitCursor;
        }

        private void Stop_Progress()
        {
                PGB1.Style = ProgressBarStyle.Blocks;
                this.Cursor = Cursors.Default;          
        }

        private void Start_Progress2()
        {
            PGB2.Style = ProgressBarStyle.Marquee;
            PGB2.MarqueeAnimationSpeed = 50;
            this.Cursor = Cursors.WaitCursor;
        }

        private void Stop_Progress2()
        {
            PGB2.Style = ProgressBarStyle.Blocks;
            this.Cursor = Cursors.Default;
        }

        //處理中methed
        public class AsyncTemplate
        {
            public static Action OnInvokeStarting { get; set; }

            public static Action OnInvokeEnding { get; set; }

            public static void DoWorkAsync(Action beginAction, Action endAction, Action<Exception> errorAction)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(
                    (o) =>
                    {
                        try
                        {
                            beginAction();

                            endAction();
                        }
                        catch (Exception ex)
                        {
                            errorAction(ex);
                            return;
                        }
                        finally
                        {
                            if (OnInvokeEnding != null)
                            {
                                OnInvokeEnding();
                            }
                        }
                    })
                , null);

                if (OnInvokeStarting != null)
                {
                    OnInvokeStarting();
                }
            }

            public static void DoWorkAsync<TResult>(Func<TResult> beginAction, Action<TResult> endAction, Action<Exception> errorAction)
            {                
                ThreadPool.QueueUserWorkItem(new WaitCallback(
                    (o) =>
                    {
                        TResult result = default(TResult);

                        try
                        {
                            result = beginAction();

                            endAction(result);

                        }
                        catch (Exception ex)
                        {
                            errorAction(ex);
                            return;
                        }
                        finally
                        {
                            if (OnInvokeEnding != null)
                            {
                                OnInvokeEnding();                                
                            }
                        }
                    })
                , null);

                if (OnInvokeStarting != null)
                {
                    OnInvokeStarting();
                }
            }
        }

        #endregion

        #region ComboBox

        private void ShowDdlWerks(ComboBox c1)
        {
            dtTemp = new DataTable();
            try
            {
                c1.Items.Clear();
                dtTemp = objAuthority.CheckPlantAuthority();

                //加入無選擇
                c1 = AddItem(c1);

                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    c1.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlWerks()");
            }
        }

        private void ShowDdlLgort(ComboBox c1,ComboBox c2)
        {
            try
            {
                stsWarning.Text = "";
                DataTable dtTemp = new DataTable();
                if (c2.SelectedIndex != -1)
                {
                    strWerks = c2.Items[c2.SelectedIndex].ToString();
                    dtTemp = objAuthority.CheckLgortAuthority(strWerks);
                }
                else
                {
                    dtTemp = objAuthority.CheckLgortAuthority();
                }
                if (c1.SelectedIndex != -1)
                {
                    strLgort = c1.Items[c1.SelectedIndex].ToString();
                }
                else
                {
                    c1.Items.Clear();
                }

                if (dtTemp.Rows.Count == 0)
                {
                    c1.Items.Clear();
                    c1 = AddItem(c1);
                    strLgort = "";
                }
                else
                {
                    c1.Items.Clear();
                    c1 = AddItem(c1);

                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        c1.Items.Add(dtTemp.Rows[i]["F_TEXT"].ToString());
                        if (dtTemp.Rows[i]["F_TEXT"].ToString() == strLgort && strLgort != "")
                        {
                            c1.SelectedIndex = i;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDdlLgort()");
            }
        }

        //檢查傳入combobox是否為有值
        private bool DdlCheck(ComboBox c1)
        {
            bool flag = true;

            if (c1.SelectedItem.ToString().Equals("--NULL--"))
                flag = false;

            return flag;
        }

        //下拉選單加入全選及沒有選擇
        private ComboBox AddItem(ComboBox c1)
        {
            c1.Items.Add("--NULL--");

            //如果為查詢功能,則加入全選選項
            if (c1.Name.Equals("cmbWerks2"))
                c1.Items.Add("--ALL--");

            return c1;
        }

        private void cmbWerks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks.SelectedItem.ToString().Equals("--NULL--"))
                {
                    cmbLgort.Items.Clear();
                    cmbLgort = AddItem(cmbLgort);
                    cmbLgort.SelectedIndex = 0;
                }
                    
                else
                {
                    stsWarning.Text = "";
                    ShowDdlLgort(cmbLgort,cmbWerks);
                    cmbLgort.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        private void cmbWerks2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbWerks2.SelectedItem.ToString().Equals("--NULL--"))
                {
                    cmbLgort2.Items.Clear();
                    cmbLgort2 = AddItem(cmbLgort2);
                    cmbLgort2.SelectedIndex = 0;
                }                    
                else
                {
                       stsWarning.Text = "";
                       ShowDdlLgort(cmbLgort2,cmbWerks2);
                       cmbLgort2.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }
        }

        //Maintain--使用率過濾條件(>,<,=)
        private void cmbOption1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Maintain_Unuse_Filter();//依照過濾條件,更新datagridview
        }

        //Query--使用率過濾條件(>,<,=)
        private void cmbOption2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Query_Usage_Filter();//依照過濾條件,更新datagridview
        }
        
        #endregion

        #region TextBox

        private void txtLocat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (txtLocat.Text.Trim().ToString().Equals(""))//輸入為空值
                    ShowDataGridView(dtData);
                else
                    Maintain_Location_Filter();
            }
        }

        #endregion

        #region DataGridView

        #region gvdata

        private void gvData_Paint(object sender, PaintEventArgs e)
        {
            // 绘制行号
            DataTable dtRowCol = new DataTable();
            DataRow[] drFound1;

            try
            {
                if (dtData != null && dtData.Rows.Count >= 0 && this.gvData.DataSource != null && !flag_refersh1)
                {
                    DataGridView.HitTestInfo hti = gvData.HitTest(pointInCell00.X, pointInCell00.Y);
                    int row = hti.RowIndex;

                    int yDelta = gvData.GetCellDisplayRectangle(row, 0, true).Height + 1;
                    int y = gvData.GetCellDisplayRectangle(row, 0, true).Top + 2;

                    CurrencyManager cm = (CurrencyManager)this.BindingContext[gvData.DataSource, gvData.DataMember];
                    ((DataView)cm.List).AllowNew = false;
                }
                else
                    flag_refersh1 = false;
            }
            catch (Exception ex)
            {
                //stsWarning.Text = ex.Message;
                return;
            }

        }

        //numbericupdown
        private void N1_Initial()
        {
            Rectangle rect = gvData.GetCellDisplayRectangle(gvData.CurrentCell.ColumnIndex, gvData.CurrentCell.RowIndex, false);
            int dgvX = gvData.Location.X;
            int dgvY = gvData.Location.Y;
            int cellX = rect.X;
            int cellY = rect.Y;
            N1.Left = dgvX + cellX;
            N1.Top = dgvY + cellY;
            N1.Size = rect.Size;
            N1.Visible = true;

            this.ActiveControl = this.N1;
            N1.Focus();

            //若cell原本有值則帶出
            if (this.gvData.CurrentCell.Value != null && !Convert.IsDBNull(this.gvData.CurrentCell.Value))
            {
                N1.Value = Convert.ToDecimal(this.gvData.CurrentCell.Value);
            }
               
            else
                N1.Value = 0;
        }

        private void N1_Leave(object sender, EventArgs e)
        {
            if (flag_numberic)
            rule_calculate();
        }

        private void gvData_Scroll(object sender, ScrollEventArgs e)
        {
            if (flag_numberic)
                rule_calculate();
            else
                N1.Visible = false;
        }

        private void rule_calculate()
        {
            //numbericupdown value to cell value
            if (this.gvData.Columns[this.gvData.CurrentCell.ColumnIndex].HeaderCell.Value.ToString().Equals("CHANGE"))
            {
                if (N1.Value != 0 && N1.Value != null && !Convert.IsDBNull(N1.Value))
                    this.gvData.CurrentCell.Value = N1.Value;
                else
                    this.gvData.CurrentCell.Value = null;
            }


            N1.Visible = false;
        }

        private void gvData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            flag_numberic = true;

            if (this.gvData.Columns[e.ColumnIndex].HeaderCell.Value != null)
            {
                if (this.gvData.Columns[e.ColumnIndex].HeaderCell.Value.ToString().Equals("CHANGE"))//點到CHANGE行,帶出numericupdown
                {
                    //若原本被標記紅色,則變回黑色
                    if (this.gvData.Rows[e.RowIndex].Cells[3].Value != null && !Convert.IsDBNull(this.gvData.Rows[e.RowIndex].Cells[3].Value))
                    {
                        if (!this.gvData.Rows[e.RowIndex].Cells[3].Value.ToString().Equals(""))
                        {
                            if (this.gvData.Rows[e.RowIndex].Cells[3].Style.ForeColor == System.Drawing.Color.Red)
                                this.gvData.Rows[e.RowIndex].Cells[3].Style.ForeColor = System.Drawing.Color.Black;
                        }
                    }

                    if (chk2.Checked)//如果為USED/UNUSE維護
                    {
                        //檢查是否已經有維護過TOTAL
                        if (this.gvData.Rows[e.RowIndex].Cells[0].Value != null && !Convert.IsDBNull(this.gvData.Rows[e.RowIndex].Cells[0].Value))
                        {
                            if (!this.gvData.Rows[e.RowIndex].Cells[0].Value.ToString().Equals(""))
                            {
                                N1_Initial();

                                if (redcell_Used != -1)
                                    this.gvData.Rows[redcell_Used].Cells[1].Style.BackColor = Color.White;//將之前USED cell顏色還原

                                this.gvData.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.Yellow;//將USED cell標記顏色
                                redcell_Used = e.RowIndex;
                            }
                            else
                            {
                                flag_numberic = false;
                                MessageBox.Show("Please Maintain Total Pallet Number first! ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                            }
                                
                        }
                        else
                        {
                            flag_numberic = false;
                            MessageBox.Show("Please Maintain Total Pallet Number first! ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);                          
                        }
                    }
                    else
                    {
                        N1_Initial();

                        if (redcell_Total != -1)
                            this.gvData.Rows[redcell_Total].Cells[0].Style.BackColor = Color.White;//將之前TOTAL cell顏色還原

                        this.gvData.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.Yellow;//將TOTAL cell標記顏色
                        redcell_Total = e.RowIndex;
                    }
                }
            }
        }

        private void gvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.LightGray;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            gvData.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            gvData.RowHeadersDefaultCellStyle = columnHeaderStyle;

            #region 設置DataGridView的列名

            if (gvData.Rows.Count != 0)
            {
                //this.gvData.ColumnHeadersHeight = 100;
                this.gvData.RowHeadersWidth = 190;

                for (int i = 0; i < M_Row.Count; i++)
                    this.gvData.Rows[i].HeaderCell.Value = M_Row[i].ToString();

                //根據user的選項,標記顏色(Total or USED/UNUSE)
                if (chk1.Checked)
                    this.gvData.Columns[0].HeaderCell.Style.BackColor = System.Drawing.Color.Yellow;
                else
                {
                    this.gvData.Columns[1].HeaderCell.Style.BackColor = System.Drawing.Color.Yellow;
                    //this.gvData.Rows[2].HeaderCell.Style.BackColor = System.Drawing.Color.Yellow;
                }
            }

            #endregion
        }

        private void gvData_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            /*
            flagCheck = true;

            //目前儲存格不符合格式檢查
            if (!(ValidaCell(gvData.Rows[e.RowIndex].Cells[e.ColumnIndex])))
            {
                flagCheck = false;
            }
            */
        }

        private void gvData_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (!flagCheck)
            {
                gvData.CurrentCell = gvData.Rows[e.RowIndex].Cells[e.ColumnIndex];//給定目前的cell
                gvData.EditMode = DataGridViewEditMode.EditProgrammatically;
                gvData.BeginEdit(true);
            }
            */
        }

        private void MaintainGrid()
        {
            #region Oringinal

            stsWarning.Text = "";
            //DataTable dtTemp = new DataTable();
            dtLgort = objPlantData.GetPlantStorageData("LGORT", cmbWerks.Items[cmbWerks.SelectedIndex].ToString(), cmbLgort.Items[cmbLgort.SelectedIndex].ToString());
            strSttyp = dtLgort.Rows[0]["CTRLC4"].ToString();
            strLotyp = dtLgort.Rows[0]["CTRLC5"].ToString();
            strWerks = cmbWerks.Items[cmbWerks.SelectedIndex].ToString();
            strLgort = cmbLgort.Items[cmbLgort.SelectedIndex].ToString();

            #endregion

            #region 帶回WHHED join WHPAL的資料 by Gary Lee 20130531

            objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Werks, Lgort);
            dtData = objStorageData.QueryStorageStatus2();

            if (dtData.Rows.Count > 0)
            {
                ShowDataGridView(dtData);
                this.linkLabel2.Enabled = true;
            }

            else
            {
                this.linkLabel2.Enabled = false;
                MessageBox.Show("No Data! ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                
            #endregion            
        }

        private void ShowDataGridView(DataTable dt)
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataTable dtTemp = new DataTable();
                DataTable dtRowCol = new DataTable();
                ArrayList USED = new ArrayList();//已使用棧板數(USED)
                ArrayList UNUSE = new ArrayList();//未使用棧板數(UNUSE)
                ArrayList TOTAL = new ArrayList();//已使用棧板數(TOTAL)
                ArrayList CHANGE = new ArrayList();//User修改棧板數量(CHANGE)
                DataGridViewTextBoxColumn aColumnTextColumn;
                DataRow drFound1, drFound2;
                string FAT_Info;
              
                #region 设置DataGridView的欄名

                string[] name = {"TOTAL","USED","UNUSE","CHANGE"};
                
                for (int i = 1; i <= name.Length; i++)
                {
                    dtTemp.Columns.Add(new DataColumn(i.ToString(), typeof(string)));
                    aColumnTextColumn = new DataGridViewTextBoxColumn();
                    aColumnTextColumn.HeaderText = name[i - 1];
                    aColumnTextColumn.DataPropertyName = i.ToString();
                    aColumnTextColumn.Width = 80;
                    aColumnTextColumn.ReadOnly = false;
                    aColumnTextColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.gvData.Columns.Add(aColumnTextColumn);
                }
                               
                #endregion

                #region 紀錄列名稱(儲位名稱,樓層區域資訊)

                M_Row = new ArrayList();

                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    drFound1 = dt.Rows[i - 1];
                    FAT_Info = "";

                    #region 樓層區域

                    if (drFound1["FLOOR"] != null)
                    {
                        if (!drFound1["FLOOR"].ToString().Trim().Equals(""))//有樓層資訊
                        {
                            if (drFound1["AREA"] != null)
                            {
                                if (!drFound1["AREA"].ToString().Trim().Equals(""))//有區域資訊
                                {
                                    FAT_Info = drFound1["FLOOR"].ToString().Trim() + "-" + drFound1["AREA"].ToString().Trim();
                                }
                                else
                                    FAT_Info = drFound1["FLOOR"].ToString().Trim();
                            }
                            else
                                FAT_Info = drFound1["FLOOR"].ToString().Trim();
                        }
                    }
                    
                    #endregion

                    #region 儲位類型

                    if (drFound1["TYPE"] != null)
                    {
                        if (!drFound1["TYPE"].ToString().Trim().Equals(""))//有儲位類型資訊
                        {
                            if (!FAT_Info.Trim().Equals(""))//有樓層資訊
                                FAT_Info = FAT_Info + "," + drFound1["TYPE"].ToString().Trim();
                            else
                                FAT_Info = drFound1["TYPE"].ToString().Trim();
                        }
                    }
                    
                    #endregion

                    if (!FAT_Info.Trim().Equals(""))//有樓層或儲位類型資訊資訊
                        FAT_Info = "(" + FAT_Info + ")";

                    M_Row.Add(drFound1["LOCAT"].ToString().Trim() + FAT_Info);
                }

                #endregion

                #region 處理DataGridView的資料

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //NULL判斷
                    USED.Add(dt.Rows[i]["USED"].ToString());
                    UNUSE.Add(dt.Rows[i]["UNUSE"].ToString());
                    TOTAL.Add(dt.Rows[i]["TOTAL"].ToString());
                    //CHANGE[i] = "0";
                }

                //CHANGE初始化
                for (int i = 0; i < M_Row.Count; i++)
                {
                    CHANGE.Add(null);
                }


                if (arr_CHANGE.Count != 0)//CHANGE有錯誤
                {
                    #region 將有錯誤的CHAMGE保留並標示

                    int record = 0;
                    bool flag_error = false;

                    for (int j = 0; j < arr_CHANGE.Count; j++)
                    {
                        a1 = (ArrayList)arr_CHANGE[j];

                        for (int k = record; k < M_Row.Count; k++)
                        {
                            if (M_Row[k].ToString().Equals(a1[0].ToString()))//若選到錯誤的儲位
                            {
                                CHANGE[k] = a1[1].ToString();
                                break;
                            }

                            record++;
                        }

                    }

                    #endregion
                }

                object[] rowArray = new object[4];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow Temp = dtTemp.NewRow();
                    rowArray[0] = TOTAL[i].ToString();
                    rowArray[1] = USED[i].ToString();
                    rowArray[2] = UNUSE[i].ToString();

                    if (CHANGE[i] != null && !Convert.IsDBNull(CHANGE[i]))
                        rowArray[3] = CHANGE[i].ToString();
                    else
                        rowArray[3] = null;

                    Temp.ItemArray = rowArray;

                    dtTemp.Rows.Add(Temp);
                }

                gvData.DataSource = dtTemp;

                //gvData.Rows[3].Cells[k].Style.BackColor = System.Drawing.Color.Red;

                #endregion

                if (dt.Rows.Count != 0)
                {
                    pointInCell00 = new Point(gvData.GetCellDisplayRectangle(0, 0, true).X + 4, gvData.GetCellDisplayRectangle(0, 0, true).Y + 4);

                    //設定提示
                    //gvData.Columns[3].HeaderCell.ToolTipText = "Please Enter Pallet Value You Want to Change";
                    Rectangle r = gvData.GetCellDisplayRectangle(3, 0, false);
                    pictureBox2.Location = new Point(r.Left + 26, r.Top - 18);
                    pictureBox2.Visible = true;

                    r.Location = gvData.PointToScreen(r.Location);
                    //Cursor.Position = new Point(r.Left + 60, r.Top - 15);
                }
                else
                    pictureBox2.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
        }

        //檢查是否輸入的儲存格是否符合格式
        private bool ValidaCell(DataGridViewCell cell)
        {
            cell.ErrorText = "";
            if (gvData.Rows[cell.RowIndex].Cells[3].Value == null || gvData.Rows[cell.RowIndex].Cells[3].Value.ToString() == "")
                return true;

            System.Text.RegularExpressions.Regex regul = new System.Text.RegularExpressions.Regex(@"[0-9]");
            if (!regul.IsMatch(cell.Value.ToString()))
            {
                cell.ErrorText = "只允許填入數字";
                return false;
            }
            return true;
        }
        
        #endregion

        #region gvdata2

        private void gvData2_Paint(object sender, PaintEventArgs e)
        {            
            // 绘制行号
            DataTable dtRowCol = new DataTable();
            DataRow[] drFound1;

            try
            {

                if (dtData2 != null && dtData2.Rows.Count >= 0 && this.gvData2.DataSource != null && !flag_refersh2)
                {

                    DataGridView.HitTestInfo hti = gvData2.HitTest(pointInCell00.X, pointInCell00.Y);
                    int row = hti.RowIndex;

                    int yDelta = gvData2.GetCellDisplayRectangle(row, 0, true).Height + 1;
                    int y = gvData2.GetCellDisplayRectangle(row, 0, true).Top + 2;

                    CurrencyManager cm = (CurrencyManager)this.BindingContext[gvData2.DataSource, gvData2.DataMember];
                    ((DataView)cm.List).AllowNew = false;
                }
                else
                    flag_refersh2 = false;
            }
            catch (Exception ex)
            {
                //stsWarning.Text = ex.Message;
                return;
            }
            
        }

        private void gvData2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.LightGray;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            gvData2.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            gvData2.RowHeadersDefaultCellStyle = columnHeaderStyle;
            gvData2.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gvData2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            for (int i = 0; i < gvData2.Columns.Count; i++)
            {
                if (gvData2.Columns[i].HeaderText.Equals("STORAGE(QUANTITY)"))
                    gvData2.Columns[i].Width = 150;
                else
                    gvData2.Columns[i].Width = 100;                
            }

                #region 設置DataGridView的行名

                this.gvData2.RowHeadersWidth = 70;

            for (int i = 0; i < gvData2.Rows.Count; i++)
            {
                this.gvData2.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

            #endregion
        }

        private void QueryGrid(int condition)
        {
            #region Oringinal

            stsWarning.Text = "";
            /*
            dtLgort = objPlantData.GetPlantStorageData("LGORT", cmbWerks2.Items[cmbWerks2.SelectedIndex].ToString(), cmbLgort2.Items[cmbLgort2.SelectedIndex].ToString());
            strSttyp = dtLgort.Rows[0]["CTRLC4"].ToString();
            strLotyp = dtLgort.Rows[0]["CTRLC5"].ToString();
            */
            strWerks = cmbWerks2.Items[cmbWerks2.SelectedIndex].ToString();
            strLgort = cmbLgort2.Items[cmbLgort2.SelectedIndex].ToString();

            #endregion

            #region 查詢WHHED的資料

            objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, Werks, Lgort);
            dtData2 = objStorageData.QueryStorageStatus3(condition, dtTemp);

            if (dtData2.Rows.Count > 0)
            {
                ShowDataGridView2(dtData2);
                this.linkLabel1.Enabled = true;
            }
            else
            {
                this.linkLabel1.Enabled = false;
                MessageBox.Show("No Data! ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                

            #endregion
        }

        private void ShowDataGridView2(DataTable dt)
        {           
            gvData2.DataSource = dt;
            foreach (DataGridViewColumn gCol in gvData2.Columns)
                gCol.Width = 72;            
        }

        private void gvData2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //選項不為ALL時,帶出該Storage下的儲位資訊
            if (!cmbWerks2.SelectedItem.ToString().Equals("--ALL--"))
            {
                //plant,storgae pk必有值,floor and area不一定
                string plant = gvData2.Rows[e.RowIndex].Cells["PLANT"].Value.ToString();
                string storage = gvData2.Rows[e.RowIndex].Cells["STORAGE"].Value.ToString();
                string floor = "";
                string area = "";
                DataTable dtTemp = new DataTable();

                if(gvData2.Rows[e.RowIndex].Cells["FLOOR"].Value!=null)
                {
                    if(!gvData2.Rows[e.RowIndex].Cells["FLOOR"].Value.ToString().Equals(""))
                        floor = gvData2.Rows[e.RowIndex].Cells["FLOOR"].Value.ToString();
                    else
                        floor="";
                }

                if (gvData2.Rows[e.RowIndex].Cells["AREA"].Value != null)
                {
                    if (!gvData2.Rows[e.RowIndex].Cells["AREA"].Value.ToString().Equals(""))
                        area = gvData2.Rows[e.RowIndex].Cells["AREA"].Value.ToString();
                    else
                        area = "";
                }

                objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, plant, storage);
                dtTemp = objStorageData.QueryStorageStatus4(plant, storage,floor, area);

                Manage_LocationPallet_Info objManage_LocationPallet_Info = new Manage_LocationPallet_Info(stsMandt.Text, stsComcd.Text, plant, storage, floor, area, dtTemp);
                objManage_LocationPallet_Info.ShowDialog();
            }
        }

        #endregion

        #endregion

        #region CheckeBox

        private void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked)
            {
                chk2.Checked = false;
                chk6.Checked = false;
            }               
        }

        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked)
            {
                chk1.Checked = false;
                chk6.Checked = false;
            }   
        }

        private void chk6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk6.Checked)
            {
                chk1.Checked = false;
                chk2.Checked = false;
                txtUpload.Enabled = true;
                linkLabel3.Enabled = true;
                btnUpload.Enabled = true;
                btnImport.Enabled = true;
            }
            else
            {
                txtUpload.Text = null;
                txtUpload.Enabled = false;
                linkLabel3.Enabled = false;
                btnUpload.Enabled = false;
                btnImport.Enabled = false;
            }
        }

        private void chk3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk3.Checked)
            {
                chk4.Checked = false;
                //this.chk3.ForeColor = System.Drawing.SystemColors.ControlText;
                txtLocat.Enabled = true;
            }
            else
            {
                txtLocat.Text = null;
                //this.chk3.ForeColor = System.Drawing.SystemColors.ControlDark;
                txtLocat.Enabled = false;
                ShowDataGridView(dtData);//將gvdata初始化
            }
                
        }

        private void chk4_CheckedChanged(object sender, EventArgs e)
        {
            if (chk4.Checked)
            {
                chk3.Checked = false;
                //this.chk4.ForeColor = System.Drawing.SystemColors.ControlText;
                N2.Enabled = true;
                cmbOption1.Enabled = true;

                Maintain_Unuse_Filter();//依照棧板數條件,更新datagridview
            }
            else
            {
                //this.chk4.ForeColor = System.Drawing.SystemColors.ControlDark;
                N2.Value = 0;
                N2.Enabled = false;
                cmbOption1.SelectedIndex = 0;
                cmbOption1.Enabled = false;
                ShowDataGridView(dtData);//將gvdata初始化
            }
                
        }

        private void chk5_CheckedChanged(object sender, EventArgs e)
        {
            if (chk5.Checked)//以使用率為條件查詢
            {
                //this.chk5.ForeColor = System.Drawing.SystemColors.ControlText;
                N3.Enabled = true;
                cmbOption2.Enabled = true;
                this.label20.ForeColor = System.Drawing.SystemColors.ControlText;

                Query_Usage_Filter();//依照過濾條件,更新datagridview
            }
            else//不以使用率為條件查詢
            {
                //this.chk5.ForeColor = System.Drawing.SystemColors.ControlDark;
                N3.Value = 0;
                N3.Enabled = false;
                cmbOption2.SelectedIndex = 0;
                cmbOption2.Enabled = false;
                this.label20.ForeColor = System.Drawing.SystemColors.ControlDark;

                gvData2.DataSource = dtData2;
            }
        }

        //檢查傳入checkbox是否為有值
        private bool ChkCheck(CheckBox c1)
        {
            bool flag = true;

            if (!c1.Checked)
                flag = false;

            return flag;
        }

        #endregion

        #region TabControl

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //檢查有無切換的權限
            if (tabControl1.SelectedIndex == 0)//Maintain
            {
                if (!(flag_Maintain_Total || flag_Maintain_Used))
                {
                    tabControl1.SelectedIndex = 1;
                    MessageBox.Show("No Sufficient Privilege! ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (tabControl1.SelectedIndex == 1)//Query
            {
                if (!flag_Query)
                {
                    tabControl1.SelectedIndex = 0;
                    MessageBox.Show("No Sufficient Privilege! ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }  
            }

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            StringFormat StrFormat = new StringFormat();
            StrFormat.LineAlignment = StringAlignment.Center;// 設置文字垂直方向居中
            StrFormat.Alignment = StringAlignment.Center;// 設置文字水平方向居中
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 9f);
            SolidBrush brush = new SolidBrush(Color.Black);
            SolidBrush brush2 = new SolidBrush(SystemColors.Control);
            SolidBrush brush3 = new SolidBrush(Color.Snow);
            int index = tabControl1.SelectedIndex;

            Rectangle recChild1 = tabControl1.GetTabRect(0); 
            Rectangle recChild2 = tabControl1.GetTabRect(1);
            if (index == 0)//Maintain
            {
                g.FillRectangle(brush3, recChild1);
                //g.FillRectangle(brush2, recChild2);
            }
            else if (index == 1)//Query
            {
                g.FillRectangle(brush3, recChild2);
                //g.FillRectangle(brush2, recChild1);
            }
            g.DrawString("Maintain", font, brush, recChild1, StrFormat);
            g.DrawString("Query", font, brush, recChild2, StrFormat);           
        }

        #endregion

        #region StatusStrip

        # region 顯示狀態欄

        private void ShowStatusData()
        {
            this.stsDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            this.stsMandt.Text = UserData.Client;
            this.stsUsrnm.Text = UserData.UserId;
            this.stsComcd.Text = UserData.CompanyCode;

        }

        # endregion
        
        #endregion

        #region NumericUpDown

        private void N3_ValueChanged(object sender, EventArgs e)
        {
            Query_Usage_Filter();//依照過濾條件,更新datagridview
        }

        private void N2_ValueChanged(object sender, EventArgs e)
        {
            Maintain_Unuse_Filter();//依照過濾條件,更新datagridview
        }

        #endregion

        #region excel檔案處理

        //Data<-->Excel(NPOI)
        private DataTable RenderDataTableFromExcel(string fileName, int SheetIndex, int HeaderRowIndex, string file_extension)
        {
            System.Data.DataTable table = new System.Data.DataTable();

            if (file_extension.Equals(".xls"))
            {
                #region xls excel版本

                using (FileStream ExcelFileStream = new FileStream(fileName, FileMode.Open))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
                    HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(SheetIndex);
                    HSSFRow headerRow = (HSSFRow)sheet.GetRow(HeaderRowIndex);
                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                    //int cellCount = headerRow.LastCellNum;

                    int cellCount = 0;
                    //找出報表中最長的列長
                    for (int i = 0; i < sheet.LastRowNum; i++)
                    {
                        HSSFRow row = (HSSFRow)sheet.GetRow(i);

                        if (row != null)
                        {
                            if (row.LastCellNum > cellCount)
                                cellCount = row.LastCellNum;
                            else
                                continue;
                        }
                        else
                            continue;
                    }

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        //DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        DataColumn column = new DataColumn();
                        table.Columns.Add(column);
                        table.Columns[i].ColumnName = headerRow.Cells[i].ToString();
                    }

                    int rowCount = sheet.LastRowNum;

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        HSSFRow row = (HSSFRow)sheet.GetRow(i);
                        DataRow dataRow = table.NewRow();

                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    dataRow[j] = row.GetCell(j).ToString();

                                    if (row.GetCell(j).CellType == NPOI.SS.UserModel.CellType.FORMULA)//為公式值
                                    {
                                        HSSFCell cell = (HSSFCell)e.EvaluateInCell(row.GetCell(j));
                                        dataRow[j] = cell.ToString();
                                    }
                                    else//不為公式值
                                    {
                                        dataRow[j] = row.GetCell(j).ToString();
                                    }
                                }
                            }
                        }
                        else
                            dataRow[0] = "";

                        table.Rows.Add(dataRow);
                    }

                    ExcelFileStream.Close();
                    workbook = null;
                    sheet = null;
                }

                #endregion
            }
            else if (file_extension.Equals(".xlsx"))
            {
                #region xlsx excel版本

                using (FileStream ExcelFileStream = new FileStream(fileName, FileMode.Open))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(ExcelFileStream);
                    XSSFSheet sheet = (XSSFSheet)workbook.GetSheetAt(SheetIndex);
                    XSSFRow headerRow = (XSSFRow)sheet.GetRow(HeaderRowIndex);
                    XSSFFormulaEvaluator e = new XSSFFormulaEvaluator(workbook);

                    //找出最大column數
                    int cellCount = 0;
                    for (int i = 0; i <= sheet.LastRowNum; i++)
                    {
                        XSSFRow row = (XSSFRow)sheet.GetRow(i);

                        if (row != null)
                        {
                            if (row.LastCellNum > cellCount)
                                cellCount = row.LastCellNum;
                        }
                    }

                    for (int i = 0; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn("");
                        table.Columns.Add(column);
                        table.Columns[i].ColumnName = headerRow.Cells[i].ToString();
                    }

                    int rowCount = sheet.LastRowNum;

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        XSSFRow row = (XSSFRow)sheet.GetRow(i);
                        DataRow dataRow = table.NewRow();

                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.Cells.Count != 0)
                                {
                                    if (row.GetCell(j) != null)
                                    {
                                        dataRow[j] = row.GetCell(j).ToString();

                                        if (row.GetCell(j).CellType == NPOI.SS.UserModel.CellType.FORMULA)//為公式值
                                        {
                                            XSSFCell cell = (XSSFCell)e.EvaluateInCell(row.GetCell(j));
                                            dataRow[j] = cell.ToString();
                                        }
                                        else//不為公式值
                                        {
                                            dataRow[j] = row.GetCell(j).ToString();
                                        }
                                    }
                                }
                            }
                        }
                        else
                            dataRow[0] = "";

                        table.Rows.Add(dataRow);
                    }

                    ExcelFileStream.Close();
                    workbook = null;
                    sheet = null;
                }

                #endregion
            }

            return table;
        }
        
        //找出檔案路徑的附檔名
        private string SplitExtension(string path)
        {
            string file_extension = "";

            for (int i = path.Length - 1; i > 0; i--)
            {
                if(path.Substring(i,1).Equals("."))
                {
                    file_extension = path.Substring(i, path.Length - i);
                    break;
                }
            }

            return file_extension;
        }

        //Excel檔案檢查
        private void CheckExcel(DataTable Excel_Data)
        {
            //(檢查順序分3大區塊 : Excel版本-->Excel資料正確性-->Excel資料重複)
            string message = "";
            bool flag_version, flag_null_L, flag_null_T, flag_null_U,flag_LOCAT,flag_num,flag_compare, flag_NonDuplicate;
            flag_version = flag_null_L = flag_null_T = flag_null_U = flag_LOCAT = flag_num = flag_compare = flag_NonDuplicate = true;
            string LOCAT,TOTAL,USED,UNUSE;
            LOCAT = TOTAL = USED = UNUSE = "";

            #region 檢查Excel版本(By 欄位數,及欄位名稱)

            flag_version = true;
            string[] a1 = { "LOCATION", "TOTAL", "USED" };//正確欄位名稱

            if (Excel_Data.Columns.Count == a1.Length)//檢查欄位數量
            {
                //檢查欄位名稱
                for (int i = 0; i < a1.Length; i++)
                {
                    //檢查到不符合的欄位名稱
                    if (!Excel_Data.Columns[i].ColumnName.ToString().Equals(a1[i].ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        flag_version = false;
                        message = "Excel Version Incorrect!(column name)";
                        break;
                    }
                }
            }
            else
            {
                flag_version = false;
                message = "Excel Version Incorrect!(column number)";
            }
                
            #endregion

            #region 檢查Excel資料正確性

            if (flag_version)//Excel版本正確
            {
                //從WHHED取出儲位清單
                objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks_M, strLgort_M);
                DataTable dtLOCAT = objStorageData.QueryStorageStatus6();
                ArrayList dataFormat = new ArrayList();//紀錄資料錯誤的種類({RowIndex,LOCAT,是否空值,LOCAT是否存在,T/U是否數字,T>U})

                for (int i = 0; i < Excel_Data.Rows.Count; i++)
                {
                    #region 檢查欄位是否都有值(LOCAT,TOTAL,USED)

                    LOCAT = TOTAL = USED = UNUSE = "";
                    flag_null_L = flag_null_T = flag_null_U =true;
                    if (Excel_Data.Rows[i]["LOCATION"] != null)
                    {
                        if(!Excel_Data.Rows[i]["LOCATION"].ToString().Trim().Equals(""))
                            LOCAT = Excel_Data.Rows[i]["LOCATION"].ToString();
                        else
                            flag_null_L = false;
                    }
                    else
                        flag_null_L = false;

                    if (Excel_Data.Rows[i]["TOTAL"] != null)
                    {
                        if (!Excel_Data.Rows[i]["TOTAL"].ToString().Trim().Equals(""))
                            TOTAL = Excel_Data.Rows[i]["TOTAL"].ToString();
                        else
                            flag_null_T = false;
                    }
                    else
                        flag_null_T = false;

                    if (Excel_Data.Rows[i]["USED"] != null)
                    {
                        if (!Excel_Data.Rows[i]["USED"].ToString().Trim().Equals(""))
                            USED = Excel_Data.Rows[i]["USED"].ToString();
                        else
                            flag_null_U = false;
                    }
                    else
                        flag_null_U = false;

                    if (!(flag_null_L && flag_null_T && flag_null_U))
                    {
                        ArrayList temp = new ArrayList { (i + 2).ToString(), LOCAT, "N", "Y", "Y", "Y" };
                        dataFormat.Add(temp);
                    }

                    #endregion

                    #region 檢查LOCAT是否存在

                    flag_LOCAT = false;
                    if (flag_null_L)//Location欄有值
                    {
                        for (int j = 0; j < dtLOCAT.Rows.Count; j++)
                        {
                            if (dtLOCAT.Rows[j]["LOCAT"].ToString().Equals(LOCAT, StringComparison.CurrentCultureIgnoreCase))
                            {
                                flag_LOCAT = true;
                                break;
                            }
                        }

                        if (!flag_LOCAT)//沒有找到對應的儲位
                        {
                            if (dataFormat.Count != 0)//從未倒到dataFormat
                            {
                                ArrayList temp1 = (ArrayList)dataFormat[dataFormat.Count - 1];//取出目前最尾端的location資訊

                                if (Convert.ToInt32(temp1[0]) == i + 2)//目前儲位已被紀錄過dataFormat error
                                {
                                    temp1[3] = "N";
                                }
                                else
                                {
                                    ArrayList temp2 = new ArrayList { (i + 2).ToString(), LOCAT, "Y", "N", "Y", "Y" };
                                    dataFormat.Add(temp2);
                                }
                            }
                            else
                            {
                                ArrayList temp2 = new ArrayList { (i + 2).ToString(), LOCAT, "Y", "N", "Y", "Y" };
                                dataFormat.Add(temp2);
                            }
                        }
                    }

                    #endregion

                    #region 檢查TOTAL,USED是否為數字

                    flag_num = true;
                    if (flag_null_T)//TOTAL不為空值
                    {
                        if (!CheckNum(TOTAL))
                            flag_num = false;                       
                    }

                    if (flag_null_U)//USED不為空值
                    {
                        if (!CheckNum(USED))
                            flag_num = false;
                    }

                    if (!flag_num)//數值檢查有誤
                    {
                        if (dataFormat.Count != 0)//從未倒到dataFormat
                        {
                            ArrayList temp1 = (ArrayList)dataFormat[dataFormat.Count - 1];//取出目前最尾端的location資訊

                            if (Convert.ToInt32(temp1[0]) == i + 2)//目前儲位已被紀錄過dataFormat error
                            {
                                temp1[4] = "N";
                            }
                            else
                            {
                                ArrayList temp2 = new ArrayList { (i + 2).ToString(), LOCAT, "Y", "Y", "N", "Y" };
                                dataFormat.Add(temp2);
                            }
                        }
                        else
                        {
                            ArrayList temp2 = new ArrayList { (i + 2).ToString(), LOCAT, "Y", "Y", "N", "Y" };
                            dataFormat.Add(temp2);
                        }
                    }


                    #endregion

                    #region 檢查TOTAL是否大於USED

                    if (flag_null_T && flag_null_U &&flag_num)//數值檢查通過
                    {
                        flag_compare = true;
                        if (Convert.ToInt32(TOTAL) < Convert.ToInt32(USED))
                            flag_compare = false;

                        if (!flag_compare)//數值比較檢查有誤
                        {
                            if (dataFormat.Count != 0)//從未倒到dataFormat
                            {
                                ArrayList temp1 = (ArrayList)dataFormat[dataFormat.Count - 1];//取出目前最尾端的location資訊

                                if (Convert.ToInt32(temp1[0]) == i + 2)//目前儲位已被紀錄過dataFormat error
                                {
                                    temp1[5] = "N";
                                }
                                else
                                {
                                    ArrayList temp2 = new ArrayList { (i + 2).ToString(), LOCAT, "Y", "Y", "Y", "N" };
                                    dataFormat.Add(temp2);
                                }
                            }
                            else
                            {
                                ArrayList temp2 = new ArrayList { (i + 2).ToString(), LOCAT, "Y", "Y", "Y", "N" };
                                dataFormat.Add(temp2);
                            }
                        }
                    }
                    
                    #endregion
                }

                if(dataFormat.Count == 0)//Excel資料正確性檢查通過
                {
                    #region 檢查是否有重複的儲位資料

                flag_NonDuplicate = true;
                ArrayList duplicate = new ArrayList();//紀錄重複的record({RowIndex,LOCAT,duplicate rowindex})
                bool[] duplicate_checked = new bool[Excel_Data.Rows.Count];//紀錄已被檢查過的row

                for (int i = 0; i < duplicate_checked.Length; i++)//初始化
                    duplicate_checked[i] = false;

                for (int i = 0; i < Excel_Data.Rows.Count; i++)
                {
                    if (!duplicate_checked[i])//沒被檢查過
                    {
                        LOCAT = Excel_Data.Rows[i]["LOCATION"].ToString();

                        for (int j = i + 1; j < Excel_Data.Rows.Count; j++)
                        {
                            if (LOCAT.Equals(Excel_Data.Rows[j]["LOCATION"].ToString(), StringComparison.CurrentCultureIgnoreCase))//找到重複的儲位名稱
                            {                                
                                if (duplicate.Count != 0)//從未倒到duplicate
                                {
                                    ArrayList temp1 = (ArrayList)duplicate[duplicate.Count - 1];//取出目前最尾端的location資訊

                                    if (Convert.ToInt32(temp1[0]) == i + 2)//目前儲位已被紀錄過duplicate
                                    {
                                        temp1.Add((j + 2).ToString());
                                    }
                                    else//目前儲位首次被記錄duplicate
                                    {
                                        ArrayList temp2 = new ArrayList { (i + 2).ToString(), LOCAT, (j + 2).ToString() };
                                        duplicate.Add(temp2);
                                    }
                                }
                                else
                                {
                                    ArrayList temp2 = new ArrayList { (i + 2).ToString(), LOCAT, (j + 2).ToString() };
                                    duplicate.Add(temp2);
                                }

                                flag_NonDuplicate = false;
                                duplicate_checked[j] = true;
                            }
                        }

                        duplicate_checked[i] = true;
                    }
                }

                if (!flag_NonDuplicate)//Excel有重複的儲位資料
                {
                    for (int i = 0; i < duplicate.Count; i++)//將重複列arraylist轉換成文字
                    {
                        ArrayList temp = (ArrayList)duplicate[i];

                        message = message + "Excel Rows " + temp[0].ToString() + ",";

                        for (int j = 2; j < temp.Count; j++)
                        {
                            if (!(j == temp.Count - 1))//非最後一列                                       
                                message = message + temp[j].ToString() + ",";
                            else//最後一列                                                              
                                message = message + temp[j].ToString() + " Location Duplicate!(Location : " + temp[1].ToString() + ")" + "\n";
                        }
                    }
                }

                #endregion
                }
                else
                {
                    for (int i = 0; i < dataFormat.Count; i++)//將資料錯誤列arraylist轉換成文字
                    {
                        ArrayList temp = (ArrayList)dataFormat[i];
                        message = message + "Excel Rows " + temp[0].ToString() + " : ";

                        bool flag_first = true;
                        for (int j = 2; j < temp.Count; j++)
                        {
                            //{RowIndex,LOCAT,是否空值,LOCAT是否存在,T/U是否數字,T>U}
                            if (j == 2)
                            {
                                if (temp[j].Equals("N"))//空值
                                {
                                    if (flag_first)
                                    {
                                        message = message + "Value Empty";
                                        flag_first = false;
                                    }
                                    else
                                    {
                                        message = message + ",Value Empty" ;
                                        flag_first = false;
                                    }
                                }
                            }

                            if (j == 3)
                            {
                                if (temp[j].Equals("N"))//LOCAT不存在
                                {
                                    if (flag_first)
                                    {
                                        message = message + "Location Not Exist";
                                        flag_first = false;
                                    }
                                    else
                                    {
                                        message = message + ",Location Not Exist";
                                        flag_first = false;
                                    }
                                }
                            }

                            if (j == 4)
                            {
                                if (temp[j].Equals("N"))//T,U不為數字
                                {
                                    if (flag_first)
                                    {
                                        message = message + "Total or Used Value Incorrect";
                                        flag_first = false;
                                    }
                                    else
                                    {
                                        message = message + ",Total or Used Value Incorrect";
                                        flag_first = false;
                                    }
                                }
                            }

                            if (j == 5)
                            {
                                if (temp[j].Equals("N"))//T<U
                                {
                                    if (flag_first)
                                    {
                                        message = message + "Total < Used";
                                        flag_first = false;
                                    }
                                    else
                                    {
                                        message = message + ",Total < Used";
                                        flag_first = false;
                                    }

                                }

                                if (temp[1].ToString().Trim().Equals(""))
                                    message = message + "!"+"\n";
                                else
                                    message = message + "!(Location : " + temp[1].ToString() + ")" + "\n";                              
                            }

                        }
                    }
                }               
            }
          
            #endregion

            if(!message.Trim().Equals(""))//有錯誤
            {
                MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }              
            else//全部檢查通過
            {
                #region DB資料寫入

                //從WHPAL取出清單
                objStorageData = new StorageData(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, UserData, strWerks_M, strLgort_M);
                DataTable dtPAL = objStorageData.QueryStorageStatus8();

                for (int i = 0; i < Excel_Data.Rows.Count; i++)
                {
                    LOCAT = Excel_Data.Rows[i]["LOCATION"].ToString();
                    TOTAL = Excel_Data.Rows[i]["TOTAL"].ToString();
                    USED = Excel_Data.Rows[i]["USED"].ToString();
                    UNUSE = (Convert.ToInt32(TOTAL) - Convert.ToInt32(USED)).ToString();

                    bool flag = false;
                    for (int j = 0; j < dtPAL.Rows.Count; j++)
                    {
                        if (LOCAT.Equals(dtPAL.Rows[j]["LOCAT"]))//在WHPAL找到相符儲位
                        {
                            //update WHPAL
                            objStorageData.Update_WHPAL(LOCAT, Convert.ToInt32(TOTAL), Convert.ToInt32(USED), Convert.ToInt32(UNUSE), 0, "update", false, true);
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)//在WHPAL沒有找到相符儲位
                    {
                        //insert WHPAL
                        objStorageData.Update_WHPAL(LOCAT, Convert.ToInt32(TOTAL), Convert.ToInt32(USED), Convert.ToInt32(UNUSE), 0, "insert", false, true);
                    }
                }

                #endregion

                MessageBox.Show("Excel Data Import Successful", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        //檢查是否為數字
        private bool CheckNum(string num)
        {
            string[] a1 = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            bool[] flag = new bool[num.Length];

            for (int i = 0; i < flag.Length; i++)
                flag[i] = false;

            for (int i = 0; i < num.Length; i++)
            {
                string num_spilt = num.Substring(i, 1);
                for (int j = 0; j < a1.Length; j++)
                {
                    if (a1[j].Equals(num_spilt))
                    {
                        flag[i] = true;
                        break;
                    }
                }
            }

            bool flag_sum = true;

            for (int i = 0; i < flag.Length; i++)
                flag_sum = flag_sum && flag[i];

            if (flag_sum)
                return true;
            else
                return false;
        }

        #region Interop
        /*
            //string path = System.IO.Directory.GetCurrentDirectory();
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            Workbook wb = null;
            Worksheet ws = null;
            //Range aRange = null;
            object mObj_opt = System.Reflection.Missing.Value;
            int Count_Row;
            
            //啟動Excel應用程式
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.DisplayAlerts = false;

            //用Excel應用程式建立一個Excel物件
            wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            ws = (Worksheet)wb.Worksheets[1];

            string[] Column = {"DATE","APTYPE","USRID","ACTNO","RGROUP","LAST_DATE","UPDATED_DATE","UPDATED_USER","co_used" };//Compare_Result欄位

            for (int i = 0; i < Column.Length; i++)
            {
                ws.Cells[1, i + 1] = Column[i];
            }

            Count_Row = 2;
            foreach (DataRow r1 in dt_Result.Rows)
            {
                ws.Cells[Count_Row,1] = r1[0];//DATE
                ws.Cells[Count_Row,2] = r1[1];//APTYPE
                ws.Cells[Count_Row,3] = r1[2];//USRID
                ws.Cells[Count_Row,4] = r1[3];//ACTNO
                ws.Cells[Count_Row,5] = r1[4];//RGROUP
                ws.Cells[Count_Row,6] = r1[5];//LAST_DATE
                ws.Cells[Count_Row,7] = r1[6];//UPDATED_DATE
                ws.Cells[Count_Row,8] = r1[7];//UPDATED_USER
                ws.Cells[Count_Row,9] = r1[8];//co_used

                Count_Row++;
            }

            //儲存Excel檔
            wb.SaveAs(@"C:\帳號比對結果.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet, mObj_opt, mObj_opt, mObj_opt, mObj_opt, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, mObj_opt, mObj_opt, mObj_opt, mObj_opt, mObj_opt);
            Console.WriteLine("Excel寫入完成..");

            wb.Close(false, mObj_opt, mObj_opt);
            xlApp.Workbooks.Close();
            xlApp.Quit();

            //刪除 Excel.exe Process  
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(aRange);
            xlApp = null;
            wb = null;
            ws = null;
            //aRange = null;

            //Garbege Collection 
            GC.Collect();
            */

        #endregion

        #endregion

        #region 调整視窗大小

        private void Manage_StorageStatus_Resize(object sender, EventArgs e)
        {
            if (N1.Visible)
            {
                if (flag_numberic)
                    rule_calculate();
                else
                    N1.Visible = false;
            }

            //********
            //gvData.Height = Convert.ToInt32(((double)this.Size.Height * (0.36)));
            //gvData2.Height = Convert.ToInt32(((double)this.Size.Height * (0.36)));
            //this.gvData2.Location = new System.Drawing.Point(177, Convert.ToInt32(((double)this.Size.Height * (0.24))));

            //this.gvData2.Location = new System.Drawing.Point(177, gvData.Location.Y+gvData.Height+30);
            //this.label6.Location = new System.Drawing.Point(177, gvData.Location.Y + gvData.Height + 10);

            //panel2.Height = Convert.ToInt32(((double)this.Size.Height * (0.5)));
            //********

            //if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
            //stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;


            //gvData2.Height = Convert.ToInt32(((double)this.Size.Height * (0.4)));
            //panel4.Height = Convert.ToInt32(((double)this.Size.Height * (0.5)));

            //gvData.Height = this.Size.Height * 2 / 5;
            //panel2.Height = (this.Size.Height * 2 / 5) + (this.Size.Height * 1 / 6);

            //gvData2.Height = this.Size.Height * 2 / 5;
            //panel4.Top = (this.Size.Height * 2 / 5) + (this.Size.Height * 1 / 6);
            //panel4.Height = (this.Size.Height * 2 / 5) + (this.Size.Height * 1 / 6);
            //gvData.Top = Height / 50;//控件距离界面上边缘始终为界面高度的1/3；
            //gvData.Width = Width * 2/ 3;//控件宽度为界面的1/2；    
            //gvData2.Height = Height * 1 / 2;//控件高度为界面的2/3；            
            //gvData.Left = Width / 50;/*控件左边框据界面左边框的距离是界面宽度的1/2；*/
            //gvData.Left = 10;

        }

        #endregion

        #region 過濾條件

        //Maintain選項,Location名稱過濾條件
        private void Maintain_Location_Filter()
        {
            string L_Name = "";

            L_Name = txtLocat.Text.Trim().ToString();

            //根據所選擇條件,帶出datagridview
            DataTable dtMaintain = new DataTable();//暫存table

            //dtMaintain schema build
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                dtMaintain.Columns.Add();
                dtMaintain.Columns[i].ColumnName = dtData.Columns[i].ColumnName;
            }

            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                if (dtData.Rows[i]["LOCAT"].ToString().Equals(L_Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    dtMaintain.Rows.Add();
                    for (int j = 0; j < dtData.Columns.Count; j++)
                        dtMaintain.Rows[dtMaintain.Rows.Count - 1][j] = dtData.Rows[i][j];
                    break;
                }
            }

            ShowDataGridView(dtMaintain);
            
        }

        //Maintain選項,Unuse Pallets過濾條件
        private void Maintain_Unuse_Filter()
        {
            string option = "";//>,<,=
            int unuse = 0;

            option = cmbOption1.SelectedItem.ToString();
            unuse = Convert.ToInt32(N2.Value.ToString());

            //根據所選擇條件,帶出datagridview
            DataTable dtMaintain = new DataTable();//暫存table

            //dtMaintain schema build
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                dtMaintain.Columns.Add();
                dtMaintain.Columns[i].ColumnName = dtData.Columns[i].ColumnName;
            }

            switch (option)
            {
                case ">":
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (dtData.Rows[i]["UNUSE"] != null)
                        {
                            if (!dtData.Rows[i]["UNUSE"].ToString().Trim().Equals(""))
                            {
                                if (Convert.ToInt32(dtData.Rows[i]["UNUSE"].ToString()) > unuse)//符合條件
                                {
                                    dtMaintain.Rows.Add();
                                    for (int j = 0; j < dtData.Columns.Count; j++)
                                        dtMaintain.Rows[dtMaintain.Rows.Count - 1][j] = dtData.Rows[i][j];
                                }
                            }
                        }
                    }
                    break;
                case "=":
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (dtData.Rows[i]["UNUSE"] != null)
                        {
                            if (!dtData.Rows[i]["UNUSE"].ToString().Trim().Equals(""))
                            {
                                if (Convert.ToInt32(dtData.Rows[i]["UNUSE"].ToString()) == unuse)//符合條件
                                {
                                    dtMaintain.Rows.Add();
                                    for (int j = 0; j < dtData.Columns.Count; j++)
                                        dtMaintain.Rows[dtMaintain.Rows.Count - 1][j] = dtData.Rows[i][j];
                                }
                            }
                        }
                    }
                    break;
                case "<":
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (dtData.Rows[i]["UNUSE"] != null)
                        {
                            if (!dtData.Rows[i]["UNUSE"].ToString().Trim().Equals(""))
                            {
                                if (Convert.ToInt32(dtData.Rows[i]["UNUSE"].ToString()) < unuse)//符合條件
                                {
                                    dtMaintain.Rows.Add();
                                    for (int j = 0; j < dtData.Columns.Count; j++)
                                        dtMaintain.Rows[dtMaintain.Rows.Count - 1][j] = dtData.Rows[i][j];
                                }
                            }
                        }
                    }
                    break;
            }

            ShowDataGridView(dtMaintain);
        }

        //Query選項,使用率過濾條件
        private void Query_Usage_Filter()
        {
            string option = "";//>,<,=
            int rate = 0;

            option = cmbOption2.SelectedItem.ToString();
            rate = Convert.ToInt32(N3.Value);

            //根據所選擇條件,帶出datagridview
            DataTable dtQuery = new DataTable();//暫存table

            //dtQuery schema build
            for (int i = 0; i < dtData2.Columns.Count; i++)
            {
                dtQuery.Columns.Add();
                dtQuery.Columns[i].ColumnName = dtData2.Columns[i].ColumnName;
            }
               
            switch (option)
            {
                case ">":
                    for (int i = 0; i < dtData2.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtData2.Rows[i]["USAGE RATE"].ToString().Replace("%", "")) > rate)//符合條件
                        {
                            dtQuery.Rows.Add();
                            for (int j = 0; j < dtData2.Columns.Count; j++)
                                dtQuery.Rows[dtQuery.Rows.Count-1][j] = dtData2.Rows[i][j];
                        }
                    }
                    break;
                case "=":
                    for (int i = 0; i < dtData2.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtData2.Rows[i]["USAGE RATE"].ToString().Replace("%", "")) == rate)//符合條件
                        {
                            dtQuery.Rows.Add();
                            for (int j = 0; j < dtData2.Columns.Count; j++)
                                dtQuery.Rows[dtQuery.Rows.Count - 1][j] = dtData2.Rows[i][j];
                        }
                    }
                    break;
                case "<":
                    for (int i = 0; i < dtData2.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtData2.Rows[i]["USAGE RATE"].ToString().Replace("%", "")) < rate)//符合條件
                        {
                            dtQuery.Rows.Add();
                            for (int j = 0; j < dtData2.Columns.Count; j++)
                                dtQuery.Rows[dtQuery.Rows.Count - 1][j] = dtData2.Rows[i][j];
                        }
                    }
                    break;
            }

            gvData2.DataSource = dtQuery;
        }

        #endregion
    }
}
