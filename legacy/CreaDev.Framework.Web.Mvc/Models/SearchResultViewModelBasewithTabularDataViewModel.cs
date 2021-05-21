using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Routing;
using CreaDev.Framework.Core.Models;

namespace CreaDev.Framework.Web.Mvc.Models
{
    public class TabularDataViewModel 
    {
        public List<NameValueCollection> Data { get; set; }
        public string Title { get; set; }

        public TabularDataViewModel()
        {
            this.Data = new List<NameValueCollection>();

        }

        public TabularDataViewModel(List<NameValueCollection> toList):this()
        {
            foreach (var nameValueCollection in toList)
            {
                Data.Add(nameValueCollection);
            }

        }
    }
    public class SearchResultViewModelBasewithTabularDataViewModel
    {
        public TabularDataViewModel TabularDataViewModel { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalResultsCount { get; set; }
        public int PageCount => (int)Math.Ceiling((double)TotalResultsCount / PageSize);
        public RouteValueDictionary RouteValueDictionary { get; set; }
        public string ListName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public static SearchResultViewModelBasewithTabularDataViewModel FromSearchCriteriaviewModelAndSearchResult<TModel>(SearchCriteriaViewModelBase<TModel> searchCriteria, SearchResult<TModel> searchResult)
            where TModel:ITabularDataViewModel
        {
            SearchResultViewModelBasewithTabularDataViewModel searchResultViewModelBasewithTabularDataViewModel = new SearchResultViewModelBasewithTabularDataViewModel();
            searchResultViewModelBasewithTabularDataViewModel.PageNumber = searchResult.PageNumber;
            searchResultViewModelBasewithTabularDataViewModel.PageSize = searchResult.PageSize;
            searchResultViewModelBasewithTabularDataViewModel.TotalResultsCount = searchResult.TotalResultsCount;
            searchResultViewModelBasewithTabularDataViewModel.TabularDataViewModel = new TabularDataViewModel(searchResult.Result.Select(s => s.ToTabularDataViewModel()).ToList());
            searchResultViewModelBasewithTabularDataViewModel.RouteValueDictionary = searchCriteria.ToRouteValueDictionary(searchResult.PageNumber);

            return searchResultViewModelBasewithTabularDataViewModel;

        }
        public static SearchResultViewModelBasewithTabularDataViewModel FromSearchCriteriaViewModelBase<TModel, TSearchCriteriaViewModel>(SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> searchResult) 
            where TModel:ITabularDataViewModel
            where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<TModel> 
        {
            SearchResultViewModelBasewithTabularDataViewModel searchResultViewModelBasewithTabularDataViewModel = new SearchResultViewModelBasewithTabularDataViewModel();
            searchResultViewModelBasewithTabularDataViewModel.PageNumber = searchResult.PageNumber;
            searchResultViewModelBasewithTabularDataViewModel.PageSize = searchResult.PageSize;
            searchResultViewModelBasewithTabularDataViewModel.TotalResultsCount = searchResult.TotalResultsCount;
            searchResultViewModelBasewithTabularDataViewModel.TabularDataViewModel = new TabularDataViewModel(searchResult.Result.Select(s=>s.ToTabularDataViewModel()).ToList());
            searchResultViewModelBasewithTabularDataViewModel.RouteValueDictionary = searchResult.SearchCriteriaViewModel.ToRouteValueDictionary(searchResult.PageNumber);

            return searchResultViewModelBasewithTabularDataViewModel;
        }


    }
    public class SearchResultViewModel
    {
        public string ItemPartialViewName { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalResultsCount { get; set; }
        public int PageCount => (int)Math.Ceiling((double)TotalResultsCount / PageSize);
        public RouteValueDictionary RouteValueDictionary { get; set; }
        public string ListName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public static SearchResultViewModel FromSearchCriteriaviewModelAndSearchResult<TModel>(SearchCriteriaViewModelBase<TModel> searchCriteria, SearchResult<TModel> searchResult)
            where TModel : ITabularDataViewModel
        {
            SearchResultViewModel searchResultViewModelBasewithTabularDataViewModel = new SearchResultViewModel();
            searchResultViewModelBasewithTabularDataViewModel.PageNumber = searchResult.PageNumber;
            searchResultViewModelBasewithTabularDataViewModel.PageSize = searchResult.PageSize;
            searchResultViewModelBasewithTabularDataViewModel.TotalResultsCount = searchResult.TotalResultsCount;
            searchResultViewModelBasewithTabularDataViewModel.RouteValueDictionary = searchCriteria.ToRouteValueDictionary(searchResult.PageNumber);

            return searchResultViewModelBasewithTabularDataViewModel;

        }
        public static SearchResultViewModel FromSearchCriteriaViewModelBase<TModel, TSearchCriteriaViewModel>(SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> searchResult)
            where TModel : ITabularDataViewModel
            where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<TModel>
        {
            SearchResultViewModel searchResultViewModelBasewithTabularDataViewModel = new SearchResultViewModel();
            searchResultViewModelBasewithTabularDataViewModel.PageNumber = searchResult.PageNumber;
            searchResultViewModelBasewithTabularDataViewModel.PageSize = searchResult.PageSize;
            searchResultViewModelBasewithTabularDataViewModel.TotalResultsCount = searchResult.TotalResultsCount;
            searchResultViewModelBasewithTabularDataViewModel.RouteValueDictionary = searchResult.SearchCriteriaViewModel.ToRouteValueDictionary(searchResult.PageNumber);

            return searchResultViewModelBasewithTabularDataViewModel;
        }


    }
}