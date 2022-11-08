namespace FinbourneCache
{
    public interface ICache<TKey, TValue>
    {
        void Add(TKey key, TValue value);
        CacheItem<TValue> Get(TKey key);
    }
}