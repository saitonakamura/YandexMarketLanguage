using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class shop
    {
        [Obsolete]
        public shop() { }

        public shop(string name, string company, string url, currency[] currencies)
        {
            this.name = name;
            this.company = company;
            this.url = url;
            this.currencies = currencies;
        }

        [XmlElement]
        public string name;

        [XmlElement]
        public string company;

        [XmlElement]
        public string url;

        [XmlElement]
        public string platform;

        [XmlElement]
        public string version;

        [XmlElement]
        public string agency;

        [XmlElement]
        public string email;

        public currency[] currencies;

        [XmlElement]
        public string categories;

        [XmlElement("delivery-options")]
        public string delivery_options;

        [XmlElement]
        public string cpa;

        [XmlElement]
        public string offers;
    }
}