using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class yml_catalog
    {
        /// <summary>
        /// DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public yml_catalog() { }

        public yml_catalog(DateTime _date, shop _shop)
        {
            // ReSharper disable once UseNameofExpression
            if (_shop == null)
                throw new ArgumentNullException("_shop");

            dateField = _date;
            shop = _shop;
        }

        [XmlAttribute]
        public string date { get { return dateField.ToString("yyyy-MM-dd HH:mm"); } set { dateField = DateTime.Parse(value); } }

        public shop shop;

        [XmlIgnore]
        public DateTime dateField;
    }
}
