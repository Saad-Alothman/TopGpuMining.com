using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class BrandController : GmiAuthorizeStandardController<Brand, BrandService, BrandSearchCrietriaViewModel>
    {

    }
}