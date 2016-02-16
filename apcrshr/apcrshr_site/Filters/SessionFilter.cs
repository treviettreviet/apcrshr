using Site.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using apcrshr_site.Helper;

namespace apcrshr_site.Filters
{
    public class SessionFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["SessionID"] == null)
            {
                filterContext.Result = new RedirectResult("/Administrator/AdminHome/Login");
            }
            else
            {
                string sessionId = filterContext.HttpContext.Session["SessionID"].ToString();
                UserSession userSession = SessionUtil.GetInstance.VerifySession(sessionId);
                if (userSession == null)
                {
                    filterContext.Result = new RedirectResult("/Administrator/AdminHome/Login");
                }
            }
        }
    }
}