using Achilles.Codex.Model;
using Achilles.Codex.Web.Indexes;

namespace Achilles.Codex.Web.Models
{
    public class HomeViewModel
    {
        public Article RandomArticle { get; set; }
        public SearchIndex.Result RandomItem { get; set; }
        public TagStatisticsIndex.TagStatistics[] Tags { get; set; }
    }
}