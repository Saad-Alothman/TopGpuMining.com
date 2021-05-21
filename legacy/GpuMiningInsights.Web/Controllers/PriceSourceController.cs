using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class PriceSourceController :GmiAuthorizeStandardController<PriceSource, PriceSourceService, PriceSourceSearchCrieteriaViewModel>
    {

    }
}