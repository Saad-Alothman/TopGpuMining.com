using System.Collections.Generic;
using System.Linq;
using TopGpuMining.Application.Services;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Domain.Models;
using TopGpuMining.Persistance;

namespace TopGpuMining.Application.Services
{
    public class FiatCurrencyService : GenericService<FiatCurrency>
    {
        public FiatCurrencyService(IGenericRepository repository) : base(repository)
        {
        }

        public void AddOrUpdate()
        {
           Dictionary<string, double> pricesDictionary = ApiLayerService.LoadCurrencies();
            using (UnitOfWork  unitOfWork= new UnitOfWork())
            {
                var allCurrenciesInDB =unitOfWork.GenericRepository.Get<FiatCurrency>();
                List<FiatCurrency> newFiatCurrencies = new List<FiatCurrency>();
                foreach (var key in pricesDictionary.Keys)
                {
                    var fiatCurrencyInDB = allCurrenciesInDB.FirstOrDefault(c => c.Code == key);
                    //not null means already exist, we will update only
                    if (fiatCurrencyInDB != null)
                        fiatCurrencyInDB.UpdateExchangeRate(pricesDictionary[key]);
                    else
                        newFiatCurrencies.Add(new FiatCurrency()
                        {
                            Code = key,
                            ExchangeRateUSD = pricesDictionary[key]
                        });
                }
                unitOfWork.GenericRepository.Create(newFiatCurrencies);
                unitOfWork.Commit();
            }
            
        }
        
    }
}