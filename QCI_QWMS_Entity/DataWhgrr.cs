using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhgrr 針對 WHGRR Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhgrr : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhgrr物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhgrr物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhgrr objWhgrr = new DataWhgrr();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhgrr(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhgrr物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhgrr物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhgrr objWhgrr = new DataWhgrr(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhgrr(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhgrr";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHGRR";

            SetFields();
        }

        #endregion
        #endregion

        #region Grrno
        //  <summary>
        // Grrno
        //  <summary>
        public string Grrno
        {
            get
            {
                if (htFields["GRRNO"] == null)
                {
                    return null;
                }
                return htFields["GRRNO"].ToString();
            }
            set
            {
                htFields["GRRNO"] = value;
            }
        }
        #endregion

        #region Itemnum
        //  <summary>
        // Itemnum
        //  <summary>
        public string Itemnum
        {
            get
            {
                if (htFields["ITEMNUM"] == null)
                {
                    return null;
                }
                return htFields["ITEMNUM"].ToString();
            }
            set
            {
                htFields["ITEMNUM"] = value;
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

        #region Zeile
        //  <summary>
        // Zeile
        //  <summary>
        public string Zeile
        {
            get
            {
                if (htFields["ZEILE"] == null)
                {
                    return null;
                }
                return htFields["ZEILE"].ToString();
            }
            set
            {
                htFields["ZEILE"] = value;
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

        #region Bkqty
        //  <summary>
        // Bkqty
        //  <summary>
        public string Bkqty
        {
            get
            {
                if (htFields["BKQTY"] == null)
                {
                    return null;
                }
                return htFields["BKQTY"].ToString();
            }
            set
            {
                htFields["BKQTY"] = value;
            }
        }
        #endregion

        #region Alqty
        //  <summary>
        // Alqty
        //  <summary>
        public string Alqty
        {
            get
            {
                if (htFields["ALQTY"] == null)
                {
                    return null;
                }
                return htFields["ALQTY"].ToString();
            }
            set
            {
                htFields["ALQTY"] = value;
            }
        }
        #endregion

        #region Blace
        //  <summary>
        // Blace
        //  <summary>
        public string Blace
        {
            get
            {
                if (htFields["BLACE"] == null)
                {
                    return null;
                }
                return htFields["BLACE"].ToString();
            }
            set
            {
                htFields["BLACE"] = value;
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

        #region Barcode
        //  <summary>
        // Barcode
        //  <summary>
        public string Barcode
        {
            get
            {
                if (htFields["BARCODE"] == null)
                {
                    return null;
                }
                return htFields["BARCODE"].ToString();
            }
            set
            {
                htFields["BARCODE"] = value;
            }
        }
        #endregion

        #region Prtyp
        //  <summary>
        // Prtyp
        //  <summary>
        public string Prtyp
        {
            get
            {
                if (htFields["PRTYP"] == null)
                {
                    return null;
                }
                return htFields["PRTYP"].ToString();
            }
            set
            {
                htFields["PRTYP"] = value;
            }
        }
        #endregion

        #region Ctrlnm
        //  <summary>
        // Ctrlnm
        //  <summary>
        public string Ctrlnm
        {
            get
            {
                if (htFields["CTRLNM"] == null)
                {
                    return null;
                }
                return htFields["CTRLNM"].ToString();
            }
            set
            {
                htFields["CTRLNM"] = value;
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

        #region Ombln
        //  <summary>
        // Omblnr
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

        #region Rndqty
        //  <summary>
        // Rndqty
        //  <summary>
        public string Rndqty
        {
            get
            {
                if (htFields["RNDQTY"] == null)
                {
                    return null;
                }
                return htFields["RNDQTY"].ToString();
            }
            set
            {
                htFields["RNDQTY"] = value;
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
            htFields.Add("GRRNO", null);
            htFields.Add("ITEMNUM", null);
            htFields.Add("MANDT", null);
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("LOCAT", null);
            htFields.Add("MATNR", null);
            htFields.Add("MBLNR", null);
            htFields.Add("ZEILE", null);
            htFields.Add("INSMK", null);
            htFields.Add("CHARG", null);
            htFields.Add("LIFNR", null);
            htFields.Add("EBELN", null);
            htFields.Add("TRNTP", null);
            htFields.Add("INDAT", null);
            htFields.Add("KDMAT", null);
            htFields.Add("BKQTY", null);
            htFields.Add("ALQTY", null);
            htFields.Add("BLACE", null);
            htFields.Add("SERNO", null);
            htFields.Add("LOCOD", null);
            htFields.Add("INSPT", null);
            htFields.Add("BARCODE", null);
            htFields.Add("PRTYP", null);
            htFields.Add("CTRLNM", null);
            htFields.Add("DACOD", null);
            htFields.Add("VEDAT", null);
            htFields.Add("OMBLN", null);
            htFields.Add("RNDQTY", null);
            htFields.Add("GRPID", null);
            htFields.Add("KOSTL", null);

            htFieldType.Add("GRRNO", DBDataType.DBString);
            htFieldType.Add("ITEMNUM", DBDataType.DBNumber);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("ZEILE", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("EBELN", DBDataType.DBString);
            htFieldType.Add("TRNTP", DBDataType.DBString);
            htFieldType.Add("INDAT", DBDataType.DBString);
            htFieldType.Add("KDMAT", DBDataType.DBString);
            htFieldType.Add("BKQTY", DBDataType.DBNumber);
            htFieldType.Add("ALQTY", DBDataType.DBNumber);
            htFieldType.Add("BLACE", DBDataType.DBNumber);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
            htFieldType.Add("INSPT", DBDataType.DBString);
            htFieldType.Add("BARCODE", DBDataType.DBString);
            htFieldType.Add("PRTYP", DBDataType.DBNumber);
            htFieldType.Add("CTRLNM", DBDataType.DBString);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("VEDAT", DBDataType.DBString);
            htFieldType.Add("OMBLN", DBDataType.DBString);
            htFieldType.Add("RNDQTY", DBDataType.DBNumber);
            htFieldType.Add("GRPID", DBDataType.DBString);
            htFieldType.Add("KOSTL", DBDataType.DBString);
        }
        #endregion
    }
}
