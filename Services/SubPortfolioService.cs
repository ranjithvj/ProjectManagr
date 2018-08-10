using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class SubPortfolioService : ISubPortfolioService
    {
        private readonly ISubPortfolioRepository _repository;

        public SubPortfolioService(ISubPortfolioRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public SubPortfolio Get(int id)
        {
            return _repository.Get(id);
        }

        public List<SubPortfolio> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(SubPortfolio item)
        {
            _repository.Insert(item);
        }

        public void Update(SubPortfolio item)
        {
            _repository.Update(item);
        }
    }
}