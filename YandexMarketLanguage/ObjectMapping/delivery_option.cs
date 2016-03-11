using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    [XmlType("option")]
    public class delivery_option
    {
        [Obsolete]
        public delivery_option() { }

        // TODO move to _field in constructor
        // TODO move to Contracts for invariants defence
        public delivery_option(int cost, int workDaysFrom, int workDaysTo, int? order_before = null)
            : this(cost, order_before)
        {
            if (workDaysFrom < 0)
                throw new ArgumentException("workDaysFrom must be >= 0");

            if (workDaysTo < 0)
                throw new ArgumentException("workDaysTo must be >= 0");

            if (workDaysFrom >= workDaysTo)
                throw new ArgumentException("workDaysFrom must be < workDaysTo");

            if ((workDaysTo - workDaysTo) > 3)
                throw new ArgumentException("period between workDaysTo and workDaysFrom must be <= 3");

            // ReSharper disable once UseStringInterpolation
            this.days = string.Format("{0}-{1}", workDaysFrom, workDaysTo);
        }

        // TODO move to _field in constructor
        public delivery_option(int cost, int workDays, int? order_before = null)
            : this(cost, order_before)
        {
            if (workDays < 0)
                throw new ArgumentException("workDays must be >= 0");

            this.days = workDays.ToString();
        }

        // TODO move to _field in constructor
        private delivery_option(int cost, int? order_before = null)
        {
            if (order_before.HasValue)
            {
                if (order_before.Value < 0 || order_before.Value > 24)
                    throw new ArgumentException("order_before must be between 0 and 24");

                this.orderBeforeField = order_before.Value;
            }

            if (cost < 0)
                throw new ArgumentException("cost must be >= 0");

            this.cost = cost;
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