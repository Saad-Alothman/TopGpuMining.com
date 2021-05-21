using System;
using System.Runtime.Caching;

namespace CreaDev.Framework.Core.Utils
{
    public static class Caching
    {
        public static T LoadChache<T>(string name) where T : class
        {

            return _cache[name] as T;
        }
        public static void SetCache<T>(string name, T objectToCache, TimeSpan? expirationDateTime = null) where T : class
        {
            DateTime expDateTime;
            if (!expirationDateTime.HasValue)
            {

                expDateTime = DateTime.Now.AddMinutes(30);
            }
            else
            {
                expDateTime = DateTime.Now.Add(expirationDateTime.Value);
            }
            DateTimeOffset dateTimeOffset = new DateTimeOffset(expDateTime);
            _cache.Set(name, objectToCache, dateTimeOffset);
        }

        public static bool HasKey(string key)
        {
            return _cache.Contains(key);
        }
        private static MemoryCache _cache = MemoryCache.Default;
    }
}
