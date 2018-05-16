using System.Collections.Generic;

namespace CreaDev.Framework.Core.Models
{
    public interface IServiceBase<TModel> where TModel : EntityBase
    {
        void Add(TModel model);
        void Delete(int id);
        TModel GetById(int id);
        SearchResult<TModel> Search(SearchCriteria<TModel> searchCriteria);
        
        void Update(TModel model);
        List<TModel> GetAll();
    }
    public interface IServiceBaseStringId<TModel> where TModel : IEntityBaseStringId
    {
        void Add(TModel model);
        void Delete(int id);
        TModel GetById(int id);
        SearchResult<TModel> Search(SearchCriteria<TModel> searchCriteria);

        void Update(TModel model);
        List<TModel> GetAll();
    }
}