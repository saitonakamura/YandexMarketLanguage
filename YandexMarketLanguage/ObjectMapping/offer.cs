using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    //TODO more test
    //TODO url required?
    //TODO fields -> properties
    /// <summary>
    ///  Product offer
    ///  </summary>
    [Serializable]
    public class offer
    {
        /// <summary>
        /// DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public offer() { }

        /// <summary>
        /// DO NOT USE, need only for others constructors
        /// </summary>
        private offer(string _id, decimal _price, CurrencyEnum _currencyId, int _categoryId)
        {
            if (string.IsNullOrWhiteSpace(_id))
                throw new ArgumentException("id must not be empty");

            id = _id;

            price = _price;
            currencyId = _currencyId;
            categoryId = _categoryId;
        }
        
        /// <summary>
        ///  Product simple offer
        ///  </summary>
        public offer(string _id, decimal _price, CurrencyEnum _currencyId, int _categoryId, string _name) 
            : this(_id, _price, _currencyId, _categoryId)
        {
            name = _name;
        }
        
        /// <summary>
        ///  Product vendor offer
        ///  </summary>
        public offer(string _id, decimal _price, CurrencyEnum _currencyId, int _categoryId, string _typePrefix, string _vendor, string _model)
            : this(_id, _price, _currencyId, _categoryId)
        {
            type = "vendor.model";

            typePrefix = _typePrefix;
            vendor = _vendor;
            model = _model;
        }
        
        /// <summary>
        /// ID offer, attribute
        /// </summary>
        [XmlAttribute]
        public string id;

        /// <summary>
        /// Type of offer (simple or vendor), attribute
        /// </summary>
        [XmlAttribute]
        public string type;

        /// <summary>
        /// Term of delivery of the goods at the point of pickup, bool, 
        /// true - within two working days after ordering, 
        /// false - in a period of three working days to two months
        /// </summary>
        [XmlAttribute]
        public string available
        {
            get
            {
                return availableField.HasValue ? availableField.ToString() : null;
            }
            set
            {
                availableField = bool.Parse(value);
            }
        }

        [XmlIgnore]
        public bool? availableField;

        private string _url;
        /// <summary>
        /// Link on product (max length 512)
        /// </summary>
        [XmlElement(Order = 1)]
        public string url
        {
            get { return _url; }
            set
            {
                if(value.Length > 512)
                    throw new ArgumentException("max URL length 512");

                _url = url;
            }
        }

        /// <summary>
        /// Price of product
        /// </summary>
        [XmlElement(Order = 2)] 
        public decimal price;

        private decimal? _oldprice;
        /// <summary>
        /// Old price of product (discount calculation)
        /// </summary>
        [XmlElement(Order = 3)] 
        public string oldprice
        {
            get
            {
                return _oldprice.HasValue ? _oldprice.ToString() : null;
            }
            set
            {
                _oldprice = Decimal.Parse(value);
            }
        }

        /// <summary>
        /// Currency identifier product (RUR, USD, UAH, KZT)
        /// </summary>
        [XmlElement(Order = 4)]
        public CurrencyEnum currencyId;

        /// <summary>
        /// Category identifier product
        /// </summary>
        [XmlElement(Order = 5)]
        public int categoryId;

        /// <summary>
        /// Path to category of product 
        /// https://download.cdn.yandex.net/market/market_categories.xls
        /// </summary>
        [XmlElement(Order = 6)]
        public string market_category;

        private string _picture;
        /// <summary>
        /// Picture link on product (max length 512)
        /// </summary>
        [XmlElement(Order = 7)]
        public string picture
        {
            get { return _picture; }
            set
            {
                if (value.Length > 512)
                    throw new ArgumentException("max URL length 512");

                _picture = value;
            }
        }

        /// <summary>
        /// can be purchased in real store, bool
        /// </summary>
        [XmlElement(Order = 8)]
        public string store
        {
            get
            {
                return storeField.HasValue ? storeField.ToString() : null;
            }
            set
            {
                storeField = bool.Parse(value);
            }
        }

        [XmlIgnore]
        public bool? storeField;

        /// <summary>
        /// can be reserved (pickup), bool
        /// </summary>
        [XmlElement(Order = 9)]
        public string pickup
        {
            get
            {
                return pickupField.HasValue ? pickupField.ToString() : null;
            }
            set
            {
                pickupField = bool.Parse(value);
            }
        }

        [XmlIgnore] 
        public bool? pickupField;

        /// <summary>
        /// can be delivered, bool
        /// </summary>
        [XmlElement(Order = 10)]
        public string delivery
        {
            get
            {
                return deliveryField.HasValue ? deliveryField.ToString() : null;
            }
            set
            {
                deliveryField = bool.Parse(value);
            }
        }

        [XmlIgnore]
        public bool? deliveryField;

        /// <summary>
        /// array of delivery options
        /// </summary>
        [XmlArray("delivery-options", Order = 11)]
        public delivery_option[] delivery_options;

        private string _nameField;
        /// <summary>
        /// Name of product
        /// <para>ONLY for simple offer</para>
        /// </summary>
        [XmlElement(Order = 12)]
        public string name
        {
            get { return _nameField; }
            set
            {
                if (typePrefix != null)
                    throw new ArgumentException("'name' must be used ONLY for simple offer");

                _nameField = value;
            }
        }

        private string _typePrefixField;
        /// <summary>
        /// Type / category of product
        /// <para>ONLY for vendor offer</para>
        /// </summary>
        [XmlElement(Order = 13)]
        public string typePrefix
        {
            get { return _typePrefixField; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'typePrefix' must be used ONLY for vendor offer");

                _typePrefixField = value;
            }
        }

        /// <summary>
        /// Vendor of product
        /// </summary>
        [XmlElement(Order = 14)]
        public string vendor;

        /// <summary>
        /// Vendor code of product
        /// </summary>
        [XmlElement(Order = 15)]
        public string vendorCode;

        private string _modelField;
        /// <summary>
        /// Model of product
        /// <para>ONLY for vendor offer</para>
        /// </summary>
        [XmlElement(Order = 16)]
        public string model
        {
            get { return _modelField; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'model' must be used ONLY for vendor offer");

                _modelField = value;
            }
        }

        private string _description;
        /// <summary>
        /// description of product (max length 175)
        /// </summary>
        [XmlElement(Order = 17)]
        public string description
        {
            get { return _description; }
            set
            {
                if (value.Length > 175)
                    throw new ArgumentException("max description length 175");

                _description = value;
            }
        }

        private string _salesNotes;
        /// <summary>
        /// the minimum order amount, minimum consignment, prepayment required (element required) (max length 50)
        /// </summary>
        [XmlElement(Order = 18)]
        public string sales_notes
        {
            get { return _salesNotes; }
            set
            {
                if (value.Length > 50)
                    throw new ArgumentException("max sales notes length 50");

                _salesNotes = value;
            }
        }

        /// <summary>
        /// has official manufacturer warranty, bool
        /// </summary>
        [XmlElement(Order = 19)]
        public string manufacturer_warranty  
        {
            get
            {
                return manufacturerWarrantyField.HasValue ? manufacturerWarrantyField.ToString() : null; 
            }
            set
            {
                manufacturerWarrantyField = bool.Parse(value);
            }
        }

        [XmlIgnore]
        public bool? manufacturerWarrantyField;

        /// <summary>
        /// country of manufacturer
        /// https://yandex.st/market-export/97.0516af5f/partner/help/Countries.pdf
        /// </summary>
        [XmlElement(Order = 20)]
        public string country_of_origin;

        /// <summary>
        /// Downloadable product, bool
        /// <para>ONLY for vendor offer</para>
        /// </summary>
        [XmlElement(Order = 21)]
        public string downloadable
        {
            get
            {
                return downloadableField.HasValue ? downloadableField.ToString() : null;
            }
            set
            {
                if (name != null)
                    throw new ArgumentException("'downloadable' must be used ONLY for vendor offer");

                downloadableField = bool.Parse(value);
            }
        }

        [XmlIgnore]
        public bool? downloadableField;

        /// <summary>
        /// sex related product
        /// </summary>
        [XmlElement(Order = 22)]
        public string adult;

        /// <summary>
        /// age category of goods
        /// </summary>
        [XmlElement(Order = 23)]
        public age age;

        /// <summary>
        /// array of manufacturer barcodes
        /// </summary>
        [XmlElement("barcode", Order = 24)]
        public string[] barcode;

        /// <summary>
        /// can be purchased in yandex market (1 - true / 0 - false)
        /// </summary>
        [XmlElement(Order = 25)]
        public string cpa;

        private string _rec;
        /// <summary>
        /// recommended products for purchase with this
        /// <para>ONLY for vendor offer</para>
        /// </summary>
        [XmlElement(Order = 26)]
        public string rec
        {
            get { return _rec; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'rec' must be used ONLY for vendor offer");

                _rec = value;
            }
        }

        private string _expiry;
        /// <summary>
        /// element indicates the expiration date / service life, ISO8601
        /// <para>ONLY for vendor offer</para>
        /// </summary>
        [XmlElement(Order = 27)]
        public string expiry
        {
            get { return _expiry; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'expiry' must be used ONLY for vendor offer");

                _expiry = value;
            }
        }

        private string _weight;
        /// <summary>
        /// weight of product with packaging (kilograms, ex: 2.07)
        /// <para>ONLY for vendor offer</para>
        /// </summary>
        [XmlElement(Order = 28)]
        public string weight
        {
            get { return _weight; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'weight' must be used ONLY for vendor offer");

                _weight = value;
            }
        }

        private string _dimensions;
        /// <summary>
        /// dimensions of product with packaging (centimeters, ex: 100/25.45/11.112)
        /// <para>ONLY for vendor offer</para>
        /// </summary>
        [XmlElement(Order = 29)]
        public string dimensions
        {
            get { return _dimensions; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'dimensions' must be used ONLY for vendor offer");

                _dimensions = value;
            }
        }

        /// <summary>
        /// array of product specifications
        /// </summary>
        [XmlElement("param", Order = 30)]
        public param[] param;
    }
}