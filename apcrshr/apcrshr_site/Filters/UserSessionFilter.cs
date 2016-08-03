using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using apcrshr_site.Helper;
using Site.Core.Repository;

namespace apcrshr_site.Filters
{
    public class UserSessionFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["User-SessionID"] == null)
            {
                filterContext.Result = new RedirectResult("/User/Login");
            }
            else
            {
                string sessionId = filterContext.HttpContext.Session["User-SessionID"].ToString();
                UserSession userSession = SessionUtil.GetInstance.VerifySession(sessionId);
                if (userSession == null)
                {
                    filterContext.Result = new RedirectResult("/User/Login");
                }
            }
        }
    }
}