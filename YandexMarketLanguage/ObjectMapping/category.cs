using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class category
    {
        private int? _parentId;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public category() {}

        public category(int _id, string _name)
        {
            if (_id <= 0)
                throw new ArgumentException("id must be > 0");

            if (string.IsNullOrWhiteSpace(_name))
                throw new ArgumentException("name must not be empty");

            id = _id;
            name = _name;
        }

        public category(int _id, string _name, category _parentCategory)
            : this(_id, _name)
        {
            _parentId = _parentCategory.id;
        }

        public category(int _id, string _name, int _parentId)
            : this(_id, _name)
        {
            this._parentId = _parentId;
        }

        [XmlAttribute]
        public int id { get; set; }

        // ReSharper disable once MergeConditionalExpression
        [XmlAttribute]
        public string parentId
        {
            get { return _parentId.HasValue ? _parentId.Value.ToString() : null; }
            set { _parentId = int.Parse(value); }
        }

        [XmlText]
        public string name { get; set; }
    }
}