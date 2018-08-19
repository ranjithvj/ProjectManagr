using Models;
using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface ICountryService : ITransactionalEntityService<Country>
    {
        List<Country> GetCountriesWithSites();
    }
}