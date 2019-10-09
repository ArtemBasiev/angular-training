using bizapps_test.BLL.Interfaces;
using bizapps_test.BLL.Services;
using Ninject.Modules;

namespace bizapps_test.SL.ASMX_Services.Infrastructure
{
    public class AsmxServiceModule: NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>().InTransientScope();
            Bind<IPostService>().To<PostService>().InTransientScope();
            Bind<IBlogUserService>().To<BlogUserService>().InTransientScope();
            Bind<ICommentService>().To<CommentService>().InTransientScope();
        }
    }
}