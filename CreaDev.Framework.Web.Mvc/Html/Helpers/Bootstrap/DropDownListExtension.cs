using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CreaDev.Framework.Web.Layout.Layouts;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap
{
    public static class DropDownListExtension
    {
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,Expression<Func<TModel, TProperty>> expression,IEnumerable<SelectListItem> items, IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes.Keys.FirstOrDefault(k => k == "class") == null)
            {
                htmlAttributes["class"] = String.Empty;
            }
            htmlAttributes["class"] = htmlAttributes["class"]+ " " + BootstrapGridLayoutSystem.Instatnce.FormControl;
            return htmlHelper.DropDownListFor(expression,items, htmlAttributes);
        }

        public static MvcHtmlString BootstrapDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> items)
        {
            IDictionary<string, object> htmlAttributesDictionary = new Dictionary<string, object>();
            return htmlHelper.BootstrapDropDownListFor(expression,items, htmlAttributesDictionary);
        }
    }
}