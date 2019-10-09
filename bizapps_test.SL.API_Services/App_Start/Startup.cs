using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using bizapps_test.SL.API_Services.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(bizapps_test.SL.API_Services.App_Start.Startup))]

namespace bizapps_test.SL.API_Services.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {    
            var config =  new HttpConfiguration();
            
            var server = new HttpServer(config);

            WebApiConfig.Register(config);

            var kernel = ConfigureNinject(app, server);

            ConfigureAuth(app, kernel);

            

            app.UseCors(CorsOptions.AllowAll);


            app.Map("/api", ap =>
            {
                ap.UseWebApi(server);
            });





        }


        private void ConfigureAuth(IAppBuilder app, IKernel kernel)
        {
            app.UseOAuthAuthorizationServer(
                kernel.Get<CustomOAuthAuthorizationServerOptions>().GetOptions());
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
