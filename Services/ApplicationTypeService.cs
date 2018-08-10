using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class ApplicationTypeService : IApplicationTypeService
    {
        private readonly IApplicationTypeRepository _applicationTypeRepository;

        public ApplicationTypeService(IApplicationTypeRepository applicationTypeRepository)
        {
            _applicationTypeRepository = applicationTypeRepository;
        }

        public void Delete(int id)
        {
            _applicationTypeRepository.Delete(id);
        }

        public ApplicationType Get(int id)
        {
            return _applicationTypeRepository.Get(id);
        }

        public List<ApplicationType> GetAll()
        {
            return _applicationTypeRepository.GetAll();
        }

        public void Insert(ApplicationType item)
        {
            _applicationTypeRepository.Insert(item);
        }

        public void Update(ApplicationType item)
        {
            _applicationTypeRepository.Update(item);
        }
    }
}