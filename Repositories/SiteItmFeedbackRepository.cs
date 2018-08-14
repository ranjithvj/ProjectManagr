using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Repositories
{
    public class SiteItmFeedbackRepository : ISiteItmFeedbackRepository
    {
        private CacheHelper _cacheHelper;
        public SiteItmFeedbackRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                SiteItmFeedback item = new SiteItmFeedback { Id = id };
                context.SiteItmFeedbacks.Attach(item);
                context.SiteItmFeedbacks.Remove(item);
                context.SaveChanges();
            }
        }

        List<SiteItmFeedback> IRepository<SiteItmFeedback>.GetAll()
        {
            List<SiteItmFeedback> items;

            //Check in cache
            items = _cacheHelper.GetAll<SiteItmFeedback>();

            if (items == null)
            {
                using (var context = new PmDbContext())
                {
                    items = context.SiteItmFeedbacks.ToList();

                    //Add in cache
                    items.ForEach(x => _cacheHelper.AddOrUpdate<SiteItmFeedback>(x.Id, x));
                }
            }
            return items;
        }

        SiteItmFeedback IRepository<SiteItmFeedback>.Get(int id)
        {
            SiteItmFeedback returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = context.SiteItmFeedbacks.FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public void Insert(SiteItmFeedback item)
        {
            using (var context = new PmDbContext())
            {
                context.SiteItmFeedbacks.Add(item);
                context.SaveChanges();
            }
        }

        public void Update(SiteItmFeedback item)
        {
            using (var context = new PmDbContext())
            {
                SiteItmFeedback originalItem = context.SiteItmFeedbacks.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}