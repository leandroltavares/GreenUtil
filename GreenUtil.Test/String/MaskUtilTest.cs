using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

//https://docs.microsoft.com/pt-br/dotnet/standard/base-types/custom-numeric-format-strings

namespace GreenUtil.Test.String
{
    [TestClass]
    public class MaskUtilTest
    {
        [TestMethod]
        public void WhenSourceStringIsNullThenToMaskedStringShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => MaskUtil.ToMaskedString(null, "#"));
        }

        [TestMethod]
        public void WhenFormatMaskStringIsNullThenToMaskedStringShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => MaskUtil.ToMaskedString(null, "#"));
        }
        [TestMethod]
        public void WhenSourceStringIsNullThenToMaskedStringShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => MaskUtil.ToMaskedString(null, StringMask.CNPJ));
        }

        [TestMethod]
        public void WhenFormatMaskStringAndSourceStringAreDifferentLengthThenToMaskedStringShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => MaskUtil.ToMaskedString("00000", "#"));
        }



        [DataTestMethod]
        [DataRow("000", "#.#.#", "0.0.0")]
        [DataRow("ABC", "#.#.#", "A.B.C")]
        [DataRow("000", "", "000")]
        [DataRow("ABC", "", "ABC")]
        [DataRow("000", "###", "000")]
        [DataRow("ABC", "###", "ABC")]
        [DataRow("", "", "")]
        [DataRow(" ", "", " ")]

        public void WhenFormatMaskStringAndSourceStringAreValidThenToMaskedStringShouldReturnMaskedString(string source, string formatMask, string targetMaskedString)
        {
            string maskedString = MaskUtil.ToMaskedString(source, formatMask);

            Assert.AreEqual(targetMaskedString, maskedString);
        }

        [DataTestMethod]
        [DataRow("00000000000", "000.000.000-00")]
        [DataRow("11111111111", "111.111.111-11")]
        [DataRow("AAAAAAAAAAA", "AAA.AAA.AAA-AA")]
        [DataRow("ZZZZZZZZZZZ", "ZZZ.ZZZ.ZZZ-ZZ")]

        public void WhenFormatMaskIsCPFThenToMaskedStringShouldReturnMaskedString(string source, string targetMaskedString)
        {
            string maskedString = MaskUtil.ToMaskedString(source, StringMask.CPF);

            Assert.AreEqual(targetMaskedString, maskedString);
        }

        [DataTestMethod]
        [DataRow("00000000000000", "00.000.000/0000-00")]
        [DataRow("11111111111111", "11.111.111/1111-11")]
        [DataRow("AAAAAAAAAAAAAA", "AA.AAA.AAA/AAAA-AA")]
        [DataRow("ZZZZZZZZZZZZZZ", "ZZ.ZZZ.ZZZ/ZZZZ-ZZ")]

        public void WhenFormatMaskIsCNPJThenToMaskedStringShouldReturnMaskedString(string source, string targetMaskedString)
        {
            string maskedString = MaskUtil.ToMaskedString(source, StringMask.CNPJ);

            Assert.AreEqual(targetMaskedString, maskedString);
        }

        [DataTestMethod]
        [DataRow("00000000", "00000-000")]
        [DataRow("11111111", "11111-111")]
        [DataRow("AAAAAAAA", "AAAAA-AAA")]
        [DataRow("ZZZZZZZZ", "ZZZZZ-ZZZ")]

        public void WhenFormatMaskIsCEPThenToMaskedStringShouldReturnMaskedString(string source, string targetMaskedString)
        {
            string maskedString = MaskUtil.ToMaskedString(source, StringMask.CEP);

            Assert.AreEqual(targetMaskedString, maskedString);
        }

        [DataTestMethod]
        [DataRow("000000000", "00.000.000-0")]
        [DataRow("111111111", "11.111.111-1")]
        [DataRow("AAAAAAAAA", "AA.AAA.AAA-A")]
        [DataRow("ZZZZZZZZZ", "ZZ.ZZZ.ZZZ-Z")]

        public void WhenFormatMaskIsRGThenToMaskedStringShouldReturnMaskedString(string source, string targetMaskedString)
        {
            string maskedString = MaskUtil.ToMaskedString(source, StringMask.RG);

            Assert.AreEqual(targetMaskedString, maskedString);
        }

    }
}
