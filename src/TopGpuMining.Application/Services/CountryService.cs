using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Application.Services
{
    public class CountryService : ServiceBase , ICountryService
    {
        public CountryService(IGenericRepository repository) : base(repository)
        {

        }

        public Task<Country> AddAsync(Country entity)
        {
            return _repository.CreateAsync(entity);
        }
        
        public Task<Country> SaveAsync(Country entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public Task<Country> GetByIdAsync(string id)
        {
            return _repository.GetByIdAsync<Country>(id);
        }

        public Task DeleteAsync(string id)
        {
            return _repository.DeleteAsync<Country>(id);
        }

        public Task<SearchResult<Country>> SearchAsync(SearchCriteria<Country> search)
        {
            return _repository.SearchAsync(search);
        }

        public Task<IEnumerable<Country>> Get() => _repository.GetAsync<Country>();
    }
}
