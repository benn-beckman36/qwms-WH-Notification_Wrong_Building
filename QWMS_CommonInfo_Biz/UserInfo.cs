using System;
using System.Collections.Generic;
using System.Text;


namespace QWMS.Common
{
    public class UserInfo
    {


        #region Constructor
        public UserInfo()
        {

        }
        #endregion

        #region DataMember


        #region UserID
        private string strUserId = "";
        /// <summary>
        /// User ID
        /// </summary>
        public string UserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }
        #endregion        

        #region Client
        private string strClient = "";
        /// <summary>
        /// Client
        /// </summary>
        public string Client
        {
            get { return strClient; }
            set { strClient = value; }
        }
        #endregion

        #region CompanyCode
        private string strComcd = "";
        /// <summary>
        /// Company Code
        /// </summary>
        public string CompanyCode
        {
            get { return strComcd; }
            set { strComcd = value; }
        }
        #endregion

        #region Permession
        private List<string> lstPermission = new List<string>();
        /// <summary>
        /// User Permission
        /// </summary>
        public List<string> Permession
        {
            get { return lstPermission; }
            set { lstPermission = value; }
        }
        #endregion

        #region ClientIP
        private string strClientIP = "";
        /// <summary>
        /// Client IP
        /// </summary>
        public string ClientIP
        {
            get { return strClientIP; }
            set { strClientIP = value; }
        }
        #endregion

        #region ServerIP
        private string strServerIP = "";
        /// <summary>
        /// Server IP
        /// </summary>
        public string ServerIP
        {
            get { return strServerIP; }
            set { strServerIP = value; }
        }
        #endregion

        #region Domain
        private string strDomain = "";
        /// <summary>
        /// Domain
        /// </summary>
        public string Domain
        {
            get { return strDomain; }
            set { strDomain = value; }
        }
        #endregion

        #region QuantaAccount
        private string strQuantaAccount = "";
        /// <summary>
        /// Domain
        /// </summary>
        public string QuantaAccount
        {
            get { return strQuantaAccount; }
            set { strQuantaAccount = value; }
        }
        #endregion

        #region DeptNo
        private string strDeptNo = "";
        /// <summary>
        /// Dept No.
        /// </summary>
        public string DeptNo
        {
            get { return strDeptNo; }
            set { strDeptNo = value; }
        }
        #endregion


        #region Password
        private string strPassword = "";
        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }
        #endregion






        #endregion

    }
}
