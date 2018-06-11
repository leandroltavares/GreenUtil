using GreenUtil.Assets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenUtil.Collections
{
    /// <summary>
    /// Classe para lógicas relacionadas a <see cref="IEnumerable{T}"/> 
    /// </summary>
    public static class IEnumerableUtil
    {
        /// <summary>
        /// Determinar os valores distintos de uma coleção com base em uma chave de seleção
        /// </summary>
        /// <typeparam name="TSource">Tipo de origem</typeparam>
        /// <typeparam name="TKey">Tipo da chave de seleçaõ</typeparam>
        /// <param name="source">Coleção de origem</param>
        /// <param name="keySelector">Função seletora</param>
        /// <returns>Coleção de valores distinto</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
            where TKey : IEquatable<TKey>
        {

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Método para determinar se duas coleções contém os mesmos elementos, ainda que os mesmos estejam foram de ordem
        /// </summary>
        /// <typeparam name="T">Tipo de origem</typeparam>
        /// <param name="source">Coleção de origem</param>
        /// <param name="other">Coleção de destino</param>
        /// <returns>Verdadeiro se ambas as coleções contém os mesmo elementos</returns>
        public static bool Contains<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (other == null)
                throw new ArgumentNullException(nameof(other));

            Dictionary<T, int> occurrences = new Dictionary<T, int>();

            foreach (var element in source)
            {
                if (!occurrences.TryGetValue(element, out int count))
                    occurrences.Add(element, 1);
                else
                    occurrences[element] = ++count;
            }

            foreach(var element in other)
            {
                if(!occurrences.TryGetValue(element, out int count))
                    return false;

                if (--count == 0)
                    occurrences.Remove(element);
                else
                    occurrences[element] = count;
            }
                
            return occurrences.Count == 0;
        }

        /// <summary>
        /// Shuffles as collection
        /// </summary>
        /// <typeparam source="T">Collection type</typeparam>
        /// <param name="source">Collection to be shuffled</param>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var buffer = source.ToList();

            for (int i = 0; i < buffer.Count; i++)
            {
                int j = Global.Random.Next(i, buffer.Count);

                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
