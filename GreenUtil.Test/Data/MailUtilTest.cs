using GreenUtil.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.Test.Data
{
    [TestClass]
    public class MailUtilTest
    {
        [TestMethod]
        public void WhenValidMailAddressThenShouldReturnTrue()
        {
            string mailAddress = "leandro.tavares@greenconcept.com.br";

            bool valid = MailUtil.ValidateAddress(mailAddress);

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void WhenInValidMailAddressThenShouldReturnTrue()
        {
            string mailAddress = "leandro.tavares@greenconcept";

            bool valid = MailUtil.ValidateAddress(mailAddress);

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void WhenEmptyMailAddressThenShouldThrowArgumentNullException()
        {
            string mailAddress = string.Empty;

            Assert.ThrowsException<ArgumentNullException>(() => MailUtil.ValidateAddress(mailAddress));
        }

        [TestMethod]
        public void WhenNullMailAddressThenShouldThrowArgumentNullException()
        {
            string mailAddress = null;

            Assert.ThrowsException<ArgumentNullException>(() => MailUtil.ValidateAddress(mailAddress));
        }

        [TestMethod]
        public void WhenMailMessageIsProvidedThenShouldSaveEmlFile()
        {

            MailMessage message = new MailMessage();
            message.From = new MailAddress("leandro.ltavares@gmail.com");
            message.To.Add(new MailAddress("leandro.ltavares@gmail.com"));

            string folderPath = "\\Mail";

            string emlPath = MailUtil.SaveEml(message, folderPath);

            FileInfo fileInfo = new FileInfo(emlPath);

            Assert.AreNotEqual(0, fileInfo.Length);

            Directory.Delete(folderPath, true);
        }

        [TestMethod]
        public void WhenMailMessageIsNullThenShouldThrowException()
        {
            string folderPath = "\\Mail";

            Assert.ThrowsException<ArgumentNullException>(() => MailUtil.SaveEml(null, folderPath));
        }

        [TestMethod]
        public void WhenPathIsNullThenShouldThrowException()
        {
            MailMessage message = new MailMessage();

            Assert.ThrowsException<ArgumentNullException>(() => MailUtil.SaveEml(message, null));
        }
    }
}
