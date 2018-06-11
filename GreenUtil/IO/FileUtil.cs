using System;
using System.IO;
using System.Text;

namespace GreenUtil.IO
{
    /// <summary>
    /// Classe para lógicas relacionadas a <see cref="File"/>
    /// </summary>
    public static class FileUtil
    {
        /// <summary>
        /// Salva um <see cref="Stream"/> em arquivo
        /// </summary>
        /// <param name="stream">O <see cref="Stream"/> original</param>
        /// <param name="filePath">O caminho de destino a salvar os dados do stream</param>
        public static void Save(this Stream stream, string filePath)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            using (FileStream fileStream = File.Create(filePath, (int)stream.Length))
            {
                byte[] bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);

                fileStream.Write(bytes, 0, bytes.Length);
            }
        }


        /// <summary>
        /// Determina o Encoding de um documento com base no BOM do arquivo
        /// </summary>
        /// <param name="path">Caminho do arquivo</param>
        /// <returns>O enconding do arquivo informado com base no BOM, se não for possível retorna o Enconding padrão</returns>
        public static Encoding GetEncodingFromBOM(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));

            // Read the BOM
            var bom = new byte[4];

            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0XFE && bom[1] == 0XFF) return Encoding.BigEndianUnicode; //Gets an encoding for the UTF-16 format that uses the big endian byte order. (UTF-16BE)
            if (bom[0] == 0XEF && bom[1] == 0XBB && bom[2] == 0XBF) return Encoding.UTF8; //Gets an encoding for the UTF-8 format.
            if (bom[0] == 0x2B && bom[1] == 0X2F && bom[2] == 0x76) return Encoding.UTF7; // Gets an encoding for the UTF-7 format            
            if (bom[0] == 0X00 && bom[1] == 0X00 && bom[2] == 0XFE && bom[3] == 0XFF) return new UTF32Encoding(true, true); //Gets an encoding for the UTF-32 format using the big endian byte order. (UTF-32BE)
            if (bom[0] == 0XFF && bom[1] == 0XFE && bom[2] == 0X00 && bom[3] == 0X00) return Encoding.UTF32; //Gets an encoding for the UTF-32 format using the little endian byte order. (UTF-32LE)
            if (bom[0] == 0XFF && bom[1] == 0XFE) return Encoding.Unicode; //Gets an encoding for the UTF-16 format using the little endian byte order. (UTF-16LE)

            return Encoding.Default;
        }

        /// <summary>
        /// Determina se o arquivo está disponível para uso exclusivo (escrita e leitura)
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <returns>Verdadeiro se arquivo está disponível, falso caso contrário</returns>
        public static bool IsFileAvailable(this string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            FileStream stream = null;

            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return true;
        }

        /// <summary>
        /// Remove caracteres inválidos do caminho e nome do arquivo
        /// </summary>
        /// <param name="filePath">Caminho do arquivo com caracteres inválidos</param>
        /// <returns>Caminho do arquivo sem caracteres inválidos</returns>
        public static string ReplaceInvalidPathChars(this string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                filePath = filePath.Replace(c.ToString(), string.Empty);
            }

            return filePath;
        }

        /// <summary>
        /// Obtém o tamanho de arquivo em na magnitude informada
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <param name="magnitude">Magnitude informada</param>
        /// <returns></returns>
        public static decimal GetFileSize(string filePath, Magnitude magnitude = Magnitude.MB)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            return (decimal)new FileInfo(filePath).Length / (1 << ((int)magnitude * 10));
        }
    }

    public enum Magnitude : int
    {
        KB = 1,
        MB = 2,
        GB = 3,
        TB = 4,
        PB = 5,
        EB = 6,
        ZB = 7,
        YB = 8
    }
}
