using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace CreaDev.Framework.Web.Mvc.Filters
{
    public class GoogleAnalyticsIgnoreAttribute : ActionFilterAttribute
    {

    }
    public class GoogleAnalyticsAttribute : ActionFilterAttribute
    {
        private string _trackingId = "";
        private bool _isTrackByUser = false;
        public GoogleAnalyticsAttribute()
        {
        }
        public GoogleAnalyticsAttribute(string trackingId,bool isIsTrackByUser=false)
        {
            this._trackingId = trackingId;
            this._isTrackByUser = isIsTrackByUser;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            FilterAttribute ignoreFilterAttribute = filterContext.ActionDescriptor.GetFilterAttributes(false).FirstOrDefault(s => s.GetType() == typeof(GoogleAnalyticsIgnoreAttribute));
            bool isIgnore = ignoreFilterAttribute != null;
            if (isIgnore)
                return;
            IncludeAnalyticsScript();
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            IncludeAnalyticsScript();
        }

        private void IncludeAnalyticsScript()
        {
            /* render something similiar to:
                    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-108221738-1"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag(){dataLayer.push(arguments);}
    gtag('js', new Date());

    gtag('config', '@RemeWebConstants.GOOGLE_ANALYTICS_TRACKING_ID');
</script>
            */
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"<script>");
            sb.AppendLine($@"(function(i,s,o,g,r,a,m){{i['GoogleAnalyticsObject'] = r;i[r]=i[r]||function(){{");
            sb.AppendLine($@"(i[r].q=i[r].q||[]).push(arguments)}},i[r].l=1*new Date();a=s.createElement(o),");
            sb.AppendLine($@"m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)");
            sb.AppendLine($@"}})(window,document,'script','https://www.google-analytics.com/analytics.js','ga');");
            sb.AppendLine($@"ga('create', 'UA-{_trackingId}-1', 'auto');");
            sb.AppendLine($@"ga('send', 'pageview');");
            if (_isTrackByUser && Thread.CurrentPrincipal.Identity.IsAuthenticated)
                sb.AppendLine($@"ga('set', 'userId', '{Thread.CurrentPrincipal.Identity.Name}'); // Set the user ID using signed-in user_id.");


            sb.AppendLine($@"</script>");
            PartialViewsDependancies.GetFromRequest().AddStyle(sb.ToString());
        }
      


    }
}