using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    // TODO do other fields
    // TODO do derived class for different model definitions
    [Serializable]
    public class offer
    {
        [Obsolete]
        public offer() { }

        // TODO move to _field in constructor
        public offer(string id, decimal price, CurrencyEnum currencyId, int categoryId, string name, string type = null, bool? available = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("id must not be empty");

            this.id = id;
            this.type = type;
            this.availableField = available;

            this.price = price;
            this.currencyId = currencyId;
            this.categoryId = categoryId;
            this.name = name;
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