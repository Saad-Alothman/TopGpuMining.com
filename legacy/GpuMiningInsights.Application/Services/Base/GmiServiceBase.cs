using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Persistance;

namespace GpuMiningInsights.Application.Services
{
    public class GmiServiceBase<TModel, TService> : ServiceBase<TModel, TService, GmiContext, Domain.Models.User>, IGmiServiceBase<TModel> where TModel : GmiEntityBase where TService : class, new()
    {
        public override SearchResult<TModel> Search(SearchCriteria<TModel> searchCriteria)
        {
            //searchCriteria.ApplyFilterBasedOnPermission();
            return Repository.Search(searchCriteria, Includes.ToArray());
        }
      



    }
}