using Ninject.Modules;
using bizapps_test.MVC.Interfaces;
using bizapps_test.MVC.Services.WcfServices;
using bizapps_test.MVC.Services.WebApiServices;
using bizapps_test.MVC.Infrastructure.Abstract;
using bizapps_test.MVC.Infrastructure.Concrete;
using bizapps_test.MVC.Services.AsmxServices;

namespace bizapps_test.MVC.Util
{
    public class MvcServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<WebApiCategoryService>().InTransientScope();
            Bind<IPostService>().To<WebApiPostService>().InTransientScope(); 
            Bind<ICommentService>().To<WebApiCommentService>().InTransientScope();
            Bind<IBlogUserService>().To<WebApiBlogUserService>().InTransientScope();
            Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}