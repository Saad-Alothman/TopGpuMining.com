using System.Linq;
using System.Web.Routing;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Web.Models.Search
{
    public abstract class GmiSearchCriteriaViewModelBase<T>: SearchCriteriaViewModelBase<T> where T : GmiEntityBase
    {

        //public DTParameters DataTableParameters { get; set; }


        public override SearchCriteria<T> ToSearchCriteria()
        {


            SearchCriteria<T> searchCriteria = base.ToSearchCriteria();

            if (this.IsDescending)
                searchCriteria.SortExpression = a => a.OrderByDescending(p => p.Id);
            else
                searchCriteria.SortExpression = a => a.OrderBy(p => p.Id);
            return searchCriteria;

        }

        public override RouteValueDictionary ToRouteValueDictionary(int page, string prefix = "")
        {
            return GetBaseValuesRouteValueDictionary(page, prefix);
        }
    }
}