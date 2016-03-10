using System.Xml.Linq;
using System.Xml.Serialization;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguage
{
    public class Serializer
    {
        public XDocument Serialize(yml_catalog ymlCatalog)
        {
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));

            using (var writer = doc.CreateWriter())
            {
                var serializer = new XmlSerializer(ymlCatalog.GetType());
                serializer.Serialize(writer, ymlCatalog);
            }

            return doc;
        }
    }
}
