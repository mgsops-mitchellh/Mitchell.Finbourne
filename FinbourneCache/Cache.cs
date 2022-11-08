using System.Data.SqlTypes;

namespace FinbourneCache
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        private Dictionary<TKey, CacheItem<TValue>> CacheItems = new Dictionary<TKey, CacheItem<TValue>>();
        private LinkedList<TKey> lruTracker = new LinkedList<TKey>();

        private readonly int _capacity;

        public Cache(int capacity = 1024)
        {
            _capacity = capacity;
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null) return;

            var cacheItem = new CacheItem<TValue>(value);
            if (CacheItems.TryGetValue(key, out var existingItem))
            {
                if (!existingItem.Equals(cacheItem)) CacheItems[key] = cacheItem;
                lruTracker.Remove(key);
            }
            else
            {
                CacheItems.Add(key, cacheItem);
            }
            lruTracker.AddLast(key);

            if (lruTracker.Count > _capacity)
            {
                var keyToRemove = lruTracker.First.Value;
                lruTracker.RemoveFirst();
                CacheItems.Remove(keyToRemove);
            }
        }

        public CacheItem<TValue> Get(TKey key)
        {
            CacheItems.TryGetValue(key, out var cacheItem);
            return cacheItem;
        }
    }
}