using System.Collections.Generic;
using System.Linq;
using GpuMiningInsights.Domain.Models;
using REME.Persistance;

namespace GpuMiningInsights.Application.Services
{
    public class FiatCurrencyService : GmiServiceBase<FiatCurrency, FiatCurrencyService>
    {
        public void AddOrUpdate()
        {
           Dictionary<string, double> pricesDictionary = ApiLayerService.LoadCurrencies();
            using (GmiUnitOfWork  unitOfWork= new GmiUnitOfWork())
            {
                var allCurrenciesInDB =unitOfWork.GenericRepository.GetAll<FiatCurrency>();
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