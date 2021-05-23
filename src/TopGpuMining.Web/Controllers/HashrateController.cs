
using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels.Search;

namespace TopGpuMining.Web.Controllers
{
    public class HashrateController : GmiAuthorizeStandardController<Hashrate, HashrateService, HashrateSearchCrietriaViewModel>
    {
        public HashrateController(HashrateService service) : base(service)
        {
        }
    }
}