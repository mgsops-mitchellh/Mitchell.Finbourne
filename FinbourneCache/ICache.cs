namespace FinbourneCache
{
    public interface ICache<TKey, TValue>
    {
        void Add(TKey key, TValue value);
        void Add(TKey key, TValue value, out CacheItem<TValue> removedItem);
        CacheItem<TValue> Get(TKey key);
    }
}