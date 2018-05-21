using SeoAnalyzerApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeoAnalyzerApp
{
    public class MyHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            LogService.Instance.LogError(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}