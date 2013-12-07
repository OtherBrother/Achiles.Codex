using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;

namespace Achilles.Codex.Web.Controllers
{
    public class ArticleController : CodexItemBaseController
    {
        //
        // GET: /Article/
        [System.Web.Mvc.HttpGet]
        public ActionResult Create()
        {
            return View(new Article());
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                article.Id = string.Format("article/{0}",article.Id);
                DocumentSession.Store(article);
                DocumentSession.SaveChanges();
            }
            return View(article);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Index(int? pageSize, int? pageNumber)
        {
            var model = GetArticleListModel(pageSize, pageNumber);
            return View(model);
        }
        [System.Web.Mvc.HttpGet]
        public JsonResult GetJsonArticles(int? pageSize, int? pageNumber)
        {
            var model = GetArticleListModel(pageSize, pageNumber);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = model };
        }

        private ArticleListViewModel GetArticleListModel(int? pageSize, int? pageNumber)
        {
            var ps = pageSize.GetValueOrDefault(10);
            var pn = pageNumber.GetValueOrDefault(1);

            var model = new ArticleListViewModel
            {
                Articles = DocumentSession.Query<Article>().Skip(ps*(pn - 1)).Take(ps).ToArray(),
                Page = ps,
                PageSize = pn
            };
            return model;
        }
    }
}