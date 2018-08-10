using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class ApplicationTypeRepository : IApplicationTypeRepository
    {
        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                ApplicationType item = new ApplicationType { Id = id };
                context.ApplicationTypes.Attach(item);
                context.ApplicationTypes.Remove(item);
                context.SaveChanges();
            }
        }

        List<ApplicationType> IRepository<ApplicationType>.GetAll()
        {
            List<ApplicationType> items;
            using (var context = new PmDbContext())
            {
                items = context.ApplicationTypes.ToList();
                return items;
            }
        }

        ApplicationType IRepository<ApplicationType>.Get(int id)
        {
            ApplicationType returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.ApplicationTypes.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public void Insert(ApplicationType item)
        {
            using (var context = new PmDbContext())
            {
                context.ApplicationTypes.Add(item);
                context.SaveChanges();
            }
        }

        public void Update(ApplicationType item)
        {
            using (var context = new PmDbContext())
            {
                ApplicationType originalItem = context.ApplicationTypes.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}