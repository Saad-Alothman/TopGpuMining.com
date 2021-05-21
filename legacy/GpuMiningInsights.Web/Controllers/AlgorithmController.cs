using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class AlgorithmController : GmiAuthorizeStandardController<Algorithm, AlgorithmService, AlgorithmSearchCrietriaViewModel>
    {
        
    }
}