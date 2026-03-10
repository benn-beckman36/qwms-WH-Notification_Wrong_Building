using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhdwn 針對 WHDWN Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhdwn : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhdwn物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhdwn物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhdwn objWhdwn = new DataWhdwn();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhdwn(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhdwn物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhdwn物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhdwn objWhdwn = new DataWhdwn(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhdwn(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhdwn";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "WHDWN";

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

        #region Otqty
        //  <summary>
        // Otqty
        //  <summary>
        public string Otqty
        {
            get
            {
                if (htFields["OTQTY"] == null)
                {
                    return null;
                }
                return htFields["OTQTY"].ToString();
            }
            set
            {
                htFields["OTQTY"] = value;
            }
        }
        #endregion

        #region Prcde
        //  <summary>
        // Prcde
        //  <summary>
        public string Prcde
        {
            get
            {
                if (htFields["PRCDE"] == null)
                {
                    return null;
                }
                return htFields["PRCDE"].ToString();
            }
            set
            {
                htFields["PRCDE"] = value;
            }
        }
        #endregion

        #region Putyp
        //  <summary>
        // Putyp
        //  <summary>
        public string Putyp
        {
            get
            {
                if (htFields["PUTYP"] == null)
                {
                    return null;
                }
                return htFields["PUTYP"].ToString();
            }
            set
            {
                htFields["PUTYP"] = value;
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

        #region Reslt
        //  <summary>
        // Reslt
        //  <summary>
        public string Reslt
        {
            get
            {
                if (htFields["RESLT"] == null)
                {
                    return null;
                }
                return htFields["RESLT"].ToString();
            }
            set
            {
                htFields["RESLT"] = value;
            }
        }
        #endregion

        #region Budat
        //  <summary>
        // Budat
        //  <summary>
        public string Budat
        {
            get
            {
                if (htFields["BUDAT"] == null)
                {
                    return null;
                }
                return htFields["BUDAT"].ToString();
            }
            set
            {
                htFields["BUDAT"] = value;
            }
        }
        #endregion

        #region Prity
        //  <summary>
        // Prity
        //  <summary>
        public string Prity
        {
            get
            {
                if (htFields["PRITY"] == null)
                {
                    return null;
                }
                return htFields["PRITY"].ToString();
            }
            set
            {
                htFields["PRITY"] = value;
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

        #region Bwart
        //  <summary>
        // Bwart
        //  <summary>
        public string Bwart
        {
            get
            {
                if (htFields["BWART"] == null)
                {
                    return null;
                }
                return htFields["BWART"].ToString();
            }
            set
            {
                htFields["BWART"] = value;
            }
        }
        #endregion

        #region Umlgo
        //  <summary>
        // Umlgo
        //  <summary>
        public string Umlgo
        {
            get
            {
                if (htFields["UMLGO"] == null)
                {
                    return null;
                }
                return htFields["UMLGO"].ToString();
            }
            set
            {
                htFields["UMLGO"] = value;
            }
        }
        #endregion

        #region Usnam
        //  <summary>
        // Usnam
        //  <summary>
        public string Usnam
        {
            get
            {
                if (htFields["USNAM"] == null)
                {
                    return null;
                }
                return htFields["USNAM"].ToString();
            }
            set
            {
                htFields["USNAM"] = value;
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

        #region Updat
        //  <summary>
        // Updat
        //  <summary>
        public string Updat
        {
            get
            {
                if (htFields["UPDAT"] == null)
                {
                    return null;
                }
                return htFields["UPDAT"].ToString();
            }
            set
            {
                htFields["UPDAT"] = value;
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

        #region Refid
        //  <summary>
        // Refid
        //  <summary>
        public string Refid
        {
            get
            {
                if (htFields["REFID"] == null)
                {
                    return null;
                }
                return htFields["REFID"].ToString();
            }
            set
            {
                htFields["REFID"] = value;
            }
        }
        #endregion

        #region Bxqty
        //  <summary>
        // Bxqty
        //  <summary>
        public string Bxqty
        {
            get
            {
                if (htFields["BXQTY"] == null)
                {
                    return null;
                }
                return htFields["BXQTY"].ToString();
            }
            set
            {
                htFields["BXQTY"] = value;
            }
        }
        #endregion

        #region Wkord
        //  <summary>
        // Wkord
        //  <summary>
        public string Wkord
        {
            get
            {
                if (htFields["WKORD"] == null)
                {
                    return null;
                }
                return htFields["WKORD"].ToString();
            }
            set
            {
                htFields["WKORD"] = value;
            }
        }
        #endregion

        #region Fmatn
        //  <summary>
        // Fmatn
        //  <summary>
        public string Fmatn
        {
            get
            {
                if (htFields["FMATN"] == null)
                {
                    return null;
                }
                return htFields["FMATN"].ToString();
            }
            set
            {
                htFields["FMATN"] = value;
            }
        }
        #endregion

        #region Ebelp
        //  <summary>
        // Ebelp
        //  <summary>
        public string Ebelp
        {
            get
            {
                if (htFields["EBELP"] == null)
                {
                    return null;
                }
                return htFields["EBELP"].ToString();
            }
            set
            {
                htFields["EBELP"] = value;
            }
        }
        #endregion

        #region Intid
        //  <summary>
        // Intid
        //  <summary>
        public string Intid
        {
            get
            {
                if (htFields["INTID"] == null)
                {
                    return null;
                }
                return htFields["INTID"].ToString();
            }
            set
            {
                htFields["INTID"] = value;
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

        #region Flage
        //  <summary>
        // Flage
        //  <summary>
        public string Flage
        {
            get
            {
                if (htFields["FLAGE"] == null)
                {
                    return null;
                }
                return htFields["FLAGE"].ToString();
            }
            set
            {
                htFields["FLAGE"] = value;
            }
        }
        #endregion

        #region Adflg
        //  <summary>
        // Adflg
        //  <summary>
        public string Adflg
        {
            get
            {
                if (htFields["ADFLG"] == null)
                {
                    return null;
                }
                return htFields["ADFLG"].ToString();
            }
            set
            {
                htFields["ADFLG"] = value;
            }
        }
        #endregion

        #region Remak1
        //  <summary>
        // Remak1
        //  <summary>
        public string Remak1
        {
            get
            {
                if (htFields["Remak1"] == null)
                {
                    return null;
                }
                return htFields["Remak1"].ToString();
            }
            set
            {
                htFields["Remak1"] = value;
            }
        }
        #endregion

        #region PROCESSING
        //  <summary>
        // Remak1
        //  <summary>
        public string PROCESSING
        {
            get
            {
                if (htFields["PROCESSING"] == null)
                {
                    return null;
                }
                return htFields["PROCESSING"].ToString();
            }
            set
            {
                htFields["PROCESSING"] = value;
            }
        }
        #endregion

        #region PROCESSING_START
        //  <summary>
        // Remak1
        //  <summary>
        public string PROCESSING_START
        {
            get
            {
                if (htFields["PROCESSING_START"] == null)
                {
                    return null;
                }
                return htFields["PROCESSING_START"].ToString();
            }
            set
            {
                htFields["PROCESSING_START"] = value;
            }
        }
        #endregion

        #region IOFLAGE
        /// <summary>
        /// IOFLAGE
        /// </summary>
        public string Ioflage
        {
            get
            {
                if (htFields["IOFLAGE"] == null)
                    return null;
                return htFields["IOFLAGE"].ToString();
            }
            set
            {
                htFields["IOFLAGE"] = value;
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
            htFields.Add("MTYPE", null);
            htFields.Add("MBLNR", null);
            htFields.Add("ZEILE", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("MATNR", null);
            htFields.Add("INSMK", null);
            htFields.Add("CHARG", null);
            htFields.Add("LIFNR", null);
            htFields.Add("EBELN", null);
            htFields.Add("MENGE", null);
            htFields.Add("OTQTY", null);
            htFields.Add("PRCDE", null);
            htFields.Add("PUTYP", null);
            htFields.Add("KOSTL", null);
            htFields.Add("RESLT", null);
            htFields.Add("BUDAT", null);
            htFields.Add("PRITY", null);
            htFields.Add("ARBPL", null);
            htFields.Add("TRNTP", null);
            htFields.Add("BWART", null);
            htFields.Add("UMLGO", null);
            htFields.Add("USNAM", null);
            htFields.Add("KDMAT", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MODAT", null);
            htFields.Add("SERNO", null);
            htFields.Add("OMBLN", null);
            htFields.Add("UPDAT", null);
            htFields.Add("INSPT", null);
            htFields.Add("RMANO", null);
            htFields.Add("COMCD", null);
            htFields.Add("REFID", null);
            htFields.Add("BXQTY", null);
            htFields.Add("WKORD", null);
            htFields.Add("FMATN", null);
            htFields.Add("EBELP", null);
            htFields.Add("INTID", null);
            htFields.Add("QWQTY", null);
            htFields.Add("BOXID", null);
            htFields.Add("FLAGE", null);
            htFields.Add("ADFLG", null);
            htFields.Add("REMAK1", null);
            htFields.Add("PROCESSING", null);
            htFields.Add("PROCESSING_START", null);
            htFields.Add("IOFLAGE", null);

            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("MTYPE", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("ZEILE", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("EBELN", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("OTQTY", DBDataType.DBNumber);
            htFieldType.Add("PRCDE", DBDataType.DBString);
            htFieldType.Add("PUTYP", DBDataType.DBString);
            htFieldType.Add("KOSTL", DBDataType.DBString);
            htFieldType.Add("RESLT", DBDataType.DBString);
            htFieldType.Add("BUDAT", DBDataType.DBString);
            htFieldType.Add("PRITY", DBDataType.DBString);
            htFieldType.Add("ARBPL", DBDataType.DBString);
            htFieldType.Add("TRNTP", DBDataType.DBString);
            htFieldType.Add("BWART", DBDataType.DBString);
            htFieldType.Add("UMLGO", DBDataType.DBString);
            htFieldType.Add("USNAM", DBDataType.DBString);
            htFieldType.Add("KDMAT", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("OMBLN", DBDataType.DBString);
            htFieldType.Add("UPDAT", DBDataType.DBCreateDate);
            htFieldType.Add("INSPT", DBDataType.DBString);
            htFieldType.Add("RMANO", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("REFID", DBDataType.DBString);
            htFieldType.Add("BXQTY", DBDataType.DBNumber);
            htFieldType.Add("WKORD", DBDataType.DBString);
            htFieldType.Add("FMATN", DBDataType.DBString);
            htFieldType.Add("EBELP", DBDataType.DBString);
            htFieldType.Add("INTID", DBDataType.DBString);
            htFieldType.Add("QWQTY", DBDataType.DBString);
            htFieldType.Add("BOXID", DBDataType.DBString);
            htFieldType.Add("FLAGE", DBDataType.DBString);
            htFieldType.Add("ADFLG", DBDataType.DBString);
            htFieldType.Add("REMAK1", DBDataType.DBString);
            htFieldType.Add("PROCESSING", DBDataType.DBString);
            htFieldType.Add("PROCESSING_START", DBDataType.DBString);
            htFieldType.Add("IOFLAGE", DBDataType.DBString);
        }
        #endregion
    }
}
