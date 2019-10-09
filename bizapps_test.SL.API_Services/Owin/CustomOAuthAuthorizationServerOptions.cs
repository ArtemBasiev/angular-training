using System;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace bizapps_test.SL.API_Services.Owin
{
    public class CustomOAuthAuthorizationServerOptions: IOAuthAuthorizationServerOptions
    {
        private IOAuthAuthorizationServerProvider _provider;

        public CustomOAuthAuthorizationServerOptions(IOAuthAuthorizationServerProvider provider)
        {
            _provider = provider;
        }

        public OAuthAuthorizationServerOptions GetOptions()
        {
            return new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = _provider
                
            };
        }
    }
}