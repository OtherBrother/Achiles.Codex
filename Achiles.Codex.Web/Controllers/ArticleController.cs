using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Models;
using Raven.Client;
using Raven.Abstractions.Extensions;
using Achilles.Codex.Web.Misc;

namespace Achilles.Codex.Web.Controllers
{
    public class ArticleController : CodexItemBaseController
    {
       
        [System.Web.Mvc.HttpGet]
        public ActionResult List(int? pageSize, int? pageNumber, string tag)
        {
            ViewBag.Tag = tag;
            return View();
        }
        [System.Web.Mvc.HttpGet]
        public JsonResult GetJsonArticles(int? pageSize, int? pageNumber, string tag)
        {
            var model = GetArticleListModel(pageSize, pageNumber,tag);
            return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = model };
        }

        private ArticleListViewModel GetArticleListModel(int? pageSize, int? pageNumber, string tag)
        {
            var ps = pageSize.GetValueOrDefault(10);
            var pn = pageNumber.GetValueOrDefault(1);
            var articles = Enumerable.Empty<Article>();
            RavenQueryStatistics stats = null;
            if (!string.IsNullOrEmpty(tag))
            {
                articles = DocumentSession.Query<Article>().Statistics(out stats).Where(x => x.Tags.Any(z => z == tag)).Skip(ps * (pn - 1)).Take(ps);
            }
            else
            {
                articles = DocumentSession.Query<Article>().Statistics(out stats).Skip(ps * (pn - 1)).Take(ps);
            }

            articles.ForEach(x => x.Description = x.Description.Fit(200));

            var model = new ArticleListViewModel
            {
                Articles = articles,
                Page = ps,
                PageSize = pn,
                TotalArticles = stats.TotalResults
            };
            return model;
        }

        [ValidateInput(true)]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Authorize(Roles = "Contributor")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new Article());
            
            var model = DocumentSession.Load<Article>(id);
            if(model ==null)
                throw new HttpException(404, "Article not found");
            return View(model);
        }
        
        [System.Web.Http.HttpPost]
        [System.Web.Mvc.Authorize(Roles = "Contributor")]
        [ValidateInput(false)]
        public ActionResult Edit(Article input)
        {
            Article article = null;
            if (string.IsNullOrEmpty(input.Id))
            {
                DocumentSession.Store(input);
                article = input;
            }
            else
            {
                article = DocumentSession.Load<Article>(input.Id);
                article.Description = input.Description;
                article.Tags = input.Tags.Select(t => t.ToLower()).ToList();
             }
            Success("Goodness gracious!", "tl;dr :)");
            SetRelatedItems(article);
            DocumentSession.SaveChanges();
            return View(article);
        }
    }
}