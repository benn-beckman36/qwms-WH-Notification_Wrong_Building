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
    public partial class ECForm : Form
    {
        UserInfo UserData = new UserInfo();
        private string strMandt = string.Empty;
        private string strComcd = string.Empty;
        private string strUsrnm = string.Empty;
        private string strWerks = string.Empty;
        private string strProgid = string.Empty;
        UserInfo UserDataEC = new UserInfo();

        QCI.QWMS.Admin objAdmin;

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

        public ECForm(UserInfo varUserData)
        {
            InitializeComponent();
            UserData = varUserData;
            Mandt = varUserData.Client;
            Comcd = varUserData.CompanyCode;
            Usrnm = varUserData.UserId;
            Progid = strProgid;

            try
            {
                objAdmin = new Admin(UserData, Progid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void txtUsrnm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    #region 工号权限判断
                    if (txtUsrnm.Text.Trim() == "")
                    {
                        return;
                    }
                    else
                    {

                        DataTable dtHR = new DataTable();
                        DataTable dtEmployeeData = new DataTable();
                        Admin objAdmin = new Admin(UserData, Progid);
                        try
                        {
                            dtHR = objAdmin.CheckHR(txtUsrnm.Text.Trim());
                        }
                        catch
                        {
                            dtEmployeeData = objAdmin.GetEmployeeData(txtUsrnm.Text.Trim());
                            if (dtEmployeeData.Rows.Count > 0)
                            {
                                dtHR = dtEmployeeData;
                            }
                        }
                        if (dtHR != null && dtHR.Rows.Count > 0)
                        {
                            if (dtHR.Rows[0]["onjobs"].ToString() != "1" || dtHR.Rows[0]["onjobs"].ToString() == "离职")
                            {
                                MessageBox.Show("此员工未在职，请确认!!");
                                return;
                            }
                            else
                            {
                                #region 判断USRNM是否和EC单厂区一致
                                DataTable dtUser = objAdmin.PermissionQuery(Mandt, Comcd, strWerks, txtUsrnm.Text.ToString());
                                if (dtUser.Rows.Count == 0)
                                {
                                    MessageBox.Show("使用者没有该EC单厂区操作权限，请先维护权限，再作业，谢谢！！！");
                                    return;
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            MessageBox.Show("HR无工号：" + txtUsrnm.Text.Trim() + "的资料,验证失败");
                            return;
                        }
                    }
                    #endregion

                    #region 不用
                    //QWMS.EmployeeData.QueryEmployeeData objHR = new EmployeeData.QueryEmployeeData();
                    //DataSet ds = objHR.GetEmployeeDataByComcod("QSMC", txtUsrnm.Text.ToString());
                    //DataTable dtHR = new DataTable();
                    //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    dtHR = ds.Tables[0];
                    //}
                    ////dtHR = objBorrowMateria.GetHR(UserID.Trim());
                    //if (dtHR.Rows.Count > 0&&dtHR.Rows[0]["onjobs"].ToString() == "离职")
                    //{
                    //    MessageBox.Show("此员工已离职，请确认!!");
                    //    return;
                    //}
                    //else
                    //{
                    //    #region 判断USRNM是否和EC单厂区一致
                    //    DataTable dtUser = objAdmin.PermissionQuery(Mandt, Comcd, strWerks, txtUsrnm.Text.ToString());
                    //    if (dtUser.Rows.Count == 0)
                    //    {
                    //        MessageBox.Show("使用者没有该EC单厂区操作权限，请先维护权限，再作业，谢谢！！！");
                    //        return;
                    //    }
                    //}
                    #endregion

                    UserDataEC.UserId = txtUsrnm.Text.ToString().Trim();
                    UserDataEC.Client = Mandt;
                    UserDataEC.CompanyCode = Comcd;
                    UserDataEC.ClientIP = UserData.ClientIP;
                    StorageIn_Intelligent objStorageIn_Intelligent = new StorageIn_Intelligent(ref UserDataEC, "B27");
                    objStorageIn_Intelligent.MdiParent = this.MdiParent;
                    this.Close();
                    objStorageIn_Intelligent.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
