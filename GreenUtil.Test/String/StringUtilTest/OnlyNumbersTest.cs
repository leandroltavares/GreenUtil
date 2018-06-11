using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenUtil.String;

namespace GreenUtil.Test.String.StringUtilTest
{
    /// <summary>
    /// Testes do método <see cref="StringUtil.OnlyNumbers"/>
    /// </summary>
    [TestClass]
    public class OnlyNumbersTest
    {
        [TestMethod]
        public void WhenNullStringThenShouldThrowArgumentNullException()
        {
            //Arrange
            string input = null;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.OnlyNumbers(input));
        }

        [TestMethod]
        public void WhenEmptyStringThenShouldReturnEmpty()
        {
            //Arrange
            string input = string.Empty;

            //Act
            string result = StringUtil.OnlyNumbers(input);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenNumericStringThenShouldReturnSameNumbers()
        {
            //Arrange
            string input = "123456789";

            //Act
            string result = StringUtil.OnlyNumbers(input);

            //Assert
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void WhenAlphaNumericStringThenShouldReturnOnlyNumbers()
        {
            //Arrange
            string input = "A1B2C3D4E5F6G7H8I9";

            //Act
            string result = StringUtil.OnlyNumbers(input);

            //Assert
            Assert.AreEqual("123456789", result);
        }

        [TestMethod]
        public void WhenOnlyAlphbeticStringThenShouldReturnEmptyString()
        {
            //Arrange
            string input = "ABCDEFGHIJKLMNOPQRSTUVWYXZ";

            //Act
            string result = StringUtil.OnlyNumbers(input);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenSpecialCharactersStringThenShouldReturnEmptyString()
        {
            //Arrange
            string input = "@!#$%¨&*(){}+=-";

            //Act
            string result = StringUtil.OnlyNumbers(input);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

    }
}
