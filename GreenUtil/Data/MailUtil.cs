using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GreenUtil.Data
{
    /// <summary>
    /// Classe para lógicas relacioandas a e-mail
    /// </summary>
    public static class MailUtil
    {
        /// <summary>
        /// Salva a mensagem enviada por e-mail como um arquivo .eml
        /// </summary>
        /// <param name="message"><see cref="MailMessage"/> a ser salva</param>
        /// <param name="folderPath">Diretório de destino</param>
        /// <returns>Caminho completo do arquivo da mensagem</returns>
        public static string SaveEml(this MailMessage message, string folderPath)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (folderPath == null)
                throw new ArgumentNullException(nameof(folderPath));

            using (var client = new SmtpClient())
            {
                var diretorioTemporario = Path.Combine(folderPath, Guid.NewGuid().ToString());

                if (!Directory.Exists(diretorioTemporario))
                {
                    Directory.CreateDirectory(diretorioTemporario);
                }

                client.UseDefaultCredentials = true;
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = diretorioTemporario;
                client.Send(message);

                return Directory.GetFiles(diretorioTemporario).Single();
            }
        }

        /// <summary>
        /// Valida um endereço de e-mail
        /// </summary>
        /// <param name="mailAddress">Endereço a ser validado</param>
        /// <returns>Verdadeiro se válido, falso caso contrário</returns>
        public static bool ValidateAddress(this string mailAddress)
        {
            if (string.IsNullOrEmpty(mailAddress))
                throw new ArgumentNullException(nameof(mailAddress));

            var expression = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            var regex = new Regex(expression);
            var match = regex.Match(mailAddress);
            return match.Success;
        }
    }
}
