using Microsoft.AspNetCore.Routing;
using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.ViewModels.Search
{
    public class CoinSearchCrietriaViewModel : SearchViewModelBase<Coin>
    {
        public string Name { get; set; }
        
        public override SearchCriteria<Coin> ToSearchModel()
        {
            //Get base Values

            SearchCriteria<Coin> searchCriteria = base.ToSearchModel();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AddAndFilter(algorithm => algorithm.Name.Contains(Name) || algorithm.Tag.Contains(Name)));

            return searchCriteria;
        }
    }
}