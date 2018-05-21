using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeoAnalyzerApp.Models
{
    public class WordCountModel
    {
        [Display(Name = "Word", ResourceType = typeof(StringResources))]
        public string Word { get; set; }
        [Display(Name = "Count", ResourceType = typeof(StringResources))]
        public int Count { get; set; }
    }

    public class LinkModel
    {
        [Url]
        [Display(Name = "Link", ResourceType = typeof(StringResources))]
        public string Link { get; set; }
    }

    public class AnalysisResultViewModel
    {
        public AnalysisResultViewModel()
        {
            Options = new OptionsModel();
            NumberOfWords = new List<WordCountModel>();
            NumberOfMetaKeywords = new List<WordCountModel>();
            ExternalLinks = new List<LinkModel>();
        }
        public OptionsModel Options { get; set; }

        [Display(Name = "NumberOfWords", ResourceType = typeof(StringResources))]
        public List<WordCountModel> NumberOfWords { get; set; }
        [Display(Name = "NumberOfMetaKeywords", ResourceType = typeof(StringResources))]
        public List<WordCountModel> NumberOfMetaKeywords { get; set; }
        [Display(Name = "ExternalLinks", ResourceType = typeof(StringResources))]
        public List<LinkModel> ExternalLinks { get; set; }
    }
}