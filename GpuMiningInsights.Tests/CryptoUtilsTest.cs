using System;
using System.Collections.Generic;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Core.Utils;
using GpuMiningInsights.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace GpuMiningInsights.Tests
{
    [TestClass]
    public class CryptoUtilsTest
    {




        [TestMethod]
        public void CalculateCoinRevenuePerDayTest()
        {
            try
            {
                double ethHashRatePowerMega = 27.9;
                double ethBlockReward = 2.91;
                double ethDifficultyMega = 3177938414649540;

                double ethPerDay = CryptoUtils.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyMega, ethBlockReward);
                int EthCurrentUsdExchangeRate = 600;
                double perDayUsd = ethPerDay * EthCurrentUsdExchangeRate;
                double ethPerDayUsdFromUtils = CryptoUtils.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyMega, ethBlockReward, EthCurrentUsdExchangeRate);

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    
    }
}
