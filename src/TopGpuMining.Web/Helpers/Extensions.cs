using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.Helpers
{
    public static class Extensions
    {
        public static string ActionWithoutRouteValues(this IUrlHelper helper, string action, string[] removeRouteValues = null)
        {
            var routeValues = helper.ActionContext.RouteData.Values;
            var routeValueKeys = routeValues.Keys.Where(o => o != "controller" && o != "action").ToList();

            // Temporarily remove route values
            var oldRouteValues = new Dictionary<string, object>();

            foreach (var key in routeValueKeys)
            {
                if (removeRouteValues != null && !removeRouteValues.Contains(key))
                {
                    continue;
                }

                oldRouteValues[key] = routeValues[key];
                routeValues.Remove(key);
            }

            // Generate URL
            var url = helper.Action(action);

            // Reinsert route values
            foreach (var keyValuePair in oldRouteValues)
            {
                routeValues.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return url;
        }

        public static Dictionary<string, string> ToRouteValues(this RouteValueDictionary data)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (var item in data)
            {
                if (item.Key == "culture")
                    continue;

                result.Add(item.Key, item.Value.ToString());
            }

            return result;
        }
    }
}
