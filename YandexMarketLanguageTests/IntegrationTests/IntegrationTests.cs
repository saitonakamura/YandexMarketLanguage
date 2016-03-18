using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;
// ReSharper disable RedundantArgumentNameForLiteralExpression
// ReSharper disable RedundantArgumentName

namespace YandexMarketLanguageTests.IntegrationTests
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
                    new currency(CurrencyEnum.RUR, _rate: 1),
                    new currency(CurrencyEnum.EUR, RateEnum.CBRF),
                },
                new[]
                {
                    new category(_id: 1, _name: "Книги"),
                    new category(_id: 2, _name: "Детективы", _parentId: 1),
                },
                new[]
                {
                    new delivery_option(_cost: 300, _workDays: 1),
                    new delivery_option(_cost: 0, _workDaysFrom: 5, _workDaysTo: 7, _orderBefore: 14),
                },
                new[]
                {
                    new offer(_id: "12346", _price: 600, _currencyId: CurrencyEnum.EUR, _categoryId: 1, _name: "Наручные часы Casio A1234567B"),
                    new offer(_id: "12341", _price: 16800, _currencyId: CurrencyEnum.RUR, _categoryId: 2, _typePrefix: "Принтер", _vendor: "HP", _model: "Deskjet D2663")
                    {
                        vendorCode = "CH366C",
                        param = new[]
                        {
                            new param("Максимальный формат", "A4"),
                            new param("Максимальное разрешение для ч/б печати", "600x600", "dpi"), 
                            new param("Плотность бумаги", "75-280", "г/м2"), 
                        },
                    },
                })
            {
                platform = "CMS",
                version = "2.3",
                agency = "Agency",
                email = "CMS@CMS.ru",
                cpa = "0",
            };

            var ymlCatalog = new yml_catalog(new DateTime(2010, 04, 01, 17, 05, 00), shop);

            var xmlStringFromObject = new YmlSerializer().ToXmlString(ymlCatalog);

            xmlStringFromObject.Should().NotBeNullOrWhiteSpace();

            var xmlStringStandart = ReadXmlFromAssembly();

            xmlStringStandart.Should().NotBeNullOrWhiteSpace();

            xmlStringFromObject.ShouldBeEquivalentTo(xmlStringStandart);
        }
        
        private static string ReadXmlFromAssembly()
        {
            string xmlStringStandart;

            // ReSharper disable once AssignNullToNotNullAttribute
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("YandexMarketLanguageTests.IntegrationTests.standart.xml"))
            {
                using (var reader = new StreamReader(stream))
                {
                    xmlStringStandart = reader.ReadToEnd();
                }
            }

            return xmlStringStandart;
        }
    }
}