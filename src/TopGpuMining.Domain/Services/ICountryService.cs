using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Domain.Services
{
    public interface ICountryService
    {
        Task<Country> AddAsync(Country entity);

        Task DeleteAsync(string id);

        Task<Country> GetByIdAsync(string id);

        Task<Country> SaveAsync(Country entity);

        Task<SearchResult<Country>> SearchAsync(SearchCriteria<Country> search);

        Task<IEnumerable<Country>> Get();
    }

    
}
