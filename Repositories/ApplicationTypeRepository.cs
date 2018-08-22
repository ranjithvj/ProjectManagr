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
    public class ApplicationTypeRepository : IApplicationTypeRepository
    {
        private CacheHelper _cacheHelper;
        public ApplicationTypeRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                ApplicationType item = new ApplicationType { Id = id };
                context.ApplicationTypes.Attach(item);
                context.ApplicationTypes.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<ApplicationType>(item.Id, this.GetValuesFromDB(context));
            }
        }

        public List<ApplicationType> GetAll()
        {
            List<ApplicationType> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<ApplicationType>(this.GetValuesFromDB(context));
            }

            return items;

        }

        public ApplicationType Get(int id)
        {
            ApplicationType returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<ApplicationType>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(ApplicationType item)
        {
            using (var context = new PmDbContext())
            {
                context.ApplicationTypes.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<ApplicationType>(item.Id, item, this.GetValuesFromDB(context));
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
                _cacheHelper.AddOrUpdate<ApplicationType>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public Func<ConcurrentDictionary<int, object>> GetValuesFromDB(PmDbContext context)
        {
            return () =>
            {
                List<ApplicationType> allItems = context.ApplicationTypes.ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allItems.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }
    }
}