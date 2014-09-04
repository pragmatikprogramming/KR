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

            routes.MapRoute("SearchCompany","Companies/SearchCompany/{pageNum}/{mode}/{filter}",new { controller = "Companies", action = "SearchCompany", pageNum = 1, mode = 0, filter = "" });
            routes.MapRoute("FilterCompany", "Companies/FilterCompanies/{pageNum}/{mode}/{filter}", new { controller = "Companies", action = "FilterCompanies", pageNum = 1, mode = 0, filter = "" });
            routes.MapRoute("DescriptionFilter", "Companies/DescriptionFilter/{pageNum}/{mode}/{filter}", new { controller = "Companies", action = "DescriptionFilter", pageNum = 1, mode = 1, filter = "" });

            routes.MapRoute("SearchCandidate", "Candidate/SearchCandidate/{pageNum}/{mode}/{filter}", new { controller = "Candidate", action = "SearchCandidate", pageNum = 1, mode = 0, filter = "" });
            routes.MapRoute("NameFilter", "Candidate/NameFilter/{pageNum}/{mode}/{filter}", new { controller = "Candidate", action = "NameFilter", pageNum = 1, mode = 0, filter = "" });
            routes.MapRoute("ResumeFilter", "Candidate/ResumeFilter/{pageNum}/{mode}/{filter}", new { controller = "Candidate", action = "ResumeFilter", pageNum = 1, mode = 1, filter = "" });

            routes.MapRoute("SearchStaff", "Staff/SearchStaff/{pageNum}/{mode}/{filter}", new { controller = "Staff", action = "SearchStaff", pageNum = 1, mode = 0, filter = "" });
            routes.MapRoute("StaffNameFilter", "Staff/FilterStaff/{pageNum}/{mode}/{filter}", new { controller = "Staff", action = "FilterStaff", pageNum = 1, mode = 0, filter = "" });
            routes.MapRoute("StaffDescriptionFilter", "Staff/DescriptionFilter/{pageNum}/{mode}/{filter}", new { controller = "Staff", action = "DescriptionFilter", pageNum = 1, mode = 1, filter = "" });
            routes.MapRoute("DeleteStaff", "Staff/DeleteStaff/{id}/{pageNum}/{mode}/{filter}", new { controller = "Staff", action = "DeleteStaff", id = 0, pageNum = 1, mode = 0, filter = "" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ); 

            routes.MapRoute(
                name: "Note",
                url: "{controller}/{action}/{id}/{noteType}",
                defaults: new { controller = "Note", action="AddNote", id = UrlParameter.Optional, noteType = UrlParameter.Optional }
            );

            
        }
    }
}