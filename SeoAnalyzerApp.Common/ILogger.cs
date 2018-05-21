using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoAnalyzerApp.Common
{
    public interface ILogger
    {
        void LogError(Exception ex);
        void LogInfo(string info);
    }
}
