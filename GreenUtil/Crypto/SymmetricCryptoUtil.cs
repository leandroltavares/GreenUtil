using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GreenUtil.Crypto
{
    /// <summary>
    /// Classe para lógicas relacionada a criptografia simétrica
    /// </summary>
    public static class SymmetricCryptoUtil
    {
        /// <summary>
        /// Método para criptografar uma <see cref="string"/>
        /// </summary>
        /// <param name="text"><see cref="string"/> a ser criptografado</param>
        /// <param name="password">Chave de criptografia</param>
        /// <param name="salt">Sal da criprografia</param>
        /// <returns><see cref="string"/> criptografado</returns>
        public static string EncryptText(string text, string password, string salt)
        {
            var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), password, salt);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Método para descriptografar uma <see cref="string"/>
        /// </summary>
        /// <param name="text"><see cref="string"/> a ser descriptografado</param>
        /// <param name="password">Chave de criptografia</param>
        /// <param name="salt">Sal da criprografia</param>
        /// <returns><see cref="string"/> descriptografado</returns>
        public static string DecryptText(string text, string password, string salt)
        {
            var decrypted = Decrypt(Convert.FromBase64String(text), password, salt);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// Método para criptografar um array de <see cref="byte"/>
        /// </summary>
        /// <param name="data">Array de <see cref="byte"/> a ser criptografado</param>
        /// <param name="password">Chave de criptografia</param>
        /// <param name="salt">Sal da criprografia</param>
        /// <returns>Array de <see cref="byte"/> criptografado</returns>
        public static byte[] Encrypt(byte[] data, string password, string salt)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            if (string.IsNullOrWhiteSpace(salt))
                throw new ArgumentNullException(nameof(salt));

            var aesAlg = CreateRijndaelManaged(password, salt);

            var encryptor = aesAlg.CreateEncryptor();
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                }

                return msEncrypt.ToArray();
            }
        }

        /// <summary>
        /// Método para descriptografar um array de <see cref="byte"/>
        /// </summary>
        /// <param name="data">Array de <see cref="byte"/> descriptografado</param>
        /// <param name="password">Chave de criptografia</param>
        /// <param name="salt">Sal da criprografia</param>
        /// <returns>Array de <see cref="byte"/> descriptografado</returns>
        public static byte[] Decrypt(byte[] data, string password, string salt)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            if (string.IsNullOrWhiteSpace(salt))
                throw new ArgumentNullException(nameof(salt));

            var aesAlg = CreateRijndaelManaged(password, salt);
            var decryptor = aesAlg.CreateDecryptor();

            using (var msDecrypt = new MemoryStream())
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                {
                    csDecrypt.Write(data, 0, data.Length);
                }

                return msDecrypt.ToArray();
            }
        }

        /// <summary>
        /// Cria um <see cref="RijndaelManaged"/> dado uma chave e um sal
        /// </summary>
        /// <param name="password">Chave de criptorgrafia</param>
        /// <param name="salt">Sal da criptografia</param>
        /// <returns></returns>
        private static RijndaelManaged CreateRijndaelManaged(string password, string salt)
        {
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            var key = new Rfc2898DeriveBytes(password, saltBytes);

            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            return aesAlg;
        }
    }
}
