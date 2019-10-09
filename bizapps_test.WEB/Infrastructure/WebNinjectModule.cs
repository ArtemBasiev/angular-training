using bizapps_test.BLL.Interfaces;
using bizapps_test.BLL.Services;
using Ninject.Modules;

namespace bizapps_test.WEB.Infrastructure
{
    public class WebNinjectModule: NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>().InTransientScope();
            Bind<IBlogUserService>().To<BlogUserService>().InTransientScope();
            Bind<IPostService>().To<PostService>().InTransientScope();
            Bind<ICommentService>().To<CommentService>().InTransientScope();
        }
       
    }
}