using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
    public class currency
    {
        private decimal? _plus;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public currency() {}

        public currency(CurrencyEnum id, decimal rate, decimal? plus = null)
            : this(id, plus)
        {
            this.rate = rate.ToString(CultureInfo.InvariantCulture);
        }

        public currency(CurrencyEnum id, RateEnum rate, decimal? plus = null)
            : this(id, plus)
        {
            this.rate = rate.ToString();
        }

        private currency(CurrencyEnum id, decimal? plus = null)
        {
            this.id = id;
            this._plus = plus;
        }

        [XmlAttribute]
        public CurrencyEnum id { get; set; }

        [XmlAttribute]
        public string rate { get; set; }

        [XmlAttribute]
        public string plus
        {
            get { return _plus.HasValue ? _plus.Value.ToString(CultureInfo.InvariantCulture) : null; }
            set { _plus = decimal.Parse(value); }
        }
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