using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FSGEMappingTool
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Client Create",
                url: "client/create",
                defaults: new { controller = "Client", action = "Create"}
            );

            routes.MapRoute(
                name: "Client Routes",
                url: "client/{id}/{action}",
                defaults: new { controller = "Client", action = "Index", id = "" }
            );

            routes.MapRoute(
                name: "Map Routes",
                url: "map/{id}/{action}",
                defaults: new { controller = "Map", action = "Index", id = "" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}