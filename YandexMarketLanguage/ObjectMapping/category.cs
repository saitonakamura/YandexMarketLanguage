using System;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class category
    {
        [Obsolete]
        public category() { }

        public category(int id, string name)
        {
            if (id <= 0)
                throw new ArgumentException("id must be > 0");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("name must not be empty");

            this.id = id;
            this.name = name;
        }

        public category(int id, string name, category parentCategory)
            : this(id, name)
        {
            this.parentIdField = parentCategory.id;
        }

        public category(int id, string name, int parentId)
            : this(id, name)
        {
            this.parentIdField = parentId;
        }

        [XmlAttribute]
        public int id;

        [XmlAttribute]
        public string parentId { get { return parentIdField.HasValue ? parentIdField.Value.ToString() : null; } set { parentIdField = int.Parse(value); } }

        [XmlIgnore]
        public int? parentIdField;

        [XmlText]
        public string name;
    }
}