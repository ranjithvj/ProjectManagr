using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
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
            }
        }

        List<Department> IRepository<Department>.GetAll()
        {
            List<Department> items;

            //Check in cache
            items = _cacheHelper.GetAll<Department>();

            if (items == null)
            {
                using (var context = new PmDbContext())
                {
                    items = context.Departments.ToList();

                    //Add in cache
                    items.ForEach(x => _cacheHelper.AddOrUpdate<Department>(x.Id, x));
                }
            }
            return items;
        }

        Department IRepository<Department>.Get(int id)
        {
            Department returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.Departments.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public void Insert(Department item)
        {
            using (var context = new PmDbContext())
            {
                context.Departments.Add(item);
                base.Insert<Department>(item, item.Id, context);
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
            }
        }
    }
}