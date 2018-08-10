using Models;

namespace ServiceInterfaces
{
    public interface ICountryService : ITransactionalEntityService<Country>
    {
    }
}