using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CsQuery;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Core;
using Newtonsoft.Json;
using OpenQA.Selenium.PhantomJS;

namespace GpuMiningInsights.Console
{
    public static class InsighterService
    {

        public static List<GPU> Gpus = new List<GPU>();
        public static List<GPU> GetInsights()
        {
            //PhantomJs & Selenium
            InitDriver();



            //seed initial data, ideally this will be read from DB
            LoadData();
            //Gather information
            GatherInfo(Gpus);
            // For each GPU ,calculate Cost / 1 MHs/s  For Each Source
            FindHashCost(Gpus);
            // sort by that value
            Gpus = Gpus.OrderByDescending(g => g.ProfitPerYearMinusCostUsd).ToList();


            service.Dispose();
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
                //TODO:now using the lowest price from the results, maybe use different hash price for each source
                //price for the GPU / Hashrate
                if (item.PriceSourceItems.Any())
                {
                    double price = item.PriceSourceItems.Min(p => p.Price);
                    HashPricePerSource hashPricePerSource = new HashPricePerSource()
                    {
                        Source = item.Name,
                        HashPrice = price / gpu.Hashrate
                    };
                    gpu.HashPricePerSourceList.Add(hashPricePerSource);
                }

            }
        }

        private static void GatherInfo(List<GPU> gpus)
        {
            foreach (var gpu in gpus)
            {
                System.Console.WriteLine($"Getting data for GPU {gpu.Name}");

                GetPrices(gpu);
                GetHashrate(gpu);
                GeRevenueAndProfit(gpu, "table tbody tr");
                GetMiningProfitability(gpu);
            }
        }

        private static void GetHashrate(GPU gpu)
        {
            System.Console.WriteLine($"Getting Hashrate From WhatToMine For GPU {gpu.Name}");

            string response = GetHttpResponseText(gpu.WhatToMineUrl);
            string hashRateText = GetTextFromHtmlStringByCssSelector(response, "#factor_eth_hr");
            gpu.Hashrate = double.Parse(hashRateText);
        }
        //private static string GeRevenueAndProfit(string response, string CssSelector, string targetCurrency)
        //{
        //    CQ dom = response;
        //    CQ rows = dom[CssSelector];
        //    foreach (var row in rows)
        //    {
        //        var firstColumn = row.ChildElements.FirstOrDefault();
        //        CQ anchor = ((CQ)firstColumn.InnerHTML)["div:nth-child(2) a"];
        //        string cryptoname = anchor.Text();
        //        if (cryptoname != targetCurrency) continue;
        //        var seventhColumn = row.ChildElements.ElementAt(7);
        //        string revenueText = seventhColumn.InnerText;
        //        //second element bcoz of the <br />
        //        string profitText = seventhColumn.ChildElements.ElementAt(1).InnerText;

        //    }
        //    //string nodeName = textElement.FirstElement().NodeName.ToLower();
        //    //string result = textElement.Text(); ;

        //    //if (nodeName == "input")
        //    //    result = textElement.Val();
        //    return "";
        //}
        private static string GeRevenueAndProfit(GPU gpu, string CssSelector)
        {
            System.Console.WriteLine($"Getting Revenu and profit From What to mine For GPU {gpu.Name}");
            string response = GetHttpResponseText(gpu.WhatToMineUrl);
            CQ dom = response;
            CQ rows = dom[CssSelector];
            foreach (var row in rows)
            {
                var firstColumn = row.ChildElements.FirstOrDefault();
                CQ anchor = ((CQ)firstColumn.InnerHTML)["div:nth-child(2) a"];
                string cryptoname = anchor.Text();
                if (cryptoname != gpu.CoinToStudyName) continue;
                var seventhColumn = row.ChildElements.ElementAt(7);
                string revenueText = seventhColumn.InnerText;
                //second element bcoz of the <br />
                string profitText = seventhColumn.ChildElements.ElementAt(1).InnerText;
                gpu.RevenuePerDayUsd = double.Parse(revenueText.Replace("$", ""));
                gpu.ProfitPerDayUsd = double.Parse(profitText.Replace("$", ""));
            }
            //string nodeName = textElement.FirstElement().NodeName.ToLower();
            //string result = textElement.Text(); ;

            //if (nodeName == "input")
            //    result = textElement.Val();
            return "";
        }
        private static void GetMiningProfitability(GPU gpu)
        {
            System.Console.WriteLine($"Getting Mining Profitability From What to mine based on {gpu.CoinToStudyName} For GPU {gpu.Name}");

            //https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=0&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=3&adapt_q_570=1&adapt_570=true&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=27.9&factor%5Beth_p%5D=120.0&factor%5Bgro_hr%5D=15.5&factor%5Bgro_p%5D=110.0&factor%5Bx11g_hr%5D=5.6&factor%5Bx11g_p%5D=110.0&factor%5Bcn_hr%5D=700.0&factor%5Bcn_p%5D=110.0&factor%5Beq_hr%5D=260.0&factor%5Beq_p%5D=110.0&factor%5Blrev2_hr%5D=5500.0&factor%5Blrev2_p%5D=110.0&factor%5Bns_hr%5D=630.0&factor%5Bns_p%5D=140.0&factor%5Blbry_hr%5D=115.0&factor%5Blbry_p%5D=115.0&factor%5Bbk2b_hr%5D=840.0&factor%5Bbk2b_p%5D=115.0&factor%5Bbk14_hr%5D=1140.0&factor%5Bbk14_p%5D=115.0&factor%5Bpas_hr%5D=580.0&factor%5Bpas_p%5D=135.0&factor%5Bskh_hr%5D=16.3&factor%5Bskh_p%5D=110.0&factor%5Bn5_hr%5D=18.0&factor%5Bn5_p%5D=110.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=&commit=Calculate
            string response = GetHttpResponseText(gpu.WhatToMineUrl);
            // get the table

            // get the first row
            // first column
            // crypto name
            string cryptoName = GetTextFromHtmlStringByCssSelector(response, "body table tbody tr:first td:first div:nth-child(2)");

            //algo
            string cryptoAlgo = GetTextFromHtmlStringByCssSelector(response, "body table tbody tr:first td:first div:nth-child(3)");
            //8th column second line, profitability
            //$('').text()
            string profitability = GetTextFromHtmlStringByCssSelector(response, "body table tbody tr:first td:nth-child(8) strong");
            gpu.MiningProfitability.CryptoName = cryptoName;
            gpu.MiningProfitability.CryptoAlgo = cryptoAlgo;
            string profitabilityText = profitability;
            if (profitabilityText.IndexOf("$") > -1)
            {
                profitabilityText = profitabilityText.Remove(profitabilityText.IndexOf("$"), 1);
            }
            gpu.MiningProfitability.Profitability24Hours = double.Parse(profitabilityText);
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

        public static void InitDriver()
        {
            service = PhantomJSDriverService.CreateDefaultService();
            //service.IgnoreSslErrors = true;
            //service.LoadImages = false;
            //service.ProxyType = "none";
            service.SslProtocol = "tlsv1"; //"any" also works

            driver = new PhantomJSDriver(service);
            options = new PhantomJSOptions();

            //options.SetLoggingPreference(LogType.Browser,LogLevel.Warning);


            driver = new PhantomJSDriver(service, options);
            //driver.ExecutePhantomJS("(service_args=['--ignore-ssl-errors=true', '--ssl-protocol=TLSv1']");
            driver.Manage().Window.Size = new Size(1360, 728);
            var size = driver.Manage().Window.Size;
        }

        static PhantomJSDriverService service;
        static PhantomJSOptions options;
        static PhantomJSDriver driver;

        private static string GetHttpResponseTextWithJavascript(string url, int attemptCount = 0)
        {


            driver.Navigate().GoToUrl(url);
            //string url = driver.Url;
            //the driver can now provide you with what you need (it will execute the script)
            //get the source of the page
            var source = driver.PageSource;
            //fully navigate the dom
            source = driver.PageSource;
            string emptyHtml = "<html><head></head><body></body></html>";
            if (source == emptyHtml && attemptCount < 4)
            {
                int count = attemptCount + 1;
                return GetHttpResponseTextWithJavascript(url, count);
            }
            return source;
        }
        private static string GetHttpResponseText(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";


            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var encoding = Encoding.GetEncoding(response.CharacterSet);

                using (var responseStream = response.GetResponseStream())
                using (var reader = new StreamReader(responseStream, encoding))
                    return reader.ReadToEnd();
            }
        }

        public static void PushData()
        {
            string url = "http://localhost/GpuMiningInsights.Web/Home/PushData";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(Gpus);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
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
                System.Console.WriteLine($"Getting Price From {priceSource.Name} For GPU {gpu.Name}");
                //is it an api or something, else we are going to scrape the shit out of it...
                if (priceSource.PriceSourceAction != null)
                {
                    List<PriceSourceItem> result = priceSource.PriceSourceAction.Invoke(priceSource.PriceSourceItemIdentifier);
                    priceSource.PriceSourceItems.AddRange(result);

                }
                else
                {
                    string response = GetHttpResponseTextWithJavascript(priceSource.URL);
                    string PriceText = GetTextFromHtmlStringByCssSelector(response, priceSource.Selector);
                    string currency = "USD";
                    string imageUrl = null;
                    if (!string.IsNullOrWhiteSpace(priceSource.ImageUrlSelector))
                        imageUrl = GetTextFromHtmlStringByCssSelector(response, priceSource.ImageUrlSelector);

                    if (PriceText.IndexOf("$") > -1)
                    {

                        PriceText = PriceText.Remove(PriceText.IndexOf("$"), 1);
                        currency = "USD";
                    }
                    if (PriceText.IndexOf("SR") > -1)
                    {
                        PriceText = PriceText.Remove(PriceText.IndexOf("SR"), 2);
                        currency = "SAR";
                    }
                    if (string.IsNullOrWhiteSpace(imageUrl))
                        priceSource.AddPriceSourceItem(PriceText);
                    else
                        priceSource.AddPriceSourceItem(PriceText, imageUrl);
                }

            }
        }
        //aaaa ssss
        private static void LoadData()
        {
            Gpus.Clear();
            List<GPU> gpus = new List<GPU>();
            //Gpus = new List<GPU> {
            //    new GPU()
            //    {
            //        Name = "RX 570",
            //        CoinToStudyName="Ethereum(ETH)",
            //        WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=0&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=3&adapt_q_570=1&adapt_570=true&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=27.9&factor%5Beth_p%5D=120.0&factor%5Bgro_hr%5D=15.5&factor%5Bgro_p%5D=110.0&factor%5Bx11g_hr%5D=5.6&factor%5Bx11g_p%5D=110.0&factor%5Bcn_hr%5D=700.0&factor%5Bcn_p%5D=110.0&factor%5Beq_hr%5D=260.0&factor%5Beq_p%5D=110.0&factor%5Blrev2_hr%5D=5500.0&factor%5Blrev2_p%5D=110.0&factor%5Bns_hr%5D=630.0&factor%5Bns_p%5D=140.0&factor%5Blbry_hr%5D=115.0&factor%5Blbry_p%5D=115.0&factor%5Bbk2b_hr%5D=840.0&factor%5Bbk2b_p%5D=115.0&factor%5Bbk14_hr%5D=1140.0&factor%5Bbk14_p%5D=115.0&factor%5Bpas_hr%5D=580.0&factor%5Bpas_p%5D=135.0&factor%5Bskh_hr%5D=16.3&factor%5Bskh_p%5D=110.0&factor%5Bn5_hr%5D=18.0&factor%5Bn5_p%5D=110.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=&commit=Calculate",
            //        PriceSources = new List<PriceSource>(){
            //            new PriceSource()
            //            {
            //                Name="New Egg",
            //                URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA6V661W8467&cm_re=rx_570-_-9SIA6V661W8467-_-Product",
            //                RequiresJavascript=true,
            //                Selector="#landingpage-price .price-current"
            //            }
            //            ,new PriceSource()
            //            {
            //                PriceSourceItemIdentifier= "B06ZYC3SW1",
            //                PriceSourceAction =AmazonService.SearchItemLookupOperation,
            //                Name="Amazon",
            //                URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA6V661W8467&cm_re=rx_570-_-9SIA6V661W8467-_-Product",
            //                RequiresJavascript=false,
            //                Selector="#landingpage-price .price-current"
            //            }
            //        }
            //    },
            //    new GPU()
            //    {
            //        Name = "RX 480",
            //        WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=0&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=1&adapt_480=true&adapt_q_570=0&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=29.5&factor%5Beth_p%5D=135.0&factor%5Bgro_hr%5D=18.0&factor%5Bgro_p%5D=130.0&factor%5Bx11g_hr%5D=6.7&factor%5Bx11g_p%5D=140.0&factor%5Bcn_hr%5D=730.0&factor%5Bcn_p%5D=110.0&factor%5Beq_hr%5D=290.0&factor%5Beq_p%5D=120.0&factor%5Blrev2_hr%5D=4900.0&factor%5Blrev2_p%5D=130.0&factor%5Bns_hr%5D=650.0&factor%5Bns_p%5D=150.0&factor%5Blbry_hr%5D=95.0&factor%5Blbry_p%5D=140.0&factor%5Bbk2b_hr%5D=990.0&factor%5Bbk2b_p%5D=150.0&factor%5Bbk14_hr%5D=1400.0&factor%5Bbk14_p%5D=150.0&factor%5Bpas_hr%5D=690.0&factor%5Bpas_p%5D=135.0&factor%5Bskh_hr%5D=18.0&factor%5Bskh_p%5D=115.0&factor%5Bn5_hr%5D=19.0&factor%5Bn5_p%5D=115.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=Main&commit=Calculate",
            //        PriceSources = new List<PriceSource>(){new PriceSource(){
            //            Name="New Egg",
            //            URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA6V66TC9897&cm_re=rx_480-_-9SIA6V66TC9897-_-Product",
            //            Selector="#landingpage-price .price-current"
            //            ,RequiresJavascript=true}
            //        }
            //    },
            //    new GPU()
            //    {
            //        Name = "R9 380",
            //        WhatToMineUrl = "https://whattomine.com/coins?utf8=%E2%9C%93&adapt_q_280x=0&adapt_q_380=1&adapt_380=true&adapt_q_fury=0&adapt_q_470=0&adapt_q_480=0&adapt_q_570=0&adapt_q_580=0&adapt_q_vega56=0&adapt_q_vega64=0&adapt_q_750Ti=0&adapt_q_1050Ti=0&adapt_q_10606=0&adapt_q_1070=0&adapt_q_1070Ti=0&adapt_q_1080=0&adapt_q_1080Ti=0&eth=true&factor%5Beth_hr%5D=20.2&factor%5Beth_p%5D=145.0&factor%5Bgro_hr%5D=15.5&factor%5Bgro_p%5D=130.0&factor%5Bx11g_hr%5D=2.5&factor%5Bx11g_p%5D=120.0&factor%5Bcn_hr%5D=530.0&factor%5Bcn_p%5D=120.0&factor%5Beq_hr%5D=205.0&factor%5Beq_p%5D=130.0&factor%5Blrev2_hr%5D=6400.0&factor%5Blrev2_p%5D=125.0&factor%5Bns_hr%5D=350.0&factor%5Bns_p%5D=145.0&factor%5Blbry_hr%5D=44.0&factor%5Blbry_p%5D=135.0&factor%5Bbk2b_hr%5D=760.0&factor%5Bbk2b_p%5D=150.0&factor%5Bbk14_hr%5D=1140.0&factor%5Bbk14_p%5D=155.0&factor%5Bpas_hr%5D=480.0&factor%5Bpas_p%5D=145.0&factor%5Bskh_hr%5D=9.0&factor%5Bskh_p%5D=120.0&factor%5Bn5_hr%5D=10.5&factor%5Bn5_p%5D=120.0&factor%5Bl2z_hr%5D=420.0&factor%5Bl2z_p%5D=300.0&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=binance&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=Main&commit=Calculate",
            //        PriceSources = new List<PriceSource>(){new PriceSource(){
            //            Name="New Egg",
            //            URL="https://www.newegg.com/Product/Product.aspx?Item=9SIA85V6DW3604",
            //            Selector="#landingpage-price .price-current"
            //            ,RequiresJavascript=true}
            //        }
            //    }
            //};

            gpus = JsonConvert.DeserializeObject<List<GPU>>(File.ReadAllText("gpus.json"));
            Gpus = gpus;
        }
    }
}