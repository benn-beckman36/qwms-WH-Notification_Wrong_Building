using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qci.Base.Common;
using QWMS.Common;
using System.Data;

namespace QCI_QWMS_Models
{
    public class ModelInfo : ControlBase
    {
        #region Constructer

        public ModelInfo()
        {
        }

        public ModelInfo(UserInfo varUserData)
                : this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData)
            {
            }

            public ModelInfo(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();

                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QCI_QWMS_Models.ModelInfo";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Tom Gao";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;
            }

            #endregion

        #region DataMember

            UserInfo UserData = new UserInfo();

        #endregion

        #region MemberFunction

            #region 查询模具基本信息
            /// <summary>
            /// 查询模具基本信息
            /// </summary>
            /// <param name="strModelNo"></param>
            /// <param name="strAssetsNo"></param>
            /// <returns></returns>
            public DataTable GetModelInfo(string strModelNo, string strAssetsNo)
            {
                try
                {
                    ControlHandleDB();
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendFormat("SELECT 'false' as Selected ,* FROM [dbo].[M_Models] AS M LEFT JOIN [dbo].[M_Assets] AS A ON M.ModelNo=A.ModelNo WHERE 1=1 ");
                    if (!string.IsNullOrEmpty(strModelNo))
                    {
                        sbSql.AppendFormat(" AND M.ModelNo='{0}' ", strModelNo);
                    }
                    if (!string.IsNullOrEmpty(strAssetsNo))
                    {
                        sbSql.AppendFormat(" AND A.AssetsNo='{0}' ", strAssetsNo);
                    }
                    dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            #region 查询模具基本信息--批量
            /// <summary>
            /// 查询模具基本信息
            /// </summary>
            /// <param name="strModelNo"></param>
            /// <param name="strAssetsNo"></param>
            /// <returns></returns>
            public DataTable GetModelInfo(DataTable dtData)
            {
                try
                {
                    ControlHandleDB();
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    string strModelNo = "";

                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        strModelNo += dtData.Rows[i]["ModelNo"].ToString().Trim() + "','";
                    }
                    strModelNo = strModelNo.Substring(0, strModelNo.Length - 3);

                    sbSql.AppendFormat("SELECT 'false' as Selected ,* FROM [dbo].[M_Models] AS M LEFT JOIN [dbo].[M_Assets] AS A ON M.ModelNo=A.ModelNo WHERE 1=1 ");
                    
                    if (dtData.Rows.Count>0)
                    {
                        sbSql.AppendFormat(" AND M.ModelNo IN('" + strModelNo + "') ");
                    }
                    dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            #region 新增模具基本信息
        /// <summary>
        /// 新增模具基本信息
        /// </summary>
        /// <param name="strModelNo"></param>
        /// <param name="strAssetsNo"></param>
        /// <param name="strItemName"></param>
        /// <param name="strBU"></param>
        /// <param name="strMachine"></param>
        /// <param name="decQuantity"></param>
        /// <param name="decNWeight"></param>
        /// <param name="strPoNo"></param>
        /// <param name="strRemark"></param>
        /// <returns></returns>
            public bool AddModelInfo(string strModelNo, string strAssetsNo, string strItemName, string strBU, string strMachine, decimal decQuantity, decimal decNWeight,string strPoNo,string strRemark)
            {
                try
                {
                    bool bolFlag = false;
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.Append("INSERT INTO [dbo].[M_Models](ModelNO,ItemName,BU,Machine,Quantity,NWeight,PoNo,Remark,CRNAM,CRDAT) ")
                        .AppendFormat("VALUES('{0}',N'{1}','{2}',N'{3}','{4}','{5}','{6}',N'{7}','{8}',GETDATE()); ", strModelNo, strItemName, strBU, strMachine, decQuantity, decNWeight, strPoNo, strRemark, UserData.UserId)
                        .AppendFormat("INSERT INTO [dbo].[M_Assets](ModelNO,AssetsNo,CRNAM,CRDAT) VALUES('{0}','{1}','{2}',GETDATE()) ",strModelNo,strAssetsNo,UserData.UserId);
                    ControlHandleDB();
                    bolFlag = ControlSqlAccess.ExecSql(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return bolFlag;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            #region 修改模具基本信息
            
            public bool UpdateModelInfo(string strModelNo, string strAssetsNo, string strItemName, string strBU, string strMachine, decimal decQuantity, decimal decNWeight, string strPoNo, string strRemark)
            { 
                try
                {
                    bool bolFlag = false;
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendFormat("UPDATE [dbo].[M_Models] SET ItemName=N'{0}',BU='{1}',Machine=N'{2}',Quantity={3},Nweight={4},", strItemName, strBU, strMachine, decQuantity, decNWeight)
                        .AppendFormat("PoNo='{0}',Remark=N'{1}',MODNM='{2}',MODTM=GETDATE() WHERE ModelNO='{3}';", strPoNo, strRemark, UserData.UserId, strModelNo)
                        .AppendFormat("UPDATE [dbo].[M_Assets] SET AssetsNo='{0}',MODNM='{1}',MODTM=GETDATE() WHERE ModelNO='{2}' ",strAssetsNo,UserData.UserId,strModelNo);
                    ControlHandleDB();
                    bolFlag = ControlSqlAccess.ExecSql(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return bolFlag;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            #region 删除模具基本信息
            
            public bool DeleteModelInfo(string strModelNo)
            {
                try
                {
                    bool bolFlag = false;
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendFormat("DELETE [dbo].[M_Models] WHERE ModelNO in('{0}');", strModelNo)
                        .AppendFormat("DELETE [dbo].[M_Assets] WHERE ModelNO in('{0}');",strModelNo);
                    ControlHandleDB();
                    bolFlag = ControlSqlAccess.ExecSql(sbSql.ToString());
                    ControlSqlAccess.CloseConnection();
                    return bolFlag;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            #region 检查模具基本信息是否能修改
            
            public bool CheckModel(string strModelNo)
            {
                try
                {
                    bool bolFlag = false;
                    ControlHandleDB();
                    DataTable dt = new DataTable();
                    StringBuilder sbSql = new StringBuilder();
                    sbSql.AppendFormat("SELECT MANDT,COMCD,MATNR FROM [dbo].[WHTRM] WHERE MATNR in('{0}') UNION ALL SELECT MANDT,COMCD,MATNR FROM [dbo].[WHITM] WHERE MATNR in('{1}') ",strModelNo,strModelNo);
                    dt = ControlSqlAccess.GetDataTable(sbSql.ToString());
                    if (dt.Rows.Count > 0)
                    {
                        bolFlag = true;
                    }
                    ControlSqlAccess.CloseConnection();
                    return bolFlag;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

        #endregion
    }
}
