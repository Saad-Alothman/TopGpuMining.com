using CsQuery;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Console
{
    class Program
    {
        /*
         * 
         element id for etherum hashrate factor_eth_hr

             */


        static void Main(string[] args)
        {
            var gpus = Insighter.GetInsights();
            System.Console.Clear();

            foreach (var item in gpus)
            {
                string gpuName = item.Name;
                string hashPrice = item.LowestHashPrice.HashPrice.ToString();
                string hashPriceSource = item.LowestHashPrice.Source;
                string gpuPriceFromSource = item.PriceSources.FirstOrDefault(s => s.Name == hashPriceSource).Price.ToString();

                System.Console.WriteLine($"GPU {gpuName} HashRate = {item.Hashrate}, HashCost = {hashPrice}, FROM = {hashPriceSource } @ Price : {gpuPriceFromSource }");
            }

            //}
            //catch (Exception ex)
            //{
            //    System.Console.WriteLine(ex);

            //}
            System.Console.WriteLine("PAKTC");
            System.Console.ReadLine();

        }
    }
    public static class Insighter
    {
        public static List<GPU> Gpus = new List<GPU>();
        internal static List<GPU> GetInsights()
        {
            //seed initial data
            LoadData();
            //Gather information
            GatherInfo(Gpus);
            // For each GPU ,calculate Cost / 1 MHs/s  For Each Source
            FindHashCost(Gpus);
            // sort by that value
            Gpus = Gpus.OrderBy(g => g.LowestHashPrice.HashPrice).ToList();
            return Gpus;
        }

        private static void FindHashCost(List<GPU> gpus)
        {
            gpus.ForEach(FindHashCost);
        }
        private static void FindHashCost(GPU gpu)
        {
            gpu.HashPricePerSourceList.Clear();

            foreach (var item in gpu.PriceSources)
            {
                //price for the GPU / Hashrate
                HashPricePerSource hashPricePerSource = new HashPricePerSource()
                {
                    Source = item.Name,
                    HashPrice = item.Price / gpu.Hashrate
                };
                gpu.HashPricePerSourceList.Add(hashPricePerSource);
            }
        }

        private static void GatherInfo(List<GPU> gpus)
        {
            foreach (var gpu in gpus)
            {
                GetPrices(gpu);
                GetHashrate(gpu);
            }
        }

        private static void GetHashrate(GPU gpu)
        {
            string response = GetHttpResponseText(gpu.WhatToMineUrl);
            string hashRateText = GetTextFromHtmlStringByCssSelector(response, "#factor_eth_hr");
            gpu.Hashrate = double.Parse(hashRateText);
        }

        private static string GetTextFromHtmlStringByCssSelector(string response, string CssSelector)
        {
            CQ dom = response;
            CQ textElement = dom[CssSelector];

            string nodeName = textElement.FirstElement().NodeName.ToLower();
            string result = textElement.Text(); ;

            if (nodeName == "input")
                result = textElement.Val();
            return result;
        }
        private static string GetHttpResponseText(string url)
        {
            var options = new PhantomJSOptions();
            var driver = new PhantomJSDriver(options);
            driver.Manage().Window.Size = new Size(1360, 728);
            var size = driver.Manage().Window.Size;

            driver.Navigate().GoToUrl(url);
            //string url = driver.Url;
            //the driver can now provide you with what you need (it will execute the script)
            //get the source of the page
            var source = driver.PageSource;
            //fully navigate the dom
            source = driver.PageSource;
            return source;
        }
        //private static string GetHttpResponseText(string url)
        //{
        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    WebHeaderCollection header = response.Headers;

        //    var encoding = ASCIIEncoding.ASCII;
        //    string responseText = string.Empty;
        //    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
        //    {
        //        responseText = reader.ReadToEnd();
        //    }

        //    return responseText;
        //}

        private static void GetPrices(GPU gpu)
        {
            foreach (var priceSource in gpu.PriceSources)
            {
                string response = GetHttpResponseText(priceSource.URL);
                string PriceText = GetTextFromHtmlStringByCssSelector(response, priceSource.Selector);
                if (PriceText.IndexOf("$") > -1)
                {
                    PriceText = PriceText.Remove(PriceText.IndexOf("$"), 1);
                }
                priceSource.Price = double.Parse(PriceText);
            }
        }
         //aaaa

        private static void LoadData()
        {
            Gpus.Clear();
            Gpus= new List<GPU> { 
                new GPU()
                {
                    Name = "RX 570",
                    WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=0&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=3&adapt_q_570=1&adapt_570=true&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=27.9&factor%5Beth_p%5D=120.0&factor%5Bgro_hr%5D=15.5&factor%5Bgro_p%5D=110.0&factor%5Bx11g_hr%5D=5.6&factor%5Bx11g_p%5D=110.0&factor%5Bcn_hr%5D=700.0&factor%5Bcn_p%5D=110.0&factor%5Beq_hr%5D=260.0&factor%5Beq_p%5D=110.0&factor%5Blrev2_hr%5D=5500.0&factor%5Blrev2_p%5D=110.0&factor%5Bns_hr%5D=630.0&factor%5Bns_p%5D=140.0&factor%5Blbry_hr%5D=115.0&factor%5Blbry_p%5D=115.0&factor%5Bbk2b_hr%5D=840.0&factor%5Bbk2b_p%5D=115.0&factor%5Bbk14_hr%5D=1140.0&factor%5Bbk14_p%5D=115.0&factor%5Bpas_hr%5D=580.0&factor%5Bpas_p%5D=135.0&factor%5Bskh_hr%5D=16.3&factor%5Bskh_p%5D=110.0&factor%5Bn5_hr%5D=18.0&factor%5Bn5_p%5D=110.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=&commit=Calculate",
                    PriceSources = new List<PriceSource>(){new PriceSource(){Name="New Egg",URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA6V661W8467&cm_re=rx_570-_-9SIA6V661W8467-_-Product",Selector="#landingpage-price .price-current"}
                    }
                },
                  new GPU()
                  {
                      Name = "RX 480",
                      WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=0&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=1&adapt_480=true&adapt_q_570=0&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=29.5&factor%5Beth_p%5D=135.0&factor%5Bgro_hr%5D=18.0&factor%5Bgro_p%5D=130.0&factor%5Bx11g_hr%5D=6.7&factor%5Bx11g_p%5D=140.0&factor%5Bcn_hr%5D=730.0&factor%5Bcn_p%5D=110.0&factor%5Beq_hr%5D=290.0&factor%5Beq_p%5D=120.0&factor%5Blrev2_hr%5D=4900.0&factor%5Blrev2_p%5D=130.0&factor%5Bns_hr%5D=650.0&factor%5Bns_p%5D=150.0&factor%5Blbry_hr%5D=95.0&factor%5Blbry_p%5D=140.0&factor%5Bbk2b_hr%5D=990.0&factor%5Bbk2b_p%5D=150.0&factor%5Bbk14_hr%5D=1400.0&factor%5Bbk14_p%5D=150.0&factor%5Bpas_hr%5D=690.0&factor%5Bpas_p%5D=135.0&factor%5Bskh_hr%5D=18.0&factor%5Bskh_p%5D=115.0&factor%5Bn5_hr%5D=19.0&factor%5Bn5_p%5D=115.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=Main&commit=Calculate",
                      PriceSources = new List<PriceSource>(){new PriceSource(){
                          Name="New Egg",
                          URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA6V66TC9897&cm_re=rx_480-_-9SIA6V66TC9897-_-Product",
                          Selector="#landingpage-price .price-current"}
                    }
                  },
                  new GPU()
                  {
                      Name = "R9 380",
                      WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=1&adapt_380=true&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=0&adapt_q_570=0&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=20.2&factor%5Beth_p%5D=145.0&factor%5Bgro_hr%5D=15.5&factor%5Bgro_p%5D=130.0&factor%5Bx11g_hr%5D=2.5&factor%5Bx11g_p%5D=120.0&factor%5Bcn_hr%5D=530.0&factor%5Bcn_p%5D=120.0&factor%5Beq_hr%5D=205.0&factor%5Beq_p%5D=130.0&factor%5Blrev2_hr%5D=6400.0&factor%5Blrev2_p%5D=125.0&factor%5Bns_hr%5D=350.0&factor%5Bns_p%5D=145.0&factor%5Blbry_hr%5D=44.0&factor%5Blbry_p%5D=135.0&factor%5Bbk2b_hr%5D=760.0&factor%5Bbk2b_p%5D=150.0&factor%5Bbk14_hr%5D=1140.0&factor%5Bbk14_p%5D=155.0&factor%5Bpas_hr%5D=480.0&factor%5Bpas_p%5D=145.0&factor%5Bskh_hr%5D=9.0&factor%5Bskh_p%5D=120.0&factor%5Bn5_hr%5D=10.5&factor%5Bn5_p%5D=120.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=Main&commit=Calculate",
                      PriceSources = new List<PriceSource>(){new PriceSource(){
                          Name="New Egg",
                          URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA85V6DW3604",
                          Selector="#landingpage-price .price-current"}
                    }
                  } 
                  };
        }
    }
    public class GPU
    {
        public string Name { get; set; }
        public string WhatToMineUrl { get; set; }
        public List<PriceSource> PriceSources { get; set; }
        public PriceSource LowestPriceSource => PriceSources.OrderBy(p => p.Price).FirstOrDefault();
        public HashPricePerSource LowestHashPrice => HashPricePerSourceList.OrderBy(p => p.HashPrice).FirstOrDefault();
        public List<HashPricePerSource> HashPricePerSourceList { get; set; }
        //MHs/s
        public double Hashrate { get; set; }

        public GPU()
        {
            this.PriceSources = new List<PriceSource>();
            this.HashPricePerSourceList = new List<HashPricePerSource>();
        }

    }
    public class HashPricePerSource
    {
        //Amazon, etc
        public string Source { get; set; }
        public double HashPrice { get; set; }
    }
    public class PriceSource
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Selector { get; set; }
        public double Price { get; set; }
    }
}
