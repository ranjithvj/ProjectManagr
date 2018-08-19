using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryInterfaces
{
    public interface IProjectSiteRepository : IRepository<ProjectSite>
    {
        List<ProjectSite> GetWithFilter(Expression<Func<ProjectSite, bool>> request);
        void SoftDelete(List<int> id, string deletedBy);
        ProjectSite InsertWithReturn(ProjectSite item);
        ProjectSite UpdateWithReturn(ProjectSite item);
    }
}