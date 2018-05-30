using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Core.Utils
{
    public static class CryptoUtils
    {
        public static double CalculateCoinRevenuePerDay(double hashrateMega, double networkDifficultyMega,
            double blockReward, double usdExchangeRate)
        {
            return CalculateCoinRevenuePerDay(hashrateMega, networkDifficultyMega, blockReward) * usdExchangeRate;
        }
        public static double CalculateCoinRevenuePerDayBtcEchangeRate(double hashrateMega, double networkDifficultyMega,
            double blockReward, double btcExchangeRate)
        {
            return CalculateCoinRevenuePerDay(hashrateMega, networkDifficultyMega, blockReward) * btcExchangeRate;
        }
        public static double CalculateCoinRevenuePerDay(double hashrateMega,double networkDifficultyMega, double blockReward)
        {
            //(Hashrate * Reward) / Difficulty = coins per second
            double coinsPerSecond = (hashrateMega * blockReward) / networkDifficultyMega;
            double perDay = coinsPerSecond * 60 * 60 * 24;
            return perDay;


            //int hashRatePowerMega = 88;
            //double blockReward = 2.91;
            //double DifficultyMega = 3287008653;//3,287,008,653;

            //double perDay = CryptoUtils.CalculateCoinRevenuePerDay(hashRatePowerMega, DifficultyMega, blockReward);
            //int currentUsdExchangeRate = 600;
            //double perDayUsd = perDay *currentUsdExchangeRate ;

            //return;
        }
    }
}
