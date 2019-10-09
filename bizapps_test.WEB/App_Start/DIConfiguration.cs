using bizapps_test.BLL.Services;
using bizapps_test.BLL.Interfaces;

using Ninject;

namespace bizapps_test.WEB.App_Start
{
    public static class DiConfiguration
    {
        public static void SetupDi(IKernel kernel)
        {
            kernel.Bind<ICategoryService>().To<CategoryService>().InTransientScope();
            kernel.Bind<IBlogUserService>().To<BlogUserService>().InTransientScope();
            kernel.Bind<IPostService>().To<PostService>().InTransientScope();
            kernel.Bind<ICommentService>().To<CommentService>().InTransientScope();
        }
    }
}