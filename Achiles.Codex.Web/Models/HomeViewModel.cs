using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Indexes;

namespace Achiles.Codex.Web.Models
{
    public class HomeViewModel
    {
        public Article RandomArticle { get; set; }
        public SearchIndex.Result RandomItem { get; set; }
        public TagStatisticsIndex.TagStatistics[] Tags { get; set; }
    }
}