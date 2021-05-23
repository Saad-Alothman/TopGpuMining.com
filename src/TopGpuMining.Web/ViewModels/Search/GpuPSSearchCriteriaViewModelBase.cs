using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.ViewModels.Search
{
    public class GpuPSSearchViewModelBase : SearchViewModelBase<GpuPriceSource>
    {
        public string Name { get; set; }
        public string GpuId { get; set; }

        
        public override SearchCriteria<GpuPriceSource> ToSearchModel()
        {
            //Get base Values

            SearchCriteria<GpuPriceSource> searchCriteria = base.ToSearchModel();

            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(Name),() => searchCriteria.AddAndFilter(gpuPriceSource => gpuPriceSource.Name.Contains(Name)));
            ConditionActionHelper.DoIf(!string.IsNullOrEmpty(GpuId), () => searchCriteria.AddAndFilter(gpuPriceSource => gpuPriceSource.GpuId == GpuId));

            return searchCriteria;
        }
    }
}