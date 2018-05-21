using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeoAnalyzerApp.Common
{
    public class FileLogger : ILogger
    {
        public string FileName { get; set; }

        private void WriteFile(string message)
        {
            try
            {
                System.IO.File.AppendAllLines(FileName, new string[] { message });
            }
            catch (Exception)
            {
            }
        }
        public void LogError(Exception ex)
        {
            WriteFile(ex.ToString());
        }

        public void LogInfo(string info)
        {
            WriteFile(info);
        }
    }
}
