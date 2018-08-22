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
    public class DepartmentRepository : IDepartmentRepository
    {
        private CacheHelper _cacheHelper;
        public DepartmentRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                Department item = new Department { Id = id };
                context.Departments.Attach(item);
                context.Departments.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<Department>(item.Id, this.GetValuesFromDB(context));
            }
        }

        public List<Department> GetAll()
        {
            List<Department> items;

            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<Department>(this.GetValuesFromDB(context));
            }

            return items;
        }

        public Department Get(int id)
        {
            Department returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<Department>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(Department item)
        {
            using (var context = new PmDbContext())
            {
                context.Departments.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<Department>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public void Update(Department item)
        {
            using (var context = new PmDbContext())
            {
                Department originalItem = context.Departments.FirstOrDefault(x => x.Id == item.Id);
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
                List<Department> allItems = context.Departments.ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allItems.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }
    }
}