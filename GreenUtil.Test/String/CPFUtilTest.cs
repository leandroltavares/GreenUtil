using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreenUtil.Test.String
{
    /// <summary>
    /// Testes da classe <see cref="CPFUtil"/>
    /// </summary>
    [TestClass]
    public class CPFUtilTest
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("53323471032")]
        [DataRow("53323471032")]
        [DataRow("24806751570")]
        [DataRow("73199127240")]
        [DataRow("83819895906")]
        [DataRow("36153464599")]
        [DataRow("40545851831")]
        [DataRow("59311249343")]
        [DataRow("37832312050")]
        [DataRow("21284567460")]
        public void WhenValidCPFWithoutMaskThenShouldReturnTrue(string cpf)
        {
            //Act
            bool result = CPFUtil.ValidateCPF(cpf);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("508.227.771-50")]
        [DataRow("776.714.664-25")]
        [DataRow("135.465.483-88")]
        [DataRow("283.706.556-02")]
        [DataRow("176.674.393-54")]
        [DataRow("573.564.236-77")]
        [DataRow("615.453.331-49")]
        [DataRow("718.141.876-93")]
        [DataRow("832.077.252-44")]
        [DataRow("305.356.268-51")]
        public void WhenValidCPFWithMaskThenShouldReturnTrue(string cpf)
        {
            //Act
            bool result = CPFUtil.ValidateCPF(cpf);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("23323471031")]
        [DataRow("24806751571")]
        [DataRow("73199127241")]
        [DataRow("83819895901")]
        [DataRow("36153464591")]
        [DataRow("40545851811")]
        [DataRow("59311249341")]
        [DataRow("37832312051")]
        [DataRow("21284567461")]
        [DataRow("11111111111")]
        [DataRow("22222222222")]
        [DataRow("33333333333")]
        [DataRow("44444444444")]
        [DataRow("55555555555")]
        [DataRow("66666666666")]
        [DataRow("77777777777")]
        [DataRow("88888888888")]
        [DataRow("99999999999")]
        [DataRow("00000000000")]
        public void WhenInvalidCPFWithoutMaskThenShouldReturnFalse(string cpf)
        {
            //Act
            bool result = CPFUtil.ValidateCPF(cpf);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("233.234.710-31")]
        [DataRow("248.067.515-71")]
        [DataRow("731.991.272-41")]
        [DataRow("838.198.959-01")]
        [DataRow("361.534.645-91")]
        [DataRow("405.458.518-11")]
        [DataRow("593.112.493-41")]
        [DataRow("378.323.120-51")]
        [DataRow("212.845.674-61")]
        [DataRow("111.111.111-11")]
        [DataRow("222.222.222-22")]
        [DataRow("333.333.333-33")]
        [DataRow("444.444.444-44")]
        [DataRow("555.555.555-55")]
        [DataRow("666.666.666-66")]
        [DataRow("777.777.777-77")]
        [DataRow("888.888.888-88")]
        [DataRow("999.999.999-99")]
        [DataRow("000.000.000-00")]
        public void WhenInvalidCPFWithMaskThenShouldReturnFalse(string cpf)
        {
            //Act
            bool result = CPFUtil.ValidateCPF(cpf);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenEmptyCPFThenShouldReturnFalse()
        {
            //Act
            bool result = CPFUtil.ValidateCPF(string.Empty);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenNullCPFThenShouldThrowException()
        {
            //Act
            Assert.ThrowsException<ArgumentNullException>(() => CPFUtil.ValidateCPF(null));
        }

        [TestMethod]
        public void WhenOnlyMaskCPFThenShouldReturnFalse()
        {
            //Act
            bool result = CPFUtil.ValidateCPF("..-");

            //Assert
            Assert.IsFalse(result);
        }
    }
}
