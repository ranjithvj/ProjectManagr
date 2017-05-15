using Ninject.Modules;
using ServiceInterfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionRoot
{
    public class DependencyMapper : NinjectModule
    {
        public override void Load()
        {
            #region Services

            this.Bind<IScreenService>().To<ScreenService>();

            #endregion

            #region Repositories

            this.Bind<IScreenRepository>().To<ScreenService>();

            #endregion
        }
    }
}
