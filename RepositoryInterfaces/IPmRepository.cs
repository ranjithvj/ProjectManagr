using Models;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryInterfaces
{
    public interface IPmRepository<T> where T : BaseModel
    {
        List<T> GetAll();

        T Get(int id);

        void Insert(T item);

        void Update(T item);

        void Delete(T item);
    }
}