using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class ModelController : GmiAuthorizeStandardController<Model, ModelService, ModelSearchCrietriaViewModel>
    {

    }
}