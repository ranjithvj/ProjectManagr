using Models;
using System.Collections.Generic;

namespace RepositoryInterfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        List<Country> GetCountriesWithSites();
    }
}