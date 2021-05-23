using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Models;
using TopGpuMining.Domain.Services;
using TopGpuMining.Web.Controllers;
using TopGpuMining.Web.ViewModels.Search;

namespace TopGpuMining.Web.Controllers
{
    public class AlgorithmController : GmiAuthorizeStandardController<Algorithm, IAlgorithmService, AlgorithmSearchCrietriaViewModel>
    {
        public AlgorithmController(IAlgorithmService service) : base(service)
        {
        }
    }
}