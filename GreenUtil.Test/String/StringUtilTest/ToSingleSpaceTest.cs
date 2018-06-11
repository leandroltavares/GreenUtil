using System;
using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenUtil.Test.String.StringUtilTest
{
    [TestClass]
    public class ToSingleSpaceTest
    {
        [TestMethod]
        public void WhenSourceStringIsNullThenToSingleSpaceTestShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.ToSingleSpace(null));
        }

        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("green", "green")]
        [DataRow("green ", "green ")]
        [DataRow(" green ", " green ")]
        [DataRow(" green", " green")]
        [DataRow(" g r e e n ", " g r e e n ")]
        [DataRow("  g  r  e  e  n  ", " g r e e n ")]
        [DataRow("   g   r   e   e   n   ", " g r e e n ")]
        [DataRow(" ", " ")]
        [DataRow("  ", " ")]
        [DataRow("            ", " ")]
        public void WhenSourceStringIsValidThenToSingleSpaceTestShouldReturnStringWithSingleSpace(string source, string expected)
        {
            Assert.AreEqual(expected, StringUtil.ToSingleSpace(source));
        }
    }
}
