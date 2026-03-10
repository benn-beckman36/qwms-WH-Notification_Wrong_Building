using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QWMS.Common;
using System.Collections;
using Qci.Base.Common;




/// <summary>
/// UserSystem 的摘要说明
/// </summary>
public class UserSystem
{
    //public UserSystem()
    //{
    //    //
    //    // TODO: 在此处添加构造函数逻辑
    //    //
    //}        

    //public static UserInfo UserData = new UserInfo();

    #region 傳入帳號、密碼及公司別驗證是否正確by Marc
    /// <summary>
    /// 傳入帳號、密碼及公司別驗證是否正確by Marc
    /// </summary>
    /// <param name="varUserID">User ID。</param>
    /// <param name="varPasswd">Password。</param>
    /// <param name="varComcod">Company Code。</param>
    /// <returns>
    /// 回傳值型態為bool。
    /// </returns>
    /// <example>
    /// <code>
    /// bool bolLogin = UserSystem.UserLogOn("UserID","PassWord","9700");
    /// </code>
    /// </example>
    /////////////////////////////////////////////////////////////////////////////
    public static bool UserLogOn(string varRolcd, string varCompid, string varUserId, string varPassWord, ref UserInfo varUserData)
    {
        try
        {
            //UserData = varUserData;


            //PILOT.RSE.ClaAuthority objAuth = new PILOT.RSE.ClaAuthority(varUserData);
            

            //if (objAuth.CreateUserInstance(varRolcd, varCompid, varUserId, varPassWord))
            //{
            //    //WriteUserId(varUserId);
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion



    public static void WriteUserId(string varUserId)
    {
        System.Web.Security.FormsAuthentication.SetAuthCookie(varUserId, false);
    }
    public static void LogOff()
    {
        System.Web.Security.FormsAuthentication.SignOut();

        System.Web.HttpContext.Current.Session["UserData"] = null;

    }

    #region 取得User資訊
    public static void GetUserInfo(ref UserInfo varUserData)
    {
        try
        {
            varUserData = (UserInfo)(System.Web.HttpContext.Current.Session["UserData"]);
            if (varUserData == null)
                System.Web.HttpContext.Current.Response.Redirect("~/AutoError.aspx");
        }
        catch (Exception)
        {
            varUserData = null;
            System.Web.HttpContext.Current.Response.Redirect("~/AutoError.aspx");
        }
    }
    #endregion


    public static void WriteSystemLog(UserInfo varUserData)
    {
        LogAccess objLog = new LogAccess(CommonInfo.Instance.ErrType, CommonInfo.Instance.ErrCode);
        objLog.ResetValue();
        objLog.SystemCode = "QWMS";
        objLog.FunctionCode = "Login";
        objLog.ActionCode = "";
        objLog.AspName = HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath;
        objLog.UrlName = HttpContext.Current.Request.Url.AbsoluteUri;
        objLog.CompanyCode = varUserData.CompanyCode;//買家放空白
        objLog.Creator = varUserData.UserId;
        objLog.RemarkText1 = HttpContext.Current.Request.PhysicalPath;
        //objLog.RemarkText2 = "Server:" + HttpContext.Current.Server.MachineName + "(" + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] + "); Client IP:" + HttpContext.Current.Request.UserHostName + "(" + HttpContext.Current.Request.UserHostAddress + ")";
        objLog.RemarkText2 = "Server:" + varUserData.ServerIP + "; Client IP:" + varUserData.ClientIP;
        objLog.RemarkText3 = varUserData.Client;
        objLog.RemarkText4 = "";
        objLog.WriteSystemLog();

    }




    //檢查使用者是否有登入網域
    public static void GetInternalUserInfo(ref UserInfo varUserData)
    {
        GetUserInfo(ref varUserData);

        if (varUserData.QuantaAccount == "")
        {
            try
            {
                //string UserName = System.Web.HttpContext.Current.User.Identity.Name;
                //string[] strID;
                //strID = UserName.Split(new char[] { '\\' });
                //if (strID[0].ToUpper() == "QUANTA")
                //{
                //    varUserData.Domain = "QUANTA";
                //    varUserData.CompanyCode = "QCI";
                //}
                //else if (strID[0].ToUpper() == "QUANTACN")
                //{
                //    varUserData.Domain = "QUANTACN";
                //    varUserData.CompanyCode = "QSMC";
                //}
                //else
                //{
                //    varUserData.Domain = strID[0].ToUpper().Trim();
                //    varUserData.CompanyCode = strID[0].ToUpper().Trim();
                //}
                //varUserData.QuantaAccount = strID[1].ToUpper().Trim();
                //varUserData.UserId = strID[1].ToUpper().Trim();

                //#region 抓HR的部門代碼判定要不要卡料頭(5碼部門別抓第2碼為"P",6碼部門別抓第3碼為"P")

                //varUserData.EnableFrontMaterial = false;

                //wsEmployee.Wsgamsmdemph objEmployee = new wsEmployee.Wsgamsmdemph();

                //ArrayList aryQueryConditions = new ArrayList();
                //aryQueryConditions.Add("(COMCOD in ('CSMC','QCI','QSMC')) ");
                //aryQueryConditions.Add("(EMPLID='" + varUserData.UserId + "')");
                //aryQueryConditions.Add("(ONJOBS='1')");

                //wsEmployee.MdemphTableItem[] arrEmployeeData = objEmployee.ExecQMdemphTableObj("HRMSQ00001", aryQueryConditions.ToArray());

                //if (arrEmployeeData.Length > 0)
                //{
                //    string strTmpDeptCode = "";
                //    for (int i = 0; i < arrEmployeeData.Length; i++)
                //    {
                //        strTmpDeptCode = arrEmployeeData[i].Depcod.ToString().ToUpper().Trim();
                //        varUserData.DeptNo = strTmpDeptCode;
                //        if (strTmpDeptCode.Length == 5)
                //        {
                //            if (strTmpDeptCode.Substring(1, 1) == "P")
                //            {
                //                varUserData.EnableFrontMaterial = true;
                //            }
                //        }
                //        else if (strTmpDeptCode.Length == 6)
                //        {
                //            if (strTmpDeptCode.Substring(2, 1) == "P")
                //            {
                //                varUserData.EnableFrontMaterial = true;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    //沒有在HR資料庫中建檔
                //    System.Web.HttpContext.Current.Response.Redirect("~/AutoError.aspx");

                //}
                //#endregion

                //#region 設定料頭
                //if (varUserData.EnableFrontMaterial == true)
                //{
                //    wsFrontMaterial.wsAuthority objMaterialAuth = new wsFrontMaterial.wsAuthority();
                //    varUserData.FrontMaterial = objMaterialAuth.wsQueryAuthorityToString("", varUserData.QuantaAccount).ToUpper().Trim();

                //    string[] arrFrontMaterial = varUserData.FrontMaterial.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                //    for (int i = 0; i < arrFrontMaterial.Length; i++)
                //    {
                //        if (arrFrontMaterial[i].Trim() == "*")
                //        {
                //            varUserData.EnableFrontMaterial = false;
                //        }
                //    }

                //}
                //#endregion

                #region 測試用
                //varUserData.EnableFrontMaterial = true;
                //varUserData.FrontMaterial = "37,3E,AH";
                #endregion
            }
            catch (System.NullReferenceException ex)
            {
                //varUserData.QuantaAccount = "";
                //varUserData.Domain = "";
                //varUserData.CompanyCode = "";
                varUserData = null;
                System.Web.HttpContext.Current.Response.Redirect("~/AutoError.aspx");
            }
        }
    }

    #region 不使用

    #region 传入系统名称、帐号、菜单序号、页面地址验证帐号的访问权限 by Brian
    /// <summary>
    /// 传入系统名称、帐号、菜单序号、页面地址验证帐号的访问权限
    /// </summary>
    /// <param name="varUserID">帐号</param>
    /// <param name="varMenuID">菜单序号</param>
    /// <param name="varURL">页面地址</param>
    /// <returns>
    /// 回傳值型態為bool。
    /// </returns>
    /// /// <example>
    /// <code>
    /// bool bolLogin = UserSystem.CheckAccessAuthority("UserID","MenuID","index.aspx");
    /// </code>
    /// </example>
    /////////////////////////////////////////////////////////////////////////////
    //public static bool CheckAccessAuthority(string varUserID, string varMenuID, string varURL, UserInfo varUserData)
    //{
    //    bool result = false;
    //    try
    //    {
    //        string strMenuSystemCode = ConfigurationManager.AppSettings["MenuSystemCode"];

    //        CsmcEC.Authority.ClsAuthority objAuth = new CsmcEC.Authority.ClsAuthority(varUserData);

    //        DataTable dt = objAuth.GetMenuList(strMenuSystemCode, varUserID, varMenuID);

    //        if (dt.Rows.Count > 0 && varURL == dt.Rows[0]["MENURL"].ToString())
    //        {
    //            result = true;
    //        }
    //    }
    //    catch
    //    {
    //        result = false;
    //    }
    //    return result;
    //}

    /// <summary>
    /// 传入系统名称、帐号、菜单序号、页面地址验证帐号的访问权限
    /// </summary>
    /// <param name="varUserID">帐号</param>
    /// <param name="varMenuID">菜单序号</param>
    /// <returns>
    /// 回傳值型態為bool。
    /// </returns>
    /// /// <example>
    /// <code>
    /// bool bolLogin = UserSystem.CheckAccessAuthority("UserID","MenuID");
    /// </code>
    /// </example>
    /////////////////////////////////////////////////////////////////////////////
    //public static bool CheckAccessAuthority(string varUserID, string varMenuID, UserInfo varUserData)
    //{
    //bool result = false;
    //try
    //{
    //    string strMenuSystemCode = ConfigurationManager.AppSettings["MenuSystemCode"];

    //    CsmcEC.Authority.ClsAuthority objAuth = new CsmcEC.Authority.ClsAuthority(varUserData);

    //    DataTable dt = objAuth.GetMenuList(strMenuSystemCode, varUserID, varMenuID);

    //    if (dt.Rows.Count > 0)
    //    {
    //        result = true;
    //    }
    //}
    //catch
    //{
    //    result = false;
    //}
    //return result;
    //}
    #endregion

    #region 設定User前端的資訊 by Marc
    ///// <summary>
    ///// 設定User前端的資訊 by Marc
    ///// </summary>
    ///// <param name="varClientIP">Client IP。</param>
    ///// <param name="varServerIP">Server IP。</param>
    ///// <returns>
    ///// 回傳值型態為bool。
    ///// </returns>
    ///// <example>
    ///// <code>
    ///// UserSystem.SetClientInfo("110.243.40.11","210.22.11.123");
    ///// </code>
    ///// </example>
    ///////////////////////////////////////////////////////////////////////////////
    //public static void SetClientInfo(string varClientIP, string varServerIP)
    //{
    //    UserData.ClientIP = varClientIP;
    //    UserData.ServerIP = varServerIP;

    //}
    #endregion
    #endregion

}
