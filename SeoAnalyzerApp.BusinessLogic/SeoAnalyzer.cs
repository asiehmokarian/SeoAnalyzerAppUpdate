using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SeoAnalyzerApp.BusinessLogic
{
    public class SeoAnalyzer : ISeoAnalyzer
    {
        public IHtmlParser HtmlParser { get; set; }
        public List<string> StopWords { get; set; }
        public string WordRegexPattern { get; set; }
        public SeoAnalyzer(IHtmlParser htmlParser, ISeoAnalyzerConfigProvider config = null)
        {
            if (htmlParser == null)
                throw new ArgumentNullException(nameof(htmlParser));
            HtmlParser = htmlParser;

            StopWords = new List<string>();
            WordRegexPattern = @"\w+";

            if (config != null)
            {
                StopWords = config.StopWords;
                WordRegexPattern = config.WordRegexPattern;
            }
        }
        protected string GetHtmlFromUrl(string url)
        {
            return HtmlParser.GetContentFromUrl(url);
        }
        protected string GetPlainText(string html)
        {
            return HtmlParser.GetTextFromHtml(html);
        }
        public bool IsAbsoluteUrl(string input)
        {
            Uri result;
            if (Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out result))
                return result.IsAbsoluteUri;
            return false;
        }
        public Dictionary<string, int> GetWordOccurancesFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            text = GetPlainText(text);

            var result = new Dictionary<string, int>();
            var matches = Regex.Matches(text, WordRegexPattern);
            foreach (Match word in matches)
            {
                var key = word.Value.ToLower();
                if (!StopWords.Contains(key, StringComparer.OrdinalIgnoreCase))
                    if (result.ContainsKey(key))
                        result[key]++;
                    else
                        result.Add(key, 1);
            }
            return result;
        }
        public Dictionary<string, int> GetWordOccurancesFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return GetWordOccurancesFromText(GetHtmlFromUrl(url));
        }
        public Dictionary<string, int> GetMetaKeywordWordOccurancesFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            var result = new Dictionary<string, int>();
            var keywords = HtmlParser.GetMetaKeyWordsFromHtml(text).Select(s => s.ToLower())
                .Distinct().ToList();

            if (keywords.Count() == 0)
                return result;

            text = GetPlainText(text).ToLower();

            var pattern = string.Join("|", keywords.Select(x => $"(\\b{x}\\b)"));
            var matches = Regex.Matches(text, pattern);

            foreach (var keyword in keywords)
                result.Add(keyword, 0);

            foreach (Match keyword in matches)
            {
                var key = keyword.Value;
                if (result.ContainsKey(key))
                    result[key]++;
                else
                    result.Add(key, 1);
            }

            return result;
        }
        public Dictionary<string, int> GetMetaKeywordWordOccurancesFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return GetMetaKeywordWordOccurancesFromText(GetHtmlFromUrl(url));
        }
        public List<string> GetExternalUrlsFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            return HtmlParser.GetATagUrlsFromHtml(text).Where(x => IsAbsoluteUrl(x)).ToList();
        }
        public List<string> GetExternalUrlsFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return GetExternalUrlsFromText(GetHtmlFromUrl(url));
        }
    }
}