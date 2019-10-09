using bizapps_test.BLL.Services;
using bizapps_test.BLL.Services.Implementation;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.WebApi.OwinHost;

namespace bizapps_test.SL.API_Services.Infrastructure
{
    public class WebApiServiceModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IPostService>().To<PostService>().InRequestScope();
            Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            Bind<ICommentService>().To<CommentService>().InRequestScope();
            Bind<IUserService>().To<UserService>().InRequestScope();
            Bind<IBlogService>().To<BlogService>().InRequestScope();
        }
    }
}