using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class ScreenRepository : IScreenRepository
    {
        public ScreenRepository()
        {
        }

        public Screen Get(int id)
        {
            Screen returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.Screens.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public List<Screen> GetAll()
        {
            IQueryable<Screen> screens;
            using (var context = new PmDbContext())
            {
                screens = context.Screens;
                return screens.ToList();
            }
        }

        public void Insert(Screen item)
        {
            using (var context = new PmDbContext())
            {
                context.Screens.Add(item);
                context.SaveChanges();
            }
        }

        public void Update(Screen item)
        {
            using (var context = new PmDbContext())
            {
                Screen originalItem = context.Screens.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Screen item)
        {
            using (var context = new PmDbContext())
            {
                Screen itemToDelete = context.Screens.FirstOrDefault(x => x.Id == item.Id);
                context.Screens.Remove(itemToDelete);
                context.SaveChanges();
            }
        }
    }
}