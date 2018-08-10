using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class SiteItmFeedbackService : ISiteItmFeedbackService
    {
        private readonly ISiteItmFeedbackRepository _repository;

        public SiteItmFeedbackService(ISiteItmFeedbackRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public SiteItmFeedback Get(int id)
        {
            return _repository.Get(id);
        }

        public List<SiteItmFeedback> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(SiteItmFeedback item)
        {
            _repository.Insert(item);
        }

        public void Update(SiteItmFeedback item)
        {
            _repository.Update(item);
        }
    }
}