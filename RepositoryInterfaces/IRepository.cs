using System.Collections.Generic;

namespace RepositoryInterfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();

        T Get(int id);

        void Insert(T item);

        void Update(T item);

        void Delete(int id);
    }
}