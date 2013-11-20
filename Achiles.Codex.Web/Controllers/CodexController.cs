using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Indexes;
using Raven.Client;
using Raven.Client.Linq;

namespace Achiles.Codex.Web.Controllers
{
    public class CodexController : CodexItemController
    {
        private const int MinQueryLength = 3;
        private readonly JsonResult _noResults = new JsonResult { Data = new SearchIndex.Result[] { }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        public ActionResult Redirect(string id)
        {
            return RedirectToAction("Index", "Home");
        }
        
        public JsonResult Get(string id)
        {
            var coedxItem = DocumentSession.Load<CodexItem>(id);
            if (coedxItem == null)
                throw new HttpException(404, string.Format("Codex item with {0} id not found", id));

            return new JsonResult { Data = coedxItem , JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
        public JsonResult Find(string q)
        {
            if (q.Length < MinQueryLength)
                return _noResults;

            CodexItemType[] types;
            string term = null;
            IRavenQueryable<SearchIndex.Result> results;
            
            if (CodexItem.GetTypesForQuery(q, out types, out term))
            {
                if (term.Length < MinQueryLength)
                    return _noResults;

                 results =
                    DocumentSession.Query<SearchIndex.Result, SearchIndex>()
                        .Where(x => x.ObjectType.In(types) && x.Name.StartsWith(term));
            }
            else
            {
                results =  DocumentSession.Query<SearchIndex.Result, SearchIndex>().Where(x => x.Name.StartsWith(q));

            }
            return new JsonResult { Data = results.AsProjection<SearchIndex.Result>().ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}