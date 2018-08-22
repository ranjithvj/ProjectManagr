using Models;
using Models.DTO;
using Models.Shared;
using RepositoryInterfaces;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services
{
    public class ProjectSiteService : IProjectSiteService
    {
        private readonly IProjectSiteRepository _projectSiteRepository;
        private readonly ISiteRepository _siteRepository;

        public ProjectSiteService(IProjectSiteRepository projectSiteRepository, ISiteRepository siteRepository)
        {
            _projectSiteRepository = projectSiteRepository;
            _siteRepository = siteRepository;
        }

        public void Delete(int id)
        {
            _projectSiteRepository.Delete(id);
        }

        public ProjectSite Get(int id)
        {
            return _projectSiteRepository.Get(id);
        }

        public List<ProjectSite> GetAll()
        {
            return _projectSiteRepository.GetAll();
        }


        public List<ProjectSite> GetWithFilter(DateTime? start, DateTime? end, int siteId, int? entityStatusId)
        {
            start = start.HasValue ? start.Value : DateTime.MinValue;
            end = end.HasValue ? end.Value : DateTime.MaxValue;
            List<int> siteIds = new List<int> { siteId };

            //Get the Site Id of the Global Site
            //We need to display the records for the site as well as the Global Sites

            Site globalSite = _siteRepository.GetbyName(Constants.Site.Global);

            if (globalSite != null)
            {
                siteIds.Add(globalSite.Id);
            }

            //Get data based on filter applied
            Func<ProjectSite, bool> filter = x => x.SiteEngagementStart >= start
                                            && x.SiteEngagementEnd <= end
                                            && siteIds.Contains(x.SiteId)
                                            && (!entityStatusId.HasValue ? true : entityStatusId.Value == 0 ? true : x.EntityStatusId == entityStatusId);

            return _projectSiteRepository.GetAll().Where(filter).ToList();

        }

        public void Insert(ProjectSite item)
        {
            _projectSiteRepository.Insert(item);
        }


        public void SoftDelete(List<int> ids, string deletedBy)
        {
            _projectSiteRepository.SoftDelete(ids, deletedBy);
        }

        public void Update(ProjectSite item)
        {
            _projectSiteRepository.Update(item);
        }

    }
}