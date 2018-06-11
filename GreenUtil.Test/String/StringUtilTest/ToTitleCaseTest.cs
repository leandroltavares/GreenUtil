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
    public class ToTitleCaseTest
    {
        [TestMethod]
        public void WhenNullInstanceThenShouldThrowArgumentNullException()
        {
            //Arrange
            string instance = null;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.ToTitleCase(instance));

        }

        [TestMethod]
        public void WhenOnlySmallLettersThenShouldReturnNull()
        {
            //Arrange
            string instance = "leandro luciani tavares";

            //Act
            string result = StringUtil.ToTitleCase(instance);

            //Assert
            Assert.AreEqual("Leandro Luciani Tavares", result);
        }
    }
}
