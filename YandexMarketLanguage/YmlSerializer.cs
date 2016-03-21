using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YandexMarketLanguage
{
    public class YmlSerializer
    {
        public XDocument ToXDocument<T>(T model)
        {
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

            // Yandex validator do not accept any foreign namespaces attributes so we must clear it
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            var settings = new XmlWriterSettings
            {
                Encoding = new UnicodeEncoding(false, false),
                Indent = true,
                OmitXmlDeclaration = false
            };

            using (var writer = XmlWriter.Create(doc.CreateWriter(), settings))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, model, ns);
            }

            return doc;
        }

        public string ToXmlString<T>(T model)
        {
            var doc = ToXDocument(model);
            string xmlString;

            using (var wr = new Utf8StringWriter())
            {
                doc.Save(wr);
                xmlString = wr.GetStringBuilder().ToString();
            }

            return xmlString;
        }

        public T FromXmlString<T>(string xmlString)
        {
            T model;

            using (var reader = new StringReader(xmlString))
            {
                var serializer = new XmlSerializer(typeof(T));
                model = (T)serializer.Deserialize(reader);
            }

            return model;
        }
    }

    public sealed class Utf8StringWriter : StringWriter
    {
        // ReSharper disable once ConvertPropertyToExpressionBody
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}