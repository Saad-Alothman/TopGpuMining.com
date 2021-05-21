using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace TopGpuMining.Application.Services
{
    public class GenericService<TModel> : ServiceBase , IGenericService<TModel> where TModel : class, IBaseEntity
    {
        public GenericService(IGenericRepository repository) : base(repository)
        {
        }

        public virtual TModel Add(TModel entity)
        {
            return _repository.Create(entity);
        }

        public virtual TModel Save(TModel entity)
        {
            return _repository.Update(entity);
        }

        public virtual TModel GetById(string id)
        {
            return _repository.GetById<TModel>(id);
        }
        public virtual List<TModel> GetAll()
        {
            return _repository.Get<TModel>().ToList();
        }

        public virtual void Delete(string id)
        {
            _repository.Delete<TModel>(id);
        }

        public virtual SearchResult<TModel> Search(SearchCriteria<TModel> search)
        {
            return _repository.Search(search);
        }
    }
}
