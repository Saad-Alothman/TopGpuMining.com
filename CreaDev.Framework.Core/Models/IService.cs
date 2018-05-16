using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Core.Models
{
    public interface IService<T>
    {
        List<T> Get();
        T Add(T entity, string relatedItemTypeId, string relatedItemId);
        T Add(T entity);
        void Add(List<T> entity);
        T Get(object id);
        T Update(T entity);
        void Delete(object id);
        int Count();
        SearchResult<T> Search(SearchCriteria<T> searchCriteria);

        List<T> Get(int relatedItemId, string relatedItemTypeId);
    }
}
