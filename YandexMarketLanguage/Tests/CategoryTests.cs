using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguage.Tests
{
    [TestFixture]
    public class CategoryTests
    {
        [Test]
        public void TestCategoryWithoutParent()
        {
            var category = new category(1, "Книги");

            var xCategory = new YmlSerializer().Serialize(category).Root;

            xCategory.Should().NotBeNull();
            xCategory.Should().HaveAttribute("id", "1");
            xCategory.Should().HaveValue("Книги");
        }

        [Test]
        public void TestCategoryWithParent()
        {
            var category = new category(2, "Детективы", 1);

            var xCategory = new YmlSerializer().Serialize(category).Root;

            xCategory.Should().NotBeNull();
            xCategory.Should().HaveAttribute("id", "2");
            xCategory.Should().HaveAttribute("parentId", "1");
            xCategory.Should().HaveValue("Детективы");
        }
    }
}
