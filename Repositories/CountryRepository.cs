﻿using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class CountryRepository : ICountryRepository
    {
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
            using (var context = new PmDbContext())
            {
                items = context.Countries.ToList();
                return items;
            }
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
    }
}