using System;
using System.IO;
using System.Xml.Serialization;

namespace GreenUtil.Data
{
    /// <summary>
    /// Clsses para lógicas relacionadas a <see cref="Object"/>
    /// </summary>
    public static class ObjectUtil
    {
        /// <summary>
        /// Clona um objeto e toda sua hieráquia
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            using (var ms = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(ms, instance);
                ms.Position = 0;

                return (T)xmlSerializer.Deserialize(ms);
            }
        }
    }
}
