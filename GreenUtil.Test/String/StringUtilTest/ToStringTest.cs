using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreenUtil.String;
using System.Threading;
using System.Globalization;
using GreenUtil.Test.Dummy;

namespace GreenUtil.Test.String.StringUtilTest
{
    /// <summary>
    /// Summary description for ToString
    /// </summary>
    [TestClass]
    public class ToStringTest
    {
        [TestMethod]
        public void WhenNullInstanceAndAllOptionsAreTrueThenShouldThrowException()
        {
            //Arrange
            object instance = null;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.ToString(instance, true, true, true));
        }

        [TestMethod]
        public void WhenNullInstanceAndAllOptionsAreFalseThenShouldThrowException()
        {
            //Arrange
            object instance = null;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.ToString(instance, false, false, false));
        }


        [TestMethod]
        public void WhenNullInstanceAndOnlyIncludePropertiesIsTrueThenShouldThrowException()
        {
            //Arrange
            object instance = null;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.ToString(instance, true, false, false));
        }

        [TestMethod]
        public void WhenNullInstanceAndOnlyIncludeMembersIsTrueThenShouldThrowException()
        {
            //Arrange
            object instance = null;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.ToString(instance, false, true, false));

        }

        [TestMethod]
        public void WhenNullInstanceAndBothPropertiesAndMembersAreTrueThenShouldThrowException()
        {
            //Arrange
            object instance = null;

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.ToString(instance, true, true, false));
        }

        [TestMethod]
        public void WhenStringEmptyAndBothPropertiesAndMembersAreTrueThenShouldReturnDefaultToStringImplementation()
        {
            //Arrange
            string instance = string.Empty;

            //Act
            string result = StringUtil.ToString(instance, true, true, false);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenStringInstanceAndBothPropertiesAndMembersAreTrueThenShouldReturnDefaultToStringImplementation()
        {
            //Arrange
            string instance = "SAMPLE STRING";

            //Act
            string result = StringUtil.ToString(instance, true, true, false);

            //Assert
            Assert.AreEqual("SAMPLE STRING", result);
        }

        [TestMethod]
        public void WhenIntInstanceAndBothPropertiesAndMembersAreTrueThenShouldReturnDefaultToStringImplementation()
        {
            //Arrange
            int instance = 42;

            //Act
            string result = StringUtil.ToString(instance, true, true, false);

            //Assert
            Assert.AreEqual("42", result);
        }

        [TestMethod]
        public void WhenDoubleInstanceAndBothPropertiesAndMembersAreTrueThenShouldReturnDefaultToStringImplementation()
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");

            //Arrange
            double instance = 3.14;

            //Act
            string result = StringUtil.ToString(instance, true, true, false);

            //Assert
            Assert.AreEqual("3,14", result);
        }

        [TestMethod]
        public void WhenCompexInstanceAndBothPropertiesAndMembersAreTrueThenShouldReturnFieldsAndProperties()
        {
            //Arrange
            Foo instance = new Foo();
            instance.DecimalProp = 3.14M;
            instance.IntProp = 42;
            instance.StringProp = "Juiz faz com que whisky de malte baixe logo preço de venda";
           
            //Act
            string result = StringUtil.ToString(instance, true, true, true);

            //Assert
            Assert.AreNotEqual(string.Empty, result);

            Assert.IsTrue(result.Contains(instance.DecimalProp.ToString()));
            Assert.IsTrue(result.Contains(instance.IntProp.ToString()));
            Assert.IsTrue(result.Contains(instance.StringProp.ToString()));
            Assert.IsTrue(result.Contains("[P]"));
            Assert.IsTrue(result.Contains("[F]"));
        }

        [TestMethod]
        public void WhenCompexInstanceWithNullValueAndBothPropertiesAndMembersAreTrueThenShouldReturnFieldsAndProperties()
        {
            //Arrange
            Foo instance = new Foo();
            instance.DecimalProp = 3.14M;
            instance.IntProp = 42;
            instance.StringProp =  null;

            //Act
            string result = StringUtil.ToString(instance, true, true, true);

            //Assert
            Assert.AreNotEqual(string.Empty, result);

            Assert.IsTrue(result.Contains(instance.DecimalProp.ToString()));
            Assert.IsTrue(result.Contains(instance.IntProp.ToString()));
            Assert.IsTrue(result.Contains("NULL"));
            Assert.IsTrue(result.Contains("[P]"));
            Assert.IsTrue(result.Contains("[F]"));
        }
    }
}
