using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers
{
    public static class PartialExtension
    {
        public enum PartialType
        {
            Form,SearchForm,List
        }
        public static MvcHtmlString PartialForByConvention<TModel, TProperty>(this HtmlHelper<TModel> helper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, bool escapFirstDot = false, bool useDisplayNamePrefix = true, PartialType partialType= PartialType.Form)
        {
            string className = expression.Body.Type.Name;
            
            string path = ViewPathByConventionHelper.GetFullPath(className, partialType);
            return PartialFor(helper, expression, path, escapFirstDot, useDisplayNamePrefix);
        }

        public static Type GetType<T>(T member)
        {
            return typeof (T);
        }
        public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper, System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression, string partialViewName,bool escapFirstDot = false, bool useDisplayNamePrefix = true)
        {

            string name = ExpressionHelper.GetExpressionText(expression);
            object model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
            string prefix = name;
            if (helper.ViewData.TemplateInfo != null && !string.IsNullOrWhiteSpace(helper.ViewData.TemplateInfo.HtmlFieldPrefix ))
            {
                if (!escapFirstDot)
                    prefix = helper.ViewData.TemplateInfo.HtmlFieldPrefix + "." + name;
                else
                    prefix = helper.ViewData.TemplateInfo.HtmlFieldPrefix + name;
            }
            var viewData = new ViewDataDictionary(helper.ViewData)
            {
                TemplateInfo = new System.Web.Mvc.TemplateInfo
                {
                    HtmlFieldPrefix = prefix
                }
            };
            if (useDisplayNamePrefix)
            {
                string displayNameVariable = "displayName";
                string displayName = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).DisplayName;
                if (viewData.ContainsKey(displayNameVariable))
                    viewData[displayNameVariable] = viewData[displayNameVariable] + " " + displayName;
                else
                    viewData.Add(displayNameVariable, displayName);
            }


            return helper.Partial(partialViewName, model, viewData);

        }

        
    }
}
