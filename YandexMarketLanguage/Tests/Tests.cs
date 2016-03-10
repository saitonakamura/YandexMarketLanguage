using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage.ObjectMapping;

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
                    })
                {
                    platform = "CMS",
                    version = "2.3",
                    agency = "Agency",
                    email = "CMS@CMS.ru",
                    cpa = "0",
                });
            var serializer = new Serializer();

            var doc = serializer.Serialize(ymlCatalog);

            /*var str = doc.ToString();

            var wr = new StringWriter();
            doc.Save(wr);

            var str3 = wr.GetStringBuilder().ToString();*/

            //Assert.NotNull(doc.Declaration.);
            doc.Should().HaveRoot("yml_catalog");
            doc.Root.Should().HaveAttribute("date", "2010-04-01 17:05");
            doc.Root.Should().HaveElement("shop");

            // ReSharper disable once PossibleNullReferenceException
            var shop = doc.Root.Element("shop");
            shop.Should().HaveElement("name");
            // ReSharper disable once PossibleNullReferenceException
            shop.Element("name").Should().HaveValue("BestShop");
            shop.Should().HaveElement("company");
            shop.Element("company").Should().HaveValue("Best online seller Inc.");
            shop.Should().HaveElement("url");
            shop.Element("url").Should().HaveValue("http://best.seller.ru/");

            shop.Should().HaveElement("platform").Which.Should().HaveValue("CMS");
            shop.Should().HaveElement("version").Which.Should().HaveValue("2.3");
            shop.Should().HaveElement("agency").Which.Should().HaveValue("Agency");
            shop.Should().HaveElement("email").Which.Should().HaveValue("CMS@CMS.ru");
            shop.Should().HaveElement("cpa").Which.Should().HaveValue("0");

            shop.Should().HaveElement("currencies")
                .Which.Should().HaveElement("currency");
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
        }
    }
}