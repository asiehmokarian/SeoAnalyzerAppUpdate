using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzerApp.BusinessLogic
{
    public interface IHtmlParser
    {
        List<string> GetATagUrlsFromHtml(string html);
        string GetContentFromUrl(string url);
        List<string> GetMetaKeyWordsFromHtml(string html);
        string GetTextFromHtml(string html);
    }
}