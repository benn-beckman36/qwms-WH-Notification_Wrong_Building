using Qci.Base.Common;
using QWMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCI_QWMS_Entity
{
    public class DataWhtww : EntityBase
    {
        #region constractor
        public DataWhtww(UserInfo varUserData)
            : this (CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
        {
        }
        public DataWhtww(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData) {
            UserData = varUserData;
            ControlErrorInfo = new ErrorInfo();
            ControlErrorInfo.CompanyCode = UserData.CompanyCode;
            ControlErrorInfo.DeptNo = UserData.DeptNo;
            ControlErrorInfo.ApplicationName = "QWMS";
            ControlErrorInfo.ObjectName = "QWMS.Entity.DataWhtww";
            ControlErrorInfo.ClientIP = UserData.ClientIP;
            ControlErrorInfo.CreateUser = UserData.UserId;
            ControlErrorInfo.CreateUserDomain = UserData.Domain;
            ControlErrorInfo.ServerIP = UserData.ServerIP;
            ControlErrorInfo.Owner = "Brian Zhao";

            ControlDBCode = varDBCode;
            ControlDBType = varDBType;
            ControlErrCode = varErrorCode;
            ControlErrType = varErrorType;

            this.EntityTableName = "DBO.WHTWW";

            SetFields();
        }
        #endregion

        #region variables
        private UserInfo UserData { get; set; }

        protected override void SettingFields() {
            #region set fields
            htFields.Add("MANDT", null);
            htFields.Add("COMCD", null);
            htFields.Add("WERKS", null);
            htFields.Add("MBLNR", null);
            htFields.Add("ZEILE", null);
            htFields.Add("MBFLG", null);
            htFields.Add("MATNR", null);
            htFields.Add("CHARG", null);
            htFields.Add("INSMK", null);
            htFields.Add("MENGE", null);
            htFields.Add("OTQTY", null);
            htFields.Add("BXQTY", null);
            htFields.Add("LIFNR", null);
            htFields.Add("LGORT", null);
            htFields.Add("LOCAT", null);
            htFields.Add("UMLGO", null);
            htFields.Add("UMLOC", null);
            htFields.Add("IEFLG", null);
            htFields.Add("BWART", null);
            htFields.Add("DACOD", null);
            htFields.Add("VEDAT", null);
            htFields.Add("EXPDAT", null);
            htFields.Add("MAXEXP", null);
            htFields.Add("LOCOD", null);
            htFields.Add("VBELN", null);
            htFields.Add("VBILE", null);
            htFields.Add("OMBLN", null);
            htFields.Add("OMFLG", null);
            htFields.Add("BOXID", null);
            htFields.Add("SERNO", null);
            htFields.Add("GRNUM", null);
            htFields.Add("PONUM", null);
            htFields.Add("CRNAM", null);
            htFields.Add("CRDAT", null);
            htFields.Add("MONAM", null);
            htFields.Add("MODAT", null);
            htFields.Add("RMAK1", null);
            htFields.Add("RMAK2", null);
            htFields.Add("RMAK3", null);
            htFields.Add("KOSTL", null);
            htFields.Add("SEDTM", null);
            htFieldType.Add("MANDT", DBDataType.DBString);
            htFieldType.Add("COMCD", DBDataType.DBString);
            htFieldType.Add("WERKS", DBDataType.DBString);
            htFieldType.Add("MBLNR", DBDataType.DBString);
            htFieldType.Add("ZEILE", DBDataType.DBString);
            htFieldType.Add("MBFLG", DBDataType.DBString);
            htFieldType.Add("MATNR", DBDataType.DBString);
            htFieldType.Add("CHARG", DBDataType.DBString);
            htFieldType.Add("INSMK", DBDataType.DBString);
            htFieldType.Add("MENGE", DBDataType.DBFunc);
            htFieldType.Add("OTQTY", DBDataType.DBFunc);
            htFieldType.Add("BXQTY", DBDataType.DBFunc);
            htFieldType.Add("LIFNR", DBDataType.DBString);
            htFieldType.Add("LGORT", DBDataType.DBString);
            htFieldType.Add("LOCAT", DBDataType.DBString);
            htFieldType.Add("UMLGO", DBDataType.DBString);
            htFieldType.Add("UMLOC", DBDataType.DBString);
            htFieldType.Add("IEFLG", DBDataType.DBString);
            htFieldType.Add("BWART", DBDataType.DBString);
            htFieldType.Add("DACOD", DBDataType.DBString);
            htFieldType.Add("VEDAT", DBDataType.DBString);
            htFieldType.Add("EXPDAT", DBDataType.DBString);
            htFieldType.Add("MAXEXP", DBDataType.DBString);
            htFieldType.Add("LOCOD", DBDataType.DBString);
            htFieldType.Add("VBELN", DBDataType.DBString);
            htFieldType.Add("VBILE", DBDataType.DBString);
            htFieldType.Add("OMBLN", DBDataType.DBString);
            htFieldType.Add("OMFLG", DBDataType.DBString);
            htFieldType.Add("BOXID", DBDataType.DBString);
            htFieldType.Add("SERNO", DBDataType.DBString);
            htFieldType.Add("GRNUM", DBDataType.DBString);
            htFieldType.Add("PONUM", DBDataType.DBString);
            htFieldType.Add("CRNAM", DBDataType.DBString);
            htFieldType.Add("CRDAT", DBDataType.DBCreateDate);
            htFieldType.Add("MONAM", DBDataType.DBString);
            htFieldType.Add("MODAT", DBDataType.DBCreateDate);
            htFieldType.Add("RMAK1", DBDataType.DBString);
            htFieldType.Add("RMAK2", DBDataType.DBString);
            htFieldType.Add("RMAK3", DBDataType.DBString);
            htFieldType.Add("KOSTL", DBDataType.DBString);
            htFieldType.Add("SEDTM", DBDataType.DBCreateDate);
            #endregion
        }

        #region properties
        public string MANDT {
            get { return (string)htFields["MANDT"]; }
            set { htFields["MANDT"] = value; }
        }
        public string COMCD {
            get { return (string)htFields["COMCD"]; }
            set { htFields["COMCD"] = value; }
        }
        public string WERKS {
            get { return (string)htFields["WERKS"]; }
            set { htFields["WERKS"] = value; }
        }
        public string MBLNR {
            get { return (string)htFields["MBLNR"]; }
            set { htFields["MBLNR"] = value; }
        }
        public string ZEILE {
            get { return (string)htFields["ZEILE"]; }
            set { htFields["ZEILE"] = value; }
        }
        public string MBFLG {
            get { return (string)htFields["MBFLG"]; }
            set { htFields["MBFLG"] = value; }
        }
        public string MATNR {
            get { return (string)htFields["MATNR"]; }
            set { htFields["MATNR"] = value; }
        }
        public string CHARG {
            get { return (string)htFields["CHARG"]; }
            set { htFields["CHARG"] = value; }
        }
        public string INSMK {
            get { return (string)htFields["INSMK"]; }
            set { htFields["INSMK"] = value; }
        }
        public string MENGE {
            get { return (string)htFields["MENGE"]; }
            set { htFields["MENGE"] = value; }
        }
        public string OTQTY {
            get { return (string)htFields["OTQTY"]; }
            set { htFields["OTQTY"] = value; }
        }
        public string BXQTY {
            get { return (string)htFields["BXQTY"]; }
            set { htFields["BXQTY"] = value; }
        }
        public string LIFNR {
            get { return (string)htFields["LIFNR"]; }
            set { htFields["LIFNR"] = value; }
        }
        public string LGORT {
            get { return (string)htFields["LGORT"]; }
            set { htFields["LGORT"] = value; }
        }
        public string LOCAT {
            get { return (string)htFields["LOCAT"]; }
            set { htFields["LOCAT"] = value; }
        }
        public string UMLGO {
            get { return (string)htFields["UMLGO"]; }
            set { htFields["UMLGO"] = value; }
        }
        public string UMLOC {
            get { return (string)htFields["UMLOC"]; }
            set { htFields["UMLOC"] = value; }
        }
        public string IEFLG {
            get { return (string)htFields["IEFLG"]; }
            set { htFields["IEFLG"] = value; }
        }
        public string BWART {
            get { return (string)htFields["BWART"]; }
            set { htFields["BWART"] = value; }
        }
        public string DACOD {
            get { return (string)htFields["DACOD"]; }
            set { htFields["DACOD"] = value; }
        }
        public string VEDAT {
            get { return (string)htFields["VEDAT"]; }
            set { htFields["VEDAT"] = value; }
        }
        public string EXPDAT {
            get { return (string)htFields["EXPDAT"]; }
            set { htFields["EXPDAT"] = value; }
        }
        public string MAXEXP {
            get { return (string)htFields["MAXEXP"]; }
            set { htFields["MAXEXP"] = value; }
        }
        public string LOCOD {
            get { return (string)htFields["LOCOD"]; }
            set { htFields["LOCOD"] = value; }
        }
        public string VBELN {
            get { return (string)htFields["VBELN"]; }
            set { htFields["VBELN"] = value; }
        }
        public string VBILE {
            get { return (string)htFields["VBILE"]; }
            set { htFields["VBILE"] = value; }
        }
        public string OMBLN {
            get { return (string)htFields["OMBLN"]; }
            set { htFields["OMBLN"] = value; }
        }
        public string OMFLG {
            get { return (string)htFields["OMFLG"]; }
            set { htFields["OMFLG"] = value; }
        }
        public string BOXID {
            get { return (string)htFields["BOXID"]; }
            set { htFields["BOXID"] = value; }
        }
        public string SERNO {
            get { return (string)htFields["SERNO"]; }
            set { htFields["SERNO"] = value; }
        }
        public string GRNUM {
            get { return (string)htFields["GRNUM"]; }
            set { htFields["GRNUM"] = value; }
        }
        public string PONUM {
            get { return (string)htFields["PONUM"]; }
            set { htFields["PONUM"] = value; }
        }
        public string CRNAM {
            get { return (string)htFields["CRNAM"]; }
            set { htFields["CRNAM"] = value; }
        }
        public string CRDAT {
            get { return (string)htFields["CRDAT"]; }
            set { htFields["CRDAT"] = value; }
        }
        public string MONAM {
            get { return (string)htFields["MONAM"]; }
            set { htFields["MONAM"] = value; }
        }
        public string MODAT {
            get { return (string)htFields["MODAT"]; }
            set { htFields["MODAT"] = value; }
        }
        public string RMAK1 {
            get { return (string)htFields["RMAK1"]; }
            set { htFields["RMAK1"] = value; }
        }
            public string RMAK2 {
            get { return (string)htFields["RMAK2"]; }
            set { htFields["RMAK2"] = value; }
        }
        public string RMAK3 {
            get { return (string)htFields["RMAK3"]; }
            set { htFields["RMAK3"] = value; }
        }
        public string KOSTL {
            get { return (string)htFields["KOSTL"]; }
            set { htFields["KOSTL"] = value; }
        }
        public string SEDTM {
            get { return (string)htFields["SEDTM"]; }
            set { htFields["SEDTM"] = value; }
        }
        #endregion
        #endregion
    }
}
