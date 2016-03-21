using System;
using System.Diagnostics.CodeAnalysis;
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
        ///     age category of goods
        /// </summary>
        [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
        public age(AgeUnit unit)
        {
            this.unit = unit;
        }

        /// <summary>
        ///     age category of goods
        /// </summary>
        [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
        public age(AgeUnit unit, int value)
        {
            this.unit = unit;
            this.value = value;
        }

        /// <summary>
        ///     age unit
        /// </summary>
        [XmlAttribute]
        public AgeUnit unit { get; set; }

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
                switch (unit)
                {
                    case AgeUnit.year:
                        if (_allowedYear.Contains(value) == false)
                            throw new ArgumentException("allowed int values for unit 'year' 0, 6, 12, 16, 18");
                        break;
                    case AgeUnit.month:
                        if (Enumerable.Range(0, 12).Contains(value) == false)
                            throw new ArgumentException("allowed int values for unit 'month' in range between 0 and 12");
                        break;
                }

                _value = value;
            }
        }
    }

    public enum AgeUnit
    {
        // ReSharper disable once InconsistentNaming
        year,
        // ReSharper disable once InconsistentNaming
        month,
    }
}