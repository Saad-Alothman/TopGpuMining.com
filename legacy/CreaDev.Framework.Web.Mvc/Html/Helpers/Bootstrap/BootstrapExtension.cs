using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using CreaDev.Framework.Web.Layout.Layouts;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap
{
    public static class RecaptchaExtension
    {
        public static MvcHtmlString Recaptcha<TModel>(this HtmlHelper<TModel> htmlHelper, string elementId = "div-recaptcha")
        {
            /*
              <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div id="div-recaptcha"></div>
                        </div>
                    </div>
                 */

            MvcHtmlString result = MvcHtmlString.Create($@"<div id=""{elementId}"">" + "</div>");
            return result;
        }
        public static MvcHtmlString RecaptchaFormGroupWithColumn<TModel>(this HtmlHelper<TModel> htmlHelper, string elementId = "div-recaptcha",int colSize=10,int offsetSize=2)
        {
            /*
              <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div id="div-recaptcha"></div>
                        </div>
                    </div>
                 */

            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""form-group"">" +$@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(colSize)} {(offsetSize>0?BootstrapGridLayoutSystem.Instatnce.MediumOffset(offsetSize):"")}"" "+htmlHelper.Recaptcha() + "</div>" + "</div>");
            return result;
        }
        public static MvcHtmlString RecaptchaFormGroup<TModel>(this HtmlHelper<TModel> htmlHelper, string elementId = "div-recaptcha")
        {
            /*
              <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div id="div-recaptcha"></div>
                        </div>
                    </div>
                 */

            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""form-group"">" +  htmlHelper.Recaptcha()  + "</div>");
            return result;
        }
    }
    public static class BootstrapExtension
    {
        public static MvcHtmlString ColumnWithFormGroup<TModel>(this HtmlHelper<TModel> htmlHelper, MvcHtmlString mvcHtmlString, int columnWidth = 12)
        {
            MvcHtmlString formGroup = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup}"">" + mvcHtmlString.ToString() + "</div>");
            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}"">" + formGroup + "</div>");
            return result;
        }
        public static MvcHtmlString TextBoxColumnWithFormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth = 12, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null, bool renderValidator = true)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();

            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, htmlAttributesDictionary, displayNamePrefix);
            var textBox = htmlHelper.BootstrapTextBoxFor(expression, htmlAttributesDictionary);
            MvcHtmlString input = MvcHtmlString.Create($@"{textBox} {htmlHelper.ValidationMessageFor(expression)}");
            MvcHtmlString formGroup = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup}"">" + label.ToString() + input.ToString() + "</div>");
            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}"">" + formGroup + "</div>");
            return result;
        }
        public static MvcHtmlString TextBoxFormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth = 12, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null, bool renderValidator = true)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();

            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, htmlAttributesDictionary, displayNamePrefix);
            var textBox = htmlHelper.BootstrapTextBoxFor(expression, htmlAttributesDictionary);
            MvcHtmlString input = MvcHtmlString.Create($@"{textBox} {htmlHelper.ValidationMessageFor(expression)}");
            MvcHtmlString column = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}"">" + label.ToString() + input.ToString() + "</div>");
            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup}"">" + column + "</div>");
            return result;
        }
        public static MvcHtmlString TextAreaFormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth = 12, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null, bool renderValidator = true)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();

            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, htmlAttributesDictionary, displayNamePrefix);
            var textArea = htmlHelper.BootstrapTextAreaFor(expression, htmlAttributesDictionary);
            MvcHtmlString input = MvcHtmlString.Create($@"{textArea} {htmlHelper.ValidationMessageFor(expression)}");
            MvcHtmlString column = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}"">" + label.ToString() + input.ToString() + "</div>");
            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup}"">" + column + "</div>");
            return result;
        }
        public static MvcHtmlString TextAreaColumnWithFormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth = 12, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null, bool renderValidator = true)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();

            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, htmlAttributesDictionary, displayNamePrefix);
            var textBox = htmlHelper.BootstrapTextAreaFor(expression, htmlAttributesDictionary);
            MvcHtmlString input = MvcHtmlString.Create($@"{textBox} {htmlHelper.ValidationMessageFor(expression)}");
            MvcHtmlString formGroup = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup}"">" + label.ToString() + input.ToString() + "</div>");
            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}"">" + formGroup + "</div>");
            return result;
        }
        public static MvcHtmlString PasswordColumnWithFormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth = 12, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null, bool renderValidator = true)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();
            

            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, htmlAttributesDictionary, displayNamePrefix);
            var textBox = htmlHelper.BootstrapPasswordFor(expression, htmlAttributesDictionary);
            MvcHtmlString input = MvcHtmlString.Create($@"{textBox} {htmlHelper.ValidationMessageFor(expression)}");
            MvcHtmlString formGroup = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup}"">" + label.ToString() + input.ToString() + "</div>");
            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}"">" + formGroup + "</div>");
            return result;
        }

        public static MvcHtmlString DropDownListColumnWithFormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> items, int columnWidth = 12, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null, bool renderValidator = true)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();

            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, htmlAttributesDictionary, displayNamePrefix);
            var textBox = htmlHelper.BootstrapDropDownListFor(expression,items, htmlAttributesDictionary);
            MvcHtmlString input = MvcHtmlString.Create($@"{textBox} {htmlHelper.ValidationMessageFor(expression)}");
            MvcHtmlString formGroup = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup}"">" + label.ToString() + input.ToString() + "</div>");
            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}"">" + formGroup + "</div>");
            return result;
        }
        
     
        public static MvcHtmlString TextBoxColumnFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int columnWidth = 12, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null, bool renderValidator = true)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();

            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, htmlAttributesDictionary, displayNamePrefix);
            var textBox = htmlHelper.BootstrapTextBoxFor(expression, htmlAttributesDictionary);
            MvcHtmlString input = MvcHtmlString.Create($@"{textBox} {htmlHelper.ValidationMessageFor(expression)}");
            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}"">" + label.ToString() + input.ToString() + "</div>");
            return result;
        }


        public static MvcHtmlString BootstrapRowWithLabelAndTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int labelColumnWidth=3, int inputColumnWidth=9, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null,bool renderValidator=true)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();

            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, labelColumnWidth, htmlAttributesDictionary, displayNamePrefix);

            MvcHtmlString input = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(inputColumnWidth)}""> {htmlHelper.BootstrapTextBoxFor(expression, htmlAttributesDictionary)} {htmlHelper.ValidationMessageFor(expression)}</div>");

            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup }"">" + label.ToString() + input.ToString() + "</div>");
            return result;
        }

        public static MvcHtmlString BootstrapRowWithLabelAndTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int labelColumnWidth, int inputColumnWidth, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null)
        {
            if (htmlAttributesDictionary == null)

                htmlAttributesDictionary = new Dictionary<string, object>();
            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, labelColumnWidth, htmlAttributesDictionary, displayNamePrefix);
            MvcHtmlString input = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(inputColumnWidth)}""> {htmlHelper.BootstrapTextAreaFor(expression, htmlAttributesDictionary)} </div>");

            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup }"">" + label.ToString() + input.ToString() + "</div>");
            return result;
        }
        public static MvcHtmlString BootstrapRowWithLabelAndDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> items, int labelColumnWidth=3, int inputColumnWidth=9, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null)
        {
            if (htmlAttributesDictionary == null)
                htmlAttributesDictionary = new Dictionary<string, object>();


            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, labelColumnWidth, htmlAttributesDictionary, displayNamePrefix);
            MvcHtmlString input = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(inputColumnWidth)}""> {htmlHelper.BootstrapDropDownListFor(expression, items, htmlAttributesDictionary)} </div>");

            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup }"">" + label.ToString() + input.ToString() + "</div>");
            return result;
        }
        public static MvcHtmlString BootstrapSubmitRow(this HtmlHelper htmlHelper, int columnWidth, string text)
        {

            string button = $@"<button type=""submit"" class=""btn btn-primary pull-end"">{text}</button>";
            MvcHtmlString input = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(columnWidth)}""> {button} </div>");

            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup }"">" + input.ToString() + "</div>");
            return result;
        }
        public static MvcHtmlString BootstrapRowWithLabelAndCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, int labelColumnWidth, int inputColumnWidth, string displayNamePrefix="",IDictionary<string, object> htmlAttributesDictionary = null)
        {
            if (htmlAttributesDictionary == null)

                htmlAttributesDictionary = new Dictionary<string, object>();
            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, labelColumnWidth, htmlAttributesDictionary, displayNamePrefix);
            MvcHtmlString input = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(inputColumnWidth)}""> {htmlHelper.CheckBoxFor(expression, htmlAttributesDictionary)} </div>");

            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup }"">" + label.ToString() + input.ToString() + "</div>");
            return result;
        }
        public static MvcHtmlString BootstrapRowWithLabelAndStaticControlFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int labelColumnWidth, int inputColumnWidth, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null)
        {
            if (htmlAttributesDictionary == null)

                htmlAttributesDictionary = new Dictionary<string, object>();
            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, labelColumnWidth, htmlAttributesDictionary, displayNamePrefix,false);
            MvcHtmlString input = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(inputColumnWidth)}""> {htmlHelper.BootstrapStaticControlFor(expression, inputColumnWidth, htmlAttributesDictionary, displayNamePrefix)} </div>");

            MvcHtmlString result = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.FormGroup }"">" + label.ToString() + input.ToString() + "</div>");
            return result;
        }
        public static MvcHtmlString BootstrapLabelAndStaticControlFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int labelColumnWidth, int inputColumnWidth, string displayNamePrefix = "", IDictionary<string, object> htmlAttributesDictionary = null)
        {
            if (htmlAttributesDictionary == null)

                htmlAttributesDictionary = new Dictionary<string, object>();
            MvcHtmlString label = htmlHelper.BootstrapControlLabelFor(expression, labelColumnWidth, htmlAttributesDictionary, displayNamePrefix,false);
            MvcHtmlString input = MvcHtmlString.Create($@"<div class=""{BootstrapGridLayoutSystem.Instatnce.MediumColumn(inputColumnWidth)}""> {htmlHelper.BootstrapStaticControlFor(expression, inputColumnWidth, htmlAttributesDictionary, displayNamePrefix)} </div>");

            MvcHtmlString result = MvcHtmlString.Create(label.ToString() + input.ToString());
            return result;
        }
    }


    //
    // Summary:
    //     Represents an HTML form element in an MVC view.
}
