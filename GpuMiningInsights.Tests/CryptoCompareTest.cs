using System;
using GpuMiningInsights.Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GpuMiningInsights.Tests
{
    [TestClass]
    public class CryptoCompareTest
    {


        [TestMethod]
        public void TestCryptoExchangeRate()
        {
            try
            {
                var result = CryptoCompareService.GetUsdBtcExchangeRate();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}