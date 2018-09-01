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

            this.Bind<IProjectSiteService>().To<ProjectSiteService>();
            this.Bind<IEntityStatusService>().To<EntityStatusService>();
            this.Bind<IProjectService>().To<ProjectService>();
            this.Bind<IApplicationTypeService>().To<ApplicationTypeService>();
            this.Bind<ICountryService>().To<CountryService>();
            this.Bind<IDepartmentService>().To<DepartmentService>();
            this.Bind<ISiteItmFeedbackService>().To<SiteItmFeedbackService>();
            this.Bind<ISiteService>().To<SiteService>();
            this.Bind<ISubPortfolioService>().To<SubPortfolioService>();
            this.Bind<IManagerService>().To<ManagerService>();

            #endregion Services

            #region DataAccess

            this.Bind<IProjectSiteRepository>().To<ProjectSiteWithCacheRepository>();
            this.Bind<IEntityStatusRepository>().To<EntityStatusRepository>();
            this.Bind<IProjectRepository>().To<ProjectRepository>();
            this.Bind<IApplicationTypeRepository>().To<ApplicationTypeRepository>();
            this.Bind<ICountryRepository>().To<CountryRepository>();
            this.Bind<IDepartmentRepository>().To<DepartmentRepository>();
            this.Bind<ISiteItmFeedbackRepository>().To<SiteItmFeedbackRepository>();
            this.Bind<ISiteRepository>().To<SiteRepository>();
            this.Bind<ISubPortfolioRepository>().To<SubPortfolioRepository>();
            this.Bind<IManagerRepository>().To<ManagerRepository>();

            #endregion DataAccess
        }
    }
}