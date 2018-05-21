using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoAnalyzerApp.Common
{
    public class LogService
    {
        private static ILogger instance = new FileLogger() { FileName=System.Configuration.ConfigurationManager.AppSettings["FileLogger.FileName"]};
        public static ILogger Instance {
            get
            {
                return instance;
            }
        }
    }
}
