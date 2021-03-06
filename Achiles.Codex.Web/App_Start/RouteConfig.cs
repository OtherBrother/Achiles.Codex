﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Achilles.Codex.Model;

namespace Achilles.Codex.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute("JsonCodexItemFind", "Codex/Find/{*q}",defaults: new { controller = "Codex", Action = "Find" });
            routes.MapRoute("RedirectToItem", "Redirect/{*id}", defaults: new { controller = "Codex", Action = "RedirectToItem" });

            routes.MapRoute("DefaultCodexItemView", "Codex/item/{*id}", defaults: new { controller = "Codex", Action = "Item" });
            routes.MapRoute("CodexGetJson", "Codex/{*id}", defaults: new { controller = "Codex", Action = "Get" });

            routes.MapRoute("Search", "search/{query}",defaults: new { controller = "Search", Action = "Results" });
            routes.MapRoute("SearchExt", "search/Results/{query}", defaults: new { controller = "Search", Action = "Results" });

            routes.MapRoute("TagSuggest", "Tags/Suggest/{q}", defaults: new { controller = "Tags", Action = "Suggest" });

            routes.MapRoute("ArticleList", "Articles/{tag}", new { controller = "Article", Action = "Index", pageSize = UrlParameter.Optional, pageNumber = UrlParameter.Optional, tag = UrlParameter.Optional });
            routes.MapRoute("ArticleListJson", "Articles/Json/{tag}", new { controller = "Article", Action = "GetJsonArticles", pageSize = UrlParameter.Optional, pageNumber = UrlParameter.Optional, tag = UrlParameter.Optional });

            routes.MapRoute("ncgRules", "Rules/ncg/", defaults: new {controller="Rule", action = "Index", gear = Gear.NonCombat });
            routes.MapRoute("cgRules", "Rules/cg/", defaults: new { controller="Rule", action = "Index", gear = Gear.Combat });
            

            routes.MapRoute(name: "Default",url: "{controller}/{action}/{*id}", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
