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
    public class LevenshteinTest
    {
        [TestMethod]
        public void WhenFirstStringIsNullThenShouldThrowArgumentNullException()
        {
            string first = null;
            string second = "greenconcept";

            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.LevenshteinDistance(first, second));
        }

        [TestMethod]
        public void WhenSecondStringIsNullThenShouldThrowArgumentNullException()
        {
            string first = "greenconcept";
            string second = null;

            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.LevenshteinDistance(first, second));
        }

        [TestMethod]
        public void WhenFirstStringIsEmptyThenShouldReturnSecondStringLength()
        {
            string first = string.Empty;
            string second = "greenconcept";

            int distance = StringUtil.LevenshteinDistance(first, second);

            Assert.AreEqual(second.Length, distance);
        }

        [TestMethod]
        public void WhenSecondStringIsEmptyThenShouldReturnFirstStringLength()
        {
            string first = "greenconcept";
            string second = string.Empty;

            int distance = StringUtil.LevenshteinDistance(first, second);

            Assert.AreEqual(first.Length, distance);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("casa", "casa", 0)]
        [DataRow("casa", "asa", 1)]
        [DataRow("asa", "casa", 1)]
        [DataRow("asa", "casas", 2)]
        [DataRow("casas", "asa", 2)]
        [DataRow("casa", "caso", 1)]
        [DataRow("caso", "casa", 1)]
        public void WhenStringIsNotEmptyThenShouldReturnLevenshteinLength(string first, string second, int expectedDistance)
        {
            int distance = StringUtil.LevenshteinDistance(first, second);

            Assert.AreEqual(expectedDistance, distance);
        }
    }
}
