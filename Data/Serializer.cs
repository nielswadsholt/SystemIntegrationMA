using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Data
{
    public static class Serializer
    {
        public static T Deserialize<T>(string xmlString) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var stringReader = new StringReader(xmlString))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }

        public static string Serialize<T>(T objectToSerialize)
        {
            var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmlSerializer.Serialize(xmlWriter, objectToSerialize);

                return stringWriter.ToString();
            }
        }
    }
}
