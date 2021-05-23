using System;
using System.Collections.Generic;
using System.Web;
using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.ViewModels.Search
{
    public class AlgorithmSearchCrietriaViewModel : SearchViewModelBase<Algorithm>
    {
        public string Name { get; set; }
        

        public override SearchCriteria<Algorithm> ToSearchModel()
        {
            //Get base Values

            SearchCriteria<Algorithm> searchCriteria = base.ToSearchModel();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name), () => searchCriteria.AddAndFilter(algorithm   => algorithm.Name.Arabic.Contains(Name) || algorithm.Name.English.Contains(Name)));

            return searchCriteria;
        }
    }
}