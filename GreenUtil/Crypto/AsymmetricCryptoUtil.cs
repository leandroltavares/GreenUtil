using System;
using System.Security.Cryptography;
using System.Text;

namespace GreenUtil.Crypto
{
    /// <summary>
    /// Classe para lógicas relacionada a criptografia assimétrica
    /// </summary>
    public class AsymmetricCryptoUtil
    {
        /// <summary>
        /// Gera um par de chaves dado o tamanho da chave, retornando as chave como XML
        /// </summary>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicKey">Chave pública</param>
        /// <param name="publicAndPrivateKey">Chave privada</param>
        public static void GenerateKeys(int keySize, out string publicKey, out string publicAndPrivateKey)
        {
            if (!IsKeySizeValid(keySize))
                throw new ArgumentOutOfRangeException("Key size is not valid", nameof(keySize));

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                publicKey = provider.ToXmlString(false);
                publicAndPrivateKey = provider.ToXmlString(true);
            }
        }


        /// <summary>
        /// Gera um par de chaves dado o tamanho da chave, retornando as chave como os <see cref="RSAParameters"/>
        /// </summary>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicKey">Chave pública</param>
        /// <param name="publicAndPrivateKey">Chave privada</param>
        public static void GenerateKeys(int keySize, out RSAParameters publicKey, out RSAParameters publicAndPrivateKey)
        {
            if (!IsKeySizeValid(keySize))
                throw new ArgumentOutOfRangeException("Key size is not valid", nameof(keySize));

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                publicKey = provider.ExportParameters(false);
                publicAndPrivateKey = provider.ExportParameters(true);
            }
        }

        /// <summary>
        /// Método pra criptografar um texto
        /// </summary>
        /// <param name="text"><see cref="string"/> a ser criptografada</param>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicKeyXml">Chave publica</param>
        /// <returns>Texto encriptado</returns>
        public static string EncryptText(string text, int keySize, string publicKeyXml)
        {
            var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), keySize, publicKeyXml);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Método pra criptografar um texto
        /// </summary>
        /// <param name="text"><see cref="string"/> a ser criptografada</param>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicKey">Chave pública</param>
        /// <returns>Texto encriptado</returns>
        public static string EncryptText(string text, int keySize, RSAParameters publicKey)
        {
            var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), keySize, publicKey);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Método para criptografar um array de <see cref="byte"/>
        /// </summary>
        /// <param name="data">Array de <see cref="byte"/> a ser criptografada</param>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicKeyXml">Chave pública</param>
        /// <returns>Array de <see cref="byte"/> criptografado</returns>
        public static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
        {
            if (data == null || data.Length == 0) 
                throw new ArgumentNullException(nameof(data));

            int maxLength = GetMaxDataLength(keySize);

            if (data.Length > maxLength)
                throw new ArgumentOutOfRangeException(string.Format("Maximum data length is {0}", maxLength), "data");


            if (string.IsNullOrEmpty(publicKeyXml))
                throw new ArgumentNullException(nameof(data));

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicKeyXml);
                return provider.Encrypt(data, true);
            }
        }

        /// <summary>
        /// Método para criptografar um array de <see cref="byte"/>
        /// </summary>
        /// <param name="data">Array de <see cref="byte"/> a ser criptografada</param>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicKey">Chave pública</param>
        /// <returns>Array de <see cref="byte"/> criptografado</returns>
        public static byte[] Encrypt(byte[] data, int keySize, RSAParameters publicKey)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("Data are empty", "data");

            int maxLength = GetMaxDataLength(keySize);

            if (data.Length > maxLength)
                throw new ArgumentOutOfRangeException(string.Format("Maximum data length is {0}", maxLength), "data");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.ImportParameters(publicKey);
                return provider.Encrypt(data, true);
            }
        }

        /// <summary>
        /// Método para descriptografar um texto
        /// </summary>
        /// <param name="text">Texto criptografado</param>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicAndPrivateKeyXml">Chave privada</param>
        /// <returns>Texto descriptografado</returns>
        public static string DecryptText(string text, int keySize, string publicAndPrivateKeyXml)
        {
            var decrypted = Decrypt(Convert.FromBase64String(text), keySize, publicAndPrivateKeyXml);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// Método para descriptografar um texto
        /// </summary>
        /// <param name="text">Texto criptografado</param>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicAndPrivateKey">Chave privada</param>
        /// <returns>Texto descriptografado</returns>
        public static string DecryptText(string text, int keySize, RSAParameters publicAndPrivateKey)
        {
            var decrypted = Decrypt(Convert.FromBase64String(text), keySize, publicAndPrivateKey);
            return Encoding.UTF8.GetString(decrypted);
        }

        /// <summary>
        /// Método para descriptografar um array de <see cref="byte"/>
        /// </summary>
        /// <param name="data">Dados criptografados</param>
        /// <param name="keySize">Tamanho da chave</param>
        /// <param name="publicAndPrivateKeyXml">Chave privada</param>
        /// <returns>Texto descriptografado</returns>
        public static byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("Data are empty", "data");

            if (!IsKeySizeValid(keySize))
                throw new ArgumentOutOfRangeException("Key size is not valid", "keySize");

            if (string.IsNullOrEmpty(publicAndPrivateKeyXml))
                throw new ArgumentNullException("Key is null or empty", "publicAndPrivateKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicAndPrivateKeyXml);
                return provider.Decrypt(data, true);
            }
        }

        /// <summary>
        /// Descriptografar um contéudo fornecido em forma de array de <see cref="byte"/>
        /// </summary>
        /// <param name="data">Dados a serem descriptografado</param>
        /// <param name="keySize">Taamnho da chave</param>
        /// <param name="publicAndPrivateKey"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] data, int keySize, RSAParameters publicAndPrivateKey)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("Data are empty", "data");

            if (!IsKeySizeValid(keySize))
                throw new ArgumentOutOfRangeException("Key size is not valid", "keySize");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.ImportParameters(publicAndPrivateKey);
                return provider.Decrypt(data, true);
            }
        }

        /// <summary>
        /// Obtem o tamanho máximo dos dados a serem criptografados com base na chave
        /// </summary>
        /// <param name="keySize">Tamanho da chave</param>
        /// <returns>O tamanho dos dados</returns>
        public static int GetMaxDataLength(int keySize)
        {
            if (!IsKeySizeValid(keySize))
                throw new ArgumentOutOfRangeException(nameof(keySize), "The keysize must be between 384 and 16384 and multiple of 8.");

            return ((keySize - 384) / 8) + 7;
        }

        /// <summary>
        /// Determina se o tamanho de uma chave é válida
        /// </summary>
        /// <param name="keySize">Tamanho da chave</param>
        /// <returns>Verdadeiro se o tamanho da chave é válido, faso caso contr
        /// ário</returns>
        public static bool IsKeySizeValid(int keySize)
        {
            return keySize >= 384 && keySize <= 16384 && keySize % 8 == 0;
        }
    }
}
