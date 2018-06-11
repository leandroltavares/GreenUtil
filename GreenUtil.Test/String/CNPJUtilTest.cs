using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.String
{
    /// <summary>
    /// Testes da classe <see cref="CNPJUtil"/>
    /// </summary>
    [TestClass]
    public class CNPJUtilTest
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("85813011000143")]
        [DataRow("57862351000129")]
        [DataRow("40147477000119")]
        [DataRow("95286540000190")]
        [DataRow("63372704000105")]
        [DataRow("85001833000120")]
        [DataRow("05171463000130")]
        [DataRow("92379756000101")]
        [DataRow("86810837000111")]
        [DataRow("38795692000184")]
        public void WhenValidCNPJWithoutMaskThenShouldReturnTrue(string cnpj)
        {
            //Act
            bool result = CNPJUtil.ValidateCNPJ(cnpj);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("08.661.165/0001-16")]
        [DataRow("55.705.256/0001-96")]
        [DataRow("58.302.800/0001-47")]
        [DataRow("14.683.673/0001-06")]
        [DataRow("51.901.769/0001-11")]
        [DataRow("33.608.761/0001-80")]
        [DataRow("12.330.983/0001-03")]
        [DataRow("36.532.351/0001-90")]
        [DataRow("11.607.056/0001-25")]
        [DataRow("71.665.843/0001-55")]
        public void WhenValidCNPJWithMaskThenShouldReturnTrue(string cnpj)
        {
            //Act
            bool result = CNPJUtil.ValidateCNPJ(cnpj);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("75508142000164")]
        [DataRow("75508142000153")]
        [DataRow("75208142000163")]
        [DataRow("71508142000163")]
        [DataRow("60102972010119")]
        [DataRow("50102972000118")]
        [DataRow("80901585000204")]
        [DataRow("80909585000104")]
        [DataRow("56123541010191")]
        [DataRow("11111111111111")]
        [DataRow("22222222222222")]
        [DataRow("33333333333333")]
        [DataRow("44444444444444")]
        [DataRow("55555555555555")]
        [DataRow("66666666666666")]
        [DataRow("77777777777777")]
        [DataRow("88888888888888")]
        [DataRow("99999999999999")]
        [DataRow("00000000000000")]
        public void WhenInvalidCNPJWithoutMaskThenShouldReturnFalse(string cnpj)
        {
            //Act
            bool result = CPFUtil.ValidateCPF(cnpj);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("75.508.142/0001-64")]
        [DataRow("75.508.142/0001-53")]
        [DataRow("75.208.142/0001-63")]
        [DataRow("71.508.142/0001-63")]
        [DataRow("60.102.972/0101-19")]
        [DataRow("50.102.972/0001-18")]
        [DataRow("80.901.585/0002-04")]
        [DataRow("80.909.525/0001-04")]
        [DataRow("56.123.541/0101-91")]
        [DataRow("11.111.111/1111-11")]
        [DataRow("22.222.222/2222-22")]
        [DataRow("33.333.333/3333-33")]
        [DataRow("44.444.444/4444-44")]
        [DataRow("55.555.555/5555-55")]
        [DataRow("66.666.666/6666-66")]
        [DataRow("77.777.777/7777-77")]
        [DataRow("88.888.888/8888-88")]
        [DataRow("99.999.999/9999-99")]
        [DataRow("00.000.000/0000-00")]
        public void WhenInvalidCNPJWithMaskThenShouldReturnFalse(string cnpj)
        {
            //Act
            bool result = CNPJUtil.ValidateCNPJ(cnpj);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenEmptyCNPJThenShouldReturnFalse()
        {
            //Act
            bool result = CNPJUtil.ValidateCNPJ(string.Empty);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenNullCNPJThenShouldThrowArgumentNullException()
        {
            //Assert
            Assert.ThrowsException<ArgumentNullException>(() => CNPJUtil.ValidateCNPJ(null));
        }

        [TestMethod]
        public void WhenOnlyMaskCNPJThenShouldReturnFalse()
        {
            //Act
            bool result = CNPJUtil.ValidateCNPJ("..-/");

            //Assert
            Assert.IsFalse(result);
        }
    }
}
