using System;
using System.Collections.Generic;
using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenUtil.Test.String.StringUtilTest
{
    [TestClass]
    public class SplitPagesTest
    {
        [TestMethod]
        public void WhenNullThenShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.SplitPages(null));
        }

        [TestMethod]
        public void WhenSinglePageShouldReturnSamePage()
        {
            //Arrange
            string page = "Single Page";

            //Act
            List<string> splittedPages = StringUtil.SplitPages(page);

            //Assert
            Assert.AreEqual(1, splittedPages.Count);
            Assert.AreEqual(page, splittedPages[0]);

        }

        [TestMethod]
        public void WhenCouplePagesShouldReturnBothPages()
        {
            //Arrange
            string page = "First Page\fSecond Page";

            //Act
            List<string> splittedPages = StringUtil.SplitPages(page);

            //Assert
            Assert.AreEqual(2, splittedPages.Count);
            Assert.AreEqual("First Page", splittedPages[0]);
            Assert.AreEqual("Second Page", splittedPages[1]);
        }

        [TestMethod]
        public void WhenSinglePageWithPageBreakAtEndShouldReturnTwoPages()
        {
            //Arrange
            string page = "First Page\f";

            //Act
            List<string> splittedPages = StringUtil.SplitPages(page);

            //Assert
            Assert.AreEqual(2, splittedPages.Count);
            Assert.AreEqual("First Page", splittedPages[0]);
            Assert.AreEqual(string.Empty, splittedPages[1]);

        }

        [TestMethod]
        public void WhenSinglePageWithPageBreakAtBeginningAndEndShouldReturnThreePages()
        {
            //Arrange
            string page = "\fFirst Page\f";

            //Act
            List<string> splittedPages = StringUtil.SplitPages(page);

            //Assert
            Assert.AreEqual(3, splittedPages.Count);
            Assert.AreEqual(string.Empty, splittedPages[0]);
            Assert.AreEqual("First Page", splittedPages[1]);
            Assert.AreEqual(string.Empty, splittedPages[2]);
        }

        [TestMethod]
        public void WhenSinglePageWithPageBreakAtEndAndRemoveEmptyEntriesShouldReturnSinglePage()
        {
            //Arrange
            string page = "First Page\f";

            //Act
            List<string> splittedPages = StringUtil.SplitPages(page, StringSplitOptions.RemoveEmptyEntries);

            //Assert
            Assert.AreEqual(1, splittedPages.Count);
            Assert.AreEqual("First Page", splittedPages[0]);
        }

        [TestMethod]
        public void WhenSinglePageWithPageBreakAtBeginningAndEndAndRemoveEmptyEntrieShouldReturnSinglePage()
        {
            //Arrange
            string page = "\fFirst Page\f";

            //Act
            List<string> splittedPages = StringUtil.SplitPages(page, StringSplitOptions.RemoveEmptyEntries);

            //Assert
            Assert.AreEqual(1, splittedPages.Count);
            Assert.AreEqual("First Page", splittedPages[0]);
        }
    }
}
