using System;
using System.IO;
using System.Xml.Serialization;

namespace GreenUtil.Data
{
    /// <summary>
    /// XML logic
    /// </summary>
    public static class XMLUtil
    {
        /// <summary>
        /// Create an instance of an object from a serialized XML 
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="xml">XML context as a <see cref="string"/></param>
        /// <returns>Created instance from the XML text</returns>
        public static T ParseXML<T>(this string xml)
        {
            if (xml == null)
                throw new ArgumentNullException(nameof(xml));

            using (TextReader reader = new StringReader(xml))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Serialize a XML from a instance of an object
        /// </summary>
        /// <typeparam name="T">Instance type</typeparam>
        /// <param name="instance">Instância to serialize</param>
        /// <returns>Generate XML as string</returns>
        public static string ToXml<T>(this T instance)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(T));

            using (TextWriter writer = new StringWriter())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(writer, instance);

                return writer.ToString();
            }
        }
    }
}
