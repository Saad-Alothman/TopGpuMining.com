using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Domain.Services
{
    public interface IGenericService<TModel> where TModel : class, IBaseEntity
    {
        TModel Add(TModel entity);

        void Delete(string id);

        TModel GetById(string id);

        TModel Save(TModel entity);

        SearchResult<TModel> Search(Core.Search.SearchCriteria<TModel> search);
    }
}
