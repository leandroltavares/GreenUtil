using GreenUtil.Data;
using GreenUtil.Test.Dummy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Data
{
    [TestClass]
    public class ObjectUtilTest
    {
        [TestMethod]
        public void WhenNullThenShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ObjectUtil.DeepClone<Foo>(null));
        }

        [TestMethod]

        public void WhenShallowComplexTypeThenShouldClonedComplexType()
        {
            var originalInstance = new Foo();
            originalInstance.IntProp = 42;
            originalInstance.StringProp = "test string";
            originalInstance.DecimalProp = 3.14M;


            var clonedInstance = ObjectUtil.DeepClone(originalInstance);

            Assert.IsNotNull(clonedInstance);
            Assert.AreEqual(originalInstance.IntProp, clonedInstance.IntProp);
            Assert.AreEqual(originalInstance.StringProp, clonedInstance.StringProp);
            Assert.AreEqual(originalInstance.DecimalProp, clonedInstance.DecimalProp);
            Assert.AreNotSame(originalInstance, clonedInstance);

        }

        [TestMethod]

        public void WhenDeepComplexTypeThenShouldClonedComplexType()
        {
            var originalInstance = new Bar();
            originalInstance.IntProp = 42;
            originalInstance.StringProp = "test string";
            originalInstance.DecimalProp = 3.14M;

            originalInstance.ComplexType = new Foo();
            originalInstance.ComplexType.IntProp = 21;
            originalInstance.ComplexType.StringProp = "test string 2";
            originalInstance.ComplexType.DecimalProp = 1.618M;

            originalInstance.ComplexTypeList = new List<Foo>();

            originalInstance.ComplexTypeList.Add(new Foo());
            originalInstance.ComplexTypeList.Add(new Foo());

            originalInstance.ComplexTypeList[0].IntProp = 99;
            originalInstance.ComplexTypeList[0].StringProp = "test string 3";
            originalInstance.ComplexTypeList[0].DecimalProp = 1.1M;

            originalInstance.ComplexTypeList[1].IntProp = 101;
            originalInstance.ComplexTypeList[1].StringProp = "test string 4";
            originalInstance.ComplexTypeList[1].DecimalProp = 0.1M;

            var clonedInstance = ObjectUtil.DeepClone(originalInstance);

            Assert.IsNotNull(clonedInstance);
            Assert.AreEqual(originalInstance.IntProp, clonedInstance.IntProp);
            Assert.AreEqual(originalInstance.StringProp, clonedInstance.StringProp);
            Assert.AreEqual(originalInstance.DecimalProp, clonedInstance.DecimalProp);
            Assert.AreNotSame(originalInstance, clonedInstance);

            Assert.AreNotSame(originalInstance.ComplexType, clonedInstance.ComplexType);
            Assert.AreEqual(originalInstance.ComplexType.IntProp, clonedInstance.ComplexType.IntProp);
            Assert.AreEqual(originalInstance.ComplexType.StringProp, clonedInstance.ComplexType.StringProp);
            Assert.AreEqual(originalInstance.ComplexType.DecimalProp, clonedInstance.ComplexType.DecimalProp);

            Assert.AreNotSame(originalInstance.ComplexTypeList, clonedInstance.ComplexTypeList);

            Assert.AreNotSame(originalInstance.ComplexTypeList[0], clonedInstance.ComplexTypeList[0]);
            Assert.AreEqual(originalInstance.ComplexTypeList[0].IntProp, clonedInstance.ComplexTypeList[0].IntProp);
            Assert.AreEqual(originalInstance.ComplexTypeList[0].StringProp, clonedInstance.ComplexTypeList[0].StringProp);
            Assert.AreEqual(originalInstance.ComplexTypeList[0].DecimalProp, clonedInstance.ComplexTypeList[0].DecimalProp);

            Assert.AreNotSame(originalInstance.ComplexTypeList[1], clonedInstance.ComplexTypeList[1]);
            Assert.AreEqual(originalInstance.ComplexTypeList[1].IntProp, clonedInstance.ComplexTypeList[1].IntProp);
            Assert.AreEqual(originalInstance.ComplexTypeList[1].StringProp, clonedInstance.ComplexTypeList[1].StringProp);
            Assert.AreEqual(originalInstance.ComplexTypeList[1].DecimalProp, clonedInstance.ComplexTypeList[1].DecimalProp);
        }
    }
}
