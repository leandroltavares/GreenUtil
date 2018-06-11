using System;

namespace GreenUtil.String
{
    /// <summary>
    /// Classe para lógicas relacionadas a datas <see cref="DateTime"/>
    /// </summary>
    public static class DateUtil
    {
        /// <summary>
        /// Formata uma data como string caso ela não seja o valor padrão <see cref="DateTime.MinValue"/>
        /// </summary>
        /// <param name="date">Data a ser formatada</param>
        /// <param name="format">Formato de data</param>
        /// <returns>Data formatada</returns>
        public static string ToStringIfNotDefault(this DateTime date, string format)
        {
            return date == DateTime.MinValue ? string.Empty : date.ToString(format);
        }
    }
}
