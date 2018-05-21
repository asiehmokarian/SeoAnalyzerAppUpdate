using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeoAnalyzerApp.BusinessLogic;

namespace SeoAnalyzerApp.Tests
{
    [TestClass]
    public class HtmlParserTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetContentFromUrl_Should_Throw_On_Null_Url()
        {
            var parser = new HtmlParser();
            parser.GetContentFromUrl(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UriFormatException))]
        public void GetContentFromUrl_Should_Throw_On_Invalid_Url()
        {
            var parser = new HtmlParser();
            parser.GetContentFromUrl("an invalid url.");
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void GetContentFromUrl_Should_Throw_On_Inaccessible_Url()
        {
            var parser = new HtmlParser();
            parser.GetContentFromUrl("http://someinaccessibleurl.myurl");
        }

        [TestMethod]
        public void GetContentFromUrl_Should_Returns_Content_On_Valid_Url()
        {
            var parser = new HtmlParser();
            Assert.IsNotNull(parser.GetContentFromUrl("http://www.google.com"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTextFromHtml_Should_Throw_On_Empty_Input()
        {
            var parser = new HtmlParser();
            parser.GetTextFromHtml(null);
        }

        [TestMethod]
        public void GetTextFromHtml_Should_Return_Text_When_Input_Is_PlainText()
        {
            var parser = new HtmlParser();
            var html = "this is a test";
            var actual = parser.GetTextFromHtml(html);
            var expected = "this is a test";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTextFromHtml_Should_Include_Text_Of_Tags_When_Input_Contains_Tag()
        {
            var parser = new HtmlParser();
            var html = "this is <a href='something'>test</a>";
            var actual = parser.GetTextFromHtml(html);
            var expected = "this is  test";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTextFromHtml_Should_Strip_Scripts()
        {
            var parser = new HtmlParser();
            var html = "this is <script>test</script>";
            var actual = parser.GetTextFromHtml(html);
            var expected = "this is ";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTextFromHtml_Should_Strip_Styles()
        {
            var parser = new HtmlParser();
            var html = "this is <style>test</style>";
            var actual = parser.GetTextFromHtml(html);
            var expected = "this is ";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTextFromHtml_Should_Strip_ScRiPTs()
        {
            var parser = new HtmlParser();
            var html = "this is <ScRiPT>test</ScRiPT>";
            var actual = parser.GetTextFromHtml(html);
            var expected = "this is ";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTextFromHtml_Should_Strip_StYles()
        {
            var parser = new HtmlParser();
            var html = "this is <StYle>test</StYle>";
            var actual = parser.GetTextFromHtml(html);
            var expected = "this is ";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMetaKeyWordsFromHtml_Should_Throw_On_Empty_Input()
        {
            var parser = new HtmlParser();
            parser.GetMetaKeyWordsFromHtml(null);
        }

        [TestMethod]
        public void GetMetaKeyWordsFromHtml_Should_Return_Empty_When_Input_Is_PlainText()
        {
            var parser = new HtmlParser();
            var html = "this is a test";
            var actual = parser.GetMetaKeyWordsFromHtml(html);
            var expected = 0;
            Assert.AreEqual(expected, actual.Count());
        }

        [TestMethod]
        public void GetMetaKeyWordsFromHtml_Should_Return_Empty_When_Input_DoesNot_Contain_MetaKeywords()
        {
            var parser = new HtmlParser();
            var html = "this is <a href='something'>test</a>";
            var actual = parser.GetMetaKeyWordsFromHtml(html);
            var expected = 0;
            Assert.AreEqual(expected, actual.Count());
        }

        [TestMethod]
        public void GetMetaKeyWordsFromHtml_Should_Return_Empty_When_MetaKeywords_DoesNot_Have_Content()
        {
            var parser = new HtmlParser();
            var html = "<html><head><meta name='keywords'></head><body>Hi!</body></html>";
            var actual = parser.GetMetaKeyWordsFromHtml(html);
            var expected = 0;
            Assert.AreEqual(expected, actual.Count());
        }

        [TestMethod]
        public void GetMetaKeyWordsFromHtml_Should_Return_Empty_When_MetaKeywords_Content_Is_Empty()
        {
            var parser = new HtmlParser();
            var html = "<html><head><meta name='keywords' content=''></head><body>Hi!</body></html>";
            var actual = parser.GetMetaKeyWordsFromHtml(html);
            var expected = 0;
            Assert.AreEqual(expected, actual.Count());
        }

        [TestMethod]
        public void GetMetaKeyWordsFromHtml_Should_Return_KeYwOrDs()
        {
            var parser = new HtmlParser();
            var html = "<html><head><meta name='KeYwOrDs' content='something'></head><body>Hi!</body></html>";
            var actual = parser.GetMetaKeyWordsFromHtml(html);
            var expected = 1;
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void GetMetaKeyWordsFromHtml_Should_Return_Distinct_MetaKeywords()
        {
            var parser = new HtmlParser();
            var html = "<html><head><meta name='keywords' content='something,SOMETHING,another,another'></head><body>Hi!</body></html>";
            var actual = parser.GetMetaKeyWordsFromHtml(html);
            var expected = 3;
            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetATagUrlsFromHtml_Should_Throw_On_Empty_Input()
        {
            var parser = new HtmlParser();
            parser.GetATagUrlsFromHtml(null);
        }

        [TestMethod]
        public void GetATagUrlsFromHtml_Should_Return_Empty_When_Input_Is_PlainText()
        {
            var parser = new HtmlParser();
            var html = "this is a test";
            var actual = parser.GetATagUrlsFromHtml(html);
            var expected = 0;
            Assert.AreEqual(expected, actual.Count());
        }

        [TestMethod]
        public void GetATagUrlsFromHtml_Should_Return_Distinct_Lower_Urls()
        {
            var parser = new HtmlParser();
            var html = "<a href='http://something'>1</a><a href='http://SOMETHING'>2</a><a href='http://something'>3</a>";
            var actual = parser.GetATagUrlsFromHtml(html);
            var expected = 1;
            Assert.AreEqual(expected, actual.Count);
        }
    }
}

