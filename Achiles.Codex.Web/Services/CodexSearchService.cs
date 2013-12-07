using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Achilles.Codex.Web.Controllers;
using Achilles.Codex.Web.Indexes;
using Raven.Client;
using Raven.Client.Linq;

namespace Achilles.Codex.Web.Services
{
    public interface ICodexSearchService
    {
        IEnumerable<SearchIndex.Result> Find(SearchQuery query);
        string[] GetSuggestions(SearchQuery query);
    }

    public class CodexSearchService : ICodexSearchService
    {
        private readonly IDocumentSession _session;

        public CodexSearchService(IDocumentSession session)
        {
            _session = session;
        }
        private IRavenQueryable<SearchIndex.Result> ConstructRavenQueryable(SearchQuery searchQuery)
        {
            var q = _session.Query<SearchIndex.Result, SearchIndex>();

            if (searchQuery.SearchTags.Any())
                q = searchQuery.SearchTags.Aggregate(q, (current, t) => current.Where(x => x.Tags.Any(y => y == t)));

            if (searchQuery.SearchObjects.Any())
                q = q.Where(x => x.ObjectType.In(searchQuery.SearchObjects));

            if (searchQuery.IsFullText)
                q = q.Search(x => x.Description, string.Format("{0}", searchQuery.SearchTerm), options: SearchOptions.And);
            else if (!string.IsNullOrEmpty(searchQuery.SearchTerm))
                q = q.Where(x => x.Name.StartsWith(searchQuery.SearchTerm));
            return q;
        }   
  
        public IEnumerable<SearchIndex.Result> Find(SearchQuery searchQuery)
        {
            var q = ConstructRavenQueryable(searchQuery);
            return q.AsProjection<SearchIndex.Result>().ToList();
        }

        public string[] GetSuggestions(SearchQuery query)
        {
            return ConstructRavenQueryable(query).Suggest().Suggestions;
        }
    }
}