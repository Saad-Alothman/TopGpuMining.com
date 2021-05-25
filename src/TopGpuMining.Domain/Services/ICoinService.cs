using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;

namespace TopGpuMining.Domain.Services
{
    public interface ICoinService:IGenericService<Coin>
    {
        void AddOrUpdate();
        void FetchAndAddOrUpdateCoins();
        SearchResult<Coin> Search(SearchCriteria<Coin> searchCriteria,bool validCoinsOnly);
        
        void UpdateCoinsInfo();
        void UpdateUsdExchangeRate(string id, double exchangeRate);
        double GetUsdBtcExchangeRate();
        double GetLiveUsdExchangeRate(string cryptoCurrencyCode);

    }
}
