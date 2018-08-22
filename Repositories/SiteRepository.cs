using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Data.Entity;
using System.Collections.Concurrent;
using System;
using Models.Shared;

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
                _cacheHelper.DeleteItem<Site>(item.Id, this.GetValuesFromDB(context));
            }
        }

        //Cache implemented only for GET ALL!!!
        public List<Site> GetAll()
        {
            List<Site> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<Site>(this.GetValuesFromDB(context));
            }
            return items;
        }

        public Site Get(int id)
        {
            Site returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<Site>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(Site item)
        {
            using (var context = new PmDbContext())
            {
                context.Sites.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<SiteItmFeedback>(item.Id, item, this.GetValuesFromDB(context));
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
                _cacheHelper.AddOrUpdate<SiteItmFeedback>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public Func<ConcurrentDictionary<int, object>> GetValuesFromDB(PmDbContext context)
        {
            return () =>
            {
                List<Site> allItems = context.Sites.ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allItems.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }

        public Site GetbyName(string name)
        {
            Site returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetOrAddAll<Site>(this.GetValuesFromDB(context))
                    .FirstOrDefault(x => string.Compare(x.Name, name) == 0);
            }
            return returnValue;
        }
    }
}