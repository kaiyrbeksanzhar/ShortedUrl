using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlShorted
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Insert", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "RedirectTo",
               url: "redirect/{path}",
               defaults: new { controller = "Home", action = "RedirecTo", path = string.Empty }
           );


        }
    }
}
