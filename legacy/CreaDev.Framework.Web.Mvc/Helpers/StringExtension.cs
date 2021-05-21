using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CreaDev.Framework.Web.Mvc.Helpers
{
    public static class StringExtension
    {


        public static List<string> ListFromCsv(this string csvString, bool trim = true)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrWhiteSpace(csvString))
                return result;
            result.AddRange(csvString.Split(',').Select(item => trim ? item.Trim() : item));

            return result;
        }

        public static string Brief(this string msg, int max)
        {
            if (msg != null && msg.Length > max)
                return msg.Substring(0, max) + " ...";

            return msg;
        }
        public static string WordBrief(this string msg, int count)
        {
            if (string.IsNullOrWhiteSpace(msg)) return msg;

            msg = msg.CleanString();
            string result = string.Empty;
            string[] words = msg.Split(' ');
            foreach (var word in words.Take(count))
            {
                result += word.Replace(" ", "") + " ";
            }

            return result;
        }
        public static string AfterWordBrief(this string msg, int count)
        {
            if (string.IsNullOrWhiteSpace(msg)) return msg;

            msg = msg.CleanString();
            string result = string.Empty;
            string[] words = msg.Split(' ');
            foreach (var word in words.Skip(count))
            {
                result += word.Replace(" ", "") + " ";
            }

            return result;
        }

        private static string CleanString(this string theString)
        {

            theString = theString.Trim();
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            theString = regex.Replace(theString, " ");
            return theString;
        }
    }
}
