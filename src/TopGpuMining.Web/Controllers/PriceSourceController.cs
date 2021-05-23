using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels.Search;

namespace TopGpuMining.Web.Controllers
{
    public class PriceSourceController : GmiAuthorizeStandardController<PriceSource, PriceSourceService, PriceSourceSearchCrieteriaViewModel>
    {
        public PriceSourceController(PriceSourceService service) : base(service)
        {
        }
    }
}