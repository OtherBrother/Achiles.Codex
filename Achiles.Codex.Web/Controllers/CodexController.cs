using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Achilles.Codex.Model;
using Achilles.Codex.Web.Indexes;
using Achilles.Codex.Web.Models;
using Achilles.Codex.Web.Services;
using Raven.Client;
using Raven.Client.Linq;

namespace Achilles.Codex.Web.Controllers
{
    public class CodexController : CodexItemBaseController
    {
        private readonly ICodexSearchService _searchService;
        private const int MinQueryLength = 3;
        private readonly JsonResult _noResults = new JsonResult { Data = new SearchIndex.Result[] { }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        
        public CodexController(ICodexSearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult RedirectToItem(string id)
        {
            return RedirectToAction("Item",new {id} );
        }
        
        public JsonResult Get(string id)
        {
            var coedxItem = DocumentSession.Load<CodexItem>(id);
            if (coedxItem == null)
                throw new HttpException(404, string.Format("Codex item with {0} id not found", id));

            return new JsonResult { Data = coedxItem , JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
        [ValidateInput(false)]
        public JsonResult Find(string q)
        {

            var searchQuery = new SearchQuery(q);

            var results = _searchService.Find(searchQuery);

            return new JsonResult { Data = results, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult Item(string id)
        {
            var item = DocumentSession.Load<CodexItem>(id);
            var model = new DefaultCodexItemViewModel
            {
                Item = item
            };
            
            var baseItem = item as CodexItemBase;
            if (baseItem != null && !string.IsNullOrEmpty(baseItem.BodyId))
            {
                model.Body = DocumentSession.Load<Body>(baseItem.BodyId);
            }
            if (item != null)
            {
                ViewBag.Title = item.Name;
            }

            return View(model);
        }

    }
}