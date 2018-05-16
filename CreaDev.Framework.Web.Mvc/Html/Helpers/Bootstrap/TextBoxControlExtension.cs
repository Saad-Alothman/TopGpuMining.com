using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CreaDev.Framework.Web.Layout.Layouts;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap
{
    public static class TextBoxControlExtension
    {
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes,bool checkDisabledViewData=false)
        {
            
            if (htmlAttributes.Keys.FirstOrDefault(k => k == "class") == null)
            {
                htmlAttributes["class"] = String.Empty;
            }
            htmlAttributes["class"] += " " + BootstrapGridLayoutSystem.Instatnce.FormControl;
            //System.Web.Mvc.Html.TextBoxExtensions.TextBoxFor(expression, true, htmlAttributes: htmlAttributes);
            

            return htmlHelper.TextBoxFor(expression, htmlAttributes);
            //return  new MvcHtmlString(htmlHelper.TextBoxFor(expression, true,htmlAttributes: htmlAttributes).ToHtmlString().ToString());

        }
        public static MvcHtmlString BootstrapPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes, bool checkDisabledViewData = false)
        {

            if (htmlAttributes.Keys.FirstOrDefault(k => k == "class") == null)
            {
                htmlAttributes["class"] = String.Empty;
            }
            htmlAttributes["class"] += " " + BootstrapGridLayoutSystem.Instatnce.FormControl;
            //System.Web.Mvc.Html.TextBoxExtensions.TextBoxFor(expression, true, htmlAttributes: htmlAttributes);


            return htmlHelper.PasswordFor(expression, htmlAttributes);
            //return  new MvcHtmlString(htmlHelper.TextBoxFor(expression, true,htmlAttributes: htmlAttributes).ToHtmlString().ToString());

        }


        public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            IDictionary<string, object> htmlAttributesDictionary = new Dictionary<string, object>();
            return htmlHelper.BootstrapTextBoxFor(expression, htmlAttributesDictionary);
        }
    }

    public static class TextAreaExtension
    {
        public static MvcHtmlString BootstrapTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes.Keys.FirstOrDefault(k => k == "class") == null)
            {
                htmlAttributes["class"] = String.Empty;
            }
            htmlAttributes["class"] = " " + BootstrapGridLayoutSystem.Instatnce.FormControl;
            return htmlHelper.TextAreaFor(expression, htmlAttributes);
        }
    }
}