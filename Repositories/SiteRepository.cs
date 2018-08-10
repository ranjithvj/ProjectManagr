using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class SiteRepository : ISiteRepository
    {
        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                Site item = new Site { Id = id };
                context.Sites.Attach(item);
                context.Sites.Remove(item);
                context.SaveChanges();
            }
        }

        List<Site> IRepository<Site>.GetAll()
        {
            List<Site> items;
            using (var context = new PmDbContext())
            {
                items = context.Sites.ToList();
                return items;
            }
        }

        Site IRepository<Site>.Get(int id)
        {
            Site returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.Sites.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public void Insert(Site item)
        {
            using (var context = new PmDbContext())
            {
                context.Sites.Add(item);
                context.SaveChanges();
            }
        }

        public void Update(Site item)
        {
            using (var context = new PmDbContext())
            {
                Site originalItem = context.Sites.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}