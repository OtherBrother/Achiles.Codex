using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achiles.Codex.Model;

namespace Achiles.Codex.Web.Models
{
    public class ArticleListViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}