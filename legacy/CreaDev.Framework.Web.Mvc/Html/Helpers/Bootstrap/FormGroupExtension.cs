using System.Collections.Generic;
using System.Web.Mvc;
using CreaDev.Framework.Core.Models;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap
{
    
   public static class FormGroupExtension
    {
        public static FormGroup BeginFormGroup(this HtmlHelper htmlHelper)
        {
            // generates <form action="{current url}" method="post">...</form>
            string formAction = htmlHelper.ViewContext.HttpContext.Request.RawUrl;
            return FormGroupHelper(htmlHelper, new Dictionary<string, object>() { { "class", "form-group" } });
        }
        private static FormGroup FormGroupHelper(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(htmlAttributes);




            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            FormGroup theForm = new FormGroup(htmlHelper.ViewContext);



            return theForm;
        }

    }

    public class SimpleAnchor
    {

        public string Text { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public string Onclick { get; set; }
        public override string ToString()
        {
            string onClickText = string.Empty;
            if (!string.IsNullOrWhiteSpace(Onclick))
            {
                onClickText = $@"onclick=""{Onclick}""";
            }
            string hrefText = string.Empty;
            if (!string.IsNullOrWhiteSpace(Link))
            {
                hrefText = $@"href=""{Link}""";
            }
            string iconText = string.Empty;
            if (!string.IsNullOrWhiteSpace(Icon))
            {
                iconText = $@"<i class=""{Icon}""></i>";
            }
            string cssClassText = string.Empty;
            if (!string.IsNullOrWhiteSpace(CssClass))
            {
                cssClassText = $@"class=""{CssClass}""";
            }
            string result =$@"<a {hrefText} {cssClassText}>{iconText}{Text}</a>";

            return result;

        }
    }
    public static class PanelExtension
    {
        public static Panel BeginPanel(this HtmlHelper htmlHelper,string title="",string panelClass="panel-info")
        {
            return PanelHelper(htmlHelper, title,new Dictionary<string, object>() { { "class", "panel "+panelClass } });
        }
        public static Panel BeginPanelWithFooterAnchors(this HtmlHelper htmlHelper, string title = "", string panelClass = "panel-info",params string[] anchors)
        {
            return PanelHelper(htmlHelper, title, new Dictionary<string, object>() { { "class", "panel " + panelClass } },anchors);
        }
        private static Panel PanelHelper(this HtmlHelper htmlHelper,string title, IDictionary<string, object> htmlAttributes, params string[] anchors)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(htmlAttributes);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));            
            htmlHelper.ViewContext.Writer.Write($@"<div class=""panel-heading""><h2 class=""panel-title"">{title}<h2></div>");
            htmlHelper.ViewContext.Writer.Write($@"<div class=""panel-body"">");
            //

            Panel theForm = new Panel(htmlHelper.ViewContext, anchors);
            return theForm;
        }
    }
    public static class AccordionExtension
    {
        public static Accordion BeginAccordion(this HtmlHelper htmlHelper, string title = "", string panelClass = "panel-info")
        {
            return AccordionHelper(htmlHelper, title, new Dictionary<string, object>() { { "class", "panel " + panelClass } });
        }
        public static Accordion BeginAccordionWithFooterAnchors(this HtmlHelper htmlHelper, string title = "", string panelClass = "panel-info", params string[] anchors)
        {
            return AccordionHelper(htmlHelper, title, new Dictionary<string, object>() { { "class", "panel " + panelClass } }, anchors);
        }
        private static Accordion AccordionHelper(this HtmlHelper htmlHelper, string title, IDictionary<string, object> htmlAttributes, params string[] anchors)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(htmlAttributes);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            htmlHelper.ViewContext.Writer.Write($@"<div class=""panel-heading""><h2 class=""panel-title"">{title}<h2></div>");
            htmlHelper.ViewContext.Writer.Write($@"<div class=""panel-body"">");
            //

            Accordion theForm = new Accordion(htmlHelper.ViewContext, anchors);
            return theForm;
        }
    }

    
   
}
