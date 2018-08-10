using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                Project item = new Project { Id = id };
                context.Projects.Attach(item);
                context.Projects.Remove(item);
                context.SaveChanges();
            }
        }

        List<Project> IRepository<Project>.GetAll()
        {
            List<Project> items;
            using (var context = new PmDbContext())
            {
                items = context.Projects.ToList();
                return items;
            }
        }

        Project IRepository<Project>.Get(int id)
        {
            Project returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.Projects.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public void Insert(Project item)
        {
            using (var context = new PmDbContext())
            {
                context.Projects.Add(item);
                context.SaveChanges();
            }
        }

        public void Update(Project item)
        {
            using (var context = new PmDbContext())
            {
                Project originalItem = context.Projects.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}