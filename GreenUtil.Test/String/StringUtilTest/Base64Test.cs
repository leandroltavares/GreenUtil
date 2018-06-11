using System;
using System.Text;
using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GreenUtil.Test.String.StringUtilTest
{
    [TestClass]
    public class Base64Test
    {
        [TestMethod]
        public void WhenNullIsProvidedThenToBase64ThenShouldThrowArgumentNullException()
        {
            string instance = null;

            Assert.ThrowsException<ArgumentNullException>(() => instance.ToBase64());
        }

        [TestMethod]
        public void WhenInstanceIsProvidedAndDefaultEncodingThenShouldReturnBase64()
        {
            string instance = "greenconcept";

            var base64 = instance.ToBase64();

            Assert.AreEqual("Z3JlZW5jb25jZXB0", base64);
        }

        [TestMethod]
        public void WhenInstanceIsProvidedAndUTF8EncodingReturnBase64()
        {
            string instance = "áéíóú";

            var base64 = instance.ToBase64(Encoding.UTF8);

            Assert.AreEqual("w6HDqcOtw7PDug==", base64);
        }

        [TestMethod]
        public void WhenInstanceIsProvidedAndWindows1252EncodingReturnBase64()
        {
            string instance = "áéíóú";

            var base64 = instance.ToBase64(Encoding.GetEncoding("Windows-1252"));

            Assert.AreEqual("4ent8/o=", base64);
        }


        [TestMethod]
        public void WhenNullIsProvidedThenFromBase64ShouldReturnNull()
        {
            string instance = null;

            Assert.ThrowsException<ArgumentNullException>(() => instance.FromBase64()); 
        }


        [TestMethod]
        public void WhenBase64AndDefaultEncodingThenShouldReturnString()
        {
            string base64 = "Z3JlZW5jb25jZXB0";

            var result = base64.FromBase64();

            Assert.AreEqual("greenconcept", result);
        }

        [TestMethod]
        public void WhenUTF8EncodingAndUTF8EncodingReturnString()
        {
            string base64 = "w6HDqcOtw7PDug==";

            var result = base64.FromBase64(Encoding.UTF8);

            Assert.AreEqual("áéíóú", result);
        }

        [TestMethod]
        public void WhenBase64AndWindows1252EncodingReturnString()
        {
            string base64 = "4ent8/o=";

            var instance = base64.FromBase64(Encoding.GetEncoding("Windows-1252"));

            Assert.AreEqual("áéíóú", instance);
        }

        [TestMethod]
        public void WhenBase64NullThenThrowException()
        {

            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.IsBase64(null));
        }

        [TestMethod]
        public void WhenValidBase64StringThenShouldReturnTrue()
        {
            string base64 = "4ent8/o=";

            bool resultado = StringUtil.IsBase64(base64);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void WhenInvalidBase64StringThenShouldReturnTrue()
        {
            string base64 = "4ent8/o";

            bool resultado = StringUtil.IsBase64(base64);

            Assert.IsFalse(resultado);
        }


        [TestMethod]
        public void WhenInvalid2Base64StringThenShouldReturnTrue()
        {
            string base64 = "áéíó";

            bool resultado = StringUtil.IsBase64(base64);

            Assert.IsFalse(resultado);
        }
    }
}
