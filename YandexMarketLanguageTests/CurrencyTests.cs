using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class CurrencyTests : BasicTests
    {
        [Test]
        public void Currency_GivenValuesWithNumericRateAndConvertedToXDocument_PersistsValues()
        {
            var currency = new currency(CurrencyEnum.RUR, 1);

            var xCurrency = new YmlSerializer().ToXDocument(currency).Root;

            xCurrency.Should().NotBeNull();
            xCurrency.Should().HaveAttribute("id", CurrencyEnum.RUR.ToString());
            xCurrency.Should().HaveAttribute("rate", 1.ToString());
        }

        [Test]
        public void Currency_GivenValuesWithEnumRateAndConvertedToXDocument_PersistsValues()
        {
            var currency = new currency(CurrencyEnum.EUR, RateEnum.CBRF);

            var xCurrency = new YmlSerializer().ToXDocument(currency).Root;

            xCurrency.Should().NotBeNull();
            xCurrency.Should().HaveAttribute("id", CurrencyEnum.EUR.ToString());
            xCurrency.Should().HaveAttribute("rate", RateEnum.CBRF.ToString());
        }

        [Test]
        public void Currency_GivenValuesWithEnumRateAndPlusAndConvertedToXDocument_PersistsValues()
        {
            var currency = new currency(CurrencyEnum.EUR, RateEnum.CBRF, plus: 3);

            var xCurrency = new YmlSerializer().ToXDocument(currency).Root;

            xCurrency.Should().NotBeNull();
            xCurrency.Should().HaveAttribute("id", CurrencyEnum.EUR.ToString());
            xCurrency.Should().HaveAttribute("rate", RateEnum.CBRF.ToString());
            xCurrency.Should().HaveAttribute("plus", 3.ToString());
        }
    }
}
