using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class GPUPriceSourceController : GmiAuthorizeStandardController<GPUPriceSource, GpuPriceSourceService, GpuPSSearchCriteriaViewModelBase>
    {
        [HttpPost]
        public virtual ActionResult AddForAll()
        {
            return SimpleAjaxAction(GpuPriceSourceService.Instance.AddForAll);
        }
    }
}