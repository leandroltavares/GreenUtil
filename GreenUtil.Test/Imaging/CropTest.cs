using GreenUtil.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Imaging
{
    [TestClass]
    public class CropTest
    {
        [TestMethod]
        public void WhenSourceImageIsNullThenCropShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.Crop(null, new Rectangle(10,10,10,10)));
        }

        [TestMethod]
        public void WhenSourceImageIsValidNullThenCropShouldThrowArgumentException()
        {
            //Arrange
            var source = (Bitmap)Image.FromFile("Dummy/Images/BMP.bmp");

            Assert.ThrowsException<ArgumentException>(() => ImageUtil.Crop(source, new Rectangle(0, 0, 0, 0)));
        }

        [TestMethod]
        public void WhenSourceImageIsValidNullThenCropShouldReturnCroppedThrowArgumentException()
        {
            //Arrange
            var source = (Bitmap)Image.FromFile("Dummy/Images/BMP.bmp");

            var target = ImageUtil.Crop(source, new Rectangle(10, 10, 50, 100));

            Assert.IsNotNull(target);
            Assert.AreEqual(50, target.Width);
            Assert.AreEqual(100, target.Height);
        }
    }
}
