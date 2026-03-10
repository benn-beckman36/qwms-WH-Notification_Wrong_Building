using System;
using System.Data;
using System.Collections;
using System.Text;
using QWMS.Common;
using QCI.QWMS;
using Qci.Base.Common;

namespace QCI
{
    namespace QWMS
    {
        /// <summary>
        /// LogData 的摘要描述。
        /// </summary>
        public class CarrouselData : ControlBase
        {
            private string strMandt = "";
            private string strComcd = "";
            private string strWerks = "";
            private string strLgort = "";
            private string strProgid = "";
            private string strCrnam = "";
            private string strErrmsg = "";
            private string strConnectionString;

            #region Constructer
            public CarrouselData()
            {

            }

            #region 利用預設的DBType, DBCode,....等來建構物件
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Carrousel物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="varUserData">UserData。</param>
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strProgid">程式代碼。</param>			
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public CarrouselData(UserInfo varUserData, string varWerks, string varLgort, string varProgid) :
                this(CommonInfo.Instance.DBType, CommonInfo.Instance.DBCode, CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode, varUserData, varWerks, varLgort, varProgid)
            {
            }
            #endregion



            #region 指定DBType, DBCode,....等來建構物件
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生QCI.QWMS.Carrousel物件, 並將所需的Connection String 傳入, 並存放於屬性objAccessSQL.ConnectionString中,並設定屬性值。
            /// </summary> 
            /// <param name="varDBType">DBType。</param>
            /// <param name="varDBCode">DBCode。</param>
            /// <param name="varErrorType">ErrorType。</param>
            /// <param name="varErrorCode">ErrorCode。</param>
            /// <param name="varUserData">UserData。</param>
            /// <param name="strWerks">廠區。</param>
            /// <param name="strLgort">倉別。</param>
            /// <param name="strProgid">程式代碼。</param>			
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.LogData objLogData =new QCI.QWMS.LogData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public CarrouselData(int varDBType, string varDBCode, int varErrorType, string varErrorCode, UserInfo varUserData, string varWerks, string varLgort, string varProgid)
            {
                UserData = varUserData;
                ControlErrorInfo = new ErrorInfo();
                ControlErrorInfo.CompanyCode = UserData.CompanyCode;
                ControlErrorInfo.DeptNo = UserData.DeptNo;
                ControlErrorInfo.ApplicationName = "QWMS";
                ControlErrorInfo.ObjectName = "QWMS.CarrouselData";
                ControlErrorInfo.ClientIP = UserData.ClientIP;
                ControlErrorInfo.CreateUser = UserData.UserId;
                ControlErrorInfo.CreateUserDomain = UserData.Domain;
                ControlErrorInfo.ServerIP = UserData.ServerIP;
                ControlErrorInfo.Owner = "Rock Tzeng";

                ControlDBCode = varDBCode;
                ControlDBType = varDBType;
                ControlErrCode = varErrorCode;
                ControlErrType = varErrorType;

                MANDT = varUserData.Client;
                COMCD = varUserData.CompanyCode;
                WERKS = varWerks;
                LGORT = varLgort;
                PROGID = varProgid;
                CRNAM = varUserData.UserId;

            }
            #endregion

            #endregion

            #region DataMember

            UserInfo UserData = new UserInfo();

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// SAP Client。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string ConnectionString
            {
                get { return strConnectionString; }
                set { strConnectionString = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// SAP Client。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string MANDT
            {
                get { return strMandt; }
                set { strMandt = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// Company code。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string COMCD
            {
                get { return strComcd; }
                set { strComcd = value; }
            }
            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 廠區。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string WERKS
            {
                get { return strWerks; }
                set { strWerks = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 倉別。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string LGORT
            {
                get { return strLgort; }
                set { strLgort = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 程式代碼。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string PROGID
            {
                get { return strProgid; }
                set { strProgid = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 建立者。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string CRNAM
            {
                get { return strCrnam; }
                set { strCrnam = value; }
            }

            /////////////////////////////////////////////////////////////////////////////
            /// <summary>
            /// 錯誤訊息。
            /// </summary>
            /////////////////////////////////////////////////////////////////////////////
            public string ERRMSG
            {
                get { return strErrmsg; }
                set { strErrmsg = value; }
            }



            #endregion

            #region MemberFunction


            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 將異動資料產生Carrousel命令
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="dtStorage">異動庫存內容。</param>
            /// <returns>
            /// ArrayList。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Carrousel objCarrouselData =new QCI.QWMS.CarrouselData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objCarrouselData.AddCarrouselData(strLocat, dtStorage);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////	
            public ArrayList AddCarrouselData(string strLocat, DataTable dtStorage)
            {
                ArrayList arySQL = new ArrayList();
                string[] aryCgcls = { "" };
                string strCommand = "";
                DataTable dtTemp = new DataTable();

                PlantData objPlantData = new PlantData(UserData);
                try
                {
                    for (int i = 0; i < dtStorage.Rows.Count; i++)
                    {
                        string strSQL = "select CTRLC3 from WHCTRL where SOLDTO='QWMS' and CTRLID='PROGM' and CTRLC2= '" + PROGID + "'";

                        DataTable dtData = new DataTable();
                        try
                        {
                            ControlHandleDB();
                            dtData = ControlSqlAccess.GetDataTable(strSQL);
                            ControlSqlAccess.CloseConnection();

                        }
                        catch (System.Exception ex)
                        {
                            ERRMSG = ex.Message + "<- AddCarrouselData()";
                        }

                        if (dtData.Rows[0]["CTRLC3"].ToString().IndexOf(";") < 0)
                        {
                            aryCgcls[0] = dtData.Rows[0]["CTRLC3"].ToString();
                        }
                        else
                        {
                            aryCgcls = dtData.Rows[0]["CTRLC3"].ToString().Split(new char[] { ';' });
                        }

                        for (int j = 0; j < aryCgcls.Length; j++)
                        {
                            if (aryCgcls[j].ToString() == "01" || aryCgcls[j].ToString() == "02" || aryCgcls[j].ToString() == "05" || aryCgcls[j].ToString() == "06") //連線入庫, 離線入庫, 連板入庫, 轉倉入庫
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {

                                    dtTemp = objPlantData.GetLocatPosition(WERKS, LGORT, dtStorage.Rows[i]["LOCAT"].ToString());
                                    strCommand = GenerateCarrouselCommand(dtStorage.Rows[i]["LOCAT"].ToString(), dtTemp.Rows[0]["STSET"].ToString(), dtTemp.Rows[0]["STHGH"].ToString(), dtTemp.Rows[0]["STLEN"].ToString(), dtTemp.Rows[0]["STWID"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["ALQTY"].ToString(), dtStorage.Rows[i]["KOSTL"].ToString(), "+");
                                    arySQL.Add("Insert into WHCRS(MANDT,COMCD, WERKS, LGORT, LOCAT, TRNTP, STSET, STHGH, STLEN, STWID, MATNR, MENGE, KOSTL, COMMD, FLAGE, DELET, CRNAM, CRDAT, MONAM, MODAT) values( '" +
                                        dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["COMCD"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + dtStorage.Rows[i]["LOCAT"].ToString() + "', '+', '" + dtTemp.Rows[0]["STSET"].ToString() + "', '" + dtTemp.Rows[0]["STHGH"].ToString() + "', '" + dtTemp.Rows[0]["STLEN"].ToString() + "', '" + dtTemp.Rows[0]["STWID"].ToString() + "','" +
                                        dtStorage.Rows[i]["MATNR"].ToString() + "'," + dtStorage.Rows[i]["ALQTY"].ToString() + ", '" + dtStorage.Rows[i]["KOSTL"].ToString() + "', '" + strCommand + "', '0', '0','" +
                                        CRNAM + "', getdate() ,'" + CRNAM + "',getdate())");
                                }
                            }

                            if (aryCgcls[j].ToString() == "11" || aryCgcls[j].ToString() == "12" || aryCgcls[j].ToString() == "15" || aryCgcls[j].ToString() == "16" || aryCgcls[j].ToString() == "17") //連線出庫, 離線出庫, 連板出庫, 轉倉出庫, 儲位出庫
                            {
                                if (int.Parse(dtStorage.Rows[i]["ALQTY"].ToString()) != 0)
                                {
                                    dtTemp = objPlantData.GetLocatPosition(WERKS, LGORT, dtStorage.Rows[i]["LOCAT"].ToString());
                                    strCommand = GenerateCarrouselCommand(dtStorage.Rows[i]["LOCAT"].ToString(), dtTemp.Rows[0]["STSET"].ToString(), dtTemp.Rows[0]["STHGH"].ToString(), dtTemp.Rows[0]["STLEN"].ToString(), dtTemp.Rows[0]["STWID"].ToString(), dtStorage.Rows[i]["MATNR"].ToString(), dtStorage.Rows[i]["ALQTY"].ToString(), dtStorage.Rows[i]["KOSTL"].ToString(), "-");
                                    arySQL.Add("Insert into WHCRS(MANDT,COMCD, WERKS, LGORT, LOCAT, TRNTP, STSET, STHGH, STLEN, STWID, MATNR, MENGE, KOSTL, COMMD, FLAGE, DELET, CRNAM, CRDAT, MONAM, MODAT) values( '" +
                                        dtStorage.Rows[i]["MANDT"].ToString() + "','" + dtStorage.Rows[i]["COMCD"].ToString() + "','" + dtStorage.Rows[i]["WERKS"].ToString() + "','" + dtStorage.Rows[i]["LGORT"].ToString() + "','" + dtStorage.Rows[i]["LOCAT"].ToString() + "', '-', '" + dtTemp.Rows[0]["STSET"].ToString() + "', '" + dtTemp.Rows[0]["STHGH"].ToString() + "', '" + dtTemp.Rows[0]["STLEN"].ToString() + "', '" + dtTemp.Rows[0]["STWID"].ToString() + "','" +
                                        dtStorage.Rows[i]["MATNR"].ToString() + "'," + dtStorage.Rows[i]["ALQTY"].ToString() + ", '" + dtStorage.Rows[i]["KOSTL"].ToString() + "', '" + strCommand + "', '0', '0','" +
                                        CRNAM + "', getdate() ,'" + CRNAM + "',getdate())");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ERRMSG = ex.Message + "<- AddCarrouselData()";
                }
                return arySQL;
            }




            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 產生Carrousel命令
            /// </summary> 
            /// <param name="strLocat">儲位。</param>
            /// <param name="strStset">座。</param>
            /// <param name="strSthgh">層。</param>
            /// <param name="strStlen">長。</param>
            /// <param name="strStwid">深。</param>
            /// <param name="strMatnr">料號。</param>
            /// <param name="strAlqty">數量。</param>
            /// <param name="strKostl">版本。</param>
            /// <param name="strType">類型。</param>
            /// <returns>
            /// string。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Carrousel objCarrouselData =new QCI.QWMS.CarrouselData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objCarrouselData.GenerateCarrouselCommand(strLocat, strStset, strSthgh, strStlen, strStwid, strMatnr, strAlqty, strKostl, strType);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public string GenerateCarrouselCommand(string strLocat, string strStset, string strSthgh, string strStlen, string strStwid, string strMatnr, string strAlqty, string strKostl, string strType)
            {
                string strStx = "";
                string strEtx = "";
                string strControlCode = "";
                string strCrane = "";
                string strHeight = "";
                string strLength = "";
                string strWidth = "";
                string strCheckSum = "";
                string strDescription = "";
                string strAllString = "";
                string strAlqty1 = "";
                try
                {
                    strControlCode = "A";	//E1
                    strCrane = Convert.ToInt32(strStset).ToString("00");
                    strHeight = Convert.ToInt32(strSthgh).ToString("000");
                    strLength = Convert.ToInt32(strStlen).ToString("00");
                    strWidth = Convert.ToInt32(strStwid).ToString("0");
                    strAlqty = Convert.ToInt32(strAlqty).ToString("0000");
                    strAlqty1 = strAlqty;
                    if (strAlqty.Length > 4)
                    {
                        strAlqty = strAlqty.Substring(0, 4);
                    }
                    strDescription = strMatnr + " " + strType + strAlqty1.ToString();
                    strCheckSum = GenerateCheckSum(strCrane + strHeight + strLength + strWidth + "0000");
                    strAllString = strStx + strControlCode + strCrane + strHeight + strLength + strWidth + "0000" + strCheckSum + strDescription + strEtx;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "<- GenerateCarrouselCommand()");
                }
                return strAllString;
            }




            //=========================================================================
            ////////////Summary by Rock Tzeng////////////////////////////////////////////
            /// <summary>
            /// 取得Carrousel命令檢查碼
            /// </summary> 
            /// <param name="strCommand">Carrousel命令。</param>
            /// <returns>
            /// string。
            /// </returns>
            /// <example>
            /// <code>
            /// <remarks>
            ///  QCI.QWMS.Carrousel objCarrouselData =new QCI.QWMS.CarrouselData(strConnectionString,strMandt,strWerks,strLgort,strProgid,strCrnam);
            ///  ArrayList arrData = objCarrouselData.GenerateCheckSum(strCommand);
            ///  Your Code Here......
            /// </remarks>
            /// </code>
            /// </example>
            /////////////////////////////////////////////////////////////////////////////
            public string GenerateCheckSum(string strCommand)
            {
                int intCheckSum = 0;
                int intCheckSum1 = 0;
                int intCheckSum2 = 0;
                for (int i = 0; i < strCommand.Length; i++)
                {
                    intCheckSum += Convert.ToInt32(strCommand.Substring(i, 1));
                }
                intCheckSum2 = intCheckSum % 16;
                intCheckSum1 = intCheckSum / 16;

                if (intCheckSum2 > 9)
                {
                    intCheckSum2 = intCheckSum2 - 10;
                    intCheckSum1 = intCheckSum1 + 1;
                }

                return Convert.ToString(intCheckSum1) + Convert.ToString(intCheckSum2);
            }



            #endregion
        }
    }
}
