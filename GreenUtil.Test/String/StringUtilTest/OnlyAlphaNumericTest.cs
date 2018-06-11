using System;
using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenUtil.Test.String.StringUtilTest
{
    /// <summary>
    /// Testes do método <see cref="StringUtil.OnlyAlphaNumeric"/>
    /// </summary>
    [TestClass]
    public class OnlyAlphanumericTest
    {
        [TestMethod]
        public void WhenNullStringThenShouldThrowArgumentNullException()
        {
            //Arrange
            string input = null;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.OnlyAlphaNumeric(input));
        }

        [TestMethod]
        public void WhenEmptyStringThenShouldReturnEmpty()
        {
            //Arrange
            string input = string.Empty;

            //Act
            string result = StringUtil.OnlyAlphaNumeric(input);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenNumericStringThenShouldReturnSameString()
        {
            //Arrange
            string input = "123456789";

            //Act
            string result = StringUtil.OnlyAlphaNumeric(input);

            //Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void WhenAlphanumericStringThenShouldReturnSameString()
        {
            //Arrange
            string input = "A1B2C3D4E5F6G7H8I9";

            //Act
            string result = StringUtil.OnlyAlphaNumeric(input);

            //Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void WhenOnlyAlphabeticStringThenShouldReturnSameString()
        {
            //Arrange
            string input = "ABCDEFGHIJKLMNOPQRSTUVWYXZ";

            //Act
            string result = StringUtil.OnlyAlphaNumeric(input);

            //Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void WhenSpecialCharactersStringThenShouldReturnEmptyString()
        {
            //Arrange
            string input = "@!#$%¨&*(){}+=-";

            //Act
            string result = StringUtil.OnlyAlphaNumeric(input);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

    }
}
