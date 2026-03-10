using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhlog 針對 WHLOG Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhlog : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhlog物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhlog物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhlog objWhlog = new DataWhlog();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhlog(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhlog物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhlog物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhlog objWhlog = new DataWhlog(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhlog(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhlog";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHLOG";

            SetFields();
        }

        #endregion
        #endregion

        #region Logid
        //  <summary>
        // Logid
        //  <summary>
        public string Logid
        {
            get
            {
                if (htFields["LOGID"] == null)
                {
                    return null;
                }
                return htFields["LOGID"].ToString();
            }
            set
            {
                htFields["LOGID"] = value;
            }
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

        #region Cgcls
        //  <summary>
        // Cgcls
        //  <summary>
        public string Cgcls
        {
            get
            {
                if (htFields["CGCLS"] == null)
                {
                    return null;
                }
                return htFields["CGCLS"].ToString();
            }
            set
            {
                htFields["CGCLS"] = value;
            }
        }
        #endregion

        #region Oloca
        //  <summary>
        // Oloca
        //  <summary>
        public string Oloca
        {
            get
            {
                if (htFields["OLOCA"] == null)
                {
                    return null;
                }
                return htFields["OLOCA"].ToString();
            }
            set
            {
                htFields["OLOCA"] = value;
            }
        }
        #endregion

        #region Nloca
        //  <summary>
        // Nloca
        //  <summary>
        public string Nloca
        {
            get
            {
                if (htFields["NLOCA"] == null)
                {
                    return null;
                }
                return htFields["NLOCA"].ToString();
            }
            set
            {
                htFields["NLOCA"] = value;
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

        #region Lifnr
        //  <summary>
        // Lifnr
        //  <summary>
        public string Lifnr
        {
            get
            {
                if (htFields["LIFNR"] == null)
                {
                    return null;
                }
                return htFields["LIFNR"].ToString();
            }
            set
            {
                htFields["LIFNR"] = value;
            }
        }
        #endregion

        #region Trntp
        //  <summary>
        // Trntp
        //  <summary>
        public string Trntp
        {
            get
            {
                if (htFields["TRNTP"] == null)
                {
                    return null;
                }
                return htFields["TRNTP"].ToString();
            }
            set
            {
                htFields["TRNTP"] = value;
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

        #region Ombln
        //  <summary>
        // Ombln
        //  <summary>
        public string Ombln
        {
            get
            {
                if (htFields["OMBLN"] == null)
                {
                    return null;
                }
                return htFields["OMBLN"].ToString();
            }
            set
            {
                htFields["OMBLN"] = value;
            }
        }
        #endregion

        #region Ebeln
        //  <summary>
        // Ebeln
        //  <summary>
        public string Ebeln
        {
            get
            {
                if (htFields["EBELN"] == null)
                {
                    return null;
                }
                return htFields["EBELN"].ToString();
            }
            set
            {
                htFields["EBELN"] = value;
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

        #region Kostl
        //  <summary>
        // Kostl
        //  <summary>
        public string Kostl
        {
            get
            {
                if (htFields["KOSTL"] == null)
                {
                    return null;
                }
                return htFields["KOSTL"].ToString();
            }
            set
            {
                htFields["KOSTL"] = value;
            }
        }
        #endregion

        #region Arbpl
        //  <summary>
        // Arbpl
        //  <summary>
        public string Arbpl
        {
            get
            {
                if (htFields["ARBPL"] == null)
                {
                    return null;
                }
                return htFields["ARBPL"].ToString();
            }
            set
            {
                htFields["ARBPL"] = value;
            }
        }
        #endregion

        #region Mrgid
        //  <summary>
        // Mrgid
        //  <summary>
        public string Mrgid
        {
            get
            {
                if (htFields["MRGID"] == null)
                {
                    return null;
                }
                return htFields["MRGID"].ToString();
            }
            set
            {
                htFields["MRGID"] = value;
            }
        }
        #endregion

        #region Indat
        //  <summary>
        // Indat
        //  <summary>
        public string Indat
        {
            get
            {
                if (htFields["INDAT"] == null)
                {
                    return null;
                }
                return htFields["INDAT"].ToString();
            }
            set
            {
                htFields["INDAT"] = value;
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

        #region Rmak1
        //  <summary>
        // Rmak1
        //  <summary>
        public string Rmak1
        {
            get
            {
                if (htFields["RMAK1"] == null)
                {
                    return null;
                }
                return htFields["RMAK1"].ToString();
            }
            set
            {
                htFields["RMAK1"] = value;
            }
        }
        #endregion

        #region Rmak2
        //  <summary>
        // Rmak2
        //  <summary>
        public string Rmak2
        {
            get
            {
                if (htFields["RMAK2"] == null)
                {
                    return null;
                }
                return htFields["RMAK2"].ToString();
            }
            set
            {
                htFields["RMAK2"] = value;
            }
        }
        #endregion

        #region Rmak3
        //  <summary>
        // Rmak3
        //  <summary>
        public string Rmak3
        {
            get
            {
                if (htFields["RMAK3"] == null)
                {
                    return null;
                }
                return htFields["RMAK3"].ToString();
            }
            set
            {
                htFields["RMAK3"] = value;
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

        #region Locod
        //  <summary>
        // Locod
        //  <summary>
        public string Locod
        {
            get
            {
                if (htFields["LOCOD"] == null)
                {
                    return null;
                }
                return htFields["LOCOD"].ToString();
            }
            set
            {
                htFields["LOCOD"] = value;
            }
        }
        #endregion

        #region Inspt
        //  <summary>
        // Inspt
        //  <summary>
        public string Inspt
        {
            get
            {
                if (htFields["INSPT"] == null)
                {
                    return null;
                }
                return htFields["INSPT"].ToString();
            }
            set
            {
                htFields["INSPT"] = value;
            }
        }
        #endregion

        #region Vedat
        //  <summary>
        // Vedat
        //  <summary>
        public string Vedat
        {
            get
            {
                if (htFields["VEDAT"] == null)
                {
                    return null;
                }
                return htFields["VEDAT"].ToString();
            }
            set
            {
                htFields["VEDAT"] = value;
            }
        }
        #endregion

        #region Dacod
        //  <summary>
        // Dacod
        //  <summary>
        public string Dacod
        {
            get
            {
                if (htFields["DACOD"] == null)
                {
                    return null;
                }
                return htFields["DACOD"].ToString();
            }
            set
            {
                htFields["DACOD"] = value;
            }
        }
        #endregion

        #region Boxid
        //  <summary>
        // Boxid
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

        #region Grpid
        //  <summary>
        // Grpid
        //  <summary>
        public string Grpid
        {
            get
            {
                if (htFields["GRPID"] == null)
                {
                    return null;
                }
                return htFields["GRPID"].ToString();
            }
            set
            {
                htFields["GRPID"] = value;
            }
        }
        #endregion

        #region Qwqty
        //  <summary>
        // Qwqty
        //  <summary>
        public string Qwqty
        {
            get
            {
                if (htFields["QWQTY"] == null)
                {
                    return null;
                }
                return htFields["QWQTY"].ToString();
            }
            set
            {
                htFields["QWQTY"] = value;
            }
        }
        #endregion

        #region Rmano
        //  <summary>
        // Rmano
        //  <summary>
        public string Rmano
        {
            get
            {
                if (htFields["RMANO"] == null)
                {
                    return null;
                }
                return htFields["RMANO"].ToString();
            }
            set
            {
                htFields["RMANO"] = value;
            }
        }
        #endregion

        #region EXPDAT
        //  <summary>
        // EXPDAT
        //  <summary>
        public string Expdat {
            get {
                if (htFields["EXPDAT"] == null) {
                    return null;
                }
                return htFields["EXPDAT"].ToString();
            }
            set {
                htFields["EXPDAT"] = value;
            }
        }
        #endregion

        #region TASKID
        /// <summary>
        /// TASKID
        /// </summary>
        public string Taskid {
            get {
                if (htFields["TASKID"] == null) {
                    return null;
                }
                return htFields["TASKID"].ToString();
            }
            set {
                htFields["TASKID"] = value;
            }
        }
        #endregion

        #region MAXEXP
        /// <summary>
        /// 
        /// </summary>
        public string Maxexp {
            get {
                if (htFields["MAXEXP"] == null) {
                    return null;
                }
                return htFields["MAXEXP"].ToString();
            }
            set {
                htFields["MAXEXP"] = value;
            }
        }
        #endregion

        #region Config
        //  <summary>
        // Config
        //  <summary>
        public string Config
        {
            get
            {
                if (htFields["CONFIG"] == null)
                {
                    return null;
                }
                return htFields["CONFIG"].ToString();
            }
            set
            {
                htFields["CONFIG"] = value;
            }
        }
        #endregion

        #region MCDAT
        /// <summary>
        /// 
        /// </summary>
        public string Mcdat
        {
            get
            {
                if (htFields["MCDAT"] == null)
                {
                    return null;
                }
                return htFields["MCDAT"].ToString();
            }
            set
            {
                htFields["MCDAT"] = value;
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
        protected override void SettingFields() {
            htFields.Add("LOGID", null);
            htFields.Add("MANDT", null);
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("CGCLS", null);
            htFields.Add("OLOCA", null);
            htFields.Add("NLOCA", null);
            htFields.Add("MATNR", null);
            htFields.Add("CHARG", null);
            htFields.Add("LIFNR", null);
            htFields.Add("TRNTP", null);
            htFields.Add("MBLNR", null);
            htFields.Add("OMBLN", null);
            htFields.Add("EBELN", null);
            htFields.Add("KDMAT", null);
            htFields.Add("MENGE", null);
            htFields.Add("INSMK", null);
            htFields.Add("KOSTL", null);
            htFields.Add("ARBPL", null);
            htFields.Add("MRGID", null);
            htFields.Add("INDAT", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("RMAK1", null);
            htFields.Add("RMAK2", null);
            htFields.Add("RMAK3", null);
            htFields.Add("SERNO", null);
            htFields.Add("LOCOD", null);
            htFields.Add("INSPT", null);
            htFields.Add("VEDAT", null);
            htFields.Add("DACOD", null);
            htFields.Add("BOXID", null);
            htFields.Add("GRPID", null);
            htFields.Add("QWQTY", null);
            htFields.Add("RMANO", null);
            htFields.Add("EXPDAT",null);
            htFields.Add("TASKID", null);
            htFields.Add("MAXEXP", null);
            htFields.Add("CONFIG", null);
            htFields.Add("MCDAT", null);
            htFieldType.Add("LOGID", DBDataType.DBNumber);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("CGCLS", DBDataType.DBString);
            htFieldType.Add("OLOCA", DBDataType.DBString);
            htFieldType.Add("NLOCA", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("TRNTP", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("OMBLN", DBDataType.DBString);
            htFieldType.Add("EBELN", DBDataType.DBString);
            htFieldType.Add("KDMAT", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("KOSTL", DBDataType.DBString);
            htFieldType.Add("ARBPL", DBDataType.DBString);
            htFieldType.Add("MRGID", DBDataType.DBString);
            htFieldType.Add("INDAT", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("RMAK1", DBDataType.DbunString);
            htFieldType.Add("RMAK2", DBDataType.DbunString);
            htFieldType.Add("RMAK3", DBDataType.DbunString);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
            htFieldType.Add("INSPT", DBDataType.DBString);
            htFieldType.Add("VEDAT", DBDataType.DBString);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("BOXID", DBDataType.DBString);
            htFieldType.Add("GRPID", DBDataType.DBString);
            htFieldType.Add("QWQTY", DBDataType.DBNumber);
            htFieldType.Add("RMANO", DBDataType.DBString);
            htFieldType.Add("EXPDAT", DBDataType.DBString);
            htFieldType.Add("TASKID", DBDataType.DBString);
            htFieldType.Add("MAXEXP", DBDataType.DBString);
            htFieldType.Add("CONFIG", DBDataType.DBString);
            htFieldType.Add("MCDAT", DBDataType.DBString);
        }
        #endregion
    }
}
