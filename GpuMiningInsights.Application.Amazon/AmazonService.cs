using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GpuMiningInsights.Core;
using GpuMiningInsights.Domain.Models;
using Newtonsoft.Json;

namespace GpuMiningInsights.Application.Amazon
{

    //https://github.com/tinohager/Nager.AmazonProductAdvertising
    public static class AmazonService
    {
        //"B076GZ3JFC"
        public static List<PriceSourceItem> Search(string term, AmazonSearchIndex amazonSearchIndex = AmazonSearchIndex.All, AmazonEndpoint? endpoint = null)
        {

            List<PriceSourceItem> results = new List<PriceSourceItem>();
            List<Endpoint> endpointsToSearch = new List<Endpoint>();
            if (endpoint != null)
                endpointsToSearch.Add(Endpoints.Get(endpoint.Value));
            else
                endpointsToSearch.AddRange(Endpoints.EndpointsList);

            foreach (var endpointItem in endpointsToSearch)
            {
                var authentication = new AmazonAuthentication();
                authentication.AccessKey = endpointItem.accessKey;
                authentication.SecretKey = endpointItem.secreteKey;
                endpoint = (AmazonEndpoint)Enum.Parse(typeof(AmazonEndpoint), endpointItem.endpointCode);
                var wrapper = new AmazonWrapper(authentication, endpoint.Value, endpointItem.merchantId);
                string searchTerm = "ASUS DUAL-RX580-O8G Radeon RX 580 8 GB GDDR5";
                //string searchTerm = //"B076GZ3JFC";
                AmazonItemResponse result = wrapper.Search(term, AmazonSearchIndex.All);
                results.AddRange(result.ToPriceSourceItems());
            }

            return results;
        }
        public static List<PriceSourceItem> SearchLookup(string term, AmazonEndpoint? endpoint = null)
        {
            List<PriceSourceItem> results = new List<PriceSourceItem>();
            List<Endpoint> endpointsToSearch = new List<Endpoint>();
            if (endpoint != null)
                endpointsToSearch.Add(Endpoints.Get(endpoint.Value));
            else
                endpointsToSearch.AddRange(Endpoints.EndpointsList);

            foreach (var endpointItem in endpointsToSearch)
            {
                var authentication = new AmazonAuthentication();
                authentication.AccessKey = endpointItem.accessKey;
                authentication.SecretKey = endpointItem.secreteKey;

                endpoint = (AmazonEndpoint)Enum.Parse(typeof(AmazonEndpoint), endpointItem.endpointCode);
                var wrapper = new AmazonWrapper(authentication, endpoint.Value, endpointItem.merchantId);
                string searchTerm = "ASUS DUAL-RX580-O8G Radeon RX 580 8 GB GDDR5";
                //string searchTerm = //"B076GZ3JFC";
                AmazonItemResponse result = wrapper.Lookup(term);
                results.AddRange(result.ToPriceSourceItems());
            }
            return results;
        }
        public static List<PriceSourceItem> SearchItemLookupOperation(string term)
        {
            return SearchItemLookupOperation(term, null);
        }
        public static List<PriceSourceItem> SearchItemLookupOperation(string term, AmazonEndpoint? endpoint)
        {
            List<PriceSourceItem> results = new List<PriceSourceItem>();
            List<Endpoint> endpointsToSearch = new List<Endpoint>();
            if (endpoint != null)
                endpointsToSearch.Add(Endpoints.Get(endpoint.Value));
            else
                endpointsToSearch.AddRange(Endpoints.EndpointsList);

            foreach (var endpointItem in endpointsToSearch)
            {
                var authentication = new AmazonAuthentication();
                authentication.AccessKey = endpointItem.accessKey;
                authentication.SecretKey = endpointItem.secreteKey;

                endpoint = (AmazonEndpoint)Enum.Parse(typeof(AmazonEndpoint), endpointItem.endpointCode);
                var wrapper = new AmazonWrapper(authentication, endpoint.Value, endpointItem.merchantId);
                var searchOperation = wrapper.ItemLookupOperation(new List<string>() { term });
                ExtendedWebResponse xmlResponse = wrapper.Request(searchOperation);
                var itemLookupResponse = XmlHelper.ParseXml<ItemLookupResponse>(xmlResponse.Content);
                if (itemLookupResponse?.Items?.Item?.FirstOrDefault() != null)
                {
                    string itemJson = JsonConvert.SerializeObject(itemLookupResponse.Items.Item.FirstOrDefault());
                }
                results.AddRange(itemLookupResponse.ToPriceSourceItems());
            }
            return results;






            
        }
    }


    public static class AmazonExtension
    {
        public static List<PriceSourceItem> ToPriceSourceItems(this AmazonItemResponse amazonItemResponse)
        {
            List<PriceSourceItem> result = new List<PriceSourceItem>();
            if (amazonItemResponse == null) return result;
            foreach (var item in amazonItemResponse.Items.Item)
            {
                string asin = item.ASIN;
                string url = item.DetailPageURL;
                string imageUrl = item.LargeImage.URL;

                if (item.Offers.TotalOffers != "0")
                {
                    foreach (var offer in item.Offers.Offer)
                    {

                        string merchant = offer.Merchant?.Name;
                        foreach (var offerListing in offer.OfferListing)
                        {
                            PriceSourceItem priceSourceItem = new PriceSourceItem();

                            priceSourceItem.Merchant = merchant;
                            priceSourceItem.ASIN = asin;
                            string itemName = item.ToString();
                            priceSourceItem.Name = itemName;
                            priceSourceItem.URL = url;
                            priceSourceItem.ImageUrl = imageUrl;
                            string priceStr = offerListing.Price.Amount;
                            priceSourceItem.PriceCurrency = offerListing.Price.CurrencyCode;
                            if (!string.IsNullOrWhiteSpace(offerListing.SalePrice?.Amount))
                            {
                                priceStr = offerListing.SalePrice.Amount;
                                priceSourceItem.PriceCurrency = offerListing.SalePrice.CurrencyCode;
                            }
                            if (priceStr.Length >= 2)
                            {
                                priceStr = priceStr.Insert(priceStr.Length - 2, ".");
                            }
                            priceSourceItem.Price = double.Parse(priceStr);
                            result.Add(priceSourceItem);
                        }
                    }
                }
                else if (item.ItemLinks.FirstOrDefault(l => l.Description == "All Offers") != null)
                {
                    url = item.ItemLinks.FirstOrDefault(l => l.Description == "All Offers").URL;
                    PriceSourceItem priceSourceItem = new PriceSourceItem();
                    string itemName = item.ToString();
                    priceSourceItem.Name = itemName;
                    priceSourceItem.ASIN = asin;
                    priceSourceItem.URL = url;
                    priceSourceItem.ImageUrl = imageUrl;
                    string priceStr = item.OfferSummary.LowestNewPrice.Amount;
                    priceSourceItem.PriceCurrency = item.OfferSummary.LowestNewPrice.CurrencyCode;
                    if (priceStr.Length >= 2)
                    {
                        priceStr = priceStr.Insert(priceStr.Length - 2, ".");
                    }
                    priceSourceItem.Price = double.Parse(priceStr);
                    result.Add(priceSourceItem);


                }




            }
            return result;
        }

        public static List<PriceSourceItem> ToPriceSourceItems(this ItemLookupResponse amazonItemResponse)
        {
            List<PriceSourceItem> result = new List<PriceSourceItem>();
            if (amazonItemResponse == null || amazonItemResponse.Items.Item == null) return result;
            foreach (var item in amazonItemResponse.Items.Item)
            {
                string asin = item.ASIN;
                string url = item.DetailPageURL;
                string imageUrl = item.LargeImage.URL;
                string itemName = item.ItemAttributes.Title;
           
                string model = item.ItemAttributes.Model;
                string modelYear = item.ItemAttributes.ModelYear;
                string brand = item.ItemAttributes.Brand;
                string manufacturer = item.ItemAttributes.Manufacturer;
                string ean = item.ItemAttributes.EAN;
                if (string.IsNullOrWhiteSpace(itemName))
                    itemName = item.ToString();

                if (item.Offers.TotalOffers != "0")
                {
                    foreach (var offer in item.Offers.Offer)
                    {
                        string merchant = offer.Merchant?.Name;
                        foreach (var offerListing in offer.OfferListing)
                        {
                            PriceSourceItem priceSourceItem = new PriceSourceItem();
                            priceSourceItem.Merchant = merchant;
                            priceSourceItem.ASIN = asin;
                            priceSourceItem.URL = url;
                            priceSourceItem.ImageUrl = imageUrl;
                            priceSourceItem.Name = itemName;
                            priceSourceItem.Model = model;
                            priceSourceItem.ModelYear = modelYear;
                            priceSourceItem.Brand = brand;
                            priceSourceItem.Manufacturer = manufacturer;
                            priceSourceItem.Ean = ean;
                            string priceStr = offerListing.Price.Amount;
                            priceSourceItem.Name = itemName;
                            priceSourceItem.PriceCurrency = offerListing.Price.CurrencyCode;
                            if (!string.IsNullOrWhiteSpace(offerListing.SalePrice?.Amount))
                            {
                                priceStr = offerListing.SalePrice.Amount;
                                priceSourceItem.PriceCurrency = offerListing.SalePrice.CurrencyCode;
                            }
                            if (priceStr.Length >= 2)
                            {
                                priceStr = priceStr.Insert(priceStr.Length - 2, ".");
                            }
                            priceSourceItem.Price = double.Parse(priceStr);
                            result.Add(priceSourceItem);
                        }
                    }
                }
                else if (item.ItemLinks.FirstOrDefault(l => l.Description == "All Offers") != null)
                {
                    //Usually if that is not here then there are no offers
                    if (item.OfferSummary?.LowestNewPrice?.Amount != null)
                    {
                        url = item.ItemLinks.FirstOrDefault(l => l.Description == "All Offers").URL;
                        PriceSourceItem priceSourceItem = new PriceSourceItem();
                        priceSourceItem.ASIN = asin;
                        priceSourceItem.Name = itemName;
                        priceSourceItem.URL = url;
                        priceSourceItem.ImageUrl = imageUrl;
                        priceSourceItem.Model = model;
                        priceSourceItem.ModelYear = modelYear;
                        priceSourceItem.Brand = brand;
                        priceSourceItem.Manufacturer = manufacturer;
                        priceSourceItem.Ean = ean;

                        string priceStr = item.OfferSummary.LowestNewPrice.Amount;
                        priceSourceItem.PriceCurrency = item.OfferSummary.LowestNewPrice.CurrencyCode;
                        if (priceStr.Length >= 2)
                        {
                            priceStr = priceStr.Insert(priceStr.Length - 2, ".");
                        }
                        priceSourceItem.Price = double.Parse(priceStr);
                        result.Add(priceSourceItem);
                    }


                }




            }
            return result;
        }

    }



}
