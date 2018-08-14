using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using Utilities;
using System;
using Models.DTO;
using System.Data.Entity;

namespace Repositories
{
    public class ProjectSiteRepository : IProjectSiteRepository
    {
        public ProjectSite Get(int id)
        {
            ProjectSite returnValue;
            using (var context = new PmDbContext())
            {
                returnValue = IncludeAllChild(context)
                    .FirstOrDefault(x => x.IsActive && x.Id == id);
            }
            return returnValue;
        }

        public List<ProjectSite> GetAll()
        {
            List<ProjectSite> projectSites;
            using (var context = new PmDbContext())
            {
                projectSites = IncludeAllChild(context).Where(x => x.IsActive).ToList();
                return projectSites;
            }
        }

        public FilterResponseDTO<ProjectSite> GetWithFilter(FilterRequestDTO request)
        {
            FilterResponseDTO<ProjectSite> response = new FilterResponseDTO<ProjectSite>();

            using (var context = new PmDbContext())
            {
                IQueryable<ProjectSite> query = IncludeAllChild(context).Where(x => x.IsActive);
                response.TotalCount = query.Count(); // Todo : Check performance

                //Search
                if (!string.IsNullOrEmpty(request.SearchText))
                {
                    query = query.Where(x => x.Project != null && x.Project.Name.Contains(request.SearchText));
                    //TODO: Confirm if this is enough or should you search using other columns as well
                }

                //Sort
                query = query.OrderBy(request.OrderByString == string.Empty ? "Project.Name asc" : request.OrderByString);
                //Todo: Need to do for all columns
                response.FilteredCount = query.Count();

                //Paging
                query = query.Skip(request.RecordCountStart).Take(request.RecordCountLength);
                response.Data = query.ToList();

            }

            return response;
        }

        public void Insert(ProjectSite item)
        {
            using (var context = new PmDbContext())
            {
                item.IsActive = true;
                item.CreatedDate = DateTime.UtcNow;
                item.ModifiedDate = DateTime.UtcNow;
                if(item.ProjectId==0)
                {
                    item.Project.IsActive = true;
                    item.Project.CreatedDate = DateTime.UtcNow;
                    item.Project.ModifiedDate = DateTime.UtcNow;
                }
                context.ProjectSites.Add(item);
                context.SaveChanges();
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
            }
        }

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
            }
        }
        #region Helper

        public IQueryable<ProjectSite> IncludeAllChild(PmDbContext context)
        {
            return context.ProjectSites
                    .Include(x => x.Project)
                    .Include(x => x.Project.SubPortfolio)
                    .Include(x => x.EntityStatus)
                    .Include(x => x.Site)
                    .Include(x => x.Country)
                    .Include(x => x.SiteItmFeedback)
                    .Include(x => x.Department)
                    .Include(x => x.ApplicationType);
        }

       
        #endregion
    }
}