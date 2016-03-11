using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class offer
    {
        [Obsolete]
        public offer() { }

        public offer(string id, decimal price, CurrencyEnum currency, int categoryId, string name, string type = null, bool? available = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("id must not be empty");

            this.id = id;
            this.type = type;
            this.availableField = available;

            this.price = price;
            this.currency = currency;
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

        public CurrencyEnum currency;

        public int categoryId;

        public string name;

    }
}