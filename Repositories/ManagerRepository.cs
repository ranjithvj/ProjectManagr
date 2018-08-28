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
    public class ManagerRepository : IManagerRepository
    {
        private CacheHelper _cacheHelper;
        public ManagerRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                Manager item = new Manager { Id = id };
                context.Managers.Attach(item);
                context.Managers.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<Manager>(item.Id, this.GetValuesFromDB(context));
            }
        }

        public List<Manager> GetAll()
        {
            List<Manager> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<Manager>(this.GetValuesFromDB(context));
            }
            return items;
        }

        public Manager Get(int id)
        {
            Manager returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<Manager>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(Manager item)
        {
            using (var context = new PmDbContext())
            {
                context.Managers.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<Manager>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public void Update(Manager item)
        {
            using (var context = new PmDbContext())
            {
                Manager originalItem = context.Managers.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<Manager>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public Func<ConcurrentDictionary<int, object>> GetValuesFromDB(PmDbContext context)
        {
            return () =>
            {
                List<Manager> allItems = context.Managers.OrderBy(x=>x.Name).ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allItems.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }
    }
}