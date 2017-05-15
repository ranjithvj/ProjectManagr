using System.Collections.Generic;

namespace ServiceInterfaces
{
    public interface ITransactionalEntityService<T>
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        void Insert(T item);

        void Delete(T item);

        void Update(T item);
    }
}