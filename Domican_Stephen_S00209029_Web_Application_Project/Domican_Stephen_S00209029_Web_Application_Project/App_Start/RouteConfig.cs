using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Domican_Stephen_S00209029_Web_Application_Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "ShowAllPlaces",
               url: "ShowAllPlaces/{id}",
               defaults: new { controller = "PlacesTbls", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ShowAllPeople",
               url: "ShowAllPeople/{id}",
               defaults: new { controller = "PeopleTbls", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
