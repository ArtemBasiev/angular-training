using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using bizapps_test.BLL.Infrastructure;
using bizapps_test.SL.API_Services.Infrastructure;
using Newtonsoft.Json;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;


namespace bizapps_test.SL.API_Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            
        }
    }
}
