using System.Collections.Generic;
using System.Linq;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Application.Services
{
    public class GenericService<TModel> : GmiServiceBase<TModel,GenericService<TModel>> where TModel : GmiEntityBase
    {

        public TModel Add(TModel entity)
        {
            return Repository.Create<TModel>(entity);
        }

        public TModel Update(TModel entity)
        {
            return Repository.Update<TModel>(entity);
        }

        public void Delete(object id)
        {
            Repository.Delete<TModel>(id);
        }

        public List<TModel> Get()
        {
            return Repository.Get<TModel>().ToList();
        }

        public virtual SearchResult<TModel> Search(SearchCriteria<TModel> searchCriteria, params string[] includes)
        {
            return Repository.Search(searchCriteria, includes);
        }

        public virtual TModel GetByID(object keys, params string[] includes)
        {

            if (includes != null && includes.Any())
            {
                int key = (int)keys;
                return Repository.Get<TModel>(includeProperties: includes.ToList(), filter: e => e.Id == key).FirstOrDefault();

            }
            return Repository.GetByID<TModel>(keys: keys);
        }
    }
}