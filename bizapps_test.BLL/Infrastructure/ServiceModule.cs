using bizapps_test.BLL.Logger.Abstract;
using bizapps_test.BLL.Logger.Concrete;
using bizapps_test.DAL.Repositories;
using bizapps_test.DAL.Repositories.Implementation.SQL;
using Ninject.Modules;

namespace bizapps_test.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogFactory>().To<FileSystemLogFactory>();
            Bind<IRepositoryFactory>().To<RepositoryFactory>().InTransientScope();
            Bind<IUnitOfWorkFactory>().To<AdoNetUnitOfWorkFactory>().InTransientScope();
        }
    }
}