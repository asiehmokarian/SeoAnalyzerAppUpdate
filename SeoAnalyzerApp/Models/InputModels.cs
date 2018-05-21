using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeoAnalyzerApp.Models
{
    public class OptionsModel
    {
        public OptionsModel()
        {
            NumberOfWords = true;
            NumberOfMetaKeywords = true;
            ExternalLinks = true;
        }
        [Display(Name = "NumberOfWords", ResourceType = typeof(StringResources))]
        public bool NumberOfWords { get; set; }
        [Display(Name = "NumberOfMetaKeywords", ResourceType = typeof(StringResources))]
        public bool NumberOfMetaKeywords { get; set; }
        [Display(Name = "ExternalLinks", ResourceType = typeof(StringResources))]
        public bool ExternalLinks { get; set; }
    }

    public class AnalysisInputModel
    {
        public AnalysisInputModel()
        {
            Options = new OptionsModel();
        }
        [Display(Name = "Options", ResourceType = typeof(StringResources))]
        public OptionsModel Options { get; set; }
    }

    public class TextAnalysisInputModel: AnalysisInputModel
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessageResourceName = "RequiredValidationMessage", ErrorMessageResourceType = typeof(StringResources))]
        [Display(Name = "Text", ResourceType = typeof(StringResources))]
        public string Text { get; set; }

    }

    public class UrlAnalysisInputModel : AnalysisInputModel
    {
        [Url(ErrorMessageResourceName = "UrlValidationMessage", ErrorMessageResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceName = "RequiredValidationMessage", ErrorMessageResourceType = typeof(StringResources))]
        [Display(Name = "Url", ResourceType = typeof(StringResources))]
        public string Url { get; set; }
    }
}