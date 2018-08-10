using Models;
using Models.DTO;
using System.Linq;

namespace RepositoryInterfaces
{
    public interface IProjectSiteRepository : IRepository<ProjectSite>
    {
        FilterResponseDTO<ProjectSite> GetWithFilter(FilterRequestDTO request);
        void SoftDelete(int id, string deletedBy);
    }
}