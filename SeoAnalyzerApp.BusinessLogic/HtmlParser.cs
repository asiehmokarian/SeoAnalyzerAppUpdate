using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzerApp.BusinessLogic
{
    /// <summary>
    /// This class uses HtmlAgilityPack to implement IHtmlParser to help parsing HTML.
    /// </summary>
    public class HtmlParser : IHtmlParser
    {
        /// <summary>
        /// Loads content from a url.
        /// </summary>
        /// <param name="url">Url of a page to get the content.</param>
        /// <returns>Html content of the page.</returns>
        public string GetContentFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));

            return new HtmlWeb().Load(url).Text;
        }

        /// <summary>
        /// Get plain text of the given html. The method strips script and style tags.
        /// </summary>
        /// <param name="html">The html to convert to plain text.</param>
        /// <returns>Plain text of the html.</returns>
        public string GetTextFromHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                throw new ArgumentNullException(nameof(html));

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//text()[not(parent::script or parent::style)]");
            if (nodes == null)
                return null;
            return HttpUtility.HtmlDecode(string.Join(" ", nodes.Select(x => x.InnerText)));
        }

        /// <summary>
        /// Gets list of keywords from meta keywords tag.
        /// </summary>
        /// <param name="html">The html to get list of keywords from its meta keywords tag.</param>
        /// <returns>A list of keywords. If there is no keywords, it returns and empty list.</returns>
        public List<string> GetMetaKeyWordsFromHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                throw new ArgumentNullException(nameof(html));

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//meta[translate(@name,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')='keywords' and @content]");
            if (nodes == null)
                return new List<string>();

            var result = nodes.Select(n => n.Attributes["content"].Value)
                              .Where(s => !string.IsNullOrWhiteSpace(s))
                              .SelectMany(s => s.Split(','))
                              .Select(s => HttpUtility.HtmlDecode(s.Trim()));

            return result.Distinct().ToList();
        }

        /// <summary>
        /// Get a list of 'a' tag 'href' attributes.
        /// </summary>
        /// <param name="html">The html to extract links from.</param>
        /// <returns>A list of lowered urls. If there is no url, it returns an empty list.</returns>
        public List<string> GetATagUrlsFromHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                throw new ArgumentNullException(nameof(html));

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//a[@href]");
            if (nodes == null)
                return new List<string>();

            var result = nodes.Select(n => n.Attributes["href"].Value)
                  .Where(s => !string.IsNullOrWhiteSpace(s))
                  .Select(x => x.ToLower());

            return result.Distinct().ToList();
        }
    }
}