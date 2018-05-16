using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace CreaDev.Framework.Core
{
    public static class Culture
    {

        public static bool IsEnglish
        {
            get
            {
                return GetLanguage().ToLower() == "en";
            }
        }
        public static bool IsArabic
        {
            get
            {
                return GetLanguage().ToLower() == "ar";
            }
        }

        public static string GetThreadLanguageValue(string arabicValue="ar", string englishValue="en")
        {
            return IsEnglish ? englishValue : arabicValue;
        }
        public static string CultureUrl { get { return GetLanguage(); } }
        public static string GetLanguage()
        {
            string lang = "ar";

            if (HttpContext.Current != null)
            {
                var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
                if (routeData != null && !string.IsNullOrWhiteSpace(routeData.Values["language"] as string))
                    lang = (string)routeData.Values["language"];
            }


            return lang;
        }



        public static void SetCulture(string culture, bool keepGregorianCalendar = true)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(culture);

            if (keepGregorianCalendar)
                SetGregorianCalendar(cultureInfo);

            SetThreadCulture(cultureInfo, culture);
            
        }

        public static void SetGregorianCalendar(CultureInfo cultureInfo)
        {
            Calendar gregorianCalendar = new GregorianCalendar();
            if (CalendarExists(cultureInfo, gregorianCalendar))
            {
                cultureInfo.DateTimeFormat.Calendar = gregorianCalendar;
            }
        }

        public static void SetThreadCulture(CultureInfo cultureInfo, string culture)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            //Utils.InitializeCulture(locale);
        }


        public static bool CalendarExists(CultureInfo culture, Calendar cal)
        {
            foreach (Calendar optionalCalendar in culture.OptionalCalendars)
                if (cal.ToString().Equals(optionalCalendar.ToString()))
                    return true;

            return false;
        }

        public static string ReverseOnEnglish(string first, string second)
        {
            return IsEnglish ? $"{second} {first}" : $"{first} {second}";
        }
    }
}
