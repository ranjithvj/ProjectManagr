using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using Utilities;

namespace Repositories
{
    public class ProjectSiteWithCacheRepository : IProjectSiteRepository
    {
        private CacheHelper _cacheHelper;

        public ProjectSiteWithCacheRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        public ProjectSite Get(int id)
        {
            ProjectSite returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = _cacheHelper.GetOrAddAll<ProjectSite>(this.GetValuesFromDB(context)).FirstOrDefault(x => x.Id == id);
            }
            return returnValue;
        }

        public List<ProjectSite> GetAll()
        {
            List<ProjectSite> items;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<ProjectSite>(this.GetValuesFromDB(context));
            }
            return items;
        }


        public void Insert(ProjectSite item)
        {
            using (var context = new PmDbContext())
            {
                item.IsActive = true;
                item.CreatedDate = DateTime.UtcNow;
                item.ModifiedDate = DateTime.UtcNow;
                context.ProjectSites.Add(item);
                context.SaveChanges();

                //Retrieve all child data and add it to cache
                ProjectSite projectSite = IncludeAllChildForProjectSite(context).FirstOrDefault(x => x.Id == item.Id);
                _cacheHelper.AddOrUpdate<ProjectSite>(projectSite.Id, projectSite, this.GetValuesFromDB(context));
            }
        }

        public void Update(ProjectSite item)
        {
            using (var context = new PmDbContext())
            {
                ProjectSite originalItem = context.ProjectSites.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                originalItem.ModifiedDate = DateTime.UtcNow;
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                //Retrieve all child data and add it to cache
                ProjectSite projectSite = IncludeAllChildForProjectSite(context).FirstOrDefault(x => x.Id == originalItem.Id);
                _cacheHelper.AddOrUpdate<ProjectSite>(projectSite.Id, projectSite, this.GetValuesFromDB(context));
            }
        }

        public void Delete(int id)          
        {
            using (var context = new PmDbContext())
            {
                ProjectSite item = new ProjectSite { Id = id };
                context.ProjectSites.Attach(item);
                context.ProjectSites.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<ProjectSite>(id, this.GetValuesFromDB(context));
            }
        }

        //Set IsActive to False in DB
        //Remove the record from Cache instead of updating the cache
        public void SoftDelete(List<int> ids, string deletedBy)
        {
            using (var context = new PmDbContext())
            {
                List<ProjectSite> items = context.ProjectSites.Where(x => ids.Contains(x.Id)).ToList();
                foreach (ProjectSite item in items)
                {
                    item.IsActive = false;
                    item.ModifiedDate = DateTime.UtcNow;
                    item.ModifiedBy = deletedBy;
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                context.SaveChanges();

                //Remove items from Cache
                foreach (int id in ids)
                {
                    _cacheHelper.DeleteItem<ProjectSite>(id, this.GetValuesFromDB(context));
                }
            }
        }

        #region Helper

        private IQueryable<ProjectSite> IncludeAllChildForProjectSite(PmDbContext context)
        {
            return context.ProjectSites
                    .Include(x => x.Project)
                    .Include(x => x.Project.SubPortfolio)
                    .Include(x => x.EntityStatus)
                    .Include(x => x.Site)
                    .Include(x => x.Site.Country)
                    .Include(x => x.SiteItmFeedback)
                    .Include(x => x.Department)
                    .Include(x => x.ApplicationType)
                    .Where(x => x.IsActive);
        }

        public Func<ConcurrentDictionary<int, object>> GetValuesFromDB(PmDbContext context)
        {
            return () =>
            {
                List<ProjectSite> allItems = IncludeAllChildForProjectSite(context).ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allItems.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }

        public List<ProjectSite> GetWithFilter(Expression<Func<ProjectSite, bool>> request)
        {
            throw new NotImplementedException();
        }

        #endregion Helper
    }
}