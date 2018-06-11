using System;
using System.Diagnostics;
using GreenUtil.Performance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenUtil.Test.Performance
{
    [TestClass]
    public class PerformanceUtilTest
    {
        [TestMethod]
        public void WhenActionIsNullThenShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => PerformanceUtil.MeasureTime(null, out TimeSpan time));
        }

        [TestMethod]
        public void WhenActionThrowsExceptionThenShouldThrowInnerException()
        {
            Assert.ThrowsException<NotImplementedException>(() => PerformanceUtil.MeasureTime(() => { throw new NotImplementedException(); }, out TimeSpan time));
        }

        [TestMethod]
        public void WhenActionIsNotNullThenElapsedTimeShouldNotBeZero()
        {
            PerformanceUtil.MeasureTime(() => { int a = 1; a++; Debug.Write("Passou aqui!"); }, out TimeSpan time);

            Assert.AreNotEqual(0, time.Ticks);
        }
    }
}
