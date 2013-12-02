using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Indexes;
using Achiles.Codex.Web.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace Achiles.Codex.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDocumentSession _session;

        public HomeController(IDocumentSession session)
        {
            _session = session;
        }
        
        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                RandomArticle = DocumentSession.Query<Article>().Customize(x => x.RandomOrdering()).Take(1).FirstOrDefault(),
                RandomItem = DocumentSession.Query<SearchIndex.Result, SearchIndex>()
                .Where(x=>!x.ObjectType.In(CodexItemType.Article))
                    .Customize(x => x.RandomOrdering()).AsProjection<SearchIndex.Result>()
                    .Take(1)
                    .FirstOrDefault(),
                Tags = DocumentSession.Query<TagStatisticsIndex.TagStatistics, TagStatisticsIndex>().ToArray()
            };
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            
            return View();
        }

       
    }
}