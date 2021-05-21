using System.Text.RegularExpressions;

namespace CreaDev.Framework.Core.Utils
{
    public static class Casting
    {
        public static T Convert<T>(object theObject) where T : class
        {
            Guard.AgainstType<T>(theObject);
            return theObject as T ;
        }


    }
    public static class Url
    {
        public static string UrlSlug(this string phrase)
        {
            string str = phrase.ToLower();
            bool containsArabic = Regex.IsMatch(phrase, @"[\u0600-\u06FF]");
            if (!containsArabic)
            {
                str = phrase.RemoveAccent().ToLower();
            }
            
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\u0600-\u06FF\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
