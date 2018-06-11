using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GreenUtil.Crypto
{
    /// <summary>
    /// Classe para lógicas relacionadas a hashing
    /// </summary>
    public static partial class HashUtil
    {
        /// <summary>
        /// Calcula o SHA1 de uma <see cref="string"/>
        /// </summary>
        /// <param name="source"><see cref="string"/> a ter seu SHA1 calculado</param>
        /// <param name="encoding"><see cref="Encoding"/>a ser utilizado para o cálculo do hash</param>
        /// <returns>SHA1 calculado considerando o <see cref="Encoding"/> informado</returns>
        public static string ToSHA1(this string source, Encoding encoding = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (encoding == null)
                encoding = Encoding.Default;

            return ToSHA1(encoding.GetBytes(source));
        }

        /// <summary>
        /// Calcula o SHA1 de uma <see cref="File"/>
        /// </summary>
        /// <param name="file"><see cref="File"/> arquivo a ter seu SHA1 calculado</param>
        /// <returns>SHA1 calculado</returns>
        public static string ToSHA1(this FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            return ToSHA1(File.ReadAllBytes(file.FullName));
        }

        /// <summary>
        /// Calcula o SHA1 de um array de <see cref="byte"/>
        /// </summary>
        /// <param name="data"> array de <see cref="byte"/> a ter seu SHA1 calculado</param>
        /// <returns>SHA1 calculado</returns>
        public static string ToSHA1(this byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (SHA1 sha1 = SHA1.Create())
            {
                StringBuilder sb = new StringBuilder();

                foreach (byte b in sha1.ComputeHash(data))
                    sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }
    }
}
