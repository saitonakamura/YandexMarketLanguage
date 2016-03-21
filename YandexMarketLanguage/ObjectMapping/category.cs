using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
    public class category
    {
        private int? _parentId;
        private int _id;
        private string _name;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public category() {}

        public category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public category(int id, string name, category parentCategory)
            : this(id, name)
        {
            this._parentId = parentCategory.id;
        }

        public category(int id, string name, int parentId)
            : this(id, name)
        {
            this._parentId = parentId;
        }

        [XmlAttribute]
        public int id
        {
            get { return _id; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("id must be > 0");

                _id = value;
            }
        }

        // ReSharper disable once MergeConditionalExpression
        [XmlAttribute]
        public string parentId
        {
            get { return _parentId.HasValue ? _parentId.Value.ToString() : null; }
            set { _parentId = int.Parse(value); }
        }

        [XmlText]
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
    }
}