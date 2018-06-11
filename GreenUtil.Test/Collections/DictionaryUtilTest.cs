using GreenUtil.Collections;
using GreenUtil.Test.Dummy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GreenUtil.Test.Collections
{
    [TestClass]
    public class DictionaryUtilTest
    {
        [TestMethod]
        public void WhenNullCollectionThenShouldThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => DictionaryUtil.ToDictionaryList<int, int>(null, ks => ks));
        }

        [TestMethod]
        public void WhenCollectionWithValuesWithMultipleKeysThenShouldGroupValuesWithSameKey()
        {
            //Arrange
            var list = new List<Foo>();
            list.Add(new Foo() { IntProp = 42, StringProp = "First object with ID 42" });
            list.Add(new Foo() { IntProp = 42, StringProp = "Second object with ID 42" });
            list.Add(new Foo() { IntProp = 21, StringProp = "First object with ID 21" });

            //Act
            var dictionary = DictionaryUtil.ToDictionaryList(list, ks => ks.IntProp);

            //Assert
            Assert.AreEqual(2, dictionary.Count);
            Assert.AreEqual(2, dictionary[42].Count);
            Assert.AreEqual(1, dictionary[21].Count);
        }
    }
}
