using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KR
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("SearchCompany","Companies/SearchCompany/{pageNum}/{filter}",new { controller = "Companies", action = "SearchCompany", pageNum = 1, filter = "" });
            routes.MapRoute("FilterCompany", "Companies/FilterCompanies/{pageNum}/{filter}", new { controller = "Companies", action = "FilterCompanies", pageNum = 1, filter = "" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "CRM", action = "Index", id = UrlParameter.Optional }
            ); 
        }
    }
}