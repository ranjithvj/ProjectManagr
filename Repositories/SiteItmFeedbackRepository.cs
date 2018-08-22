using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class SiteItmFeedbackRepository : ISiteItmFeedbackRepository
    {
        private CacheHelper _cacheHelper;
        public SiteItmFeedbackRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                SiteItmFeedback item = new SiteItmFeedback { Id = id };
                context.SiteItmFeedbacks.Attach(item);
                context.SiteItmFeedbacks.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<SiteItmFeedback>(item.Id, this.GetValuesFromDB(context));
            }
        }

        public List<SiteItmFeedback> GetAll()
        {
            List<SiteItmFeedback> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<SiteItmFeedback>(this.GetValuesFromDB(context));
            }
            return items;
        }

        public SiteItmFeedback Get(int id)
        {
            SiteItmFeedback returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<SiteItmFeedback>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(SiteItmFeedback item)
        {
            using (var context = new PmDbContext())
            {
                context.SiteItmFeedbacks.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<SiteItmFeedback>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public void Update(SiteItmFeedback item)
        {
            using (var context = new PmDbContext())
            {
                SiteItmFeedback originalItem = context.SiteItmFeedbacks.FirstOrDefault(x => x.Id == item.Id);
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
                List<SiteItmFeedback> allItems = context.SiteItmFeedbacks.ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allItems.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }
    }
}