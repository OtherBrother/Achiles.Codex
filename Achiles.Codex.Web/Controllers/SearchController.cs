using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Indexes;
using Achiles.Codex.Web.Misc;
using Achiles.Codex.Web.Models;
using Microsoft.Win32.SafeHandles;
using Raven.Client;
using Raven.Client.Linq;

namespace Achiles.Codex.Web.Controllers
{
    public class SearchController : CodexItemController
    {
    
        
       

        public SearchController()
        {
           
        }

        [ValidateInput(false)]
        public ActionResult Results(string query)
        {
            ViewBag.Title = "Search results";

            var searchQuery = new SearchQuery(query);

            var q = DocumentSession.Query<SearchIndex.Result, SearchIndex>();
            
            if (searchQuery.SearchTags.Any())
                q = searchQuery.SearchTags.Aggregate(q, (current, t) => current.Where(x => x.Tags.Any(y => y == t)));
            
            if (searchQuery.SearchObjects.Any())
                q = q.Where(x => x.ObjectType.In(searchQuery.SearchObjects));

            if (searchQuery.IsFullText)
                q = q.Search(x => x.Description, string.Format("{0}", searchQuery.SearchTerm), options: SearchOptions.And);
            else if (!string.IsNullOrEmpty(searchQuery.SearchTerm))
                q = q.Where(x => x.Name.StartsWith(searchQuery.SearchTerm));
 
            var model = new SearchResultsViewModel
            {
                Results  = q.AsProjection<SearchIndex.Result>().ToList(),
                OriginalQuery = searchQuery
            };

            if (!model.Results.Any())
            {
                model.Suggestions = q.Suggest().Suggestions;
            }

            return View(model);
        }
    }
    public class SearchQuery
    {
        public const string FullTextSymbol = "!";
        private static readonly List<Mapping> Mappings = new List<Mapping>();
        static SearchQuery()
        {
            Mappings.Add(new Mapping { PossibleInputs = { "attribute", "at", "attr" }, MappedItemTypes = { CodexItemType.Attribute } });
            Mappings.Add(new Mapping { PossibleInputs = { "article", "ar" }, MappedItemTypes = { CodexItemType.Article } });
            Mappings.Add(new Mapping { PossibleInputs = { "ruleset", "rs" }, MappedItemTypes = { CodexItemType.RuleSet } });
            Mappings.Add(new Mapping { PossibleInputs = { "rule", "r" }, MappedItemTypes = { CodexItemType.Rule } });
            Mappings.Add(new Mapping { PossibleInputs = { "skill", "sk", }, MappedItemTypes = { CodexItemType.Skill } });
            Mappings.Add(new Mapping { PossibleInputs = { "combatskill", "cs" }, MappedItemTypes = { CodexItemType.CombatSkill } });
            Mappings.Add(new Mapping { PossibleInputs = { "misc", "m" }, MappedItemTypes = { CodexItemType.MiscellaneousItem } });
            Mappings.Add(new Mapping { PossibleInputs = { "s" }, MappedItemTypes = { CodexItemType.Skill, CodexItemType.CombatSkill } });
            Mappings.Add(new Mapping { PossibleInputs = { "t" }, MappedItemTypes = { CodexItemType.Talent } });
            Mappings.Add(new Mapping { PossibleInputs = { "cg" }, MappedItemTypes = { CodexItemType.CombatSkill, CodexItemType.Attribute } });
            Mappings.Add(new Mapping { PossibleInputs = { "ncg" }, MappedItemTypes = { CodexItemType.Skill, CodexItemType.Talent } });

            Mappings.Add(new Mapping { PossibleInputs = { "armor", "am" }, MappedItemTypes = { CodexItemType.HeadArmor, CodexItemType.BodyArmor, CodexItemType.ArmArmor, CodexItemType.LegArmor } });
            Mappings.Add(new Mapping { PossibleInputs = { "weapon", "w" }, MappedItemTypes = { CodexItemType.HandWeapon, CodexItemType.RangedWeapon, CodexItemType.Ammo, CodexItemType.Shield } });
            Mappings.Add(new Mapping { PossibleInputs = { "equipment", "eq" }, MappedItemTypes = { CodexItemType.HeadArmor, CodexItemType.BodyArmor, CodexItemType.ArmArmor, CodexItemType.LegArmor, CodexItemType.HandWeapon, CodexItemType.RangedWeapon, CodexItemType.Ammo, CodexItemType.Shield } });
        }

        private class Mapping
        {
            public Mapping()
            {
                PossibleInputs = new HashSet<string>();
                MappedItemTypes = new HashSet<CodexItemType>();
            }

            public HashSet<string> PossibleInputs { get; set; }
            public HashSet<CodexItemType> MappedItemTypes { get; set; }
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

           SearchObjects = Mappings.Where(x => x.PossibleInputs.Overlaps(searchObjects))
                .SelectMany(x => x.MappedItemTypes)
                .Distinct()
                .ToArray();
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