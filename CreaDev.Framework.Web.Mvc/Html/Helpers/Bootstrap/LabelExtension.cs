using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using CreaDev.Framework.Web.Layout.Layouts;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap
{
    public static class LabelExtension
    {
        public static MvcHtmlString BootstrapControlLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes, string displayNamePrefix, bool addAsteriskIfRequired = true)
        {
            IDictionary<string, object> labelhtmlAttributes = new Dictionary<string, object>(htmlAttributes);
            if (labelhtmlAttributes.Keys.FirstOrDefault(k => k == "class") == null)
            {
                labelhtmlAttributes["class"] = String.Empty;
            }
            labelhtmlAttributes["class"] = " " + "control-label";
            if (labelhtmlAttributes.Keys.FirstOrDefault(k => k == "id") != null)
            {
                labelhtmlAttributes["for"] = labelhtmlAttributes["id"];
                labelhtmlAttributes["id"] = "label-" + labelhtmlAttributes["id"];
            }
            string asteriskHtml = string.Empty;
            if (addAsteriskIfRequired)
            {
                var memberExpression = expression.Body as MemberExpression;
                bool isRequired= memberExpression?.Member.GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault() != null;
                if (isRequired)
                asteriskHtml = "<span class=\"asterisk-required\">*</span>";

            }
            return htmlHelper.LabelFor(expression, labelhtmlAttributes, displayNamePrefix, asteriskHtml);
        }
        public static MvcHtmlString BootstrapControlLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth, IDictionary<string, object> htmlAttributes, string displayNamePrefix, bool addAsteriskIfRequired = true)
        {
            IDictionary<string, object> labelhtmlAttributes = new Dictionary<string, object>(htmlAttributes);
            if (labelhtmlAttributes.Keys.FirstOrDefault(k => k == "class") == null)
            {
                labelhtmlAttributes["class"] = String.Empty;
            }
            labelhtmlAttributes["class"] = " " + BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth) + " control-label";
            if (labelhtmlAttributes.Keys.FirstOrDefault(k => k == "id") != null)
            {
                labelhtmlAttributes["id"] = "label-" + labelhtmlAttributes["id"];
            }
            string asteriskHtml = string.Empty;
            if (addAsteriskIfRequired)
            {
                asteriskHtml = "<span class=\"asterisk-required\">*</span>";

            }
            return htmlHelper.LabelFor(expression, labelhtmlAttributes, displayNamePrefix, asteriskHtml);
        }
        public static MvcHtmlString BootstrapStaticControlFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth, IDictionary<string, object> htmlAttributes, string displayNamePrefix)
        {
            if (htmlAttributes.Keys.FirstOrDefault(k => k == "class") == null)
            {
                htmlAttributes["class"] = String.Empty;
            }
            htmlAttributes["class"] = " " + " form-control-static";


            return htmlHelper.ValueLabelFor(expression, htmlAttributes, displayNamePrefix);
        }
        public static MvcHtmlString BootstrapControlLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth, string displayNamePrefix, bool addAsteriskIfRequired = true)
        {
            IDictionary<string, object> htmlAttributesDictionary = new Dictionary<string, object>();
                    string asteriskHtml = string.Empty;
            if (addAsteriskIfRequired)
            {
                     asteriskHtml = "<span class=\"asterisk-required\">*</span>";
                
            }
            return htmlHelper.LabelFor(expression, htmlAttributesDictionary, displayNamePrefix,asteriskHtml);
        }
        public static MvcHtmlString BootstrapStaticControlFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth)
        {
            IDictionary<string, object> htmlAttributesDictionary = new Dictionary<string, object>();
            return htmlHelper.ValueLabelFor(expression, htmlAttributesDictionary, "");
        }


    }
}
