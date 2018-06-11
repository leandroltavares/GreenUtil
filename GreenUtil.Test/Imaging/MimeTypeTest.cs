using System;
using System.Drawing;
using GreenUtil.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenUtil.Test.Imaging
{
    [TestClass]
    public class MimeTypeTest
    {

        [TestMethod]
        public void WhenImageIsNullThenMimeTypeShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.MimeType(null));
        }


        [TestMethod]
        public void WhenJPGImageThenMimeTypeShouldReturnCorrectMime()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/JPG.jpg");

            //Act
            var mimeType = ImageUtil.MimeType(image);

            //Assert
            Assert.AreEqual("image/jpeg", mimeType);
        }

        [TestMethod]
        public void WhenPNGImageThenMimeTypeShouldReturnCorrectMime()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/PNG.png");

            //Act
            var mimeType = ImageUtil.MimeType(image);

            //Assert
            Assert.AreEqual("image/png", mimeType);
        }

        [TestMethod]
        public void WhenGIFImageThenMimeTypeShouldReturnCorrectMime()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/GIF.gif");

            //Act
            var mimeType = ImageUtil.MimeType(image);

            //Assert
            Assert.AreEqual("image/gif", mimeType);
        }

        [TestMethod]
        public void WhenBMPImageThenMimeTypeShouldReturnCorrectMime()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/BMP.bmp");

            //Act
            var mimeType = ImageUtil.MimeType(image);

            //Assert
            Assert.AreEqual("image/bmp", mimeType);
        }

        [TestMethod]
        public void WhenEMFImageThenMimeTypeShouldReturnCorrectMime()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/EMF.emf");

            //Act
            var mimeType = ImageUtil.MimeType(image);

            //Assert
            Assert.AreEqual("image/emf", mimeType);
        }

        [TestMethod]
        public void WhenTIFFImageThenMimeTypeShouldReturnCorrectMime()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/TIFF.tiff");

            //Act
            var mimeType = ImageUtil.MimeType(image);

            //Assert
            Assert.AreEqual("image/tiff", mimeType);
        }


        [TestMethod]
        public void WhenICOImageThenMimeTypeShouldReturnCorrectMime()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/ICO.ico");

            //Act
            var mimeType = ImageUtil.MimeType(image);

            //Assert
            Assert.AreEqual("image/x-icon", mimeType);
        }

        [TestMethod]
        public void WhenWMFImageThenMimeTypeShouldReturnCorrectMime()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/WMF.wmf");

            //Act
            var mimeType = ImageUtil.MimeType(image);

            //Assert
            Assert.AreEqual("image/wmf", mimeType);
        }
    }
}
