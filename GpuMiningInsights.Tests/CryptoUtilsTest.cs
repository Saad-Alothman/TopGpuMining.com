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
            double ethBlockTimeSeconds = 15.2786;
            double ethBlock_reward = 2.91000000000004;
            double ethNetHashMega = 215188375022144.0/1000.0/1000.0;
            double ethCardHashrate = 27.9;
            var ethPerDay = CryptoUtils.CalculateCoinRevenuePerDayByNethashAndBlockTime(ethBlockTimeSeconds,ethBlock_reward,ethNetHashMega,ethCardHashrate);

            double xmrBlockTimeSeconds = 117.0;
            double xmrBlock_reward = 4.5;
            double xmrNetHashMega = 443.54 ;
            double xmrCardHashrate = 830.0/1000/1000;
            var xmrPerDay = CryptoUtils.CalculateCoinRevenuePerDayByNethashAndBlockTime(xmrBlockTimeSeconds, xmrBlock_reward, xmrNetHashMega, xmrCardHashrate);
        }

        [TestMethod]
        public void CalculateCoinRevenuePerDayOldTest()
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
                double ethPerDayUsdMethod11 = CryptoUtilsOld.CalculateCoinRevenuePerDayUsd(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward,EthCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method1);
                double ethCoinsPerDay = CryptoUtilsOld.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method1);
                
                double perDayUsd2UsdMethod2 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, EthCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method2);
                double ethCoinsPerDay2 = CryptoUtilsOld.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method2);
                double perDayUsd2UsdMethod3 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(ethHashRatePowerMega, ethNetHash, ethBlockReward, EthCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method3, blockTimeSeconds);
                double ethCoinsPerDay3 = CryptoUtilsOld.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method3,blockTimeSeconds);
                double perDayUsd2UsdMethod4 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, EthCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method4);
                double ethCoinsPerDay4 = CryptoUtilsOld.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method4);
                double perDayUsd2UsdMethod5 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(ethHashRatePowerMega, ethDifficultyRate, ethBlockReward, EthCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method5);
                double ethCoinsPerDay5 = CryptoUtilsOld.CalculateCoinRevenuePerDay(ethHashRatePowerMega, ethNetHash, ethBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method5);

                //Grs
                double GrsCurrentUsdExchangeRate = 0.975868;
                double GrsHashRatePowerMega = 15.5;
                double GrsBlockReward = 5;
                double GrsDifficultyRate = 41828.026;
                double GrsNnetash = 2.9 * 1000 * 1000;
                double GrsblockTimeSeconds = 62;
                double GrsPerDayUsdMethod11 = CryptoUtilsOld.CalculateCoinRevenuePerDayUsd(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method1);
                double GrsCoinsPerDay = CryptoUtilsOld.CalculateCoinRevenuePerDay(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method1);

                double GrsperDayUsd2UsdMethod2 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method2);
                double GrsperDayUsd2UsdMethod3 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(GrsHashRatePowerMega, GrsNnetash, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method3, blockTimeSeconds);
                double GrsperDayUsd2UsdMethod4 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method4);
                double GrsperDayUsd2UsdMethod5 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(GrsHashRatePowerMega, GrsDifficultyRate, GrsBlockReward, GrsCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method5);


                //Hal
                double HalCurrentUsdExchangeRate = 0.117947;
                double HalHashRatePowerMega = 700.0 / 1000;
                double HalBlockReward = 5;
                double HalDifficultyRate = 9.847;
                double HalNnetash = 74.49;
                double HalblockTimeSeconds = 600;
                double HalPerDayUsdMethod11 = CryptoUtilsOld.CalculateCoinRevenuePerDayUsd(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method1);
                double HalCoinsPerDay = CryptoUtilsOld.CalculateCoinRevenuePerDay(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method1);

                double HalperDayUsd2UsdMethod2 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method2);
                double HalperDayUsd2UsdMethod3 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(HalHashRatePowerMega, HalNnetash, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method3, blockTimeSeconds);
                double HalperDayUsd2UsdMethod4 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method4);
                double HalperDayUsd2UsdMethod5 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(HalHashRatePowerMega, HalDifficultyRate, HalBlockReward, HalCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method5);



                //Metaverse
                double MetaverseCurrentUsdExchangeRate = 0.808332;
                double MetaverseHashRatePowerMega = 27.9;
                double MetaverseBlockReward = 2.85;
                double MetaverseDifficultyRate = 4746784;
                double MetaverseNnetash = 148.34*1000;
                double MetaverseblockTimeSeconds = 32;
                double MetaversePerDayUsdMethod11 = CryptoUtilsOld.CalculateCoinRevenuePerDayUsd(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method1);
                double MetaverseCoinsPerDay = CryptoUtilsOld.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method1);
                double MetaverseCoinsPerDay2 = CryptoUtilsOld.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method2);
                double MetaverseCoinsPerDay3 = CryptoUtilsOld.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method3, MetaverseblockTimeSeconds);
                double MetaverseCoinsPerDay4 = CryptoUtilsOld.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method4);
                double MetaverseCoinsPerDay5 = CryptoUtilsOld.CalculateCoinRevenuePerDay(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method5);

                double MetaverseperDayUsd2UsdMethod2 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method2);
                double MetaverseperDayUsd2UsdMethod3 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(MetaverseHashRatePowerMega, MetaverseNnetash, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method3, blockTimeSeconds);
                double MetaverseperDayUsd2UsdMethod4 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method4);
                double MetaverseperDayUsd2UsdMethod5 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(MetaverseHashRatePowerMega, MetaverseDifficultyRate, MetaverseBlockReward, MetaverseCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method5);



                //Monero
                double MoneroCurrentUsdExchangeRate = 155.78;
                double MoneroHashRatePowerMega = 830.0 / 1000/1000;
                double MoneroBlockReward = 4.51;
                double MoneroDifficultyRate = 50328693738;
                double MoneroNnetash = 415.94;
                double MoneroblockTimeSeconds = 121;
                double MoneroPerDayUsdMethod11 = CryptoUtilsOld.CalculateCoinRevenuePerDayUsd(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method1);
                double MoneroCoinsPerDay = CryptoUtilsOld.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method1);
                double MoneroCoinsPerDay2 = CryptoUtilsOld.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method2);
                double MoneroCoinsPerDay3 = CryptoUtilsOld.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method3, MoneroblockTimeSeconds);
                double MoneroCoinsPerDay4 = CryptoUtilsOld.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method4);
                double MoneroCoinsPerDay5 = CryptoUtilsOld.CalculateCoinRevenuePerDay(MoneroHashRatePowerMega, MoneroNnetash, MoneroBlockReward, CryptoUtilsOld.RevenueCalcMethod.Method5);

                double MoneroperDayUsd2UsdMethod2 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method2);
                double MoneroperDayUsd2UsdMethod3 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(MoneroHashRatePowerMega, MoneroNnetash, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method3, blockTimeSeconds);
                double MoneroperDayUsd2UsdMethod4 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method4);
                double MoneroperDayUsd2UsdMethod5 = CryptoUtilsOld.CalculateCoinRevenuePerDayBtcEchangeRate(MoneroHashRatePowerMega, MoneroDifficultyRate, MoneroBlockReward, MoneroCurrentUsdExchangeRate, CryptoUtilsOld.RevenueCalcMethod.Method5);


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    
    }
}
