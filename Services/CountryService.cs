using Models;
using RepositoryInterfaces;
using ServiceInterfaces;
using System.Collections.Generic;
using System;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public void Delete(int id)
        {
            _countryRepository.Delete(id);
        }

        public Country Get(int id)
        {
            return _countryRepository.Get(id);
        }

        public List<Country> GetAll()
        {
            return _countryRepository.GetAll();
        }

        public List<Country> GetCountriesWithSites()
        {
            return _countryRepository.GetCountriesWithSites();
        }

        public void Insert(Country item)
        {
            _countryRepository.Insert(item);
        }

        public void Update(Country item)
        {
            _countryRepository.Update(item);
        }
    }
}