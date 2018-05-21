using SeoAnalyzerApp.BusinessLogic;
using SeoAnalyzerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeoAnalyzerApp.Controllers
{
    public class HomeController : Controller
    {
        private ISeoAnalyzer seoAnalyzer;
        public HomeController(ISeoAnalyzer analyzer)
        {
            seoAnalyzer = analyzer;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AnalyzeText(TextAnalysisInputModel model)
        {
            var result = new AnalysisResultViewModel() { Options = model.Options };

            if (model.Options.NumberOfWords)
                result.NumberOfWords = seoAnalyzer.GetWordOccurancesFromText(model.Text)
                    .Select(x => new WordCountModel() { Word = x.Key, Count = x.Value }).ToList();

            if (model.Options.NumberOfMetaKeywords)
                result.NumberOfMetaKeywords = seoAnalyzer.GetMetaKeywordWordOccurancesFromText(model.Text)
                    .Select(x => new WordCountModel() { Word = x.Key, Count = x.Value }).ToList();

            if (model.Options.ExternalLinks)
                result.ExternalLinks = seoAnalyzer.GetExternalUrlsFromText(model.Text)
                    .Select(x => new LinkModel() { Link = x }).ToList();

            return PartialView("AnalysisResult", result);
        }

        [HttpPost]
        public ActionResult AnalyzeUrl(UrlAnalysisInputModel model)
        {
            var result = new AnalysisResultViewModel() { Options = model.Options };

            if (model.Options.NumberOfWords)
                result.NumberOfWords = seoAnalyzer.GetWordOccurancesFromUrl(model.Url)
                    .Select(x => new WordCountModel() { Word = x.Key, Count = x.Value }).ToList();

            if (model.Options.NumberOfMetaKeywords)
                result.NumberOfMetaKeywords = seoAnalyzer.GetMetaKeywordWordOccurancesFromUrl(model.Url)
                    .Select(x => new WordCountModel() { Word = x.Key, Count = x.Value }).ToList();

            if (model.Options.ExternalLinks)
                result.ExternalLinks = seoAnalyzer.GetExternalUrlsFromUrl(model.Url)
                    .Select(x => new LinkModel() { Link = x }).ToList();

            return PartialView("AnalysisResult", result);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}