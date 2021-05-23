using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.ViewModels.Search
{
    public class BrandSearchCrietriaViewModel: SearchViewModelBase<Brand>
    {
        public string Name { get; set; }
        
        public override SearchCriteria<Brand> ToSearchModel()
        {
            //Get base Values
            
            SearchCriteria<Brand> searchCriteria = base.ToSearchModel();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AddAndFilter(brand => brand.Name.Arabic.Contains(Name) || brand.Name.English.Contains(Name)));

            return searchCriteria;
        }
    }
}