using GreenUtil.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace GreenUtil.Test.IO
{
    [TestClass]
    public class DirectoryUtilTest
    {
        [TestMethod]
        public void WhenDirectoryWithFilesAndRegularExpressionThenShouldReturnFilesAndMatchesDetailsAboutEachFile()
        {
            //Arrange
            var resultado = DirectoryUtil.GetFilesByRegex("Dummy", "(?<id>[0-9]+)_(?<text>[A-Z0-9a-z]+)", SearchOption.AllDirectories);

            //Assert
            Assert.AreEqual(4, resultado.Count());
            Assert.AreEqual("01", resultado.ElementAt(0).Item2.Groups["id"].Value);
            Assert.AreEqual("SAMPLE1", resultado.ElementAt(0).Item2.Groups["text"].Value);

            Assert.AreEqual("02", resultado.ElementAt(1).Item2.Groups["id"].Value);
            Assert.AreEqual("SAMPLE2", resultado.ElementAt(1).Item2.Groups["text"].Value);

            Assert.AreEqual("03", resultado.ElementAt(2).Item2.Groups["id"].Value);
            Assert.AreEqual("SAMPLE3", resultado.ElementAt(2).Item2.Groups["text"].Value);

            Assert.AreEqual("04", resultado.ElementAt(3).Item2.Groups["id"].Value);
            Assert.AreEqual("SAMPLE4", resultado.ElementAt(3).Item2.Groups["text"].Value);
        }

        [TestMethod]
        public void WhenDirectoryIsNullThenShouldThrowException()
        { 
            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => DirectoryUtil.GetFilesByRegex(null, ".txt", SearchOption.AllDirectories).ToList());
        }

        [TestMethod]
        public void WhenPatternIsNullThenShouldThrowException()
        {
            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => { DirectoryUtil.GetFilesByRegex("Dummy", null, SearchOption.AllDirectories).ToList(); });
        }

        [TestMethod]
        public void WhenDirectoryWithFilesAndFileExtensionsThenShouldReturnFilesAndMatchesDetailsAboutEachFile()
        {
            //Arrange
            var resultado = DirectoryUtil.GetFilesByRegex("Dummy", ".txt", SearchOption.AllDirectories);

            //Assert
            Assert.IsTrue(resultado.Count() >= 8);
        }
    }
}
