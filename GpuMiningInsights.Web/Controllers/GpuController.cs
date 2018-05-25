using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class GpuController : GmiAuthorizeStandardController<Gpu, GpuService, GpuSearchCrietriaViewModel>
    {
        public ActionResult FetchGpuInfoByAsin(string asin)
        {
            string asinNumber = asin;

            PriceSourceOld priceSource = new PriceSourceOld()
            {
                PriceSourceItemIdentifier = asinNumber,
                PriceSourceAction = AmazonService.SearchItemLookupOperation
            };

            GPUOld gpuOld = new GPUOld()
            {
                PriceSources = new List<PriceSourceOld>() { priceSource },

            };

            List<PriceSourceItem> priceSourceItems = InsighterService.GetPrice(gpuOld, priceSource);
            string data = string.Empty;
            if (priceSourceItems.FirstOrDefault() != null)
                data = CreaDev.Framework.Core.Utils.Serialization.SerializeJavaScript(priceSourceItems.FirstOrDefault());

            JsonResultObject jsonResultObject = new JsonResultObject()
            {
                Data = data,
                Success = true
            };
            return Json(jsonResultObject, JsonRequestBehavior.AllowGet);
        }
    }
}