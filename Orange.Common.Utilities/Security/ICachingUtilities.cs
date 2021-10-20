namespace Orange.Common.Utilities
{
   public interface ICachingUtilities
    {
        object GetValueFromCache(string cacheKey);
        object GetValueFromCache(Strings.CachingKeys cacheKey);
        T GetCachedEaiData<T>(string cacheKey,string dial) ;
        void AddValueToCache(string cacheKey, object obj, double hours);
        void RemoveCache(string CacheKey);
    }
}
