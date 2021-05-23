using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels.Search;

namespace TopGpuMining.Web.Controllers
{
    public class ModelController : GmiAuthorizeStandardController<Model, ModelService, ModelSearchCrietriaViewModel>
    {
        public ModelController(ModelService service) : base(service)
        {
        }
    }
}