using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class yml_catalog
    {
        [Obsolete]
        public yml_catalog() { }

        public yml_catalog(DateTime date, shop shop)
        {
            if (shop == null)
                throw new ArgumentNullException("shop");

            this.dateField = date;
            this.shop = shop;
        }

        [XmlAttribute]
        public string date { get { return dateField.ToString("yyyy-MM-dd HH:mm"); } set { dateField = DateTime.Parse(value); } }

        [XmlElement]
        public shop shop;

        [XmlIgnore]
        public DateTime dateField;
    }
}
