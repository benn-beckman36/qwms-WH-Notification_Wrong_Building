using Qci.Base.Common;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Entity
{
    public class DataWhiqc : EntityBase
    {
        #region Constructor
        #region DataWhiqc
        /////////////////////////////////////////////////////////////////////////////
        public DataWhiqc(UserInfo varUserData)
            : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        #endregion

        #region DataWhiqc
        public DataWhiqc(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
        {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhiqc";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHIQC";

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
                    return null;
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
                    return null;
                return htFields["COMCD"].ToString();
            }
            set
            {
                htFields["COMCD"] = value;
            }
        }
        #endregion

        #region Taskid
        //  <summary>
        // Taskid
        //  <summary>
        public string Taskid
        {
            get
            {
                if (htFields["TASKID"] == null)
                    return null;
                return htFields["TASKID"].ToString();
            }
            set
            {
                htFields["TASKID"] = value;
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
                    return null;
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
                    return null;
                return htFields["LGORT"].ToString();
            }
            set
            {
                htFields["LGORT"] = value;
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
                    return null;
                return htFields["INSMK"].ToString();
            }
            set
            {
                htFields["INSMK"] = value;
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
                    return null;
                return htFields["MATNR"].ToString();
            }
            set
            {
                htFields["MATNR"] = value;
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
                    return null;
                return htFields["LIFNR"].ToString();
            }
            set
            {
                htFields["LIFNR"] = value;
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
                    return null;
                return htFields["MENGE"].ToString();
            }
            set
            {
                htFields["MENGE"] = value;
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
                    return null;
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
                    return null;
                return htFields["VEDAT"].ToString();
            }
            set
            {
                htFields["VEDAT"] = value;
            }
        }
        #endregion

        #region Expdat
        //  <summary>
        // Expdat
        //  <summary>
        public string Expdat
        {
            get
            {
                if (htFields["EXPDAT"] == null)
                    return null;
                return htFields["EXPDAT"].ToString();
            }
            set
            {
                htFields["EXPDAT"] = value;
            }
        }
        #endregion

        #region Expdat_after
        //  <summary>
        // Expdat_after
        //  <summary>
        public string Expdat_after
        {
            get
            {
                if (htFields["EXPDAT_AFTER"] == null)
                    return null;
                return htFields["EXPDAT_AFTER"].ToString();
            }
            set
            {
                htFields["EXPDAT_AFTER"] = value;
            }
        }
        #endregion

        #region Maxexp
        //  <summary>
        // Maxexp
        //  <summary>
        public string Maxexp
        {
            get
            {
                if (htFields["MAXEXP"] == null)
                    return null;
                return htFields["MAXEXP"].ToString();
            }
            set
            {
                htFields["MAXEXP"] = value;
            }
        }
        #endregion

        #region Maxexp_after
        /// <summary>
        /// Maxexp_after
        /// </summary>
        public string Maxexp_after
        {
            get
            {
                if (htFields["MAXEXP_AFTER"] == null)
                    return null;
                return htFields["MAXEXP_AFTER"].ToString();
            }
            set
            {
                htFields["MAXEXP_AFTER"] = value;
            }
        }
        #endregion

        #region QMEXP
        //  <summary>
        // QMEXP
        //  <summary>
        public string QMEXP
        {
            get
            {
                if (htFields["QMEXP"] == null)
                    return null;
                return htFields["QMEXP"].ToString();
            }
            set
            {
                htFields["QMEXP"] = value;
            }
        }
        #endregion

        #region Chkqty
        /// <summary>
        /// Chkqty
        /// </summary>
        public string Chkqty
        {
            get
            {
                if (htFields["CHKQTY"] == null)
                    return null;
                return htFields["CHKQTY"].ToString();
            }
            set
            {
                htFields["CHKQTY"] = value;
            }
        }
        #endregion

        #region Result
        //  <summary>
        // Result
        //  <summary>
        public string Result
        {
            get
            {
                if (htFields["RESULT"] == null)
                    return null;
                return htFields["RESULT"].ToString();
            }
            set
            {
                htFields["RESULT"] = value;
            }
        }
        #endregion

        #region Chknam
        /// <summary>
        /// Chknam
        /// </summary>
        public string Chknam
        {
            get
            {
                if (htFields["CHKNAM"] == null)
                    return null;
                return htFields["CHKNAM"].ToString();
            }
            set
            {
                htFields["CHKNAM"] = value;
            }
        }
        #endregion

        #region Chktim
        /// <summary>
        /// Chktim
        /// </summary>
        public string Chktim
        {
            get
            {
                if (htFields["CHKTIM"] == null)
                    return null;
                return htFields["CHKTIM"].ToString();
            }
            set
            {
                htFields["CHKTIM"] = value;
            }
        }
        #endregion

        #region Qcflg
        /// <summary>
        /// Qcflg
        /// </summary>
        public string Qcflg
        {
            get
            {
                if (htFields["QCFLG"] == null)
                    return null;
                return htFields["QCFLG"].ToString();
            }
            set
            {
                htFields["QCFLG"] = value;
            }
        }
        #endregion

        #region Appno
        //  <summary>
        // Appno
        //  <summary>
        public string Appno
        {
            get
            {
                if (htFields["APPNO"] == null)
                {
                    return null;
                }
                return htFields["APPNO"].ToString();
            }
            set
            {
                htFields["APPNO"] = value;
            }
        }
        #endregion

        #region Mrbno
        /// <summary>
        /// Mrbno
        /// </summary>
        public string Mrbno
        {
            get
            {
                if (htFields["MRBNO"] == null)
                    return null;
                return htFields["MRBNO"].ToString();
            }
            set
            {
                htFields["MRBNO"] = value;
            }
        }
        #endregion

        #region Exptp
        /// <summary>
        /// Exptp
        /// </summary>
        public string Exptp
        {
            get
            {
                if (htFields["EXPTP"] == null)
                    return null;
                return htFields["EXPTP"].ToString();
            }
            set
            {
                htFields["EXPTP"] = value;
            }
        }
        #endregion

        #region Prtid
        /// <summary>
        /// Prtid
        /// </summary>
        public string Prtid
        {
            get
            {
                if (htFields["PRTID"] == null)
                    return null;
                return htFields["PRTID"].ToString();
            }
            set
            {
                htFields["PRTID"] = value;
            }
        }
        #endregion

        #region Ifprt
        /// <summary>
        /// Ifprt
        /// </summary>
        public string Ifprt
        {
            get
            {
                if (htFields["IFPRT"] == null)
                    return null;
                return htFields["IFPRT"].ToString();
            }
            set
            {
                htFields["IFPRT"] = value;
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

        #region Rmak4
        //  <summary>
        // Rmak4
        //  <summary>
        public string Rmak4
        {
            get
            {
                if (htFields["RMAK4"] == null)
                {
                    return null;
                }
                return htFields["RMAK4"].ToString();
            }
            set
            {
                htFields["RMAK4"] = value;
            }
        }
        #endregion

        #region Rmak5
        //  <summary>
        // Rmak5
        //  <summary>
        public string Rmak5
        {
            get
            {
                if (htFields["RMAK5"] == null)
                {
                    return null;
                }
                return htFields["RMAK5"].ToString();
            }
            set
            {
                htFields["RMAK5"] = value;
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

        #region TEMPID
        /// <summary>
        /// TEMPID
        /// </summary>
        public string TEMPID
        {
            get
            {
                if (htFields["TEMPID"] == null)
                    return null;
                return htFields["TEMPID"].ToString();
            }
            set
            {
                htFields["TEMPID"] = value;
            }
        }
        #endregion

        #region MTYPE
        /// <summary>
        /// MTYPE
        /// </summary>
        public string MTYPE
        {
            get
            {
                if (htFields["MTYPE"] == null)
                    return null;
                return htFields["MTYPE"].ToString();
            }
            set
            {
                htFields["MTYPE"] = value;
            }
        }
        #endregion

        #region NGDES
        /// <summary>
        /// NGDES
        /// </summary>
        public string NGDES
        {
            get
            {
                if (htFields["NGDES"] == null)
                    return null;
                return htFields["NGDES"].ToString();
            }
            set
            {
                htFields["NGDES"] = value;
            }
        }
        #endregion
        #region ENGID
        /// <summary>
        /// MTYPE
        /// </summary>
        public string ENGID
        {
            get
            {
                if (htFields["ENGID"] == null)
                    return null;
                return htFields["ENGID"].ToString();
            }
            set
            {
                htFields["ENGID"] = value;
            }
        }
        #endregion

        #region DataMember
        UserInfo UserData = new UserInfo();
        protected override void SettingFields()
        {
            #region Set Fields
            htFields.Add("MANDT", null);
            htFields.Add("COMCD", null);
            htFields.Add("TASKID", null);
            htFields.Add("WERKS", null);
            htFields.Add("LGORT", null);
            htFields.Add("INSMK", null);
            htFields.Add("MATNR", null);
            htFields.Add("LIFNR", null);
            htFields.Add("MENGE", null);
            htFields.Add("DACOD", null);
            htFields.Add("VEDAT", null);
            htFields.Add("EXPDAT", null);
            htFields.Add("EXPDAT_AFTER", null);
            htFields.Add("MAXEXP", null);
            htFields.Add("MAXEXP_AFTER", null);
            htFields.Add("QMEXP", null);
            htFields.Add("CHKQTY", null);
            htFields.Add("RESULT", null);
            htFields.Add("CHKNAM", null);
            htFields.Add("CHKTIM", null);
            htFields.Add("QCFLG", null);
            htFields.Add("APPNO", null);
            htFields.Add("MRBNO", null);
            htFields.Add("EXPTP", null);
            htFields.Add("PRTID", null);
            htFields.Add("IFPRT", null);
            htFields.Add("RMAK1", null);
            htFields.Add("RMAK2", null);
            htFields.Add("RMAK3", null);
            htFields.Add("RMAK4", null);
            htFields.Add("RMAK5", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("ENGID", null);
            #endregion

            #region Set Fields Type
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("TASKID", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBNumber);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("VEDAT", DBDataType.DBString);
            htFieldType.Add("EXPDAT", DBDataType.DBString);
            htFieldType.Add("EXPDAT_AFTER", DBDataType.DBString);
            htFieldType.Add("MAXEXP", DBDataType.DBString);
            htFieldType.Add("MAXEXP_AFTER", DBDataType.DBString);
            htFieldType.Add("QMEXP", DBDataType.DBString);
            htFieldType.Add("CHKQTY", DBDataType.DBNumber);
            htFieldType.Add("RESULT", DBDataType.DBString);
            htFieldType.Add("CHKNAM", DBDataType.DBString);
            htFieldType.Add("CHKTIM", DBDataType.DBCreateDate);
            htFieldType.Add("QCFLG", DBDataType.DBString);
            htFieldType.Add("APPNO", DBDataType.DBString);
            htFieldType.Add("MRBNO", DBDataType.DBString);
            htFieldType.Add("EXPTP", DBDataType.DBString);
            htFieldType.Add("PRTID", DBDataType.DBString);
            htFieldType.Add("IFPRT", DBDataType.DBString);
            htFieldType.Add("RMAK1", DBDataType.DbunString);
            htFieldType.Add("RMAK2", DBDataType.DbunString);
            htFieldType.Add("RMAK3", DBDataType.DbunString);
            htFieldType.Add("RMAK4", DBDataType.DbunString);
            htFieldType.Add("RMAK5", DBDataType.DbunString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("TEMPID", DBDataType.DBString);
            htFieldType.Add("MTYPE", DBDataType.DBString);
            htFieldType.Add("NGDES", DBDataType.DBString);
            htFieldType.Add("ENGID", DBDataType.DBString);
            #endregion
        }
        #endregion
    }
}