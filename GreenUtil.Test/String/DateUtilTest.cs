using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.String
{
    /// <summary>
    /// Testes da classe <see cref="DateUtil"/>
    /// </summary>
    [TestClass]
    public class DateUtilTest
    {

        [TestMethod]
        public void WhenDateIsMinValueAndNoFormatIsSpecifiedThenShouldReturnEmptyString()
        {
            //Arrange
            DateTime date = DateTime.MinValue;
            string format = string.Empty;

            //Act
            string result = DateUtil.ToStringIfNotDefault(date, format);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenDateIsMinValueAndFormatIsSpecifiedThenShouldReturnEmptyString()
        {
            //Arrange
            DateTime date = DateTime.MinValue;
            string format = "dd/MM/yyyy HH:mm:ss";

            //Act
            string result = DateUtil.ToStringIfNotDefault(date, format);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenDateIsCurrentAndNoFormatIsSpecifiedThenShouldReturnEmptyString()
        {
            //Arrange
            DateTime date = DateTime.Now;
            string format = string.Empty;

            //Act
            string result = DateUtil.ToStringIfNotDefault(date, format);

            //Assert
            Assert.AreEqual(date.ToString(), result);
        }

        [TestMethod]
        public void WhenDateIsCurrentAndFormatIsSpecifiedThenShouldReturnEmptyString()
        {
            //Arrange
            DateTime date = DateTime.Now;
            string format = "dd/MM/yyyy HH:mm:ss";

            //Act
            string result = DateUtil.ToStringIfNotDefault(date, format);

            //Assert
            Assert.AreEqual(date.ToString(format), result);
        }
    }
}
