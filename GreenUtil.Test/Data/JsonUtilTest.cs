using GreenUtil.Data;
using GreenUtil.Test.Dummy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.Data
{
    [TestClass]
    public class XmlTest
    {
        [TestMethod]
        public void WhenNullIsProvidedThenJsonShouldThrowException()
        {
            object instance = null;

            Assert.ThrowsException<ArgumentNullException>(() => instance.ToJson());
        }

        [TestMethod]
        public void WhenInstanceIsProvidedThenJsonShouldBeReturned()
        {
            var foo = new Foo();
            foo.DecimalProp = 3.14M;
            foo.IntProp = 42;
            foo.StringProp = "Juiz faz com que whisky de malte baixe logo preço de venda";

            var json = foo.ToJson();

            Assert.AreNotEqual(string.Empty, json);
            Assert.IsTrue(json.Contains("3.14"));
            Assert.IsTrue(json.Contains("42"));
            Assert.IsTrue(json.Contains("Juiz faz com que whisky de malte baixe logo preço de venda"));
            Assert.IsTrue(json.StartsWith("{"));
            Assert.IsTrue(json.EndsWith("}"));
        }

        [TestMethod]
        public void WhenStringWithJsonIsProvidedThenInstanceShouldbeReturned()
        {
            string json = "{\"IntProp\":42,\"StringProp\":\"Juiz faz com que whisky de malte baixe logo preço de venda\",\"DecimalProp\":3.14}";

            var instance = json.ParseJson<Foo>();

            Assert.IsNotNull(instance);
            Assert.AreEqual(42, instance.IntProp);
            Assert.AreEqual("Juiz faz com que whisky de malte baixe logo preço de venda", instance.StringProp);
            Assert.AreEqual(3.14M, instance.DecimalProp);
        }


        [TestMethod]
        public void WhenNullStringIsProvidedThenShouldThrowArgumentNullException()
        {
            string json = null;

            Assert.ThrowsException<ArgumentNullException>(() => json.ParseJson<Foo>());
        }

        [TestMethod]
        public void WhenTypeIsProvidedThenSchemaShouldBeReturned()
        {
            string jsonSchema = JsonUtil.GenerateJsonSchemaText<Foo>();

            Assert.IsTrue(jsonSchema.StartsWith("{"));
            Assert.IsTrue(jsonSchema.EndsWith("}"));

        }
    }
}
