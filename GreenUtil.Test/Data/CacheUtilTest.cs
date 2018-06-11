
using GreenUtil.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace GreenUtil.Test.Data
{
    [TestClass]
    public class CacheUtilTest
    {

        [TestMethod]
        public void WhenCallbackIsNullThenGetOrSetShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => CacheUtil.GetOrSet("KEY", (Func<string>)null));
        }

        [TestMethod]
        public void WhenCachedFunctionIsCalledThenGetOrSetShouldReturnTheFirstCallValue()
        {
            Func<string> function = () => { return new Random((int)DateTime.Now.Ticks).Next().ToString(); };

            var firstCallValue = CacheUtil.GetOrSet("KEY_1", function);

            var secondCallValue = CacheUtil.GetOrSet("KEY_1", function);

            var thirdCallValue = CacheUtil.GetOrSet("KEY_1", function);

            Assert.AreEqual(firstCallValue, secondCallValue);
            Assert.AreEqual(firstCallValue, thirdCallValue);

        }

        [TestMethod]
        public void WhenCachedFunctionIsLongRunningThenGetOrSetShouldReturnCachedValueWithLowerTimes()
        {
            Func<string> function = () => { Thread.Sleep(1000); return new Random((int)DateTime.Now.Ticks).Next().ToString(); };

            var time1 = DateTime.Now;

            var firstCallValue = CacheUtil.GetOrSet("KEY_X", function);

            var time2 = DateTime.Now;

            var secondCallValue = CacheUtil.GetOrSet("KEY_X", function);

            var time3 = DateTime.Now;

            var thirdCallValue = CacheUtil.GetOrSet("KEY_X", function);

            var time4 = DateTime.Now;

            Assert.AreEqual(firstCallValue, secondCallValue);
            Assert.AreEqual(firstCallValue, thirdCallValue);

            Assert.IsTrue(time2 - time1 > time3 - time2);
            Assert.IsTrue(time2 - time1 > time4 - time3);
        }


        [TestMethod]
        public void WhenInvalidateAllCacheThenKeysCountShouldBeEqualToZero()
        {
            Func<string> function = () => { return new Random((int)DateTime.Now.Ticks).Next().ToString(); };

            CacheUtil.GetOrSet("KEY_Y", function);

            Assert.IsTrue(CacheUtil.Count > 0);

            CacheUtil.GetOrSet("KEY_Y", function);

            CacheUtil.InvalidateAll();

            Assert.IsTrue(CacheUtil.Count == 0);
        }

        [TestMethod]
        public void WhenInvalidateSimilarCacheThenKeysCountShouldBeEqualToZero()
        {
            Func<string> function = () => { return new Random((int)DateTime.Now.Ticks).Next().ToString(); };

            CacheUtil.GetOrSet("KEY_A", function);
            CacheUtil.GetOrSet("KEY_B", function);
            CacheUtil.GetOrSet("KEY_SIMILAR_A", function);
            CacheUtil.GetOrSet("KEY_SIMILAR_B", function);

            int keysCount = CacheUtil.Count;

            Assert.IsTrue(keysCount > 0);

            CacheUtil.InvalidateSimilar("SIMILAR");

            Assert.IsTrue(CacheUtil.Count > 0);
            Assert.IsTrue(CacheUtil.Count < keysCount);

        }

        [TestMethod]
        public void WhenSetExpirationTimeThenDefaultExpirationShouldBeNewValue()
        {
            Assert.AreEqual(10, CacheUtil.DefaultExpiration);

            CacheUtil.DefaultExpiration = 15;

            Assert.AreEqual(15, CacheUtil.DefaultExpiration);
        }
    }
}
