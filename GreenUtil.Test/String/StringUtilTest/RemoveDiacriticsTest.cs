using GreenUtil.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.String.StringUtilTest
{
    [TestClass]
    public class RemoveDiacriticsTest
    {
        [TestMethod]
        public void WhenDiacriticStringIsPresentedThenShouldReturnNonDiacritcString()
        {
            string sanitizedString = StringUtil.RemoveDiacritics("áâãàÁÂÃÀª@éèêÉÈÊúùûÚÙÛíìîÍÌÎóõôòÓÒÕÔçÇ");

            Assert.AreEqual("aaaaAAAAª@eeeEEEuuuUUUiiiIIIooooOOOOcC", sanitizedString);
        }

        [TestMethod]
        public void WhenNullStringIsPresentedThenShouldThrowArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => StringUtil.RemoveDiacritics(null));
        }


        [TestMethod]
        public void WhenEmptyStringIsPresentedThenShouldReturnEmptyString()
        {
            string sanitizedString = StringUtil.RemoveDiacritics(string.Empty);

            Assert.AreEqual(string.Empty, sanitizedString);
        }
    }
}
