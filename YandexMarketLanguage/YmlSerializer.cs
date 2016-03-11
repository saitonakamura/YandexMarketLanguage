using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YandexMarketLanguage
{
    public class YmlSerializer
    {
        public XDocument Serialize<T>(T model)
        {
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

            // Yandex validator do not accept any foreign namespaces attributes so we must clear it
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

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
    }
}
