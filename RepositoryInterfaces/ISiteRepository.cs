using Models;

namespace RepositoryInterfaces
{
    public interface ISiteRepository : IRepository<Site>
    {
        Site GetbyName(string name);
    }
}