using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Domain.Services;
using System.Linq;
using TopGpuMining.Persistance;

namespace TopGpuMining.Application.Services
{
    public class AlgorithmService : GenericService<Algorithm>, IAlgorithmService
    {
        public AlgorithmService(IGenericRepository repository) : base(repository)
        {
        }
        public Algorithm AddOrUpdate(Algorithm model)
        {
            var algoInDB = Search(new SearchCriteria<Algorithm>(a => a.Name.English.ToLower() == model.Name.English.Trim().ToLower())).Result.FirstOrDefault();
            if (algoInDB != null)
                return algoInDB;
            Add(model);
            algoInDB = Search(new SearchCriteria<Algorithm>(a => a.Name.English.ToLower() == model.Name.English.Trim().ToLower())).Result.FirstOrDefault();

            return algoInDB;
        }
    }

    public class CoinService : GenericService<Coin>, ICoinService
    {


        public CoinService(IGenericRepository repository) : base(repository)
        {
        }

        //Coins.Json
        //{"id":151,"tag":"ETH",                  "algorithm":"Ethash","block_time":"15.2385","block_reward":2.91,"block_reward24":2.91000000000003,"last_block":5694564,"difficulty":3.25620720085259e+15,"difficulty24":3.22891863103978e+15,"nethash":213682921603346,"exchange_rate":0.073289,"exchange_rate24":0.0736619195402299,"exchange_rate_vol":10846.1477343,"exchange_rate_curr":"BTC","market_cap":"$51,886,419,280.94","estimated_rewards":"0.00649","estimated_rewards24":"0.00654","btc_revenue":"0.00047535","btc_revenue24":"0.00047937","profitability":100,"profitability24":100,"lagging":false,"timestamp":1527559607}
        //this is sepcific coin https://whattomine.com/coins/151.json
        //{"id":151,"name":"Ethereum","tag":"ETH","algorithm":"Ethash","block_time":"15.2281","block_reward":2.91,"block_reward24":2.91000000000003,"block_reward3":2.91000000000001,"block_reward7":2.91000000000001,"last_block":5694599,"difficulty":3.23405773003569e+15,"difficulty24":3.2289629306661e+15,"difficulty3":3.23756569326551e+15,"difficulty7":3.24083077422111e+15,"nethash":212374342829091,"exchange_rate":0.07327,"exchange_rate24":0.0736613845050216,"exchange_rate3":0.0772009770987365,"exchange_rate7":0.0785249303750595,"exchange_rate_vol":10836.71146607,"exchange_rate_curr":"BTC","market_cap":"$51,886,120,780","pool_fee":"0.000000","estimated_rewards":"0.006541","btc_revenue":"0.00047924","revenue":"$3.40","cost":"$0.97","profit":"$2.43","status":"Active","lagging":false,"timestamp":1527560081}
        public void AddOrUpdate()
        {
            FetchAndAddOrUpdateCoins();
            UpdateCoinsInfo();
        }
        public void UpdateUsdExchangeRates()
        {

            using (UnitOfWork UnitOfWork = new UnitOfWork())
            {

                //Get Coins from DB,
                int allCoinsInDBCount = UnitOfWork.GenericRepository.Count(new SearchCriteria<Coin>(int.MaxValue, 1) { FilterExpression = c => c.Difficulty > 0 && c.BlockReward > 0 && c.LastBlock > 0 });


                int pageNumber = 1;
                int pageSize = 30;
                int lastPage = (int)Math.Ceiling((double)allCoinsInDBCount / pageSize);

                string usdCode = "USD";

                while (pageNumber <= lastPage)
                {
                    var searchCriteria = new SearchCriteria<Coin>(pageSize, pageNumber) { FilterExpression = c => c.Difficulty > 0 && c.BlockReward > 0 && c.LastBlock > 0 };
                    var partialResult = UnitOfWork.GenericRepository.Search(searchCriteria).Result;
                    var coinNames = partialResult.Select(s => s.Tag.ToUpper()).ToList();
                    List<CryptoComparePriceResult> exchangeRatesPartial = CryptoCompareService.GetCryptoComparePriceExchangeRateMultiSource(coinNames, usdCode);
                    foreach (var cryptoComparePriceResult in exchangeRatesPartial)
                    {
                        var coin = partialResult.FirstOrDefault(c =>
                            c.Tag.ToUpper() == cryptoComparePriceResult.SourceCurrenceCode.ToUpper());
                        if (coin == null)
                            continue;
                        coin.ExchangeRateUsd = cryptoComparePriceResult.ExchangeRates[usdCode];

                    }
                    pageNumber++;
                }

                UnitOfWork.Commit();
            }

        }

        private double? _usdBtcExchangeRate;


        public void UpdateUsdExchangeRate(string id, double exchangeRate)
        {
            using (UnitOfWork UnitOfWork = new UnitOfWork())
            {

                Coin coin = UnitOfWork.GenericRepository.GetByID<Coin>(id);
                Guard.AgainstNull(coin);
                coin.ExchangeRateUsd = exchangeRate;
                UnitOfWork.Commit();
            }
        }



        public double GetUsdBtcExchangeRate()
        {
            if (_usdBtcExchangeRate == null)
            {
                var cryptoCompareResult = CryptoCompareService.GetUsdBtcExchangeRate();
                _usdBtcExchangeRate = cryptoCompareResult.ExchangeRates.Values.FirstOrDefault();
            }
            return _usdBtcExchangeRate.Value;

        }

        public double GetLiveUsdExchangeRate(string cryptoCurrencyCode)
        {

            return CryptoCompareService.GetCryptoComparePriceExchangeRate(cryptoCurrencyCode, "USD")?.ExchangeRates.Values.FirstOrDefault() ?? 0;
        }

        public double AddOrUpdateUsdExchangeRates()
        {
            if (_usdBtcExchangeRate == null)
            {
                var cryptoCompareResult = CryptoCompareService.GetUsdBtcExchangeRate();
                _usdBtcExchangeRate = cryptoCompareResult.ExchangeRates.Values.FirstOrDefault();
            }
            return _usdBtcExchangeRate.Value;

        }



        public SearchResult<Coin> Search(SearchCriteria<Coin> searchCriteria, bool validCoinsOnly)
        {
            if (validCoinsOnly)
                searchCriteria.FilterExpression = searchCriteria.FilterExpression.And(c => c.Difficulty > 0 && c.BlockReward > 0 && c.LastBlock > 0);

            searchCriteria.FilterExpression = searchCriteria.FilterExpression.And(c => c.IsDisabled == false);
            return Search(searchCriteria);
        }

        public void UpdateCoinsInfo()
        {

            using (UnitOfWork UnitOfWork = new UnitOfWork())
            {

                //Get Coins from DB,
                List<Coin> allCoinsInDB = UnitOfWork.GenericRepository.GetAll<Coin>();
                //Read coin data from Coins.JSON, NOTE Not all coins are here
                List<Coin> allCoinsInfoFromWTM = WhatToMineService.GetCoinsInfoFromCoinsJson();
                //CoinsDB: Filter out the ones that dont have info from Coins.JSON.
                List<Coin> coinsWithInfoReturned = allCoinsInDB.Where(cdb => allCoinsInfoFromWTM.Any(c => c.WhatToMineId == cdb.WhatToMineId)).ToList();
                //CoinsDB: Filter out the ones that dont have info from Coins.JSON.
                List<Coin> coinsWithoutInfoReturned = allCoinsInDB.Where(cdb => allCoinsInfoFromWTM.All(c => c.WhatToMineId != cdb.WhatToMineId)).ToList();
                //Foreach one that no info returned, get its info
                foreach (var coin in coinsWithoutInfoReturned)
                {
                    Coin coinInfo = WhatToMineService.GetCoinInfo(coin.WhatToMineId);
                    if (coinInfo != null)
                        coin.UpdateInfo(coinInfo);
                }
                foreach (var coin in coinsWithInfoReturned)
                {
                    var coinInfo = allCoinsInfoFromWTM.FirstOrDefault(c => c.WhatToMineId == coin.WhatToMineId);
                    if (coinInfo == null)
                        throw new Exception("Someting is wrong, this coin should belong to the unknown list");
                    coin.UpdateInfo(coinInfo);
                }

                UnitOfWork.Commit();
            }




        }

        public void FetchAndAddOrUpdateCoins()
        {
            //get all coins from external source
            var allcoinsFromResponse = WhatToMineService.GetAllCoinsFromCalculators();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var coinsFromDB = Search(new SearchCriteria<Coin>(int.MaxValue, 1)).Result;
                var newCoinsToInsert = allcoinsFromResponse
                    .Where(c => coinsFromDB.FirstOrDefault(dbc => dbc.Name.ToLower() == c.Name.ToLower()) == null).ToList();

                //update existing
                foreach (var coin in coinsFromDB)
                {
                    var newData = allcoinsFromResponse.FirstOrDefault(dbc => dbc.Name.ToLower() == coin.Name.ToLower());
                    if (newData == null) continue;

                    coin.UpdateInfo(newData);
                }

                // add new ones
                unitOfWork.GenericRepository.Create(newCoinsToInsert);
                unitOfWork.Commit();
            }
        }

        public override SearchResult<Coin> Search(SearchCriteria<Coin> searchCriteria)
        {
            return base.Search(searchCriteria);
        }
    }

}
