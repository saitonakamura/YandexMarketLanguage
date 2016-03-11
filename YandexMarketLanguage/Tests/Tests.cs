using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage.ObjectMapping;
// ReSharper disable UseCollectionCountProperty

namespace YandexMarketLanguage.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var ymlCatalog = new yml_catalog(
                new DateTime(2010, 04, 01, 17, 05, 00),
                new shop("BestShop",
                    "Best online seller Inc.",
                    "http://best.seller.ru/",
                    new[]
                    {
                        new currency(CurrencyEnum.RUR, RateEnum.СВ),
                        new currency(CurrencyEnum.EUR, RateEnum.СВ),
                    },
                    new []
                    {
                        new category(1, "Книги"), 
                        new category(2, "Детективы", 1), 
                    },
                    new []
                    {
                        new delivery_option(300, 1), 
                        new delivery_option(0, 5, 7, 14), 
                    },
                    new []
                    {
                        new offer("12346", 600, CurrencyEnum.USD, 1, "Наручные часы Casio A1234567B"),
                        new offer("12341", 16800, CurrencyEnum.RUR, 2, "Принтер НP Deskjet D2663"),
                    }));
            var serializer = new Serializer();

            var doc = serializer.Serialize(ymlCatalog);

            //Assert.NotNull(doc.Declaration.);
            doc.Should().HaveRoot("yml_catalog");
            doc.Root.Should().HaveAttribute("date", "2010-04-01 17:05");
            doc.Root.Should().HaveElement("shop");

            // ReSharper disable once PossibleNullReferenceException
            var shop = doc.Root.Element("shop");
            shop.Should().HaveElement("currencies").Which.Should().HaveElement("currency");
            // ReSharper disable once PossibleNullReferenceException
            var currencies = shop.Element("currencies").Elements("currency").ToList();
            currencies.Count().Should().Be(2);

            foreach (var currency in currencies)
            {
                Assert.NotNull(currency.Attribute("id"), "currency.Attribute('id') != null");
            }

            foreach (var currency in ymlCatalog.shop.currencies)
            {
                var xElement = currencies.Single(x => x.Attribute("id").Value == currency.id.ToString());
                xElement.Should().HaveAttribute("id", currency.id.ToString());
                xElement.Should().HaveAttribute("rate", currency.rate);
            }

            shop.Should().HaveElement("categories").Which.Should().HaveElement("category");
            // ReSharper disable once PossibleNullReferenceException
            var categories = shop.Element("categories").Elements("category").ToList();
            categories.Count().Should().Be(2);
            foreach (var category in categories)
            {
                Assert.NotNull(category.Attribute("id"), "category.Attribute('id') != null");
            }

            var category1 = categories.SingleOrDefault(x => x.Attribute("id").Value == 1.ToString());
            Assert.NotNull(category1, "category1 != null");

            var category2 = categories.SingleOrDefault(x => x.Attribute("id").Value == 2.ToString());
            Assert.NotNull(category2, "category2 != null");

            shop.Should().HaveElement("delivery-options").Which.Should().HaveElement("option");
            // ReSharper disable once PossibleNullReferenceException
            var delivery_options = shop.Element("delivery-options").Elements("option").ToList();
            delivery_options.Count().Should().Be(2);
            foreach (var deliveryOption in delivery_options)
            {
                Assert.NotNull(deliveryOption.Attribute("cost"), "delivery_option.Attribute('cost') != null");
            }

            var deliveryOption1 = delivery_options.SingleOrDefault(x => x.Attribute("cost").Value == 300.ToString());
            Assert.NotNull(deliveryOption1, "deliveryOption1 != null");

            var deliveryOption2 = delivery_options.SingleOrDefault(x => x.Attribute("cost").Value == 0.ToString());
            Assert.NotNull(deliveryOption2, "deliveryOption2 != null");

            shop.Should().HaveElement("offers").Which.Should().HaveElement("offer");
            // ReSharper disable once PossibleNullReferenceException
            var offers = shop.Element("offers").Elements("offer").ToList();
            offers.Count().Should().Be(2);
            foreach (var offer in offers)
            {
                Assert.NotNull(offer.Attribute("id"), "offer.Attribute('id') != null");
            }

            var offer1 = offers.SingleOrDefault(x => x.Attribute("id").Value == 12346.ToString());
            Assert.NotNull(offer1, "offer1 != null");

            var offer2 = offers.SingleOrDefault(x => x.Attribute("id").Value == 12341.ToString());
            Assert.NotNull(offer2, "offer2 != null");

            var wr = new StringWriter();
            doc.Save(wr);
            var str3 = wr.GetStringBuilder().ToString();
            Assert.IsNotEmpty(str3);
        }

        [Test]
        public void TestCategory1()
        {
            var category = new category(1, "Книги");

            var xCategory = new Serializer().Serialize(category).Root;

            Assert.NotNull(xCategory, "xCategory != null");
            xCategory.Should().HaveAttribute("id", "1");
            xCategory.Should().HaveValue("Книги");
        }

        [Test]
        public void TestCategory2()
        {
            var category = new category(2, "Детективы", 1);

            var xCategory = new Serializer().Serialize(category).Root;

            Assert.NotNull(xCategory, "xCategory != null");
            xCategory.Should().HaveAttribute("id", "2");
            xCategory.Should().HaveAttribute("parentId", "1");
            xCategory.Should().HaveValue("Детективы");
        }

        [Test]
        public void TestDeliveryOption1()
        {
            var deliveryOption = new delivery_option(300, 1);

            var xDeliveryOption = new Serializer().Serialize(deliveryOption).Root;

            Assert.NotNull(xDeliveryOption, "xDeliveryOption != null");
            xDeliveryOption.Should().HaveAttribute("cost", "300");
            xDeliveryOption.Should().HaveAttribute("days", "1");
        }

        [Test]
        public void TestDeliveryOption2()
        {
            var deliveryOption = new delivery_option(0, 5, 7, 14);

            var xDeliveryOption = new Serializer().Serialize(deliveryOption).Root;

            Assert.NotNull(xDeliveryOption, "xDeliveryOption != null");
            xDeliveryOption.Should().HaveAttribute("cost", "0");
            xDeliveryOption.Should().HaveAttribute("days", "5-7");
            xDeliveryOption.Should().HaveAttribute("order_before", "14");
        }

        [Test]
        public void TestShop()
        {
            var shop = new shop("BestShop",
                "Best online seller Inc.",
                "http://best.seller.ru/",
                new[]
                {
                    new currency(CurrencyEnum.RUR, RateEnum.СВ),
                    new currency(CurrencyEnum.EUR, RateEnum.СВ),
                },
                new[]
                {
                    new category(1, "Книги"),
                    new category(2, "Детективы", 1),
                },
                new[]
                {
                    new delivery_option(300, 1),
                    new delivery_option(0, 5, 7, 14),
                },
                new[]
                {
                    new offer("12346", 600, CurrencyEnum.USD, 1, "Наручные часы Casio A1234567B"),
                    new offer("12341", 16800, CurrencyEnum.RUR, 2, "Принтер НP Deskjet D2663"),
                })
            {
                platform = "CMS",
                version = "2.3",
                agency = "Agency",
                email = "CMS@CMS.ru",
                cpa = "0",
            };

            var xShop = new Serializer().Serialize(shop).Root;

            Assert.NotNull(xShop, "xShop != null");
            xShop.Should().HaveElement("name").Which.Should().HaveValue("BestShop");
            xShop.Should().HaveElement("company").Which.Should().HaveValue("Best online seller Inc.");
            xShop.Should().HaveElement("url").Which.Should().HaveValue("http://best.seller.ru/");

            xShop.Should().HaveElement("platform").Which.Should().HaveValue("CMS");
            xShop.Should().HaveElement("version").Which.Should().HaveValue("2.3");
            xShop.Should().HaveElement("agency").Which.Should().HaveValue("Agency");
            xShop.Should().HaveElement("email").Which.Should().HaveValue("CMS@CMS.ru");
            xShop.Should().HaveElement("cpa").Which.Should().HaveValue("0");

            xShop.Should().HaveElement("currencies").Which.Should().HaveElement("currency");
        }

        [Test]
        public void TestOffer1()
        {
            var _offer = new offer("12346", 600, CurrencyEnum.USD, 1, "Наручные часы Casio A1234567B");

            var xOffer = new Serializer().Serialize(_offer).Root;

            Assert.NotNull(xOffer, "xOffer != null");
            xOffer.Should().HaveAttribute("id", "12346");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("600");
            xOffer.Should().HaveElement("currency").Which.Should().HaveValue(CurrencyEnum.USD.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(1.ToString());
            xOffer.Should().HaveElement("name").Which.Should().HaveValue("Наручные часы Casio A1234567B");
        }

        [Test]
        public void TestOffer2()
        {
            var offer = new offer("12341", 16800, CurrencyEnum.RUR, 2, "Принтер НP Deskjet D2663");

            var xOffer = new Serializer().Serialize(offer).Root;

            Assert.NotNull(xOffer, "xOffer != null");
            xOffer.Should().HaveAttribute("id", "12341");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("16800");
            xOffer.Should().HaveElement("currency").Which.Should().HaveValue(CurrencyEnum.RUR.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(2.ToString());
            xOffer.Should().HaveElement("name").Which.Should().HaveValue("Принтер НP Deskjet D2663");
        }

    }
}