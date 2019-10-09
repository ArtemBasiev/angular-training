using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bizapps_test.MVC.Filters
{
    public class CustomExceptionHandlerAttribute: FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled &&
                filterContext.Exception is ApplicationException)
            {
                filterContext.Result =
                    new RedirectResult("~/ExceptionPages/Error.html");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}