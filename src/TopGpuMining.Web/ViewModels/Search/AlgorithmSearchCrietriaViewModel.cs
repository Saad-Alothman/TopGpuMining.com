using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using CreaDev.Framework.Core.Helpers;
using CreaDev.Framework.Core.Linq;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using TopGpuMining.Core.Search;

namespace GpuMiningInsights.Web.Models.Search
{
    public class AlgorithmSearchCrietriaViewModel : GmiSearchCriteriaViewModelBase<Algorithm>
    {
        public string Name { get; set; }
        

        public override SearchCriteria<Algorithm> ToSearchCriteria()
        {
            //Get base Values

            SearchCriteria<Algorithm> searchCriteria = base.ToSearchCriteria();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AndCondition(algorithm   => algorithm.Name.Arabic.Contains(Name) || algorithm.Name.English.Contains(Name)));

            return searchCriteria;
        }
    }
}