using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.String.StringUtilTest
{
    /// <summary>
    /// Summary description for SliceTest
    /// </summary>
    [TestClass]
    public class SliceTest
    {
        [TestMethod]
        public void WhenNullStringThenShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.Slice(null, 0, 100));
        }

        [DataTestMethod]
        [DataRow("0123456789_", 100, 100)]
        [DataRow("0123456789_", -100, -100)]
        [DataRow("0123456789_", 1, 100)]
        [DataRow("0123456789_", -100, 1)]
        public void WhenIndexArgumentIsOutOfRangeThenShouldThrowArgumentOutOfRangeException(string source, int start, int end)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => StringUtil.Slice(source, start, end));
        }

        [DataTestMethod]
        [DataRow("greenconcept", 0, 1, "g")]
        [DataRow("Peaceful", 1, 4, "eac")]
        [DataRow("The morning is upon us.", 3, -2, " morning is upon u")]
        [DataRow("0123456789_", 0, 1, "0")]
        [DataRow("0123456789_", 0, 2, "01")]
        [DataRow("0123456789_", 1, 2, "1")]
        [DataRow("0123456789_", 8, 11, "89_")]
        [DataRow("0123456789_", 0, 0, "")]
        public void WhenStringIsNotNullAndIndexesAreValidStringThenShouldReturnSlicedString(string source, int start, int end, string expectedSliced)
        {
            string sliced = StringUtil.Slice(source, start, end);

            Assert.AreEqual(expectedSliced, sliced);
        }

    }
}
