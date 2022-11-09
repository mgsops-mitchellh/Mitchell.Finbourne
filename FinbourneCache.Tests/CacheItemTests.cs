using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinbourneCache.Tests
{
    [TestClass]
    public class CacheItemTests
    {
        [TestMethod]
        public void CanCreate()
        {
            new CacheItem<int>(0);
            new CacheItem<string>("Nineteen");
            new CacheItem<bool>(false);
        }

        [DataTestMethod]
        [DataRow("TESTHASHCODE")]
        [DataRow("TestHashCode")]
        [DataRow("TeStHaShCoDe")]
        public void GetHashCode_ShouldReturnSameAsStringHashCode(string cacheString)
        {
            var cacheItem = new CacheItem<string>(cacheString);
            Assert.AreEqual(cacheString.GetHashCode(), cacheItem.GetHashCode());
        }

        [DataTestMethod]
        [DataRow(5)]
        [DataRow(-5)]
        [DataRow(65536)]
        public void GetHashCode_ShouldReturnSameAsIntHashCode(int cacheInt)
        {
            var cacheItem = new CacheItem<int>(cacheInt);
            Assert.AreEqual(cacheInt.GetHashCode(), cacheItem.GetHashCode());
        }

        [TestMethod]
        public void ToString_ShouldReturnStringOfValue()
        {
            var intCacheItem = new CacheItem<int>(5);
            Assert.AreEqual("5", intCacheItem.ToString());

            var strCacheItem = new CacheItem<string>("Five");
            Assert.AreEqual("Five", strCacheItem.ToString());
        }

        [TestMethod]
        public void Equals_ShouldReturnFalseForNull()
        {
            var cacheItem = new CacheItem<int>(88);
            Assert.IsFalse(cacheItem.Equals(null));
        }

        [TestMethod]
        public void Equals_ShouldReturnTrueForRefEquals()
        {
            var cacheItem = new CacheItem<int>(88);
            var other = cacheItem;
            Assert.IsTrue(cacheItem.Equals(other));
        }

        [TestMethod]
        public void Equals_ShouldReturnTrueForObjectValueEquals()
        {
            var cacheItem = new CacheItem<int>(88);
            var other = new CacheItem<int>(88);
            Assert.IsTrue(cacheItem.Equals(other));
        }

        [TestMethod]
        public void Equals_ShouldReturnFalseForObjectValueNotEquals()
        {
            var cacheItem = new CacheItem<int>(88);
            var other = new CacheItem<int>(87);
            Assert.IsFalse(cacheItem.Equals(other));
        }

        [TestMethod]
        public void Equals_ShouldReturnTrueForNativeValueEquals()
        {
            var cacheItem = new CacheItem<int>(88);
            var other = 88;
            Assert.IsTrue(cacheItem.Equals(other));
        }

        [TestMethod]
        public void Equals_ShouldReturnFalseForNativeValueNotEquals()
        {
            var cacheItem = new CacheItem<int>(88);
            var other = 85;
            Assert.IsFalse(cacheItem.Equals(other));
        }
    }
}
