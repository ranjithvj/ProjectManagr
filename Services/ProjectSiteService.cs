using Models;
using Models.DTO;
using RepositoryInterfaces;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class ProjectSiteService : IProjectSiteService
    {
        private readonly IProjectSiteRepository _projectSiteRepository;

        public ProjectSiteService(IProjectSiteRepository projectSiteRepository)
        {
            _projectSiteRepository = projectSiteRepository;
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

        public FilterResponseDTO<ProjectSite> GetWithFilter(FilterRequestDTO request)
        {
            if(request==null)
            {
                //TODO: Log Error!
                return null;
            }

            return _projectSiteRepository.GetWithFilter(request);
        }

        public void Insert(ProjectSite item)
        {
            _projectSiteRepository.Insert(item);
        }

        public void SoftDelete(int id, string deletedBy)
        {
            _projectSiteRepository.SoftDelete(id, deletedBy);
        }

        public void Update(ProjectSite item)
        {
            _projectSiteRepository.Update(item);
        }

    }
}