using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeoAnalyzerApp.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoAnalyzerApp.Tests
{
    [TestClass]
    public class SeoAnalyzerTest
    {
        [TestMethod]
        public void UrlTest()
        {
            var dic = new Dictionary<string, bool>() {
                { "/home", false },
                { "home", false },
                { "/home.aspx", false },
                { "home.aspx", false },
                { "/home/something", false },
                { "home/something", false },
                { "/home/something.aspx", false },
                { "home/something.aspx", false },
                { "?something", false },
                { "#", false },
                { "#something", false },
                { "http://host", true },
                { "http://host:8090", true },
                { @"\\home", true },
                { @"\\home\something.txt", true },
                { @"file:///d:/", true },
                { @"file:///d:/home", true },
                { @"file:///d:/home/something.txt", true },
            };
            foreach (var item in dic)
            {
                var seoAnalayzer = new SeoAnalyzer(new HtmlParser());
                var actual = seoAnalayzer.IsAbsoluteUrl(item.Key);
                var expected = item.Value;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
