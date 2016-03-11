using System;
using System.IO;
using System.Reflection;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguage.Tests.IntegrationTests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void Test()
        {
            var shop = new shop("BestShop",
                "Best online seller Inc.",
                "http://best.seller.ru/",
                new[]
                {
                    new currency(CurrencyEnum.RUR, 1),
                    new currency(CurrencyEnum.EUR, RateEnum.CBRF),
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
                    new offer("12346", 600, CurrencyEnum.EUR, 1, "Наручные часы Casio A1234567B"),
                    new offer("12341", 16800, CurrencyEnum.RUR, 2, "Принтер НP Deskjet D2663"),
                })
            {
                platform = "CMS",
                version = "2.3",
                agency = "Agency",
                email = "CMS@CMS.ru",
                cpa = "0",
            };

            var ymlCatalog = new yml_catalog(new DateTime(2010, 04, 01, 17, 05, 00), shop);
            var serializer = new YmlSerializer();

            var doc = serializer.Serialize(ymlCatalog);

            string xmlStringFromObject;

            using (var wr = new Utf8StringWriter())
            {
                doc.Save(wr);
                xmlStringFromObject = wr.GetStringBuilder().ToString();
            }

            xmlStringFromObject.Should().NotBeNullOrWhiteSpace();

            string xmlStringStandart;

            // ReSharper disable once AssignNullToNotNullAttribute
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("YandexMarketLanguage.Tests.IntegrationTests.standart.xml"))
            using (var reader = new StreamReader(stream))
            {
                xmlStringStandart = reader.ReadToEnd();
            }

            xmlStringStandart.Should().NotBeNullOrWhiteSpace();

            xmlStringFromObject.ShouldBeEquivalentTo(xmlStringStandart);
        }

        public sealed class Utf8StringWriter : StringWriter
        {
            // ReSharper disable once ConvertPropertyToExpressionBody
            public override Encoding Encoding { get { return Encoding.UTF8; } }
        }
    }
}