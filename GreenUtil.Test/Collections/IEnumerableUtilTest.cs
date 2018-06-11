using GreenUtil.Collections;
using GreenUtil.Test.Dummy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenUtil.Test.Collections
{
    [TestClass]
    public class IEnumerableUtilTest
    {
        [TestMethod]
        public void WhenNullListThenDistinctShouldThrowArgumentNullException()
        {
            List<Foo> sourceList = null;

            Assert.ThrowsException<ArgumentNullException>(() => { IEnumerableUtil.DistinctBy(sourceList, f => f.IntProp).ToList(); });
        }


        [TestMethod]
        public void WhenListWithoutRepeatedIntegerValuesThenDistinctShouldReturnListWithSameCount()
        {
            List<Foo> sourceList = new List<Foo>();
            sourceList.Add(new Foo() { IntProp = 43, StringProp = "First String" });
            sourceList.Add(new Foo() { IntProp = 42, StringProp = "Second String" });
            sourceList.Add(new Foo() { IntProp = 41, StringProp = "Third String" });
            sourceList.Add(new Foo() { IntProp = 40, StringProp = "Fourth String" });

            var distinctEnumerable = IEnumerableUtil.DistinctBy(sourceList, f => f.IntProp);

            Assert.AreEqual(sourceList.Count, distinctEnumerable.Count());
        }

        [TestMethod]
        public void WhenListWithoutRepeatedStringValuesThenDistinctShouldReturnListWithSameCount()
        {
            List<Foo> sourceList = new List<Foo>();
            sourceList.Add(new Foo() { IntProp = 43, StringProp = "First String" });
            sourceList.Add(new Foo() { IntProp = 42, StringProp = "Second String" });
            sourceList.Add(new Foo() { IntProp = 41, StringProp = "Third String" });
            sourceList.Add(new Foo() { IntProp = 40, StringProp = "Fourth String" });

            var distinctEnumerable = IEnumerableUtil.DistinctBy(sourceList, f => f.StringProp);

            Assert.AreEqual(sourceList.Count, distinctEnumerable.Count());
        }

        [TestMethod]
        public void WhenListWithRepeatedIntegerValuesThenDistinctShouldReturnListWithSameCount()
        {
            List<Foo> sourceList = new List<Foo>();
            sourceList.Add(new Foo() { IntProp = 42, StringProp = "First String" });
            sourceList.Add(new Foo() { IntProp = 42, StringProp = "Second String" });
            sourceList.Add(new Foo() { IntProp = 42, StringProp = "Second String" });

            sourceList.Add(new Foo() { IntProp = 41, StringProp = "Third String" });
            sourceList.Add(new Foo() { IntProp = 40, StringProp = "Fourth String" });


            var distinctEnumerable = IEnumerableUtil.DistinctBy(sourceList, f => f.IntProp);

            Assert.AreEqual(3, distinctEnumerable.Count());
        }

        [TestMethod]
        public void WhenListWithRepeatedStringValuesThenDistinctShouldReturnListWithSameCount()
        {
            List<Foo> sourceList = new List<Foo>();
            sourceList.Add(new Foo() { IntProp = 43, StringProp = "First String" });
            sourceList.Add(new Foo() { IntProp = 43, StringProp = "First String" });
            sourceList.Add(new Foo() { IntProp = 42, StringProp = "First String" });
            sourceList.Add(new Foo() { IntProp = 41, StringProp = "Third String" });
            sourceList.Add(new Foo() { IntProp = 40, StringProp = "Fourth String" });

            var distinctEnumerable = IEnumerableUtil.DistinctBy(sourceList, f => f.StringProp);

            Assert.AreEqual(3, distinctEnumerable.Count());
        }

        [TestMethod]
        public void WhenSourceListIsNullThenContainsShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => IEnumerableUtil.Contains(null, new List<int>() { }));
        }

        [TestMethod]
        public void WhenOtherListIsNullThenContainsShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => IEnumerableUtil.Contains(new List<int>() { }, null));
        }

        [TestMethod]
        public void WhenListsAreSameThenContainsShouldReturnTrue()
        {
            List<int> sourceList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Assert.IsTrue(IEnumerableUtil.Contains(sourceList, sourceList));
        }

        [TestMethod]
        public void WhenListsContainsSameElementsThenContainsShouldReturnTrue()
        {
            List<int> sourceList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> otherList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Assert.IsTrue(IEnumerableUtil.Contains(sourceList, otherList));
        }

        [TestMethod]
        public void WhenListsContainsSameElementsButInDifferentOrderThenContainsShouldReturnTrue()
        {
            List<int> sourceList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> otherList = new List<int>() { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            Assert.IsTrue(IEnumerableUtil.Contains(sourceList, otherList));
        }

        [TestMethod]
        public void WhenListsContainsDifferentElementsThenContainsShouldReturnFalse()
        {
            List<int> sourceList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> otherList = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            Assert.IsFalse(IEnumerableUtil.Contains(sourceList, otherList));
        }

        [TestMethod]
        public void WhenBothListsContainsDuplicatesThenContainsShouldReturnTrue()
        {
            List<int> sourceList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            List<int> otherList = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9, 1, 1 };

            Assert.IsTrue(IEnumerableUtil.Contains(sourceList, otherList));
        }

        [TestMethod]
        public void WhenOneListContainsDuplicatesAndOtherDontThenContainsShouldReturnFalse()
        {
            List<int> sourceList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> otherList = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9, 1, 1 };

            Assert.IsFalse(IEnumerableUtil.Contains(sourceList, otherList));
        }

        [TestMethod]
        public void WhenOneListIsEmptyAndOtherDontThenContainsShouldReturnFalse()
        {
            List<int> sourceList = new List<int>() { };
            List<int> otherList = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9, 1, 1 };

            Assert.IsFalse(IEnumerableUtil.Contains(sourceList, otherList));
        }

        [TestMethod]
        public void WhenOtherListIsEmptyAndOtherDontThenContainsShouldReturnFalse()
        {
            List<int> sourceList = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9, 1, 1 };
            List<int> otherList = new List<int>() { };

            Assert.IsFalse(IEnumerableUtil.Contains(sourceList, otherList));
        }

        [TestMethod]
        public void WhenSourceCollectionIsNullThenShuffleShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => IEnumerableUtil.Shuffle<int>(null).ToList());
        }

        [TestMethod]
        public void WhenOrderedCollectionsThenShuffleShouldReturnUnorderedCollection()
        {
            //Arrange
            var source = Enumerable.Range(0, 1000).ToList();
            var shuffled = IEnumerableUtil.Shuffle(source).ToList();

            Assert.AreNotSame(source, shuffled);
            CollectionAssert.AreNotEqual(source, shuffled);
        }
    }
}
