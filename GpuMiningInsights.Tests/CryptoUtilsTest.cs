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
                double EthCurrentUsdExchangeRate = 600;
                double ethHashRatePowerMega = 27.9;
                double ethBlockReward = 2.91;
                double ethDifficultyRate = 3139455711174180;
                double ethDifficultyRateTimesMillion = 3238018574 * 1000000.0;
                double ethNetHash = 210.40*1000*1000;
                double blockTimeSeconds = 15.39;
                double ethPerDayUsdMethod11 = CryptoUtils.CalculateCoinRevenuePerDayUsd(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward,EthCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method1);
                double ethCoinsPerDay = CryptoUtils.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, CryptoUtils.RevenueCalcMethod.Method1);
                
                double perDayUsd2UsdMethod2 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, EthCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method2);
                double ethCoinsPerDay2 = CryptoUtils.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, CryptoUtils.RevenueCalcMethod.Method2);
                double perDayUsd2UsdMethod3 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(ethHashRatePowerMega, ethNetHash, ethBlockReward, EthCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method3, blockTimeSeconds);
                double ethCoinsPerDay3 = CryptoUtils.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, CryptoUtils.RevenueCalcMethod.Method3,blockTimeSeconds);
                double perDayUsd2UsdMethod4 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, EthCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method4);
                double ethCoinsPerDay4 = CryptoUtils.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, CryptoUtils.RevenueCalcMethod.Method4);
                double perDayUsd2UsdMethod5 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, EthCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method5);
                double ethCoinsPerDay5 = CryptoUtils.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethNetHash, ethBlockReward, CryptoUtils.RevenueCalcMethod.Method5);

                //Grs
                double GrsCurrentUsdExchangeRate = 0.975868;
                double GrsHashRatePowerMega = 15.5;
                double GrsBlockReward = 5;
                double GrsDifficultyRate = 41828.026;
                double GrsNnetash = 2.9 * 1000 * 1000;
                double GrsblockTimeSeconds = 62;
                double GrsPerDayUsdMethod11 = CryptoUtils.CalculateCoinRevenuePerDayUsd(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method1);
                double GrsCoinsPerDay = CryptoUtils.CalculateCoinRevenuePerDay(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, CryptoUtils.RevenueCalcMethod.Method1);

                double GrsperDayUsd2UsdMethod2 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method2);
                double GrsperDayUsd2UsdMethod3 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(GrsHashRatePowerMega, GrsNnetash, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method3, blockTimeSeconds);
                double GrsperDayUsd2UsdMethod4 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method4);
                double GrsperDayUsd2UsdMethod5 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method5);


                //Hal
                double HalCurrentUsdExchangeRate = 0.117947;
                double HalHashRatePowerMega = 700.0 / 1000;
                double HalBlockReward = 5;
                double HalDifficultyRate = 9.847;
                double HalNnetash = 74.49;
                double HalblockTimeSeconds = 600;
                double HalPerDayUsdMethod11 = CryptoUtils.CalculateCoinRevenuePerDayUsd(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method1);
                double HalCoinsPerDay = CryptoUtils.CalculateCoinRevenuePerDay(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, CryptoUtils.RevenueCalcMethod.Method1);

                double HalperDayUsd2UsdMethod2 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method2);
                double HalperDayUsd2UsdMethod3 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(HalHashRatePowerMega, HalNnetash, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method3, blockTimeSeconds);
                double HalperDayUsd2UsdMethod4 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method4);
                double HalperDayUsd2UsdMethod5 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method5);



                //Metaverse
                double MetaverseCurrentUsdExchangeRate = 0.808332;
                double MetaverseHashRatePowerMega = 27.9;
                double MetaverseBlockReward = 2.85;
                double MetaverseDifficultyRate = 4746784;
                double MetaverseNnetash = 148.34*1000;
                double MetaverseblockTimeSeconds = 32;
                double MetaversePerDayUsdMethod11 = CryptoUtils.CalculateCoinRevenuePerDayUsd(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method1);
                double MetaverseCoinsPerDay = CryptoUtils.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtils.RevenueCalcMethod.Method1);
                double MetaverseCoinsPerDay2 = CryptoUtils.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtils.RevenueCalcMethod.Method2);
                double MetaverseCoinsPerDay3 = CryptoUtils.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtils.RevenueCalcMethod.Method3, MetaverseblockTimeSeconds);
                double MetaverseCoinsPerDay4 = CryptoUtils.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtils.RevenueCalcMethod.Method4);
                double MetaverseCoinsPerDay5 = CryptoUtils.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtils.RevenueCalcMethod.Method5);

                double MetaverseperDayUsd2UsdMethod2 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method2);
                double MetaverseperDayUsd2UsdMethod3 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(MetaverseHashRatePowerMega, MetaverseNnetash, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method3, blockTimeSeconds);
                double MetaverseperDayUsd2UsdMethod4 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method4);
                double MetaverseperDayUsd2UsdMethod5 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method5);



                //Monero
                double MoneroCurrentUsdExchangeRate = 155.78;
                double MoneroHashRatePowerMega = 830.0 / 1000/1000;
                double MoneroBlockReward = 4.51;
                double MoneroDifficultyRate = 50328693738;
                double MoneroNnetash = 415.94;
                double MoneroblockTimeSeconds = 121;
                double MoneroPerDayUsdMethod11 = CryptoUtils.CalculateCoinRevenuePerDayUsd(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method1);
                double MoneroCoinsPerDay = CryptoUtils.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, CryptoUtils.RevenueCalcMethod.Method1);
                double MoneroCoinsPerDay2 = CryptoUtils.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, CryptoUtils.RevenueCalcMethod.Method2);
                double MoneroCoinsPerDay3 = CryptoUtils.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, CryptoUtils.RevenueCalcMethod.Method3, MoneroblockTimeSeconds);
                double MoneroCoinsPerDay4 = CryptoUtils.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, CryptoUtils.RevenueCalcMethod.Method4);
                double MoneroCoinsPerDay5 = CryptoUtils.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroNnetash, MoneroBlockReward, CryptoUtils.RevenueCalcMethod.Method5);

                double MoneroperDayUsd2UsdMethod2 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method2);
                double MoneroperDayUsd2UsdMethod3 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(MoneroHashRatePowerMega, MoneroNnetash, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method3, blockTimeSeconds);
                double MoneroperDayUsd2UsdMethod4 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method4);
                double MoneroperDayUsd2UsdMethod5 = CryptoUtils.CalculateCoinRevenuePerDayBtcEchangeRate(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtils.RevenueCalcMethod.Method5);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    
    }
}
