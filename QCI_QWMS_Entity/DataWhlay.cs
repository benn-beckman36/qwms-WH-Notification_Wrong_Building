using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;


namespace QWMS.Entity
{


    //  <summary>
    // DataWhlay 針對 WHLAY Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhlay : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhlay物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhlay物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhlay objWhlay = new DataWhlay();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhlay(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhlay物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhlay物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhlay objWhlay = new DataWhlay(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhlay(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhlay";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHLAY";

            SetFields();
        }

        #endregion
        #endregion

        #region Mandt
        //  <summary>
        // Mandt
        //  <summary>
        public string Mandt
        {
            get
            {
                if (htFields["MANDT"] == null)
                {
                    return null;
                }
                return htFields["MANDT"].ToString();
            }
            set
            {
                htFields["MANDT"] = value;
            }
        }
        #endregion

        #region Werks
        //  <summary>
        // Werks
        //  <summary>
        public string Werks
        {
            get
            {
                if (htFields["WERKS"] == null)
                {
                    return null;
                }
                return htFields["WERKS"].ToString();
            }
            set
            {
                htFields["WERKS"] = value;
            }
        }
        #endregion

        #region Lgort
        //  <summary>
        // Lgort
        //  <summary>
        public string Lgort
        {
            get
            {
                if (htFields["LGORT"] == null)
                {
                    return null;
                }
                return htFields["LGORT"].ToString();
            }
            set
            {
                htFields["LGORT"] = value;
            }
        }
        #endregion

        #region Stset
        //  <summary>
        // Stset
        //  <summary>
        public string Stset
        {
            get
            {
                if (htFields["STSET"] == null)
                {
                    return null;
                }
                return htFields["STSET"].ToString();
            }
            set
            {
                htFields["STSET"] = value;
            }
        }
        #endregion

        #region Sthgh
        //  <summary>
        // Sthgh
        //  <summary>
        public string Sthgh
        {
            get
            {
                if (htFields["STHGH"] == null)
                {
                    return null;
                }
                return htFields["STHGH"].ToString();
            }
            set
            {
                htFields["STHGH"] = value;
            }
        }
        #endregion

        #region Rctyp
        //  <summary>
        // Rctyp
        //  <summary>
        public string Rctyp
        {
            get
            {
                if (htFields["RCTYP"] == null)
                {
                    return null;
                }
                return htFields["RCTYP"].ToString();
            }
            set
            {
                htFields["RCTYP"] = value;
            }
        }
        #endregion

        #region Rcnum
        //  <summary>
        // Rcnum
        //  <summary>
        public string Rcnum
        {
            get
            {
                if (htFields["RCNUM"] == null)
                {
                    return null;
                }
                return htFields["RCNUM"].ToString();
            }
            set
            {
                htFields["RCNUM"] = value;
            }
        }
        #endregion

        #region Rcnam
        //  <summary>
        // Rcnam
        //  <summary>
        public string Rcnam
        {
            get
            {
                if (htFields["RCNAM"] == null)
                {
                    return null;
                }
                return htFields["RCNAM"].ToString();
            }
            set
            {
                htFields["RCNAM"] = value;
            }
        }
        #endregion

        #region Crnam
        //  <summary>
        // Crnam
        //  <summary>
        public string Crnam
        {
            get
            {
                if (htFields["CRNAM"] == null)
                {
                    return null;
                }
                return htFields["CRNAM"].ToString();
            }
            set
            {
                htFields["CRNAM"] = value;
            }
        }
        #endregion

        #region Crdat
        //  <summary>
        // Crdat
        //  <summary>
        public string Crdat
        {
            get
            {
                if (htFields["CRDAT"] == null)
                {
                    return null;
                }
                return htFields["CRDAT"].ToString();
            }
            set
            {
                htFields["CRDAT"] = value;
            }
        }
        #endregion

        #region Monam
        //  <summary>
        // Monam
        //  <summary>
        public string Monam
        {
            get
            {
                if (htFields["MONAM"] == null)
                {
                    return null;
                }
                return htFields["MONAM"].ToString();
            }
            set
            {
                htFields["MONAM"] = value;
            }
        }
        #endregion

        #region Modat
        //  <summary>
        // Modat
        //  <summary>
        public string Modat
        {
            get
            {
                if (htFields["MODAT"] == null)
                {
                    return null;
                }
                return htFields["MODAT"].ToString();
            }
            set
            {
                htFields["MODAT"] = value;
            }
        }
        #endregion

        #region DataMember
        UserInfo UserData = new UserInfo();
        //  <summary>
        // 建立htFields,設定所有的欄位並加到 hashtable中
        //  <summary>
        //  <returns>
        // 回傳值型態為void。
        //  <returns>
        protected override void SettingFields()
        {
            htFields.Add("MANDT", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("STSET", null);
            htFields.Add("STHGH", null);
            htFields.Add("RCTYP", null);
            htFields.Add("RCNUM", null);
            htFields.Add("RCNAM", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("STSET", DBDataType.DBNumber);
            htFieldType.Add("STHGH", DBDataType.DBNumber);
            htFieldType.Add("RCTYP", DBDataType.DBString);
            htFieldType.Add("RCNUM", DBDataType.DBNumber);
            htFieldType.Add("RCNAM", DBDataType.DbunString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
        }
        #endregion
    }
}
