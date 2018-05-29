using System.Collections.Generic;
using GpuMiningInsights.Domain.Models;
using Newtonsoft.Json;

namespace GpuMiningInsights.Application.Services
{
    public static class ApiLayerService
    {
        public static Dictionary<string, double> LoadCurrencies(bool removeSourceCurrencyFromKey=true)
        {
            string currencyApiUrl = "http://www.apilayer.net/api/live?access_key=ff139c408a9439cd66d94f7ee26a598b&format=1&source=USD";

            string respomseText = InsighterService.GetHttpResponseText(currencyApiUrl);
            var pricesDictResponse = JsonConvert.DeserializeObject<CurrencyPricesResponse>(respomseText);
            Dictionary<string, double> pricesDict = pricesDictResponse.quotes;
            if (removeSourceCurrencyFromKey)
            {
                Dictionary<string,double> withoutSourceCurrencyCode = new Dictionary<string, double>();
                foreach (var key in pricesDict.Keys)
                {
                    string newKey = key.Substring(3);
                    withoutSourceCurrencyCode.Add(newKey,pricesDict[key]);
                }
                pricesDict = withoutSourceCurrencyCode;
            }

            return pricesDict;
        }
    }
}