using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Core.Utils
{
    public static class CryptoUtils
    {
        public static double CalculateCoinRevenuePerDay(double hashrateMega,double networkDifficultyMega, double blockReward)
        {
            //(Hashrate * Reward) / Difficulty = coins per second
            double coinsPerSecond = (hashrateMega * blockReward) / networkDifficultyMega;
            double perDay = coinsPerSecond * 60 * 60 * 24;
            return perDay;
        }
    }
}
