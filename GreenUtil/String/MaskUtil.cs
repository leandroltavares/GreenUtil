using GreenUtil.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenUtil.String
{
    /// <summary>
    /// Mask utilities
    /// </summary>
    public static class MaskUtil
    {
        /// <summary>
        /// Masks a string with a specific string mask
        /// </summary>
        /// <param name="source">The string to be masked</param>
        /// <param name="mask">A commom known mask to be used</param>
        /// <returns>The masked string</returns>
        public static string ToMaskedString(this string source, StringMask mask)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            string maskPattern = string.Empty;

            switch (mask)
            {
                case StringMask.CEP:
                    maskPattern = "#####-###";
                    break;
                case StringMask.CNPJ:
                    maskPattern = "##.###.###/####-##";
                    break;
                case StringMask.CPF:
                    maskPattern = "###.###.###-##";
                    break;
                case StringMask.RG:
                    maskPattern = "##.###.###-#";
                    break;

            }

            return ToMaskedString(source.OnlyAlphaNumeric(), maskPattern);

        }

        /// <summary>
        /// Masks a string with the specificied mask
        /// </summary>
        /// <param name="source">The string to be masked</param>
        /// <param name="formatMask">The mask to be used</param>
        /// <returns>The masked string</returns>
        public static string ToMaskedString(string source, string formatMask)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (formatMask == null)
                throw new ArgumentNullException(nameof(formatMask));

            int maskLength = formatMask.KeepChars(PLACEHOLDERS_CHAR).Length;

            if (maskLength == 0)
                return source;

            if (source.Length != maskLength)
                throw new ArgumentException($"The mask string placeholders ({PLACEHOLDERS_CHAR.ToString(",", "'", "'")}) and the source string lengths must match. Source length = {source.Length}, Mask length = {maskLength}.");

            StringBuilder sb = new StringBuilder();

            for (int i = 0, j = 0; i < formatMask.Length; i++)
            {
                char currentChar = formatMask[i];

                switch (formatMask[i])
                {
                    case '#':
                        currentChar = source[j];
                        j++;
                        break;
                }

                sb.Append(currentChar);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Placeholders characters
        /// </summary>
        private readonly static char[] PLACEHOLDERS_CHAR = new char[] { '#' };
    }

    /// <summary>
    /// Commom masks
    /// </summary>
    public enum StringMask
    {
        /// <summary>
        /// Brazilian ZIP Code (CEP) (e.g, 00000-000)
        /// </summary>
        CEP,
        /// <summary>
        /// CNPJ Mask in form  (e.g., 00.000.000/0000-00)
        /// </summary>
        CNPJ,
        /// <summary>
        /// CPF Mask in form (e.g., 000.000.000-00)
        /// </summary>
        CPF,
        /// <summary>
        /// RG Mask in form (e.g., 00.000.000-0)
        /// </summary>
        RG,        

    }
}
