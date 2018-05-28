using System.ComponentModel.DataAnnotations;
using CreaDev.Framework.Core.Resources;

namespace GpuMiningInsights.Domain.Models
{
    public class Coin : GmiEntityBase
    {

        [Display(Name = nameof(Common.Name), ResourceType = typeof(Common))]
        public string Name { get; set; }

        [Display(Name = nameof(Common.Description), ResourceType = typeof(Common))]

        public string Description { get; set; }

        public override void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as Coin;
            this.Name = updateData.Name;
            this.Description = updateData.Description;

        }
        public void UpdateInfo(object objectWithNewData)
        {
            var updatedData = objectWithNewData as Coin;
            this.WhatToMineId = updatedData.WhatToMineId;
            this.AlgorithmId = updatedData.AlgorithmId;
            this.BlockTime = updatedData.BlockTime;
            this.BlockReward = updatedData.BlockReward;
            this.BlockReward24 = updatedData.BlockReward24;
            this.LastBlock = updatedData.LastBlock;
            this.Difficulty = updatedData.Difficulty;
            this.Difficulty24 = updatedData.Difficulty24;
            this.Nethash = updatedData.Nethash;
            this.ExchangeRate = updatedData.ExchangeRate;
            this.ExchangeRate24 = updatedData.ExchangeRate24;
            this.ExchangeRateVol = updatedData.ExchangeRateVol;
            this.ExchangeRateCurr = updatedData.ExchangeRateCurr;
            this.MarketCap = updatedData.MarketCap;
            this.EstimatedRewards = updatedData.EstimatedRewards;
            this.EstimatedRewards24 = updatedData.EstimatedRewards24;
            this.BtcRevenue = updatedData.BtcRevenue;
            this.BtcRevenue24 = updatedData.BtcRevenue24;
            this.Profitability = updatedData.Profitability;
            this.Profitability24 = updatedData.Profitability24;
            this.Lagging = updatedData.Lagging;


        }


        public int WhatToMineId { get; set; }
        public string Tag { get; set; }
        public Algorithm Algorithm { get; set; }
        public int? AlgorithmId { get; set; }
        public string BlockTime { get; set; }
        public double BlockReward { get; set; }
        public double BlockReward24 { get; set; }
        public int LastBlock { get; set; }
        public double Difficulty { get; set; }
        public double Difficulty24 { get; set; }
        public long Nethash { get; set; }
        public double ExchangeRate { get; set; }
        public double ExchangeRate24 { get; set; }
        public double ExchangeRateVol { get; set; }
        public string ExchangeRateCurr { get; set; }
        public string MarketCap { get; set; }
        public string EstimatedRewards { get; set; }
        public string EstimatedRewards24 { get; set; }
        public string BtcRevenue { get; set; }
        public string BtcRevenue24 { get; set; }
        public int Profitability { get; set; }
        public int Profitability24 { get; set; }
        public bool Lagging { get; set; }
    }
}