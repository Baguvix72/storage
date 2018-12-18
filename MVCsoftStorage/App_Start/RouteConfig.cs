using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCsoftStorage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "List", action = "All", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ListFilter",
                url: "{controller}/{action}/{categ}/{id}",
                defaults: new { controller = "List", action = "Filter", categ = "all", id = UrlParameter.Optional }
            );
        }
    }
}
