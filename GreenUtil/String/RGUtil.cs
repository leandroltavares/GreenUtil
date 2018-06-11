
using System;
using System.Linq;

namespace GreenUtil.String
{
    /// <summary>
    /// Classe para lógicas relacionadas a RG
    /// </summary>
    public static class RGUtil
    {
        /// <summary>
        /// Método para validar um RG
        /// </summary>
        /// <param name="rg">Número do RG</param>
        /// <returns>Verdadeiro se RG é válido</returns>
        public static bool ValidateRG(this string rg)
        {

            if (rg == null)
                throw new ArgumentNullException(nameof(rg));

            int[] multiplicador = new int[9] { 2, 3, 4, 5, 6, 7, 8, 9, 100 };

            int soma;
            int resto;
            rg = rg.Trim();
            rg = rg.Replace(".", "").Replace("-", "").ToUpper();

            if (rg.Length != 9 || rg.All(c => c == rg[0]))
                return false;

            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                var digitoAtual = rg[i].ToString();

                if (digitoAtual == "X")
                {
                    soma += 10 * multiplicador[i];
                }
                else
                {
                    soma += int.Parse(digitoAtual) * multiplicador[i];
                }
            }

            resto = soma % 11;

            return resto == 0;
        }
    }
}
