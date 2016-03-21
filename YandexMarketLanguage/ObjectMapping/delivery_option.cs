using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    [XmlType("option")]
    [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
    public class delivery_option
    {
        private int _cost;
        private int? _orderBeforeInt;
        private int? _workDays;
        private int? _workDaysFrom;
        private int? _workDaysTo;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public delivery_option() {}

        public delivery_option(int cost, int workDaysFrom, int workDaysTo, int? orderBefore = null)
            : this(cost, orderBefore)
        {
            SetWorkDaysPeriod(workDaysFrom, workDaysTo);
        }

        public delivery_option(int cost, int workDays, int? orderBefore = null)
            : this(cost, orderBefore)
        {
            SetWorkDays(workDays);
        }

        private delivery_option(int cost, int? orderBefore = null)
        {
            this.orderBeforeInt = orderBefore;
            this.cost = cost;
        }

        private int? orderBeforeInt
        {
            get { return _orderBeforeInt; }
            set
            {
                if (value.HasValue)
                {
                    if (value.Value < 0 || value.Value > 24)
                        throw new ArgumentException("order_before must be between 0 and 24");
                }

                _orderBeforeInt = value;
            }
        }

        [XmlAttribute]
        public int cost
        {
            get { return _cost; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("cost must be >= 0");

                _cost = value;
            }
        }

        [XmlAttribute]
        public string days { get; set; }

        [XmlAttribute("order-before")]
        public string order_before
        {
            get { return orderBeforeInt.HasValue ? orderBeforeInt.ToString() : null; }
            set { orderBeforeInt = int.Parse(value); }
        }

        private void SetWorkDays(int workDays)
        {
            if (workDays < 0)
                throw new ArgumentException("workDays must be >= 0");

            _workDays = workDays;

            days = _workDays.ToString();
        }

        private void SetWorkDaysPeriod(int workDaysFrom, int workDaysTo)
        {
            if (workDaysFrom < 0)
                throw new ArgumentException("workDaysFrom must be >= 0");

            if (workDaysTo < 0)
                throw new ArgumentException("workDaysTo must be >= 0");

            if (workDaysFrom >= workDaysTo)
                throw new ArgumentException("workDaysFrom must be < workDaysTo");

            if (workDaysTo - workDaysFrom > 3)
                throw new ArgumentException("period between workDaysTo and workDaysFrom must be <= 3");

            _workDaysFrom = workDaysFrom;
            _workDaysTo = workDaysTo;

            // ReSharper disable once UseStringInterpolation
            days = string.Format("{0}-{1}", _workDaysFrom, _workDaysTo);
        }
    }
}