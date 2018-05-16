using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using GpuMiningInsights.Application.Amazon;
using GpuMiningInsights.Core;
using GpuMiningInsights.Domain.Models;
using Newtonsoft.Json;
using OpenQA.Selenium.PhantomJS;

namespace GpuMiningInsights.Application.Services
{
    public static class InsighterService
    {

        public static List<GPU> Gpus = new List<GPU>();
        public static Dictionary<string, double> pricesDict;
        private static string baseCurrency = "USD";

        public static void TestDriver()
        {
            InitDriver();
            driver.Close();
            driver.Quit();
            driver.Dispose();
            service.Dispose();
            driver.Dispose();

        }
        public static List<GPU> GetInsights()
        {
            //PhantomJs & Selenium

            InitDriver();

            //seed initial data, ideally this will be read from DB
            LoadData();
            LoadCurrencies();
            //Gather information
            GatherInfo(Gpus);
            // For each GPU ,calculate Cost / 1 MHs/s  For Each Source
            FindHashCost(Gpus);
            // sort by that value
            Gpus = Gpus.OrderByDescending(g => g.ROI).ToList();


            driver.Close();
            driver.Quit();
            driver.Dispose();
            service.Dispose();
            driver.Dispose();
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
                WriteLine($"Getting data for GPU {gpu.Name}");

                GetPrices(gpu);
                GetHashrate(gpu);
                GeRevenueAndProfit(gpu, "table tbody tr");
                GetMiningProfitability(gpu);
            }
            CalculatePricesBasedOnUSD(gpus);
        }

        private static void CalculatePricesBasedOnUSD(List<GPU> gpus)
        {
            string baseCurrency = "USD";
            string currencyApiUrl = "http://www.apilayer.net/api/live?access_key=ff139c408a9439cd66d94f7ee26a598b&format=1&source=USD";

            string respomseText = GetHttpResponseText(currencyApiUrl);
            var pricesDictResponse = JsonConvert.DeserializeObject<CurrencyPricesResponse>(respomseText);
            var pricesDict = pricesDictResponse.quotes;
            foreach (var priceSourceItem in gpus.SelectMany(s => s.PriceSources).SelectMany(a => a.PriceSourceItems))
            {
                string curr = priceSourceItem.PriceCurrency.ToUpper();
                string targetCurrKey = baseCurrency + curr;
                if (pricesDict.ContainsKey(targetCurrKey))
                {
                    //this is what 1 USD equals
                    double rate = pricesDict[targetCurrKey];
                    //1 USD = rate in target currency
                    //? USD = price target currency
                    //so divide the price by the rate
                    double priceUsd = priceSourceItem.Price / rate;
                    priceSourceItem.PriceUSD = Math.Round(priceUsd, 2);
                }
            }
        }
        public static void LoadCurrencies()
        {
            string currencyApiUrl = "http://www.apilayer.net/api/live?access_key=ff139c408a9439cd66d94f7ee26a598b&format=1&source=USD";

            string respomseText = GetHttpResponseText(currencyApiUrl);
            var pricesDictResponse = JsonConvert.DeserializeObject<CurrencyPricesResponse>(respomseText);
            pricesDict = pricesDictResponse.quotes;
        }
        private static void FillUSDPrice(PriceSourceItem priceSourceItem)
        {

            if (priceSourceItem == null || priceSourceItem.Price <= 0 || string.IsNullOrWhiteSpace(priceSourceItem.PriceCurrency))
                return;

            string curr = priceSourceItem.PriceCurrency.ToUpper();
            string targetCurrKey = baseCurrency + curr;
            if (pricesDict.ContainsKey(targetCurrKey))
            {
                //this is what 1 USD equals
                double rate = pricesDict[targetCurrKey];
                //1 USD = rate in target currency
                //? USD = price target currency
                //so divide the price by the rate
                double priceUsd = priceSourceItem.Price / rate;
                priceSourceItem.PriceUSD = Math.Round(priceUsd, 2);
            }
        }

        private static void GetHashrate(GPU gpu)
        {
            WriteLine($"Getting Hashrate From WhatToMine For GPU {gpu.Name}");

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
            WriteLine($"Getting Revenu and profit From What to mine For GPU {gpu.Name}");
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
            WriteLine($"Getting Mining Profitability From What to mine based on {gpu.CoinToStudyName} For GPU {gpu.Name}");

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
            string nodeName = textElement.FirstElement()?.NodeName?.ToLower();
            string result = textElement.Text();

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
        public static string GetHttpResponseText(string url)
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

        public static void PushData(ClientGpuListData clientGpuListData = null)
        {
            string baseUrl = ConfigurationManager.AppSettings["WebAppBaseURL"];
            string url = $"{baseUrl}/Home/PushData";
            HttpClient client = new HttpClient();
            if (clientGpuListData == null)
                clientGpuListData = new ClientGpuListData()
                {
                    Date = DateTime.Now.ToString(Settings.DateFormat),
                    Gpus = Gpus
                };
            string json= JsonConvert.SerializeObject(clientGpuListData);
            var values = new Dictionary<string, string>
{
   { "clientGpuListDataJson", json  },
};

            var content = new FormUrlEncodedContent(values);
            var response = Task.Run(()=> client.PostAsync(url, content)).Result;
            var responseString = Task.Run(()=> response.Content.ReadAsStringAsync()).Result;

            
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
                WriteLine($"Getting Price From {priceSource.Name} For GPU {gpu.Name}");
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

                    string nameText = GetTextFromHtmlStringByCssSelector(response, priceSource.ItemNameSelector);
                    if (string.IsNullOrEmpty(PriceText))
                    {
                        continue;
                    }
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
                        priceSource.AddPriceSourceItem(PriceText, nameText, currency);
                    else
                        priceSource.AddPriceSourceItem(PriceText, nameText, currency, imageUrl);
                }

            }
            //Calculate USD Price
            foreach (var priceSourceItem in gpu.PriceSources.SelectMany(s => s.PriceSourceItems))
            {
                FillUSDPrice(priceSourceItem);
            }
        }
        //aaaa ssss
        private static void LoadData()
        {
            Gpus.Clear();
            List<GPU> gpus = new List<GPU>();
           //TODO: will be moved to databse
            gpus = JsonConvert.DeserializeObject<List<GPU>>(File.ReadAllText("gpus.json"));
            foreach (var item in gpus)
            {
                foreach (var psources in item.PriceSources.Where(s => s.Name.ToLower().Contains("amazon")))
                {
                    psources.PriceSourceAction = AmazonService.SearchItemLookupOperation;
                }
            }
            Gpus = gpus;
        }

        public static void WriteLine(string message)
        {
            bool isDebug;
            if (bool.TryParse(ConfigurationManager.AppSettings["IsDebug"],out isDebug) && isDebug)
            {
                System.Console.WriteLine(message);
            }
        }
    }

}
