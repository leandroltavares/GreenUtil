using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.IO;

namespace GreenUtil.Data
{
    /// <summary>
    /// Classe para lógicas relacionadas a dados no formato JSON
    /// </summary>
    public static class JsonUtil
    {
        /// <summary>
        /// Método para carregar um JSON para uma instância do tipo T
        /// </summary>
        /// <typeparam name="T">Tipo a ser criado</typeparam>
        /// <param name="source">Conteúdo JSON em formato <see cref="string"/></param>
        /// <returns>Instância do tipo T gerado a partir do JSON</returns>
        public static T ParseJson<T>(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentNullException(nameof(source));

            JObject jObject = JObject.Parse(source);

            JSchemaValidatingReader validatingReader = new JSchemaValidatingReader(new JsonTextReader(new StringReader(source)))
            {
                Schema = GenerateJsonSchema<T>()
            };

            JsonSerializer serializer = new JsonSerializer();

            return serializer.Deserialize<T>(validatingReader);
        }

        /// <summary>
        /// Método para realizar o Schema JSON de um tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de origem do Schema</typeparam>
        /// <returns><see cref="JSchema"/></returns>
        public static JSchema GenerateJsonSchema<T>()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(T));

            return schema;
        }

        /// <summary>
        /// Método para realizar o Schema JSON de um tipo T em formato texto
        /// </summary>
        /// <typeparam name="T">Tipo de origem do Schema</typeparam>
        /// <returns>string so Schema JSON</returns>
        public static string GenerateJsonSchemaText<T>()
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(T));

            return schema.ToString();
        }


        /// <summary>
        /// Método para gerar um JSON a partir de uma instância do tipo T
        /// </summary>
        /// <typeparam name="T">Tipo de origem do Schema</typeparam>
        /// <param name="instance">Instância de origem</param>
        /// <returns>JSON gerado</returns>
        public static string ToJson<T>(this T instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            using (TextWriter textWriter = new StringWriter())
            {
                JsonSerializer serializer = new JsonSerializer();

                serializer.Serialize(textWriter, instance);

                return textWriter.ToString();
            }
        }
    }
}
