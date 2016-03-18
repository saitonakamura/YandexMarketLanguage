using System;
using System.Linq;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    /// <summary>
    ///     Age category of goods
    /// </summary>
    [Serializable]
    public class age
    {
        private readonly int[] _allowedYear = { 0, 6, 12, 16, 18 };
        private int _value;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public age() {}

        /// <summary>
        ///     age category of goods, unit must be 'year' or 'month'
        /// </summary>
        public age(string _unit)
        {
            if (_unit != "year" && _unit != "month")
                throw new ArgumentException("unit must be 'year' or 'month'");

            unit = _unit;
        }

        /// <summary>
        ///     age category of goods, unit must be 'year' or 'month'
        ///     <para>allowed int values for unit 'year' 0, 6, 12, 16, 18</para>
        ///     <para>allowed int values for unit 'month' in range between 0 and 12</para>
        /// </summary>
        public age(string _unit, int _value)
        {
            if (_unit != "year" && _unit != "month")
                throw new ArgumentException("unit must be 'year' or 'month'");

            unit = _unit;
            value = _value;
        }

        /// <summary>
        ///     age unit, allowed values 'year' or 'month'
        /// </summary>
        [XmlAttribute]
        public string unit { get; set; }

        /// <summary>
        ///     allowed int values for unit 'year' 0, 6, 12, 16, 18,
        ///     allowed int values for unit 'month' in range between 0 and 12
        /// </summary>
        [XmlText]
        public int value
        {
            get { return _value; }
            set
            {
                if (unit == "year" && !_allowedYear.Contains(value))
                    throw new ArgumentException("allowed int values for unit 'year' 0, 6, 12, 16, 18");

                if (unit == "month" && Enumerable.Range(0, 12).Contains(value))
                    throw new ArgumentException("allowed int values for unit 'month' in range between 0 and 12");

                _value = value;
            }
        }
    }
}