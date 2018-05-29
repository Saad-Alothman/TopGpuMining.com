using System;
using System.Collections.Generic;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace GpuMiningInsights.Tests
{
    [TestClass]
    public class UnitTest1

    {
        [TestMethod]
        public void GetCoinInfoTest()
        {
            try
            {
                GmiApp.Initialize();
                ApiLayerService.LoadCurrencies();
                var coin =WhatToMineService.GetCoinInfo(1);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
        [TestMethod]
        public void TestMethod3()
        {
            //try
            //{
            //    //Coins => {}
            //    //{"365Coin":{"id":74,"tag":"365","algorithm":"Keccak","lagging":true,"listed":false,"status":"No available stats","testing":false}

            //    string coinsUrl = "https://whattomine.com/coins.json";// "https://whattomine.com/calculators.json";
            //    string response = InsighterService.GetHttpResponseText(coinsUrl);
            //  //  var values = CreaDev.Framework.Core.Utils.Serialization.DeSerialize<Dictionary<string, string>>(response);
            //    JObject json = JObject.Parse(response);
            //    JToken coinsjson = json.SelectToken("coins");
            //    JEnumerable<JToken> allCoins = coinsjson.Children();
            //    JEnumerable<JToken> coins = json.First.Children();
            //    foreach (JToken property in coinsjson.Children())
            //    {
            //        JProperty p = property as JProperty;
            //        string coinName = p.Name;
            //        string values = p.Value.ToString();
            //        WhatToMineCoinResponse whatToMineCoinResponse = CreaDev.Framework.Core.Utils.Serialization.DeSerialize<WhatToMineCoinResponse>(values);

            //    }

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}
            try
            {
                var coin = WhatToMineService.GetCoinInfo(1);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }



        }


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
