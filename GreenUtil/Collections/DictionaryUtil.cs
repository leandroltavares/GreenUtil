using System;
using System.Collections.Generic;
using System.Linq;

namespace GreenUtil.Collections
{
    /// <summary>
    /// Classe para lógicas relacionadas a dicionários
    /// </summary>
    public static class DictionaryUtil
    {
        /// <summary>
        /// Converte uma coleção para um dicionário, agrupando por um seletor
        /// </summary>
        /// <typeparam name="TKey">Tipo de chave</typeparam>
        /// <typeparam name="TValue">Tipo do valor</typeparam>
        /// <param name="list">Lista de origem</param>
        /// <param name="keySelector">Função seletora</param>
        /// <returns>O dicionário criado</returns>
        public static Dictionary<TKey, List<TValue>> ToDictionaryList<TKey, TValue>(this IEnumerable<TValue> list, Func<TValue, TKey> keySelector)
            where TKey : IEquatable<TKey>
            where TValue : new()
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            return list.GroupBy(keySelector).ToDictionary(ks => ks.Key, ks => ks.ToList());
        }
    }
}
