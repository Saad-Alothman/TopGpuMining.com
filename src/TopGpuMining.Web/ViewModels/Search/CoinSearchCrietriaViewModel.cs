using System.Web.Routing;
using CreaDev.Framework.Core.Helpers;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Web.Models.Search
{
    public class CoinSearchCrietriaViewModel : GmiSearchCriteriaViewModelBase<Coin>
    {
        public string Name { get; set; }
        public override RouteValueDictionary ToRouteValueDictionary(int page, string prefix = "")
        {
            //Get The Base Values
            RouteValueDictionary routeValueDictionary = base.ToRouteValueDictionary(page, prefix);

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => routeValueDictionary.Add(nameof(Name), Name));

            return routeValueDictionary;

        }

        public override SearchCriteria<Coin> ToSearchCriteria()
        {
            //Get base Values

            SearchCriteria<Coin> searchCriteria = base.ToSearchCriteria();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AndCondition(algorithm => algorithm.Name.Contains(Name) || algorithm.Tag.Contains(Name)));

            return searchCriteria;
        }
    }
}