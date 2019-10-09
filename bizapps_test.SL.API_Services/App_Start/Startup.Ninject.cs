using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using bizapps_test.BLL.Infrastructure;
using bizapps_test.BLL.Services;
using bizapps_test.SL.API_Services.Owin;
using bizapps_test.SL.API_Services.Providers;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace bizapps_test.SL.API_Services.App_Start
{
    public partial class Startup
    {
        public IKernel ConfigureNinject(IAppBuilder app, HttpServer server)
        {
           
            var kernel = CreateKernel();
            app.UseNinjectMiddleware(() => kernel);
            app.UseNinjectWebApi(server);

            return kernel;
        }

        public IKernel CreateKernel()
        {
            NinjectModule bllModule = new ServiceModule();
            var kernel = new StandardKernel(bllModule);
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }

        public class NinjectConfig : NinjectModule
        {
            public override void Load()
            {
                RegisterServices();
            }

            private void RegisterServices()
            {
                Bind<IOAuthAuthorizationServerOptions>()
                    .To<CustomOAuthAuthorizationServerOptions>().InRequestScope();
                Bind<IOAuthAuthorizationServerProvider>()
                    .To<ApplicationOAuthProvider>().InRequestScope();

            }
        }
    }

}