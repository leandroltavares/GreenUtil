using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.String
{
    [TestClass]
    public class RGUtilTest
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("162551381")]
        [DataRow("231348666")]
        [DataRow("462344897")]
        [DataRow("471518013")]
        [DataRow("500038016")]
        [DataRow("404535124")]
        [DataRow("236184428")]
        [DataRow("341807667")]
        [DataRow("167356173")]
        [DataRow("187582609")]
        [DataRow("42387665X")]
        public void WhenValidRGWithoutMaskThenShouldReturnTrue(string rg)
        {
            //Act
            bool result = RGUtil.ValidateRG(rg);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("40.633.815-2")]
        [DataRow("36.750.477-7")]
        [DataRow("27.240.211-4")]
        [DataRow("30.316.554-6")]
        [DataRow("29.001.829-8")]
        [DataRow("15.932.165-7")]
        [DataRow("12.924.925-7")]
        [DataRow("27.185.748-1")]
        [DataRow("40.845.167-1")]
        [DataRow("21.016.502-9")]
        [DataRow("42.387.665-X")]
        public void WhenValidRGWithMaskThenShouldReturnTrue(string rg)
        {
            //Act
            bool result = RGUtil.ValidateRG(rg);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("483178001")]
        [DataRow("194031832")]
        [DataRow("19403183X")]
        [DataRow("160506209")]
        [DataRow("152875605")]
        [DataRow("189070825")]
        [DataRow("42934509X")]
        [DataRow("180204720")]
        [DataRow("28595288X")]
        [DataRow("111111111")]
        [DataRow("222222222")]
        [DataRow("333333333")]
        [DataRow("444444444")]
        [DataRow("555555555")]
        [DataRow("666666666")]
        [DataRow("777777777")]
        [DataRow("888888888")]
        [DataRow("999999999")]
        [DataRow("000000000")]
        public void WhenInvalidRGWithoutMaskThenShouldReturnFalse(string rg)
        {
            //Act
            bool result = RGUtil.ValidateRG(rg);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("48.317.800-1")]
        [DataRow("19.403.183-2")]
        [DataRow("19.403.183-X")]
        [DataRow("16.050.620-9")]
        [DataRow("15.287.560-5")]
        [DataRow("18.907.082-5")]
        [DataRow("42.934.509-X")]
        [DataRow("18.020.472-0")]
        [DataRow("28.595.288-X")]
        [DataRow("11.111.111-1")]
        [DataRow("22.222.222-2")]
        [DataRow("33.333.333-3")]
        [DataRow("44.444.444-4")]
        [DataRow("55.555.555-5")]
        [DataRow("66.666.666-6")]
        [DataRow("77.777.777-7")]
        [DataRow("88.888.888-8")]
        [DataRow("99.999.999-9")]
        [DataRow("00.000.000-0")]
        public void WhenInvalidRGWithMaskThenShouldReturnFalse(string rg)
        {
            //Act
            bool result = RGUtil.ValidateRG(rg);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenEmptyCPFThenShouldReturnFalse()
        {
            //Act
            bool result = RGUtil.ValidateRG(string.Empty);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenNullCPFThenShouldReturnFalse()
        {
            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => RGUtil.ValidateRG(null));
        }

        [TestMethod]
        public void WhenOnlyMaskCPFThenShouldReturnFalse()
        {
            //Act
            bool result = RGUtil.ValidateRG("..-");

            //Assert
            Assert.IsFalse(result);
        }
    }
}
