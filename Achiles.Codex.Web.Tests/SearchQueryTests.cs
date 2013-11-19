using System;
using System.Linq;
using System.Text;
using Achiles.Codex.Model;
using Achiles.Codex.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Achiles.Codex.Web.Tests
{
    [TestClass]
    public class SearchQueryTests
    {
        [TestMethod]
        
        public void given_simple_term_query_object_constructs_correctly()
        {
            const string queryStr = "search string";
            
            var query = new SearchQuery(queryStr);
            
            Assert.AreEqual("search string", query.SearchTerm);
            
            Assert.IsFalse(query.IsFullText);
            Assert.IsFalse(query.SearchTags.Any());
            Assert.IsFalse(query.SearchObjects.Any());
        }
        [TestMethod]
        public void given_fulltext_term_query_object_constructs_correctly()
        {
            const string queryStr = "!search string";

            var query = new SearchQuery(queryStr);

            Assert.AreEqual("search string", query.SearchTerm);
            Assert.IsTrue(query.IsFullText);
        }
        [TestMethod]
        public void given_type_query_object_contructs_correctly()
        {
            const string queryStr = "attribute:search string";

            var query = new SearchQuery(queryStr);

            Assert.AreEqual("search string", query.SearchTerm);
            Assert.AreEqual(CodexItemType.Attribute, query.SearchObjects[0]);
            Assert.IsFalse(query.IsFullText);
        }
        [TestMethod]
        public void given_type_query_with_tags_object_contructs_correctly()
        {
            const string queryStr = "attribute:search string@tag";

            var query = new SearchQuery(queryStr);

            Assert.AreEqual("search string", query.SearchTerm);
            Assert.AreEqual(CodexItemType.Attribute, query.SearchObjects[0]);
            Assert.AreEqual("tag", query.SearchTags[0]);
            Assert.IsFalse(query.IsFullText);

        }

        [TestMethod]
        public void given_full_text_search__multiple_type_query_with_multiple_tags_object_contructs_correctly()
        {
            const string queryStr = "!attribute,skill:search string@tag1,tag2";

            var query = new SearchQuery(queryStr);

            Assert.AreEqual("search string", query.SearchTerm);
            Assert.AreEqual(CodexItemType.Attribute, query.SearchObjects[0]);
            Assert.AreEqual(CodexItemType.Skill, query.SearchObjects[1]);
            Assert.AreEqual("tag1", query.SearchTags[0]);
            Assert.AreEqual("tag2", query.SearchTags[1]);
            Assert.IsTrue(query.IsFullText);

        }
        [TestMethod]
        public void given_search_multiple_type_query_with_multiple_tags_object_contructs_correctly()
        {
            const string queryStr = "attribute,skill:search string@tag1,tag2";

            var query = new SearchQuery(queryStr);

            Assert.AreEqual("search string", query.SearchTerm);
            Assert.AreEqual(CodexItemType.Attribute, query.SearchObjects[0]);
            Assert.AreEqual(CodexItemType.Skill, query.SearchObjects[1]);
            Assert.AreEqual("tag1", query.SearchTags[0]);
            Assert.AreEqual("tag2", query.SearchTags[1]);
            Assert.IsFalse(query.IsFullText);
            }

        [TestMethod]
        public void given_weird_string_exception_is_not_thrown()
        {
            const string queryStr = "saD!sd:asd@tag";

            var query = new SearchQuery(queryStr);

            Assert.AreEqual("asd", query.SearchTerm);
            Assert.AreEqual(0, query.SearchObjects.Count());
            Assert.AreEqual("tag", query.SearchTags[0]);
            Assert.IsFalse(query.IsFullText);
        }

        [TestMethod]
        public void given_weirder_string_exception_is_not_thrown()
        {
            const string queryStr = "asd@sa:D!sd:asd@tag:sd";
            var query = new SearchQuery(queryStr);
            Assert.IsNotNull(query);
            Console.WriteLine(query.ToString());
        }
        [TestMethod]
        public void object_type_mappings_work()
        {
            const string queryStr = "at,ar,rs,sk,r,cs,m,s,cg,ncg,eq:search string";
            var query = new SearchQuery(queryStr);

            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.Attribute));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.Article));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.RuleSet));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.Skill));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.CombatSkill));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.MiscellaneousItem));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.HandWeapon));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.RangedWeapon));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.Shield));
            Assert.IsTrue(query.SearchObjects.Contains(CodexItemType.Ammo));
            Console.WriteLine(query.ToString());

        }
    }
}
