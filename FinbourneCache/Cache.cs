using System.Data.SqlTypes;

namespace FinbourneCache
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        private Dictionary<TKey, CacheItem<TValue>> CacheItems = new Dictionary<TKey, CacheItem<TValue>>();

        public void Add(TKey key, TValue value)
        {
            var cacheItem = new CacheItem<TValue>(value);
            if (CacheItems.ContainsKey(key))
            {
                CacheItems[key] = cacheItem;
            }
            else
            {
                CacheItems.Add(key, cacheItem);
            }
        }

        public CacheItem<TValue> Get(TKey key)
        {
            CacheItems.TryGetValue(key, out var cacheItem);
            return cacheItem;
        }
    }
}