using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using Repositories.Cache;
using System.Collections.Concurrent;
using System;

namespace Repositories
{
    public class EntityStatusRepository : IEntityStatusRepository
    {
        private CacheHelper _cacheHelper;
        public EntityStatusRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }
        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                EntityStatus item = new EntityStatus { Id = id };
                context.EntityStatuses.Attach(item);
                context.EntityStatuses.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<EntityStatus>(item.Id, this.GetValuesFromDB(context));
            }
        }

        public List<EntityStatus> GetAll()
        {
            List<EntityStatus> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<EntityStatus>(this.GetValuesFromDB(context));
            }

            return items;
        }

        public EntityStatus Get(int id)
        {
            EntityStatus returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<EntityStatus>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(EntityStatus item)
        {
            using (var context = new PmDbContext())
            {
                context.EntityStatuses.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<EntityStatus>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public void Update(EntityStatus item)
        {
            using (var context = new PmDbContext())
            {
                EntityStatus originalItem = context.EntityStatuses.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<EntityStatus>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public Func<ConcurrentDictionary<int, object>> GetValuesFromDB(PmDbContext context)
        {
            return () =>
            {
                List<EntityStatus> allItems = context.EntityStatuses.ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allItems.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }
    }
}