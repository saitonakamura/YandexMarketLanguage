using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class yml_catalog
    {
        private DateTime _date;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public yml_catalog() {}

        public yml_catalog(DateTime _date, shop _shop)
        {
            // ReSharper disable once UseNameofExpression
            if (_shop == null)
                throw new ArgumentNullException("_shop");

            this._date = _date;
            shop = _shop;
        }

        [XmlAttribute]
        public string date
        {
            get { return _date.ToString("yyyy-MM-dd HH:mm"); }
            set { _date = DateTime.Parse(value); }
        }

        public shop shop { get; set; }
    }
}