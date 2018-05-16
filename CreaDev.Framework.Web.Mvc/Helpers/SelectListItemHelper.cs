using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Helpers;
using System.Web.Mvc;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Web.Mvc.Helpers
{
    public static class SelectListItemHelper
    {
        
        
        public static List<System.Web.Mvc.SelectListItem> ToSelectListItems<TModel, TDisplayProperty, TValueyProperty>(this List<TModel> departments, Expression<Func<TModel, TDisplayProperty>> displayExpression, Expression<Func<TModel, TValueyProperty>> valueExpression, string selectedValue = "", bool addEmptyItem = false)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var department in departments)
            {

                string textPropertyName = ((displayExpression.Body as MemberExpression).Member as PropertyInfo).GetValue(department).ToString();
                string valuePropertyName = ((valueExpression.Body as MemberExpression).Member as PropertyInfo).GetValue(department).ToString();
                selectListItems.Add(new SelectListItem()
                {
                    Value = valuePropertyName,
                    Text = textPropertyName,
                    Selected = selectedValue == valuePropertyName
                });
            }
            if (addEmptyItem)
            {
                selectListItems.Insert(0, new SelectListItem() { Text = Common.Select, Value = "" });
            }
            return selectListItems.ToList();
        }
        public static List<System.Web.Mvc.SelectListItem> ToSelectListItems<TModel>(this List<TModel> lookupList, string selectedValue = "", bool addEmptyItem = false) where TModel : ILookupEntity
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var department in lookupList)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Value = department.LookupValue,
                    Text = department.LookupName,
                    Selected = selectedValue == department.LookupValue
                });
            }
            if (addEmptyItem)
            {
                selectListItems.Insert(0, new SelectListItem() { Text = Common.Select, Value = "" });
            }
            return selectListItems.ToList();
        }

        public static List<SelectListItem> GetSortSelectListItem()
        {
            
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem(){Text = Common.Ascending,Value = ((int)SortDirection.Ascending).ToString()});
            selectListItems.Add(new SelectListItem(){Text = Common.Descending, Value = ((int)SortDirection.Descending).ToString() });
            return selectListItems;

        }
    }
}