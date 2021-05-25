using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TopGpuMining.Application.Services.External.Models;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Application.Services
{
    public class AmazonService
    {
        Dictionary<PriceSourceType, string> amazonStores = new()
        {
            { PriceSourceType.AmazonCanada, "CA" },
            { PriceSourceType.AmazonIndia, "IN" },
            { PriceSourceType.AmazonUs, "US" },
            { PriceSourceType.AmazonUk, "GB" }
        };
        public List<PriceSourceItem> GetProductDetails(string asin, PriceSourceType priceSourceType = PriceSourceType.AmazonUs)
        {
            List<PriceSourceItem> results = new List<PriceSourceItem>();
            string filetype = "json";
            var baseDir = Assembly.GetExecutingAssembly().Location;
            baseDir = Path.GetDirectoryName(baseDir);
            string filePath = $"{baseDir}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}";
            string country = amazonStores[priceSourceType];
            string argument = $"/C amazon-buddy asin {asin} --filetype {filetype} --random-ua --country {country}";
            string output = ExecuteCommand(filePath, argument);

            string fileName = output.Substring(output.IndexOf(":") + 1).Trim();
            string resultJson = System.IO.File.ReadAllText($"{filePath}{Path.DirectorySeparatorChar}{fileName}.{filetype}");
            System.IO.File.Delete($"{filePath}{Path.DirectorySeparatorChar}{fileName}.{filetype}");

            if (!string.IsNullOrWhiteSpace(resultJson))
            {
                var products = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<AmazonBuddyProductResponse>>(resultJson);
                results.AddRange(products.ToPriceSourceItems());

            }

            return results;
        }
        public List<PriceSourceItemOld> GetProductDetailsOld(string asin, PriceSourceType priceSourceType = PriceSourceType.AmazonUs)
        {
            List<PriceSourceItemOld> results = new List<PriceSourceItemOld>();
            string filetype = "json";
            var baseDir = Assembly.GetExecutingAssembly().Location;
            baseDir = Path.GetDirectoryName(baseDir);
            string filePath = $"{baseDir}{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}";
            string country = amazonStores[priceSourceType];
            string argument = $"/C amazon-buddy asin {asin} --filetype {filetype} --random-ua --country {country}";
            string output = ExecuteCommand(filePath, argument);

            string fileName = output.Substring(output.IndexOf(":") + 1).Trim();
            string resultJson = System.IO.File.ReadAllText($"{filePath}{Path.DirectorySeparatorChar}{fileName}.{filetype}");
            System.IO.File.Delete($"{filePath}{Path.DirectorySeparatorChar}{fileName}.{filetype}");

            if (!string.IsNullOrWhiteSpace(resultJson))
            {
                var products = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<AmazonBuddyProductResponse>>(resultJson);
                results.AddRange(products.ToPriceSourceItemsOld());

            }

            return results;
        }

        private string ExecuteCommand(string filePath, string argument)
        {
            string output = string.Empty;
            Process p = new Process();
            p.StartInfo.WorkingDirectory = filePath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.StartInfo.FileName = @"cmd.exe";
            //string argument = "/C amazon-buddy products -k 'vacume cleaner' -n 40 --filetype json --random-ua";
            //
            p.StartInfo.Arguments = argument;
            p.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                output += e.Data;
            };
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();

            return output;
        }

       
    }

    public static class AmazonExtension
    {
        public static List<PriceSourceItem> ToPriceSourceItems(this IEnumerable<AmazonBuddyProductResponse> amazonItemResponse)
        {
            List<PriceSourceItem> result = new List<PriceSourceItem>();
            if (amazonItemResponse == null) return result;
            foreach (var item in amazonItemResponse)
            {
                string asin = item.Asin;
                string url = item.Url.AbsoluteUri;
                string imageUrl = item.MainImage?.AbsoluteUri;

                //if (string.IsNullOrWhiteSpace(imageUrl) && item.ImageSets?.Length > 0)
                //    imageUrl = item.ImageSets.FirstOrDefault()?.LargeImage?.URL;

                string itemName = item.Title;

                string model = item.ProductInformation.ModelNumber;
                //string modelYear = item.;
                string brand = item.ProductInformation.Brand;
                string manufacturer = item.ProductInformation.Manufacturer;
                string merchant = item.ProductInformation.StoreId;

                if (string.IsNullOrWhiteSpace(itemName))
                    itemName = item.ToString();


                PriceSourceItem priceSourceItem = new PriceSourceItem();
                priceSourceItem.Merchant = merchant;
                priceSourceItem.ASIN = asin;
                priceSourceItem.ItemURL = url;
                priceSourceItem.ImageUrl = imageUrl;
                priceSourceItem.ItemName = itemName;
                priceSourceItem.Model = model;
                priceSourceItem.Brand = brand;
                priceSourceItem.Manufacturer = manufacturer;
                priceSourceItem.Price = item.Price.CurrentPrice;
                priceSourceItem.PriceCurrency = item.Price.Currency;

                result.Add(priceSourceItem);
            }

            return result;

        } public static List<PriceSourceItemOld> ToPriceSourceItemsOld(this IEnumerable<AmazonBuddyProductResponse> amazonItemResponse)
        {
            List<PriceSourceItemOld> result = new List<PriceSourceItemOld>();
            if (amazonItemResponse == null) return result;
            foreach (var item in amazonItemResponse)
            {
                string asin = item.Asin;
                string url = item.Url.AbsoluteUri;
                string imageUrl = item.MainImage?.AbsoluteUri;

                //if (string.IsNullOrWhiteSpace(imageUrl) && item.ImageSets?.Length > 0)
                //    imageUrl = item.ImageSets.FirstOrDefault()?.LargeImage?.URL;

                string itemName = item.Title;

                string model = item.ProductInformation.ModelNumber;
                //string modelYear = item.;
                string brand = item.ProductInformation.Brand;
                string manufacturer = item.ProductInformation.Manufacturer;
                string merchant = item.ProductInformation.StoreId;

                if (string.IsNullOrWhiteSpace(itemName))
                    itemName = item.ToString();


                PriceSourceItemOld priceSourceItem = new PriceSourceItemOld();
                priceSourceItem.Merchant = merchant;
                priceSourceItem.ASIN = asin;
                priceSourceItem.URL = url;
                priceSourceItem.ImageUrl = imageUrl;
                priceSourceItem.Name = itemName;
                priceSourceItem.Model = model;
                priceSourceItem.Brand = brand;
                priceSourceItem.Manufacturer = manufacturer;
                priceSourceItem.Price = item.Price.CurrentPrice;
                priceSourceItem.PriceCurrency = item.Price.Currency;

                result.Add(priceSourceItem);
            }

            return result;

        }
    }

}