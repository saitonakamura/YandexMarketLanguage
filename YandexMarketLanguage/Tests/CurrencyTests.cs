using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguage.Tests
{
    [TestFixture]
    public class CurrencyTests
    {
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
    }
}
