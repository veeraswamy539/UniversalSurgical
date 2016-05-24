using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UniversalSurgicals
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapRoute(
        //    name: "dashboard",
        //url: "Employee/Dashboard",
        //defaults: new { controller = "Employee", action = "Dashboard" });

            //routes.MapRoute(
            //  name: "dashboard",
            //  url: "Employee/home/{id}",
            //  defaults: new { controller = "Employee", action = "home", id = UrlParameter.Optional }
            //  );

//            routes.MapRoute(
//        name: "dashboard",
//        url: "Employee/Dashboard",
//        defaults: new { controller = "Employee", action = "Dashboard" }
//);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Signin", id = UrlParameter.Optional }
            );
        }
    }
}