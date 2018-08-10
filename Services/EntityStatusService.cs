using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;

namespace Services
{
    public class EntityStatusService : IEntityStatusService
    {
        private readonly IEntityStatusRepository _entityStatusRepository;

        public EntityStatusService(IEntityStatusRepository entityStatusRepository)
        {
            _entityStatusRepository = entityStatusRepository;
        }

        public void Delete(int id)
        {
            _entityStatusRepository.Delete(id);
        }

        public EntityStatus Get(int id)
        {
            return _entityStatusRepository.Get(id);
        }

        public List<EntityStatus> GetAll()
        {
            return _entityStatusRepository.GetAll();
        }

        public void Insert(EntityStatus item)
        {
            _entityStatusRepository.Insert(item);
        }

        public void Update(EntityStatus item)
        {
            _entityStatusRepository.Update(item);
        }
    }
}