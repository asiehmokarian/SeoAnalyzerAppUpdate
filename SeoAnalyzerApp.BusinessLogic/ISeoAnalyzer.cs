using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzerApp.BusinessLogic
{
    public interface ISeoAnalyzer
    {
        List<string> StopWords { get; set; }
        Dictionary<string, int> GetWordOccurancesFromText(string text);
        Dictionary<string, int> GetWordOccurancesFromUrl(string url);
        Dictionary<string, int> GetMetaKeywordWordOccurancesFromText(string text);
        Dictionary<string, int> GetMetaKeywordWordOccurancesFromUrl(string url);
        List<string> GetExternalUrlsFromText(string text);
        List<string> GetExternalUrlsFromUrl(string url);
    }
}