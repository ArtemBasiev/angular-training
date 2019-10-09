using System;
using bizapps_test.BLL.Infrastructure;
using bizapps_test.SL.Wcf_Services.Infrastructure;
using Ninject;
using Ninject.Web.Common.WebHost;


namespace bizapps_test.SL.Wcf_Services
{
    public class Global : NinjectHttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
           
        }

        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new WcfServiceModule(), new ServiceModule());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}