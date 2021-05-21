using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TopGpuMining.Web.Controllers
{
    public class CropperController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
