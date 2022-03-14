using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Filters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)//判斷是否已驗證
                return false;
            return true;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext); // call --> AuthorizeCore
            //未授權網頁, 導到首頁
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Unauthorized/" +
                filterContext.HttpContext.User.Identity.Name);
                filterContext.Result = new EmptyResult();
                return;
            }
            string loginUser = filterContext.HttpContext.User.Identity.Name;
            Match m = Regex.Match(loginUser, @"\\{0,1}(\d{4})@{0,1}");
            if (m.Success)
                loginUser = m.Groups[1].ToString();
            //-------------------------------------------------------
            if (filterContext.HttpContext.Session["empno"] == null)
            {
                //Get Userinfo
                filterContext.HttpContext.Session["empno"] = loginUser;
                filterContext.HttpContext.Session["empname"] = loginUser;
            }
        }
    }
}