using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels.Search;

namespace TopGpuMining.Web.Controllers
{
    public class GPUPriceSourceController : GmiAuthorizeStandardController<GpuPriceSource, GpuPriceSourceService, GpuPSSearchViewModelBase>
    {
        public GPUPriceSourceController(GpuPriceSourceService service):base(service)
        {
        }

        [HttpPost]
        public virtual IActionResult AddForAll()
        {
            return SimpleAjaxAction(_service.AddForAll);
        }
    }
}