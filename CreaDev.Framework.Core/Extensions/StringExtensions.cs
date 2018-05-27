using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Resources;

namespace CreaDev.Framework.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum) throw new ArgumentException(String.Format("Type '{0}' is not Enum", type));

            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
                return "";

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes.Length > 0)
            {
                var attribute = (DisplayAttribute)attributes[0];
                return attribute.GetName();
            }
            else
                return value.ToString();
        }

    }

    public static class DictionaryExtensions
    {
        public static void AddOrAppend<TKey>(this Dictionary<TKey, object> dictionary, TKey key, object value)

        {
            if (dictionary.ContainsKey(key))
            {
                string orginal = dictionary[key].ToString();
                dictionary[key] = (orginal +" "+value.ToString());
            }
            else
            {
                dictionary.Add(key,value);
            }
        }
    }
    public static class StringExtensions
    {

        public static string LowerFirst(this string text, bool foreachDot = true)
        {
            if (text == null)
                return text;
            string result = string.Empty;
            if (foreachDot)
            {
                var splitted = text.Split('.');
                foreach (var word in splitted)
                {
                    result += LowerFirst(word,false) + '.';
                }
                int indexLastDot = result.LastIndexOf('.');
                if (indexLastDot > -1)
                {
                    result=result.Remove(indexLastDot);

                }
                return result;
            }

            result = Char.ToLowerInvariant(text[0]) + text.Substring(1);

            return result;
        }

        public static bool ContainsIgnoreCase(this IEnumerable<string> stringList, string stringToFind)
        {
            return stringList.Any(str => str.ToLower() == stringToFind.ToLower());
        }
        public static string Brief(this string msg, int max)
        {
            if (msg != null && msg.Length > max)
                return msg.Substring(0, max) + " ...";

            return msg;
        }

        public static string ToCsv(this IEnumerable<string> stringList)
        {
            string result = string.Empty;

            if (stringList == null)
                return result;

            for (int i = 0; i < stringList.Count(); i++)
            {
                var item = stringList.ElementAt(i);
                result += item;
                if (i < stringList.Count() - 1)
                {
                    
                    result += Common.Comma+ " ";
                }
            }
            return result;
        }
        public static string ToCsv(params string[] stringList)
        {
            string result = string.Empty;

            if (stringList == null)
                return result;

            for (int i = 0; i < stringList.Count(); i++)
            {
                var item = stringList.ElementAt(i);
                result += item;
                if (i < stringList.Count() - 1)
                {

                    result += Common.Comma + " ";
                }
            }
            return result;
        }
        public static List<string> ToCsv(this string csvString)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrWhiteSpace(csvString))
                return result;

            result.AddRange(csvString.Split(','));

            return result;
        }

        public static string StripHtml(this string text)
        {
            string result = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
            result = result.Replace("&nbsp;", " ");
            return result;
        }

        public static string ToCamelCase(this string the_string)
        {

            // If there are 0 or 1 characters, just return the string.
            if (the_string == null || the_string.Length < 2)
                return the_string;
            the_string = the_string.ToProperCase();
            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }
        public static string ToProperCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Start with the first character.
            string result = the_string.Substring(0, 1).ToUpper();

            // Add the remaining characters.
            for (int i = 1; i < the_string.Length; i++)
            {
                if (char.IsUpper(the_string[i])) result += " ";
                result += the_string[i];
            }

            return result;
        }
        public static string ToProperCaseTowWords(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Start with the first character.
            string result = the_string.Substring(0, 1).ToUpper();

            // Add the remaining characters.
            for (int i = 1; i < the_string.Length; i++)
            {
                //if (char.IsUpper(the_string[i])) result += " ";
                result += the_string[i];
            }

            return result;
        }
        public static string ToPascalCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();
            the_string = the_string.ToProperCase();

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }
    }
}
