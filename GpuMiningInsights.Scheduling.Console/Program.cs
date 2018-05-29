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
            //Update Fiat Currencies Exchange Rate
            TryHelper.Try(FiatCurrencyService.Instance.AddOrUpdate);

            //Update Crypto Currencies info, and update Algorithms
            TryHelper.Try(CoinService.Instance.AddOrUpdate);

            //Generate Profitability Report
            GpusInsightsReport report =TryHelper.Try(GpuInsightsService.GenerateReport);

            //Save the Report To DB
            GpusInsightsReportService.Instance.Add(report);



        }
    }
}
