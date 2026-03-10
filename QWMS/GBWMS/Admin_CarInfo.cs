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
using System.Web.UI.WebControls;
using System.Drawing.Imaging;


namespace QWMS
{
    public partial class Admin_CarInfo : Form
    {
        #region 声明变量
        UserInfo UserData = new UserInfo();
        private string strMandt = "";
        private string strComcd = "";
        private string strUsrnm = "";
        private string strProgid = "";
        private string strType = "";
        private string strCarNo = "";
        private string strDriverID = "";
        private string strDriver = "";
        private string strTel = "";
        private DataTable dtData = new DataTable();
        private Admin objAdmin;
        private CarData objCarData ;
        private Authority objAuthority;
        public  int intRowNo;
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
        public string Type
        {
            get
            {
                return strType;
            }
            set
            {
                strType = value;
            }
        }
        public string StrCarNo
        {
            get
            {
                return strCarNo;
            }
            set
            {
                strCarNo = value;
            }
        }
        public string StrDriverID
        {
            get
            {
                return strDriverID;
            }
            set
            {
                strDriverID = value;
            }
        }
        public string StrTel
        {
            get
            {
                return strTel;
            }
            set
            {
                strTel = value;
            }
        }
        public string StrDriver
        {
            get
            {
                return strDriver;
            }
            set
            {
                strDriver = value;
            }
        }
        string strImgPath;
        string strJobID;
        #endregion
      
        #region 构造函数
        public Admin_CarInfo(UserInfo varUserData, string varProgid)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = UserData.Client;
            Comcd = UserData.CompanyCode;
            Usrnm = UserData.UserId;
            Progid = varProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
                objCarData = new CarData(UserData);
                objAuthority = new Authority(UserData);
                if (!objAdmin.CheckAuthority())
                {
                    throw new Exception("You don't have right to use this program!!");
                }
                else 
                {
                    //秀出Status的資料
                    ShowStatusData();          
                    dtData = objCarData.QueryCarInfo("", "", "", "","");
                    initialControl();
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
            this.stsMandt.Text = Mandt;
            this.stsUsrnm.Text = Usrnm;
            this.stsComcd.Text = Comcd;

        }
        # endregion

        #region 初始化控件

        public void initialControl()
        {

            txtCarNo.Text = "";
            txtDriver.Text = "";
            txtDriverID.Text = "";
            txtTel.Text = "";
            txtJobID.Text = "";
            this.rdoDelete.Checked = false;
            this.rdoAdd.Checked = false;
            this.rdoModify.Checked = false;
            this.txtJobID.Enabled = true;
            this.btnUpload.Enabled = false;
            this.pbDriver.Image = null;
            this.strImgPath = "";
            pbDriver.ImageLocation = "";
            
            gbFunction.Enabled = true;
            ShowDataGridView();
            stsWarning.Text = "";
            strType = "";
           

        }

        #endregion

        #region ShowDataGridView
        private void 
            ShowDataGridView()
        {
            try
            {
                this.gvData.AutoGenerateColumns = false;
                this.gvData.Columns.Clear();

                DataGridViewTextBoxColumn dgvcCarNo = new DataGridViewTextBoxColumn();
                dgvcCarNo.DataPropertyName = "CARNO";
                dgvcCarNo.HeaderText = "车牌号";
                dgvcCarNo.ReadOnly = true;
                dgvcCarNo.Width = 90;
                this.gvData.Columns.Add(dgvcCarNo);

                DataGridViewTextBoxColumn dgvcDriver = new DataGridViewTextBoxColumn();
                dgvcDriver.DataPropertyName = "DRINAME";
                dgvcDriver.HeaderText = "司机";
                dgvcDriver.ReadOnly = true;
                dgvcDriver.Width = 60;
                this.gvData.Columns.Add(dgvcDriver);

                DataGridViewTextBoxColumn dgvcJobID = new DataGridViewTextBoxColumn();
                dgvcJobID.DataPropertyName = "JOBID";
                dgvcJobID.HeaderText = "司机工号";
                dgvcJobID.ReadOnly = true;
                dgvcJobID.Width = 100;
                this.gvData.Columns.Add(dgvcJobID);

                DataGridViewTextBoxColumn dgvcDriverTel = new DataGridViewTextBoxColumn();
                dgvcDriverTel.DataPropertyName = "TEL";
                dgvcDriverTel.HeaderText = "司机手机号";
                dgvcDriverTel.ReadOnly = true;
                dgvcDriverTel.Width = 120;
                this.gvData.Columns.Add(dgvcDriverTel);

                DataGridViewTextBoxColumn dgvcDriverID = new DataGridViewTextBoxColumn();
                dgvcDriverID.DataPropertyName = "DRID";
                dgvcDriverID.HeaderText = "司机身份证号";
                dgvcDriverID.ReadOnly = true;
                dgvcDriverID.Width = 150;
                this.gvData.Columns.Add(dgvcDriverID);

                this.gvData.DataSource = dtData;

                if (dtData.Rows.Count > 0)
                {
                    gvData.Rows[0].Selected = false;
                    gvData.AllowUserToAddRows = false;
                }
                else
                {
                    stsWarning.Text = "No Data!";
                }

            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "<-ShowDataGridView()");
            }
            
        }
        #endregion

        #region 调整布局大小
        private void Admin_CarInfo_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60 > 0)
                stsWarning.Width = this.Size.Width - stsMandt.Width - this.stsComcd.Width - this.stsUsrnm.Width - this.stsDate.Width - 60;
        }
        #endregion

        #region  Confirm
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            stsWarning.Text= "";
            strCarNo = txtCarNo.Text.Trim();
            strDriver = txtDriver.Text.Trim();
            strDriverID = txtDriverID.Text.Trim();
            strTel = txtTel.Text.Trim();
            strJobID = txtJobID.Text.Trim();
            
            strImgPath = pbDriver.ImageLocation;
             try
            {
                if (strType == "")
                { 
                    dtData = objCarData.QueryCarInfo(strCarNo, strDriver, strDriverID, strTel,strJobID);
                    ShowDataGridView();
                    return;
                }
                if (strType == "DELETE" && strCarNo!="")
                {
                   
                    strJobID =dtData.Rows[intRowNo]["JOBID"].ToString();
                    strImgPath = dtData.Rows[intRowNo]["IMGPATH"].ToString();
                    bool flg = objCarData.DeleteCar(strJobID);
                    FileInfo file = new FileInfo(strImgPath);
                    file.Delete();

                    dtData = objCarData.QueryCarInfo("","","","","");
                    initialControl();
                    if (flg)
                    {
                        stsWarning.Text = "Delete Success!";
                    }
                    else
                    {
                        stsWarning.Text = "Delete Fail !";
                    }
                }
                if (strType == "ADD" || strType == "MODIFY")
                {
                    this.btnUpload.Enabled = true;
                    strImgPath = pbDriver.ImageLocation;
                    if (strCarNo == "" || strDriver == "" || strDriverID == "" || strTel == "" || string.IsNullOrEmpty(strImgPath)||strJobID=="")
                    {
                        stsWarning.Text = "车牌号、司机、司机身份证、手机号、司机照片、司机工号不能为空！";
                        return;
                    }
                    if (strDriverID.Length != 18)
                    {
                        stsWarning.Text = "身份证号长度必须为18位";
                        return;
                    }
                    if (strType == "ADD")
                    {
                        if (!objCarData.CheckExist(strJobID,strCarNo))   //判断司机工号以及车牌号是否已经存在
                        {
                            bool flg = objCarData.AddCar(strDriver, strCarNo, strDriverID, strTel, strImgPath,strJobID);
                            dtData = objCarData.QueryCarInfo("", "", "", "","");
                            initialControl();
                            if (flg)
                            {
                                stsWarning.Text = "Add Success";
                            }
                            else
                            {
                                stsWarning.Text = "Add Fail";
                            }
                        }
                        else
                        {
                            stsWarning.Text = "Add Fail, This car has existed now ,Please don't add again ! ";
                        }
                    }
                    if (strType == "MODIFY" && strJobID != "")
                    {
                        bool flg = objCarData.ModifyCar(strCarNo, strDriver, strDriverID, strTel, strImgPath,strJobID);
                        dtData = objCarData.QueryCarInfo("", "", "", "","");
                        initialControl();
                        if (flg)
                        {
                            stsWarning.Text = "Update Success !";
                        }
                        else
                        {
                            stsWarning.Text = "Update Fail !";
                        }
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

        #region Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.btnConfirm.Enabled = true;
                dtData = objCarData.QueryCarInfo("", "", "", "","");
                initialControl();
            }
            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
                return;
            }

        }
         #endregion

        #region Radio Changed
        private void rdoDelete_CheckedChanged(object sender, EventArgs e)
        {
            this.panel4.Enabled = true;
            this.gbFunction.Enabled = false;
            this.strType = "DELETE";
            this.gvData.Enabled = true;
        }

        private void rdoAdd_CheckedChanged_1(object sender, EventArgs e)
        {
            this.panel4.Enabled = true;
            this.gbFunction.Enabled = false;
            this.strType = "ADD";
            this.gvData.Enabled = true;
            this.btnUpload.Enabled = true;
        }

        private void rdoModify_CheckedChanged_1(object sender, EventArgs e)
        {
            this.panel4.Enabled = true;
            this.gbFunction.Enabled = false;
            this.strType = "MODIFY";
            this.gvData.Enabled = true;
            this.btnUpload.Enabled = true;
            this.txtJobID.Enabled = false;
        }
#endregion

        #region gvData Mouse Down Event
        private void gvData_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
               
                stsWarning.Text = "";
                DataGridView dgvClick = (DataGridView)sender;
                DataGridView.HitTestInfo hitRow;
                hitRow = dgvClick.HitTest(e.X, e.Y);
                intRowNo = hitRow.RowIndex;
                if (strType == "MODIFY" || strType == "DELETE"||strType=="")
                {
                    txtCarNo.Text = dtData.Rows[intRowNo]["CARNO"].ToString();
                    txtDriver.Text = dtData.Rows[intRowNo]["DRINAME"].ToString();
                    txtDriverID.Text = dtData.Rows[intRowNo]["DRID"].ToString();
                    txtTel.Text = dtData.Rows[intRowNo]["TEL"].ToString();
                    txtJobID.Text = dtData.Rows[intRowNo]["JOBID"].ToString();
                    txtJobID.Enabled = false;
                    pbDriver.ImageLocation = dtData.Rows[intRowNo]["IMGPATH"].ToString();
                }

            }

            catch (Exception ex)
            {
                stsWarning.Text = ex.Message;
            }
        }
#endregion

        private void btnUpload_Click(object sender, EventArgs e)
        {
             OpenFileDialog imgFile = new OpenFileDialog();
             imgFile.Title = "请选择要上传的照片";
             imgFile.Multiselect = false;

             if (imgFile.ShowDialog() == DialogResult.OK)
             {
                 string LocalPath = imgFile.FileName;
                 FileInfo fi = new FileInfo(LocalPath);
                 string imgName = fi.Name;
                 string type=fi.Extension;
                 if (type == ".jpg" || type == ".gif" || type == ".bmp" || type == ".png")
                 {
                     //string Path = @"\\Csmcec4\GBWMSPIC\" + imgName;
                     string SavePath = @"\\Csmcec4\GBWMSPIC\" + imgName;
                     FileInfo file = new FileInfo(SavePath);

                     if (! file.Exists)
                     {
                         File.Copy(LocalPath, SavePath);
                         //GetReducedImage(100, 120, SavePath);
                         pbDriver.ImageLocation = SavePath;
                     }
                     else
                     {
                         stsWarning.Text = "该照片已存在，请确认！";
                     }
                     
                 }
                 else
                 {
                     strImgPath = "";
                     stsWarning.Text = "请选择正确的图片格式：jpg、gif、bmp、png";
                 }
            }

        }

    
    }
}
