using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage.ObjectMapping;
// ReSharper disable UseCollectionCountProperty
// ReSharper disable RedundantArgumentNameForLiteralExpression
// ReSharper disable PossibleNullReferenceException

namespace YandexMarketLanguage.Tests
{
    [TestFixture]
    public class Tests
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

            var xYmlCatalog = serializer.Serialize(ymlCatalog);

            xYmlCatalog.Should().NotBeNull();

            xYmlCatalog.Should().HaveRoot("yml_catalog");
            xYmlCatalog.Root.Should().HaveAttribute("date", "2010-04-01 17:05");
            xYmlCatalog.Root.Should().HaveElement("shop");
        }

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

            var xShop = new YmlSerializer().Serialize(shop).Root;

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

            var xShop = new YmlSerializer().Serialize(shop).Root;

            xShop.Should().NotBeNull();

            xShop.Should().HaveElement("currencies").Which.Should().HaveElement("currency");

            // ReSharper disable once PossibleNullReferenceException
            var currencies = xShop.Element("currencies").Elements("currency").ToList();
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

            var xShop = new YmlSerializer().Serialize(shop).Root;

            xShop.Should().NotBeNull();

            xShop.Should().HaveElement("categories").Which.Should().HaveElement("category");
            // ReSharper disable once PossibleNullReferenceException
            var categories = xShop.Element("categories").Elements("category").ToList();
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

            var xShop = new YmlSerializer().Serialize(shop).Root;

            xShop.Should().NotBeNull();

            xShop.Should().HaveElement("delivery-options").Which.Should().HaveElement("option");
            var delivery_options = xShop.Element("delivery-options").Elements("option").ToList();
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

            var xShop = new YmlSerializer().Serialize(shop).Root;

            xShop.Should().HaveElement("offers").Which.Should().HaveElement("offer");
            var offers = xShop.Element("offers").Elements("offer").ToList();
            offers.Count().Should().Be(2);
            foreach (var offer in offers)
            {
                Assert.NotNull(offer.Attribute("id"), "offer.Attribute('id') != null");
            }

            offers.SingleOrDefault(x => x.Attribute("id").Value == 12346.ToString()).Should().NotBeNull();
            offers.SingleOrDefault(x => x.Attribute("id").Value == 12341.ToString()).Should().NotBeNull();
        }

        [Test]
        public void TestCurrencyWithNumericRate()
        {
            var currency = new currency(CurrencyEnum.RUR, 1);

            var xCurrency = new YmlSerializer().Serialize(currency).Root;

            xCurrency.Should().NotBeNull();
            xCurrency.Should().HaveAttribute("id", CurrencyEnum.RUR.ToString());
            xCurrency.Should().HaveAttribute("rate", 1.ToString());
        }

        [Test]
        public void TestCurrencyWithEnumRate()
        {
            var currency = new currency(CurrencyEnum.EUR, RateEnum.CBRF);

            var xCurrency = new YmlSerializer().Serialize(currency).Root;

            xCurrency.Should().NotBeNull();
            xCurrency.Should().HaveAttribute("id", CurrencyEnum.EUR.ToString());
            xCurrency.Should().HaveAttribute("rate", RateEnum.CBRF.ToString());
        }

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

        [Test]
        public void TestDeliveryOptionWithDays()
        {
            var deliveryOption = new delivery_option(300, 1);

            var xDeliveryOption = new YmlSerializer().Serialize(deliveryOption).Root;

            xDeliveryOption.Should().NotBeNull();
            xDeliveryOption.Should().HaveAttribute("cost", "300");
            xDeliveryOption.Should().HaveAttribute("days", "1");
        }

        [Test]
        public void TestDeliveryOptionWithDaysPeriodAndOrderBefore()
        {
            var deliveryOption = new delivery_option(cost: 0, workDaysFrom: 5, workDaysTo: 7, order_before: 14);

            var xDeliveryOption = new YmlSerializer().Serialize(deliveryOption).Root;

            xDeliveryOption.Should().NotBeNull();
            xDeliveryOption.Should().HaveAttribute("cost", "0");
            xDeliveryOption.Should().HaveAttribute("days", "5-7");
            xDeliveryOption.Should().HaveAttribute("order-before", "14");
        }

        [Test]
        public void TestOffer1()
        {
            var _offer = new offer("12346", 600, CurrencyEnum.USD, 1, "Наручные часы Casio A1234567B");

            var xOffer = new YmlSerializer().Serialize(_offer).Root;

            xOffer.Should().NotBeNull();
            xOffer.Should().HaveAttribute("id", "12346");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("600");
            xOffer.Should().HaveElement("currencyId").Which.Should().HaveValue(CurrencyEnum.USD.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(1.ToString());
            xOffer.Should().HaveElement("name").Which.Should().HaveValue("Наручные часы Casio A1234567B");
        }

        [Test]
        public void TestOffer2()
        {
            var offer = new offer("12341", 16800, CurrencyEnum.RUR, 2, "Принтер НP Deskjet D2663");

            var xOffer = new YmlSerializer().Serialize(offer).Root;

            xOffer.Should().NotBeNull();
            xOffer.Should().HaveAttribute("id", "12341");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("16800");
            xOffer.Should().HaveElement("currencyId").Which.Should().HaveValue(CurrencyEnum.RUR.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(2.ToString());
            xOffer.Should().HaveElement("name").Which.Should().HaveValue("Принтер НP Deskjet D2663");
        }

    }
}