namespace FinbourneCache
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        private Dictionary<TKey, CacheItem<TValue>> _cacheItems = new Dictionary<TKey, CacheItem<TValue>>();
        private LinkedList<TKey> _lruTracker = new LinkedList<TKey>();
        private object _lockOperation = new object();
        private readonly int _capacity;

        public Cache(int capacity = 1024)
        {
            _capacity = capacity;
        }

        public void Add(TKey key, TValue value)
        {
            this.Add(key, value, out var _);
        }

        public void Add(TKey key, TValue value, out CacheItem<TValue> removedItem)
        {
            removedItem = null;

            if (key == null) return;

            var cacheItem = new CacheItem<TValue>(value);
            lock (_lockOperation)
            {
                try
                {
                    if (!(_cacheItems.TryGetValue(key, out var existingItem) && existingItem.Equals(cacheItem)))
                    {
                        _cacheItems.Add(key, cacheItem);
                    }
                    UpdateLruList(key);

                    if (_lruTracker.Count > _capacity)
                    {
                        var keyToRemove = _lruTracker.First.Value;
                        _lruTracker.RemoveFirst();
                        removedItem = _cacheItems[keyToRemove];
                        _cacheItems.Remove(keyToRemove);
                    }
                }
                catch (Exception e)
                {
                    // Logging and error handling
                }
            }
        }

        public CacheItem<TValue> Get(TKey key)
        {
            lock (_lockOperation)
            {
                try
                {
                    if (_cacheItems.TryGetValue(key, out var cacheItem))
                        UpdateLruList(key);

                    return cacheItem;
                }
                catch (Exception e)
                {
                    // Logging and error handling
                    return null;
                }
            }
        }

        private void UpdateLruList(TKey key)
        {
            _lruTracker.Remove(key);
            _lruTracker.AddLast(key);
        }
    }
}