using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ServiceInterfaces
{
    public interface IProjectSiteService : ITransactionalEntityService<ProjectSite>
    {
        List<ProjectSite> GetWithFilter(Expression<Func<ProjectSite, bool>> request);

        void SoftDelete(List<int> ids, string deletedBy);

        ProjectSite InsertWithReturn(ProjectSite item);
        ProjectSite UpdateWithReturn(ProjectSite item);
    }
}