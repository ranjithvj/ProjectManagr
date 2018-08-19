using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Data.Entity;

namespace Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private CacheHelper _cacheHelper;
        public SiteRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

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

        //Cache implemented only for GET ALL!!!
        List<Site> IRepository<Site>.GetAll()
        {
            List<Site> items;

            //Check in cache
            items = _cacheHelper.GetAll<Site>();

            if (items == null)
            {
                using (var context = new PmDbContext())
                {
                    items = context.Sites
                        //.Include(x=>x.Country)
                        .ToList();

                    //Add in cache
                    items.ForEach(x => _cacheHelper.AddOrUpdate<Site>(x.Id, x));
                }
            }
            return items;
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