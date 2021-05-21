using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Core.Models
{
    public class SearchResult<T>
    {

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public SearchCriteria<T> SearchCriteria { get; set; }
        public List<T> Result { get; set; }
        public int TotalResultsCount { get; set; }

        public int PageCount
        {
            get { return (int)Math.Ceiling((double)TotalResultsCount / PageSize); }
        }
        public static SearchResult<T> ConvertSearchResult<TSource>(SearchResult<TSource> searchResultSource) 
        {
            SearchResult<T> searchResult = new SearchResult<T>();
            searchResult.PageNumber = searchResultSource.PageNumber;
            searchResult.PageSize = searchResultSource.PageSize;
            searchResult.TotalResultsCount= searchResultSource.TotalResultsCount;
            searchResult.Result= searchResultSource.Result.Cast<T>().ToList();
            return searchResult;
        }
        public SearchResult(SearchCriteria<T> searchCriteria) : this()
        {
            this.PageNumber = searchCriteria.PageNumber;
            this.PageSize = searchCriteria.PageSize;
            this.SearchCriteria = searchCriteria;
        }
        public SearchResult()
        {
            this.Result = new List<T>();
        }
    }

    public class AccountSearchResult<T> where T : class, new()
    {

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public AccountSearchCriteria<T> AccountSearchCriteria { get; set; }

        public List<T> Result { get; set; }

        public int TotalResultsCount { get; set; }

        public int PageCount
        {
            get { return (int)Math.Ceiling((double)TotalResultsCount / PageSize); }
        }

        public AccountSearchResult(AccountSearchCriteria<T> searchCriteria) : this()
        {
            this.PageNumber = searchCriteria.PageNumber;
            this.PageSize = searchCriteria.PageSize;
            this.AccountSearchCriteria = searchCriteria;
        }
        public AccountSearchResult()
        {
            this.Result = new List<T>();
        }
    }
}
