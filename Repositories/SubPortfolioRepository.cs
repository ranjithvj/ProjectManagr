﻿using Models;
using Repositories.Cache;
using RepositoryInterfaces;
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
            }
        }

        List<SubPortfolio> IRepository<SubPortfolio>.GetAll()
        {
            List<SubPortfolio> items;

            //Check in cache
            items = _cacheHelper.GetAll<SubPortfolio>();

            if (items == null)
            {
                using (var context = new PmDbContext())
                {
                    items = context.SubPortfolios.ToList();

                    //Add in cache
                    items.ForEach(x => _cacheHelper.AddOrUpdate<SubPortfolio>(x.Id, x));
                }
            }
            return items;
        }

        SubPortfolio IRepository<SubPortfolio>.Get(int id)
        {
            SubPortfolio returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.SubPortfolios.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public void Insert(SubPortfolio item)
        {
            using (var context = new PmDbContext())
            {
                context.SubPortfolios.Add(item);
                context.SaveChanges();
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
            }
        }
    }
}