namespace FinbourneCache
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue> where TKey : IComparable, IComparable<TKey>
    {
        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public TValue Get(TKey key)
        {
            throw new NotImplementedException();
        }
    }
}