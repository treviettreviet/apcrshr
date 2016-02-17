using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace apcrshr_site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                "Detail",                                              // Route name
                "{controller}/{action}/{ActionURL}",                           // URL with parameters
                new { controller = "Home", action = "Index", ActionURL = "" }  // Parameter defaults
            );

            ///Artist/GetImages/cher/api-key
            routes.MapRoute(
                "Category",                                              // Route name
                "{controller}/{action}/{ActionURL}/{pageIndex}",                           // URL with parameters
                new { controller = "Home", action = "Index", ActionURL = "", pageIndex = 1 }  // Parameter defaults
            );
        }
    }
}