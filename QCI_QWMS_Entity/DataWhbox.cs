using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{
    //  <summary>
    // DataWhbox 針對 WHBOX Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhbox : EntityBase
    {
        #region Constructor

        #region 不傳入任何參數產生DataWhbox物件 by Brian Zhao
        /// <summary>
        /// 不傳入任何參數產生DataWhbox物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhbox objWHBOX = new DataWhbox();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhbox(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhbox物件 by Brian Zhao
        /// <summary>
        /// 利用傳入參數產生DataWhbox物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhbox objWHBOX = new DataWhbox(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhbox(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhbox";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Brian Zhao";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHBOX";

            SetFields();
        }

        #endregion

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
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("LOCAT", null); 
            htFields.Add("MBLNR", null);
            htFields.Add("BOXID", null);
            htFields.Add("MATNR", null);
            htFields.Add("SERNO", null);
            htFields.Add("INSMK", null);
            htFields.Add("CHARG", null);
            htFields.Add("MENGE", null);
            htFields.Add("DELFLG", null);
            htFields.Add("DELDAT", null);
            htFields.Add("LOADID", null);
            htFields.Add("KDMAT", null);
            htFields.Add("DELDOC", null);

            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("BOXID", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("DELFLG", DBDataType.DBString);
            htFieldType.Add("DELDAT", DBDataType.DBCreateDate);
            htFieldType.Add("LOADID", DBDataType.DBString);
            htFieldType.Add("KDMAT", DBDataType.DBString);
            htFieldType.Add("DELDOC", DBDataType.DBString);
        }

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

        #region Locat
        //  <summary>
        // Locat
        //  <summary>
        public string Locat
        {
            get
            {
                if (htFields["LOCAT"] == null)
                {
                    return null;
                }
                return htFields["LOCAT"].ToString();
            }
            set
            {
                htFields["LOCAT"] = value;
            }
        }
        #endregion
       
        #region Mblnr
        //  <summary>
        // Mblnr
        //  <summary>
        public string Mblnr
        {
            get
            {
                if (htFields["MBLNR"] == null)
                {
                    return null;
                }
                return htFields["MBLNR"].ToString();
            }
            set
            {
                htFields["MBLNR"] = value;
            }
        }
        #endregion

        #region Boxid
        //  <summary>
        // Box id
        //  <summary>
        public string Boxid
        {
            get
            {
                if (htFields["BOXID"] == null)
                {
                    return null;
                }
                return htFields["BOXID"].ToString();
            }
            set
            {
                htFields["BOXID"] = value;
            }
        }
        #endregion

        #region Matnr
        //  <summary>
        // Matnr
        //  <summary>
        public string Matnr
        {
            get
            {
                if (htFields["MATNR"] == null)
                {
                    return null;
                }
                return htFields["MATNR"].ToString();
            }
            set
            {
                htFields["MATNR"] = value;
            }
        }
        #endregion

        #region Serno
        //  <summary>
        // Serno
        //  <summary>
        public string Serno
        {
            get
            {
                if (htFields["SERNO"] == null)
                {
                    return null;
                }
                return htFields["SERNO"].ToString();
            }
            set
            {
                htFields["SERNO"] = value;
            }
        }
        #endregion

        #region Insmk
        //  <summary>
        // Insmk
        //  <summary>
        public string Insmk
        {
            get
            {
                if (htFields["INSMK"] == null)
                {
                    return null;
                }
                return htFields["INSMK"].ToString();
            }
            set
            {
                htFields["INSMK"] = value;
            }
        }
        #endregion

        #region Charg
        //  <summary>
        // Charg
        //  <summary>
        public string Charg
        {
            get
            {
                if (htFields["CHARG"] == null)
                {
                    return null;
                }
                return htFields["CHARG"].ToString();
            }
            set
            {
                htFields["CHARG"] = value;
            }
        }
        #endregion

        #region Delflg
        //  <summary>
        // Delflg
        //  <summary>
        public string Delflg
        {
            get
            {
                if (htFields["DELFLG"] == null)
                {
                    return null;
                }
                return htFields["DELFLG"].ToString();
            }
            set
            {
                htFields["DELFLG"] = value;
            }
        }
        #endregion

        #region Deldat
        //  <summary>
        // Deldat
        //  <summary>
        public string Deldat
        {
            get
            {
                if (htFields["DELDAT"] == null)
                {
                    return null;
                }
                return htFields["DELDAT"].ToString();
            }
            set
            {
                htFields["DELDAT"] = value;
            }
        }
        #endregion

        #region Menge
        //  <summary>
        // Menge
        //  <summary>
        public string Menge
        {
            get
            {
                if (htFields["MENGE"] == null)
                {
                    return null;
                }
                return htFields["MENGE"].ToString();
            }
            set
            {
                htFields["MENGE"] = value;
            }
        }
        #endregion

        #region Loadid
        //  <summary>
        // Loadid
        //  <summary>
        public string Loadid
        {
            get
            {
                if (htFields["LOADID"] == null)
                {
                    return null;
                }
                return htFields["LOADID"].ToString();
            }
            set
            {
                htFields["LOADID"] = value;
            }
        }
        #endregion

        #region Kdmat
        //  <summary>
        // Kdmat
        //  <summary>
        public string Kdmat
        {
            get
            {
                if (htFields["KDMAT"] == null)
                {
                    return null;
                }
                return htFields["KDMAT"].ToString();
            }
            set
            {
                htFields["KDMAT"] = value;
            }
        }
        #endregion

        #region Deldoc
        //  <summary>
        // Deldoc
        //  <summary>
        public string Deldoc
        {
            get
            {
                if (htFields["DELDOC"] == null)
                {
                    return null;
                }
                return htFields["DELDOC"].ToString();
            }
            set
            {
                htFields["DELDOC"] = value;
            }
        }
        #endregion

        #region Crtdat
        //  <summary>
        // Deldoc
        //  <summary>
        public string Crtdat
        {
            get
            {
                if (htFields["CRTDAT"] == null)
                {
                    return null;
                }
                return htFields["CRTDAT"].ToString();
            }
            set
            {
                htFields["CRTDAT"] = value;
            }
        }
        #endregion
    }
}
