using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhmat 針對 WHMAT Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhmat : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhmat物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhmat物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhmat objWhmat = new DataWhmat();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhmat(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhmat物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhmat物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhmat objWhmat = new DataWhmat(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhmat(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhmat";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHMAT";

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

        #region Soldto
        //  <summary>
        // Soldto
        //  <summary>
        public string Soldto
        {
            get
            {
                if (htFields["SOLDTO"] == null)
                {
                    return null;
                }
                return htFields["SOLDTO"].ToString();
            }
            set
            {
                htFields["SOLDTO"] = value;
            }
        }
        #endregion

        #region Mapid
        //  <summary>
        // Mapid
        //  <summary>
        public string Mapid
        {
            get
            {
                if (htFields["MAPID"] == null)
                {
                    return null;
                }
                return htFields["MAPID"].ToString();
            }
            set
            {
                htFields["MAPID"] = value;
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
            htFields.Add("SOLDTO", null);
            htFields.Add("MAPID", null);
            htFields.Add("MATNR", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("SOLDTO", DBDataType.DBString);
            htFieldType.Add("MAPID", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
        }
        #endregion
    }
}
