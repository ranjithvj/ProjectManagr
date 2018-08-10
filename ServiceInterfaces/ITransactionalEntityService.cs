using Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ServiceInterfaces
{
    public interface ITransactionalEntityService<T>
    {
        List<T> GetAll();

        T Get(int id);

        void Insert(T item);

        void Delete(int id);

        void Update(T item);
    }
}