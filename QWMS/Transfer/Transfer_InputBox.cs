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
    public partial class Transfer_InputBox : Form
    {
        private static string strData = "";
        private static bool boUpper = false;//是否限制输入结果为大写
        private static bool boTrim = true;//是否去掉空格
        private Transfer_InputBox()
        {
            InitializeComponent();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (boTrim)
            {
                strData = txtData.Text.ToString().Trim();
            }
            else
            {
                strData = txtData.Text.ToString();
            }
            this.Close();
        }

        private void txtData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (boTrim)
                {
                    strData = txtData.Text.ToString().Trim();
                }
                else
                {
                    strData = txtData.Text.ToString();
                }
                this.Close();
            }
            if (boUpper && e.KeyChar >= 97 && e.KeyChar <= 122)
            {
                e.KeyChar = (char)((int)e.KeyChar - 32);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            strData = string.Empty;
            this.Close();
        }

        /// <summary>
        /// 显示InputBox
        /// </summary>
        /// <param name="strTitle">边框text</param>
        /// <param name="strInfo"></param>
        /// <param name="strPassword">暗文显示字符</param>
        /// <param name="Upper">是否限制大写</param>
        /// <param name="trim">结果是否保留空格</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static string ShowInputBox(string strTitle, string strInfo,string strPassword="",bool Upper=false,bool trim=true,int X=0,int Y=0)
        {
            Transfer_InputBox inputbox = new Transfer_InputBox();
            inputbox.Text = strTitle;
            inputbox.lblInfo.Text = strInfo;
            inputbox.Location = new Point(X, Y);
            if (strPassword != "")
            {
                inputbox.txtData.PasswordChar = strPassword[0];
            }
            boUpper = Upper;
            boTrim = trim;
            inputbox.ShowDialog();

            return strData;
        }











    }
}
