using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _repository;

        public ManagerService(IManagerRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Manager Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Manager> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Manager item)
        {
            _repository.Insert(item);
        }

        public void Update(Manager item)
        {
            _repository.Update(item);
        }
    }
}