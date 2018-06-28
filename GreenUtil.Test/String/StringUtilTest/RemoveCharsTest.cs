using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.String.StringUtilTest
{
    [TestClass]
    public class RemoveCharsTest
    {
        [TestMethod]
        public void WhenSourceIsNullThenRemoveCharsShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.RemoveChars(null, new char[] { }));
        }

        [TestMethod]
        public void WhenCharArrayIsNullThenRemoveCharsShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.RemoveChars(string.Empty, null));
        }

        [DataTestMethod]
        [DataRow("green")]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("This is a test string")]
        [DataRow("ABCDEFGHIJKLMNOPQRSTUWXYZ")]
        [DataRow("0123456789")]
        public void WhenCharArrayIsEmptyThenRemoveCharsShouldReturnSameString(string source)
        {
            string replacedString = StringUtil.RemoveChars(source, new char[] { });

            Assert.AreEqual(source, replacedString);
        }

        [DataTestMethod]
        [DataRow("green", "green")]
        [DataRow("", "")]
        [DataRow("   ", "")]
        [DataRow("This is a test string", "Thisisateststring")]
        [DataRow("ABCDEFGHIJKLMNOPQRSTUWXYZ", "BCDEFGHIJKLMNOPQRSTUWXYZ")]
        [DataRow("0123456789", "123456789")]
        [DataRow("100A A001", "11")]
        public void WhenCharArrayContainsSomeCharThenRemoveCharsShouldReturnSameStringWithoutSomeChars(string source, string target)
        {
            string replacedString = StringUtil.RemoveChars(source, new char[] { 'A', ' ', '0' });

            Assert.AreEqual(target, replacedString);
        }
    }
}
