using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoAnalyzerApp.BusinessLogic
{
    public class AppConfigSeoAnalyzerConfigProvider : ISeoAnalyzerConfigProvider
    {
        public List<string> StopWords
        {
            get
            {
                var words = System.Configuration.ConfigurationManager.AppSettings["StopWords"];
                if (!string.IsNullOrEmpty(words))
                    return words.Split(',').ToList();

                return new List<string>();
            }
        }
        public string WordRegexPattern
        {
            get
            {
                var regex = System.Configuration.ConfigurationManager.AppSettings["WordRegexPattern"];
                if (!string.IsNullOrEmpty(regex))
                    return regex;

                return @"\w+";
            }
        }
    }
}
