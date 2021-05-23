
using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.ViewModels.Search
{
    public class ModelSearchCrietriaViewModel : SearchViewModelBase<Model>
    {
        public string Name { get; set; }
        

        public override SearchCriteria<Model> ToSearchModel()
        {
            //Get base Values

            SearchCriteria<Model> searchCriteria = base.ToSearchModel();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AddAndFilter(brand => brand.Name.Arabic.Contains(Name) || brand.Name.English.Contains(Name)));

            return searchCriteria;
        }
    }
}