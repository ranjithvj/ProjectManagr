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
        void SoftDelete(List<int> id, string deletedBy);
    }
}