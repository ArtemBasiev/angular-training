using Ninject.Modules;
using bizapps_test.BLL.Interfaces;
using bizapps_test.BLL.Services;



namespace bizapps_test.SL.Wcf_Services.Infrastructure
{
    public class WcfServiceModule: NinjectModule
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