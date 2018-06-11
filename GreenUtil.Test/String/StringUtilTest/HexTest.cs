using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using GreenUtil.String;
using System;

namespace GreenUtil.Test.String.StringUtilTest
{
    [TestClass]
    public class HexTest
    {
        [TestMethod]
        public void WhenNullIsProvidedThenToHexShouldThrowArgumentNullException()
        {
            string instance = null;

            Assert.ThrowsException<ArgumentNullException>(() => instance.ToHex());
        }

        [TestMethod]
        public void WhenInstanceIsProvidedAndDefaultEncodingThenShouldReturnHexString()
        {
            string instance = "greenconcept";

            var hex = instance.ToHex();

            Assert.AreEqual("67-72-65-65-6E-63-6F-6E-63-65-70-74", hex);
        }

        [TestMethod]
        public void WhenInstanceIsProvidedAndUTF8EncodingReturnBase64()
        {
            string instance = "áéíóú";

            var hex = instance.ToHex(Encoding.UTF8);

            Assert.AreEqual("C3-A1-C3-A9-C3-AD-C3-B3-C3-BA", hex);
        }

        [TestMethod]
        public void WhenInstanceIsProvidedAndWindows1252EncodingReturnHexString()
        {
            string instance = "áéíóú";

            var hex = instance.ToHex(Encoding.GetEncoding("Windows-1252"));

            Assert.AreEqual("E1-E9-ED-F3-FA", hex);
        }


        [TestMethod]
        public void WhenNullIsProvidedThenFromBase64ThenShouldThrowArgumentNullException()
        {
            string instance = null;

            Assert.ThrowsException<ArgumentNullException>(() => instance.FromHex());
        }


        [TestMethod]
        public void WhenHexStringWithDashesAndDefaultEncodingThenShouldReturnString()
        {
            string hex = "67-72-65-65-6E-63-6F-6E-63-65-70-74";

            var result = hex.FromHex();

            Assert.AreEqual("greenconcept", result);
        }

        [TestMethod]
        public void WhenHexStringWithoutDashesAndDefaultEncodingThenShouldReturnString()
        {
            string hex = "677265656E636F6E63657074";

            var result = hex.FromHex();

            Assert.AreEqual("greenconcept", result);
        }

        [TestMethod]
        public void WhenUTF8EncodingAndUTF8EncodingReturnString()
        {
            string hex = "C3A1C3A9C3ADC3B3C3BA";

            var result = hex.FromHex(Encoding.UTF8);

            Assert.AreEqual("áéíóú", result);
        }

        [TestMethod]
        public void WhenHexStringAndWindows1252EncodingReturnString()
        {
            string hex = "E1-E9-ED-F3-FA";

            var instance = hex.FromHex(Encoding.GetEncoding("Windows-1252"));

            Assert.AreEqual("áéíóú", instance);
        }
    }
}
