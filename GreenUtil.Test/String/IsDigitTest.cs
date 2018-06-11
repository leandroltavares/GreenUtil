using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.String
{
    [TestClass]
    public class IsDigitTest
    {
        [TestMethod]
        public void WhenSourceStringIsNullThenIsDigitShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.IsDigit(null));
        }

        [DataTestMethod]
        [DataRow("123456789")]
        [DataRow("000000000")]
        [DataRow("1")]
        [DataRow("2")]
        [DataRow("3")]
        [DataRow("4")]
        [DataRow("5")]
        [DataRow("6")]
        [DataRow("7")]
        [DataRow("8")]
        [DataRow("9")]
        public void WhenSourceStringIsDigitOnlyThenIsDigitShouldReturnTrue(string source)
        {
            Assert.IsTrue(StringUtil.IsDigit(source));
        }

        [DataTestMethod]
        [DataRow("ABCDEFGHIJKLMNOPQRSTWUXYZ")]
        [DataRow("A0")]
        [DataRow("0A")]
        [DataRow("1234567890A")]
        [DataRow("")]
        [DataRow("           ")]
        [DataRow("²³")]
        [DataRow("@!@#!#!@#!#!@#!$%¨&*&%*(%!@#$!@#$@!")]
        [DataRow("@1@#!#2!@#!4#!@#!2$%¨33&*2151&%12*31(3%1!5@#665$767!87@978#09$089@!")]
        public void WhenSourceStringIsNotDigitOnlyThenIsDigitShouldReturnFalse(string source)
        {
            Assert.IsFalse(StringUtil.IsDigit(source));
        }
    }
}
