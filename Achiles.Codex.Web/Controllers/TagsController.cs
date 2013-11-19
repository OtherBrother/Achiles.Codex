using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Web.Indexes;
using Raven.Client;

namespace Achiles.Codex.Web.Controllers
{
    public class TagsController : CodexItemController
    {
        public JsonResult Suggest(string q)
        {
            var suggestedTags = DocumentSession.Query<TagStatisticsIndex.TagStatistics, TagStatisticsIndex>()
                .Where(t => t.Tag.StartsWith(q)).ToArray().Select(x => x.Tag).ToArray();

            return new JsonResult() { Data = suggestedTags, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
	}
}