using GreenUtil.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace GreenUtil.Test.Imaging
{
    [TestClass]
    public class ScaleTest
    {
        [TestMethod]
        public void WhenSourceIsNullThenScaleShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.Scale(null, 100, 100));
        }

        [TestMethod]
        public void WhenSourceIsNullThenScaleWithFactorShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.Scale(null, 1));
        }

        [TestMethod]
        public void WhenSourceIsNotNullThenScaleShouldReturnScaledImage()
        {
            using (var source = new Bitmap(1000, 500))
            {
                using (var scaled = ImageUtil.Scale(source, 100, 50))
                {
                    Assert.IsNotNull(scaled);
                    Assert.AreEqual(100, scaled.Width);
                    Assert.AreEqual(50, scaled.Height);
                }
            }
        }

        [TestMethod]
        public void WhenSourceIsNotNullThenScaleFactorShouldReturnScaledImage()
        {
            using (var source = new Bitmap(1000, 500))
            {
                using (var scaled = ImageUtil.Scale(source, 0.5))
                {
                    Assert.IsNotNull(scaled);
                    Assert.AreEqual(500, scaled.Width);
                    Assert.AreEqual(250, scaled.Height);
                }
            }
        }

        [TestMethod]
        public void WhenSourceIsNotNullThenScaleFactorEqualOneShouldReturnSameSizeImage()
        {
            using (var source = new Bitmap(1000, 500))
            {
                using (var scaled = ImageUtil.Scale(source, 1))
                {
                    Assert.IsNotNull(scaled);
                    Assert.AreEqual(1000, scaled.Width);
                    Assert.AreEqual(500, scaled.Height);
                }
            }
        }
    }
}
