using System.Data.SqlTypes;

namespace FinbourneCache
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        private Dictionary<TKey, CacheItem<TValue>> _cacheItems = new Dictionary<TKey, CacheItem<TValue>>();
        private LinkedList<TKey> lruTracker = new LinkedList<TKey>();

        private readonly int _capacity;

        public Cache(int capacity = 1024)
        {
            _capacity = capacity;
        }

        public void Add(TKey key, TValue value)
        {
            this.Add(key, value, out var _);
        }

        public void Add(TKey key, TValue value, out CacheItem<TValue> expiredItem)
        {
            expiredItem = null;

            if (key == null) return;

            var cacheItem = new CacheItem<TValue>(value);
            if (_cacheItems.TryGetValue(key, out var existingItem))
            {
                if (!existingItem.Equals(cacheItem)) _cacheItems[key] = cacheItem;
                lruTracker.Remove(key);
            }
            else
            {
                _cacheItems.Add(key, cacheItem);
            }
            lruTracker.AddLast(key);

            if (lruTracker.Count > _capacity)
            {
                var keyToRemove = lruTracker.First.Value;
                lruTracker.RemoveFirst();
                expiredItem = _cacheItems[keyToRemove];
                _cacheItems.Remove(keyToRemove);
            }
        }

        public CacheItem<TValue> Get(TKey key)
        {
            _cacheItems.TryGetValue(key, out var cacheItem);
            return cacheItem;
        }
    }
}