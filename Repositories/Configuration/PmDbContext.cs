using Models;
using Models.Shared;
using System.Data.Entity;

namespace Repositories
{
    public class PmDbContext : DbContext
    {
        public PmDbContext() : base(Constants.ConnectionStrings.LocalSQLExpressConnectionString)
        {

        }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Screen> Screens { get; set; }

        public DbSet<Allocation> Allocations { get; set; }

        public DbSet<Task> Tasks { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Task>().HasRequired(c => c.Resource).WithMany().WillCascadeOnDelete(false);
        }
    }
}
