using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class category
    {
        /// <summary>
        /// DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public category() { }

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
            parentIdField = _parentCategory.id;
        }

        public category(int _id, string _name, int _parentId)
            : this(_id, _name)
        {
            parentIdField = _parentId;
        }

        [XmlAttribute]
        public int id;

        // ReSharper disable once MergeConditionalExpression
        [XmlAttribute]
        public string parentId { get { return parentIdField.HasValue ? parentIdField.Value.ToString() : null; } set { parentIdField = int.Parse(value); } }

        [XmlIgnore]
        public int? parentIdField;

        [XmlText]
        public string name;
    }
}