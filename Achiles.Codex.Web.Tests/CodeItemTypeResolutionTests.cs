using System;
using System.Linq;
using Achilles.Codex.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Achilles.Codex.Web.Tests
{
    [TestClass]
    public class CodexItemTypeResolutionTests
    {
        [TestMethod]
        public void can_resolve_type()
        {
            CodexItemType[] types = new CodexItemType[0];
            string term = null;
            var result = CodexItem.GetTypesForQuery("ncg/test", out types, out term);

            Assert.IsTrue(result);
            Assert.IsTrue(types.Contains(CodexItemType.Talent));
            Assert.IsTrue(types.Contains(CodexItemType.Skill));
            Assert.AreEqual("test",term);
        }

        [TestMethod]
        public void requires_at_least_one_letter_after_type_to_resolve()
        {
            CodexItemType[] types;
            string term = null;
            var result = CodexItem.GetTypesForQuery("ncg/", out types, out term);

            Assert.IsFalse(result);
            
        }
    }
}
