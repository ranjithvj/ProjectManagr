using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Department Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Department> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Department item)
        {
            _repository.Insert(item);
        }

        public void Update(Department item)
        {
            _repository.Update(item);
        }
    }
}