using System;
using System.Web;
using System.Web.Caching;

namespace Orange.Common.Utilities
{
    class CachingUtilities : ICachingUtilities
    {
        private readonly ILogger _logger;
        public CachingUtilities(ILogger logger)
        {
            _logger = logger;
        }
        public object GetValueFromCache(string cacheKey)
        {
            if (HttpContext.Current == null || HttpContext.Current.Cache == null)
                return null;

            return HttpContext.Current.Cache[cacheKey] == null ? null : HttpContext.Current.Cache[cacheKey];
        }
        public object GetValueFromCache(Strings.CachingKeys cacheKey)
        {
            if (HttpContext.Current == null || HttpContext.Current.Cache == null)
                return null;

            return HttpContext.Current.Cache[cacheKey.ToString()] == null
                ? null
                : HttpContext.Current.Cache[cacheKey.ToString()];
        }
        public T GetCachedEaiData<T>(string cacheKey, string dial)
        {
            if (dial.StartsWith(Strings.Numbers.Zero))
                cacheKey += dial;
            else
                cacheKey += string.Concat(Strings.Numbers.Zero, dial);
            var cachedData = GetValueFromCache(cacheKey);
            if (cachedData != null)
                return (T)cachedData;
            return default(T);
        }
        public void AddValueToCache(string cacheKey, object obj, double hours)
        {
            if (HttpContext.Current == null || HttpContext.Current.Cache == null)
                return;
            var cache = HttpContext.Current.Cache;
            lock (cache)
            {
                HttpContext.Current.Cache.Add(cacheKey, obj, null, DateTime.Now.AddHours(hours), Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);
            }
        }
        public void RemoveCache(string CacheKey)
        {
            System.Web.HttpContext.Current.Cache.Remove(CacheKey);
        }
    }
}
