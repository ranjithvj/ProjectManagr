using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using Utilities;
using System;
using Models.DTO;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Repositories
{
    public class ProjectSiteRepository : IProjectSiteRepository
    {
        public ProjectSite Get(int id)
        {
            ProjectSite returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = IncludeAllChildForProjectSite(context)
                    .FirstOrDefault(x=>x.Id == id);
            }
            return returnValue;
        }

        public List<ProjectSite> GetAll()
        {
            List<ProjectSite> projectSites;
            using (var context = new PmDbContext())
            {
                projectSites = IncludeAllChildForProjectSite(context).ToList();
                return projectSites;
            }
        }

        public List<ProjectSite> GetWithFilter(Expression<Func<ProjectSite, bool>> request)
        {
            List<ProjectSite> query = new List<ProjectSite>();

            using (var context = new PmDbContext())
            {
                query = IncludeAllChildForProjectSite(context).Where(request).ToList();
            }

            return query;
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
            }
        }

        public ProjectSite InsertWithReturn(ProjectSite item)
        {
            ProjectSite insertedRecord = null;
            using (var context = new PmDbContext())
            {
                item.IsActive = true;
                item.CreatedDate = DateTime.UtcNow;
                item.ModifiedDate = DateTime.UtcNow;

                context.ProjectSites.Add(item);
                context.SaveChanges();
                insertedRecord = IncludeAllChildForProjectSite(context).FirstOrDefault(x => x.Id == item.Id);
            }

            return insertedRecord;
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
            }
        }

        public ProjectSite UpdateWithReturn(ProjectSite item)
        {
            ProjectSite updatedRecord = null;
            using (var context = new PmDbContext())
            {
                ProjectSite originalItem = context.ProjectSites.FirstOrDefault(x => x.Id == item.Id);
                Helper.TransferData(item, originalItem);
                originalItem.ModifiedDate = DateTime.UtcNow;
                context.Entry(originalItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                updatedRecord = IncludeAllChildForProjectSite(context).FirstOrDefault(x => x.Id == item.Id);
            }
            return updatedRecord;
        }


        public void Delete(int id)
        {
            using (var context = new PmDbContext())
            {
                ProjectSite item = new ProjectSite { Id = id };
                context.ProjectSites.Attach(item);
                context.ProjectSites.Remove(item);
                context.SaveChanges();
            }
        }

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
            }
        }
        #region Helper

        public IQueryable<ProjectSite> IncludeAllChildForProjectSite(PmDbContext context)
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

       

        #endregion
    }
}