using Models;
using Models.DTO;
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

        public List<ProjectSite> GetWithFilter(Expression<Func<ProjectSite, bool>> request)
        {
            return _projectSiteRepository.GetWithFilter(request);
        }

        public void Insert(ProjectSite item)
        {
            _projectSiteRepository.Insert(item);
        }

        public ProjectSite InsertWithReturn(ProjectSite item)
        {
            return _projectSiteRepository.InsertWithReturn(item);
        }

        public ProjectSite UpdateWithReturn(ProjectSite item)
        {
            return _projectSiteRepository.UpdateWithReturn(item);
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