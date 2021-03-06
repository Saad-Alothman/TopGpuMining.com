﻿using System.Collections.Generic;
using CreaDev.Framework.Core.Extensions;

namespace GpuMiningInsights.Application.Services
{
    public static class CryptoCompareService
    {
        public static CryptoComparePriceResult GetUsdBtcExchangeRate()
        {
            string sourceCurrency = "BTC";
            List<string> toCurrency = new List<string>(){"USD" };
            return GetCryptoComparePriceExchangeRate(sourceCurrency, toCurrency.ToArray());
        }

        public static CryptoComparePriceResult GetCryptoComparePriceExchangeRate(string sourceCurrency,params string[] toCurrency)
        {
            string toCurrencyCsv = toCurrency.ToCsv(",");
            string coinUrl = $"https://min-api.cryptocompare.com/data/price?fsym={sourceCurrency}&tsyms={toCurrencyCsv}";
            string response = InsighterService.GetHttpResponseText(coinUrl);
            //  var values = CreaDev.Framework.Core.Utils.Serialization.DeSerialize<Dictionary<string, string>>(response);
            if (response.Contains("\"Response\":\"Error\""))
            {
                return null;
            }
            Dictionary<string, double> whatToMineCoinResponse =
                CreaDev.Framework.Core.Utils.Serialization.DeSerialize<Dictionary<string, double>>(response);

            return new CryptoComparePriceResult()
            {
                ExchangeRates = whatToMineCoinResponse,
                SourceCurrenceCode = sourceCurrency
            };
        }
        public static List<CryptoComparePriceResult> GetCryptoComparePriceExchangeRateMultiSource(List<string> sourceCurrency, params string[] toCurrency)
        {
            
            List<CryptoComparePriceResult> comparePriceResults = new List<CryptoComparePriceResult>();

            
            string coinUrl = $"https://min-api.cryptocompare.com/data/pricemulti?fsyms={sourceCurrency.ToCsv(",")}&tsyms={toCurrency.ToCsv(",")}";
            string response = InsighterService.GetHttpResponseText(coinUrl);
            //  var values = CreaDev.Framework.Core.Utils.Serialization.DeSerialize<Dictionary<string, string>>(response);
            Dictionary<string, Dictionary<string, double>> whatToMineCoinResponse =
                CreaDev.Framework.Core.Utils.Serialization.DeSerialize<Dictionary<string, Dictionary<string, double>>>(response);
            foreach (var key in whatToMineCoinResponse.Keys)
            {

                comparePriceResults .Add(new CryptoComparePriceResult()
                {
                    ExchangeRates = whatToMineCoinResponse[key],
                    SourceCurrenceCode = key
                });
            }

            return comparePriceResults;


        }
    }
    
    public class CryptoComparePriceResult
    {
        public string SourceCurrenceCode { get; set; }

        public Dictionary<string,double> ExchangeRates { get; set; }
    }

}