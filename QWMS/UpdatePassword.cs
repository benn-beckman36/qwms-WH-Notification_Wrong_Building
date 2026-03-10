using QCI.QWMS;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QWMS
{
    public partial class UpdatePassword : Form
    {
        UserInfo UserData = new UserInfo();
        public bool bolReturn = false;
        public UpdatePassword(UserInfo userData)
        {
            InitializeComponent();
            UserData = userData;
            txtUsrnm.Text = UserData.UserId;
            txtOldPaswd.Text = UserData.Password;
            txtNewPaswd.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblWarn.Text = "";
            var regex = new Regex(@"
                    (?=.*[0-9])                     
                    (?=.*[a-zA-Z])                  
                    (?=([\x21-\x7e]+)[^a-zA-Z0-9])  
                    .{8,10}$                        
                    ", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            if (txtNewPaswd.Text.Trim() == "" || txtNewPaswd1.Text.Trim() == "")
            {
                lblWarn.Text = "Please input NewPassword!!";
                return;
            }
            if (txtNewPaswd.Text.Trim() != txtNewPaswd1.Text.Trim())
            {
                lblWarn.Text ="NewPassword和Retype Password需一致!!";
                return;
            }
            if (!regex.IsMatch(txtNewPaswd.Text.Trim())) //校验密码是否符合
            {
                MessageBox.Show("新密码复杂度不够，请重新输入新密码（密码长度8-10位，包含数字，英文字母和特殊字符）!!");
                txtNewPaswd.Focus();
                txtNewPaswd.SelectAll();
                return;
            }
            #region 更新密碼
            Authority objAuthority = new Authority(UserData);
            if (objAuthority.UpdateUserPassword(txtNewPaswd.Text.Trim()))
            {
                lblWarn.Text ="Update OK!!";
                bolReturn = true;
                this.Close();
            }
            else
            {
                lblWarn.Text ="Update fail!!" + objAuthority.ERRMSG;
                bolReturn = false;
                return;
            }
            #endregion
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            bolReturn = false;
            this.Close();
        }
    }
}
