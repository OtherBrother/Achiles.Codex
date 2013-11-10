using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Win32.SafeHandles;

namespace Achiles.Codex.Web.Controllers
{
    public class SearchController : CodexItemController
    {

        private Dictionary<string[], string[]> mappings = new Dictionary<string[], string[]>();

        public SearchController()
        {
            mappings.Add(new string[] { "attribute", "at", "attr" }, new string[] { "attribute" });
            mappings.Add(new string[] { "article", "ar" }, new string[] { "article" });
            mappings.Add(new string[] { "ruleset", "rs" }, new string[] { "ruleset" });
            mappings.Add(new string[] { "talent", "ta" }, new string[] { "talent" });
            mappings.Add(new string[] { "rule", "ru" }, new string[] { "rule" });
            mappings.Add(new string[] { "skill", "sk" }, new string[] { "skill" });
            mappings.Add(new string[] { "combatskill", "cs" }, new string[] { "CombatSkill" });
            mappings.Add(new string[] { "misc", "m" }, new string[] { "MiscellaneousItem" });
        }


        public ActionResult Results(string query)
        {

            return View();
        }
    }
    public class SearchQuery
    {
        /*Serach syntax:
        * [?][ObjectType[,ObjectType]:]Search term[@tag1,tag2]
        * ?:skill,m:
        */

        public SearchQuery(string query)
        {

            IsFullText = query.StartsWith("?");
            if (IsFullText)
                query = query.Remove(0, 1);

            var typeSeparatorIndex = query.IndexOf(":");
            var tagSeparatorIndex = query.LastIndexOf("@");

            var typeModifier = typeSeparatorIndex > -1 ? typeSeparatorIndex + 1 : 0;

            SearchTerm = query.Substring(typeModifier, (tagSeparatorIndex > -1 ? tagSeparatorIndex : (query.Length)) - typeModifier);
            SearchObjects = typeSeparatorIndex > -1 ? query.Substring(0, typeSeparatorIndex).Split(',') : new string[0];
            SearchTags = tagSeparatorIndex > -1 ? query.Substring(tagSeparatorIndex + 1).Split(',') : new string[0];
        }

        public bool IsFullText { get; set; }
        public string SearchTerm { get; set; }
        public string[] SearchObjects { get; set; }
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