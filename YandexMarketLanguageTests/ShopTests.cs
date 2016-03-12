﻿using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

// ReSharper disable UseCollectionCountProperty
// ReSharper disable RedundantArgumentNameForLiteralExpression
// ReSharper disable PossibleNullReferenceException

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class ShopTests
    {
        [Test]
        public void TestShopSimpleAttributes()
        {
            var shop = new shop("BestShop",
                "Best online seller Inc.",
                "http://best.seller.ru/",
                new currency[0],
                new category[0],
                new delivery_option[0],
                new offer[0])
            {
                platform = "CMS",
                version = "2.3",
                agency = "Agency",
                email = "CMS@CMS.ru",
                cpa = "0",
            };

            var xShop = new YmlSerializer().ToXDocument(shop).Root;

            xShop.Should().NotBeNull();

            xShop.Should().HaveElement("name").Which.Should().HaveValue("BestShop");
            xShop.Should().HaveElement("company").Which.Should().HaveValue("Best online seller Inc.");
            xShop.Should().HaveElement("url").Which.Should().HaveValue("http://best.seller.ru/");

            xShop.Should().HaveElement("platform").Which.Should().HaveValue("CMS");
            xShop.Should().HaveElement("version").Which.Should().HaveValue("2.3");
            xShop.Should().HaveElement("agency").Which.Should().HaveValue("Agency");
            xShop.Should().HaveElement("email").Which.Should().HaveValue("CMS@CMS.ru");
            xShop.Should().HaveElement("cpa").Which.Should().HaveValue("0");

            xShop.Should().HaveElement("currencies");
            xShop.Should().HaveElement("categories");
            xShop.Should().HaveElement("delivery-options");
            xShop.Should().HaveElement("offers");
        }

        [Test]
        public void TestShopCurrencies()
        {
            var shop = new shop("BestShop", "Best online seller Inc.", "http://best.seller.ru/",
                new[]
                {
                    new currency(CurrencyEnum.RUR, 1),
                    new currency(CurrencyEnum.EUR, RateEnum.CBRF),
                }, 
                new category[0], new delivery_option[0], new offer[0]);

            var xShop = new YmlSerializer().ToXDocument(shop).Root;

            xShop.Should().NotBeNull();

            xShop.Should().HaveElement("currencies").Which.Should().HaveElement("currency");

            // ReSharper disable once PossibleNullReferenceException
            var currencies = Enumerable.ToList<XElement>(xShop.Element("currencies").Elements("currency"));
            currencies.Count().Should().Be(2);

            foreach (var currency in currencies)
            {
                currency.Attribute("id").Should().NotBeNull();
            }

            currencies.SingleOrDefault(x => x.Attribute("id").Value == CurrencyEnum.RUR.ToString()).Should().NotBeNull();
            currencies.SingleOrDefault(x => x.Attribute("id").Value == CurrencyEnum.EUR.ToString()).Should().NotBeNull();
        }

        [Test]
        public void TestShopCategories()
        {
            var shop = new shop("BestShop", "Best online seller Inc.", "http://best.seller.ru/", new currency[0],
                new[]
                {
                    new category(1, "Книги"),
                    new category(2, "Детективы", 1),
                },
                new delivery_option[0], new offer[0]);

            var xShop = new YmlSerializer().ToXDocument(shop).Root;

            xShop.Should().NotBeNull();

            xShop.Should().HaveElement("categories").Which.Should().HaveElement("category");
            // ReSharper disable once PossibleNullReferenceException
            var categories = Enumerable.ToList<XElement>(xShop.Element("categories").Elements("category"));
            categories.Count().Should().Be(2);

            foreach (var category in categories)
            {
                category.Attribute("id").Should().NotBeNull();
            }

            categories.SingleOrDefault(x => x.Attribute("id").Value == 1.ToString()).Should().NotBeNull();
            categories.SingleOrDefault(x => x.Attribute("id").Value == 2.ToString()).Should().NotBeNull();
        }

        [Test]
        public void TestShopDeliveryOptions()
        {
            var shop = new shop("BestShop", "Best online seller Inc.", "http://best.seller.ru/", new currency[0], new category[0],
                new[]
                {
                    new delivery_option(300, 1),
                    new delivery_option(0, 5, 7, 14),
                },
                new offer[0]);

            var xShop = new YmlSerializer().ToXDocument(shop).Root;

            xShop.Should().NotBeNull();

            xShop.Should().HaveElement("delivery-options").Which.Should().HaveElement("option");
            var delivery_options = Enumerable.ToList<XElement>(xShop.Element("delivery-options").Elements("option"));
            delivery_options.Count().Should().Be(2);

            foreach (var deliveryOption in delivery_options)
            {
                deliveryOption.Attribute("cost").Should().NotBeNull();
            }

            delivery_options.SingleOrDefault(x => x.Attribute("cost").Value == 300.ToString()).Should().NotBeNull();
            delivery_options.SingleOrDefault(x => x.Attribute("cost").Value == 0.ToString()).Should().NotBeNull();
        }

        [Test]
        public void TestShopOffers()
        {
            var shop = new shop("BestShop", "Best online seller Inc.", "http://best.seller.ru/", new currency[0], new category[0], new delivery_option[0], 
                new[]
                {
                    new offer("12346", 600, CurrencyEnum.EUR, 1, "Наручные часы Casio A1234567B"),
                    new offer("12341", 16800, CurrencyEnum.RUR, 2, "Принтер НP Deskjet D2663"),
                });

            var xShop = new YmlSerializer().ToXDocument(shop).Root;

            xShop.Should().HaveElement("offers").Which.Should().HaveElement("offer");
            var offers = Enumerable.ToList<XElement>(xShop.Element("offers").Elements("offer"));
            offers.Count().Should().Be(2);
            foreach (var offer in offers)
            {
                Assert.NotNull(offer.Attribute("id"), "offer.Attribute('id') != null");
            }

            offers.SingleOrDefault(x => x.Attribute("id").Value == 12346.ToString()).Should().NotBeNull();
            offers.SingleOrDefault(x => x.Attribute("id").Value == 12341.ToString()).Should().NotBeNull();
        }

        

    }
}