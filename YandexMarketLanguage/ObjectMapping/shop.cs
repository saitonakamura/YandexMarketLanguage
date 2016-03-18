using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class shop
    {
        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public shop() {}

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

        public string name { get; set; }

        public string company { get; set; }

        public string url { get; set; }

        public string platform { get; set; }

        public string version { get; set; }

        public string agency { get; set; }

        public string email { get; set; }

        public currency[] currencies { get; set; }

        public category[] categories { get; set; }

        [XmlArray("delivery-options")]
        public delivery_option[] delivery_options { get; set; }

        public string cpa { get; set; }

        public offer[] offers { get; set; }
    }
}