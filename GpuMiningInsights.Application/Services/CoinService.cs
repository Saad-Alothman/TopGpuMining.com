using System.Linq;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using REME.Persistance;

namespace GpuMiningInsights.Application.Services
{
    public class CoinService : GmiServiceBase<Coin, CoinService>
    {
        public void AddOrUpdateCoins()
        {
            //get all coins from external source
            var allcoinsFromResponse = WhatToMineService.GetAllCoins();
            
            using (GmiUnitOfWork unitOfWork = new GmiUnitOfWork())
            {
                var coinsFromDB = Search(new SearchCriteria<Coin>(int.MaxValue, 1)).Result;
                var newCoinsToInsert = allcoinsFromResponse.Where(c =>coinsFromDB.FirstOrDefault(dbc => dbc.Name.ToLower() == c.Name.ToLower()) == null).ToList();
                
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
    }
}