
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class HashrateController : GmiAuthorizeStandardController<Hashrate, HashrateService, HashrateSearchCrietriaViewModel>
    {
    }
}