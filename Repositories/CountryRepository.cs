using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System.Data.Entity;
using System;

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
            }
        }

        List<Country> IRepository<Country>.GetAll()
        {
            List<Country> items;

            //Check in cache
            items = _cacheHelper.GetAll<Country>();

            if (items == null)
            {
                using (var context = new PmDbContext())
                {
                    items = context.Countries.Include(x => x.Site)
                        .ToList();

                    //Add in cache
                    items.ForEach(x => _cacheHelper.AddOrUpdate<Country>(x.Id, x));
                }
            }
            return items;
        }

        Country IRepository<Country>.Get(int id)
        {
            Country returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.Countries.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public void Insert(Country item)
        {
            using (var context = new PmDbContext())
            {
                context.Countries.Add(item);
                context.SaveChanges();
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
            }
        }

        public List<Country> GetCountriesWithSites()
        {
            List<Country> items;

            //Check in cache
            items = _cacheHelper.GetAll<Country>()
                     ?.Where(x => x.Site != null && x.Site.Count > 0)
                     ?.ToList();

            if (items == null)
            {
                using (var context = new PmDbContext())
                {
                    items = context.Countries.Include(x => x.Site)
                        .ToList();

                    //Add all items in cache to avoid losing data which got filtered
                    items.ForEach(x => _cacheHelper.AddOrUpdate<Country>(x.Id, x));

                    items = _cacheHelper.GetAll<Country>()
                     .Where(x => x.Site != null && x.Site.Count > 0)
                     .ToList();
                }
            }
            return items;
        }
    }
}