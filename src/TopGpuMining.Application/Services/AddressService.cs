using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TopGpuMining.Application.Services;
using Newtonsoft.Json;

namespace TopGpuMining.Application.Services
{
    public static class ApiLayerService
    {
        public static Dictionary<string, double> LoadCurrencies(bool removeSourceCurrencyFromKey = true)
        {
            string currencyApiUrl = "http://www.apilayer.net/api/live?access_key=ff139c408a9439cd66d94f7ee26a598b&format=1&source=USD";

            string respomseText = InsighterService.GetHttpResponseText(currencyApiUrl);
            var pricesDictResponse = JsonConvert.DeserializeObject<CurrencyPricesResponse>(respomseText);
            Dictionary<string, double> pricesDict = pricesDictResponse.quotes;
            if (removeSourceCurrencyFromKey)
            {
                Dictionary<string, double> withoutSourceCurrencyCode = new Dictionary<string, double>();
                foreach (var key in pricesDict.Keys)
                {
                    string newKey = key.Substring(3);
                    withoutSourceCurrencyCode.Add(newKey, pricesDict[key]);
                }
                pricesDict = withoutSourceCurrencyCode;
            }

            return pricesDict;
        }
    }
    public class AddressService : ServiceBase , IAddressService
    {
        public AddressService(IGenericRepository repository) : base(repository)
        {

        }

        public Address Add(Address entity)
        {
            return _repository.Create(entity);
        }

        public Address GetById(string id)
        {
            return _repository.GetById<Address>(id);
        }

        public Address Save(Address entity)
        {
            return _repository.Update(entity);
        }

        public void Delete(string id)
        {
            _repository.Delete<Address>(id);
        }

        public SearchResult<Address> Search(SearchCriteria<Address> search)
        {
            return _repository.Search(search);
        }
    }
}
