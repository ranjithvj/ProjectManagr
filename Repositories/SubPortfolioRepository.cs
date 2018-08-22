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
    public class SubPortfolioRepository : ISubPortfolioRepository
    {
        private CacheHelper _cacheHelper;
        public SubPortfolioRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                SubPortfolio item = new SubPortfolio { Id = id };
                context.SubPortfolios.Attach(item);
                context.SubPortfolios.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<SubPortfolio>(item.Id, this.GetValuesFromDB(context));
            }
        }

        public List<SubPortfolio> GetAll()
        {
            List<SubPortfolio> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<SubPortfolio>(this.GetValuesFromDB(context));
            }
            return items;
        }

        public SubPortfolio Get(int id)
        {
            SubPortfolio returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<SubPortfolio>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(SubPortfolio item)
        {
            using (var context = new PmDbContext())
            {
                context.SubPortfolios.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<SubPortfolio>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public void Update(SubPortfolio item)
        {
            using (var context = new PmDbContext())
            {
                SubPortfolio originalItem = context.SubPortfolios.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<SubPortfolio>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public Func<ConcurrentDictionary<int, object>> GetValuesFromDB(PmDbContext context)
        {
            return () =>
            {
                List<SubPortfolio> allItems = context.SubPortfolios.ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allItems.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }
    }
}