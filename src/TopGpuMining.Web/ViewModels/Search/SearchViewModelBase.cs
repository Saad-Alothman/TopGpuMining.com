using TopGpuMining.Core.Extensions;
using TopGpuMining.Core.Search;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.ViewModels
{
    public class SearchViewModelBase<TModel> : SearchCriteria<TModel> where TModel : class
    {
        public virtual SearchCriteria<TModel> ToSearchModel()
        {
            return this;
        }

        public virtual Dictionary<string, string> ToRouteValueDictionary(bool ignorePageNumber = false)
        {
            var type = this.GetType();

            var properties = type.GetProperties();

            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (var item in properties)
            {
                if (item.Name == nameof(FilterExpression) ||
                    item.Name == nameof(SortExpression) ||
                    item.Name == nameof(StartIndex))
                    continue;

                if (!ignorePageNumber)
                {
                    if (item.Name == nameof(PageNumber)) continue;
                }

                var value = item.GetValue(this);

                if (value != null)
                {
                    if (value is DateTime date)
                    {
                        result.Add(item.Name, date.ToSystemDate());
                    }
                    else
                    {
                        result.Add(item.Name, value.ToString());
                    }
                }
            }

            return result;
        }
        
    }
}
