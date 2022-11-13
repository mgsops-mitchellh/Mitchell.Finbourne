using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var cacheItem = cache.Get(1);
            Assert.AreEqual(2, cacheItem.Value);
        }

        [TestMethod]
        public void Get_ItemDoesntExist_ShouldReturnNull()
        {
            var cache = new Cache<int, int>();
            var item = cache.Get(1);
            Assert.IsNull(item);
        }

        #endregion

        #region Adding / Reading with capacity limits

        [TestMethod]
        public void AddItemsUnderCapacity_CanAddAndRead()
        {
            var cache = new Cache<int, int>(2);
            cache.Add(1, 1);
            cache.Add(2, 2);
            Assert.AreEqual(cache.Get(1).Value, 1);
            Assert.AreEqual(cache.Get(2).Value, 2);

        }

        [TestMethod]
        public void AddItemsOverCapacity_ShouldReturnNullForLRUItem()
        {
            var cache = new Cache<int, int>(2);
            cache.Add(1, 1);
            cache.Add(2, 2);
            cache.Add(3, 3);
            Assert.IsNull(cache.Get(1));
            Assert.AreEqual(cache.Get(2).Value, 2);
            Assert.AreEqual(cache.Get(3).Value, 3);
        }

        [TestMethod]
        public void AddItemsUnderCapacityWithExpiredItem_ShouldReturnNull()
        {
            var cache = new Cache<int, int>(2);
            cache.Add(1, 1, out var removedItem1);
            cache.Add(2, 2, out var removedItem2);

            Assert.IsNull(removedItem1);
            Assert.IsNull(removedItem2);
        }

        [TestMethod]
        public void AddItemsOverCapacityWithExpiredItem_ShouldReturnRemovedItem()
        {
            var cache = new Cache<int, int>(2);
            cache.Add(1, 1);
            cache.Add(2, 2);
            cache.Add(3, 3, out var removedItem);

            Assert.AreEqual(removedItem.Value, 1);
        }

        #endregion
    }
}