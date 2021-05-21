using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Domain.Services
{
    public interface ICoinService
    {
        void AddOrUpdate();
        void FetchAndAddOrUpdateCoins();
        SearchResult<Coin> Search(SearchCriteria<Coin> searchCriteria,bool validCoinsOnly);
        
        void UpdateCoinsInfo();
        void UpdateUsdExchangeRate(int id, double exchangeRate);
        double GetUsdBtcExchangeRate();
        double GetLiveUsdExchangeRate(string cryptoCurrencyCode);

    }
}
