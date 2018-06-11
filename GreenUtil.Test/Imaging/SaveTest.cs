using GreenUtil.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Imaging
{
    [TestClass]
    public class SaveTest
    {

        [TestMethod]
        public void WhenSourceIsNullThenSaveShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.Save(null, "FILE.jpg", ImageFormat.Jpeg, 1));
        }

        [TestMethod]
        public void WhenFilePathIsNullThenSaveShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.Save(new Bitmap(10, 10), null, ImageFormat.Bmp, 1));
        }

        [TestMethod]
        public void WhenFilePathIsEmptyThenSaveShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ImageUtil.Save(new Bitmap(10, 10), string.Empty, ImageFormat.Bmp, 1));
        }

        [TestMethod]
        public void WhenSourceImageAndPathAreValidThenSaveShouldSaveImage()
        {
            var source = new Bitmap(10, 10);
            string fileName = "FILE_TESTE.bmp";
            
            ImageUtil.Save(source, fileName, ImageFormat.Bmp, 1);

            using (var target = Image.FromFile(fileName))
            {

                Assert.IsNotNull(target);
                Assert.AreEqual(target.Width, source.Width);
                Assert.AreEqual(target.Height, source.Height);
                Assert.AreEqual("image/bmp", target.MimeType());
            }

            File.Delete(fileName);
        }
    }
}
