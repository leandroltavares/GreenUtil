using GreenUtil.Data;
using GreenUtil.Test.Dummy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace GreenUtil.Test.Data
{
    [TestClass]
    public class XmlUtilTest
    {
        [TestMethod]
        public void WhenNullIsProvidedThenXMLShouldThrowException()
        {
            object instance = null;

            Assert.ThrowsException<ArgumentNullException>(() => instance.ToXml());
        }

        [TestMethod]
        public void WhenInstanceIsProvidedThenXMLShouldBeReturned()
        {
            var foo = new Foo();
            foo.DecimalProp = 3.14M;
            foo.IntProp = 42;
            foo.StringProp = "Juiz faz com que whisky de malte baixe logo preço de venda";

            var xml = foo.ToXml();

            Assert.AreNotEqual(string.Empty, xml);
            Assert.IsTrue(xml.Contains("3.14"));
            Assert.IsTrue(xml.Contains("42"));
            Assert.IsTrue(xml.Contains("Juiz faz com que whisky de malte baixe logo preço de venda"));
            Assert.IsTrue(xml.StartsWith("<?xml version=\"1.0\" encoding=\"utf-16\"?>"));
            Assert.IsTrue(xml.EndsWith(">"));
        }

        [TestMethod]
        public void WhenStringWithXMLIsProvidedThenInstanceShouldbeReturned()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Foo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><IntProp>42</IntProp><StringProp>Juiz faz com que whisky de malte baixe logo preço de venda</StringProp><DecimalProp>3.14</DecimalProp></Foo>";

            var instance = xml.ParseXML<Foo>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(42, instance.IntProp);
            Assert.AreEqual("Juiz faz com que whisky de malte baixe logo preço de venda", instance.StringProp);
            Assert.AreEqual(3.14M, instance.DecimalProp);
        }


        [TestMethod]
        public void WhenNullStringIsProvidedThenInstanceShouldThrowArgumentNullException()
        {
            string xml = null;

            Assert.ThrowsException<ArgumentNullException>(() => xml.ParseXML<Foo>());
        }
    }
}
