using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Data.Entity;
using System;
using System.Collections.Concurrent;

namespace Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private CacheHelper _cacheHelper;
        public CountryRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                Country item = new Country { Id = id };
                context.Countries.Attach(item);
                context.Countries.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<Country>(id, this.GetValuesFromDB(context));
            }
        }

        public List<Country> GetAll()
        {
            List<Country> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<Country>(this.GetValuesFromDB(context));
            }
            return items;
        }

        public Country Get(int id)
        {
            Country returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<Country>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(Country item)
        {
            using (var context = new PmDbContext())
            {
                context.Countries.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<Country>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public void Update(Country item)
        {
            using (var context = new PmDbContext())
            {
                Country originalItem = context.Countries.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<Country>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public List<Country> GetCountriesWithSites()
        {
            List<Country> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<Country>(this.GetValuesFromDB(context))
                        .Where(x => x.Site != null && x.Site.Count > 0)
                        .ToList();
            }
            return items;
        }

        public Func<ConcurrentDictionary<int, object>> GetValuesFromDB(PmDbContext context)
        {
            return () =>
            {
                List<Country> allCountries = context.Countries.Include(x => x.Site).ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allCountries.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }
    }
}