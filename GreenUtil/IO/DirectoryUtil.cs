using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GreenUtil.IO
{
    /// <summary>
    /// Classe para lógicas relacionadas a <see cref="Directory"/>
    /// </summary>
    public static class DirectoryUtil
    {
        /// <summary>
        /// Obtem os arquivos através de uma expressão regular, para buscar por extensões utilize apenas o .txt
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pattern"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static IEnumerable<(string, Match)> GetFilesByRegex(string path, string pattern, SearchOption searchOption)
        {
            if (path == null)
                throw new ArgumentNullException("Um caminho válido deve ser informado.", nameof(path));

            if (pattern == null)
                throw new ArgumentNullException("Uma expressão regular válida deve ser informada.", nameof(pattern));

            Regex expression = new Regex(pattern);

            foreach (var file in Directory.GetFiles(path, "*.*", searchOption)
                .Select(f => new { filePath = f, match = expression.Match(Path.GetFileName(f)), })
                .Where(a => a.match.Success))
            {
                yield return (file.filePath, file.match);
            };
        }
    }
}
