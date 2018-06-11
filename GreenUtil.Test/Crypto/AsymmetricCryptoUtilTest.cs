using GreenUtil.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography;

namespace GreenUtil.Test.Crypto
{
    [TestClass]
    public class AsymmetricCryptoUtilTest
    {
        [TestMethod]
        public void WhenKeySizeIsLessThan384ThenShouldReturnFalse()
        {
            Assert.IsFalse(AsymmetricCryptoUtil.IsKeySizeValid(376));
        }

        [TestMethod]
        public void WhenKeySizeIsGreaterThan384ThenShouldReturnFalse()
        {
            Assert.IsFalse(AsymmetricCryptoUtil.IsKeySizeValid(16392));
        }

        [TestMethod]
        public void WhenKeySizeIsInRangeAndMultipleOfEightThenShouldReturnTrue()
        {
            Assert.IsTrue(AsymmetricCryptoUtil.IsKeySizeValid(4096));
        }

        [TestMethod]
        public void WhenKeySizeIsInRangeAndNotMultipleOfEightThenShouldReturnFalse()
        {
            Assert.IsFalse(AsymmetricCryptoUtil.IsKeySizeValid(4092));
        }

        [TestMethod]
        public void WhenKeySizeIs4096ThenShouldReturnMaxDataLength471()
        {
            Assert.AreEqual(471, AsymmetricCryptoUtil.GetMaxDataLength(4096));
        }

        [TestMethod]
        public void WhenKeySizeIs2048ThenShouldReturnMaxDataLength215()
        {
            Assert.AreEqual(215, AsymmetricCryptoUtil.GetMaxDataLength(2048));
        }

        [TestMethod]
        public void WhenKeySizeIsOutOfRangeThenShouldThrowArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => AsymmetricCryptoUtil.GetMaxDataLength(int.MaxValue));
        }

        [TestMethod]
        public void WhenGeneratingKeysThenPublicAndPrivateKeyXMLMustBeDifferent()
        {
            int keySize = 4096;
            string publicKey;
            string publicAndPrivateKey;
            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.AreNotEqual(string.Empty, publicKey);
            Assert.IsNotNull(publicKey);

            Assert.AreNotEqual(string.Empty, publicAndPrivateKey);
            Assert.IsNotNull(publicAndPrivateKey);

            Assert.AreNotEqual(publicKey, publicAndPrivateKey);
        }

        [TestMethod]
        public void WhenGeneratingKeysAreOutOfRangeThenShouldThrowException()
        {
            int keySize = int.MaxValue;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                AsymmetricCryptoUtil.GenerateKeys(keySize, out string publicKey, out string publicAndPrivateKey);
            });
        }

        [TestMethod]
        public void WhenGeneratingKeysThenPublicAndPrivateAndRSAParametersKeyMustBeDifferent()
        {
            int keySize = 4096;
            RSAParameters publicKey;
            RSAParameters publicAndPrivateKey;
            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.AreNotEqual(string.Empty, publicKey);
            Assert.IsNotNull(publicKey);

            Assert.AreNotEqual(string.Empty, publicAndPrivateKey);
            Assert.IsNotNull(publicAndPrivateKey);

            Assert.AreNotEqual(publicKey, publicAndPrivateKey);
        }

        [TestMethod]
        public void WhenGeneratingKeysAreOutOfRangeAndRSAParametersThenShouldThrowException()
        {
            int keySize = int.MaxValue;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                AsymmetricCryptoUtil.GenerateKeys(keySize, out RSAParameters publicKey, out RSAParameters publicAndPrivateKey);
            });
        }

        [TestMethod]
        public void WhenByteArrayIsProvidedThenShouldBeAbleToEncryptAndDecryptTest()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25};
            int keySize = 1024;
            string publicKey;
            string publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            var encryptedData = AsymmetricCryptoUtil.Encrypt(data, keySize, publicKey);

            var decryptedData = AsymmetricCryptoUtil.Decrypt(encryptedData, keySize, publicAndPrivateKey);

            CollectionAssert.AreNotEqual(data, encryptedData);
            CollectionAssert.AreEqual(data, decryptedData);
        }

        [TestMethod]
        public void WhenByteArrayIsEmptyAndDrecryptThenShouldThrowArgumentNullException()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 1024;
            string publicKey;
            string publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Decrypt(new byte[] { }, keySize, publicAndPrivateKey));
        }

        [TestMethod]
        public void WhenByteArrayIsNullAndDrecryptThenShouldThrowArgumentNullException()
        {
            int keySize = 1024;
            string publicKey;
            string publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Decrypt(null, keySize, publicAndPrivateKey));
        }

        [TestMethod]
        public void WhenByteArrayIsEmptyAndRSAParametersDrecryptThenShouldThrowArgumentNullException()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 1024;
            RSAParameters publicKey;
            RSAParameters publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Decrypt(new byte[] { }, keySize, publicAndPrivateKey));
        }

        [TestMethod]
        public void WhenByteArrayIsNullAndRSAParametersDrecryptThenShouldThrowArgumentNullException()
        {

            int keySize = 1024;
            RSAParameters publicKey;
            RSAParameters publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Decrypt(null, keySize, publicAndPrivateKey));
        }


        [TestMethod]
        public void WhenByteArrayKeyIsOutOfRangeAndDecryptThenShouldThrowArgumentNullException()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 1024;
            string publicKey;
            string publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => AsymmetricCryptoUtil.Decrypt(data, int.MaxValue, publicAndPrivateKey));
        }

        [TestMethod]
        public void WhenByteArrayKeyIsOutOfRangeAndDecryptAndRSAParametersThenShouldThrowArgumentNullException()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 1024;
            RSAParameters publicKey;
            RSAParameters publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => AsymmetricCryptoUtil.Decrypt(data, int.MaxValue, publicAndPrivateKey));
        }

        [TestMethod]
        public void WhenByteArrayAndKeyIsNullAndDrecryptThenShouldThrowArgumentNullException()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 1024;

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Decrypt(data, keySize, null));
        }

        [TestMethod]
        public void WhenTextIsProvidedThenShouldBeAbleToEncryptAndDecryptTest()
        {
            string data = "Essa é uma string de teste para testar a criptografia assimétrica utilizando RSA.";
            int keySize = 4096;
            string publicKey;
            string publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            var encryptedData = AsymmetricCryptoUtil.EncryptText(data, keySize, publicKey);

            var decryptedData = AsymmetricCryptoUtil.DecryptText(encryptedData, keySize, publicAndPrivateKey);

            Assert.AreNotEqual(data, encryptedData);
            Assert.AreEqual(data, decryptedData);
        }

        [TestMethod]
        public void WhenByteArrayIsProvidedAndRSAParameterKeysThenShouldBeAbleToEncryptAndDecryptTest()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 1024;
            RSAParameters publicKey;
            RSAParameters publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            var encryptedData = AsymmetricCryptoUtil.Encrypt(data, keySize, publicKey);

            var decryptedData = AsymmetricCryptoUtil.Decrypt(encryptedData, keySize, publicAndPrivateKey);

            CollectionAssert.AreNotEqual(data, encryptedData);
            CollectionAssert.AreEqual(data, decryptedData);
        }

        [TestMethod]
        public void WhenTextIsProvidedAndRSAParameterKeysThenShouldBeAbleToEncryptAndDecryptTest()
        {
            string data = "Test cryptographic RSA";
            int keySize = 8192;
            RSAParameters publicKey;
            RSAParameters publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            var encryptedData = AsymmetricCryptoUtil.EncryptText(data, keySize, publicKey);

            var decryptedData = AsymmetricCryptoUtil.DecryptText(encryptedData, keySize, publicAndPrivateKey);

            Assert.AreNotEqual(data, encryptedData);
            Assert.AreEqual(data, decryptedData);
        }

        [TestMethod]
        public void WhenDataToEncryptIsNullThenShouldThrowArgumentNullException()
        {
            int keySize = 1024;
            string publicKey;
            string publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Encrypt(null, keySize, publicKey));
        }

        [TestMethod]
        public void WhenDataToEncryptIsemptyThenShouldThrowArgumentNullException()
        {
            int keySize = 1024;
            string publicKey;
            string publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Encrypt(new byte[] { }, keySize, publicKey));
        }


        [TestMethod]
        public void WhenDataToEncryptIsNullAndRSAParametersThenShouldThrowArgumentNullException()
        {
            int keySize = 1024;
            RSAParameters publicKey;
            RSAParameters publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Encrypt(null, keySize, publicKey));
        }

        [TestMethod]
        public void WhenDataToEncryptIsEmptyAndRSAParametersThenShouldThrowArgumentNullException()
        {
            int keySize = 1024;
            RSAParameters publicKey;
            RSAParameters publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Encrypt(new byte[] { }, keySize, publicKey));
        }

        [TestMethod]
        public void WhenKeySizeIsGreaterThanMaxValueThenShouldThrowArgumentException()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 4096;
            string publicKey;
            string publicAndPrivateKey;

            AsymmetricCryptoUtil.GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => AsymmetricCryptoUtil.Encrypt(data, int.MaxValue, publicKey));
        }

        [TestMethod]
        public void WhenPublicKeyIsEmptyThenShouldThrowArgumentException()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 2048;

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Encrypt(data, keySize, string.Empty));
        }


        [TestMethod]
        public void WhenPublicKeyIsNullhenShouldThrowArgumentException()
        {
            byte[] data = { 10, 47, 67, 10, 91, 15, 33, 25 };
            int keySize = 2048;

            Assert.ThrowsException<ArgumentNullException>(() => AsymmetricCryptoUtil.Encrypt(data, keySize, null));
        }

        [TestMethod]
        public void WhenDataSizeIsGreaterThanMaximumAllowedForKeyThenShouldThrowArgumentException()
        {  
            int keySize = 384;
            byte[] data = new byte[AsymmetricCryptoUtil.GetMaxDataLength(384) * 2];

            Random random = new Random();
            
            random.NextBytes(data);

            AsymmetricCryptoUtil.GenerateKeys(keySize, out string publicKey, out string publicAndPrivateKey);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => AsymmetricCryptoUtil.Encrypt(data, keySize, publicKey));
        }

        [TestMethod]
        public void WhenDataSizeIsGreaterThanMaximumAllowedForKeyAndRSAParametersThenShouldThrowArgumentException()
        {
            int keySize = 384;
            byte[] data = new byte[AsymmetricCryptoUtil.GetMaxDataLength(384) * 2];

            Random random = new Random();

            random.NextBytes(data);

            AsymmetricCryptoUtil.GenerateKeys(keySize, out RSAParameters publicKey, out RSAParameters publicAndPrivateKey);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => AsymmetricCryptoUtil.Encrypt(data, keySize, publicKey));
        }
    }
}
