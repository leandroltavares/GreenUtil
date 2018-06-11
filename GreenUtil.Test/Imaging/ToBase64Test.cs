using GreenUtil.Imaging;
using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace GreenUtil.Test.Imaging
{
    [TestClass]
    public class ToBase64Test
    {
        [TestMethod]
        public void WhenImageIsNullThenToBase64ShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.ToBase64(null));
        }

        [TestMethod]
        public void WhenMemoryImageIsValidThenToBase64ShouldReturnBase64String()
        {
            //Arrange
            var image = new Bitmap(100, 100);

            using (Graphics g = Graphics.FromImage(image))
                g.FillRectangle(Brushes.Green, 0, 0, 100, 100);

            //Act
            var base64 = ImageUtil.ToBase64(image);

            //Assert
            Assert.AreNotEqual(string.Empty, base64);
            Assert.IsTrue(base64.StartsWith("data:image/bmp;base64,"));
            Assert.IsTrue(StringUtil.IsBase64(base64.Replace("data:image/bmp;base64,", string.Empty)));

        }

        [TestMethod]
        public void WhenPNGImageIsValidThenToBase64ShouldReturnBase64String()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/PNG.png");

            //Act
            var base64 = ImageUtil.ToBase64(image);

            //Assert
            Assert.AreNotEqual(string.Empty, base64);
            Assert.IsTrue(base64.StartsWith("data:image/png;base64,"));
            Assert.IsTrue(StringUtil.IsBase64(base64.Replace("data:image/png;base64,", string.Empty)));
        }

        [TestMethod]
        public void WhenBMPImageIsValidThenToBase64ShouldReturnBase64String()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/BMP.bmp");

            //Act
            var base64 = ImageUtil.ToBase64(image);

            //Assert
            Assert.AreNotEqual(string.Empty, base64);
            Assert.IsTrue(base64.StartsWith("data:image/bmp;base64,"));
            Assert.IsTrue(StringUtil.IsBase64(base64.Replace("data:image/bmp;base64,", string.Empty)));
        }

        [TestMethod]
        public void WhenJPGImageIsValidThenToBase64ShouldReturnBase64String()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/JPG.jpg");

            //Act
            var base64 = ImageUtil.ToBase64(image);

            //Assert
            Assert.AreNotEqual(string.Empty, base64);
            Assert.IsTrue(base64.StartsWith("data:image/jpeg;base64,"));
            Assert.IsTrue(StringUtil.IsBase64(base64.Replace("data:image/jpeg;base64,", string.Empty)));

        }

        [TestMethod]
        public void WhenGIFImageIsValidThenToBase64ShouldReturnBase64String()
        {
            //Arrange
            var image = Image.FromFile("Dummy/Images/GIF.gif");

            //Act
            var base64 = ImageUtil.ToBase64(image);

            //Assert
            Assert.AreNotEqual(string.Empty, base64);
            Assert.IsTrue(base64.StartsWith("data:image/gif;base64,"));
            Assert.IsTrue(StringUtil.IsBase64(base64.Replace("data:image/gif;base64,", string.Empty)));

        }

        //[TestMethod]
        //public void WhenEMFImageIsValidThenToBase64ShouldReturnBase64String()
        //{
        //    //Arrange
        //    var image = Image.FromFile("Dummy/Images/EMF.emf");

        //    //Act
        //    var base64 = ImageUtil.ToBase64(image);

        //    //Assert
        //    Assert.AreNotEqual(string.Empty, base64);
        //    Assert.IsTrue(base64.StartsWith("data:image/emf;base64,"));
        //    Assert.IsTrue(StringUtil.IsBase64(base64.Replace("data:image/emf;base64,", string.Empty)));

        //}
    }
}
