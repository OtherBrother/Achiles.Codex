using System.Linq;
using System.Web.Mvc;
using Achiles.Codex.Web.Models;
using Achiles.Codex.Web.Services;

namespace Achiles.Codex.Web.Controllers
{
    public class SearchController : CodexItemController
    {
        private readonly ICodexSearchService _searchService;


        public SearchController(ICodexSearchService searchService)
        {
            _searchService = searchService;
        }

        [ValidateInput(false)]
        public ActionResult Results(string query)
        {
            ViewBag.Title = "Search results";

            var searchQuery = new SearchQuery(query);
            
 
            var model = new SearchResultsViewModel
            {
                Results = _searchService.Find(searchQuery),
                OriginalQuery = searchQuery
            };

            if (!model.Results.Any())
            {
                model.Suggestions = _searchService.GetSuggestions(searchQuery);
            }

            return View(model);
        }
    }
}