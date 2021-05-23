using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.ViewModels.Search
{
    public class PriceSourceSearchCrieteriaViewModel: SearchViewModelBase<PriceSource>
    {
        public string Name { get; set; }
        

        public override SearchCriteria<PriceSource> ToSearchModel()
        {
            //Get base Values

            SearchCriteria<PriceSource> searchCriteria = base.ToSearchModel();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AddAndFilter(priceSource => priceSource.Name.Contains(Name)));

            return searchCriteria;
        }
    }
}