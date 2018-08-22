using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ServiceInterfaces
{
    public interface IProjectSiteService : ITransactionalEntityService<ProjectSite>
    {
        List<ProjectSite> GetWithFilter(DateTime? start, DateTime? end, int siteId, int? entityStatusId);

        void SoftDelete(List<int> ids, string deletedBy);
        
    }
}