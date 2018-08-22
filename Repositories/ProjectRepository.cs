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
    public class ProjectRepository : IProjectRepository
    {
        private CacheHelper _cacheHelper;
        public ProjectRepository()
        {
            _cacheHelper = CacheHelper.GetInstance();
        }

        //Do not use this.
        //Need to softdelete
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        //Set IsActive to False in DB
        //Remove the record from Cache instead of updating the cache
        public void SoftDelete(int id, string deletedBy)
        {
            using (var context = new PmDbContext())
            {
                ProjectSite item = context.ProjectSites.FirstOrDefault(x => x.Id == id);

                item.IsActive = false;
                item.ModifiedDate = DateTime.UtcNow;
                item.ModifiedBy = deletedBy;
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                //Remove items from Cache
                _cacheHelper.DeleteItem<ProjectSite>(id, this.GetValuesFromDB(context));
            }
        }

        public List<Project> GetAll()
        {
            List<Project> items;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                items = _cacheHelper.GetOrAddAll<Project>(this.GetValuesFromDB(context));
            }

            return items;
        }

        public Project Get(int id)
        {
            Project returnValue;
            using (var context = new PmDbContext())
            {
                //Check in cache, otherwise send a cache miss delegate
                returnValue = _cacheHelper.GetById<Project>(id, this.GetValuesFromDB(context));
            }
            return returnValue;
        }

        public void Insert(Project item)
        {
            using (var context = new PmDbContext())
            {
                item.IsActive = true;
                item.CreatedDate = DateTime.UtcNow;
                item.ModifiedDate = DateTime.UtcNow;
                context.Projects.Add(item);
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<Project>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public void Update(Project item)
        {
            using (var context = new PmDbContext())
            {
                Project originalItem = context.Projects.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                _cacheHelper.AddOrUpdate<Project>(item.Id, item, this.GetValuesFromDB(context));
            }
        }

        public Func<ConcurrentDictionary<int, object>> GetValuesFromDB(PmDbContext context)
        {
            return () =>
            {
                List<Project> allProjects = context.Projects.Where(x => x.IsActive).ToList();

                ConcurrentDictionary<int, object> typeRecords = new ConcurrentDictionary<int, object>();
                allProjects.ForEach(item => typeRecords.TryAdd(item.Id, item));

                return typeRecords;
            };
        }
    }
}