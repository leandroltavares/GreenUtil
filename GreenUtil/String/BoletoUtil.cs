using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace GreenUtil.String
{   
    /// <summary>
    /// Classe para lógicas relacionadas a Boleto
    /// </summary>
    public static class BoletoUtil
    {
        /// <summary>
        /// Método para validar a máscara Boleto
        /// </summary>
        /// <param name="boleto"></param>
        /// <returns></returns>
        public static bool ValidateMask(this string linhaDigitalvel)
        {
            if (linhaDigitalvel == null)
                throw new ArgumentNullException(nameof(linhaDigitalvel));

            return Regex.IsMatch(linhaDigitalvel, @"\d{5}\.\d{5} \d{5}\.\d{6} \d{5}\.\d{6} \d \d{14}");
        }

        /// <summary>
        /// Método para validar a linha digitável de um Boleto
        /// </summary>
        /// <param name="linhaDigitavel">Boleto a ser validado</param>
        /// <returns>Verdadeiro se o Boleto é valido, falso caso contrário</returns>
        public static bool ValidateBoleto(this string linhaDigitavel)
        {
            if (linhaDigitavel == null)
                throw new ArgumentNullException(nameof(linhaDigitavel));

            linhaDigitavel = linhaDigitavel.Trim().Replace(".", string.Empty).Replace(" ", string.Empty);

            if (linhaDigitavel.Length != 47 || !linhaDigitavel.All(Char.IsDigit) || linhaDigitavel.All(c => c == '0'))
                return false;

            if (!ValidateGroup(linhaDigitavel.Substring(0, 10)))
                return false;

            if (!ValidateGroup(linhaDigitavel.Substring(10, 11)))
                return false;

            if (!ValidateGroup(linhaDigitavel.Substring(21, 11)))
                return false;

            int[] multiplicador = new int [] { 2, 3, 4, 5, 6, 7, 8, 9 };

            var codigoBarra = GenerateBarCodeText(linhaDigitavel);

            int digito = 0;

            for(int i = codigoBarra.Length - 1, j = 0; i >= 0; i--, j++)
            {
                digito += int.Parse(codigoBarra[i].ToString()) * multiplicador[j % multiplicador.Length];
            }

            digito %= 11;
           
            if (digito == 0 || digito == 1 || digito == 10)
                digito = 1;
            else
                digito = 11 - digito;

            if (int.Parse(linhaDigitavel[32]. ToString()) != digito)
                return false;
            
            return true;
        }

        /// <summary>
        /// Gera o texto do código de barras a partir da linha digitável
        /// </summary>
        /// <param name="linhaDigitavel"></param>
        /// <returns></returns>
        public static string GenerateBarCodeText(string linhaDigitavel)
        {
            if (linhaDigitavel == null)
                throw new ArgumentNullException(nameof(linhaDigitavel));
            
            if (linhaDigitavel.Length != 47)
                throw new ArgumentOutOfRangeException(nameof(linhaDigitavel), "A linha digitável deve conter 47 caracteres.");

            //Remove os digitos verificadores
            string linhaDigitavalSemDigito = linhaDigitavel.Substring(0, 9) + linhaDigitavel.Substring(10, 10) + linhaDigitavel.Substring(21, 10) + linhaDigitavel.Substring(33, 14);

            //Reordena as posições para simular o código de barra
            return linhaDigitavalSemDigito.Substring(0, 4) + linhaDigitavalSemDigito.Substring(29, 14) + linhaDigitavalSemDigito.Substring(4, 25);

        }

        private static bool ValidateGroup(string input)
        {
            int digit = 0;

            for (int i = input.Length - 2, j = 0; i >= 0; i--, j++)
            {
                int d = int.Parse(input[i].ToString());

                if (j % 2 == 0)
                    d *= 2;

                if (d > 9)
                    d -= 9;

                digit += d;
            }

            digit = digit % 10;

            if (digit != 0)
                digit = 10 - digit;

            if (int.Parse(input[input.Length - 1].ToString()) != digit)
                return false;

            return true;
        }
    }
}
