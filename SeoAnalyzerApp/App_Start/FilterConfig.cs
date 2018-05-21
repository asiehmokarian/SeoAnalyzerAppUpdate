using System.Web;
using System.Web.Mvc;

namespace SeoAnalyzerApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyHandleErrorAttribute());
        }
    }
}
