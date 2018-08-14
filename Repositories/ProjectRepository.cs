using Models;
using Repositories.Cache;
using RepositoryInterfaces;
using System;
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

        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                Project item = new Project { Id = id };
                context.Projects.Attach(item);
                context.Projects.Remove(item);
                context.SaveChanges();
                _cacheHelper.DeleteItem<Project>(id);
            }
        }

        List<Project> IRepository<Project>.GetAll()
        {
            List<Project> items;

            //Check in cache
            items = _cacheHelper.GetAll<Project>();

            if (items == null)
            {
                using (var context = new PmDbContext())
                {
                    items = context.Projects.ToList();

                    //Add in cache
                    items.ForEach(x => _cacheHelper.AddOrUpdate<Project>(x.Id, x));
                }
            }
            return items;

        }

        Project IRepository<Project>.Get(int id)
        {
            Project returnValue;
            returnValue = _cacheHelper.GetById<Project>(id);
            if (returnValue == null)
            {
                using (var context = new PmDbContext())
                {
                    returnValue = context.Projects.FirstOrDefault(x => x.Id == id);
                    if (returnValue != null)
                        _cacheHelper.AddOrUpdate<Project>(id, returnValue);
                }
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
                _cacheHelper.AddOrUpdate<Project>(item.Id, item);
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
                _cacheHelper.AddOrUpdate<Project>(item.Id, item);
            }
        }
    }
}