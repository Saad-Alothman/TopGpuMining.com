using System.Web.Routing;
using CreaDev.Framework.Core.Helpers;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Web.Models.Search
{
    public class GpuSearchCrietriaViewModel : GmiSearchCriteriaViewModelBase<Gpu>
    {
        public string Name { get; set; }
        public int? ModelId { get; set; }
        public override RouteValueDictionary ToRouteValueDictionary(int page, string prefix = "")
        {
            //Get The Base Values
            RouteValueDictionary routeValueDictionary = base.ToRouteValueDictionary(page, prefix);

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => routeValueDictionary.Add(nameof(Name), Name));
            ConditionActionHelper.DoIf(ModelId.HasValue,  () => routeValueDictionary.Add(nameof(ModelId), ModelId.Value));

            return routeValueDictionary;

        }

        public override SearchCriteria<Gpu> ToSearchCriteria()
        {
            //Get base Values

            SearchCriteria<Gpu> searchCriteria = base.ToSearchCriteria();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AndCondition(brand => brand.Name.Contains(Name) ));
            ConditionActionHelper.DoIf(ModelId.HasValue, () => searchCriteria.AndCondition(brand => brand.ModelId == ModelId ));

            return searchCriteria;
        }
    }
}