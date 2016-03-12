using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    // TODO do other fields
    // TODO do derived class for different model definitions
    [Serializable]
    public class offer
    {
        /// <summary>
        /// DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public offer() { }

        public offer(string _id, decimal _price, CurrencyEnum _currencyId, int _categoryId, string _name, string _type = null, bool? _available = null)
        {
            if (string.IsNullOrWhiteSpace(_id))
                throw new ArgumentException("id must not be empty");

            id = _id;
            type = _type;
            availableField = _available;

            price = _price;
            currencyId = _currencyId;
            categoryId = _categoryId;
            name = _name;
        }

        [XmlAttribute]
        public string id;

        [XmlAttribute]
        public string type;

        [XmlAttribute]
        public string available { get { return availableField.HasValue ? availableField.ToString() : null; } set { availableField = bool.Parse(value); } }

        [XmlIgnore]
        public bool? availableField;

        public decimal price;

        public CurrencyEnum currencyId;

        public int categoryId;

        public string name;

    }
}