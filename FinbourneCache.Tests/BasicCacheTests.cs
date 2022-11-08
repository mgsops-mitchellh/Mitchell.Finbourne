using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;

namespace FinbourneCache.Tests
{
    [TestClass]
    public class BasicCacheTests
    {
        [TestMethod]
        public void CanCreateInstance()
        {
            // Some smoke tests to ensure the Cache can be created.
            _ = new Cache<int, int>();
            _ = new Cache<string, string>();
        }
    }
}