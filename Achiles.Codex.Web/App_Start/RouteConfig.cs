using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Achiles.Codex.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("JsonCodexItemFind", "Codex/Find/{*q}",defaults: new { controller = "Codex", Action = "Find" });
            routes.MapRoute("RedirectToItem", "Redirect/{*id}",defaults: new { controller = "Codex", Action = "Redirect" });
            routes.MapRoute("CodexGetJson", "Codex/{*id}", defaults: new { controller = "Codex", Action = "Get" });
            


            routes.MapRoute("Search", "search/{query}",
                defaults: new { controller = "Search", Action = "Results" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
