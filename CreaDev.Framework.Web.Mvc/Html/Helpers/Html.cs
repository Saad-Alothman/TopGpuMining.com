using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;
using CreaDev.Framework.Core;
using CreaDev.Framework.Core.Extensions;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers
{
    public static class Html
    {

        public static System.Web.IHtmlString Break(string str, int wordsPerLine)
        {

            string result = string.Empty;
            string[] words = str.Trim().Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                result += words[i];
                if (i + 1 % 5 == 0)
                    result += "<br />";

            }
            return new System.Web.HtmlString(result);
        }
        public static List<SelectListItem> EnumToListItem<TEnum>(ResourceManager resourceManager, List<TEnum> excludedItems = null, bool useEnumTextAsValue = false, string selectedValue = "")
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            
            foreach (var enumVal in Enum.GetValues(typeof(TEnum)))
            {
                TEnum theENum = (TEnum)enumVal;
                if (excludedItems == null || !excludedItems.Contains(theENum))
                {


                    var name = Enum.GetName(typeof(TEnum), enumVal);
                    int enumValueInt = Convert.ToInt32(theENum);
                    string text = name;
                   FieldInfo fieldInfo= theENum.GetType().GetField(theENum.ToString());
                    if (fieldInfo.GetCustomAttributes<DisplayAttribute>().FirstOrDefault() != null)
                    {
                        text = fieldInfo.GetCustomAttributes<DisplayAttribute>().FirstOrDefault().GetName();
                    }
                    else if (resourceManager != null && !string.IsNullOrWhiteSpace(resourceManager.GetString(name)))
                    {
                        text = resourceManager.GetString(name);
                    }

                    


                    string listItemValue = useEnumTextAsValue ? name : enumValueInt.ToString();
                    var listitem = new SelectListItem() { Text = text, Value = listItemValue, Selected = selectedValue == listItemValue };
                    listItems.Add(listitem);
                }
            }


            return listItems;
        }
        public static NameValueCollection EnumToNameValueCollection<TEnum>(ResourceManager resourceManager, List<TEnum> excludedItems = null, bool useEnumTextAsValue = false, string selectedValue = "",bool useEnumTextAsText=false)
        {
            NameValueCollection listItems = new NameValueCollection();

            foreach (var enumVal in Enum.GetValues(typeof(TEnum)))
            {
                TEnum theENum = (TEnum)enumVal;
                if (excludedItems == null || !excludedItems.Contains(theENum))
                {
                    var name = Enum.GetName(typeof(TEnum), enumVal);
                    int enumValueInt = Convert.ToInt32(theENum);
                    string text = name;
                    FieldInfo fieldInfo = theENum.GetType().GetField(theENum.ToString());
                    if (!useEnumTextAsText)
                    {
                        if (fieldInfo.GetCustomAttributes<DisplayAttribute>().FirstOrDefault() != null)
                        {
                            text = fieldInfo.GetCustomAttributes<DisplayAttribute>().FirstOrDefault().GetName();
                        }
                        else if (resourceManager != null && !string.IsNullOrWhiteSpace(resourceManager.GetString(name)))
                        {
                            text = resourceManager.GetString(name);
                        }
                    }
                    
                    string listItemValue = useEnumTextAsValue ? name : enumValueInt.ToString();
                    listItems.Add(text,listItemValue);
                }
            }


            return listItems;
        }
        public static string GetPropertyName<T, TReturn>(this HtmlHelper<T> html,Expression<Func<T, TReturn>> expression,bool javascriptFormat=false)
        {
            MemberExpression body = (MemberExpression)expression.Body;
            string name = body.Member.Name;
            if (javascriptFormat)
            {
                name = name.LowerFirst();
            }
            return name;
        }
        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, string displayNamePrefix, string diasplayNamePostfixHtml = "")

        {

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(labelText))

            {

                return MvcHtmlString.Empty;

            }
            if (!string.IsNullOrEmpty(displayNamePrefix))
            {
                labelText = displayNamePrefix + " " + labelText;
            }



            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            if (!htmlAttributes.ContainsKey("for"))
                tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            tag.InnerHtml = labelText;
            if (!string.IsNullOrWhiteSpace(diasplayNamePostfixHtml))
            {
                tag.InnerHtml += diasplayNamePostfixHtml;

            }

            MvcHtmlString laMvcHtmlString = MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));

            return laMvcHtmlString;

        }
        public static MvcHtmlString ValueLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes, string displayNamePrefix)

        {

            Func<TModel, TValue> method = expression.Compile();

            TValue value = method(html.ViewData.Model);

            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            //tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            if (value != null)
                tag.SetInnerText(value.ToString());
            else
                tag.SetInnerText("");

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));

        }

        public static MvcHtmlString DisableIf(this MvcHtmlString htmlString, Func<bool> expression)
        {
            if (expression.Invoke())
            {
                var html = htmlString.ToString();
                const string disabled = "\"disabled\"";
                html = html.Insert(html.IndexOf(">",
                  StringComparison.Ordinal), " disabled= " + disabled);
                return new MvcHtmlString(html);
            }
            return htmlString;

        }

        public static string RightForArabic(string format)
        {

            return string.Format(format, Culture.IsEnglish ? "left" : "right");
        }
        public static string LeftForArabic(string format)
        {

            return string.Format(format, Culture.IsEnglish ? "right" : "left");
        }
    }
}
