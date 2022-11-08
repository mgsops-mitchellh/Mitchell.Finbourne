namespace FinbourneCache
{
    public interface ICache<TKey, TValue>
    {
        public void Add(TKey key, TValue value);
        public TValue Get(TKey key);
    }
}