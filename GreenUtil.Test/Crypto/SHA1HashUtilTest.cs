using GreenUtil.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace GreenUtil.Test.Crypto
{
    [TestClass]
    public class SHA1HashUtilTest
    {
        [TestMethod]
        public void WhenStringIsNullShouldThrowArgumentNullException()
        {
            string input = null;

            Assert.ThrowsException<ArgumentNullException>(() => HashUtil.ToSHA1(input));
        }

        [TestMethod]
        public void WhenStringShouldReturnSHA1()
        {
            string input = "greenconcept";

            string hash = HashUtil.ToSHA1(input);

            Assert.AreEqual("942A4C8B5DC9132AC0F5BF7CD19E3A0242AEB7E9", hash);
        }


        [TestMethod]
        public void WhenStringAndEncodingUTF8ShouldReturnSHA1()
        {
            string input = "greenconcept";

            string hash = HashUtil.ToSHA1(input, Encoding.UTF8);

            Assert.AreEqual("942A4C8B5DC9132AC0F5BF7CD19E3A0242AEB7E9", hash);
        }

        [TestMethod]
        public void WhenStringWithAccentsAndEncodingUTF8ShouldReturnSHA1()
        {
            string input = "áéíóú";

            string hash = HashUtil.ToSHA1(input, Encoding.UTF8);

            Assert.AreEqual("4AC78123350CEAC9794914199572C53DC5734861", hash);
        }

        [TestMethod]
        public void WhenStringWithAccentsAndEncodingWindows1252ShouldReturnSHA1()
        {
            string input = "áéíóú";

            string hash = HashUtil.ToSHA1(input, Encoding.GetEncoding("Windows-1252"));

            Assert.AreEqual("763B9E1AAEA74F8A8DB4BA1280276BF0FB4EBF02", hash);
        }

        [TestMethod]
        public void WhenByteArrayIsNullShouldThrowArgumentNullException()
        {
            byte[] input = null;

            Assert.ThrowsException<ArgumentNullException>(() => HashUtil.ToSHA1(input));
        }

        [TestMethod]
        public void WhenByteArrayShouldReturnSHA1()
        {
            byte[] input = new byte[8] {42, 13, 21, 04, 06, 92, 01, 19};

            string hash = HashUtil.ToSHA1(input);

            Assert.AreEqual("D4F5AD44618AF3CDB41A69551AF4ACDB441C4E5D", hash);
        }

        [TestMethod]
        public void WhenFileShouldReturnSHA1()
        {
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Dummy\\SAMPLE.txt";

            string hash = HashUtil.ToSHA1(new FileInfo(filePath));

            Assert.AreEqual("2F10AB5C4DD79B8CAD762516051016DCBB4A8B31", hash);
        }

        [TestMethod]
        public void WhenNullFileShouldThrowArgumentNullException()
        {
            FileInfo file = null;

            Assert.ThrowsException<ArgumentNullException>(() => HashUtil.ToSHA1(file));
        }
    }
}
