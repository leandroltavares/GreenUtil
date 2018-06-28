using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.String.StringUtilTest
{
    [TestClass]
    public class KeepCharsTest
    {
        [TestMethod]
        public void WhenSourceIsNullThenKeepCharsShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.KeepChars(null, new char[] { }));
        }

        [TestMethod]
        public void WhenCharArrayIsNullThenKeepCharsShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.KeepChars(string.Empty, null));
        }

        [DataTestMethod]
        [DataRow("green")]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow("This is a test string")]
        [DataRow("ABCDEFGHIJKLMNOPQRSTUWXYZ")]
        [DataRow("0123456789")]
        public void WhenCharArrayIsEmptyThenvCharsShouldReturnEmptyString(string source)
        {
            string replacedString = StringUtil.KeepChars(source, new char[] { });

            Assert.AreEqual(string.Empty, replacedString);
        }

        [DataTestMethod]
        [DataRow("green", "")]
        [DataRow("", "")]
        [DataRow("   ", "   ")]
        [DataRow("This is a test string", "    ")]
        [DataRow("ABCDEFGHIJKLMNOPQRSTUWXYZ", "A")]
        [DataRow("0123456789", "0")]
        [DataRow("1100A A0011", "00A A00")]
        public void WhenCharArrayContainsSomeCharThenRemoveCharsShouldReturnSameStringWithoutSomeChars(string source, string target)
        {
            string replacedString = StringUtil.KeepChars(source, new char[] { 'A', ' ', '0' });

            Assert.AreEqual(target, replacedString);
        }
    }
}