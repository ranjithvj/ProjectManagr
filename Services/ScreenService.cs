using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace Services
{
    public class ScreenService : IScreenService
    {
        private readonly IScreenRepository _screenRepository;

        public ScreenService(IScreenRepository screenRepository)
        {
            _screenRepository = screenRepository;
        }

        public void Delete(Screen item)
        {
             _screenRepository.Delete(item);
        }

        public Screen Get(int id)
        {
            return _screenRepository.Get(id);
        }

        public IEnumerable<Screen> GetAll()
        {
            return _screenRepository.GetAll();
        }

        public void Insert(Screen item)
        {
             _screenRepository.Insert(item);
        }

        public void Update(Screen item)
        {
            _screenRepository.Update(item);
        }
    }
}