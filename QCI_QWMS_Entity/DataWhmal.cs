using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;




namespace QWMS.Entity
{


    //  <summary>
    // DataWhmal 針對 WHMAL Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhmal : EntityBase
    {

        
        #region Constructor
        #region 不傳入任何參數產生DataWhmal物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhmal物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhmal objWhmal = new DataWhmal();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhmal(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhmal物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhmal物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhmal objWhmal = new DataWhmal(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhmal(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhmal";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHMAL";

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

        #region Funct
        //  <summary>
        // Funct
        //  <summary>
        public string Funct
        {
            get
            {
                if (htFields["FUNCT"] == null)
                {
                    return null;
                }
                return htFields["FUNCT"].ToString();
            }
            set
            {
                htFields["FUNCT"] = value;
            }
        }
        #endregion

        #region Mtype
        //  <summary>
        // Mtype
        //  <summary>
        public string Mtype
        {
            get
            {
                if (htFields["MTYPE"] == null)
                {
                    return null;
                }
                return htFields["MTYPE"].ToString();
            }
            set
            {
                htFields["MTYPE"] = value;
            }
        }
        #endregion

        #region Email
        //  <summary>
        // Email
        //  <summary>
        public string Email
        {
            get
            {
                if (htFields["EMAIL"] == null)
                {
                    return null;
                }
                return htFields["EMAIL"].ToString();
            }
            set
            {
                htFields["EMAIL"] = value;
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

        #region Comcd
        //  <summary>
        // Comcd
        //  <summary>
        public string Comcd
        {
            get
            {
                if (htFields["COMCD"] == null)
                {
                    return null;
                }
                return htFields["COMCD"].ToString();
            }
            set
            {
                htFields["COMCD"] = value;
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
            htFields.Add("FUNCT", null);
            htFields.Add("MTYPE", null);
            htFields.Add("EMAIL", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("COMCD", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("FUNCT", DBDataType.DBString);
            htFieldType.Add("MTYPE", DBDataType.DBString);
            htFieldType.Add("EMAIL", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("COMCD", DBDataType.DBNumber);
        }
        #endregion
    }
}
