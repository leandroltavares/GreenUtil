using GreenUtil.Enumeration;
using GreenUtil.Test.Dummy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.Enumeration
{
    [TestClass]
    public class EnumUtilTest
    {
        [TestMethod]
        public void WhenNullInstanceThenShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => EnumUtil.Description<FooEnum?>(null));
        }

        [TestMethod]
        public void WhenNonEnumInstanceThenShouldThrowException()
        {
            Assert.ThrowsException<ArgumentException>(() => EnumUtil.Description<Foo>(new Foo()), "T must be an enumerated type.");
        }

        [TestMethod]
        public void WhenRetrievingEnumWithDescriptionThenShouldReturnRespectiveDescription()
        {
            string description = EnumUtil.Description(FooEnum.Value1);

            Assert.AreEqual("Description for Value 1", description);
        }

        [TestMethod]
        public void WhenRetrievingEnumWithOutDescriptionThenShouldReturnEnumName()
        {
            string description = EnumUtil.Description(BarEnum.Value1);

            Assert.AreEqual("Value1", description);
        }

        [TestMethod]
        public void WhenRetrievingEnumFromStringWithOutDescriptionThenShouldReturnRespectiveEnumValue()
        {
            var enumValue = EnumUtil.Value<BarEnum>("Value3");

            Assert.AreEqual(BarEnum.Value3, enumValue);
        }

        [TestMethod]
        public void WhenRetrievingEnumFromStringWithDescriptionThenShouldReturnRespectiveEnumValue()
        {
            var enumValue = EnumUtil.Value<FooEnum>("Description for Value 3");

            Assert.AreEqual(FooEnum.Value3, enumValue);
        }

        [TestMethod]
        public void WhenRetrievingEnumFromNullUsingDefaultValueThenShouldReturnDefaultEnumValue()
        {
            var enumValue = EnumUtil.Value<FooEnum>(null, true);

            Assert.AreEqual(FooEnum.Value1, enumValue);
        }

        [TestMethod]
        public void WhenRetrievingEnumFromNullAndNotUsingDefaultValueThenShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => EnumUtil.Value<FooEnum>(null, false));
        }
    }
}
