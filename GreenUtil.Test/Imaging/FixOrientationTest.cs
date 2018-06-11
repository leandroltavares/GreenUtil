using GreenUtil.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace GreenUtil.Test.Imaging
{
    [TestClass]
    public class FixOrientationTest
    {

        [TestMethod]
        public void WhenSourceIsNullThenFixOrientationShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.FixOrientation(null));
        }

        [TestMethod]
        public void WhenImageIsWithNormalOrientationDoNothing()
        {
            var image = Image.FromFile("Dummy/Images/JPG.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldWidth, newWidth);
            Assert.AreEqual(oldHeight, newHeight);

        }

        [TestMethod]
        public void WhenImageIsNoneOrientationDoNothing()
        {
            var image = Image.FromFile("Dummy/Images/JPG_NONE_ORIENTATION.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldWidth, newWidth);
            Assert.AreEqual(oldHeight, newHeight);

        }

        [TestMethod]
        public void WhenImageIsMirrorHorizontalOrientationThenShoulfdFixOrientation()
        {
            var image = Image.FromFile("Dummy/Images/JPG_MIRROR_HORIZONTAL.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldWidth, newWidth);
            Assert.AreEqual(oldHeight, newHeight);

        }

        [TestMethod]
        public void WhenImageIsMirrorVerticalOrientationThenShoulfdFixOrientation()
        {
            var image = Image.FromFile("Dummy/Images/JPG_MIRROR_VERTICAL.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldWidth, newWidth);
            Assert.AreEqual(oldHeight, newHeight);

        }

        [TestMethod]
        public void WhenImageIsRotate180OrientationThenShoulfdFixOrientation()
        {
            var image = Image.FromFile("Dummy/Images/JPG_ROTATE_180.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldWidth, newWidth);
            Assert.AreEqual(oldHeight, newHeight);

        }

        [TestMethod]
        public void WhenImageIsRotate270CWOrientationThenShoulfdFixOrientation()
        {
            var image = Image.FromFile("Dummy/Images/JPG_ROTATE_270CW.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldHeight, newWidth);
            Assert.AreEqual(oldWidth, newHeight);

        }

        [TestMethod]
        public void WhenImageIsRotate90CWOrientationThenShoulfdFixOrientation()
        {
            var image = Image.FromFile("Dummy/Images/JPG_ROTATE_90CW.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldHeight, newWidth);
            Assert.AreEqual(oldWidth, newHeight);

        }

        [TestMethod]
        public void WhenImageIsMirrorHorizontalAndRotate90CWOrientationThenShoulfdFixOrientation()
        {
            var image = Image.FromFile("Dummy/Images/MIRROR_HORIZONTAL_AND_ROTATE_90CW.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldHeight, newWidth);
            Assert.AreEqual(oldWidth, newHeight);

        }

        [TestMethod]
        public void WhenImageIsMirrorHorizontalAndRotate270CWOrientationThenShoulfdFixOrientation()
        {
            var image = Image.FromFile("Dummy/Images/MIRROR_HORIZONTAL_AND_ROTATE_270CW.jpg");

            int oldWidth = image.Width;
            int oldHeight = image.Height;

            image.FixOrientation();

            int newWidth = image.Width;
            int newHeight = image.Height;

            Assert.IsNotNull(image);
            Assert.AreEqual(oldHeight, newWidth);
            Assert.AreEqual(oldWidth, newHeight);

        }
    }
}
