using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Core.Exceptions;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class BrandController : GmiStandardController<Brand, BrandService, BrandSearchCrietriaViewModel>
    {

    }
    
}