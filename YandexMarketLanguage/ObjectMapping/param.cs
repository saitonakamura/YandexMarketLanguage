using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    /// <summary>
    ///     Goods specification
    /// </summary>
    [Serializable]
    // TODO tests for param
    public class param
    {
        private string _name;
        private string _value;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public param() {}

        /// <summary>
        ///     Goods specification
        /// </summary>
        public param(string name, string value)
        {
            this.value = value;
            this.name = name;
        }

        /// <summary>
        ///     Goods specification
        /// </summary>
        public param(string name, string value, string unit)
            : this(name, value)
        {
            this.unit = unit;
        }

        /// <summary>
        ///     name of specification
        /// </summary>
        [XmlAttribute]
        public string name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("name must not be empty");

                _name = value;
            }
        }

        /// <summary>
        ///     unit of measurement
        /// </summary>
        [XmlAttribute]
        public string unit { get; set; }

        /// <summary>
        ///     value
        /// </summary>
        [XmlText]
        public string value
        {
            get { return _value; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("value must not be empty");

                _value = value;
            }
        }
    }
}