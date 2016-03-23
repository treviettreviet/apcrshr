using Site.Core.DataModel.Model;
using Site.Core.DataModel.Response;
using Site.Core.Service.Contract;
using Site.Core.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace apcrshr_site.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorizationFilter : AuthorizeAttribute
    {
        private static readonly string ADMINISTRATOR_ID = "03d48cb6-46b5-43d3-98f2-872eaacd36bc";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            IAdminService _adminService = new AdminService();
            string userId = HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrEmpty(userId))
            {
                filterContext.Result = new RedirectResult("/Administrator/AdminHome/Login");
            }
            else
            {
                FindItemReponse<AdminModel> adminResponse = _adminService.FindAdminByUsername(userId);
                if (adminResponse.Item == null)
                {
                    filterContext.Result = new RedirectResult("/Administrator/AdminHome/Login");
                }
                else
                {
                    //Id administrator, skip verify
                    if (!adminResponse.Item.AdminID.Equals(ADMINISTRATOR_ID))
                    {
                        //Current route
                        string route = string.Format("{0}/{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);

                        FindItemReponse<ResourceModel> resourceResponse = _adminService.GetAuthorizedResource(adminResponse.Item.AdminID, route);
                        if (resourceResponse.Item == null)
                        {
                            filterContext.Result = new RedirectResult("/Administrator/Error/Index");
                        }
                    }
                }
            }

            base.OnAuthorization(filterContext);
        }
    }
}