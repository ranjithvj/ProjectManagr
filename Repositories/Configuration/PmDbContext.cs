using Models;
using Models.Shared;
using System.Data.Entity;

namespace Repositories
{
    public class PmDbContext : DbContext
    {
        public PmDbContext() : base(Constants.ConnectionStrings.LocalSQLExpressConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ProjectSite> ProjectSites { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EntityStatus> EntityStatuses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<SiteItmFeedback> SiteItmFeedbacks { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<SubPortfolio> SubPortfolios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
