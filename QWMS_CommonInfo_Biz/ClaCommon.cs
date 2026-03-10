using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QWMS.Common
{
    public class ClaCommon
    {

        #region DataTable转化成xml
        /// <summary>
        /// DataTable转化成xml
        /// </summary>
        /// <param name="xmlDS"></param>
        /// <returns></returns>
        public string ConvertDataTableToXML(DataTable dtData)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;
            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.Default);
                dtData.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim().Replace("DocumentElement", "XML");
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        #endregion

        #region 加一个checkbox控件跟datagridview组合来实现全选反选功能

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

        #region 确认是否为数字
        public bool IsNumber(string input)
        {
            string pattern = "^[0-9]*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        #endregion

        #region 判断日期是否有效
        public static bool CheckDateValid(string strDate)
        {
            bool bolIsValid = false;

            //采用正则表达式校验日期
            Regex reg = new Regex(@"^(\d{4})(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])$");

            if (!reg.IsMatch(strDate))
            {
                bolIsValid = false;
            }
            else
            {
                string strDateFormat = "yyyyMMdd";
                //System.IFormatProvider format = new System.Globalization.CultureInfo("zh-CN", true);
                DateTime result;
                bolIsValid = DateTime.TryParseExact(strDate, strDateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result);
            }

            return bolIsValid;
        }
        #endregion


        #region 判断日期是否有效
        public static bool CheckDateValid(string strDate, string strDateFormat)
        {
            bool bolIsValid = false;

            //System.IFormatProvider format = new System.Globalization.CultureInfo("zh-CN", true);
            DateTime result;
            bolIsValid = DateTime.TryParseExact(strDate, strDateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result);

            return bolIsValid;
        }
        #endregion

        #region 账号加密
        public static string AesEncryptToHexadecimal(string encryptString)
        {
            byte[] Keys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
            string KeysAES = "1234ecdf5678ghjk";
            if (string.IsNullOrEmpty(encryptString)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(encryptString);
            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(KeysAES),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };
            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return ConvertBytesToHexadecimalString(resultArray);
        }
        public static string ConvertBytesToHexadecimalString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        #endregion

        #region 调用域验证api
        public async Task<bool> CheckAccount(string hostName, string strPsw)
        {
            ClaHttpHelper objHttpHelper = new ClaHttpHelper();
            string result = string.Empty;
            try
            {
                string strClienrID = "EC";
                string strUserName = AesEncryptToHexadecimal(hostName);
                string strPassword = AesEncryptToHexadecimal(strPsw);

                string strUrl = $"https://oaap.quantacn.com/oams/webapi/Api/Pass/CheckDomainUserLogin";
                var strUserInfo = new
                {
                    ClientID = strClienrID,
                    UserName = strUserName,
                    Password = strPassword,
                    Domain = "QUANTACN"
                };
                string strPostData = JsonConvert.SerializeObject(strUserInfo);
                string strResult = await objHttpHelper.HttpPostAsync(strUrl, strPostData, "application/json");

                JObject obj = JObject.Parse(strResult);
                return obj["result"].ToString().ToUpper().Equals("TRUE");
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}