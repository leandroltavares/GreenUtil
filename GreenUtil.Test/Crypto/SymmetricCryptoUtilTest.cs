using GreenUtil.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.Crypto
{
    [TestClass]
    public class SymmetricCryptoUtilTest
    {
        [TestMethod]
        public void WhenDataIsNullThenEncryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Encrypt(null, "some random password", "some random salt"));
        }

        [TestMethod]
        public void WhenDataIsEmptyNullThenEncryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Encrypt(new byte[] { }, "some random password", "some random salt"));
        }

        [TestMethod]
        public void WhenPasswordIsNullThenEncryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Encrypt(new byte[] { 10, 42 },  null, "some random salt"));
        }

        [TestMethod]
        public void WhenPasswordIsEmptyThenEncryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Encrypt(new byte[] { 10, 42 }, string.Empty, "some random salt"));
        }

        [TestMethod]
        public void WhenSaltIsNullThenEncryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Encrypt(new byte[] { 10, 42 }, "some random password", null));
        }

        [TestMethod]
        public void WhenSaltIsEmptyThenEncryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Encrypt(new byte[] { 10, 42 }, "some random password", string.Empty));
        }

        [TestMethod]
        public void WhenDataIsNullThenDecryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Decrypt(null, "some random password", "some random salt"));
        }

        [TestMethod]
        public void WhenDataIsEmptyNullThenDecryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Decrypt(new byte[] { }, "some random password", "some random salt"));
        }

        [TestMethod]
        public void WhenPasswordIsNullThenDecryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Decrypt(new byte[] { 10, 42 }, null, "some random salt"));
        }

        [TestMethod]
        public void WhenPasswordIsEmptyThenDecryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Decrypt(new byte[] { 10, 42 }, string.Empty, "some random salt"));
        }

        [TestMethod]
        public void WhenSaltIsNullThenDecryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Decrypt(new byte[] { 10, 42 }, "some random password", null));
        }

        [TestMethod]
        public void WhenSaltIsEmptyThenDecryptShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => SymmetricCryptoUtil.Decrypt(new byte[] { 10, 42 }, "some random password", string.Empty));
        }


        [TestMethod]
        public void WhenByteArrayThenDecryptAfterEncryptShouldReturnSameData()
        {
            //Arrange
            byte[] data = new byte[256];

            Random random = new Random((int)DateTime.Now.Ticks);
            random.NextBytes(data);

            string password = "gr33np@ssw0rd!";
            string salt = "s@lt.123456";

            //Act
            byte[] encriptedData = SymmetricCryptoUtil.Encrypt(data, password, salt);
            byte[] decriptedData = SymmetricCryptoUtil.Decrypt(encriptedData, password, salt);

            //Assert
            Assert.IsNotNull(encriptedData);
            Assert.IsNotNull(decriptedData);

            Assert.AreNotEqual(0, encriptedData.Length);
            Assert.AreNotEqual(0, decriptedData.Length);

            CollectionAssert.AreEqual(data, decriptedData);
            CollectionAssert.AreNotEqual(encriptedData, data);
        }

        [TestMethod]
        public void WhenTextThenDecryptAfterEncryptShouldReturnSameData()
        {
            //Arrange
            string text = "This is a sample string with some diacritics: áâàâãç";

            string password = "gr33np@ssw0rd!";
            string salt = "s@lt.123456";

            //Act
            var encriptedText = SymmetricCryptoUtil.EncryptText(text, password, salt);
            var decriptedText = SymmetricCryptoUtil.DecryptText(encriptedText, password, salt);

            //Assert
            Assert.IsNotNull(encriptedText);
            Assert.IsNotNull(decriptedText);

            Assert.AreNotEqual(0, encriptedText.Length);
            Assert.AreNotEqual(0, decriptedText.Length);

            Assert.AreEqual(text, decriptedText);
            Assert.AreNotEqual(encriptedText, text);
        }
    }
}
