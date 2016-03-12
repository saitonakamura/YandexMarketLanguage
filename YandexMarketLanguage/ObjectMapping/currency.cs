using System;
using System.Globalization;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class currency
    {
        /// <summary>
        /// DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public currency() { }

        public currency(CurrencyEnum _id, decimal _rate)
        {
            id = _id;
            rate = _rate.ToString(CultureInfo.InvariantCulture);
        }

        public currency(CurrencyEnum _id, RateEnum _rate)
        {
            id = _id;
            rate = _rate.ToString();
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
        KZT
    }

    public enum RateEnum
    {
        CBRF,
        NBU,
        NBK,
        СВ
    }
}