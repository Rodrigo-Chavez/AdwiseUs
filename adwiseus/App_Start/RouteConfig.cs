using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace adwiseus
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
     name: "Localized",
     url: "{culture}/{controller}/{action}/{id}",
     defaults: new { culture = "es-ES", controller = "Home", action = "Index", id = UrlParameter.Optional },
     constraints: new { culture = @"[a-z]{2}-[A-Z]{2}" } // Restringe el formato de cultura a "es-ES", "en-US", etc.
 );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { culture = "es-ES", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
