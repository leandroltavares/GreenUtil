using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace GreenUtil.String
{
    /// <summary>
    /// Classe para lógicas relacionadas a datas <see cref="string"/>
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// Método para deixar apenas os caracteres numéricos de uma <see cref="string"/>
        /// </summary>
        /// <param name="source"><see cref="string"/> a ser limpa</param>
        /// <returns><see cref="string"/> com somente os caracteres numéricos</returns>
        public static string OnlyNumbers(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new string(source.Where(Char.IsDigit).ToArray());
        }

        /// <summary>
        /// Método para deixar apenas os caracteres alfanuméricos de uma <see cref="string"/>
        /// </summary>
        /// <param name="source"><see cref="string"/> a ser limpa</param>
        /// <returns><see cref="string"/> com somente os caracteres alfanuméricos</returns>
        public static string OnlyAlphaNumeric(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return new string(source.Where(Char.IsLetterOrDigit).ToArray());
        }

        /// <summary>
        /// Método para deixar a primeira letra de cada palavra de uma <see cref="String"/> maíuscula. Ex. green concept, torna-se Green Concept
        /// </summary>
        /// <param name="source"><see cref="string"/> a ter a primeira letra de cada palavra definida como maíuscula</param>
        /// <returns></returns>
        public static string ToTitleCase(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(source);
        }

        /// <summary>
        /// Método para exibir os nomes e conteúdo das propriedades e atributos de um objeto dinâmicamente
        /// </summary>
        /// <param name="instance"><see cref="object"/> a ter seus atributos e propriedades exibidos</param>
        /// <param name="includeProperties">Verdadeiro para incluir as propriedades na exibição, falso caso contrário</param>
        /// <param name="includeMembers">Verdadeiro para incluir os atributos na exibição, falso caso contrário</param>
        /// <param name="includeDeclaredOnly">Verdadeiro para incluir apenas os membros declarado na instância passada como parametro, falso para incluir os membros herdados</param>
        /// <returns></returns>
        public static string ToString(this object instance, bool includeProperties = true, bool includeMembers = false, bool includeDeclaredOnly = true)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            var type = instance.GetType();

            if (type.IsPrimitive || type == typeof(string))
                return instance.ToString();

            StringBuilder sb = new StringBuilder();

            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            if (includeDeclaredOnly)
                bindingFlags = bindingFlags | BindingFlags.DeclaredOnly;

            if (includeProperties)
            {
                foreach (var property in type.GetProperties(bindingFlags))
                {
                    var name = property.Name;
                    var value = property.GetValue(instance)?.ToString() ?? "<NULL>";

                    sb.Append($"[P]{name}: {value} ");
                }
            }

            if (includeMembers)
            {
                foreach (var field in type.GetFields(bindingFlags))
                {
                    var name = field.Name;
                    var value = field.GetValue(instance)?.ToString() ?? "<NULL>";

                    sb.Append($"[F]{name}: {value} ");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Método para remover diacríticos (símbolos como acentos, cedilhas, tils, etc) de <see cref="string"/>
        /// </summary>
        /// <param name="source"><see cref="string"/> original</param>
        /// <returns><see cref="string"/> com somente os caracteres sem diacríticos</returns>
        public static string RemoveDiacritics(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));


            source = source.Normalize(NormalizationForm.FormD);
            var chars = source.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();

            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Método para verificar se uma string está em formado Base64
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsBase64(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source = source.Trim();

            return (source.Length % 4 == 0) && Regex.IsMatch(source, @"^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$", RegexOptions.None);
        }

        /// <summary>
        /// Método para converter um string para Base64 
        /// </summary>
        /// <param name="source"><see cref="string"/> a ser convertida</param>
        /// <param name="encoding"><see cref="Encoding"/> a ser utilizado na conversão</param>
        /// <returns><see cref="string"/> codificada em Base64</returns>
        public static string ToBase64(this string source, Encoding encoding = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (encoding == null)
                encoding = Encoding.Default;

            return Convert.ToBase64String(encoding.GetBytes(source));
        }

        /// <summary>
        /// Método para converter uma string a partir de Base64
        /// </summary>
        /// <param name="source"><see cref="string"/> a ser desconvertida</param>
        /// <param name="encoding"><see cref="Encoding"/> a ser utilizado na conversão</param>
        /// <returns><see cref="string"/> decodificada a partir Base64</returns>
        public static string FromBase64(this string source, Encoding encoding = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (encoding == null)
                encoding = Encoding.Default;

            return encoding.GetString(Convert.FromBase64String(source));
        }

        /// <summary>
        /// Método para converter uma string para uma string em forma Hexadecimal (base16)
        /// </summary>
        /// <param name="source"><see cref="string"/> a ser convertida</param>
        /// <param name="encoding"><see cref="Encoding"/> a ser utilizado para conversão</param>
        /// <returns><see cref="string"/> convertida a para hexadecimal</returns>
        public static string ToHex(this string source, Encoding encoding = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (encoding == null)
                encoding = Encoding.Default;

            byte[] ba = encoding.GetBytes(source);

            return BitConverter.ToString(ba);
        }


        /// <summary>
        /// Método para converter uma string de formato Hexadecimal (base16) para uma string
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"><see cref="Encoding"/> a ser utilizado para conversão</param>
        /// <returns><see cref="string"/> convertida a partir de hexadecimal</returns>
        public static string FromHex(this string source, Encoding encoding = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source = source.Replace("-", "");

            byte[] raw = new byte[source.Length / 2];

            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(source.Substring(i * 2, 2), 16);
            }

            if (encoding == null)
                encoding = Encoding.Default;

            return encoding.GetString(raw);
        }


        /// <summary>
        /// Calcula a distância de Levensthein
        /// </summary>
        /// <param name="a">Primeira <see cref="string"/></param>
        /// <param name="b">Segunda <see cref="string"/></param>
        /// <returns></returns>
        public static int LevenshteinDistance(this string a, string b)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a));

            if (b == null)
                throw new ArgumentNullException(nameof(b));

            char charA;
            char charB;
            int del, ins, sub, cost;

            if (a.Length == 0)
                return b.Length;

            if (b.Length == 0)
                return a.Length;

            int[] v0 = new int[a.Length + 1];
            int[] v1 = new int[a.Length + 1];
            int[] vTmp;

            for (int i = 1; i <= a.Length; i++)
                v0[i] = i;

            for (int j = 1; j <= b.Length; j++)
            {
                v1[0] = j;

                charB = b[j - 1];

                for (int i = 1; i <= a.Length; i++)
                {
                    charA = a[i - 1];

                    if (charA == charB)
                        cost = 0;
                    else
                        cost = 1;

                    del = v0[i] + 1;
                    ins = v1[i - 1] + 1;
                    sub = v0[i - 1] + cost;

                    if (ins < del)
                        del = ins;

                    if (sub < del)
                        del = sub;


                    v1[i] = del;

                }

                vTmp = v0;
                v0 = v1;
                v1 = vTmp;
            }

            return v0[a.Length];
        }

        /// <summary>
        /// Fatia uma string com base em um índice de início (inclusivo) e um de final (exclusivo)
        /// </summary>
        /// <param name="source"><see cref="string"/> a ser fatiada</param>
        /// <param name="start">Índice de início (inclusivo)</param>
        /// <param name="end">Índice de final (exclusivo)</param>
        /// <returns>A string fatiada</returns>
        public static string Slice(this string source, int start, int end)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (end < 0) // Keep this for negative end support
            {
                end = source.Length + end;
            }

            int len = end - start;               // Calculate length
            return source.Substring(start, len); // Return Substring of length
        }

        /// <summary>
        /// Dividir uma string em páginas com base no indicador de nova página (page break/form feed) '\f'
        /// </summary>
        /// <param name="source"><see cref="string"/> a ser dividida</param>
        /// <param name="option">Opção da quebra de páginas</param>
        /// <returns>Páginas da <see cref="string"/></returns>
        public static List<string> SplitPages(this string source, StringSplitOptions option = StringSplitOptions.None)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var pages = source.Split(new char[] { '\f' }, option).ToList();

            return pages;
        }

        /// <summary>
        /// Determina se um valor é um dígito decimal [0-9], números como '²' ou '³' NÂO são contabilizados
        /// </summary>
        /// <param name="source"><see cref="string"/> a verificar se somente contém números</param>
        /// <returns>Verdadeiro se somente dígitos, falso caso contário</returns>
        public static bool IsDigit(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrWhiteSpace(source))
                return false;

            return source.All(Char.IsDigit);
        }

        /// <summary>
        /// Remove multiple white spaces
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToSingleSpace(this string source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Regex.Replace(source, @"\s+", " ");
        }
    }
}
