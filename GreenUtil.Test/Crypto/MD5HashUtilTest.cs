using GreenUtil.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace GreenUtil.Test.Crypto
{
    [TestClass]
    public class MD5HashUtilTest
    {
        [TestMethod]
        public void WhenStringIsNullShouldThrowArgumentNullException()
        {
            string input = null;

            Assert.ThrowsException<ArgumentNullException>(() => HashUtil.ToMD5(input));
            
        }

        [TestMethod]
        public void WhenStringShouldReturnMD5()
        {
            string input = "greenconcept";

            string hash = HashUtil.ToMD5(input);

            Assert.AreEqual("A1ADCC17D7C332FF8DF2BD4027C25B49", hash);
        }


        [TestMethod]
        public void WhenStringAndEncodingUTF8ShouldReturnMD5()
        {
            string input = "greenconcept";

            string hash = HashUtil.ToMD5(input, Encoding.UTF8);

            Assert.AreEqual("A1ADCC17D7C332FF8DF2BD4027C25B49", hash);
        }

        [TestMethod]
        public void WhenStringWithAccentsAndEncodingUTF8ShouldReturnMD5()
        {
            string input = "áéíóú";

            string hash = HashUtil.ToMD5(input, Encoding.UTF8);

            Assert.AreEqual("E3428CD5067A502FBE9C00249585C0EE", hash);
        }

        [TestMethod]
        public void WhenStringWithAccentsAndEncodingWindows1252ShouldReturnMD5()
        {
            string input = "áéíóú";

            string hash = HashUtil.ToMD5(input, Encoding.GetEncoding("Windows-1252"));

            Assert.AreEqual("E4A690E12144F4B8E416770741330280", hash);
        }

        [TestMethod]
        public void WhenByteArrayIsNullShouldThrowArgumentNullException()
        {
            byte[] input = null;

            Assert.ThrowsException<ArgumentNullException>(() => HashUtil.ToMD5(input));
           
        }

        [TestMethod]
        public void WhenByteArrayShouldReturnMD5()
        {
            byte[] input = new byte[8] {42, 13, 21, 04, 06, 92, 01, 19};

            string hash = HashUtil.ToMD5(input);

            Assert.AreEqual("E49C82D369D729AE708005E9D7BDE49F", hash);
        }

        [TestMethod]
        public void WhenFileShouldReturnMD5()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Dummy\\SAMPLE.txt";

            string hash = HashUtil.ToMD5(new FileInfo(filePath));

            Assert.AreEqual("F183A048AFF8789B01962B98C6994B1B", hash);
        }

        [TestMethod]
        public void WhenNullFileShouldThrowArgumentNullException()
        {
            FileInfo file = null;

            Assert.ThrowsException<ArgumentNullException>(() => HashUtil.ToMD5(file));
        }
    }
}
