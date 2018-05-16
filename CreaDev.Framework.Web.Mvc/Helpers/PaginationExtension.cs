using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using CreaDev.Framework.Core.Collections;
using CreaDev.Framework.Core.Extensions;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Web.Mvc.Html.Ajax;
using CreaDev.Framework.Web.Mvc.Models;

namespace CreaDev.Framework.Web.Mvc.Helpers
{
    public static class PaginationExtension
    {
        //To render pagination without Ajax
        public static string RenderPagination<T>(this HtmlHelper htmlHelper, SearchResult<T> searchResult, string action, string controller,
            RouteValueDictionary additionalValues = null, string httpMethod = "GET")
        {

            if (additionalValues == null)
                additionalValues = new RouteValueDictionary();


            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            TagBuilder unorderedListTag = new TagBuilder("ul");
            unorderedListTag.AddCssClass("pagination");
            for (int i = 0; i < searchResult.PageCount; i++)
            {

                TagBuilder listItemTag = new TagBuilder("li");

                RouteValueDictionary pageDictionary = new RouteValueDictionary(additionalValues);
                pageDictionary.Add("page", i + 1);


                string url = urlHelper.Action(action, controller, pageDictionary);
                //anchorTag.Attributes.Add("href", url);
                if (searchResult.PageNumber == i + 1)
                {
                    listItemTag.Attributes.Add("class", "active");
                    listItemTag.InnerHtml = "<a>" + (i + 1).ToString() + "</a>";
                }
                else
                {
                    string outterNumber = (i + 1).ToString();
                    pageDictionary.Add("controller", controller);

                    listItemTag.InnerHtml = htmlHelper.ActionLink(outterNumber, action, pageDictionary).ToString();
                }
                unorderedListTag.InnerHtml += listItemTag.ToString();

            }
            return unorderedListTag.ToString();
        }


        //public static string RenderPagination<TModel, TSearchCriteriaViewModel>
        //    (this AjaxHelper ajaxHelper, SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> searchResult, string action, string controller,
        //        string httpMethod = "POST", string updateTargetId = "", bool isViewModelSearchResults = true) where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<TModel>
        //{
        //    if (String.IsNullOrWhiteSpace(updateTargetId))
        //    {
        //        var pluralizationService = PluralizationService.CreateService(
        //            CultureInfo.GetCultureInfo("en-us"));
        //        string typename = typeof(TModel).Name.ToLower();
        //        string typeNamePlural = pluralizationService.IsPlural(typename)
        //            ? typename
        //            : pluralizationService.Pluralize(typename);
        //        updateTargetId = typeNamePlural + "-list";
        //    }


        //    UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        //    TagBuilder unorderedListTag = new TagBuilder("ul");
        //    unorderedListTag.AddCssClass("pagination");
        //    for (int i = 0; i < searchResult.PageCount; i++)
        //    {

        //        TagBuilder listItemTag = new TagBuilder("li");
        //        string prefix = String.Empty;
        //        if (isViewModelSearchResults)
        //        {
        //            prefix =
        //                ReflectionUtils.GetMemberName(
        //                    ((SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> c) =>
        //                        c.SearchCriteriaViewModel)) + ".";
        //        }
        //        RouteValueDictionary pageDictionary = searchResult.SearchCriteriaViewModel.ToRouteValueDictionary(i + 1, prefix);



        //        string url = urlHelper.Action(action, controller, pageDictionary);
        //        CreaDevAjaxOptions ajaxOptions = new CreaDevAjaxOptions(httpMethod, updateTargetId, "#sss", "#sss");
        //        //anchorTag.Attributes.Add("href", url);
        //        if (searchResult.PageNumber == i + 1)
        //        {
        //            listItemTag.Attributes.Add("class", "active");
        //            listItemTag.InnerHtml = "<a>" + (i + 1).ToString() + "</a>";
        //        }
        //        else
        //        {
        //            listItemTag.InnerHtml = ajaxHelper.ActionLink((i + 1).ToString(), action, controller, pageDictionary, ajaxOptions).ToString();
        //        }
        //        unorderedListTag.InnerHtml += listItemTag.ToString();

        //    }
        //    return unorderedListTag.ToString();
        //}
        public static string RenderPagination(this AjaxHelper ajaxHelper, SearchResultViewModelBasewithTabularDataViewModel searchResult, RouteValueDictionary additionalValues = null, string httpMethod = "POST", string updateTargetSelector = "", bool isViewModelSearchResults = true, string activeListItemCssClass = "active")
        {
            // basically if the item is issue, it will return issues-list
            //if (String.IsNullOrWhiteSpace(updateTargetSelector))
            //    updateTargetSelector = GetTypeNameList<TModel, TSearchCriteriaViewModel>();
            

            TagBuilder unorderedListTag = new TagBuilder("ul");
            unorderedListTag.AddCssClass("pagination");
            for (int i = 0; i < searchResult.PageCount; i++)
            {
                var listItemTagString = GetPageLinkAjax(ajaxHelper, searchResult, searchResult.Action, searchResult.Controller, additionalValues, httpMethod, updateTargetSelector, activeListItemCssClass, i + 1);
                unorderedListTag.InnerHtml += listItemTagString;
            }
            return unorderedListTag.ToString();
        }

        public static string RenderPagination<TModel, TSearchCriteriaViewModel>
            (this AjaxHelper ajaxHelper, SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> searchResult, string action, string controller,
                RouteValueDictionary additionalValues = null, string httpMethod = "POST", string updateTargetSelector = "", bool isViewModelSearchResults = true,string activeListItemCssClass="active") where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<TModel>
        {
            // basically if the item is issue, it will return issues-list
            if (String.IsNullOrWhiteSpace(updateTargetSelector))
                updateTargetSelector = GetTypeNameList<TModel, TSearchCriteriaViewModel>();


            TagBuilder unorderedListTag = new TagBuilder("ul");
            unorderedListTag.AddCssClass("pagination");
            for (int i = 0; i < searchResult.PageCount; i++)
            {
                var listItemTagString = GetPageLinkAjax(ajaxHelper, searchResult, action, controller, additionalValues, httpMethod, updateTargetSelector, activeListItemCssClass, i+1);
                unorderedListTag.InnerHtml += listItemTagString;
            }
            return unorderedListTag.ToString();
        }

        private static string GetPageLinkAjax<TModel, TSearchCriteriaViewModel>(AjaxHelper ajaxHelper,
            SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> searchResult, string action, string controller, RouteValueDictionary additionalValues,
            string httpMethod, string updateTargetSelector, string activeListItemCssClass, int pageNumber)
            where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<TModel>
        {
            TagBuilder listItemTag = new TagBuilder("li");
            RouteValueDictionary pageDictionary = searchResult.SearchCriteriaViewModel.ToRouteValueDictionary(pageNumber );
            if (additionalValues != null && additionalValues.Count > 0)
            {
                foreach (var additionalValue in additionalValues)
                {
                    pageDictionary.Add(additionalValue.Key, additionalValue.Value);
                }
            }


            CreaDevAjaxOptions ajaxOptions = new CreaDevAjaxOptions(httpMethod, updateTargetSelector, "#sss", "#sss");
            if (searchResult.PageNumber == pageNumber)
            {
                listItemTag.Attributes.Add("class", activeListItemCssClass);
                listItemTag.InnerHtml = "<a>" + (pageNumber).ToString() + "</a>";
            }
            else
            {
                listItemTag.InnerHtml =
                    ajaxHelper.ActionLink((pageNumber).ToString(), action, controller, pageDictionary, ajaxOptions).ToString();
                //Wrokaround bug that generates space befpre the values
                listItemTag.InnerHtml = listItemTag.InnerHtml.Replace("%20?", "?");
            }
            string listItemTagString = listItemTag.ToString();
            return listItemTagString;
        }
        private static string GetPageLinkAjax(AjaxHelper ajaxHelper,
            SearchResultViewModelBasewithTabularDataViewModel searchResult, string action, string controller, RouteValueDictionary additionalValues,
            string httpMethod, string updateTargetSelector, string activeListItemCssClass, int pageNumber)
        {
            TagBuilder listItemTag = new TagBuilder("li");
            searchResult.RouteValueDictionary["PageNumber"] = pageNumber;
            RouteValueDictionary pageDictionary = searchResult.RouteValueDictionary;
            if (additionalValues != null && additionalValues.Count > 0)
            {
                foreach (var additionalValue in additionalValues)
                {
                    pageDictionary.Add(additionalValue.Key, additionalValue.Value);
                }
            }


            CreaDevAjaxOptions ajaxOptions = new CreaDevAjaxOptions(httpMethod, updateTargetSelector, "","");
            if (searchResult.PageNumber == pageNumber)
            {
                listItemTag.Attributes.Add("class", activeListItemCssClass);
                listItemTag.InnerHtml = "<a>" + (pageNumber).ToString() + "</a>";
            }
            else
            {
                listItemTag.InnerHtml =
                    ajaxHelper.ActionLink((pageNumber).ToString(), action, controller, pageDictionary, ajaxOptions).ToString();
                //Wrokaround bug that generates space befpre the values
                listItemTag.InnerHtml = listItemTag.InnerHtml.Replace("%20?", "?");
            }
            string listItemTagString = listItemTag.ToString();
            return listItemTagString;
        }

        //public static string RenderPagination<TModel, TSearchCriteriaViewModel>
        //(this AjaxHelper ajaxHelper, SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> searchResult, string action, string controller,
        //    RouteValueDictionary additionalValues = null, string httpMethod = "POST", string updateTargetSelector = "", bool isViewModelSearchResults = true, string activeListItemCssClass = "active") where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<TModel>
        //{

        //    if (String.IsNullOrWhiteSpace(updateTargetSelector))
        //    {
        //        updateTargetSelector = GetTypeNameList<TModel, TSearchCriteriaViewModel>();
        //    }


        //    TagBuilder unorderedListTag = new TagBuilder("ul");
        //    unorderedListTag.AddCssClass("pagination");
        //    for (int i = 0; i < searchResult.PageCount; i++)
        //    {

        //        TagBuilder listItemTag = new TagBuilder("li");
        //        string prefix = String.Empty;
        //        //if (isViewModelSearchResults)
        //        //{
        //        //    prefix =
        //        //        ReflectionUtils.GetMemberName(
        //        //            ((SearchResultViewModelBase<TModel, TSearchCriteriaViewModel> c) =>
        //        //                c.SearchCriteriaViewModel)) + ".";
        //        //}
        //        RouteValueDictionary pageDictionary = searchResult.SearchCriteriaViewModel.ToRouteValueDictionary(i + 1, prefix);
        //        if (additionalValues != null && additionalValues.Count > 0)
        //        {
        //            foreach (var additionalValue in additionalValues)
        //            {
        //                pageDictionary.Add(additionalValue.Key, additionalValue.Value);
        //            }
        //        }


        //        //   string url = urlHelper.Action(action, controller, pageDictionary);
        //        CreaDevAjaxOptions ajaxOptions = new CreaDevAjaxOptions(httpMethod, updateTargetSelector, "#sss", "#sss");
        //        //anchorTag.Attributes.Add("href", url);
        //        if (searchResult.PageNumber == i + 1)
        //        {
        //            listItemTag.Attributes.Add("class", activeListItemCssClass);
        //            listItemTag.InnerHtml = "<a>" + (i + 1).ToString() + "</a>";
        //        }
        //        else
        //        {
        //            listItemTag.InnerHtml = ajaxHelper.ActionLink((i + 1).ToString(), action, controller, pageDictionary, ajaxOptions).ToString();
        //            //Wrokaround bug that generates space befpre the values
        //            listItemTag.InnerHtml = listItemTag.InnerHtml.Replace("%20?", "?");
        //        }
        //        unorderedListTag.InnerHtml += listItemTag.ToString();

        //    }
        //    return unorderedListTag.ToString();
        //}

        private static string GetTypeNameList<TModel, TSearchCriteriaViewModel>()
            where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<TModel>
        {
            string updateTargetSelector;
            var pluralizationService = PluralizationService.CreateService(
                CultureInfo.GetCultureInfo("en-us"));
            string typename = typeof (TModel).Name.ToProperCase().Replace(" ","-").ToLower();
            string typeNamePlural = pluralizationService.IsPlural(typename)
                ? typename
                : pluralizationService.Pluralize(typename);
            updateTargetSelector = typeNamePlural + "-list";
            return updateTargetSelector;
        }

        public static string RenderPagination<T>(this AjaxHelper ajaxHelper, SearchResult<T> searchResult, string action, string controller,
            RouteValueDictionary additionalValues = null, string httpMethod = "GET", string updateTargetSelector = "")
        {
            if (String.IsNullOrWhiteSpace(updateTargetSelector))
            {
                var pluralizationService = PluralizationService.CreateService(
                    CultureInfo.GetCultureInfo("en-us"));
                string typename = typeof(T).Name.ToLower();
                string typeNamePlural = pluralizationService.IsPlural(typename)
                    ? typename
                    : pluralizationService.Pluralize(typename);
                updateTargetSelector = typeNamePlural + "-list";
            }
            if (additionalValues == null)
            {
                additionalValues = new RouteValueDictionary();
            }
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            TagBuilder unorderedListTag = new TagBuilder("ul");
            unorderedListTag.AddCssClass("pagination");
            for (int i = 0; i < searchResult.PageCount; i++)
            {

                TagBuilder listItemTag = new TagBuilder("li");

                RouteValueDictionary pageDictionary = new RouteValueDictionary(additionalValues);
                pageDictionary.Add("page", i + 1);


                string url = urlHelper.Action(action, controller, pageDictionary);
                AjaxOptions ajaxOptions = new AjaxOptions() { Url = url, HttpMethod = httpMethod, UpdateTargetId = updateTargetSelector };
                //anchorTag.Attributes.Add("href", url);
                if (searchResult.PageNumber == i + 1)
                {
                    listItemTag.Attributes.Add("class", "active");
                    listItemTag.InnerHtml = "<a>" + (i + 1).ToString() + "</a>";
                }
                else
                {
                    listItemTag.InnerHtml = ajaxHelper.ActionLink((i + 1).ToString(), action, controller, pageDictionary, ajaxOptions).ToString();
                }
                unorderedListTag.InnerHtml += listItemTag.ToString();

            }
            return unorderedListTag.ToString();
        }

        public static string RenderPagination<T>(this SearchResult<T> searchResult, string action, string controller, RouteValueDictionary additionalValues = null)
        {
            if (additionalValues == null)
            {
                additionalValues = new RouteValueDictionary();
            }
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            TagBuilder unorderedListTag = new TagBuilder("ul");
            for (int i = 0; i < searchResult.PageCount; i++)
            {
                TagBuilder listItemTag = new TagBuilder("ul");
                TagBuilder anchorTag = new TagBuilder("a");
                RouteValueDictionary pageDictionary = new RouteValueDictionary(additionalValues);
                pageDictionary.Add("page", i + 1);

                string url = urlHelper.Action(action, controller, pageDictionary);
                anchorTag.Attributes.Add("href", url);
                anchorTag.InnerHtml = (i + 1).ToString();
                listItemTag.InnerHtml += anchorTag.ToString();
                unorderedListTag.InnerHtml += listItemTag.ToString();

            }
            return unorderedListTag.ToString();
        }
    }
}
