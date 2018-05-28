using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using Newtonsoft.Json.Linq;

namespace GpuMiningInsights.Application.Services
{
    public static class WhatToMineService
    {
        public static List<Coin> GetAllCoins()
        {
            List<Coin> allCoins = new List<Coin>();
            string coinsUrl = "https://whattomine.com/coins.json";// "https://whattomine.com/calculators.json";
            string response = InsighterService.GetHttpResponseText(coinsUrl);
            //  var values = CreaDev.Framework.Core.Utils.Serialization.DeSerialize<Dictionary<string, string>>(response);
            JObject json = JObject.Parse(response);
            JToken coinsjson = json.SelectToken("coins");
            foreach (JToken property in coinsjson.Children())
            {
                JProperty p = property as JProperty;
                string coinName = p.Name;
                string values = p.Value.ToString();
                WhatToMineCoinResponse whatToMineCoinResponse = CreaDev.Framework.Core.Utils.Serialization.DeSerialize<WhatToMineCoinResponse>(values);
                allCoins.Add(whatToMineCoinResponse.ToCoin(coinName));
            }

            return allCoins;
        }
    }

    public class WhatToMineCoinResponse
    {
        public int id { get; set; }
        public string tag { get; set; }
        public string algorithm { get; set; }
        public string block_time { get; set; }
        public double block_reward { get; set; }
        public double block_reward24 { get; set; }
        public int last_block { get; set; }
        public double difficulty { get; set; }
        public double difficulty24 { get; set; }
        public long nethash { get; set; }
        public double exchange_rate { get; set; }
        public double exchange_rate24 { get; set; }
        public double exchange_rate_vol { get; set; }
        public string exchange_rate_curr { get; set; }
        public string market_cap { get; set; }
        public string estimated_rewards { get; set; }
        public string estimated_rewards24 { get; set; }
        public string btc_revenue { get; set; }
        public string btc_revenue24 { get; set; }
        public int profitability { get; set; }
        public int profitability24 { get; set; }
        public bool lagging { get; set; }
        public int timestamp { get; set; }

        public Coin ToCoin(string name)
        {
            Coin coin = new Coin() { Name = name };
            coin.Tag = this.tag;
            IAlgorithmService algorithmService =  ServiceLocator.Get<IAlgorithmService>();
            var algorithmInDb =  algorithmService.AddOrUpdate(new Algorithm() {Name = new LocalizableTextRequired(algorithm, algorithm)});
            
            coin.WhatToMineId = id;
            //coin.Algorithm = this.algorithm;
            coin.AlgorithmId = algorithmInDb.Id;
            coin.BlockTime = this.block_time;
            coin.BlockReward = this.block_reward;
            coin.BlockReward24 = this.block_reward24;
            coin.LastBlock = this.last_block;
            coin.Difficulty = this.difficulty;
            coin.Difficulty24 = this.difficulty24;
            coin.Nethash = this.nethash;
            coin.ExchangeRate = this.exchange_rate;
            coin.ExchangeRate24 = this.exchange_rate24;
            coin.ExchangeRateVol = this.exchange_rate_vol;
            coin.ExchangeRateCurr = this.exchange_rate_curr;
            coin.MarketCap = this.market_cap;
            coin.EstimatedRewards = this.estimated_rewards;
            coin.EstimatedRewards24 = this.estimated_rewards24;
            coin.BtcRevenue = this.btc_revenue;
            coin.BtcRevenue24 = this.btc_revenue24;
            coin.Profitability = this.profitability;
            coin.Profitability24 = this.profitability24;
            coin.Lagging = this.lagging;

            return coin;
        }
    }
}
