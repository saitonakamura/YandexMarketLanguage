using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    [XmlType("option")]
    public class delivery_option
    {
        /// <summary>
        /// DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public delivery_option() { }

        public delivery_option(int _cost, int _workDaysFrom, int _workDaysTo, int? _orderBefore = null)
            : this(_cost, _orderBefore)
        {
            if (_workDaysFrom < 0)
                throw new ArgumentException("workDaysFrom must be >= 0");

            if (_workDaysTo < 0)
                throw new ArgumentException("workDaysTo must be >= 0");

            if (_workDaysFrom >= _workDaysTo)
                throw new ArgumentException("workDaysFrom must be < workDaysTo");

            if ((_workDaysTo - _workDaysFrom) > 3)
                throw new ArgumentException("period between workDaysTo and workDaysFrom must be <= 3");

            // ReSharper disable once UseStringInterpolation
            days = string.Format("{0}-{1}", _workDaysFrom, _workDaysTo);
        }

        public delivery_option(int _cost, int _workDays, int? _orderBefore = null)
            : this(_cost, _orderBefore)
        {
            if (_workDays < 0)
                throw new ArgumentException("workDays must be >= 0");

            days = _workDays.ToString();
        }

        private delivery_option(int _cost, int? _orderBefore = null)
        {
            if (_orderBefore.HasValue)
            {
                if (_orderBefore.Value < 0 || _orderBefore.Value > 24)
                    throw new ArgumentException("order_before must be between 0 and 24");

                orderBeforeField = _orderBefore.Value;
            }

            if (_cost < 0)
                throw new ArgumentException("cost must be >= 0");

            cost = _cost;
        }

        [XmlAttribute] 
        public int cost;

        [XmlAttribute] 
        public string days;

        [XmlAttribute("order-before")]
        public string order_before { get { return orderBeforeField.HasValue ? orderBeforeField.ToString() : null; } set { orderBeforeField = int.Parse(value); } }

        [XmlIgnore]
        public int? orderBeforeField;
    }
}