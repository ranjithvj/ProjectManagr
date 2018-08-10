using Models;
using Models.DTO;

namespace ServiceInterfaces
{
    public interface IProjectSiteService : ITransactionalEntityService<ProjectSite>
    {
        FilterResponseDTO<ProjectSite> GetWithFilter(FilterRequestDTO request);

        void SoftDelete(int id, string deletedBy);
    }
}