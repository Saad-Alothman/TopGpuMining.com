using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using CreaDev.Framework.Core.Helpers;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Web.Models.Search
{
    public class GpuPSSearchCriteriaViewModelBase : GmiSearchCriteriaViewModelBase<GPUPriceSource>
    {
        public string Name { get; set; }

        public override RouteValueDictionary ToRouteValueDictionary(int page, string prefix = "")
        {
            //Get The Base Values
            RouteValueDictionary routeValueDictionary = base.ToRouteValueDictionary(page, prefix);

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => routeValueDictionary.Add(nameof(Name), Name));

            return routeValueDictionary;
        }

        public override SearchCriteria<GPUPriceSource> ToSearchCriteria()
        {
            //Get base Values

            SearchCriteria<GPUPriceSource> searchCriteria = base.ToSearchCriteria();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name),
                () => searchCriteria.AndCondition(gpuPriceSource => gpuPriceSource.Name.Contains(Name)));

            return searchCriteria;
        }
    }
}