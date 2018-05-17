using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using CreaDev.Framework.Core.Models;
using X.PagedList;

namespace GpuMiningInsights.Web.Helpers
{
    public static class PagedListExtension
    {

        public static StaticPagedList<TEntity> ToPagedList<TEntity>(this SearchResult<TEntity> searchResult) where TEntity : class
        {
            var result = new StaticPagedList<TEntity>(
                searchResult.Result,
                searchResult.PageNumber,
                searchResult.PageSize,
                searchResult.TotalResultsCount);

            return result;
        }

        public static StaticPagedList<TEntity> ToPagedList<TEntity>(this AccountSearchResult<TEntity> searchResult) where TEntity : class, new()
        {
            var result = new StaticPagedList<TEntity>(
                searchResult.Result,
                searchResult.PageNumber,
                searchResult.PageSize,
                searchResult.TotalResultsCount);

            return result;
        }

   
    }
}