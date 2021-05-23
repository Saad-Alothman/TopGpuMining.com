using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using TopGpuMining.Application.Services;
using TopGpuMining.Core.Extensions;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.Models;
using TopGpuMining.Web.ViewModels;
using TopGpuMining.Web.ViewModels.Search;

namespace TopGpuMining.Web.Controllers
{
    public class GpuController : GmiAuthorizeStandardController<Gpu, GpuService, GpuSearchCrietriaViewModel>
    {
        readonly BrandService _brandService;
        readonly GpuPriceSourceService _gpuPriceSourceService;
        readonly ModelService _modelService;
        readonly AmazonService _amazonService;


        public GpuController(GpuService service, BrandService brandService, ModelService modelService, AmazonService amazonService) : base(service)
        {
            _brandService = brandService;
            _modelService = modelService;
            _amazonService = amazonService;
        }

        public IActionResult FetchGpuInfoByAsin(string asin)
        {
            string asinNumber = asin;

            var priceSourceItem = FetchGpu(asinNumber);
            string data = string.Empty;

            return Json(data);
        }

        private PriceSourceItemOld FetchGpu(string asinNumber)
        {
            PriceSourceOld priceSource = new PriceSourceOld()
            {
                PriceSourceItemIdentifier = asinNumber,
                PriceSourceAction = (string term) => _amazonService.SearchItemLookupOperationOld(term)
            };

            GPUOld gpuOld = new GPUOld()
            {
                PriceSources = new List<PriceSourceOld>() { priceSource },
            };

            List<PriceSourceItemOld> priceSourceItems = InsighterService.GetPrice(gpuOld, priceSource);
            return priceSourceItems?.FirstOrDefault();
        }

        public IActionResult AddBulk(string asinsCsv, string modelId, bool addPriceSources = true)
        {
            var model = _modelService.GetById(modelId);
            var brands = _brandService.GetAll();
            List<Gpu> gpusToAdd = new List<Gpu>();
            var asins = new List<string>();
            foreach (var asin in asinsCsv.Split(','))
            {
                asins.Add(asin.Replace(",", ""));
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
            _service.Add(gpusToAdd);
            _gpuPriceSourceService.AddForAll();
            string addedAsins = gpusToAdd.Select(s => s.Asin).ToCsv();
            string count = $"{gpusToAdd.Count}/{asins.Count}";
            JsonResultObject jsonResultObject = new JsonResultObject()
            {
                Success = true,
                Alert = new Alert($"{count} Added, {addedAsins}", Alert.Type.Success)
            };
            return Json(jsonResultObject);
        }
    }
}