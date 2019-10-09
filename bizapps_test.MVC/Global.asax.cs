using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Modules;
using bizapps_test.MVC.Util;
using Ninject;
using Ninject.Web.Mvc;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace bizapps_test.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            NinjectModule mvcModule = new MvcServiceModule();
            var kernel = new StandardKernel(mvcModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
            {
            }
            else
            {
                FormsAuthenticationTicket authTicket;
                try
                {
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch
                {
                    return;
                }

                // retrieve roles from UserData
                string[] roles = authTicket.UserData.Split(';');

                if (Context.User != null)
                    Context.User = new GenericPrincipal(Context.User.Identity, roles);
            }
        }
    }
}
