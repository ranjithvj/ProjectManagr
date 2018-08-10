using Models;
using RepositoryInterfaces;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using System;

namespace Repositories
{
    public class ScreenRepository : IScreenRepository
    {
        public ScreenRepository()
        {
        }

        public Screen Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Screen> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Screen item)
        {
            throw new NotImplementedException();
        }

        public void Update(Screen item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Screen item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Screen> GetWithFilter()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}