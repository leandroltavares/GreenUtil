using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using GreenUtil.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenUtil.Test.Imaging
{
    [TestClass]
    public class DirectBitmapTest
    {
        [TestMethod]
        public void WhenWidthIsZeroThenDirectBitmapConstructorShouldThrowArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DirectBitmap(0, 100, PixelFormat.Format32bppArgb));
        }

        [TestMethod]
        public void WhenWidthIsLessThanZeroThenDirectBitmapConstructorShouldThrowArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DirectBitmap(-100, 100, PixelFormat.Format32bppArgb));
        }

        [TestMethod]
        public void WhenHeightIsZeroThenDirectBitmapConstructorShouldThrowArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DirectBitmap(100, 0, PixelFormat.Format32bppArgb));
        }

        [TestMethod]
        public void WhenHeightIsLessThanZeroThenDirectBitmapConstructorShouldThrowArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new DirectBitmap(100, -100, PixelFormat.Format32bppArgb));
        }

        [TestMethod]
        public void WhenConstructorParametersAreValidaThenDirectBitmapConstructorShouldReturnInstanceWithValidValues()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {

                Assert.IsNotNull(instance);
                Assert.IsNotNull(instance.Bits);
                Assert.IsNotNull(instance.Bitmap);
                Assert.IsFalse(instance.Disposed);
                Assert.AreEqual(width, instance.Width);
                Assert.AreEqual(height, instance.Height);
            }
        }

        [TestMethod]
        public void WhenXPositionIsLessZeroThenSetPixelShouldThrowArgumentOutOfRangeException()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.SetPixel(-1, 0, Color.Green));
            }
        }

        [TestMethod]
        public void WhenXPositionIsGreaterThanImageSizeThenSetPixelShouldThrowArgumentOutOfRangeException()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.SetPixel(width + 1, 0, Color.Green));
            }
        }

        [TestMethod]
        public void WhenYPositionIsLessZeroThenSetPixelShouldThrowArgumentOutOfRangeException()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.SetPixel(0, -1, Color.Green));
            }
        }

        [TestMethod]
        public void WhenYPositionIsGreaterThanImageSizeThenSetPixelShouldThrowArgumentOutOfRangeException()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.SetPixel(0, height + 1, Color.Green));
            }
        }

        [TestMethod]
        public void WhenPositionIsInsideImageThenSetPixelShouldSetPixelColor()
        {
            int width = 100;
            int height = 200;
            var color = Color.Green;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                int targetX = width / 2;
                int targetY = height / 2;

                instance.SetPixel(targetX, targetY, color);

                Assert.AreEqual(color.ToArgb(), instance.Bitmap.GetPixel(targetX, targetY).ToArgb());
            }
        }

        [TestMethod]
        public void WhenXPositionIsLessZeroThenGetPixelShouldThrowArgumentOutOfRangeException()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.GetPixel(-1, 0));
            }
        }

        [TestMethod]
        public void WhenXPositionIsGreaterThanImageSizeThenGetPixelShouldThrowArgumentOutOfRangeException()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.GetPixel(width + 1, 0));
            }
        }

        [TestMethod]
        public void WhenYPositionIsLessZeroThenGetPixelShouldThrowArgumentOutOfRangeException()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.GetPixel(0, -1));
            }
        }

        [TestMethod]
        public void WhenYPositionIsGreaterThanImageSizeThenGetPixelShouldThrowArgumentOutOfRangeException()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => instance.GetPixel(0, height + 1));
            }
        }

        [TestMethod]
        public void WhenPositionIsInsideImageThenGetPixelShouldGetPixelColor()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                int targetX = width / 2;
                int targetY = height / 2;

                var colorARGB = instance.GetPixel(targetX, targetY).ToArgb();

                Assert.AreEqual(instance.Bitmap.GetPixel(targetX, targetY).ToArgb(), colorARGB);
            }
        }

        [TestMethod]
        public void WhenDirectedBitmapIsUsedThenPerformanceShouldBeBetterThanStandardSetPixel()
        {
            int width = 100;
            int height = 200;

            using (var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb))
            {
                var time1 = DateTime.Now;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        instance.Bitmap.SetPixel(i, j, Color.Orange);
                    }
                }

                var time2 = DateTime.Now;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        instance.SetPixel(i, j, Color.Orange);
                    }
                }

                var time3 = DateTime.Now;

                Debug.WriteLine("Standard Time:" + (time2 - time1).TotalMilliseconds);
                Debug.WriteLine("Fast Time:" + (time3 - time2).TotalMilliseconds);

                Console.WriteLine("Standard Time:" + (time2 - time1).TotalMilliseconds);
                Console.WriteLine("Fast Time:" + (time3 - time2).TotalMilliseconds);

                Assert.IsTrue(time2 - time1 > time3 - time2);

            }
        }

        [TestMethod]
        public void WhenSetPixelAfterDisposeThenShouldThrowException()
        {
            int width = 100;
            int height = 200;

            var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb);

            instance.Dispose();

            Assert.ThrowsException<ObjectDisposedException>(() => instance.SetPixel(0, 0, Color.Green));
            
        }

        [TestMethod]
        public void WhenGetPixelAfterDisposeThenShouldThrowException()
        {
            int width = 100;
            int height = 200;

            var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb);

            instance.Dispose();

            Assert.ThrowsException<ObjectDisposedException>(() => instance.GetPixel(0, 0));

        }

        [TestMethod]
        public void WhenMultipleCallsToDisposeThenSecondCallShouldBeIgnored()
        {
            int width = 100;
            int height = 200;

            var instance = new DirectBitmap(width, height, PixelFormat.Format32bppArgb);
            
            Assert.IsFalse(instance.Disposed);

            instance.Dispose();

            Assert.IsTrue(instance.Disposed);

            instance.Dispose();

            Assert.IsTrue(instance.Disposed);
        }


        [TestMethod]
        public void WhenSourceIsNullThenFromImageShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => DirectBitmap.FromImage(null));
        }


        [TestMethod]
        public void WhenSourceIsNotNullThenFromImageShouldReturnDirectBitmapInstance()
        {
            int width = 100;
            int height = 200;

            using (var image = new Bitmap(width, height))
            {
                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.FillRectangle(Brushes.Green, new Rectangle(0, 0, width, height));
                }

                var directBitmap = DirectBitmap.FromImage(image);

                Assert.IsNotNull(directBitmap);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Assert.AreEqual(Color.Green.ToArgb(), directBitmap.GetPixel(i, j).ToArgb());
                    }
                }

            }
                
        }

        //TODO Leandro: Add tests for other PixelFormats


        [TestMethod]
        public void WhenPixelFormatIsDifferentFrom32BppARGBThenConstructorShouldThrowNotSupportedException()
        {
            Assert.ThrowsException<NotSupportedException>(() => new DirectBitmap(100, 100, PixelFormat.Format1bppIndexed));
        }

    }
}
