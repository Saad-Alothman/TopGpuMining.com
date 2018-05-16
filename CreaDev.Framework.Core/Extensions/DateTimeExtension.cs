using System;
using System.Globalization;

namespace CreaDev.Framework.Core.Extensions
{
    public static class DateTimeExtension
    {
      
        
        public static int DifferenceTodayDays(this System.DateTime dateTime, bool withTime = false,bool isAbsoluteValue = true)
        {

            int result = (System.DateTime.Now - dateTime).Days;
            if (isAbsoluteValue)
            {
                result = Math.Abs(result);
            }
            return result;
        }
        public static int DifferenceTodayHours(this System.DateTime dateTime, bool withTime = false, bool isAbsoluteValue = true)
        {
            
            int result = (System.DateTime.Now - dateTime).Hours;
            if (isAbsoluteValue)
            {
                result = Math.Abs(result);
            }
            return result;
        }
        public static bool Within(this System.DateTime dateTime, System.DateTime from,System.DateTime to,bool inclusiveFrom =true,bool inclusiveTo=false)
        {
            bool isAfterFrom = true;
            if (inclusiveFrom)
                isAfterFrom = dateTime >= from;
            else
                isAfterFrom = dateTime > from;

            bool isBeforeTo = true;
            if (inclusiveTo)
                isBeforeTo = dateTime <= to;
            else
                isBeforeTo = dateTime < to;

            return isAfterFrom && isBeforeTo;
        }
        public static bool IsPast(this System.DateTime dateTime,bool ignoreTime=false)
        {
            DateTime dateToCompare = dateTime;
            DateTime now = DateTime.Now;
            if (ignoreTime)
            {
                dateToCompare = dateToCompare.Date;
                now = now.Date;
            }

            return dateToCompare < now;
        }
        public static bool IsFuture(this System.DateTime dateTime, bool ignoreTime = false)
        {
            DateTime dateToCompare = dateTime;
            DateTime now = DateTime.Now;
            if (ignoreTime)
            {
                dateToCompare = dateToCompare.Date;
                now = now.Date;
            }

            return dateToCompare > now;
        }

        public static bool IsPastOrPresent(this System.DateTime dateTime, bool ignoreTime = true)
        {
            return IsPast(dateTime, ignoreTime) || IsPresent(dateTime);
        }
        public static bool IsPresent(this System.DateTime dateTime, bool ignoreTime = true)
        {
            DateTime dateToCompare = dateTime;
            DateTime now = DateTime.Now;
            if (ignoreTime)
            {
                dateToCompare = dateToCompare.Date;
                now = now.Date;
            }

            return dateToCompare == now;
        }


        /// <summary>
        /// Convert a gregorian date to UmAlQura (Hijri) Date 
        /// </summary>
        /// <param name="gregorian">
        /// Gregorian Date
        /// </param>
        /// <returns>
        /// Hijri date in the following format yyyy/M/d
        /// </returns>
        public static string ToHijriDate(this DateTime gregorian)
        {
            return gregorian.ToString("yyyy/M/d", new CultureInfo("ar-SA", false));

        }

        /// <summary>
        /// Convert a gregorian date to UmAlQura (Hijri) Date 
        /// </summary>
        /// <param name="gregorian">
        /// Gregorian Date
        /// </param>
        /// <para name="format">
        /// the result date format
        /// </para>
        /// <returns>
        /// Hijri date in the given format
        /// </returns>
        public static string ToHijriDate(this DateTime gregorian, string format)
        {
            return gregorian.ToString(format, new CultureInfo("ar-SA", false));
        }

        /// <summary>
        /// Add UmAlqura years to a given Datetime
        /// </summary>
        /// <param name="date">a given date to add UmAlqura years</param>
        /// <param name="years">Numbers of years to be added</param>
        /// <returns>Datetime after years added</returns>
        public static DateTime AddHijriYears(this DateTime date, int years)
        {
            UmAlQuraCalendar calendar = new UmAlQuraCalendar();
            return calendar.AddYears(date, years);
        }

        /// <summary>
        /// Add UmAlqura months to a given Datetime
        /// </summary>
        /// <param name="date">a given date to add UmAlqura months</param>
        /// <param name="years">Numbers of months to be added</param>
        /// <returns>Datetime after months added</returns>
        public static DateTime AddHijriMonths(this DateTime date, int months)
        {
            UmAlQuraCalendar calendar = new UmAlQuraCalendar();

            return calendar.AddMonths(date, months);
        }

        /// <summary>
        /// Add UmAlqura days to a given Datetime
        /// </summary>
        /// <param name="date">a given date to add UmAlqura days</param>
        /// <param name="years">Numbers of days to be added</param>
        /// <returns>Datetime after days added</returns>
        public static DateTime AddHijriDays(this DateTime date, int days)
        {
            UmAlQuraCalendar calendar = new UmAlQuraCalendar();
            return calendar.AddDays(date, days);
        }

    }
}
