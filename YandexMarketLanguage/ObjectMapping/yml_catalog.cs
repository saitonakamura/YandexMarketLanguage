using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class yml_catalog
    {
        private DateTime _date;
        private shop _shop;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public yml_catalog() {}

        public yml_catalog(DateTime date, shop shop)
        {
            this._date = date;
            this.shop = shop;
        }

        [XmlAttribute]
        public string date
        {
            get { return _date.ToString("yyyy-MM-dd HH:mm"); }
            set { _date = DateTime.Parse(value); }
        }

        public shop shop
        {
            get { return _shop; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                _shop = value;
            }
        }
    }
}