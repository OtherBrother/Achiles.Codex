using System;
using Achiles.Codex.Web.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Achiles.Codex.Web.Tests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void cleanup_cleans_markup_correctly()
        {
            const string str = @"<p>string<p/>";
            var result = str.Cleanup();
            Assert.AreEqual("string",result);

        }
        [TestMethod]
        public void cleanup_cleans_unclosed_tag_correctly()
        {
            const string str = @"<p>string<p";
            var result = str.Cleanup();
            Assert.AreEqual("string", result);

        }
    }
}
