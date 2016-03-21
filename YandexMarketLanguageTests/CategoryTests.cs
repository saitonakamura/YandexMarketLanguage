using System;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class CategoryTests : BasicTests
    {
        [Test]
        public void Category_GivenAttributesAndConvertedToXDocument_PersistValues()
        {
            var category = new category(1, "Книги");

            var xCategory = new YmlSerializer().ToXDocument(category).Root;

            xCategory.Should().NotBeNull();
            xCategory.Should().HaveAttribute("id", "1");
            xCategory.Should().HaveValue("Книги");
        }

        [Test]
        public void Category_GivenAttributesWithParentCategoryAndConvertedToXDocument_PersistValues()
        {
            var category = new category(2, "Детективы", 1);

            var xCategory = new YmlSerializer().ToXDocument(category).Root;

            xCategory.Should().NotBeNull();
            xCategory.Should().HaveAttribute("id", "2");
            xCategory.Should().HaveAttribute("parentId", "1");
            xCategory.Should().HaveValue("Детективы");
        }

        [Test]
        public void CategoryId_GivenNegativeValue_ThrowsArgumentException()
        {
            var category = new category(1, "Детективы");

            Call(() => { category.id = -1; }).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void CategoryId_GivenZeroValue_ThrowsArgumentException()
        {
            var category = new category(1, "Детективы");

            Call(() => { category.id = 0; }).ShouldThrow<ArgumentException>();
        }

        [Test] 
        public void CategoryName_GivenNullString_ThrowsArgumentException()
        {
            var category = new category(1, "Детективы");

            Call(() => { category.name = null; }).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void CategoryName_GivenEmptyString_ThrowsArgumentException()
        {
            var category = new category(1, "Детективы");

            Call(() => { category.name = ""; }).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void CategoryName_GivenWhitespaceString_ThrowsArgumentException()
        {
            var category = new category(1, "Детективы");

            Call(() => { category.name = "     "; }).ShouldThrow<ArgumentException>();
        }
    }
}
