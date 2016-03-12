using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class shop
    {
        /// <summary>
        /// DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public shop() { }

        public shop(string _name, string _company, string _url, currency[] _currencies, category[] _categories, delivery_option[] _deliveryOptions, offer[] _offers)
        {
            name = _name;
            company = _company;
            url = _url;
            currencies = _currencies;
            categories = _categories;
            delivery_options = _deliveryOptions;
            offers = _offers;
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