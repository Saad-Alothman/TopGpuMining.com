using TopGpuMining.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Application.Services
{
    public  class GpuInsightsService
    {
        GpuService _gpuService;
        FiatCurrencyService _fiatCurrencyService;

        public GpuInsightsService(GpuService gpuService, FiatCurrencyService fiatCurrencyService)
        {
            _gpuService = gpuService;
            _fiatCurrencyService = fiatCurrencyService;
        }
        public GpusInsightsReport GenerateReport()
        {
            GpusInsightsReport gpusInsightsReport = null;

            gpusInsightsReport = new GpusInsightsReport();
            /*
             Requirements:
             GpuPriceSourceItems > 0
             ASIN Number

             */
            List<Gpu> gpus = _gpuService.GetAll();
            foreach (Gpu gpu in gpus)
            {
                    
                List<PriceSourceItem> gpuPriceSourceItems = GetPrices(gpu);
                if (!gpuPriceSourceItems.Any())
                    continue;

                GpuInsightReport gpuInsightReport = new GpuInsightReport();
                gpuInsightReport.GpuId = gpu.Id;
                gpuInsightReport.PriceSourceItems = gpuPriceSourceItems;
                
                gpusInsightsReport.GpuInsightReports.Add(gpuInsightReport);
            }

            return gpusInsightsReport;

        }


        public List<PriceSourceItem> GetPrices(Gpu gpu)
        {
            List<PriceSourceItem> allPriceSourceItems = new List<PriceSourceItem>();
            foreach (var gpuPriceSource in gpu.GPUPriceSources)
            {
                var priceSource = gpuPriceSource.PriceSource;
                var priceSourceItems = GetPrice(gpu, priceSource);
                allPriceSourceItems.AddRange(priceSourceItems);
            }
            //Calculate USD Price
            FillUsdPrices(allPriceSourceItems);
            return allPriceSourceItems;
        }

        private void FillUsdPrices(List<PriceSourceItem> allPriceSourceItems)
        {
            List<FiatCurrency> fiatCurrencies = _fiatCurrencyService.GetAll();
            foreach (var priceSourceItem in allPriceSourceItems)
            {
                FiatCurrency fiatCurrency = fiatCurrencies.FirstOrDefault(fc => fc.Code.ToLower() == priceSourceItem.PriceCurrency.ToLower());
                if (fiatCurrency == null) continue;

                double priceUsd = priceSourceItem.Price / fiatCurrency.ExchangeRateUSD;
                priceSourceItem.PriceUSD = Math.Round(priceUsd, 2);
            }
        }

        public static List<PriceSourceItem> GetPrice(Gpu gpu, PriceSource priceSource)
        {
            if (string.IsNullOrWhiteSpace(gpu.Asin))
            {
                throw new ArgumentNullException(nameof(Gpu.Asin));
            }

            List<PriceSourceItem> priceSourceItems = new List<PriceSourceItem>();
            
            throw new NotImplementedException();
            //AmazonEndpoint? amazonEndpoint = null;
            //if (priceSource != null)
            //{
            //    WriteLine($"Getting Price From {priceSource?.Name ?? "NULL"} For GPU {gpu.Name}");
            //    amazonEndpoint = PriceSourceTypeHelper.ToAmazonEndpoint(priceSource.PriceSourceType);
            //}
            
            //List<PriceSourceItem> result = Amazon.AmazonService.SearchItemLookupOperation(gpu.Asin, amazonEndpoint);
            //result.ForEach(p => p.PriceSourceId = priceSource.Id);
            //priceSourceItems.AddRange(result);

            //return priceSourceItems;
        }
        
        private static void WriteLine(string v)
        {
        }
    }
}
