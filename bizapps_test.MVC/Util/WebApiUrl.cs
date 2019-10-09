using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using bizapps_test.MVC.CustomConfigSection;

namespace bizapps_test.MVC.Util
{
    public static class WebApiUrl
    {
        public static string GetWebApiUrl()
        {   
            return  ConfigurationManager.AppSettings["UrlPart"];
        }
    }
}