using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TopGpuMining.Core.Resources;

namespace TopGpuMining.Web.Helpers
{
    public static class SelectListItemHelper
    {


        public static List<SelectListItem> ToSelectListItems<TModel, TDisplayProperty, TValueyProperty>(this List<TModel> items, Expression<Func<TModel, TDisplayProperty>> displayExpression, Expression<Func<TModel, TValueyProperty>> valueExpression, string selectedValue = "", bool addEmptyItem = false)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var item in items)
            {
                string textPropertyName = ((displayExpression.Body as MemberExpression).Member as PropertyInfo).GetValue(item).ToString();
                string valuePropertyName = ((valueExpression.Body as MemberExpression).Member as PropertyInfo).GetValue(item).ToString();
                selectListItems.Add(new SelectListItem()
                {
                    Value = valuePropertyName,
                    Text = textPropertyName,
                    Selected = selectedValue == valuePropertyName
                });
            }
            if (addEmptyItem)
            {
                selectListItems.Insert(0, new SelectListItem() { Text = CommonText.Select, Value = "" });
            }
            return selectListItems.ToList();
        }
        //public static List<SelectListItem> ToSelectListItems<TModel>(this List<TModel> lookupList, string selectedValue = "", bool addEmptyItem = false) where TModel : ILookupEntity
        //{
        //    List<SelectListItem> selectListItems = new List<SelectListItem>();
        //    foreach (var department in lookupList)
        //    {
        //        selectListItems.Add(new SelectListItem()
        //        {
        //            Value = department.LookupValue,
        //            Text = department.LookupName,
        //            Selected = selectedValue == department.LookupValue
        //        });
        //    }
        //    if (addEmptyItem)
        //    {
        //        selectListItems.Insert(0, new SelectListItem() { Text = CommonText.Select, Value = "" });
        //    }
        //    return selectListItems.ToList();
        //}

        //public static List<SelectListItem> GetSortSelectListItem()
        //{

        //    List<SelectListItem> selectListItems = new List<SelectListItem>();
        //    selectListItems.Add(new SelectListItem() { Text = CommonText.Ascending, Value = ((int)SortDirection.Ascending).ToString() });
        //    selectListItems.Add(new SelectListItem() { Text = CommonText.Descending, Value = ((int)SortDirection.Descending).ToString() });
        //    return selectListItems;

        //}
    }
}
