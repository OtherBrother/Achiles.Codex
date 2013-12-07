using System.Collections.Generic;
using System.Text;
using Achilles.Codex.Model;

namespace Achilles.Codex.Web.Controllers
{
    public class SearchQuery
    {
        public const string FullTextSymbol = "!";
        
        static SearchQuery()
        {
           
        }
        
        /*
         * Serach syntax:
        * [?][ObjectType[,ObjectType]:]Search term[@tag1,tag2]
        * ?:skill,attribute:
        */

        public SearchQuery(string query)
        {

            IsFullText = query.StartsWith(FullTextSymbol);
            if (IsFullText)
                query = query.Remove(0, 1);
            
            var typeSeparatorIndex = query.IndexOf(":");
            var tagSeparatorIndex = query.LastIndexOf("@");

            var typeModifier = typeSeparatorIndex > -1 ? typeSeparatorIndex + 1 : 0;

            SearchTerm = query.Substring(typeModifier, (tagSeparatorIndex > -1 ? tagSeparatorIndex : (query.Length)) - typeModifier);
            var searchObjects =
                new HashSet<string>(typeSeparatorIndex > -1
                    ? query.Substring(0, typeSeparatorIndex).Split(',')
                    : new string[0]);

            SearchTags = tagSeparatorIndex > -1 ? query.Substring(tagSeparatorIndex + 1).Split(',') : new string[0];

            SearchObjects = CodexItem.MatchTypes(searchObjects);
        }

        public bool IsFullText { get; set; }
        public string SearchTerm { get; set; }
        public CodexItemType[] SearchObjects { get; set; }
        public string[] SearchTags { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.AppendFormat("IsFullText: {0}", IsFullText)
                .AppendLine()
                .AppendFormat("Search term : {0}", SearchTerm)
                .AppendLine()
                .AppendFormat("SearchObjects:", string.Concat(",", SearchObjects))
                .AppendLine().AppendFormat("SearchTags:", string.Concat(",", SearchTags));
            
            return sb.ToString();
        }
    }
}