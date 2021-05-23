using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels.Search;

namespace TopGpuMining.Web.Controllers
{
    public class BrandController : GmiAuthorizeStandardController<Brand, BrandService, BrandSearchCrietriaViewModel>
    {
        public BrandController(BrandService service) : base(service)
        {
        }
    }
}