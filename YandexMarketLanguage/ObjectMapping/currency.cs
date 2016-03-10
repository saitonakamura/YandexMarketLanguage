using System;
using System.Globalization;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class currency
    {
        [Obsolete]
        public currency() { }

        public currency(CurrencyEnum id, decimal rate)
        {
            this.id = id;
            this.rate = rate.ToString(CultureInfo.InvariantCulture);
        }

        public currency(CurrencyEnum id, RateEnum rate)
        {
            this.id = id;
            this.rate = rate.ToString();
        }

        [XmlAttribute]
        public CurrencyEnum id;

        [XmlAttribute]
        public string rate;
    }

    public enum CurrencyEnum
    {
        RUR,
        USD,
        EUR,
        UAH,
        KZT,
    }

    public enum RateEnum
    {
        CBRF,
        NBU,
        NBK,
        СВ,
    }
}