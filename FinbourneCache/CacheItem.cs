using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinbourneCache
{
    public class CacheItem<T>
    {
        public readonly T Value;

        public CacheItem(T value)
        {
            this.Value = value;
        }

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object? other)
        {
            // Null case
            if (other == null) return false;
            // Reference case
            return other == this ||
                // Value cases
                (other is CacheItem<T> && ((CacheItem<T>)other).Value.Equals(this.Value) ||
                ((other is T) && other.Equals(this.Value)));
        }
    }
}
