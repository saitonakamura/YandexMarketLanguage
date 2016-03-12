using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class shop
    {
        [Obsolete]
        public shop() { }

        // TODO rewrite to _field in constructor
        public shop(string name, string company, string url, currency[] currencies, category[] categories, delivery_option[] delivery_options, offer[] offers)
        {
            this.name = name;
            this.company = company;
            this.url = url;
            this.currencies = currencies;
            this.categories = categories;
            this.delivery_options = delivery_options;
            this.offers = offers;
        }

        public string name;

        public string company;

        public string url;

        public string platform;

        public string version;

        public string agency;

        public string email;

        public currency[] currencies;

        public category[] categories;

        [XmlArray("delivery-options")]
        public delivery_option[] delivery_options;

        public string cpa;

        public offer[] offers;
    }
}