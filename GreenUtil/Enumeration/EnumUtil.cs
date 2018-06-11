using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GreenUtil.Enumeration
{
    /// <summary>
    /// Classe para lógicas relacionadas a enumerações (Enum) <see cref="Enum"/>
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// Método para obter a descrição de um enum
        /// </summary>
        /// <typeparam name="T">Tipo do Enum</typeparam>
        /// <param name="source"></param>
        /// <returns>Retorna o texto da descrição</returns>
        public static string Description<T>(this T source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type.");
            }

            FieldInfo fi = typeof(T).GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return source.ToString();
        }

        /// <summary>
        /// Obter o valor de um enum a partir de um objeto, em geral do tipo string
        /// </summary>
        /// <typeparam name="T">Tipo do Enum</typeparam>
        /// <param name="value">objeto a ser convertido para o enum</param>
        /// <param name="useDefault">Se verdadeiro, utilizar o valor padrão caso o valor a ser convertido seja nulo, caso contrário lança exceção</param>
        /// <returns></returns>
        public static T Value<T>(this object value, bool useDefault = false)
        {
            if (value == null)
            {
                if (useDefault)
                {
                    return default(T);
                }

                throw new ArgumentNullException(nameof(value));
            }
            
            return Enum.GetValues(typeof(T)).Cast<T>().FirstOrDefault(v => Description(v).Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
