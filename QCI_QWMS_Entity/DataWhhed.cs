using System;
using System.Collections;
using System.Data;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;

namespace QWMS.Entity
{


    //  <summary>
    // DataWhhed 針對 WHHED Table提供Insert,Update,Delete,Query的功能
    //  <summary>
    public class DataWhhed : EntityBase
    {

        #region Constructor
        #region 不傳入任何參數產生DataWhhed物件 by Rock Tzeng
        /// <summary>
        /// 不傳入任何參數產生DataWhhed物件。
        /// </summary>
        /// <example>
        /// <code>
        ///  DataWhhed objWhhed = new DataWhhed();
        ///  Your Code Here......
        /// </code>
        /// </example>
        /////////////////////////////////////////////////////////////////////////////
        public DataWhhed(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region 利用傳入參數產生DataWhhed物件 by Rock Tzeng
        /// <summary>
        /// 利用傳入參數產生DataWhhed物件。
        /// </summary>
        /// <param name="varDBType">DB Type。</param>
        /// <param name="varDBCode">DB Code。</param>
        /// <param name="varErrorType">Error Type。</param>
        /// <param name="varErrorCode">Error Code。</param>
        /// <example>
        /// <code>
        ///  DataWhhed objWhhed = new DataWhhed(1, "TEST", 2, "ERR");
        ///  Your Code Here......
        /// </code>
        /// </example>
        public DataWhhed(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhhed";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Rock Tzeng";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHHED";

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

        #region Losts
        //  <summary>
        // Losts
        //  <summary>
        public string Losts
        {
            get
            {
                if (htFields["LOSTS"] == null)
                {
                    return null;
                }
                return htFields["LOSTS"].ToString();
            }
            set
            {
                htFields["LOSTS"] = value;
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

        #region Stlen
        //  <summary>
        // Stlen
        //  <summary>
        public string Stlen
        {
            get
            {
                if (htFields["STLEN"] == null)
                {
                    return null;
                }
                return htFields["STLEN"].ToString();
            }
            set
            {
                htFields["STLEN"] = value;
            }
        }
        #endregion

        #region Stwid
        //  <summary>
        // Stwid
        //  <summary>
        public string Stwid
        {
            get
            {
                if (htFields["STWID"] == null)
                {
                    return null;
                }
                return htFields["STWID"].ToString();
            }
            set
            {
                htFields["STWID"] = value;
            }
        }
        #endregion

        #region Ismrg
        //  <summary>
        // Ismrg
        //  <summary>
        public string Ismrg
        {
            get
            {
                if (htFields["ISMRG"] == null)
                {
                    return null;
                }
                return htFields["ISMRG"].ToString();
            }
            set
            {
                htFields["ISMRG"] = value;
            }
        }
        #endregion

        #region Isres
        //  <summary>
        // Isres
        //  <summary>
        public string Isres
        {
            get
            {
                if (htFields["ISRES"] == null)
                {
                    return null;
                }
                return htFields["ISRES"].ToString();
            }
            set
            {
                htFields["ISRES"] = value;
            }
        }
        #endregion

        #region Ctbto
        //  <summary>
        // Ctbto
        //  <summary>
        public string Ctbto
        {
            get
            {
                if (htFields["CTBTO"] == null)
                {
                    return null;
                }
                return htFields["CTBTO"].ToString();
            }
            set
            {
                htFields["CTBTO"] = value;
            }
        }
        #endregion

        #region Regon
        //  <summary>
        // Regon
        //  <summary>
        public string Regon
        {
            get
            {
                if (htFields["REGON"] == null)
                {
                    return null;
                }
                return htFields["REGON"].ToString();
            }
            set
            {
                htFields["REGON"] = value;
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

        #region Lsdat
        //  <summary>
        // Lsdat
        //  <summary>
        public string Lsdat
        {
            get
            {
                if (htFields["LSDAT"] == null)
                {
                    return null;
                }
                return htFields["LSDAT"].ToString();
            }
            set
            {
                htFields["LSDAT"] = value;
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

        #region Block
        //  <summary>
        // Block
        //  <summary>
        public string Block
        {
            get
            {
                if (htFields["BLOCK"] == null)
                {
                    return null;
                }
                return htFields["BLOCK"].ToString();
            }
            set
            {
                htFields["BLOCK"] = value;
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

        #region FLOOR
        //  <summary>
        // Comcd
        //  <summary>
        public string FLOOR
        {
            get
            {
                if (htFields["FLOOR"] == null)
                {
                    return null;
                }
                return htFields["FLOOR"].ToString();
            }
            set
            {
                htFields["FLOOR"] = value;
            }
        }
        #endregion

        #region AREA
        //  <summary>
        // Comcd
        //  <summary>
        public string AREA
        {
            get
            {
                if (htFields["AREA"] == null)
                {
                    return null;
                }
                return htFields["AREA"].ToString();
            }
            set
            {
                htFields["AREA"] = value;
            }
        }
        #endregion

        #region TYPE
        //  <summary>
        // Comcd
        //  <summary>
        public string TYPE
        {
            get
            {
                if (htFields["TYPE"] == null)
                {
                    return null;
                }
                return htFields["TYPE"].ToString();
            }
            set
            {
                htFields["TYPE"] = value;
            }
        }
        #endregion

        #region PNLOC
        //  <summary>
        //  Part No Locat
        //  <summary>
        public string PNLOC
        {
            get
            {
                if (htFields["PNLOC"] == null)
                {
                    return null;
                }
                return htFields["PNLOC"].ToString();
            }
            set
            {
                htFields["PNLOC"] = value;
            }
        }
        #endregion

        #region PALQTY
        //  <summary>
        //  滿板數量
        //  <summary>
        public string PALQTY
        {
            get
            {
                if (htFields["PALQTY"] == null)
                {
                    return null;
                }
                return htFields["PALQTY"].ToString();
            }
            set
            {
                htFields["PALQTY"] = value;
            }
        }
        #endregion

        #region MODEL
        //  <summary>
        //  機種
        //  <summary>
        public string MODEL
        {
            get
            {
                if (htFields["MODEL"] == null)
                {
                    return null;
                }
                return htFields["MODEL"].ToString();
            }
            set
            {
                htFields["MODEL"] = value;
            }
        }
        #endregion

        #region VERSION
        //  <summary>
        //  版本
        //  <summary>
        public string VERSION
        {
            get
            {
                if (htFields["VERSION"] == null)
                {
                    return null;
                }
                return htFields["VERSION"].ToString();
            }
            set
            {
                htFields["VERSION"] = value;
            }
        }
        #endregion

        #region ISATL
        public string ISATL
        {
            get
            {
                if (htFields["ISATL"] == null)
                {
                    return null;
                }
                return htFields["ISATL"].ToString();
            }
            set
            {
                htFields["ISATL"] = value;
            }
        }
        #endregion

        #region RIDNO
        public string RIDNO
        {
            get
            {
                if (htFields["RIDNO"] == null)
                {
                    return null;
                }
                return htFields["RIDNO"].ToString();
            }
            set
            {
                htFields["RIDNO"] = value;
            }
        }
        #endregion

        #region PDNAM
        public string PDNAM
        {
            get
            {
                if (htFields["PDNAM"] == null)
                {
                    return null;
                }
                return htFields["PDNAM"].ToString();
            }
            set
            {
                htFields["PDNAM"] = value;
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
            htFields.Add("LOCAT", null);
            htFields.Add("LOSTS", null);
            htFields.Add("STSET", null);
            htFields.Add("STHGH", null);
            htFields.Add("STLEN", null);
            htFields.Add("STWID", null);
            htFields.Add("ISMRG", null);
            htFields.Add("ISRES", null);
            htFields.Add("CTBTO", null);
            htFields.Add("REGON", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("LSDAT", null);
            htFields.Add("MAPID", null);
            htFields.Add("BLOCK", null);
            htFields.Add("COMCD", null);
            htFields.Add("FLOOR", null);
            htFields.Add("AREA", null);
            htFields.Add("TYPE", null);
            //For QCMC AutoRunLocat
            htFields.Add("ISATL", null);
            htFields.Add("RIDNO", null);
            htFields.Add("PDNAM", null);
            //For CSMC JHX Model  Smose Liao 20141027
            htFields.Add("PNLOC", null);
            htFields.Add("PALQTY", null);
            htFields.Add("MODEL", null);
            htFields.Add("VERSION", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("LOSTS", DBDataType.DBString);
            htFieldType.Add("STSET", DBDataType.DBNumber);
            htFieldType.Add("STHGH", DBDataType.DBNumber);
            htFieldType.Add("STLEN", DBDataType.DBNumber);
            htFieldType.Add("STWID", DBDataType.DBNumber);
            htFieldType.Add("ISMRG", DBDataType.DBString);
            htFieldType.Add("ISRES", DBDataType.DBString);
            htFieldType.Add("CTBTO", DBDataType.DBString);
            htFieldType.Add("REGON", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("LSDAT", DBDataType.DBString);
            htFieldType.Add("MAPID", DBDataType.DBString);
            htFieldType.Add("BLOCK", DBDataType.DBNumber);
            htFieldType.Add("COMCD", DBDataType.DBNumber);
            htFieldType.Add("FLOOR", DBDataType.DBString);
            htFieldType.Add("AREA", DBDataType.DBString);
            htFieldType.Add("TYPE", DBDataType.DBString);
            //For QCMC AutoRunLocat
            htFieldType.Add("ISATL", DBDataType.DBString);
            htFieldType.Add("RIDNO", DBDataType.DBString);
            htFieldType.Add("PDNAM", DBDataType.DBString);
            //For CSMC JHX Model  Smose Liao 20141027
            htFieldType.Add("PNLOC", DBDataType.DBString);
            htFieldType.Add("PALQTY", DBDataType.DBNumber);
            htFieldType.Add("MODEL", DBDataType.DBString);
            htFieldType.Add("VERSION", DBDataType.DBString);
        }
        #endregion
    }
}
