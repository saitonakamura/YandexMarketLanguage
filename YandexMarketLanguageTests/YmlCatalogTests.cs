using System;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class YmlCatalogTests
    {
        [Test]
        public void TestYmlCatalog()
        {
            var ymlCatalog = new yml_catalog(
                new DateTime(2010, 04, 01, 17, 05, 00),
                new shop("BestShop",
                    "Best online seller Inc.",
                    "http://best.seller.ru/",
                    new currency[0],
                    new category[0],
                    new delivery_option[0],
                    new offer[0]));

            var serializer = new YmlSerializer();

            var xYmlCatalog = serializer.ToXDocument(ymlCatalog);

            xYmlCatalog.Should().NotBeNull();

            xYmlCatalog.Should().HaveRoot("yml_catalog");
            xYmlCatalog.Root.Should().HaveAttribute("date", "2010-04-01 17:05");
            xYmlCatalog.Root.Should().HaveElement("shop");
        }
    }
}
