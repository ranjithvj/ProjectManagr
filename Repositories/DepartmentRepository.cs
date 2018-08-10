using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
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
            using (var context = new PmDbContext())
            {
                items = context.Departments.ToList();
                return items;
            }
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
                context.SaveChanges();
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