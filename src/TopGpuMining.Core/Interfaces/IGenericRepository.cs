using TopGpuMining.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Core.Interfaces
{
    public interface IGenericRepository
    {
        Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        List<TEntity> Create<TEntity>(List<TEntity> entities) where TEntity : class, IBaseEntity;
        TEntity Create<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;

        TEntity Update<TEntity>(TEntity entityToUpdate) where TEntity : class, IBaseEntity;

        Task<TEntity> UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class, IBaseEntity;

        void Delete<TEntity>(string id) where TEntity : class, IBaseEntity;

        Task DeleteAsync<TEntity>(string id) where TEntity : class, IBaseEntity;

        void Delete<TEntity>(TEntity entityToDelete) where TEntity : class, IBaseEntity;

        Task DeleteAsync<TEntity>(TEntity entityToDelete) where TEntity : class, IBaseEntity;

        int Count<TEntity>() where TEntity : class, IBaseEntity;

        Task<int> CountAsync<TEntity>() where TEntity : class, IBaseEntity;

        int Count<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IBaseEntity;

        Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class, IBaseEntity;

        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null, int? maxSize = null) where TEntity : class, IBaseEntity;
        
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null, int? maxSize = null) where TEntity : class, IBaseEntity;
        
        Task<TEntity> GetByIdAsync<TEntity>(string id, params string[] includes) where TEntity : class, IBaseEntity;
        
        Task<SearchResult<TEntity>> SearchAsync<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class, IBaseEntity;
               
        SearchResult<TEntity> Search<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class, IBaseEntity;
        TEntity GetById<TEntity>(string id, params string[] includes) where TEntity : class, IBaseEntity;
    }
}
