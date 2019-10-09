//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(bizapps_test.SL.ASMX_Services.App_Start.NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(bizapps_test.SL.ASMX_Services.App_Start.NinjectWebCommon), "Stop")]

//namespace bizapps_test.SL.ASMX_Services.App_Start
//{
//    using System;
//    using System.Web;
//    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
//    using Ninject;
//    using Ninject.Web.Common;
//    using Ninject.Web.Common.WebHost;
//    using bizapps_test.BLL.Infrastructure;
//    using bizapps_test.SL.ASMX_Services.Infrastructure;

//    public static class NinjectWebCommon 
//    {
//        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

//        public static void Start() 
//        {
//            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
//            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
//            bootstrapper.Initialize(CreateKernel);
//        }
        

//        public static void Stop()
//        {
//            bootstrapper.ShutDown();
//        }
        
//        private static IKernel CreateKernel()
//        {
//            var kernel = new StandardKernel(new AsmxServiceModule(), new ServiceModule());
//            try
//            {
//                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
//                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
//                RegisterServices(kernel);
//                return kernel;
//            }
//            catch
//            {
//                kernel.Dispose();
//                throw;
//            }
//        }

//        private static void RegisterServices(IKernel kernel)
//        {
//        }        
//    }
//}