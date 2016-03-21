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
        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public param() {}

        /// <summary>
        ///     Goods specification
        /// </summary>
        public param(string _name)
        {
            name = _name;
        }

        /// <summary>
        ///     Goods specification
        /// </summary>
        public param(string _name, string _value)
        {
            name = _name;
            value = _value;
        }

        /// <summary>
        ///     Goods specification
        /// </summary>
        public param(string _name, string _value, string _unit)
        {
            name = _name;
            value = _value;
            unit = _unit;
        }

        /// <summary>
        ///     name of specification
        /// </summary>
        [XmlAttribute]
        public string name { get; set; }

        /// <summary>
        ///     unit of measurement
        /// </summary>
        [XmlAttribute]
        public string unit { get; set; }

        /// <summary>
        ///     value
        /// </summary>
        [XmlText]
        public string value { get; set; }
    }
}