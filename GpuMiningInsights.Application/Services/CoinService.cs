using System;
using System.Collections.Generic;
using System.Linq;
using CreaDev.Framework.Core;
using CreaDev.Framework.Core.Linq;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Domain.Services;
using REME.Persistance;

namespace GpuMiningInsights.Application.Services
{
    public class CoinService : GmiServiceBase<Coin, CoinService>, ICoinService
    {

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

            using (GmiUnitOfWork gmiUnitOfWork = new GmiUnitOfWork())
            {

                //Get Coins from DB,
                int allCoinsInDBCount = gmiUnitOfWork.GenericRepository.Count(new SearchCriteria<Coin>(int.MaxValue,1) { FilterExpression = c => c.Difficulty > 0 && c.BlockReward > 0 && c.LastBlock > 0 });
                

                int pageNumber = 1;
                int pageSize = 30;
                int lastPage = (int) Math.Ceiling((double)allCoinsInDBCount / pageSize) ;

                string usdCode = "USD";
                
                while (pageNumber<= lastPage)
                {
                    var searchCriteria = new SearchCriteria<Coin>(pageSize, pageNumber){FilterExpression = c => c.Difficulty > 0 && c.BlockReward > 0 && c.LastBlock > 0 };
                    var partialResult =gmiUnitOfWork.GenericRepository.Search(searchCriteria).Result;
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
                
                gmiUnitOfWork.Commit();
            }

        }

        private double? _usdBtcExchangeRate;
        public void UpdateUsdExchangeRate(int id,double exchangeRate)
        {
            using (GmiUnitOfWork gmiUnitOfWork = new GmiUnitOfWork())
            {

                Coin coin = gmiUnitOfWork.GenericRepository.GetByID<Coin>(id);
                Guard.AgainstNull(coin);
                coin.ExchangeRateUsd = exchangeRate;
                gmiUnitOfWork.Commit();
            }
        }

        

        public double GetUsdBtcExchangeRate()
        {
            if (_usdBtcExchangeRate == null)
            {
                var cryptoCompareResult = CryptoCompareService.GetUsdBtcExchangeRate();
                _usdBtcExchangeRate=cryptoCompareResult.ExchangeRates.Values.FirstOrDefault();
            }
            return _usdBtcExchangeRate.Value;
            
        }

        public double GetLiveUsdExchangeRate(string cryptoCurrencyCode)
        {
            return CryptoCompareService.GetCryptoComparePriceExchangeRate(cryptoCurrencyCode, "USD").ExchangeRates.Values.FirstOrDefault();
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
                searchCriteria.FilterExpression =searchCriteria.FilterExpression.And(c => c.Difficulty > 0 && c.BlockReward > 0 && c.LastBlock > 0);
            
            searchCriteria.FilterExpression =searchCriteria.FilterExpression.And(c => c.IsDisabled == false);
            return Search(searchCriteria);
        }

        public void UpdateCoinsInfo()
        {

            using (GmiUnitOfWork gmiUnitOfWork = new GmiUnitOfWork())
            {
                
                //Get Coins from DB,
                List<Coin> allCoinsInDB = gmiUnitOfWork.GenericRepository.GetAll<Coin>();
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
                    var coinInfo =allCoinsInfoFromWTM.FirstOrDefault(c => c.WhatToMineId == coin.WhatToMineId);
                    if (coinInfo == null) 
                        throw new Exception("Someting is wrong, this coin should belong to the unknown list");
                    coin.UpdateInfo(coinInfo);
                }

                gmiUnitOfWork.Commit();
            }
            



        }

        public void FetchAndAddOrUpdateCoins()
        {
//get all coins from external source
            var allcoinsFromResponse = WhatToMineService.GetAllCoinsFromCalculators();

            using (GmiUnitOfWork unitOfWork = new GmiUnitOfWork())
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