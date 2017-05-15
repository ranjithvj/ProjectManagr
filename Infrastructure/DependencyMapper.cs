using Ninject.Modules;
using Repositories;
using RepositoryInterfaces;
using ServiceInterfaces;
using Services;

namespace CompositionRoot
{
    public class DependencyMapper : NinjectModule
    {
        public override void Load()
        {
            #region Services

            this.Bind<IScreenService>().To<ScreenService>();

            #endregion Services

            #region DataAccess

            this.Bind<IScreenRepository>().To<ScreenRepository>();

            #endregion DataAccess
        }
    }
}