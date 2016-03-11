using System.Xml.Linq;
using System.Xml.Serialization;

namespace YandexMarketLanguage
{
    public class Serializer
    {
        public XDocument Serialize<T>(T model)
        {
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

            using (var writer = doc.CreateWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, model);
            }

            return doc;
        }
    }
}
