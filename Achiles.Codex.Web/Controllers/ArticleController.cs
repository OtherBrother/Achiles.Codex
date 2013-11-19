using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Model;

namespace Achiles.Codex.Web.Controllers
{
    public class ArticleController : CodexItemBaseController
    {
        //
        // GET: /Article/
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Article());
        }
        [HttpPost]
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


    }
}