using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.ViewModels.Search
{
    public class GpuSearchCrietriaViewModel : SearchViewModelBase<Gpu>
    {
        public string Name { get; set; }
        public string ModelId { get; set; }
        
        public override SearchCriteria<Gpu> ToSearchModel()
        {
            //Get base Values

            SearchCriteria<Gpu> searchCriteria = base.ToSearchModel();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AddAndFilter(brand => brand.Name.Contains(Name) ));
            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(ModelId), () => searchCriteria.AddAndFilter(brand => brand.ModelId == ModelId ));

            return searchCriteria;
        }
    }
}