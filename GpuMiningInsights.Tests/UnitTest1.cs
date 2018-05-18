using System;
using System.Collections.Generic;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GpuMiningInsights.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string asinNumber = "";
            PriceSource priceSource = new PriceSource()
            {
                PriceSourceItemIdentifier = "B06Y15M48C",
                PriceSourceAction = AmazonService.SearchItemLookupOperation
            };
            GPU gpu= new GPU()
            {
                PriceSources = new List<PriceSource>() { priceSource },
                
            };
            InsighterService.GetPrice(gpu, priceSource);
        }
    }
}
