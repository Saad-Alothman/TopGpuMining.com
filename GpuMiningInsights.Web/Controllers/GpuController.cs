using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using CreaDev.Framework.Core.Extensions;
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

            var priceSourceItem = FetchGpu(asinNumber);
            string data = string.Empty;
            if (priceSourceItem != null)
                data = CreaDev.Framework.Core.Utils.Serialization.SerializeJavaScript(priceSourceItem);

            JsonResultObject jsonResultObject = new JsonResultObject()
            {
                Data = data,
                Success = true
            };
            return Json(jsonResultObject, JsonRequestBehavior.AllowGet);
        }

        private static PriceSourceItemOld FetchGpu(string asinNumber)
        {
            PriceSourceOld priceSource = new PriceSourceOld()
            {
                PriceSourceItemIdentifier = asinNumber,
                PriceSourceAction = AmazonService.SearchItemLookupOperationOld
            };

            GPUOld gpuOld = new GPUOld()
            {
                PriceSources = new List<PriceSourceOld>() {priceSource},
            };

            List<PriceSourceItemOld> priceSourceItems = InsighterService.GetPrice(gpuOld, priceSource);
            return priceSourceItems?.FirstOrDefault();
        }

        public ActionResult AddBulk(string asinsCsv,int modelId,bool addPriceSources=true)
        {
            var model =ModelService.Instance.GetById(modelId);
            var brands = BrandService.Instance.GetAll();
            List<Gpu> gpusToAdd = new List<Gpu>();
            var asins = new List<string>();
            foreach (var asin in asinsCsv.Split(','))
            {
                asins.Add(asin.Replace(",",""));
            }
            foreach (var asin in asins)
            {
                var priceSourceItem = FetchGpu(asin);
                if (string.IsNullOrWhiteSpace(priceSourceItem?.Name))
                    continue;

                string assumedBrand = Regex.Replace(priceSourceItem.Name.Split()[0], @"[^0-9a-zA-Z\ ]+", "").ToLower();
                var brand = brands.FirstOrDefault(b => b.Name.English.ToLower() == assumedBrand);

                if (brand == null)
                    continue;

                Gpu gpu = new Gpu()
                {
                    ModelId = modelId,
                    BrandId = brand.Id,
                    Name = priceSourceItem.Name,
                    Asin = asin,
                    Ean = priceSourceItem.Ean,
                    ImageUrl = priceSourceItem.ImageUrl
                    
                };
                gpusToAdd.Add(gpu);
                Thread.Sleep(1000);
            }
            GpuService.Instance.Add(gpusToAdd);
            GpuPriceSourceService.Instance.AddForAll();
            string addedAsins =gpusToAdd.Select(s => s.Asin).ToCsv();
            string count = $"{gpusToAdd.Count}/{asins.Count}";
            JsonResultObject jsonResultObject = new JsonResultObject()
            {
                Success = true,
                AlertMessage = new Alert($"{count} Added, {addedAsins}",Alert.Type.Success)
            };
            return Json(jsonResultObject,JsonRequestBehavior.AllowGet);
        }
    }
}