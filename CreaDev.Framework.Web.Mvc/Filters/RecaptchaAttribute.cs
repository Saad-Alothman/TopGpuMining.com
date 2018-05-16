using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace CreaDev.Framework.Web.Mvc.Filters
{
    /*Requirements:
     * 1- use the filter on the Action to include and the action to validate
     * 2- call                 @Html.RecaptchaFormGroupWithColumn() within the form
     * 
     * Dependencies 
     * 1- RemeWebConstants class
     * 2- PartialViewsDependancies in the layout to render scripts required automatically
     */
    public enum RecaptchaFilterType
    {
        Include = 0, IncludeIfNotAuthenticated = 1, Validate = 2, ValidateIfNotAuthenticated = 3
    }
    public class RecaptchaAttribute : ActionFilterAttribute
    {
        private string _siteKey = "";
        private string _secretKey = "";
        private string _viewDataKeyName = "";
        private RecaptchaFilterType _filterType = RecaptchaFilterType.Include;
        public RecaptchaAttribute()
        {
            
        }
        public RecaptchaAttribute(string siteKey, string secretKey, RecaptchaFilterType filterType, string viewDataKeyName = "")
        {
            this._secretKey = secretKey;
            this._siteKey = siteKey;
            this._filterType = filterType;
            this._viewDataKeyName = viewDataKeyName;
            //Log = new Utils.CreadevLogger(typeof(AutoErrorLoggerAttribute));
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            List<RecaptchaFilterType> ignoreFilterTypes = new List<RecaptchaFilterType>(){RecaptchaFilterType.ValidateIfNotAuthenticated,RecaptchaFilterType.IncludeIfNotAuthenticated};
            if (ignoreFilterTypes.Contains(_filterType) && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return;
            }

            IncludeRecaptcha();
        }

        //TODO:OnResultExecuting  instead?
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["RecaptchaFilterType"] = _filterType;
            switch (_filterType)
            {
                case RecaptchaFilterType.Validate:

                    ValidateCaptcha(filterContext);
                    break;
                case RecaptchaFilterType.ValidateIfNotAuthenticated:
                    if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        ValidateCaptcha(filterContext);
                    }
                    break;

            }

        }

        private void ValidateCaptcha(ActionExecutingContext filterContext)
        {
            if (!IsRecaptchaValid(filterContext))
            {
                filterContext.Result = new ViewResult
                {
                    ViewData = filterContext.Controller.ViewData,
                    TempData = filterContext.Controller.TempData
                };
            }

        }



        private void IncludeRecaptcha()
        {
            /* render something similiar to:
                     <script type="text/javascript">
                            var onloadCallback = function() {
                                grecaptcha.render('div-recaptcha', { 'sitekey': '@Settings.ReCaptchaSiteKey' });
                            };
                    </script>
            */
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($@"<script type=""text/javascript"">");
            sb.AppendLine($@"var onloadCallback = function() {{");
            sb.AppendLine($@"grecaptcha.render('div-recaptcha', {{ 'sitekey': '{_siteKey}' }});");
            sb.AppendLine($@"}};");
            sb.AppendLine($@"</script>");
            PartialViewsDependancies.GetFromRequest().AddStyle(sb.ToString());

            string language = CreaDev.Framework.Core.Culture.GetThreadLanguageValue();
            string scriptBottomOfPage =
                $@"<script src=""https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit&hl={
                        language
                    }"" async defer> </script>";
            PartialViewsDependancies.GetFromRequest().AddScript(scriptBottomOfPage);
        }
        public bool IsRecaptchaValid(ActionExecutingContext filterContext)
        {
            string ip = HttpContext.Current.Request.UserHostAddress;
            string response = HttpContext.Current.Request["g-recaptcha-response"];
            string secretKey = _secretKey;
            WebClient client = new WebClient();
            string result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}", secretKey, response, ip));
            JObject obj = JObject.Parse(result);
            bool isSucess = (bool)obj.SelectToken("success");
            filterContext.Controller.ViewData[_viewDataKeyName] = isSucess;
            return isSucess;
        }


    }
}