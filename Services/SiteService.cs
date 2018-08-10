using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository _repository;

        public SiteService(ISiteRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public Site Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Site> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Site item)
        {
            _repository.Insert(item);
        }

        public void Update(Site item)
        {
            _repository.Update(item);
        }
    }
}