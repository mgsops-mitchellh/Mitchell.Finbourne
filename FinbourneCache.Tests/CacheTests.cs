using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;

namespace FinbourneCache.Tests
{
    [TestClass]
    public class CacheTests
    {
        #region Creation

        [TestMethod]
        public void CanCreateInstance()
        {
            // Some smoke tests to ensure the Cache can be created.
            _ = new Cache<int, int>();
            _ = new Cache<string, string>();
        }

        #endregion

        #region Adding / Reading basic

        [TestMethod]
        public void AddToCache_GivenItem_ItemAdded()
        {
            var cache = new Cache<int, int>();
            cache.Add(1, 2);
        }

        [TestMethod]
        public void AddToCache_GivenItem_CanAddAndRetrieve()
        {
            var cache = new Cache<int, int>();
            cache.Add(1, 2);
            cache.Get(1);
        }

        #endregion
    }
}