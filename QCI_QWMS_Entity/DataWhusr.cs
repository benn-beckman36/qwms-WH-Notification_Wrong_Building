using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhusr 針對 WHUSR Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhusr : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhusr物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhusr物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhusr objWhusr = new DataWhusr();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhusr(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhusr物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhusr物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhusr objWhusr = new DataWhusr(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhusr(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhusr";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHUSR";

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

        #region Usrnm
        //  <summary>
        // Usrnm
        //  <summary>
        public string Usrnm
        {
            get
            {
                if (htFields["USRNM"] == null)
                {
                    return null;
                }
                return htFields["USRNM"].ToString();
            }
            set
            {
                htFields["USRNM"] = value;
            }
        }
        #endregion

        #region Paswd
        //  <summary>
        // Paswd
        //  <summary>
        public string Paswd
        {
            get
            {
                if (htFields["PASWD"] == null)
                {
                    return null;
                }
                return htFields["PASWD"].ToString();
            }
            set
            {
                htFields["PASWD"] = value;
            }
        }
        #endregion

        #region Wkaut
        //  <summary>
        // Wkaut
        //  <summary>
        public string Wkaut
        {
            get
            {
                if (htFields["WKAUT"] == null)
                {
                    return null;
                }
                return htFields["WKAUT"].ToString();
            }
            set
            {
                htFields["WKAUT"] = value;
            }
        }
        #endregion

        #region Maaut
        //  <summary>
        // Maaut
        //  <summary>
        public string Maaut
        {
            get
            {
                if (htFields["MAAUT"] == null)
                {
                    return null;
                }
                return htFields["MAAUT"].ToString();
            }
            set
            {
                htFields["MAAUT"] = value;
            }
        }
        #endregion

        #region Inaut
        //  <summary>
        // Inaut
        //  <summary>
        public string Inaut
        {
            get
            {
                if (htFields["INAUT"] == null)
                {
                    return null;
                }
                return htFields["INAUT"].ToString();
            }
            set
            {
                htFields["INAUT"] = value;
            }
        }
        #endregion

        #region Otaut
        //  <summary>
        // Otaut
        //  <summary>
        public string Otaut
        {
            get
            {
                if (htFields["OTAUT"] == null)
                {
                    return null;
                }
                return htFields["OTAUT"].ToString();
            }
            set
            {
                htFields["OTAUT"] = value;
            }
        }
        #endregion

        #region Cgaut
        //  <summary>
        // Cgaut
        //  <summary>
        public string Cgaut
        {
            get
            {
                if (htFields["CGAUT"] == null)
                {
                    return null;
                }
                return htFields["CGAUT"].ToString();
            }
            set
            {
                htFields["CGAUT"] = value;
            }
        }
        #endregion

        #region Ivaut
        //  <summary>
        // Ivaut
        //  <summary>
        public string Ivaut
        {
            get
            {
                if (htFields["IVAUT"] == null)
                {
                    return null;
                }
                return htFields["IVAUT"].ToString();
            }
            set
            {
                htFields["IVAUT"] = value;
            }
        }
        #endregion

        #region Mgaut
        //  <summary>
        // Mgaut
        //  <summary>
        public string Mgaut
        {
            get
            {
                if (htFields["MGAUT"] == null)
                {
                    return null;
                }
                return htFields["MGAUT"].ToString();
            }
            set
            {
                htFields["MGAUT"] = value;
            }
        }
        #endregion

        #region Repln
        //  <summary>
        // Repln
        //  <summary>
        public string Repln
        {
            get
            {
                if (htFields["REPLN"] == null)
                {
                    return null;
                }
                return htFields["REPLN"].ToString();
            }
            set
            {
                htFields["REPLN"] = value;
            }
        }
        #endregion

        #region Isadm
        //  <summary>
        // Isadm
        //  <summary>
        public string Isadm
        {
            get
            {
                if (htFields["ISADM"] == null)
                {
                    return null;
                }
                return htFields["ISADM"].ToString();
            }
            set
            {
                htFields["ISADM"] = value;
            }
        }
        #endregion

        #region Logtm
        //  <summary>
        // Logtm
        //  <summary>
        public string Logtm
        {
            get
            {
                if (htFields["LOGTM"] == null)
                {
                    return null;
                }
                return htFields["LOGTM"].ToString();
            }
            set
            {
                htFields["LOGTM"] = value;
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
            htFields.Add("COMCD", null);
            htFields.Add("USRNM", null);
            htFields.Add("PASWD", null);
            htFields.Add("WKAUT", null);
            htFields.Add("MAAUT", null);
            htFields.Add("INAUT", null);
            htFields.Add("OTAUT", null);
            htFields.Add("CGAUT", null);
            htFields.Add("IVAUT", null);
            htFields.Add("MGAUT", null);
            htFields.Add("REPLN", null);
            htFields.Add("ISADM", null);
            htFields.Add("LOGTM", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("PASWDDAT", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("USRNM", DBDataType.DBString);
            htFieldType.Add("PASWD", DBDataType.DBString);
            htFieldType.Add("WKAUT", DBDataType.DBString);
            htFieldType.Add("MAAUT", DBDataType.DBString);
            htFieldType.Add("INAUT", DBDataType.DBString);
            htFieldType.Add("OTAUT", DBDataType.DBString);
            htFieldType.Add("CGAUT", DBDataType.DBString);
            htFieldType.Add("IVAUT", DBDataType.DBString);
            htFieldType.Add("MGAUT", DBDataType.DBString);
            htFieldType.Add("REPLN", DBDataType.DBString);
            htFieldType.Add("ISADM", DBDataType.DBString);
            htFieldType.Add("LOGTM", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("PASWDDAT", DBDataType.DBCreateDate);
        }
        #endregion

        #region Paswddat
        //  <summary>
        // Paswddat
        //  <summary>
        public string Paswddat
        {
            get
            {
                if (htFields["PASWDDAT"] == null)
                {
                    return null;
                }
                return htFields["PASWDDAT"].ToString();
            }
            set
            {
                htFields["PASWDDAT"] = value;
            }
        }
        #endregion
    }
}
