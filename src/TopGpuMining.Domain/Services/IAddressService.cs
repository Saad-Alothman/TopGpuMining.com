using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Domain.Services
{
    public interface IAddressService
    {
        Address Add(Address entity);
        void Delete(string id);
        Address GetById(string id);
        Address Save(Address entity);
        SearchResult<Address> Search(SearchCriteria<Address> search);
    }
}
