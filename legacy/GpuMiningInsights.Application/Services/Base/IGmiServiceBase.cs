using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Application.Services
{
    public interface IGmiServiceBase<TModel>: IServiceBase<TModel> where TModel : GmiEntityBase
    {
        new SearchResult<TModel> Search(SearchCriteria<TModel> searchCriteria);
    }
}