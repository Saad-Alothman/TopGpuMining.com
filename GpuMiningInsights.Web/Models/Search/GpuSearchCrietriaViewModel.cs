using System.Web.Routing;
using CreaDev.Framework.Core.Helpers;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Web.Models.Search
{
    public class GpuSearchCrietriaViewModel : GmiSearchCriteriaViewModelBase<Gpu>
    {
        public string Name { get; set; }
        public override RouteValueDictionary ToRouteValueDictionary(int page, string prefix = "")
        {
            //Get The Base Values
            RouteValueDictionary routeValueDictionary = base.ToRouteValueDictionary(page, prefix);

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => routeValueDictionary.Add(nameof(Name), Name));

            return routeValueDictionary;

        }

        public override SearchCriteria<Gpu> ToSearchCriteria()
        {
            //Get base Values

            SearchCriteria<Gpu> searchCriteria = base.ToSearchCriteria();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AndCondition(brand => brand.Name.Contains(Name) ));

            return searchCriteria;
        }
    }
}