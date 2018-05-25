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
            string asinNumber = "B06Y15M48C";
            PriceSourceOld priceSource = new PriceSourceOld()
            {
                PriceSourceItemIdentifier = asinNumber,
                PriceSourceAction = AmazonService.SearchItemLookupOperation
            };
            GPUOld gpuOld= new GPUOld()
            {
                PriceSources = new List<PriceSourceOld>() { priceSource },
                
            };
            InsighterService.GetPrice(gpuOld, priceSource);
        }
    }
}
