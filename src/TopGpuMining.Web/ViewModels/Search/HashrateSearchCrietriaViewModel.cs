using Microsoft.AspNetCore.Routing;
using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.ViewModels.Search
{
    public class HashrateSearchCrietriaViewModel : SearchViewModelBase<Hashrate>
    {
        public string Name { get; set; }
        
        public override SearchCriteria<Hashrate> ToSearchModel()
        {
            //Get base Values

            SearchCriteria<Hashrate> searchCriteria = base.ToSearchModel();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AddAndFilter(hashrate => hashrate.HashrateValueMhz.Contains(Name)));

            return searchCriteria;
        }
    }
}