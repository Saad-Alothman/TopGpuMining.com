using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using CreaDev.Framework.Core.Models;

namespace CreaDev.Framework.Web.Mvc.Models
{
 
    public abstract class SearchCriteriaViewModelBase<T>
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortProperty { get; set; }
        public bool IsDescending { get; set; }
        public string RelatedItemTypeId { get; set; }
        public string RelatedItemId { get; set; }

        public SearchCriteriaViewModelBase()
        {
            PageSize = 10;
            PageNumber = 1;
        }
        public SearchCriteriaViewModelBase(int pageSize, int pageNumber)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
        }
        public void SetPaginationAndSize(SearchCriteria<T> searchCriteria)
        {
            searchCriteria.PageNumber = PageNumber;
            searchCriteria.PageSize = PageSize;
        }
        public RouteValueDictionary GetBaseValuesRouteValueDictionary(int page, string prefix = "")
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary
            {
                {prefix + nameof(PageSize), this.PageSize},
                {prefix + nameof(PageNumber), page},
                {prefix + nameof(SortProperty), this.SortProperty},
                {prefix + nameof(IsDescending), this.IsDescending}
            };

            return routeValueDictionary;
        }
        public abstract SearchCriteria<T> ToSearchCriteria();
        public abstract RouteValueDictionary ToRouteValueDictionary(int page, string prefix = "");
    }
}
