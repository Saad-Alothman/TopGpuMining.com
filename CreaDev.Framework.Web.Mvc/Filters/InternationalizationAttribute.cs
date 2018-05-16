using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CreaDev.Framework.Core;

namespace CreaDev.Framework.Web.Mvc.Filters
{
    public class InternationalizationAttribute : ActionFilterAttribute
    {
        public const string DEAFULT_LANGUAGE = "ar";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string language = (string)filterContext.RouteData.Values["language"] ?? DEAFULT_LANGUAGE;
            if (!ExistCultureLanguage(language))
            {
                if (IsArea(language))
                {
                    HttpContext.Current.Response.Redirect("/"+DEAFULT_LANGUAGE + filterContext.RequestContext.HttpContext.Request.Url.LocalPath);
                }
                
            }
            string culture = language == "en" ? "US" : "SA";
            Culture.SetCulture(string.Format("{0}-{1}", language, culture));
            

            
        }

        public bool ExistCultureLanguage( string language)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures).Except(CultureInfo.GetCultures(CultureTypes.SpecificCultures));
            return cultures.FirstOrDefault(c => c.TwoLetterISOLanguageName == language) != null;
        }
        public bool IsArea(string value)
        {
            return IsExistArea(value);
        }

        private static bool IsExistArea(string value)
        {
            var areaNames = RouteTable.Routes.OfType<Route>()
                .Where(d => d.DataTokens != null && d.DataTokens.ContainsKey("area"))
                .Select(r => r.DataTokens["area"]).ToArray();

            return areaNames.FirstOrDefault(a => a.ToString() == value) != null;
        }
    }
}
