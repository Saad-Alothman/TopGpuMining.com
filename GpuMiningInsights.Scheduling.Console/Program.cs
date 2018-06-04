
using CreaDev.Framework.Core.Helpers;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Scheduling.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            GmiApp.Initialize();
            //Update Fiat Currencies Exchange Rate
            TryHelper.Try(FiatCurrencyService.Instance.AddOrUpdate,Log);

            //Update Crypto Currencies info, and update Algorithms
            TryHelper.Try(CoinService.Instance.AddOrUpdate, Log);
            //Update USD Exchange Rates;
            TryHelper.Try(CoinService.Instance.UpdateUsdExchangeRates, Log);
            
            //Generate Profitability Report
            GpusInsightsReport report =TryHelper.Try(GpuInsightsService.GenerateReport, Log);

            //Save the Report To DB
            TryHelper.Try(() => GpusInsightsReportService.Instance.Add(report), Log);




        }

        public static void Log(Exception ex)
        {
            System.Console.WriteLine(ex.ToString());
        }
        public static void Log(string message)
        {
            System.Console.WriteLine(message.ToString());
        }
    }
}
