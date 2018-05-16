using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using CreaDev.Framework.Core.Models;

namespace CreaDev.Framework.Web.Mvc.Models
{
    public class SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> 
        where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<TModel>
    {        
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public TSearchCriteriaViewModel SearchCriteriaViewModel { get; set; }
        public List<TModel> Result { get; set; }
        public int TotalResultsCount { get; set; }
        public int PageCount
        {
            get { return (int)Math.Ceiling((double)TotalResultsCount / PageSize); }
        }

        public SearchResultViewModelBase(SearchCriteriaViewModelBase<TModel> searchCriteria, SearchResult<TModel> searchResult) : this()
        {
            this.PageNumber = searchCriteria.PageNumber;
            this.PageSize = searchCriteria.PageSize;
            this.SearchCriteriaViewModel = searchCriteria as TSearchCriteriaViewModel;
            this.Result = searchResult.Result;
            this.TotalResultsCount = searchResult.TotalResultsCount;


        }

        public virtual SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> Create(
            SearchCriteriaViewModelBase<TModel> searchCriteria, SearchResult<TModel> searchResult)
        {
            return new SearchResultViewModelBase<TModel, TSearchCriteriaViewModel>(searchCriteria,searchResult);
        }

        public static SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> CreateObject(
        SearchCriteriaViewModelBase<TModel> searchCriteria, SearchResult<TModel> searchResult)
        {
            return new SearchResultViewModelBase<TModel, TSearchCriteriaViewModel>(searchCriteria, searchResult);
        }

        public SearchResultViewModelBase()
        {
            this.Result = new List<TModel>();
        }


    }
}
